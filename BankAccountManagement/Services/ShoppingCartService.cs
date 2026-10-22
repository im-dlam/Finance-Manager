using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Dịch vụ quản lý Giỏ hàng mua sắm trong bộ nhớ tạm (In-Memory Shopping Cart Service).
    /// Đây là một static class duy trì trạng thái giỏ hàng xuyên suốt phiên làm việc của người dùng:
    /// - Lưu trữ danh sách các mặt hàng được chọn mua.
    /// - Tự động cộng dồn số lượng nếu sản phẩm đã tồn tại trong giỏ.
    /// - Tính toán tổng số lượng và tổng số tiền thanh toán thời gian thực qua LINQ.
    /// - Phát tín hiệu sự kiện OnCartChanged để các Form giao diện tự động cập nhật số lượng badge giỏ hàng.
    /// </summary>
    public static class ShoppingCartService
    {
        /// <summary>
        /// Danh sách nội bộ lưu trữ các món hàng trong giỏ hàng.
        /// </summary>
        private static readonly List<CartItem> _items = new List<CartItem>();

        /// <summary>
        /// Sự kiện phát ra thông báo khi giỏ hàng có sự thay đổi (Thêm, Sửa số lượng, Xóa món, Làm trống giỏ).
        /// Giúp các thành phần UI (như biểu tượng giỏ hàng trên Header) cập nhật ngay số lượng hiển thị.
        /// </summary>
        public static event Action OnCartChanged;

        /// <summary>
        /// Lấy danh sách bản sao các sản phẩm hiện có trong giỏ hàng (sử dụng ToList để bảo vệ tính đóng gói).
        /// </summary>
        public static List<CartItem> Items => _items.ToList();

        /// <summary>
        /// Tổng số lượng tất cả các món hàng đang có trong giỏ hàng (tính bằng LINQ Sum).
        /// </summary>
        public static int TotalCount => _items.Sum(i => i.Quantity);

        /// <summary>
        /// Tổng số tiền thanh toán của toàn bộ giỏ hàng (VNĐ) tính bằng tổng TotalPrice của từng món.
        /// </summary>
        public static decimal TotalAmount => _items.Sum(i => i.TotalPrice);

        /// <summary>
        /// Thêm một sản phẩm vào giỏ hàng với số lượng tùy chọn (mặc định là 1).
        /// Nếu sản phẩm đã có sẵn trong giỏ, hệ thống sẽ tự động cộng dồn thêm số lượng mới.
        /// </summary>
        /// <param name="product">Đối tượng Product cần thêm vào giỏ.</param>
        /// <param name="quantity">Số lượng cần mua thêm (phải lớn hơn 0).</param>
        public static void AddItem(Product product, int quantity = 1)
        {
            // BƯỚC 1: Kiểm tra tính hợp lệ của tham số
            if (product == null || quantity <= 0) return;

            // BƯỚC 2: Tìm xem sản phẩm này đã từng được thêm vào giỏ trước đó hay chưa
            var existing = _items.FirstOrDefault(i => i.Product != null && i.Product.MaSP == product.MaSP);
            if (existing != null)
            {
                // Nếu đã có: Cộng dồn thêm số lượng
                existing.Quantity += quantity;
            }
            else
            {
                // Nếu chưa có: Tạo mới một CartItem và thêm vào danh sách
                _items.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity
                });
            }

            // BƯỚC 3: Phát tín hiệu thông báo giỏ hàng đã thay đổi để UI làm mới
            NotifyChanged();
        }

        /// <summary>
        /// Cập nhật số lượng của một sản phẩm trong giỏ hàng.
        /// Nếu số lượng mới &lt;= 0, sản phẩm sẽ tự động bị xóa khỏi giỏ hàng.
        /// </summary>
        /// <param name="maSP">Mã định danh sản phẩm cần thay đổi số lượng.</param>
        /// <param name="quantity">Số lượng mới muốn thiết lập.</param>
        public static void UpdateQuantity(int maSP, int quantity)
        {
            var existing = _items.FirstOrDefault(i => i.Product != null && i.Product.MaSP == maSP);
            if (existing != null)
            {
                // BƯỚC 1: Nếu số lượng <= 0 thì xóa hẳn khỏi giỏ
                if (quantity <= 0)
                {
                    _items.Remove(existing);
                }
                else
                {
                    // BƯỚC 2: Ngược lại thì gán số lượng mới
                    existing.Quantity = quantity;
                }

                // BƯỚC 3: Phát sự kiện thông báo thay đổi
                NotifyChanged();
            }
        }

        /// <summary>
        /// Xóa một mặt hàng ra khỏi giỏ hàng dựa theo mã sản phẩm.
        /// </summary>
        /// <param name="maSP">Mã sản phẩm cần gỡ bỏ.</param>
        public static void RemoveItem(int maSP)
        {
            var existing = _items.FirstOrDefault(i => i.Product != null && i.Product.MaSP == maSP);
            if (existing != null)
            {
                _items.Remove(existing);
                NotifyChanged();
            }
        }

        /// <summary>
        /// Làm trống toàn bộ giỏ hàng (xóa sạch tất cả các mặt hàng).
        /// Thường được gọi sau khi thanh toán đơn hàng thành công.
        /// </summary>
        public static void Clear()
        {
            if (_items.Count > 0)
            {
                _items.Clear();
                NotifyChanged();
            }
        }

        /// <summary>
        /// Kích hoạt sự kiện OnCartChanged một cách an toàn có bẫy lỗi để tránh làm gián đoạn chương trình.
        /// </summary>
        private static void NotifyChanged()
        {
            try
            {
                OnCartChanged?.Invoke();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ShoppingCartService] Notify error: {ex.Message}");
            }
        }
    }
}
