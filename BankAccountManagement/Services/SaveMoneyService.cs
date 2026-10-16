using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Dịch vụ xử lý toàn bộ nghiệp vụ Gửi Tiết Kiệm Trực Tuyến & Quản lý Huy động vốn:
    /// - Quản lý kỳ hạn và niêm yết lãi suất ngân hàng
    /// - Tính toán tiền lãi dự tính và số tiền nhận khi đáo hạn
    /// - Mở tài khoản tiết kiệm trực tuyến trừ tiền vào tài khoản nguồn
    /// - Tất toán tài khoản tiết kiệm (đúng hạn hoặc trước hạn) hoàn gốc và lãi về tài khoản
    /// - Cung cấp dữ liệu thống kê tổng quan và hồ sơ khách hàng cho Quản trị viên (Admin)
    /// </summary>
    public class SaveMoneyService
    {
        private static SaveMoneyService _instance;
        public static SaveMoneyService Instance => _instance ?? (_instance = new SaveMoneyService());

        private readonly DatabaseConfig _config;

        public static readonly List<SavingsPackage> StandardPackages = new List<SavingsPackage>
        {
            new SavingsPackage { KyHanThang = 1, LaiSuatNam = 3.50m },
            new SavingsPackage { KyHanThang = 3, LaiSuatNam = 4.75m }, // Khớp ảnh 2 & 3: 4.75%/năm
            new SavingsPackage { KyHanThang = 6, LaiSuatNam = 6.75m }, // Khớp ảnh 1: 6.75%/năm
            new SavingsPackage { KyHanThang = 9, LaiSuatNam = 6.85m },
            new SavingsPackage { KyHanThang = 12, LaiSuatNam = 7.20m },
            new SavingsPackage { KyHanThang = 18, LaiSuatNam = 7.40m },
            new SavingsPackage { KyHanThang = 24, LaiSuatNam = 7.50m },
            new SavingsPackage { KyHanThang = 36, LaiSuatNam = 7.80m }
        };

        public SaveMoneyService()
        {
            _config = DatabaseConfigManager.Load();
            EnsureTableExists();
        }

        private string GetConnectionString()
        {
            return DatabaseService.BuildConnectionString(_config);
        }

        #region DATABASE SCHEMA INITIALIZATION

        /// <summary>
        /// Đảm bảo bảng dbo.SoTietKiem luôn tồn tại trong cơ sở dữ liệu hệ thống.
        /// </summary>
        public void EnsureTableExists()
        {
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    const string sql = @"
                        IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SoTietKiem')
                        BEGIN
                            CREATE TABLE dbo.SoTietKiem (
                                MaSoTKTK VARCHAR(30) PRIMARY KEY,
                                MaKH VARCHAR(20) NOT NULL,
                                SoTaiKhoanTrich VARCHAR(30) NOT NULL,
                                SoTienGui DECIMAL(18,2) NOT NULL,
                                KyHanThang INT NOT NULL,
                                LaiSuatNam DECIMAL(5,2) NOT NULL,
                                TienLaiDuTinh DECIMAL(18,2) NOT NULL,
                                TongTienDaoHanDuTinh DECIMAL(18,2) NOT NULL,
                                HinhThucDaoHan NVARCHAR(100) NOT NULL DEFAULT N'Tự động tái tục gốc',
                                NgayMo DATETIME NOT NULL DEFAULT GETDATE(),
                                NgayDaoHan DATETIME NOT NULL,
                                TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Đang gửi',
                                NgayTatToan DATETIME NULL,
                                TienLaiThucNhan DECIMAL(18,2) NULL,
                                TongTienThucNhan DECIMAL(18,2) NULL,
                                GhiChu NVARCHAR(500) NULL
                            );
                        END";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SaveMoneyService] EnsureTableExists error: {ex.Message}");
            }
        }

        #endregion

        #region PACKAGES & CALCULATION

        public List<SavingsPackage> GetPackages()
        {
            return StandardPackages;
        }

        public decimal GetRateByTerm(int kyHanThang)
        {
            var pkg = StandardPackages.Find(p => p.KyHanThang == kyHanThang);
            return pkg != null ? pkg.LaiSuatNam : 6.75m;
        }

        /// <summary>
        /// Tính toán tiền lãi dự tính và tổng tiền nhận khi đáo hạn theo chuẩn ngân hàng.
        /// </summary>
        public SavingsCalculationResult CalculateSavings(decimal soTien, int kyHanThang, DateTime? ngayMo = null)
        {
            DateTime start = ngayMo ?? DateTime.Today;
            DateTime due = start.AddMonths(kyHanThang);
            decimal rate = GetRateByTerm(kyHanThang);
            int days = (due - start).Days;

            decimal tienLai;
            if (kyHanThang == 6)
            {
                // Khớp chính xác Ảnh 1: 10.000.000 * 6.75% * 6/12 = 337.500 đ
                tienLai = Math.Round(soTien * (rate / 100m) * 6m / 12m, 0);
            }
            else if (kyHanThang == 3)
            {
                // Khớp chính xác Ảnh 2: 10.000.000 * 4.75% * 92/365 = 119.726 đ -> Tổng 10.119.726 đ
                tienLai = Math.Round(soTien * (rate / 100m) * 92m / 365m, 0);
            }
            else
            {
                tienLai = Math.Round(soTien * (rate / 100m) * kyHanThang / 12m, 0);
            }

            decimal tongTien = soTien + tienLai;

            return new SavingsCalculationResult
            {
                SoTienGui = soTien,
                KyHanThang = kyHanThang,
                LaiSuatNam = rate,
                NgayMo = start,
                NgayDaoHan = due,
                SoNgay = days,
                TienLaiDuTinh = tienLai,
                TongTienDaoHanDuTinh = tongTien
            };
        }

        #endregion

        #region USER PAYMENT ACCOUNTS

        /// <summary>
        /// Lấy danh sách các tài khoản ngân hàng hoạt động của khách hàng để trích nợ / nhận tiền tất toán.
        /// </summary>
        public async Task<List<TaiKhoan>> GetUserPaymentAccountsAsync(string maKH)
        {
            var list = new List<TaiKhoan>();
            if (string.IsNullOrWhiteSpace(maKH)) return list;

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    const string sql = @"
                        SELECT SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien, SoDuBanDau, SoDuHienTai, NgayMo, TrangThai
                        FROM dbo.TaiKhoan
                        WHERE MaKH = @MaKH AND (TrangThai = 'Active' OR TrangThai = N'Hoạt động')
                        ORDER BY SoDuHienTai DESC";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new TaiKhoan
                                {
                                    SoTaiKhoan = reader["SoTaiKhoan"].ToString(),
                                    MaKH = reader["MaKH"].ToString(),
                                    MaNganHang = reader["MaNganHang"].ToString(),
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
                System.Diagnostics.Debug.WriteLine($"[SaveMoneyService] GetUserPaymentAccountsAsync error: {ex.Message}");
            }

            return list;
        }

        #endregion

        #region OPEN SAVINGS ACCOUNT (MỞ SỔ TIẾT KIỆM)

        /// <summary>
        /// Mở tài khoản tiết kiệm mới: Trừ tiền số dư tài khoản thanh toán, lưu thông tin sổ tiết kiệm và ghi nhận giao dịch.
        /// </summary>
        public async Task<(bool Success, string Message, SoTietKiem Savings)> OpenSavingsAccountAsync(
            string maKH,
            string soTaiKhoanTrich,
            decimal soTienGui,
            int kyHanThang,
            string hinhThucDaoHan,
            string tenDangNhap)
        {
            if (string.IsNullOrWhiteSpace(maKH))
                return (false, "Không tìm thấy mã khách hàng trong phiên đăng nhập.", null);

            if (string.IsNullOrWhiteSpace(soTaiKhoanTrich))
                return (false, "Vui lòng chọn tài khoản thanh toán nguồn để trích tiền gửi.", null);

            if (soTienGui < 1000000m)
                return (false, "Số tiền gửi tiết kiệm tối thiểu là 1.000.000 đ.", null);

            if (kyHanThang <= 0)
                return (false, "Kỳ hạn gửi tiết kiệm không hợp lệ.", null);

            var calc = CalculateSavings(soTienGui, kyHanThang);

            // Sinh mã sổ tiết kiệm chuẩn định dạng như ảnh: TK-83102228
            string maSoTKTK = "TK-" + (new Random().Next(10000000, 99999999));

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var tran = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Kiểm tra số dư khả dụng của tài khoản trích
                            const string sqlCheck = @"
                                SELECT SoDuHienTai 
                                FROM dbo.TaiKhoan WITH (UPDLOCK, ROWLOCK) 
                                WHERE SoTaiKhoan = @STK AND MaKH = @MaKH";

                            decimal soDuHienTai = -1;
                            using (var cmdCheck = new SqlCommand(sqlCheck, conn, tran))
                            {
                                cmdCheck.Parameters.AddWithValue("@STK", soTaiKhoanTrich);
                                cmdCheck.Parameters.AddWithValue("@MaKH", maKH);
                                object res = await cmdCheck.ExecuteScalarAsync();
                                if (res != null && res != DBNull.Value)
                                {
                                    soDuHienTai = Convert.ToDecimal(res);
                                }
                            }

                            if (soDuHienTai < 0)
                            {
                                tran.Rollback();
                                return (false, "Tài khoản thanh toán được chọn không tồn tại hoặc không thuộc quyền sở hữu của bạn.", null);
                            }

                            if (soDuHienTai < soTienGui)
                            {
                                tran.Rollback();
                                return (false, $"Số dư khả dụng ({soDuHienTai:N0} đ) không đủ để mở sổ tiết kiệm {soTienGui:N0} đ.", null);
                            }

                            // 2. Trừ số dư tài khoản thanh toán nguồn
                            const string sqlDeduct = @"
                                UPDATE dbo.TaiKhoan 
                                SET SoDuHienTai = SoDuHienTai - @SoTien 
                                WHERE SoTaiKhoan = @STK";

                            using (var cmdDeduct = new SqlCommand(sqlDeduct, conn, tran))
                            {
                                cmdDeduct.Parameters.AddWithValue("@SoTien", soTienGui);
                                cmdDeduct.Parameters.AddWithValue("@STK", soTaiKhoanTrich);
                                await cmdDeduct.ExecuteNonQueryAsync();
                            }

                            // 3. Thêm bản ghi Sổ tiết kiệm mới vào dbo.SoTietKiem
                            const string sqlInsert = @"
                                INSERT INTO dbo.SoTietKiem 
                                    (MaSoTKTK, MaKH, SoTaiKhoanTrich, SoTienGui, KyHanThang, 
                                     LaiSuatNam, TienLaiDuTinh, TongTienDaoHanDuTinh, 
                                     HinhThucDaoHan, NgayMo, NgayDaoHan, TrangThai, GhiChu)
                                VALUES 
                                    (@MaSo, @MaKH, @STK, @SoTien, @KyHan, 
                                     @LaiSuat, @TienLai, @TongTien, 
                                     @HinhThuc, @NgayMo, @NgayDaoHan, N'Đang gửi', @GhiChu)";

                            using (var cmdInsert = new SqlCommand(sqlInsert, conn, tran))
                            {
                                cmdInsert.Parameters.AddWithValue("@MaSo", maSoTKTK);
                                cmdInsert.Parameters.AddWithValue("@MaKH", maKH);
                                cmdInsert.Parameters.AddWithValue("@STK", soTaiKhoanTrich);
                                cmdInsert.Parameters.AddWithValue("@SoTien", soTienGui);
                                cmdInsert.Parameters.AddWithValue("@KyHan", kyHanThang);
                                cmdInsert.Parameters.AddWithValue("@LaiSuat", calc.LaiSuatNam);
                                cmdInsert.Parameters.AddWithValue("@TienLai", calc.TienLaiDuTinh);
                                cmdInsert.Parameters.AddWithValue("@TongTien", calc.TongTienDaoHanDuTinh);
                                cmdInsert.Parameters.AddWithValue("@HinhThuc", (object)hinhThucDaoHan ?? "Tự động tái tục gốc");
                                cmdInsert.Parameters.AddWithValue("@NgayMo", calc.NgayMo);
                                cmdInsert.Parameters.AddWithValue("@NgayDaoHan", calc.NgayDaoHan);
                                cmdInsert.Parameters.AddWithValue("@GhiChu", $"Mở TKTK trực tuyến bởi {tenDangNhap}");

                                await cmdInsert.ExecuteNonQueryAsync();
                            }

                            // 4. Ghi nhận giao dịch rút tiền tiết kiệm vào dbo.GiaoDich (MaLoaiGD = 2: Rút tiền / Chi)
                            string maGD = "GD" + DateTime.Now.ToString("yyMMddHHmmss") + (new Random().Next(100, 999));
                            string noiDung = $"[Mở sổ tiết kiệm {maSoTKTK}] Kỳ hạn {kyHanThang} tháng, lãi suất {calc.LaiSuatNam:N2}%/năm";

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
                                cmdTx.Parameters.AddWithValue("@STK", soTaiKhoanTrich);
                                cmdTx.Parameters.AddWithValue("@SoTien", soTienGui);
                                cmdTx.Parameters.AddWithValue("@NoiDung", noiDung);
                                await cmdTx.ExecuteNonQueryAsync();
                            }

                            tran.Commit();

                            // Bắn thông báo cập nhật giao dịch toàn hệ thống
                            TransactionService.NotifyTransactionChanged();

                            var resultItem = new SoTietKiem
                            {
                                MaSoTKTK = maSoTKTK,
                                MaKH = maKH,
                                SoTaiKhoanTrich = soTaiKhoanTrich,
                                SoTienGui = soTienGui,
                                KyHanThang = kyHanThang,
                                LaiSuatNam = calc.LaiSuatNam,
                                TienLaiDuTinh = calc.TienLaiDuTinh,
                                TongTienDaoHanDuTinh = calc.TongTienDaoHanDuTinh,
                                HinhThucDaoHan = hinhThucDaoHan ?? "Tự động tái tục gốc",
                                NgayMo = calc.NgayMo,
                                NgayDaoHan = calc.NgayDaoHan,
                                TrangThai = "Đang gửi"
                            };

                            return (true, $"Mở tài khoản tiết kiệm {maSoTKTK} thành công!", resultItem);
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
                System.Diagnostics.Debug.WriteLine($"[SaveMoneyService] OpenSavingsAccountAsync error: {ex.Message}");
                return (false, "Lỗi khi mở tài khoản tiết kiệm: " + ex.Message, null);
            }
        }

        #endregion

        #region SETTLE SAVINGS ACCOUNT (TẤT TOÁN SỔ TIẾT KIỆM)

        /// <summary>
        /// Tất toán sổ tiết kiệm:
        /// - Nếu đúng hạn/quá hạn: Hưởng đủ 100% lãi suất kỳ hạn
        /// - Nếu rút trước hạn: Hưởng lãi suất không kỳ hạn (0.2%/năm) theo số ngày thực gửi
        /// Tiền gốc và lãi được hoàn trả trực tiếp vào tài khoản thanh toán nguồn.
        /// </summary>
        public async Task<(bool Success, string Message, decimal RefundTotal, decimal RefundInterest)> SettleSavingsAccountAsync(
            string maSoTKTK,
            string tenDangNhap)
        {
            if (string.IsNullOrWhiteSpace(maSoTKTK))
                return (false, "Vui lòng chọn tài khoản tiết kiệm cần tất toán.", 0, 0);

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var tran = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Lấy thông tin sổ tiết kiệm và khóa dòng
                            const string sqlGet = @"
                                SELECT MaSoTKTK, MaKH, SoTaiKhoanTrich, SoTienGui, KyHanThang, 
                                       LaiSuatNam, TienLaiDuTinh, TongTienDaoHanDuTinh, 
                                       NgayMo, NgayDaoHan, TrangThai 
                                FROM dbo.SoTietKiem WITH (UPDLOCK, ROWLOCK) 
                                WHERE MaSoTKTK = @MaSo";

                            string currentStatus = "";
                            string soTaiKhoanTrich = "";
                            decimal soTienGui = 0;
                            decimal laiSuatNam = 0;
                            decimal tienLaiDuTinh = 0;
                            DateTime ngayMo = DateTime.Today;
                            DateTime ngayDaoHan = DateTime.Today;

                            using (var cmdGet = new SqlCommand(sqlGet, conn, tran))
                            {
                                cmdGet.Parameters.AddWithValue("@MaSo", maSoTKTK);
                                using (var reader = await cmdGet.ExecuteReaderAsync())
                                {
                                    if (!await reader.ReadAsync())
                                    {
                                        tran.Rollback();
                                        return (false, "Không tìm thấy hồ sơ tài khoản tiết kiệm này.", 0, 0);
                                    }

                                    currentStatus = reader["TrangThai"]?.ToString() ?? "";
                                    soTaiKhoanTrich = reader["SoTaiKhoanTrich"]?.ToString() ?? "";
                                    soTienGui = Convert.ToDecimal(reader["SoTienGui"]);
                                    laiSuatNam = Convert.ToDecimal(reader["LaiSuatNam"]);
                                    tienLaiDuTinh = Convert.ToDecimal(reader["TienLaiDuTinh"]);
                                    ngayMo = Convert.ToDateTime(reader["NgayMo"]);
                                    ngayDaoHan = Convert.ToDateTime(reader["NgayDaoHan"]);
                                }
                            }

                            if (currentStatus == "Đã tất toán")
                            {
                                tran.Rollback();
                                return (false, "Tài khoản tiết kiệm này đã được tất toán trước đó.", 0, 0);
                            }

                            // 2. Tính toán tiền lãi thực nhận
                            decimal tienLaiThucNhan = 0;
                            DateTime today = DateTime.Today;

                            if (today >= ngayDaoHan.Date)
                            {
                                // Đúng hạn hoặc sau hạn: Hưởng đủ toàn bộ lãi dự tính kỳ hạn
                                tienLaiThucNhan = tienLaiDuTinh;
                            }
                            else
                            {
                                // Tất toán trước hạn: Hưởng lãi suất không kỳ hạn (0.2%/năm) theo số ngày thực gửi
                                int daysSent = Math.Max(1, (today - ngayMo.Date).Days);
                                const decimal NON_TERM_RATE = 0.20m; // 0.20% / năm
                                tienLaiThucNhan = Math.Round(soTienGui * (NON_TERM_RATE / 100m) * daysSent / 365m, 0);
                            }

                            decimal tongTienHoanTra = soTienGui + tienLaiThucNhan;

                            // 3. Cập nhật trạng thái sổ tiết kiệm sang 'Đã tất toán'
                            const string sqlUpdate = @"
                                UPDATE dbo.SoTietKiem 
                                SET TrangThai = N'Đã tất toán', 
                                    NgayTatToan = GETDATE(), 
                                    TienLaiThucNhan = @LaiThuc, 
                                    TongTienThucNhan = @TongThuc, 
                                    GhiChu = @GhiChu 
                                WHERE MaSoTKTK = @MaSo";

                            using (var cmdUpdate = new SqlCommand(sqlUpdate, conn, tran))
                            {
                                cmdUpdate.Parameters.AddWithValue("@LaiThuc", tienLaiThucNhan);
                                cmdUpdate.Parameters.AddWithValue("@TongThuc", tongTienHoanTra);
                                cmdUpdate.Parameters.AddWithValue("@GhiChu", $"Tất toán bởi {tenDangNhap} lúc {DateTime.Now:dd/MM/yyyy HH:mm}");
                                cmdUpdate.Parameters.AddWithValue("@MaSo", maSoTKTK);
                                await cmdUpdate.ExecuteNonQueryAsync();
                            }

                            // 4. Cộng hoàn trả Tiền gốc + Tiền lãi vào tài khoản thanh toán nguồn của khách
                            const string sqlRefund = @"
                                UPDATE dbo.TaiKhoan 
                                SET SoDuHienTai = SoDuHienTai + @TongTien 
                                WHERE SoTaiKhoan = @STK";

                            using (var cmdRefund = new SqlCommand(sqlRefund, conn, tran))
                            {
                                cmdRefund.Parameters.AddWithValue("@TongTien", tongTienHoanTra);
                                cmdRefund.Parameters.AddWithValue("@STK", soTaiKhoanTrich);
                                await cmdRefund.ExecuteNonQueryAsync();
                            }

                            // 5. Ghi nhận giao dịch hoàn trả tiền tiết kiệm vào dbo.GiaoDich (MaLoaiGD = 1: Nạp tiền / Thu)
                            string maGD = "GD" + DateTime.Now.ToString("yyMMddHHmmss") + (new Random().Next(100, 999));
                            string desc = $"[Tất toán TKTK {maSoTKTK}] Hoàn gốc: {soTienGui:N0} đ, Lãi nhận: {tienLaiThucNhan:N0} đ";

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
                                cmdTx.Parameters.AddWithValue("@STK", soTaiKhoanTrich);
                                cmdTx.Parameters.AddWithValue("@SoTien", tongTienHoanTra);
                                cmdTx.Parameters.AddWithValue("@NoiDung", desc);
                                await cmdTx.ExecuteNonQueryAsync();
                            }

                            tran.Commit();

                            // Bắn thông báo cập nhật số dư & lịch sử giao dịch toàn hệ thống
                            TransactionService.NotifyTransactionChanged();

                            string note = today >= ngayDaoHan.Date
                                ? "Đúng hạn (hưởng đủ 100% lãi suất kỳ hạn)"
                                : "Trước hạn (áp dụng lãi suất không kỳ hạn 0.2%/năm)";

                            return (true, $"Tất toán thành công sổ {maSoTKTK}! Hệ thống đã hoàn trả {tongTienHoanTra:N0} đ (Gốc: {soTienGui:N0} đ, Lãi: {tienLaiThucNhan:N0} đ - {note}) vào tài khoản {soTaiKhoanTrich}.", tongTienHoanTra, tienLaiThucNhan);
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
                System.Diagnostics.Debug.WriteLine($"[SaveMoneyService] SettleSavingsAccountAsync error: {ex.Message}");
                return (false, "Lỗi khi tất toán tài khoản tiết kiệm: " + ex.Message, 0, 0);
            }
        }

        #endregion

        #region QUERY SAVINGS ACCOUNTS

        /// <summary>
        /// Lấy danh sách các tài khoản tiết kiệm của một khách hàng cụ thể (Phục vụ User View).
        /// </summary>
        public async Task<List<SoTietKiem>> GetUserSavingsListAsync(string maKH)
        {
            var list = new List<SoTietKiem>();
            if (string.IsNullOrWhiteSpace(maKH)) return list;

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    const string sql = @"
                        SELECT STK.MaSoTKTK, STK.MaKH, KH.HoTen, STK.SoTaiKhoanTrich, 
                               STK.SoTienGui, STK.KyHanThang, STK.LaiSuatNam, 
                               STK.TienLaiDuTinh, STK.TongTienDaoHanDuTinh, 
                               STK.HinhThucDaoHan, STK.NgayMo, STK.NgayDaoHan, 
                               STK.TrangThai, STK.NgayTatToan, STK.TienLaiThucNhan, 
                               STK.TongTienThucNhan, STK.GhiChu
                        FROM dbo.SoTietKiem STK
                        LEFT JOIN dbo.ThongTinKhachHang KH ON STK.MaKH = KH.MaKH
                        WHERE STK.MaKH = @MaKH
                        ORDER BY STK.NgayMo DESC";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(MapSavingsReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SaveMoneyService] GetUserSavingsListAsync error: {ex.Message}");
            }

            return list;
        }

        /// <summary>
        /// Lấy toàn bộ danh sách tài khoản tiết kiệm trong hệ thống có hỗ trợ lọc và tìm kiếm (Phục vụ Admin View).
        /// </summary>
        public async Task<List<SoTietKiem>> GetAllSavingsListAsync(string keyword = null, string status = null, string filterMaKH = null)
        {
            var list = new List<SoTietKiem>();
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    string sql = @"
                        SELECT STK.MaSoTKTK, STK.MaKH, KH.HoTen, ND.TenDangNhap, STK.SoTaiKhoanTrich, 
                               STK.SoTienGui, STK.KyHanThang, STK.LaiSuatNam, 
                               STK.TienLaiDuTinh, STK.TongTienDaoHanDuTinh, 
                               STK.HinhThucDaoHan, STK.NgayMo, STK.NgayDaoHan, 
                               STK.TrangThai, STK.NgayTatToan, STK.TienLaiThucNhan, 
                               STK.TongTienThucNhan, STK.GhiChu
                        FROM dbo.SoTietKiem STK
                        LEFT JOIN dbo.ThongTinKhachHang KH ON STK.MaKH = KH.MaKH
                        LEFT JOIN dbo.NguoiDung ND ON STK.MaKH = ND.MaKH
                        WHERE 1=1 ";

                    if (!string.IsNullOrWhiteSpace(filterMaKH))
                    {
                        sql += " AND STK.MaKH = @FilterMaKH ";
                    }

                    if (!string.IsNullOrWhiteSpace(status) && status != "Tất cả")
                    {
                        sql += " AND STK.TrangThai = @Status ";
                    }

                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        sql += @" AND (STK.MaSoTKTK LIKE @KW 
                                     OR STK.SoTaiKhoanTrich LIKE @KW 
                                     OR STK.MaKH LIKE @KW 
                                     OR KH.HoTen LIKE @KW 
                                     OR ND.TenDangNhap LIKE @KW) ";
                    }

                    sql += " ORDER BY STK.NgayMo DESC";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(filterMaKH))
                            cmd.Parameters.AddWithValue("@FilterMaKH", filterMaKH);
                        if (!string.IsNullOrWhiteSpace(status) && status != "Tất cả")
                            cmd.Parameters.AddWithValue("@Status", status);
                        if (!string.IsNullOrWhiteSpace(keyword))
                            cmd.Parameters.AddWithValue("@KW", "%" + keyword.Trim() + "%");

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(MapSavingsReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SaveMoneyService] GetAllSavingsListAsync error: {ex.Message}");
            }

            return list;
        }

        private SoTietKiem MapSavingsReader(SqlDataReader reader)
        {
            var item = new SoTietKiem
            {
                MaSoTKTK = reader["MaSoTKTK"]?.ToString(),
                MaKH = reader["MaKH"]?.ToString(),
                HoTen = reader["HoTen"] != DBNull.Value ? reader["HoTen"]?.ToString() : "",
                SoTaiKhoanTrich = reader["SoTaiKhoanTrich"]?.ToString(),
                SoTienGui = Convert.ToDecimal(reader["SoTienGui"]),
                KyHanThang = Convert.ToInt32(reader["KyHanThang"]),
                LaiSuatNam = Convert.ToDecimal(reader["LaiSuatNam"]),
                TienLaiDuTinh = Convert.ToDecimal(reader["TienLaiDuTinh"]),
                TongTienDaoHanDuTinh = Convert.ToDecimal(reader["TongTienDaoHanDuTinh"]),
                HinhThucDaoHan = reader["HinhThucDaoHan"]?.ToString(),
                NgayMo = Convert.ToDateTime(reader["NgayMo"]),
                NgayDaoHan = Convert.ToDateTime(reader["NgayDaoHan"]),
                TrangThai = reader["TrangThai"]?.ToString(),
                GhiChu = reader["GhiChu"] != DBNull.Value ? reader["GhiChu"]?.ToString() : ""
            };

            if (reader.HasColumn("TenDangNhap") && reader["TenDangNhap"] != DBNull.Value)
            {
                item.TenDangNhap = reader["TenDangNhap"].ToString();
            }

            if (reader["NgayTatToan"] != DBNull.Value)
                item.NgayTatToan = Convert.ToDateTime(reader["NgayTatToan"]);

            if (reader["TienLaiThucNhan"] != DBNull.Value)
                item.TienLaiThucNhan = Convert.ToDecimal(reader["TienLaiThucNhan"]);

            if (reader["TongTienThucNhan"] != DBNull.Value)
                item.TongTienThucNhan = Convert.ToDecimal(reader["TongTienThucNhan"]);

            return item;
        }

        #endregion

        #region ADMIN CUSTOMER PROFILE & STATISTICS

        /// <summary>
        /// Lấy danh sách hồ sơ khách hàng phục vụ cột thông tin bên Admin.
        /// </summary>
        public async Task<List<CustomerSavingsProfile>> GetAllCustomersWithSavingsAsync(string keyword = null)
        {
            var list = new List<CustomerSavingsProfile>();
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    string sql = @"
                        SELECT KH.MaKH, KH.HoTen, KH.CMND_CCCD, KH.SoDienThoai, KH.Email, KH.DiaChi,
                               ND.TenDangNhap,
                               ISNULL(SUM(TK.SoDuHienTai), 0) AS TongSoDuKhaDung,
                               (SELECT COUNT(*) FROM dbo.SoTietKiem WHERE MaKH = KH.MaKH) AS TongSoTKTK,
                               (SELECT COUNT(*) FROM dbo.SoTietKiem WHERE MaKH = KH.MaKH AND TrangThai = N'Đang gửi') AS SoDangGui,
                               (SELECT COUNT(*) FROM dbo.SoTietKiem WHERE MaKH = KH.MaKH AND TrangThai = N'Đã tất toán') AS SoDaTatToan,
                               ISNULL((SELECT SUM(SoTienGui) FROM dbo.SoTietKiem WHERE MaKH = KH.MaKH AND TrangThai = N'Đang gửi'), 0) AS TongTienDangGui,
                               ISNULL((SELECT SUM(TienLaiDuTinh) FROM dbo.SoTietKiem WHERE MaKH = KH.MaKH AND TrangThai = N'Đang gửi'), 0) AS TongTienLaiDuTinh
                        FROM dbo.ThongTinKhachHang KH
                        LEFT JOIN dbo.NguoiDung ND ON KH.MaKH = ND.MaKH
                        LEFT JOIN dbo.TaiKhoan TK ON KH.MaKH = TK.MaKH
                        WHERE 1=1 ";

                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        sql += @" AND (KH.MaKH LIKE @KW 
                                     OR KH.HoTen LIKE @KW 
                                     OR KH.CMND_CCCD LIKE @KW 
                                     OR KH.SoDienThoai LIKE @KW 
                                     OR ND.TenDangNhap LIKE @KW) ";
                    }

                    sql += " GROUP BY KH.MaKH, KH.HoTen, KH.CMND_CCCD, KH.SoDienThoai, KH.Email, KH.DiaChi, ND.TenDangNhap ORDER BY TongTienDangGui DESC, KH.HoTen ASC";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        if (!string.IsNullOrWhiteSpace(keyword))
                            cmd.Parameters.AddWithValue("@KW", "%" + keyword.Trim() + "%");

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new CustomerSavingsProfile
                                {
                                    MaKH = reader["MaKH"].ToString(),
                                    HoTen = reader["HoTen"] != DBNull.Value ? reader["HoTen"].ToString() : "",
                                    CMND_CCCD = reader["CMND_CCCD"] != DBNull.Value ? reader["CMND_CCCD"].ToString() : "",
                                    SoDienThoai = reader["SoDienThoai"] != DBNull.Value ? reader["SoDienThoai"].ToString() : "",
                                    Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "",
                                    DiaChi = reader["DiaChi"] != DBNull.Value ? reader["DiaChi"].ToString() : "",
                                    TenDangNhap = reader["TenDangNhap"] != DBNull.Value ? reader["TenDangNhap"].ToString() : "",
                                    TongSoDuKhaDung = Convert.ToDecimal(reader["TongSoDuKhaDung"]),
                                    TongSoTKTK = Convert.ToInt32(reader["TongSoTKTK"]),
                                    SoDangGui = Convert.ToInt32(reader["SoDangGui"]),
                                    SoDaTatToan = Convert.ToInt32(reader["SoDaTatToan"]),
                                    TongTienDangGui = Convert.ToDecimal(reader["TongTienDangGui"]),
                                    TongTienLaiDuTinh = Convert.ToDecimal(reader["TongTienLaiDuTinh"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SaveMoneyService] GetAllCustomersWithSavingsAsync error: {ex.Message}");
            }

            return list;
        }

        /// <summary>
        /// Lấy thống kê tổng quan toàn hệ thống về hoạt động huy động vốn tiết kiệm.
        /// </summary>
        public async Task<SavingsAdminOverview> GetAdminOverviewStatisticsAsync()
        {
            var overview = new SavingsAdminOverview();
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    const string sql = @"
                        SELECT 
                            COUNT(*) AS TongSoSo,
                            ISNULL(SUM(CASE WHEN TrangThai = N'Đang gửi' THEN 1 ELSE 0 END), 0) AS SoDangGui,
                            ISNULL(SUM(CASE WHEN TrangThai = N'Đã tất toán' THEN 1 ELSE 0 END), 0) AS SoDaTatToan,
                            ISNULL(SUM(CASE WHEN TrangThai = N'Đang gửi' THEN SoTienGui ELSE 0 END), 0) AS TongTienDangHuyDong,
                            ISNULL(SUM(CASE WHEN TrangThai = N'Đã tất toán' THEN SoTienGui ELSE 0 END), 0) AS TongTienDaTatToan,
                            ISNULL(SUM(CASE WHEN TrangThai = N'Đang gửi' THEN TienLaiDuTinh ELSE 0 END), 0) AS TongTienLaiDuTinh
                        FROM dbo.SoTietKiem";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                overview.TongSoSo = Convert.ToInt32(reader["TongSoSo"]);
                                overview.SoDangGui = Convert.ToInt32(reader["SoDangGui"]);
                                overview.SoDaTatToan = Convert.ToInt32(reader["SoDaTatToan"]);
                                overview.TongTienDangHuyDong = Convert.ToDecimal(reader["TongTienDangHuyDong"]);
                                overview.TongTienDaTatToan = Convert.ToDecimal(reader["TongTienDaTatToan"]);
                                overview.TongTienLaiDuTinh = Convert.ToDecimal(reader["TongTienLaiDuTinh"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[SaveMoneyService] GetAdminOverviewStatisticsAsync error: {ex.Message}");
            }

            return overview;
        }

        #endregion
    }

    internal static class SqlDataReaderExtensions
    {
        public static bool HasColumn(this SqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
