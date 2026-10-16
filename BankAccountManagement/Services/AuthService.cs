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
                kh.MaKH      AS MaKHReal,       -- ⭐ bản ghi KH thật (NULL nếu chưa có)
                kh.HoTen     AS HoTenKH,
                kh.Email     AS EmailKH
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

                        var user = new User
                        {
                            MaNguoiDung = reader.GetInt32(reader.GetOrdinal("MaNguoiDung")),
                            TenDangNhap = reader.GetString(reader.GetOrdinal("TenDangNhap")),
                            Quyen = reader.GetString(reader.GetOrdinal("Quyen")),
                            TrangThai = trangThai,

                            MaKH = maKH,
                            HoTen = hoTen,
                            Email = email
                        };

                        reader.Close();

                        // Đảm bảo mỗi Member luôn có đúng 1 tài khoản ngân hàng hoạt động
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
        /// Đảm bảo mỗi Member luôn có đúng 1 mã khách hàng và 1 tài khoản ngân hàng hoạt động.
        /// </summary>
        private static void EnsureMemberHasAccount(SqlConnection conn, User user)
        {
            try
            {
                // 1. Kiểm tra hoặc tạo MaKH
                string maKH = user.MaKH;
                if (string.IsNullOrWhiteSpace(maKH))
                {
                    using (var cmdCheck = new SqlCommand("SELECT MaKH FROM dbo.NguoiDung WHERE MaNguoiDung = @Id", conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@Id", user.MaNguoiDung);
                        object val = cmdCheck.ExecuteScalar();
                        if (val != null && val != DBNull.Value && !string.IsNullOrWhiteSpace(val.ToString()))
                        {
                            maKH = val.ToString().Trim();
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(maKH))
                {
                    maKH = $"KH{100000 + user.MaNguoiDung}";
                    int offset = 0;
                    while (true)
                    {
                        string testMaKH = offset == 0 ? maKH : $"KH{100000 + user.MaNguoiDung + offset}";
                        using (var cmdDup = new SqlCommand("SELECT 1 FROM dbo.KhachHang WHERE MaKH = @MaKH", conn))
                        {
                            cmdDup.Parameters.AddWithValue("@MaKH", testMaKH);
                            if (cmdDup.ExecuteScalar() == null)
                            {
                                maKH = testMaKH;
                                break;
                            }
                        }
                        offset++;
                    }

                    string hoTen = !string.IsNullOrWhiteSpace(user.HoTen) ? user.HoTen : user.TenDangNhap;
                    string email = !string.IsNullOrWhiteSpace(user.Email) ? user.Email : $"{user.TenDangNhap}@gmail.com";
                    string cmnd = $"001200{user.MaNguoiDung:D6}";

                    using (var cmdInsKH = new SqlCommand(@"
                        IF NOT EXISTS (SELECT 1 FROM dbo.KhachHang WHERE MaKH = @MaKH)
                        BEGIN
                            INSERT INTO dbo.KhachHang (MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, TrangThai, NgayTao)
                            VALUES (@MaKH, @HoTen, @CMND, '1995-01-01', N'Nam', '0901234567', @Email, N'Hà Nội', 'Active', GETDATE());
                        END
                        IF NOT EXISTS (SELECT 1 FROM dbo.ThongTinKhachHang WHERE MaKH = @MaKH)
                        BEGIN
                            INSERT INTO dbo.ThongTinKhachHang (MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, TrangThai, NgayTao)
                            VALUES (@MaKH, @HoTen, @CMND, '1995-01-01', N'Nam', '0901234567', @Email, N'Hà Nội', 'Active', SYSDATETIME());
                        END
                        UPDATE dbo.NguoiDung SET MaKH = @MaKH WHERE MaNguoiDung = @MaND;
                    ", conn))
                    {
                        cmdInsKH.Parameters.AddWithValue("@MaKH", maKH);
                        cmdInsKH.Parameters.AddWithValue("@HoTen", hoTen);
                        cmdInsKH.Parameters.AddWithValue("@CMND", cmnd);
                        cmdInsKH.Parameters.AddWithValue("@Email", email);
                        cmdInsKH.Parameters.AddWithValue("@MaND", user.MaNguoiDung);
                        cmdInsKH.ExecuteNonQuery();
                    }

                    user.MaKH = maKH;
                }

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
        /// INSERT user mới với Quyen = 'Member' và tự động tạo thông tin Khách hàng, 
        /// 1 tài khoản ngân hàng duy nhất kèm số dư ban đầu 50.000.000 VNĐ.
        /// </summary>
        private static void InsertUser(
            SqlConnection conn, SqlTransaction tran,
            string username, string password, string hoTen, string email)
        {
            const string sql = @"
                INSERT INTO NguoiDung
                    (TenDangNhap, MatKhau, Quyen, MaKH, HoTen, Email, TrangThai)
                OUTPUT INSERTED.MaNguoiDung
                VALUES
                    (@TenDangNhap, @MatKhau, 'Member', NULL, @HoTen, @Email, N'Active')";

            int newUserId;
            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", username);
                cmd.Parameters.AddWithValue("@MatKhau", password);
                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);

                object res = cmd.ExecuteScalar();
                if (res == null)
                    throw new Exception("Không thể tạo tài khoản người dùng.");
                newUserId = Convert.ToInt32(res);
            }

            // Tạo mã khách hàng duy nhất
            string maKH = $"KH{100000 + newUserId}";
            int offset = 0;
            while (true)
            {
                string testMaKH = offset == 0 ? maKH : $"KH{100000 + newUserId + offset}";
                using (var cmdDup = new SqlCommand("SELECT 1 FROM dbo.KhachHang WHERE MaKH = @MaKH", conn, tran))
                {
                    cmdDup.Parameters.AddWithValue("@MaKH", testMaKH);
                    if (cmdDup.ExecuteScalar() == null) 
                    {
                        maKH = testMaKH;
                        break;
                    }
                }
                offset++;
            }

            string userEmail = !string.IsNullOrWhiteSpace(email) ? email : $"{username}@gmail.com";
            string cmnd = $"001200{newUserId:D6}";

            // Insert KhachHang, ThongTinKhachHang và liên kết NguoiDung.MaKH
            using (var cmdKH = new SqlCommand(@"
                INSERT INTO dbo.KhachHang (MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, TrangThai, NgayTao)
                VALUES (@MaKH, @HoTen, @CMND, '1995-01-01', N'Nam', '0901234567', @Email, N'Hà Nội', 'Active', GETDATE());

                INSERT INTO dbo.ThongTinKhachHang (MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, TrangThai, NgayTao)
                VALUES (@MaKH, @HoTen, @CMND, '1995-01-01', N'Nam', '0901234567', @Email, N'Hà Nội', 'Active', SYSDATETIME());

                UPDATE dbo.NguoiDung SET MaKH = @MaKH WHERE MaNguoiDung = @MaND;
            ", conn, tran))
            {
                cmdKH.Parameters.AddWithValue("@MaKH", maKH);
                cmdKH.Parameters.AddWithValue("@HoTen", hoTen);
                cmdKH.Parameters.AddWithValue("@CMND", cmnd);
                cmdKH.Parameters.AddWithValue("@Email", userEmail);
                cmdKH.Parameters.AddWithValue("@MaND", newUserId);
                cmdKH.ExecuteNonQuery();
            }

            // Tạo duy nhất 1 tài khoản ngân hàng (NH01 - VND)
            string newAccountNum = "100000" + (1000 + newUserId);
            using (var cmdMax = new SqlCommand("SELECT ISNULL(MAX(TRY_CAST(SoTaiKhoan AS BIGINT)), 1000001000) FROM dbo.TaiKhoan WHERE ISNUMERIC(SoTaiKhoan) = 1", conn, tran))
            {
                object maxObj = cmdMax.ExecuteScalar();
                if (maxObj != null && long.TryParse(maxObj.ToString(), out long maxVal))
                {
                    newAccountNum = (maxVal + 1).ToString();
                }
            }

            using (var cmdAcc = new SqlCommand(@"
                INSERT INTO dbo.TaiKhoan (SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien, SoDuBanDau, SoDuHienTai, NgayMo, TrangThai)
                VALUES (@STK, @MaKH, 'NH01', 'VND', 50000000, 50000000, GETDATE(), 'Active');
            ", conn, tran))
            {
                cmdAcc.Parameters.AddWithValue("@STK", newAccountNum);
                cmdAcc.Parameters.AddWithValue("@MaKH", maKH);
                cmdAcc.ExecuteNonQuery();
            }

            // Khởi tạo giao dịch nạp tiền ban đầu
            string maGD = "GD" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + newUserId;
            using (var cmdGD = new SqlCommand(@"
                INSERT INTO dbo.GiaoDich (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, NgayGiaoDich, NoiDung, TrangThai, LoaiTien, SoTienGoc, MaLoaiTien)
                VALUES (@MaGD, @STK, 1, 50000000, GETDATE(), N'Nạp tiền khởi tạo số dư mở tài khoản', N'Thành công', 'VND', 50000000, 'VND');
            ", conn, tran))
            {
                cmdGD.Parameters.AddWithValue("@MaGD", maGD);
                cmdGD.Parameters.AddWithValue("@STK", newAccountNum);
                cmdGD.ExecuteNonQuery();
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