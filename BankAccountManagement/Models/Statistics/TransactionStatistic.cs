namespace BankAccountManagement.Models.Statistics
{
    /// <summary>
    /// Đối tượng chứa số liệu thống kê biến động giao dịch theo từng tháng trong năm.
    /// Dùng để vẽ biểu đồ đường (Line Chart) hoặc biểu đồ cột (Bar Chart) xu hướng phát triển giao dịch.
    /// </summary>
    public class TransactionStatistic
    {
        /// <summary>
        /// Năm diễn ra các giao dịch được thống kê (Ví dụ: 2026).
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Tháng diễn ra các giao dịch trong năm (Từ tháng 1 đến tháng 12).
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Tổng số lượng giao dịch đã phát sinh và hoàn tất trong tháng đó.
        /// </summary>
        public int TransactionCount { get; set; }

        /// <summary>
        /// Nhãn hiển thị trực quan ngắn gọn trên trục hoành biểu đồ (Ví dụ: "T10/2026").
        /// </summary>
        public string MonthLabel => $"T{Month}/{Year}";
    }
}
