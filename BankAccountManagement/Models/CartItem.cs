namespace BankAccountManagement.Models
{
    /// <summary>
    /// Đối tượng thể hiện một mặt hàng được thêm vào giỏ hàng mua sắm (Shopping Cart) của người dùng.
    /// Giúp theo dõi sản phẩm được chọn, số lượng mua và tính toán tổng tiền tạm tính của từng dòng hàng.
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// Thông tin chi tiết của sản phẩm tương ứng trong kho hàng (giá bán, tên, tồn kho,...).
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Số lượng sản phẩm khách hàng chọn mua trong giỏ hàng.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Tổng thành tiền của mặt hàng này trong giỏ: Đơn giá bán x Số lượng mua.
        /// </summary>
        public decimal TotalPrice => Product != null ? Product.GiaBan * Quantity : 0m;

        /// <summary>
        /// Tổng tiền định dạng có dấu phẩy phân cách hàng nghìn và đơn vị VNĐ (Ví dụ: 21,980,000 đ).
        /// </summary>
        public string FormattedTotalPrice => $"{TotalPrice:N0} đ";
    }
}
