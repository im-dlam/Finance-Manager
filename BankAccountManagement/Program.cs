using BankAccountManagement.Forms;
using BankAccountManagement.Services;
using System;
using System.Windows.Forms;

namespace BankAccountManagement
{
    internal static class Program
    {
        // Giữ tham chiếu static mạnh cấp AppDomain để ngăn ngừa JIT/GC thu gom Form
        // và các con trỏ window procedure delegate (NativeMethods.WndProc) khi modal dialog đang chạy
        private static LoginForm _activeLoginForm;
        private static MainForm _activeMainForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!StartupService.EnsureDatabaseReady())
            {
                return;
            }

            while (true)
            {
                DialogResult loginResult;
                _activeLoginForm = new LoginForm();
                try
                {
                    loginResult = _activeLoginForm.ShowDialog();
                    GC.KeepAlive(_activeLoginForm);

                    if (loginResult != DialogResult.OK || CurrentSession.User == null)
                    {
                        break;
                    }
                }
                finally
                {
                    var formToDispose = _activeLoginForm;
                    _activeLoginForm = null;
                    formToDispose?.Dispose();
                }

                // Xả sạch các thông điệp Win32 còn tồn đọng trong message queue trước khi chạy MainForm
                Application.DoEvents();

                _activeMainForm = new MainForm(CurrentSession.User);
                try
                {
                    Application.Run(_activeMainForm);
                    GC.KeepAlive(_activeMainForm);

                    if (_activeMainForm.DialogResult != DialogResult.Retry)
                    {
                        break;
                    }
                    CurrentSession.Clear();
                }
                finally
                {
                    var formToDispose = _activeMainForm;
                    _activeMainForm = null;
                    formToDispose?.Dispose();
                }

                // Xả sạch các thông điệp Win32 còn tồn đọng trước khi mở lại LoginForm (khi đăng xuất)
                Application.DoEvents();
            }
        }
    }
}
