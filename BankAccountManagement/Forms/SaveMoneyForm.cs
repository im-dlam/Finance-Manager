using BankAccountManagement.Services;
using System;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Form điều phối trung gian cho tính năng Gửi Tiết Kiệm (Save Money).
    /// Tự động định tuyến hiển thị SaveMoneyAdminForm cho Quản trị viên
    /// hoặc SaveMoneyUserForm cho Khách hàng (Member).
    /// </summary>
    public partial class SaveMoneyForm : Form
    {
        private Form _activeSubForm;

        public SaveMoneyForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (CurrentSession.IsAdmin)
            {
                LoadSubForm(new SaveMoneyAdminForm());
            }
            else
            {
                LoadSubForm(new SaveMoneyUserForm());
            }
        }

        private void LoadSubForm(Form subForm)
        {
            if (_activeSubForm != null)
            {
                _activeSubForm.Close();
                _activeSubForm.Dispose();
            }

            _activeSubForm = subForm;
            subForm.TopLevel = false;
            subForm.FormBorderStyle = FormBorderStyle.None;
            subForm.Dock = DockStyle.Fill;
            this.Controls.Add(subForm);
            subForm.Show();
        }
    }
}
