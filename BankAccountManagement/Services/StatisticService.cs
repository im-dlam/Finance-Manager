using BankAccountManagement.Models;
using BankAccountManagement.Models.Statistics;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BankAccountManagement.Services
{
    public class StatisticService
    {
        private readonly DatabaseConfig _databaseConfig;

        public StatisticService(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<StatisticSummary> GetSummaryAsync(
    DateTime fromDate,
    DateTime toDate)
        {
            const string sql = @"
SELECT
    (
        SELECT COUNT(*)
        FROM TaiKhoan
        WHERE TrangThai = N'Active'
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
    ) AS TotalBalance,

    (
        SELECT COUNT(*)
        FROM GiaoDich
        WHERE TrangThai = N'Success'
          AND NgayGiaoDich >= @FromDate
          AND NgayGiaoDich < DATEADD(DAY, 1, @ToDate)
    ) AS TotalTransactions,

    (
        SELECT COUNT(*)
        FROM KhachHang
        WHERE TrangThai = N'Active'
    ) AS TotalCustomers;
";

            using (SqlConnection connection =
                DatabaseService.CreateConnection(_databaseConfig))
            {
                await connection.OpenAsync();

                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue(
                        "@FromDate",
                        fromDate);

                    command.Parameters.AddWithValue(
                        "@ToDate",
                        toDate);

                    using (SqlDataReader reader =
                        await command.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync())
                        {
                            return new StatisticSummary();
                        }

                        return new StatisticSummary
                        {
                            TotalAccounts =
                                Convert.ToInt32(
                                    reader["TotalAccounts"]),

                            TotalBalance =
                                Convert.ToDecimal(
                                    reader["TotalBalance"]),

                            TotalTransactions =
                                Convert.ToInt32(
                                    reader["TotalTransactions"]),

                            TotalCustomers =
                                Convert.ToInt32(
                                    reader["TotalCustomers"])
                        };
                    }
                }
            }
        }

        public async Task<List<TransactionStatistic>>
            GetTransactionsByMonthAsync(
                DateTime fromDate,
                DateTime toDate)
        {
            const string sql = @"
SELECT
    YEAR(NgayGiaoDich) AS Nam,
    MONTH(NgayGiaoDich) AS Thang,
    COUNT(*) AS SoLuong
FROM GiaoDich
WHERE TrangThai = N'Success'
  AND NgayGiaoDich >= @FromDate
  AND NgayGiaoDich < DATEADD(DAY, 1, @ToDate)
GROUP BY
    YEAR(NgayGiaoDich),
    MONTH(NgayGiaoDich)
ORDER BY
    YEAR(NgayGiaoDich),
    MONTH(NgayGiaoDich);
";

            List<TransactionStatistic> result =
                new List<TransactionStatistic>();

            using (SqlConnection connection =
                DatabaseService.CreateConnection(_databaseConfig))
            {
                await connection.OpenAsync();

                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue(
                        "@FromDate",
                        fromDate);

                    command.Parameters.AddWithValue(
                        "@ToDate",
                        toDate);

                    using (SqlDataReader reader =
                        await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(
                                new TransactionStatistic
                                {
                                    Year =
                                        Convert.ToInt32(
                                            reader["Nam"]),

                                    Month =
                                        Convert.ToInt32(
                                            reader["Thang"]),

                                    TransactionCount =
                                        Convert.ToInt32(
                                            reader["SoLuong"])
                                });
                        }
                    }
                }
            }

            return result;
        }

        public async Task<List<AccountTypeStatistic>>
            GetAccountTypesAsync()
        {
            const string sql = @"
SELECT
    LT.TenLoaiTien,
    COUNT(TK.SoTaiKhoan) AS SoLuong
FROM LoaiTien LT
LEFT JOIN TaiKhoan TK
    ON LT.MaLoaiTien = TK.MaLoaiTien
   AND TK.TrangThai = N'Active'
GROUP BY
    LT.MaLoaiTien,
    LT.TenLoaiTien
ORDER BY
    SoLuong DESC;
";

            List<AccountTypeStatistic> result =
                new List<AccountTypeStatistic>();

            using (SqlConnection connection =
                DatabaseService.CreateConnection(_databaseConfig))
            {
                await connection.OpenAsync();

                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader =
                        await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(
                                new AccountTypeStatistic
                                {
                                    AccountType =
                                        reader["TenLoaiTien"]
                                            .ToString(),

                                    AccountCount =
                                        Convert.ToInt32(
                                            reader["SoLuong"])
                                });
                        }
                    }
                }
            }

            return result;
        }

        public async Task<List<TopAccountStatistic>>
            GetTopAccountsAsync(int limit = 10)
        {
            const string sql = @"
SELECT TOP (@Limit)
    ROW_NUMBER() OVER (
        ORDER BY
            TK.SoDuHienTai * LT.QuyDoi DESC
    ) AS STT,

    TK.SoTaiKhoan,

    KH.HoTen,

    TK.SoDuHienTai,

    TK.SoDuHienTai * LT.QuyDoi AS SoDuVND,

    LT.TenLoaiTien AS LoaiTien

FROM TaiKhoan TK

INNER JOIN LoaiTien LT
    ON TK.MaLoaiTien = LT.MaLoaiTien

LEFT JOIN KhachHang KH
    ON TK.MaKH = KH.MaKH

WHERE TK.TrangThai = N'Active'

ORDER BY
    TK.SoDuHienTai * LT.QuyDoi DESC;
";

            List<TopAccountStatistic> result =
                new List<TopAccountStatistic>();

            using (SqlConnection connection =
                DatabaseService.CreateConnection(_databaseConfig))
            {
                await connection.OpenAsync();

                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue(
                        "@Limit",
                        limit);

                    using (SqlDataReader reader =
                        await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(
                                new TopAccountStatistic
                                {
                                    Rank = Convert.ToInt32(reader["STT"]),

                                    AccountNumber =reader["SoTaiKhoan"].ToString(),

                                    AccountHolder =reader["HoTen"] == DBNull.Value? string.Empty: reader["HoTen"].ToString(),

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