using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Quản lý trạng thái giỏ hàng trong suốt phiên làm việc của người dùng.
    /// </summary>
    public static class ShoppingCartService
    {
        private static readonly List<CartItem> _items = new List<CartItem>();

        /// <summary>
        /// Sự kiện kích hoạt khi giỏ hàng có sự thay đổi (thêm, sửa, xóa, làm trống).
        /// </summary>
        public static event Action OnCartChanged;

        /// <summary>
        /// Danh sách các sản phẩm đang có trong giỏ hàng.
        /// </summary>
        public static List<CartItem> Items => _items.ToList();

        /// <summary>
        /// Tổng số lượng món hàng trong giỏ.
        /// </summary>
        public static int TotalCount => _items.Sum(i => i.Quantity);

        /// <summary>
        /// Tổng số tiền thanh toán của tất cả món hàng trong giỏ.
        /// </summary>
        public static decimal TotalAmount => _items.Sum(i => i.TotalPrice);

        /// <summary>
        /// Thêm một sản phẩm vào giỏ hàng với số lượng xác định.
        /// </summary>
        public static void AddItem(Product product, int quantity = 1)
        {
            if (product == null || quantity <= 0) return;

            var existing = _items.FirstOrDefault(i => i.Product != null && i.Product.MaSP == product.MaSP);
            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                _items.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity
                });
            }

            NotifyChanged();
        }

        /// <summary>
        /// Cập nhật số lượng của một sản phẩm trong giỏ hàng.
        /// </summary>
        public static void UpdateQuantity(int maSP, int quantity)
        {
            var existing = _items.FirstOrDefault(i => i.Product != null && i.Product.MaSP == maSP);
            if (existing != null)
            {
                if (quantity <= 0)
                {
                    _items.Remove(existing);
                }
                else
                {
                    existing.Quantity = quantity;
                }
                NotifyChanged();
            }
        }

        /// <summary>
        /// Xóa một sản phẩm khỏi giỏ hàng.
        /// </summary>
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
        /// Làm trống toàn bộ giỏ hàng.
        /// </summary>
        public static void Clear()
        {
            if (_items.Count > 0)
            {
                _items.Clear();
                NotifyChanged();
            }
        }

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
