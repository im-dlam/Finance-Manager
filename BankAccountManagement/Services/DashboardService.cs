using System.Data;
using System.Data.SqlClient;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Service cung cấp dữ liệu cho Dashboard (thống kê tổng quan).
    /// 
    /// Schema v2.0: Bảng khách hàng là ThongTinKhachHang (không phải KhachHang).
    /// </summary>
    public class DashboardService
    {
        private string GetConnectionString()
        {
            var config = DatabaseConfigManager.Load();
            return DatabaseService.BuildConnectionString(config);
        }

        // ============================================================
        //  1. TỔNG QUAN — 4 thẻ summary
        // ============================================================
        public DataTable GetDashboardSummary()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                const string sql = @"
                    SELECT 
                        (SELECT COUNT(*) FROM dbo.TaiKhoan) AS TongTaiKhoan,
                        (SELECT COUNT(*) FROM dbo.ThongTinKhachHang
                         WHERE TrangThai = N'Active') AS TongKhachHang,
                        (SELECT COUNT(*) FROM dbo.NganHang) AS TongNganHang,
                        (SELECT COUNT(*) FROM dbo.GiaoDich) AS TongGiaoDich";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        // ============================================================
        //  2. SỐ TÀI KHOẢN MỞ THEO THÁNG (năm hiện tại)
        // ============================================================
        public DataTable GetAccountByMonth()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                const string sql = @"
                    SELECT 
                        MONTH(NgayMo) AS Thang,
                        COUNT(*)      AS SoLuong
                    FROM   dbo.TaiKhoan
                    WHERE  YEAR(NgayMo) = YEAR(SYSDATETIME())
                    GROUP  BY MONTH(NgayMo)
                    ORDER  BY MONTH(NgayMo)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        // ============================================================
        //  3. SỐ TÀI KHOẢN THEO NGÂN HÀNG
        // ============================================================
        public DataTable GetAccountByBank()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                const string sql = @"
                    SELECT 
                        NH.TenNganHang        AS NganHang,
                        COUNT(TK.SoTaiKhoan)  AS SoLuong
                    FROM   dbo.TaiKhoan TK
                    INNER JOIN dbo.NganHang NH ON TK.MaNganHang = NH.MaNganHang
                    GROUP  BY NH.TenNganHang
                    ORDER  BY SoLuong DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
    }
}