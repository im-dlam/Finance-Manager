using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Form đăng ký tài khoản người dùng mới (SignupForm).
    /// Giao diện đồng bộ với LoginForm:
    /// - Panel trái (pnlSide): Gradient tím-hồng và logo thương hiệu.
    /// - Panel phải (pnlMain): Các trường nhập liệu (Họ tên, Username, Chọn ngân hàng, Email, Mật khẩu, Xác nhận mật khẩu).
    /// </summary>
    public partial class SignupForm : Form
    {
        #region BIẾN TRẠNG THÁI (STATE VARIABLES)

        /// <summary>Cờ theo dõi trạng thái ẩn/hiện mật khẩu.</summary>
        private bool _passVisible = false;

        /// <summary>Cờ theo dõi trạng thái ẩn/hiện ô xác nhận mật khẩu.</summary>
        private bool _confirmVisible = false;

        /// <summary>Tên đăng nhập vừa được đăng ký thành công (dùng để điền sẵn vào LoginForm khi quay lại).</summary>
        public string RegisteredUsername { get; private set; }

        /// <summary>Tọa độ con trỏ chuột khi bắt đầu kéo form.</summary>
        private Point _dragStartCursor;

        /// <summary>Tọa độ form trên màn hình khi bắt đầu kéo.</summary>
        private Point _dragStartForm;

        /// <summary>Cờ đánh dấu đang trong trạng thái kéo form.</summary>
        private bool _isDragging = false;

        #endregion

        #region KHỞI TẠO FORM (CONSTRUCTOR)

        /// <summary>
        /// Khởi tạo màn hình đăng ký tài khoản.
        /// </summary>
        /// <param name="preselectedBank">Mã ngân hàng được chọn mặc định (nếu có).</param>
        public SignupForm(string preselectedBank = null)
        {
            InitializeComponent();

            // BƯỚC 1: Bo góc mềm mại cho các panel nhập liệu và form
            ApplyRoundedCorners();

            // BƯỚC 2: Tải danh sách ngân hàng đang hoạt động vào ComboBox
            LoadBanks(preselectedBank);

            // BƯỚC 3: Thiết lập các sự kiện bàn phím, nút bấm, kéo form
            SetupEvents();

            // BƯỚC 4: Cài đặt chữ gợi ý mờ (Placeholder) cho các ô nhập liệu
            SetupPlaceholders();
        }

        #endregion

        #region BO TRÒN GÓC GIAO DIỆN (ROUNDED CORNERS)

        /// <summary>
        /// Áp dụng đường viền bo góc cho Form và tất cả các ô nhập liệu.
        /// </summary>
        private void ApplyRoundedCorners()
        {
            SetRoundedRegion(pnlName, 10);
            SetRoundedRegion(pnlUser, 10);
            SetRoundedRegion(pnlBank, 10);
            SetRoundedRegion(pnlEmail, 10);
            SetRoundedRegion(pnlPass, 10);
            SetRoundedRegion(pnlConfirm, 10);
            SetRoundedRegion(btnSignup, 10);
            SetRoundedRegion(this, 16);
        }

        /// <summary>
        /// Cắt vùng hiển thị của control theo hình chữ nhật bo tròn.
        /// </summary>
        private static void SetRoundedRegion(Control ctrl, int radius)
        {
            if (ctrl.Width <= 0 || ctrl.Height <= 0) return;

            using (var path = RoundedPath(ctrl.ClientRectangle, radius))
            {
                ctrl.Region = new Region(path);
            }
        }

        /// <summary>
        /// Tạo đường viền hình học (GraphicsPath) bo cong 4 góc.
        /// </summary>
        private static GraphicsPath RoundedPath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        #endregion

        #region VẼ ĐỒ HỌA TRANG TRÍ (CUSTOM PAINTING)

        /// <summary>
        /// Sự kiện vẽ nền trang trí cho panel thương hiệu bên trái.
        /// </summary>
        private void PnlSide_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 1. Nền Gradient đường chéo từ màu tím sang hồng tím
            using (var brush = new LinearGradientBrush(
                pnlSide.ClientRectangle,
                Color.FromArgb(150, 0, 252),
                Color.FromArgb(230, 0, 180),
                LinearGradientMode.ForwardDiagonal))
            {
                g.FillRectangle(brush, pnlSide.ClientRectangle);
            }

            // 2. Các bong bóng bán trong suốt trang trí
            using (var c1 = new SolidBrush(Color.FromArgb(30, 255, 255, 255)))
                g.FillEllipse(c1, -80, -80, 300, 300);

            using (var c2 = new SolidBrush(Color.FromArgb(25, 255, 255, 255)))
                g.FillEllipse(c2, 250, 400, 320, 320);

            using (var c3 = new SolidBrush(Color.FromArgb(20, 255, 255, 255)))
                g.FillEllipse(c3, 300, -80, 220, 220);

            // 3. Đường gạch chéo thương hiệu
            using (var pen = new Pen(Color.FromArgb(15, 255, 255, 255), 60))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                g.DrawLine(pen, 420, 0, 0, 600);
            }
        }

        #endregion

        #region TẢI DANH SÁCH NGÂN HÀNG (LOAD BANKS)

        /// <summary>
        /// Nạp danh sách các ngân hàng từ Database vào ComboBox để người dùng lựa chọn ngân hàng mở tài khoản.
        /// </summary>
        private void LoadBanks(string preselectedBank = null)
        {
            try
            {
                var banks = NganHangService.GetPublicBanks();
                cboBank.DisplayMember = "TenNganHang";
                cboBank.ValueMember = "MaNganHang";
                cboBank.DataSource = banks;

                // Nếu có chỉ định sẵn ngân hàng
                if (!string.IsNullOrWhiteSpace(preselectedBank))
                {
                    cboBank.SelectedValue = preselectedBank;
                }
                else if (cboBank.Items.Count > 0)
                {
                    // Ưu tiên chọn ngân hàng Quân đội (MB) nếu có, ngược lại chọn ngân hàng đầu tiên
                    int mbIndex = -1;
                    for (int i = 0; i < banks.Count; i++)
                    {
                        if (banks[i].TenNganHang.IndexOf("MB", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            mbIndex = i;
                            break;
                        }
                    }
                    cboBank.SelectedIndex = mbIndex >= 0 ? mbIndex : 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[SignupForm] LoadBanks error: " + ex.Message);
            }
        }

        #endregion

        #region ĐĂNG KÝ SỰ KIỆN GIAO DIỆN (EVENT SETUP)

        /// <summary>
        /// Gắn các sự kiện bàn phím, click và điều hướng cho giao diện.
        /// </summary>
        private void SetupEvents()
        {
            // 1. Nút Đóng Form (✕)
            btnClose.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; };

            // 2. Kéo thả cửa sổ bằng chuột thuần (Managed Dragging)
            pnlSide.MouseDown += DragMouseDown;
            pnlSide.MouseMove += DragMouseMove;
            pnlSide.MouseUp += DragMouseUp;

            pnlMain.MouseDown += DragMouseDown;
            pnlMain.MouseMove += DragMouseMove;
            pnlMain.MouseUp += DragMouseUp;

            lblBrand.MouseDown += DragMouseDown;
            lblBrand.MouseMove += DragMouseMove;
            lblBrand.MouseUp += DragMouseUp;

            // 3. Hiệu ứng đổi màu viền chân ô nhập khi focus
            HookFocusLine(txtFullName, pnlNameLine);
            HookFocusLine(txtUsername, pnlUserLine);
            HookFocusLine(txtEmail, pnlEmailLine);
            HookFocusLine(txtPassword, pnlPassLine);
            HookFocusLine(txtConfirm, pnlConfirmLine);

            cboBank.Enter += (s, e) => pnlBankLine.BackColor = Color.FromArgb(150, 0, 252);
            cboBank.Leave += (s, e) => pnlBankLine.BackColor = Color.FromArgb(220, 222, 230);

            // 4. Nhấn Enter tại bất kỳ ô nào cũng kích hoạt đăng ký
            txtFullName.KeyDown += OnEnterSignup;
            txtUsername.KeyDown += OnEnterSignup;
            txtEmail.KeyDown += OnEnterSignup;
            txtPassword.KeyDown += OnEnterSignup;
            txtConfirm.KeyDown += OnEnterSignup;

            // 5. Nút ẩn/hiện mật khẩu
            btnTogglePass.Click += (s, e) =>
            {
                _passVisible = !_passVisible;
                txtPassword.UseSystemPasswordChar = !_passVisible;
                btnTogglePass.Text = _passVisible ? "🙈" : "👁";
            };

            // 6. Nút ẩn/hiện xác nhận mật khẩu
            btnToggleConfirm.Click += (s, e) =>
            {
                _confirmVisible = !_confirmVisible;
                txtConfirm.UseSystemPasswordChar = !_confirmVisible;
                btnToggleConfirm.Text = _confirmVisible ? "🙈" : "👁";
            };

            // 7. Nút Đăng ký tài khoản
            btnSignup.Click += BtnSignup_Click;

            // 8. Liên kết quay lại màn hình Đăng nhập (Sign in)
            linkLogin.LinkClicked += (s, e) => { this.DialogResult = DialogResult.Cancel; };
        }

        private void OnEnterSignup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Ngăn tiếng chuông bíp mặc định của Windows
                btnSignup.PerformClick();
            }
        }

        private void HookFocusLine(TextBox txt, Panel line)
        {
            txt.Enter += (s, e) => line.BackColor = Color.FromArgb(150, 0, 252);
            txt.Leave += (s, e) => line.BackColor = Color.FromArgb(220, 222, 230);
        }

        #endregion

        #region KÉO THẢ FORM THUẦN MANAGED (WINDOW DRAGGING)

        private void DragMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isDragging = true;
                _dragStartCursor = Cursor.Position;
                _dragStartForm = this.Location;
            }
        }

        private void DragMouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && e.Button == MouseButtons.Left)
            {
                Point cur = Cursor.Position;
                this.Location = new Point(
                    _dragStartForm.X + (cur.X - _dragStartCursor.X),
                    _dragStartForm.Y + (cur.Y - _dragStartCursor.Y)
                );
            }
        }

        private void DragMouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        #endregion

        #region XỬ LÝ CHỮ GỢI Ý (PLACEHOLDER)

        private void SetupPlaceholders()
        {
            SetPlaceholder(txtFullName, "Phat Tai Phat Loc");
            SetPlaceholder(txtUsername, "Nhập tên đăng nhập");
            SetPlaceholder(txtEmail, "team@36.com");
            SetPlaceholder(txtPassword, "Ít nhất 6 ký tự", true);
            SetPlaceholder(txtConfirm, "Nhập lại mật khẩu", true);
        }

        private void SetPlaceholder(TextBox txt, string placeholder, bool isPassword = false)
        {
            txt.Tag = placeholder;
            txt.ForeColor = Color.FromArgb(150, 155, 170);
            txt.Text = placeholder;
            if (isPassword) txt.UseSystemPasswordChar = false;

            txt.GotFocus += (s, e) =>
            {
                if (txt.Text == placeholder)
                {
                    txt.Text = "";
                    txt.ForeColor = Color.FromArgb(40, 45, 60);
                    if (isPassword)
                    {
                        bool visible = (txt == txtPassword) ? _passVisible : _confirmVisible;
                        txt.UseSystemPasswordChar = !visible;
                    }
                }
            };

            txt.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    txt.ForeColor = Color.FromArgb(150, 155, 170);
                    txt.Text = placeholder;
                    if (isPassword) txt.UseSystemPasswordChar = false;
                }
            };
        }

        private bool IsPlaceholder(TextBox txt) => txt.Text == (txt.Tag as string);

        private string GetValue(TextBox txt) => IsPlaceholder(txt) ? "" : txt.Text.Trim();

        #endregion

        #region XỬ LÝ ĐĂNG KÝ (SIGNUP LOGIC)

        /// <summary>
        /// Xử lý xác thực dữ liệu và đăng ký tài khoản người dùng mới.
        /// </summary>
        private void BtnSignup_Click(object sender, EventArgs e)
        {
            // BƯỚC 1: Lấy các giá trị người dùng nhập vào
            string fullName = GetValue(txtFullName);
            string username = GetValue(txtUsername);
            string email = GetValue(txtEmail);
            string password = IsPlaceholder(txtPassword) ? "" : txtPassword.Text;
            string confirm = IsPlaceholder(txtConfirm) ? "" : txtConfirm.Text;
            string bankCode = cboBank.SelectedValue?.ToString() ?? "NH01";

            // BƯỚC 2: Kiểm tra tính hợp lệ dữ liệu đầu vào (Validation)
            if (string.IsNullOrWhiteSpace(fullName))
            {
                ShowError("⚠  Vui lòng nhập họ tên.");
                txtFullName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(username) || username.Length < 4)
            {
                ShowError("⚠  Tên đăng nhập phải có ít nhất 4 ký tự.");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                ShowError("⚠  Email không hợp lệ.");
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                ShowError("⚠  Mật khẩu phải có ít nhất 6 ký tự.");
                txtPassword.Focus();
                return;
            }

            if (password != confirm)
            {
                ShowError("✖  Mật khẩu xác nhận không khớp.");
                txtConfirm.SelectAll();
                txtConfirm.Focus();
                return;
            }

            if (!chkTerms.Checked)
            {
                ShowError("⚠  Bạn phải đồng ý với Điều khoản dịch vụ.");
                return;
            }

            // BƯỚC 3: Gọi AuthService để ghi nhận thông tin vào Cơ sở dữ liệu SQL Server
            AuthResult result = AuthService.Register(fullName, username, email, password, bankCode);

            if (!result.Success)
            {
                ShowError("✖  " + result.Message);
                return;
            }

            // BƯỚC 4: Đăng ký thành công -> Thông báo và đóng form về LoginForm
            HideError();
            MessageBox.Show(result.Message, "Bank Management",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Ghi nhớ tên đăng nhập để LoginForm tự động điền sẵn
            RegisteredUsername = username;

            // Đóng hộp thoại trả về OK
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Kiểm tra định dạng địa chỉ Email bằng biểu thức chính quy (Regex).
        /// </summary>
        private static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }

        private void ShowError(string msg)
        {
            lblError.Text = msg;
            lblError.Visible = true;
        }

        private void HideError()
        {
            lblError.Visible = false;
            lblError.Text = "";
        }

        #endregion
    }
}
