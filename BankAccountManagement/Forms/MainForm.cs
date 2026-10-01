using BankAccountManagement.Forms;
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
        public MainForm()
        {
            InitializeComponent();
            LoadMultiForm(new DashboardForm());
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
            if (this.pnMain.Controls.Count > 0) // Kiểm tra nếu pnMain đang chứa Form khác
            {
                this.pnMain.Controls.RemoveAt(0); // xoa Form cu
            }

            form.TopLevel = false;
            form.Dock = DockStyle.Fill;

            this.pnMain.Controls.Add(form);
            this.pnMain.Tag = form;

            form.Show();
        }

        private void btnDashboards_Click(object sender, EventArgs e)
        {
            LoadMultiForm(new DashboardForm()); // Hiển thị Form DashboardForm bên trong pnMain
        }

        private void btnAccounts_Click(object sender, EventArgs e)
        {
            LoadMultiForm(new AccountForm()); // Hiển thị Form AccountForm bên trong pnMain
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
    }
}
