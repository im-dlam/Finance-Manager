using System;

namespace BankAccountManagement.Models
{
    /// <summary>
    /// Đại diện cho một người dùng (bảng NguoiDung).
    /// Quyền được lưu trực tiếp dạng chuỗi: 'Admin' hoặc 'Member'.
    /// </summary>
    public class User
    {
        public int MaNguoiDung { get; set; }
        public string TenDangNhap { get; set; }

        public string Quyen { get; set; }

        /// <summary>Mã KH liên kết – NULL nếu chưa link với KhachHang.</summary>
        public string MaKH { get; set; }

        public string HoTen { get; set; }
        public string Email { get; set; }
        public string TrangThai { get; set; }

        /// <summary>Kiểm tra nhanh có phải Admin không.</summary>
        public bool IsAdmin =>
            string.Equals(Quyen, "Admin", StringComparison.OrdinalIgnoreCase);
    }
}