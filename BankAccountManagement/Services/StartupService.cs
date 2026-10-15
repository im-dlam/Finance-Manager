using BankAccountManagement.Forms;
using BankAccountManagement.Models;
using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Service bootstrap ứng dụng: đảm bảo database sẵn sàng trước khi mở LoginForm.
    /// 
    /// Chiến lược:
    /// 1. Đọc config cũ (nếu có). Nếu kết nối OK → dùng luôn.
    /// 2. Nếu config hỏng/chưa có → tự dò SQL Server trên máy.
    ///    Ưu tiên SQL Server thật (SQLEXPRESS, MSSQLSERVER) trước,
    ///    LocalDB chỉ dùng khi không có gì khác.
    /// 3. Tạo config mới: Windows Auth + Database = "BankM36".
    /// 4. Tạo DB + chạy schema + seed admin + lưu config.
    /// 5. Nếu auto-detect thất bại → mở SettingsForm cho admin nhập tay.
    /// </summary>
    public static class StartupService
    {
        /// <summary>Tên database mặc định – cố định cho đồ án.</summary>
        private const string DefaultDatabaseName = "BankM36";

        // ============================================================
        //  ENTRY POINT
        // ============================================================

        /// <summary>
        /// Đảm bảo database sẵn sàng. Trả về true nếu OK để mở LoginForm.
        /// </summary>
        public static bool EnsureDatabaseReady()
        {
            // ============================================================
            //  BƯỚC 1: Thử config hiện tại (nếu có)
            // ============================================================
            DatabaseConfig existingConfig = DatabaseConfigManager.Load();

            if (!string.IsNullOrWhiteSpace(existingConfig.Server)
                && !string.IsNullOrWhiteSpace(existingConfig.Database))
            {
                // ⚠ Sửa lỗi config cũ: (localdb) một mình là SAI cú pháp
                if (existingConfig.Server.Trim().Equals("(localdb)",
                        StringComparison.OrdinalIgnoreCase))
                {
                    existingConfig.Server = @"(localdb)\MSSQLLocalDB";
                }

                if (TryUseExistingConfig(existingConfig))
                    return true;

                // Config hỏng → tiếp tục auto-detect
            }

            // ============================================================
            //  BƯỚC 2: Auto-detect SQL Server
            // ============================================================
            string detectedServer = AutoDetectServer();

            if (string.IsNullOrEmpty(detectedServer))
            {
                MessageBox.Show(
                    "Không tìm thấy SQL Server đang chạy trên máy.\n\n" +
                    "Vui lòng mở phần cài đặt để nhập thông tin kết nối thủ công.",
                    "Không tìm thấy SQL Server",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return ShowSettingsAndWait();
            }

            // ============================================================
            //  BƯỚC 3: Tạo config mới với server vừa detect được
            // ============================================================
            var newConfig = new DatabaseConfig
            {
                Server = detectedServer,
                Database = DefaultDatabaseName,
                Username = string.Empty,
                Password = string.Empty,
                WindowsAuthentication = true
            };

            // ============================================================
            //  BƯỚC 4: Tạo DB + chạy schema + seed + lưu config
            // ============================================================
            return BootstrapDatabase(newConfig);
        }

        // ============================================================
        //  DANH SÁCH INSTANCE ƯU TIÊN
        // ============================================================

        /// <summary>
        /// Trả về danh sách các instance SQL Server cần thử, theo thứ tự ưu tiên.
        /// 
        /// Nguyên tắc:
        /// - Ưu tiên SQL Server thật (Express / Full) trước.
        /// - LocalDB chỉ để CUỐI vì là loại lightweight, không phải SQL Server chuẩn.
        /// - Thêm tên máy thật (Environment.MachineName) để cover trường hợp
        ///   SQL Server cấu hình với tên máy riêng.
        /// </summary>
        private static string[] GetCommonInstances()
        {
            string machineName = Environment.MachineName;

            return new[]
            {
                // ============================================================
                //  ƯU TIÊN 1: SQL Server thật – named instance phổ biến
                // ============================================================
                @".\SQLEXPRESS",
                $@"{machineName}\SQLEXPRESS",
                @".\MSSQLSERVER2019",
                @".\MSSQLSERVER2022",
                @".\MSSQLSERVER",
                $@"{machineName}\MSSQLSERVER",

                // ============================================================
                //  ƯU TIÊN 2: Localhost variants – trỏ tới default instance
                // ============================================================
                @"localhost\SQLEXPRESS",
                @"(local)\SQLEXPRESS",
                @"localhost",
                @"(local)",
                @".",
                machineName,

                // ============================================================
                //  ƯU TIÊN 3: LocalDB – chỉ dùng khi không có gì khác
                // ============================================================
                @"(localdb)\MSSQLLocalDB"
            };
        }

        // ============================================================
        //  XỬ LÝ CONFIG CŨ
        // ============================================================

        /// <summary>
        /// Kiểm tra config hiện tại có dùng được không.
        /// - Nếu kết nối OK và DB đã có → true luôn.
        /// - Nếu kết nối OK nhưng chưa có DB → tạo.
        /// - Nếu kết nối fail → false (để gọi auto-detect).
        /// </summary>
        private static bool TryUseExistingConfig(DatabaseConfig config)
        {
            try
            {
                DatabaseService.TestConnectionAsync(config)
                    .GetAwaiter().GetResult();

                bool dbExists = DatabaseService.DatabaseExistsAsync(config)
                    .GetAwaiter().GetResult();

                if (dbExists)
                    return true;

                // Chưa có DB → tạo
                return BootstrapDatabase(config);
            }
            catch
            {
                // Config hỏng → để caller chạy auto-detect
                return false;
            }
        }

        // ============================================================
        //  TẠO DATABASE + SCHEMA + LƯU CONFIG
        // ============================================================

        /// <summary>
        /// Tạo database, chạy file schema, seed admin và lưu config.
        /// Trả về true nếu thành công.
        /// </summary>
        private static bool BootstrapDatabase(DatabaseConfig config)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                bool dbExists = DatabaseService.DatabaseExistsAsync(config)
                    .GetAwaiter().GetResult();

                if (!dbExists)
                {
                    DatabaseService.CreateDatabaseAsync(config)
                        .GetAwaiter().GetResult();

                    DatabaseService.InitializeDatabaseAsync(config)
                        .GetAwaiter().GetResult();
                }

                DatabaseConfigManager.Save(config);

                Cursor.Current = Cursors.Default;

                return true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;

                DialogResult choice = MessageBox.Show(
                    $"Không thể khởi tạo database tự động.\n\n" +
                    $"Chi tiết: {ex.Message}\n\n" +
                    "Bạn có muốn mở phần cài đặt để nhập thông tin thủ công không?",
                    "Lỗi khởi tạo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error);

                if (choice == DialogResult.Yes)
                    return ShowSettingsAndWait();

                return false;
            }
        }

        // ============================================================
        //  AUTO-DETECT SQL SERVER
        // ============================================================

        /// <summary>
        /// Dò tìm SQL Server đang chạy. Thử 2 chiến lược:
        /// 1. List ưu tiên (nhanh, cover 95% trường hợp).
        /// 2. SqlDataSourceEnumerator – quét UDP cả LAN (dùng backup).
        /// </summary>
        /// <returns>Tên instance kết nối được, hoặc null nếu không tìm thấy.</returns>
        private static string AutoDetectServer()
        {
            string[] candidates = GetCommonInstances();

            // ---- Chiến lược 1: Thử list ưu tiên ----
            foreach (string instance in candidates)
            {
                if (CanConnect(instance))
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"[AutoDetect] Chọn SQL Server: {instance}");
                    return instance;
                }
            }

            // ---- Chiến lược 2: Quét qua SqlDataSourceEnumerator ----
            try
            {
                DataTable sources = SqlDataSourceEnumerator.Instance.GetDataSources();

                foreach (DataRow row in sources.Rows)
                {
                    string serverName = row["ServerName"]?.ToString();
                    string instanceName = row["InstanceName"]?.ToString();

                    if (string.IsNullOrWhiteSpace(serverName))
                        continue;

                    string fullName = string.IsNullOrWhiteSpace(instanceName)
                        ? serverName
                        : $"{serverName}\\{instanceName}";

                    // Bỏ qua nếu đã thử trong list cứng
                    if (Array.IndexOf(candidates, fullName) >= 0)
                        continue;

                    if (CanConnect(fullName))
                    {
                        System.Diagnostics.Debug.WriteLine(
                            $"[AutoDetect] Tìm được qua enumerator: {fullName}");
                        return fullName;
                    }
                }
            }
            catch
            {
                // SQL Browser service có thể không chạy → bỏ qua
            }

            return null;
        }

        /// <summary>
        /// Thử mở kết nối tới instance với timeout 2 giây.
        /// Windows Authentication, không cần username/password.
        /// </summary>
        private static bool CanConnect(string serverInstance)
        {
            try
            {
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = serverInstance,
                    InitialCatalog = "master",
                    IntegratedSecurity = true,      // Windows Authentication
                    ConnectTimeout = 2,
                    TrustServerCertificate = true
                };

                using (var conn = new SqlConnection(builder.ConnectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        // ============================================================
        //  FALLBACK: MỞ SETTINGS
        // ============================================================

        /// <summary>
        /// Mở SettingsForm dạng dialog và chờ admin đóng.
        /// Trả về true nếu file config tồn tại sau khi form đóng.
        /// </summary>
        private static bool ShowSettingsAndWait()
        {
            using (var settings = new SettingsForm())
            {
                settings.ShowDialog();
            }

            return DatabaseConfigManager.Exists();
        }
    }
}