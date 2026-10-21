using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Form Bảng điều khiển cá nhân dành cho Khách hàng (Member User Dashboard).
    /// <para>
    /// Cung cấp trải nghiệm ngân hàng số hiện đại:
    /// - Thẻ ATM ảo (Virtual Card) đổi màu theo hạng tài khoản (Kim Cương / Vàng / Bạc / Tiêu Chuẩn), bật/tắt ẩn số dư, sao chép nhanh STK.
    /// - 4 Tác vụ nhanh: Chuyển khoản, Mở thẻ tín dụng, Gửi tiết kiệm, Đăng ký vay vốn.
    /// - Quản lý chỉ số chi tiêu thông minh: Theo dõi hạn mức ngày, tuần, cảnh báo vượt mức trực quan bằng thanh tiến độ đổi màu.
    /// - Theo dõi khoản nợ &amp; kỳ hạn đáo hạn gần nhất, hỗ trợ thanh toán nợ ngay trong một cú nhấp.
    /// - Danh sách các giao dịch phát sinh gần đây nhất.
    /// </para>
    /// </summary>
    public partial class DashboardUserForm : Form
    {
        #region 1. CÁC BIẾN TRẠNG THÁI VÀ DỊCH VỤ

        /// <summary>Dịch vụ quản lý hạn mức và thống kê chi tiêu.</summary>
        private readonly SpendingService _spendingService = new SpendingService();

        /// <summary>Dịch vụ quản lý các khoản vay và tất toán nợ.</summary>
        private readonly BorrowingService _borrowingService = new BorrowingService();

        /// <summary>Dịch vụ quản lý và tra cứu lịch sử giao dịch.</summary>
        private readonly TransactionService _transactionService = new TransactionService();

        /// <summary>Đối tượng chứa toàn bộ dữ liệu tổng hợp của khách hàng trên trang chủ.</summary>
        private UserHomeSummary _summary = new UserHomeSummary();

        /// <summary>Cờ bật / tắt hiển thị số dư tài khoản trên thẻ ATM ảo (mặc định ẩn ••••••••• để bảo mật).</summary>
        private bool _isBalanceVisible = false;

        /// <summary>Chỉ số tài khoản đang được chọn hiển thị trong trường hợp khách hàng sở hữu nhiều tài khoản.</summary>
        private int _selectedAccountIndex = 0;

        /// <summary>Delegate xử lý sự kiện đổi hạng thành viên thời gian thực.</summary>
        private Action<string> _tierChangedHandler;

        /// <summary>Delegate xử lý sự kiện khi có giao dịch mới phát sinh trong hệ thống.</summary>
        private Action _transactionChangedHandler;

        #endregion

        #region 2. KHỞI TẠO VÀ ĐĂNG KÝ SỰ KIỆN (INITIALIZATION)

        /// <summary>
        /// Hàm dựng khởi tạo giao diện người dùng, bật DoubleBuffered và đăng ký các sự kiện tương tác.
        /// </summary>
        public DashboardUserForm()
        {
            InitializeComponent();
            WireEvents();
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// Đăng ký toàn bộ các sự kiện tương tác giao diện và sự kiện toàn cục.
        /// </summary>
        private void WireEvents()
        {
            this.Load += async (s, e) =>
            {
                ApplyTierThemeToCard(CurrentSession.SelectedUserTier);
                await LoadDashboardDataAsync();
            };

            this.Resize += (s, e) => UpdateProgressBars();
            pnlDailyProgressTrack.Resize += (s, e) => UpdateProgressBars();
            pnlWeeklyProgressTrack.Resize += (s, e) => UpdateProgressBars();

            // Đăng ký nhận sự kiện thay đổi hạng thành viên (ví dụ khi chọn trên ComboBox ở thanh Topbar)
            _tierChangedHandler = tierKey =>
            {
                if (this.IsDisposed || !this.IsHandleCreated) return;
                this.BeginInvoke((Action)(() => ApplyTierThemeToCard(tierKey)));
            };
            CurrentSession.OnTierChanged += _tierChangedHandler;

            // Đăng ký nhận sự kiện khi có bất kỳ giao dịch phát sinh để tải lại dữ liệu tức thì
            _transactionChangedHandler = () =>
            {
                if (this.IsDisposed || !this.IsHandleCreated) return;
                this.BeginInvoke((Action)(async () => await LoadDashboardDataAsync()));
            };
            TransactionService.OnTransactionChanged += _transactionChangedHandler;

            this.FormClosed += (s, e) =>
            {
                CurrentSession.OnTierChanged -= _tierChangedHandler;
                TransactionService.OnTransactionChanged -= _transactionChangedHandler;
            };

            btnToggleBalance.Click += BtnToggleBalance_Click;
            btnCopyStk.Click += BtnCopyStk_Click;

            // Chuyển đổi qua lại giữa các tài khoản nếu khách hàng sở hữu nhiều tài khoản
            lblSwitchInfo.Cursor = Cursors.Hand;
            lblSwitchInfo.Click += (s, e) =>
            {
                if (_summary.DanhSachTaiKhoan != null && _summary.DanhSachTaiKhoan.Count > 1)
                {
                    _selectedAccountIndex = (_selectedAccountIndex + 1) % _summary.DanhSachTaiKhoan.Count;
                    UpdateDisplayedAccount();
                }
            };

            // Nhấp vào nhãn Hạng thành viên để xem bảng tiêu chuẩn phân hạng và điều kiện nâng cấp
            lblUserTier.Cursor = Cursors.Hand;
            lblUserTier.Click += (s, e) =>
            {
                decimal bal = _summary?.TongSoDu ?? 0m;
                string curTier = UserTierTheme.DetermineTierByBalance(bal);
                string nextReq = "";
                if (bal < 200000000m)
                    nextReq = "Cần thêm " + string.Format("{0:N0}", 200000000m - bal) + " đ để nâng lên 🥈 Hạng Bạc (200 - 500 triệu).";
                else if (bal < 500000000m)
                    nextReq = "Cần thêm " + string.Format("{0:N0}", 500000000m - bal) + " đ để nâng lên 🥇 Hạng Vàng (500 triệu - 1 tỷ).";
                else if (bal < 1000000000m)
                    nextReq = "Cần thêm " + string.Format("{0:N0}", 1000000000m - bal) + " đ để nâng lên 💎 Hạng Kim Cương (Trên 1 tỷ).";
                else
                    nextReq = "Bạn đang ở Hạng Kim Cương - Hạng thành viên cao cấp nhất!";

                string msg = string.Format(
                    @"Hạng hiện tại của bạn: {0}
Tổng số dư thực tế: {1:N0} đ

Tiêu chuẩn phân hạng tài khoản:
• 💎 Hạng Kim Cương: Trên 1 tỷ VNĐ
• 🥇 Hạng Vàng: Từ 500 triệu đến 1 tỷ VNĐ
• 🥈 Hạng Bạc: Từ 200 triệu đến 500 triệu VNĐ
• ⭐ Hạng Tiêu Chuẩn: Dưới 200 triệu VNĐ (Mặc định khi bắt đầu)

{2}",
                    curTier, bal, nextReq);

                MessageBox.Show(msg, "Thông tin Hạng Thành Viên", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            // 4 Nút tác vụ nhanh (Quick Actions)
            btnQuickTransfer.Click += async (s, e) => await OpenQuickTransferDialogAsync();
            btnQuickCards.Click += (s, e) => OpenCreditCardsForm();
            btnQuickSavings.Click += (s, e) => OpenSavingsForm();
            btnQuickLoan.Click += (s, e) => OpenBorrowingForm();

            // Khoản vay nợ
            btnPayDebt.Click += async (s, e) => await BtnPayDebt_ClickAsync();
            btnViewLoanDetails.Click += (s, e) => OpenBorrowingForm();
            btnRegisterLoan.Click += (s, e) => OpenBorrowingForm();

            // Quản lý chi tiêu & Giao dịch
            btnOpenLimitDialog.Click += async (s, e) => await OpenSpendingLimitDialogAsync();
            btnViewAllTransactions.Click += (s, e) => BtnNavHistory_Click(s, e);
        }

        #endregion

        #region 3. THẺ ATM ẢO VÀ CHỦ ĐỀ HẠNG THÀNH VIÊN (VIRTUAL CARD THEMING)

        /// <summary>
        /// Thay đổi phong cách và màu sắc Thẻ ATM ảo (Virtual Card) theo Hạng thành viên (Kim Cương/Vàng/Bạc/Tiêu Chuẩn).
        /// </summary>
        /// <param name="tierKey">Mã hoặc tên hạng cần áp dụng.</param>
        public void ApplyTierThemeToCard(string tierKey)
        {
            if (string.IsNullOrWhiteSpace(tierKey))
                tierKey = CurrentSession.SelectedUserTier;

            var theme = UserTierTheme.GetTheme(tierKey);

            pnlVirtualCard.FillColor = theme.CardBackground;
            pnlVirtualCard.BorderColor = theme.CardBorder;
            pnlVirtualCard.BorderSize = theme.CardBorderSize;

            lblVCardBrand.ForeColor = theme.PrimaryTextColor;
            lblVCardTier.Text = theme.BadgeText;
            lblVCardTier.ForeColor = theme.PrimaryTextColor;

            lblAccountNumber.ForeColor = Color.White;
            btnCopyStk.ForeColor = theme.SecondaryTextColor;

            lblBalance.ForeColor = theme.PrimaryTextColor;
            btnToggleBalance.ForeColor = theme.PrimaryTextColor;

            lblUserTier.Text = theme.TierDisplayName + "\r\n" + theme.TierPerkDescription;
            lblUserTier.ForeColor = theme.TierLabelColor;

            lblOfferTitle.Text = theme.OfferTitle;
            lblOfferDesc.Text = theme.OfferDescription;

            pnlVirtualCard.Invalidate();
        }

        #endregion

        #region 4. TẢI DỮ LIỆU BẤT ĐỒNG BỘ (ASYNC DATA LOADING)

        /// <summary>
        /// Tải toàn bộ dữ liệu tổng hợp Dashboard bất đồng bộ (Task.Run) để giữ giao diện phản hồi tức thì.
        /// </summary>
        public async Task LoadDashboardDataAsync()
        {
            try
            {
                string maKH = CurrentSession.MaKH;
                string tenDangNhap = CurrentSession.TenDangNhap;

                _summary = await _spendingService.GetUserHomeSummaryAsync(maKH, tenDangNhap);

                // Tự động thăng/hạ hạng tài khoản theo số dư thực tế nếu người dùng không chọn thủ công
                if (_summary != null && !CurrentSession.IsManualTierOverride)
                {
                    UserTierTheme.UpdateSessionTierByBalance(_summary.TongSoDu);
                }

                UpdateUserInfoUI();
                UpdateDebtCardUI();
                UpdateSpendingCardUI();
                await LoadRecentTransactionsAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[DashboardUserForm] LoadDashboardDataAsync error: {ex.Message}");
            }
        }

        /// <summary>
        /// Cập nhật hiển thị tên chủ thẻ, số tài khoản và số dư lên Thẻ ATM ảo.
        /// </summary>
        private void UpdateUserInfoUI()
        {
            if (_summary == null) return;

            string hoTen = !string.IsNullOrWhiteSpace(_summary.HoTen)
                ? _summary.HoTen.Trim()
                : (!string.IsNullOrWhiteSpace(_summary.TenDangNhap) ? _summary.TenDangNhap.Trim() : "NGƯỜI DÙNG");
            lblUserName.Text = hoTen.ToUpper();

            ApplyTierThemeToCard(CurrentSession.SelectedUserTier);

            UpdateDisplayedAccount();
            UpdateBalanceDisplay();
        }

        /// <summary>
        /// Cập nhật số tài khoản đang được chọn hiển thị trên thẻ ATM ảo.
        /// </summary>
        private void UpdateDisplayedAccount()
        {
            if (_summary.DanhSachTaiKhoan != null && _summary.DanhSachTaiKhoan.Count > 0)
            {
                if (_selectedAccountIndex >= _summary.DanhSachTaiKhoan.Count)
                    _selectedAccountIndex = 0;

                var acc = _summary.DanhSachTaiKhoan[_selectedAccountIndex];
                _summary.SoTaiKhoanChinh = acc.SoTaiKhoan;

                string rawStk = !string.IsNullOrWhiteSpace(acc.SoTaiKhoan) ? acc.SoTaiKhoan.Trim() : "1010000001";
                lblAccountNumber.Text = rawStk;

                if (_summary.DanhSachTaiKhoan.Count > 1)
                {
                    lblSwitchInfo.Text = $"ⓘ TK: {acc.SoTaiKhoan} ({_selectedAccountIndex + 1}/{_summary.DanhSachTaiKhoan.Count}) 🔁 Đổi";
                }
                else
                {
                    lblSwitchInfo.Text = "ⓘ TK thanh toán chính (Mặc định)";
                }
            }
            else
            {
                lblAccountNumber.Text = !string.IsNullOrWhiteSpace(_summary.SoTaiKhoanChinh) ? _summary.SoTaiKhoanChinh.Trim() : "1010000001";
                lblSwitchInfo.Text = "ⓘ TK thanh toán chính (Mặc định)";
            }

            btnCopyStk.Left = Math.Min(pnlVCardMid.Width - btnCopyStk.Width - 4, lblAccountNumber.Right + 8);
        }

        /// <summary>
        /// Hiển thị số dư thực tế hoặc chuỗi che dấu (•••••••••) tùy thuộc vào cờ _isBalanceVisible.
        /// </summary>
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
        /// Cập nhật thẻ hiển thị Kỳ hạn trả nợ và thanh tiến độ thời hạn khoản vay.
        /// </summary>
        private void UpdateDebtCardUI()
        {
            if (_summary.KhoanVaySapToi != null)
            {
                var kv = _summary.KhoanVaySapToi;
                pnlDebtActive.Visible = true;
                pnlDebtEmpty.Visible = false;

                lblDebtStatus.Text = "Chưa thanh toán";
                lblDebtStatus.ForeColor = Color.FromArgb(225, 29, 72);

                lblDebtAmount.Text = $"{kv.TongPhaiTra:N0} đ";
                lblDebtBreakdown.Text = $"Tiền gốc: {kv.SoTienVay:N0} đ  •  Tiền lãi phát sinh: {kv.TienLai:N0} đ";

                int daysLeft = (kv.NgayDaoHan.Date - DateTime.Now.Date).Days;
                string daysText = daysLeft > 0 ? $"(Còn {daysLeft} ngày)" : (daysLeft == 0 ? "(Hôm nay đến hạn)" : $"(Quá hạn {Math.Abs(daysLeft)} ngày)");

                lblDebtDueDate.Text = $"📅 Hạn thanh toán: {kv.NgayDaoHan:dd/MM/yyyy} {daysText}";
                lblDebtLoanId.Text = $"🏷️ Mã khoản vay: {kv.MaKhoanVay} • {(string.IsNullOrWhiteSpace(kv.TenNganHang) ? "Vay tiêu dùng" : kv.TenNganHang)}";
                lblDebtRate.Text = $"📈 Lãi suất hợp đồng: {kv.FormattedLaiSuat}";
                lblDebtAccount.Text = $"🏦 Tài khoản trích nợ: {(!string.IsNullOrWhiteSpace(kv.SoTaiKhoan) ? kv.SoTaiKhoan : _summary.SoTaiKhoanChinh)} (Đủ số dư ✔)";

                // Cập nhật thanh tiến độ thời hạn khoản vay
                lblTimelineTitle.Text = $"Tiến độ kỳ hạn khoản vay {daysText}:";
                lblTimelineDates.Text = $"{kv.NgayDangKy:dd/MM/yyyy} (Bắt đầu)                                                                       {kv.NgayDaoHan:dd/MM/yyyy} (Đáo hạn)";

                double totalDays = Math.Max(1.0, (kv.NgayDaoHan.Date - kv.NgayDangKy.Date).TotalDays);
                double elapsedDays = Math.Max(0.0, Math.Min(totalDays, (DateTime.Now.Date - kv.NgayDangKy.Date).TotalDays));
                int trackWidth = Math.Max(10, pnlLoanTrack.ClientSize.Width);
                pnlLoanBar.Width = Math.Max(10, (int)(trackWidth * (elapsedDays / totalDays)));

                btnPayDebt.Enabled = true;
                btnPayDebt.Text = "Thanh toán ngay";
                btnPayDebt.NormalColor = Color.FromArgb(37, 99, 235);
                btnPayDebt.HoverColor = Color.FromArgb(29, 78, 216);
            }
            else
            {
                pnlDebtActive.Visible = false;
                pnlDebtEmpty.Visible = true;

                lblDebtStatus.Text = "An toàn";
                lblDebtStatus.ForeColor = Color.FromArgb(22, 163, 74);
            }
        }

        /// <summary>
        /// Cập nhật thẻ Quản lý hạn mức chi tiêu cá nhân (Hạn mức ngày và tuần).
        /// </summary>
        private void UpdateSpendingCardUI()
        {
            var stats = _summary.Spending ?? new SpendingStats();

            // Thống kê ngày
            lblDailySpent.Text = $"Đã chi: {stats.FormattedDaChiHomNay} / {stats.FormattedHanMucNgay} (Ngày)";
            lblDailyRemaining.Text = $"Còn lại: {stats.FormattedConLaiHomNay} ({stats.PhanTramNgay}%)";

            // Thống kê tuần
            lblWeeklySpent.Text = $"Đã chi: {stats.FormattedDaChiTuanNay} / {stats.FormattedHanMucTuan} (Tuần)";
            lblWeeklyRemaining.Text = $"Còn lại: {stats.FormattedConLaiTuanNay} ({stats.PhanTramTuan}%)";

            // Huy hiệu (Badge) cảnh báo vượt hạn mức
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

        /// <summary>
        /// Tính toán chiều rộng thanh tiến độ chi tiêu theo tỷ lệ phần trăm (0-100%) và tự đổi màu cảnh báo.
        /// </summary>
        private void UpdateProgressBars()
        {
            if (_summary == null || _summary.Spending == null) return;
            var stats = _summary.Spending;

            // 1. Thanh tiến độ ngày
            int dailyTrackWidth = Math.Max(10, pnlDailyProgressTrack.ClientSize.Width);
            double dailyPercent = Math.Max(0.0, Math.Min(1.0, (double)stats.PhanTramNgay / 100.0));
            int dailyBarWidth = (int)(dailyTrackWidth * dailyPercent);
            pnlDailyProgressBar.Width = Math.Max(6, dailyBarWidth);

            if (stats.VuotMucNgay)
            {
                pnlDailyProgressBar.FillColor = Color.FromArgb(239, 68, 68);
            }
            else if (stats.PhanTramNgay >= 80)
            {
                pnlDailyProgressBar.FillColor = Color.FromArgb(245, 158, 11);
            }
            else
            {
                pnlDailyProgressBar.FillColor = Color.FromArgb(37, 99, 235);
            }

            // 2. Thanh tiến độ tuần
            int weeklyTrackWidth = Math.Max(10, pnlWeeklyProgressTrack.ClientSize.Width);
            double weeklyPercent = Math.Max(0.0, Math.Min(1.0, (double)stats.PhanTramTuan / 100.0));
            int weeklyBarWidth = (int)(weeklyTrackWidth * weeklyPercent);
            pnlWeeklyProgressBar.Width = Math.Max(6, weeklyBarWidth);

            if (stats.VuotMucTuan)
            {
                pnlWeeklyProgressBar.FillColor = Color.FromArgb(239, 68, 68);
            }
            else if (stats.PhanTramTuan >= 80)
            {
                pnlWeeklyProgressBar.FillColor = Color.FromArgb(245, 158, 11);
            }
            else
            {
                pnlWeeklyProgressBar.FillColor = Color.FromArgb(16, 185, 129);
            }

            // 3. Thanh tiến độ khoản vay
            if (_summary.KhoanVaySapToi != null && pnlLoanTrack.ClientSize.Width > 10)
            {
                var kv = _summary.KhoanVaySapToi;
                double totalDays = Math.Max(1.0, (kv.NgayDaoHan.Date - kv.NgayDangKy.Date).TotalDays);
                double elapsedDays = Math.Max(0.0, Math.Min(totalDays, (DateTime.Now.Date - kv.NgayDangKy.Date).TotalDays));
                int trackWidth = pnlLoanTrack.ClientSize.Width;
                pnlLoanBar.Width = Math.Max(10, (int)(trackWidth * (elapsedDays / totalDays)));
            }
        }

        /// <summary>
        /// Nạp danh sách các giao dịch mới nhất vào danh sách mini ở góc phải giao diện.
        /// </summary>
        private async Task LoadRecentTransactionsAsync()
        {
            try
            {
                DataTable dt = await Task.Run(() => _transactionService.GetAllTransactions());

                pnlRecentList.SuspendLayout();
                pnlRecentList.Controls.Clear();

                if (dt != null && dt.Rows.Count > 0)
                {
                    int maxCount = Math.Min(6, dt.Rows.Count);
                    for (int i = 0; i < maxCount; i++)
                    {
                        DataRow row = dt.Rows[i];
                        Panel rowPanel = CreateRecentTransactionRow(row);
                        rowPanel.Dock = DockStyle.Top;
                        pnlRecentList.Controls.Add(rowPanel);
                        rowPanel.BringToFront();
                    }
                }
                else
                {
                    Label lblEmpty = new Label
                    {
                        Text = "Chưa có giao dịch gần đây.",
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                        ForeColor = Color.FromArgb(148, 163, 184)
                    };
                    pnlRecentList.Controls.Add(lblEmpty);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[DashboardUserForm] LoadRecentTransactionsAsync error: {ex.Message}");
            }
            finally
            {
                pnlRecentList.ResumeLayout();
            }
        }

        /// <summary>
        /// Tạo một mục dòng giao dịch với biểu tượng mũi tên Thu (xanh lá) / Chi (đỏ) và số tiền trực quan.
        /// </summary>
        private Panel CreateRecentTransactionRow(DataRow row)
        {
            string noiDung = row["NoiDung"]?.ToString() ?? "Giao dịch";
            DateTime ngayGD = row["NgayGiaoDich"] != DBNull.Value ? Convert.ToDateTime(row["NgayGiaoDich"]) : DateTime.Now;
            string loai = row["Loai"]?.ToString() ?? "";
            string phanLoai = row["PhanLoai"]?.ToString() ?? "";
            decimal soTien = row["SoTien"] != DBNull.Value ? Convert.ToDecimal(row["SoTien"]) : 0m;
            string loaiTien = row["LoaiTien"]?.ToString() ?? "VND";
            string stk = row["SoTaiKhoan"]?.ToString() ?? "";

            bool isIncome = string.Equals(loai, "Thu", StringComparison.OrdinalIgnoreCase) ||
                            string.Equals(phanLoai, "INCOME", StringComparison.OrdinalIgnoreCase);

            Color amountColor = soTien == 0 ? Color.FromArgb(100, 116, 139) : (isIncome ? Color.FromArgb(22, 163, 74) : Color.FromArgb(220, 38, 38));
            string amountPrefix = soTien == 0 ? "" : (isIncome ? "+" : "-");
            string amtFormatted = soTien == 0 ? $"0 {loaiTien}" : $"{amountPrefix}{soTien:N0} {loaiTien}";

            Panel pnl = new Panel
            {
                Height = 44,
                Padding = new Padding(0, 4, 0, 4),
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };

            // Biểu tượng tròn chỉ báo dòng tiền
            RoundedPanel pnlIcon = new RoundedPanel
            {
                Size = new Size(28, 28),
                Location = new Point(4, 8),
                BorderRadius = 14,
                BorderSize = 0,
                FillColor = isIncome ? Color.FromArgb(240, 253, 244) : Color.FromArgb(254, 242, 242),
                BackColor = Color.Transparent
            };

            Label lblIcon = new Label
            {
                Dock = DockStyle.Fill,
                Text = isIncome ? "↓" : "↑",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = isIncome ? Color.FromArgb(22, 163, 74) : Color.FromArgb(220, 38, 38),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlIcon.Controls.Add(lblIcon);

            // Cột bên phải: Số tiền và nhãn trạng thái Success
            Panel pnlRight = new Panel
            {
                Dock = DockStyle.Right,
                Width = 150,
                BackColor = Color.Transparent
            };

            Label lblAmount = new Label
            {
                Dock = DockStyle.Top,
                Height = 20,
                Text = amtFormatted,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = amountColor,
                TextAlign = ContentAlignment.MiddleRight
            };

            Label lblStatus = new Label
            {
                Dock = DockStyle.Top,
                Height = 16,
                Text = "✔ Thành công",
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(22, 163, 74),
                TextAlign = ContentAlignment.MiddleRight
            };

            pnlRight.Controls.Add(lblStatus);
            pnlRight.Controls.Add(lblAmount);

            // Cột trung tâm: Nội dung giao dịch và thời gian thực hiện
            Panel pnlText = new Panel
            {
                Location = new Point(38, 4),
                Size = new Size(Math.Max(100, pnl.Width - 195), 36),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent
            };

            Label lblContent = new Label
            {
                Dock = DockStyle.Top,
                Height = 18,
                Text = noiDung.Length > 32 ? noiDung.Substring(0, 32) + "..." : noiDung,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label lblDate = new Label
            {
                Dock = DockStyle.Top,
                Height = 16,
                Text = $"{ngayGD:dd/MM/yyyy HH:mm} • TK: {stk}",
                Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 116, 139),
                TextAlign = ContentAlignment.MiddleLeft
            };

            pnlText.Controls.Add(lblDate);
            pnlText.Controls.Add(lblContent);

            pnl.Controls.Add(pnlText);
            pnl.Controls.Add(pnlRight);
            pnl.Controls.Add(pnlIcon);

            // Hiệu ứng hover đổi màu nền nhẹ khi trỏ chuột
            pnl.MouseEnter += (s, e) => pnl.BackColor = Color.FromArgb(248, 250, 252);
            pnl.MouseLeave += (s, e) => pnl.BackColor = Color.White;

            // Nhấp vào bất kỳ điểm nào trên dòng đều điều hướng sang trang Lịch sử giao dịch
            pnl.Click += (s, e) => BtnNavHistory_Click(s, e);
            lblContent.Click += (s, e) => BtnNavHistory_Click(s, e);
            lblDate.Click += (s, e) => BtnNavHistory_Click(s, e);
            lblAmount.Click += (s, e) => BtnNavHistory_Click(s, e);
            lblStatus.Click += (s, e) => BtnNavHistory_Click(s, e);

            return pnl;
        }

        #endregion

        #region 5. XỬ LÝ SỰ KIỆN NÚT BẤM VÀ ĐIỀU HƯỚNG (USER ACTIONS & NAVIGATION)

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

        private void BtnNavHistory_Click(object sender, EventArgs e)
        {
            var main = this.TopLevelControl as MainForm ?? this.ParentForm as MainForm;
            main?.OpenFormWithCustomerCheck(() => new TransactionForm());
        }

        private async Task OpenQuickTransferDialogAsync()
        {
            using (var addForm = new AddTransactionForm())
            {
                addForm.StartPosition = FormStartPosition.CenterParent;
                if (addForm.ShowDialog(this) == DialogResult.OK)
                {
                    await LoadDashboardDataAsync();
                    TransactionService.NotifyTransactionChanged();
                }
            }
        }

        private void OpenCreditCardsForm()
        {
            var main = this.TopLevelControl as MainForm ?? this.ParentForm as MainForm;
            main?.OpenFormWithCustomerCheck(() => new CreditCardUserForm());
        }

        private void OpenSavingsForm()
        {
            var main = this.TopLevelControl as MainForm ?? this.ParentForm as MainForm;
            main?.OpenFormWithCustomerCheck(() => new SaveMoneyUserForm());
        }

        private void OpenBorrowingForm()
        {
            var main = this.TopLevelControl as MainForm ?? this.ParentForm as MainForm;
            main?.OpenFormWithCustomerCheck(() => new BorrowingUserForm());
        }

        /// <summary>
        /// Xử lý thanh toán nhanh khoản vay sắp tới hạn từ tài khoản thanh toán chính.
        /// </summary>
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

        /// <summary>
        /// Mở hộp thoại cài đặt giới hạn hạn mức chi tiêu cá nhân trong ngày và trong tuần.
        /// </summary>
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
