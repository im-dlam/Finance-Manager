namespace BankAccountManagement.Models
{
    /// <summary>
    /// Thực thể đại diện cho một Ngân hàng hoặc chi nhánh ngân hàng đối tác trong hệ thống (bảng NganHang).
    /// </summary>
    public class NganHang
    {
        /// <summary>
        /// Mã định danh duy nhất của ngân hàng (Khóa chính, ví dụ: "VCB", "MB", "TCB", "ACB").
        /// </summary>
        public string MaNganHang { get; set; }

        /// <summary>
        /// Tên gọi thương mại đầy đủ của ngân hàng (Ví dụ: "Ngân hàng TMCP Ngoại thương Việt Nam - Vietcombank").
        /// </summary>
        public string TenNganHang { get; set; }

        /// <summary>
        /// Tên chi nhánh quản lý hoặc địa bàn hoạt động (Ví dụ: "Hội sở chính", "Chi nhánh Ba Đình").
        /// </summary>
        public string ChiNhanh { get; set; }

        /// <summary>
        /// Mã SWIFT/BIC phục vụ nhận diện ngân hàng trong mạng lưới thanh toán tài chính liên ngân hàng quốc tế.
        /// </summary>
        public string SwiftCode { get; set; }
    }
}
