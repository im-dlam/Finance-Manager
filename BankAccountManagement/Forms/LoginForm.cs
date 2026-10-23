using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Form đăng nhập chính của ứng dụng Bank Account Management.
    /// Thiết kế hiện đại dạng 2 cột:
    /// - Cột trái (pnlSide): Nền gradient tím-hồng, logo ngân hàng và thông điệp giới thiệu.
    /// - Cột phải (pnlMain): Khung nhập thông tin tài khoản (Username, Password), hiển thị lỗi và nút Đăng nhập.
    /// </summary>
    public partial class LoginForm : Form
    {
        #region BIẾN TRẠNG THÁI (STATE VARIABLES)

        /// <summary>Cờ theo dõi trạng thái ẩn/hiện mật khẩu.</summary>
        private bool _passwordVisible = false;

        /// <summary>Tọa độ con trỏ chuột khi bắt đầu nhấn giữ để kéo form.</summary>
        private Point _dragStartCursor;

        /// <summary>Tọa độ của Form trên màn hình khi bắt đầu kéo.</summary>
        private Point _dragStartForm;

        /// <summary>Cờ đánh dấu người dùng đang giữ chuột trái để kéo form.</summary>
        private bool _isDragging = false;

        #endregion

        #region KHỞI TẠO FORM (CONSTRUCTOR)

        /// <summary>
        /// Khởi tạo màn hình đăng nhập.
        /// </summary>
        /// <param name="prefilledUsername">Tên đăng nhập điền sẵn (thường lấy từ màn hình Đăng ký mới).</param>
        /// <param name="bankCode">Mã ngân hàng (nếu có).</param>
        public LoginForm(string prefilledUsername = null, string bankCode = null)
        {
            InitializeComponent();

            // Bật bộ đệm kép (DoubleBuffer) để chống hiện tượng giật/nháy hình khi vẽ giao diện
            this.DoubleBuffered = true;

            // BƯỚC 1: Bo góc mềm mại cho form và các panel nhập liệu
            ApplyRoundedCorners();

            // BƯỚC 2: Đăng ký các sự kiện bàn phím, chuột và placeholder
            SetupEvents();

            // BƯỚC 3: Điền sẵn tên đăng nhập nếu được truyền từ màn hình đăng ký thành công
            if (!string.IsNullOrWhiteSpace(prefilledUsername))
            {
                txtUsername.Text = prefilledUsername;
                txtUsername.ForeColor = Color.FromArgb(40, 45, 60);
                this.ActiveControl = txtPassword; // Tự động focus vào ô nhập mật khẩu
            }
        }

        #endregion

        #region BO TRÒN GÓC GIAO DIỆN (ROUNDED CORNERS)

        /// <summary>
        /// Áp dụng đường viền bo góc cho các thành phần điều khiển trên Form.
        /// </summary>
        private void ApplyRoundedCorners()
        {
            SetRoundedRegion(pnlUser, 10);
            SetRoundedRegion(pnlPass, 10);
            SetRoundedRegion(btnLogin, 10);
            SetRoundedRegion(this, 16);
        }

        /// <summary>
        /// Cắt vùng hiển thị (Region) của control theo hình chữ nhật bo góc.
        /// </summary>
        /// <param name="ctrl">Control cần bo góc.</param>
        /// <param name="radius">Bán kính bo tròn góc.</param>
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
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);                         // Góc trên-trái
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);                 // Góc trên-phải
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);           // Góc dưới-phải
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);                  // Góc dưới-trái
            path.CloseFigure();
            return path;
        }

        #endregion

        #region VẼ ĐỒ HỌA TRANG TRÍ (CUSTOM PAINTING)

        /// <summary>
        /// Sự kiện vẽ nền trang trí bên panel trái: gradient màu sắc kết hợp các hình tròn nghệ thuật.
        /// </summary>
        private void PnlSide_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias; // Khử răng cưa cho đường nét mềm mại

            // 1. Nền Gradient nghiêng 55 độ từ màu tím (#7800DC) sang hồng cánh sen (#C605FF)
            using (var brush = new LinearGradientBrush(
                pnlSide.ClientRectangle,
                Color.FromArgb(120, 0, 220),
                Color.FromArgb(198, 5, 255),
                55f))
            {
                g.FillRectangle(brush, pnlSide.ClientRectangle);
            }

            // 2. Vẽ các vòng tròn trang trí bán trong suốt tạo chiều sâu
            using (var c1 = new SolidBrush(Color.FromArgb(30, 255, 255, 255)))
                g.FillEllipse(c1, -80, -80, 300, 300);

            using (var c2 = new SolidBrush(Color.FromArgb(25, 255, 255, 255)))
                g.FillEllipse(c2, 250, 400, 320, 320);

            using (var c3 = new SolidBrush(Color.FromArgb(20, 255, 255, 255)))
                g.FillEllipse(c3, 300, -80, 220, 220);

            // 3. Đường thẳng nghệ thuật vắt ngang
            using (var pen = new Pen(Color.FromArgb(15, 255, 255, 255), 60))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                g.DrawLine(pen, 420, 0, 0, 600);
            }
        }

        #endregion

        #region ĐĂNG KÝ SỰ KIỆN GIAO DIỆN (EVENT SETUP)

        /// <summary>
        /// Gắn các sự kiện điều khiển bàn phím, chuột và điều hướng.
        /// </summary>
        private void SetupEvents()
        {
            // 1. Nút Đóng form (✕): Gán DialogResult = Cancel để Program.cs nhận diện thoát ứng dụng
            btnClose.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; };

            // 2. Kéo thả form bằng chuột thuần (Managed Dragging - Không dùng Win32 API P/Invoke để tránh lỗi bộ nhớ)
            pnlSide.MouseDown += DragMouseDown;
            pnlSide.MouseMove += DragMouseMove;
            pnlSide.MouseUp += DragMouseUp;

            pnlMain.MouseDown += DragMouseDown;
            pnlMain.MouseMove += DragMouseMove;
            pnlMain.MouseUp += DragMouseUp;

            lblBrand.MouseDown += DragMouseDown;
            lblBrand.MouseMove += DragMouseMove;
            lblBrand.MouseUp += DragMouseUp;

            // 3. Nhấn Enter tại ô Username hoặc Password để kích hoạt đăng nhập nhanh
            txtUsername.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnLogin.PerformClick(); } };
            txtPassword.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnLogin.PerformClick(); } };

            // 4. Hiệu ứng đổi màu đường viền chân ô nhập khi focus
            txtUsername.Enter += (s, e) => pnlUserLine.BackColor = Color.FromArgb(150, 0, 252);
            txtUsername.Leave += (s, e) => pnlUserLine.BackColor = Color.FromArgb(220, 222, 230);
            txtPassword.Enter += (s, e) => pnlPassLine.BackColor = Color.FromArgb(150, 0, 252);
            txtPassword.Leave += (s, e) => pnlPassLine.BackColor = Color.FromArgb(220, 222, 230);

            // 5. Nút ẩn/hiện mật khẩu (👁 / 🙈)
            btnTogglePass.Click += (s, e) =>
            {
                _passwordVisible = !_passwordVisible;
                txtPassword.UseSystemPasswordChar = !_passwordVisible;
                btnTogglePass.Text = _passwordVisible ? "🙈" : "👁";
            };

            // 6. Nút Đăng nhập
            btnLogin.Click += BtnLogin_Click;

            // 7. Liên kết chuyển sang Form Đăng ký tài khoản (SignupForm)
            linkSignup.LinkClicked += (s, e) =>
            {
                this.Hide(); // Ẩn LoginForm tạm thời
                using (var signup = new SignupForm())
                {
                    // Mở SignupForm modal, truyền this làm form cha
                    if (signup.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(signup.RegisteredUsername))
                    {
                        // Điền sẵn username vừa tạo thành công
                        txtUsername.Text = signup.RegisteredUsername;
                        txtUsername.ForeColor = Color.FromArgb(40, 45, 60);
                        this.ActiveControl = txtPassword;
                    }
                    GC.KeepAlive(signup); // Giữ an toàn bộ nhớ
                }
                this.Show(); // Hiện lại LoginForm
            };

            // 8. Cấu hình chữ gợi ý (Placeholder)
            SetPlaceholder(txtUsername, "Enter your username");
            SetPlaceholder(txtPassword, "Enter your password", isPassword: true);
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

        /// <summary>
        /// Tạo cơ chế hiển thị chữ gợi ý xám nhạt khi ô nhập trống và tự biến mất khi người dùng click vào.
        /// </summary>
        private void SetPlaceholder(TextBox txt, string placeholder, bool isPassword = false)
        {
            txt.Tag = placeholder;
            if (string.IsNullOrWhiteSpace(txt.Text) || txt.Text == placeholder)
            {
                txt.ForeColor = Color.FromArgb(150, 155, 170);
                txt.Text = placeholder;
                if (isPassword) txt.UseSystemPasswordChar = false;
            }

            txt.GotFocus += (s, e) =>
            {
                if (txt.Text == placeholder)
                {
                    txt.Text = "";
                    txt.ForeColor = Color.FromArgb(40, 45, 60);
                    if (isPassword) txt.UseSystemPasswordChar = !_passwordVisible;
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

        /// <summary>Kiểm tra xem nội dung trong TextBox có phải là chữ gợi ý placeholder hay không.</summary>
        private bool IsPlaceholder(TextBox txt) => txt.Text == (txt.Tag as string);

        #endregion

        #region XỬ LÝ ĐĂNG NHẬP (AUTHENTICATION)

        /// <summary>
        /// Xử lý logic khi người dùng nhấn nút "SIGN IN".
        /// </summary>
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            // BƯỚC 1: Lấy và chuẩn hóa dữ liệu đầu vào
            string user = IsPlaceholder(txtUsername) ? "" : txtUsername.Text.Trim();
            string pass = IsPlaceholder(txtPassword) ? "" : txtPassword.Text;

            // BƯỚC 2: Kiểm tra validation cơ bản
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                ShowError("⚠  Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.");
                return;
            }

            // BƯỚC 3: Gọi AuthService để xác thực tài khoản với Cơ sở dữ liệu SQL Server
            AuthResult result = AuthService.Login(user, pass);
            if (!result.Success)
            {
                // Đăng nhập thất bại: hiển thị thông báo lỗi thân thiện và chọn lại ô mật khẩu
                ShowError("✖  " + result.Message);
                txtPassword.SelectAll();
                txtPassword.Focus();
                return;
            }

            // BƯỚC 4: Đăng nhập thành công -> Lưu thông tin người dùng vào phiên làm việc toàn cục (CurrentSession)
            CurrentSession.SetUser(result.User);
            Console.WriteLine($"[Login] User '{CurrentSession.User.MaNguoiDung}' ({CurrentSession.User.Quyen}) logged in successfully.");
            HideError();

            // BƯỚC 5: Thiết lập DialogResult = OK để thoát modal loop về Program.cs và mở MainForm
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>Hiển thị nhãn thông báo lỗi màu đỏ.</summary>
        private void ShowError(string msg)
        {
            lblError.Text = msg;
            lblError.Visible = true;
        }

        /// <summary>Ẩn nhãn thông báo lỗi.</summary>
        private void HideError()
        {
            lblError.Visible = false;
            lblError.Text = "";
        }

        #endregion
    }
}
