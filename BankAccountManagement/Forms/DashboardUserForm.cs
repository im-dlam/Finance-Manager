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
            btnQR.Click += BtnQR_Click;
            btnNotification.Click += BtnNotification_Click;
            btnSearch.Click += BtnSearch_Click;
            btnSwitchBank.Click += BtnSwitchBank_Click;

            btnNavHistory.Click += BtnNavHistory_Click;
            btnNavCards.Click += BtnNavCards_Click;

            btnPayDebt.Click += async (s, e) => await BtnPayDebt_ClickAsync();
            btnOpenLimitDialog.Click += async (s, e) => await OpenSpendingLimitDialogAsync();
            btnSetSpendingLimit.Click += async (s, e) => await OpenSpendingLimitDialogAsync();
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
        /// Cập nhật hiển thị thẻ thông tin cá nhân và tài khoản (Navy Card).
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

            // Số tài khoản
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
                lblAccountNumber.Text = _summary.SoTaiKhoanMasked;

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
                lblAccountNumber.Text = _summary.SoTaiKhoanMasked;
                lblSwitchInfo.Text = "ⓘ Tài khoản ngân hàng";
            }
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
        }

        /// <summary>
        /// Cập nhật thẻ Kỳ hạn trả nợ.
        /// </summary>
        private void UpdateDebtCardUI()
        {
            if (_summary.KhoanVaySapToi != null)
            {
                lblDebtDueDate.Text = $"Ngày đến hạn: {_summary.KhoanVaySapToi.NgayDaoHan:dd/MM/yyyy}";
                lblDebtAmount.Text = $"{_summary.KhoanVaySapToi.TongPhaiTra:N0} đ";
                lblDebtStatus.Text = "Chưa thanh toán";
                lblDebtStatus.ForeColor = Color.FromArgb(220, 38, 38); // Đỏ
                lblDebtStatus.BackColor = Color.FromArgb(254, 242, 242);

                btnPayDebt.Enabled = true;
                btnPayDebt.Text = "Thanh toán ngay";
                btnPayDebt.NormalColor = Color.FromArgb(239, 68, 68);
                btnPayDebt.HoverColor = Color.FromArgb(220, 38, 38);
                btnPayDebt.PressedColor = Color.FromArgb(185, 28, 28);
                btnPayDebt.ForeColor = Color.White;
            }
            else
            {
                lblDebtDueDate.Text = "Không có khoản vay nào đến hạn";
                lblDebtAmount.Text = "0 đ";
                lblDebtStatus.Text = "An toàn";
                lblDebtStatus.ForeColor = Color.FromArgb(22, 163, 74); // Xanh lá
                lblDebtStatus.BackColor = Color.FromArgb(240, 253, 244);

                btnPayDebt.Enabled = false;
                btnPayDebt.Text = "Không có dư nợ";
                btnPayDebt.NormalColor = Color.FromArgb(226, 232, 240);
                btnPayDebt.HoverColor = Color.FromArgb(226, 232, 240);
                btnPayDebt.PressedColor = Color.FromArgb(226, 232, 240);
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
            lblDailySpent.Text = $"Đã chi: {stats.FormattedDaChiHomNay} / {stats.FormattedHanMucNgay}";
            lblDailyRemaining.Text = $"Còn lại: {stats.FormattedConLaiHomNay} ({stats.PhanTramNgay}%)";

            // Tuần
            lblWeeklySpent.Text = $"Đã chi: {stats.FormattedDaChiTuanNay} / {stats.FormattedHanMucTuan}";
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
                lblSpendingStatusBadge.ForeColor = Color.FromArgb(22, 163, 74);
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
                pnlDailyProgressBar.FillColor = Color.FromArgb(239, 68, 68); // Đỏ
                pnlDailyProgressBar.BorderColor = Color.FromArgb(239, 68, 68);
            }
            else if (stats.PhanTramNgay >= 80)
            {
                pnlDailyProgressBar.FillColor = Color.FromArgb(245, 158, 11); // Cam
                pnlDailyProgressBar.BorderColor = Color.FromArgb(245, 158, 11);
            }
            else
            {
                pnlDailyProgressBar.FillColor = Color.FromArgb(37, 99, 235); // Xanh dương
                pnlDailyProgressBar.BorderColor = Color.FromArgb(37, 99, 235);
            }

            // Thanh tiến độ tuần
            int weeklyTrackWidth = Math.Max(10, pnlWeeklyProgressTrack.ClientSize.Width);
            double weeklyPercent = Math.Max(0.0, Math.Min(1.0, (double)stats.PhanTramTuan / 100.0));
            int weeklyBarWidth = (int)(weeklyTrackWidth * weeklyPercent);
            pnlWeeklyProgressBar.Width = Math.Max(6, weeklyBarWidth);

            if (stats.VuotMucTuan)
            {
                pnlWeeklyProgressBar.FillColor = Color.FromArgb(239, 68, 68); // Đỏ
                pnlWeeklyProgressBar.BorderColor = Color.FromArgb(239, 68, 68);
            }
            else if (stats.PhanTramTuan >= 80)
            {
                pnlWeeklyProgressBar.FillColor = Color.FromArgb(245, 158, 11); // Cam
                pnlWeeklyProgressBar.BorderColor = Color.FromArgb(245, 158, 11);
            }
            else
            {
                pnlWeeklyProgressBar.FillColor = Color.FromArgb(16, 185, 129); // Xanh ngọc
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

        private void BtnQR_Click(object sender, EventArgs e)
        {
            string stk = !string.IsNullOrWhiteSpace(_summary.SoTaiKhoanChinh) ? _summary.SoTaiKhoanChinh : "Chưa có tài khoản";
            string name = !string.IsNullOrWhiteSpace(_summary.HoTen) ? _summary.HoTen : CurrentSession.TenDangNhap;

            using (var qrForm = new Form())
            {
                qrForm.Text = "Mã QR Nhận Tiền";
                qrForm.Size = new Size(360, 420);
                qrForm.StartPosition = FormStartPosition.CenterParent;
                qrForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                qrForm.MaximizeBox = false;
                qrForm.MinimizeBox = false;
                qrForm.BackColor = Color.White;

                var pnlCard = new RoundedPanel
                {
                    Dock = DockStyle.Fill,
                    FillColor = Color.White,
                    BorderColor = Color.FromArgb(226, 232, 240),
                    BorderSize = 1,
                    BorderRadius = 16,
                    Padding = new Padding(20)
                };

                var lblTitle = new Label
                {
                    Text = "QUÉT ĐỂ CHUYỂN TIỀN",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(15, 23, 42),
                    Dock = DockStyle.Top,
                    Height = 30,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                var lblName = new Label
                {
                    Text = name.ToUpper(),
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(37, 99, 235),
                    Dock = DockStyle.Top,
                    Height = 26,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                var lblStk = new Label
                {
                    Text = $"STK: {stk}",
                    Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    Dock = DockStyle.Top,
                    Height = 24,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                var pnlQrBox = new RoundedPanel
                {
                    Size = new Size(200, 200),
                    Location = new Point(70, 100),
                    BorderColor = Color.FromArgb(203, 213, 225),
                    BorderSize = 2,
                    BorderRadius = 12,
                    FillColor = Color.FromArgb(248, 250, 252)
                };

                var lblQrSim = new Label
                {
                    Dock = DockStyle.Fill,
                    Text = "[\n  QR CODE  \n  VIETQR  \n]",
                    Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 41, 59),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                pnlQrBox.Controls.Add(lblQrSim);

                var btnClose = new RoundedButton
                {
                    Text = "Đóng",
                    Dock = DockStyle.Bottom,
                    Height = 40,
                    NormalColor = Color.FromArgb(241, 245, 249),
                    HoverColor = Color.FromArgb(226, 232, 240),
                    PressedColor = Color.FromArgb(203, 213, 225),
                    ForeColor = Color.FromArgb(51, 65, 85),
                    BorderRadius = 10,
                    Cursor = Cursors.Hand
                };
                btnClose.Click += (s2, e2) => qrForm.Close();

                pnlCard.Controls.Add(pnlQrBox);
                pnlCard.Controls.Add(lblStk);
                pnlCard.Controls.Add(lblName);
                pnlCard.Controls.Add(lblTitle);
                pnlCard.Controls.Add(btnClose);

                qrForm.Controls.Add(pnlCard);
                qrForm.ShowDialog(this);
            }
        }

        private void BtnNotification_Click(object sender, EventArgs e)
        {
            string msg = "Không có thông báo mới nào.";
            if (_summary.Spending != null && (_summary.Spending.VuotMucNgay || _summary.Spending.VuotMucTuan))
            {
                msg = "⚠ Cảnh báo: Chi tiêu của bạn đã vượt quá hạn mức thiết lập!\nVui lòng điều chỉnh hoặc kiểm tra lại lịch sử giao dịch.";
            }
            else if (_summary.KhoanVaySapToi != null)
            {
                msg = $"🔔 Nhắc nhở: Bạn có khoản vay {_summary.KhoanVaySapToi.MaKhoanVay} đến hạn ngày {_summary.KhoanVaySapToi.NgayDaoHan:dd/MM/yyyy}.\nSố tiền cần trả: {_summary.KhoanVaySapToi.TongPhaiTra:N0} đ.";
            }
            MessageBox.Show(msg, "Trung tâm thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as MainForm ?? this.ParentForm as MainForm;
            if (main != null)
            {
                main.OpenFormWithCustomerCheck(() => new TransactionForm());
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
