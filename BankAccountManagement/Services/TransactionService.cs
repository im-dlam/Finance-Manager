using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Service quản lý giao dịch.
    /// 
    /// Phân quyền:
    /// - Admin:  thấy TẤT CẢ giao dịch.
    /// - Member: CHỈ thấy giao dịch của tài khoản mình sở hữu.
    /// </summary>
    public class TransactionService
    {
        public static event Action OnTransactionChanged;

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

        private string GetConnectionString()
        {
            return DatabaseService.BuildConnectionString(DatabaseConfigManager.Load());
        }

        // ============================================================
        //  WHERE CLAUSE DÙNG CHUNG cho phân quyền
        //  Trả về mệnh đề WHERE để filter theo user hiện tại.
        //  Admin → "" (không filter)
        //  Member → " AND TK.MaKH = @MaKH"
        // ============================================================
        private static string GetUserFilter(string tableAlias = "TK")
        {
            if (CurrentSession.IsAdmin) return "";
            return $" AND {tableAlias}.MaKH = @MaKH";
        }

        private static void AddUserParam(SqlCommand cmd)
        {
            if (!CurrentSession.IsAdmin)
                cmd.Parameters.AddWithValue("@MaKH", CurrentSession.MaKH ?? "");
        }

        // ============================================================
        //  1. LẤY TẤT CẢ GIAO DỊCH (có phân quyền)
        // ============================================================
        public DataTable GetAllTransactions()
        {
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
                        G.SoTienGoc, 
                        ISNULL(NULLIF(G.LoaiTien, ''), ISNULL(NULLIF(G.MaLoaiTien, ''), 'VND')) AS LoaiTien, 
                        ISNULL(NULLIF(G.MaLoaiTien, ''), ISNULL(NULLIF(G.LoaiTien, ''), 'VND')) AS MaLoaiTien, 
                        L.PhanLoai,
                        L.TenLoaiGD AS ThaoTac, 
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
        //  2. LẤY CURRENCY CỦA 1 TÀI KHOẢN
        // ============================================================
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
                            return new CurrencyInfo
                            {
                                MaLoaiTien = reader["MaLoaiTien"].ToString(),
                                TenLoaiTien = reader["TenLoaiTien"].ToString(),
                                QuyDoi = Convert.ToDecimal(reader["QuyDoi"])
                            };
                        }
                    }
                }
            }
            return null;
        }

        // ============================================================
        //  3. TÌM KIẾM GIAO DỊCH (có phân quyền)
        // ============================================================
        public DataTable SearchTransactions(string keyword, string soTaiKhoan = "", string tenLoai = "")
        {
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
                        G.SoTienGoc, 
                        ISNULL(NULLIF(G.LoaiTien, ''), ISNULL(NULLIF(G.MaLoaiTien, ''), 'VND')) AS LoaiTien, 
                        ISNULL(NULLIF(G.MaLoaiTien, ''), ISNULL(NULLIF(G.LoaiTien, ''), 'VND')) AS MaLoaiTien, 
                        L.PhanLoai,
                        L.TenLoaiGD AS ThaoTac, 
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
        //  4. THÊM GIAO DỊCH (transaction)
        // ============================================================
        public bool AddTransaction(string maGD, string soTaiKhoan, int maLoaiGD,
            decimal soTienGoc, string loaiTien, decimal soTienVND, string noiDung,
            DateTime ngayGD, string trangThai)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 4.1. INSERT GiaoDich
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

                        // 4.2. Lấy phân loại INCOME/EXPENSE
                        string phanLoai = "";
                        const string getLoaiSql =
                            "SELECT PhanLoai FROM dbo.LoaiGiaoDich WHERE MaLoaiGD = @MaLoai";
                        using (SqlCommand cmd = new SqlCommand(getLoaiSql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaLoai", maLoaiGD);
                            phanLoai = cmd.ExecuteScalar()?.ToString() ?? "";
                        }

                        // 4.3. Cập nhật số dư nếu giao dịch thành công
                        if (trangThai == "Success" || trangThai == "Hoàn tất")
                        {
                            string sqlUpdate = "";
                            if (phanLoai == "INCOME")
                                sqlUpdate = @"UPDATE dbo.TaiKhoan 
                                              SET SoDuHienTai = SoDuHienTai + @SoTien 
                                              WHERE SoTaiKhoan = @SoTaiKhoan";
                            else if (phanLoai == "EXPENSE")
                                sqlUpdate = @"UPDATE dbo.TaiKhoan 
                                              SET SoDuHienTai = SoDuHienTai - @SoTien 
                                              WHERE SoTaiKhoan = @SoTaiKhoan";

                            if (!string.IsNullOrEmpty(sqlUpdate))
                            {
                                using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn, transaction))
                                {
                                    cmdUpdate.Parameters.AddWithValue("@SoTien", soTienVND);
                                    cmdUpdate.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                                    cmdUpdate.ExecuteNonQuery();
                                }
                            }
                        }

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
        //  4.1. CHUYỂN KHOẢN 2 BÊN (Giao dịch 2 bên cho User)
        //  - Bên gửi: Trừ tiền, tạo GD Chi (EXPENSE)
        //  - Bên nhận: Cộng tiền, tạo GD Thu (INCOME)
        // ============================================================
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
                        // 1. Kiểm tra tài khoản gửi và số dư
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
                                }
                                else
                                {
                                    throw new Exception("Không tìm thấy tài khoản chuyển tiền!");
                                }
                            }
                        }

                        // Trừ tiền tài khoản gửi
                        decimal amountDeductSender = (curGui == "VND") ? soTienVND : (soTienVND / (quyDoiGui > 0 ? quyDoiGui : 1));
                        if (!CurrentSession.IsAdmin && soDuGui < amountDeductSender)
                        {
                            throw new Exception($"Số dư tài khoản chuyển ({soDuGui:N0} {curGui}) không đủ để thực hiện giao dịch!");
                        }

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

                        // 2. Ghi nhận giao dịch bên gửi (CHI - MaLoaiGD = 3 Chuyển khoản)
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

                        // 3. Kiểm tra tài khoản nhận
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
                                }
                            }
                        }

                        if (recipientExists)
                        {
                            decimal amountCreditRecipient = (curNhan == "VND") ? soTienVND : (soTienVND / (quyDoiNhan > 0 ? quyDoiNhan : 1));
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

                            // 4. Ghi nhận giao dịch bên nhận (THU - MaLoaiGD = 1 Nạp tiền / Thu tiền chuyển khoản)
                            string maGD_Nhan = maGD + "2";
                            if (maGD_Nhan.Length > 30) maGD_Nhan = maGD_Nhan.Substring(0, 30);

                            decimal soTienGocNhan = (curNhan == "VND") ? soTienVND : amountCreditRecipient;
                            const string sqlInsertRecipient = @"
                                INSERT INTO dbo.GiaoDich 
                                    (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, SoTienGoc, 
                                     MaLoaiTien, LoaiTien, NgayGiaoDich, NoiDung, TrangThai)
                                VALUES 
                                    (@MaGD, @SoTaiKhoan, 1, @SoTienVND, @SoTienGoc, 
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
        //  5. THỐNG KÊ GIAO DỊCH (có phân quyền)
        // ============================================================
        public DataTable GetTransactionStatistics(string keyword = "",
            string soTaiKhoan = "", string tenLoai = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                // ⭐ Mệnh đề filter theo user — dùng chung cho cả 3 subquery
                string userFilter = CurrentSession.IsAdmin
                    ? ""
                    : " AND TK.MaKH = @MaKH";

                string sql = $@"
                    SELECT 
                        (SELECT COUNT(*) 
                         FROM   dbo.GiaoDich G 
                         INNER JOIN dbo.LoaiGiaoDich L ON G.MaLoaiGD = L.MaLoaiGD
                         INNER JOIN dbo.TaiKhoan TK    ON TK.SoTaiKhoan = G.SoTaiKhoan
                         WHERE  (@Kw = '' OR G.NoiDung LIKE N'%' + @Kw + N'%') 
                           AND  (@Tk = N'Tất cả' OR G.SoTaiKhoan = @Tk) 
                           AND  (@Loai = N'Tất cả' OR L.TenLoaiGD = @Loai)
                           {userFilter}) AS TongGiaoDich,

                        (SELECT ISNULL(SUM(G.SoTien), 0) 
                         FROM   dbo.GiaoDich G 
                         INNER JOIN dbo.LoaiGiaoDich L ON G.MaLoaiGD = L.MaLoaiGD
                         INNER JOIN dbo.TaiKhoan TK    ON TK.SoTaiKhoan = G.SoTaiKhoan
                         WHERE  L.PhanLoai = 'INCOME'
                           AND  (@Kw = '' OR G.NoiDung LIKE N'%' + @Kw + N'%') 
                           AND  (@Tk = N'Tất cả' OR G.SoTaiKhoan = @Tk) 
                           AND  (@Loai = N'Tất cả' OR L.TenLoaiGD = @Loai)
                           {userFilter}) AS TongThu,

                        (SELECT ISNULL(SUM(G.SoTien), 0) 
                         FROM   dbo.GiaoDich G 
                         INNER JOIN dbo.LoaiGiaoDich L ON G.MaLoaiGD = L.MaLoaiGD
                         INNER JOIN dbo.TaiKhoan TK    ON TK.SoTaiKhoan = G.SoTaiKhoan
                         WHERE  L.PhanLoai = 'EXPENSE'
                           AND  (@Kw = '' OR G.NoiDung LIKE N'%' + @Kw + N'%') 
                           AND  (@Tk = N'Tất cả' OR G.SoTaiKhoan = @Tk) 
                           AND  (@Loai = N'Tất cả' OR L.TenLoaiGD = @Loai)
                           {userFilter}) AS TongChi";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Kw", keyword);
                    cmd.Parameters.AddWithValue("@Tk", string.IsNullOrEmpty(soTaiKhoan) ? "Tất cả" : soTaiKhoan);
                    cmd.Parameters.AddWithValue("@Loai", string.IsNullOrEmpty(tenLoai) ? "Tất cả" : tenLoai);

                    if (!CurrentSession.IsAdmin)
                        cmd.Parameters.AddWithValue("@MaKH", CurrentSession.MaKH ?? "");

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        adapter.Fill(dt);
                }
            }
            return dt;
        }

        // ============================================================
        //  6. LẤY DANH SÁCH TÀI KHOẢN CHO COMBOBOX (có phân quyền)
        // ============================================================
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
                    WHERE  1 = 1" + GetUserFilter() + @"
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
        public decimal GetAccountBalance(string soTaiKhoan)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                const string sql =
                    "SELECT ISNULL(SoDuHienTai, 0) FROM dbo.TaiKhoan WHERE SoTaiKhoan = @SoTaiKhoan";

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
                            currencies.Add(new CurrencyInfo
                            {
                                MaLoaiTien = reader["MaLoaiTien"].ToString(),
                                TenLoaiTien = reader["TenLoaiTien"].ToString(),
                                QuyDoi = Convert.ToDecimal(reader["QuyDoi"])
                            });
                        }
                    }
                }
            }
            return currencies;
        }

        // ============================================================
        //  10. LẤY DANH SÁCH GIAO DỊCH CHUYỂN KHOẢN THÀNH CÔNG GẦN ĐÂY
        // ============================================================
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
        //  11. LẤY DANH SÁCH TẤT CẢ NGÂN HÀNG (MaNganHang - TenNganHang)
        // ============================================================
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
        //  11.1. LẤY DANH SÁCH TÀI KHOẢN VÀ THÔNG TIN NGÂN HÀNG CỦA USER
        // ============================================================
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
                    WHERE TK.TrangThai = 'Active' AND TK.MaKH = @MaKH
                    ORDER BY NH.TenNganHang";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", maKH ?? "");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        da.Fill(dt);
                }
            }
            return dt;
        }

        // ============================================================
        //  11.2. LẤY TÀI KHOẢN CỦA USER TẠI NGÂN HÀNG ĐƯỢC CHỌN
        // ============================================================
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
        //  12. LẤY TỔNG SỐ DƯ THỰC TẾ CỦA TÀI KHOẢN ĐĂNG NHẬP (VND)
        // ============================================================
        public decimal GetTotalUserBalance(string maKH)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string userFilter = CurrentSession.IsAdmin ? "" : " AND TK.MaKH = @MaKH";
                string sql = @"
                    SELECT ISNULL(SUM(
                        CASE 
                            WHEN LT.TenLoaiTien = N'VND' THEN TK.SoDuHienTai 
                            ELSE TK.SoDuHienTai * LT.QuyDoi 
                        END
                    ), 0)
                    FROM dbo.TaiKhoan TK
                    INNER JOIN dbo.LoaiTien LT ON TK.MaLoaiTien = LT.MaLoaiTien
                    WHERE TK.TrangThai = 'Active'" + userFilter;

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (!CurrentSession.IsAdmin)
                        cmd.Parameters.AddWithValue("@MaKH", maKH ?? CurrentSession.MaKH ?? "");

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null && result != DBNull.Value ? Convert.ToDecimal(result) : 0m;
                }
            }
        }

        // ============================================================
        //  13. LẤY MÃ NGÂN HÀNG CỦA 1 SỐ TÀI KHOẢN
        // ============================================================
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
        //  14. LẤY TÊN CHỦ TÀI KHOẢN (NGƯỜI NHẬN) THEO SỐ TÀI KHOẢN
        // ============================================================
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