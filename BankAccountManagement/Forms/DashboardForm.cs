using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BankAccountManagement.Forms
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear(); // Xóa dữ liệu cột rác mặc định

            // Tắt lưới ô vuông đậm, thay bằng màu xám nhạt cho tinh tế
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            // --- Tạo đường 1: Doanh Thu ---
            Series sRevenue = new Series("Revenue");
            sRevenue.ChartType = SeriesChartType.Spline; // Spline giúp đường nét cong mượt
            sRevenue.BorderWidth = 3;
            sRevenue.Color = Color.DodgerBlue;

            sRevenue.Points.AddXY("Sep 01", 2800000);
            sRevenue.Points.AddXY("Sep 08", 4000000);
            sRevenue.Points.AddXY("Sep 13", 3500000);
            sRevenue.Points.AddXY("Sep 17", 4500000);
            sRevenue.Points.AddXY("Sep 24", 7000000);
            sRevenue.Points.AddXY("Sep 30", 5500000);
            chart1.Series.Add(sRevenue);
            // --- Tạo đường 2: Chi Phí ---
            Series sExpenses = new Series("Expenses");
            sExpenses.ChartType = SeriesChartType.Spline;
            sExpenses.BorderWidth = 3;
            sExpenses.Color = Color.MediumSeaGreen;

            sExpenses.Points.AddXY("Sep 01", 1800000);
            sExpenses.Points.AddXY("Sep 08", 2500000);
            sExpenses.Points.AddXY("Sep 13", 3000000);
            sExpenses.Points.AddXY("Sep 17", 2500000);
            sExpenses.Points.AddXY("Sep 24", 4000000);
            sExpenses.Points.AddXY("Sep 30", 3800000);
            chart1.Series.Add(sExpenses);
            // ==========================================
            // 2. DỮ LIỆU BIỂU ĐỒ TRÒN (chart2 - Trạng thái Tài khoản)
            // ==========================================
            chart2.Series.Clear();
            Series sStatus = new Series("Account Status");
            sStatus.ChartType = SeriesChartType.Doughnut; // Biến thành bánh vòng Donut
            sStatus.Points.AddXY("Active", 78);
            sStatus.Points.AddXY("Inactive", 14);
            sStatus.Points.AddXY("Pending", 8);
            // Set màu thủ công cho 3 miếng bánh
            sStatus.Points[0].Color = Color.DodgerBlue;
            sStatus.Points[1].Color = Color.LightSkyBlue;
            sStatus.Points[2].Color = Color.MediumSeaGreen;
            // Bật số % hiện lên trên miếng bánh
            sStatus.IsValueShownAsLabel = true;

            chart2.Series.Add(sStatus);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
