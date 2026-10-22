using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Form thiết lập kết nối Cơ sở dữ liệu SQL Server (SettingsForm).
    /// Cung cấp các tính năng:
    /// - Đọc và lưu thông tin file cấu hình `Config/database.ini`.
    /// - Hỗ trợ 2 chế độ xác thực: Windows Authentication và SQL Server Authentication (User/Password).
    /// - Kiểm tra thử kết nối (Test Connection Async) trước khi lưu.
    /// - Khởi tạo tự động Database, chạy file schema tạo bảng và dữ liệu mẫu nếu chưa có.
    /// </summary>
    public partial class SettingsForm : Form
    {
        #region KHỞI TẠO & NẠP CẤU HÌNH CŨ

        /// <summary>
        /// Khởi tạo màn hình cài đặt cấu hình CSDL.
        /// </summary>
        public SettingsForm()
        {
            InitializeComponent();

            // Tải cấu hình đang lưu trong database.ini lên các trường nhập liệu
            LoadDatabaseConfig();

            // Cập nhật trạng thái hiển thị của ô Username/Password theo checkbox Windows Auth
            UpdateAuthenticationControls();
        }

        /// <summary>
        /// Đọc cấu hình database từ file database.ini và hiển thị lên giao diện.
        /// </summary>
        private void LoadDatabaseConfig()
        {
            try
            {
                DatabaseConfig config = DatabaseConfigManager.Load();

                txtServer.Text = config.Server;
                txtDatabase.Text = config.Database;
                txtUsername.Text = config.Username;
                txtPassword.Text = config.Password;
                checkboxWindowAuthen.Checked = config.WindowsAuthentication;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể đọc cấu hình cơ sở dữ liệu.\n\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #endregion

        #region CHUYỂN ĐỔI CHẾ ĐỘ XÁC THỰC (AUTHENTICATION MODE)

        /// <summary>
        /// Xử lý khi người dùng tích chọn hoặc bỏ chọn Windows Authentication.
        /// </summary>
        private void checkboxWindowAuthen_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAuthenticationControls();
        }

        /// <summary>
        /// Ẩn/Hiện (Enable/Disable) ô Username và Password tùy theo chế độ xác thực:
        /// - Windows Auth: Đăng nhập bằng tài khoản Windows hiện tại (không cần user/pass).
        /// - SQL Server Auth: Yêu cầu bắt buộc phải có user (sa) và mật khẩu.
        /// </summary>
        private void UpdateAuthenticationControls()
        {
            bool useWindowsAuthentication = checkboxWindowAuthen.Checked;
            txtUsername.Enabled = !useWindowsAuthentication;
            txtPassword.Enabled = !useWindowsAuthentication;
        }

        #endregion

        #region KIỂM TRA TÍNH HỢP LỆ DỮ LIỆU (VALIDATION)

        /// <summary>
        /// Lấy đối tượng DatabaseConfig từ các trường người dùng nhập trên giao diện.
        /// </summary>
        private DatabaseConfig GetDatabaseConfig()
        {
            return new DatabaseConfig
            {
                Server = txtServer.Text.Trim(),
                Database = txtDatabase.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text,
                WindowsAuthentication = checkboxWindowAuthen.Checked
            };
        }

        /// <summary>
        /// Kiểm tra các trường thông tin bắt buộc trước khi test kết nối hoặc lưu file.
        /// </summary>
        private bool ValidateConfig(DatabaseConfig config)
        {
            // 1. Kiểm tra Server name
            if (string.IsNullOrWhiteSpace(config.Server))
            {
                MessageBox.Show("Vui lòng nhập tên SQL Server (ví dụ: .\\SQLEXPRESS hoặc localhost).",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtServer.Focus();
                return false;
            }

            // 2. Kiểm tra Database name
            if (string.IsNullOrWhiteSpace(config.Database))
            {
                MessageBox.Show("Vui lòng nhập tên Cơ sở dữ liệu (ví dụ: BankM36).",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDatabase.Focus();
                return false;
            }

            // 3. Nếu dùng SQL Server Authentication: bắt buộc phải có Username và Password
            if (!config.WindowsAuthentication)
            {
                if (string.IsNullOrWhiteSpace(config.Username))
                {
                    MessageBox.Show("Vui lòng nhập Username khi dùng SQL Server Authentication.",
                        "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(config.Password))
                {
                    MessageBox.Show("Vui lòng nhập Password cho tài khoản SQL Server.",
                        "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region KIỂM TRA KẾT NỐI & LƯU CẤU HÌNH (CONNECTION & SAVE)

        /// <summary>
        /// Kiểm tra kết nối thử tới SQL Server bất đồng bộ (Async/Await) để tránh treo đơ giao diện.
        /// </summary>
        private async void btnCheckConnect_Click(object sender, EventArgs e)
        {
            DatabaseConfig config = GetDatabaseConfig();
            if (!ValidateConfig(config)) return;

            // Khóa nút trong lúc đang kết nối
            btnCheckConnect.Enabled = false;

            try
            {
                // Thử mở kết nối tới database
                await DatabaseService.TestConnectionAsync(config);

                MessageBox.Show(
                    "Kết nối cơ sở dữ liệu thành công!",
                    "Kết nối CSDL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    $"Không thể kết nối cơ sở dữ liệu.\n\nChi tiết: {ex.Message}",
                    "Kết nối thất bại",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Đã xảy ra lỗi ngoài ý muốn.\n\nChi tiết: {ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnCheckConnect.Enabled = true;
            }
        }

        /// <summary>
        /// Lưu cấu hình hiện tại vào tệp tin Config/database.ini.
        /// </summary>
        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            DatabaseConfig config = GetDatabaseConfig();
            if (!ValidateConfig(config)) return;

            try
            {
                DatabaseConfigManager.Save(config);
                string path = DatabaseConfigManager.GetConfigFilePath();

                MessageBox.Show(
                    $"Đã lưu cấu hình thành công.\n\nFile lưu tại:\n{path}",
                    "Lưu cấu hình",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể lưu cấu hình.\n\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #endregion

        #region KHỞI TẠO DATABASE TỰ ĐỘNG (CREATE DATABASE & SCHEMA)

        /// <summary>
        /// Tạo mới database, khởi chạy schema tạo bảng và seed dữ liệu khởi tạo.
        /// </summary>
        private async void btnCreateDatabase_Click(object sender, EventArgs e)
        {
            DatabaseConfig config = GetDatabaseConfig();
            if (!ValidateConfig(config)) return;

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn tạo mới database '{config.Database}' trên server '{config.Server}' không?",
                "Tạo cơ sở dữ liệu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                btnCreateDatabase.Enabled = false;
                Cursor = Cursors.WaitCursor;

                // BƯỚC 1: Kiểm tra xem database đã có sẵn chưa
                bool exists = await DatabaseService.DatabaseExistsAsync(config);
                if (exists)
                {
                    MessageBox.Show(
                        $"Database '{config.Database}' đã tồn tại trên SQL Server.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                // BƯỚC 2: Tạo database mới trên master
                await DatabaseService.CreateDatabaseAsync(config);

                // BƯỚC 3: Đọc file schema SQL và thực thi tạo toàn bộ bảng, khóa chính, khóa ngoại
                await DatabaseService.InitializeDatabaseAsync(config);

                // BƯỚC 4: Lưu lại file cấu hình
                DatabaseConfigManager.Save(config);

                MessageBox.Show(
                    $"Database '{config.Database}' và toàn bộ các bảng quan hệ đã được tạo thành công!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    $"Lỗi SQL Server khi tạo database:\n\n{ex.Message}",
                    "SQL Server Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể khởi tạo database:\n\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                btnCreateDatabase.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        #endregion
    }
}
