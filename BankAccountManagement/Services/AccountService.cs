using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Dịch vụ quản lý Tài khoản ngân hàng (Account Service).
    /// Chịu trách nhiệm thực hiện toàn bộ nghiệp vụ liên quan đến tài khoản thanh toán:
    /// - Mở tài khoản mới và tự động tạo giao dịch khởi tạo số dư ban đầu.
    /// - Tự động sinh số tài khoản tăng dần theo từng ngân hàng liên kết.
    /// - Tra cứu, tìm kiếm tài khoản theo số tài khoản hoặc mã khách hàng.
    /// - Cập nhật thông tin và thực hiện đóng tài khoản an toàn (Soft Close).
    /// - Áp dụng Parameterized Query để phòng chống triệt để tấn công SQL Injection.
    /// </summary>
    public class AccountService
    {
        /// <summary>
        /// Chuỗi kết nối tới cơ sở dữ liệu SQL Server.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Chuỗi truy vấn SQL cơ sở để đọc các trường thông tin của bảng TaiKhoan.
        /// </summary>
        private const string SelectAccounts = @"
            SELECT SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien,
                   SoDuBanDau, SoDuHienTai, NgayMo, TrangThai
            FROM TaiKhoan";

        /// <summary>
        /// Hàm khởi tạo của AccountService: Nạp thông số cấu hình và khởi tạo connection string.
        /// </summary>
        public AccountService()
        {
            DatabaseConfig config = DatabaseConfigManager.Load();
            _connectionString = DatabaseService.BuildConnectionString(config);
        }

        // ============================================================
        //  1. TRUY VẤN DANH SÁCH TÀI KHOẢN ĐANG HOẠT ĐỘNG
        // ============================================================

        /// <summary>
        /// Lấy danh sách tất cả các tài khoản thanh toán đang ở trạng thái hoạt động (TrangThai = 'Active').
        /// Sắp xếp giảm dần theo ngày mở tài khoản (tài khoản mới nhất lên đầu).
        /// </summary>
        /// <returns>Danh sách các đối tượng TaiKhoan.</returns>
        public List<TaiKhoan> GetActiveTaiKhoan()
        {
            return QueryAccounts(
                SelectAccounts + " WHERE TrangThai = 'Active' ORDER BY NgayMo DESC",
                null,
                "Lỗi lấy danh sách tài khoản");
        }

        // ============================================================
        //  2. TÌM KIẾM TÀI KHOẢN THEO TỪ KHÓA
        // ============================================================

        /// <summary>
        /// Tìm kiếm các tài khoản đang hoạt động có số tài khoản chứa từ khóa tìm kiếm.
        /// </summary>
        /// <param name="keyword">Số tài khoản hoặc một phần số tài khoản cần tìm.</param>
        /// <returns>Danh sách tài khoản thỏa mãn điều kiện.</returns>
        public List<TaiKhoan> SearchTaiKhoan(string keyword)
        {
            // BƯỚC 1: Nếu từ khóa rỗng thì trả về toàn bộ tài khoản Active
            if (string.IsNullOrWhiteSpace(keyword))
                return GetActiveTaiKhoan();

            // BƯỚC 2: Thực thi truy vấn có tham số @Keyword (LIKE %keyword%)
            return QueryAccounts(
                SelectAccounts + @"
                    WHERE TrangThai = 'Active'
                      AND SoTaiKhoan LIKE @Keyword
                    ORDER BY NgayMo DESC",
                parameters => parameters.AddWithValue("@Keyword", "%" + keyword.Trim() + "%"),
                "Lỗi tìm kiếm tài khoản");
        }

        // ============================================================
        //  3. LẤY CHI TIẾT TÀI KHOẢN THEO SỐ TÀI KHOẢN
        // ============================================================

        /// <summary>
        /// Tìm kiếm thông tin chi tiết một tài khoản ngân hàng dựa theo Số tài khoản (Khóa chính).
        /// </summary>
        /// <param name="soTaiKhoan">Số tài khoản cần tra cứu.</param>
        /// <returns>Đối tượng TaiKhoan nếu tìm thấy; ngược lại trả về null.</returns>
        public TaiKhoan GetBySoTaiKhoan(string soTaiKhoan)
        {
            // BƯỚC 1: Kiểm tra tính hợp lệ của tham số đầu vào
            string accountNumber = RequiredText(soTaiKhoan, "Số tài khoản", nameof(soTaiKhoan));

            // BƯỚC 2: Truy vấn tài khoản theo số tài khoản chính xác
            List<TaiKhoan> accounts = QueryAccounts(
                SelectAccounts + " WHERE SoTaiKhoan = @SoTaiKhoan",
                parameters => parameters.AddWithValue("@SoTaiKhoan", accountNumber),
                "Lỗi lấy tài khoản");

            return accounts.Count > 0 ? accounts[0] : null;
        }

        // ============================================================
        //  4. LẤY TẤT CẢ TÀI KHOẢN CỦA MỘT KHÁCH HÀNG
        // ============================================================

        /// <summary>
        /// Lấy tất cả các tài khoản thuộc sở hữu của một khách hàng (bao gồm cả tài khoản đang hoạt động và đã đóng).
        /// </summary>
        /// <param name="maKH">Mã khách hàng cần tra cứu (ví dụ: "KH001").</param>
        /// <returns>Danh sách tài khoản của khách hàng đó.</returns>
        public List<TaiKhoan> GetByMaKH(string maKH)
        {
            string customerCode = RequiredText(maKH, "Mã khách hàng", nameof(maKH));

            return QueryAccounts(
                SelectAccounts + " WHERE MaKH = @MaKH ORDER BY NgayMo DESC",
                parameters => parameters.AddWithValue("@MaKH", customerCode),
                "Lỗi lấy tài khoản theo khách hàng");
        }

        // ============================================================
        //  5. MỞ TÀI KHOẢN MỚI KÈM KHỞI TẠO SỐ DƯ
        // ============================================================

        /// <summary>
        /// Mở một tài khoản ngân hàng mới ở trạng thái 'Active'.
        /// Nếu khách hàng nộp số dư ban đầu (> 0), hệ thống sẽ tự động ghi nhận một giao dịch nạp tiền khởi tạo vào bảng GiaoDich.
        /// </summary>
        /// <param name="tk">Đối tượng TaiKhoan chứa thông tin tài khoản cần mở.</param>
        /// <returns>True nếu mở tài khoản thành công; ngược lại là False.</returns>
        /// <exception cref="ArgumentException">Ném ngoại lệ nếu số dư ban đầu mang giá trị âm.</exception>
        public bool AddTaiKhoan(TaiKhoan tk)
        {
            // BƯỚC 1: Kiểm tra tính toàn vẹn dữ liệu của tài khoản
            ValidateAccount(tk);

            if (tk.SoDuBanDau < 0)
                throw new ArgumentException("Số dư ban đầu không được âm.", nameof(tk));

            // BƯỚC 2: Câu lệnh SQL kép:
            // 1. INSERT tài khoản mới vào bảng TaiKhoan (SoDuHienTai = SoDuBanDau)
            // 2. Nếu SoDuBanDau > 0: Tự động INSERT bản ghi GiaoDich nạp tiền ban đầu với mã giao dịch sinh ngẫu nhiên
            const string sql = @"
                INSERT INTO TaiKhoan
                    (SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien,
                     SoDuBanDau, SoDuHienTai, NgayMo, TrangThai)
                VALUES
                    (@SoTaiKhoan, @MaKH, @MaNganHang, @MaLoaiTien,
                     @SoDuBanDau, @SoDuBanDau, @NgayMo, 'Active');

                IF @SoDuBanDau > 0
                BEGIN
                    DECLARE @MaGD VARCHAR(30) = 'GD' + CONVERT(VARCHAR(8), @NgayMo, 112) + RIGHT('000000' + CAST(ABS(CHECKSUM(NEWID())) % 1000000 AS VARCHAR(6)), 6);
                    INSERT INTO dbo.GiaoDich 
                        (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, SoTienGoc, 
                         MaLoaiTien, LoaiTien, NgayGiaoDich, NoiDung, TrangThai)
                    VALUES 
                        (@MaGD, @SoTaiKhoan, 1, @SoDuBanDau, @SoDuBanDau, 
                         ISNULL(@MaLoaiTien, 'VND'), ISNULL(@MaLoaiTien, 'VND'), @NgayMo, N'Ngân hàng khởi tạo tài khoản', 'Success');
                END;";

            // BƯỚC 3: Gán các tham số Parameter an toàn và thực thi
            return ExecuteChange(sql, parameters =>
            {
                AddAccountParameters(parameters, tk);
                parameters.AddWithValue("@SoDuBanDau", tk.SoDuBanDau);
                parameters.AddWithValue("@NgayMo", tk.NgayMo == DateTime.MinValue ? DateTime.Now : tk.NgayMo);
            }, "Lỗi thêm tài khoản");
        }

        // ============================================================
        //  6. TỰ ĐỘNG SINH SỐ TÀI KHOẢN TIẾP THEO
        // ============================================================

        /// <summary>
        /// Tự động sinh số tài khoản tiếp theo theo chuẩn 10 chữ số dựa trên mã ngân hàng.
        /// Thuật toán: Tìm số tài khoản lớn nhất (MAX) của ngân hàng đó rồi cộng thêm 1.
        /// </summary>
        /// <param name="maNganHang">Mã ngân hàng (ví dụ: "NH01", "NH02").</param>
        /// <returns>Chuỗi số tài khoản mới gồm 10 chữ số.</returns>
        public string GenerateNextAccountNumber(string maNganHang)
        {
            if (string.IsNullOrWhiteSpace(maNganHang))
                maNganHang = "NH01";

            // BƯỚC 1: Xây dựng truy vấn lấy MAX(SoTaiKhoan) ép kiểu BIGINT
            // Nếu ngân hàng chưa có số tài khoản nào, tính số khởi đầu dựa trên mã số ngân hàng
            const string sql = @"
                SELECT ISNULL(
                    MAX(TRY_CONVERT(BIGINT, SoTaiKhoan)),
                    CAST(
                        CASE 
                            WHEN TRY_CONVERT(INT, REPLACE(REPLACE(@MaNganHang, 'NH', ''), 'nh', '')) IS NOT NULL
                            THEN CAST(1000000000 + TRY_CONVERT(INT, REPLACE(REPLACE(@MaNganHang, 'NH', ''), 'nh', '')) * 10000000 AS BIGINT)
                            ELSE 1000001000
                        END AS BIGINT
                    )
                )
                FROM dbo.TaiKhoan WITH (NOLOCK)
                WHERE MaNganHang = @MaNganHang 
                  AND TRY_CONVERT(BIGINT, SoTaiKhoan) IS NOT NULL";

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@MaNganHang", maNganHang.Trim());
                    connection.Open();

                    // BƯỚC 2: ExecuteScalar thực thi truy vấn lấy giá trị MAX đơn lẻ
                    object result = command.ExecuteScalar();
                    long maxSoTaiKhoan = (result != null && result != DBNull.Value) ? Convert.ToInt64(result) : 1000001000L;

                    // BƯỚC 3: Cộng thêm 1 để ra số tài khoản tiếp theo
                    return (maxSoTaiKhoan + 1).ToString();
                }
            }
            catch
            {
                // Dự phòng trả về số mặc định nếu xảy ra ngoại lệ
                return "1000001001";
            }
        }

        // ============================================================
        //  7. CẬP NHẬT THÔNG TIN TÀI KHOẢN
        // ============================================================

        /// <summary>
        /// Cập nhật thông tin cấu hình tài khoản (Mã khách hàng, Ngân hàng, Loại tiền, Ngày mở, Trạng thái).
        /// Lưu ý bảo mật tài chính: Không ghi đè hoặc chỉnh sửa trực tiếp cột số dư (SoDuHienTai).
        /// </summary>
        /// <param name="tk">Đối tượng TaiKhoan chứa thông tin cập nhật.</param>
        /// <returns>True nếu cập nhật thành công; ngược lại là False.</returns>
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

        // ============================================================
        //  8. ĐÓNG TÀI KHOẢN AN TOÀN (SOFT CLOSE)
        // ============================================================

        /// <summary>
        /// Đóng tài khoản bằng cách chuyển TrangThai = 'Closed' thay vì xóa hoàn toàn bản ghi khỏi CSDL (Soft Delete).
        /// Giúp bảo toàn lịch sử giao dịch và đối soát tài chính của ngân hàng.
        /// </summary>
        /// <param name="soTaiKhoan">Số tài khoản cần đóng.</param>
        /// <returns>True nếu đóng tài khoản thành công; ngược lại là False.</returns>
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

        // ============================================================
        //  CÁC PHƯƠNG THỨC NỘI BỘ HỖ TRỢ TRUY VẤN (INTERNAL HELPERS)
        // ============================================================

        /// <summary>
        /// Phương thức dùng chung để thực thi truy vấn SELECT trả về danh sách tài khoản.
        /// </summary>
        /// <param name="sql">Câu lệnh SQL SELECT.</param>
        /// <param name="configureParameters">Hành động cấu hình tham số truy vấn.</param>
        /// <param name="errorMessage">Thông điệp lỗi mô tả ngữ cảnh khi xảy ra lỗi SQL.</param>
        /// <returns>Danh sách đối tượng TaiKhoan.</returns>
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

        /// <summary>
        /// Phương thức dùng chung để thực thi câu lệnh SQL INSERT / UPDATE / DELETE tác động dữ liệu.
        /// </summary>
        /// <param name="sql">Câu lệnh SQL thực thi.</param>
        /// <param name="configureParameters">Hành động gắn tham số Parameter.</param>
        /// <param name="errorMessage">Thông điệp mô tả lỗi ngữ cảnh.</param>
        /// <returns>True nếu có ít nhất 1 dòng bị ảnh hưởng; False nếu không có dòng nào.</returns>
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

                    // ExecuteNonQuery thực thi lệnh DML trả về số dòng bị tác động
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new InvalidOperationException(errorMessage + ": " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Thêm các tham số cơ bản của tài khoản vào tập hợp tham số của SqlCommand.
        /// </summary>
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

        /// <summary>
        /// Kiểm tra tính hợp lệ cơ bản của dữ liệu tài khoản (không null và các trường khóa không để trống).
        /// </summary>
        private static void ValidateAccount(TaiKhoan tk)
        {
            if (tk == null)
                throw new ArgumentNullException(nameof(tk));

            RequiredText(tk.SoTaiKhoan, "Số tài khoản", nameof(tk.SoTaiKhoan));
            RequiredText(tk.MaKH, "Mã khách hàng", nameof(tk.MaKH));
            RequiredText(tk.MaNganHang, "Mã ngân hàng", nameof(tk.MaNganHang));
        }

        /// <summary>
        /// Kiểm tra chuỗi bắt buộc không được null hoặc rỗng, trả về chuỗi đã cắt khoảng trắng thừa.
        /// </summary>
        private static string RequiredText(string value, string label, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(label + " không được để trống.", parameterName);

            return value.Trim();
        }

        /// <summary>
        /// Chuẩn hóa trạng thái tài khoản về chuỗi hợp lệ: "Active" hoặc "Closed".
        /// </summary>
        private static string NormalizeStatus(string value)
        {
            string status = RequiredText(value, "Trạng thái", nameof(value));

            if (string.Equals(status, "Active", StringComparison.OrdinalIgnoreCase))
                return "Active";

            if (string.Equals(status, "Closed", StringComparison.OrdinalIgnoreCase))
                return "Closed";

            throw new ArgumentException("Trạng thái phải là Active hoặc Closed.", nameof(value));
        }

        /// <summary>
        /// Ánh xạ một dòng dữ liệu từ SqlDataReader sang đối tượng thực thể <see cref="TaiKhoan"/>.
        /// </summary>
        private static TaiKhoan MapReader(SqlDataReader reader)
        {
            return new TaiKhoan
            {
                SoTaiKhoan = reader["SoTaiKhoan"].ToString(),
                MaKH = reader["MaKH"].ToString(),
                MaNganHang = reader["MaNganHang"].ToString(),
                MaLoaiTien = reader["MaLoaiTien"] == DBNull.Value ? null : reader["MaLoaiTien"].ToString(),
                SoDuBanDau = Convert.ToDecimal(reader["SoDuBanDau"]),
                SoDuHienTai = Convert.ToDecimal(reader["SoDuHienTai"]),
                NgayMo = Convert.ToDateTime(reader["NgayMo"]),
                TrangThai = reader["TrangThai"].ToString()
            };
        }
    }
}
