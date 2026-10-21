using BankAccountManagement.Models;
using BankAccountManagement.Models.Statistics;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Dịch vụ Thống kê và Báo cáo tài chính (Statistic &amp; BI Reporting Service).
    /// Hỗ trợ tổng hợp số liệu cho biểu đồ và bảng báo cáo:
    /// - Thống kê tổng quan: Tổng số tài khoản, tổng số dư quy đổi VNĐ, tổng số giao dịch, tổng khách hàng.
    /// - Thống kê biến động giao dịch theo từng tháng trong năm.
    /// - Thống kê cơ cấu tài khoản theo loại tiền tệ (VND, USD,...).
    /// - Xếp hạng Top tài khoản có số dư lớn nhất (Admin) hoặc Bảng cân đối thu chi theo ngân hàng (Member).
    /// - Cơ chế phân quyền tự động: Admin xem toàn hệ thống; Member chỉ xem dữ liệu của chính mình.
    /// </summary>
    public class StatisticService
    {
        /// <summary>
        /// Cấu hình kết nối cơ sở dữ liệu.
        /// </summary>
        private readonly DatabaseConfig _databaseConfig;

        /// <summary>
        /// Khởi tạo dịch vụ StatisticService với cấu hình cơ sở dữ liệu truyền vào.
        /// </summary>
        /// <param name="databaseConfig">Đối tượng cấu hình DatabaseConfig.</param>
        public StatisticService(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        // ============================================================
        //  CÁC THUỘC TÍNH TIỆN ÍCH PHÂN QUYỀN (ROLE GUARDS)
        // ============================================================

        /// <summary>Kiểm tra xem phiên làm việc hiện tại có quyền Quản trị viên hay không.</summary>
        private static bool IsAdmin => CurrentSession.IsAdmin;

        /// <summary>Mã khách hàng của người dùng hiện tại (rỗng nếu là Admin hoặc chưa liên kết).</summary>
        private static string MaKH => CurrentSession.MaKH ?? "";

        // ============================================================
        //  1. THỐNG KÊ TỔNG QUAN HỆ THỐNG (SUMMARY METRICS)
        // ============================================================

        /// <summary>
        /// Lấy số liệu tổng hợp 4 chỉ số cốt lõi trong khoảng thời gian từ ngày [fromDate] đến ngày [toDate].
        /// Tự động quy đổi các loại ngoại tệ sang VNĐ theo tỷ giá chuẩn để tính tổng số dư chính xác.
        /// </summary>
        /// <param name="fromDate">Ngày bắt đầu thống kê.</param>
        /// <param name="toDate">Ngày kết thúc thống kê.</param>
        /// <returns>Đối tượng <see cref="StatisticSummary"/> chứa 4 chỉ số tổng quát.</returns>
        public async Task<StatisticSummary> GetSummaryAsync(DateTime fromDate, DateTime toDate)
        {
            // BƯỚC 1: Xây dựng truy vấn SQL tổng hợp 4 chỉ số đồng thời bằng Subquery
            // Áp dụng điều kiện (@IsAdmin = 1 OR MaKH = @MaKH) để tự động cô lập dữ liệu người dùng
            string sql = @"
                SELECT
                    (
                        SELECT COUNT(*)
                        FROM TaiKhoan
                        WHERE TrangThai = N'Active'
                          AND (@IsAdmin = 1 OR MaKH = @MaKH)
                    ) AS TotalAccounts,

                    (
                        SELECT ISNULL(
                            SUM(
                                CASE
                                    WHEN LT.MaLoaiTien = 'VND' OR LT.TenLoaiTien = N'VND'
                                        THEN TK.SoDuHienTai
                                    WHEN LT.MaLoaiTien = 'USD' AND (LT.QuyDoi IS NULL OR LT.QuyDoi <= 1)
                                        THEN TK.SoDuHienTai * 25400
                                    ELSE
                                        TK.SoDuHienTai * ISNULL(LT.QuyDoi, 1.0)
                                END
                            ),
                            0
                        )
                        FROM TaiKhoan TK
                        INNER JOIN LoaiTien LT
                            ON TK.MaLoaiTien = LT.MaLoaiTien
                        WHERE TK.TrangThai = N'Active'
                          AND (@IsAdmin = 1 OR TK.MaKH = @MaKH)
                    ) AS TotalBalance,

                    (
                        SELECT COUNT(*)
                        FROM GiaoDich GD
                        INNER JOIN TaiKhoan TK2 ON TK2.SoTaiKhoan = GD.SoTaiKhoan
                        WHERE (GD.TrangThai = 'Success' OR GD.TrangThai = N'Hoàn tất' OR GD.TrangThai = N'Thành công')
                          AND GD.NgayGiaoDich >= @FromDate
                          AND GD.NgayGiaoDich < DATEADD(DAY, 1, @ToDate)
                          AND (@IsAdmin = 1 OR TK2.MaKH = @MaKH)
                    ) AS TotalTransactions,

                    (
                        SELECT COUNT(*)
                        FROM ThongTinKhachHang
                        WHERE TrangThai = N'Active'
                          AND (@IsAdmin = 1 OR MaKH = @MaKH)
                    ) AS TotalCustomers;
            ";

            // BƯỚC 2: Mở kết nối và truyền các tham số truy vấn an toàn
            using (SqlConnection connection = DatabaseService.CreateConnection(_databaseConfig))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@ToDate", toDate);
                    command.Parameters.AddWithValue("@IsAdmin", IsAdmin ? 1 : 0);
                    command.Parameters.AddWithValue("@MaKH", MaKH);

                    // BƯỚC 3: Đọc kết quả qua SqlDataReader và đóng gói vào Model DTO
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync())
                        {
                            return new StatisticSummary();
                        }

                        return new StatisticSummary
                        {
                            TotalAccounts = Convert.ToInt32(reader["TotalAccounts"]),
                            TotalBalance = Convert.ToDecimal(reader["TotalBalance"]),
                            TotalTransactions = Convert.ToInt32(reader["TotalTransactions"]),
                            TotalCustomers = Convert.ToInt32(reader["TotalCustomers"])
                        };
                    }
                }
            }
        }

        // ============================================================
        //  2. THỐNG KÊ BIẾN ĐỘNG GIAO DỊCH THEO THÁNG
        // ============================================================

        /// <summary>
        /// Thống kê số lượng giao dịch thành công nhóm theo từng tháng trong khoảng thời gian được chọn.
        /// Thường dùng làm nguồn dữ liệu để vẽ biểu đồ đường (Line Chart) xu hướng phát triển.
        /// </summary>
        /// <param name="fromDate">Ngày bắt đầu chu kỳ thống kê.</param>
        /// <param name="toDate">Ngày kết thúc chu kỳ thống kê.</param>
        /// <returns>Danh sách các đối tượng <see cref="TransactionStatistic"/> theo từng tháng.</returns>
        public async Task<List<TransactionStatistic>> GetTransactionsByMonthAsync(DateTime fromDate, DateTime toDate)
        {
            string sql = @"
                SELECT
                    YEAR(GD.NgayGiaoDich)  AS Nam,
                    MONTH(GD.NgayGiaoDich) AS Thang,
                    COUNT(*)               AS SoLuong
                FROM GiaoDich GD
                INNER JOIN TaiKhoan TK ON TK.SoTaiKhoan = GD.SoTaiKhoan
                WHERE (GD.TrangThai = 'Success' OR GD.TrangThai = N'Hoàn tất' OR GD.TrangThai = N'Thành công')
                  AND GD.NgayGiaoDich >= @FromDate
                  AND GD.NgayGiaoDich < DATEADD(DAY, 1, @ToDate)
                  AND (@IsAdmin = 1 OR TK.MaKH = @MaKH)
                GROUP BY
                    YEAR(GD.NgayGiaoDich),
                    MONTH(GD.NgayGiaoDich)
                ORDER BY
                    YEAR(GD.NgayGiaoDich),
                    MONTH(GD.NgayGiaoDich);
            ";

            var result = new List<TransactionStatistic>();

            using (SqlConnection connection = DatabaseService.CreateConnection(_databaseConfig))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@FromDate", fromDate);
                    command.Parameters.AddWithValue("@ToDate", toDate);
                    command.Parameters.AddWithValue("@IsAdmin", IsAdmin ? 1 : 0);
                    command.Parameters.AddWithValue("@MaKH", MaKH);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new TransactionStatistic
                            {
                                Year = Convert.ToInt32(reader["Nam"]),
                                Month = Convert.ToInt32(reader["Thang"]),
                                TransactionCount = Convert.ToInt32(reader["SoLuong"])
                            });
                        }
                    }
                }
            }

            return result;
        }

        // ============================================================
        //  3. THỐNG KÊ CƠ CẤU TÀI KHOẢN THEO LOẠI TIỀN TỆ
        // ============================================================

        /// <summary>
        /// Thống kê số lượng tài khoản theo từng đơn vị loại tiền tệ (VND, USD, EUR,...).
        /// Thường dùng làm nguồn dữ liệu để hiển thị biểu đồ tròn (Pie Chart) phân bổ tài khoản.
        /// </summary>
        /// <returns>Danh sách các đối tượng <see cref="AccountTypeStatistic"/>.</returns>
        public async Task<List<AccountTypeStatistic>> GetAccountTypesAsync()
        {
            string sql = @"
                SELECT
                    LT.TenLoaiTien,
                    COUNT(TK.SoTaiKhoan) AS SoLuong
                FROM LoaiTien LT
                INNER JOIN TaiKhoan TK
                    ON LT.MaLoaiTien = TK.MaLoaiTien
                WHERE TK.TrangThai = N'Active'
                  AND (@IsAdmin = 1 OR TK.MaKH = @MaKH)
                GROUP BY
                    LT.MaLoaiTien,
                    LT.TenLoaiTien
                HAVING COUNT(TK.SoTaiKhoan) > 0
                ORDER BY
                    SoLuong DESC;
            ";

            var result = new List<AccountTypeStatistic>();

            using (SqlConnection connection = DatabaseService.CreateConnection(_databaseConfig))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IsAdmin", IsAdmin ? 1 : 0);
                    command.Parameters.AddWithValue("@MaKH", MaKH);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new AccountTypeStatistic
                            {
                                AccountType = reader["TenLoaiTien"].ToString(),
                                AccountCount = Convert.ToInt32(reader["SoLuong"])
                            });
                        }
                    }
                }
            }

            return result;
        }

        // ============================================================
        //  4. TOP TÀI KHOẢN SỐ DƯ CAO NHẤT (ADMIN) HOẶC SỐ DƯ USER
        // ============================================================

        /// <summary>
        /// Lấy danh sách Top tài khoản có số dư lớn nhất:
        /// - Với Quản trị viên (Admin): Xếp hạng Top N tài khoản có số dư quy đổi lớn nhất trên toàn ngân hàng.
        /// - Với Khách hàng (Member): Hiển thị bảng cân đối dòng tiền và số dư tại các ngân hàng mà khách hàng mở tài khoản.
        /// </summary>
        /// <param name="limit">Số lượng bản ghi tối đa cần lấy (mặc định là 10).</param>
        /// <returns>Danh sách đối tượng <see cref="TopAccountStatistic"/>.</returns>
        public async Task<List<TopAccountStatistic>> GetTopAccountsAsync(int limit = 10)
        {
            // BƯỚC 1: Nếu là Member thì chuyển hướng gọi hàm thống kê số dư cá nhân theo từng ngân hàng
            if (!IsAdmin)
            {
                return await GetUserBankBalancesAsync(MaKH);
            }

            // BƯỚC 2: Với Admin: Truy vấn Top N tài khoản có số dư lớn nhất toàn hệ thống
            string sql = @"
                SELECT TOP (@Limit)
                    ROW_NUMBER() OVER (
                        ORDER BY TK.SoDuHienTai * ISNULL(LT.QuyDoi, 1.0) DESC
                    ) AS STT,

                    TK.SoTaiKhoan,
                    KH.HoTen,
                    TK.SoDuHienTai,
                    TK.SoDuHienTai * ISNULL(LT.QuyDoi, 1.0) AS SoDuVND,
                    LT.TenLoaiTien AS LoaiTien

                FROM TaiKhoan TK
                INNER JOIN LoaiTien LT
                    ON TK.MaLoaiTien = LT.MaLoaiTien
                LEFT JOIN ThongTinKhachHang KH
                    ON TK.MaKH = KH.MaKH
                WHERE TK.TrangThai = N'Active'
                ORDER BY TK.SoDuHienTai * ISNULL(LT.QuyDoi, 1.0) DESC;
            ";

            var result = new List<TopAccountStatistic>();

            using (SqlConnection connection = DatabaseService.CreateConnection(_databaseConfig))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Limit", limit);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new TopAccountStatistic
                            {
                                Rank = Convert.ToInt32(reader["STT"]),
                                AccountNumber = reader["SoTaiKhoan"].ToString(),
                                AccountHolder = reader["HoTen"] == DBNull.Value ? string.Empty : reader["HoTen"].ToString(),
                                Balance = Convert.ToDecimal(reader["SoDuHienTai"]),
                                AccountCurrency = reader["LoaiTien"].ToString(),
                                BalanceVND = Convert.ToDecimal(reader["SoDuVND"])
                            });
                        }
                    }
                }
            }

            return result;
        }

        // ============================================================
        //  5. CÂN ĐỐI DÒNG TIỀN VÀ SỐ DƯ CÁC NGÂN HÀNG CỦA USER
        // ============================================================

        /// <summary>
        /// Tính toán dòng tiền Thu - Chi và số dư thực tế của từng tài khoản ngân hàng của một khách hàng cụ thể.
        /// </summary>
        /// <param name="maKH">Mã khách hàng cần tra cứu.</param>
        /// <returns>Danh sách <see cref="TopAccountStatistic"/> thể hiện từng tài khoản ngân hàng.</returns>
        public async Task<List<TopAccountStatistic>> GetUserBankBalancesAsync(string maKH)
        {
            string sql = @"
                SELECT 
                    TK.SoTaiKhoan,
                    NH.TenNganHang,
                    ISNULL(LT.TenLoaiTien, N'VND') AS LoaiTien,
                    ISNULL(SUM(CASE WHEN L.PhanLoai = 'INCOME' THEN G.SoTien ELSE 0 END), 0) AS TongThu,
                    ISNULL(SUM(CASE WHEN L.PhanLoai = 'EXPENSE' THEN G.SoTien ELSE 0 END), 0) AS TongChi,
                    ISNULL(SUM(CASE WHEN L.PhanLoai = 'INCOME' THEN G.SoTien WHEN L.PhanLoai = 'EXPENSE' THEN -G.SoTien ELSE 0 END), 0) AS SoDuThuChi
                FROM dbo.TaiKhoan TK
                INNER JOIN dbo.NganHang NH ON TK.MaNganHang = NH.MaNganHang
                LEFT JOIN dbo.LoaiTien LT ON TK.MaLoaiTien = LT.MaLoaiTien
                LEFT JOIN dbo.GiaoDich G ON TK.SoTaiKhoan = G.SoTaiKhoan AND (G.TrangThai = 'Success' OR G.TrangThai = N'Hoàn tất' OR G.TrangThai = N'Thành công')
                LEFT JOIN dbo.LoaiGiaoDich L ON G.MaLoaiGD = L.MaLoaiGD
                WHERE TK.MaKH = @MaKH AND TK.TrangThai = 'Active'
                GROUP BY TK.SoTaiKhoan, NH.TenNganHang, LT.TenLoaiTien
                ORDER BY NH.TenNganHang;
            ";

            var result = new List<TopAccountStatistic>();

            using (SqlConnection connection = DatabaseService.CreateConnection(_databaseConfig))
            {
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@MaKH", maKH ?? "");

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        int stt = 1;
                        while (await reader.ReadAsync())
                        {
                            decimal soDuThuChi = Convert.ToDecimal(reader["SoDuThuChi"]);
                            result.Add(new TopAccountStatistic
                            {
                                Rank = stt++,
                                AccountNumber = reader["SoTaiKhoan"].ToString(),
                                AccountHolder = reader["TenNganHang"].ToString(),
                                Balance = soDuThuChi,
                                AccountCurrency = reader["LoaiTien"].ToString(),
                                BalanceVND = soDuThuChi
                            });
                        }
                    }
                }
            }

            return result;
        }
    }
}
