using System;

namespace BankAccountManagement.Models
{
    public class TaiKhoan
    {
        public string SoTaiKhoan { get; set; }
        public string MaKH { get; set; }
        public string MaNganHang { get; set; }
        public string MaLoaiTien { get; set; }
        public decimal SoDuBanDau { get; set; }
        public decimal SoDuHienTai { get; set; }
        public DateTime NgayMo { get; set; }
        public string TrangThai { get; set; }
    }
}