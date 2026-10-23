using BankAccountManagement.Forms;
using BankAccountManagement.Services;
using System;
using System.Windows.Forms;

namespace BankAccountManagement
{
    /// <summary>
    /// Điểm khởi đầu (Entry Point) của toàn bộ ứng dụng Bank Account Management.
    /// Chịu trách nhiệm khởi tạo môi trường WinForms, kiểm tra kết nối cơ sở dữ liệu,
    /// và điều phối vòng đời giữa Form Đăng nhập (LoginForm) và Màn hình chính (MainForm).
    /// </summary>
    internal static class Program
    {
        // =========================================================================
        // KHAI BÁO THAM CHIẾU TĨNH (STATIC GC ROOT)
        // =========================================================================
        // Trong .NET WinForms, khi gọi ShowDialog() hoặc Run() với biến cục bộ trong hàm,
        // trình tối ưu JIT và Garbage Collector (GC) có thể xem đối tượng Form là "hết vòng đời",
        // dẫn đến việc thu gom sớm các con trỏ hàm native WndProc (gây lỗi CallbackOnCollectedDelegate).
        // Giữ 2 biến static ở cấp AppDomain này đảm bảo Form luôn tồn tại bền vững trong suốt thời gian hiển thị.
        private static LoginForm _activeLoginForm;
        private static MainForm _activeMainForm;

        /// <summary>
        /// Hàm Main - Điểm bắt đầu thực thi của chương trình.
        /// </summary>
        [STAThread] // Bắt buộc cho WinForms: Thiết lập mô hình phân luồng Single Thread Apartment cho UI
        static void Main()
        {
            // BƯỚC 1: Cấu hình giao diện Windows Forms hiện đại và chế độ bắt ngoại lệ UI
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.EnableVisualStyles(); // Bật phong cách giao diện trực quan của hệ điều hành Windows
            Application.SetCompatibleTextRenderingDefault(false); // Dùng GDI+ để kết xuất chữ sắc nét, nhất quán

            // BƯỚC 2: Kiểm tra và chuẩn bị cơ sở dữ liệu trước khi mở ứng dụng
            // Nếu không tìm thấy SQL Server hoặc khởi tạo thất bại, dừng ứng dụng an toàn
            if (!StartupService.EnsureDatabaseReady())
            {
                return;
            }

            // BƯỚC 3: Vòng lặp chính điều phối Đăng nhập <-> Sử dụng hệ thống <-> Đăng xuất
            // Cho phép người dùng Đăng xuất (Logout) từ MainForm để quay lại LoginForm mà không cần khởi động lại file .exe
            while (true)
            {
                // -------------------------------------------------------------
                // 3.1. KHỞI CHẠY MÀN HÌNH ĐĂNG NHẬP (MODAL DIALOG)
                // -------------------------------------------------------------
                DialogResult loginResult;
                using (var loginForm = new LoginForm())
                {
                    _activeLoginForm = loginForm;
                    try
                    {
                        loginResult = loginForm.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"Đã xảy ra lỗi trong quá trình hiển thị màn hình Đăng nhập:\n\n{ex.Message}\n\nNgăn xếp lỗi (Stack Trace):\n{ex.StackTrace}",
                            "Lỗi hệ thống",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                    }
                    finally
                    {
                        _activeLoginForm = null;
                    }

                    // Nếu người dùng nhấn nút Đóng (✕) hoặc đăng nhập thất bại thì thoát hẳn ứng dụng
                    if (loginResult != DialogResult.OK || CurrentSession.User == null)
                    {
                        break;
                    }
                }

                // -------------------------------------------------------------
                // 3.2. KHỞI CHẠY MÀN HÌNH CHÍNH (MAINFORM)
                // -------------------------------------------------------------
                using (var mainForm = new MainForm(CurrentSession.User))
                {
                    _activeMainForm = mainForm;
                    Application.Run(mainForm);
                    _activeMainForm = null;

                    // Kiểm tra kết quả đóng MainForm:
                    // - Nếu DialogResult == Retry: Người dùng chọn "Đăng xuất" -> Tiếp tục vòng lặp để mở lại LoginForm.
                    // - Nếu không phải Retry: Người dùng tắt ứng dụng -> Thoát khỏi vòng lặp và kết thúc chương trình.
                    if (mainForm.DialogResult != DialogResult.Retry)
                    {
                        break;
                    }

                    // Xóa thông tin phiên làm việc cũ khi đăng xuất để đảm bảo an toàn bảo mật
                    CurrentSession.Clear();
                }
            }
        }
    }
}
