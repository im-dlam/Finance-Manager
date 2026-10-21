using BankAccountManagement.Models;
using BankAccountManagement.Services;
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
    /// - Thu thập: họ tên, username, ngân hàng mở tài khoản, email, mật khẩu, xác nhận mật khẩu.
    /// - Kiểm tra dữ liệu đầu vào (validate) trước khi cho phép đăng ký.
    /// - Cho phép ẩn/hiện mật khẩu, kéo form, quay lại form Login.
    /// </summary>
    public partial class SignupForm : Form
    {

        private bool _passVisible = false;
        private bool _confirmVisible = false;
        public string RegisteredUsername { get; private set; }

        public SignupForm(string preselectedBank = null)
        {
            InitializeComponent();
            ApplyRoundedCorners();
            LoadBanks(preselectedBank);
            SetupEvents();
            SetupPlaceholders();
        }

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

        private static void SetRoundedRegion(Control ctrl, int radius)
        {
            if (ctrl.Width <= 0 || ctrl.Height <= 0) return;

            using (var path = RoundedPath(ctrl.ClientRectangle, radius))
                ctrl.Region = new Region(path);
        }

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

        private void PnlSide_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (var brush = new LinearGradientBrush(
                pnlSide.ClientRectangle,
                Color.FromArgb(150, 0, 252),
                Color.FromArgb(230, 0, 180),
                LinearGradientMode.ForwardDiagonal))
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

        private void LoadBanks(string preselectedBank = null)
        {
            try
            {
                var banks = NganHangService.GetPublicBanks();
                cboBank.DisplayMember = "TenNganHang";
                cboBank.ValueMember = "MaNganHang";
                cboBank.DataSource = banks;

                if (!string.IsNullOrWhiteSpace(preselectedBank))
                {
                    cboBank.SelectedValue = preselectedBank;
                }
                else if (cboBank.Items.Count > 0)
                {
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
                System.Diagnostics.Debug.WriteLine("LoadBanks error: " + ex.Message);
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

            HookFocusLine(txtFullName, pnlNameLine);
            HookFocusLine(txtUsername, pnlUserLine);
            HookFocusLine(txtEmail, pnlEmailLine);
            HookFocusLine(txtPassword, pnlPassLine);
            HookFocusLine(txtConfirm, pnlConfirmLine);

            cboBank.Enter += (s, e) => pnlBankLine.BackColor = Color.FromArgb(150, 0, 252);
            cboBank.Leave += (s, e) => pnlBankLine.BackColor = Color.FromArgb(220, 222, 230);

            txtFullName.KeyDown += OnEnterSignup;
            txtUsername.KeyDown += OnEnterSignup;
            txtEmail.KeyDown += OnEnterSignup;
            txtPassword.KeyDown += OnEnterSignup;
            txtConfirm.KeyDown += OnEnterSignup;

            btnTogglePass.Click += (s, e) =>
            {
                _passVisible = !_passVisible;
                txtPassword.UseSystemPasswordChar = !_passVisible;
                btnTogglePass.Text = _passVisible ? "🙈" : "👁";
            };

            btnToggleConfirm.Click += (s, e) =>
            {
                _confirmVisible = !_confirmVisible;
                txtConfirm.UseSystemPasswordChar = !_confirmVisible;
                btnToggleConfirm.Text = _confirmVisible ? "🙈" : "👁";
            };

            btnSignup.Click += BtnSignup_Click;

            linkLogin.LinkClicked += (s, e) => { this.DialogResult = DialogResult.Cancel; };
        }

        private void OnEnterSignup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSignup.PerformClick();
            }
        }

        private void HookFocusLine(TextBox txt, Panel line)
        {
            txt.Enter += (s, e) => line.BackColor = Color.FromArgb(150, 0, 252);
            txt.Leave += (s, e) => line.BackColor = Color.FromArgb(220, 222, 230);
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

        private void BtnSignup_Click(object sender, EventArgs e)
        {
            string fullName = GetValue(txtFullName);
            string username = GetValue(txtUsername);
            string email = GetValue(txtEmail);
            string password = IsPlaceholder(txtPassword) ? "" : txtPassword.Text;
            string confirm = IsPlaceholder(txtConfirm) ? "" : txtConfirm.Text;
            string bankCode = cboBank.SelectedValue?.ToString() ?? "NH01";

            if (string.IsNullOrWhiteSpace(fullName))
            { ShowError("⚠  Vui lòng nhập họ tên."); txtFullName.Focus(); return; }

            if (string.IsNullOrWhiteSpace(username) || username.Length < 4)
            { ShowError("⚠  Tên đăng nhập phải có ít nhất 4 ký tự."); txtUsername.Focus(); return; }

            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            { ShowError("⚠  Email không hợp lệ."); txtEmail.Focus(); return; }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            { ShowError("⚠  Mật khẩu phải có ít nhất 6 ký tự."); txtPassword.Focus(); return; }

            if (password != confirm)
            { ShowError("✖  Mật khẩu xác nhận không khớp."); txtConfirm.SelectAll(); txtConfirm.Focus(); return; }

            if (!chkTerms.Checked)
            { ShowError("⚠  Bạn phải đồng ý với Điều khoản dịch vụ."); return; }

            AuthResult result = AuthService.Register(fullName, username, email, password, bankCode);

            if (!result.Success)
            {
                ShowError("✖  " + result.Message);
                return;
            }

            HideError();
            MessageBox.Show(result.Message, "Bank Management",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            RegisteredUsername = username;

            this.DialogResult = DialogResult.OK;
        }

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
    }
}
