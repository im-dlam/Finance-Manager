using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Services
{
    public class TransactionService
    {
        // Hàm lấy chuỗi kết nối Database từ cấu hình chung
        private string GetConnectionString()
        {
            DatabaseConfig config = DatabaseConfigManager.Load();
            return DatabaseService.BuildConnectionString(config);
        }

        // 1. Lấy toàn bộ danh sách giao dịch
        public DataTable GetAllTransactions()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = @"SELECT MaGD, SoTaiKhoan, LoaiGiaoDich, SoTien, NgayGiaoDich, NoiDung 
                               FROM GiaoDich 
                               ORDER BY NgayGiaoDich DESC";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        // 2. Tìm kiếm giao dịch theo Mã GD, Số tài khoản, hoặc Nội dung
        public DataTable SearchTransactions(string keyword)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = @"SELECT MaGD, SoTaiKhoan, LoaiGiaoDich, SoTien, NgayGiaoDich, NoiDung 
                               FROM GiaoDich 
                               WHERE MaGD LIKE @Keyword 
                                  OR SoTaiKhoan LIKE @Keyword 
                                  OR NoiDung LIKE @Keyword 
                                  OR LoaiGiaoDich LIKE @Keyword
                               ORDER BY NgayGiaoDich DESC";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        // 3. Thực hiện giao dịch (Gửi tiền / Rút tiền) dùng Transaction để đảm bảo tính toàn vẹn dữ liệu
        public bool ExecuteTransaction(string maGD, string soTaiKhoan, string loaiGiaoDich, decimal soTien, string noiDung)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // a. Thêm bản ghi vào bảng GiaoDich
                        string sqlInsert = @"INSERT INTO GiaoDich (MaGD, SoTaiKhoan, LoaiGiaoDich, SoTien, NgayGiaoDich, NoiDung) 
                                             VALUES (@MaGD, @SoTaiKhoan, @LoaiGiaoDich, @SoTien, GETDATE(), @NoiDung)";
                        using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn, transaction))
                        {
                            cmdInsert.Parameters.AddWithValue("@MaGD", maGD);
                            cmdInsert.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                            cmdInsert.Parameters.AddWithValue("@LoaiGiaoDich", loaiGiaoDich);
                            cmdInsert.Parameters.AddWithValue("@SoTien", soTien);
                            cmdInsert.Parameters.AddWithValue("@NoiDung", noiDung);
                            cmdInsert.ExecuteNonQuery();
                        }

                        // b. Cập nhật số dư tài khoản
                        string sqlUpdateBalance = "";
                        if (loaiGiaoDich.Equals("GuiTien", StringComparison.OrdinalIgnoreCase) ||
                            loaiGiaoDich.Equals("Gửi tiền", StringComparison.OrdinalIgnoreCase) ||
                            loaiGiaoDich.Equals("Nạp tiền", StringComparison.OrdinalIgnoreCase))
                        {
                            sqlUpdateBalance = "UPDATE TaiKhoan SET SoDu = SoDu + @SoTien WHERE SoTaiKhoan = @SoTaiKhoan";
                        }
                        else
                        {
                            sqlUpdateBalance = "UPDATE TaiKhoan SET SoDu = SoDu - @SoTien WHERE SoTaiKhoan = @SoTaiKhoan AND SoDu >= @SoTien";
                        }

                        using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdateBalance, conn, transaction))
                        {
                            cmdUpdate.Parameters.AddWithValue("@SoTien", soTien);
                            cmdUpdate.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                            int rowsAffected = cmdUpdate.ExecuteNonQuery();

                            // Nếu không có dòng nào được cập nhật (ví dụ: Số dư không đủ rút)
                            if (rowsAffected == 0)
                            {
                                transaction.Rollback();
                                return false;
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

        // 4. Thống kê thông số giao dịch cho các thẻ thông tin (Cards)
        public DataTable GetTransactionStatistics()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = @"
                    SELECT 
                        (SELECT COUNT(*) FROM GiaoDich) AS TongGiaoDich,
                        (SELECT ISNULL(SUM(SoTien), 0) FROM GiaoDich WHERE LoaiGiaoDich LIKE N'%Gửi%' OR LoaiGiaoDich LIKE N'%Nap%') AS TongTienGui,
                        (SELECT ISNULL(SUM(SoTien), 0) FROM GiaoDich WHERE LoaiGiaoDich LIKE N'%Rút%') AS TongTienRut,
                        (SELECT COUNT(*) FROM GiaoDich WHERE CAST(NgayGiaoDich AS DATE) = CAST(GETDATE() AS DATE)) AS GiaoDichHomNay
                ";
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

