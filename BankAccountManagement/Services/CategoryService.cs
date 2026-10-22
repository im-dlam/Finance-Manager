using BankAccountManagement.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Dịch vụ cung cấp danh mục dữ liệu dùng chung (Category / Lookup Service).
    /// Chuyên phụ trách truy vấn các danh mục dùng làm nguồn dữ liệu (DataSource) cho các ComboBox trên giao diện:
    /// - Danh mục Khách hàng (bảng ThongTinKhachHang).
    /// - Danh mục Ngân hàng (bảng NganHang).
    /// - Danh mục Loại tiền tệ (bảng LoaiTien).
    /// - Danh mục Loại giao dịch (bảng LoaiGiaoDich).
    /// </summary>
    public class DanhMucService
    {
        /// <summary>
        /// Chuỗi kết nối tới cơ sở dữ liệu SQL Server.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Hàm khởi tạo của DanhMucService: Nạp tệp cấu hình và chuẩn bị chuỗi kết nối.
        /// </summary>
        public DanhMucService()
        {
            DatabaseConfig config = DatabaseConfigManager.Load();
            _connectionString = DatabaseService.BuildConnectionString(config);
        }

        // ============================================================
        //  DANH MỤC KHÁCH HÀNG (CUSTOMER LOOKUP)
        // ============================================================

        /// <summary>
        /// Lấy danh sách cặp mã và họ tên khách hàng đang hoạt động (TrangThai = 'Active') để nạp vào ComboBox.
        /// </summary>
        /// <returns>Danh sách các cặp giá trị (Mã khách hàng, Họ tên).</returns>
        public List<(string Ma, string Ten)> GetKhachHang()
            => QueryPairs(@"
                SELECT MaKH, HoTen 
                FROM   dbo.ThongTinKhachHang 
                WHERE  TrangThai = N'Active'
                ORDER  BY HoTen");

        // ============================================================
        //  DANH MỤC NGÂN HÀNG (BANK LOOKUP)
        // ============================================================

        /// <summary>
        /// Lấy danh sách cặp mã ngân hàng và tên ngân hàng để nạp vào ComboBox lựa chọn ngân hàng.
        /// </summary>
        /// <returns>Danh sách các cặp giá trị (Mã ngân hàng, Tên ngân hàng).</returns>
        public List<(string Ma, string Ten)> GetNganHang()
            => QueryPairs(@"
                SELECT MaNganHang, TenNganHang 
                FROM   dbo.NganHang 
                ORDER  BY TenNganHang");

        // ============================================================
        //  DANH MỤC LOẠI TIỀN (CURRENCY LOOKUP)
        // ============================================================

        /// <summary>
        /// Lấy danh sách cặp mã loại tiền và tên loại tiền (VND, USD,...) cho ComboBox tiền tệ.
        /// </summary>
        /// <returns>Danh sách các cặp giá trị (Mã tiền tệ, Tên tiền tệ).</returns>
        public List<(string Ma, string Ten)> GetLoaiTien()
            => QueryPairs(@"
                SELECT MaLoaiTien, TenLoaiTien 
                FROM   dbo.LoaiTien 
                ORDER  BY TenLoaiTien");

        // ============================================================
        //  DANH MỤC LOẠI GIAO DỊCH (TRANSACTION TYPE LOOKUP)
        // ============================================================

        /// <summary>
        /// Lấy danh sách cặp mã loại giao dịch và tên loại giao dịch (Nạp tiền, Rút tiền, Chuyển khoản,...).
        /// </summary>
        /// <returns>Danh sách các cặp giá trị (Mã loại giao dịch, Tên loại giao dịch).</returns>
        public List<(string Ma, string Ten)> GetLoaiGiaoDich()
            => QueryPairs(@"
                SELECT CAST(MaLoaiGD AS VARCHAR(10)), TenLoaiGD 
                FROM   dbo.LoaiGiaoDich 
                ORDER  BY TenLoaiGD");

        // ============================================================
        //  PHƯƠNG THỨC TRỢ GIÚP TRUY VẤN NỘI BỘ (INTERNAL QUERY HELPER)
        // ============================================================

        /// <summary>
        /// Thực thi câu lệnh SQL SELECT trả về 2 cột: Cột 0 làm Mã (ValueMember), Cột 1 làm Tên (DisplayMember).
        /// </summary>
        /// <param name="sql">Câu lệnh SQL cần thực thi.</param>
        /// <returns>Danh sách các Tuple (Ma, Ten) tương ứng.</returns>
        private List<(string Ma, string Ten)> QueryPairs(string sql)
        {
            var result = new List<(string, string)>();

            // BƯỚC 1: Khởi tạo và mở kết nối tới cơ sở dữ liệu
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                // BƯỚC 2: Tạo đối tượng SqlCommand và thực thi SqlDataReader
                using (var cmd = new SqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    // BƯỚC 3: Duyệt tuần tự từng dòng kết quả
                    while (reader.Read())
                    {
                        // Kiểm tra an toàn giá trị DBNull trước khi đọc
                        string ma = reader.IsDBNull(0) ? "" : reader.GetValue(0).ToString();
                        string ten = reader.IsDBNull(1) ? "" : reader.GetString(1);

                        // Thêm vào danh sách trả về
                        result.Add((ma, ten));
                    }
                }
            }

            return result;
        }
    }
}
