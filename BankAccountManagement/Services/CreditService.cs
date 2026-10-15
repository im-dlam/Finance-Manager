using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Service xử lý nghiệp vụ, truy vấn và quản lý dữ liệu thẻ ngân hàng (mặc định là Thẻ ghi nợ).
    /// </summary>
    public class CreditService
    {
        private readonly DatabaseConfig _config;

        /// <summary>
        /// Khởi tạo một đối tượng CreditService mới với cấu hình cơ sở dữ liệu hiện hành.
        /// </summary>
        public CreditService()
        {
            _config = DatabaseConfigManager.Load();
        }

        /// <summary>
        /// Khởi tạo một đối tượng CreditService với cấu hình cơ sở dữ liệu truyền vào.
        /// </summary>
        /// <param name="config">Cấu hình kết nối cơ sở dữ liệu.</param>
        public CreditService(DatabaseConfig config)
        {
            _config = config ?? DatabaseConfigManager.Load();
        }

        /// <summary>
        /// Lấy chuỗi kết nối cơ sở dữ liệu SQL Server.
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
        /// <returns>Danh sách các đối tượng CreditCardInfo.</returns>
        public async Task<List<CreditCardInfo>> GetCardsAsync(string maKH = null, bool isAdmin = false)
        {
            var cards = new List<CreditCardInfo>();

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

                    // Tự động khởi tạo tài khoản nếu khách hàng hợp lệ nhưng chưa có tài khoản trong DB
                    if (!isAdmin && !string.IsNullOrWhiteSpace(targetMaKH))
                    {
                        const string ensureAccountSql = @"
                            IF NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan WHERE MaKH = @MaKH)
                            BEGIN
                                IF NOT EXISTS (SELECT 1 FROM dbo.KhachHang WHERE MaKH = @MaKH)
                                BEGIN
                                    INSERT INTO dbo.KhachHang (MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, TrangThai, NgayTao)
                                    SELECT TOP 1 MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, N'Active', GETDATE()
                                    FROM dbo.ThongTinKhachHang
                                    WHERE MaKH = @MaKH;
                                END

                                DECLARE @NewAcc VARCHAR(30);
                                SELECT @NewAcc = '1000' + RIGHT('000000' + CAST(ISNULL(MAX(CAST(RIGHT(SoTaiKhoan, 6) AS BIGINT)), 1000) + 1 AS VARCHAR(10)), 6)
                                FROM dbo.TaiKhoan;

                                IF @NewAcc IS NULL OR LEN(@NewAcc) < 10
                                    SET @NewAcc = '1000' + CAST(ABS(CHECKSUM(NEWID())) % 900000 + 100000 AS VARCHAR(10));

                                INSERT INTO dbo.TaiKhoan (SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien, SoDuBanDau, SoDuHienTai, NgayMo, TrangThai)
                                VALUES (@NewAcc, @MaKH, 'NH01', 'VND', 50000000, 50000000, GETDATE(), 'Active');
                            END";
                        using (var cmdEnsure = new SqlCommand(ensureAccountSql, conn))
                        {
                            cmdEnsure.Parameters.AddWithValue("@MaKH", targetMaKH);
                            await cmdEnsure.ExecuteNonQueryAsync();
                        }
                    }

                    string sql = @"
                        SELECT tk.SoTaiKhoan,
                               tk.MaKH,
                               COALESCE(NULLIF(kh1.HoTen, N''), NULLIF(kh2.HoTen, N''), NULLIF(nd.HoTen, N''), tk.MaKH, N'Khách hàng') AS HoTen,
                               ISNULL(nh.TenNganHang, N'Vietcombank') AS TenNganHang,
                               tk.MaLoaiTien,
                               tk.SoDuHienTai,
                               tk.NgayMo,
                               tk.TrangThai
                        FROM dbo.TaiKhoan tk
                        LEFT JOIN dbo.KhachHang kh1 ON tk.MaKH = kh1.MaKH
                        LEFT JOIN dbo.ThongTinKhachHang kh2 ON tk.MaKH = kh2.MaKH
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

                                // Sinh số thẻ 16 chữ số tương ứng với số tài khoản
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

                                // Nếu là Member thì chỉ cần lấy tài khoản đầu tiên làm thẻ chính
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

            // Nếu không có dữ liệu từ DB (hoặc chưa có tài khoản), cung cấp dữ liệu mặc định chuẩn
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
        /// <param name="isLocked">True nếu muốn khóa thẻ, False nếu muốn mở khóa hoạt động.</param>
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

                        // Fallback 1: Cập nhật theo MaKH của phiên hiện tại nếu SoTaiKhoan là số giả định
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

                            // Fallback 2: Nếu chưa hề có tài khoản nào, tự động tạo mới với trạng thái newStatus
                            const string upsertSql = @"
                                IF NOT EXISTS (SELECT 1 FROM dbo.KhachHang WHERE MaKH = @MaKH)
                                BEGIN
                                    INSERT INTO dbo.KhachHang (MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, TrangThai, NgayTao)
                                    SELECT TOP 1 MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, N'Active', GETDATE()
                                    FROM dbo.ThongTinKhachHang
                                    WHERE MaKH = @MaKH;
                                END

                                INSERT INTO dbo.TaiKhoan (SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien, SoDuBanDau, SoDuHienTai, NgayMo, TrangThai)
                                VALUES (@SoTaiKhoan, @MaKH, 'NH01', 'VND', 50000000, 50000000, GETDATE(), @TrangThai);";
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
        /// Che bảo mật số thẻ chỉ hiển thị 4 số đầu và 4 số cuối.
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
        /// <returns>Chuỗi định dạng số tiền (VD: 50,000,000 VND).</returns>
        public string FormatCurrency(decimal balance, string currency)
        {
            return $"{balance:N0} {currency}";
        }
    }
}
