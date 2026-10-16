using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    public partial class SpendingLimitDialog : Form
    {
        private readonly string _maKH;
        private readonly SpendingService _spendingService;

        public decimal HanMucNgayMoi { get; private set; }
        public decimal HanMucTuanMoi { get; private set; }
        public bool CanhBaoMoi { get; private set; }

        public SpendingLimitDialog(string maKH = "")
            : this(maKH, 2000000m, 10000000m, true)
        {
        }

        public SpendingLimitDialog(string maKH, decimal currentDaily, decimal currentWeekly, bool currentAlert)
        {
            InitializeComponent();
            _maKH = maKH;
            _spendingService = new SpendingService();

            txtDailyLimit.Text = $"{currentDaily:N0}";
            txtWeeklyLimit.Text = $"{currentWeekly:N0}";
            chkAlert.Checked = currentAlert;

            SetupEvents();
            EnableDragWindow();
        }

        private void SetupEvents()
        {
            btnClose.Click += (s, e) => this.DialogResult = DialogResult.Cancel;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            // Chips ngày
            btnD500k.Click += (s, e) => txtDailyLimit.Text = "500,000";
            btnD1M.Click += (s, e) => txtDailyLimit.Text = "1,000,000";
            btnD2M.Click += (s, e) => txtDailyLimit.Text = "2,000,000";
            btnD5M.Click += (s, e) => txtDailyLimit.Text = "5,000,000";

            // Chips tuần
            btnW3M.Click += (s, e) => txtWeeklyLimit.Text = "3,000,000";
            btnW5M.Click += (s, e) => txtWeeklyLimit.Text = "5,000,000";
            btnW10M.Click += (s, e) => txtWeeklyLimit.Text = "10,000,000";
            btnW20M.Click += (s, e) => txtWeeklyLimit.Text = "20,000,000";

            // Format số tiền khi người dùng gõ
            txtDailyLimit.Leave += (s, e) => FormatTextBox(txtDailyLimit);
            txtWeeklyLimit.Leave += (s, e) => FormatTextBox(txtWeeklyLimit);

            // Nút Lưu
            btnSave.Click += async (s, e) => await HandleSaveAsync();
        }

        private void FormatTextBox(TextBox txt)
        {
            decimal val = ParseAmount(txt.Text);
            if (val > 0)
            {
                txt.Text = $"{val:N0}";
            }
        }

        private decimal ParseAmount(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0m;
            string clean = text.Replace(".", "").Replace(",", "").Replace("đ", "").Replace("VNĐ", "").Trim();
            if (decimal.TryParse(clean, out decimal val))
            {
                return val;
            }
            return 0m;
        }

        private async System.Threading.Tasks.Task HandleSaveAsync()
        {
            decimal daily = ParseAmount(txtDailyLimit.Text);
            decimal weekly = ParseAmount(txtWeeklyLimit.Text);

            if (daily <= 0)
            {
                MessageBox.Show("Vui lòng nhập hạn mức chi tiêu 1 ngày hợp lệ (> 0 đ).",
                    "Dữ liệu chưa đúng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDailyLimit.Focus();
                return;
            }

            if (weekly <= 0)
            {
                MessageBox.Show("Vui lòng nhập hạn mức chi tiêu 1 tuần hợp lệ (> 0 đ).",
                    "Dữ liệu chưa đúng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtWeeklyLimit.Focus();
                return;
            }

            if (daily > weekly)
            {
                MessageBox.Show("Hạn mức chi tiêu theo ngày không thể lớn hơn hạn mức chi tiêu theo tuần.",
                    "Dữ liệu chưa đúng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtWeeklyLimit.Focus();
                return;
            }

            btnSave.Enabled = false;
            btnSave.Text = "Đang lưu...";

            var result = await _spendingService.SaveSpendingLimitAsync(
                _maKH, daily, weekly, chkAlert.Checked);

            if (result.Success)
            {
                HanMucNgayMoi = daily;
                HanMucTuanMoi = weekly;
                CanhBaoMoi = chkAlert.Checked;

                MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = true;
                btnSave.Text = "💾 Lưu hạn mức chi tiêu";
            }
        }

        #region Kéo thả form không viền
        private bool _isDragging = false;
        private Point _dragStartPoint;

        private void EnableDragWindow()
        {
            this.pnlDialogCard.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    _isDragging = true;
                    _dragStartPoint = e.Location;
                }
            };

            this.pnlDialogCard.MouseMove += (s, e) =>
            {
                if (_isDragging)
                {
                    Point diff = Point.Subtract(Cursor.Position, new Size(_dragStartPoint));
                    this.Location = diff;
                }
            };

            this.pnlDialogCard.MouseUp += (s, e) =>
            {
                _isDragging = false;
            };
        }
        #endregion
    }
}
