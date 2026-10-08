using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Form đăng ký tài khoản mới cho ứng dụng Bank Management.
    /// Kế thừa <see cref="Form"/> và dùng chung style với <see cref="LoginForm"/>:
    /// panel trái gradient tím-hồng, panel phải chứa form nhập liệu bo góc.
    /// 
    /// Chức năng chính:
    /// - Thu thập: họ tên, username, email, mật khẩu, xác nhận mật khẩu.
    /// - Kiểm tra dữ liệu đầu vào (validate) trước khi cho phép đăng ký.
    /// - Cho phép ẩn/hiện mật khẩu, kéo form, quay lại form Login.
    /// </summary>
    public partial class SignupForm : Form
    {

        /// <summary>
        /// WIN32 API – dùng để kéo form không viền (borderless form)
        /// Vì FormBorderStyle = None nên mất thanh title bar mặc định,
        /// phải tự xử lý việc kéo thả bằng cách gửi message WM_NCLBUTTONDOWN
        /// tới Windows để nó hiểu "đang giữ chuột ở vùng caption".
        /// </summary>

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

        /// <summary>Trạng thái hiển thị mật khẩu ở ô Password (true = đang hiện dạng text).</summary>
        private bool _passVisible = false;

        /// <summary>Trạng thái hiển thị mật khẩu ở ô Confirm Password.</summary>
        private bool _confirmVisible = false;

        /// <summary>
        /// Khởi tạo form đăng ký.
        /// Trình tự: build UI → bo góc các control → gắn sự kiện → gán placeholder.
        /// </summary>
        public SignupForm()
        {
            InitializeComponent();   // Sinh tự động từ Designer – tạo control, layout
            ApplyRoundedCorners();   // Bo góc các panel input, button, form
            SetupEvents();           // Đăng ký toàn bộ event handler
            SetupPlaceholders();     // Gán text gợi ý mờ cho các TextBox
        }

        /// <summary>
        /// Bo góc cho các control chính trong form (input panel + button + form).
        /// Gọi 1 lần trong constructor vì kích thước control đã cố định.
        /// </summary>
        private void ApplyRoundedCorners()
        {
            SetRoundedRegion(pnlName, 10);
            SetRoundedRegion(pnlUser, 10);
            SetRoundedRegion(pnlEmail, 10);
            SetRoundedRegion(pnlPass, 10);
            SetRoundedRegion(pnlConfirm, 10);
            SetRoundedRegion(btnSignup, 10);
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
                g.FillEllipse(c2, 250, 560, 320, 320);

            using (var c3 = new SolidBrush(Color.FromArgb(20, 255, 255, 255)))
                g.FillEllipse(c3, 300, -80, 220, 220);

            // Đường chéo mờ (opacity 15/255) chạy từ góc trên-phải xuống dưới-trái
            using (var pen = new Pen(Color.FromArgb(15, 255, 255, 255), 60))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                g.DrawLine(pen, 420, 0, 0, 800);
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

            // Hiệu ứng gạch dưới đổi màu tím khi focus vào từng ô input
            HookFocusLine(txtFullName, pnlNameLine);
            HookFocusLine(txtUsername, pnlUserLine);
            HookFocusLine(txtEmail, pnlEmailLine);
            HookFocusLine(txtPassword, pnlPassLine);
            HookFocusLine(txtConfirm, pnlConfirmLine);

            // Nhấn Enter ở bất kỳ ô nào cũng kích hoạt nút Sign Up
            txtFullName.KeyDown += OnEnterSignup;
            txtUsername.KeyDown += OnEnterSignup;
            txtEmail.KeyDown += OnEnterSignup;
            txtPassword.KeyDown += OnEnterSignup;
            txtConfirm.KeyDown += OnEnterSignup;

            // Nút con mắt ở ô Password: đảo trạng thái ẩn/hiện
            btnTogglePass.Click += (s, e) =>
            {
                _passVisible = !_passVisible;
                txtPassword.UseSystemPasswordChar = !_passVisible;
                btnTogglePass.Text = _passVisible ? "🙈" : "👁";
            };

            // Tương tự cho ô Confirm Password (dùng cờ riêng)
            btnToggleConfirm.Click += (s, e) =>
            {
                _confirmVisible = !_confirmVisible;
                txtConfirm.UseSystemPasswordChar = !_confirmVisible;
                btnToggleConfirm.Text = _confirmVisible ? "🙈" : "👁";
            };

            // Nút Sign Up – xử lý đăng ký
            btnSignup.Click += BtnSignup_Click;

            // Link "Sign in" – quay về form Login
            linkLogin.LinkClicked += (s, e) =>
            {
                this.Hide();
                new LoginForm().Show();
            };
        }

        /// <summary>
        /// Bắt sự kiện Enter trên các TextBox để thay vì xuống dòng (không có tác dụng),
        /// thì kích hoạt nút Sign Up cho tiện.
        /// </summary>
        private void OnEnterSignup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;   // chặn tiếng "beep" mặc định của Windows
                btnSignup.PerformClick();
            }
        }

        /// <summary>
        /// Gắn hiệu ứng đổi màu gạch dưới (panel line) khi TextBox được focus / mất focus.
        /// Khi focus: gạch dưới chuyển màu tím. Khi mất focus: trở về xám nhạt.
        /// </summary>
        /// <param name="txt">TextBox cần theo dõi.</param>
        /// <param name="line">Panel gạch dưới tương ứng.</param>
        private void HookFocusLine(TextBox txt, Panel line)
        {
            txt.Enter += (s, e) => line.BackColor = Color.FromArgb(150, 0, 252);
            txt.Leave += (s, e) => line.BackColor = Color.FromArgb(220, 222, 230);
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

        // ==========================================================
        //  PLACEHOLDER (TEXT GỢI Ý MỜ)
        //  WinForms TextBox không có sẵn placeholder như HTML,
        //  nên phải tự giả lập: gán text mờ, xóa khi focus, khôi phục khi mất focus.
        // ==========================================================

        /// <summary>
        /// Gán placeholder cho từng TextBox trong form.
        /// Text placeholder được lưu vào Tag để phân biệt với text thật người dùng nhập.
        /// </summary>
        private void SetupPlaceholders()
        {
            SetPlaceholder(txtFullName, "Phat Tai Phat Loc");
            SetPlaceholder(txtUsername, "Nhập tên đăng nhập");
            SetPlaceholder(txtEmail, "team@36.com");
            SetPlaceholder(txtPassword, "ít nhất 6 ký tự", true);
            SetPlaceholder(txtConfirm, "Nhập lại mật khẩu", true);
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
        /// <param name="isPassword">True nếu đây là ô mật khẩu (xử lý UseSystemPasswordChar).</param>
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

                    // Nếu là ô mật khẩu, bật lại chế độ ẩn ký tự (trừ khi user đang bật hiện)
                    if (isPassword)
                    {
                        bool visible = (txt == txtPassword) ? _passVisible : _confirmVisible;
                        txt.UseSystemPasswordChar = !visible;
                    }
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
        private bool IsPlaceholder(TextBox txt) => txt.Text == (txt.Tag as string);

        /// <summary>
        /// Lấy giá trị thực của TextBox (đã trim).
        /// Nếu đang là placeholder → trả về chuỗi rỗng để tránh validate nhầm.
        /// </summary>
        private string GetValue(TextBox txt) => IsPlaceholder(txt) ? "" : txt.Text.Trim();

        // ==========================================================
        //  XỬ LÝ ĐĂNG KÝ (SIGNUP HANDLER)
        // ==========================================================

        /// <summary>
        /// Xử lý khi người dùng bấm nút SIGN UP.
        /// Trình tự:
        /// 1. Lấy giá trị từ các TextBox (bỏ qua placeholder).
        /// 2. Validate lần lượt: họ tên, username, email, password, confirm, điều khoản.
        /// 3. Nếu tất cả hợp lệ → (TODO) lưu vào DB, thông báo thành công, quay về Login.
        /// </summary>
        private void BtnSignup_Click(object sender, EventArgs e)
        {
            // Lấy giá trị người dùng nhập (rỗng nếu đang là placeholder)
            string fullName = GetValue(txtFullName);
            string username = GetValue(txtUsername);
            string email = GetValue(txtEmail);
            string password = IsPlaceholder(txtPassword) ? "" : txtPassword.Text;
            string confirm = IsPlaceholder(txtConfirm) ? "" : txtConfirm.Text;

            // ---- Validate từng bước, dừng ngay khi gặp lỗi và focus vào ô sai ----

            if (string.IsNullOrWhiteSpace(fullName))
            { ShowError("⚠  Please enter your full name."); txtFullName.Focus(); return; }

            if (string.IsNullOrWhiteSpace(username))
            { ShowError("⚠  Please choose a username."); txtUsername.Focus(); return; }

            if (username.Length < 4)
            { ShowError("⚠  Username must be at least 4 characters."); txtUsername.Focus(); return; }

            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            { ShowError("⚠  Please enter a valid email address."); txtEmail.Focus(); return; }

            if (string.IsNullOrWhiteSpace(password))
            { ShowError("⚠  Please enter a password."); txtPassword.Focus(); return; }

            if (password.Length < 6)
            { ShowError("⚠  Password must be at least 6 characters."); txtPassword.Focus(); return; }

            if (password != confirm)
            { ShowError("✖  Passwords do not match."); txtConfirm.SelectAll(); txtConfirm.Focus(); return; }

            if (!chkTerms.Checked)
            { ShowError("⚠  You must agree to the Terms of Service."); return; }

            // ---- Vượt qua hết validate ----
            HideError();

            // TODO: Thay MessageBox dưới đây bằng logic lưu tài khoản thật.
            // Ví dụ: AccountService.Register(fullName, username, email, password);
            MessageBox.Show(
                $"Account created successfully! 🎉\n\nWelcome, {fullName}!\nYou can now sign in.",
                "Bank Management",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Đóng form đăng ký và quay về form Login
            this.Hide();
            new LoginForm().Show();
        }

        /// <summary>
        /// Kiểm tra chuỗi đầu vào có phải địa chỉ email hợp lệ hay không.
        /// Dùng Regex đơn giản: có ký tự trước @, tên miền, dấu chấm và đuôi miền.
        /// (Không theo chuẩn RFC đầy đủ, nhưng đủ dùng cho validate UI.)
        /// </summary>
        private static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }

        /// <summary>Hiện thông báo lỗi màu đỏ ngay dưới khu vực nhập liệu.</summary>
        private void ShowError(string msg)
        {
            lblError.Text = msg;
            lblError.Visible = true;
        }

        /// <summary>Ẩn thông báo lỗi (thường gọi sau khi validate thành công).</summary>
        private void HideError()
        {
            lblError.Visible = false;
            lblError.Text = "";
        }
    }
}