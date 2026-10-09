using System;
using System.Data;
using System.Data.SqlClient;

namespace BankAccountManagement.Services
{
    public class DashboardService
    {
        private string GetConnectionString()
        {
            var config = DatabaseConfigManager.Load();
            return DatabaseService.BuildConnectionString(config);
        }

        public DataTable GetDashboardSummary()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = @"
                    SELECT 
                        (SELECT COUNT(*) FROM TaiKhoan) AS TongTaiKhoan,
                        (SELECT COUNT(*) FROM KhachHang WHERE TrangThai = 'Active') AS TongKhachHang,
                        (SELECT COUNT(*) FROM NganHang) AS TongNganHang,
                        (SELECT COUNT(*) FROM GiaoDich) AS TongGiaoDich
                ";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GetAccountByMonth()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = @"
                    SELECT 
                        MONTH(NgayMo) AS Thang, 
                        COUNT(*) AS SoLuong 
                    FROM TaiKhoan 
                    WHERE YEAR(NgayMo) = YEAR(GETDATE())
                    GROUP BY MONTH(NgayMo)
                    ORDER BY MONTH(NgayMo)
                ";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable GetAccountByBank()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = @"
                    SELECT 
                        NH.TenNganHang AS NganHang, 
                        COUNT(TK.SoTaiKhoan) AS SoLuong 
                    FROM TaiKhoan TK
                    INNER JOIN NganHang NH ON TK.MaNganHang = NH.MaNganHang
                    GROUP BY NH.TenNganHang
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
