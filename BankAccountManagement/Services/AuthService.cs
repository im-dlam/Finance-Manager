using BankAccountManagement.Models;
using System;
using System.Data;
using System.Data.SqlClient;

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
            try
            {
                // Chỉ kiểm tra/tạo tài khoản ngân hàng nếu user ĐÃ có MaKH và ĐÃ có hồ sơ khách hàng thật
                if (string.IsNullOrWhiteSpace(user.MaKH) || !user.HasCompleteProfile)
                {
                    return;
                }

                string maKH = user.MaKH;

                // 2. Kiểm tra xem tài khoản ngân hàng Active đã tồn tại chưa
                bool hasAccount = false;
                using (var cmdAcc = new SqlCommand("SELECT 1 FROM dbo.TaiKhoan WHERE MaKH = @MaKH AND TrangThai IN ('Active', N'Hoạt động')", conn))
                {
                    cmdAcc.Parameters.AddWithValue("@MaKH", maKH);
                    hasAccount = cmdAcc.ExecuteScalar() != null;
                }

                if (!hasAccount)
                {
                    string newAccountNum = "100000" + (1000 + user.MaNguoiDung);
                    using (var cmdMax = new SqlCommand("SELECT ISNULL(MAX(TRY_CAST(SoTaiKhoan AS BIGINT)), 1000001000) FROM dbo.TaiKhoan WHERE ISNUMERIC(SoTaiKhoan) = 1", conn))
                    {
                        object maxObj = cmdMax.ExecuteScalar();
                        if (maxObj != null && long.TryParse(maxObj.ToString(), out long maxVal))
                        {
                            newAccountNum = (maxVal + 1).ToString();
                        }
                    }

                    using (var cmdCreateAcc = new SqlCommand(@"
                        INSERT INTO dbo.TaiKhoan (SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien, SoDuBanDau, SoDuHienTai, NgayMo, TrangThai)
                        VALUES (@STK, @MaKH, 'NH01', 'VND', 50000000, 50000000, GETDATE(), 'Active');
                    ", conn))
                    {
                        cmdCreateAcc.Parameters.AddWithValue("@STK", newAccountNum);
                        cmdCreateAcc.Parameters.AddWithValue("@MaKH", maKH);
                        cmdCreateAcc.ExecuteNonQuery();
                    }

                    string maGD = "GD" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + user.MaNguoiDung;
                    using (var cmdGD = new SqlCommand(@"
                        INSERT INTO dbo.GiaoDich (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, NgayGiaoDich, NoiDung, TrangThai, LoaiTien, SoTienGoc, MaLoaiTien)
                        VALUES (@MaGD, @STK, 1, 50000000, GETDATE(), N'Nạp tiền khởi tạo số dư mở tài khoản', N'Thành công', 'VND', 50000000, 'VND');
                    ", conn))
                    {
                        cmdGD.Parameters.AddWithValue("@MaGD", maGD);
                        cmdGD.Parameters.AddWithValue("@STK", newAccountNum);
                        cmdGD.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("EnsureMemberHasAccount error: " + ex.Message);
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