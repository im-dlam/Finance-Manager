namespace BankAccountManagement.Models
{
    /// <summary>
    /// Đối tượng lưu trữ thông số cấu hình chuỗi kết nối đến hệ quản trị cơ sở dữ liệu Microsoft SQL Server.
    /// Dữ liệu này được đọc/ghi từ file cấu hình Config/database.ini của ứng dụng.
    /// </summary>
    public class DatabaseConfig
    {
        /// <summary>
        /// Tên máy chủ SQL Server hoặc địa chỉ mạng IP (Ví dụ: "localhost", ".\SQLEXPRESS", "(localdb)\MSSQLLocalDB").
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Tên cơ sở dữ liệu cần kết nối thao tác trong hệ thống (Mặc định: "BankM36").
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Tên tài khoản SQL Server khi sử dụng chế độ xác thực SQL Server Authentication (Ví dụ: "sa").
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Mật khẩu đăng nhập SQL Server tương ứng với tài khoản Username.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Cờ xác thực tài khoản Windows (Windows Authentication): 
        /// True nếu sử dụng chứng thực trực tiếp của tài khoản người dùng Windows hiện tại;
        /// False nếu sử dụng tài khoản/mật khẩu SQL Server (Username/Password).
        /// </summary>
        public bool WindowsAuthentication { get; set; }
    }
}
