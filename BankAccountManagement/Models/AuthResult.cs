namespace BankAccountManagement.Models
{
    /// <summary>
    /// Đối tượng chứa kết quả xác thực danh tính người dùng (Đăng nhập, Đăng ký, Đổi mật khẩu).
    /// Giúp đóng gói kết quả trả về gồm cờ thành công/thất bại, thông điệp phản hồi và dữ liệu người dùng.
    /// </summary>
    public class AuthResult
    {
        /// <summary>
        /// Trạng thái kết quả của thao tác xác thực: True nếu thành công, False nếu thất bại.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Thông điệp phản hồi chi tiết (ví dụ thông báo lỗi hoặc lời chào mừng đăng nhập thành công).
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Thực thể thông tin người dùng sau khi xác thực thành công (mang giá trị null nếu thất bại).
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Phương thức tiện ích (Factory Method) khởi tạo kết quả xác thực thành công kèm thông tin người dùng.
        /// </summary>
        /// <param name="user">Đối tượng User vừa đăng nhập thành công.</param>
        /// <param name="message">Thông điệp thông báo kèm theo (tuỳ chọn).</param>
        /// <returns>Đối tượng AuthResult với Success = true.</returns>
        public static AuthResult Ok(User user, string message = null)
            => new AuthResult { Success = true, User = user, Message = message };

        /// <summary>
        /// Phương thức tiện ích khởi tạo kết quả thao tác thành công (ví dụ đổi mật khẩu thành công).
        /// </summary>
        /// <param name="message">Thông điệp thông báo thành công.</param>
        /// <returns>Đối tượng AuthResult với Success = true.</returns>
        public static AuthResult Ok(string message)
            => new AuthResult { Success = true, Message = message };

        /// <summary>
        /// Phương thức tiện ích khởi tạo kết quả thao tác thất bại kèm nguyên nhân lỗi.
        /// </summary>
        /// <param name="message">Nội dung thông báo lỗi để hiển thị lên giao diện người dùng.</param>
        /// <returns>Đối tượng AuthResult với Success = false.</returns>
        public static AuthResult Fail(string message)
            => new AuthResult { Success = false, Message = message };
    }
}
