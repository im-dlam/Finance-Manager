using BankAccountManagement.Models;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Dịch vụ quản lý thông tin Sản phẩm hàng hóa (Product Service).
    /// Kế thừa toàn bộ logic nghiệp vụ từ <see cref="ShopService"/>:
    /// - Quản lý danh mục sản phẩm (thêm mới, cập nhật, xóa, tìm kiếm).
    /// - Quản lý tồn kho hàng hóa và cập nhật trạng thái kinh doanh.
    /// - Đảm bảo tính tương thích ngược và cấu trúc phân tầng Service hướng đối tượng chuẩn chỉ.
    /// </summary>
    public class ProductService : ShopService
    {
        /// <summary>
        /// Khởi tạo dịch vụ ProductService với cấu hình kết nối CSDL mặc định đọc từ database.ini.
        /// </summary>
        public ProductService() : base()
        {
        }

        /// <summary>
        /// Khởi tạo dịch vụ ProductService với cấu hình cơ sở dữ liệu tùy chỉnh.
        /// </summary>
        /// <param name="config">Đối tượng cấu hình kết nối SQL Server.</param>
        public ProductService(DatabaseConfig config) : base(config)
        {
        }
    }
}
