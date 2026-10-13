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
        private System.Windows.Forms.Timer _autoRefreshTimer;
        private bool _isRefreshing = false;

        public StatisticsForm()
        {
            InitializeComponent();

            DatabaseConfig config = DatabaseConfigManager.Load();
            _statisticService = new StatisticService(config);

            buttonRefresh.Click += ButtonRefresh_Click;

            // ⭐ Lắng nghe sự kiện thêm/thay đổi giao dịch để cập nhật tức thời
            TransactionService.OnTransactionChanged += OnTransactionChangedHandler;

            this.VisibleChanged += async (s, e) =>
            {
                if (this.Visible && !this.Disposing && !this.IsDisposed)
                {
                    await RefreshAllStatsAsync(false);
                }
            };

            this.Activated += async (s, e) =>
            {
                if (this.Visible && !this.Disposing && !this.IsDisposed)
                {
                    await RefreshAllStatsAsync(false);
                }
            };

            // ⭐ Tự động đồng bộ ngầm định kỳ mỗi 3 giây
            _autoRefreshTimer = new System.Windows.Forms.Timer();
            _autoRefreshTimer.Interval = 3000;
            _autoRefreshTimer.Tick += async (s, e) =>
            {
                if (this.Visible && !this.Disposing && !this.IsDisposed)
                {
                    await RefreshAllStatsAsync(false);
                }
            };
            _autoRefreshTimer.Start();
        }

        private void OnTransactionChangedHandler()
        {
            if (!this.IsDisposed && this.IsHandleCreated)
            {
                this.BeginInvoke((Action)(async () =>
                {
                    await RefreshAllStatsAsync(false);
                }));
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            TransactionService.OnTransactionChanged -= OnTransactionChangedHandler;
            if (_autoRefreshTimer != null)
            {
                _autoRefreshTimer.Stop();
                _autoRefreshTimer.Dispose();
                _autoRefreshTimer = null;
            }
            base.OnFormClosed(e);
        }

        /// <summary>
        /// Cập nhật toàn bộ số liệu và biểu đồ thống kê tức thời.
        /// </summary>
        public async Task RefreshAllStatsAsync(bool updateDatePickers = false)
        {
            if (_isRefreshing) return;
            _isRefreshing = true;

            try
            {
                if (updateDatePickers)
                {
                    int currentYear = DateTime.Now.Year;
                    int currentQuarter = (DateTime.Now.Month - 1) / 3 + 1;
                    int currentStartMonth = (currentQuarter - 1) * 3 + 1;
                    DateTime quarterStart = new DateTime(currentYear, currentStartMonth, 1);

                    dateFrom.Value = quarterStart;
                    dateTo.Value = DateTime.Today;
                }

                DateTime fromDate = dateFrom.Value.Date;
                DateTime toDate = dateTo.Value.Date;

                await LoadSummaryAsync(fromDate, toDate);
                await LoadTransactionChartAsync();
                await LoadAccountTypeChartAsync();
                await LoadTopAccountsAsync();

                if (labelUpdatedAt != null)
                {
                    labelUpdatedAt.Text = $"Cập nhật lúc: {DateTime.Now:HH:mm:ss dd/MM/yyyy}";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[StatisticsForm] Refresh error: {ex.Message}");
            }
            finally
            {
                _isRefreshing = false;
            }
        }

        /// <summary>
        /// Khi form thống kê được tải, phương thức này sẽ được gọi để cấu hình biểu đồ giao dịch và tải dữ liệu biểu đồ giao dịch bất đồng bộ.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void StatisticsForm_Load(object sender, EventArgs e)
        {
            // Thiết lập mặc định từ đầu quý hiện tại đến hôm nay
            int currentYear = DateTime.Now.Year;
            int currentQuarter = (DateTime.Now.Month - 1) / 3 + 1;
            int currentStartMonth = (currentQuarter - 1) * 3 + 1;
            DateTime quarterStart = new DateTime(currentYear, currentStartMonth, 1);

            dateFrom.Value = quarterStart;
            dateTo.Value = DateTime.Today;

            ConfigureTransactionChart();
            ConfigureAccountTypeChart();
            ConfigureTopAccountsGrid();

            await RefreshAllStatsAsync(false);
        }

        /// <summary>
        /// Tải dữ liệu thống kê giao dịch theo quý (hiển thị 3 tháng của quý đó).
        /// 1 năm có 4 quý:
        /// Quý 1: T1, T2, T3
        /// Quý 2: T4, T5, T6
        /// Quý 3: T7, T8, T9
        /// Quý 4: T10, T11, T12
        /// Sang tháng mới thuộc quý mới thì chuyển sang quý mới.
        /// </summary>
        private async Task LoadTransactionChartAsync()
        {
            DateTime refDate = dateTo.Value.Date;
            int year = refDate.Year;
            int quarter = (refDate.Month - 1) / 3 + 1;
            int startMonth = (quarter - 1) * 3 + 1; // 1, 4, 7, 10

            DateTime quarterStart = new DateTime(year, startMonth, 1);
            DateTime quarterEnd = quarterStart.AddMonths(3).AddDays(-1);

            List<TransactionStatistic> data =
                await _statisticService.GetTransactionsByMonthAsync(quarterStart, quarterEnd);

            chartTransactions.Series.Clear();
            chartTransactions.Titles.Clear();

            labelChartBarTitle.Text = $"Giao dịch theo quý (Quý {quarter}/{year})";

            Series series = new Series("Giao dịch")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                CustomProperties = "PointWidth=0.55"
            };

            for (int m = startMonth; m < startMonth + 3; m++)
            {
                var item = data.FirstOrDefault(x => x.Year == year && x.Month == m);
                int count = item != null ? item.TransactionCount : 0;
                series.Points.AddXY($"T{m}/{year}", count);
            }

            chartTransactions.Series.Add(series);
            chartTransactions.Invalidate();
            chartTransactions.Update();
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

            await RefreshAllStatsAsync(false);
        }

        private async void ButtonRefresh_Click(object sender, EventArgs e)
        {
            await RefreshAllStatsAsync(true);
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
        /// Chỉ hiển thị loại tiền nào có tài khoản thực tế (VND, USD, Euro...).
        /// </summary>
        /// <returns></returns>
        private async Task LoadAccountTypeChartAsync()
        {
            List<AccountTypeStatistic> data =
                await _statisticService
                    .GetAccountTypesAsync();

            chartAccountTypes.Series.Clear();
            chartAccountTypes.Titles.Clear();

            // ⭐ Chỉ hiển thị các loại tiền thực tế có tài khoản (AccountCount > 0)
            data = data.Where(item => item.AccountCount > 0).ToList();

            if (data.Count == 0)
            {
                Title title =
                    new Title("Không có dữ liệu loại tiền");

                title.Font =
                    new Font("Segoe UI", 11, FontStyle.Regular);

                title.ForeColor =
                    Color.Gray;

                title.Docking =
                    Docking.Top;

                chartAccountTypes.Titles.Add(title);

                if (labelDonutCenter1 != null)
                    labelDonutCenter1.Text = "0";

                return;
            }

            int totalAccounts = data.Sum(x => x.AccountCount);
            if (labelDonutCenter1 != null)
                labelDonutCenter1.Text = totalAccounts.ToString();

            Series series = new Series("Loại tiền")
            {
                ChartType =
                    SeriesChartType.Doughnut,

                IsValueShownAsLabel = true
            };
            series["DoughnutRadius"] = "60";

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
            chartAccountTypes.Invalidate();
            chartAccountTypes.Update();
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
            dgvTopAccounts.Refresh();
        }

        /// <summary>
        /// Cấu hình DataGridView hiển thị top tài khoản (Admin) hoặc số dư các ngân hàng (User).
        /// Với User: Tiêu đề là 'Số dư các ngân hàng', cột 'Ngân hàng', và cột 'Số dư (Thu - Chi)'.
        /// Số dư dương hiển thị màu xanh, số dư âm hiển thị màu đỏ.
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
            dgvTopAccounts.Columns[0].FillWeight = 12;


            // Số tài khoản
            dgvTopAccounts.Columns[1]
                .DataPropertyName = "AccountNumber";

            dgvTopAccounts.Columns[1]
                .HeaderText = "Số tài khoản";
            dgvTopAccounts.Columns[1].FillWeight = 28;


            if (!CurrentSession.IsAdmin)
            {
                labelTopTitle.Text = "Số dư các ngân hàng";

                // Ngân hàng
                dgvTopAccounts.Columns[2]
                    .DataPropertyName = "AccountHolder";

                dgvTopAccounts.Columns[2]
                    .HeaderText = "Ngân hàng";
                dgvTopAccounts.Columns[2].FillWeight = 30;


                // Số dư (Tổng thu - Tổng chi)
                dgvTopAccounts.Columns[3]
                    .DataPropertyName = "Balance";

                dgvTopAccounts.Columns[3]
                    .HeaderText = "Số dư (Thu - Chi)";
                dgvTopAccounts.Columns[3].FillWeight = 30;

                if (linkViewAll != null)
                    linkViewAll.Visible = false;
            }
            else
            {
                labelTopTitle.Text = "Top tài khoản theo số dư";

                // Chủ tài khoản
                dgvTopAccounts.Columns[2]
                    .DataPropertyName = "AccountHolder";

                dgvTopAccounts.Columns[2]
                    .HeaderText = "Chủ tài khoản";
                dgvTopAccounts.Columns[2].FillWeight = 30;


                // Số dư
                dgvTopAccounts.Columns[3]
                    .DataPropertyName = "Balance";

                dgvTopAccounts.Columns[3]
                    .HeaderText = "Số dư";
                dgvTopAccounts.Columns[3].FillWeight = 30;

                if (linkViewAll != null)
                    linkViewAll.Visible = true;
            }

            dgvTopAccounts.Columns[3]
                .DefaultCellStyle.Format = "N0";

            dgvTopAccounts.Columns[3]
                .DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;

            dgvTopAccounts.Columns[3]
                .DefaultCellStyle.Font =
                    new Font(
                        dgvTopAccounts.Font,
                        FontStyle.Bold);

            dgvTopAccounts.CellFormatting -= DgvTopAccounts_CellFormatting;
            dgvTopAccounts.CellFormatting += DgvTopAccounts_CellFormatting;
        }

        /// <summary>
        /// Format đúng loại tiền tệ cho cột số dư trong DataGridView.
        /// Admin: Màu xanh SeaGreen.
        /// User: Số dư là Tổng thu - Tổng chi. Dương: màu xanh (+), Âm: màu đỏ (-).
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

            if (CurrentSession.IsAdmin)
            {
                e.Value =
                    $"{balance:N0} {currency}";

                e.CellStyle.ForeColor =
                    Color.SeaGreen;
            }
            else
            {
                // Khi là User: số dư là tổng thu - tổng chi
                // Nếu âm thì là âm màu đỏ, nếu dương thì là màu xanh
                if (balance > 0)
                {
                    e.Value = $"+{balance:N0} {currency}";
                    e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129); // Màu xanh
                }
                else if (balance < 0)
                {
                    e.Value = $"{balance:N0} {currency}"; // Đã có dấu '-' tự nhiên
                    e.CellStyle.ForeColor = Color.FromArgb(239, 68, 68); // Màu đỏ
                }
                else
                {
                    e.Value = $"0 {currency}";
                    e.CellStyle.ForeColor = Color.FromArgb(100, 116, 139); // Màu xám
                }
            }

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
