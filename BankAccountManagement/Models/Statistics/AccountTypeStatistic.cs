namespace BankAccountManagement.Models.Statistics
{
    /// <summary>
    /// Đối tượng chứa số liệu thống kê số lượng tài khoản theo từng phân loại tiền tệ / loại tài khoản.
    /// Dùng để vẽ biểu đồ tròn hoặc biểu đồ cột phân bổ tài khoản trong hệ thống.
    /// </summary>
    public class AccountTypeStatistic
    {
        /// <summary>
        /// Tên phân loại tài khoản hoặc mã loại tiền (Ví dụ: "VND", "USD", "EUR").
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        /// Tổng số lượng tài khoản thuộc phân loại này đang được mở trong ngân hàng.
        /// </summary>
        public int AccountCount { get; set; }
    }
}
