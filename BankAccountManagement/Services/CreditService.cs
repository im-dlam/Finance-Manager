using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Dịch vụ quản lý Thẻ ngân hàng điện tử (Digital Bank Card / Debit Card Service).
    /// Chịu trách nhiệm:
    /// - Quản lý thông tin thẻ ghi nợ gắn liền với tài khoản thanh toán của người dùng.
    /// - Tự động định dạng số thẻ 16 số, tạo số thẻ che bảo mật (Masked Card Number: 4213 **** **** 9876).
    /// - Tính toán ngày hết hạn (5 năm kể từ ngày phát hành) và mã bảo mật CVV.
    /// - Thực hiện khóa thẻ / mở khóa thẻ an toàn (đồng bộ trực tiếp vào trạng thái bảng TaiKhoan).
    /// </summary>
    public class CreditService
    {
        /// <summary>
        /// Cấu hình kết nối cơ sở dữ liệu.
        /// </summary>
        private readonly DatabaseConfig _config;

        /// <summary>
        /// Khởi tạo một đối tượng CreditService mới với cấu hình cơ sở dữ liệu đọc từ database.ini.
        /// </summary>
        public CreditService()
        {
            _config = DatabaseConfigManager.Load();
        }

        /// <summary>
        /// Khởi tạo một đối tượng CreditService với cấu hình cơ sở dữ liệu tùy chỉnh truyền vào.
        /// </summary>
        /// <param name="config">Cấu hình kết nối cơ sở dữ liệu.</param>
        public CreditService(DatabaseConfig config)
        {
            _config = config ?? DatabaseConfigManager.Load();
        }

        /// <summary>
        /// Tạo chuỗi kết nối chuẩn tới cơ sở dữ liệu SQL Server.
        /// </summary>
        /// <returns>Chuỗi kết nối hợp lệ.</returns>
        private string GetConnectionString()
        {
            return DatabaseService.BuildConnectionString(_config);
        }

        /// <summary>
        /// Lấy danh sách thẻ ghi nợ của người dùng hiện tại hoặc tất cả tài khoản (nếu là Admin).
        /// Mặc định phát hành và hiển thị Thẻ ghi nợ theo yêu cầu hệ thống.
        /// </summary>
        /// <param name="maKH">Mã khách hàng cần lấy dữ liệu (nếu null sẽ lấy theo CurrentSession).</param>
        /// <param name="isAdmin">Cờ xác định quyền Admin.</param>
        /// <returns>Danh sách các đối tượng <see cref="CreditCardInfo"/>.</returns>
        public async Task<List<CreditCardInfo>> GetCardsAsync(string maKH = null, bool isAdmin = false)
        {
            var cards = new List<CreditCardInfo>();

            // BƯỚC 1: Xác định mã khách hàng mục tiêu cần lấy thẻ
            string targetMaKH = maKH;
            if (string.IsNullOrWhiteSpace(targetMaKH) && !isAdmin)
            {
                targetMaKH = CurrentSession.MaKH;
            }

            string sessionName = !string.IsNullOrWhiteSpace(CurrentSession.HoTen)
                ? CurrentSession.HoTen.ToUpper()
                : "PHONG HOÁ";

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();

                    // BƯỚC 2: Tự động khởi tạo tài khoản nếu khách hàng hợp lệ nhưng chưa có tài khoản trong DB
                    if (!isAdmin && !string.IsNullOrWhiteSpace(targetMaKH))
                    {
                        const string ensureAccountSql = @"
                            IF NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan WHERE MaKH = @MaKH)
                            BEGIN
                                DECLARE @Bank VARCHAR(20);
                                SELECT TOP 1 @Bank = MaNganHang FROM dbo.NguoiDung WHERE MaKH = @MaKH AND MaNganHang IS NOT NULL;
                                IF @Bank IS NULL OR LEN(@Bank) = 0
                                    SELECT TOP 1 @Bank = MaNganHang FROM dbo.NganHang ORDER BY MaNganHang;
                                IF @Bank IS NULL OR LEN(@Bank) = 0
                                    SET @Bank = 'NH01';

                                DECLARE @NewAcc VARCHAR(30);
                                SELECT @NewAcc = CAST(
                                    ISNULL(
                                        MAX(TRY_CONVERT(BIGINT, SoTaiKhoan)),
                                        CAST(
                                            CASE 
                                                WHEN TRY_CONVERT(INT, REPLACE(REPLACE(@Bank, 'NH', ''), 'nh', '')) IS NOT NULL
                                                THEN CAST(1000000000 + TRY_CONVERT(INT, REPLACE(REPLACE(@Bank, 'NH', ''), 'nh', '')) * 10000000 AS BIGINT)
                                                ELSE 1000001000
                                            END AS BIGINT
                                        )
                                    ) + 1 AS VARCHAR(30)
                                )
                                FROM dbo.TaiKhoan WITH (UPDLOCK, HOLDLOCK)
                                WHERE MaNganHang = @Bank AND TRY_CONVERT(BIGINT, SoTaiKhoan) IS NOT NULL;

                                INSERT INTO dbo.TaiKhoan (SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien, SoDuBanDau, SoDuHienTai, NgayMo, TrangThai)
                                VALUES (@NewAcc, @MaKH, @Bank, 'VND', 50000000, 50000000, GETDATE(), 'Active');

                                DECLARE @MaGD VARCHAR(30) = 'GD' + CONVERT(VARCHAR(8), GETDATE(), 112) + RIGHT('000000' + CAST(ABS(CHECKSUM(NEWID())) % 1000000 AS VARCHAR(6)), 6);
                                INSERT INTO dbo.GiaoDich 
                                    (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, SoTienGoc, 
                                     MaLoaiTien, LoaiTien, NgayGiaoDich, NoiDung, TrangThai)
                                VALUES 
                                    (@MaGD, @NewAcc, 1, 50000000, 50000000, 
                                     'VND', 'VND', GETDATE(), N'Ngân hàng khởi tạo tài khoản', 'Success');
                            END";

                        using (var cmdEnsure = new SqlCommand(ensureAccountSql, conn))
                        {
                            cmdEnsure.Parameters.AddWithValue("@MaKH", targetMaKH);
                            await cmdEnsure.ExecuteNonQueryAsync();
                        }
                    }

                    // BƯỚC 3: Truy vấn danh sách tài khoản để tạo đối tượng thẻ ngân hàng số
                    string sql = @"
                        SELECT tk.SoTaiKhoan,
                               tk.MaKH,
                               COALESCE(NULLIF(kh.HoTen, N''), NULLIF(nd.HoTen, N''), tk.MaKH, N'Khách hàng') AS HoTen,
                               ISNULL(nh.TenNganHang, N'Vietcombank') AS TenNganHang,
                               tk.MaLoaiTien,
                               tk.SoDuHienTai,
                               tk.NgayMo,
                               tk.TrangThai
                        FROM dbo.TaiKhoan tk
                        LEFT JOIN dbo.ThongTinKhachHang kh ON tk.MaKH = kh.MaKH
                        LEFT JOIN dbo.NguoiDung nd ON tk.MaKH = nd.MaKH
                        LEFT JOIN dbo.NganHang nh ON tk.MaNganHang = nh.MaNganHang
                        WHERE 1 = 1 ";

                    if (!isAdmin && !string.IsNullOrWhiteSpace(targetMaKH))
                    {
                        sql += " AND tk.MaKH = @MaKH ";
                    }

                    sql += " ORDER BY tk.NgayMo DESC";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        if (!isAdmin && !string.IsNullOrWhiteSpace(targetMaKH))
                        {
                            cmd.Parameters.AddWithValue("@MaKH", targetMaKH);
                        }

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string accountNumber = reader["SoTaiKhoan"]?.ToString() ?? string.Empty;
                                string custId = reader["MaKH"]?.ToString() ?? string.Empty;
                                string holderName = reader["HoTen"]?.ToString();
                                if (string.IsNullOrWhiteSpace(holderName))
                                {
                                    holderName = isAdmin
                                        ? (!string.IsNullOrWhiteSpace(custId) ? $"KHÁCH HÀNG {custId}" : "KHÁCH HÀNG")
                                        : sessionName;
                                }
                                holderName = holderName.ToUpper();

                                string bankName = reader["TenNganHang"]?.ToString() ?? "Vietcombank";
                                string currency = reader["MaLoaiTien"]?.ToString() ?? "VND";
                                decimal balance = reader["SoDuHienTai"] != DBNull.Value
                                    ? Convert.ToDecimal(reader["SoDuHienTai"])
                                    : 0m;

                                DateTime issueDate = reader["NgayMo"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["NgayMo"])
                                    : DateTime.Today.AddYears(-1);

                                string status = reader["TrangThai"]?.ToString() ?? "Active";

                                // BƯỚC 4: Tạo số thẻ 16 chữ số tương ứng với số tài khoản
                                string digits = Regex.Replace(accountNumber, "[^0-9]", "");
                                if (digits.Length < 8) digits = digits.PadLeft(8, '0');

                                string last4 = digits.Length >= 4 ? digits.Substring(digits.Length - 4) : "9876";
                                string mid4 = digits.Length >= 8 ? digits.Substring(digits.Length - 8, 4) : "5869";

                                string debitFull = $"4213 {mid4} 1234 {last4}";
                                string debitMasked = $"4213 **** **** {last4}";

                                var debitCard = new CreditCardInfo
                                {
                                    AccountNumber = accountNumber,
                                    CustomerId = custId,
                                    CardHolderName = holderName,
                                    FullCardNumber = debitFull,
                                    MaskedCardNumber = debitMasked,
                                    CardType = "Thẻ ghi nợ",
                                    BankName = bankName,
                                    Balance = balance,
                                    Currency = currency,
                                    IssueDate = issueDate,
                                    ExpiryDate = issueDate.AddYears(5),
                                    Status = status,
                                    StatusDate = DateTime.Today,
                                    CVV = (Math.Abs(debitFull.GetHashCode()) % 900 + 100).ToString()
                                };

                                cards.Add(debitCard);

                                // Nếu là Member thì chỉ cần lấy tài khoản đầu tiên làm thẻ chính hiển thị
                                if (!isAdmin)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[CreditService] Lỗi truy vấn thẻ: {ex.Message}");
            }

            // BƯỚC 5: Nếu không có dữ liệu từ DB (hoặc chưa có tài khoản), cung cấp dữ liệu mặc định chuẩn
            if (cards.Count == 0)
            {
                string defaultHolder = isAdmin ? "KHÁCH HÀNG" : sessionName;
                cards.AddRange(GetDefaultCards(defaultHolder));
            }

            return cards;
        }

        /// <summary>
        /// Tìm kiếm thẻ ngân hàng theo từ khóa (Số thẻ, Số tài khoản, Mã KH, Tên chủ thẻ, Ngân hàng).
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm.</param>
        /// <param name="maKH">Mã khách hàng cần lọc (nếu có).</param>
        /// <param name="isAdmin">Cờ quyền quản trị viên.</param>
        /// <returns>Danh sách thẻ phù hợp với từ khóa.</returns>
        public async Task<List<CreditCardInfo>> SearchCardsAsync(string keyword, string maKH = null, bool isAdmin = false)
        {
            var allCards = await GetCardsAsync(maKH, isAdmin);

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return allCards;
            }

            string kw = keyword.Trim().ToLower();

            return allCards.FindAll(c =>
                (c.AccountNumber != null && c.AccountNumber.ToLower().Contains(kw)) ||
                (c.FullCardNumber != null && c.FullCardNumber.ToLower().Contains(kw)) ||
                (c.MaskedCardNumber != null && c.MaskedCardNumber.ToLower().Contains(kw)) ||
                (c.CustomerId != null && c.CustomerId.ToLower().Contains(kw)) ||
                (c.CardHolderName != null && c.CardHolderName.ToLower().Contains(kw)) ||
                (c.BankName != null && c.BankName.ToLower().Contains(kw))
            );
        }

        /// <summary>
        /// Tạo danh sách thẻ mặc định chuẩn (Thẻ ghi nợ) để hiển thị khi hệ thống chưa có dữ liệu tài khoản.
        /// </summary>
        /// <param name="holderName">Họ tên chủ thẻ hiển thị.</param>
        /// <returns>Danh sách chứa Thẻ ghi nợ mặc định.</returns>
        public List<CreditCardInfo> GetDefaultCards(string holderName = "PHONG HOÁ")
        {
            if (string.IsNullOrWhiteSpace(holderName))
            {
                holderName = "PHONG HOÁ";
            }
            holderName = holderName.ToUpper();

            DateTime issueDate = new DateTime(2023, 8, 27);
            DateTime expiryDate = issueDate.AddYears(5);

            return new List<CreditCardInfo>
            {
                new CreditCardInfo
                {
                    AccountNumber = "1000000001",
                    CustomerId = "KH100001",
                    CardHolderName = holderName,
                    FullCardNumber = "4213 5869 1234 9876",
                    MaskedCardNumber = "4213 **** **** 9876",
                    CardType = "Thẻ ghi nợ",
                    BankName = "Vietcombank",
                    Balance = 50000000m,
                    Currency = "VND",
                    IssueDate = issueDate,
                    ExpiryDate = expiryDate,
                    Status = "Active",
                    StatusDate = DateTime.Today,
                    CVV = "888"
                }
            };
        }

        /// <summary>
        /// Cập nhật trạng thái khóa hoặc mở khóa của thẻ (đồng bộ vào bảng TaiKhoan trong cơ sở dữ liệu).
        /// </summary>
        /// <param name="accountNumber">Số tài khoản liên kết với thẻ.</param>
        /// <param name="isLocked">True nếu muốn khóa thẻ ("Locked"), False nếu muốn mở khóa hoạt động ("Active").</param>
        /// <returns>True nếu cập nhật thành công, False nếu thất bại.</returns>
        public async Task<bool> UpdateCardStatusAsync(string accountNumber, bool isLocked)
        {
            if (string.IsNullOrWhiteSpace(accountNumber)) return false;

            string newStatus = isLocked ? "Locked" : "Active";
            string acc = accountNumber.Trim();

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();

                    // BƯỚC 1: Cập nhật trạng thái bảng TaiKhoan theo Số tài khoản
                    const string sql = @"
                        UPDATE dbo.TaiKhoan
                        SET TrangThai = @TrangThai
                        WHERE SoTaiKhoan = @SoTaiKhoan";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TrangThai", newStatus);
                        cmd.Parameters.AddWithValue("@SoTaiKhoan", acc);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        if (rows > 0) return true;

                        // BƯỚC 2: Fallback 1: Cập nhật theo MaKH của phiên hiện tại nếu SoTaiKhoan là số giả định
                        string targetMaKH = CurrentSession.MaKH;
                        if (!string.IsNullOrWhiteSpace(targetMaKH))
                        {
                            const string fallbackMaKHSql = @"
                                UPDATE dbo.TaiKhoan
                                SET TrangThai = @TrangThai
                                WHERE MaKH = @MaKH";

                            using (var cmdFb = new SqlCommand(fallbackMaKHSql, conn))
                            {
                                cmdFb.Parameters.AddWithValue("@TrangThai", newStatus);
                                cmdFb.Parameters.AddWithValue("@MaKH", targetMaKH);

                                int fbRows = await cmdFb.ExecuteNonQueryAsync();
                                if (fbRows > 0) return true;
                            }

                            // BƯỚC 3: Fallback 2: Nếu chưa hề có tài khoản nào, tự động tạo mới với trạng thái newStatus
                            const string upsertSql = @"
                                DECLARE @Bank VARCHAR(20);
                                SELECT TOP 1 @Bank = MaNganHang FROM dbo.NguoiDung WHERE MaKH = @MaKH AND MaNganHang IS NOT NULL;
                                IF @Bank IS NULL OR LEN(@Bank) = 0
                                    SELECT TOP 1 @Bank = MaNganHang FROM dbo.NganHang ORDER BY MaNganHang;

                                INSERT INTO dbo.TaiKhoan (SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien, SoDuBanDau, SoDuHienTai, NgayMo, TrangThai)
                                VALUES (@SoTaiKhoan, @MaKH, ISNULL(@Bank, 'NH01'), 'VND', 50000000, 50000000, GETDATE(), @TrangThai);

                                DECLARE @MaGD VARCHAR(30) = 'GD' + CONVERT(VARCHAR(8), GETDATE(), 112) + RIGHT('000000' + CAST(ABS(CHECKSUM(NEWID())) % 1000000 AS VARCHAR(6)), 6);
                                INSERT INTO dbo.GiaoDich 
                                    (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, SoTienGoc, 
                                     MaLoaiTien, LoaiTien, NgayGiaoDich, NoiDung, TrangThai)
                                VALUES 
                                    (@MaGD, @SoTaiKhoan, 1, 50000000, 50000000, 
                                     'VND', 'VND', GETDATE(), N'Ngân hàng khởi tạo tài khoản', 'Success');";

                            using (var cmdUpsert = new SqlCommand(upsertSql, conn))
                            {
                                cmdUpsert.Parameters.AddWithValue("@SoTaiKhoan", acc);
                                cmdUpsert.Parameters.AddWithValue("@MaKH", targetMaKH);
                                cmdUpsert.Parameters.AddWithValue("@TrangThai", newStatus);

                                int upRows = await cmdUpsert.ExecuteNonQueryAsync();
                                if (upRows > 0) return true;
                            }
                        }

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[CreditService] Lỗi cập nhật trạng thái thẻ: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Che bảo mật số thẻ chỉ hiển thị 4 số đầu và 4 số cuối (Ví dụ: "4213 **** **** 9876").
        /// </summary>
        /// <param name="cardNumber">Số thẻ đầy đủ 16 chữ số.</param>
        /// <returns>Chuỗi số thẻ đã được che dạng 4213 **** **** 9876.</returns>
        public string MaskCardNumber(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber)) return "**** **** **** ****";

            string clean = Regex.Replace(cardNumber, "[^0-9]", "");
            if (clean.Length < 8) return cardNumber;

            string first4 = clean.Substring(0, 4);
            string last4 = clean.Substring(clean.Length - 4);
            return $"{first4} **** **** {last4}";
        }

        /// <summary>
        /// Định dạng số dư tài khoản kèm đơn vị tiền tệ.
        /// </summary>
        /// <param name="balance">Số dư hiện tại.</param>
        /// <param name="currency">Đơn vị tiền tệ (VND, USD,...).</param>
        /// <returns>Chuỗi định dạng số tiền (Ví dụ: 50,000,000 VND).</returns>
        public string FormatCurrency(decimal balance, string currency)
        {
            return $"{balance:N0} {currency}";
        }
    }
}
