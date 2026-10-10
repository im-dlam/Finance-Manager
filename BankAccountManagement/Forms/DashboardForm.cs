using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BankAccountManagement.Services;

namespace BankAccountManagement.Forms
{
    public partial class DashboardForm : Form
    {
        private DashboardService _dashboardService;

        public DashboardForm()
        {
            InitializeComponent();
            _dashboardService = new DashboardService();
            // Automatically bind the Load event to avoid messing with the Designer file
            this.Load += DashboardForm_Load;
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            // Đặt tên biến thống nhất là maKH luôn cho gọn
            string maKH = null;

            if (CurrentSession.IsMember)
            {
                // 1. Ẩn card "Tổng Khách Hàng" (roundedPanel2) đi theo yêu cầu nhóm trưởng
                if (roundedPanel2 != null) roundedPanel2.Visible = false;

                // 2. Lấy MaKH của tài khoản đang đăng nhập
                maKH = CurrentSession.MaKH;
            }
            else if (CurrentSession.IsAdmin)
            {
                // Admin giữ nguyên hiển thị và không lọc
                if (roundedPanel2 != null) roundedPanel2.Visible = true;
                maKH = null;
            }

            // Truyền maKH vào các hàm load dữ liệu
            LoadSummaryData(maKH);
            LoadBarChart(maKH);
            LoadPieChart(maKH);

            //LoadSummaryData();
            //LoadBarChart();
            //LoadPieChart();
        }

        private void LoadSummaryData(string maKH = null)
        {
            try
            {
                // Truyền maKH xuống service
                DataTable dt = _dashboardService.GetDashboardSummary(maKH);
                if (dt != null && dt.Rows.Count > 0)
                {
                    label3.Text = dt.Rows[0]["TongTaiKhoan"].ToString();
                    label7.Text = dt.Rows[0]["TongKhachHang"].ToString();
                    label10.Text = dt.Rows[0]["TongNganHang"].ToString();
                    label13.Text = dt.Rows[0]["TongGiaoDich"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu tổng quan: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBarChart(string maKH = null)
        {
            try
            {
                DataTable dt = _dashboardService.GetAccountByMonth(maKH);
                
                chart1.Series.Clear();
                chart1.Titles.Clear();
                
                Series series = new Series("Tài khoản mở mới");
                series.ChartType = SeriesChartType.Column;
                series.Color = Color.FromArgb(66, 133, 244); // Google Blue
                chart1.Series.Add(series);
                
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

                if (dt != null && dt.Rows.Count > 0)
                {
                    int maxSoLuong = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        string thang = "Tháng " + row["Thang"].ToString();
                        int soLuong = Convert.ToInt32(row["SoLuong"]);
                        if (soLuong > maxSoLuong) maxSoLuong = soLuong;
                        series.Points.AddXY(thang, soLuong);
                    }
                    chart1.ChartAreas[0].AxisX.Enabled = AxisEnabled.True;
                    chart1.ChartAreas[0].AxisY.Enabled = AxisEnabled.True;
                    chart1.ChartAreas[0].AxisX.Title = "Tháng";
                    chart1.ChartAreas[0].AxisY.Title = "Số lượng tài khoản";
                    chart1.ChartAreas[0].AxisX.Interval = 1;
                    
                    // Ép trục Y hiển thị số tự nhiên nếu dữ liệu quá ít (vd: 1, 2, 3...)
                    if (maxSoLuong < 10)
                        chart1.ChartAreas[0].AxisY.Interval = 1;
                }
                else
                {
                    chart1.Titles.Add("Không có dữ liệu để phân tích");
                    chart1.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Italic);
                    chart1.Titles[0].ForeColor = Color.Gray;
                    chart1.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;
                    chart1.ChartAreas[0].AxisY.Enabled = AxisEnabled.False;
                }
            }
            catch (Exception)
            {
                chart1.Titles.Clear();
                chart1.Titles.Add("Không có dữ liệu để phân tích");
                chart1.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;
                chart1.ChartAreas[0].AxisY.Enabled = AxisEnabled.False;
            }
        }

        private void LoadPieChart(string maKH = null)
        {
            try
            {
                DataTable dt = _dashboardService.GetAccountByBank(maKH);
                
                chart2.Series.Clear();
                chart2.Titles.Clear();

                Series series = new Series("Tỉ lệ");
                series.ChartType = SeriesChartType.Pie;
                chart2.Series.Add(series);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string nganHang = row["NganHang"].ToString();
                        int soLuong = Convert.ToInt32(row["SoLuong"]);
                        series.Points.AddXY(nganHang, soLuong);
                    }

                    // Cấu hình hiển thị % và Legend
                    series.IsValueShownAsLabel = true;
                    series.Label = "#PERCENT{P0}";
                    series.LegendText = "#VALX (#VAL)";
                }
                else
                {
                    chart2.Titles.Add("Không có dữ liệu để phân tích");
                    chart2.Titles[0].Font = new Font("Segoe UI", 12, FontStyle.Italic);
                    chart2.Titles[0].ForeColor = Color.Gray;
                }
            }
            catch (Exception)
            {
                chart2.Titles.Clear();
                chart2.Titles.Add("Không có dữ liệu để phân tích");
            }
        }

        private void DashboardForm_Load_1(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void roundedPanel1_Paint(object sender, PaintEventArgs e) { }
        private void roundedPanel2_Paint(object sender, PaintEventArgs e) { }
        private void roundedPanel3_Paint(object sender, PaintEventArgs e) { }
        private void roundedPanel4_Paint(object sender, PaintEventArgs e) { }
    }
}
