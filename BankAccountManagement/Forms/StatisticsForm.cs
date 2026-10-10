using BankAccountManagement.Models;
using BankAccountManagement.Models.Statistics;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace BankAccountManagement.Forms
{
    public partial class StatisticsForm : Form
    {
        /// <summary>
        /// khai báo một trường riêng tư _statisticService kiểu StatisticService để quản lý các chức năng thống kê.
        /// </summary>
        private readonly StatisticService _statisticService; 
        public StatisticsForm()
        {
            InitializeComponent();

            DatabaseConfig config = DatabaseConfigManager.Load();

            _statisticService = new StatisticService(config); // Khởi tạo đối tượng StatisticService với cấu hình cơ sở dữ liệu
        }

        /// <summary>
        /// Khi form thống kê được tải, phương thức này sẽ được gọi để cấu hình biểu đồ giao dịch và tải dữ liệu biểu đồ giao dịch bất đồng bộ.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void StatisticsForm_Load(object sender, EventArgs e)
        {
            ConfigureTransactionChart();

            ConfigureAccountTypeChart();

            ConfigureTopAccountsGrid();

            await LoadTransactionChartAsync();

            await LoadAccountTypeChartAsync();

            await LoadTopAccountsAsync();

            // Load the summary data for the initial date range
            DateTime fromDate = dateFrom.Value.Date;
            DateTime toDate = dateTo.Value.Date;
            await LoadSummaryAsync(fromDate, toDate);
        }

        /// <summary>
        /// Tạo một phương thức bất đồng bộ LoadTransactionChartAsync để tải dữ liệu thống kê giao dịch và hiển thị trên biểu đồ.
        /// </summary>
        /// <returns></returns>
        private async Task LoadTransactionChartAsync()
        {
            DateTime fromDate =
                dateFrom.Value.Date;

            DateTime toDate =
                dateTo.Value.Date;

            List<TransactionStatistic> data =
                await _statisticService
                    .GetTransactionsByMonthAsync(
                        fromDate,
                        toDate);

            chartTransactions.Series.Clear();

            if (data.Count == 0)
            {
                chartTransactions.Titles.Clear();

                Title title =
                    new Title("Không có dữ liệu giao dịch");

                title.Font =
                    new Font("Segoe UI", 11, FontStyle.Regular);

                title.ForeColor =
                    Color.Gray;

                title.Docking =
                    Docking.Top;

                chartTransactions.Titles.Add(title);

                return;
            }

            chartTransactions.Titles.Clear();

            Series series =
                new Series("Giao dịch")
                {
                    ChartType =
                        SeriesChartType.Column,

                    IsValueShownAsLabel = true
                };

            foreach (TransactionStatistic item in data)
            {
                series.Points.AddXY(
                    item.MonthLabel,
                    item.TransactionCount);
            }

            chartTransactions.Series.Add(series);
        }

        /// <summary>
        /// Đẩy dữ liệu thống kê tổng quan từ dịch vụ thống kê và hiển thị trên giao diện người dùng.
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        private async Task LoadSummaryAsync(DateTime fromDate, DateTime toDate)
        {
            StatisticSummary summary  = await _statisticService.GetSummaryAsync(fromDate,toDate);

            lblTotalAccounts.Text = summary.TotalAccounts.ToString("N0");

            lblTotalBalance.Text = summary.TotalBalance.ToString("N0");

            lblTotalTransactions.Text = summary.TotalTransactions.ToString("N0");

            lblTotalCustomers.Text = summary.TotalCustomers.ToString("N0");
        }

        private async void buttonViewStats_Click(object sender, EventArgs e)
        {
            DateTime fromDate = dateFrom.Value.Date;

            DateTime toDate = dateTo.Value.Date;

            if (fromDate > toDate)
            {
                MessageBox.Show(
                    "Ngày bắt đầu không được lớn hơn ngày kết thúc.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            await LoadSummaryAsync(
                fromDate,
                toDate);
            // Load the transaction chart after loading the summary
            await LoadTransactionChartAsync();
            await LoadAccountTypeChartAsync();
            await LoadTransactionChartAsync();
        }



        /// <summary>
        /// Cấu hình biểu đồ giao dịch, bao gồm việc xóa các series hiện có, thêm một ChartArea mới nếu chưa tồn tại, và thiết lập các thuộc tính của trục X và Y.
        /// </summary>
        private void ConfigureTransactionChart()
        {
            chartTransactions.Series.Clear();

            if (chartTransactions.ChartAreas.Count == 0)
            {
                chartTransactions.ChartAreas.Add(
                    new ChartArea("MainArea"));
            }

            ChartArea area =
                chartTransactions.ChartAreas[0];

            area.BackColor = Color.White;

            area.AxisX.MajorGrid.Enabled = false;

            area.AxisY.MajorGrid.Enabled = true;

            area.AxisY.LabelStyle.Format = "N0";

            area.AxisX.Interval = 1;
        }

        /// <summary>
        /// Cấu hình biểu đồ loại tài khoản, bao gồm việc xóa các series hiện có, thêm một ChartArea mới nếu chưa tồn tại, và thiết lập các thuộc tính của Legend.
        /// </summary>
        private void ConfigureAccountTypeChart()
        {
            chartAccountTypes.Series.Clear();

            if (chartAccountTypes.ChartAreas.Count == 0)
            {
                chartAccountTypes.ChartAreas.Add(
                    new ChartArea("MainArea"));
            }

            chartAccountTypes.ChartAreas[0]
                .BackColor = Color.White;

            // Không hiển thị Legend
            chartAccountTypes.Legends.Clear();
        }

        /// <summary>
        /// Load dữ liệu thống kê loại tài khoản từ dịch vụ thống kê và hiển thị trên biểu đồ loại tài khoản.
        /// </summary>
        /// <returns></returns>
        private async Task LoadAccountTypeChartAsync()
        {
            List<AccountTypeStatistic> data =
                await _statisticService
                    .GetAccountTypesAsync();

            chartAccountTypes.Series.Clear();

            chartAccountTypes.Titles.Clear();

            if (data.Count == 0)
            {
                Title title =
                    new Title("Không có dữ liệu loại tài khoản");

                title.Font =
                    new Font("Segoe UI", 11, FontStyle.Regular);

                title.ForeColor =
                    Color.Gray;

                title.Docking =
                    Docking.Top;

                chartAccountTypes.Titles.Add(title);

                return;
            }

            Series series = new Series("Loại tiền")
                {
                    ChartType =
                        SeriesChartType.Doughnut,

                    IsValueShownAsLabel = true
                };

            foreach (AccountTypeStatistic item in data)
            {
                DataPoint point =
                    new DataPoint();

                point.SetValueY(
                    item.AccountCount);

                point.Label =
                    $"{item.AccountType} ({item.AccountCount})";

                series.Points.Add(point);
            }

            chartAccountTypes.Series.Add(series);
        }

        /// <summary>
        /// Tải top 10 tài khoản có số dư cao nhất từ dịch vụ thống kê và hiển thị trong DataGridView.
        /// </summary>
        /// <returns></returns>
        private async Task LoadTopAccountsAsync()
        {
            List<TopAccountStatistic> data =
                await _statisticService
                    .GetTopAccountsAsync(10);

            dgvTopAccounts.DataSource = null;

            if (data.Count == 0)
            {
                dgvTopAccounts.Rows.Clear();

                int rowIndex =
                    dgvTopAccounts.Rows.Add();

                DataGridViewCell cell =
                    dgvTopAccounts.Rows[rowIndex].Cells[0];

                cell.Value =
                    "Không có dữ liệu tài khoản";

                dgvTopAccounts.Rows[rowIndex]
                    .Cells[0]
                    .Style.ForeColor = Color.Gray;

                dgvTopAccounts.Rows[rowIndex]
                    .Cells[0]
                    .Style.Alignment =
                        DataGridViewContentAlignment.MiddleCenter;

                return;
            }

            dgvTopAccounts.DataSource = data;

            ConfigureTopAccountsGrid();
        }

        /// <summary>
        /// Cấu hình DataGridView hiển thị top 10 tài khoản có số dư cao nhất, bao gồm việc thiết lập các thuộc tính như chế độ đọc, không cho phép thêm hoặc xóa hàng, ẩn tiêu đề hàng, và định dạng các cột.
        /// </summary>
        private void ConfigureTopAccountsGrid()
        {
            dgvTopAccounts.AutoGenerateColumns = false;

            dgvTopAccounts.ReadOnly = true;

            dgvTopAccounts.AllowUserToAddRows = false;

            dgvTopAccounts.AllowUserToDeleteRows = false;

            dgvTopAccounts.AllowUserToResizeRows = false;

            dgvTopAccounts.RowHeadersVisible = false;

            dgvTopAccounts.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvTopAccounts.MultiSelect = false;

            dgvTopAccounts.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;


            // STT
            dgvTopAccounts.Columns[0]
                .DataPropertyName = "Rank";

            dgvTopAccounts.Columns[0]
                .HeaderText = "STT";
            dgvTopAccounts.Columns[0].Width = 30;


            // Số tài khoản
            dgvTopAccounts.Columns[1]
                .DataPropertyName = "AccountNumber";

            dgvTopAccounts.Columns[1]
                .HeaderText = "Số tài khoản";
            dgvTopAccounts.Columns[1].Width = 50;


            // Chủ tài khoản
            dgvTopAccounts.Columns[2]
                .DataPropertyName = "AccountHolder";

            dgvTopAccounts.Columns[2]
                .HeaderText = "Chủ tài khoản";


            // Số dư
            dgvTopAccounts.Columns[3]
                .DataPropertyName = "Balance";

            dgvTopAccounts.Columns[3]
                .HeaderText = "Số dư";

            dgvTopAccounts.Columns[3]
                .DefaultCellStyle.Format = "N0";

            dgvTopAccounts.Columns[3]
                .DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;

            dgvTopAccounts.Columns[3]
                .DefaultCellStyle.ForeColor =
                    Color.SeaGreen;

            dgvTopAccounts.Columns[3]
                .DefaultCellStyle.Font =
                    new Font(
                        dgvTopAccounts.Font,
                        FontStyle.Bold);

            dgvTopAccounts.CellFormatting -= DgvTopAccounts_CellFormatting;
            dgvTopAccounts.CellFormatting += DgvTopAccounts_CellFormatting;
        }

        /// <summary>
        /// Format đúng loại tiền tệ cho cột số dư trong DataGridView, bao gồm việc hiển thị số dư với định dạng số và thêm ký hiệu tiền tệ tương ứng.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvTopAccounts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex != 3 ||
                e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row =
                dgvTopAccounts.Rows[e.RowIndex];

            object balanceValue =
                row.Cells[3].Value;

            if (balanceValue == null ||
                balanceValue == DBNull.Value)
            {
                return;
            }

            decimal balance =
                Convert.ToDecimal(balanceValue);

            object currencyValue =
                row.DataBoundItem is TopAccountStatistic item
                    ? item.AccountCurrency
                    : null;

            string currency =
                currencyValue?.ToString() ?? string.Empty;

            e.Value =
                $"{balance:N0} {currency}";

            e.CellStyle.ForeColor =
                Color.SeaGreen;

            e.CellStyle.Font =
                new Font(
                    dgvTopAccounts.Font,
                    FontStyle.Bold);

            e.CellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight;

            e.FormattingApplied = true;
        }
    }
}
