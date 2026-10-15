using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    public partial class CreditCardForm : Form
    {
        public CreditCardForm()
        {
            InitializeComponent();
            ApplyRoundedCircles();
            LoadMockData();
        }
        private void ApplyRoundedCircles()
        {
            RoundCorners(pnlLogoCircle1, 17);       // 34/2 = 17 → tròn hoàn hảo
            RoundCorners(pnlLogoCircle2, 17);
            RoundCorners(lblChip, 8);
        }

        private void RoundCorners(Control ctrl, int radius)
        {
            if (ctrl == null || ctrl.Width <= 0 || ctrl.Height <= 0) return;

            int d = Math.Min(radius * 2, Math.Min(ctrl.Width, ctrl.Height));
            using (var path = new System.Drawing.Drawing2D.GraphicsPath())
            {
                path.AddArc(0, 0, d, d, 180, 90);
                path.AddArc(ctrl.Width - d, 0, d, d, 270, 90);
                path.AddArc(ctrl.Width - d, ctrl.Height - d, d, d, 0, 90);
                path.AddArc(0, ctrl.Height - d, d, d, 90, 90);
                path.CloseFigure();
                ctrl.Region = new System.Drawing.Region(path);
            }
        }

        private void LoadMockData()
        {
            // Dữ liệu mẫu — thay bằng dữ liệu thật từ service
            lblCardNumber.Text = "4213 **** **** 9876";
            lblHolderName.Text = "PHONG HOÁ";
            lblExpiryValue.Text = "12/28";

            lblStatusValue.Text = "Đang hoạt động";
            lblStatusValue.ForeColor = Color.FromArgb(5, 150, 105);

            lblNumValue.Text = "4213 **** **** 9876";
            lblNumStatus.Text = "Đang hoạt động";
            lblBalValue.Text = "**** **** ****";
            lblBalDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblStmtValue.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblStmtDate.Text = DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy");
            lblDueValue.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblDueDate.Text = DateTime.Now.AddMonths(1).ToString("dd/MM/yyyy");
            lblRateValue.Text = "8,8%";
            lblRateDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblStateValue.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblStateStatus.Text = "Đang hoạt động";
        }
    }
}
