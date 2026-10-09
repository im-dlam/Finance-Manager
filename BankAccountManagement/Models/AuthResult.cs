namespace BankAccountManagement.Models
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        /// <summary>Người dùng sau khi đăng nhập thành công (null nếu thất bại).</summary>
        public User User { get; set; }

        public static AuthResult Ok(User user, string message = null)
            => new AuthResult { Success = true, User = user, Message = message };

        public static AuthResult Ok(string message)
            => new AuthResult { Success = true, Message = message };

        public static AuthResult Fail(string message)
            => new AuthResult { Success = false, Message = message };
    }
}