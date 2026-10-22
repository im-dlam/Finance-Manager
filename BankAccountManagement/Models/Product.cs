using System;

namespace BankAccountManagement.Models
{
    /// <summary>
    /// Thực thể đại diện cho một sản phẩm hàng hóa trong phân hệ Cửa hàng Mua sắm (Shopping / Marketplace).
    /// Ánh xạ tương ứng với bảng dữ liệu dbo.SanPham trong cơ sở dữ liệu.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Mã định danh duy nhất của sản phẩm (Khóa chính tự tăng IDENTITY trong bảng dbo.SanPham).
        /// </summary>
        public int MaSP { get; set; }

        /// <summary>
        /// Tên gọi hiển thị đầy đủ của sản phẩm (Ví dụ: "iPhone 15 Pro Max 256GB").
        /// </summary>
        public string TenSP { get; set; }

        /// <summary>
        /// Đơn giá bán niêm yết của sản phẩm (Đơn vị: VNĐ).
        /// </summary>
        public decimal GiaBan { get; set; }

        /// <summary>
        /// Đường dẫn file ảnh hoặc tên biểu tượng icon đại diện cho sản phẩm.
        /// </summary>
        public string HinhAnh { get; set; }

        /// <summary>
        /// Đoạn văn bản giới thiệu, mô tả chi tiết công năng và đặc điểm nổi bật của sản phẩm.
        /// </summary>
        public string MoTa { get; set; }

        /// <summary>
        /// Thông số kỹ thuật chi tiết của sản phẩm (Ví dụ: RAM, CPU, Bộ nhớ trong, Kích thước).
        /// </summary>
        public string ThongSo { get; set; }

        /// <summary>
        /// Số lượng hàng hóa thực tế còn lại khả dụng trong kho.
        /// </summary>
        public int SoLuongTon { get; set; }

        /// <summary>
        /// Trạng thái kinh doanh của sản phẩm: "Active" / "Hoạt động" hoặc "Inactive" / "Ngừng bán".
        /// </summary>
        public string TrangThai { get; set; }

        /// <summary>
        /// Thời điểm sản phẩm được tạo mới và niêm yết lên hệ thống.
        /// </summary>
        public DateTime NgayTao { get; set; }

        /// <summary>
        /// Chuỗi định dạng đơn giá bán có dấu phẩy phân cách hàng nghìn và đơn vị VNĐ (Ví dụ: 29,990,000 đ).
        /// </summary>
        public string FormattedPrice => $"{GiaBan:N0} đ";

        /// <summary>
        /// Kiểm tra nhanh xem sản phẩm có đủ điều kiện bán (còn tồn kho > 0 và trạng thái hoạt động).
        /// </summary>
        public bool IsInStock => SoLuongTon > 0 && (TrangThai == "Active" || TrangThai == "Hoạt động");
    }
}
