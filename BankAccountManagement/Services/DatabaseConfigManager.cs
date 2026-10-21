using BankAccountManagement.Models;
using System;
using System.IO;
using System.Text;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Lớp quản lý cấu hình kết nối cơ sở dữ liệu (Database Configuration Manager).
    /// Chịu trách nhiệm đọc và lưu trữ thông tin kết nối máy chủ SQL Server từ/vào tệp tin cấu hình "Config/database.ini".
    /// Giúp phần mềm có thể linh hoạt chuyển đổi môi trường máy chủ mà không cần biên dịch lại mã nguồn.
    /// </summary>
    public static class DatabaseConfigManager
    {
        /// <summary>
        /// Đường dẫn tuyệt đối tới thư mục chứa tệp cấu hình Config nằm cùng cấp với file thực thi .exe.
        /// </summary>
        private static readonly string ConfigDirectory =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");

        /// <summary>
        /// Đường dẫn tuyệt đối tới tệp tin cấu hình cơ sở dữ liệu database.ini.
        /// </summary>
        private static readonly string ConfigFilePath =
            Path.Combine(ConfigDirectory, "database.ini");

        /// <summary>
        /// Đọc và nạp thông số cấu hình cơ sở dữ liệu từ tệp tin database.ini vào đối tượng <see cref="DatabaseConfig"/>.
        /// Tự động tìm kiếm dự phòng nếu file chưa tồn tại trong thư mục chạy chương trình.
        /// </summary>
        /// <returns>Đối tượng DatabaseConfig chứa thông số kết nối đầy đủ.</returns>
        public static DatabaseConfig Load()
        {
            // BƯỚC 1: Khởi tạo đối tượng cấu hình với các giá trị mặc định an toàn
            DatabaseConfig config = new DatabaseConfig
            {
                Server = string.Empty,
                Database = string.Empty,
                Username = string.Empty,
                Password = string.Empty,
                WindowsAuthentication = true
            };

            // BƯỚC 2: Kiểm tra vị trí file cấu hình; nếu chưa thấy, tìm kiếm quanh thư mục Assembly
            string resolvedPath = ConfigFilePath;
            if (!File.Exists(resolvedPath))
            {
                try
                {
                    string asmDir = Path.GetDirectoryName(typeof(DatabaseConfigManager).Assembly.Location);
                    if (!string.IsNullOrEmpty(asmDir))
                    {
                        string asmConfig = Path.Combine(asmDir, "Config", "database.ini");
                        if (File.Exists(asmConfig))
                            resolvedPath = asmConfig;
                    }
                }
                catch
                {
                    // Bỏ qua lỗi tìm kiếm assembly để tiếp tục cơ chế dự phòng
                }
            }

            // BƯỚC 3: Dự phòng tìm file trong thư mục nguồn của đồ án (phục vụ lúc chạy debug trong Visual Studio)
            if (!File.Exists(resolvedPath))
            {
                try
                {
                    string sourceConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Config\database.ini");
                    if (File.Exists(sourceConfigPath))
                    {
                        Directory.CreateDirectory(ConfigDirectory);
                        File.Copy(sourceConfigPath, ConfigFilePath, true);
                        resolvedPath = ConfigFilePath;
                    }
                    else
                    {
                        return config;
                    }
                }
                catch
                {
                    return config;
                }
            }

            // BƯỚC 4: Đọc toàn bộ các dòng văn bản trong tệp database.ini theo chuẩn mã hóa UTF-8
            string[] lines = File.ReadAllLines(resolvedPath, Encoding.UTF8);

            // BƯỚC 5: Lặp qua từng dòng để bóc tách cấu trúc Key = Value
            foreach (string rawLine in lines)
            {
                string line = rawLine.Trim();

                // Bỏ qua các dòng trống không có nội dung
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Bỏ qua các dòng chú thích bắt đầu bằng dấu chấm phẩy ';' hoặc dấu thăng '#'
                if (line.StartsWith(";") || line.StartsWith("#"))
                    continue;

                // Bỏ qua phần tiêu đề phân đoạn Section dạng [Database]
                if (line.StartsWith("[") && line.EndsWith("]"))
                    continue;

                // Tách chuỗi theo ký tự bằng '=' thành tối đa 2 phần: [0] là Khóa (Key), [1] là Giá trị (Value)
                string[] parts = line.Split(new[] { '=' }, 2);
                if (parts.Length != 2)
                    continue;

                string key = parts[0].Trim();
                string value = parts[1].Trim();

                // BƯỚC 6: Ánh xạ giá trị đọc được vào các thuộc tính tương ứng của DatabaseConfig
                switch (key.ToLower())
                {
                    case "server":
                        config.Server = value;
                        break;

                    case "database":
                        config.Database = value;
                        break;

                    case "username":
                        config.Username = value;
                        break;

                    case "password":
                        config.Password = value;
                        break;

                    case "windowsauthentication":
                        if (bool.TryParse(value, out bool windowsAuth))
                        {
                            config.WindowsAuthentication = windowsAuth;
                        }
                        break;
                }
            }

            return config;
        }

        /// <summary>
        /// Ghi và lưu lại các thông số cấu hình cơ sở dữ liệu vào tệp tin database.ini.
        /// </summary>
        /// <param name="config">Đối tượng DatabaseConfig chứa thông số kết nối mới cần lưu.</param>
        public static void Save(DatabaseConfig config)
        {
            // BƯỚC 1: Đảm bảo thư mục Config luôn tồn tại trên ổ đĩa
            Directory.CreateDirectory(ConfigDirectory);

            // BƯỚC 2: Xây dựng nội dung định dạng tệp INI chuẩn với StringBuilder
            StringBuilder content = new StringBuilder();
            content.AppendLine("[Database]");
            content.AppendLine($"Server={config.Server}");
            content.AppendLine($"Database={config.Database}");
            content.AppendLine($"Username={config.Username}");
            content.AppendLine($"Password={config.Password}");
            content.AppendLine($"WindowsAuthentication={config.WindowsAuthentication}");

            // BƯỚC 3: Ghi toàn bộ nội dung ra đĩa với bảng mã chuẩn UTF-8
            File.WriteAllText(ConfigFilePath, content.ToString(), Encoding.UTF8);
        }

        /// <summary>
        /// Kiểm tra xem tệp tin cấu hình database.ini đã tồn tại trên đĩa hay chưa.
        /// </summary>
        /// <returns>True nếu file đã tồn tại; ngược lại là False.</returns>
        public static bool Exists()
        {
            return File.Exists(ConfigFilePath);
        }

        /// <summary>
        /// Lấy đường dẫn tuyệt đối đầy đủ tới tệp tin cấu hình database.ini.
        /// </summary>
        /// <returns>Đường dẫn tệp tin dạng chuỗi.</returns>
        public static string GetConfigFilePath()
        {
            return ConfigFilePath;
        }
    }
}
