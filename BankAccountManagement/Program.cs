using BankAccountManagement.Forms;
using BankAccountManagement.Services;
using System;
using System.Windows.Forms;

namespace BankAccountManagement
{
    public class BankAppContext : ApplicationContext
    {
        private LoginForm _loginForm;
        private MainForm _mainForm;

        public BankAppContext()
        {
            ShowLoginForm();
        }

        private void ShowLoginForm(string prefillUser = null)
        {
            _loginForm = new LoginForm(prefillUser);
            _loginForm.FormClosed += (s, e) =>
            {
                if (_loginForm.DialogResult == DialogResult.OK && CurrentSession.User != null)
                {
                    // Chờ LoginForm giải phóng hoàn toàn và thoát khỏi stack trước khi khởi tạo MainForm
                    if (System.Threading.SynchronizationContext.Current != null)
                    {
                        System.Threading.SynchronizationContext.Current.Post(_ => ShowMainForm(), null);
                    }
                    else
                    {
                        ShowMainForm();
                    }
                }
                else
                {
                    ExitThread();
                }
            };
            _loginForm.Show();
        }

        private void ShowMainForm()
        {
            _mainForm = new MainForm(CurrentSession.User);
            _mainForm.FormClosed += (s, e) =>
            {
                if (_mainForm.DialogResult == DialogResult.Retry)
                {
                    CurrentSession.Clear();
                    if (System.Threading.SynchronizationContext.Current != null)
                    {
                        System.Threading.SynchronizationContext.Current.Post(_ => ShowLoginForm(), null);
                    }
                    else
                    {
                        ShowLoginForm();
                    }
                }
                else
                {
                    ExitThread();
                }
            };
            _mainForm.Show();
        }
    }

    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!StartupService.EnsureDatabaseReady())
            {
                return;
            }

            Application.Run(new BankAppContext());
        }
    }
}
