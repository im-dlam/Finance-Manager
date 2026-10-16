using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    public partial class DashboardUserForm : Form
    {
        private readonly SpendingService _spendingService = new SpendingService();
        private readonly BorrowingService _borrowingService = new BorrowingService();
        private UserHomeSummary _summary = new UserHomeSummary();
        private bool _isBalanceVisible = false;
        private int _selectedAccountIndex = 0;

        public DashboardUserForm()
        {
            InitializeComponent();
            WireEvents();
        }

        private void WireEvents()
        {
            this.Load += async (s, e) => await LoadDashboardDataAsync();
            this.Resize += (s, e) => UpdateProgressBars();
            pnlDailyProgressTrack.Resize += (s, e) => UpdateProgressBars();
            pnlWeeklyProgressTrack.Resize += (s, e) => UpdateProgressBars();

            btnToggleBalance.Click += BtnToggleBalance_Click;
            btnCopyStk.Click += BtnCopyStk_Click;
            btnSwitchBank.Click += BtnSwitchBank_Click;

            btnNavHistory.Click += BtnNavHistory_Click;
            btnNavCards.Click += BtnNavCards_Click;

            btnPayDebt.Click += async (s, e) => await BtnPayDebt_ClickAsync();
            btnOpenLimitDialog.Click += async (s, e) => await OpenSpendingLimitDialogAsync();

            // Cho phép click vào toàn bộ thẻ Quản lý chi tiêu để mở dialog thiết lập
            pnlSpendingCard.Click += async (s, e) => await OpenSpendingLimitDialogAsync();
            lblSpendingTitle.Click += async (s, e) => await OpenSpendingLimitDialogAsync();
        }

        /// <summary>
        /// Tải toàn bộ dữ liệu trang chủ User và thống kê hạn mức chi tiêu.
        /// </summary>
        public async Task LoadDashboardDataAsync()
        {
            try
            {
                string maKH = CurrentSession.MaKH;
                string tenDangNhap = CurrentSession.TenDangNhap;

                _summary = await _spendingService.GetUserHomeSummaryAsync(maKH, tenDangNhap);

                UpdateUserInfoUI();
                UpdateDebtCardUI();
                UpdateSpendingCardUI();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[DashboardUserForm] LoadDashboardDataAsync error: {ex.Message}");
            }
        }

        /// <summary>
        /// Cập nhật hiển thị thẻ thông tin cá nhân và tài khoản (White Card sáng sủa, rõ ràng).
        /// </summary>
        private void UpdateUserInfoUI()
        {
            if (_summary == null) return;

            // Tên và Avatar
            string hoTen = !string.IsNullOrWhiteSpace(_summary.HoTen) ? _summary.HoTen.Trim() : (!string.IsNullOrWhiteSpace(_summary.TenDangNhap) ? _summary.TenDangNhap.Trim() : "NGƯỜI DÙNG");
            lblUserName.Text = hoTen.ToUpper();

            string firstChar = "D";
            if (!string.IsNullOrWhiteSpace(hoTen))
            {
                string[] parts = hoTen.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                firstChar = parts[parts.Length - 1].Substring(0, 1).ToUpper();
            }
            lblAvatar.Text = firstChar;

            // Hạng thành viên
            lblUserTier.Text = $"🌸 {_summary.HangThanhVien} >";

            // Số tài khoản hiển thị rõ ràng, không ẩn
            UpdateDisplayedAccount();

            // Số dư
            UpdateBalanceDisplay();
        }

        private void UpdateDisplayedAccount()
        {
            if (_summary.DanhSachTaiKhoan != null && _summary.DanhSachTaiKhoan.Count > 0)
            {
                if (_selectedAccountIndex >= _summary.DanhSachTaiKhoan.Count)
                    _selectedAccountIndex = 0;

                var acc = _summary.DanhSachTaiKhoan[_selectedAccountIndex];
                _summary.SoTaiKhoanChinh = acc.SoTaiKhoan;

                // Hiển thị số tài khoản rõ ràng, không ẩn dấu sao
                lblAccountNumber.Text = !string.IsNullOrWhiteSpace(acc.SoTaiKhoan) ? acc.SoTaiKhoan.Trim() : "Chưa có tài khoản";

                if (_summary.DanhSachTaiKhoan.Count > 1)
                {
                    lblSwitchInfo.Text = $"ⓘ Tài khoản: {acc.SoTaiKhoan} ({_selectedAccountIndex + 1}/{_summary.DanhSachTaiKhoan.Count})";
                }
                else
                {
                    lblSwitchInfo.Text = "ⓘ Tài khoản ngân hàng chính";
                }
            }
            else
            {
                lblAccountNumber.Text = !string.IsNullOrWhiteSpace(_summary.SoTaiKhoanChinh) ? _summary.SoTaiKhoanChinh.Trim() : "Chưa có tài khoản";
                lblSwitchInfo.Text = "ⓘ Tài khoản ngân hàng chính";
            }

            // Tự động căn chỉnh nút sao chép nằm ngay sau số tài khoản
            btnCopyStk.Left = lblAccountNumber.Right + 8;
        }

        private void UpdateBalanceDisplay()
        {
            if (_isBalanceVisible)
            {
                lblBalance.Text = _summary.FormattedTongSoDu;
                btnToggleBalance.Text = "👁‍🗨";
            }
            else
            {
                lblBalance.Text = "•••••••••";
                btnToggleBalance.Text = "👁";
            }

            // Tự động căn chỉnh nút con mắt nằm ngay sau số dư
            btnToggleBalance.Left = lblBalance.Right + 8;
        }

        /// <summary>
        /// Cập nhật thẻ Kỳ hạn trả nợ.
        /// </summary>
        private void UpdateDebtCardUI()
        {
            if (_summary.KhoanVaySapToi != null)
            {
                lblDebtDueDate.Text = $"Kỳ hạn tiếp theo: {_summary.KhoanVaySapToi.NgayDaoHan:dd/MM/yyyy}";
                lblDebtAmount.Text = $"- Số tiền: {_summary.KhoanVaySapToi.TongPhaiTra:N0} đ";
                lblDebtStatus.Text = "- Trạng thái: Chưa thanh toán";
                lblDebtStatus.ForeColor = Color.FromArgb(225, 29, 72);

                btnPayDebt.Enabled = true;
                btnPayDebt.Text = "Thanh toán ngay";
                btnPayDebt.NormalColor = Color.FromArgb(239, 68, 68);
                btnPayDebt.HoverColor = Color.FromArgb(220, 38, 38);
                btnPayDebt.PressedColor = Color.FromArgb(185, 28, 28);
                btnPayDebt.ForeColor = Color.White;
            }
            else
            {
                lblDebtDueDate.Text = "Kỳ hạn tiếp theo: Không có khoản vay đến hạn";
                lblDebtAmount.Text = "- Số tiền: 0 đ";
                lblDebtStatus.Text = "- Trạng thái: An toàn (Không có dư nợ)";
                lblDebtStatus.ForeColor = Color.FromArgb(22, 163, 74);

                btnPayDebt.Enabled = false;
                btnPayDebt.Text = "Không có dư nợ";
                btnPayDebt.NormalColor = Color.FromArgb(241, 245, 249);
                btnPayDebt.HoverColor = Color.FromArgb(241, 245, 249);
                btnPayDebt.PressedColor = Color.FromArgb(241, 245, 249);
                btnPayDebt.ForeColor = Color.FromArgb(148, 163, 184);
            }
        }

        /// <summary>
        /// Cập nhật thẻ Quản lý chi tiêu (Hạn mức 1 ngày và 1 tuần).
        /// </summary>
        private void UpdateSpendingCardUI()
        {
            var stats = _summary.Spending ?? new SpendingStats();

            // Ngày
            lblDailySpent.Text = $"Đã chi: {stats.FormattedDaChiHomNay} / {stats.FormattedHanMucNgay} (Ngày)";
            lblDailyRemaining.Text = $"Còn lại: {stats.FormattedConLaiHomNay} ({stats.PhanTramNgay}%)";

            // Tuần
            lblWeeklySpent.Text = $"Đã chi: {stats.FormattedDaChiTuanNay} / {stats.FormattedHanMucTuan} (Tuần)";
            lblWeeklyRemaining.Text = $"Còn lại: {stats.FormattedConLaiTuanNay} ({stats.PhanTramTuan}%)";

            // Badge trạng thái
            lblSpendingStatusBadge.Text = stats.TrangThaiText;
            if (stats.VuotMucNgay || stats.VuotMucTuan)
            {
                lblSpendingStatusBadge.ForeColor = Color.FromArgb(220, 38, 38);
                lblSpendingStatusBadge.BackColor = Color.FromArgb(254, 242, 242);
            }
            else if (stats.PhanTramNgay >= 80 || stats.PhanTramTuan >= 80)
            {
                lblSpendingStatusBadge.ForeColor = Color.FromArgb(217, 119, 6);
                lblSpendingStatusBadge.BackColor = Color.FromArgb(254, 243, 199);
            }
            else
            {
                lblSpendingStatusBadge.ForeColor = Color.FromArgb(22, 101, 52);
                lblSpendingStatusBadge.BackColor = Color.FromArgb(240, 253, 244);
            }

            UpdateProgressBars();
        }

        private void UpdateProgressBars()
        {
            if (_summary == null || _summary.Spending == null) return;
            var stats = _summary.Spending;

            // Thanh tiến độ ngày
            int dailyTrackWidth = Math.Max(10, pnlDailyProgressTrack.ClientSize.Width);
            double dailyPercent = Math.Max(0.0, Math.Min(1.0, (double)stats.PhanTramNgay / 100.0));
            int dailyBarWidth = (int)(dailyTrackWidth * dailyPercent);
            pnlDailyProgressBar.Width = Math.Max(6, dailyBarWidth);

            if (stats.VuotMucNgay)
            {
                pnlDailyProgressBar.FillColor = Color.FromArgb(239, 68, 68);
                pnlDailyProgressBar.BorderColor = Color.FromArgb(239, 68, 68);
            }
            else if (stats.PhanTramNgay >= 80)
            {
                pnlDailyProgressBar.FillColor = Color.FromArgb(245, 158, 11);
                pnlDailyProgressBar.BorderColor = Color.FromArgb(245, 158, 11);
            }
            else
            {
                pnlDailyProgressBar.FillColor = Color.FromArgb(37, 99, 235);
                pnlDailyProgressBar.BorderColor = Color.FromArgb(37, 99, 235);
            }

            // Thanh tiến độ tuần
            int weeklyTrackWidth = Math.Max(10, pnlWeeklyProgressTrack.ClientSize.Width);
            double weeklyPercent = Math.Max(0.0, Math.Min(1.0, (double)stats.PhanTramTuan / 100.0));
            int weeklyBarWidth = (int)(weeklyTrackWidth * weeklyPercent);
            pnlWeeklyProgressBar.Width = Math.Max(6, weeklyBarWidth);

            if (stats.VuotMucTuan)
            {
                pnlWeeklyProgressBar.FillColor = Color.FromArgb(239, 68, 68);
                pnlWeeklyProgressBar.BorderColor = Color.FromArgb(239, 68, 68);
            }
            else if (stats.PhanTramTuan >= 80)
            {
                pnlWeeklyProgressBar.FillColor = Color.FromArgb(245, 158, 11);
                pnlWeeklyProgressBar.BorderColor = Color.FromArgb(245, 158, 11);
            }
            else
            {
                pnlWeeklyProgressBar.FillColor = Color.FromArgb(16, 185, 129);
                pnlWeeklyProgressBar.BorderColor = Color.FromArgb(16, 185, 129);
            }
        }

        #region EVENT HANDLERS

        private void BtnToggleBalance_Click(object sender, EventArgs e)
        {
            _isBalanceVisible = !_isBalanceVisible;
            UpdateBalanceDisplay();
        }

        private void BtnCopyStk_Click(object sender, EventArgs e)
        {
            if (_summary != null && !string.IsNullOrWhiteSpace(_summary.SoTaiKhoanChinh))
            {
                try
                {
                    Clipboard.SetText(_summary.SoTaiKhoanChinh.Trim());
                    MessageBox.Show($"Đã sao chép số tài khoản: {_summary.SoTaiKhoanChinh}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể sao chép vào bộ nhớ đệm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnSwitchBank_Click(object sender, EventArgs e)
        {
            if (_summary.DanhSachTaiKhoan != null && _summary.DanhSachTaiKhoan.Count > 1)
            {
                _selectedAccountIndex = (_selectedAccountIndex + 1) % _summary.DanhSachTaiKhoan.Count;
                UpdateDisplayedAccount();
            }
            else
            {
                MessageBox.Show("Hiện tại bạn chỉ có 1 tài khoản đang hoạt động.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnNavHistory_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as MainForm ?? this.ParentForm as MainForm;
            if (main != null)
            {
                main.OpenFormWithCustomerCheck(() => new TransactionForm());
            }
        }

        private void BtnNavCards_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as MainForm ?? this.ParentForm as MainForm;
            if (main != null)
            {
                main.OpenFormWithCustomerCheck(() => new AccountForm());
            }
        }

        private async Task BtnPayDebt_ClickAsync()
        {
            if (_summary.KhoanVaySapToi == null)
            {
                MessageBox.Show("Không có khoản vay nào cần thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var kv = _summary.KhoanVaySapToi;
            var confirm = MessageBox.Show(
                $"Xác nhận thanh toán khoản vay {kv.MaKhoanVay}?\n\n" +
                $"- Số tiền cần thanh toán: {kv.TongPhaiTra:N0} đ\n" +
                $"- Tài khoản trích tiền: {_summary.SoTaiKhoanChinh}\n" +
                $"- Số dư hiện tại: {_summary.TongSoDu:N0} đ",
                "Xác nhận thanh toán nợ",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            btnPayDebt.Enabled = false;
            try
            {
                var result = await _borrowingService.PayLoanAsync(kv.MaKhoanVay, CurrentSession.TenDangNhap);
                if (result.Success)
                {
                    MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDashboardDataAsync();
                }
                else
                {
                    MessageBox.Show(result.Message, "Thanh toán thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xử lý thanh toán: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnPayDebt.Enabled = true;
            }
        }

        private async Task OpenSpendingLimitDialogAsync()
        {
            decimal curDaily = _summary?.Spending?.HanMucNgay ?? 2000000m;
            decimal curWeekly = _summary?.Spending?.HanMucTuan ?? 10000000m;
            bool curAlert = _summary?.Spending?.CanhBaoVuotMuc ?? true;

            using (var dlg = new SpendingLimitDialog(CurrentSession.MaKH, curDaily, curWeekly, curAlert))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    await LoadDashboardDataAsync();
                }
            }
        }

        #endregion
    }
}
