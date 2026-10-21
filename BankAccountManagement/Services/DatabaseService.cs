using BankAccountManagement.Models;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Lớp cung cấp các dịch vụ hạ tầng cơ sở dữ liệu cấp thấp (Database Service).
    /// Chịu trách nhiệm:
    /// - Tạo chuỗi kết nối an toàn (Connection String) thông qua SqlConnectionStringBuilder.
    /// - Kiểm tra sự tồn tại của Database trên máy chủ SQL Server.
    /// - Tự động tạo cơ sở dữ liệu mới và thực thi kịch bản tạo bảng (Schema Script DDL).
    /// - Khởi tạo các đối tượng SqlConnection cho toàn bộ các dịch vụ nghiệp vụ khác sử dụng.
    /// </summary>
    public static class DatabaseService
    {
        /// <summary>
        /// Tạo chuỗi kết nối (Connection String) chuẩn tới CSDL ứng dụng dựa trên đối tượng cấu hình.
        /// Sử dụng SqlConnectionStringBuilder để tự động định dạng và tránh các lỗi cú pháp chuỗi.
        /// </summary>
        /// <param name="config">Cấu hình kết nối cơ sở dữ liệu.</param>
        /// <returns>Chuỗi kết nối SQL Server hợp lệ.</returns>
        public static string BuildConnectionString(DatabaseConfig config)
        {
            // BƯỚC 1: Khởi tạo Builder quản lý tham số chuỗi kết nối
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            // BƯỚC 2: Cài đặt thông số Máy chủ và Tên cơ sở dữ liệu mục tiêu
            builder.DataSource = config.Server;
            builder.InitialCatalog = config.Database;

            // BƯỚC 3: Bật TrustServerCertificate để bỏ qua xác thực SSL chứng chỉ tự ký trên máy cục bộ (Localhost)
            builder.TrustServerCertificate = true;

            // BƯỚC 4: Đặt thời gian chờ kết nối tối đa 5 giây tránh việc ứng dụng bị treo khi máy chủ không phản hồi
            builder.ConnectTimeout = 5;

            // BƯỚC 5: Thiết lập phương thức xác thực (Windows Auth vs SQL Auth)
            if (config.WindowsAuthentication)
            {
                // Chế độ xác thực bằng tài khoản Windows hiện tại
                builder.IntegratedSecurity = true;
            }
            else
            {
                // Chế độ xác thực bằng tài khoản/mật khẩu SQL Server riêng biệt
                builder.IntegratedSecurity = false;
                builder.UserID = config.Username;
                builder.Password = config.Password;
            }

            return builder.ConnectionString;
        }

        /// <summary>
        /// Kiểm tra xem cơ sở dữ liệu mục tiêu đã tồn tại trên máy chủ SQL Server hay chưa.
        /// Thực hiện kết nối tới database hệ thống 'master' và truy vấn bảng 'sys.databases'.
        /// </summary>
        /// <param name="config">Cấu hình kết nối cơ sở dữ liệu.</param>
        /// <returns>Task trả về True nếu CSDL đã tồn tại; ngược lại là False.</returns>
        public static async Task<bool> DatabaseExistsAsync(DatabaseConfig config)
        {
            // BƯỚC 1: Tạo chuỗi kết nối tới cơ sở dữ liệu hệ thống master
            string connectionString = BuildMasterConnectionString(config);

            // BƯỚC 2: Mở kết nối bất đồng bộ tới SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // BƯỚC 3: Truy vấn đếm số lượng database trùng tên trong bảng danh mục sys.databases
                // Sử dụng Parameter @DatabaseName để bảo mật và chống SQL Injection
                string sql = @"
                    SELECT COUNT(*)
                    FROM sys.databases
                    WHERE name = @DatabaseName";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@DatabaseName", config.Database);

                    // BƯỚC 4: Thực thi truy vấn với ExecuteScalarAsync lấy về con số đếm
                    int count = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return count > 0;
                }
            }
        }

        /// <summary>
        /// Khởi tạo cấu trúc cơ sở dữ liệu: Đọc file kịch bản Database/DatabaseSchema.sql và thực thi để tạo bảng, khóa chính, khóa ngoại.
        /// </summary>
        /// <param name="config">Cấu hình kết nối cơ sở dữ liệu.</param>
        public static async Task InitializeDatabaseAsync(DatabaseConfig config)
        {
            // BƯỚC 1: Xây dựng chuỗi kết nối trực tiếp vào database của ứng dụng
            string connectionString = BuildConnectionString(config);

            // BƯỚC 2: Đọc toàn bộ nội dung kịch bản DDL từ file DatabaseSchema.sql
            string schemaScript = LoadSchemaScript();

            // BƯỚC 3: Thực thi kịch bản tạo bảng trên CSDL
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(schemaScript, connection))
                {
                    // Thiết lập thời gian chờ 30 giây cho các kịch bản DDL tạo nhiều bảng
                    command.CommandTimeout = 30;

                    // ExecuteNonQueryAsync dùng để thực thi các lệnh tạo bảng/thay đổi cấu trúc không trả về bảng kết quả
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        /// <summary>
        /// Tạo cơ sở dữ liệu mới trên máy chủ SQL Server nếu CSDL này chưa từng tồn tại.
        /// </summary>
        /// <param name="config">Cấu hình cơ sở dữ liệu.</param>
        /// <exception cref="ArgumentException">Ném ra ngoại lệ nếu tên database bị để trống.</exception>
        public static async Task CreateDatabaseAsync(DatabaseConfig config)
        {
            if (string.IsNullOrWhiteSpace(config.Database))
            {
                throw new ArgumentException("Tên database không được để trống.");
            }

            // BƯỚC 1: Tạo kết nối vào database hệ thống 'master' để có quyền chạy lệnh DDL CREATE DATABASE
            string masterConnectionString = BuildMasterConnectionString(config);

            using (SqlConnection connection = new SqlConnection(masterConnectionString))
            {
                await connection.OpenAsync();

                // BƯỚC 2: Chuẩn hóa tên database tránh ký tự ngoặc vuông đặc biệt
                string databaseName = config.Database.Replace("]", "]]");

                // BƯỚC 3: Dùng hàm DB_ID để kiểm tra an toàn trước khi chạy lệnh CREATE DATABASE
                string sql = $@"
                    IF DB_ID(N'{databaseName.Replace("'", "''")}') IS NULL
                    BEGIN
                        CREATE DATABASE [{databaseName}]
                    END";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        /// <summary>
        /// Tạo chuỗi kết nối đặc biệt trỏ tới cơ sở dữ liệu hệ thống 'master' của SQL Server.
        /// Cho phép kiểm tra trạng thái máy chủ và thực hiện các quyền quản trị như tạo mới Database.
        /// </summary>
        /// <param name="config">Cấu hình cơ sở dữ liệu hiện tại.</param>
        /// <returns>Chuỗi kết nối trỏ tới database master.</returns>
        private static string BuildMasterConnectionString(DatabaseConfig config)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = config.Server;
            builder.InitialCatalog = "master"; // CSDL hệ thống quản trị của SQL Server
            builder.TrustServerCertificate = true;
            builder.ConnectTimeout = 5;

            if (config.WindowsAuthentication)
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.IntegratedSecurity = false;
                builder.UserID = config.Username;
                builder.Password = config.Password;
            }

            return builder.ConnectionString;
        }

        /// <summary>
        /// Kiểm tra nhanh khả năng kết nối tới cơ sở dữ liệu theo cấu hình.
        /// Nếu không kết nối được, OpenAsync sẽ ném ra ngoại lệ SqlException chi tiết.
        /// </summary>
        /// <param name="config">Cấu hình cơ sở dữ liệu cần thử nghiệm.</param>
        public static async Task TestConnectionAsync(DatabaseConfig config)
        {
            string connectionString = BuildConnectionString(config);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
            }
        }

        /// <summary>
        /// Khởi tạo một đối tượng kết nối SqlConnection mới dựa trên thông số cấu hình.
        /// Lưu ý: Đối tượng này ở trạng thái đóng (Closed), phương thức gọi cần gọi conn.Open() và bọc trong khối using.
        /// </summary>
        /// <param name="config">Cấu hình cơ sở dữ liệu.</param>
        /// <returns>Đối tượng SqlConnection sẵn sàng sử dụng.</returns>
        public static SqlConnection CreateConnection(DatabaseConfig config)
        {
            string connectionString = BuildConnectionString(config);
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Xác định đường dẫn tuyệt đối tới tệp kịch bản Database/DatabaseSchema.sql.
        /// </summary>
        /// <returns>Đường dẫn tệp tin trên ổ đĩa.</returns>
        private static string GetSchemaFilePath()
        {
            return Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Database",
                "DatabaseSchema.sql"
            );
        }

        /// <summary>
        /// Đọc toàn bộ nội dung văn bản trong tệp tin kịch bản DatabaseSchema.sql.
        /// </summary>
        /// <returns>Chuỗi chứa kịch bản lệnh SQL DDL.</returns>
        /// <exception cref="FileNotFoundException">Ném ra nếu không tìm thấy tệp tin kịch bản trên máy.</exception>
        private static string LoadSchemaScript()
        {
            string path = GetSchemaFilePath();

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(
                    "Không tìm thấy file DatabaseSchema.sql.",
                    path
                );
            }

            return File.ReadAllText(path, Encoding.UTF8);
        }
    }
}
