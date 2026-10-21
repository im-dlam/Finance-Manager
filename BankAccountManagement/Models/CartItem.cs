namespace BankAccountManagement.Models
{
    /// <summary>
    /// Đối tượng thể hiện một mặt hàng được thêm vào giỏ hàng mua sắm của người dùng.
    /// </summary>
    public class CartItem
    {
        /// <summary>Thông tin sản phẩm tương ứng trong kho.</summary>
        public Product Product { get; set; }

        /// <summary>Số lượng sản phẩm khách hàng chọn mua.</summary>
        public int Quantity { get; set; }

        /// <summary>Tổng tiền của dòng hàng này (Số lượng x Đơn giá).</summary>
        public decimal TotalPrice => Product != null ? Product.GiaBan * Quantity : 0m;

        /// <summary>Tổng tiền định dạng có dấu phẩy phân cách hàng nghìn (VD: 21,980,000 đ).</summary>
        public string FormattedTotalPrice => $"{TotalPrice:N0} đ";
    }
}
