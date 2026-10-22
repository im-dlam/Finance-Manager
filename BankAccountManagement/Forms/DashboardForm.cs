using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BankAccountManagement.Services;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Form Bảng điều khiển tổng quan dành cho Quản trị viên (Admin Dashboard Form).
    /// <para>
    /// Hiển thị các chỉ số thống kê vĩ mô của toàn hệ thống ngân hàng:
    /// - 4 thẻ tóm tắt (KPI Cards): Tổng số tài khoản, Tổng số khách hàng, Tổng số ngân hàng liên kết, Tổng số giao dịch.
    /// - Biểu đồ cột (Column Chart): Thống kê số lượng tài khoản mở mới theo từng tháng trong năm.
    /// - Biểu đồ tròn (Pie Chart): Tỷ lệ phân bổ tài khoản theo từng ngân hàng đối tác.
    /// </para>
    /// </summary>
    public partial class DashboardForm : Form
    {
        #region 1. KHỞI TẠO VÀ SỰ KIỆN FORM LOAD

        /// <summary>Dịch vụ xử lý thống kê số liệu tổng quan bảng điều khiển.</summary>
        private readonly DashboardService _dashboardService;

        /// <summary>
        /// Hàm dựng khởi tạo giao diện và kết nối dịch vụ DashboardService.
        /// </summary>
        public DashboardForm()
        {
            InitializeComponent();
            _dashboardService = new DashboardService();
            this.Load += DashboardForm_Load;
        }

        /// <summary>
        /// Tải dữ liệu tổng quan và vẽ các biểu đồ thống kê khi Form xuất hiện.
        /// </summary>
        private void DashboardForm_Load(object sender, EventArgs e)
        {
            string maKH = null;

            if (CurrentSession.IsMember)
            {
                // Giao diện Khách hàng (Member): Đổi tiêu đề phụ và thu gọn 3 thẻ KPI
                if (lblSubtitle != null)
                {
                    lblSubtitle.Text = "Tổng quan quản lý tài khoản cá nhân";
                }

                if (pnlCards != null)
                {
                    pnlCards.SuspendLayout();
                    pnlCards.Controls.Clear();
                    pnlCards.ColumnCount = 3;
                    pnlCards.ColumnStyles.Clear();
                    pnlCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
                    pnlCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
                    pnlCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33334F));

                    if (roundedPanel2 != null) roundedPanel2.Visible = false;

                    if (roundedPanel1 != null)
                    {
                        roundedPanel1.Visible = true;
                        roundedPanel1.Margin = new Padding(0, 0, 10, 0);
                        pnlCards.Controls.Add(roundedPanel1, 0, 0);
                    }
                    if (roundedPanel3 != null)
                    {
                        roundedPanel3.Visible = true;
                        roundedPanel3.Margin = new Padding(0, 0, 10, 0);
                        pnlCards.Controls.Add(roundedPanel3, 1, 0);
                    }
                    if (roundedPanel4 != null)
                    {
                        roundedPanel4.Visible = true;
                        roundedPanel4.Margin = new Padding(0);
                        pnlCards.Controls.Add(roundedPanel4, 2, 0);
                    }
                    pnlCards.ResumeLayout();
                }

                maKH = CurrentSession.MaKH;
            }
            else
            {
                // Giao diện Quản trị viên (Admin): Đầy đủ 4 thẻ KPI bao quát toàn bộ hệ thống
                if (lblSubtitle != null)
                {
                    lblSubtitle.Text = "Tổng quan hệ thống quản lý tài khoản ngân hàng";
                }

                if (pnlCards != null)
                {
                    pnlCards.SuspendLayout();
                    pnlCards.Controls.Clear();
                    pnlCards.ColumnCount = 4;
                    pnlCards.ColumnStyles.Clear();
                    pnlCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                    pnlCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                    pnlCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                    pnlCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

                    if (roundedPanel1 != null)
                    {
                        roundedPanel1.Visible = true;
                        roundedPanel1.Margin = new Padding(0, 0, 10, 0);
                        pnlCards.Controls.Add(roundedPanel1, 0, 0);
                    }
                    if (roundedPanel2 != null)
                    {
                        roundedPanel2.Visible = true;
                        roundedPanel2.Margin = new Padding(0, 0, 10, 0);
                        pnlCards.Controls.Add(roundedPanel2, 1, 0);
                    }
                    if (roundedPanel3 != null)
                    {
                        roundedPanel3.Visible = true;
                        roundedPanel3.Margin = new Padding(0, 0, 10, 0);
                        pnlCards.Controls.Add(roundedPanel3, 2, 0);
                    }
                    if (roundedPanel4 != null)
                    {
                        roundedPanel4.Visible = true;
                        roundedPanel4.Margin = new Padding(0);
                        pnlCards.Controls.Add(roundedPanel4, 3, 0);
                    }
                    pnlCards.ResumeLayout();
                }

                maKH = null;
            }

            // Nạp dữ liệu vào 4 thẻ tóm tắt và 2 biểu đồ phân tích
            LoadSummaryData(maKH);
            LoadBarChart(maKH);
            LoadPieChart(maKH);
        }

        #endregion

        #region 2. TẢI DỮ LIỆU THẺ CHỈ SỐ KPI (SUMMARY CARDS)

        /// <summary>
        /// Nạp các chỉ số tổng quan từ cơ sở dữ liệu lên 4 thẻ hiển thị.
        /// </summary>
        /// <param name="maKH">Mã khách hàng (để lọc dữ liệu cá nhân nếu là Member, null nếu là Admin xem toàn hệ thống).</param>
        private void LoadSummaryData(string maKH = null)
        {
            try
            {
                DataTable dt = _dashboardService.GetDashboardSummary(maKH);
                if (dt != null && dt.Rows.Count > 0)
                {
                    label3.Text = dt.Rows[0]["TongTaiKhoan"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["TongTaiKhoan"]).ToString("N0") : "0";
                    label7.Text = dt.Rows[0]["TongKhachHang"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["TongKhachHang"]).ToString("N0") : "0";
                    label10.Text = dt.Rows[0]["TongNganHang"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["TongNganHang"]).ToString("N0") : "0";
                    label13.Text = dt.Rows[0]["TongGiaoDich"] != DBNull.Value ? Convert.ToInt64(dt.Rows[0]["TongGiaoDich"]).ToString("N0") : "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu tổng quan: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 3. VẼ BIỂU ĐỒ CỘT THỐNG KÊ THEO THÁNG (COLUMN CHART)

        /// <summary>
        /// Vẽ biểu đồ cột biểu diễn số lượng tài khoản được mở mới theo các tháng trong năm.
        /// </summary>
        /// <param name="maKH">Mã khách hàng cần lọc (null nếu xem toàn hệ thống).</param>
        private void LoadBarChart(string maKH = null)
        {
            try
            {
                DataTable dt = _dashboardService.GetAccountByMonth(maKH);

                chart1.Series.Clear();
                chart1.Titles.Clear();

                Series series = new Series("Tài khoản mở mới")
                {
                    ChartType = SeriesChartType.Column,
                    Color = Color.FromArgb(66, 133, 244) // Google Blue hiện đại
                };
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

                    // Nếu số lượng nhỏ, định dạng bước nhảy trục Y là số nguyên (1, 2, 3...)
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

        #endregion

        #region 4. VẼ BIỂU ĐỒ TRÒN THỐNG KÊ THEO NGÂN HÀNG (PIE CHART)

        /// <summary>
        /// Vẽ biểu đồ tròn phân tích tỷ lệ phần trăm phân bổ tài khoản theo từng ngân hàng đối tác.
        /// </summary>
        /// <param name="maKH">Mã khách hàng cần lọc (null nếu xem toàn hệ thống).</param>
        private void LoadPieChart(string maKH = null)
        {
            try
            {
                DataTable dt = _dashboardService.GetAccountByBank(maKH);

                chart2.Series.Clear();
                chart2.Titles.Clear();

                Series series = new Series("Tỉ lệ")
                {
                    ChartType = SeriesChartType.Pie
                };
                chart2.Series.Add(series);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string nganHang = row["NganHang"].ToString();
                        int soLuong = Convert.ToInt32(row["SoLuong"]);
                        series.Points.AddXY(nganHang, soLuong);
                    }

                    // Cấu hình nhãn tỷ lệ phần trăm (%) trên từng miếng bánh và hiển thị chú giải (Legend)
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

        #endregion

        #region 5. CÁC PHƯƠNG THỨC RỖNG TƯƠNG THÍCH DESIGNER

        private void DashboardForm_Load_1(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void roundedPanel1_Paint(object sender, PaintEventArgs e) { }
        private void roundedPanel2_Paint(object sender, PaintEventArgs e) { }
        private void roundedPanel3_Paint(object sender, PaintEventArgs e) { }
        private void roundedPanel4_Paint(object sender, PaintEventArgs e) { }

        #endregion
    }
}
