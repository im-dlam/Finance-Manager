using System;

namespace BankAccountManagement.Models
{
    /// <summary>
    /// Thực thể đại diện cho một Tài khoản ngân hàng (bảng dbo.TaiKhoan).
    /// Chứa thông tin số tài khoản, khách hàng sở hữu, ngân hàng phát hành, loại tiền và số dư.
    /// </summary>
    public class TaiKhoan
    {
        /// <summary>
        /// Số tài khoản ngân hàng duy nhất (Khóa chính, ví dụ: "1029384756").
        /// </summary>
        public string SoTaiKhoan { get; set; }

        /// <summary>
        /// Mã khách hàng sở hữu tài khoản này (Khóa ngoại tham chiếu ThongTinKhachHang).
        /// </summary>
        public string MaKH { get; set; }

        /// <summary>
        /// Mã ngân hàng nơi tài khoản được mở (Khóa ngoại tham chiếu NganHang).
        /// </summary>
        public string MaNganHang { get; set; }

        /// <summary>
        /// Mã loại tiền tệ sử dụng cho tài khoản (Ví dụ: "VND", "USD", tham chiếu LoaiTien).
        /// </summary>
        public string MaLoaiTien { get; set; }

        /// <summary>
        /// Số dư ban đầu tại thời điểm mở tài khoản (VNĐ hoặc nguyên tệ).
        /// </summary>
        public decimal SoDuBanDau { get; set; }

        /// <summary>
        /// Số dư khả dụng hiện tại của tài khoản (VNĐ hoặc nguyên tệ).
        /// </summary>
        public decimal SoDuHienTai { get; set; }

        /// <summary>
        /// Thời điểm tài khoản được kích hoạt và mở trên hệ thống.
        /// </summary>
        public DateTime NgayMo { get; set; }

        /// <summary>
        /// Trạng thái hoạt động của tài khoản: "Active" / "Đang hoạt động" hoặc "Locked" / "Đã khóa".
        /// </summary>
        public string TrangThai { get; set; }
    }
}
