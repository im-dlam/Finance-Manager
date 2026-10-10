namespace BankAccountManagement.Models
{
    public class User
    {
        public int MaNguoiDung { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string Quyen { get; set; } // 'Admin' hoặc 'Member'
        public string MaKH { get; set; }
    }
}