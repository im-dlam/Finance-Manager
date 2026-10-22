using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BankAccountManagement.Models;
using BankAccountManagement.Services;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Form giao diện nhập và hoàn tất thông tin định danh khách hàng (KYC Profile).
    /// <para>
    /// Được kích hoạt khi người dùng lần đầu tiên đăng nhập vào hệ thống mà chưa có thông tin nhân thân
    /// (CCCD/CMND, Ngày sinh, Địa chỉ, Số điện thoại) trong bảng ThongTinKhachHang.
    /// </para>
    /// </summary>
    public partial class CustomerInfoForm : Form
    {
        #region SỰ KIỆN TÙY BIẾN (CUSTOM EVENTS)

        /// <summary>
        /// Sự kiện bắn ra khi người dùng lưu thông tin hồ sơ khách hàng thành công.
        /// </summary>
        public event EventHandler SaveSuccess;

        #endregion

        #region KHỞI TẠO VÀ CẤU HÌNH BAN ĐẦU (INITIALIZATION & SETUP)

        /// <summary>
        /// Khởi tạo Form hoàn tất hồ sơ khách hàng, cài đặt hiệu ứng tương tác focus và nạp dữ liệu từ phiên hiện tại.
        /// </summary>
        public CustomerInfoForm()
        {
            InitializeComponent();
            SetupFocusEffects();
            SetupEvents();
            PrefillFromSession();
        }

        #endregion

        #region TÙY BIẾN ĐỒ HỌA GDI+ & HIỆU ỨNG FOCUS (GDI+ DRAWING & FOCUS EFFECTS)

        /// <summary>
        /// Sự kiện vẽ khung viền bo góc và đường phân cách icon cho từng ô nhập liệu (Input Box).
        /// </summary>
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
            {
                g.DrawPath(pen, path);
            }

            // Vẽ đường kẻ dọc mảnh phân cách giữa biểu tượng (icon) và ô nhập văn bản
            using (var sepPen = new Pen(Color.FromArgb(226, 232, 240), 1))
            {
                g.DrawLine(sepPen, 42, 8, 42, box.Height - 8);
            }
        }

        /// <summary>
        /// Tạo đường viền hình chữ nhật bo góc mềm mại bằng <see cref="GraphicsPath"/>.
        /// </summary>
        /// <param name="rect">Khuôn chữ nhật bao quanh.</param>
        /// <param name="radius">Bán kính bo tròn góc.</param>
        /// <returns>Đối tượng <see cref="GraphicsPath"/> bo tròn 4 góc.</returns>
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

        /// <summary>
        /// Đăng ký hiệu ứng đổi màu viền phát sáng khi người dùng trỏ chuột hoặc gõ vào ô nhập liệu (Focus Effects).
        /// </summary>
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

        #endregion

        #region ĐĂNG KÝ SỰ KIỆN & ĐIỀU HƯỚNG (EVENT HANDLERS & NAVIGATION)

        /// <summary>
        /// Đăng ký các sự kiện bàn phím Enter và thao tác nút bấm Lưu / Hủy.
        /// </summary>
        private void SetupEvents()
        {
            txtFullName.KeyDown += OnEnterSave;
            txtCccd.KeyDown += OnEnterSave;
            txtPhone.KeyDown += OnEnterSave;
            txtEmail.KeyDown += OnEnterSave;

            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
        }

        /// <summary>
        /// Tự động thực hiện lưu khi người dùng nhấn Enter trên các ô nhập văn bản.
        /// </summary>
        private void OnEnterSave(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSave.PerformClick();
            }
        }

        /// <summary>
        /// Xử lý khi người dùng chọn nút Hủy bỏ. Cảnh báo hạn chế tính năng nếu chưa hoàn thiện hồ sơ.
        /// </summary>
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

        #endregion

        #region TỰ ĐỘNG ĐIỀN THÔNG TIN TỪ PHIÊN ĐĂNG NHẬP (PREFILL FROM SESSION)

        /// <summary>
        /// Nạp sẵn Họ tên và Email đã biết từ tài khoản người dùng đăng nhập hiện tại.
        /// </summary>
        private void PrefillFromSession()
        {
            if (!CurrentSession.IsLoggedIn) return;

            txtFullName.Text = CurrentSession.User?.HoTen ?? "";
            txtEmail.Text = CurrentSession.User?.Email ?? "";

            // Nếu người dùng đã có MaKH, nạp lại thông tin cũ để người dùng chỉ cần kiểm tra lại
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

            // Đặt con trỏ chuột vào ô trống đầu tiên cần bổ sung
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

        #endregion

        #region XÁC THỰC & LƯU HỒ SƠ BẤT ĐỒNG BỘ (VALIDATION & ASYNC SAVE)

        /// <summary>
        /// Xác thực dữ liệu nhập liệu theo biểu thức chính quy (Regex) và thực thi lưu hồ sơ bằng phương thức bất đồng bộ Task.Run.
        /// </summary>
        private async void BtnSave_Click(object sender, EventArgs e)
        {
            string hoTen = txtFullName.Text.Trim();
            string cccd = txtCccd.Text.Trim();
            DateTime ngaySinh = dtpDob.Value;
            string gioiTinh = cboGender.SelectedItem?.ToString() ?? string.Empty;
            string sdt = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();
            string diaChi = txtAddress.Text.Trim();

            // 1. Kiểm tra họ và tên
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                ShowError("Vui lòng nhập họ và tên.");
                txtFullName.Focus();
                return;
            }

            // 2. Kiểm tra độ dài và định dạng số CCCD/CMND (9 đến 12 chữ số)
            if (!Regex.IsMatch(cccd, @"^\d{9,12}$"))
            {
                ShowError("CMND/CCCD phải có từ 9 đến 12 chữ số.");
                txtCccd.Focus();
                return;
            }

            // 3. Kiểm tra độ tuổi tối thiểu theo quy định mở tài khoản ngân hàng (ít nhất 16 tuổi)
            if (ngaySinh > DateTime.Now.AddYears(-16))
            {
                ShowError("Bạn phải đủ 16 tuổi theo quy định mở tài khoản.");
                dtpDob.Focus();
                return;
            }

            // 4. Kiểm tra giới tính
            if (string.IsNullOrWhiteSpace(gioiTinh))
            {
                ShowError("Vui lòng chọn giới tính.");
                cboGender.Focus();
                return;
            }

            // 5. Kiểm tra định dạng số điện thoại Việt Nam (bắt đầu bằng 0, gồm 10 hoặc 11 chữ số)
            if (!Regex.IsMatch(sdt, @"^0\d{9,10}$"))
            {
                ShowError("Số điện thoại không hợp lệ (cần bắt đầu bằng số 0 và có 10-11 số).");
                txtPhone.Focus();
                return;
            }

            // 6. Kiểm tra cấu trúc địa chỉ Email hợp lệ
            if (!IsValidEmail(email))
            {
                ShowError("Địa chỉ Email không đúng định dạng hợp lệ.");
                txtEmail.Focus();
                return;
            }

            // 7. Kiểm tra địa chỉ cư trú
            if (string.IsNullOrWhiteSpace(diaChi))
            {
                ShowError("Vui lòng nhập địa chỉ cư trú.");
                txtAddress.Focus();
                return;
            }

            // Khóa nút Lưu và chuyển con trỏ chuột sang trạng thái chờ (Wait Cursor) để tránh submit nhiều lần
            btnSave.Enabled = false;
            UseWaitCursor = true;

            try
            {
                // Thực thi lưu hồ sơ dưới luồng nền (Background Task) để giữ giao diện luôn mượt mà
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
                    "Thông báo hoàn tất",
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

        /// <summary>
        /// Kiểm tra tính hợp lệ của địa chỉ Email bằng Regular Expression.
        /// </summary>
        private static bool IsValidEmail(string email)
            => Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);

        private void ShowError(string msg) { lblError.Text = msg; lblError.Visible = true; }
        private void HideError() { lblError.Text = ""; lblError.Visible = false; }

        #endregion
    }
}
