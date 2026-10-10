using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Form đăng nhập của ứng dụng Bank Management.
    /// Kế thừa <see cref="Form"/>, dùng layout 2 cột:
    /// - Panel trái: gradient tím-hồng + logo, mang tính trang trí thương hiệu.
    /// - Panel phải: form nhập username/password với các control bo góc.
    /// 
    /// Chức năng chính:
    /// - Đăng nhập bằng username/password (hiện tạm so sánh cứng, sẽ thay bằng DB sau).
    /// - Cho phép ẩn/hiện mật khẩu, ghi nhớ đăng nhập.
    /// - Điều hướng sang <see cref="SignupForm"/> khi bấm "Sign up now".
    /// - Hỗ trợ kéo form vì đã tắt border mặc định của Windows.
    /// </summary>
    public partial class LoginForm : Form
    {
        // ==========================================================
        //  WIN32 API – dùng để kéo form không viền (borderless form)
        //  Vì FormBorderStyle = None nên mất thanh title bar mặc định,
        //  phải tự xử lý việc kéo thả bằng cách gửi message WM_NCLBUTTONDOWN
        //  tới Windows để nó hiểu "đang giữ chuột ở vùng caption".
        // ==========================================================

        /// <summary>
        /// Gửi một thông điệp (message) tới window handle chỉ định.
        /// Dùng để mô phỏng hành vi giữ chuột trên thanh tiêu đề của cửa sổ.
        /// </summary>
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        /// <summary>
        /// Giải phóng việc "bắt" chuột của control hiện tại,
        /// để chuột có thể tiếp tục gửi sự kiện tới cửa sổ đích.
        /// </summary>
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        /// <summary>Windows message: chuột trái vừa nhấn vào vùng non-client (caption, border...).</summary>
        private const int WM_NCLBUTTONDOWN = 0xA1;

        /// <summary>Mã vùng "caption" – vùng tiêu đề của cửa sổ.</summary>
        private const int HT_CAPTION = 0x2;

        // ==========================================================
        //  STATE
        // ==========================================================

        /// <summary>Trạng thái hiển thị mật khẩu (true = đang hiện dạng text, false = đang ẩn dạng ●).</summary>
        private bool _passwordVisible = false;

        /// <summary>
        /// Khởi tạo form đăng nhập.
        /// Trình tự: build UI → bo góc các control → gắn sự kiện.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();   // Sinh tự động từ Designer – tạo control, layout
            ApplyRoundedCorners();   // Bo góc các panel input, button, form
            SetupEvents();           // Đăng ký toàn bộ event handler
        }

        // ==========================================================
        //  BO GÓC (ROUNDED CORNERS)
        //  WinForms không có sẵn thuộc tính BorderRadius, nên phải
        //  tự tạo GraphicsPath và gán vào Region của control.
        // ==========================================================

        /// <summary>
        /// Bo góc cho các control chính trong form (input panel + button + form).
        /// Gọi 1 lần trong constructor vì kích thước control đã cố định.
        /// </summary>
        private void ApplyRoundedCorners()
        {
            SetRoundedRegion(pnlUser, 10);
            SetRoundedRegion(pnlPass, 10);
            SetRoundedRegion(btnLogin, 10);
            SetRoundedRegion(this, 16);      // Bo góc cho cả form
        }

        /// <summary>
        /// Gán vùng (Region) bo góc cho một control bất kỳ.
        /// Region quyết định phần nào của control được vẽ ra màn hình,
        /// nên khi gán path bo góc thì control sẽ hiển thị như hình bo tròn.
        /// </summary>
        /// <param name="ctrl">Control cần bo góc.</param>
        /// <param name="radius">Bán kính bo góc (pixel).</param>
        private static void SetRoundedRegion(Control ctrl, int radius)
        {
            // Bỏ qua nếu control chưa có kích thước (tránh lỗi khi control chưa layout xong)
            if (ctrl.Width <= 0 || ctrl.Height <= 0) return;

            using (var path = RoundedPath(ctrl.ClientRectangle, radius))
                ctrl.Region = new Region(path);
        }

        /// <summary>
        /// Tạo đường dẫn (GraphicsPath) là hình chữ nhật bo 4 góc.
        /// Cách làm: vẽ 4 cung tròn (AddArc) cho 4 góc, rồi nối lại bằng đường thẳng.
        /// </summary>
        /// <param name="rect">Hình chữ nhật gốc.</param>
        /// <param name="radius">Bán kính bo góc.</param>
        /// <returns>Đối tượng GraphicsPath mô tả hình bo góc.</returns>
        private static GraphicsPath RoundedPath(Rectangle rect, int radius)
        {
            int d = radius * 2;   // đường kính cung tròn
            var path = new GraphicsPath();

            // Góc trên-trái (bắt đầu từ 180°, quét 90°)
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            // Góc trên-phải
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            // Góc dưới-phải
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            // Góc dưới-trái
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);

            path.CloseFigure();
            return path;
        }
        /// <summary>
        /// Vẽ nền gradient và các hình trang trí cho panel bên trái.
        /// Được gọi tự động mỗi khi panel cần vẽ lại.
        /// </summary>
        private void PnlSide_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;   // vẽ mượt, không răng cưa

            // Gradient chéo 55° từ tím đậm (120,0,220) sang hồng tím (198,5,255)
            using (var brush = new LinearGradientBrush(
                pnlSide.ClientRectangle,
                Color.FromArgb(120, 0, 220),
                Color.FromArgb(198, 5, 255),
                55f))
            {
                g.FillRectangle(brush, pnlSide.ClientRectangle);
            }

            // 3 vòng tròn mờ tạo hiệu ứng "bokeh" nhẹ cho nền
            using (var c1 = new SolidBrush(Color.FromArgb(35, 255, 255, 255)))
                g.FillEllipse(c1, -90, -90, 300, 300);

            using (var c2 = new SolidBrush(Color.FromArgb(25, 255, 255, 255)))
                g.FillEllipse(c2, 250, 400, 320, 320);

            using (var c3 = new SolidBrush(Color.FromArgb(20, 255, 255, 255)))
                g.FillEllipse(c3, 300, -80, 220, 220);

            // Đường chéo mờ (opacity 15/255) chạy từ góc trên-phải xuống dưới-trái
            using (var pen = new Pen(Color.FromArgb(15, 255, 255, 255), 60))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                g.DrawLine(pen, 420, 0, 0, 600);
            }
        }

        /// <summary>
        /// Gắn toàn bộ event handler cho các control trong form.
        /// Tách riêng khỏi constructor để code dễ đọc, dễ bảo trì.
        /// </summary>
        private void SetupEvents()
        {
            // Nút ✕ góc phải – thoát toàn bộ ứng dụng
            btnClose.Click += (s, e) => Application.Exit();

            // Cho phép kéo form bằng cách giữ chuột trên panel trái, panel phải, hoặc label brand
            pnlSide.MouseDown += DragForm;
            pnlMain.MouseDown += DragForm;
            lblBrand.MouseDown += DragForm;

            // Nhấn Enter ở ô username hoặc password đều kích hoạt nút Login
            // (SuppressKeyPress = true để chặn tiếng "beep" mặc định của Windows)
            txtUsername.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnLogin.PerformClick(); } };
            txtPassword.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnLogin.PerformClick(); } };

            // Hiệu ứng gạch dưới đổi màu tím khi focus vào ô input, trở lại xám khi mất focus
            txtUsername.Enter += (s, e) => pnlUserLine.BackColor = Color.FromArgb(150, 0, 252);
            txtUsername.Leave += (s, e) => pnlUserLine.BackColor = Color.FromArgb(220, 222, 230);
            txtPassword.Enter += (s, e) => pnlPassLine.BackColor = Color.FromArgb(150, 0, 252);
            txtPassword.Leave += (s, e) => pnlPassLine.BackColor = Color.FromArgb(220, 222, 230);

            // Nút con mắt: đảo trạng thái ẩn/hiện mật khẩu
            btnTogglePass.Click += (s, e) =>
            {
                _passwordVisible = !_passwordVisible;
                txtPassword.UseSystemPasswordChar = !_passwordVisible;
                btnTogglePass.Text = _passwordVisible ? "🙈" : "👁";
            };

            // Nút đăng nhập – xử lý logic chính
            btnLogin.Click += BtnLogin_Click;

            // Link "Sign up now" – mở form đăng ký, ẩn form Login hiện tại
            linkSignup.LinkClicked += (s, e) =>
            {
                this.Hide();
                new SignupForm().Show();
            };

            //linkForgot.LinkClicked += (s, e) =>
            //    MessageBox.Show("Please contact the administrator to reset your password.",
            //        "Bank Management", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Gán placeholder cho 2 ô nhập liệu
            SetPlaceholder(txtUsername, "Enter your username");
            SetPlaceholder(txtPassword, "Enter your password", isPassword: true);
        }

        /// <summary>
        /// Xử lý kéo form: khi người dùng giữ chuột trái ở bất kỳ control nào
        /// đã gắn DragForm, ta gửi message WM_NCLBUTTONDOWN với tham số HT_CAPTION
        /// để Windows tưởng người dùng đang giữ thanh tiêu đề → form di chuyển theo chuột.
        /// </summary>
        private void DragForm(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }


        /// <summary>
        /// Cài đặt placeholder cho một TextBox cụ thể.
        /// Cơ chế:
        /// - Lưu text gợi ý vào <c>Tag</c> để nhận diện sau này.
        /// - Khi focus vào và text hiện tại chính là placeholder → xóa đi, đổi màu chữ đậm.
        /// - Khi rời focus mà text rỗng → khôi phục placeholder, đổi màu chữ xám nhạt.
        /// </summary>
        /// <param name="txt">TextBox cần gán placeholder.</param>
        /// <param name="placeholder">Nội dung gợi ý.</param>
        /// <param name="isPassword">True nếu là ô mật khẩu (xử lý UseSystemPasswordChar).</param>
        private void SetPlaceholder(TextBox txt, string placeholder, bool isPassword = false)
        {
            txt.Tag = placeholder;
            txt.ForeColor = Color.FromArgb(150, 155, 170);   // xám nhạt
            txt.Text = placeholder;
            if (isPassword) txt.UseSystemPasswordChar = false;   // hiện text placeholder dạng thường

            txt.GotFocus += (s, e) =>
            {
                if (txt.Text == placeholder)
                {
                    txt.Text = "";
                    txt.ForeColor = Color.FromArgb(40, 45, 60);   // đổi sang màu chữ đậm
                    // Nếu là ô mật khẩu và user chưa bật hiện → bật lại chế độ ẩn ký tự
                    if (isPassword && !_passwordVisible) txt.UseSystemPasswordChar = true;
                }
            };

            txt.LostFocus += (s, e) =>
            {
                // Nếu người dùng bỏ trống → khôi phục placeholder
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    if (isPassword) txt.UseSystemPasswordChar = false;
                    txt.Text = placeholder;
                    txt.ForeColor = Color.FromArgb(150, 155, 170);
                }
            };
        }

        /// <summary>
        /// Kiểm tra TextBox hiện tại có đang chứa placeholder hay không.
        /// So sánh text hiện tại với text gợi ý đã lưu trong Tag.
        /// </summary>
        private bool IsPlaceholder(TextBox txt)
            => txt.Text == (txt.Tag as string);

        /// <summary>
        /// Xử lý khi người dùng bấm nút LOGIN.
        /// Trình tự:
        /// 1. Lấy giá trị thực từ 2 TextBox (bỏ qua placeholder).
        /// 2. Nếu để trống → hiện cảnh báo và dừng.
        /// 3. (Tạm) So sánh với tài khoản cứng admin/123456.
        ///    TODO: Thay bằng truy vấn DB / service xác thực thật.
        /// </summary>
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string user = IsPlaceholder(txtUsername) ? "" : txtUsername.Text.Trim();
            string pass = IsPlaceholder(txtPassword) ? "" : txtPassword.Text;

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                ShowError("⚠  Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.");
                return;
            }

            AuthResult result = AuthService.Login(user, pass);
            if (!result.Success)
            {
                ShowError("✖  " + result.Message);
                txtPassword.SelectAll();
                txtPassword.Focus();
                return;
            }
            CurrentSession.SetUser(result.User); // set user vào session
            Console.WriteLine($"[Login] User '{CurrentSession.User.MaNguoiDung}' logged in successfully.");
            HideError();
            OpenMainForm(result.User);   // open ui main,truyền User
        }

        /// <summary>
        /// Mở MainForm sau khi đăng nhập thành công.
        /// 
        /// Luồng:
        /// 1. Ẩn LoginForm (không đóng — để show lại khi Logout).
        /// 2. Mở MainForm dạng ShowDialog (block).
        /// 3. Khi MainForm đóng:
        ///    - DialogResult = Retry (Logout) → hiện lại LoginForm, reset input.
        ///    - Khác → đóng LoginForm, kết thúc app.
        /// </summary>
        private void OpenMainForm(User loggedInUser)
        {
            using (var mainForm = new MainForm(loggedInUser))
            {
                this.Hide();
                DialogResult result = mainForm.ShowDialog();

                if (result == DialogResult.Retry)
                {
                    // Logout -> quay lại Login
                    ResetLoginForm();
                    CurrentSession.Clear();
                    this.Show();
                }
                else
                { 
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Reset input Login về trạng thái ban đầu sau khi Logout.
        /// </summary>
        private void ResetLoginForm()
        {
            HideError();

            txtUsername.Text = txtUsername.Tag as string ?? "Enter your username";
            txtUsername.ForeColor = Color.FromArgb(150, 155, 170);

            txtPassword.UseSystemPasswordChar = false;
            txtPassword.Text = txtPassword.Tag as string ?? "Enter your password";
            txtPassword.ForeColor = Color.FromArgb(150, 155, 170);

            _passwordVisible = false;
            btnTogglePass.Text = "👁";
        }

        /// <summary>Hiện thông báo lỗi màu đỏ ngay dưới khu vực nhập liệu.</summary>
        private void ShowError(string msg)
        {
            lblError.Text = msg;
            lblError.Visible = true;
        }

        /// <summary>Ẩn thông báo lỗi (thường gọi trước khi thực hiện hành động thành công).</summary>
        private void HideError()
        {
            lblError.Visible = false;
            lblError.Text = "";
        }
    }
}