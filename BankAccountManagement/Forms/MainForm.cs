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
        /// Khá»Ÿi táº¡o MainForm vá»›i user Ä‘Ã£ Ä‘Äƒng nháº­p.
        /// </summary>
        /// <param name="user">User hiá»‡n táº¡i (báº¯t buá»™c, khÃ´ng Ä‘Æ°á»£c null).</param>
        public MainForm(User user)
        {
            InitializeComponent();
            LoadMultiForm(new DashboardForm());
            ApplyRolePermissions();
        }

        /// <summary>
        /// Hiá»ƒn thá»‹ Form con bÃªn trong pnMain.
        /// Náº¿u pnMain Ä‘ang chá»©a Form khÃ¡c, Form cÅ© sáº½ Ä‘Æ°á»£c xÃ³a trÆ°á»›c.
        /// </summary>
        /// <param name="form">
        /// Form cáº§n hiá»ƒn thá»‹ bÃªn trong pnMain.
        /// </param>
        public void LoadMultiForm(Form form)
        {
            // Dispose form cÅ© Ä‘á»ƒ giáº£i phÃ³ng window handle + delegate
            // fix lá»—i CallbackOnCollectedDelegate khi form cÅ© bá»‹ GC.
            if (pnMain.Controls.Count > 0)
            {
                var oldForms = new System.Collections.Generic.List<Control>();
                foreach (Control c in pnMain.Controls)
                    oldForms.Add(c);

                pnMain.Controls.Clear();

                foreach (var ctrl in oldForms)
                {
                    try { ctrl.Dispose(); }
                    catch { /* bá» qua */ }
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
        /// PhÃ¢n quyá»n dá»±a trÃªn vai trÃ² cá»§a ngÆ°á»i dÃ¹ng hiá»‡n táº¡i (Admin/Member).
        /// Náº¿u lÃ  Member, áº©n cÃ¡c menu khÃ´ng Ä‘Æ°á»£c phÃ©p truy cáº­p (vÃ­ dá»¥: Quáº£n lÃ½ khÃ¡ch hÃ ng).
        /// </summary>
        private void ApplyRolePermissions()
        {
            Console.WriteLine($"CurrentSession.IsAdmin: {CurrentSession.IsAdmin}");

            if (CurrentSession.IsAdmin)
                return;

            // Member â€“ áº©n menu quáº£n trá»‹ khÃ¡ch hÃ ng
            // btnCustomers.Visible = false;
        }

        private void btnDashboards_Click(object sender, EventArgs e)
        {
            LoadMultiForm(new DashboardForm()); // Hiá»ƒn thá»‹ Form DashboardForm bÃªn trong pnMain
        }

        private void btnAccounts_Click(object sender, EventArgs e)
        {
            LoadMultiForm(new AccountForm()); // Hiá»ƒn thá»‹ Form AccountForm bÃªn trong pnMain
        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            LoadMultiForm(new TransactionForm()); // Hiá»ƒn thá»‹ Form TransactionForm bÃªn trong pnMain
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            LoadMultiForm(new CustomerForm()); // Hiá»ƒn thá»‹ Form CustomerForm bÃªn trong pnMain
        }

        private void btnBanks_Click(object sender, EventArgs e)
        {
            LoadMultiForm(new BankForm()); // Hiá»ƒn thá»‹ Form BankForm bÃªn trong pnMain
        }

        private void btnStatistic_Click(object sender, EventArgs e)
        {
            LoadMultiForm(new StatisticsForm()); // Hiá»ƒn thá»‹ Form StatisticForm bÃªn trong pnMain
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            LoadMultiForm(new SettingsForm());
        }

        private void pnMain_Paint(object sender, PaintEventArgs e)
        {
        }
        /// <summary>
        /// Dá»n dáº¹p form con trong pnMain TRÆ¯á»šC KHI MainForm Ä‘Ã³ng.
        /// 
        /// KhÃ´ng lÃ m bÆ°á»›c nÃ y â†’ form con váº«n giá»¯ delegate WndProc 
        /// â†’ khi form bá»‹ GC sáº½ gÃ¢y lá»—i CallbackOnCollectedDelegate.
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            DisposeChildForms();
            base.OnFormClosed(e);
        }

        /// <summary>
        /// Dispose táº¥t cáº£ form con Ä‘ang nhÃºng trong pnMain.
        /// Gá»i tá»« OnFormClosed Ä‘á»ƒ Ä‘áº£m báº£o giáº£i phÃ³ng sáº¡ch trÆ°á»›c khi app thoÃ¡t.
        /// </summary>
        private void DisposeChildForms()
        {
            if (pnMain == null || pnMain.IsDisposed)
                return;

            if (pnMain.Controls.Count == 0)
                return;

            // Copy ra máº£ng trÆ°á»›c khi Clear Ä‘á»ƒ trÃ¡nh lá»—i collection bá»‹ modify khi duyá»‡t
            var children = new System.Collections.Generic.List<Control>();
            foreach (Control c in pnMain.Controls)
                children.Add(c);

            pnMain.Controls.Clear();

            foreach (var ctrl in children)
            {
                try { ctrl.Dispose(); }
                catch { /* bá» qua náº¿u Ä‘Ã£ dispose */ }
            }
        }
    }
}

