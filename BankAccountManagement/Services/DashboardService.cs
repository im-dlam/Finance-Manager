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

        // Nhận vào maKH: null là Admin (lấy hết), có giá trị là Member (lọc theo MaKH)
        public DataTable GetDashboardSummary(string maKH = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                // 1. Tổng tài khoản: Lọc theo MaKH nếu là Member
                string taiKhoanSql = !string.IsNullOrEmpty(maKH)
                    ? "(SELECT COUNT(*) FROM TaiKhoan WHERE MaKH = @MaKH)"
                    : "(SELECT COUNT(*) FROM TaiKhoan)";

                // 2. Tổng ngân hàng: Member dùng GROUP BY theo MaKH để đếm số ngân hàng đang mở tài khoản (Đúng ý nhóm trưởng)
                string nganHangSql = !string.IsNullOrEmpty(maKH)
                    ? "(SELECT COUNT(*) FROM (SELECT MaNganHang FROM TaiKhoan WHERE MaKH = @MaKH GROUP BY MaNganHang) AS TempNganHang)"
                    : "(SELECT COUNT(*) FROM NganHang)";

                // 3. Tổng giao dịch: Lọc qua bảng TaiKhoan nối với MaKH
                string giaoDichSql = !string.IsNullOrEmpty(maKH)
                    ? "(SELECT COUNT(*) FROM GiaoDich G INNER JOIN TaiKhoan TK ON G.SoTaiKhoan = TK.SoTaiKhoan WHERE TK.MaKH = @MaKH)"
                    : "(SELECT COUNT(*) FROM GiaoDich)";

                string sql = $@"
                    SELECT 
                        {taiKhoanSql} AS TongTaiKhoan,
                        (SELECT COUNT(*) FROM ThongTinKhachHang WHERE TrangThai = N'Active') AS TongKhachHang,
                        {nganHangSql} AS TongNganHang,
                        {giaoDichSql} AS TongGiaoDich
                ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(maKH))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

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
                    FROM TaiKhoan 
                    WHERE YEAR(NgayMo) = YEAR(GETDATE()) {whereClause}
                    GROUP BY MONTH(NgayMo)
                    ORDER BY MONTH(NgayMo)
                ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(maKH))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

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
                    FROM TaiKhoan TK
                    INNER JOIN NganHang NH ON TK.MaNganHang = NH.MaNganHang
                    {whereClause}
                    GROUP BY NH.TenNganHang
                ";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(maKH))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
    }
}