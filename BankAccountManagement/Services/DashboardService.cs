using System.Data;
using System.Data.SqlClient;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Service cung cấp dữ liệu cho Dashboard (thống kê tổng quan).
    /// Hỗ trợ phân quyền: Admin thấy toàn bộ, Member (User) thấy theo MaKH.
    /// </summary>
    public class DashboardService
    {
        private string GetConnectionString()
        {
            var config = DatabaseConfigManager.Load();
            return DatabaseService.BuildConnectionString(config);
        }

        // ============================================================
        //  1. TỔNG QUAN — 4 thẻ summary (lọc theo maKH nếu có)
        // ============================================================
        public DataTable GetDashboardSummary(string maKH = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                // 1. Tổng tài khoản: Lọc theo MaKH nếu là Member
                string taiKhoanSql = !string.IsNullOrEmpty(maKH)
                    ? "(SELECT COUNT(*) FROM dbo.TaiKhoan WHERE MaKH = @MaKH)"
                    : "(SELECT COUNT(*) FROM dbo.TaiKhoan)";

                // 2. Tổng ngân hàng: Nếu là User, đếm số ngân hàng user đang mở tài khoản
                string nganHangSql = !string.IsNullOrEmpty(maKH)
                    ? "(SELECT COUNT(DISTINCT MaNganHang) FROM dbo.TaiKhoan WHERE MaKH = @MaKH)"
                    : "(SELECT COUNT(*) FROM dbo.NganHang)";

                // 3. Tổng giao dịch: Lọc qua bảng TaiKhoan nối với MaKH
                string giaoDichSql = !string.IsNullOrEmpty(maKH)
                    ? "(SELECT COUNT(*) FROM dbo.GiaoDich G INNER JOIN dbo.TaiKhoan TK ON G.SoTaiKhoan = TK.SoTaiKhoan WHERE TK.MaKH = @MaKH)"
                    : "(SELECT COUNT(*) FROM dbo.GiaoDich)";

                // 4. Tổng khách hàng: Chỉ tính cho Admin
                string khachHangSql = !string.IsNullOrEmpty(maKH)
                    ? "0"
                    : "(SELECT COUNT(*) FROM dbo.ThongTinKhachHang WHERE TrangThai = N'Active')";

                string sql = $@"
                    SELECT 
                        {taiKhoanSql} AS TongTaiKhoan,
                        {khachHangSql} AS TongKhachHang,
                        {nganHangSql} AS TongNganHang,
                        {giaoDichSql} AS TongGiaoDich";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    if (!string.IsNullOrEmpty(maKH))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                    }

                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        // ============================================================
        //  2. SỐ TÀI KHOẢN MỞ THEO THÁNG (năm hiện tại)
        // ============================================================
        public DataTable GetAccountByMonth(string maKH = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string whereClause = !string.IsNullOrEmpty(maKH) ? "AND MaKH = @MaKH" : "";
                string sql = $@"
                    SELECT 
                        MONTH(NgayMo) AS Thang, 
                        COUNT(*) AS SoLuong 
                    FROM dbo.TaiKhoan 
                    WHERE YEAR(NgayMo) = YEAR(GETDATE()) {whereClause}
                    GROUP BY MONTH(NgayMo)
                    ORDER BY MONTH(NgayMo)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    if (!string.IsNullOrEmpty(maKH))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                    }

                     adapter.Fill(dt);
                }
            }
            return dt;
        }

        // ============================================================
        //  3. SỐ TÀI KHOẢN THEO NGÂN HÀNG
        // ============================================================
        public DataTable GetAccountByBank(string maKH = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string whereClause = !string.IsNullOrEmpty(maKH) ? "WHERE TK.MaKH = @MaKH" : "";
                string sql = $@"
                    SELECT 
                        NH.TenNganHang AS NganHang, 
                        COUNT(TK.SoTaiKhoan) AS SoLuong 
                    FROM dbo.TaiKhoan TK
                    INNER JOIN dbo.NganHang NH ON TK.MaNganHang = NH.MaNganHang
                    {whereClause}
                    GROUP BY NH.TenNganHang
                    ORDER BY SoLuong DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    if (!string.IsNullOrEmpty(maKH))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                    }

                     adapter.Fill(dt);
                }
            }
            return dt;
        }
    }
}