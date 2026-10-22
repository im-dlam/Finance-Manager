using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Dịch vụ quản lý Giao dịch tài chính (Financial Transaction Service).
    /// Đây là trung tâm điều phối toàn bộ các nghiệp vụ tiền tệ trong hệ thống ngân hàng:
    /// - Quản lý giao dịch Thu/Chi/Chuyển tiền với cơ chế phân quyền bảo mật:
    ///   + Quản trị viên (Admin): Giám sát toàn bộ các luồng tiền và giao dịch trên toàn hệ thống.
    ///   + Khách hàng (Member): Chỉ xem và thực hiện giao dịch trên các tài khoản thuộc quyền sở hữu của chính mình.
    /// - Thực hiện chuyển khoản hai chiều (Transfer Money) trong một giao tác an toàn <see cref="SqlTransaction"/> (ACID).
    /// - Kiểm tra số dư với cơ chế khóa dòng bi quan (Pessimistic Locking: WITH UPDLOCK) chống chi vượt số dư (Overdraft Protection).
    /// - Tự động quy đổi tỷ giá ngoại tệ (VND, USD) theo thời gian thực.
    /// </summary>
    public class TransactionService
    {
        /// <summary>
        /// Sự kiện phát tín hiệu khi có bất kỳ giao dịch tài chính nào hoàn tất thành công.
        /// Giúp các Form giao diện (Dashboard, History, Account) tự động nạp lại số dư và lịch sử mới nhất.
        /// </summary>
        public static event Action OnTransactionChanged;

        /// <summary>
        /// Kích hoạt sự kiện OnTransactionChanged an toàn có bẫy lỗi.
        /// </summary>
        public static void NotifyTransactionChanged()
        {
            try
            {
                OnTransactionChanged?.Invoke();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[TransactionService] Notify error: {ex.Message}");
            }
        }

        /// <summary>
        /// Khởi tạo dịch vụ TransactionService và đảm bảo cấu trúc bảng GiaoDich đã sẵn sàng.
        /// </summary>
        public TransactionService()
        {
            EnsureTableSchema();
        }

        /// <summary>Cờ đánh dấu cấu trúc bảng đã được kiểm tra kiểm định.</summary>
        private static bool _schemaChecked = false;
        private static readonly object _schemaLock = new object();

        /// <summary>
        /// Kiểm tra và tự động cập nhật các cột mở rộng cho bảng dbo.GiaoDich (LoaiTien, SoTienGoc, MaLoaiTien)
        /// đồng thời tự động bổ sung giao dịch nạp tiền khởi tạo cho các tài khoản có số dư ban đầu nhưng chưa có bản ghi giao dịch.
        /// </summary>
        public void EnsureTableSchema()
        {
            if (_schemaChecked) return;
            lock (_schemaLock)
            {
                if (_schemaChecked) return;
                try
                {
                    using (var conn = new SqlConnection(GetConnectionString()))
                    {
                        conn.Open();
                        const string sql = @"
                            IF OBJECT_ID(N'dbo.GiaoDich', N'U') IS NOT NULL
                            BEGIN
                                IF COL_LENGTH(N'dbo.GiaoDich', N'LoaiTien') IS NULL
                                BEGIN
                                    ALTER TABLE dbo.GiaoDich ADD LoaiTien VARCHAR(10) NULL CONSTRAINT DF_GiaoDich_LoaiTien DEFAULT 'VND';
                                END;
                                IF COL_LENGTH(N'dbo.GiaoDich', N'SoTienGoc') IS NULL
                                BEGIN
                                    ALTER TABLE dbo.GiaoDich ADD SoTienGoc DECIMAL(18,2) NULL;
                                END;
                                IF COL_LENGTH(N'dbo.GiaoDich', N'MaLoaiTien') IS NULL
                                BEGIN
                                    ALTER TABLE dbo.GiaoDich ADD MaLoaiTien VARCHAR(10) NULL;
                                END;

                                -- Tự động bổ sung giao dịch khởi tạo cho các tài khoản có số dư ban đầu nhưng chưa có giao dịch
                                INSERT INTO dbo.GiaoDich 
                                    (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, SoTienGoc, 
                                     MaLoaiTien, LoaiTien, NgayGiaoDich, NoiDung, TrangThai)
                                SELECT 
                                    'GD' + CONVERT(VARCHAR(8), TK.NgayMo, 112) + RIGHT('000000' + CAST(ABS(CHECKSUM(NEWID())) % 1000000 AS VARCHAR(6)), 6),
                                    TK.SoTaiKhoan,
                                    1, -- Nạp tiền / Thu
                                    CASE WHEN TK.MaLoaiTien = 'VND' OR LT.QuyDoi IS NULL THEN TK.SoDuBanDau ELSE TK.SoDuBanDau * LT.QuyDoi END,
                                    TK.SoDuBanDau,
                                    ISNULL(TK.MaLoaiTien, 'VND'),
                                    ISNULL(TK.MaLoaiTien, 'VND'),
                                    TK.NgayMo,
                                    N'Ngân hàng khởi tạo tài khoản',
                                    'Success'
                                FROM dbo.TaiKhoan TK
                                LEFT JOIN dbo.LoaiTien LT ON TK.MaLoaiTien = LT.MaLoaiTien
                                WHERE TK.SoDuBanDau > 0
                                  AND NOT EXISTS (SELECT 1 FROM dbo.GiaoDich GD WHERE GD.SoTaiKhoan = TK.SoTaiKhoan);
                            END;";
                        using (var cmd = new SqlCommand(sql, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    _schemaChecked = true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"[TransactionService] EnsureTableSchema error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Tạo chuỗi kết nối tới CSDL SQL Server dựa trên cấu hình database.ini.
        /// </summary>
        private string GetConnectionString()
        {
            return DatabaseService.BuildConnectionString(DatabaseConfigManager.Load());
        }

        // ============================================================
        //  CƠ CHẾ LỌC PHÂN QUYỀN TRUY VẤN (DATA SECURITY GUARDS)
        // ============================================================

        /// <summary>
        /// Sinh mệnh đề WHERE lọc dữ liệu theo phân quyền của người dùng hiện tại:
        /// - Admin: Chuỗi rỗng "" (xem tất cả).
        /// - Member: Lọc theo MaKH hoặc các tài khoản gắn với Username của người dùng.
        /// </summary>
        /// <param name="tableAlias">Bí danh đại diện bảng TaiKhoan trong câu lệnh SQL.</param>
        /// <returns>Mệnh đề SQL WHERE lọc người dùng.</returns>
        private static string GetUserFilter(string tableAlias = "TK")
        {
            if (CurrentSession.IsAdmin) return "";
            return $" AND ({tableAlias}.MaKH = @MaKH OR {tableAlias}.MaKH IN (SELECT MaKH FROM dbo.NguoiDung WHERE MaNguoiDung = @MaND OR TenDangNhap = @UserLogin))";
        }

        /// <summary>
        /// Gán các tham số phân quyền (@MaKH, @MaND, @UserLogin) vào SqlCommand nếu người dùng không phải Quản trị viên.
        /// </summary>
        /// <param name="cmd">Đối tượng SqlCommand cần gắn tham số.</param>
        private static void AddUserParam(SqlCommand cmd)
        {
            if (!CurrentSession.IsAdmin)
            {
                cmd.Parameters.AddWithValue("@MaKH", CurrentSession.MaKH ?? "");
                cmd.Parameters.AddWithValue("@MaND", CurrentSession.MaNguoiDung);
                cmd.Parameters.AddWithValue("@UserLogin", CurrentSession.TenDangNhap ?? "");
            }
        }

        // ============================================================
        //  1. TRUY VẤN TẤT CẢ GIAO DỊCH (THEO PHÂN QUYỀN)
        // ============================================================

        /// <summary>
        /// Lấy toàn bộ danh sách lịch sử giao dịch phù hợp với phân quyền của người dùng hiện tại.
        /// </summary>
        /// <returns>DataTable chứa danh sách giao dịch được sắp xếp giảm dần theo ngày giao dịch.</returns>
        public DataTable GetAllTransactions()
        {
            EnsureTableSchema();
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = @"
                    SELECT 
                        G.NgayGiaoDich, 
                        G.NoiDung, 
                        G.SoTaiKhoan, 
                        CASE 
                            WHEN L.PhanLoai = 'INCOME' THEN N'Thu' 
                            WHEN L.PhanLoai = 'EXPENSE' THEN N'Chi' 
                            ELSE L.TenLoaiGD 
                        END AS Loai, 
                        G.SoTien, 
                        ISNULL(G.SoTienGoc, G.SoTien) AS SoTienGoc, 
                        ISNULL(NULLIF(G.LoaiTien, ''), ISNULL(NULLIF(G.MaLoaiTien, ''), 'VND')) AS LoaiTien, 
                        ISNULL(NULLIF(G.MaLoaiTien, ''), ISNULL(NULLIF(G.LoaiTien, ''), 'VND')) AS MaLoaiTien, 
                        L.PhanLoai,
                        CASE 
                            WHEN G.NoiDung LIKE N'%khởi tạo%' THEN N'Khởi tạo'
                            ELSE L.TenLoaiGD 
                        END AS ThaoTac, 
                        G.TrangThai 
                    FROM dbo.GiaoDich G
                    INNER JOIN dbo.LoaiGiaoDich L ON G.MaLoaiGD = L.MaLoaiGD
                    INNER JOIN dbo.TaiKhoan TK    ON TK.SoTaiKhoan = G.SoTaiKhoan
                    WHERE 1 = 1" + GetUserFilter() + @"
                    ORDER BY G.NgayGiaoDich DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    AddUserParam(cmd);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        adapter.Fill(dt);
                }
            }
            return dt;
        }

        // ============================================================
        //  2. LẤY THÔNG TIN LOẠI TIỀN CỦA TÀI KHOẢN
        // ============================================================

        /// <summary>
        /// Lấy thông tin tiền tệ và tỷ giá quy đổi của một tài khoản cụ thể.
        /// </summary>
        /// <param name="soTaiKhoan">Số tài khoản cần tra cứu.</param>
        /// <returns>Đối tượng <see cref="CurrencyInfo"/> nếu tìm thấy; ngược lại là null.</returns>
        public CurrencyInfo GetAccountCurrency(string soTaiKhoan)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                const string sql = @"
                    SELECT LT.MaLoaiTien, LT.TenLoaiTien, LT.QuyDoi
                    FROM   dbo.TaiKhoan TK
                    INNER JOIN dbo.LoaiTien LT ON TK.MaLoaiTien = LT.MaLoaiTien
                    WHERE  TK.SoTaiKhoan = @SoTaiKhoan";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string code = reader["MaLoaiTien"].ToString();
                            decimal qd = Convert.ToDecimal(reader["QuyDoi"]);
                            if (code.Equals("USD", StringComparison.OrdinalIgnoreCase) && qd <= 1)
                            {
                                qd = 25400m;
                            }
                            return new CurrencyInfo
                            {
                                MaLoaiTien = code,
                                TenLoaiTien = reader["TenLoaiTien"].ToString(),
                                QuyDoi = qd
                            };
                        }
                    }
                }
            }
            return null;
        }

        // ============================================================
        //  3. TÌM KIẾM VÀ LỌC GIAO DỊCH
        // ============================================================

        /// <summary>
        /// Tìm kiếm giao dịch kết hợp bộ lọc đa tiêu chí (Từ khóa, Số tài khoản, Loại giao dịch) có phân quyền.
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm trên nội dung, số tài khoản hoặc mã giao dịch.</param>
        /// <param name="soTaiKhoan">Bộ lọc theo số tài khoản ("Tất cả" hoặc số cụ thể).</param>
        /// <param name="tenLoai">Bộ lọc theo tên loại giao dịch ("Tất cả" hoặc tên loại cụ thể).</param>
        /// <returns>DataTable chứa kết quả tìm kiếm.</returns>
        public DataTable SearchTransactions(string keyword, string soTaiKhoan = "", string tenLoai = "")
        {
            EnsureTableSchema();
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = @"
                    SELECT 
                        G.NgayGiaoDich, 
                        G.NoiDung, 
                        G.SoTaiKhoan, 
                        CASE 
                            WHEN L.PhanLoai = 'INCOME' THEN N'Thu' 
                            WHEN L.PhanLoai = 'EXPENSE' THEN N'Chi' 
                            ELSE L.TenLoaiGD 
                        END AS Loai, 
                        G.SoTien, 
                        ISNULL(G.SoTienGoc, G.SoTien) AS SoTienGoc, 
                        ISNULL(NULLIF(G.LoaiTien, ''), ISNULL(NULLIF(G.MaLoaiTien, ''), 'VND')) AS LoaiTien, 
                        ISNULL(NULLIF(G.MaLoaiTien, ''), ISNULL(NULLIF(G.LoaiTien, ''), 'VND')) AS MaLoaiTien, 
                        L.PhanLoai,
                        CASE 
                            WHEN G.NoiDung LIKE N'%khởi tạo%' THEN N'Khởi tạo'
                            ELSE L.TenLoaiGD 
                        END AS ThaoTac, 
                        G.TrangThai 
                    FROM dbo.GiaoDich G
                    INNER JOIN dbo.LoaiGiaoDich L ON G.MaLoaiGD = L.MaLoaiGD
                    INNER JOIN dbo.TaiKhoan TK    ON TK.SoTaiKhoan = G.SoTaiKhoan
                    WHERE (G.NoiDung LIKE @Keyword 
                        OR G.SoTaiKhoan LIKE @Keyword 
                        OR G.MaGiaoDich LIKE @Keyword)
                        " + GetUserFilter();

                if (!string.IsNullOrEmpty(soTaiKhoan) && soTaiKhoan != "Tất cả")
                    sql += " AND G.SoTaiKhoan = @SoTaiKhoan";

                if (!string.IsNullOrEmpty(tenLoai) && tenLoai != "Tất cả")
                    sql += " AND L.TenLoaiGD = @TenLoai";

                sql += " ORDER BY G.NgayGiaoDich DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    AddUserParam(cmd);

                    if (!string.IsNullOrEmpty(soTaiKhoan) && soTaiKhoan != "Tất cả")
                        cmd.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);

                    if (!string.IsNullOrEmpty(tenLoai) && tenLoai != "Tất cả")
                        cmd.Parameters.AddWithValue("@TenLoai", tenLoai);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        adapter.Fill(dt);
                }
            }
            return dt;
        }

        // ============================================================
        //  4. THÊM GIAO DỊCH ĐƠN TÀI KHOẢN (NẠP / RÚT / CHI TIÊU)
        // ============================================================

        /// <summary>
        /// Thực hiện thêm mới một giao dịch tài chính đơn tài khoản (Nạp tiền, Rút tiền) trong một SqlTransaction:
        /// - BƯỚC 1: Thêm bản ghi vào bảng dbo.GiaoDich.
        /// - BƯỚC 2: Kiểm tra phân loại giao dịch (INCOME là Thu, EXPENSE là Chi).
        /// - BƯỚC 3: Nếu là Chi (EXPENSE), khóa dòng kiểm tra số dư và chặn giao dịch nếu không đủ tiền.
        /// - BƯỚC 4: Cập nhật tăng/giảm số dư SoDuHienTai của tài khoản tương ứng.
        /// - BƯỚC 5: Commit giao tác và thông báo cập nhật giao diện.
        /// </summary>
        /// <param name="maGD">Mã giao dịch duy nhất.</param>
        /// <param name="soTaiKhoan">Số tài khoản thực hiện giao dịch.</param>
        /// <param name="maLoaiGD">Mã loại giao dịch (1: Nạp, 2: Rút, 3: Chuyển tiền,...).</param>
        /// <param name="soTienGoc">Số tiền theo đơn vị tiền gốc của giao dịch.</param>
        /// <param name="loaiTien">Mã loại tiền tệ.</param>
        /// <param name="soTienVND">Số tiền đã quy đổi ra VNĐ.</param>
        /// <param name="noiDung">Nội dung chi tiết của giao dịch.</param>
        /// <param name="ngayGD">Thời điểm thực hiện giao dịch.</param>
        /// <param name="trangThai">Trạng thái giao dịch ("Success" hoặc "Hoàn tất").</param>
        /// <returns>True nếu giao dịch thành công.</returns>
        public bool AddTransaction(string maGD, string soTaiKhoan, int maLoaiGD,
            decimal soTienGoc, string loaiTien, decimal soTienVND, string noiDung,
            DateTime ngayGD, string trangThai)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();

                // Bắt đầu một Transaction để đảm bảo tính nguyên tử giữa bảng GiaoDich và TaiKhoan
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 4.1. Thêm bản ghi mới vào bảng GiaoDich
                        const string sqlInsert = @"
                            INSERT INTO dbo.GiaoDich 
                                (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, SoTienGoc, 
                                 MaLoaiTien, LoaiTien, NgayGiaoDich, NoiDung, TrangThai)
                            VALUES 
                                (@MaGD, @SoTaiKhoan, @MaLoai, @SoTienVND, @SoTienGoc, 
                                 @LoaiTien, @LoaiTien, @Ngay, @NoiDung, @TrangThai)";

                        using (SqlCommand cmd = new SqlCommand(sqlInsert, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaGD", maGD);
                            cmd.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                            cmd.Parameters.AddWithValue("@MaLoai", maLoaiGD);
                            cmd.Parameters.AddWithValue("@SoTienVND", soTienVND);
                            cmd.Parameters.AddWithValue("@SoTienGoc", soTienGoc);
                            cmd.Parameters.AddWithValue("@LoaiTien", loaiTien);
                            cmd.Parameters.AddWithValue("@Ngay", ngayGD);
                            cmd.Parameters.AddWithValue("@NoiDung", noiDung);
                            cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                            cmd.ExecuteNonQuery();
                        }

                        // 4.2. Tra cứu phân loại giao dịch (INCOME / EXPENSE)
                        string phanLoai = "";
                        const string getLoaiSql = "SELECT PhanLoai FROM dbo.LoaiGiaoDich WHERE MaLoaiGD = @MaLoai";
                        using (SqlCommand cmd = new SqlCommand(getLoaiSql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaLoai", maLoaiGD);
                            phanLoai = cmd.ExecuteScalar()?.ToString() ?? "";
                        }

                        // 4.3. Cập nhật số dư tài khoản nếu giao dịch thành công
                        if (trangThai == "Success" || trangThai == "Hoàn tất")
                        {
                            string curAccount = "VND";
                            decimal qdAccount = 1m;
                            const string sqlCur = @"SELECT LT.MaLoaiTien, LT.QuyDoi 
                                                    FROM dbo.TaiKhoan TK 
                                                    INNER JOIN dbo.LoaiTien LT ON TK.MaLoaiTien = LT.MaLoaiTien 
                                                    WHERE TK.SoTaiKhoan = @STK";
                            using (SqlCommand cmdCur = new SqlCommand(sqlCur, conn, transaction))
                            {
                                cmdCur.Parameters.AddWithValue("@STK", soTaiKhoan);
                                using (SqlDataReader rCur = cmdCur.ExecuteReader())
                                {
                                    if (rCur.Read())
                                    {
                                        curAccount = rCur["MaLoaiTien"]?.ToString() ?? "VND";
                                        qdAccount = Convert.ToDecimal(rCur["QuyDoi"]);
                                        if (curAccount.Equals("USD", StringComparison.OrdinalIgnoreCase) && qdAccount <= 1)
                                            qdAccount = 25400m;
                                    }
                                }
                            }

                            decimal amountAdjust = (curAccount == "VND")
                                ? soTienVND
                                : (curAccount.Equals(loaiTien, StringComparison.OrdinalIgnoreCase)
                                    ? soTienGoc
                                    : (soTienVND / (qdAccount > 0 ? qdAccount : 1m)));

                            string sqlUpdate = "";
                            if (phanLoai == "INCOME")
                            {
                                sqlUpdate = @"UPDATE dbo.TaiKhoan 
                                              SET SoDuHienTai = SoDuHienTai + @SoTien 
                                              WHERE SoTaiKhoan = @SoTaiKhoan";
                            }
                            else if (phanLoai == "EXPENSE")
                            {
                                // Đối với Member: Kiểm tra khóa dòng tuyệt đối không cho phép chi tiêu vượt quá số dư hiện có
                                const string sqlCheckBal = @"
                                    SELECT SoDuHienTai 
                                    FROM dbo.TaiKhoan WITH (UPDLOCK) 
                                    WHERE SoTaiKhoan = @SoTaiKhoan";
                                decimal curBal = 0m;
                                using (SqlCommand cmdCheckBal = new SqlCommand(sqlCheckBal, conn, transaction))
                                {
                                    cmdCheckBal.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                                    object objBal = cmdCheckBal.ExecuteScalar();
                                    if (objBal != null && objBal != DBNull.Value)
                                    {
                                        curBal = Convert.ToDecimal(objBal);
                                    }
                                }

                                if (!CurrentSession.IsAdmin && curBal < amountAdjust)
                                {
                                    throw new Exception($"Số dư tài khoản ({curBal:N0} {curAccount}) không đủ để thực hiện giao dịch ({amountAdjust:N0} {curAccount})!");
                                }

                                sqlUpdate = @"UPDATE dbo.TaiKhoan 
                                              SET SoDuHienTai = SoDuHienTai - @SoTien 
                                              WHERE SoTaiKhoan = @SoTaiKhoan";
                            }

                            if (!string.IsNullOrEmpty(sqlUpdate))
                            {
                                using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn, transaction))
                                {
                                    cmdUpdate.Parameters.AddWithValue("@SoTien", amountAdjust);
                                    cmdUpdate.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                                    cmdUpdate.ExecuteNonQuery();
                                }
                            }
                        }

                        // Hoàn tất Transaction
                        transaction.Commit();
                        NotifyTransactionChanged();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // ============================================================
        //  4.1. CHUYỂN KHOẢN HAI CHIỀU (TWO-PARTY TRANSFER ENGINE)
        // ============================================================

        /// <summary>
        /// Thực hiện quy trình chuyển khoản liên tài khoản 2 bên trong một giao tác SQL Transaction toàn vẹn (ACID):
        /// - Bên gửi: Khóa dòng kiểm tra số dư, trích tiền và ghi bản ghi giao dịch Chi (MaLoaiGD = 3).
        /// - Bên nhận (nếu tồn tại trong hệ thống): Cộng tiền tương ứng và ghi bản ghi giao dịch Thu (MaLoaiGD = 4).
        /// - Nếu bất kỳ bước nào lỗi (không đủ số dư, tài khoản khóa), toàn bộ giao dịch sẽ Rollback hoàn hảo.
        /// </summary>
        /// <param name="maGD">Mã giao dịch gốc.</param>
        /// <param name="tkGui">Số tài khoản nguồn trích nợ.</param>
        /// <param name="tkNhan">Số tài khoản thụ hưởng nhận tiền.</param>
        /// <param name="soTienGoc">Số tiền gốc theo loại tiền tệ chuyển.</param>
        /// <param name="loaiTien">Loại tiền tệ chuyển khoản.</param>
        /// <param name="soTienVND">Số tiền tương đương quy đổi sang VNĐ.</param>
        /// <param name="noiDungGui">Nội dung ghi chú phía người gửi.</param>
        /// <param name="noiDungNhan">Nội dung thông báo phía người nhận.</param>
        /// <param name="ngayGD">Ngày giờ phát sinh giao dịch.</param>
        /// <param name="trangThai">Trạng thái giao dịch.</param>
        /// <returns>True nếu chuyển khoản thành công cả hai chiều.</returns>
        public bool TransferMoney(string maGD, string tkGui, string tkNhan,
            decimal soTienGoc, string loaiTien, decimal soTienVND,
            string noiDungGui, string noiDungNhan, DateTime ngayGD, string trangThai)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // BƯỚC 1: Khóa dòng và kiểm tra số dư tài khoản gửi
                        decimal soDuGui = 0;
                        string curGui = "VND";
                        decimal quyDoiGui = 1;
                        const string sqlCheckSender = @"
                            SELECT TK.SoDuHienTai, LT.MaLoaiTien, LT.QuyDoi 
                            FROM dbo.TaiKhoan TK WITH (UPDLOCK)
                            INNER JOIN dbo.LoaiTien LT ON TK.MaLoaiTien = LT.MaLoaiTien
                            WHERE TK.SoTaiKhoan = @TkGui";
                        using (SqlCommand cmd = new SqlCommand(sqlCheckSender, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@TkGui", tkGui);
                            using (SqlDataReader r = cmd.ExecuteReader())
                            {
                                if (r.Read())
                                {
                                    soDuGui = Convert.ToDecimal(r["SoDuHienTai"]);
                                    curGui = r["MaLoaiTien"].ToString();
                                    quyDoiGui = Convert.ToDecimal(r["QuyDoi"]);
                                    if (curGui.Equals("USD", StringComparison.OrdinalIgnoreCase) && quyDoiGui <= 1)
                                        quyDoiGui = 25400m;
                                }
                                else
                                {
                                    throw new Exception("Không tìm thấy tài khoản chuyển tiền!");
                                }
                            }
                        }

                        // Tính số tiền cần khấu trừ theo loại tiền của tài khoản gửi
                        decimal amountDeductSender = (curGui == "VND") 
                            ? soTienVND 
                            : (curGui.Equals(loaiTien, StringComparison.OrdinalIgnoreCase) 
                                ? soTienGoc 
                                : (soTienVND / (quyDoiGui > 0 ? quyDoiGui : 1)));
                        if (!CurrentSession.IsAdmin && soDuGui < amountDeductSender)
                        {
                            throw new Exception($"Số dư tài khoản chuyển ({soDuGui:N0} {curGui}) không đủ để thực hiện giao dịch!");
                        }

                        // BƯỚC 2: Khấu trừ tiền tài khoản gửi
                        const string sqlDeductSender = @"
                            UPDATE dbo.TaiKhoan 
                            SET SoDuHienTai = SoDuHienTai - @Amount 
                            WHERE SoTaiKhoan = @TkGui";
                        using (SqlCommand cmd = new SqlCommand(sqlDeductSender, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Amount", amountDeductSender);
                            cmd.Parameters.AddWithValue("@TkGui", tkGui);
                            cmd.ExecuteNonQuery();
                        }

                        // BƯỚC 3: Ghi nhận giao dịch phía bên gửi (Chi - MaLoaiGD = 3)
                        string maGD_Gui = maGD + "1";
                        if (maGD_Gui.Length > 30) maGD_Gui = maGD_Gui.Substring(0, 30);

                        const string sqlInsertSender = @"
                            INSERT INTO dbo.GiaoDich 
                                (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, SoTienGoc, 
                                 MaLoaiTien, LoaiTien, NgayGiaoDich, NoiDung, TrangThai)
                            VALUES 
                                (@MaGD, @SoTaiKhoan, 3, @SoTienVND, @SoTienGoc, 
                                 @MaLoaiTien, @LoaiTien, @Ngay, @NoiDung, @TrangThai)";
                        using (SqlCommand cmd = new SqlCommand(sqlInsertSender, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaGD", maGD_Gui);
                            cmd.Parameters.AddWithValue("@SoTaiKhoan", tkGui);
                            cmd.Parameters.AddWithValue("@SoTienVND", soTienVND);
                            cmd.Parameters.AddWithValue("@SoTienGoc", soTienGoc);
                            cmd.Parameters.AddWithValue("@MaLoaiTien", loaiTien);
                            cmd.Parameters.AddWithValue("@LoaiTien", loaiTien);
                            cmd.Parameters.AddWithValue("@Ngay", ngayGD);
                            cmd.Parameters.AddWithValue("@NoiDung", noiDungGui);
                            cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                            cmd.ExecuteNonQuery();
                        }

                        // BƯỚC 4: Kiểm tra tài khoản nhận trong hệ thống
                        string curNhan = "VND";
                        decimal quyDoiNhan = 1;
                        bool recipientExists = false;
                        const string sqlCheckRecipient = @"
                            SELECT TK.SoDuHienTai, LT.MaLoaiTien, LT.QuyDoi 
                            FROM dbo.TaiKhoan TK WITH (UPDLOCK)
                            INNER JOIN dbo.LoaiTien LT ON TK.MaLoaiTien = LT.MaLoaiTien
                            WHERE TK.SoTaiKhoan = @TkNhan";
                        using (SqlCommand cmd = new SqlCommand(sqlCheckRecipient, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@TkNhan", tkNhan);
                            using (SqlDataReader r = cmd.ExecuteReader())
                            {
                                if (r.Read())
                                {
                                    recipientExists = true;
                                    curNhan = r["MaLoaiTien"].ToString();
                                    quyDoiNhan = Convert.ToDecimal(r["QuyDoi"]);
                                    if (curNhan.Equals("USD", StringComparison.OrdinalIgnoreCase) && quyDoiNhan <= 1)
                                        quyDoiNhan = 25400m;
                                }
                            }
                        }

                        // BƯỚC 5: Nếu tài khoản nhận cùng nằm trong hệ thống ngân hàng, cộng tiền và ghi nhận giao dịch Thu
                        if (recipientExists)
                        {
                            decimal amountCreditRecipient = (curNhan == "VND") 
                                ? soTienVND 
                                : (curNhan.Equals(loaiTien, StringComparison.OrdinalIgnoreCase) 
                                    ? soTienGoc 
                                    : (soTienVND / (quyDoiNhan > 0 ? quyDoiNhan : 1)));
                            const string sqlCreditRecipient = @"
                                UPDATE dbo.TaiKhoan 
                                SET SoDuHienTai = SoDuHienTai + @Amount 
                                WHERE SoTaiKhoan = @TkNhan";
                            using (SqlCommand cmd = new SqlCommand(sqlCreditRecipient, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Amount", amountCreditRecipient);
                                cmd.Parameters.AddWithValue("@TkNhan", tkNhan);
                                cmd.ExecuteNonQuery();
                            }

                            string maGD_Nhan = maGD + "2";
                            if (maGD_Nhan.Length > 30) maGD_Nhan = maGD_Nhan.Substring(0, 30);

                            decimal soTienGocNhan = (curNhan.Equals(loaiTien, StringComparison.OrdinalIgnoreCase))
                                ? soTienGoc
                                : ((curNhan == "VND") ? soTienVND : amountCreditRecipient);
                            const string sqlInsertRecipient = @"
                                INSERT INTO dbo.GiaoDich 
                                    (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, SoTienGoc, 
                                     MaLoaiTien, LoaiTien, NgayGiaoDich, NoiDung, TrangThai)
                                VALUES 
                                    (@MaGD, @SoTaiKhoan, 4, @SoTienVND, @SoTienGoc, 
                                     @MaLoaiTien, @LoaiTien, @Ngay, @NoiDung, @TrangThai)";
                            using (SqlCommand cmd = new SqlCommand(sqlInsertRecipient, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@MaGD", maGD_Nhan);
                                cmd.Parameters.AddWithValue("@SoTaiKhoan", tkNhan);
                                cmd.Parameters.AddWithValue("@SoTienVND", soTienVND);
                                cmd.Parameters.AddWithValue("@SoTienGoc", soTienGocNhan);
                                cmd.Parameters.AddWithValue("@MaLoaiTien", curNhan);
                                cmd.Parameters.AddWithValue("@LoaiTien", curNhan);
                                cmd.Parameters.AddWithValue("@Ngay", ngayGD);
                                cmd.Parameters.AddWithValue("@NoiDung", noiDungNhan);
                                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // BƯỚC 6: Commit giao tác hoàn tất thành công
                        transaction.Commit();
                        NotifyTransactionChanged();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // ============================================================
        //  5. THỐNG KÊ TỔNG QUAN GIAO DỊCH (THU, CHI, SỐ LƯỢNG)
        // ============================================================

        /// <summary>
        /// Thống kê các chỉ số giao dịch (Tổng số giao dịch, Tổng tiền thu, Tổng tiền chi) theo bộ lọc tìm kiếm.
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm nội dung.</param>
        /// <param name="soTaiKhoan">Số tài khoản lọc ("Tất cả" hoặc số cụ thể).</param>
        /// <param name="tenLoai">Loại giao dịch lọc ("Tất cả" hoặc tên loại).</param>
        /// <returns>DataTable chứa 1 hàng gồm 3 cột: TongGiaoDich, TongThu, TongChi.</returns>
        public DataTable GetTransactionStatistics(string keyword = "", string soTaiKhoan = "", string tenLoai = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string userFilter = GetUserFilter();

                string sql = $@"
                    SELECT 
                        (SELECT COUNT(*) 
                         FROM   dbo.GiaoDich G 
                         INNER JOIN dbo.LoaiGiaoDich L ON G.MaLoaiGD = L.MaLoaiGD
                         INNER JOIN dbo.TaiKhoan TK    ON TK.SoTaiKhoan = G.SoTaiKhoan
                         WHERE  (@Kw = '' OR G.NoiDung LIKE N'%' + @Kw + N'%') 
                           AND  (@Tk = N'Tất cả' OR G.SoTaiKhoan = @Tk) 
                           AND  (@Loai = N'Tất cả' OR (@Loai = N'Khởi tạo' AND G.NoiDung LIKE N'%khởi tạo%') OR (@Loai = N'Nạp tiền' AND L.TenLoaiGD = @Loai AND G.NoiDung NOT LIKE N'%khởi tạo%') OR (@Loai <> N'Khởi tạo' AND @Loai <> N'Nạp tiền' AND L.TenLoaiGD = @Loai))
                           {userFilter}) AS TongGiaoDich,

                        (SELECT ISNULL(SUM(G.SoTien), 0) 
                         FROM   dbo.GiaoDich G 
                         INNER JOIN dbo.LoaiGiaoDich L ON G.MaLoaiGD = L.MaLoaiGD
                         INNER JOIN dbo.TaiKhoan TK    ON TK.SoTaiKhoan = G.SoTaiKhoan
                         WHERE  L.PhanLoai = 'INCOME'
                           AND  (@Kw = '' OR G.NoiDung LIKE N'%' + @Kw + N'%') 
                           AND  (@Tk = N'Tất cả' OR G.SoTaiKhoan = @Tk) 
                           AND  (@Loai = N'Tất cả' OR (@Loai = N'Khởi tạo' AND G.NoiDung LIKE N'%khởi tạo%') OR (@Loai = N'Nạp tiền' AND L.TenLoaiGD = @Loai AND G.NoiDung NOT LIKE N'%khởi tạo%') OR (@Loai <> N'Khởi tạo' AND @Loai <> N'Nạp tiền' AND L.TenLoaiGD = @Loai))
                           {userFilter}) AS TongThu,

                        (SELECT ISNULL(SUM(G.SoTien), 0) 
                         FROM   dbo.GiaoDich G 
                         INNER JOIN dbo.LoaiGiaoDich L ON G.MaLoaiGD = L.MaLoaiGD
                         INNER JOIN dbo.TaiKhoan TK    ON TK.SoTaiKhoan = G.SoTaiKhoan
                         WHERE  L.PhanLoai = 'EXPENSE'
                           AND  (@Kw = '' OR G.NoiDung LIKE N'%' + @Kw + N'%') 
                           AND  (@Tk = N'Tất cả' OR G.SoTaiKhoan = @Tk) 
                           AND  (@Loai = N'Tất cả' OR (@Loai = N'Khởi tạo' AND G.NoiDung LIKE N'%khởi tạo%') OR (@Loai = N'Nạp tiền' AND L.TenLoaiGD = @Loai AND G.NoiDung NOT LIKE N'%khởi tạo%') OR (@Loai <> N'Khởi tạo' AND @Loai <> N'Nạp tiền' AND L.TenLoaiGD = @Loai))
                           {userFilter}) AS TongChi";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Kw", keyword);
                    cmd.Parameters.AddWithValue("@Tk", string.IsNullOrEmpty(soTaiKhoan) ? "Tất cả" : soTaiKhoan);
                    cmd.Parameters.AddWithValue("@Loai", string.IsNullOrEmpty(tenLoai) ? "Tất cả" : tenLoai);

                    AddUserParam(cmd);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        adapter.Fill(dt);
                }
            }
            return dt;
        }

        // ============================================================
        //  6. LẤY DANH SÁCH TÀI KHOẢN CHO COMBOBOX
        // ============================================================

        /// <summary>
        /// Lấy danh sách các tài khoản đang hoạt động kèm tên ngân hàng để nạp vào ComboBox chọn tài khoản.
        /// </summary>
        /// <returns>DataTable gồm 2 cột: SoTaiKhoan và HienThi.</returns>
        public DataTable GetAccountsList()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = @"
                    SELECT TK.SoTaiKhoan, 
                           (NH.TenNganHang + ' - ' + TK.SoTaiKhoan) AS HienThi
                    FROM   dbo.TaiKhoan TK
                    INNER JOIN dbo.NganHang NH ON TK.MaNganHang = NH.MaNganHang
                    WHERE  (TK.TrangThai = 'Active' OR TK.TrangThai = N'Hoạt động')" + GetUserFilter() + @"
                    ORDER BY NH.TenNganHang";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    AddUserParam(cmd);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        adapter.Fill(dt);
                }
            }
            return dt;
        }

        // ============================================================
        //  7. LẤY DANH SÁCH LOẠI GIAO DỊCH
        // ============================================================

        /// <summary>
        /// Lấy danh mục tất cả các loại giao dịch (MaLoaiGD, TenLoaiGD) trong bảng dbo.LoaiGiaoDich.
        /// </summary>
        /// <returns>DataTable chứa danh mục loại giao dịch.</returns>
        public DataTable GetTransactionTypes()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                const string sql = @"
                    SELECT MaLoaiGD, TenLoaiGD 
                    FROM   dbo.LoaiGiaoDich 
                    ORDER  BY TenLoaiGD";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        // ============================================================
        //  8. LẤY SỐ DƯ HIỆN TẠI CỦA TÀI KHOẢN
        // ============================================================

        /// <summary>
        /// Truy vấn số dư hiện tại của một tài khoản cụ thể.
        /// </summary>
        /// <param name="soTaiKhoan">Số tài khoản cần kiểm tra.</param>
        /// <returns>Số dư hiện tại dạng decimal (0 nếu không tìm thấy).</returns>
        public decimal GetAccountBalance(string soTaiKhoan)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                const string sql = "SELECT ISNULL(SoDuHienTai, 0) FROM dbo.TaiKhoan WHERE SoTaiKhoan = @SoTaiKhoan";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }
        }

        // ============================================================
        //  9. LẤY DANH SÁCH LOẠI TIỀN TỆ
        // ============================================================

        /// <summary>
        /// Lấy danh sách các đơn vị tiền tệ và tỷ giá quy đổi trong hệ thống.
        /// </summary>
        /// <returns>Danh sách các đối tượng <see cref="CurrencyInfo"/>.</returns>
        public List<CurrencyInfo> GetCurrencies()
        {
            var currencies = new List<CurrencyInfo>();

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                const string sql = @"
                    SELECT MaLoaiTien, TenLoaiTien, QuyDoi 
                    FROM   dbo.LoaiTien 
                    ORDER  BY MaLoaiTien";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string code = reader["MaLoaiTien"].ToString();
                            decimal qd = Convert.ToDecimal(reader["QuyDoi"]);
                            if (code.Equals("USD", StringComparison.OrdinalIgnoreCase) && qd <= 1)
                            {
                                qd = 25400m;
                            }
                            currencies.Add(new CurrencyInfo
                            {
                                MaLoaiTien = code,
                                TenLoaiTien = reader["TenLoaiTien"].ToString(),
                                QuyDoi = qd
                            });
                        }
                    }
                }
            }
            return currencies;
        }

        // ============================================================
        //  10. LẤY GIAO DỊCH CHUYỂN KHOẢN GẦN ĐÂY
        // ============================================================

        /// <summary>
        /// Lấy 20 giao dịch chuyển khoản thành công gần nhất phục vụ danh sách gợi ý chuyển tiền nhanh.
        /// </summary>
        /// <param name="maKH">Mã khách hàng (tuỳ chọn).</param>
        /// <returns>DataTable chứa các giao dịch chuyển tiền gần nhất.</returns>
        public DataTable GetRecentTransfers(string maKH = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string userFilter = CurrentSession.IsAdmin ? "" : " AND TK.MaKH = @MaKH";
                string sql = @"
                    SELECT TOP 20
                        G.MaGiaoDich,
                        G.SoTaiKhoan,
                        TK.MaNganHang,
                        G.NoiDung,
                        G.SoTien,
                        G.SoTienGoc,
                        G.MaLoaiTien,
                        G.NgayGiaoDich,
                        (G.SoTaiKhoan + ' - ' + G.NoiDung) AS HienThi
                    FROM dbo.GiaoDich G
                    INNER JOIN dbo.TaiKhoan TK ON G.SoTaiKhoan = TK.SoTaiKhoan
                    WHERE G.TrangThai = 'Success' AND G.MaLoaiGD = 3" + userFilter + @"
                    ORDER BY G.NgayGiaoDich DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (!CurrentSession.IsAdmin)
                        cmd.Parameters.AddWithValue("@MaKH", maKH ?? CurrentSession.MaKH ?? "");

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        adapter.Fill(dt);
                }
            }
            return dt;
        }

        // ============================================================
        //  11. LẤY DANH SÁCH TẤT CẢ NGÂN HÀNG
        // ============================================================

        /// <summary>
        /// Lấy danh mục tất cả ngân hàng trong hệ thống (MaNganHang, HienThi).
        /// </summary>
        /// <returns>DataTable danh mục ngân hàng.</returns>
        public DataTable GetBanksList()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                const string sql = @"
                    SELECT MaNganHang, 
                           (MaNganHang + ' - ' + TenNganHang) AS HienThi
                    FROM dbo.NganHang
                    ORDER BY MaNganHang";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        // ============================================================
        //  11.1. LẤY TÀI KHOẢN VÀ THÔNG TIN NGÂN HÀNG CỦA USER
        // ============================================================

        /// <summary>
        /// Lấy danh sách các tài khoản kèm thông tin chi tiết tên ngân hàng, số dư và loại tiền của một khách hàng.
        /// </summary>
        /// <param name="maKH">Mã khách hàng.</param>
        /// <returns>DataTable chứa danh sách tài khoản.</returns>
        public DataTable GetUserAccountsWithBankInfo(string maKH)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                const string sql = @"
                    SELECT TK.SoTaiKhoan, TK.MaNganHang, 
                           (TK.SoTaiKhoan + ' - ' + NH.TenNganHang) AS HienThi,
                           TK.SoDuHienTai, LT.MaLoaiTien, LT.TenLoaiTien, LT.QuyDoi
                    FROM dbo.TaiKhoan TK
                    INNER JOIN dbo.NganHang NH ON TK.MaNganHang = NH.MaNganHang
                    INNER JOIN dbo.LoaiTien LT ON TK.MaLoaiTien = LT.MaLoaiTien
                    WHERE (TK.TrangThai = 'Active' OR TK.TrangThai = N'Hoạt động') 
                      AND (TK.MaKH = @MaKH OR TK.MaKH IN (SELECT MaKH FROM dbo.NguoiDung WHERE MaNguoiDung = @MaND OR TenDangNhap = @UserLogin))
                    ORDER BY NH.TenNganHang";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", maKH ?? "");
                    cmd.Parameters.AddWithValue("@MaND", CurrentSession.MaNguoiDung);
                    cmd.Parameters.AddWithValue("@UserLogin", CurrentSession.TenDangNhap ?? "");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        da.Fill(dt);
                }
            }
            return dt;
        }

        // ============================================================
        //  11.2. LẤY SỐ TÀI KHOẢN CỦA USER TẠI MỘT NGÂN HÀNG
        // ============================================================

        /// <summary>
        /// Lấy số tài khoản đầu tiên của một khách hàng mở tại một ngân hàng cụ thể.
        /// </summary>
        /// <param name="maKH">Mã khách hàng.</param>
        /// <param name="maNganHang">Mã ngân hàng (ví dụ: "NH01").</param>
        /// <returns>Chuỗi số tài khoản nếu có; ngược lại trả về null.</returns>
        public string GetUserAccountForBank(string maKH, string maNganHang)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                const string sql = @"
                    SELECT TOP 1 SoTaiKhoan 
                    FROM dbo.TaiKhoan 
                    WHERE MaKH = @MaKH AND MaNganHang = @MaNganHang AND TrangThai = 'Active'
                    ORDER BY SoTaiKhoan";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", maKH ?? "");
                    cmd.Parameters.AddWithValue("@MaNganHang", maNganHang ?? "");
                    conn.Open();
                    object res = cmd.ExecuteScalar();
                    return res != null && res != DBNull.Value ? res.ToString() : null;
                }
            }
        }

        // ============================================================
        //  12. TÍNH TỔNG SỐ DƯ QUY ĐỔI VNĐ CỦA USER
        // ============================================================

        /// <summary>
        /// Tính toán tổng số dư thực tế trên toàn bộ các tài khoản thanh toán của người dùng hiện tại (quy đổi VNĐ).
        /// </summary>
        /// <param name="maKH">Mã khách hàng.</param>
        /// <returns>Tổng số dư tiền gửi khả dụng.</returns>
        public decimal GetTotalUserBalance(string maKH)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = @"
                    SELECT ISNULL(SUM(
                        CASE 
                            WHEN LT.MaLoaiTien = 'VND' THEN TK.SoDuHienTai 
                            WHEN LT.MaLoaiTien = 'USD' AND (LT.QuyDoi IS NULL OR LT.QuyDoi <= 1) THEN TK.SoDuHienTai * 25400 
                            ELSE TK.SoDuHienTai * ISNULL(LT.QuyDoi, 1.0) 
                        END
                    ), 0)
                    FROM dbo.TaiKhoan TK
                    INNER JOIN dbo.LoaiTien LT ON TK.MaLoaiTien = LT.MaLoaiTien
                    WHERE (TK.TrangThai = 'Active' OR TK.TrangThai = N'Hoạt động')" + GetUserFilter();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    AddUserParam(cmd);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value ? Convert.ToDecimal(result) : 0m;
                }
            }
        }

        // ============================================================
        //  13. LẤY MÃ NGÂN HÀNG CỦA MỘT TÀI KHOẢN
        // ============================================================

        /// <summary>
        /// Tra cứu mã ngân hàng nơi một số tài khoản được phát hành.
        /// </summary>
        /// <param name="soTaiKhoan">Số tài khoản ngân hàng cần kiểm tra.</param>
        /// <returns>Mã ngân hàng (ví dụ: "NH01") hoặc null nếu không tồn tại.</returns>
        public string GetAccountBank(string soTaiKhoan)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                const string sql = "SELECT MaNganHang FROM dbo.TaiKhoan WHERE SoTaiKhoan = @SoTaiKhoan";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value ? result.ToString() : null;
                }
            }
        }

        // ============================================================
        //  14. LẤY HỌ TÊN CHỦ SỞ HỮU TÀI KHOẢN
        // ============================================================

        /// <summary>
        /// Tra cứu họ và tên chủ tài khoản thụ hưởng theo số tài khoản (phục vụ hiển thị xác nhận người nhận khi chuyển khoản).
        /// </summary>
        /// <param name="soTaiKhoan">Số tài khoản người nhận.</param>
        /// <returns>Họ và tên của chủ tài khoản.</returns>
        public string GetAccountOwnerName(string soTaiKhoan)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                const string sql = @"
                    SELECT TOP 1 KH.HoTen 
                    FROM dbo.TaiKhoan TK
                    INNER JOIN dbo.ThongTinKhachHang KH ON TK.MaKH = KH.MaKH
                    WHERE TK.SoTaiKhoan = @SoTaiKhoan";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value ? result.ToString() : "";
                }
            }
        }
    }
}
