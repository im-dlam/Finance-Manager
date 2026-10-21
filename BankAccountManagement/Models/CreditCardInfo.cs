using System;

namespace BankAccountManagement.Models
{
    /// <summary>
    /// Đối tượng chứa thông tin chi tiết của thẻ ngân hàng điện tử (Thẻ tín dụng / Thẻ thanh toán ghi nợ).
    /// Hỗ trợ hiển thị giao diện thẻ ATM trực quan, mô phỏng số thẻ, ngày hết hạn và mã CVV bảo mật.
    /// </summary>
    public class CreditCardInfo
    {
        /// <summary>
        /// Số tài khoản thanh toán ngân hàng được liên kết trực tiếp với thẻ này.
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Mã định danh khách hàng sở hữu thẻ (liên kết bảng ThongTinKhachHang).
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Họ và tên của chủ sở hữu thẻ (thường được in hoa không dấu hoặc có dấu trên mặt thẻ).
        /// </summary>
        public string CardHolderName { get; set; }

        /// <summary>
        /// Dãy số thẻ đầy đủ 16 chữ số được định dạng cách đều từng cụm 4 số (Ví dụ: "4213 5678 9012 3456").
        /// </summary>
        public string FullCardNumber { get; set; }

        /// <summary>
        /// Số thẻ được che giấu bảo mật ở giữa, chỉ hiện 4 số đầu và 4 số cuối (Ví dụ: "4213 **** **** 9876").
        /// </summary>
        public string MaskedCardNumber { get; set; }

        /// <summary>
        /// Loại hình thẻ phát hành: "Thẻ tín dụng" (Credit Card) hoặc "Thẻ thanh toán" (Debit Card).
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Tên ngân hàng phát hành thẻ (Ví dụ: "Vietcombank", "MBBank", "Techcombank").
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Số dư khả dụng hiện tại của tài khoản gắn liền với thẻ.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Loại tiền tệ của tài khoản thẻ (Mặc định là VND, hoặc USD, EUR,...).
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Ngày phát hành / kích hoạt thẻ ngân hàng.
        /// </summary>
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// Ngày hết hạn sử dụng của thẻ (thường có hiệu lực 5 năm kể từ ngày phát hành).
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Trạng thái hoạt động của thẻ: "Active" / "Đang hoạt động" hoặc "Locked" / "Đã khóa".
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Thời điểm ghi nhận trạng thái hiện tại của thẻ ngân hàng.
        /// </summary>
        public DateTime StatusDate { get; set; }

        /// <summary>
        /// Mã bảo mật thẻ CVV (Card Verification Value) gồm 3 chữ số bảo mật ở mặt sau thẻ.
        /// </summary>
        public string CVV { get; set; }

        /// <summary>
        /// Thuộc tính kiểm tra nhanh xem thẻ có đang ở trạng thái hoạt động hay không.
        /// </summary>
        public bool IsActive =>
            string.Equals(Status, "Active", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(Status, "Đang hoạt động", StringComparison.OrdinalIgnoreCase);
    }
}
