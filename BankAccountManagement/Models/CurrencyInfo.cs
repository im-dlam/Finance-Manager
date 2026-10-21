namespace BankAccountManagement.Models
{
    /// <summary>
    /// Thực thể đại diện cho một đơn vị tiền tệ được quản lý trong hệ thống ngân hàng (bảng LoaiTien).
    /// Hỗ trợ quy đổi tỷ giá tiền tệ sang đồng Việt Nam (VND) để tổng hợp báo cáo tài chính.
    /// </summary>
    public class CurrencyInfo
    {
        /// <summary>
        /// Mã định danh tiền tệ theo chuẩn ISO hoặc quy ước hệ thống (Ví dụ: "VND", "USD", "EUR", "JPY").
        /// </summary>
        public string MaLoaiTien { get; set; }

        /// <summary>
        /// Tên gọi đầy đủ của loại tiền tệ (Ví dụ: "Đồng Việt Nam", "Đô la Mỹ", "Đồng Euro").
        /// </summary>
        public string TenLoaiTien { get; set; }

        /// <summary>
        /// Tỷ giá quy đổi của loại tiền này sang đơn vị cơ sở VNĐ (Ví dụ: 1 USD = 25,450 VNĐ; với VND thì QuyDoi = 1).
        /// </summary>
        public decimal QuyDoi { get; set; }
    }
}
