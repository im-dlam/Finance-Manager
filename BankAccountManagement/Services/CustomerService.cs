using System;
using System.Data;
using System.Data.SqlClient;
using BankAccountManagement.Models;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Dịch vụ xử lý toàn bộ nghiệp vụ dữ liệu cho chức năng Quản lý Người dùng &amp; Khách hàng (Customer &amp; User Service).
    /// <para>
    /// Kiến trúc bảo mật &amp; phân quyền hai lớp:
    /// - Quản trị viên (Admin): Quản lý tài khoản đăng nhập trên bảng <c>dbo.NguoiDung</c> (phân quyền, kích hoạt/khóa tài khoản).
    /// - Khách hàng / Thành viên (Member): Quản lý hồ sơ định danh nhân thân trên bảng <c>dbo.ThongTinKhachHang</c> (CCCD/CMND, Ngày sinh, Địa chỉ, SĐT).
    /// </para>
    /// </summary>
    public class CustomerService
    {
        #region CẤU HÌNH & KẾT NỐI CƠ SỞ DỮ LIỆU

        /// <summary>
        /// Đọc cấu hình từ tệp tin cấu hình hệ thống (database.ini) và tạo chuỗi kết nối SQL Server.
        /// </summary>
        /// <returns>Chuỗi kết nối hợp lệ tới cơ sở dữ liệu SQL Server.</returns>
        private string GetConnectionString()
        {
            DatabaseConfig config = DatabaseConfigManager.Load();
            return DatabaseService.BuildConnectionString(config);
        }

        #endregion

        #region QUẢN LÝ TÀI KHOẢN HỆ THỐNG - DÀNH CHO ADMIN

        /// <summary>
        /// Cập nhật trạng thái hoạt động của tài khoản người dùng ("Active" hoặc "Locked").
        /// Sử dụng Parameterized Query (@NewStatus, @Ma) để chống triệt để tấn công SQL Injection.
        /// </summary>
        /// <param name="maNguoiDung">Mã định danh người dùng trong hệ thống (khóa chính bảng NguoiDung).</param>
        /// <param name="newStatus">Trạng thái mới cần cập nhật ("Active" hoặc "Locked").</param>
        /// <returns>True nếu cập nhật thành công ít nhất một bản ghi; ngược lại trả về False.</returns>
        public bool SetUserStatus(string maNguoiDung, string newStatus)
        {
            // BƯỚC 1: Mở kết nối tới CSDL
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();

                // BƯỚC 2: Chuẩn bị câu lệnh UPDATE có tham số
                string updateSql = "UPDATE dbo.NguoiDung SET TrangThai = @NewStatus WHERE MaNguoiDung = @Ma";
                using (SqlCommand cmdUpdate = new SqlCommand(updateSql, conn))
                {
                    cmdUpdate.Parameters.AddWithValue("@NewStatus", newStatus);
                    cmdUpdate.Parameters.AddWithValue("@Ma", maNguoiDung);

                    // BƯỚC 3: Thực thi câu lệnh và kiểm tra số dòng bị ảnh hưởng
                    return cmdUpdate.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// Đảo ngược trạng thái hoạt động của tài khoản: nếu đang Active thì khóa (Locked), nếu đang Locked thì mở lại (Active).
        /// </summary>
        /// <param name="maNguoiDung">Mã người dùng cần đảo trạng thái.</param>
        /// <returns>True nếu thao tác cập nhật thành công; ngược lại là False.</returns>
        public bool ToggleUserStatus(string maNguoiDung)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();

                // BƯỚC 1: Truy vấn trạng thái hiện tại của tài khoản bằng ExecuteScalar
                string getStatusSql = "SELECT TrangThai FROM dbo.NguoiDung WHERE MaNguoiDung = @Ma";
                string currentStatus = "Active";

                using (SqlCommand cmdGet = new SqlCommand(getStatusSql, conn))
                {
                    cmdGet.Parameters.AddWithValue("@Ma", maNguoiDung);
                    object result = cmdGet.ExecuteScalar();
                    if (result != null)
                    {
                        currentStatus = result.ToString();
                    }
                }

                // BƯỚC 2: Tính toán trạng thái đảo ngược
                string newStatus = (currentStatus == "Active") ? "Locked" : "Active";

                // BƯỚC 3: Gọi hàm SetUserStatus để cập nhật trạng thái mới
                return SetUserStatus(maNguoiDung, newStatus);
            }
        }

        /// <summary>
        /// Thêm mới tài khoản người dùng vào bảng dbo.NguoiDung (chức năng quản trị hệ thống).
        /// Mật khẩu mặc định khởi tạo ban đầu là '123456'.
        /// </summary>
        /// <param name="tenDangNhap">Tên đăng nhập duy nhất của người dùng.</param>
        /// <param name="hoTen">Họ và tên hiển thị.</param>
        /// <param name="quyen">Vai trò người dùng ('Admin' hoặc 'Member').</param>
        /// <param name="email">Hộp thư điện tử liên lạc.</param>
        /// <returns>True nếu thêm thành công; ngược lại trả về False.</returns>
        public bool AddAdminUser(string tenDangNhap, string hoTen, string quyen, string email)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();

                // BƯỚC 1: Câu lệnh SQL INSERT với ngày tạo mặc định lấy GETDATE() của máy chủ
                string sql = @"INSERT INTO dbo.NguoiDung (TenDangNhap, MatKhau, HoTen, Quyen, Email, TrangThai, NgayTao) 
                               VALUES (@TenDangNhap, '123456', @HoTen, @Quyen, @Email, 'Active', GETDATE())";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // BƯỚC 2: Khởi tạo tham số với đúng kiểu dữ liệu cột trong CSDL
                    cmd.Parameters.Add("@TenDangNhap", SqlDbType.VarChar, 50).Value = tenDangNhap;
                    cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 100).Value = (object)hoTen ?? DBNull.Value;
                    cmd.Parameters.Add("@Quyen", SqlDbType.VarChar, 20).Value = (object)quyen ?? "Member";
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = (object)email ?? DBNull.Value;

                    // BƯỚC 3: Thực thi lệnh INSERT
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// Cập nhật thông tin định danh và phân quyền của tài khoản người dùng trong bảng NguoiDung.
        /// </summary>
        /// <param name="maND">Mã người dùng cần chỉnh sửa.</param>
        /// <param name="tenDangNhap">Tên đăng nhập mới.</param>
        /// <param name="hoTen">Họ tên mới.</param>
        /// <param name="quyen">Quyền hạn mới ('Admin' hoặc 'Member').</param>
        /// <param name="email">Địa chỉ email mới.</param>
        /// <returns>True nếu cập nhật thành công; ngược lại là False.</returns>
        public bool UpdateAdminUser(string maND, string tenDangNhap, string hoTen, string quyen, string email)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();

                string sql = @"UPDATE dbo.NguoiDung 
                               SET TenDangNhap = @TenDangNhap, HoTen = @HoTen, Quyen = @Quyen, Email = @Email 
                               WHERE MaNguoiDung = @MaND";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@MaND", SqlDbType.Int).Value = int.Parse(maND);
                    cmd.Parameters.Add("@TenDangNhap", SqlDbType.VarChar, 50).Value = tenDangNhap;
                    cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 100).Value = (object)hoTen ?? DBNull.Value;
                    cmd.Parameters.Add("@Quyen", SqlDbType.VarChar, 20).Value = (object)quyen ?? "Member";
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = (object)email ?? DBNull.Value;

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// Xóa tài khoản người dùng khỏi hệ thống theo mã người dùng.
        /// </summary>
        /// <param name="maND">Mã người dùng cần xóa.</param>
        /// <returns>True nếu xóa thành công; ngược lại là False.</returns>
        public bool DeleteAdminUser(string maND)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                string sql = "DELETE FROM dbo.NguoiDung WHERE MaNguoiDung = @MaND";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaND", maND);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        #endregion

        #region QUẢN LÝ HỒ SƠ KHÁCH HÀNG - DÀNH CHO NHÂN VIÊN & MEMBER

        /// <summary>
        /// Thêm mới hồ sơ khách hàng đầy đủ vào bảng ThongTinKhachHang.
        /// </summary>
        /// <param name="maKH">Mã khách hàng dạng chuỗi (ví dụ: KH000001).</param>
        /// <param name="hoTen">Họ và tên khách hàng.</param>
        /// <param name="cmnd">Số CMND hoặc CCCD định danh cá nhân.</param>
        /// <param name="ngaySinh">Ngày tháng năm sinh.</param>
        /// <param name="gioiTinh">Giới tính (Nam / Nữ / Khác).</param>
        /// <param name="sdt">Số điện thoại di động.</param>
        /// <param name="email">Địa chỉ email liên hệ.</param>
        /// <param name="diaChi">Địa chỉ cư trú thường trú hoặc tạm trú.</param>
        /// <returns>True nếu lưu bản ghi thành công; ngược lại là False.</returns>
        public bool AddCustomer(string maKH, string hoTen, string cmnd, DateTime ngaySinh, string gioiTinh, string sdt, string email, string diaChi)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();

                string sql = @"INSERT INTO dbo.ThongTinKhachHang (MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, TrangThai, NgayTao) 
                               VALUES (@MaKH, @HoTen, @CMND, @NgaySinh, @GioiTinh, @SDT, @Email, @DiaChi, 'Active', GETDATE())";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", maKH);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@CMND", cmnd);
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh.Date);
                    cmd.Parameters.AddWithValue("@GioiTinh", (object)gioiTinh ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SDT", (object)sdt ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiaChi", (object)diaChi ?? DBNull.Value);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// Cập nhật thông tin hồ sơ khách hàng hiện có trong bảng ThongTinKhachHang.
        /// </summary>
        /// <param name="maKH">Mã khách hàng cần cập nhật.</param>
        /// <param name="hoTen">Họ tên mới.</param>
        /// <param name="cmnd">Số CMND/CCCD mới.</param>
        /// <param name="ngaySinh">Ngày sinh mới.</param>
        /// <param name="gioiTinh">Giới tính mới.</param>
        /// <param name="sdt">Số điện thoại mới.</param>
        /// <param name="email">Email mới.</param>
        /// <param name="diaChi">Địa chỉ mới.</param>
        /// <returns>True nếu cập nhật thành công; ngược lại là False.</returns>
        public bool UpdateCustomer(string maKH, string hoTen, string cmnd, DateTime ngaySinh, string gioiTinh, string sdt, string email, string diaChi)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();

                string sql = @"UPDATE dbo.ThongTinKhachHang 
                               SET HoTen = @HoTen, CMND_CCCD = @CMND, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, 
                                   SoDienThoai = @SDT, Email = @Email, DiaChi = @DiaChi 
                               WHERE MaKH = @MaKH";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", maKH);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@CMND", cmnd);
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh.Date);
                    cmd.Parameters.AddWithValue("@GioiTinh", (object)gioiTinh ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SDT", (object)sdt ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DiaChi", (object)diaChi ?? DBNull.Value);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        /// <summary>
        /// Xóa thông tin khách hàng khỏi bảng ThongTinKhachHang.
        /// Lưu ý: Nếu khách hàng đã mở tài khoản ngân hàng hoặc có giao dịch, SQL Server sẽ ném lỗi ràng buộc khóa ngoại (SqlException mã 547).
        /// </summary>
        /// <param name="maKH">Mã khách hàng cần xóa.</param>
        /// <returns>True nếu xóa thành công; ngược lại là False.</returns>
        public bool DeleteCustomer(string maKH)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                string sql = "DELETE FROM dbo.ThongTinKhachHang WHERE MaKH = @MaKH";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", maKH);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        #endregion

        #region TRUY VẤN TÌM KIẾM & BÁO CÁO THỐNG KÊ

        /// <summary>
        /// Tải toàn bộ danh sách dữ liệu tùy thuộc vào vai trò của tài khoản đang đăng nhập:
        /// - Nếu là Admin: Tải danh sách người dùng hệ thống từ bảng <c>dbo.NguoiDung</c>.
        /// - Nếu là Member: Tải danh sách hồ sơ từ bảng <c>dbo.ThongTinKhachHang</c>.
        /// </summary>
        /// <param name="isAdmin">Cờ báo quyền hạn Admin.</param>
        /// <returns>Đối tượng <see cref="DataTable"/> chứa dữ liệu truy vấn từ CSDL.</returns>
        public DataTable GetAllData(bool isAdmin)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = isAdmin
                    ? "SELECT MaNguoiDung, TenDangNhap, HoTen, Quyen, Email, TrangThai, NgayTao FROM dbo.NguoiDung ORDER BY MaNguoiDung ASC"
                    : "SELECT MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, TrangThai FROM dbo.ThongTinKhachHang ORDER BY MaKH ASC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    // Dùng SqlDataAdapter để tự động điền toàn bộ kết quả vào DataTable
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Tìm kiếm dữ liệu theo từ khóa trên nhiều cột thông tin khác nhau.
        /// Sử dụng Parameterized Query với tham số @Keyword dạng %keyword% để tìm kiếm an toàn trước SQL Injection.
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm do người dùng nhập từ ô Search.</param>
        /// <param name="isAdmin">Cờ phân quyền quản trị.</param>
        /// <returns>Đối tượng <see cref="DataTable"/> chứa các bản ghi phù hợp với từ khóa.</returns>
        public DataTable SearchData(string keyword, bool isAdmin)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = isAdmin
                    ? @"SELECT MaNguoiDung, TenDangNhap, HoTen, Quyen, Email, TrangThai, NgayTao 
                        FROM dbo.NguoiDung 
                        WHERE (TenDangNhap LIKE @Keyword OR HoTen LIKE @Keyword OR Email LIKE @Keyword 
                               OR CAST(MaNguoiDung AS NVARCHAR) LIKE @Keyword OR Quyen LIKE @Keyword OR TrangThai LIKE @Keyword)
                        ORDER BY MaNguoiDung ASC"
                    : @"SELECT MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, TrangThai 
                        FROM dbo.ThongTinKhachHang 
                        WHERE (MaKH LIKE @Keyword OR HoTen LIKE @Keyword OR SoDienThoai LIKE @Keyword OR CMND_CCCD LIKE @Keyword)
                        ORDER BY MaKH ASC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Lấy 4 chỉ số thống kê quan trọng để hiển thị lên 4 thẻ Stats Cards trên đầu Form:
        /// - TongKhachHang: Tổng số tài khoản/khách hàng được quản lý.
        /// - DangHoatDong: Số lượng tài khoản đang ở trạng thái 'Active'.
        /// - MoiTrongThang: Số lượng khách hàng/tài khoản mới đăng ký trong tháng hiện tại.
        /// - TaiKhoanDaMo: Tổng số tài khoản ngân hàng (bảng TaiKhoan) đã được phát hành.
        /// </summary>
        /// <param name="isAdmin">Cờ báo quyền hạn Admin.</param>
        /// <returns><see cref="DataTable"/> chứa 1 hàng với 4 cột số liệu thống kê.</returns>
        public DataTable GetCustomerStatistics(bool isAdmin)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = isAdmin ? @"
                    SELECT 
                        (SELECT COUNT(*) FROM dbo.NguoiDung) AS TongKhachHang,
                        (SELECT COUNT(*) FROM dbo.NguoiDung WHERE TrangThai = 'Active') AS DangHoatDong,
                        (SELECT COUNT(*) FROM dbo.NguoiDung WHERE MONTH(NgayTao) = MONTH(GETDATE()) AND YEAR(NgayTao) = YEAR(GETDATE())) AS MoiTrongThang,
                        (SELECT COUNT(*) FROM TaiKhoan) AS TaiKhoanDaMo
                " : @"
                    SELECT 
                        (SELECT COUNT(*) FROM dbo.ThongTinKhachHang) AS TongKhachHang,
                        (SELECT COUNT(*) FROM dbo.ThongTinKhachHang WHERE TrangThai = 'Active') AS DangHoatDong,
                        (SELECT COUNT(*) FROM dbo.ThongTinKhachHang WHERE MONTH(NgayTao) = MONTH(GETDATE()) AND YEAR(NgayTao) = YEAR(GETDATE())) AS MoiTrongThang,
                        (SELECT COUNT(*) FROM TaiKhoan) AS TaiKhoanDaMo
                ";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        #endregion

        #region HỒ SƠ KHÁCH HÀNG & GIAO TÁC TRANSACTION (ACID)

        /// <summary>
        /// Truy vấn hồ sơ chi tiết của khách hàng theo mã <paramref name="maKH"/> và ánh xạ sang đối tượng <see cref="CustomerProfile"/>.
        /// </summary>
        /// <param name="maKH">Mã khách hàng cần lấy thông tin (ví dụ: "KH000001").</param>
        /// <returns>Đối tượng <see cref="CustomerProfile"/> chứa thông tin; trả về null nếu không tìm thấy hoặc mã trống.</returns>
        public static CustomerProfile GetCustomerProfile(string maKH)
        {
            if (string.IsNullOrWhiteSpace(maKH)) return null;

            DatabaseConfig config = DatabaseConfigManager.Load();
            using (var conn = DatabaseService.CreateConnection(config))
            {
                conn.Open();
                const string sql = @"
                    SELECT TOP 1 MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, TrangThai
                    FROM dbo.ThongTinKhachHang
                    WHERE MaKH = @MaKH";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", maKH);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read()) return null;

                        return new CustomerProfile
                        {
                            MaKH = reader.GetString(reader.GetOrdinal("MaKH")),
                            HoTen = reader.IsDBNull(reader.GetOrdinal("HoTen")) ? null : reader.GetString(reader.GetOrdinal("HoTen")),
                            CMND_CCCD = reader.IsDBNull(reader.GetOrdinal("CMND_CCCD")) ? null : reader.GetString(reader.GetOrdinal("CMND_CCCD")),
                            NgaySinh = reader.IsDBNull(reader.GetOrdinal("NgaySinh")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("NgaySinh")),
                            GioiTinh = reader.IsDBNull(reader.GetOrdinal("GioiTinh")) ? null : reader.GetString(reader.GetOrdinal("GioiTinh")),
                            SoDienThoai = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? null : reader.GetString(reader.GetOrdinal("SoDienThoai")),
                            Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                            DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? null : reader.GetString(reader.GetOrdinal("DiaChi")),
                            TrangThai = reader.IsDBNull(reader.GetOrdinal("TrangThai")) ? null : reader.GetString(reader.GetOrdinal("TrangThai"))
                        };
                    }
                }
            }
        }

        /// <summary>
        /// Lưu hoặc cập nhật hồ sơ khách hàng ban đầu và liên kết với tài khoản người dùng đang đăng nhập.
        /// Sử dụng <see cref="SqlTransaction"/> đảm bảo tính toàn vẹn ACID (nếu một bước thất bại sẽ Rollback hoàn toàn).
        /// </summary>
        /// <param name="hoTen">Họ và tên khách hàng.</param>
        /// <param name="cccd">Số CMND/CCCD hợp lệ.</param>
        /// <param name="ngaySinh">Ngày tháng năm sinh.</param>
        /// <param name="gioiTinh">Giới tính.</param>
        /// <param name="sdt">Số điện thoại liên lạc.</param>
        /// <param name="email">Hộp thư điện tử.</param>
        /// <param name="diaChi">Địa chỉ cư trú.</param>
        /// <returns>Đối tượng <see cref="AuthResult"/> chứa kết quả thành công kèm thông báo hoặc nguyên nhân thất bại.</returns>
        public static AuthResult SaveCustomerInfo(
            string hoTen,
            string cccd,
            DateTime ngaySinh,
            string gioiTinh,
            string sdt,
            string email,
            string diaChi)
        {
            // BƯỚC 0: Kiểm tra tính hợp lệ của phiên làm việc (Session Validation)
            if (!CurrentSession.IsLoggedIn)
            {
                return AuthResult.Fail("Bạn chưa đăng nhập vào hệ thống.");
            }

            if (CurrentSession.HasCustomerInfo)
            {
                return AuthResult.Fail("Bạn đã hoàn tất hồ sơ khách hàng trước đó rồi.");
            }

            DatabaseConfig config = DatabaseConfigManager.Load();

            try
            {
                using (var conn = DatabaseService.CreateConnection(config))
                {
                    conn.Open();

                    // Bắt đầu một Transaction để bảo vệ chuỗi thao tác nhiều bảng
                    using (var tran = conn.BeginTransaction())
                    {
                        try
                        {
                            string currentMaKH = CurrentSession.User?.MaKH;

                            // BƯỚC 1: Kiểm tra chống trùng số CCCD/CMND trên toàn hệ thống
                            using (var cmdCheckCccd = new SqlCommand(
                                "SELECT 1 FROM dbo.ThongTinKhachHang WHERE CMND_CCCD = @C AND (@CurrentMaKH IS NULL OR MaKH <> @CurrentMaKH)",
                                conn, tran))
                            {
                                cmdCheckCccd.Parameters.AddWithValue("@C", cccd);
                                cmdCheckCccd.Parameters.AddWithValue("@CurrentMaKH", (object)currentMaKH ?? DBNull.Value);
                                if (cmdCheckCccd.ExecuteScalar() != null)
                                {
                                    return AuthResult.Fail("Số CMND/CCCD này đã được đăng ký bởi một khách hàng khác trong hệ thống.");
                                }
                            }

                            // BƯỚC 2: Tự động sinh mã khách hàng mới theo mẫu KH000001 nếu chưa có mã
                            string maKH = currentMaKH;
                            if (string.IsNullOrWhiteSpace(maKH))
                            {
                                using (var cmdNextId = new SqlCommand(@"
                                    SELECT ISNULL(MAX(CAST(SUBSTRING(MaKH, 3, 10) AS INT)), 0) + 1
                                    FROM   dbo.ThongTinKhachHang
                                    WHERE  MaKH LIKE 'KH%'", conn, tran))
                                {
                                    int nextNumber = (int)cmdNextId.ExecuteScalar();
                                    maKH = "KH" + nextNumber.ToString("D6");
                                }
                            }

                            // BƯỚC 3: Thêm mới (INSERT) hoặc cập nhật (UPDATE) hồ sơ vào bảng ThongTinKhachHang
                            using (var cmdSaveProfile = new SqlCommand(@"
                                IF EXISTS (SELECT 1 FROM dbo.ThongTinKhachHang WHERE MaKH = @MaKH)
                                BEGIN
                                    UPDATE dbo.ThongTinKhachHang
                                    SET HoTen = @HoTen, CMND_CCCD = @CCCD, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh,
                                        SoDienThoai = @SDT, Email = @Email, DiaChi = @DiaChi, TrangThai = N'Active'
                                    WHERE MaKH = @MaKH;
                                END
                                ELSE
                                BEGIN
                                    INSERT INTO dbo.ThongTinKhachHang
                                        (MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh,
                                         SoDienThoai, Email, DiaChi, TrangThai)
                                    VALUES
                                        (@MaKH, @HoTen, @CCCD, @NgaySinh, @GioiTinh,
                                         @SDT, @Email, @DiaChi, N'Active');
                                END
                            ", conn, tran))
                            {
                                cmdSaveProfile.Parameters.AddWithValue("@MaKH", maKH);
                                cmdSaveProfile.Parameters.AddWithValue("@HoTen", hoTen);
                                cmdSaveProfile.Parameters.AddWithValue("@CCCD", cccd);
                                cmdSaveProfile.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                                cmdSaveProfile.Parameters.AddWithValue("@GioiTinh", (object)gioiTinh ?? DBNull.Value);
                                cmdSaveProfile.Parameters.AddWithValue("@SDT", (object)sdt ?? DBNull.Value);
                                cmdSaveProfile.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                                cmdSaveProfile.Parameters.AddWithValue("@DiaChi", (object)diaChi ?? DBNull.Value);
                                cmdSaveProfile.ExecuteNonQuery();
                            }

                            // BƯỚC 4: Liên kết mã khách hàng mới tạo vào tài khoản người dùng NguoiDung tương ứng
                            using (var cmdLinkUser = new SqlCommand(@"
                                UPDATE dbo.NguoiDung
                                SET    MaKH = @MaKH,
                                       HoTen = @HoTen,
                                       Email = @Email,
                                       LanDangNhap = ISNULL(LanDangNhap, GETDATE())
                                WHERE  MaNguoiDung = @MaNguoiDung", conn, tran))
                            {
                                cmdLinkUser.Parameters.AddWithValue("@MaKH", maKH);
                                cmdLinkUser.Parameters.AddWithValue("@HoTen", hoTen);
                                cmdLinkUser.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                                cmdLinkUser.Parameters.AddWithValue("@MaNguoiDung", CurrentSession.MaNguoiDung);

                                int rows = cmdLinkUser.ExecuteNonQuery();
                                if (rows == 0)
                                {
                                    throw new Exception("Không thể liên kết tài khoản đăng nhập với hồ sơ khách hàng.");
                                }
                            }

                            // Xác nhận Transaction hoàn tất thành công - ghi nhận vĩnh viễn vào CSDL
                            tran.Commit();

                            // BƯỚC 5: Cập nhật thông tin phiên làm việc cục bộ trong ứng dụng
                            CurrentSession.MarkCustomerInfoCompleted(maKH, hoTen, email);

                            return AuthResult.Ok($"Lưu hồ sơ thành công! Mã khách hàng của bạn: {maKH}");
                        }
                        catch
                        {
                            // Nếu xảy ra lỗi ở bất kỳ bước nào trong Transaction, Rollback toàn bộ để đảm bảo an toàn
                            tran.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return AuthResult.Fail("Lỗi cơ sở dữ liệu: " + ex.Message);
            }
            catch (Exception ex)
            {
                return AuthResult.Fail("Lỗi hệ thống: " + ex.Message);
            }
        }

        #endregion
    }

    #region LỚP MÔ HÌNH DỮ LIỆU DTO

    /// <summary>
    /// Đối tượng truyền tải dữ liệu (DTO) biểu diễn thông tin hồ sơ của một khách hàng trong hệ thống.
    /// </summary>
    public class CustomerProfile
    {
        /// <summary>Mã khách hàng định danh duy nhất (ví dụ: KH000001).</summary>
        public string MaKH { get; set; }

        /// <summary>Họ và tên đầy đủ của khách hàng.</summary>
        public string HoTen { get; set; }

        /// <summary>Số CMND hoặc Căn cước công dân (CCCD).</summary>
        public string CMND_CCCD { get; set; }

        /// <summary>Ngày tháng năm sinh.</summary>
        public DateTime? NgaySinh { get; set; }

        /// <summary>Giới tính (Nam, Nữ, hoặc Khác).</summary>
        public string GioiTinh { get; set; }

        /// <summary>Số điện thoại liên lạc.</summary>
        public string SoDienThoai { get; set; }

        /// <summary>Địa chỉ thư điện tử (Email).</summary>
        public string Email { get; set; }

        /// <summary>Địa chỉ cư trú đầy đủ.</summary>
        public string DiaChi { get; set; }

        /// <summary>Trạng thái hoạt động ('Active' hoặc 'Inactive').</summary>
        public string TrangThai { get; set; }
    }

    #endregion
}
