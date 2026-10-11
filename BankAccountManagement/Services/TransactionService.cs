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
                        L.TenLoaiGD AS Loai, 
                        G.SoTien, 
                        G.SoTienGoc, 
                        G.MaLoaiTien, 
                        L.PhanLoai,
                        N'Chi tiết' AS ThaoTac, 
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
                        L.TenLoaiGD AS Loai, 
                        G.SoTien, 
                        G.SoTienGoc, 
                        G.MaLoaiTien, 
                        L.PhanLoai,
                        N'Chi tiết' AS ThaoTac, 
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
                                 MaLoaiTien, NgayGiaoDich, NoiDung, TrangThai)
                            VALUES 
                                (@MaGD, @SoTaiKhoan, @MaLoai, @SoTienVND, @SoTienGoc, 
                                 @LoaiTien, @Ngay, @NoiDung, @TrangThai)";

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
    }
}