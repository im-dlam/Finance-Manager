using System;
using System.Data;
using System.Data.SqlClient;
using BankAccountManagement.Models;

namespace BankAccountManagement.Services
{
    public class CustomerService
    {
        private string GetConnectionString()
        {
            DatabaseConfig config = DatabaseConfigManager.Load();
            return DatabaseService.BuildConnectionString(config);
        }

        public bool ToggleUserStatus(string maNguoiDung)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                string getStatusSql = "SELECT TrangThai FROM dbo.NguoiDung WHERE MaNguoiDung = @Ma";
                string currentStatus = "Active";
                using (SqlCommand cmdGet = new SqlCommand(getStatusSql, conn))
                {
                    cmdGet.Parameters.AddWithValue("@Ma", maNguoiDung);
                    object result = cmdGet.ExecuteScalar();
                    if (result != null) currentStatus = result.ToString();
                }

                string newStatus = (currentStatus == "Active") ? "Locked" : "Active";

                string updateSql = "UPDATE dbo.NguoiDung SET TrangThai = @NewStatus WHERE MaNguoiDung = @Ma";
                using (SqlCommand cmdUpdate = new SqlCommand(updateSql, conn))
                {
                    cmdUpdate.Parameters.AddWithValue("@NewStatus", newStatus);
                    cmdUpdate.Parameters.AddWithValue("@Ma", maNguoiDung);
                    return cmdUpdate.ExecuteNonQuery() > 0;
                }
            }
        }

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
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable SearchData(string keyword, bool isAdmin)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = isAdmin 
                    ? @"SELECT MaNguoiDung, TenDangNhap, HoTen, Quyen, Email, TrangThai, NgayTao 
                        FROM dbo.NguoiDung 
                        WHERE (TenDangNhap LIKE @Keyword OR HoTen LIKE @Keyword OR Email LIKE @Keyword)
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

        public bool AddAdminUser(string tenDangNhap, string hoTen, string quyen, string email)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                string sql = @"INSERT INTO dbo.NguoiDung (TenDangNhap, MatKhau, HoTen, Quyen, Email, TrangThai, NgayTao) 
                               VALUES (@TenDangNhap, '123456', @HoTen, @Quyen, @Email, 'Active', GETDATE())";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@Quyen", quyen);
                    cmd.Parameters.AddWithValue("@Email", email);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

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
                    cmd.Parameters.AddWithValue("@MaND", maND);
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@Quyen", quyen);
                    cmd.Parameters.AddWithValue("@Email", email);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

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
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@SDT", sdt);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                    
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

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
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@SDT", sdt);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                    
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

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
    
        public static AuthResult SaveCustomerInfo(
            string hoTen,
            string cccd,
            DateTime ngaySinh,
            string gioiTinh,
            string sdt,
            string email,
            string diaChi)
        {
            // ---------- Validate session ----------
            if (!CurrentSession.IsLoggedIn)
                return AuthResult.Fail("Bạn chưa đăng nhập.");

            if (CurrentSession.HasCustomerInfo)
                return AuthResult.Fail("Bạn đã có hồ sơ khách hàng rồi.");

            DatabaseConfig config = DatabaseConfigManager.Load();

            try
            {
                using (var conn = DatabaseService.CreateConnection(config))
                {
                    conn.Open();
                    using (var tran = conn.BeginTransaction())
                    {
                        try
                        {
                            // ---------- 1. Kiểm tra trùng CCCD ----------
                            using (var cmd = new SqlCommand(
                                "SELECT 1 FROM dbo.ThongTinKhachHang WHERE CMND_CCCD = @C",
                                conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@C", cccd);
                                if (cmd.ExecuteScalar() != null)
                                    return AuthResult.Fail("CMND/CCCD đã được đăng ký.");
                            }

                            // ---------- 2. Sinh MaKH mới ----------
                            string maKH;
                            using (var cmd = new SqlCommand(@"
                        SELECT ISNULL(MAX(CAST(SUBSTRING(MaKH, 3, 10) AS INT)), 0) + 1
                        FROM   dbo.ThongTinKhachHang
                        WHERE  MaKH LIKE 'KH%'", conn, tran))
                            {
                                int next = (int)cmd.ExecuteScalar();
                                maKH = "KH" + next.ToString("D6");
                            }

                            // ---------- 3. INSERT ThongTinKhachHang ----------
                            using (var cmd = new SqlCommand(@"
                        INSERT INTO dbo.ThongTinKhachHang
                            (MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh,
                             SoDienThoai, Email, DiaChi, TrangThai)
                        VALUES
                            (@MaKH, @HoTen, @CCCD, @NgaySinh, @GioiTinh,
                             @SDT, @Email, @DiaChi, N'Active')", conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@MaKH", maKH);
                                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                                cmd.Parameters.AddWithValue("@CCCD", cccd);
                                cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                                cmd.Parameters.AddWithValue("@GioiTinh", (object)gioiTinh ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@SDT", (object)sdt ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@DiaChi", (object)diaChi ?? DBNull.Value);
                                cmd.ExecuteNonQuery();
                            }

                            // ---------- 3.1. INSERT dbo.KhachHang (đảm bảo đồng bộ FK với TaiKhoan) ----------
                            using (var cmdKh = new SqlCommand(@"
                        INSERT INTO dbo.KhachHang
                            (MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh,
                             SoDienThoai, Email, DiaChi, TrangThai, NgayTao)
                        VALUES
                            (@MaKH, @HoTen, @CCCD, @NgaySinh, @GioiTinh,
                             @SDT, @Email, @DiaChi, N'Active', GETDATE())", conn, tran))
                            {
                                cmdKh.Parameters.AddWithValue("@MaKH", maKH);
                                cmdKh.Parameters.AddWithValue("@HoTen", hoTen);
                                cmdKh.Parameters.AddWithValue("@CCCD", cccd);
                                cmdKh.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                                cmdKh.Parameters.AddWithValue("@GioiTinh", (object)gioiTinh ?? DBNull.Value);
                                cmdKh.Parameters.AddWithValue("@SDT", (object)sdt ?? DBNull.Value);
                                cmdKh.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                                cmdKh.Parameters.AddWithValue("@DiaChi", (object)diaChi ?? DBNull.Value);
                                cmdKh.ExecuteNonQuery();
                            }

                            // ---------- 4. UPDATE NguoiDung.MaKH để link ----------
                            using (var cmd = new SqlCommand(@"
                        UPDATE dbo.NguoiDung
                        SET    MaKH = @MaKH
                        WHERE  MaNguoiDung = @MaNguoiDung", conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@MaKH", maKH);
                                cmd.Parameters.AddWithValue("@MaNguoiDung", CurrentSession.MaNguoiDung);

                                int rows = cmd.ExecuteNonQuery();
                                if (rows == 0)
                                    throw new Exception("Không thể link tài khoản với khách hàng.");
                            }

                            tran.Commit();

                            // ---------- 5. Cập nhật session ----------
                            // MaKH là string trong schema hiện tại
                            CurrentSession.User.MaKH = maKH;
                            CurrentSession.User.HoTen = hoTen;
                            CurrentSession.User.Email = email;
                            CurrentSession.MarkCustomerInfoCompleted(maKH, hoTen, email);

                            return AuthResult.Ok(
                                $"Lưu hồ sơ thành công! Mã khách hàng của bạn: {maKH}");
                        }
                        catch
                        {
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
    
    }
}