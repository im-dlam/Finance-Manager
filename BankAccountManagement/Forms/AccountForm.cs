using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Form Quản lý thông tin tài khoản ngân hàng và hồ sơ người dùng.
    /// <para>
    /// Phân quyền chức năng theo vai trò:
    /// - Quản trị viên (Admin): Toàn quyền xem, tìm kiếm, thêm mới, sửa đổi và đóng (khóa) tài khoản của toàn bộ khách hàng trên hệ thống.
    /// - Khách hàng (Member): Xem thông tin tài khoản cá nhân, xem thẻ ATM ảo đổi màu theo hạng thành viên (Kim Cương/Vàng/Bạc/Tiêu Chuẩn),
    ///   chỉnh sửa hồ sơ cá nhân (Họ tên, CMND/CCCD, SĐT, Email, Địa chỉ, Ngày sinh, Giới tính) và thực hiện đổi mật khẩu an toàn.
    /// </para>
    /// </summary>
    public partial class AccountForm : Form
    {
        #region 1. CÁC BIẾN TRẠNG THÁI VÀ DỮ LIỆU

        /// <summary>Dịch vụ xử lý nghiệp vụ tài khoản ngân hàng (truy vấn, thêm, cập nhật, đóng tài khoản).</summary>
        private readonly AccountService _AccountService = new AccountService();

        /// <summary>Cờ đánh dấu trạng thái nhập liệu: true nếu đang thêm mới tài khoản, false nếu đang chỉnh sửa tài khoản cũ.</summary>
        private bool _isAddNew = false;

        /// <summary>Mã khách hàng của tài khoản đang được chọn trong chế độ quản trị.</summary>
        private string _currentMaKH = "";

        /// <summary>Số dư hiện tại của tài khoản đang chọn.</summary>
        private decimal _currentSoDuHienTai = 0;

        /// <summary>Số dư ban đầu lúc mở tài khoản đang chọn.</summary>
        private decimal _currentSoDuBanDau = 0;

        /// <summary>Danh sách bộ nhớ đệm lưu toàn bộ tài khoản phục vụ chức năng tìm kiếm nhanh không cần gọi lại DB.</summary>
        private List<TaiKhoan> _allAccounts = new List<TaiKhoan>();

        /// <summary>Cờ bật/tắt ẩn số dư trên thẻ ATM ảo của khách hàng (hiển thị •••••••• VND).</summary>
        private bool _isBalanceHidden = false;

        /// <summary>Con trỏ hàm (Delegate Action) xử lý sự kiện đổi hạng thành viên thời gian thực.</summary>
        private Action<string> _tierChangedHandler;

        /// <summary>Từ điển tra cứu nhanh Họ tên khách hàng theo Mã khách hàng (MaKH -> HoTen).</summary>
        private Dictionary<string, string> _customerNames = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        /// <summary>Cờ chống kích hoạt đệ quy sự kiện khi đang đồng bộ giữa 2 ComboBox khách hàng.</summary>
        private bool _syncingCustomer = false;

        /// <summary>Cờ đánh dấu ComboBox khách hàng đã được nạp dữ liệu xong.</summary>
        private bool _customersBound = false;

        #endregion

        #region 2. KẾT NỐI VÀ TRUY VẤN CƠ SỞ DỮ LIỆU

        /// <summary>
        /// Lấy chuỗi kết nối (Connection String) từ cấu hình hệ thống DatabaseConfigManager.
        /// </summary>
        private string GetConnectionString()
        {
            DatabaseConfig config = DatabaseConfigManager.Load();
            return DatabaseService.BuildConnectionString(config);
        }

        /// <summary>
        /// Thực thi một câu truy vấn SELECT và nạp kết quả trả về vào DataTable.
        /// </summary>
        /// <param name="sql">Câu lệnh SQL SELECT cần thực thi.</param>
        /// <returns>Đối tượng DataTable chứa dữ liệu kết quả.</returns>
        private DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        #endregion

        #region 3. KHỞI TẠO VÀ SỰ KIỆN FORM LOAD

        /// <summary>
        /// Hàm dựng khởi tạo các điều khiển, đăng ký sự kiện click và lắng nghe sự kiện đổi hạng thời gian thực.
        /// </summary>
        public AccountForm()
        {
            InitializeComponent();
            this.Load += AccountForm_Load;

            // BƯỚC 1: Đăng ký các sự kiện thao tác cho Quản trị viên (Admin)
            dgvAccounts.CellClick += dgvAccounts_CellContentClick;
            dgvAccounts.CellFormatting += dgvAccounts_CellFormatting;
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnClear.Click += btnClear_Click;
            btnSearch.Click += btnSearch_Click;
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.KeyDown += txtSearch_KeyDown;
            cboCustomer.SelectedIndexChanged += cboCustomer_SelectedIndexChanged;
            cboCustomerCode.SelectedIndexChanged += cboCustomerCode_SelectedIndexChanged;

            // BƯỚC 2: Đăng ký các sự kiện thao tác cho Khách hàng (Member)
            btnUserRefresh.Click += btnUserRefresh_Click;
            btnUserCopyStk.Click += btnUserCopyStk_Click;
            btnUserToggleBal.Click += btnUserToggleBal_Click;
            btnSaveProfile.Click += btnSaveProfile_Click;
            btnResetProfile.Click += btnResetProfile_Click;

            cboEditGender.Items.Clear();
            cboEditGender.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            cboEditGender.SelectedIndex = 0;

            // BƯỚC 3: Đăng ký lắng nghe sự kiện đổi hạng thành viên toàn cục để cập nhật màu sắc Thẻ ATM ảo
            _tierChangedHandler = tierKey =>
            {
                if (this.IsDisposed || !this.IsHandleCreated) return;
                this.BeginInvoke((Action)(() => ApplyTierThemeToCard(tierKey)));
            };
            CurrentSession.OnTierChanged += _tierChangedHandler;

            this.FormClosed += (s, e) =>
            {
                CurrentSession.OnTierChanged -= _tierChangedHandler;
            };
        }

        /// <summary>
        /// Xử lý tải dữ liệu khi Form được mở lên, tự động rẽ nhánh giao diện Admin hoặc Member.
        /// </summary>
        private void AccountForm_Load(object sender, EventArgs e)
        {
            if (CurrentSession.IsAdmin)
            {
                // CẤU HÌNH GIAO DIỆN CHO ADMIN
                pnlAdminView.Visible = true;
                pnlAdminView.BringToFront();
                pnlUserView.Visible = false;

                lblSearch.Visible = true;
                txtSearch.Visible = true;
                btnSearch.Visible = true;
                btnUserRefresh.Visible = false;

                lblTitle.Text = "QUẢN LÝ TÀI KHOẢN NGÂN HÀNG";
                lblSubtitle.Text = "Theo dõi và quản lý tài khoản ngân hàng của khách hàng trong hệ thống";

                try
                {
                    LoadComboBoxData();
                    LoadData();
                    ApplyRolePermissions();
                    SetControlState(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khởi động giao diện Quản trị: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // CẤU HÌNH GIAO DIỆN CHO MEMBER
                pnlAdminView.Visible = false;
                pnlUserView.Visible = true;
                pnlUserView.BringToFront();

                lblSearch.Visible = false;
                txtSearch.Visible = false;
                btnSearch.Visible = false;
                btnUserRefresh.Visible = true;

                lblTitle.Text = "QUẢN LÝ THÔNG TIN TÀI KHOẢN";
                lblSubtitle.Text = "Xem và chỉnh sửa thông tin tài khoản cá nhân, định danh và đổi mật khẩu";

                ApplyTierThemeToCard(CurrentSession.SelectedUserTier);
                LoadUserProfileData();
            }
        }

        /// <summary>
        /// Cấu hình hiển thị các nút thao tác CRUD theo quyền Admin.
        /// </summary>
        private void ApplyRolePermissions()
        {
            btnAdd.Visible = CurrentSession.IsAdmin;
            btnEdit.Visible = CurrentSession.IsAdmin;
            btnDelete.Visible = CurrentSession.IsAdmin;
        }

        /// <summary>Kiểm tra quyền quản lý tài khoản (chỉ Admin mới có quyền).</summary>
        private bool CanManageAccounts => CurrentSession.IsAdmin;

        #endregion

        #region 4. QUẢN LÝ HỒ SƠ & THẺ ẢO PHÍA THÀNH VIÊN (MEMBER PROFILE & CARD)

        /// <summary>
        /// Tải toàn bộ thông tin cá nhân, số tài khoản, số dư và ngân hàng của người dùng hiện tại lên màn hình.
        /// </summary>
        private void LoadUserProfileData()
        {
            try
            {
                string username = CurrentSession.TenDangNhap ?? "";
                string maKH = CurrentSession.MaKH ?? "";

                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    const string sql = @"
                        SELECT TOP 1
                            nd.TenDangNhap,
                            nd.MatKhau,
                            nd.Quyen,
                            nd.MaKH,
                            nd.HoTen AS UserHoTen,
                            nd.Email AS UserEmail,
                            nd.MaNganHang AS UserMaNH,
                            nd.TrangThai AS UserTrangThai,
                            nd.NgayTao AS UserNgayTao,
                            kh.HoTen AS KhHoTen,
                            kh.CMND_CCCD,
                            kh.NgaySinh,
                            kh.GioiTinh,
                            kh.SoDienThoai,
                            kh.Email AS KhEmail,
                            kh.DiaChi,
                            tk.SoTaiKhoan,
                            tk.SoDuHienTai,
                            tk.MaLoaiTien,
                            tk.NgayMo,
                            tk.TrangThai AS TaiKhoanTrangThai,
                            nh.TenNganHang
                        FROM dbo.NguoiDung nd
                        LEFT JOIN dbo.ThongTinKhachHang kh ON kh.MaKH = nd.MaKH
                        LEFT JOIN dbo.TaiKhoan tk ON tk.MaKH = nd.MaKH
                        LEFT JOIN dbo.NganHang nh ON nh.MaNganHang = tk.MaNganHang OR nh.MaNganHang = nd.MaNganHang
                        WHERE nd.TenDangNhap = @TenDangNhap OR (nd.MaKH IS NOT NULL AND nd.MaKH = @MaKH)";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenDangNhap", username);
                        cmd.Parameters.AddWithValue("@MaKH", maKH);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string uName = reader["TenDangNhap"]?.ToString() ?? username;
                                string soTK = reader["SoTaiKhoan"]?.ToString() ?? "";
                                string hoTen = reader["KhHoTen"]?.ToString();
                                if (string.IsNullOrWhiteSpace(hoTen)) hoTen = reader["UserHoTen"]?.ToString();
                                if (string.IsNullOrWhiteSpace(hoTen)) hoTen = CurrentSession.HoTen ?? "";

                                string cmnd = reader["CMND_CCCD"]?.ToString() ?? "";
                                string sdt = reader["SoDienThoai"]?.ToString() ?? "";
                                string email = reader["KhEmail"]?.ToString();
                                if (string.IsNullOrWhiteSpace(email)) email = reader["UserEmail"]?.ToString();
                                if (string.IsNullOrWhiteSpace(email)) email = CurrentSession.Email ?? "";

                                string gioiTinh = reader["GioiTinh"]?.ToString() ?? "Nam";
                                string diaChi = reader["DiaChi"]?.ToString() ?? "";
                                string tenNH = reader["TenNganHang"]?.ToString() ?? "Vietcombank";
                                string loaiTien = reader["MaLoaiTien"]?.ToString() ?? "VND";
                                decimal soDu = reader["SoDuHienTai"] != DBNull.Value ? Convert.ToDecimal(reader["SoDuHienTai"]) : 0;

                                if (CurrentSession.IsMember)
                                {
                                    UserTierTheme.UpdateSessionTierByBalance(soDu);
                                }
                                string userMaKH = reader["MaKH"]?.ToString() ?? maKH;

                                DateTime ngaySinh = DateTime.Today.AddYears(-20);
                                if (reader["NgaySinh"] != DBNull.Value)
                                {
                                    ngaySinh = Convert.ToDateTime(reader["NgaySinh"]);
                                }

                                DateTime ngayMo = DateTime.Today;
                                if (reader["NgayMo"] != DBNull.Value)
                                {
                                    ngayMo = Convert.ToDateTime(reader["NgayMo"]);
                                }

                                // BƯỚC 1: Điền thông tin vào Form chỉnh sửa
                                txtEditUsername.Text = uName; // CỐ ĐỊNH 🔒
                                txtEditAccountNumber.Text = string.IsNullOrEmpty(soTK) ? "Chưa mở tài khoản" : soTK; // CỐ ĐỊNH 🔒
                                txtEditFullName.Text = hoTen;
                                txtEditIdCard.Text = cmnd;
                                txtEditPhone.Text = sdt;
                                txtEditEmail.Text = email;
                                dtpEditBirthDate.Value = ngaySinh > dtpEditBirthDate.MinDate && ngaySinh < dtpEditBirthDate.MaxDate ? ngaySinh : DateTime.Today.AddYears(-20);

                                int gIdx = cboEditGender.FindStringExact(gioiTinh);
                                cboEditGender.SelectedIndex = gIdx >= 0 ? gIdx : 0;

                                txtEditAddress.Text = diaChi;

                                // Xóa trắng các ô mật khẩu
                                txtEditOldPassword.Clear();
                                txtEditNewPassword.Clear();
                                txtEditConfirmPassword.Clear();
                                lblStatusMessage.Text = "";

                                // BƯỚC 2: Cập nhật Thẻ ATM ảo & Cột tóm tắt hồ sơ bên trái
                                lblUserBankName.Text = tenNH.ToUpper();
                                lblUserStk.Text = FormatAccountNumber(soTK);
                                lblUserBalance.Text = $"{soDu:N0} {loaiTien}";
                                lblUserHolder.Text = hoTen.ToUpper();

                                lblSideFullName.Text = hoTen;
                                lblSideUsername.Text = "@" + uName + "  🔒 (Định danh)";
                                lblSideMaKH.Text = string.IsNullOrEmpty(userMaKH) ? "--" : userMaKH;
                                lblSideBank.Text = tenNH;
                                lblSideOpenDate.Text = ngayMo.ToString("dd/MM/yyyy");

                                // BƯỚC 3: Đồng bộ dữ liệu mới vào CurrentSession
                                if (CurrentSession.User != null)
                                {
                                    CurrentSession.User.HoTen = hoTen;
                                    CurrentSession.User.Email = email;
                                    if (!string.IsNullOrEmpty(userMaKH)) CurrentSession.User.MaKH = userMaKH;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AccountForm] LoadUserProfileData Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi khách hàng bấm "Lưu thông tin":
        /// Kiểm tra tính hợp lệ dữ liệu, xác thực mật khẩu cũ nếu đổi mật khẩu, và cập nhật vào CSDL.
        /// </summary>
        private void btnSaveProfile_Click(object sender, EventArgs e)
        {
            lblStatusMessage.Text = "";

            string hoTen = txtEditFullName.Text.Trim();
            string cmnd = txtEditIdCard.Text.Trim();
            string sdt = txtEditPhone.Text.Trim();
            string email = txtEditEmail.Text.Trim();
            DateTime ngaySinh = dtpEditBirthDate.Value.Date;
            string gioiTinh = cboEditGender.SelectedItem?.ToString() ?? "Nam";
            string diaChi = txtEditAddress.Text.Trim();

            string oldPass = txtEditOldPassword.Text;
            string newPass = txtEditNewPassword.Text;
            string confirmPass = txtEditConfirmPassword.Text;

            // BƯỚC 1: Kiểm tra tính hợp lệ dữ liệu cơ bản
            if (string.IsNullOrWhiteSpace(hoTen))
            {
                MessageBox.Show("Vui lòng nhập Họ và tên!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEditFullName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(cmnd))
            {
                MessageBox.Show("Vui lòng nhập Số CMND / CCCD!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEditIdCard.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(sdt))
            {
                MessageBox.Show("Vui lòng nhập Số điện thoại liên hệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEditPhone.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập Địa chỉ Email!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEditEmail.Focus();
                return;
            }

            // BƯỚC 2: Kiểm tra nếu người dùng muốn đổi mật khẩu đăng nhập
            bool isChangingPassword = !string.IsNullOrEmpty(oldPass) || !string.IsNullOrEmpty(newPass) || !string.IsNullOrEmpty(confirmPass);
            if (isChangingPassword)
            {
                if (string.IsNullOrEmpty(oldPass))
                {
                    MessageBox.Show("Vui lòng nhập Mật khẩu hiện tại để xác thực thay đổi mật khẩu!", "Yêu cầu xác thực",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEditOldPassword.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(newPass))
                {
                    MessageBox.Show("Vui lòng nhập Mật khẩu mới!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEditNewPassword.Focus();
                    return;
                }

                if (newPass.Length < 6)
                {
                    MessageBox.Show("Mật khẩu mới phải có độ dài tối thiểu từ 6 ký tự trở lên!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEditNewPassword.Focus();
                    return;
                }

                if (!string.Equals(newPass, confirmPass))
                {
                    MessageBox.Show("Xác nhận mật khẩu mới không trùng khớp với mật khẩu mới!", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEditConfirmPassword.Focus();
                    return;
                }
            }

            // BƯỚC 3: Mở kết nối và thực thi cập nhật CSDL
            try
            {
                string username = CurrentSession.TenDangNhap ?? "";
                string maKH = CurrentSession.MaKH ?? "";

                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    // Xác thực mật khẩu cũ nếu có đổi mật khẩu
                    if (isChangingPassword)
                    {
                        const string sqlCheckPass = "SELECT MatKhau FROM dbo.NguoiDung WHERE TenDangNhap = @TenDangNhap";
                        using (var cmdCheck = new SqlCommand(sqlCheckPass, conn))
                        {
                            cmdCheck.Parameters.AddWithValue("@TenDangNhap", username);
                            object dbPass = cmdCheck.ExecuteScalar();
                            if (dbPass == null || !string.Equals(dbPass.ToString(), oldPass))
                            {
                                MessageBox.Show("Mật khẩu hiện tại không chính xác! Vui lòng nhập đúng mật khẩu hiện tại.",
                                    "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtEditOldPassword.Focus();
                                return;
                            }
                        }
                    }

                    // Đảm bảo có MaKH liên kết với khách hàng
                    if (string.IsNullOrEmpty(maKH))
                    {
                        const string sqlFindMaKH = "SELECT MaKH FROM dbo.NguoiDung WHERE TenDangNhap = @TenDangNhap";
                        using (var cmdFind = new SqlCommand(sqlFindMaKH, conn))
                        {
                            cmdFind.Parameters.AddWithValue("@TenDangNhap", username);
                            object res = cmdFind.ExecuteScalar();
                            if (res != null && res != DBNull.Value) maKH = res.ToString();
                        }
                    }

                    if (string.IsNullOrEmpty(maKH))
                    {
                        // Tạo mới MaKH tự động nếu tài khoản chưa có hồ sơ
                        const string sqlNextMaKH = "SELECT ISNULL(MAX(TRY_CONVERT(INT, SUBSTRING(MaKH, 3, 10))), 0) + 1 FROM dbo.ThongTinKhachHang";
                        using (var cmdNext = new SqlCommand(sqlNextMaKH, conn))
                        {
                            int nextId = Convert.ToInt32(cmdNext.ExecuteScalar());
                            maKH = "KH" + nextId.ToString("D6");
                        }

                        const string sqlInsertKH = @"
                            INSERT INTO dbo.ThongTinKhachHang (MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, TrangThai, NgayTao)
                            VALUES (@MaKH, @HoTen, @CMND, @NgaySinh, @GioiTinh, @SDT, @Email, @DiaChi, 'Active', GETDATE());
                            UPDATE dbo.NguoiDung SET MaKH = @MaKH WHERE TenDangNhap = @TenDangNhap;";

                        using (var cmdIns = new SqlCommand(sqlInsertKH, conn))
                        {
                            cmdIns.Parameters.AddWithValue("@MaKH", maKH);
                            cmdIns.Parameters.AddWithValue("@HoTen", hoTen);
                            cmdIns.Parameters.AddWithValue("@CMND", cmnd);
                            cmdIns.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                            cmdIns.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                            cmdIns.Parameters.AddWithValue("@SDT", sdt);
                            cmdIns.Parameters.AddWithValue("@Email", email);
                            cmdIns.Parameters.AddWithValue("@DiaChi", diaChi);
                            cmdIns.Parameters.AddWithValue("@TenDangNhap", username);
                            cmdIns.ExecuteNonQuery();
                        }

                        if (CurrentSession.User != null) CurrentSession.User.MaKH = maKH;
                    }
                    else
                    {
                        // Cập nhật thông tin vào bảng ThongTinKhachHang
                        const string sqlUpdateKH = @"
                            UPDATE dbo.ThongTinKhachHang 
                            SET HoTen = @HoTen, 
                                CMND_CCCD = @CMND, 
                                NgaySinh = @NgaySinh, 
                                GioiTinh = @GioiTinh, 
                                SoDienThoai = @SDT, 
                                Email = @Email, 
                                DiaChi = @DiaChi 
                            WHERE MaKH = @MaKH";

                        using (var cmdUpKH = new SqlCommand(sqlUpdateKH, conn))
                        {
                            cmdUpKH.Parameters.AddWithValue("@MaKH", maKH);
                            cmdUpKH.Parameters.AddWithValue("@HoTen", hoTen);
                            cmdUpKH.Parameters.AddWithValue("@CMND", cmnd);
                            cmdUpKH.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                            cmdUpKH.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                            cmdUpKH.Parameters.AddWithValue("@SDT", sdt);
                            cmdUpKH.Parameters.AddWithValue("@Email", email);
                            cmdUpKH.Parameters.AddWithValue("@DiaChi", diaChi);
                            cmdUpKH.ExecuteNonQuery();
                        }
                    }

                    // Cập nhật bảng NguoiDung (Họ tên, Email và Mật khẩu nếu có đổi)
                    string sqlUpdateND = isChangingPassword
                        ? "UPDATE dbo.NguoiDung SET HoTen = @HoTen, Email = @Email, MatKhau = @MatKhau WHERE TenDangNhap = @TenDangNhap"
                        : "UPDATE dbo.NguoiDung SET HoTen = @HoTen, Email = @Email WHERE TenDangNhap = @TenDangNhap";

                    using (var cmdUpND = new SqlCommand(sqlUpdateND, conn))
                    {
                        cmdUpND.Parameters.AddWithValue("@HoTen", hoTen);
                        cmdUpND.Parameters.AddWithValue("@Email", email);
                        cmdUpND.Parameters.AddWithValue("@TenDangNhap", username);
                        if (isChangingPassword)
                        {
                            cmdUpND.Parameters.AddWithValue("@MatKhau", newPass);
                        }
                        cmdUpND.ExecuteNonQuery();
                    }
                }

                // BƯỚC 4: Cập nhật thông tin trong phiên làm việc hiện tại
                if (CurrentSession.User != null)
                {
                    CurrentSession.User.HoTen = hoTen;
                    CurrentSession.User.Email = email;
                    if (!string.IsNullOrEmpty(maKH)) CurrentSession.User.MaKH = maKH;
                }

                // BƯỚC 5: Cập nhật hiển thị giao diện bên trái
                lblUserHolder.Text = hoTen.ToUpper();
                lblSideFullName.Text = hoTen;

                // Xóa trắng các ô mật khẩu sau khi đổi thành công
                txtEditOldPassword.Clear();
                txtEditNewPassword.Clear();
                txtEditConfirmPassword.Clear();

                lblStatusMessage.Text = "✓ Cập nhật thông tin tài khoản thành công!";
                lblStatusMessage.ForeColor = Color.FromArgb(22, 163, 74);

                string successMsg = isChangingPassword
                    ? "Cập nhật thông tin cá nhân và thay đổi mật khẩu thành công!"
                    : "Cập nhật thông tin tài khoản thành công!";

                MessageBox.Show(successMsg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu thông tin: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetProfile_Click(object sender, EventArgs e) => LoadUserProfileData();

        private void btnUserRefresh_Click(object sender, EventArgs e) => LoadUserProfileData();

        /// <summary>
        /// Sao chép số tài khoản vào khay nhớ tạm (Clipboard) của hệ thống.
        /// </summary>
        private void btnUserCopyStk_Click(object sender, EventArgs e)
        {
            string stk = txtEditAccountNumber.Text.Trim();
            if (string.IsNullOrEmpty(stk) || stk.Contains("Chưa")) return;

            try
            {
                Clipboard.SetText(stk);
                btnUserCopyStk.Text = "✓ Đã chép!";
                var timer = new Timer { Interval = 1800 };
                timer.Tick += (s, ev) =>
                {
                    btnUserCopyStk.Text = "📋 Chép STK";
                    timer.Stop();
                    timer.Dispose();
                };
                timer.Start();
            }
            catch { }
        }

        /// <summary>
        /// Bật / tắt ẩn hiện số dư tài khoản trên Thẻ ATM ảo để đảm bảo tính riêng tư.
        /// </summary>
        private void btnUserToggleBal_Click(object sender, EventArgs e)
        {
            _isBalanceHidden = !_isBalanceHidden;
            if (_isBalanceHidden)
            {
                lblUserBalance.Text = "•••••••• VND";
                btnUserToggleBal.Text = "🙈";
            }
            else
            {
                LoadUserProfileData();
                btnUserToggleBal.Text = "👁️";
            }
        }

        /// <summary>
        /// Thay đổi phong cách và màu sắc Thẻ ATM ảo (Virtual Card) theo Hạng thành viên (Kim Cương / Vàng / Bạc / Tiêu Chuẩn).
        /// </summary>
        public void ApplyTierThemeToCard(string tierKey)
        {
            if (string.IsNullOrWhiteSpace(tierKey))
                tierKey = CurrentSession.SelectedUserTier;

            string lower = (tierKey ?? "").ToLower();

            if (lower.Contains("kim cuong") || lower.Contains("kim c") || lower.Contains("diamond") || lower.Contains("den"))
            {
                // 💎 Hạng Kim Cương (Đen chữ Vàng VIP)
                pnlUserCard.FillColor = Color.FromArgb(15, 23, 42);
                pnlUserCard.BorderColor = Color.FromArgb(250, 204, 21);
                pnlUserCard.BorderSize = 1;

                lblUserBankName.ForeColor = Color.FromArgb(250, 204, 21);
                lblUserTier.Text = "💎 VIP DIAMOND";
                lblUserTier.ForeColor = Color.FromArgb(250, 204, 21);
                lblUserChip.ForeColor = Color.FromArgb(250, 204, 21);

                lblUserStk.ForeColor = Color.White;
                btnUserCopyStk.ForeColor = Color.FromArgb(147, 197, 253);

                lblUserBalance.ForeColor = Color.FromArgb(250, 204, 21);
                btnUserToggleBal.ForeColor = Color.FromArgb(250, 204, 21);
                lblUserHolder.ForeColor = Color.White;
            }
            else if (lower.Contains("vang") || lower.Contains("v") || lower.Contains("gold"))
            {
                // 🥇 Hạng Vàng (Hoàng gia VIP)
                pnlUserCard.FillColor = Color.FromArgb(120, 53, 15);
                pnlUserCard.BorderColor = Color.FromArgb(253, 224, 71);
                pnlUserCard.BorderSize = 1;

                lblUserBankName.ForeColor = Color.FromArgb(254, 240, 138);
                lblUserTier.Text = "🥇 GOLD VIP";
                lblUserTier.ForeColor = Color.FromArgb(254, 240, 138);
                lblUserChip.ForeColor = Color.FromArgb(254, 240, 138);

                lblUserStk.ForeColor = Color.White;
                btnUserCopyStk.ForeColor = Color.FromArgb(254, 240, 138);

                lblUserBalance.ForeColor = Color.FromArgb(254, 240, 138);
                btnUserToggleBal.ForeColor = Color.FromArgb(254, 240, 138);
                lblUserHolder.ForeColor = Color.White;
            }
            else if (lower.Contains("bac") || lower.Contains("b") || lower.Contains("silver"))
            {
                // 🥈 Hạng Bạc (Titanium Privilege)
                pnlUserCard.FillColor = Color.FromArgb(51, 65, 85);
                pnlUserCard.BorderColor = Color.FromArgb(203, 213, 225);
                pnlUserCard.BorderSize = 1;

                lblUserBankName.ForeColor = Color.FromArgb(241, 245, 249);
                lblUserTier.Text = "🥈 SILVER VIP";
                lblUserTier.ForeColor = Color.FromArgb(226, 232, 240);
                lblUserChip.ForeColor = Color.FromArgb(226, 232, 240);

                lblUserStk.ForeColor = Color.White;
                btnUserCopyStk.ForeColor = Color.FromArgb(147, 197, 253);

                lblUserBalance.ForeColor = Color.White;
                btnUserToggleBal.ForeColor = Color.White;
                lblUserHolder.ForeColor = Color.White;
            }
            else
            {
                // 🥉 Hạng Tiêu Chuẩn (Navy Classic)
                pnlUserCard.FillColor = Color.FromArgb(30, 58, 138);
                pnlUserCard.BorderColor = Color.FromArgb(96, 165, 250);
                pnlUserCard.BorderSize = 1;

                lblUserBankName.ForeColor = Color.FromArgb(191, 219, 254);
                lblUserTier.Text = "⭐ STANDARD";
                lblUserTier.ForeColor = Color.FromArgb(191, 219, 254);
                lblUserChip.ForeColor = Color.FromArgb(191, 219, 254);

                lblUserStk.ForeColor = Color.White;
                btnUserCopyStk.ForeColor = Color.FromArgb(147, 197, 253);

                lblUserBalance.ForeColor = Color.White;
                btnUserToggleBal.ForeColor = Color.White;
                lblUserHolder.ForeColor = Color.White;
            }
        }

        /// <summary>
        /// Định dạng số tài khoản ngân hàng cách nhau bằng khoảng trắng cho dễ đọc (vd: 1234 5678 9012).
        /// </summary>
        private string FormatAccountNumber(string stk)
        {
            if (string.IsNullOrEmpty(stk)) return "----  ----  ----";
            stk = stk.Trim();
            if (stk.Length >= 8)
            {
                var sb = new StringBuilder();
                for (int i = 0; i < stk.Length; i++)
                {
                    if (i > 0 && i % 4 == 0) sb.Append("  ");
                    sb.Append(stk[i]);
                }
                return sb.ToString();
            }
            return stk;
        }

        #endregion

        #region 5. QUẢN LÝ DỮ LIỆU PHÍA QUẢN TRỊ VIÊN (ADMIN CRUD)

        /// <summary>
        /// Nạp danh sách khách hàng, ngân hàng và loại tiền vào các ComboBox trên giao diện Admin.
        /// </summary>
        private void LoadComboBoxData()
        {
            DataTable dtKH = GetDataTable(
                "SELECT MaKH, HoTen FROM dbo.ThongTinKhachHang WHERE TrangThai = N'Active' ORDER BY HoTen");
            BindCustomers(dtKH);

            DataTable dtNH = GetDataTable(
                "SELECT MaNganHang, TenNganHang FROM dbo.NganHang ORDER BY TenNganHang");
            cboBank.DataSource = dtNH;
            cboBank.DisplayMember = "TenNganHang";
            cboBank.ValueMember = "MaNganHang";

            DataTable dtLT = GetDataTable(@"
                SELECT MaLoaiTien, TenLoaiTien 
                FROM   dbo.LoaiTien 
                ORDER  BY TenLoaiTien");
            cboCurrency.DataSource = dtLT;
            cboCurrency.DisplayMember = "TenLoaiTien";
            cboCurrency.ValueMember = "MaLoaiTien";

            cboStatus.Items.Clear();
            cboStatus.Items.Add("Active");
            cboStatus.Items.Add("Closed");
            cboStatus.SelectedIndex = 0;
        }

        /// <summary>
        /// Ràng buộc nguồn dữ liệu khách hàng vào 2 ComboBox (Tên KH và Mã KH) và đồng bộ chúng với nhau.
        /// </summary>
        private void BindCustomers(DataTable customers)
        {
            _customersBound = false;
            if (customers == null) return;

            _syncingCustomer = true;
            try
            {
                cboCustomer.DataSource = null;
                cboCustomerCode.DataSource = null;

                cboCustomer.DisplayMember = "HoTen";
                cboCustomer.ValueMember = "MaKH";
                cboCustomer.DataSource = customers;

                cboCustomerCode.DisplayMember = "MaKH";
                cboCustomerCode.ValueMember = "MaKH";
                cboCustomerCode.DataSource = customers.Copy();

                _customerNames = BuildLookup(customers, "MaKH", "HoTen");
                _customersBound = true;
            }
            finally
            {
                _syncingCustomer = false;
            }

            SetCustomerSelection(cboCustomer.SelectedValue);
        }

        /// <summary>
        /// Đồng bộ lựa chọn giữa ComboBox Tên KH và ComboBox Mã KH.
        /// </summary>
        private void SetCustomerSelection(object maKH)
        {
            if (_syncingCustomer || !_customersBound) return;
            if (cboCustomer.DataSource == null || cboCustomerCode.DataSource == null) return;

            if (maKH is DataRowView row) maKH = row["MaKH"];
            if (maKH == null || maKH == DBNull.Value) return;

            _syncingCustomer = true;
            try
            {
                cboCustomer.SelectedValue = maKH;
                cboCustomerCode.SelectedValue = maKH;
            }
            finally
            {
                _syncingCustomer = false;
            }
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_syncingCustomer || !_customersBound) return;
            if (cboCustomer.SelectedValue != null)
                SetCustomerSelection(cboCustomer.SelectedValue);
        }

        private void cboCustomerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_syncingCustomer || !_customersBound) return;
            if (cboCustomerCode.SelectedValue != null)
                SetCustomerSelection(cboCustomerCode.SelectedValue);
        }

        /// <summary>
        /// Tải danh sách tất cả tài khoản ngân hàng trong hệ thống lên bảng DataGridView.
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm (nếu có).</param>
        private void LoadData(string keyword = "")
        {
            try
            {
                var all = _AccountService.GetActiveTaiKhoan() ?? new List<TaiKhoan>();
                _allAccounts = all;

                dgvAccounts.AutoGenerateColumns = false;
                if (dgvAccounts.Columns.Count > 0)
                {
                    dgvAccounts.Columns["colAccountNumber"].DataPropertyName = "SoTaiKhoan";
                    ConfigureCustomerColumns();
                    dgvAccounts.Columns["colBank"].DataPropertyName = "MaNganHang";
                    dgvAccounts.Columns["colCurrency"].DataPropertyName = "MaLoaiTien";
                    dgvAccounts.Columns["colBalance"].DataPropertyName = "SoDuHienTai";
                    dgvAccounts.Columns["colOpenDate"].DataPropertyName = "NgayMo";
                    dgvAccounts.Columns["colStatus"].DataPropertyName = "TrangThai";
                }

                ApplyFilter(keyword);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu tài khoản: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Cấu hình các cột Mã khách hàng và Tên khách hàng trên bảng DataGridView.
        /// </summary>
        private void ConfigureCustomerColumns()
        {
            DataGridViewColumn nameColumn = dgvAccounts.Columns["colCustomer"];
            if (nameColumn == null) return;

            DataGridViewColumn codeColumn = dgvAccounts.Columns
                .Cast<DataGridViewColumn>()
                .FirstOrDefault(column => column != nameColumn &&
                    (column.Name == "colCustomerCode" ||
                     new string(Normalize(column.HeaderText)
                         .Where(c => !char.IsWhiteSpace(c)).ToArray()) == "makhachhang"));

            if (codeColumn == null)
            {
                codeColumn = new DataGridViewTextBoxColumn { Name = "colCustomerCode" };
                dgvAccounts.Columns.Add(codeColumn);
            }

            codeColumn.HeaderText = "Mã khách hàng";
            codeColumn.DataPropertyName = "MaKH";
            codeColumn.Visible = true;
            codeColumn.ReadOnly = true;
            codeColumn.MinimumWidth = 110;
            codeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            codeColumn.DisplayIndex = Math.Max(0, nameColumn.DisplayIndex - 1);

            nameColumn.HeaderText = "Khách hàng";
            nameColumn.DataPropertyName = "MaKH";
            nameColumn.Visible = true;
            nameColumn.ReadOnly = true;
            nameColumn.MinimumWidth = 160;
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            nameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        /// <summary>
        /// Định dạng hiển thị tên khách hàng thay cho mã khách hàng trên ô DataGridView.
        /// </summary>
        private void dgvAccounts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
                dgvAccounts.Columns[e.ColumnIndex].Name != "colCustomer") return;

            TaiKhoan account = dgvAccounts.Rows[e.RowIndex].DataBoundItem as TaiKhoan;
            string customerName;
            e.Value = account != null && account.MaKH != null &&
                _customerNames.TryGetValue(account.MaKH, out customerName)
                ? customerName : "Chưa có tên khách hàng";
            e.FormattingApplied = true;
        }

        /// <summary>
        /// Thiết lập trạng thái Enabled/Disabled của các trường nhập liệu tương ứng với chế độ xem hay sửa.
        /// </summary>
        /// <param name="isEditing">True nếu đang thêm mới hoặc sửa; False nếu ở trạng thái xem.</param>
        private void SetControlState(bool isEditing)
        {
            if (!CanManageAccounts)
            {
                txtAccountNumber.Enabled = false;
                cboCustomer.Enabled = false;
                cboCustomerCode.Enabled = false;
                txtBalance.Enabled = false;
                cboBank.Enabled = false;
                cboCurrency.Enabled = false;
                dtpOpenDate.Enabled = false;
                cboStatus.Enabled = false;
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                return;
            }

            txtAccountNumber.Enabled = isEditing && _isAddNew;
            cboCustomer.Enabled = isEditing && _isAddNew && CurrentSession.IsAdmin;
            cboCustomerCode.Enabled = isEditing && _isAddNew && CurrentSession.IsAdmin;
            txtBalance.Enabled = isEditing && _isAddNew;

            cboBank.Enabled = isEditing;
            cboCurrency.Enabled = isEditing;
            dtpOpenDate.Enabled = isEditing;
            cboStatus.Enabled = isEditing;

            if (isEditing && _isAddNew)
            {
                btnAdd.Text = "💾 Lưu tài khoản";
                btnAdd.Enabled = true;
                btnEdit.Text = "❌ Hủy";
                btnEdit.Enabled = true;
                btnDelete.Enabled = false;
            }
            else if (isEditing && !_isAddNew)
            {
                btnAdd.Text = "💾 Lưu thay đổi";
                btnAdd.Enabled = true;
                btnEdit.Text = "❌ Hủy chỉnh sửa";
                btnEdit.Enabled = true;
                btnDelete.Enabled = false;
            }
            else
            {
                btnAdd.Text = "+ Thêm tài khoản mới";
                btnAdd.Enabled = true;
                btnEdit.Text = "✏️ Chỉnh sửa";
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        /// <summary>
        /// Xóa trắng dữ liệu trên các ô nhập liệu bên phía Admin.
        /// </summary>
        private void ClearInputs()
        {
            txtAccountNumber.Clear();
            txtBalance.Clear();

            if (cboCustomer.Items.Count > 0)
            {
                cboCustomer.SelectedIndex = 0;
                cboCustomerCode.SelectedIndex = 0;
            }

            if (cboBank.Items.Count > 0) cboBank.SelectedIndex = 0;
            if (cboCurrency.Items.Count > 0) cboCurrency.SelectedIndex = 0;
            if (cboStatus.Items.Count > 0) cboStatus.SelectedIndex = 0;
            dtpOpenDate.Value = DateTime.Now;

            _currentMaKH = "";
            _currentSoDuHienTai = 0;
            _currentSoDuBanDau = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!CanManageAccounts) return;
            if (btnAdd.Text.Contains("Thêm"))
            {
                _isAddNew = true;
                ClearInputs();
                SetControlState(true);
                txtAccountNumber.Focus();
            }
            else
            {
                ThucHienLuu();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!CanManageAccounts) return;
            if (btnEdit.Text.Contains("Hủy"))
            {
                _isAddNew = false;
                ClearInputs();
                LoadData(txtSearch.Text);
                SetControlState(false);
                return;
            }

            if (string.IsNullOrEmpty(txtAccountNumber.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _isAddNew = false;
            SetControlState(true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!CanManageAccounts) return;
            if (string.IsNullOrEmpty(txtAccountNumber.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần đóng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(
                    $"Xác nhận ĐÓNG tài khoản: {txtAccountNumber.Text}?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                try
                {
                    if (_AccountService.CloseTaiKhoan(txtAccountNumber.Text))
                    {
                        MessageBox.Show("Đóng tài khoản thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(txtSearch.Text);
                        ClearInputs();
                        SetControlState(false);
                    }
                    else
                    {
                        MessageBox.Show("Không thể đóng: tài khoản không tồn tại hoặc không thuộc quyền quản lý của bạn.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvAccounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvAccounts.CurrentRow == null) return;

            DataGridViewRow row = dgvAccounts.Rows[e.RowIndex];
            TaiKhoan account = row.DataBoundItem as TaiKhoan;
            if (account == null) return;

            _currentMaKH = account.MaKH ?? "";
            _currentSoDuHienTai = Convert.ToDecimal(row.Cells["colBalance"].Value ?? 0);
            _currentSoDuBanDau = _currentSoDuHienTai;

            txtAccountNumber.Text = row.Cells["colAccountNumber"].Value?.ToString();
            txtBalance.Text = _currentSoDuHienTai.ToString();

            SetCustomerSelection(account.MaKH);

            cboBank.SelectedValue = row.Cells["colBank"].Value;

            var currencyValue = row.Cells["colCurrency"].Value;
            if (currencyValue != null && currencyValue != DBNull.Value)
                cboCurrency.SelectedValue = currencyValue;
            else
                cboCurrency.SelectedIndex = -1;

            if (row.Cells["colOpenDate"].Value != null)
                dtpOpenDate.Value = Convert.ToDateTime(row.Cells["colOpenDate"].Value);

            cboStatus.Text = row.Cells["colStatus"].Value?.ToString();

            _isAddNew = false;
            SetControlState(false);
        }

        /// <summary>
        /// Thực thi lưu thông tin tài khoản (thêm mới hoặc cập nhật) thông qua AccountService.
        /// </summary>
        private void ThucHienLuu()
        {
            if (!CanManageAccounts) return;
            if (string.IsNullOrWhiteSpace(txtAccountNumber.Text))
            {
                MessageBox.Show("Vui lòng nhập Số tài khoản!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountNumber.Focus();
                return;
            }

            TaiKhoan tk;

            if (_isAddNew)
            {
                if (!decimal.TryParse(txtBalance.Text, out decimal soDuMoi) || soDuMoi < 0)
                {
                    MessageBox.Show("Số dư ban đầu không hợp lệ!", "Lỗi nhập liệu",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBalance.Focus();
                    return;
                }

                string maKH = cboCustomerCode.SelectedValue?.ToString()
                              ?? cboCustomer.SelectedValue?.ToString();

                tk = new TaiKhoan
                {
                    SoTaiKhoan = txtAccountNumber.Text.Trim(),
                    MaKH = maKH,
                    MaNganHang = cboBank.SelectedValue?.ToString(),
                    MaLoaiTien = cboCurrency.SelectedValue?.ToString(),
                    SoDuBanDau = soDuMoi,
                    SoDuHienTai = soDuMoi,
                    NgayMo = dtpOpenDate.Value,
                    TrangThai = cboStatus.Text
                };
            }
            else
            {
                tk = new TaiKhoan
                {
                    SoTaiKhoan = txtAccountNumber.Text.Trim(),
                    MaKH = _currentMaKH,
                    MaNganHang = cboBank.SelectedValue?.ToString(),
                    MaLoaiTien = cboCurrency.SelectedValue?.ToString(),
                    SoDuBanDau = _currentSoDuBanDau,
                    SoDuHienTai = _currentSoDuHienTai,
                    NgayMo = dtpOpenDate.Value,
                    TrangThai = cboStatus.Text
                };
            }

            try
            {
                bool ok = _isAddNew
                    ? _AccountService.AddTaiKhoan(tk)
                    : _AccountService.UpdateTaiKhoan(tk);

                if (ok)
                {
                    string msg = _isAddNew ? "Thêm tài khoản thành công!" : "Cập nhật thành công!";
                    MessageBox.Show(msg, "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(txtSearch.Text);
                    ClearInputs();
                    _isAddNew = false;
                    SetControlState(false);
                }
                else
                {
                    MessageBox.Show("Lưu thất bại! Tài khoản không tồn tại hoặc không thuộc quyền quản lý của bạn.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 6. TÌM KIẾM, LỌC VÀ TIỆN ÍCH (SEARCH & UTILITIES)

        private void btnSearch_Click(object sender, EventArgs e) => ApplyFilter(txtSearch.Text);

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.TextChanged -= txtSearch_TextChanged;
            txtSearch.Clear();
            txtSearch.TextChanged += txtSearch_TextChanged;

            _isAddNew = false;
            ClearInputs();
            LoadData();
            SetControlState(false);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilter(txtSearch.Text);

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ApplyFilter(txtSearch.Text);
            }
        }

        /// <summary>
        /// Lọc danh sách tài khoản theo từ khóa tìm kiếm trên nhiều trường thông tin.
        /// </summary>
        private void ApplyFilter(string keyword)
        {
            string key = Normalize(keyword);
            List<TaiKhoan> ketQua;

            if (key.Length == 0)
            {
                ketQua = _allAccounts;
            }
            else
            {
                var tenKH = BuildLookup(cboCustomer.DataSource as DataTable, "MaKH", "HoTen");
                var tenNH = BuildLookup(cboBank.DataSource as DataTable, "MaNganHang", "TenNganHang");
                var tenLT = BuildLookup(cboCurrency.DataSource as DataTable, "MaLoaiTien", "TenLoaiTien");

                ketQua = _allAccounts.Where(tk =>
                       Match(tk.SoTaiKhoan, key)
                    || Match(tk.MaKH, key)
                    || Match(GetValue(tenKH, tk.MaKH), key)
                    || Match(tk.MaNganHang, key)
                    || Match(GetValue(tenNH, tk.MaNganHang), key)
                    || Match(tk.MaLoaiTien, key)
                    || Match(GetValue(tenLT, tk.MaLoaiTien), key)
                    || Match(tk.TrangThai, key)
                ).ToList();
            }

            dgvAccounts.DataSource = null;
            dgvAccounts.DataSource = ketQua;

            lblListCount.Text = (key.Length > 0 && ketQua.Count == 0)
                ? "Không tìm thấy tài khoản phù hợp"
                : $"{ketQua.Count} tài khoản";
        }

        /// <summary>
        /// Xây dựng từ điển tra cứu nhanh (Dictionary) từ một DataTable.
        /// </summary>
        private static Dictionary<string, string> BuildLookup(DataTable dt, string keyCol, string valueCol)
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (dt == null) return dict;

            foreach (DataRow r in dt.Rows)
            {
                string k = r[keyCol]?.ToString();
                if (!string.IsNullOrEmpty(k) && !dict.ContainsKey(k))
                    dict[k] = r[valueCol]?.ToString() ?? "";
            }
            return dict;
        }

        private static string GetValue(Dictionary<string, string> dict, string key)
            => key != null && dict.TryGetValue(key, out string v) ? v : "";

        private static bool Match(string source, string normalizedKey)
            => !string.IsNullOrEmpty(source) && Normalize(source).Contains(normalizedKey);

        /// <summary>
        /// Chuẩn hóa chuỗi tiếng Việt: loại bỏ dấu cách thừa, chuyển về chữ thường và khử dấu tiếng Việt để tìm kiếm không dấu.
        /// </summary>
        private static string Normalize(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";

            string s = text.Trim().ToLowerInvariant().Replace('đ', 'd');
            var sb = new StringBuilder();
            foreach (char c in s.Normalize(NormalizationForm.FormD))
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        private void lblSubtitle_Click(object sender, EventArgs e)
        {
        }

        #endregion
    }
}
