using System;
using System.Data;
using System.Data.SqlClient;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Service cung cấp dữ liệu cho Dashboard (thống kê tổng quan).
    /// Hỗ trợ phân quyền nghiêm ngặt:     
    /// - Admin thấy toàn bộ dữ liệu hệ thống.
    /// - Member (User) chỉ thấy dữ liệu gắn với MaKH của chính mình.
    /// - Nếu Member chưa có MaKH hoặc tài khoản mới, trả về 0/rỗng (không lộ dữ liệu Admin).
    /// </summary>
    public class DashboardService
    {
        private string GetConnectionString()
        {
            var config = DatabaseConfigManager.Load();
            return DatabaseService.BuildConnectionString(config);
        }

        // ============================================================
        //  1. TỔNG QUAN — 4 thẻ summary (lọc theo maKH nếu là Member)
        // ============================================================
        public DataTable GetDashboardSummary(string maKH = null, bool? isAdmin = null)
        {
            bool admin = isAdmin ?? CurrentSession.IsAdmin;
            DataTable dt = new DataTable();

            // Nếu là Member nhưng chưa có hồ sơ MaKH -> trả về 0 tuyệt đối
            if (!admin && string.IsNullOrEmpty(maKH))
            {
                dt.Columns.Add("TongTaiKhoan", typeof(long));
                dt.Columns.Add("TongKhachHang", typeof(long));
                dt.Columns.Add("TongNganHang", typeof(long));
                dt.Columns.Add("TongGiaoDich", typeof(long));
                dt.Rows.Add(0L, 0L, 0L, 0L);
                return dt;
            }

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql;
                if (admin)
                {
                    sql = @"
                        SELECT 
                            (SELECT COUNT(*) FROM dbo.TaiKhoan) AS TongTaiKhoan,
                            (SELECT COUNT(*) FROM dbo.ThongTinKhachHang WHERE TrangThai = N'Active') AS TongKhachHang,
                            (SELECT COUNT(*) FROM dbo.NganHang) AS TongNganHang,
                            (SELECT COUNT(*) FROM dbo.GiaoDich) AS TongGiaoDich";
                }
                else
                {
                    sql = @"
                        SELECT 
                            (SELECT COUNT(*) FROM dbo.TaiKhoan WHERE MaKH = @MaKH) AS TongTaiKhoan,
                            0 AS TongKhachHang,
                            (SELECT COUNT(DISTINCT MaNganHang) FROM dbo.TaiKhoan WHERE MaKH = @MaKH) AS TongNganHang,
                            (SELECT COUNT(*) FROM dbo.GiaoDich G INNER JOIN dbo.TaiKhoan TK ON G.SoTaiKhoan = TK.SoTaiKhoan WHERE TK.MaKH = @MaKH) AS TongGiaoDich";
                }

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    if (!admin && !string.IsNullOrEmpty(maKH))
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
        public DataTable GetAccountByMonth(string maKH = null, bool? isAdmin = null)
        {
            bool admin = isAdmin ?? CurrentSession.IsAdmin;
            DataTable dt = new DataTable();

            if (!admin && string.IsNullOrEmpty(maKH))
            {
                dt.Columns.Add("Thang", typeof(int));
                dt.Columns.Add("SoLuong", typeof(int));
                return dt;
            }

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string whereClause = admin ? "" : "AND MaKH = @MaKH";
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
                    if (!admin && !string.IsNullOrEmpty(maKH))
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
        public DataTable GetAccountByBank(string maKH = null, bool? isAdmin = null)
        {
            bool admin = isAdmin ?? CurrentSession.IsAdmin;
            DataTable dt = new DataTable();

            if (!admin && string.IsNullOrEmpty(maKH))
            {
                dt.Columns.Add("NganHang", typeof(string));
                dt.Columns.Add("SoLuong", typeof(int));
                return dt;
            }

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string whereClause = admin ? "" : "WHERE TK.MaKH = @MaKH";
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
                    if (!admin && !string.IsNullOrEmpty(maKH))
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