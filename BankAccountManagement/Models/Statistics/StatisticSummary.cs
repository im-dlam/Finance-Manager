namespace BankAccountManagement.Models.Statistics
{
    /// <summary>
    /// Đối tượng tổng hợp các chỉ số tài chính và hoạt động cốt lõi của toàn bộ hệ thống ngân hàng.
    /// Thường được hiển thị trên các thẻ Widget Card đầu tiên của trang Tổng quan thống kê (Statistics Dashboard).
    /// </summary>
    public class StatisticSummary
    {
        /// <summary>
        /// Tổng số lượng tài khoản thanh toán đã phát hành trong hệ thống.
        /// </summary>
        public int TotalAccounts { get; set; }

        /// <summary>
        /// Tổng số dư tiền gửi khả dụng quy đổi sang đơn vị VNĐ trên toàn bộ tài khoản.
        /// </summary>
        public decimal TotalBalance { get; set; }

        /// <summary>
        /// Tổng số lượng giao dịch tài chính (Nạp, Rút, Chuyển tiền, Mua sắm) đã thực hiện.
        /// </summary>
        public int TotalTransactions { get; set; }

        /// <summary>
        /// Tổng số lượng khách hàng cá nhân/doanh nghiệp đã đăng ký thông tin trong hệ thống.
        /// </summary>
        public int TotalCustomers { get; set; }
    }
}
