namespace BankAccountManagement.Models.Statistics
{
    /// <summary>
    /// Đối tượng thể hiện dữ liệu xếp hạng tài khoản có số dư lớn nhất trong ngân hàng (Top Accounts).
    /// Hỗ trợ ban giám đốc nhận diện nhóm khách hàng ưu tiên (VIP/Priority) để chăm sóc đặc biệt.
    /// </summary>
    public class TopAccountStatistic
    {
        /// <summary>
        /// Thứ hạng xếp hạng số dư trong danh sách (Bắt đầu từ hạng 1, 2, 3,...).
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng của khách hàng.
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// Họ và tên của chủ sở hữu tài khoản thanh toán.
        /// </summary>
        public string AccountHolder { get; set; }

        /// <summary>
        /// Số dư hiện tại theo loại tiền gốc của tài khoản.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Đơn vị loại tiền gốc của tài khoản (Ví dụ: "VND", "USD").
        /// </summary>
        public string AccountCurrency { get; set; }

        /// <summary>
        /// Số dư đã được quy đổi sang đồng Việt Nam (VNĐ) theo tỷ giá niêm yết để phục vụ so sánh xếp hạng công bằng.
        /// </summary>
        public decimal BalanceVND { get; set; }
    }
}
