using System;
using System.Data;
using System.Data.SqlClient;
using BankAccountManagement.Models;

namespace BankAccountManagement.Services
{
    public class CustomerService
    {
        // Hàm lấy chuỗi kết nối Database từ cấu hình chung
        private string GetConnectionString()
        {
            DatabaseConfig config = DatabaseConfigManager.Load();
            return DatabaseService.BuildConnectionString(config);
        }

        // 1. Lấy toàn bộ danh sách khách hàng
        public DataTable GetAllCustomers()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = "SELECT MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, TrangThai FROM KhachHang WHERE TrangThai = 'Active' ORDER BY NgayTao DESC";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        // 2. Tìm kiếm khách hàng theo Tên, SĐT, hoặc CMND
        public DataTable SearchCustomers(string keyword)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = @"SELECT MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, TrangThai 
                               FROM KhachHang 
                               WHERE TrangThai = 'Active' AND (MaKH LIKE @Keyword OR HoTen LIKE @Keyword OR SoDienThoai LIKE @Keyword OR CMND_CCCD LIKE @Keyword)
                               ORDER BY NgayTao DESC";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        // 3. Thêm mới khách hàng
        public bool AddCustomer(string maKH, string hoTen, string cmnd, DateTime ngaySinh, string gioiTinh, string sdt, string email, string diaChi)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                string sql = @"INSERT INTO KhachHang (MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, TrangThai, NgayTao) 
                               VALUES (@MaKH, @HoTen, @CMND, @NgaySinh, @GioiTinh, @SDT, @Email, @DiaChi, 'Active', GETDATE())";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", maKH);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@CMND", cmnd);
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh.Date); // Chỉ lấy phần ngày
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@SDT", sdt);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                    
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 4. Cập nhật thông tin khách hàng
        public bool UpdateCustomer(string maKH, string hoTen, string cmnd, DateTime ngaySinh, string gioiTinh, string sdt, string email, string diaChi)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                string sql = @"UPDATE KhachHang 
                               SET HoTen = @HoTen, CMND_CCCD = @CMND, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, 
                                   SoDienThoai = @SDT, Email = @Email, DiaChi = @DiaChi 
                               WHERE MaKH = @MaKH";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", maKH);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@CMND", cmnd);
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh.Date);
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@SDT", sdt);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                    
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 5. Xóa khách hàng (Chỉ nên chuyển trạng thái thành Inactive để không bị lỗi lịch sử giao dịch)
        public bool DeleteCustomer(string maKH)
        {
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                conn.Open();
                // Thực hiện Xóa cứng (Hard Delete) khỏi database để giải phóng Mã KH
                string sql = "DELETE FROM KhachHang WHERE MaKH = @MaKH";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", maKH);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // 6. Lấy thống kê số liệu cho 4 thẻ (Cards) trên cùng
        public DataTable GetCustomerStatistics()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                string sql = @"
                    SELECT 
                        (SELECT COUNT(*) FROM KhachHang) AS TongKhachHang,
                        (SELECT COUNT(*) FROM KhachHang WHERE TrangThai = 'Active') AS DangHoatDong,
                        (SELECT COUNT(*) FROM KhachHang WHERE MONTH(NgayTao) = MONTH(GETDATE()) AND YEAR(NgayTao) = YEAR(GETDATE())) AS MoiTrongThang,
                        (SELECT COUNT(*) FROM TaiKhoan) AS TaiKhoanDaMo
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
