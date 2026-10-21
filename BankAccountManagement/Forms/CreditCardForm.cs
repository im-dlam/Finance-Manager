using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Form Quản lý thẻ ngân hàng dành cho Quản trị viên (Admin Credit Card Management Form).
    /// <para>
    /// Chức năng chính:
    /// - Hiển thị danh sách toàn bộ thẻ ngân hàng của tất cả khách hàng trong hệ thống (Số thẻ, Số tài khoản, Chủ thẻ, Ngân hàng, Số dư, Hạn sử dụng, Trạng thái).
    /// - Tìm kiếm thẻ thông minh theo từ khóa (Mã KH, Tên khách hàng, Số thẻ, STK, Ngân hàng).
    /// - Xem chi tiết hồ sơ thẻ được chọn bên bảng điều khiển chi tiết bên phải.
    /// - Khóa thẻ (Lock) khẩn cấp hoặc Mở khóa thẻ (Unlock) bất đồng bộ (Task.Run) và cập nhật trực tiếp vào CSDL.
    /// </para>
    /// </summary>
    public partial class CreditCardForm : Form
    {
        #region 1. CÁC BIẾN VÀ DỊCH VỤ

        /// <summary>Dịch vụ xử lý nghiệp vụ thẻ tín dụng và thẻ thanh toán.</summary>
        private readonly CreditService _creditService;

        /// <summary>Danh sách bộ nhớ đệm lưu trữ toàn bộ thẻ ngân hàng trong hệ thống.</summary>
        private List<CreditCardInfo> _adminCards = new List<CreditCardInfo>();

        /// <summary>Đối tượng thẻ hiện đang được chọn hiển thị trên bảng chi tiết.</summary>
        private CreditCardInfo _selectedAdminCard = null;

        /// <summary>Cờ bảo vệ chống bấm liên tiếp nút Khóa / Mở khóa thẻ khi đang gửi yêu cầu CSDL.</summary>
        private bool _isUpdatingAdminStatus = false;

        #endregion

        #region 2. KHỞI TẠO VÀ SỰ KIỆN FORM LOAD

        /// <summary>
        /// Khởi tạo Form quản lý thẻ cho Quản trị viên và đăng ký các sự kiện tương tác.
        /// </summary>
        public CreditCardForm()
        {
            InitializeComponent();
            _creditService = new CreditService();

            RegisterEvents();
            this.Load += CreditCardForm_Load;
        }

        private async void CreditCardForm_Load(object sender, EventArgs e)
        {
            await LoadAdminCardsAsync();
        }

        /// <summary>
        /// Đăng ký các sự kiện thao tác chuột, bàn phím và định dạng ô DataGridView.
        /// </summary>
        private void RegisterEvents()
        {
            dgvAdminCards.AutoGenerateColumns = false;
            dgvAdminCards.CellClick += DgvAdminCards_CellClick;
            dgvAdminCards.SelectionChanged += DgvAdminCards_SelectionChanged;
            dgvAdminCards.CellFormatting += DgvAdminCards_CellFormatting;

            btnAdminUnlock.Click += async (s, e) => await ToggleAdminCardLockAsync();
            btnAdminRefresh.Click += async (s, e) =>
            {
                txtAdminSearch.Clear();
                await LoadAdminCardsAsync();
            };

            btnAdminSearch.Click += async (s, e) => await PerformAdminSearchAsync();
            txtAdminSearch.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    await PerformAdminSearchAsync();
                }
            };
        }

        #endregion

        #region 3. TẢI VÀ TÌM KIẾM DANH SÁCH THẺ (DATA QUERY & SEARCH)

        /// <summary>
        /// Tải toàn bộ danh sách thẻ ngân hàng của tất cả khách hàng từ cơ sở dữ liệu.
        /// </summary>
        private async Task LoadAdminCardsAsync()
        {
            try
            {
                _adminCards = await _creditService.GetCardsAsync(null, true);
                BindAdminGrid(_adminCards);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[CreditCardForm] Lỗi tải dữ liệu thẻ Admin: {ex.Message}");
                _adminCards = _creditService.GetDefaultCards("KHÁCH HÀNG");
                BindAdminGrid(_adminCards);
            }
        }

        /// <summary>
        /// Gán nguồn dữ liệu vào DataGridView và tự động hiển thị thông tin thẻ đầu tiên.
        /// </summary>
        private void BindAdminGrid(List<CreditCardInfo> list)
        {
            dgvAdminCards.DataSource = null;
            dgvAdminCards.DataSource = list;

            lblAdminListCount.Text = $"{list?.Count ?? 0} thẻ ngân hàng";

            if (list != null && list.Count > 0)
            {
                dgvAdminCards.ClearSelection();
                dgvAdminCards.Rows[0].Selected = true;
                DisplayAdminCard(list[0]);
            }
            else
            {
                ClearAdminForm();
            }
        }

        /// <summary>
        /// Thực hiện tìm kiếm danh sách thẻ theo từ khóa nhập vào ô tìm kiếm.
        /// </summary>
        private async Task PerformAdminSearchAsync()
        {
            string keyword = txtAdminSearch.Text.Trim();
            var searchResults = await _creditService.SearchCardsAsync(keyword, null, true);
            BindAdminGrid(searchResults);
        }

        #endregion

        #region 4. TƯƠNG TÁC BẢNG VÀ ĐỊNH DẠNG Ô (DATAGRIDVIEW FORMATTING)

        private void DgvAdminCards_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvAdminCards.Rows.Count)
            {
                var card = dgvAdminCards.Rows[e.RowIndex].DataBoundItem as CreditCardInfo;
                if (card != null)
                {
                    DisplayAdminCard(card);
                }
            }
        }

        private void DgvAdminCards_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAdminCards.SelectedRows.Count > 0)
            {
                var card = dgvAdminCards.SelectedRows[0].DataBoundItem as CreditCardInfo;
                if (card != null && card != _selectedAdminCard)
                {
                    DisplayAdminCard(card);
                }
            }
        }

        /// <summary>
        /// Định dạng số dư tiền tệ, trạng thái Active (xanh lá) / Locked (đỏ), và ngày cấp/hết hạn.
        /// </summary>
        private void DgvAdminCards_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvAdminCards.Rows.Count) return;

            // 1. Cột Số dư
            if (dgvAdminCards.Columns[e.ColumnIndex].Name == "colAdminBalance" && e.Value != null)
            {
                var card = dgvAdminCards.Rows[e.RowIndex].DataBoundItem as CreditCardInfo;
                if (card != null)
                {
                    e.Value = $"{card.Balance:N0} {card.Currency}";
                    e.FormattingApplied = true;
                }
            }

            // 2. Cột Trạng thái thẻ
            if (dgvAdminCards.Columns[e.ColumnIndex].Name == "colAdminStatus" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (string.Equals(status, "Active", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(status, "Đang hoạt động", StringComparison.OrdinalIgnoreCase))
                {
                    e.Value = "Active";
                    e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74);
                    e.CellStyle.Font = new Font(dgvAdminCards.Font, FontStyle.Bold);
                }
                else
                {
                    e.Value = "Locked";
                    e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                    e.CellStyle.Font = new Font(dgvAdminCards.Font, FontStyle.Bold);
                }
                e.FormattingApplied = true;
            }

            // 3. Cột Ngày phát hành và Ngày hết hạn
            if ((dgvAdminCards.Columns[e.ColumnIndex].Name == "colAdminIssueDate" ||
                 dgvAdminCards.Columns[e.ColumnIndex].Name == "colAdminExpiryDate") && e.Value is DateTime dt)
            {
                e.Value = dt.ToString("dd/MM/yyyy");
                e.FormattingApplied = true;
            }
        }

        #endregion

        #region 5. HIỂN THỊ CHI TIẾT & KHÓA/MỞ KHÓA THẺ (CARD DETAIL & LOCK TOGGLE)

        /// <summary>
        /// Hiển thị thông tin chi tiết của thẻ lên bảng điều khiển bên phải.
        /// </summary>
        private void DisplayAdminCard(CreditCardInfo card)
        {
            if (card == null) return;
            _selectedAdminCard = card;

            txtAdminCardNumber.Text = card.FullCardNumber;
            txtAdminAccountNumber.Text = card.AccountNumber;
            txtAdminCustomerCode.Text = card.CustomerId;
            txtAdminCustomerName.Text = card.CardHolderName;
            txtAdminBank.Text = card.BankName;
            txtAdminCardType.Text = card.CardType;
            txtAdminBalance.Text = $"{card.Balance:N0} {card.Currency}";
            txtAdminIssueDate.Text = card.IssueDate.ToString("dd/MM/yyyy");
            txtAdminExpiryDate.Text = card.ExpiryDate.ToString("dd/MM/yyyy");

            bool isActive = card.IsActive;
            txtAdminStatus.Text = isActive ? "Active (Đang hoạt động)" : "Locked (Đã khóa)";
            txtAdminStatus.ForeColor = isActive ? Color.FromArgb(22, 163, 74) : Color.FromArgb(220, 38, 38);

            // Cập nhật giao diện nút bấm: nếu đang hoạt động thì hiện nút Khóa màu đỏ, ngược lại hiện nút Mở khóa màu xanh
            if (isActive)
            {
                btnAdminUnlock.Text = "🔒  Khóa thẻ";
                btnAdminUnlock.BackColor = Color.FromArgb(220, 38, 38);
            }
            else
            {
                btnAdminUnlock.Text = "🔓  Mở khóa thẻ";
                btnAdminUnlock.BackColor = Color.FromArgb(22, 163, 74);
            }
        }

        /// <summary>
        /// Xóa sạch các trường thông tin khi danh sách thẻ trống.
        /// </summary>
        private void ClearAdminForm()
        {
            _selectedAdminCard = null;
            txtAdminCardNumber.Clear();
            txtAdminAccountNumber.Clear();
            txtAdminCustomerCode.Clear();
            txtAdminCustomerName.Clear();
            txtAdminBank.Clear();
            txtAdminCardType.Clear();
            txtAdminBalance.Clear();
            txtAdminIssueDate.Clear();
            txtAdminExpiryDate.Clear();
            txtAdminStatus.Clear();
            btnAdminUnlock.Text = "🔓  Mở khóa thẻ";
            btnAdminUnlock.BackColor = Color.FromArgb(22, 163, 74);
        }

        /// <summary>
        /// Chuyển đổi trạng thái Khóa / Mở khóa thẻ bất đồng bộ và lưu vào CSDL qua CreditService.
        /// </summary>
        private async Task ToggleAdminCardLockAsync()
        {
            if (_isUpdatingAdminStatus || _selectedAdminCard == null) return;

            bool currentIsActive = _selectedAdminCard.IsActive;
            bool newIsLocked = currentIsActive;

            _isUpdatingAdminStatus = true;
            try
            {
                bool ok = await _creditService.UpdateCardStatusAsync(_selectedAdminCard.AccountNumber, newIsLocked);
                if (ok)
                {
                    _selectedAdminCard.Status = newIsLocked ? "Locked" : "Active";
                    DisplayAdminCard(_selectedAdminCard);
                    dgvAdminCards.Refresh();

                    string actionName = newIsLocked ? "Khóa" : "Mở khóa";
                    MessageBox.Show(
                        $"{actionName} thẻ thành công cho khách hàng {_selectedAdminCard.CardHolderName}!\n" +
                        $"• Số thẻ: {_selectedAdminCard.FullCardNumber}\n" +
                        $"• Số tài khoản: {_selectedAdminCard.AccountNumber}\n" +
                        $"• Trạng thái mới: {_selectedAdminCard.Status}",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật trạng thái thẻ. Vui lòng kiểm tra lại kết nối cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi ngoại lệ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isUpdatingAdminStatus = false;
            }
        }

        #endregion
    }
}
