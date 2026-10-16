using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Form điều hướng Vay vốn: Tự động phân quyền hiển thị:
    /// - Quản trị viên (Admin): Mở giao diện Thẩm định & Xét duyệt khoản vay (BorrowingAdminForm)
    /// - Khách hàng (User/Member): Mở giao diện Đăng ký vay nhanh trực tuyến (BorrowingUserForm)
    /// </summary>
    public partial class BorrowingForm : Form
    {
        public BorrowingForm()
        {
            InitializeComponent();
            this.Load += BorrowingForm_Load;
        }

        private void BorrowingForm_Load(object sender, EventArgs e)
        {
            Form targetForm = CurrentSession.IsAdmin
                ? (Form)new BorrowingAdminForm()
                : (Form)new BorrowingUserForm();

            targetForm.TopLevel = false;
            targetForm.FormBorderStyle = FormBorderStyle.None;
            targetForm.Dock = DockStyle.Fill;

            this.Controls.Clear();
            this.Controls.Add(targetForm);
            targetForm.Show();
        }
    }
}
