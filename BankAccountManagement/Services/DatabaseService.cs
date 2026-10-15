using BankAccountManagement.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;

using System;
using System.IO;
using System.Text;
namespace BankAccountManagement.Services
{
    public static class DatabaseService
    {
        /// <summary>
        /// Tạo connection string từ cấu hình cơ sở dữ liệu.
        /// </summary>
        public static string BuildConnectionString(
            DatabaseConfig config)
        {
            SqlConnectionStringBuilder builder =
                new SqlConnectionStringBuilder();

            builder.DataSource = config.Server;
            builder.InitialCatalog = config.Database;

            // Tránh lỗi chứng chỉ trong môi trường local.
            builder.TrustServerCertificate = true;

            // Không để ứng dụng chờ quá lâu khi Server không phản hồi.
            builder.ConnectTimeout = 5;

            if (config.WindowsAuthentication)
            {
                // Windows Authentication
                builder.IntegratedSecurity = true;
            }
            else
            {
                // SQL Server Authentication
                builder.IntegratedSecurity = false;
                builder.UserID = config.Username;
                builder.Password = config.Password;
            }

            return builder.ConnectionString;
        }
        /// <summary>
        /// Kiem tra xem cơ sở dữ liệu đã tồn tại hay chưa.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static async Task<bool> DatabaseExistsAsync(
    DatabaseConfig config)
        {
            string connectionString =
                BuildMasterConnectionString(config);

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string sql = @"
SELECT COUNT(*)
FROM sys.databases
WHERE name = @DatabaseName";

                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue(
                        "@DatabaseName",
                        config.Database
                    );

                    int count = Convert.ToInt32(
                        await command.ExecuteScalarAsync()
                    );

                    return count > 0;
                }
            }
        }

        /// <summary>
        /// Tạo cơ sở dữ liệu và thực hiện các lệnh SQL trong file DatabaseSchema.sql để tạo các bảng, chỉ mục, v.v.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static async Task InitializeDatabaseAsync(DatabaseConfig config)
        {
            string connectionString =
                BuildConnectionString(config);

            string schemaScript = LoadSchemaScript();

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command =
                    new SqlCommand(schemaScript, connection))
                {
                    command.CommandTimeout = 30;

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        /// <summary>
        /// Tạo cơ sở dữ liệu nếu chưa tồn tại.
        /// </summary>
        /// <param name="config">Cấu hình cơ sở dữ liệu.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static async Task CreateDatabaseAsync(DatabaseConfig config)
        {
            if (string.IsNullOrWhiteSpace(config.Database))
            {
                throw new ArgumentException("Tên database không được để trống.");
            }

            string masterConnectionString =
                BuildMasterConnectionString(config);

            using (SqlConnection connection =
                new SqlConnection(masterConnectionString))
            {
                await connection.OpenAsync();

                string databaseName = config.Database
                    .Replace("]", "]]");

                string sql = $@"
IF DB_ID(N'{databaseName.Replace("'", "''")}') IS NULL
BEGIN
    CREATE DATABASE [{databaseName}]
END";

                using (SqlCommand command =
                    new SqlCommand(sql, connection))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        /// <summary>
        /// Tạo connection string tới cơ sở dữ liệu master để thực hiện các thao tác quản lý cơ sở dữ liệu.
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private static string BuildMasterConnectionString(
    DatabaseConfig config)
        {
            SqlConnectionStringBuilder builder =
                new SqlConnectionStringBuilder();

            builder.DataSource = config.Server;
            builder.InitialCatalog = "master";
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
        /// Kiểm tra kết nối tới cơ sở dữ liệu.
        /// </summary>
        public static async Task TestConnectionAsync(
            DatabaseConfig config)
        {
            string connectionString =
                BuildConnectionString(config);

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
            }
        }

        /// <summary>
        /// Tạo một SqlConnection dựa trên cấu hình hiện tại.
        /// </summary>
        public static SqlConnection CreateConnection(
            DatabaseConfig config)
        {
            string connectionString =
                BuildConnectionString(config);

            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// Tìm path tới file DatabaseSchema.sql trong thư mục Database.
        /// </summary>
        /// <returns></returns>
        private static string GetSchemaFilePath()
        {
            return Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Database",
                "DatabaseSchema.sql"
            );
        }


        /// <summary>
        /// Đọc nội dung file DatabaseSchema.sql để tạo cơ sở dữ liệu.  
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
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