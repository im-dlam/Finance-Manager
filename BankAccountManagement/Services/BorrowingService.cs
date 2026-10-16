using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Dịch vụ xử lý nghiệp vụ Vay vốn / Vay nhanh (Borrowing Service):
    /// - Quản lý đăng ký hồ sơ vay cho Khách hàng
    /// - Thẩm định tài chính 1 quý (90 ngày) cho Admin
    /// - Phê duyệt giải ngân trực tiếp vào tài khoản ngân hàng và từ chối hồ sơ
    /// </summary>
    public class BorrowingService
    {
        private readonly DatabaseConfig _config;

        public BorrowingService()
        {
            _config = DatabaseConfigManager.Load();
            EnsureTableExists();
        }

        public BorrowingService(DatabaseConfig config)
        {
            _config = config ?? DatabaseConfigManager.Load();
            EnsureTableExists();
        }

        private string GetConnectionString()
        {
            return DatabaseService.BuildConnectionString(_config);
        }

        /// <summary>
        /// Đảm bảo bảng dbo.KhoanVay luôn tồn tại trong cơ sở dữ liệu.
        /// </summary>
        public void EnsureTableExists()
        {
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    const string sql = @"
                        IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'KhoanVay')
                        BEGIN
                            CREATE TABLE dbo.KhoanVay (
                                MaKhoanVay VARCHAR(30) PRIMARY KEY,
                                MaKH VARCHAR(20) NOT NULL,
                                SoTaiKhoan VARCHAR(30) NOT NULL,
                                SoTienVay DECIMAL(18,2) NOT NULL,
                                ThoiGianVay INT NOT NULL,
                                LaiSuat DECIMAL(5,2) NOT NULL DEFAULT 1.55,
                                TienLai DECIMAL(18,2) NOT NULL DEFAULT 0,
                                TongPhaiTra DECIMAL(18,2) NOT NULL DEFAULT 0,
                                NgayDangKy DATETIME NOT NULL DEFAULT GETDATE(),
                                NgayDaoHan DATETIME NOT NULL,
                                TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Chờ xét duyệt',
                                GhiChu NVARCHAR(500) NULL,
                                NgayDuyet DATETIME NULL,
                                NguoiDuyet NVARCHAR(50) NULL
                            );
                        END";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BorrowingService] EnsureTableExists error: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy tài khoản ngân hàng đang hoạt động của khách hàng để nhận giải ngân.
        /// </summary>
        public async Task<List<TaiKhoan>> GetUserPaymentAccountsAsync(string maKH)
        {
            var list = new List<TaiKhoan>();
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    const string sql = @"
                        SELECT TK.SoTaiKhoan, TK.MaKH, TK.MaNganHang, TK.MaLoaiTien,
                               TK.SoDuBanDau, TK.SoDuHienTai, TK.NgayMo, TK.TrangThai,
                               NH.TenNganHang
                        FROM dbo.TaiKhoan TK
                        INNER JOIN dbo.NganHang NH ON TK.MaNganHang = NH.MaNganHang
                        WHERE (TK.TrangThai = 'Active' OR TK.TrangThai = N'Hoạt động')
                          AND (TK.MaKH = @MaKH OR TK.MaKH IN (
                               SELECT MaKH FROM dbo.NguoiDung 
                               WHERE MaNguoiDung = @MaND OR TenDangNhap = @UserLogin))
                        ORDER BY TK.SoTaiKhoan";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH ?? "");
                        cmd.Parameters.AddWithValue("@MaND", CurrentSession.MaNguoiDung);
                        cmd.Parameters.AddWithValue("@UserLogin", CurrentSession.TenDangNhap ?? "");

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new TaiKhoan
                                {
                                    SoTaiKhoan = reader["SoTaiKhoan"].ToString(),
                                    MaKH = reader["MaKH"].ToString(),
                                    MaNganHang = reader["TenNganHang"]?.ToString() ?? reader["MaNganHang"].ToString(),
                                    MaLoaiTien = reader["MaLoaiTien"].ToString(),
                                    SoDuBanDau = Convert.ToDecimal(reader["SoDuBanDau"]),
                                    SoDuHienTai = Convert.ToDecimal(reader["SoDuHienTai"]),
                                    NgayMo = Convert.ToDateTime(reader["NgayMo"]),
                                    TrangThai = reader["TrangThai"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BorrowingService] GetUserPaymentAccountsAsync error: {ex.Message}");
            }
            return list;
        }

        /// <summary>
        /// Tạo mới một hồ sơ đăng ký khoản vay cho Khách hàng.
        /// </summary>
        public async Task<(bool Success, string Message, string MaKhoanVay)> CreateLoanAsync(
            string maKH, string soTaiKhoan, decimal soTienVay, int thoiGianVay, decimal laiSuat, string ghiChu)
        {
            if (string.IsNullOrWhiteSpace(maKH))
                return (false, "Không tìm thấy thông tin khách hàng.", "");

            if (string.IsNullOrWhiteSpace(soTaiKhoan))
                return (false, "Vui lòng chọn tài khoản ngân hàng nhận tiền giải ngân.", "");

            if (soTienVay <= 0)
                return (false, "Số tiền vay phải lớn hơn 0.", "");

            if (thoiGianVay <= 0)
                return (false, "Thời gian vay không hợp lệ.", "");

            try
            {
                // Công thức tính lãi suất: Lãi = Vốn * (Lãi suất ngày) * Số ngày
                decimal dailyRate = (laiSuat > 0 ? laiSuat : 1.55m) / 100m;
                decimal tienLai = Math.Round(soTienVay * dailyRate * thoiGianVay, 0);
                decimal tongPhaiTra = soTienVay + tienLai;
                DateTime ngayDangKy = DateTime.Now;
                DateTime ngayDaoHan = ngayDangKy.AddDays(thoiGianVay);

                string maKV = "KV" + DateTime.Now.ToString("yyMMddHHmmss") + (new Random().Next(100, 999));

                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    const string sql = @"
                        INSERT INTO dbo.KhoanVay 
                            (MaKhoanVay, MaKH, SoTaiKhoan, SoTienVay, ThoiGianVay, LaiSuat, TienLai, 
                             TongPhaiTra, NgayDangKy, NgayDaoHan, TrangThai, GhiChu)
                        VALUES 
                            (@MaKV, @MaKH, @STK, @SoTien, @ThoiGian, @LaiSuat, @TienLai, 
                             @TongPhaiTra, @NgayDK, @NgayDH, N'Chờ xét duyệt', @GhiChu)";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKV", maKV);
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        cmd.Parameters.AddWithValue("@STK", soTaiKhoan);
                        cmd.Parameters.AddWithValue("@SoTien", soTienVay);
                        cmd.Parameters.AddWithValue("@ThoiGian", thoiGianVay);
                        cmd.Parameters.AddWithValue("@LaiSuat", laiSuat);
                        cmd.Parameters.AddWithValue("@TienLai", tienLai);
                        cmd.Parameters.AddWithValue("@TongPhaiTra", tongPhaiTra);
                        cmd.Parameters.AddWithValue("@NgayDK", ngayDangKy);
                        cmd.Parameters.AddWithValue("@NgayDH", ngayDaoHan);
                        cmd.Parameters.AddWithValue("@GhiChu", (object)ghiChu ?? DBNull.Value);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return (true, "Đăng ký khoản vay thành công! Hồ sơ đang chờ Ban quản trị xét duyệt giải ngân.", maKV);
            }
            catch (Exception ex)
            {
                return (false, "Lỗi khi đăng ký khoản vay: " + ex.Message, "");
            }
        }

        /// <summary>
        /// Lấy danh sách hồ sơ vay của một khách hàng cụ thể.
        /// </summary>
        public async Task<List<BorrowingLoan>> GetUserLoansAsync(string maKH)
        {
            var list = new List<BorrowingLoan>();
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    const string sql = @"
                        SELECT KV.MaKhoanVay, KV.MaKH, KV.SoTaiKhoan, KV.SoTienVay, KV.ThoiGianVay,
                               KV.LaiSuat, KV.TienLai, KV.TongPhaiTra, KV.NgayDangKy, KV.NgayDaoHan,
                               KV.TrangThai, KV.GhiChu, KV.NgayDuyet, KV.NguoiDuyet,
                               ISNULL(KH.HoTen, ND.HoTen) AS HoTen,
                               ND.TenDangNhap,
                               NH.TenNganHang
                        FROM dbo.KhoanVay KV
                        LEFT JOIN dbo.ThongTinKhachHang KH ON KV.MaKH = KH.MaKH
                        LEFT JOIN dbo.NguoiDung ND ON KV.MaKH = ND.MaKH
                        LEFT JOIN dbo.TaiKhoan TK ON KV.SoTaiKhoan = TK.SoTaiKhoan
                        LEFT JOIN dbo.NganHang NH ON TK.MaNganHang = NH.MaNganHang
                        WHERE KV.MaKH = @MaKH OR KV.MaKH IN (
                              SELECT MaKH FROM dbo.NguoiDung WHERE MaNguoiDung = @MaND OR TenDangNhap = @UserLogin)
                        ORDER BY KV.NgayDangKy DESC";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH ?? "");
                        cmd.Parameters.AddWithValue("@MaND", CurrentSession.MaNguoiDung);
                        cmd.Parameters.AddWithValue("@UserLogin", CurrentSession.TenDangNhap ?? "");

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(ReadLoanFromReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BorrowingService] GetUserLoansAsync error: {ex.Message}");
            }
            return list;
        }

        /// <summary>
        /// Lấy toàn bộ danh sách hồ sơ vay phục vụ quản lý phía Admin (có lọc và tìm kiếm).
        /// </summary>
        public async Task<List<BorrowingLoan>> GetAllLoansAsync(string statusFilter = "Tất cả", string keyword = "")
        {
            var list = new List<BorrowingLoan>();
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    string sql = @"
                        SELECT KV.MaKhoanVay, KV.MaKH, KV.SoTaiKhoan, KV.SoTienVay, KV.ThoiGianVay,
                               KV.LaiSuat, KV.TienLai, KV.TongPhaiTra, KV.NgayDangKy, KV.NgayDaoHan,
                               KV.TrangThai, KV.GhiChu, KV.NgayDuyet, KV.NguoiDuyet,
                               ISNULL(KH.HoTen, ISNULL(ND.HoTen, N'Chưa cập nhật')) AS HoTen,
                               ISNULL(ND.TenDangNhap, '') AS TenDangNhap,
                               ISNULL(NH.TenNganHang, '') AS TenNganHang
                        FROM dbo.KhoanVay KV
                        LEFT JOIN dbo.ThongTinKhachHang KH ON KV.MaKH = KH.MaKH
                        LEFT JOIN dbo.NguoiDung ND ON KV.MaKH = ND.MaKH
                        LEFT JOIN dbo.TaiKhoan TK ON KV.SoTaiKhoan = TK.SoTaiKhoan
                        LEFT JOIN dbo.NganHang NH ON TK.MaNganHang = NH.MaNganHang
                        WHERE 1 = 1 ";

                    if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "Tất cả")
                    {
                        sql += " AND KV.TrangThai = @Status ";
                    }

                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        sql += @" AND (KV.MaKhoanVay LIKE @Kw OR KV.MaKH LIKE @Kw 
                                       OR KV.SoTaiKhoan LIKE @Kw OR KH.HoTen LIKE @Kw 
                                       OR ND.TenDangNhap LIKE @Kw) ";
                    }

                    sql += " ORDER BY CASE WHEN KV.TrangThai = N'Chờ xét duyệt' THEN 0 ELSE 1 END, KV.NgayDangKy DESC";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "Tất cả")
                        {
                            cmd.Parameters.AddWithValue("@Status", statusFilter);
                        }
                        if (!string.IsNullOrWhiteSpace(keyword))
                        {
                            cmd.Parameters.AddWithValue("@Kw", "%" + keyword.Trim() + "%");
                        }

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(ReadLoanFromReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BorrowingService] GetAllLoansAsync error: {ex.Message}");
            }
            return list;
        }

        /// <summary>
        /// Thẩm định tài chính trong 1 quý (90 ngày gần nhất) của khách hàng
        /// gồm: Mã tài khoản, Mã khách hàng, Tên người dùng, Tổng thu chi trong 1 quý.
        /// </summary>
        public async Task<FinancialAppraisal> GetFinancialAppraisalAsync(string maKH, string soTaiKhoan, decimal soTienVay)
        {
            var fa = new FinancialAppraisal
            {
                MaKH = maKH,
                SoTaiKhoan = soTaiKhoan
            };

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();

                    // 1. Thông tin khách hàng & tài khoản
                    const string sqlInfo = @"
                        SELECT TOP 1 
                               ISNULL(KH.HoTen, ISNULL(ND.HoTen, N'Chưa cập nhật')) AS HoTen,
                               ISNULL(ND.TenDangNhap, '') AS TenDangNhap,
                               TK.SoDuHienTai,
                               NH.TenNganHang
                        FROM dbo.TaiKhoan TK
                        LEFT JOIN dbo.ThongTinKhachHang KH ON TK.MaKH = KH.MaKH
                        LEFT JOIN dbo.NguoiDung ND ON TK.MaKH = ND.MaKH
                        LEFT JOIN dbo.NganHang NH ON TK.MaNganHang = NH.MaNganHang
                        WHERE TK.SoTaiKhoan = @STK";

                    using (var cmdInfo = new SqlCommand(sqlInfo, conn))
                    {
                        cmdInfo.Parameters.AddWithValue("@STK", soTaiKhoan);
                        using (var reader = await cmdInfo.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                fa.HoTen = reader["HoTen"]?.ToString();
                                fa.TenDangNhap = reader["TenDangNhap"]?.ToString();
                                fa.TenNganHang = reader["TenNganHang"]?.ToString();
                                fa.SoDuHienTai = reader["SoDuHienTai"] != DBNull.Value ? Convert.ToDecimal(reader["SoDuHienTai"]) : 0m;
                            }
                        }
                    }

                    // 2. Thống kê thu chi trong 1 quý (3 tháng / 90 ngày gần nhất)
                    const string sqlQuarter = @"
                        SELECT 
                            ISNULL(SUM(CASE WHEN L.PhanLoai = 'INCOME' THEN G.SoTien ELSE 0 END), 0) AS TongThuQuy,
                            ISNULL(SUM(CASE WHEN L.PhanLoai = 'EXPENSE' THEN G.SoTien ELSE 0 END), 0) AS TongChiQuy,
                            COUNT(G.MaGiaoDich) AS SoGiaoDichQuy
                        FROM dbo.GiaoDich G
                        INNER JOIN dbo.LoaiGiaoDich L ON G.MaLoaiGD = L.MaLoaiGD
                        WHERE G.SoTaiKhoan = @STK
                          AND G.NgayGiaoDich >= DATEADD(DAY, -90, GETDATE())
                          AND (G.TrangThai = 'Success' OR G.TrangThai = N'Hoàn tất')";

                    using (var cmdQuarter = new SqlCommand(sqlQuarter, conn))
                    {
                        cmdQuarter.Parameters.AddWithValue("@STK", soTaiKhoan);
                        using (var reader = await cmdQuarter.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                fa.TongThuQuy = reader["TongThuQuy"] != DBNull.Value ? Convert.ToDecimal(reader["TongThuQuy"]) : 0m;
                                fa.TongChiQuy = reader["TongChiQuy"] != DBNull.Value ? Convert.ToDecimal(reader["TongChiQuy"]) : 0m;
                                fa.SoGiaoDichQuy = reader["SoGiaoDichQuy"] != DBNull.Value ? Convert.ToInt32(reader["SoGiaoDichQuy"]) : 0;
                            }
                        }
                    }

                    // 3. Đánh giá năng lực trả nợ
                    decimal netCashFlow = fa.ChenhLechThuChi;
                    if (netCashFlow >= soTienVay * 1.5m || (fa.SoDuHienTai >= soTienVay && netCashFlow >= 0))
                    {
                        fa.DanhGia = "Tài chính rất tốt (Đủ điều kiện giải ngân)";
                        fa.GhiChuChiTiet = $"Dòng tiền ròng trong quý đạt {netCashFlow:N0} đ, vượt xa giá trị khoản vay ({soTienVay:N0} đ). Khả năng thu hồi vốn rất cao.";
                    }
                    else if (netCashFlow > 0 || fa.SoDuHienTai >= soTienVay * 0.5m)
                    {
                        fa.DanhGia = "Đạt yêu cầu tiêu chuẩn";
                        fa.GhiChuChiTiet = $"Dòng tiền thặng dư quý là {netCashFlow:N0} đ, có lịch sử giao dịch ổn định ({fa.SoGiaoDichQuy} giao dịch).";
                    }
                    else
                    {
                        fa.DanhGia = "Cảnh báo rủi ro tài chính";
                        fa.GhiChuChiTiet = $"Tổng chi trong quý ({fa.TongChiQuy:N0} đ) vượt tổng thu ({fa.TongThuQuy:N0} đ) hoặc số dư hiện tại thấp. Cần cân nhắc kỹ trước khi phê duyệt.";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BorrowingService] GetFinancialAppraisalAsync error: {ex.Message}");
                fa.DanhGia = "Chưa có đủ dữ liệu lịch sử";
                fa.GhiChuChiTiet = "Không thể tính toán chi tiết: " + ex.Message;
            }

            return fa;
        }

        /// <summary>
        /// Admin xác nhận duyệt hồ sơ vay:
        /// - Chuyển trạng thái sang 'Đã duyệt'
        /// - GIẢI NGÂN TRỰC TIẾP: Cộng tiền vào tài khoản ngân hàng của khách
        /// - Ghi nhận lịch sử giao dịch vào dbo.GiaoDich (Thu)
        /// </summary>
        public async Task<(bool Success, string Message)> ApproveLoanAsync(string maKhoanVay, string adminUser)
        {
            if (string.IsNullOrWhiteSpace(maKhoanVay))
                return (false, "Mã khoản vay không hợp lệ.");

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var tran = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Kiểm tra trạng thái hiện tại của khoản vay
                            decimal soTienVay = 0m;
                            int thoiGianVay = 30;
                            decimal laiSuat = 1.55m;
                            string soTaiKhoan = "";
                            string currentStatus = "";

                            const string sqlCheck = @"
                                SELECT SoTaiKhoan, SoTienVay, ThoiGianVay, LaiSuat, TrangThai 
                                FROM dbo.KhoanVay WITH (UPDLOCK) 
                                WHERE MaKhoanVay = @MaKV";

                            using (var cmdCheck = new SqlCommand(sqlCheck, conn, tran))
                            {
                                cmdCheck.Parameters.AddWithValue("@MaKV", maKhoanVay);
                                using (var reader = await cmdCheck.ExecuteReaderAsync())
                                {
                                    if (!await reader.ReadAsync())
                                    {
                                        tran.Rollback();
                                        return (false, "Không tìm thấy hồ sơ khoản vay.");
                                    }

                                    currentStatus = reader["TrangThai"]?.ToString() ?? "";
                                    soTaiKhoan = reader["SoTaiKhoan"]?.ToString() ?? "";
                                    soTienVay = Convert.ToDecimal(reader["SoTienVay"]);
                                    thoiGianVay = Convert.ToInt32(reader["ThoiGianVay"]);
                                    laiSuat = Convert.ToDecimal(reader["LaiSuat"]);
                                }
                            }

                            if (currentStatus != "Chờ xét duyệt" && currentStatus != "Pending")
                            {
                                tran.Rollback();
                                return (false, $"Hồ sơ này đã ở trạng thái '{currentStatus}', không thể duyệt lại.");
                            }

                            // 2. Cập nhật trạng thái KhoanVay sang 'Đã duyệt'
                            const string sqlUpdateLoan = @"
                                UPDATE dbo.KhoanVay 
                                SET TrangThai = N'Đã duyệt', 
                                    NgayDuyet = GETDATE(), 
                                    NguoiDuyet = @Admin 
                                WHERE MaKhoanVay = @MaKV";

                            using (var cmdUpdate = new SqlCommand(sqlUpdateLoan, conn, tran))
                            {
                                cmdUpdate.Parameters.AddWithValue("@Admin", (object)adminUser ?? "Admin");
                                cmdUpdate.Parameters.AddWithValue("@MaKV", maKhoanVay);
                                await cmdUpdate.ExecuteNonQueryAsync();
                            }

                            // 3. GIẢI NGÂN: Cộng số tiền vay vào số dư tài khoản thụ hưởng của khách
                            const string sqlDisburse = @"
                                UPDATE dbo.TaiKhoan 
                                SET SoDuHienTai = SoDuHienTai + @SoTien 
                                WHERE SoTaiKhoan = @STK";

                            using (var cmdDisburse = new SqlCommand(sqlDisburse, conn, tran))
                            {
                                cmdDisburse.Parameters.AddWithValue("@SoTien", soTienVay);
                                cmdDisburse.Parameters.AddWithValue("@STK", soTaiKhoan);
                                await cmdDisburse.ExecuteNonQueryAsync();
                            }

                            // 4. Ghi nhận giao dịch giải ngân vào dbo.GiaoDich (MaLoaiGD = 1: Nạp tiền / Thu)
                            string maGD = "GD" + DateTime.Now.ToString("yyMMddHHmmss") + (new Random().Next(100, 999));
                            string desc = $"[Giải ngân khoản vay {maKhoanVay}] Kỳ hạn {thoiGianVay} ngày - Lãi suất {laiSuat:N2}%/ngày";

                            const string sqlTx = @"
                                INSERT INTO dbo.GiaoDich 
                                    (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, SoTienGoc, 
                                     MaLoaiTien, LoaiTien, NgayGiaoDich, NoiDung, TrangThai)
                                VALUES 
                                    (@MaGD, @STK, 1, @SoTien, @SoTien, 
                                     'VND', 'VND', GETDATE(), @NoiDung, 'Success')";

                            using (var cmdTx = new SqlCommand(sqlTx, conn, tran))
                            {
                                cmdTx.Parameters.AddWithValue("@MaGD", maGD);
                                cmdTx.Parameters.AddWithValue("@STK", soTaiKhoan);
                                cmdTx.Parameters.AddWithValue("@SoTien", soTienVay);
                                cmdTx.Parameters.AddWithValue("@NoiDung", desc);
                                await cmdTx.ExecuteNonQueryAsync();
                            }

                            tran.Commit();

                            // Thông báo toàn hệ thống cập nhật số dư & lịch sử giao dịch
                            TransactionService.NotifyTransactionChanged();

                            return (true, $"Đã phê duyệt thành công! Đã giải ngân {soTienVay:N0} đ trực tiếp vào tài khoản {soTaiKhoan}.");
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "Lỗi khi xét duyệt giải ngân: " + ex.Message);
            }
        }

        /// <summary>
        /// Admin từ chối hồ sơ vay (Không xác nhận).
        /// </summary>
        public async Task<(bool Success, string Message)> RejectLoanAsync(string maKhoanVay, string reason, string adminUser)
        {
            if (string.IsNullOrWhiteSpace(maKhoanVay))
                return (false, "Mã khoản vay không hợp lệ.");

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    const string sql = @"
                        UPDATE dbo.KhoanVay 
                        SET TrangThai = N'Từ chối', 
                            GhiChu = @Reason, 
                            NgayDuyet = GETDATE(), 
                            NguoiDuyet = @Admin 
                        WHERE MaKhoanVay = @MaKV AND TrangThai = N'Chờ xét duyệt'";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Reason", string.IsNullOrWhiteSpace(reason) ? "Không đáp ứng điều kiện tín dụng" : reason.Trim());
                        cmd.Parameters.AddWithValue("@Admin", (object)adminUser ?? "Admin");
                        cmd.Parameters.AddWithValue("@MaKV", maKhoanVay);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        if (rows > 0)
                        {
                            return (true, $"Đã từ chối hồ sơ khoản vay {maKhoanVay}.");
                        }
                        else
                        {
                            return (false, "Hồ sơ không tồn tại hoặc đã được xử lý trước đó.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "Lỗi khi từ chối hồ sơ: " + ex.Message);
            }
        }

        private BorrowingLoan ReadLoanFromReader(SqlDataReader reader)
        {
            return new BorrowingLoan
            {
                MaKhoanVay = reader["MaKhoanVay"]?.ToString() ?? "",
                MaKH = reader["MaKH"]?.ToString() ?? "",
                HoTen = reader["HoTen"]?.ToString() ?? "Chưa rõ",
                TenDangNhap = reader["TenDangNhap"]?.ToString() ?? "",
                SoTaiKhoan = reader["SoTaiKhoan"]?.ToString() ?? "",
                TenNganHang = reader["TenNganHang"]?.ToString() ?? "",
                SoTienVay = reader["SoTienVay"] != DBNull.Value ? Convert.ToDecimal(reader["SoTienVay"]) : 0m,
                ThoiGianVay = reader["ThoiGianVay"] != DBNull.Value ? Convert.ToInt32(reader["ThoiGianVay"]) : 30,
                LaiSuat = reader["LaiSuat"] != DBNull.Value ? Convert.ToDecimal(reader["LaiSuat"]) : 1.55m,
                TienLai = reader["TienLai"] != DBNull.Value ? Convert.ToDecimal(reader["TienLai"]) : 0m,
                TongPhaiTra = reader["TongPhaiTra"] != DBNull.Value ? Convert.ToDecimal(reader["TongPhaiTra"]) : 0m,
                NgayDangKy = reader["NgayDangKy"] != DBNull.Value ? Convert.ToDateTime(reader["NgayDangKy"]) : DateTime.Now,
                NgayDaoHan = reader["NgayDaoHan"] != DBNull.Value ? Convert.ToDateTime(reader["NgayDaoHan"]) : DateTime.Now,
                TrangThai = reader["TrangThai"]?.ToString() ?? "Chờ xét duyệt",
                GhiChu = reader["GhiChu"]?.ToString() ?? "",
                NgayDuyet = reader["NgayDuyet"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["NgayDuyet"]) : null,
                NguoiDuyet = reader["NguoiDuyet"]?.ToString() ?? ""
            };
        }
    }
}
