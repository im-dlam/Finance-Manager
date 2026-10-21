using System;
using System.Data;
using System.Data.SqlClient;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Dịch vụ tổng hợp dữ liệu màn hình Bảng điều khiển (Dashboard Service).
    /// Chịu trách nhiệm cung cấp dữ liệu số liệu thẻ thống kê (Stats Cards) và biểu đồ (Charts):
    /// - 4 thẻ tóm tắt tổng quan: Tổng tài khoản, Tổng khách hàng, Tổng ngân hàng, Tổng giao dịch.
    /// - Biểu đồ số lượng tài khoản mở mới theo từng tháng trong năm hiện tại.
    /// - Biểu đồ phân bổ tài khoản theo từng ngân hàng liên kết.
    /// - Thực thi cơ chế phân quyền nghiêm ngặt:
    ///   + Quản trị viên (Admin): Thấy toàn bộ dữ liệu trên toàn hệ thống.
    ///   + Khách hàng (Member): Chỉ thấy số liệu liên quan đến các tài khoản của chính mình.
    ///   + Nếu tài khoản mới chưa có MaKH: Trả về bảng số liệu 0 tuyệt đối để chống rò rỉ dữ liệu của Admin.
    /// </summary>
    public class DashboardService
    {
        /// <summary>
        /// Tạo chuỗi kết nối chuẩn tới cơ sở dữ liệu SQL Server.
        /// </summary>
        /// <returns>Chuỗi connection string hợp lệ.</returns>
        private string GetConnectionString()
        {
            var config = DatabaseConfigManager.Load();
            return DatabaseService.BuildConnectionString(config);
        }

        // ============================================================
        //  1. TỔNG QUAN — 4 THẺ THỐNG KÊ SUMMARY CARDS
        // ============================================================

        /// <summary>
        /// Lấy số liệu cho 4 thẻ thống kê tổng quan trên Dashboard:
        /// - Admin: Tổng tài khoản toàn hệ thống, Tổng khách hàng Active, Tổng ngân hàng đối tác, Tổng giao dịch.
        /// - Member: Tổng tài khoản của bản thân, 0 khách hàng, Số ngân hàng đang mở tài khoản, Tổng giao dịch của bản thân.
        /// </summary>
        /// <param name="maKH">Mã khách hàng cần lọc (nếu null sẽ lấy theo phiên đăng nhập).</param>
        /// <param name="isAdmin">Cờ quyền Admin (nếu null sẽ lấy theo CurrentSession.IsAdmin).</param>
        /// <returns>DataTable chứa 1 dòng dữ liệu với 4 cột: TongTaiKhoan, TongKhachHang, TongNganHang, TongGiaoDich.</returns>
        public DataTable GetDashboardSummary(string maKH = null, bool? isAdmin = null)
        {
            bool admin = isAdmin ?? CurrentSession.IsAdmin;
            DataTable dt = new DataTable();

            // BƯỚC 1: Nếu là Member nhưng chưa có mã khách hàng (tài khoản mới đăng ký), trả về bảng số 0 an toàn
            if (!admin && string.IsNullOrEmpty(maKH))
            {
                dt.Columns.Add("TongTaiKhoan", typeof(long));
                dt.Columns.Add("TongKhachHang", typeof(long));
                dt.Columns.Add("TongNganHang", typeof(long));
                dt.Columns.Add("TongGiaoDich", typeof(long));
                dt.Rows.Add(0L, 0L, 0L, 0L);
                return dt;
            }

            // BƯỚC 2: Mở kết nối và thực thi truy vấn theo phân quyền
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql;
                if (admin)
                {
                    // Truy vấn dành cho Admin: Đếm trên toàn bộ hệ thống
                    sql = @"
                        SELECT 
                            (SELECT COUNT(*) FROM dbo.TaiKhoan) AS TongTaiKhoan,
                            (SELECT COUNT(*) FROM dbo.ThongTinKhachHang WHERE TrangThai = N'Active') AS TongKhachHang,
                            (SELECT COUNT(*) FROM dbo.NganHang) AS TongNganHang,
                            (SELECT COUNT(*) FROM dbo.GiaoDich) AS TongGiaoDich";
                }
                else
                {
                    // Truy vấn dành cho Member: Chỉ đếm các bản ghi thuộc về MaKH
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

                    // BƯỚC 3: Điền kết quả vào DataTable trả về
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        // ============================================================
        //  2. SỐ LƯỢNG TÀI KHOẢN MỞ THEO THÁNG TRONG NĂM HIỆN TẠI
        // ============================================================

        /// <summary>
        /// Thống kê số lượng tài khoản được mở mới theo từng tháng trong năm hiện tại.
        /// Dùng làm dữ liệu vẽ biểu đồ cột/đường tăng trưởng tài khoản.
        /// </summary>
        /// <param name="maKH">Mã khách hàng cần lọc (nếu là Member).</param>
        /// <param name="isAdmin">Cờ phân quyền quản trị.</param>
        /// <returns>DataTable gồm 2 cột: Thang (tháng 1-12) và SoLuong (số lượng tài khoản mở).</returns>
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
        //  3. SỐ LƯỢNG TÀI KHOẢN THEO NGÂN HÀNG LIÊN KẾT
        // ============================================================

        /// <summary>
        /// Thống kê số lượng tài khoản theo từng ngân hàng liên kết.
        /// Dùng làm dữ liệu vẽ biểu đồ tròn (Pie Chart) phân bổ ngân hàng.
        /// </summary>
        /// <param name="maKH">Mã khách hàng cần lọc (nếu là Member).</param>
        /// <param name="isAdmin">Cờ phân quyền quản trị.</param>
        /// <returns>DataTable gồm 2 cột: NganHang (Tên ngân hàng) và SoLuong (Số lượng tài khoản).</returns>
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
