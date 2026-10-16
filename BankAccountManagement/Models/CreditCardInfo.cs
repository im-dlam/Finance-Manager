using System;

namespace BankAccountManagement.Models
{
    /// <summary>
    /// Đối tượng chứa thông tin chi tiết của thẻ ngân hàng (Thẻ tín dụng / Thẻ thanh toán).
    /// </summary>
    public class CreditCardInfo
    {
        /// <summary>
        /// Số tài khoản ngân hàng liên kết với thẻ.
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Mã khách hàng sở hữu thẻ.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Họ và tên của chủ sở hữu thẻ (viết hoa hiển thị trên thẻ).
        /// </summary>
        public string CardHolderName { get; set; }

        /// <summary>
        /// Số thẻ đầy đủ 16 chữ số được định dạng cách đều từng cụm 4 chữ số.
        /// </summary>
        public string FullCardNumber { get; set; }

        /// <summary>
        /// Số thẻ được che bảo mật dạng 4213 **** **** 9876.
        /// </summary>
        public string MaskedCardNumber { get; set; }

        /// <summary>
        /// Loại thẻ hiển thị: "Thẻ tín dụng" hoặc "Thẻ thanh toán".
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Tên thương hiệu thẻ hoặc tên ngân hàng liên kết.
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Số dư hiện tại của tài khoản/thẻ.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Đơn vị tiền tệ của thẻ (VND, USD,...).
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Ngày phát hành / cấp thẻ.
        /// </summary>
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// Ngày hết hạn thẻ (thường là 5 năm sau ngày cấp thẻ).
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Trạng thái hoạt động của thẻ: "Active" (Đang hoạt động) hoặc "Locked" (Đã khóa).
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Ngày ghi nhận trạng thái của thẻ (lấy ngày hôm nay).
        /// </summary>
        public DateTime StatusDate { get; set; }

        /// <summary>
        /// Mã số bảo mật CVV phía sau thẻ (3 chữ số).
        /// </summary>
        public string CVV { get; set; }

        /// <summary>
        /// Thuộc tính kiểm tra nhanh thẻ có đang hoạt động hay không.
        /// </summary>
        public bool IsActive =>
            string.Equals(Status, "Active", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(Status, "Đang hoạt động", StringComparison.OrdinalIgnoreCase);
    }
}
