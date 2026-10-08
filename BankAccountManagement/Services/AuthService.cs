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

        // ==========================================================
        //  ĐĂNG NHẬP — chỉ 1 bảng, không JOIN
        // ==========================================================
        public static AuthResult Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                return AuthResult.Fail("Vui lòng nhập tên đăng nhập.");

            if (string.IsNullOrWhiteSpace(password))
                return AuthResult.Fail("Vui lòng nhập mật khẩu.");

            const string sql = @"
                SELECT  MaNguoiDung,
                        TenDangNhap,
                        MaKH,
                        HoTen,
                        Email,
                        TrangThai,
                        Quyen
                FROM    NguoiDung
                WHERE   TenDangNhap = @TenDangNhap
                    AND MatKhau     = @MatKhau";

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
                            return AuthResult.Fail("Tài khoản đã bị khóa. Vui lòng liên hệ Admin.");

                        var user = new User
                        {
                            MaNguoiDung = reader.GetInt32(reader.GetOrdinal("MaNguoiDung")),
                            TenDangNhap = reader.GetString(reader.GetOrdinal("TenDangNhap")),
                            MaKH = reader.IsDBNull(reader.GetOrdinal("MaKH")) ? null : reader.GetString(reader.GetOrdinal("MaKH")),
                            HoTen = reader.IsDBNull(reader.GetOrdinal("HoTen")) ? null : reader.GetString(reader.GetOrdinal("HoTen")),
                            Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")),
                            TrangThai = trangThai,
                            Quyen = reader.GetString(reader.GetOrdinal("Quyen"))
                        };

                        UpdateLastLogin(user.MaNguoiDung);
                        return AuthResult.Ok(user, $"Xin chào {user.HoTen ?? user.TenDangNhap}!");
                    }
                }
            }
            catch (SqlException ex)
            {
                return AuthResult.Fail("Không kết nối được cơ sở dữ liệu: " + ex.Message);
            }
            catch (Exception ex)
            {
                return AuthResult.Fail("Lỗi hệ thống: " + ex.Message);
            }
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