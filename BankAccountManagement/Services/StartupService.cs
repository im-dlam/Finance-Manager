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
    /// Dịch vụ khởi động hệ thống (Bootstrap Startup Service):
    /// Đảm bảo môi trường máy chủ và cơ sở dữ liệu sẵn sàng hoạt động trước khi ứng dụng mở Form Đăng nhập (LoginForm).
    /// 
    /// Chiến lược kiểm tra và khởi động tự động:
    /// - Bước 1: Đọc tệp cấu hình database.ini đã lưu trước đó. Nếu kết nối thành công tới Database thì sử dụng ngay.
    /// - Bước 2: Nếu tệp cấu hình chưa có hoặc máy chủ cũ không phản hồi, tự động quét tìm SQL Server đang chạy trên máy (Auto-detect).
    ///   Ưu tiên các phiên bản SQL Server thật (.\SQLEXPRESS, MSSQLSERVER) trước, sau đó mới thử tới LocalDB.
    /// - Bước 3: Tạo cấu hình chuẩn với chế độ Windows Authentication và tên cơ sở dữ liệu mặc định "BankM36".
    /// - Bước 4: Tự động khởi tạo Database, thực thi kịch bản DatabaseSchema.sql tạo các bảng và tài khoản Quản trị viên (admin).
    /// - Bước 5: Nếu tất cả các nỗ lực tự động đều thất bại, hiển thị hộp thoại SettingsForm để người dùng nhập thông tin kết nối thủ công.
    /// </summary>
    public static class StartupService
    {
        /// <summary>
        /// Tên cơ sở dữ liệu mặc định được quy ước cố định cho toàn bộ đồ án.
        /// </summary>
        private const string DefaultDatabaseName = "BankM36";

        // ============================================================
        //  ĐIỂM VÀO CHÍNH CỦA QUÁ TRÌNH KHỞI ĐỘNG (BOOTSTRAP ENTRY POINT)
        // ============================================================

        /// <summary>
        /// Điểm vào chính kiểm tra và khởi tạo cơ sở dữ liệu tự động.
        /// </summary>
        /// <returns>True nếu cơ sở dữ liệu đã sẵn sàng để mở màn hình Đăng nhập; False nếu thất bại hoặc người dùng hủy.</returns>
        public static bool EnsureDatabaseReady()
        {
            // BƯỚC 1: Thử nghiệm thông số kết nối từ tệp cấu hình hiện tại (nếu có)
            DatabaseConfig existingConfig = DatabaseConfigManager.Load();

            if (!string.IsNullOrWhiteSpace(existingConfig.Server)
                && !string.IsNullOrWhiteSpace(existingConfig.Database))
            {
                // Sửa lỗi định dạng cấu hình cũ: "(localdb)" đơn lẻ là sai cú pháp kết nối của SQL Server
                if (existingConfig.Server.Trim().Equals("(localdb)", StringComparison.OrdinalIgnoreCase))
                {
                    existingConfig.Server = @"(localdb)\MSSQLLocalDB";
                }

                // Nếu cấu hình cũ dùng tốt thì tiếp tục vào ứng dụng
                if (TryUseExistingConfig(existingConfig))
                    return true;

                // Nếu cấu hình cũ không thể kết nối được thì tiếp tục bước tự động dò tìm máy chủ
            }

            // BƯỚC 2: Tự động quét và tìm kiếm phiên bản SQL Server đang hoạt động trên máy tính
            string detectedServer = AutoDetectServer();

            if (string.IsNullOrEmpty(detectedServer))
            {
                MessageBox.Show(
                    "Không tìm thấy SQL Server đang chạy trên máy tính của bạn.\n\n" +
                    "Vui lòng mở phần cài đặt để nhập thông tin kết nối thủ công.",
                    "Không tìm thấy SQL Server",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return ShowSettingsAndWait();
            }

            // BƯỚC 3: Tạo đối tượng cấu hình mới với Server vừa tìm được
            var newConfig = new DatabaseConfig
            {
                Server = detectedServer,
                Database = DefaultDatabaseName,
                Username = string.Empty,
                Password = string.Empty,
                WindowsAuthentication = true
            };

            // BƯỚC 4: Tạo cơ sở dữ liệu, chạy kịch bản schema và lưu cấu hình vào database.ini
            return BootstrapDatabase(newConfig);
        }

        // ============================================================
        //  DANH SÁCH INSTANCE MÁY CHỦ ƯU TIÊN (SERVER DISCOVERY LIST)
        // ============================================================

        /// <summary>
        /// Tạo danh sách các tên thể hiện (Instance) SQL Server phổ biến theo thứ tự ưu tiên giảm dần.
        /// - Ưu tiên 1: SQL Server thật (Express hoặc Bản Full).
        /// - Ưu tiên 2: Địa chỉ mạng máy tính cục bộ (localhost, tên máy tính).
        /// - Ưu tiên 3: Phiên bản nhẹ LocalDB (chỉ dùng dự phòng khi không cài SQL Server chuẩn).
        /// </summary>
        /// <returns>Mảng danh sách các chuỗi tên Server Instance.</returns>
        private static string[] GetCommonInstances()
        {
            string machineName = Environment.MachineName;

            return new[]
            {
                // ƯU TIÊN 1: SQL Server thật – Named Instance phổ biến nhất trên môi trường học tập và phát triển
                @".\SQLEXPRESS",
                $@"{machineName}\SQLEXPRESS",
                @".\MSSQLSERVER2019",
                @".\MSSQLSERVER2022",
                @".\MSSQLSERVER",
                $@"{machineName}\MSSQLSERVER",

                // ƯU TIÊN 2: Localhost variants – trỏ tới Default Instance của SQL Server
                @"localhost\SQLEXPRESS",
                @"(local)\SQLEXPRESS",
                @"localhost",
                @"(local)",
                @".",
                machineName,

                // ƯU TIÊN 3: LocalDB – phiên bản nhẹ đính kèm của Visual Studio
                @"(localdb)\MSSQLLocalDB"
            };
        }

        // ============================================================
        //  KIỂM TRA CẤU HÌNH CŨ (VALIDATE EXISTING CONFIG)
        // ============================================================

        /// <summary>
        /// Thử kiểm tra xem cấu hình cơ sở dữ liệu hiện tại có hoạt động được hay không.
        /// - Nếu kết nối được và CSDL đã có sẵn: Trả về True ngay.
        /// - Nếu kết nối được máy chủ nhưng chưa có CSDL: Tự động khởi tạo và chạy kịch bản bảng.
        /// - Nếu kết nối thất bại: Trả về False để chuyển sang bước quét máy chủ tự động.
        /// </summary>
        /// <param name="config">Cấu hình CSDL cần kiểm tra.</param>
        /// <returns>True nếu CSDL hoạt động bình thường; False nếu cấu hình lỗi.</returns>
        private static bool TryUseExistingConfig(DatabaseConfig config)
        {
            try
            {
                // BƯỚC 1: Thử nghiệm kết nối tới máy chủ
                DatabaseService.TestConnectionAsync(config).GetAwaiter().GetResult();

                // BƯỚC 2: Kiểm tra xem CSDL đã tồn tại chưa
                bool dbExists = DatabaseService.DatabaseExistsAsync(config).GetAwaiter().GetResult();

                if (dbExists)
                    return true;

                // BƯỚC 3: Nếu kết nối được nhưng CSDL chưa có, tiến hành tạo mới
                return BootstrapDatabase(config);
            }
            catch
            {
                // Cấu hình không thể kết nối được -> trả về false để hệ thống tự động dò tìm
                return false;
            }
        }

        // ============================================================
        //  KHỞI TẠO CƠ SỞ DỮ LIỆU TỰ ĐỘNG (DATABASE BOOTSTRAPPER)
        // ============================================================

        /// <summary>
        /// Tự động tạo mới cơ sở dữ liệu, chạy tệp kịch bản Schema, nạp dữ liệu ban đầu và lưu lại cấu hình.
        /// </summary>
        /// <param name="config">Cấu hình kết nối cơ sở dữ liệu hợp lệ.</param>
        /// <returns>True nếu khởi tạo thành công trọn vẹn; False nếu phát sinh lỗi.</returns>
        private static bool BootstrapDatabase(DatabaseConfig config)
        {
            try
            {
                // Hiển thị con trỏ chuột dạng chờ (Wait Cursor) trong khi khởi tạo CSDL
                Cursor.Current = Cursors.WaitCursor;

                // BƯỚC 1: Kiểm tra lại sự tồn tại của Database
                bool dbExists = DatabaseService.DatabaseExistsAsync(config).GetAwaiter().GetResult();

                // BƯỚC 2: Nếu chưa có CSDL thì thực thi lệnh tạo CSDL và chạy kịch bản tạo bảng
                if (!dbExists)
                {
                    DatabaseService.CreateDatabaseAsync(config).GetAwaiter().GetResult();
                    DatabaseService.InitializeDatabaseAsync(config).GetAwaiter().GetResult();
                }

                // BƯỚC 3: Lưu lại cấu hình kết nối chuẩn vào tệp database.ini
                DatabaseConfigManager.Save(config);

                // Khôi phục lại con trỏ chuột bình thường
                Cursor.Current = Cursors.Default;
                return true;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;

                // BƯỚC 4: Hiển thị thông báo lỗi chi tiết và hỏi người dùng có muốn cấu hình thủ công không
                DialogResult choice = MessageBox.Show(
                    $"Không thể khởi tạo cơ sở dữ liệu tự động.\n\n" +
                    $"Chi tiết lỗi: {ex.Message}\n\n" +
                    "Bạn có muốn mở phần Cài đặt để nhập thông tin kết nối thủ công không?",
                    "Lỗi khởi tạo cơ sở dữ liệu",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error);

                if (choice == DialogResult.Yes)
                    return ShowSettingsAndWait();

                return false;
            }
        }

        // ============================================================
        //  TỰ ĐỘNG DÒ TÌM SQL SERVER (AUTO-DETECT ENGINE)
        // ============================================================

        /// <summary>
        /// Dò tìm phiên bản SQL Server đang hoạt động trên máy tính.
        /// Áp dụng 2 chiến lược:
        /// 1. Kiểm tra danh sách các Instance phổ biến nhất (Nhanh chóng, chính xác đến 95%).
        /// 2. Quét thông qua SqlDataSourceEnumerator (Dự phòng quét qua giao thức UDP).
        /// </summary>
        /// <returns>Tên chuỗi máy chủ kết nối được, hoặc null nếu không tìm thấy.</returns>
        private static string AutoDetectServer()
        {
            string[] candidates = GetCommonInstances();

            // ---- CHIẾN LƯỢC 1: Thử lần lượt trong danh sách các Instance phổ biến nhất ----
            foreach (string instance in candidates)
            {
                if (CanConnect(instance))
                {
                    System.Diagnostics.Debug.WriteLine($"[AutoDetect] Đã tìm thấy SQL Server: {instance}");
                    return instance;
                }
            }

            // ---- CHIẾN LƯỢC 2: Quét toàn mạng thông qua SqlDataSourceEnumerator ----
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

                    // Bỏ qua nếu đã thử qua trong danh sách candidates ở Chiến lược 1
                    if (Array.IndexOf(candidates, fullName) >= 0)
                        continue;

                    if (CanConnect(fullName))
                    {
                        System.Diagnostics.Debug.WriteLine($"[AutoDetect] Tìm thấy máy chủ qua Enumerator: {fullName}");
                        return fullName;
                    }
                }
            }
            catch
            {
                // Bỏ qua lỗi trong trường hợp dịch vụ SQL Browser trên Windows không được bật
            }

            return null;
        }

        /// <summary>
        /// Thử mở kết nối tới một máy chủ SQL Server với thời gian chờ giới hạn 2 giây.
        /// Dùng chế độ Windows Authentication (Integrated Security).
        /// </summary>
        /// <param name="serverInstance">Tên máy chủ hoặc Instance cần thử kết nối.</param>
        /// <returns>True nếu kết nối thành công; False nếu không thể kết nối.</returns>
        private static bool CanConnect(string serverInstance)
        {
            try
            {
                // BƯỚC 1: Tạo cấu hình kết nối thử nghiệm nhanh vào database hệ thống master
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = serverInstance,
                    InitialCatalog = "master",
                    IntegratedSecurity = true, // Windows Authentication
                    ConnectTimeout = 2,        // Thời gian chờ ngắn 2s để quét nhanh
                    TrustServerCertificate = true
                };

                // BƯỚC 2: Thử mở kết nối
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
        //  CƠ CHẾ DỰ PHÒNG: MỞ HỘP THOẠI CÀI ĐẶT (FALLBACK SETTINGS DIALOG)
        // ============================================================

        /// <summary>
        /// Mở hộp thoại SettingsForm dạng Modal Dialog để người dùng nhập thông tin kết nối thủ công.
        /// Áp dụng cơ chế an toàn bộ nhớ GC.KeepAlive theo chuẩn kiến trúc đồ án.
        /// </summary>
        /// <returns>True nếu tệp cấu hình database.ini đã được lưu thành công sau khi đóng form; ngược lại là False.</returns>
        private static bool ShowSettingsAndWait()
        {
            using (var settings = new SettingsForm())
            {
                settings.ShowDialog();
                // Giữ nguyên cơ chế an toàn bộ nhớ không để GC thu dọn trước khi form đóng
                GC.KeepAlive(settings);
            }

            return DatabaseConfigManager.Exists();
        }
    }
}
