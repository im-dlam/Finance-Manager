using BankAccountManagement.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace BankAccountManagement.Services
{
    public static class AuthService
    {
        private static SqlConnection OpenConnection()
        {
            DatabaseConfig config = DatabaseConfigManager.Load();
            var conn = DatabaseService.CreateConnection(config);
            conn.Open();
            return conn;
        }

        public static AuthResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                return AuthResult.Fail("Vui lòng nhập tên đăng nhập.");
            if (string.IsNullOrWhiteSpace(password))
                return AuthResult.Fail("Vui lòng nhập mật khẩu.");

            // ⭐ JOIN để biết chắc hồ sơ KH có tồn tại thật không
            const string sql = @"
        SELECT  nd.MaNguoiDung,
                nd.TenDangNhap,
                nd.Quyen,
                nd.TrangThai,
                nd.MaKH      AS MaKHLink,       -- link FK trong NguoiDung
                nd.HoTen,
                nd.Email,
                nd.LanDangNhap,
                kh.MaKH      AS MaKHReal,       -- ⭐ bản ghi KH thật (NULL nếu chưa có)
                kh.HoTen     AS HoTenKH,
                kh.Email     AS EmailKH,
                kh.CMND_CCCD,
                kh.SoDienThoai,
                kh.DiaChi,
                kh.NgaySinh
        FROM    dbo.NguoiDung nd
        LEFT JOIN dbo.ThongTinKhachHang kh 
               ON kh.MaKH = nd.MaKH
        WHERE   nd.TenDangNhap = @TenDangNhap
            AND nd.MatKhau     = @MatKhau";

            try
            {
                using (var conn = OpenConnection())
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@TenDangNhap", SqlDbType.VarChar, 50).Value = username;
                    cmd.Parameters.Add("@MatKhau", SqlDbType.VarChar, 255).Value = password;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return AuthResult.Fail("Tên đăng nhập hoặc mật khẩu không đúng.");

                        string trangThai = reader.GetString(reader.GetOrdinal("TrangThai"));
                        if (trangThai != "Active")
                            return AuthResult.Fail("Tài khoản đã bị khóa.");

                        string maKHReal = reader.IsDBNull(reader.GetOrdinal("MaKHReal"))
                                            ? null
                                            : reader.GetString(reader.GetOrdinal("MaKHReal"));
                        string maKHLink = reader.IsDBNull(reader.GetOrdinal("MaKHLink"))
                                            ? null
                                            : reader.GetString(reader.GetOrdinal("MaKHLink"));
                        string maKH = maKHReal ?? maKHLink;

                        // Ưu tiên dùng HoTen/Email từ bảng KH (mới nhất) nếu có
                        string hoTen = maKHReal != null && !reader.IsDBNull(reader.GetOrdinal("HoTenKH"))
                                        ? reader.GetString(reader.GetOrdinal("HoTenKH"))
                                        : (reader.IsDBNull(reader.GetOrdinal("HoTen"))
                                            ? null : reader.GetString(reader.GetOrdinal("HoTen")));

                        string email = maKHReal != null && !reader.IsDBNull(reader.GetOrdinal("EmailKH"))
                                        ? reader.GetString(reader.GetOrdinal("EmailKH"))
                                        : (reader.IsDBNull(reader.GetOrdinal("Email"))
                                            ? null : reader.GetString(reader.GetOrdinal("Email")));

                        bool isFirstLogin = reader.IsDBNull(reader.GetOrdinal("LanDangNhap"));
                        DateTime? lanDangNhap = isFirstLogin ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("LanDangNhap"));

                        string cmnd = reader.IsDBNull(reader.GetOrdinal("CMND_CCCD")) ? null : reader.GetString(reader.GetOrdinal("CMND_CCCD"));
                        string sdt = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? null : reader.GetString(reader.GetOrdinal("SoDienThoai"));
                        string diaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? null : reader.GetString(reader.GetOrdinal("DiaChi"));
                        bool hasDob = !reader.IsDBNull(reader.GetOrdinal("NgaySinh"));

                        bool isDummyCccd = !string.IsNullOrWhiteSpace(cmnd) && (cmnd.StartsWith("001200") && cmnd.Length == 12);
                        bool isDummyPhone = sdt == "0901234567";

                        bool hasCompleteProfile = !string.IsNullOrWhiteSpace(maKHReal)
                                               && !string.IsNullOrWhiteSpace(cmnd)
                                               && !isDummyCccd
                                               && !string.IsNullOrWhiteSpace(sdt)
                                               && !isDummyPhone
                                               && !string.IsNullOrWhiteSpace(diaChi)
                                               && hasDob;

                        var user = new User
                        {
                            MaNguoiDung = reader.GetInt32(reader.GetOrdinal("MaNguoiDung")),
                            TenDangNhap = reader.GetString(reader.GetOrdinal("TenDangNhap")),
                            Quyen = reader.GetString(reader.GetOrdinal("Quyen")),
                            TrangThai = trangThai,

                            MaKH = maKH,
                            HoTen = hoTen,
                            Email = email,
                            LanDangNhap = lanDangNhap,
                            IsFirstLogin = isFirstLogin,
                            HasCompleteProfile = hasCompleteProfile
                        };

                        reader.Close();

                        // Đảm bảo mỗi Member đã có hồ sơ KH hợp lệ luôn có đúng 1 tài khoản ngân hàng hoạt động
                        //Console.WriteLine($"[AuthService] Đăng nhập: {user.TenDangNhap}, Quyen: {user.IsAdmin}, MaKH: {user.MaKH}, HasCompleteProfile: {user.HasCompleteProfile}");
                        if (!user.IsAdmin)
                        {

                            EnsureMemberHasAccount(conn, user);
                        }

                        UpdateLastLogin(user.MaNguoiDung);

                        return AuthResult.Ok(user, $"Xin chào {user.TenDangNhap}!");
                    }
                }
            }
            catch (SqlException ex) { return AuthResult.Fail("Lỗi DB: " + ex.Message); }
            catch (Exception ex) { return AuthResult.Fail("Lỗi: " + ex.Message); }
        }

        private static void UpdateLastLogin(int maNguoiDung)
        {
            try
            {
                const string sql = @"
                    UPDATE NguoiDung
                    SET    LanDangNhap = GETDATE()
                    WHERE  MaNguoiDung = @Id";

                using (var conn = OpenConnection())
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", maNguoiDung);
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }

        // ==========================================================
        //  ĐĂNG KÝ — chỉ INSERT vào NguoiDung, Quyen = 'Member'
        // ==========================================================
        public static AuthResult Register(
            string fullName,
            string username,
            string email,
            string password)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return AuthResult.Fail("Vui lòng nhập họ tên.");

            if (string.IsNullOrWhiteSpace(username) || username.Length < 4)
                return AuthResult.Fail("Tên đăng nhập phải có ít nhất 4 ký tự.");

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                return AuthResult.Fail("Mật khẩu phải có ít nhất 6 ký tự.");

            try
            {
                using (var conn = OpenConnection())
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        if (ExistsByUsername(conn, tran, username))
                            return AuthResult.Fail("Tên đăng nhập đã được sử dụng.");

                        InsertUser(conn, tran, username, password, fullName, email);
                        InsertAuditLog(conn, tran, username, fullName);

                        tran.Commit();

                        return AuthResult.Ok(
                            $"Đăng ký thành công! Bạn có thể đăng nhập bằng tài khoản '{username}'.");
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
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

        /// <summary>
        /// Đảm bảo mỗi Member đã có hồ sơ KH hợp lệ luôn có đúng 1 tài khoản ngân hàng hoạt động.
        /// Lưu ý: KHÔNG tự tạo MaKH hay dữ liệu giả lập cho Member chưa hoàn tất hồ sơ.
        /// </summary>

        private static void EnsureMemberHasAccount(SqlConnection conn, User user)
        {
            if (conn == null)
                throw new ArgumentNullException(nameof(conn));

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(user.MaKH) || !user.HasCompleteProfile)
                return;

            using (SqlTransaction tran = conn.BeginTransaction(
                System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    string maKH = user.MaKH.Trim();

                    // 1. Xác nhận hồ sơ khách hàng thực sự tồn tại và đang hoạt động.
                    using (var cmd = new SqlCommand(@"
                SELECT COUNT(1)
                FROM dbo.ThongTinKhachHang WITH (UPDLOCK, HOLDLOCK)
                WHERE MaKH = @MaKH
                  AND TrangThai = N'Active';",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);

                        if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                        {
                            tran.Commit();
                            return;
                        }
                    }

                    // 2. Không tạo thêm nếu khách hàng đã có tài khoản hoạt động.
                    using (var cmd = new SqlCommand(@"
                SELECT TOP (1) 1
                FROM dbo.TaiKhoan WITH (UPDLOCK, HOLDLOCK)
                WHERE MaKH = @MaKH
                  AND TrangThai IN ('Active', N'Hoạt động');",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);

                        if (cmd.ExecuteScalar() != null)
                        {
                            tran.Commit();
                            return;
                        }
                    }

                    // 3. Lấy mã ngân hàng mặc định có thật trong database.
                    string maNganHang;

                    using (var cmd = new SqlCommand(@"
                SELECT TOP (1) MaNganHang
                FROM dbo.NganHang
                ORDER BY CASE WHEN MaNganHang = 'NH01' THEN 0 ELSE 1 END,
                         MaNganHang;",
                        conn, tran))
                    {
                        object result = cmd.ExecuteScalar();

                        if (result == null || result == DBNull.Value)
                            throw new InvalidOperationException(
                                "Chưa có ngân hàng nào được cấu hình trong hệ thống.");

                        maNganHang = result.ToString();
                    }

                    // 4. Lấy loại tiền VND từ database.
                    string maLoaiTien;

                    using (var cmd = new SqlCommand(@"
                SELECT TOP (1) MaLoaiTien
                FROM dbo.LoaiTien
                WHERE MaLoaiTien = 'VND';",
                        conn, tran))
                    {
                        object result = cmd.ExecuteScalar();

                        if (result == null || result == DBNull.Value)
                            throw new InvalidOperationException(
                                "Chưa cấu hình loại tiền VND trong hệ thống.");

                        maLoaiTien = result.ToString();
                    }

                    // 5. Sinh số tài khoản mới.
                    // HOLDLOCK giúp bảo vệ khoảng dữ liệu trong transaction,
                    // nhưng database vẫn cần UNIQUE constraint cho SoTaiKhoan.
                    long maxSoTaiKhoan;

                    using (var cmd = new SqlCommand(@"
                SELECT ISNULL(
                    MAX(TRY_CONVERT(BIGINT, SoTaiKhoan)),
                    1000001000
                )
                FROM dbo.TaiKhoan WITH (UPDLOCK, HOLDLOCK)
                WHERE TRY_CONVERT(BIGINT, SoTaiKhoan) IS NOT NULL;",
                        conn, tran))
                    {
                        maxSoTaiKhoan = Convert.ToInt64(cmd.ExecuteScalar());
                    }

                    if (maxSoTaiKhoan == long.MaxValue)
                        throw new InvalidOperationException(
                            "Không thể sinh số tài khoản mới.");

                    string soTaiKhoan = (maxSoTaiKhoan + 1).ToString();

                    // 6. Mở tài khoản với số dư bằng 0.
                    using (var cmd = new SqlCommand(@"
                INSERT INTO dbo.TaiKhoan
                (
                    SoTaiKhoan,
                    MaKH,
                    MaNganHang,
                    MaLoaiTien,
                    SoDuBanDau,
                    SoDuHienTai,
                    NgayMo,
                    TrangThai
                )
                VALUES
                (
                    @SoTaiKhoan,
                    @MaKH,
                    @MaNganHang,
                    @MaLoaiTien,
                    0,
                    0,
                    GETDATE(),
                    'Active'
                );",
                        conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        cmd.Parameters.AddWithValue("@MaNganHang", maNganHang);
                        cmd.Parameters.AddWithValue("@MaLoaiTien", maLoaiTien);

                        cmd.ExecuteNonQuery();
                    }

                    // Không tạo giao dịch nạp tiền ở đây.
                    // Chỉ ghi giao dịch khi có nghiệp vụ nạp tiền thực sự.

                    tran.Commit();
                }
                catch
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch
                    {
                        // Giữ nguyên ngoại lệ gốc.
                    }

                    throw;
                }
            }
        }


        // ==========================================================
        //  PRIVATE HELPERS
        // ==========================================================
        private static bool ExistsByUsername(
            SqlConnection conn, SqlTransaction tran, string username)
        {
            const string sql = "SELECT 1 FROM NguoiDung WHERE TenDangNhap = @U";
            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.AddWithValue("@U", username);
                return cmd.ExecuteScalar() != null;
            }
        }

        /// <summary>
        /// INSERT user mới với Quyen = 'Member'.
        /// MaKH và LanDangNhap ban đầu là NULL để yêu cầu điền hồ sơ khách hàng khi đăng nhập lần đầu.
        /// </summary>
        private static void InsertUser(
            SqlConnection conn, SqlTransaction tran,
            string username, string password, string hoTen, string email)
        {
            const string sql = @"
                INSERT INTO NguoiDung
                    (TenDangNhap, MatKhau, Quyen, MaKH, HoTen, Email, TrangThai, LanDangNhap)
                OUTPUT INSERTED.MaNguoiDung
                VALUES
                    (@TenDangNhap, @MatKhau, 'Member', NULL, @HoTen, @Email, N'Active', NULL)";

            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", username);
                cmd.Parameters.AddWithValue("@MatKhau", password);
                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);

                object res = cmd.ExecuteScalar();
                if (res == null)
                    throw new Exception("Không thể tạo tài khoản người dùng.");
            }
        }

        private static void InsertAuditLog(
            SqlConnection conn, SqlTransaction tran,
            string username, string hoTen)
        {
            const string sql = @"
                INSERT INTO NhatKyHoatDong
                    (MaNguoiDung, HanhDong, DoiTuong, DoiTuongID, ChiTiet)
                SELECT MaNguoiDung, N'REGISTER', N'NguoiDung', @Username, @ChiTiet
                FROM   NguoiDung
                WHERE  TenDangNhap = @Username";

            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@ChiTiet",
                    $"Đăng ký tài khoản Member mới: {hoTen}");
                cmd.ExecuteNonQuery();
            }
        }
    }
}