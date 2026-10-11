using BankAccountManagement.Models;
using BankAccountManagement.Models.Statistics;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Service thống kê.
    /// 
    /// Schema v2.0: Bảng khách hàng là ThongTinKhachHang (không phải KhachHang).
    /// 
    /// Phân quyền:
    /// - Admin:  thống kê toàn hệ thống.
    /// - Member: chỉ thống kê trên tài khoản/giao dịch của chính mình.
    /// </summary>
    public class StatisticService
    {
        private readonly DatabaseConfig _databaseConfig;

        public StatisticService(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        // ============================================================
        //  HELPER — phân quyền
        // ============================================================
        private static bool IsAdmin => CurrentSession.IsAdmin;

        private static string MaKH => CurrentSession.MaKH ?? "";

        // ============================================================
        //  1. TỔNG QUAN
        // ============================================================
        public async Task<StatisticSummary> GetSummaryAsync(
            DateTime fromDate,
            DateTime toDate)
        {
            // ⭐ Đổi KhachHang → ThongTinKhachHang
            // ⭐ Thêm filter theo user nếu là Member
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
                                    WHEN LT.TenLoaiTien = N'VND'
                                        THEN TK.SoDuHienTai
                                    ELSE
                                        TK.SoDuHienTai * LT.QuyDoi
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
                        WHERE GD.TrangThai = N'Success'
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
        //  2. GIAO DỊCH THEO THÁNG
        // ============================================================
        public async Task<List<TransactionStatistic>> GetTransactionsByMonthAsync(
            DateTime fromDate,
            DateTime toDate)
        {
            string sql = @"
                SELECT
                    YEAR(GD.NgayGiaoDich)  AS Nam,
                    MONTH(GD.NgayGiaoDich) AS Thang,
                    COUNT(*)               AS SoLuong
                FROM GiaoDich GD
                INNER JOIN TaiKhoan TK ON TK.SoTaiKhoan = GD.SoTaiKhoan
                WHERE GD.TrangThai = N'Success'
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
        //  3. TÀI KHOẢN THEO LOẠI TIỀN
        // ============================================================
        public async Task<List<AccountTypeStatistic>> GetAccountTypesAsync()
        {
            string sql = @"
                SELECT
                    LT.TenLoaiTien,
                    COUNT(TK.SoTaiKhoan) AS SoLuong
                FROM LoaiTien LT
                LEFT JOIN TaiKhoan TK
                    ON LT.MaLoaiTien = TK.MaLoaiTien
                   AND TK.TrangThai = N'Active'
                   AND (@IsAdmin = 1 OR TK.MaKH = @MaKH)
                GROUP BY
                    LT.MaLoaiTien,
                    LT.TenLoaiTien
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
        //  4. TOP TÀI KHOẢN SỐ DƯ CAO NHẤT
        //  ⭐ Đổi KhachHang → ThongTinKhachHang
        // ============================================================
        public async Task<List<TopAccountStatistic>> GetTopAccountsAsync(int limit = 10)
        {
            // ⭐ Đã đổi LEFT JOIN KhachHang KH → LEFT JOIN ThongTinKhachHang KH
            string sql = @"
                SELECT TOP (@Limit)
                    ROW_NUMBER() OVER (
                        ORDER BY TK.SoDuHienTai * LT.QuyDoi DESC
                    ) AS STT,

                    TK.SoTaiKhoan,

                    KH.HoTen,

                    TK.SoDuHienTai,

                    TK.SoDuHienTai * LT.QuyDoi AS SoDuVND,

                    LT.TenLoaiTien AS LoaiTien

                FROM TaiKhoan TK

                INNER JOIN LoaiTien LT
                    ON TK.MaLoaiTien = LT.MaLoaiTien

                LEFT JOIN ThongTinKhachHang KH          -- ⭐ ĐỔI TÊN BẢNG
                    ON TK.MaKH = KH.MaKH

                WHERE TK.TrangThai = N'Active'
                  AND (@IsAdmin = 1 OR TK.MaKH = @MaKH)

                ORDER BY TK.SoDuHienTai * LT.QuyDoi DESC;
            ";

            var result = new List<TopAccountStatistic>();

            using (SqlConnection connection = DatabaseService.CreateConnection(_databaseConfig))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Limit", limit);
                    command.Parameters.AddWithValue("@IsAdmin", IsAdmin ? 1 : 0);
                    command.Parameters.AddWithValue("@MaKH", MaKH);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new TopAccountStatistic
                            {
                                Rank = Convert.ToInt32(reader["STT"]),

                                AccountNumber = reader["SoTaiKhoan"].ToString(),

                                AccountHolder = reader["HoTen"] == DBNull.Value
                                    ? string.Empty
                                    : reader["HoTen"].ToString(),

                                Balance = Convert.ToDecimal(reader["SoDuHienTai"]),

                                AccountCurrency = reader["LoaiTien"].ToString()
                            });
                        }
                    }
                }
            }

            return result;
        }
    }
}