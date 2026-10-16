using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Giao diện Quản lý & Thẩm định Hồ sơ Vay Vốn dành cho Ban Quản Trị (Admin).
    /// - Hiển thị danh sách hồ sơ đăng ký vay trực tuyến
    /// - Thẩm định chi tiết tài khoản: Mã tài khoản, Mã khách hàng, Tên người dùng, Tổng số thu chi trong 1 quý (90 ngày)
    /// - Hàng nút bo tròn trên cùng 1 dòng: "✔ Xác nhận" (Duyệt & Giải ngân) và "✖ Không xác nhận" (Từ chối)
    /// </summary>
    public partial class BorrowingAdminForm : Form
    {
        private readonly BorrowingService _borrowingService;
        private List<BorrowingLoan> _loans = new List<BorrowingLoan>();
        private BorrowingLoan _selectedLoan = null;

        public BorrowingAdminForm()
        {
            InitializeComponent();
            _borrowingService = new BorrowingService();

            SetupDataGridViewColumns();
            WireEvents();

            this.Load += async (s, e) =>
            {
                cboFilterStatus.SelectedIndex = 0; // "Tất cả"
                await LoadLoansAsync();
            };
        }

        private void SetupDataGridViewColumns()
        {
            dgvLoans.AutoGenerateColumns = false;
            dgvLoans.Columns.Clear();
            dgvLoans.ScrollBars = ScrollBars.Vertical;

            dgvLoans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaKV",
                HeaderText = "Mã hồ sơ",
                DataPropertyName = "MaKhoanVay",
                Width = 95
            });

            dgvLoans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colKH",
                HeaderText = "Khách hàng",
                DataPropertyName = "HoTen",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 110
            });

            dgvLoans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSTK",
                HeaderText = "Số tài khoản",
                DataPropertyName = "SoTaiKhoan",
                Width = 95
            });

            dgvLoans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoTien",
                HeaderText = "Số tiền vay",
                DataPropertyName = "FormattedLoanAmount",
                Width = 90
            });

            dgvLoans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colThoiGian",
                HeaderText = "Kỳ hạn",
                DataPropertyName = "FormattedTerm",
                Width = 60
            });

            dgvLoans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTongTra",
                HeaderText = "Tổng phải trả",
                DataPropertyName = "FormattedTotalPay",
                Width = 95
            });

            dgvLoans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNgayDK",
                HeaderText = "Ngày ĐK",
                DataPropertyName = "FormattedRegisterDate",
                Width = 75
            });

            dgvLoans.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTrangThai",
                HeaderText = "Trạng thái",
                DataPropertyName = "TrangThai",
                Width = 95
            });
        }

        private void WireEvents()
        {
            pnlAppraisalScroll.Resize += (s, e) =>
            {
                pnlAppraisalContent.Width = Math.Max(420, pnlAppraisalScroll.ClientSize.Width - 4);
            };
            btnRefresh.Click += async (s, e) => await LoadLoansAsync();
            btnSearch.Click += async (s, e) => await LoadLoansAsync();

            cboFilterStatus.SelectedIndexChanged += async (s, e) => await LoadLoansAsync();

            txtSearch.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    await LoadLoansAsync();
                }
            };

            dgvLoans.SelectionChanged += async (s, e) =>
            {
                if (dgvLoans.SelectedRows.Count > 0)
                {
                    var loan = dgvLoans.SelectedRows[0].DataBoundItem as BorrowingLoan;
                    if (loan != null)
                    {
                        _selectedLoan = loan;
                        await DisplayAppraisalDetailsAsync(loan);
                    }
                }
            };

            dgvLoans.CellFormatting += DgvLoans_CellFormatting;

            btnApprove.Click += async (s, e) => await HandleApproveLoanAsync();
            btnReject.Click += async (s, e) => await HandleRejectLoanAsync();
        }

        private void DgvLoans_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvLoans.Rows.Count) return;

            if (dgvLoans.Columns[e.ColumnIndex].Name == "colTrangThai" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "Chờ xét duyệt")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(217, 119, 6); // Amber
                    e.CellStyle.Font = new Font(dgvLoans.Font, FontStyle.Bold);
                }
                else if (status == "Đã duyệt")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74); // Green
                    e.CellStyle.Font = new Font(dgvLoans.Font, FontStyle.Bold);
                }
                else if (status == "Đã từ chối")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38); // Red
                    e.CellStyle.Font = new Font(dgvLoans.Font, FontStyle.Bold);
                }
            }
        }

        /// <summary>
        /// Tải toàn bộ danh sách hồ sơ vay và cập nhật thống kê Admin.
        /// </summary>
        public async Task LoadLoansAsync()
        {
            try
            {
                string status = cboFilterStatus.SelectedItem?.ToString() ?? "Tất cả";
                string kw = txtSearch.Text.Trim();

                _loans = await _borrowingService.GetAllLoansAsync(status, kw);

                // Cập nhật huy hiệu thống kê
                int total = _loans.Count;
                int pending = _loans.Count(x => x.TrangThai == "Chờ xét duyệt");
                int approved = _loans.Count(x => x.TrangThai == "Đã duyệt");
                lblStatBadge.Text = $"Hồ sơ: {total} | Chờ duyệt: {pending} | Đã duyệt: {approved}";

                dgvLoans.DataSource = null;
                dgvLoans.DataSource = _loans;

                if (_loans.Count > 0)
                {
                    // Giữ lại lựa chọn trước đó nếu còn tồn tại
                    int targetIndex = 0;
                    if (_selectedLoan != null)
                    {
                        int foundIndex = _loans.FindIndex(x => x.MaKhoanVay == _selectedLoan.MaKhoanVay);
                        if (foundIndex >= 0) targetIndex = foundIndex;
                    }

                    dgvLoans.ClearSelection();
                    dgvLoans.Rows[targetIndex].Selected = true;
                    _selectedLoan = _loans[targetIndex];
                    await DisplayAppraisalDetailsAsync(_selectedLoan);
                }
                else
                {
                    ClearAppraisalCard();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách hồ sơ vay: " + ex.Message,
                    "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hiển thị toàn bộ thông tin thẩm định tài chính khách hàng:
        /// - Mã tài khoản, Mã khách hàng, Tên người dùng
        /// - Tổng số thu chi trong 1 quý (90 ngày)
        /// - Thông tin khoản vay và đánh giá năng lực tài chính
        /// </summary>
        private async Task DisplayAppraisalDetailsAsync(BorrowingLoan loan)
        {
            if (loan == null)
            {
                ClearAppraisalCard();
                return;
            }

            // Hiển thị thông tin khoản vay đăng ký
            lblLoanIdVal.Text = loan.MaKhoanVay;
            lblLoanAmountVal.Text = loan.FormattedLoanAmount;
            lblTermInterestVal.Text = $"{loan.FormattedTerm} ({loan.LaiSuat}%/ngày - Tiền lãi: {loan.FormattedInterest})";
            lblTotalDebtVal.Text = loan.FormattedTotalPay;

            // Cập nhật trạng thái
            lblStatusBadge.Text = loan.TrangThai;
            if (loan.TrangThai == "Chờ xét duyệt")
            {
                lblStatusBadge.BackColor = Color.FromArgb(254, 243, 199);
                lblStatusBadge.ForeColor = Color.FromArgb(180, 83, 9);
                btnApprove.Enabled = true;
                btnReject.Enabled = true;
            }
            else if (loan.TrangThai == "Đã duyệt")
            {
                lblStatusBadge.BackColor = Color.FromArgb(220, 252, 231);
                lblStatusBadge.ForeColor = Color.FromArgb(21, 128, 61);
                btnApprove.Enabled = false;
                btnReject.Enabled = false;
            }
            else
            {
                lblStatusBadge.BackColor = Color.FromArgb(254, 226, 226);
                lblStatusBadge.ForeColor = Color.FromArgb(185, 28, 28);
                btnApprove.Enabled = false;
                btnReject.Enabled = false;
            }

            // Gọi dịch vụ tính toán dữ liệu thẩm định 1 quý (90 ngày)
            FinancialAppraisal fa = await _borrowingService.GetFinancialAppraisalAsync(
                loan.MaKH, loan.SoTaiKhoan, loan.SoTienVay);

            // Cập nhật khối Thông tin khách hàng & Tài khoản
            lblAccVal.Text = $"{loan.SoTaiKhoan} ({fa.TenNganHang})";
            lblMaKHVal.Text = loan.MaKH;
            lblUserVal.Text = $"{fa.TenDangNhap} ({fa.HoTen})";
            lblBalanceVal.Text = fa.FormattedBalance;

            // Cập nhật khối Thống kê thu chi trong 1 quý (90 ngày)
            lblThuQuyVal.Text = fa.FormattedIncome;
            lblChiQuyVal.Text = fa.FormattedExpense;
            lblChenhLechVal.Text = fa.FormattedNetCashFlow;
            lblChenhLechVal.ForeColor = fa.ChenhLechThuChi >= 0
                ? Color.FromArgb(2, 132, 199)
                : Color.FromArgb(220, 38, 38);
            lblSoGDVal.Text = $"{fa.SoGiaoDichQuy} giao dịch hoàn tất trong 90 ngày";

            // Cập nhật khối Đánh giá năng lực tài chính
            lblEvalBadge.Text = fa.DanhGia;
            lblEvalNote.Text = fa.GhiChuChiTiet;

            if (fa.DanhGia.Contains("Tài chính rất tốt") || fa.DanhGia.Contains("Đạt yêu cầu"))
            {
                pnlEvaluation.BackColor = Color.FromArgb(240, 253, 244);
                pnlEvaluation.BorderColor = Color.FromArgb(187, 247, 208);
                lblEvalTitle.ForeColor = Color.FromArgb(21, 128, 61);
                lblEvalBadge.ForeColor = Color.FromArgb(21, 128, 61);
            }
            else
            {
                pnlEvaluation.BackColor = Color.FromArgb(254, 242, 242);
                pnlEvaluation.BorderColor = Color.FromArgb(254, 202, 202);
                lblEvalTitle.ForeColor = Color.FromArgb(185, 28, 28);
                lblEvalBadge.ForeColor = Color.FromArgb(185, 28, 28);
            }
        }

        private void ClearAppraisalCard()
        {
            lblAccVal.Text = "---";
            lblMaKHVal.Text = "---";
            lblUserVal.Text = "---";
            lblBalanceVal.Text = "0 VND";

            lblThuQuyVal.Text = "+ 0 đ";
            lblChiQuyVal.Text = "- 0 đ";
            lblChenhLechVal.Text = "0 đ";
            lblSoGDVal.Text = "0 giao dịch trong quý";

            lblLoanIdVal.Text = "---";
            lblLoanAmountVal.Text = "0 VND";
            lblTermInterestVal.Text = "---";
            lblTotalDebtVal.Text = "0 VND";

            lblStatusBadge.Text = "---";
            lblEvalBadge.Text = "Chưa có hồ sơ được chọn";
            lblEvalNote.Text = "Vui lòng chọn hồ sơ vay trong danh sách để xem dữ liệu thẩm định.";

            btnApprove.Enabled = false;
            btnReject.Enabled = false;
        }

        /// <summary>
        /// Xử lý XÁC NHẬN phê duyệt hồ sơ vay và giải ngân trực tiếp vào tài khoản ngân hàng.
        /// </summary>
        private async Task HandleApproveLoanAsync()
        {
            if (_selectedLoan == null)
            {
                MessageBox.Show("Vui lòng chọn một hồ sơ vay cần phê duyệt xét duyệt.",
                    "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_selectedLoan.TrangThai != "Chờ xét duyệt")
            {
                MessageBox.Show($"Hồ sơ {_selectedLoan.MaKhoanVay} đang ở trạng thái '{_selectedLoan.TrangThai}', không thể duyệt lại.",
                    "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string confirmMsg = $"XÁC NHẬN PHÊ DUYỆT & GIẢI NGÂN HỒ SƠ:\n\n" +
                                $"• Mã hồ sơ: {_selectedLoan.MaKhoanVay}\n" +
                                $"• Khách hàng: {_selectedLoan.HoTen} (Mã: {_selectedLoan.MaKH})\n" +
                                $"• Số tài khoản thụ hưởng: {_selectedLoan.SoTaiKhoan}\n" +
                                $"• Số tiền giải ngân: {_selectedLoan.FormattedLoanAmount}\n" +
                                $"• Thời hạn vay: {_selectedLoan.FormattedTerm}\n" +
                                $"• Tổng nợ cần thu hồi: {_selectedLoan.FormattedTotalPay}\n\n" +
                                $"Hệ thống sẽ cộng trực tiếp {_selectedLoan.FormattedLoanAmount} vào tài khoản ngân hàng của khách và ghi nhận giao dịch.\n\n" +
                                $"Bạn có chắc chắn muốn tiến hành?";

            DialogResult dr = MessageBox.Show(confirmMsg, "Xác Nhận Giải Ngân",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr != DialogResult.Yes) return;

            btnApprove.Enabled = false;
            btnReject.Enabled = false;

            try
            {
                string adminUser = CurrentSession.TenDangNhap ?? "Admin";
                var result = await _borrowingService.ApproveLoanAsync(_selectedLoan.MaKhoanVay, adminUser);

                if (result.Success)
                {
                    MessageBox.Show(result.Message, "Phê Duyệt Thành Công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadLoansAsync();
                }
                else
                {
                    MessageBox.Show(result.Message, "Phê Duyệt Thất Bại",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnApprove.Enabled = true;
                    btnReject.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xét duyệt giải ngân: " + ex.Message,
                    "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnApprove.Enabled = true;
                btnReject.Enabled = true;
            }
        }

        /// <summary>
        /// Xử lý KHÔNG XÁC NHẬN (từ chối hồ sơ vay).
        /// </summary>
        private async Task HandleRejectLoanAsync()
        {
            if (_selectedLoan == null)
            {
                MessageBox.Show("Vui lòng chọn một hồ sơ vay cần từ chối xét duyệt.",
                    "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_selectedLoan.TrangThai != "Chờ xét duyệt")
            {
                MessageBox.Show($"Hồ sơ {_selectedLoan.MaKhoanVay} đang ở trạng thái '{_selectedLoan.TrangThai}', không thể từ chối.",
                    "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string reason = PromptRejectionReason(_selectedLoan);
            if (reason == null) return; // Người dùng bấm Hủy

            btnApprove.Enabled = false;
            btnReject.Enabled = false;

            try
            {
                string adminUser = CurrentSession.TenDangNhap ?? "Admin";
                var result = await _borrowingService.RejectLoanAsync(_selectedLoan.MaKhoanVay, reason, adminUser);

                if (result.Success)
                {
                    MessageBox.Show(result.Message, "Đã Từ Chối Hồ Sơ",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadLoansAsync();
                }
                else
                {
                    MessageBox.Show(result.Message, "Thao Tác Thất Bại",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnApprove.Enabled = true;
                    btnReject.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi từ chối hồ sơ: " + ex.Message,
                    "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnApprove.Enabled = true;
                btnReject.Enabled = true;
            }
        }

        /// <summary>
        /// Hộp thoại nhập lý do từ chối hồ sơ khoản vay thiết kế bo góc hiện đại.
        /// </summary>
        private string PromptRejectionReason(BorrowingLoan loan)
        {
            using (var prompt = new Form())
            {
                prompt.Width = 480;
                prompt.Height = 260;
                prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                prompt.Text = "Từ Chối Hồ Sơ Khoản Vay";
                prompt.StartPosition = FormStartPosition.CenterParent;
                prompt.MaximizeBox = false;
                prompt.MinimizeBox = false;
                prompt.BackColor = Color.White;

                var lblInfo = new Label
                {
                    Left = 20,
                    Top = 15,
                    Width = 430,
                    Height = 45,
                    Text = $"Nhập lý do không xác nhận hồ sơ {loan.MaKhoanVay} của khách hàng {loan.HoTen}:",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(15, 23, 42)
                };

                var txtReason = new TextBox
                {
                    Left = 20,
                    Top = 65,
                    Width = 425,
                    Height = 65,
                    Multiline = true,
                    Font = new Font("Segoe UI", 9.5F),
                    Text = "Không đạt tiêu chuẩn thẩm định tín dụng / dòng tiền quý không đủ khả năng hoàn trả."
                };

                var btnConfirmReject = new RoundedButton
                {
                    Text = "Từ chối hồ sơ",
                    Left = 20,
                    Top = 150,
                    Width = 140,
                    Height = 38,
                    BorderRadius = 8,
                    NormalColor = Color.FromArgb(220, 38, 38),
                    HoverColor = Color.FromArgb(185, 28, 28),
                    TextColor = Color.White,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    DialogResult = DialogResult.OK
                };

                var btnCancel = new RoundedButton
                {
                    Text = "Hủy bỏ",
                    Left = 170,
                    Top = 150,
                    Width = 100,
                    Height = 38,
                    BorderRadius = 8,
                    NormalColor = Color.FromArgb(100, 116, 139),
                    HoverColor = Color.FromArgb(71, 85, 105),
                    TextColor = Color.White,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    DialogResult = DialogResult.Cancel
                };

                prompt.Controls.Add(lblInfo);
                prompt.Controls.Add(txtReason);
                prompt.Controls.Add(btnConfirmReject);
                prompt.Controls.Add(btnCancel);
                prompt.AcceptButton = btnConfirmReject;
                prompt.CancelButton = btnCancel;

                return prompt.ShowDialog(this) == DialogResult.OK ? txtReason.Text.Trim() : null;
            }
        }
    }
}
