using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Dịch vụ xử lý toàn bộ nghiệp vụ Vay vốn & Thẩm định tài chính (Borrowing Service):
    /// - Quản lý cấu hình mức vay, kỳ hạn và biểu phí lãi suất chuẩn Fintech.
    /// - Tính toán lãi suất thời gian thực, tổng dư nợ và ngày đáo hạn.
    /// - Quản lý đăng ký hồ sơ vay cho Khách hàng.
    /// - Thẩm định tài chính 1 quý (90 ngày) theo giao dịch thực tế cho Admin.
    /// - Phê duyệt giải ngân trực tiếp vào số dư tài khoản ngân hàng và ghi nhận lịch sử giao dịch.
    /// - Từ chối hồ sơ kèm lý do và chức năng thanh toán/tất toán nợ.
    /// - Thống kê tổng hợp số liệu khoản vay.
    /// </summary>
    public class BorrowingService
    {
        private readonly DatabaseConfig _config;

        #region CONSTANTS & SYSTEM PARAMETERS

        /// <summary>
        /// Mức vay tối thiểu cho phép (100.000 VND).
        /// </summary>
        public const decimal VAY_MIN = 100000m;

        /// <summary>
        /// Mức vay tối đa cho phép (10.000.000 VND).
        /// </summary>
        public const decimal VAY_MAX = 10000000m;

        /// <summary>
        /// Bước nhảy số tiền vay trên thanh trượt (100.000 VND).
        /// </summary>
        public const decimal VAY_STEP = 100000m;

        /// <summary>
        /// Thời hạn vay tối thiểu (7 ngày).
        /// </summary>
        public const int TIME_MIN = 7;

        /// <summary>
        /// Thời hạn vay tối đa (30 ngày).
        /// </summary>
        public const int TIME_MAX = 30;

        /// <summary>
        /// Bước nhảy thời hạn vay (1 ngày).
        /// </summary>
        public const int TIME_STEP = 1;

        /// <summary>
        /// Lãi suất ưu đãi mặc định: 1.55%/ngày (tương đương 465.000 đ lãi cho khoản 1.000.000 đ trong 30 ngày).
        /// </summary>
        public const decimal DEFAULT_DAILY_INTEREST_RATE = 1.55m;

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Khởi tạo BorrowingService với cấu hình cơ sở dữ liệu mặc định từ file cấu hình.
        /// Tự động kiểm tra và khởi tạo bảng KhoanVay nếu chưa tồn tại.
        /// </summary>
        public BorrowingService()
        {
            _config = DatabaseConfigManager.Load();
            EnsureTableExists();
        }

        /// <summary>
        /// Khởi tạo BorrowingService với đối tượng cấu hình tùy chỉnh truyền vào.
        /// </summary>
        /// <param name="config">Cấu hình kết nối cơ sở dữ liệu</param>
        public BorrowingService(DatabaseConfig config)
        {
            _config = config ?? DatabaseConfigManager.Load();
            EnsureTableExists();
        }

        /// <summary>
        /// Tạo chuỗi kết nối SQL Server từ cấu hình hiện tại.
        /// </summary>
        /// <returns>Chuỗi kết nối ConnectionString</returns>
        private string GetConnectionString()
        {
            return DatabaseService.BuildConnectionString(_config);
        }

        #endregion

        #region DATABASE SCHEMA INITIALIZATION

        /// <summary>
        /// Đảm bảo bảng dbo.KhoanVay luôn tồn tại trong cơ sở dữ liệu hệ thống.
        /// Tự động thực thi câu lệnh DDL CREATE TABLE nếu bảng chưa có.
        /// </summary>
        public void EnsureTableExists()
        {
            try
            {
                // BƯỚC 1: Mở kết nối đến SQL Server
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();

                    // BƯỚC 2: Kiểm tra trong INFORMATION_SCHEMA xem bảng KhoanVay đã tồn tại chưa
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

                    // BƯỚC 3: Thực thi lệnh tạo bảng
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

        #endregion

        #region CALCULATION & BUSINESS RULES

        /// <summary>
        /// Tính toán tự động số tiền lãi, tổng số nợ phải trả và ngày đáo hạn theo chuẩn công thức hệ thống.
        /// Công thức: Lãi = Vốn × (Lãi suất ngày / 100) × Số ngày vay.
        /// Tổng phải trả = Vốn + Lãi.
        /// </summary>
        /// <param name="soTienVay">Số tiền vay (Vốn gốc)</param>
        /// <param name="thoiGianVay">Thời gian vay tính theo số ngày</param>
        /// <param name="dailyRatePercent">Lãi suất theo ngày (%/ngày), mặc định 1.55%</param>
        /// <returns>Đối tượng LoanCalculationResult chứa kết quả tính toán chi tiết</returns>
        public LoanCalculationResult CalculateLoan(decimal soTienVay, int thoiGianVay, decimal dailyRatePercent = DEFAULT_DAILY_INTEREST_RATE)
        {
            // BƯỚC 1: Xác định lãi suất ngày hợp lệ
            decimal rate = dailyRatePercent > 0 ? dailyRatePercent : DEFAULT_DAILY_INTEREST_RATE;
            decimal dailyRateFraction = rate / 100m;

            // BƯỚC 2: Tính số tiền lãi phát sinh: Lãi = Vốn * (% lãi ngày) * Số ngày
            decimal tienLai = Math.Round(soTienVay * dailyRateFraction * thoiGianVay, 0);

            // BƯỚC 3: Tính tổng số tiền phải trả khi đáo hạn: Tổng = Vốn gốc + Tiền lãi
            decimal tongPhaiTra = soTienVay + tienLai;

            // BƯỚC 4: Xác định ngày đăng ký và tính toán ngày đáo hạn tương lai
            DateTime ngayDangKy = DateTime.Now;
            DateTime ngayDaoHan = DateTime.Today.AddDays(thoiGianVay);

            return new LoanCalculationResult
            {
                SoTienVay = soTienVay,
                ThoiGianVay = thoiGianVay,
                LaiSuatNgay = rate,
                TienLai = tienLai,
                TongPhaiTra = tongPhaiTra,
                NgayDangKy = ngayDangKy,
                NgayDaoHan = ngayDaoHan
            };
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của các tham số khi đăng ký hồ sơ khoản vay.
        /// </summary>
        /// <param name="maKH">Mã khách hàng</param>
        /// <param name="soTaiKhoan">Số tài khoản ngân hàng thụ hưởng</param>
        /// <param name="soTienVay">Số tiền yêu cầu vay</param>
        /// <param name="thoiGianVay">Thời hạn vay tính theo ngày</param>
        /// <returns>Bộ giá trị (IsValid: Hợp lệ hay không, ErrorMessage: Thông báo lỗi nếu có)</returns>
        public (bool IsValid, string ErrorMessage) ValidateLoanRegistration(
            string maKH, string soTaiKhoan, decimal soTienVay, int thoiGianVay)
        {
            // BƯỚC 1: Kiểm tra thông tin mã khách hàng
            if (string.IsNullOrWhiteSpace(maKH))
                return (false, "Không tìm thấy thông tin khách hàng đăng ký.");

            // BƯỚC 2: Kiểm tra số tài khoản ngân hàng thụ hưởng giải ngân
            if (string.IsNullOrWhiteSpace(soTaiKhoan))
                return (false, "Vui lòng chọn tài khoản ngân hàng nhận tiền giải ngân.");

            // BƯỚC 3: Kiểm tra hạn mức vay cho phép [VAY_MIN .. VAY_MAX]
            if (soTienVay < VAY_MIN || soTienVay > VAY_MAX)
                return (false, $"Số tiền vay phải nằm trong khoảng từ {VAY_MIN:N0} đ đến {VAY_MAX:N0} đ.");

            // BƯỚC 4: Kiểm tra thời hạn vay cho phép [TIME_MIN .. TIME_MAX]
            if (thoiGianVay < TIME_MIN || thoiGianVay > TIME_MAX)
                return (false, $"Thời hạn vay phải từ {TIME_MIN} ngày đến {TIME_MAX} ngày.");

            return (true, string.Empty);
        }

        #endregion

        #region CUSTOMER LOAN REGISTRATION & ACCOUNT QUERIES

        /// <summary>
        /// Lấy danh sách tài khoản ngân hàng đang hoạt động của khách hàng để nhận giải ngân.
        /// </summary>
        /// <param name="maKH">Mã khách hàng</param>
        /// <returns>Danh sách tài khoản ngân hàng của khách hàng</returns>
        public async Task<List<TaiKhoan>> GetUserPaymentAccountsAsync(string maKH)
        {
            var list = new List<TaiKhoan>();
            try
            {
                // BƯỚC 1: Mở kết nối bất đồng bộ đến cơ sở dữ liệu
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();

                    // BƯỚC 2: Truy vấn danh sách tài khoản thuộc khách hàng hoặc liên kết với tài khoản đăng nhập
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

                    // BƯỚC 3: Thực thi đọc dữ liệu và ánh xạ sang Model TaiKhoan
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
        /// <param name="maKH">Mã khách hàng đăng ký</param>
        /// <param name="soTaiKhoan">Số tài khoản nhận giải ngân</param>
        /// <param name="soTienVay">Số tiền cần vay</param>
        /// <param name="thoiGianVay">Thời hạn vay tính theo ngày</param>
        /// <param name="laiSuat">Lãi suất áp dụng (%/ngày)</param>
        /// <param name="ghiChu">Ghi chú hoặc mục đích vay</param>
        /// <returns>Bộ giá trị (Success: Thành công hay không, Message: Thông báo, MaKhoanVay: Mã khoản vay sinh ra)</returns>
        public async Task<(bool Success, string Message, string MaKhoanVay)> CreateLoanAsync(
            string maKH, string soTaiKhoan, decimal soTienVay, int thoiGianVay, decimal laiSuat, string ghiChu)
        {
            // BƯỚC 1: Kiểm tra tính hợp lệ của tham số đầu vào
            var valResult = ValidateLoanRegistration(maKH, soTaiKhoan, soTienVay, thoiGianVay);
            if (!valResult.IsValid)
                return (false, valResult.ErrorMessage, "");

            try
            {
                // BƯỚC 2: Tính toán tự động số tiền lãi và tổng phải trả
                var calc = CalculateLoan(soTienVay, thoiGianVay, laiSuat);

                // BƯỚC 3: Sinh mã định danh duy nhất cho hồ sơ khoản vay (KV + yyMMddHHmmss + random)
                string maKV = "KV" + DateTime.Now.ToString("yyMMddHHmmss") + (new Random().Next(100, 999));

                // BƯỚC 4: Ghi nhận hồ sơ vào bảng dbo.KhoanVay với trạng thái 'Chờ xét duyệt'
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
                        cmd.Parameters.AddWithValue("@SoTien", calc.SoTienVay);
                        cmd.Parameters.AddWithValue("@ThoiGian", calc.ThoiGianVay);
                        cmd.Parameters.AddWithValue("@LaiSuat", calc.LaiSuatNgay);
                        cmd.Parameters.AddWithValue("@TienLai", calc.TienLai);
                        cmd.Parameters.AddWithValue("@TongPhaiTra", calc.TongPhaiTra);
                        cmd.Parameters.AddWithValue("@NgayDK", calc.NgayDangKy);
                        cmd.Parameters.AddWithValue("@NgayDH", calc.NgayDaoHan);
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
        /// <param name="maKH">Mã khách hàng cần xem hồ sơ</param>
        /// <returns>Danh sách các khoản vay của khách hàng</returns>
        public async Task<List<BorrowingLoan>> GetUserLoansAsync(string maKH)
        {
            var list = new List<BorrowingLoan>();
            try
            {
                // BƯỚC 1: Mở kết nối CSDL
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();

                    // BƯỚC 2: Truy vấn thông tin hồ sơ vay kết hợp bảng khách hàng và ngân hàng
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

                    // BƯỚC 3: Đọc dữ liệu và ánh xạ sang danh sách BorrowingLoan
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

        #endregion

        #region ADMIN LOAN MANAGEMENT & QUERIES

        /// <summary>
        /// Lấy toàn bộ danh sách hồ sơ vay phục vụ quản lý phía Admin (có lọc và tìm kiếm).
        /// </summary>
        /// <param name="statusFilter">Bộ lọc trạng thái hồ sơ ("Tất cả", "Chờ xét duyệt", "Đã duyệt", "Đã từ chối", "Đã tất toán")</param>
        /// <param name="keyword">Từ khóa tìm kiếm theo mã hồ sơ, mã KH, họ tên, tài khoản hoặc tên đăng nhập</param>
        /// <returns>Danh sách hồ sơ vay thỏa mãn điều kiện</returns>
        public async Task<List<BorrowingLoan>> GetAllLoansAsync(string statusFilter = "Tất cả", string keyword = "")
        {
            var list = new List<BorrowingLoan>();
            try
            {
                // BƯỚC 1: Mở kết nối CSDL
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();

                    // BƯỚC 2: Xây dựng câu truy vấn động theo bộ lọc
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

                    // BƯỚC 3: Gán tham số và thực thi truy vấn
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
        /// Lấy danh sách hồ sơ vay phân loại rõ ràng: Chưa trả (Đang còn nợ) và Đã trả (Đã tất toán).
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm tùy chọn</param>
        /// <returns>Bộ dữ liệu (UnpaidLoans: Khoản vay chưa trả, PaidLoans: Khoản vay đã tất toán)</returns>
        public async Task<(List<BorrowingLoan> UnpaidLoans, List<BorrowingLoan> PaidLoans)> GetRepaymentTrackingAsync(string keyword = "")
        {
            // BƯỚC 1: Lấy toàn bộ danh sách khoản vay theo từ khóa
            var all = await GetAllLoansAsync("Tất cả", keyword);

            // BƯỚC 2: Tách nhóm khoản vay đang hoạt động / nợ cần thu hồi (Trạng thái 'Đã duyệt')
            var unpaid = all.Where(x => x.TrangThai == "Đã duyệt").OrderBy(x => x.NgayDaoHan).ToList();

            // BƯỚC 3: Tách nhóm khoản vay đã thanh toán hoàn tất (Trạng thái 'Đã tất toán')
            var paid = all.Where(x => x.TrangThai == "Đã tất toán").OrderByDescending(x => x.NgayDuyet ?? x.NgayDangKy).ToList();

            return (unpaid, paid);
        }

        /// <summary>
        /// Lấy chi tiết một hồ sơ khoản vay theo mã định danh.
        /// </summary>
        /// <param name="maKhoanVay">Mã hồ sơ khoản vay cần tìm</param>
        /// <returns>Thông tin hồ sơ khoản vay hoặc null nếu không tìm thấy</returns>
        public async Task<BorrowingLoan> GetLoanByIdAsync(string maKhoanVay)
        {
            if (string.IsNullOrWhiteSpace(maKhoanVay)) return null;

            try
            {
                // BƯỚC 1: Mở kết nối CSDL
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();

                    // BƯỚC 2: Truy vấn chi tiết theo MaKhoanVay
                    const string sql = @"
                        SELECT TOP 1 
                               KV.MaKhoanVay, KV.MaKH, KV.SoTaiKhoan, KV.SoTienVay, KV.ThoiGianVay,
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
                        WHERE KV.MaKhoanVay = @MaKV";

                    // BƯỚC 3: Đọc và trả về kết quả
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKV", maKhoanVay.Trim());
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return ReadLoanFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BorrowingService] GetLoanByIdAsync error: {ex.Message}");
            }
            return null;
        }

        /// <summary>
        /// Lấy thống kê tổng hợp các chỉ số hồ sơ vay cho Admin Dashboard.
        /// </summary>
        /// <returns>Đối tượng BorrowingStatistics chứa số liệu tổng hợp</returns>
        public async Task<BorrowingStatistics> GetLoanStatisticsAsync()
        {
            var stats = new BorrowingStatistics();
            try
            {
                // BƯỚC 1: Mở kết nối CSDL
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();

                    // BƯỚC 2: Thực thi truy vấn tính toán tổng hợp các chỉ số
                    const string sql = @"
                        SELECT 
                            COUNT(*) AS TongHoSo,
                            SUM(CASE WHEN TrangThai = N'Chờ xét duyệt' THEN 1 ELSE 0 END) AS ChoDuyet,
                            SUM(CASE WHEN TrangThai = N'Đã duyệt' THEN 1 ELSE 0 END) AS DaDuyet,
                            SUM(CASE WHEN TrangThai = N'Từ chối' OR TrangThai = N'Đã từ chối' THEN 1 ELSE 0 END) AS TuChoi,
                            SUM(CASE WHEN TrangThai = N'Đã tất toán' THEN 1 ELSE 0 END) AS DaTatToan,
                            SUM(CASE WHEN TrangThai = N'Đã duyệt' THEN SoTienVay ELSE 0 END) AS TongTienDaGiaiNgan,
                            SUM(CASE WHEN TrangThai = N'Đã duyệt' THEN TongPhaiTra ELSE 0 END) AS TongDuNoCanThuHoi
                        FROM dbo.KhoanVay";

                    // BƯỚC 3: Đọc kết quả thống kê
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                stats.TongHoSo = reader["TongHoSo"] != DBNull.Value ? Convert.ToInt32(reader["TongHoSo"]) : 0;
                                stats.ChoDuyet = reader["ChoDuyet"] != DBNull.Value ? Convert.ToInt32(reader["ChoDuyet"]) : 0;
                                stats.DaDuyet = reader["DaDuyet"] != DBNull.Value ? Convert.ToInt32(reader["DaDuyet"]) : 0;
                                stats.TuChoi = reader["TuChoi"] != DBNull.Value ? Convert.ToInt32(reader["TuChoi"]) : 0;
                                stats.DaTatToan = reader["DaTatToan"] != DBNull.Value ? Convert.ToInt32(reader["DaTatToan"]) : 0;
                                stats.TongTienDaGiaiNgan = reader["TongTienDaGiaiNgan"] != DBNull.Value ? Convert.ToDecimal(reader["TongTienDaGiaiNgan"]) : 0m;
                                stats.TongDuNoCanThuHoi = reader["TongDuNoCanThuHoi"] != DBNull.Value ? Convert.ToDecimal(reader["TongDuNoCanThuHoi"]) : 0m;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BorrowingService] GetLoanStatisticsAsync error: {ex.Message}");
            }
            return stats;
        }

        #endregion

        #region FINANCIAL APPRAISAL (THẨM ĐỊNH TÀI CHÍNH 1 QUÝ)

        /// <summary>
        /// Thẩm định tài chính trong 1 quý (90 ngày gần nhất) của khách hàng
        /// gồm: Mã tài khoản, Mã khách hàng, Tên người dùng, Tổng số thu chi trong 1 quý.
        /// </summary>
        /// <param name="maKH">Mã khách hàng</param>
        /// <param name="soTaiKhoan">Số tài khoản đăng ký nhận giải ngân</param>
        /// <param name="soTienVay">Số tiền yêu cầu vay</param>
        /// <returns>Đối tượng FinancialAppraisal chứa kết quả phân tích năng lực tín dụng</returns>
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

                    // BƯỚC 1: Lấy thông tin khách hàng, tài khoản và số dư khả dụng hiện tại
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

                    // BƯỚC 2: Thống kê thu chi trong 1 quý (90 ngày gần nhất)
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

                    // BƯỚC 3: Đánh giá năng lực trả nợ theo tiêu chuẩn tín dụng
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

        #endregion

        #region APPROVAL, REJECTION & SETTLEMENT

        /// <summary>
        /// Admin xác nhận duyệt hồ sơ vay:
        /// - Chuyển trạng thái sang 'Đã duyệt'
        /// - GIẢI NGÂN TRỰC TIẾP: Cộng tiền vào tài khoản ngân hàng của khách
        /// - Ghi nhận lịch sử giao dịch vào dbo.GiaoDich (Thu)
        /// - Phát thông báo giao dịch để các Form đồng bộ số dư
        /// </summary>
        /// <param name="maKhoanVay">Mã hồ sơ vay cần phê duyệt</param>
        /// <param name="adminUser">Tên người quản trị thực hiện duyệt</param>
        /// <returns>Bộ giá trị (Success: Phê duyệt thành công hay không, Message: Thông báo kết quả)</returns>
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
                            // BƯỚC 1: Kiểm tra trạng thái hiện tại của khoản vay (Khóa bản ghi bằng UPDLOCK)
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

                            // BƯỚC 2: Cập nhật trạng thái KhoanVay sang 'Đã duyệt' và ghi nhận người duyệt
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

                            // BƯỚC 3: GIẢI NGÂN TRỰC TIẾP: Cộng số tiền vay vào số dư tài khoản thụ hưởng
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

                            // BƯỚC 4: Ghi nhận giao dịch giải ngân vào dbo.GiaoDich (MaLoaiGD = 1: Nạp tiền / Thu)
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

                            // BƯỚC 5: Hoàn tất Transaction
                            tran.Commit();

                            // BƯỚC 6: Bắn thông báo toàn hệ thống để các Form đồng bộ số dư & lịch sử giao dịch
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
        /// <param name="maKhoanVay">Mã hồ sơ vay cần từ chối</param>
        /// <param name="reason">Lý do từ chối hồ sơ</param>
        /// <param name="adminUser">Tên người quản trị thực hiện từ chối</param>
        /// <returns>Bộ giá trị (Success: Từ chối thành công hay không, Message: Thông báo kết quả)</returns>
        public async Task<(bool Success, string Message)> RejectLoanAsync(string maKhoanVay, string reason, string adminUser)
        {
            if (string.IsNullOrWhiteSpace(maKhoanVay))
                return (false, "Mã khoản vay không hợp lệ.");

            try
            {
                // BƯỚC 1: Mở kết nối CSDL
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();

                    // BƯỚC 2: Cập nhật trạng thái KhoanVay sang 'Đã từ chối' kèm lý do
                    const string sql = @"
                        UPDATE dbo.KhoanVay 
                        SET TrangThai = N'Đã từ chối', 
                            GhiChu = @Reason, 
                            NgayDuyet = GETDATE(), 
                            NguoiDuyet = @Admin 
                        WHERE MaKhoanVay = @MaKV AND TrangThai = N'Chờ xét duyệt'";

                    // BƯỚC 3: Thực thi câu lệnh UPDATE
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

        /// <summary>
        /// Khách hàng thực hiện thanh toán / tất toán nợ khoản vay từ số dư tài khoản ngân hàng.
        /// </summary>
        /// <param name="maKhoanVay">Mã khoản vay cần tất toán</param>
        /// <param name="userLogin">Tên đăng nhập của người thực hiện thanh toán</param>
        /// <returns>Bộ giá trị (Success: Thanh toán thành công hay không, Message: Thông báo kết quả)</returns>
        public async Task<(bool Success, string Message)> PayLoanAsync(string maKhoanVay, string userLogin)
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
                            // BƯỚC 1: Kiểm tra hồ sơ khoản vay (Khóa dòng bằng UPDLOCK)
                            decimal tongPhaiTra = 0m;
                            string soTaiKhoan = "";
                            string currentStatus = "";

                            const string sqlCheck = @"
                                SELECT SoTaiKhoan, TongPhaiTra, TrangThai 
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
                                    tongPhaiTra = Convert.ToDecimal(reader["TongPhaiTra"]);
                                }
                            }

                            if (currentStatus != "Đã duyệt")
                            {
                                tran.Rollback();
                                return (false, $"Chỉ có thể thanh toán khoản vay ở trạng thái 'Đã duyệt'. Trạng thái hiện tại: {currentStatus}.");
                            }

                            // BƯỚC 2: Kiểm tra số dư tài khoản trích nợ
                            decimal currentBalance = 0m;
                            const string sqlBal = @"SELECT SoDuHienTai FROM dbo.TaiKhoan WITH (UPDLOCK) WHERE SoTaiKhoan = @STK";
                            using (var cmdBal = new SqlCommand(sqlBal, conn, tran))
                            {
                                cmdBal.Parameters.AddWithValue("@STK", soTaiKhoan);
                                var balObj = await cmdBal.ExecuteScalarAsync();
                                if (balObj == null || balObj == DBNull.Value)
                                {
                                    tran.Rollback();
                                    return (false, "Không tìm thấy thông tin tài khoản trích nợ.");
                                }
                                currentBalance = Convert.ToDecimal(balObj);
                            }

                            if (currentBalance < tongPhaiTra)
                            {
                                tran.Rollback();
                                return (false, $"Số dư tài khoản ({currentBalance:N0} đ) không đủ để thanh toán nợ ({tongPhaiTra:N0} đ).");
                            }

                            // BƯỚC 3: Trừ tiền nợ từ số dư tài khoản
                            const string sqlDeduct = @"UPDATE dbo.TaiKhoan SET SoDuHienTai = SoDuHienTai - @Amount WHERE SoTaiKhoan = @STK";
                            using (var cmdDeduct = new SqlCommand(sqlDeduct, conn, tran))
                            {
                                cmdDeduct.Parameters.AddWithValue("@Amount", tongPhaiTra);
                                cmdDeduct.Parameters.AddWithValue("@STK", soTaiKhoan);
                                await cmdDeduct.ExecuteNonQueryAsync();
                            }

                            // BƯỚC 4: Cập nhật trạng thái KhoanVay sang 'Đã tất toán'
                            const string sqlUpdateLoan = @"
                                UPDATE dbo.KhoanVay 
                                SET TrangThai = N'Đã tất toán', 
                                    GhiChu = ISNULL(GhiChu + N' | ', N'') + N'Tất toán ngày ' + CONVERT(NVARCHAR, GETDATE(), 103)
                                WHERE MaKhoanVay = @MaKV";

                            using (var cmdUpdate = new SqlCommand(sqlUpdateLoan, conn, tran))
                            {
                                cmdUpdate.Parameters.AddWithValue("@MaKV", maKhoanVay);
                                await cmdUpdate.ExecuteNonQueryAsync();
                            }

                            // BƯỚC 5: Ghi nhận giao dịch chi trả nợ vào dbo.GiaoDich (MaLoaiGD = 2: Chi tiền / Rút tiền)
                            string maGD = "GD" + DateTime.Now.ToString("yyMMddHHmmss") + (new Random().Next(100, 999));
                            string desc = $"[Tất toán khoản vay {maKhoanVay}] Thanh toán toàn bộ gốc và lãi";

                            const string sqlTx = @"
                                INSERT INTO dbo.GiaoDich 
                                    (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, SoTienGoc, 
                                     MaLoaiTien, LoaiTien, NgayGiaoDich, NoiDung, TrangThai)
                                VALUES 
                                    (@MaGD, @STK, 2, @SoTien, @SoTien, 
                                     'VND', 'VND', GETDATE(), @NoiDung, 'Success')";

                            using (var cmdTx = new SqlCommand(sqlTx, conn, tran))
                            {
                                cmdTx.Parameters.AddWithValue("@MaGD", maGD);
                                cmdTx.Parameters.AddWithValue("@STK", soTaiKhoan);
                                cmdTx.Parameters.AddWithValue("@SoTien", tongPhaiTra);
                                cmdTx.Parameters.AddWithValue("@NoiDung", desc);
                                await cmdTx.ExecuteNonQueryAsync();
                            }

                            // BƯỚC 6: Hoàn tất giao dịch
                            tran.Commit();

                            // BƯỚC 7: Bắn thông báo toàn hệ thống để cập nhật số dư & lịch sử giao dịch
                            TransactionService.NotifyTransactionChanged();

                            return (true, $"Tất toán thành công khoản vay {maKhoanVay}! Đã thanh toán {tongPhaiTra:N0} đ.");
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
                return (false, "Lỗi khi tất toán khoản vay: " + ex.Message);
            }
        }

        #endregion

        #region HELPER METHODS

        /// <summary>
        /// Ánh xạ một dòng dữ liệu từ SqlDataReader sang đối tượng Model BorrowingLoan.
        /// </summary>
        /// <param name="reader">SqlDataReader đang đọc dòng dữ liệu hiện tại</param>
        /// <returns>Đối tượng BorrowingLoan chứa dữ liệu đầy đủ</returns>
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
                LaiSuat = reader["LaiSuat"] != DBNull.Value ? Convert.ToDecimal(reader["LaiSuat"]) : DEFAULT_DAILY_INTEREST_RATE,
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

        #endregion
    }
}
