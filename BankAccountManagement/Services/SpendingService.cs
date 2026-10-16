using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Service quản lý chi tiêu (hạn mức ngày, tuần) và cung cấp dữ liệu trang chủ User.
    /// </summary>
    public class SpendingService
    {
        private string GetConnectionString()
        {
            var config = DatabaseConfigManager.Load();
            return DatabaseService.BuildConnectionString(config);
        }

        /// <summary>
        /// Khởi tạo và tự động đảm bảo bảng dbo.HanMucChiTieu tồn tại.
        /// </summary>
        public async Task EnsureTableExistsAsync()
        {
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    const string sql = @"
                        IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'HanMucChiTieu')
                        BEGIN
                            CREATE TABLE dbo.HanMucChiTieu (
                                MaKH VARCHAR(30) PRIMARY KEY,
                                HanMucNgay DECIMAL(18, 2) NOT NULL DEFAULT 2000000,
                                HanMucTuan DECIMAL(18, 2) NOT NULL DEFAULT 10000000,
                                CanhBaoVuotMuc BIT NOT NULL DEFAULT 1,
                                NgayCapNhat DATETIME NOT NULL DEFAULT GETDATE()
                            );
                        END";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SpendingService] EnsureTableExistsAsync error: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy thông tin hạn mức chi tiêu của khách hàng.
        /// </summary>
        public async Task<SpendingLimit> GetSpendingLimitAsync(string maKH)
        {
            await EnsureTableExistsAsync();

            var limit = new SpendingLimit { MaKH = maKH };
            if (string.IsNullOrWhiteSpace(maKH)) return limit;

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    const string sql = @"
                        SELECT MaKH, HanMucNgay, HanMucTuan, CanhBaoVuotMuc, NgayCapNhat
                        FROM dbo.HanMucChiTieu
                        WHERE MaKH = @MaKH";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH.Trim());
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                limit.HanMucNgay = Convert.ToDecimal(reader["HanMucNgay"]);
                                limit.HanMucTuan = Convert.ToDecimal(reader["HanMucTuan"]);
                                limit.CanhBaoVuotMuc = Convert.ToBoolean(reader["CanhBaoVuotMuc"]);
                                limit.NgayCapNhat = Convert.ToDateTime(reader["NgayCapNhat"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SpendingService] GetSpendingLimitAsync error: {ex.Message}");
            }

            return limit;
        }

        /// <summary>
        /// Lưu hoặc cập nhật hạn mức chi tiêu cho khách hàng.
        /// </summary>
        public async Task<(bool Success, string Message)> SaveSpendingLimitAsync(
            string maKH, decimal hanMucNgay, decimal hanMucTuan, bool canhBao)
        {
            if (string.IsNullOrWhiteSpace(maKH))
            {
                return (false, "Không xác định được mã khách hàng.");
            }

            if (hanMucNgay <= 0 || hanMucTuan <= 0)
            {
                return (false, "Hạn mức chi tiêu phải lớn hơn 0 đ.");
            }

            if (hanMucNgay > hanMucTuan)
            {
                return (false, "Hạn mức ngày không thể lớn hơn hạn mức tuần.");
            }

            await EnsureTableExistsAsync();

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    const string sql = @"
                        IF EXISTS (SELECT 1 FROM dbo.HanMucChiTieu WHERE MaKH = @MaKH)
                        BEGIN
                            UPDATE dbo.HanMucChiTieu
                            SET HanMucNgay = @HanMucNgay,
                                HanMucTuan = @HanMucTuan,
                                CanhBaoVuotMuc = @CanhBao,
                                NgayCapNhat = GETDATE()
                            WHERE MaKH = @MaKH;
                        END
                        ELSE
                        BEGIN
                            INSERT INTO dbo.HanMucChiTieu (MaKH, HanMucNgay, HanMucTuan, CanhBaoVuotMuc, NgayCapNhat)
                            VALUES (@MaKH, @HanMucNgay, @HanMucTuan, @CanhBao, GETDATE());
                        END";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH.Trim());
                        cmd.Parameters.AddWithValue("@HanMucNgay", hanMucNgay);
                        cmd.Parameters.AddWithValue("@HanMucTuan", hanMucTuan);
                        cmd.Parameters.AddWithValue("@CanhBao", canhBao);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return (true, "Đã lưu cài đặt hạn mức chi tiêu thành công!");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi khi lưu hạn mức: " + ex.Message);
            }
        }

        /// <summary>
        /// Thống kê chi tiêu thực tế của khách hàng trong ngày hôm nay và tuần này.
        /// </summary>
        public async Task<SpendingStats> GetSpendingStatsAsync(string maKH)
        {
            var limit = await GetSpendingLimitAsync(maKH);
            var stats = new SpendingStats
            {
                HanMucNgay = limit.HanMucNgay,
                HanMucTuan = limit.HanMucTuan,
                CanhBaoVuotMuc = limit.CanhBaoVuotMuc
            };

            if (string.IsNullOrWhiteSpace(maKH)) return stats;

            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);

            // Xác định đầu tuần (Thứ 2)
            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime startOfWeek = today.AddDays(-1 * diff);

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();

                    // 1. Tính tổng tiền đã chi hôm nay
                    const string sqlToday = @"
                        SELECT ISNULL(SUM(G.SoTien), 0)
                        FROM dbo.GiaoDich G
                        INNER JOIN dbo.TaiKhoan TK ON G.SoTaiKhoan = TK.SoTaiKhoan
                        LEFT JOIN dbo.LoaiGiaoDich L ON G.MaLoaiGD = L.MaLoaiGD
                        WHERE TK.MaKH = @MaKH
                          AND (L.PhanLoai = 'EXPENSE' OR G.MaLoaiGD = 2 
                               OR L.TenLoaiGD LIKE N'%Rút%' OR L.TenLoaiGD LIKE N'%Chuyển%' 
                               OR L.TenLoaiGD LIKE N'%Thanh toán%' OR L.TenLoaiGD LIKE N'%Mua%')
                          AND G.NgayGiaoDich >= @StartToday AND G.NgayGiaoDich < @EndToday
                          AND (G.TrangThai = 'Success' OR G.TrangThai = N'Hoàn tất' OR G.TrangThai = N'Thành công')";

                    using (var cmd = new SqlCommand(sqlToday, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH.Trim());
                        cmd.Parameters.AddWithValue("@StartToday", today);
                        cmd.Parameters.AddWithValue("@EndToday", tomorrow);

                        var res = await cmd.ExecuteScalarAsync();
                        stats.DaChiHomNay = res != null && res != DBNull.Value ? Convert.ToDecimal(res) : 0m;
                    }

                    // 2. Tính tổng tiền đã chi tuần này (từ Thứ 2 đến hết hôm nay)
                    const string sqlWeek = @"
                        SELECT ISNULL(SUM(G.SoTien), 0)
                        FROM dbo.GiaoDich G
                        INNER JOIN dbo.TaiKhoan TK ON G.SoTaiKhoan = TK.SoTaiKhoan
                        LEFT JOIN dbo.LoaiGiaoDich L ON G.MaLoaiGD = L.MaLoaiGD
                        WHERE TK.MaKH = @MaKH
                          AND (L.PhanLoai = 'EXPENSE' OR G.MaLoaiGD = 2 
                               OR L.TenLoaiGD LIKE N'%Rút%' OR L.TenLoaiGD LIKE N'%Chuyển%' 
                               OR L.TenLoaiGD LIKE N'%Thanh toán%' OR L.TenLoaiGD LIKE N'%Mua%')
                          AND G.NgayGiaoDich >= @StartWeek AND G.NgayGiaoDich < @EndToday
                          AND (G.TrangThai = 'Success' OR G.TrangThai = N'Hoàn tất' OR G.TrangThai = N'Thành công')";

                    using (var cmd = new SqlCommand(sqlWeek, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH.Trim());
                        cmd.Parameters.AddWithValue("@StartWeek", startOfWeek);
                        cmd.Parameters.AddWithValue("@EndToday", tomorrow);

                        var res = await cmd.ExecuteScalarAsync();
                        stats.DaChiTuanNay = res != null && res != DBNull.Value ? Convert.ToDecimal(res) : 0m;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SpendingService] GetSpendingStatsAsync error: {ex.Message}");
            }

            return stats;
        }

        /// <summary>
        /// Lấy khoản vay sắp tới đến hạn của khách hàng (nếu có).
        /// </summary>
        public async Task<BorrowingLoan> GetUpcomingLoanAsync(string maKH)
        {
            if (string.IsNullOrWhiteSpace(maKH)) return null;

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    const string sql = @"
                        SELECT TOP 1 
                            KV.MaKhoanVay, KV.MaKH, KV.SoTaiKhoan, KV.SoTienVay, KV.ThoiGianVay, 
                            KV.LaiSuat, KV.TienLai, KV.TongPhaiTra, KV.NgayDangKy, KV.NgayDaoHan, 
                            KV.TrangThai, KV.GhiChu,
                            ISNULL(KH.HoTen, ND.HoTen) AS HoTen,
                            ISNULL(ND.TenDangNhap, '') AS TenDangNhap
                        FROM dbo.KhoanVay KV
                        LEFT JOIN dbo.ThongTinKhachHang KH ON KV.MaKH = KH.MaKH
                        LEFT JOIN dbo.NguoiDung ND ON KV.MaKH = ND.MaKH
                        WHERE KV.MaKH = @MaKH AND (KV.TrangThai = N'Đã duyệt' OR KV.TrangThai = N'Chờ xét duyệt')
                        ORDER BY 
                            CASE WHEN KV.TrangThai = N'Đã duyệt' THEN 0 ELSE 1 END,
                            KV.NgayDaoHan ASC";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH.Trim());
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new BorrowingLoan
                                {
                                    MaKhoanVay = reader["MaKhoanVay"]?.ToString() ?? "",
                                    MaKH = reader["MaKH"]?.ToString() ?? "",
                                    SoTaiKhoan = reader["SoTaiKhoan"]?.ToString() ?? "",
                                    SoTienVay = Convert.ToDecimal(reader["SoTienVay"]),
                                    ThoiGianVay = Convert.ToInt32(reader["ThoiGianVay"]),
                                    LaiSuat = Convert.ToDecimal(reader["LaiSuat"]),
                                    TienLai = Convert.ToDecimal(reader["TienLai"]),
                                    TongPhaiTra = Convert.ToDecimal(reader["TongPhaiTra"]),
                                    NgayDangKy = Convert.ToDateTime(reader["NgayDangKy"]),
                                    NgayDaoHan = Convert.ToDateTime(reader["NgayDaoHan"]),
                                    TrangThai = reader["TrangThai"]?.ToString() ?? "",
                                    GhiChu = reader["GhiChu"]?.ToString() ?? "",
                                    HoTen = reader["HoTen"]?.ToString() ?? "",
                                    TenDangNhap = reader["TenDangNhap"]?.ToString() ?? ""
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SpendingService] GetUpcomingLoanAsync error: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Lấy toàn bộ thông tin tổng hợp cho trang chủ phía User.
        /// </summary>
        public async Task<UserHomeSummary> GetUserHomeSummaryAsync(string maKH, string tenDangNhap)
        {
            var summary = new UserHomeSummary
            {
                MaKH = maKH ?? "",
                TenDangNhap = tenDangNhap ?? "",
                HoTen = CurrentSession.HoTen ?? "Người dùng"
            };

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();

                    // 1. Lấy thông tin tài khoản ngân hàng và tổng số dư
                    const string sqlAccounts = @"
                        SELECT SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien, SoDuBanDau, SoDuHienTai, NgayMo, TrangThai
                        FROM dbo.TaiKhoan
                        WHERE MaKH = @MaKH AND (TrangThai = N'Đang hoạt động' OR TrangThai = 'Active' OR TrangThai IS NULL)
                        ORDER BY SoDuHienTai DESC";

                    using (var cmd = new SqlCommand(sqlAccounts, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH ?? "");
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var acc = new TaiKhoan
                                {
                                    SoTaiKhoan = reader["SoTaiKhoan"].ToString(),
                                    MaKH = reader["MaKH"].ToString(),
                                    SoDuHienTai = Convert.ToDecimal(reader["SoDuHienTai"]),
                                    TrangThai = reader["TrangThai"]?.ToString()
                                };
                                summary.DanhSachTaiKhoan.Add(acc);
                                summary.TongSoDu += acc.SoDuHienTai;
                            }
                        }
                    }

                    if (summary.DanhSachTaiKhoan.Count > 0)
                    {
                        summary.SoTaiKhoanChinh = summary.DanhSachTaiKhoan[0].SoTaiKhoan;
                    }

                    // 2. Tên người dùng chính xác từ ThongTinKhachHang nếu có
                    const string sqlName = @"
                        SELECT TOP 1 ISNULL(KH.HoTen, ND.HoTen) AS HoTen
                        FROM dbo.NguoiDung ND
                        LEFT JOIN dbo.ThongTinKhachHang KH ON ND.MaKH = KH.MaKH
                        WHERE ND.TenDangNhap = @User OR ND.MaKH = @MaKH";

                    using (var cmdName = new SqlCommand(sqlName, conn))
                    {
                        cmdName.Parameters.AddWithValue("@User", tenDangNhap ?? "");
                        cmdName.Parameters.AddWithValue("@MaKH", maKH ?? "");
                        var resName = await cmdName.ExecuteScalarAsync();
                        if (resName != null && resName != DBNull.Value && !string.IsNullOrWhiteSpace(resName.ToString()))
                        {
                            summary.HoTen = resName.ToString();
                        }
                    }
                }

                // 3. Phân hạng thành viên theo tổng số dư
                if (summary.TongSoDu >= 500000000m) summary.HangThanhVien = "Hạng Kim Cương";
                else if (summary.TongSoDu >= 100000000m) summary.HangThanhVien = "Hạng Titan";
                else if (summary.TongSoDu >= 20000000m) summary.HangThanhVien = "Hạng Vàng";
                else summary.HangThanhVien = "Hạng Tiêu Chuẩn";

                // 4. Lấy khoản vay sắp tới
                summary.KhoanVaySapToi = await GetUpcomingLoanAsync(maKH);

                // 5. Lấy thống kê chi tiêu
                summary.Spending = await GetSpendingStatsAsync(maKH);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SpendingService] GetUserHomeSummaryAsync error: {ex.Message}");
            }

            return summary;
        }
    }
}
