using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            LoadDatabaseConfig();
            UpdateAuthenticationControls();
        }

        /// <summary>
        /// Xử lý khi thay đổi trạng thái Windows Authentication.
        /// </summary>
        private void checkboxWindowAuthen_CheckedChanged(
            object sender,
            EventArgs e)
        {
            UpdateAuthenticationControls();
        }

        /// <summary>
        /// Cập nhật trạng thái Username và Password
        /// dựa trên phương thức xác thực.
        /// </summary>
        private void UpdateAuthenticationControls()
        {
            bool useWindowsAuthentication =
                checkboxWindowAuthen.Checked;

            txtUsername.Enabled =
                !useWindowsAuthentication;

            txtPassword.Enabled =
                !useWindowsAuthentication;
        }

        /// <summary>
        /// Đọc cấu hình database từ database.ini
        /// và hiển thị lên giao diện.
        /// </summary>
        private void LoadDatabaseConfig()
        {
            try
            {
                DatabaseConfig config =
                    DatabaseConfigManager.Load();

                txtServer.Text = config.Server;
                txtDatabase.Text = config.Database;
                txtUsername.Text = config.Username;
                txtPassword.Text = config.Password;

                checkboxWindowAuthen.Checked =
                    config.WindowsAuthentication;
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

        /// <summary>
        /// Lấy cấu hình database từ các trường nhập liệu
        /// trên SettingsForm.
        /// </summary>
        private DatabaseConfig GetDatabaseConfig()
        {
            return new DatabaseConfig
            {
                Server = txtServer.Text.Trim(),

                Database = txtDatabase.Text.Trim(),

                Username = txtUsername.Text.Trim(),

                Password = txtPassword.Text,

                WindowsAuthentication =
                    checkboxWindowAuthen.Checked
            };
        }

        /// <summary>
        /// Kiểm tra thông tin cấu hình trước khi
        /// test connection hoặc lưu cấu hình.
        /// </summary>
        private bool ValidateConfig(
            DatabaseConfig config)
        {
            // Server
            if (string.IsNullOrWhiteSpace(config.Server))
            {
                MessageBox.Show(
                    "Vui lòng nhập Server.",
                    "Thiếu thông tin",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtServer.Focus();

                return false;
            }

            // Database
            if (string.IsNullOrWhiteSpace(config.Database))
            {
                MessageBox.Show(
                    "Vui lòng nhập Database.",
                    "Thiếu thông tin",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtDatabase.Focus();

                return false;
            }

            // SQL Server Authentication
            if (!config.WindowsAuthentication)
            {
                // Username
                if (string.IsNullOrWhiteSpace(config.Username))
                {
                    MessageBox.Show(
                        "Vui lòng nhập Username.",
                        "Thiếu thông tin",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtUsername.Focus();

                    return false;
                }

                // Password
                if (string.IsNullOrWhiteSpace(config.Password))
                {
                    MessageBox.Show(
                        "Vui lòng nhập Password.",
                        "Thiếu thông tin",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtPassword.Focus();

                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Kiểm tra kết nối cơ sở dữ liệu.
        /// </summary>
        private async void btnCheckConnect_Click(
            object sender,
            EventArgs e)
        {
            DatabaseConfig config =
                GetDatabaseConfig();

            if (!ValidateConfig(config))
            {
                return;
            }

            // Disable button trong lúc đang test.
            btnCheckConnect.Enabled = false;

            try
            {
                await DatabaseService.TestConnectionAsync(
                    config);

                MessageBox.Show(
                    "Kết nối cơ sở dữ liệu thành công.",
                    "Kết nối CSDL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    $"Không thể kết nối cơ sở dữ liệu.\n\n{ex.Message}",
                    "Kết nối thất bại",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Đã xảy ra lỗi.\n\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // Luôn enable lại button,
                // kể cả khi connection bị lỗi.
                btnCheckConnect.Enabled = true;
            }
        }


        /// <summary>
        /// Lưu cấu hình cơ sở dữ liệu vào database.ini.
        /// </summary>
        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            DatabaseConfig config = GetDatabaseConfig();

            if (!ValidateConfig(config))
            {
                return;
            }

            try
            {
                DatabaseConfigManager.Save(config);

                string path =
                    DatabaseConfigManager.GetConfigFilePath();

                MessageBox.Show(
                    $"Đã lưu cấu hình thành công.\n\nFile:\n{path}",
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

        private async void btnCreateDatabase_Click(object sender, EventArgs e)
        {
            DatabaseConfig config = GetDatabaseConfig();

            if (!ValidateConfig(config))
            {
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có muốn tạo database '{config.Database}' không?",
                "Create Database",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                btnCreateDatabase.Enabled = false;
                Cursor = Cursors.WaitCursor;

                // Kiểm tra database đã tồn tại chưa
                bool exists =
                    await DatabaseService.DatabaseExistsAsync(config);

                if (exists)
                {
                    MessageBox.Show(
                        $"Database '{config.Database}' đã tồn tại.",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    return;
                }

                // Tạo database
                await DatabaseService.CreateDatabaseAsync(config);

                // Tạo bảng và index
                await DatabaseService.InitializeDatabaseAsync(config);

                // Lưu cấu hình
                DatabaseConfigManager.Save(config);

                MessageBox.Show(
                    $"Database '{config.Database}' và các bảng đã được tạo thành công.",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    $"Không thể tạo database.\n\n{ex.Message}",
                    "SQL Server Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể khởi tạo database.\n\n{ex.Message}",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            finally
            {
                btnCreateDatabase.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

    }
}