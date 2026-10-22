using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Dịch vụ quản lý Hạn mức chi tiêu và Thống kê trang chủ người dùng (Spending &amp; Dashboard Service).
    /// Chịu trách nhiệm:
    /// - Quản lý cài đặt hạn mức chi tiêu cá nhân theo Ngày và theo Tuần (bảng dbo.HanMucChiTieu).
    /// - Tính toán tổng số tiền đã chi tiêu thực tế trong ngày và trong tuần từ lịch sử giao dịch.
    /// - Cung cấp dữ liệu cảnh báo vượt hạn mức chi tiêu trực quan.
    /// - Tổng hợp thông tin tổng thể trang chủ phía khách hàng (số dư, tài khoản chính, khoản vay sắp đáo hạn).
    /// </summary>
    public class SpendingService
    {
        /// <summary>
        /// Tạo chuỗi kết nối chuẩn tới cơ sở dữ liệu SQL Server.
        /// </summary>
        /// <returns>Chuỗi connection string hợp lệ.</returns>
        private string GetConnectionString()
        {
            var config = DatabaseConfigManager.Load();
            return DatabaseService.BuildConnectionString(config);
        }

        /// <summary>
        /// Đảm bảo bảng dbo.HanMucChiTieu luôn tồn tại trong CSDL.
        /// Nếu bảng chưa có, hệ thống sẽ tự động thực thi kịch bản CREATE TABLE.
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
        /// Lấy thông tin cấu hình hạn mức chi tiêu của một khách hàng.
        /// Nếu khách hàng chưa cài đặt, trả về đối tượng mặc định (2 triệu/ngày, 10 triệu/tuần).
        /// </summary>
        /// <param name="maKH">Mã khách hàng cần tra cứu.</param>
        /// <returns>Đối tượng <see cref="SpendingLimit"/>.</returns>
        public async Task<SpendingLimit> GetSpendingLimitAsync(string maKH)
        {
            // BƯỚC 1: Đảm bảo bảng lưu trữ tồn tại
            await EnsureTableExistsAsync();

            var limit = new SpendingLimit { MaKH = maKH };
            if (string.IsNullOrWhiteSpace(maKH)) return limit;

            try
            {
                // BƯỚC 2: Truy vấn dữ liệu hạn mức từ bảng dbo.HanMucChiTieu
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
        /// Lưu hoặc cập nhật hạn mức chi tiêu cho khách hàng (mẫu Upsert: IF EXISTS UPDATE ELSE INSERT).
        /// </summary>
        /// <param name="maKH">Mã khách hàng cần thiết lập.</param>
        /// <param name="hanMucNgay">Hạn mức chi tiêu trong 1 ngày (VNĐ).</param>
        /// <param name="hanMucTuan">Hạn mức chi tiêu trong 1 tuần (VNĐ).</param>
        /// <param name="canhBao">Cờ bật cảnh báo khi vượt hạn mức.</param>
        /// <returns>Tuple (Success: trạng thái, Message: thông báo).</returns>
        public async Task<(bool Success, string Message)> SaveSpendingLimitAsync(
            string maKH, decimal hanMucNgay, decimal hanMucTuan, bool canhBao)
        {
            // BƯỚC 1: Kiểm tra tính hợp lệ nghiệp vụ
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
                // BƯỚC 2: Thực thi câu lệnh Upsert vào bảng CSDL
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
        /// Thống kê số tiền chi tiêu thực tế của khách hàng trong ngày hôm nay và trong tuần hiện tại.
        /// Tổng hợp toàn bộ các giao dịch mang tính chất chi tiêu (Rút tiền, Chuyển khoản, Mua sắm hàng hóa).
        /// </summary>
        /// <param name="maKH">Mã khách hàng cần thống kê.</param>
        /// <returns>Đối tượng <see cref="SpendingStats"/> chứa số liệu chi tiêu và tỷ lệ phần trăm tiến độ.</returns>
        public async Task<SpendingStats> GetSpendingStatsAsync(string maKH)
        {
            // BƯỚC 1: Lấy thông số hạn mức đã cài đặt của khách hàng
            var limit = await GetSpendingLimitAsync(maKH);
            var stats = new SpendingStats
            {
                HanMucNgay = limit.HanMucNgay,
                HanMucTuan = limit.HanMucTuan,
                CanhBaoVuotMuc = limit.CanhBaoVuotMuc
            };

            if (string.IsNullOrWhiteSpace(maKH)) return stats;

            // BƯỚC 2: Xác định mốc thời gian ngày hôm nay và ngày mai
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);

            // BƯỚC 3: Xác định ngày đầu tuần (Thứ Hai) theo lịch dương
            int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
            DateTime startOfWeek = today.AddDays(-1 * diff);

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();

                    // BƯỚC 4: Tính tổng số tiền đã chi tiêu trong ngày hôm nay (00:00:00 -> 23:59:59)
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

                    // BƯỚC 5: Tính tổng số tiền đã chi tiêu trong tuần này (từ Thứ Hai đầu tuần tới hết hôm nay)
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
        /// Tìm khoản vay sắp tới hạn thanh toán gần nhất của khách hàng (nếu có hồ sơ Đã duyệt hoặc Chờ duyệt).
        /// </summary>
        /// <param name="maKH">Mã khách hàng cần kiểm tra nợ vay.</param>
        /// <returns>Đối tượng <see cref="BorrowingLoan"/> nếu có khoản vay; ngược lại là null.</returns>
        public async Task<BorrowingLoan> GetUpcomingLoanAsync(string maKH)
        {
            if (string.IsNullOrWhiteSpace(maKH)) return null;

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();

                    // BƯỚC 1: Lấy bản ghi khoản vay có ngày đáo hạn gần nhất, ưu tiên hồ sơ Đã duyệt lên trước
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
        /// Tổng hợp toàn bộ dữ liệu thông tin tài chính phục vụ hiển thị trang chủ người dùng (User Dashboard):
        /// - Danh sách tài khoản thanh toán và tổng số dư khả dụng.
        /// - Xác định số tài khoản chính và phân hạng thành viên (Standard, Silver, Gold, Diamond).
        /// - Khoản nợ vay sắp tới hạn thanh toán.
        /// - Tiến độ chi tiêu ngày và tuần so với hạn mức cài đặt.
        /// </summary>
        /// <param name="maKH">Mã khách hàng.</param>
        /// <param name="tenDangNhap">Tên tài khoản người dùng đăng nhập.</param>
        /// <returns>Đối tượng <see cref="UserHomeSummary"/> tổng hợp đầy đủ số liệu.</returns>
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

                    // BƯỚC 1: Lấy danh sách các tài khoản ngân hàng và tính tổng số dư khả dụng
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

                    // BƯỚC 2: Truy vấn họ tên chính xác của khách hàng từ ThongTinKhachHang hoặc NguoiDung
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

                // BƯỚC 3: Xác định phân hạng thành viên tự động dựa trên tổng số dư
                summary.HangThanhVien = UserTierTheme.DetermineTierByBalance(summary.TongSoDu);

                // BƯỚC 4: Lấy thông tin khoản vay sắp tới hạn
                summary.KhoanVaySapToi = await GetUpcomingLoanAsync(maKH);

                // BƯỚC 5: Lấy thống kê chi tiêu ngày và tuần
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
