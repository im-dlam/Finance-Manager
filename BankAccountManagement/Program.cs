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
            using (_loginForm = new LoginForm(prefillUser))
            {
                if (_loginForm.ShowDialog() == DialogResult.OK && CurrentSession.User != null)
                {
                    ShowMainForm();
                }
                else
                {
                    ExitThread();
                }
            }
        }

        private void ShowMainForm()
        {
            _mainForm = new MainForm(CurrentSession.User);
            this.MainForm = _mainForm;
            _mainForm.FormClosed += (s, e) =>
            {
                if (_mainForm.DialogResult == DialogResult.Retry)
                {
                    CurrentSession.Clear();
                    ShowLoginForm();
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

            while (true)
            {
                using (var loginForm = new LoginForm())
                {
                    DialogResult result = loginForm.ShowDialog();
                    if (result != DialogResult.OK || CurrentSession.User == null)
                    {
                        break;
                    }
                }

                using (var mainForm = new MainForm(CurrentSession.User))
                {
                    Application.Run(mainForm);
                    if (mainForm.DialogResult != DialogResult.Retry)
                    {
                        break;
                    }
                    CurrentSession.Clear();
                }
            }
        }
    }
}
