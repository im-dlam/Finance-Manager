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
    /// Form quản lý và tra cứu Thẻ ngân hàng dành cho Quản trị viên (Admin).
    /// </summary>
    public partial class CreditCardForm : Form
    {
        private readonly CreditService _creditService;
        private List<CreditCardInfo> _adminCards = new List<CreditCardInfo>();
        private CreditCardInfo _selectedAdminCard = null;
        private bool _isUpdatingAdminStatus = false;

        /// <summary>
        /// Khởi tạo một thể hiện mới của CreditCardForm cho Quản trị viên.
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
        /// Đăng ký các sự kiện tương tác của Admin.
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

        /// <summary>
        /// Tải toàn bộ danh sách thẻ ngân hàng của tất cả khách hàng từ SQL Server.
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
        /// Đổ dữ liệu danh sách thẻ vào DataGridView của Admin và chọn thẻ đầu tiên.
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
        /// Thực hiện tìm kiếm thẻ ngân hàng theo từ khóa người dùng nhập vào.
        /// </summary>
        private async Task PerformAdminSearchAsync()
        {
            string keyword = txtAdminSearch.Text.Trim();
            var searchResults = await _creditService.SearchCardsAsync(keyword, null, true);
            BindAdminGrid(searchResults);
        }

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

        private void DgvAdminCards_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvAdminCards.Rows.Count) return;

            // Cột số dư: định dạng tiền tệ
            if (dgvAdminCards.Columns[e.ColumnIndex].Name == "colAdminBalance" && e.Value != null)
            {
                var card = dgvAdminCards.Rows[e.RowIndex].DataBoundItem as CreditCardInfo;
                if (card != null)
                {
                    e.Value = $"{card.Balance:N0} {card.Currency}";
                    e.FormattingApplied = true;
                }
            }

            // Cột trạng thái: tô màu theo trạng thái
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

            // Cột ngày cấp và ngày hết hạn
            if ((dgvAdminCards.Columns[e.ColumnIndex].Name == "colAdminIssueDate" ||
                 dgvAdminCards.Columns[e.ColumnIndex].Name == "colAdminExpiryDate") && e.Value is DateTime dt)
            {
                e.Value = dt.ToString("dd/MM/yyyy");
                e.FormattingApplied = true;
            }
        }

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

            // Cập nhật nút Mở khóa thẻ / Khóa thẻ
            if (isActive)
            {
                btnAdminUnlock.Text = "🔒  Khóa thẻ";
                btnAdminUnlock.BackColor = Color.FromArgb(220, 38, 38); // Màu đỏ
            }
            else
            {
                btnAdminUnlock.Text = "🔓  Mở khóa thẻ";
                btnAdminUnlock.BackColor = Color.FromArgb(22, 163, 74); // Màu xanh lá
            }
        }

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
    }
}
