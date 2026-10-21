using BankAccountManagement.Models;
using System;
using System.IO;
using System.Text;

namespace BankAccountManagement.Services
{
    public static class DatabaseConfigManager
    {
        private static readonly string ConfigDirectory =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Config");

        private static readonly string ConfigFilePath =
            Path.Combine(
                ConfigDirectory,
                "database.ini");

        /// <summary>
        /// Đọc cấu hình cơ sở dữ liệu từ database.ini.
        /// </summary>
        /// <returns>DatabaseConfig</returns>
        public static DatabaseConfig Load()
        {
            DatabaseConfig config = new DatabaseConfig
            {
                Server = string.Empty,
                Database = string.Empty,
                Username = string.Empty,
                Password = string.Empty,
                WindowsAuthentication = true
            };

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
                catch { }
            }

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

            string[] lines = File.ReadAllLines(
                resolvedPath,
                Encoding.UTF8);

            foreach (string rawLine in lines)
            {
                string line = rawLine.Trim();

                // Bỏ qua dòng trống
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                // Bỏ qua comment
                if (line.StartsWith(";") || line.StartsWith("#"))
                {
                    continue;
                }

                // Bỏ qua section [Database]
                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    continue;
                }

                string[] parts = line.Split(
                    new[] { '=' },
                    2);

                if (parts.Length != 2)
                {
                    continue;
                }

                string key = parts[0].Trim();
                string value = parts[1].Trim();

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
                        if (bool.TryParse(
                            value,
                            out bool windowsAuthentication))
                        {
                            config.WindowsAuthentication =
                                windowsAuthentication;
                        }

                        break;
                }
            }

            return config;
        }

        /// <summary>
        /// Lưu cấu hình cơ sở dữ liệu vào database.ini.
        /// </summary>
        /// <param name="config">Cấu hình cơ sở dữ liệu.</param>
        public static void Save(DatabaseConfig config)
        {
            Directory.CreateDirectory(ConfigDirectory);

            StringBuilder content = new StringBuilder();

            content.AppendLine("[Database]");
            content.AppendLine($"Server={config.Server}");
            content.AppendLine($"Database={config.Database}");
            content.AppendLine($"Username={config.Username}");
            content.AppendLine($"Password={config.Password}");
            content.AppendLine(
                $"WindowsAuthentication={config.WindowsAuthentication}");

            File.WriteAllText(
                ConfigFilePath,
                content.ToString(),
                Encoding.UTF8);
        }

        /// <summary>
        /// Kiểm tra file cấu hình đã tồn tại hay chưa.
        /// </summary>
        public static bool Exists()
        {
            return File.Exists(ConfigFilePath);
        }

        /// <summary>
        /// Lấy đường dẫn tới file database.ini.
        /// </summary>
        public static string GetConfigFilePath()
        {
            return ConfigFilePath;
        }
    }

}