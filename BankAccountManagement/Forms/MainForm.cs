using BankAccountManagement.Forms;
using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountManagement
{
    public partial class MainForm : Form
    {

        /// <summary>
        /// Khởi tạo MainForm với user đã đăng nhập.
        /// </summary>
        /// <param name="user">User hiện tại (bắt buộc, không được null).</param>
        public MainForm(User user)
        {
            InitializeComponent();
            LoadMultiForm(new DashboardForm());
            ApplyRolePermissions();
        }

        /// <summary>
        /// Hiển thị Form con bên trong pnMain.
        /// Nếu pnMain đang chứa Form khác, Form cũ sẽ được xóa trước.
        /// </summary>
        /// <param name="form">
        /// Form cần hiển thị bên trong pnMain.
        /// </param>
        public void LoadMultiForm(Form form)
        {
            // Dispose form cũ để giải phóng window handle + delegate
            // fix lỗi CallbackOnCollectedDelegate khi form cũ bị GC.
            if (pnMain.Controls.Count > 0)
            {
                var oldForms = new System.Collections.Generic.List<Control>();
                foreach (Control c in pnMain.Controls)
                    oldForms.Add(c);

                pnMain.Controls.Clear();

                foreach (var ctrl in oldForms)
                {
                    try { ctrl.Dispose(); }
                    catch { /* bỏ qua */ }
                }
            }

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            pnMain.Controls.Add(form);
            pnMain.Tag = form;

            form.Show();
        }

        /// <summary>
        /// Phân quyền dựa trên vai trò của người dùng hiện tại (Admin/Member).
        /// Nếu là Member, ẩn các menu không được phép truy cập (ví dụ: Quản lý khách hàng).
        /// </summary>
        private void ApplyRolePermissions()
        {
            Console.WriteLine($"CurrentSession.IsAdmin: {CurrentSession.IsAdmin}");

            if (CurrentSession.IsAdmin)
                return;

            // Member – ẩn menu quản trị khách hàng
            // btnCustomers.Visible = false;
        }

        private void btnDashboards_Click(object sender, EventArgs e)
        {
            LoadMultiForm(new DashboardForm()); // Hiển thị Form DashboardForm bên trong pnMain
        }

        private void btnAccounts_Click(object sender, EventArgs e)
        {
            if (!CurrentSession.HasCustomerInfo)
            {
                LoadMultiForm(new CustomerInfoForm());
                return;
            }
            
            
             LoadMultiForm(new AccountForm());
            
        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            LoadMultiForm(new TransactionForm()); // Hiển thị Form TransactionForm bên trong pnMain
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            LoadMultiForm(new CustomerForm()); // Hiển thị Form CustomerForm bên trong pnMain
        }

        private void btnBanks_Click(object sender, EventArgs e)
        {
            LoadMultiForm(new BankForm()); // Hiển thị Form BankForm bên trong pnMain
        }

        private void btnStatistic_Click(object sender, EventArgs e)
        {
            LoadMultiForm(new StatisticsForm()); // Hiển thị Form StatisticForm bên trong pnMain
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            LoadMultiForm(new SettingsForm());
        }

        private void pnMain_Paint(object sender, PaintEventArgs e)
        {
        }
        
        /// <summary>
        /// Dọn dẹp form con trong pnMain TRƯỚC KHI MainForm đóng.
        /// 
        /// Không làm bước này -> form con vẫn giữ delegate WndProc 
        /// -> khi form bị GC sẽ gây lỗi CallbackOnCollectedDelegate.
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            DisposeChildForms();
            base.OnFormClosed(e);
        }

        /// <summary>
        /// Dispose tất cả form con đang nhúng trong pnMain.
        /// Gọi từ OnFormClosed để đảm bảo giải phóng sạch trước khi app thoát.
        /// </summary>
        private void DisposeChildForms()
        {
            if (pnMain == null || pnMain.IsDisposed)
                return;

            if (pnMain.Controls.Count == 0)
                return;

            // Copy ra mảng trước khi Clear để tránh lỗi collection bị modify khi duyệt
            var children = new System.Collections.Generic.List<Control>();
            foreach (Control c in pnMain.Controls)
                children.Add(c);

            pnMain.Controls.Clear();

            foreach (var ctrl in children)
            {
                try { ctrl.Dispose(); }
                catch { /* bỏ qua nếu đã dispose */ }
            }
        }
    }
}