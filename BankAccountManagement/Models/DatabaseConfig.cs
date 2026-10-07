namespace BankAccountManagement.Models
{
    public class DatabaseConfig
    {
        public string Server { get; set; }

        public string Database { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool WindowsAuthentication { get; set; }
    }
}