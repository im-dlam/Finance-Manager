using System;

namespace BankAccountManagement.Models
{
    /// <summary>
    /// Thực thể đại diện cho một người dùng tài khoản trong hệ thống (bảng dbo.NguoiDung).
    /// Hỗ trợ phân quyền hệ thống qua trường Quyen: "Admin" (Quản trị viên) hoặc "Member" (Khách hàng thông thường).
    /// </summary>
    public class User
    {
        /// <summary>
        /// Mã định danh số tự tăng của tài khoản người dùng (Khóa chính bảng dbo.NguoiDung).
        /// </summary>
        public int MaNguoiDung { get; set; }

        /// <summary>
        /// Tên tài khoản dùng để đăng nhập vào phần mềm.
        /// </summary>
        public string TenDangNhap { get; set; }

        /// <summary>
        /// Chuỗi quyền hạn của người dùng: "Admin" (Quản trị viên) hoặc "Member" (Thành viên/Khách hàng).
        /// </summary>
        public string Quyen { get; set; }

        /// <summary>
        /// Mã khách hàng liên kết với tài khoản (Giá trị NULL nếu tài khoản chưa liên kết hồ sơ khách hàng).
        /// </summary>
        public string MaKH { get; set; }

        /// <summary>
        /// Họ và tên hiển thị của người dùng (ưu tiên lấy từ hồ sơ khách hàng).
        /// </summary>
        public string HoTen { get; set; }

        /// <summary>
        /// Địa chỉ thư điện tử Email của người dùng.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Mã ngân hàng liên kết mặc định của người dùng.
        /// </summary>
        public string MaNganHang { get; set; }

        /// <summary>
        /// Tên ngân hàng liên kết tương ứng với MaNganHang.
        /// </summary>
        public string TenNganHang { get; set; }

        /// <summary>
        /// Trạng thái kích hoạt của tài khoản người dùng ("Active" hoặc "Locked").
        /// </summary>
        public string TrangThai { get; set; }

        /// <summary>
        /// Thời điểm đăng nhập gần nhất. Mang giá trị NULL nếu đây là lần đầu tiên người dùng đăng nhập hệ thống.
        /// </summary>
        public DateTime? LanDangNhap { get; set; }

        /// <summary>
        /// Cờ đánh dấu đây có phải là lần đăng nhập đầu tiên hay không (True nếu LanDangNhap là null).
        /// </summary>
        public bool IsFirstLogin { get; set; }

        /// <summary>
        /// Cờ đánh dấu người dùng đã cập nhật đầy đủ và hợp lệ hồ sơ khách hàng (CMND/CCCD thật, SĐT thật, địa chỉ, ngày sinh).
        /// </summary>
        public bool HasCompleteProfile { get; set; }

        /// <summary>
        /// Thuộc tính tiện ích kiểm tra nhanh xem tài khoản hiện tại có quyền Quản trị viên (Admin) hay không.
        /// So sánh chuỗi Quyen không phân biệt chữ hoa, chữ thường.
        /// </summary>
        public bool IsAdmin =>
            string.Equals(Quyen, "Admin", StringComparison.OrdinalIgnoreCase);
    }
}
