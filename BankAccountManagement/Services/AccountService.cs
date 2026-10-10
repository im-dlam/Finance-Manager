using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BankAccountManagement.Services
{
    public class AccountService
    {
        private readonly string _connectionString;

        private const string SelectAccounts = @"
            SELECT SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien,
                   SoDuBanDau, SoDuHienTai, NgayMo, TrangThai
            FROM TaiKhoan";

        public AccountService()
        {
            DatabaseConfig config = DatabaseConfigManager.Load();
            _connectionString = DatabaseService.BuildConnectionString(config);
        }

        /// <summary>Lấy các tài khoản đang hoạt động.</summary>
        public List<TaiKhoan> GetActiveTaiKhoan()
        {
            return QueryAccounts(
                SelectAccounts + " WHERE TrangThai = 'Active' ORDER BY NgayMo DESC",
                null,
                "Lỗi lấy danh sách tài khoản");
        }

        /// <summary>Tìm tài khoản Active theo một phần số tài khoản.</summary>
        public List<TaiKhoan> SearchTaiKhoan(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return GetActiveTaiKhoan();

            return QueryAccounts(
                SelectAccounts + @"
                    WHERE TrangThai = 'Active'
                      AND SoTaiKhoan LIKE @Keyword
                    ORDER BY NgayMo DESC",
                parameters => parameters.AddWithValue("@Keyword", "%" + keyword.Trim() + "%"),
                "Lỗi tìm kiếm tài khoản");
        }

        /// <summary>Lấy tài khoản theo số tài khoản, trả về null nếu không có.</summary>
        public TaiKhoan GetBySoTaiKhoan(string soTaiKhoan)
        {
            string accountNumber = RequiredText(soTaiKhoan, "Số tài khoản", nameof(soTaiKhoan));
            List<TaiKhoan> accounts = QueryAccounts(
                SelectAccounts + " WHERE SoTaiKhoan = @SoTaiKhoan",
                parameters => parameters.AddWithValue("@SoTaiKhoan", accountNumber),
                "Lỗi lấy tài khoản");

            return accounts.Count > 0 ? accounts[0] : null;
        }

        /// <summary>Lấy mọi tài khoản của khách hàng, gồm cả tài khoản đã đóng.</summary>
        public List<TaiKhoan> GetByMaKH(string maKH)
        {
            string customerCode = RequiredText(maKH, "Mã khách hàng", nameof(maKH));
            return QueryAccounts(
                SelectAccounts + " WHERE MaKH = @MaKH ORDER BY NgayMo DESC",
                parameters => parameters.AddWithValue("@MaKH", customerCode),
                "Lỗi lấy tài khoản theo khách hàng");
        }

        /// <summary>
        /// Mở tài khoản Active; số dư hiện tại ban đầu bằng số dư ban đầu.
        /// </summary>
        public bool AddTaiKhoan(TaiKhoan tk)
        {
            ValidateAccount(tk);
            if (tk.SoDuBanDau < 0)
                throw new ArgumentException("Số dư ban đầu không được âm.", nameof(tk));

            const string sql = @"
                INSERT INTO TaiKhoan
                    (SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien,
                     SoDuBanDau, SoDuHienTai, NgayMo, TrangThai)
                VALUES
                    (@SoTaiKhoan, @MaKH, @MaNganHang, @MaLoaiTien,
                     @SoDuBanDau, @SoDuBanDau, @NgayMo, 'Active')";

            return ExecuteChange(sql, parameters =>
            {
                AddAccountParameters(parameters, tk);
                parameters.AddWithValue("@SoDuBanDau", tk.SoDuBanDau);
                parameters.AddWithValue("@NgayMo",
                    tk.NgayMo == DateTime.MinValue ? DateTime.Now : tk.NgayMo);
            }, "Lỗi thêm tài khoản");
        }

        /// <summary>Cập nhật thông tin tài khoản; không ghi đè các cột số dư.</summary>
        public bool UpdateTaiKhoan(TaiKhoan tk)
        {
            ValidateAccount(tk);
            string status = NormalizeStatus(tk.TrangThai);
            if (tk.NgayMo == DateTime.MinValue)
                throw new ArgumentException("Ngày mở tài khoản không hợp lệ.", nameof(tk));

            const string sql = @"
                UPDATE TaiKhoan
                SET MaKH = @MaKH,
                    MaNganHang = @MaNganHang,
                    MaLoaiTien = @MaLoaiTien,
                    NgayMo = @NgayMo,
                    TrangThai = @TrangThai
                WHERE SoTaiKhoan = @SoTaiKhoan";

            return ExecuteChange(sql, parameters =>
            {
                AddAccountParameters(parameters, tk);
                parameters.AddWithValue("@NgayMo", tk.NgayMo);
                parameters.AddWithValue("@TrangThai", status);
            }, "Lỗi cập nhật tài khoản");
        }

        /// <summary>Đóng tài khoản bằng cách đổi trạng thái, không xóa bản ghi.</summary>
        public bool CloseTaiKhoan(string soTaiKhoan)
        {
            string accountNumber = RequiredText(soTaiKhoan, "Số tài khoản", nameof(soTaiKhoan));
            const string sql = @"
                UPDATE TaiKhoan
                SET TrangThai = 'Closed'
                WHERE SoTaiKhoan = @SoTaiKhoan";

            return ExecuteChange(sql,
                parameters => parameters.AddWithValue("@SoTaiKhoan", accountNumber),
                "Lỗi đóng tài khoản");
        }

        private List<TaiKhoan> QueryAccounts(
            string sql,
            Action<SqlParameterCollection> configureParameters,
            string errorMessage)
        {
            try
            {
                var accounts = new List<TaiKhoan>();
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(sql, connection))
                {
                    if (configureParameters != null)
                        configureParameters(command.Parameters);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            accounts.Add(MapReader(reader));
                    }
                }

                return accounts;
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException(errorMessage + ": " + ex.Message, ex);
            }
        }

        private bool ExecuteChange(
            string sql,
            Action<SqlParameterCollection> configureParameters,
            string errorMessage)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(sql, connection))
                {
                    configureParameters(command.Parameters);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException(errorMessage + ": " + ex.Message, ex);
            }
        }

        private static void AddAccountParameters(SqlParameterCollection parameters, TaiKhoan tk)
        {
            parameters.AddWithValue("@SoTaiKhoan", tk.SoTaiKhoan.Trim());
            parameters.AddWithValue("@MaKH", tk.MaKH.Trim());
            parameters.AddWithValue("@MaNganHang", tk.MaNganHang.Trim());

            if (string.IsNullOrWhiteSpace(tk.MaLoaiTien))
                parameters.Add("@MaLoaiTien", SqlDbType.VarChar).Value = DBNull.Value;
            else
                parameters.AddWithValue("@MaLoaiTien", tk.MaLoaiTien.Trim());
        }

        private static void ValidateAccount(TaiKhoan tk)
        {
            if (tk == null)
                throw new ArgumentNullException(nameof(tk));

            RequiredText(tk.SoTaiKhoan, "Số tài khoản", nameof(tk.SoTaiKhoan));
            RequiredText(tk.MaKH, "Mã khách hàng", nameof(tk.MaKH));
            RequiredText(tk.MaNganHang, "Mã ngân hàng", nameof(tk.MaNganHang));
        }

        private static string RequiredText(string value, string label, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(label + " không được để trống.", parameterName);

            return value.Trim();
        }

        private static string NormalizeStatus(string value)
        {
            string status = RequiredText(value, "Trạng thái", nameof(value));
            if (string.Equals(status, "Active", StringComparison.OrdinalIgnoreCase))
                return "Active";
            if (string.Equals(status, "Closed", StringComparison.OrdinalIgnoreCase))
                return "Closed";

            throw new ArgumentException("Trạng thái phải là Active hoặc Closed.", nameof(value));
        }

        private static TaiKhoan MapReader(SqlDataReader reader)
        {
            return new TaiKhoan
            {
                SoTaiKhoan = reader["SoTaiKhoan"].ToString(),
                MaKH = reader["MaKH"].ToString(),
                MaNganHang = reader["MaNganHang"].ToString(),
                MaLoaiTien = reader["MaLoaiTien"] == DBNull.Value
                    ? null : reader["MaLoaiTien"].ToString(),
                SoDuBanDau = Convert.ToDecimal(reader["SoDuBanDau"]),
                SoDuHienTai = Convert.ToDecimal(reader["SoDuHienTai"]),
                NgayMo = Convert.ToDateTime(reader["NgayMo"]),
                TrangThai = reader["TrangThai"].ToString()
            };
        }
    }
}
