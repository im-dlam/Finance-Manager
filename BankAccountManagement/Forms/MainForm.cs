using BankAccountManagement.Forms;
using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountManagement
{
    /// <summary>
    /// Form giao diện chính (Main Shell Form) của toàn bộ ứng dụng.
    /// <para>
    /// Đóng vai trò là cửa sổ điều hướng trung tâm (SPA - Single Page Application container trong WinForms):
    /// - Quản lý thanh điều hướng Sidebar bên trái (Menu động theo phân quyền Admin / Member).
    /// - Quản lý thanh Topbar tiêu đề &amp; bộ chuyển đổi hạng tài khoản (User Tier Theme).
    /// - Quản lý vùng nội dung trung tâm (pnMain) để nhúng động các Form con (Sub-forms) như Dashboard, Account, Customer,...
    /// - Lắng nghe các sự kiện tài chính toàn cục để tự động đồng bộ giao diện và cập nhật cấp độ khách hàng theo thời gian thực.
    /// </para>
    /// </summary>
    public partial class MainForm : Form
    {
        #region 1. CÁC BIẾN TRẠNG THÁI VÀ GIAO DIỆN

        /// <summary>Nút menu đang được chọn (active) hiện tại trên Sidebar.</summary>
        private Button _activeMenuButton;

        /// <summary>Hộp chọn hạng thành viên trên Topbar (chỉ hiển thị cho Member để đổi giao diện màu sắc).</summary>
        private ComboBox cboMainTierSelector;

        /// <summary>Nhãn thông báo đổi hạng thành viên trên Topbar.</summary>
        private Label lblMainTier;

        /// <summary>Bảng màu chủ đề giao diện hiện tại tương ứng với hạng khách hàng.</summary>
        private UserTierTheme _currentTheme;

        /// <summary>Cờ đánh dấu đang trong quá trình đồng bộ chọn hạng ComboBox để tránh vòng lặp đệ quy sự kiện.</summary>
        private bool _isSyncingTier = false;

        /// <summary>Thanh chỉ báo (Indicator) bo góc màu nổi bật chạy dọc theo nút menu đang active.</summary>
        private RoundedPanel _pnlMenuIndicator;

        /// <summary>Bộ đếm thời gian (Timer) thực hiện hiệu ứng chuyển động mượt (Lerp animation) cho Indicator.</summary>
        private Timer _indicatorAnimTimer;

        /// <summary>Tọa độ trục Y hiện tại của Indicator trên giao diện.</summary>
        private float _currentIndicatorY = 0f;

        /// <summary>Tọa độ trục Y mục tiêu mà Indicator cần lướt tới.</summary>
        private float _targetIndicatorY = 0f;

        /// <summary>Phông chữ chuẩn cho các nút menu chưa active.</summary>
        private static readonly Font _fontRegular = new Font("Segoe UI Semibold", 9.5F, FontStyle.Regular);

        /// <summary>Phông chữ in đậm nổi bật cho nút menu đang được chọn active.</summary>
        private static readonly Font _fontBold = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);

        #endregion

        #region 2. KHỞI TẠO VÀ VÒNG ĐỜI (CONSTRUCTOR & LIFECYCLE)

        /// <summary>
        /// Kích hoạt kỹ thuật DoubleBuffered cho một Control bất kỳ thông qua Reflection
        /// nhằm chống hiện tượng chớp nháy (flickering) khi vẽ lại giao diện.
        /// </summary>
        /// <param name="control">Điều khiển cần bật DoubleBuffered.</param>
        /// <param name="enable">True để bật (mặc định), False để tắt.</param>
        public static void SetDoubleBuffered(Control control, bool enable = true)
        {
            if (control == null) return;
            try
            {
                var prop = typeof(Control).GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                prop?.SetValue(control, enable, null);
            }
            catch { }
        }

        /// <summary>
        /// Hàm dựng chính của MainForm. Khởi tạo cấu trúc giao diện, phân quyền người dùng và áp dụng chủ đề màu sắc.
        /// </summary>
        /// <param name="user">Thực thể người dùng hiện tại đã đăng nhập thành công từ LoginForm.</param>
        public MainForm(User user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // BƯỚC 1: Bật kỹ thuật Double Buffering cho cửa sổ chính và các Panel quan trọng để chống nhấp nháy
            this.DoubleBuffered = true;
            SetDoubleBuffered(pnMain, true);
            SetDoubleBuffered(panel1, true);
            SetDoubleBuffered(pnTitlebar, true);

            // BƯỚC 2: Khởi tạo thanh trượt chỉ báo Menu Indicator
            _activeMenuButton = btnDashboards;
            InitializeMenuIndicator();

            // BƯỚC 3: Khởi tạo hộp chọn cấp bậc thành viên (User Tier Selector) trên Topbar
            InitializeTopBarTierSelector();

            // BƯỚC 4: Đăng ký lắng nghe các sự kiện hệ thống (Giao dịch mới, Đổi hạng)
            TransactionService.OnTransactionChanged += OnSystemTransactionOccurred;
            this.FormClosed += (s, e) => TransactionService.OnTransactionChanged -= OnSystemTransactionOccurred;

            // Lắng nghe sự kiện người dùng đổi hạng tài khoản
            CurrentSession.OnTierChanged += tierKey =>
            {
                if (this.IsDisposed || !this.IsHandleCreated) return;
                if (CurrentSession.IsAdmin) return; // Admin luôn giữ giao diện chuẩn công vụ, không đổi màu

                this.BeginInvoke((Action)(() =>
                {
                    ApplyTierTheme(UserTierTheme.GetTheme(tierKey));
                    SyncTopBarTierSelector(tierKey);
                }));
            };

            // BƯỚC 5: Đăng ký hiệu ứng vẽ viền tinh tế cho Sidebar và Topbar
            panel1.Paint += (s, e) =>
            {
                if (_currentTheme != null)
                {
                    using (var pen = new Pen(_currentTheme.SidebarBorderColor, 1))
                    {
                        e.Graphics.DrawLine(pen, panel1.Width - 1, 0, panel1.Width - 1, panel1.Height);
                    }
                }
            };

            pnTitlebar.Paint += (s, e) =>
            {
                if (_currentTheme != null)
                {
                    using (var pen = new Pen(_currentTheme.TopBarBorderColor, 1))
                    {
                        e.Graphics.DrawLine(pen, 0, pnTitlebar.Height - 1, pnTitlebar.Width, pnTitlebar.Height - 1);
                    }
                }
            };

            // BƯỚC 6: Áp dụng chủ đề giao diện ban đầu (Admin dùng tiêu chuẩn, Member xác định theo số dư tài khoản)
            if (CurrentSession.IsAdmin)
            {
                ApplyTierTheme(UserTierTheme.GetTheme("Standard"));
            }
            else
            {
                decimal initBal = CurrentSession.GetUserTotalBalance();
                UserTierTheme.UpdateSessionTierByBalance(initBal);

                string initTier = !string.IsNullOrWhiteSpace(CurrentSession.SelectedUserTier)
                    ? CurrentSession.SelectedUserTier
                    : "Hạng Tiêu Chuẩn";
                ApplyTierTheme(UserTierTheme.GetTheme(initTier));
            }

            // BƯỚC 7: Nạp màn hình khởi đầu mặc định tương ứng với vai trò của người dùng
            if (CurrentSession.IsMember)
            {
                if (CurrentSession.NeedsCustomerInfo)
                {
                    OpenFormWithCustomerCheck(() => new DashboardUserForm());
                }
                else
                {
                    LoadMultiForm(new DashboardUserForm());
                }
            }
            else
            {
                LoadMultiForm(new DashboardForm());
            }

            // BƯỚC 8: Áp dụng phân quyền hiển thị các nút chức năng trên Sidebar
            ApplyRolePermissions();
        }

        /// <summary>
        /// Xử lý dọn dẹp tài nguyên Form con và bộ đếm Timer khi đóng cửa sổ chính.
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _indicatorAnimTimer?.Stop();
            _indicatorAnimTimer?.Dispose();
            DisposeChildForms();
            base.OnFormClosed(e);
        }

        #endregion

        #region 3. QUẢN LÝ THEME & CẤP BẬC THÀNH VIÊN (TIER THEMING)

        /// <summary>
        /// Khởi tạo ComboBox chọn hạng tài khoản trên Topbar dành riêng cho khách hàng.
        /// </summary>
        private void InitializeTopBarTierSelector()
        {
            // Nếu là Quản trị viên (Admin), ẩn bộ đổi màu này để giữ phong cách làm việc chuyên nghiệp
            if (CurrentSession.IsAdmin)
            {
                panel6.Width = 60;
                return;
            }

            panel6.Width = 430;

            lblMainTier = new Label
            {
                Name = "lblMainTier",
                Text = "🎨 Đổi Hạng:",
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(16, 11)
            };

            cboMainTierSelector = new ComboBox
            {
                Name = "cboMainTierSelector",
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                Location = new Point(96, 8),
                Width = 250,
                Cursor = Cursors.Hand
            };

            cboMainTierSelector.Items.AddRange(new object[]
            {
                "💎 Hạng Kim Cương (> 1 Tỷ)",
                "🥇 Hạng Vàng (500Tr - 1 Tỷ)",
                "🥈 Hạng Bạc (200Tr - 500Tr)",
                "⭐ Hạng Tiêu Chuẩn (< 200Tr)",
                "🔄 Tự động (Theo số dư)"
            });

            SyncTopBarTierSelector(CurrentSession.SelectedUserTier);

            // Khi người dùng tự tay chọn thay đổi chủ đề hạng từ danh sách thả xuống
            cboMainTierSelector.SelectedIndexChanged += (s, e) =>
            {
                if (_isSyncingTier) return;
                int idx = cboMainTierSelector.SelectedIndex;

                if (idx == 4) // Lựa chọn: "Tự động (Theo số dư)"
                {
                    CurrentSession.IsManualTierOverride = false;
                    decimal bal = CurrentSession.GetUserTotalBalance();
                    CurrentSession.SelectedUserTier = UserTierTheme.DetermineTierByBalance(bal);
                    return;
                }

                // Người dùng chủ động ép chọn một hạng nhất định để trải nghiệm giao diện
                CurrentSession.IsManualTierOverride = true;
                if (idx == 0) CurrentSession.SelectedUserTier = "Hạng Kim Cương";
                else if (idx == 1) CurrentSession.SelectedUserTier = "Hạng Vàng";
                else if (idx == 2) CurrentSession.SelectedUserTier = "Hạng Bạc";
                else if (idx == 3) CurrentSession.SelectedUserTier = "Hạng Tiêu Chuẩn";
            };

            panel6.Controls.Add(lblMainTier);
            panel6.Controls.Add(cboMainTierSelector);
            lblMainTier.BringToFront();
            cboMainTierSelector.BringToFront();
        }

        /// <summary>
        /// Đồng bộ vị trí lựa chọn trong ComboBox với tên hạng hiện tại của phiên đăng nhập.
        /// </summary>
        /// <param name="tierKey">Tên hạng (ví dụ: Kim Cương, Vàng, Bạc, Tiêu Chuẩn).</param>
        private void SyncTopBarTierSelector(string tierKey)
        {
            if (cboMainTierSelector == null) return;
            _isSyncingTier = true;
            try
            {
                string lower = (tierKey ?? "").ToLower();
                if (lower.Contains("kim cuong") || lower.Contains("kim c") || lower.Contains("diamond") || lower.Contains("den"))
                    cboMainTierSelector.SelectedIndex = 0;
                else if (lower.Contains("vang") || lower.Contains("v") || lower.Contains("gold"))
                    cboMainTierSelector.SelectedIndex = 1;
                else if (lower.Contains("bac") || lower.Contains("b") || lower.Contains("silver"))
                    cboMainTierSelector.SelectedIndex = 2;
                else
                    cboMainTierSelector.SelectedIndex = 3;
            }
            finally
            {
                _isSyncingTier = false;
            }
        }

        /// <summary>
        /// Áp dụng bộ màu sắc chủ đề (Theme) lên thanh Sidebar và Topbar.
        /// Không làm biến dạng màu nền nội dung công việc trung tâm của các Form con.
        /// </summary>
        /// <param name="theme">Đối tượng chứa các mã màu của hạng tài khoản tương ứng.</param>
        public void ApplyTierTheme(UserTierTheme theme)
        {
            if (theme == null) return;
            _currentTheme = theme;

            // 1. THANH MENU BÊN TRÁI (SIDEBAR) - ĐỔI MÀU THEO HẠNG
            panel1.BackColor = theme.SidebarBg;
            panel2.BackColor = theme.SidebarHeaderBg;
            panel3.BackColor = theme.SidebarBg;
            panel4.BackColor = theme.SidebarBg;

            // 2. Logo thương hiệu
            label1.ForeColor = theme.SidebarLogoColor;

            // 3. THANH TIÊU ĐỀ TRÊN CÙNG (TOPBAR) - ĐỔI MÀU THEO HẠNG
            pnTitlebar.BackColor = theme.TopBarBg;
            panel6.BackColor = theme.TopBarBg;
            panel7.BackColor = theme.TopBarBg;
            btnNotifications.BackColor = theme.TopBarBg;
            btnNotifications.ForeColor = theme.TopBarTextColor;

            if (lblMainTier != null)
            {
                lblMainTier.ForeColor = theme.TopBarTextColor;
            }
            if (cboMainTierSelector != null)
            {
                cboMainTierSelector.BackColor = Color.White;
                cboMainTierSelector.ForeColor = Color.Black;
            }

            // 4. Các nút bấm trên thanh Menu bên trái
            if (_pnlMenuIndicator != null)
            {
                _pnlMenuIndicator.FillColor = _currentTheme.AccentColor;
                _pnlMenuIndicator.Invalidate();
            }
            RefreshSidebarButtonStyles();

            // 5. Yêu cầu vẽ lại viền phân cách
            panel1.Invalidate();
            pnTitlebar.Invalidate();
        }

        #endregion

        #region 4. HIỆU ỨNG VÀ TRÌNH BÀY MENU SIDEBAR (ANIMATION & STYLING)

        /// <summary>
        /// Khởi tạo con trỏ chỉ báo Menu động (_pnlMenuIndicator) và Timer chuyển động nội suy.
        /// </summary>
        private void InitializeMenuIndicator()
        {
            if (_pnlMenuIndicator != null) return;

            Color accent = (_currentTheme != null && _currentTheme.AccentColor != Color.Empty)
                ? _currentTheme.AccentColor
                : Color.FromArgb(37, 99, 235);

            _pnlMenuIndicator = new RoundedPanel
            {
                Name = "_pnlMenuIndicator",
                Width = 4,
                Height = 24,
                Left = 4,
                BorderRadius = 4,
                BorderSize = 0,
                FillColor = accent,
                Visible = false
            };
            panel1.Controls.Add(_pnlMenuIndicator);
            _pnlMenuIndicator.BringToFront();

            // Timer chạy ở tần số ~60FPS (khoảng 16ms/tick) để tạo chuyển động trượt êm ái
            _indicatorAnimTimer = new Timer { Interval = 16 };
            _indicatorAnimTimer.Tick += (s, e) =>
            {
                float diff = _targetIndicatorY - _currentIndicatorY;
                if (Math.Abs(diff) < 0.6f)
                {
                    _currentIndicatorY = _targetIndicatorY;
                    _indicatorAnimTimer.Stop();
                }
                else
                {
                    // Thuật toán Lerp (Linear Interpolation) làm mềm gia tốc chuyển động
                    _currentIndicatorY += diff * 0.28f;
                }
                _pnlMenuIndicator.Top = (int)Math.Round(_currentIndicatorY);
            };
        }

        /// <summary>
        /// Làm mới màu sắc và kiểu chữ (Bold / Regular) cho tất cả các nút bấm trên Sidebar theo trạng thái Active.
        /// </summary>
        private void RefreshSidebarButtonStyles()
        {
            if (_currentTheme == null) return;

            Button[] allButtons = new Button[]
            {
                btnDashboards, btnAccounts, btnTransactions, btnCustomers, btnBanks,
                btnCreditCard, btnShop, btnBorrowing, btnSaveMoney, btnStatistic, btnSettings
            };

            Color accent = (_currentTheme.AccentColor != Color.Empty)
                ? _currentTheme.AccentColor
                : _currentTheme.SidebarButtonActiveText;

            foreach (var btn in allButtons)
            {
                if (btn == null) continue;

                bool isActive = (btn == _activeMenuButton);
                btn.BackColor = isActive ? _currentTheme.SidebarButtonActive : _currentTheme.SidebarBg;
                btn.ForeColor = isActive ? accent : _currentTheme.SidebarButtonText;
                btn.Font = isActive ? _fontBold : _fontRegular;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = _currentTheme.SidebarButtonHover;
                btn.FlatAppearance.MouseDownBackColor = _currentTheme.SidebarButtonActive;
            }
        }

        /// <summary>
        /// Thiết lập vùng bo tròn (Rounded Region) cho nút bấm thông qua GraphicsPath.
        /// </summary>
        /// <param name="btn">Nút bấm cần bo góc.</param>
        /// <param name="radius">Bán kính bo tròn góc (pixel).</param>
        private void ApplyRoundedButtonRegion(Button btn, int radius)
        {
            if (btn == null || btn.Width <= 0 || btn.Height <= 0) return;
            try
            {
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    int d = radius * 2;
                    int w = btn.Width;
                    int h = btn.Height;
                    path.AddArc(0, 0, d, d, 180, 90);
                    path.AddArc(w - d, 0, d, d, 270, 90);
                    path.AddArc(w - d, h - d, d, d, 0, 90);
                    path.AddArc(0, h - d, d, d, 90, 90);
                    path.CloseFigure();
                    var old = btn.Region;
                    btn.Region = new Region(path);
                    old?.Dispose();
                }
            }
            catch { }
        }

        /// <summary>
        /// Kích hoạt một nút menu trên Sidebar, cập nhật giao diện và dịch chuyển con trỏ chỉ báo.
        /// </summary>
        /// <param name="btn">Nút bấm được người dùng nhấp chọn.</param>
        private void SetActiveMenuButton(Button btn)
        {
            if (btn == null) return;

            bool changed = (_activeMenuButton != btn);
            _activeMenuButton = btn;
            RefreshSidebarButtonStyles();

            if (_pnlMenuIndicator != null)
            {
                float targetY = btn.Top + (btn.Height - _pnlMenuIndicator.Height) / 2f;
                _targetIndicatorY = targetY;

                if (!_pnlMenuIndicator.Visible || _currentIndicatorY <= 0)
                {
                    _currentIndicatorY = _targetIndicatorY;
                    _pnlMenuIndicator.Top = (int)Math.Round(_currentIndicatorY);
                    _pnlMenuIndicator.Visible = true;
                }
                else if (changed || !_indicatorAnimTimer.Enabled)
                {
                    _indicatorAnimTimer.Stop();
                    _indicatorAnimTimer.Start();
                }
                _pnlMenuIndicator.BringToFront();
            }
        }

        /// <summary>
        /// Phân quyền hiển thị danh mục chức năng dựa vào vai trò tài khoản (Admin vs Member).
        /// </summary>
        private void ApplyRolePermissions()
        {
            if (CurrentSession.IsAdmin)
            {
                btnCustomers.Visible = true;
                btnBanks.Visible = true;
                btnStatistic.Text = "   Báo cáo";
                ArrangeSidebarMenu(isAdmin: true);

                if (lblMainTier != null) lblMainTier.Visible = false;
                if (cboMainTierSelector != null) cboMainTierSelector.Visible = false;
                ApplyTierTheme(UserTierTheme.GetTheme("Standard"));
                return;
            }

            // Với khách hàng Member: ẩn danh mục quản lý khách hàng và liên kết ngân hàng hệ thống
            btnCustomers.Visible = false;
            btnCustomers.Height = 0;

            btnBanks.Visible = false;
            btnBanks.Height = 0;

            btnStatistic.Text = "   Thống kê";

            // Sắp xếp lại danh sách các nút để không để lại khoảng trống màu đen trên Sidebar
            ArrangeSidebarMenu(isAdmin: false);
        }

        /// <summary>
        /// Sắp xếp vị trí tọa độ các nút menu theo chiều dọc với khoảng cách chuẩn hóa,
        /// bảo đảm giao diện gọn gàng, liền mạch không có khoảng cách rỗng.
        /// </summary>
        /// <param name="isAdmin">True nếu là Quản trị viên, False nếu là Khách hàng.</param>
        private void ArrangeSidebarMenu(bool isAdmin)
        {
            int currentTop = 86;
            const int buttonHeight = 38;
            const int spacing = 4;
            const int buttonWidth = 154;
            const int buttonLeft = 16;
            int step = buttonHeight + spacing; // 42px mỗi nút

            Button[] menuButtons = isAdmin
                ? new Button[]
                {
                    btnDashboards,
                    btnAccounts,
                    btnTransactions,
                    btnCustomers,
                    btnBanks,
                    btnCreditCard,
                    btnShop,
                    btnBorrowing,
                    btnSaveMoney,
                    btnStatistic
                }
                : new Button[]
                {
                    btnDashboards,
                    btnAccounts,
                    btnTransactions,
                    btnCreditCard,
                    btnShop,
                    btnBorrowing,
                    btnSaveMoney,
                    btnStatistic
                };

            foreach (var btn in menuButtons)
            {
                btn.Left = buttonLeft;
                btn.Top = currentTop;
                btn.Width = buttonWidth;
                btn.Height = buttonHeight;
                btn.Visible = true;
                ApplyRoundedButtonRegion(btn, 8);
                currentTop += step;
            }

            // Nút "Cài đặt" luôn được cố định ở đáy thanh Sidebar
            if (btnSettings != null)
            {
                btnSettings.Dock = DockStyle.None;
                btnSettings.Left = buttonLeft;
                btnSettings.Width = buttonWidth;
                btnSettings.Height = buttonHeight;
                btnSettings.Top = panel1.ClientSize.Height - buttonHeight - 12;
                btnSettings.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                ApplyRoundedButtonRegion(btnSettings, 8);
            }

            // Định vị lại con trỏ chỉ báo nếu nút đang chọn đã đổi vị trí
            if (_activeMenuButton != null && _pnlMenuIndicator != null)
            {
                _targetIndicatorY = _activeMenuButton.Top + (_activeMenuButton.Height - _pnlMenuIndicator.Height) / 2;
                _currentIndicatorY = _targetIndicatorY;
                _pnlMenuIndicator.Top = (int)_currentIndicatorY;
                _pnlMenuIndicator.Visible = true;
                _pnlMenuIndicator.BringToFront();
            }
        }

        #endregion

        #region 5. QUẢN LÝ VÀ NHÚNG FORM CON (SUB-FORM SPA NAVIGATION)

        /// <summary>
        /// Nạp và hiển thị một Form con vào Panel nội dung trung tâm (pnMain).
        /// <para>
        /// Sử dụng kỹ thuật nhúng TopLevel = false, loại bỏ viền FormBorderStyle.None,
        /// và dọn dẹp sạch tài nguyên Form cũ trước đó để tránh rò rỉ bộ nhớ.
        /// </para>
        /// </summary>
        /// <param name="form">Đối tượng Form con cần hiển thị.</param>
        public void LoadMultiForm(Form form)
        {
            if (form == null) return;

            // BƯỚC 1: Đồng bộ nút menu và thanh lướt Indicator trên Sidebar tương ứng với Form con
            SyncActiveMenuButtonForForm(form);

            // BƯỚC 2: Tạm ngưng bố cục Panel để loại bỏ hiện tượng chớp hình (flicker)
            pnMain.SuspendLayout();
            try
            {
                // Hủy giải phóng an toàn Form đang hiển thị trước đó
                DisposeCurrentForm();

                // Cấu hình Form con hoạt động như một UserControl bên trong pnMain
                form.TopLevel = false;
                form.FormBorderStyle = FormBorderStyle.None;
                form.MinimumSize = Size.Empty;
                form.MaximumSize = Size.Empty;
                form.Dock = DockStyle.Fill;

                pnMain.Controls.Add(form);
                pnMain.Tag = form;
                form.Show();
            }
            finally
            {
                pnMain.ResumeLayout(true);
            }

            try
            {
                form.Focus();
            }
            catch { }
        }

        /// <summary>
        /// Tự động xác định và đồng bộ nút Sidebar active tương ứng với loại Form đang hiển thị.
        /// </summary>
        private void SyncActiveMenuButtonForForm(Form form)
        {
            if (form is DashboardUserForm || form is DashboardForm) SetActiveMenuButton(btnDashboards);
            else if (form is AccountForm) SetActiveMenuButton(btnAccounts);
            else if (form is TransactionForm || form is AddTransactionForm) SetActiveMenuButton(btnTransactions);
            else if (form is CustomerForm) SetActiveMenuButton(btnCustomers);
            else if (form is BankForm) SetActiveMenuButton(btnBanks);
            else if (form is CreditCardForm || form is CreditCardUserForm) SetActiveMenuButton(btnCreditCard);
            else if (form is ShopForm || form is ShopAdminForm || form is ShopUserForm) SetActiveMenuButton(btnShop);
            else if (form is BorrowingForm || form is BorrowingAdminForm || form is BorrowingUserForm) SetActiveMenuButton(btnBorrowing);
            else if (form is SaveMoneyForm || form is SaveMoneyAdminForm || form is SaveMoneyUserForm) SetActiveMenuButton(btnSaveMoney);
            else if (form is StatisticsForm) SetActiveMenuButton(btnStatistic);
            else if (form is SettingsForm) SetActiveMenuButton(btnSettings);
        }

        /// <summary>
        /// Đóng và giải phóng tài nguyên (Dispose) Form hiện tại đang nhúng trong vùng pnMain.
        /// </summary>
        private void DisposeCurrentForm()
        {
            if (pnMain.Controls.Count == 0) return;

            var oldControls = new System.Collections.Generic.List<Control>();
            foreach (Control c in pnMain.Controls)
            {
                oldControls.Add(c);
            }

            foreach (var ctrl in oldControls)
            {
                if (ctrl.IsDisposed) continue;

                try
                {
                    pnMain.Controls.Remove(ctrl);
                    if (ctrl is StatisticsForm sf)
                    {
                        sf.CleanupResources();
                    }
                    if (ctrl is Form f)
                    {
                        f.Close();
                    }
                    ctrl.Dispose();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[LoadMultiForm] Dispose error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Giải phóng toàn bộ các Form con đang nhúng trong pnMain khi MainForm đóng hoàn toàn.
        /// </summary>
        private void DisposeChildForms()
        {
            if (pnMain == null || pnMain.IsDisposed) return;
            if (pnMain.Controls.Count == 0) return;

            var children = new System.Collections.Generic.List<Control>();
            foreach (Control c in pnMain.Controls) children.Add(c);

            pnMain.Controls.Clear();

            foreach (var ctrl in children)
            {
                try { ctrl.Dispose(); }
                catch { }
            }
        }

        /// <summary>
        /// Kiểm tra xem thông tin khách hàng đã được hoàn tất hồ sơ hay chưa.
        /// <para>
        /// Nếu người dùng chưa cập nhật thông tin cá nhân (CCCD, địa chỉ, SĐT...), 
        /// điều hướng sang màn hình CustomerInfoForm trước; sau khi lưu hồ sơ xong sẽ tự mở Form đích mong muốn.
        /// </para>
        /// </summary>
        /// <param name="formFactory">Hàm lambda khởi tạo Form đích sau khi hồ sơ hợp lệ.</param>
        public void OpenFormWithCustomerCheck(Func<Form> formFactory)
        {
            if (CurrentSession.IsAdmin)
            {
                LoadMultiForm(formFactory());
                return;
            }

            if (!CurrentSession.NeedsCustomerInfo)
            {
                LoadMultiForm(formFactory());
                return;
            }

            // Chuyển hướng sang màn hình hoàn thiện hồ sơ thông tin khách hàng
            var infoForm = new CustomerInfoForm();
            infoForm.SaveSuccess += (s, args) =>
            {
                System.Diagnostics.Debug.WriteLine("[MainForm] SaveSuccess — hồ sơ hoàn tất, mở form đích");
                LoadMultiForm(formFactory());
            };

            LoadMultiForm(infoForm);
        }

        #endregion

        #region 6. XỬ LÝ SỰ KIỆN CLICK MENU SIDEBAR (MENU ACTIONS)

        private void btnDashboards_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnDashboards);
            if (CurrentSession.IsMember)
            {
                LoadMultiForm(new DashboardUserForm());
            }
            else
            {
                LoadMultiForm(new DashboardForm());
            }
        }

        private void btnAccounts_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnAccounts);
            OpenFormWithCustomerCheck(() => new AccountForm());
        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnTransactions);
            OpenFormWithCustomerCheck(() => new TransactionForm());
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            if (!CurrentSession.IsAdmin) return;
            SetActiveMenuButton(btnCustomers);
            LoadMultiForm(new CustomerForm());
        }

        private void btnBanks_Click(object sender, EventArgs e)
        {
            if (!CurrentSession.IsAdmin) return;
            SetActiveMenuButton(btnBanks);
            LoadMultiForm(new BankForm());
        }

        private void btnCreditCard_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnCreditCard);
            if (CurrentSession.IsAdmin)
            {
                LoadMultiForm(new CreditCardForm());
            }
            else
            {
                OpenFormWithCustomerCheck(() => new CreditCardUserForm());
            }
        }

        private void btnShop_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnShop);
            if (CurrentSession.IsAdmin)
            {
                LoadMultiForm(new ShopAdminForm());
            }
            else
            {
                OpenFormWithCustomerCheck(() => new ShopUserForm());
            }
        }

        private void btnBorrowing_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnBorrowing);
            if (CurrentSession.IsAdmin)
            {
                LoadMultiForm(new BorrowingAdminForm());
            }
            else
            {
                OpenFormWithCustomerCheck(() => new BorrowingUserForm());
            }
        }

        private void btnSaveMoney_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnSaveMoney);
            if (CurrentSession.IsAdmin)
            {
                LoadMultiForm(new SaveMoneyAdminForm());
            }
            else
            {
                OpenFormWithCustomerCheck(() => new SaveMoneyUserForm());
            }
        }

        private void btnStatistic_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnStatistic);
            LoadMultiForm(new StatisticsForm());
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SetActiveMenuButton(btnSettings);
            LoadMultiForm(new SettingsForm());
        }

        private void pnMain_Paint(object sender, PaintEventArgs e)
        {
        }

        #endregion

        #region 7. ĐỒNG BỘ GIAO DỊCH VÀ CẬP NHẬT THEO THỜI GIAN THỰC

        /// <summary>
        /// Xử lý sự kiện khi có bất kỳ giao dịch tài chính phát sinh trong toàn bộ hệ thống.
        /// Tự động cập nhật tổng số dư thực tế và nâng/hạ hạng tài khoản nếu đạt ngưỡng mới.
        /// </summary>
        private void OnSystemTransactionOccurred()
        {
            if (!CurrentSession.IsMember || this.IsDisposed || !this.IsHandleCreated) return;
            this.BeginInvoke((Action)(() =>
            {
                decimal totalBal = CurrentSession.GetUserTotalBalance();
                UserTierTheme.UpdateSessionTierByBalance(totalBal);
            }));
        }

        #endregion
    }
}
