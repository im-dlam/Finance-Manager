using System;

namespace BankAccountManagement.Models
{
    /// <summary>
    /// Đối tượng thực thể đại diện cho một sản phẩm hàng hóa trong phân hệ Mua sắm (Shopping).
    /// </summary>
    public class Product
    {
        /// <summary>Mã định danh duy nhất của sản phẩm (Khóa chính tự tăng trong CSDL dbo.SanPham).</summary>
        public int MaSP { get; set; }

        /// <summary>Tên hiển thị đầy đủ của sản phẩm.</summary>
        public string TenSP { get; set; }

        /// <summary>Đơn giá bán niêm yết của sản phẩm (đơn vị: VNĐ).</summary>
        public decimal GiaBan { get; set; }

        /// <summary>Đường dẫn tương đối/tuyệt đối tới file ảnh hoặc ký tự biểu tượng icon đại diện.</summary>
        public string HinhAnh { get; set; }

        /// <summary>Bài viết / đoạn văn bản mô tả chi tiết tính năng của sản phẩm.</summary>
        public string MoTa { get; set; }

        /// <summary>Thông số kỹ thuật phần cứng, kích thước hoặc cấu hình của sản phẩm.</summary>
        public string ThongSo { get; set; }

        /// <summary>Số lượng sản phẩm còn lại khả dụng trong kho hàng.</summary>
        public int SoLuongTon { get; set; }

        /// <summary>Trạng thái kinh doanh của sản phẩm ('Active' / 'Inactive').</summary>
        public string TrangThai { get; set; }

        /// <summary>Thời điểm sản phẩm được tạo và đưa lên hệ thống.</summary>
        public DateTime NgayTao { get; set; }

        /// <summary>Chuỗi định dạng tiền tệ có dấu phân cách hàng nghìn (VD: 10,990,000 đ).</summary>
        public string FormattedPrice => $"{GiaBan:N0} đ";

        /// <summary>Kiểm tra xem sản phẩm có đang còn hàng trong kho và ở trạng thái hoạt động hay không.</summary>
        public bool IsInStock => SoLuongTon > 0 && (TrangThai == "Active" || TrangThai == "Hoạt động");
    }
}
