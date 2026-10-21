using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    public partial class CustomerInfoForm : Form
    {
        public event EventHandler SaveSuccess;

        public CustomerInfoForm()
        {
            InitializeComponent();
            SetupFocusEffects();
            SetupEvents();
            PrefillFromSession();
        }

        // ============================================================
        //  HEADER GRADIENT
        // ============================================================

        // ============================================================
        //  INPUT BOX PAINT — vẽ border 1px xung quanh ô input
        // ============================================================
        private void InputBox_Paint(object sender, PaintEventArgs e)
        {
            var box = sender as Panel;
            if (box == null) return;

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            bool isFocused = box.Tag as string == "FOCUSED";
            var borderColor = isFocused
                ? Color.FromArgb(99, 102, 241)
                : Color.FromArgb(226, 232, 240);
            float borderWidth = isFocused ? 2f : 1f;

            var rect = new Rectangle(0, 0, box.Width - 1, box.Height - 1);
            using (var pen = new Pen(borderColor, borderWidth))
            using (var path = RoundedPath(rect, 8))
                g.DrawPath(pen, path);

            // Vẽ đường kẻ dọc phân cách icon và input
            using (var sepPen = new Pen(Color.FromArgb(226, 232, 240), 1))
                g.DrawLine(sepPen, 42, 8, 42, box.Height - 8);
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

        // ============================================================
        //  FOCUS EFFECTS
        // ============================================================
        private void SetupFocusEffects()
        {
            HookFocus(txtFullName, pnlFullNameBox);
            HookFocus(txtCccd, pnlCccdBox);
            HookFocus(txtPhone, pnlPhoneBox);
            HookFocus(txtEmail, pnlEmailBox);
            HookFocus(txtAddress, pnlAddressBox);
            HookFocusCombo(cboGender, pnlGenderBox);
            HookFocusDate(dtpDob, pnlDobBox);
        }

        private void HookFocus(TextBox txt, Panel box)
        {
            txt.GotFocus += (s, e) => { box.Tag = "FOCUSED"; box.Invalidate(); };
            txt.LostFocus += (s, e) => { box.Tag = null; box.Invalidate(); };
        }

        private void HookFocusCombo(ComboBox cbo, Panel box)
        {
            cbo.GotFocus += (s, e) => { box.Tag = "FOCUSED"; box.Invalidate(); };
            cbo.LostFocus += (s, e) => { box.Tag = null; box.Invalidate(); };
        }

        private void HookFocusDate(DateTimePicker dtp, Panel box)
        {
            dtp.Enter += (s, e) => { box.Tag = "FOCUSED"; box.Invalidate(); };
            dtp.Leave += (s, e) => { box.Tag = null; box.Invalidate(); };
        }

        // ============================================================
        //  EVENTS
        // ============================================================
        private void SetupEvents()
        {
            txtFullName.KeyDown += OnEnterSave;
            txtCccd.KeyDown += OnEnterSave;
            txtPhone.KeyDown += OnEnterSave;
            txtEmail.KeyDown += OnEnterSave;

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        private void OnEnterSave(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSave.PerformClick();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Hủy nhập thông tin? Bạn sẽ không thể sử dụng đầy đủ các tính năng cho đến khi hoàn tất hồ sơ.",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            this.DialogResult = DialogResult.Cancel;
            if (!this.TopLevel)
            {
                var main = this.TopLevelControl as MainForm ?? this.ParentForm as MainForm;
                if (main != null)
                {
                    main.LoadMultiForm(new DashboardUserForm());
                    return;
                }
            }
            this.Close();
        }

        // ============================================================
        //  PREFILL
        // ============================================================
        private void PrefillFromSession()
        {
            if (!CurrentSession.IsLoggedIn) return;

            txtFullName.Text = CurrentSession.User?.HoTen ?? "";
            txtEmail.Text = CurrentSession.User?.Email ?? "";

            // Nếu user đã có MaKH hoặc thông tin cũ, prefill các trường chưa bị dummy
            if (!string.IsNullOrWhiteSpace(CurrentSession.User?.MaKH))
            {
                try
                {
                    var profile = CustomerService.GetCustomerProfile(CurrentSession.User.MaKH);
                    if (profile != null)
                    {
                        if (!string.IsNullOrWhiteSpace(profile.HoTen))
                            txtFullName.Text = profile.HoTen;

                        if (!string.IsNullOrWhiteSpace(profile.Email))
                            txtEmail.Text = profile.Email;

                        if (!string.IsNullOrWhiteSpace(profile.CMND_CCCD) && !profile.CMND_CCCD.StartsWith("001200"))
                            txtCccd.Text = profile.CMND_CCCD;

                        if (!string.IsNullOrWhiteSpace(profile.SoDienThoai) && profile.SoDienThoai != "0901234567")
                            txtPhone.Text = profile.SoDienThoai;

                        if (!string.IsNullOrWhiteSpace(profile.DiaChi) && profile.DiaChi != "Hà Nội")
                            txtAddress.Text = profile.DiaChi;

                        if (profile.NgaySinh.HasValue && profile.NgaySinh.Value.Year > 1900)
                            dtpDob.Value = profile.NgaySinh.Value;

                        if (!string.IsNullOrWhiteSpace(profile.GioiTinh))
                        {
                            int idx = cboGender.FindStringExact(profile.GioiTinh);
                            if (idx >= 0) cboGender.SelectedIndex = idx;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("[CustomerInfoForm] Prefill error: " + ex.Message);
                }
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
                txtFullName.Focus();
            else if (string.IsNullOrWhiteSpace(txtEmail.Text))
                txtEmail.Focus();
            else if (string.IsNullOrWhiteSpace(txtCccd.Text))
                txtCccd.Focus();
            else if (string.IsNullOrWhiteSpace(txtPhone.Text))
                txtPhone.Focus();
            else
                txtAddress.Focus();
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            string hoTen = txtFullName.Text.Trim();
            string cccd = txtCccd.Text.Trim();
            DateTime ngaySinh = dtpDob.Value;
            string gioiTinh = cboGender.SelectedItem?.ToString() ?? string.Empty;
            string sdt = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();
            string diaChi = txtAddress.Text.Trim();

            // Validate dữ liệu
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                ShowError("Vui lòng nhập họ và tên.");
                txtFullName.Focus();
                return;
            }

            if (!Regex.IsMatch(cccd, @"^\d{9,12}$"))
            {
                ShowError("CMND/CCCD phải có từ 9 đến 12 chữ số.");
                txtCccd.Focus();
                return;
            }

            if (ngaySinh > DateTime.Now.AddYears(-16))
            {
                ShowError("Bạn phải đủ 16 tuổi.");
                dtpDob.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(gioiTinh))
            {
                ShowError("Vui lòng chọn giới tính.");
                cboGender.Focus();
                return;
            }

            if (!Regex.IsMatch(sdt, @"^0\d{9,10}$"))
            {
                ShowError("Số điện thoại không hợp lệ.");
                txtPhone.Focus();
                return;
            }

            if (!IsValidEmail(email))
            {
                ShowError("Email không hợp lệ.");
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(diaChi))
            {
                ShowError("Vui lòng nhập địa chỉ.");
                txtAddress.Focus();
                return;
            }

            // Lưu dữ liệu mà không làm đơ giao diện
            btnSave.Enabled = false;
            UseWaitCursor = true;

            try
            {
                AuthResult result = await Task.Run(() =>
                    CustomerService.SaveCustomerInfo(
                        hoTen,
                        cccd,
                        ngaySinh,
                        gioiTinh,
                        sdt,
                        email,
                        diaChi
                    )
                );

                if (!result.Success)
                {
                    ShowError(result.Message);
                    return;
                }

                HideError();

                MessageBox.Show(
                    result.Message,
                    "Bank Management",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                SaveSuccess?.Invoke(this, EventArgs.Empty);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ShowError($"Không thể lưu thông tin khách hàng: {ex.Message}");
            }
            finally
            {
                if (!IsDisposed && !Disposing)
                {
                    btnSave.Enabled = true;
                    UseWaitCursor = false;
                }
            }
        }
        private static bool IsValidEmail(string email)
            => Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);

        private void ShowError(string msg) { lblError.Text = msg; lblError.Visible = true; }
        private void HideError() { lblError.Text = ""; lblError.Visible = false; }
    }
}