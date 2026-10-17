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
                return AuthResult.Fail("Bạn đã có hồ sơ khách hàng hoàn tất rồi.");

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
                            string currentMaKH = CurrentSession.User?.MaKH;

                            // ---------- 1. Kiểm tra trùng CCCD ----------
                            using (var cmd = new SqlCommand(
                                "SELECT 1 FROM dbo.ThongTinKhachHang WHERE CMND_CCCD = @C AND (@CurrentMaKH IS NULL OR MaKH <> @CurrentMaKH)",
                                conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@C", cccd);
                                cmd.Parameters.AddWithValue("@CurrentMaKH", (object)currentMaKH ?? DBNull.Value);
                                if (cmd.ExecuteScalar() != null)
                                    return AuthResult.Fail("CMND/CCCD đã được đăng ký bởi khách hàng khác.");
                            }

                            // ---------- 2. Sinh MaKH mới nếu chưa có ----------
                            string maKH = currentMaKH;
                            if (string.IsNullOrWhiteSpace(maKH))
                            {
                                using (var cmd = new SqlCommand(@"
                                    SELECT ISNULL(MAX(CAST(SUBSTRING(MaKH, 3, 10) AS INT)), 0) + 1
                                    FROM   dbo.ThongTinKhachHang
                                    WHERE  MaKH LIKE 'KH%'", conn, tran))
                                {
                                    int next = (int)cmd.ExecuteScalar();
                                    maKH = "KH" + next.ToString("D6");
                                }
                            }

                            // ---------- 3. INSERT / UPDATE ThongTinKhachHang ----------
                            using (var cmd = new SqlCommand(@"
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

                            // ---------- 3.1. INSERT / UPDATE dbo.KhachHang ----------
                            using (var cmdKh = new SqlCommand(@"
                                IF EXISTS (SELECT 1 FROM dbo.KhachHang WHERE MaKH = @MaKH)
                                BEGIN
                                    UPDATE dbo.KhachHang
                                    SET HoTen = @HoTen, CMND_CCCD = @CCCD, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh,
                                        SoDienThoai = @SDT, Email = @Email, DiaChi = @DiaChi, TrangThai = N'Active'
                                    WHERE MaKH = @MaKH;
                                END
                                ELSE
                                BEGIN
                                    INSERT INTO dbo.KhachHang
                                        (MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh,
                                         SoDienThoai, Email, DiaChi, TrangThai, NgayTao)
                                    VALUES
                                        (@MaKH, @HoTen, @CCCD, @NgaySinh, @GioiTinh,
                                         @SDT, @Email, @DiaChi, N'Active', GETDATE());
                                END
                            ", conn, tran))
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

                            // ---------- 3.2. INSERT dbo.TaiKhoan nếu chưa có tài khoản ----------
                            using (var cmdAcc = new SqlCommand(@"
                                IF NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan WHERE MaKH = @MaKH)
                                BEGIN
                                    DECLARE @NewAcc VARCHAR(30);
                                    SELECT @NewAcc = '1000' + RIGHT('000000' + CAST(ISNULL(MAX(CAST(RIGHT(SoTaiKhoan, 6) AS BIGINT)), 1000) + 1 AS VARCHAR(10)), 6)
                                    FROM dbo.TaiKhoan;

                                    IF @NewAcc IS NULL OR LEN(@NewAcc) < 10
                                        SET @NewAcc = '1000' + CAST(ABS(CHECKSUM(NEWID())) % 900000 + 100000 AS VARCHAR(10));

                                    INSERT INTO dbo.TaiKhoan (SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien, SoDuBanDau, SoDuHienTai, NgayMo, TrangThai)
                                    VALUES (@NewAcc, @MaKH, 'NH01', 'VND', 50000000, 50000000, GETDATE(), 'Active');

                                    INSERT INTO dbo.GiaoDich (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, NgayGiaoDich, NoiDung, TrangThai, LoaiTien, SoTienGoc, MaLoaiTien)
                                    VALUES ('GD' + CONVERT(VARCHAR(20), GETDATE(), 112) + REPLACE(CONVERT(VARCHAR(20), GETDATE(), 108), ':', '') + '_' + CAST(@MaNguoiDung AS VARCHAR(10)),
                                            @NewAcc, 1, 50000000, GETDATE(), N'Nạp tiền khởi tạo số dư mở tài khoản', N'Thành công', 'VND', 50000000, 'VND');
                                END
                            ", conn, tran))
                            {
                                cmdAcc.Parameters.AddWithValue("@MaKH", maKH);
                                cmdAcc.Parameters.AddWithValue("@MaNguoiDung", CurrentSession.MaNguoiDung);
                                cmdAcc.ExecuteNonQuery();
                            }

                            // ---------- 4. UPDATE NguoiDung.MaKH để link ----------
                            using (var cmd = new SqlCommand(@"
                                UPDATE dbo.NguoiDung
                                SET    MaKH = @MaKH,
                                       HoTen = @HoTen,
                                       Email = @Email,
                                       LanDangNhap = ISNULL(LanDangNhap, GETDATE())
                                WHERE  MaNguoiDung = @MaNguoiDung", conn, tran))
                            {
                                cmd.Parameters.AddWithValue("@MaKH", maKH);
                                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                                cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@MaNguoiDung", CurrentSession.MaNguoiDung);

                                int rows = cmd.ExecuteNonQuery();
                                if (rows == 0)
                                    throw new Exception("Không thể link tài khoản với khách hàng.");
                            }

                            tran.Commit();

                            // ---------- 5. Cập nhật session ----------
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

    public class CustomerProfile
    {
        public string MaKH { get; set; }
        public string HoTen { get; set; }
        public string CMND_CCCD { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string TrangThai { get; set; }
    }
}