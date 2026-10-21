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
    public partial class MainForm : Form
    {
        private Button _activeMenuButton;
        private ComboBox cboMainTierSelector;
        private Label lblMainTier;
        private UserTierTheme _currentTheme;
        private bool _isSyncingTier = false;

        // --- MENU ACTIVE INDICATOR & ANIMATION ---
        private RoundedPanel _pnlMenuIndicator;
        private Timer _indicatorAnimTimer;
        private float _currentIndicatorY = 0f;
        private float _targetIndicatorY = 0f;
        private static readonly Font _fontRegular = new Font("Segoe UI Semibold", 9.5F, FontStyle.Regular);
        private static readonly Font _fontBold = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);


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
        /// Khởi tạo MainForm với user đã đăng nhập.
        /// </summary>
        /// <param name="user">User hiện tại (bắt buộc, không được null).</param>
        public MainForm(User user)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            _activeMenuButton = btnDashboards;
            InitializeMenuIndicator();
            this.DoubleBuffered = true;
            SetDoubleBuffered(pnMain, true);
            SetDoubleBuffered(panel1, true);
            SetDoubleBuffered(pnTitlebar, true);


            InitializeTopBarTierSelector();
            TransactionService.OnTransactionChanged += OnSystemTransactionOccurred;
            this.FormClosed += (s, e) => TransactionService.OnTransactionChanged -= OnSystemTransactionOccurred;

            // Lắng nghe sự kiện đổi hạng toàn cục
            CurrentSession.OnTierChanged += tierKey =>
            {
                if (this.IsDisposed || !this.IsHandleCreated) return;
                if (CurrentSession.IsAdmin) return; // Admin luon giu mau tieu chuan, khong doi mau
                this.BeginInvoke((Action)(() =>
                {
                    ApplyTierTheme(UserTierTheme.GetTheme(tierKey));
                    SyncTopBarTierSelector(tierKey);
                }));
            };

            // Vẽ viền tinh tế cho Sidebar và Topbar
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

            // Áp dụng màu giao diện ngay khi khởi động: Admin dùng màu tiêu chuẩn, Member dùng theo hạng
            if (CurrentSession.IsAdmin)
            {
                ApplyTierTheme(UserTierTheme.GetTheme("Standard"));
            }
            else
            {
                // Cập nhật hạng ban đầu theo số dư tài khoản thực tế
                decimal initBal = CurrentSession.GetUserTotalBalance();
                UserTierTheme.UpdateSessionTierByBalance(initBal);

                string initTier = !string.IsNullOrWhiteSpace(CurrentSession.SelectedUserTier)
                    ? CurrentSession.SelectedUserTier
                    : "Hạng Tiêu Chuẩn";
                ApplyTierTheme(UserTierTheme.GetTheme(initTier));
            }

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
            ApplyRolePermissions();
        }

        private void InitializeTopBarTierSelector()
        {
            // BÊN ADMIN: Không tạo hộp chọn đổi màu/hạng, giữ nguyên màu chuẩn
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

            cboMainTierSelector.SelectedIndexChanged += (s, e) =>
            {
                if (_isSyncingTier) return;
                int idx = cboMainTierSelector.SelectedIndex;
                if (idx == 4) // Tự động (Theo số dư)
                {
                    CurrentSession.IsManualTierOverride = false;
                    decimal bal = CurrentSession.GetUserTotalBalance();
                    CurrentSession.SelectedUserTier = UserTierTheme.DetermineTierByBalance(bal);
                    return;
                }

                // Người dùng chủ động test đổi hạng qua ComboBox
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

        public void ApplyTierTheme(UserTierTheme theme)
        {
            if (theme == null) return;
            _currentTheme = theme;

            // 1. THANH MENU BÊN TRÁI (SIDEBAR) - ĐỔI MÀU THEO HẠNG
            panel1.BackColor = theme.SidebarBg;
            panel2.BackColor = theme.SidebarHeaderBg;
            panel3.BackColor = theme.SidebarBg;
            panel4.BackColor = theme.SidebarBg;

            // 2. Logo / Tiêu đề "Bank Manager"
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

            // 5. Vẽ lại viền Menu & Topbar
            panel1.Invalidate();
            pnTitlebar.Invalidate();

            // LƯU Ý: Phần nội dung (pnMain và các Form con) GIỮ NGUYÊN GIAO DIỆN GỐC, KHÔNG ĐỔI MÀU
        }

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
                    _currentIndicatorY += diff * 0.28f;
                }
                _pnlMenuIndicator.Top = (int)Math.Round(_currentIndicatorY);
            };
        }

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

        private int GetMenuIndexForButton(Button btn)
        {
            Button[] list = CurrentSession.IsAdmin
                ? new Button[] { btnDashboards, btnAccounts, btnTransactions, btnCustomers, btnBanks, btnCreditCard, btnShop, btnBorrowing, btnSaveMoney, btnStatistic, btnSettings }
                : new Button[] { btnDashboards, btnAccounts, btnTransactions, btnCreditCard, btnShop, btnBorrowing, btnSaveMoney, btnStatistic, btnSettings };
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == btn) return i;
            }
            return 0;
        }

        private int GetMenuIndexForForm(Form form)
        {
            if (form is DashboardUserForm || form is DashboardForm) return GetMenuIndexForButton(btnDashboards);
            if (form is AccountForm) return GetMenuIndexForButton(btnAccounts);
            if (form is TransactionForm || form is AddTransactionForm) return GetMenuIndexForButton(btnTransactions);
            if (form is CustomerForm) return GetMenuIndexForButton(btnCustomers);
            if (form is BankForm) return GetMenuIndexForButton(btnBanks);
            if (form is CreditCardForm || form is CreditCardUserForm) return GetMenuIndexForButton(btnCreditCard);
            if (form is ShopForm || form is ShopAdminForm || form is ShopUserForm) return GetMenuIndexForButton(btnShop);
            if (form is BorrowingForm || form is BorrowingAdminForm || form is BorrowingUserForm) return GetMenuIndexForButton(btnBorrowing);
            if (form is SaveMoneyForm || form is SaveMoneyAdminForm || form is SaveMoneyUserForm) return GetMenuIndexForButton(btnSaveMoney);
            if (form is StatisticsForm) return GetMenuIndexForButton(btnStatistic);
            if (form is SettingsForm) return GetMenuIndexForButton(btnSettings);
            return 0;
        }

        /// <summary>
        /// Hiển thị Form con bên trong pnMain tức thì ("bấm phát hiện luôn"),
        /// đồng thời thanh menu lướt indicator mượt mà theo màu hạng tài khoản.
        /// </summary>
        public void LoadMultiForm(Form form)
        {
            if (form == null) return;

            // 1. Đồng bộ highlight và lướt indicator trên menu bên trái
            SyncActiveMenuButtonForForm(form);

            // 2. Nạp form mới vào pnMain tức thì, tránh nhấp nháy bằng Suspend/Resume
            pnMain.SuspendLayout();
            try
            {
                DisposeCurrentForm();

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
        /// Dispose form hiện tại trong pnMain (nếu có).
        /// An toàn với try/catch - không throw ra ngoài.
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
                    System.Diagnostics.Debug.WriteLine(
                        $"[LoadMultiForm] Dispose error: {ex.Message}");
                }
            }
        }
        /// <summary>
        /// Phân quyền dựa trên vai trò của người dùng hiện tại (Admin/Member).
        /// </summary>
        private void ApplyRolePermissions()
        {
            Console.WriteLine($"CurrentSession.IsAdmin: {CurrentSession.IsAdmin}");

            if (CurrentSession.IsAdmin)
            {
                btnCustomers.Visible = true;
                btnBanks.Visible = true;
                btnStatistic.Text = "   Báo cáo";
                ArrangeSidebarMenu(isAdmin: true);

                // Admin không đổi màu giao diện: xóa/ẩn hộp chọn đổi hạng và cố định màu tiêu chuẩn
                if (lblMainTier != null) lblMainTier.Visible = false;
                if (cboMainTierSelector != null) cboMainTierSelector.Visible = false;
                ApplyTierTheme(UserTierTheme.GetTheme("Standard"));
                return;
            }

            // Member — ẩn menu quản trị khách hàng và ngân hàng
            btnCustomers.Visible = false;
            btnCustomers.Height = 0;

            btnBanks.Visible = false;
            btnBanks.Height = 0;

            btnStatistic.Text = "   Thống kê";

            // Kéo các nút chức năng ở dưới lên liền mạch
            ArrangeSidebarMenu(isAdmin: false);
        }

        /// <summary>
        /// Sắp xếp vị trí các nút menu bên trái theo thứ tự và khoảng cách chuẩn (46px),
        /// loại bỏ hoàn toàn khoảng cách trống khi ẩn các mục quản trị ở phía Member.
        /// </summary>
        /// <param name="isAdmin">True nếu là tài khoản Quản trị viên, False nếu là Khách hàng (Member).</param>
        private void ArrangeSidebarMenu(bool isAdmin)
        {
            int currentTop = 86;
            const int buttonHeight = 38;
            const int spacing = 4;
            const int buttonWidth = 154;
            const int buttonLeft = 16;
            int step = buttonHeight + spacing; // 42px

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

            if (_activeMenuButton != null && _pnlMenuIndicator != null)
            {
                _targetIndicatorY = _activeMenuButton.Top + (_activeMenuButton.Height - _pnlMenuIndicator.Height) / 2;
                _currentIndicatorY = _targetIndicatorY;
                _pnlMenuIndicator.Top = (int)_currentIndicatorY;
                _pnlMenuIndicator.Visible = true;
                _pnlMenuIndicator.BringToFront();
            }
        }

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
        
        /// <summary>
        /// Dọn dẹp form con trong pnMain TRƯỚC KHI MainForm đóng.
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _indicatorAnimTimer?.Stop();
            _indicatorAnimTimer?.Dispose();
            DisposeChildForms();
            base.OnFormClosed(e);
        }

        /// <summary>
        /// Dispose tất cả form con đang nhúng trong pnMain.
        /// </summary>
        private void DisposeChildForms()
        {
            if (pnMain == null || pnMain.IsDisposed)
                return;

            if (pnMain.Controls.Count == 0)
                return;

            var children = new System.Collections.Generic.List<Control>();
            foreach (Control c in pnMain.Controls)
                children.Add(c);

            pnMain.Controls.Clear();

            foreach (var ctrl in children)
            {
                try { ctrl.Dispose(); }
                catch { }
            }
        }

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

            var infoForm = new CustomerInfoForm();

            infoForm.SaveSuccess += (s, args) =>
            {
                System.Diagnostics.Debug.WriteLine(
                    "[MainForm] SaveSuccess — mở form đích");

                LoadMultiForm(formFactory());
            };

            LoadMultiForm(infoForm);
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
        private void OnSystemTransactionOccurred()
        {
            if (!CurrentSession.IsMember || this.IsDisposed || !this.IsHandleCreated) return;
            this.BeginInvoke((Action)(() =>
            {
                decimal totalBal = CurrentSession.GetUserTotalBalance();
                UserTierTheme.UpdateSessionTierByBalance(totalBal);
            }));
        }
    }

}
