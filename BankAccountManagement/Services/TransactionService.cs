using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BankAccountManagement.Services
{
    public class TransactionService
    {
        private string GetConnectionString()
        {
            return DatabaseService.BuildConnectionString(DatabaseConfigManager.Load());
        }

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
                        L.TenLoaiGD AS Loai, G.SoTien, G.SoTienGoc, G.LoaiTien, L.PhanLoai, -- Thêm để biết thu/chi
                        N'Chi tiết' AS ThaoTac, 
                        G.TrangThai 
                    FROM GiaoDich G
                    INNER JOIN LoaiGiaoDich L ON G.MaLoaiGD = L.MaLoaiGD
                    ORDER BY G.NgayGiaoDich DESC";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        /// <summary>
        /// Get currency theo stk 
        /// Fix: sai curren khi thêm giao dịch
        /// </summary>
        /// <param name="soTaiKhoan"></param>
        /// <returns></returns>
        public CurrencyInfo GetAccountCurrency(string soTaiKhoan)
        {
            using (SqlConnection conn =
                new SqlConnection(GetConnectionString()))
            {
                string sql = @"
            SELECT
                LT.MaLoaiTien,
                LT.TenLoaiTien,
                LT.QuyDoi
            FROM TaiKhoan TK
            INNER JOIN LoaiTien LT
                ON TK.MaLoaiTien = LT.MaLoaiTien
            WHERE TK.SoTaiKhoan = @SoTaiKhoan";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue(
                        "@SoTaiKhoan",
                        soTaiKhoan);

                    conn.Open();

                    using (SqlDataReader reader =
                        cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CurrencyInfo
                            {
                                MaLoaiTien =
                                    reader["MaLoaiTien"].ToString(),

                                TenLoaiTien =
                                    reader["TenLoaiTien"].ToString(),

                                QuyDoi =
                                    Convert.ToDecimal(
                                        reader["QuyDoi"])
                            };
                        }
                    }
                }
            }

            return null;
        }

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
                        L.TenLoaiGD AS Loai, G.SoTien, G.SoTienGoc, G.LoaiTien, L.PhanLoai,
                        N'Chi tiết' AS ThaoTac, 
                        G.TrangThai 
                    FROM GiaoDich G
                    INNER JOIN LoaiGiaoDich L ON G.MaLoaiGD = L.MaLoaiGD
                    WHERE (G.NoiDung LIKE @Keyword OR G.SoTaiKhoan LIKE @Keyword OR G.MaGiaoDich LIKE @Keyword)";

                if (!string.IsNullOrEmpty(soTaiKhoan) && soTaiKhoan != "Tất cả")
                {
                    sql += " AND G.SoTaiKhoan = @SoTaiKhoan";
                }
                if (!string.IsNullOrEmpty(tenLoai) && tenLoai != "Tất cả")
                {
                    sql += " AND L.TenLoaiGD = @TenLoai";
                }

                sql += " ORDER BY G.NgayGiaoDich DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    if (!string.IsNullOrEmpty(soTaiKhoan) && soTaiKhoan != "Tất cả") cmd.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                    if (!string.IsNullOrEmpty(tenLoai) && tenLoai != "Tất cả") cmd.Parameters.AddWithValue("@TenLoai", tenLoai);
                    
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public bool AddTransaction(string maGD, string soTaiKhoan, int maLoaiGD, decimal soTienGoc, string loaiTien, decimal soTienVND, string noiDung, DateTime ngayGD, string trangThai)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Thêm giao dịch
                        string sqlInsert = @"INSERT INTO GiaoDich (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, SoTienGoc, LoaiTien, NgayGiaoDich, NoiDung, TrangThai) VALUES (@MaGD, @SoTaiKhoan, @MaLoai, @SoTienVND, @SoTienGoc, @LoaiTien, @Ngay, @NoiDung, @TrangThai)";
                        using (SqlCommand cmd = new SqlCommand(sqlInsert, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaGD", maGD);
                            cmd.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                            cmd.Parameters.AddWithValue("@MaLoai", maLoaiGD);
                            cmd.Parameters.AddWithValue("@SoTienVND", soTienVND); cmd.Parameters.AddWithValue("@SoTienGoc", soTienGoc); cmd.Parameters.AddWithValue("@LoaiTien", loaiTien);
                            cmd.Parameters.AddWithValue("@Ngay", ngayGD);
                            cmd.Parameters.AddWithValue("@NoiDung", noiDung);
                            cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                            cmd.ExecuteNonQuery();
                        }

                        // Lấy loại giao dịch là Thu hay Chi
                        string phanLoai = "";
                        string getLoaiSql = "SELECT PhanLoai FROM LoaiGiaoDich WHERE MaLoaiGD = @MaLoai";
                        using (SqlCommand cmd = new SqlCommand(getLoaiSql, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaLoai", maLoaiGD);
                            phanLoai = cmd.ExecuteScalar()?.ToString() ?? "";
                        }

                        // Nếu thành công thì trừ/cộng tiền
                        if (trangThai == "Success" || trangThai == "Hoàn tất")
                        {
                            string sqlUpdate = "";
                            if (phanLoai == "INCOME")
                                sqlUpdate = "UPDATE TaiKhoan SET SoDuHienTai = SoDuHienTai + @SoTien WHERE SoTaiKhoan = @SoTaiKhoan";
                            else if (phanLoai == "EXPENSE")
                                sqlUpdate = "UPDATE TaiKhoan SET SoDuHienTai = SoDuHienTai - @SoTien WHERE SoTaiKhoan = @SoTaiKhoan";

                            // fix: Must declare the scalar variable "@SoTien".
                            if (!string.IsNullOrEmpty(sqlUpdate))
                            {
                                using (SqlCommand cmdUpdate =
                                    new SqlCommand(sqlUpdate, conn, transaction))
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

        public DataTable GetTransactionStatistics(string keyword = "", string soTaiKhoan = "", string tenLoai = "")
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = @"
                    SELECT 
                        (SELECT COUNT(*) FROM GiaoDich G INNER JOIN LoaiGiaoDich L ON G.MaLoaiGD = L.MaLoaiGD WHERE (@Kw='' OR G.NoiDung LIKE N'%'+@Kw+N'%') AND (@Tk=N'Tất cả' OR G.SoTaiKhoan=@Tk) AND (@Loai=N'Tất cả' OR L.TenLoaiGD=@Loai)) AS TongGiaoDich,
                        (SELECT ISNULL(SUM(G.SoTien), 0) FROM GiaoDich G INNER JOIN LoaiGiaoDich L ON G.MaLoaiGD = L.MaLoaiGD WHERE L.PhanLoai = 'INCOME' AND (@Kw='' OR G.NoiDung LIKE N'%'+@Kw+N'%') AND (@Tk=N'Tất cả' OR G.SoTaiKhoan=@Tk) AND (@Loai=N'Tất cả' OR L.TenLoaiGD=@Loai)) AS TongThu,
                        (SELECT ISNULL(SUM(G.SoTien), 0) FROM GiaoDich G INNER JOIN LoaiGiaoDich L ON G.MaLoaiGD = L.MaLoaiGD WHERE L.PhanLoai = 'EXPENSE' AND (@Kw='' OR G.NoiDung LIKE N'%'+@Kw+N'%') AND (@Tk=N'Tất cả' OR G.SoTaiKhoan=@Tk) AND (@Loai=N'Tất cả' OR L.TenLoaiGD=@Loai)) AS TongChi
                ";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Kw", keyword);
                    cmd.Parameters.AddWithValue("@Tk", string.IsNullOrEmpty(soTaiKhoan) ? "Tất cả" : soTaiKhoan);
                    cmd.Parameters.AddWithValue("@Loai", string.IsNullOrEmpty(tenLoai) ? "Tất cả" : tenLoai);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        // Lấy danh sách tài khoản cho ComboBox
        public DataTable GetAccountsList()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = "SELECT SoTaiKhoan, (NH.TenNganHang + ' - ' + TK.SoTaiKhoan) AS HienThi FROM TaiKhoan TK INNER JOIN NganHang NH ON TK.MaNganHang = NH.MaNganHang";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        // Lấy danh sách loại giao dịch cho ComboBox
        public DataTable GetTransactionTypes()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = "SELECT MaLoaiGD, TenLoaiGD FROM LoaiGiaoDich";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public decimal GetAccountBalance(string soTaiKhoan)
        {
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(GetConnectionString()))
            {
                string sql = "SELECT ISNULL(SoDuHienTai, 0) FROM TaiKhoan WHERE SoTaiKhoan = @SoTaiKhoan";
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }
        }

        /// <summary>
        /// Lấy danh sách các loại tiền tệ từ cơ sở dữ liệu
        /// </summary>
        /// <returns></returns>
        public List<CurrencyInfo> GetCurrencies()
        {
            List<CurrencyInfo> currencies =
                new List<CurrencyInfo>();

            using (SqlConnection conn =
                new SqlConnection(GetConnectionString()))
            {
                string sql = @"
            SELECT
                MaLoaiTien,
                TenLoaiTien,
                QuyDoi
            FROM LoaiTien
            ORDER BY MaLoaiTien";

                using (SqlCommand cmd =
                    new SqlCommand(sql, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader =
                        cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            currencies.Add(
                                new CurrencyInfo
                                {
                                    MaLoaiTien =
                                        reader["MaLoaiTien"].ToString(),

                                    TenLoaiTien =
                                        reader["TenLoaiTien"].ToString(),

                                    QuyDoi =
                                        Convert.ToDecimal(
                                            reader["QuyDoi"])
                                });
                        }
                    }
                }
            }

            return currencies;
        }

    }
}
