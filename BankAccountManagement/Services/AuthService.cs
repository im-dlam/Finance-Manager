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

                        // ⭐ Đọc MaKH TỪ BẢNG KH (chỉ set khi thật sự có bản ghi)
                        string maKHReal = reader.IsDBNull(reader.GetOrdinal("MaKHReal"))
                                            ? null
                                            : reader.GetString(reader.GetOrdinal("MaKHReal"));

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

                            MaKH = maKHReal,   // ⭐ CHỈ set khi bản ghi KH tồn tại thật
                            HoTen = hoTen,
                            Email = email
                        };

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
        /// INSERT user mới với Quyen = 'Member', MaKH = NULL.
        /// Admin/Staff sẽ link với KhachHang sau khi khách đến quầy.
        /// </summary>
        private static void InsertUser(
            SqlConnection conn, SqlTransaction tran,
            string username, string password, string hoTen, string email)
        {
            const string sql = @"
                INSERT INTO NguoiDung
                    (TenDangNhap, MatKhau, Quyen, MaKH, HoTen, Email, TrangThai)
                VALUES
                    (@TenDangNhap, @MatKhau, 'Member', NULL, @HoTen, @Email, N'Active')";

            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", username);
                cmd.Parameters.AddWithValue("@MatKhau", password);
                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);

                int rows = cmd.ExecuteNonQuery();
                if (rows == 0)
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