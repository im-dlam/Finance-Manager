using BankAccountManagement.Models;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Kế thừa toàn bộ nghiệp vụ từ ShopService để đảm bảo tính tương thích ngược và cấu trúc module đồng nhất.
    /// </summary>
    public class ProductService : ShopService
    {
        /// <summary>Khởi tạo dịch vụ ProductService với cấu hình kết nối CSDL mặc định.</summary>
        public ProductService() : base() { }

        /// <summary>Khởi tạo dịch vụ ProductService với cấu hình cơ sở dữ liệu tùy chỉnh.</summary>
        /// <param name="config">Đối tượng cấu hình kết nối SQL Server.</param>
        public ProductService(DatabaseConfig config) : base(config) { }
    }
}
