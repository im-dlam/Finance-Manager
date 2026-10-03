using BankAccountManagement.Models;
using System;
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
                        L.TenLoaiGD AS Loai, 
                        G.SoTien,
                        L.PhanLoai, -- Thêm để biết thu/chi
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
                        L.PhanLoai,
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

        public bool AddTransaction(string maGD, string soTaiKhoan, int maLoaiGD, decimal soTien, string noiDung, DateTime ngayGD, string trangThai)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Thêm giao dịch
                        string sqlInsert = @"INSERT INTO GiaoDich (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, NgayGiaoDich, NoiDung, TrangThai) 
                                             VALUES (@MaGD, @SoTaiKhoan, @MaLoai, @SoTien, @Ngay, @NoiDung, @TrangThai)";
                        using (SqlCommand cmd = new SqlCommand(sqlInsert, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaGD", maGD);
                            cmd.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                            cmd.Parameters.AddWithValue("@MaLoai", maLoaiGD);
                            cmd.Parameters.AddWithValue("@SoTien", soTien);
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

                            if (!string.IsNullOrEmpty(sqlUpdate))
                            {
                                using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn, transaction))
                                {
                                    cmdUpdate.Parameters.AddWithValue("@SoTien", soTien);
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
    }
}
