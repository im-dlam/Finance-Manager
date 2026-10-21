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
    /// - Panel phải: form chọn ngân hàng, nhập username/password với các control bo góc.
    /// </summary>
    public partial class LoginForm : Form
    {

        private bool _passwordVisible = false;

        public LoginForm(string prefilledUsername = null, string bankCode = null)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            ApplyRoundedCorners();
            SetupEvents();

            if (!string.IsNullOrWhiteSpace(prefilledUsername))
            {
                txtUsername.Text = prefilledUsername;
                txtUsername.ForeColor = Color.FromArgb(40, 45, 60);
                this.ActiveControl = txtPassword;
            }
        }

        private void ApplyRoundedCorners()
        {
            SetRoundedRegion(pnlUser, 10);
            SetRoundedRegion(pnlPass, 10);
            SetRoundedRegion(btnLogin, 10);
            SetRoundedRegion(this, 16);
        }

        private static void SetRoundedRegion(Control ctrl, int radius)
        {
            if (ctrl.Width <= 0 || ctrl.Height <= 0) return;

            using (var path = RoundedPath(ctrl.ClientRectangle, radius))
                ctrl.Region = new Region(path);
        }

        private static GraphicsPath RoundedPath(Rectangle rect, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void PnlSide_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var brush = new LinearGradientBrush(
                pnlSide.ClientRectangle,
                Color.FromArgb(120, 0, 220),
                Color.FromArgb(198, 5, 255),
                55f))
            {
                g.FillRectangle(brush, pnlSide.ClientRectangle);
            }

            using (var c1 = new SolidBrush(Color.FromArgb(30, 255, 255, 255)))
                g.FillEllipse(c1, -80, -80, 300, 300);

            using (var c2 = new SolidBrush(Color.FromArgb(25, 255, 255, 255)))
                g.FillEllipse(c2, 250, 400, 320, 320);

            using (var c3 = new SolidBrush(Color.FromArgb(20, 255, 255, 255)))
                g.FillEllipse(c3, 300, -80, 220, 220);

            using (var pen = new Pen(Color.FromArgb(15, 255, 255, 255), 60))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                g.DrawLine(pen, 420, 0, 0, 600);
            }
        }


        private void SetupEvents()
        {
            btnClose.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; };

            pnlSide.MouseDown += DragMouseDown;
            pnlSide.MouseMove += DragMouseMove;
            pnlSide.MouseUp += DragMouseUp;

            pnlMain.MouseDown += DragMouseDown;
            pnlMain.MouseMove += DragMouseMove;
            pnlMain.MouseUp += DragMouseUp;

            lblBrand.MouseDown += DragMouseDown;
            lblBrand.MouseMove += DragMouseMove;
            lblBrand.MouseUp += DragMouseUp;

            txtUsername.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnLogin.PerformClick(); } };
            txtPassword.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; btnLogin.PerformClick(); } };
            txtUsername.Enter += (s, e) => pnlUserLine.BackColor = Color.FromArgb(150, 0, 252);
            txtUsername.Leave += (s, e) => pnlUserLine.BackColor = Color.FromArgb(220, 222, 230);
            txtPassword.Enter += (s, e) => pnlPassLine.BackColor = Color.FromArgb(150, 0, 252);
            txtPassword.Leave += (s, e) => pnlPassLine.BackColor = Color.FromArgb(220, 222, 230);

            btnTogglePass.Click += (s, e) =>
            {
                _passwordVisible = !_passwordVisible;
                txtPassword.UseSystemPasswordChar = !_passwordVisible;
                btnTogglePass.Text = _passwordVisible ? "🙈" : "👁";
            };

            btnLogin.Click += BtnLogin_Click;

            linkSignup.LinkClicked += (s, e) =>
            {
                this.Hide();
                using (var signup = new SignupForm())
                {
                    if (signup.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(signup.RegisteredUsername))
                    {
                        txtUsername.Text = signup.RegisteredUsername;
                        txtUsername.ForeColor = Color.FromArgb(40, 45, 60);
                        this.ActiveControl = txtPassword;
                    }
                    GC.KeepAlive(signup);
                }
                Application.DoEvents();
                this.Show();
            };

            SetPlaceholder(txtUsername, "Enter your username");
            SetPlaceholder(txtPassword, "Enter your password", isPassword: true);
        }

                private Point _dragStartCursor;
        private Point _dragStartForm;
        private bool _isDragging = false;

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

        private bool IsPlaceholder(TextBox txt)
            => txt.Text == (txt.Tag as string);

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
            CurrentSession.SetUser(result.User);
            Console.WriteLine($"[Login] User '{CurrentSession.User.MaNguoiDung}' logged in successfully.");
            HideError();

            this.DialogResult = DialogResult.OK;
        }

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
    }
}
