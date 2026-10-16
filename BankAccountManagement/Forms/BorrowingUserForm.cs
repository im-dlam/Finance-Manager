using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Giao diện đăng ký khoản vay vốn trực tuyến dành riêng cho Khách hàng (User/Member).
    /// Thiết kế chuẩn chỉnh theo phong cách Fintech hiện đại:
    /// - Thanh trượt tương tác mượt mà kéo thả số tiền và kỳ hạn
    /// - Tính toán lãi suất và số nợ phải trả tự động thời gian thực
    /// - Bảng tóm tắt hiển thị rõ ràng lãi suất ưu đãi và ngày đáo hạn
    /// - Đăng ký giải ngân trực tiếp vào tài khoản ngân hàng của khách
    /// </summary>
    public partial class BorrowingUserForm : Form
    {
        private readonly BorrowingService _borrowingService;
        private List<TaiKhoan> _userAccounts = new List<TaiKhoan>();
        private TaiKhoan _selectedAccount = null;

        // Cấu hình giá trị khoản vay từ BorrowingService (VND)
        private const decimal VAY_MIN = BorrowingService.VAY_MIN;
        private const decimal VAY_MAX = BorrowingService.VAY_MAX;
        private const decimal VAY_STEP = BorrowingService.VAY_STEP;
        private decimal _currentVayAmount = 1000000m;

        // Cấu hình kỳ hạn vay từ BorrowingService (Ngày)
        private const int TIME_MIN = BorrowingService.TIME_MIN;
        private const int TIME_MAX = BorrowingService.TIME_MAX;
        private const int TIME_STEP = BorrowingService.TIME_STEP;
        private int _currentTime = 30;

        // Lãi suất ưu đãi từ BorrowingService
        private const decimal DAILY_INTEREST_RATE = BorrowingService.DEFAULT_DAILY_INTEREST_RATE;

        // Trạng thái kéo chuột slider
        private bool _isDraggingVay = false;
        private bool _isDraggingTime = false;
        private bool _isUpdatingText = false;

        public BorrowingUserForm()
        {
            InitializeComponent();
            _borrowingService = new BorrowingService();

            pnlFooter.Resize += (s, e) => CenterRegisterButton();
            CenterRegisterButton();

            SetupHistoryGridView();
            WireSliderEvents();
            WireTextEvents();

            this.Load += async (s, e) => await InitializeDataAsync();
            this.lblAgreeText.Click += (s, e) => { chkAgree.Checked = !chkAgree.Checked; };
            this.btnRegister.Click += async (s, e) => await HandleRegisterLoanAsync();
            this.btnHistory.Click += async (s, e) => await ToggleHistoryViewAsync();
            this.btnCloseHistory.Click += (s, e) => CloseHistoryView();
        }

        private void CenterRegisterButton()
        {
            if (btnRegister != null && pnlFooter != null)
            {
                btnRegister.Left = Math.Max(0, (pnlFooter.ClientSize.Width - btnRegister.Width) / 2);
                btnRegister.Top = Math.Max(0, (pnlFooter.ClientSize.Height - btnRegister.Height) / 2);
            }
        }

        private async Task InitializeDataAsync()
        {
            await LoadUserAccountsAsync();
            CenterRegisterButton();
            UpdateCalculations();
            UpdateVaySliderUI();
            UpdateTimeSliderUI();
            thumbVay.BringToFront();
            thumbTime.BringToFront();
        }

        /// <summary>
        /// Tải danh sách tài khoản ngân hàng khả dụng của khách hàng.
        /// </summary>
        private async Task LoadUserAccountsAsync()
        {
            string maKH = CurrentSession.MaKH;
            _userAccounts = await _borrowingService.GetUserPaymentAccountsAsync(maKH);

            cboAccount.Items.Clear();
            if (_userAccounts.Count == 0)
            {
                cboAccount.Items.Add("Chưa có tài khoản ngân hàng hoạt động");
                cboAccount.SelectedIndex = 0;
                btnRegister.Enabled = false;
                return;
            }

            foreach (var acc in _userAccounts)
            {
                cboAccount.Items.Add($"{acc.SoTaiKhoan} ({acc.MaNganHang} - Số dư: {acc.SoDuHienTai:N0} VND)");
            }

            cboAccount.SelectedIndex = 0;
            _selectedAccount = _userAccounts[0];
            cboAccount.SelectedIndexChanged += (s, e) =>
            {
                if (cboAccount.SelectedIndex >= 0 && cboAccount.SelectedIndex < _userAccounts.Count)
                {
                    _selectedAccount = _userAccounts[cboAccount.SelectedIndex];
                }
            };
        }

        #region SLIDER INTERACTION & TWO-WAY BINDING

        private void WireSliderEvents()
        {
            // Slider Khoản vay
            trackVayBg.MouseDown += TrackVay_MouseDown;
            trackVayFill.MouseDown += TrackVay_MouseDown;
            thumbVay.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _isDraggingVay = true; };

            this.MouseMove += TrackVay_MouseMove;
            pnlLeftCard.MouseMove += TrackVay_MouseMove;
            trackVayBg.MouseMove += TrackVay_MouseMove;
            trackVayFill.MouseMove += TrackVay_MouseMove;
            thumbVay.MouseMove += TrackVay_MouseMove;

            this.MouseUp += (s, e) => { _isDraggingVay = false; _isDraggingTime = false; };
            pnlLeftCard.MouseUp += (s, e) => { _isDraggingVay = false; _isDraggingTime = false; };
            thumbVay.MouseUp += (s, e) => { _isDraggingVay = false; };
            trackVayBg.MouseUp += (s, e) => { _isDraggingVay = false; };
            trackVayFill.MouseUp += (s, e) => { _isDraggingVay = false; };

            // Slider Thời gian vay
            trackTimeBg.MouseDown += TrackTime_MouseDown;
            trackTimeFill.MouseDown += TrackTime_MouseDown;
            thumbTime.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) _isDraggingTime = true; };

            this.MouseMove += TrackTime_MouseMove;
            pnlLeftCard.MouseMove += TrackTime_MouseMove;
            trackTimeBg.MouseMove += TrackTime_MouseMove;
            trackTimeFill.MouseMove += TrackTime_MouseMove;
            thumbTime.MouseMove += TrackTime_MouseMove;

            thumbTime.MouseUp += (s, e) => { _isDraggingTime = false; };
            trackTimeBg.MouseUp += (s, e) => { _isDraggingTime = false; };
            trackTimeFill.MouseUp += (s, e) => { _isDraggingTime = false; };
        }

        private void WireTextEvents()
        {
            txtVayAmount.Leave += (s, e) =>
            {
                if (_isUpdatingText) return;
                string raw = txtVayAmount.Text.Replace(".", "").Replace(",", "").Trim();
                if (decimal.TryParse(raw, out decimal val))
                {
                    SetVayAmount(val, true);
                }
                else
                {
                    SetVayAmount(_currentVayAmount, true);
                }
            };

            txtVayAmount.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtVayAmount.Parent.Focus();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };

            txtTime.Leave += (s, e) =>
            {
                if (_isUpdatingText) return;
                string raw = txtTime.Text.Trim();
                if (int.TryParse(raw, out int val))
                {
                    SetTime(val, true);
                }
                else
                {
                    SetTime(_currentTime, true);
                }
            };

            txtTime.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtTime.Parent.Focus();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };
        }

        private void TrackVay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _isDraggingVay = true;
            Point p = trackVayBg.PointToClient(Cursor.Position);
            UpdateVayFromMouse(p.X);
        }

        private void TrackVay_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDraggingVay) return;
            Point p = trackVayBg.PointToClient(Cursor.Position);
            UpdateVayFromMouse(p.X);
        }

        private void UpdateVayFromMouse(int mouseX)
        {
            int trackWidth = trackVayBg.Width;
            if (trackWidth <= 0) return;

            float ratio = Math.Max(0f, Math.Min(1f, (float)mouseX / trackWidth));
            decimal rawVal = VAY_MIN + (decimal)ratio * (VAY_MAX - VAY_MIN);
            // Làm tròn theo bước nhảy VAY_STEP
            decimal steppedVal = Math.Round(rawVal / VAY_STEP) * VAY_STEP;
            SetVayAmount(steppedVal, true);
        }

        private void SetVayAmount(decimal val, bool updateUI)
        {
            if (val < VAY_MIN) val = VAY_MIN;
            if (val > VAY_MAX) val = VAY_MAX;

            _currentVayAmount = val;

            if (updateUI)
            {
                _isUpdatingText = true;
                txtVayAmount.Text = $"{_currentVayAmount:N0}";
                _isUpdatingText = false;
                UpdateVaySliderUI();
                UpdateCalculations();
            }
        }

        private void UpdateVaySliderUI()
        {
            int trackWidth = trackVayBg.Width;
            if (trackWidth <= 0) return;

            float ratio = (float)((_currentVayAmount - VAY_MIN) / (VAY_MAX - VAY_MIN));
            int fillWidth = (int)(ratio * trackWidth);
            trackVayFill.Width = Math.Max(0, fillWidth);

            int thumbX = trackVayBg.Left + fillWidth - (thumbVay.Width / 2);
            thumbX = Math.Max(trackVayBg.Left - (thumbVay.Width / 2), Math.Min(trackVayBg.Right - (thumbVay.Width / 2), thumbX));
            thumbVay.Left = thumbX;
            thumbVay.BringToFront();
        }

        private void TrackTime_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _isDraggingTime = true;
            Point p = trackTimeBg.PointToClient(Cursor.Position);
            UpdateTimeFromMouse(p.X);
        }

        private void TrackTime_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDraggingTime) return;
            Point p = trackTimeBg.PointToClient(Cursor.Position);
            UpdateTimeFromMouse(p.X);
        }

        private void UpdateTimeFromMouse(int mouseX)
        {
            int trackWidth = trackTimeBg.Width;
            if (trackWidth <= 0) return;

            float ratio = Math.Max(0f, Math.Min(1f, (float)mouseX / trackWidth));
            int rawDays = TIME_MIN + (int)Math.Round(ratio * (TIME_MAX - TIME_MIN));
            SetTime(rawDays, true);
        }

        private void SetTime(int days, bool updateUI)
        {
            if (days < TIME_MIN) days = TIME_MIN;
            if (days > TIME_MAX) days = TIME_MAX;

            _currentTime = days;

            if (updateUI)
            {
                _isUpdatingText = true;
                txtTime.Text = _currentTime.ToString();
                _isUpdatingText = false;
                UpdateTimeSliderUI();
                UpdateCalculations();
            }
        }

        private void UpdateTimeSliderUI()
        {
            int trackWidth = trackTimeBg.Width;
            if (trackWidth <= 0) return;

            float ratio = (float)(_currentTime - TIME_MIN) / (TIME_MAX - TIME_MIN);
            int fillWidth = (int)(ratio * trackWidth);
            trackTimeFill.Width = Math.Max(0, fillWidth);

            int thumbX = trackTimeBg.Left + fillWidth - (thumbTime.Width / 2);
            thumbX = Math.Max(trackTimeBg.Left - (thumbTime.Width / 2), Math.Min(trackTimeBg.Right - (thumbTime.Width / 2), thumbX));
            thumbTime.Left = thumbX;
            thumbTime.BringToFront();
        }

        #endregion

        #region AUTO CALCULATION

        /// <summary>
        /// Tính toán tự động số nợ phải trả, tiền lãi và cập nhật bảng tóm tắt thời gian thực.
        /// </summary>
        private void UpdateCalculations()
        {
            var calc = _borrowingService.CalculateLoan(_currentVayAmount, _currentTime, DAILY_INTEREST_RATE);

            // Cập nhật bảng tóm tắt 3 cột (Bảng trống trước đây)
            lblSum1Value.Text = calc.FormattedSoTienVay;
            lblSum2Value.Text = calc.FormattedLaiSuat;
            lblSum3Value.Text = calc.FormattedNgayDaoHan;
            lblSumInterestDetail.Text = $"Tiền lãi & phí dự tính ({calc.ThoiGianVay} ngày): + {calc.FormattedTienLai}";

            // Cập nhật ô số nợ phải trả to nổi bật
            txtDebtAmount.Text = $"{calc.TongPhaiTra:N0}";
        }

        #endregion

        #region REGISTER LOAN & HISTORY

        private async Task HandleRegisterLoanAsync()
        {
            if (_selectedAccount == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản ngân hàng để nhận tiền giải ngân.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboAccount.Focus();
                return;
            }

            if (!chkAgree.Checked)
            {
                MessageBox.Show("Vui lòng đọc và tích chọn đồng ý cam kết xác minh thông tin tín dụng CIC để hoàn tất đăng ký.", "Xác nhận cam kết", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chkAgree.Focus();
                return;
            }

            var calc = _borrowingService.CalculateLoan(_currentVayAmount, _currentTime, DAILY_INTEREST_RATE);

            DialogResult confirm = MessageBox.Show(
                $"XÁC NHẬN ĐĂNG KÝ KHOẢN VAY:\n\n" +
                $"• Khoản vay: {calc.FormattedSoTienVay}\n" +
                $"• Thời hạn vay: {calc.ThoiGianVay} ngày (Đáo hạn: {calc.FormattedNgayDaoHan})\n" +
                $"• Lãi suất ưu đãi: {calc.FormattedLaiSuat} (Tiền lãi: {calc.FormattedTienLai})\n" +
                $"• Tổng nợ phải trả: {calc.FormattedTongPhaiTra}\n" +
                $"• Tài khoản thụ hưởng: {_selectedAccount.SoTaiKhoan} ({_selectedAccount.MaNganHang})\n\n" +
                $"Bạn có chắc chắn muốn gửi hồ sơ đăng ký vay vốn này không?",
                "Xác nhận hồ sơ vay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            btnRegister.Enabled = false;
            btnRegister.Text = "Đang xử lý hồ sơ...";

            var result = await _borrowingService.CreateLoanAsync(
                CurrentSession.MaKH,
                _selectedAccount.SoTaiKhoan,
                _currentVayAmount,
                _currentTime,
                DAILY_INTEREST_RATE,
                $"Vay nhanh trực tuyến kỳ hạn {_currentTime} ngày"
            );

            btnRegister.Enabled = true;
            btnRegister.Text = "Đăng ký ngay";

            if (result.Success)
            {
                DialogResult dialogRes = MessageBox.Show(
                    $"ĐĂNG KÝ HỒ SƠ THÀNH CÔNG!\n\n" +
                    $"• Mã hồ sơ: {result.MaKhoanVay}\n" +
                    $"• Số tiền: {_currentVayAmount:N0} VND\n" +
                    $"• Trạng thái: Chờ ban quản trị xét duyệt\n\n" +
                    $"Hệ thống sẽ tiến hành thẩm định và giải ngân trực tiếp vào tài khoản của bạn ngay khi được phê duyệt.\n\n" +
                    $"Bạn có muốn mở xem Lịch sử khoản vay của mình ngay bây giờ không?",
                    "Đăng ký thành công",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (dialogRes == DialogResult.Yes)
                {
                    await ShowHistoryInRightCardAsync();
                }
            }
            else
            {
                MessageBox.Show(result.Message, "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region EMBEDDED HISTORY VIEW (IN RIGHT CARD)

        private void SetCalcControlsVisible(bool visible)
        {
            pnlSummary.Visible = visible;
            pnlRedDot.Visible = visible;
            lblPayTitle.Visible = visible;
            lblPayDesc.Visible = visible;
            lblDebtTitle.Visible = visible;
            pnlDebtInputBox.Visible = visible;
            pnlDebtBox.Visible = visible;
            chkAgree.Visible = visible;
            lblAgreeText.Visible = visible;
        }

        private async Task ToggleHistoryViewAsync()
        {
            if (pnlLoanHistoryView.Visible)
            {
                CloseHistoryView();
            }
            else
            {
                await ShowHistoryInRightCardAsync();
            }
        }

        private async Task ShowHistoryInRightCardAsync()
        {
            SetCalcControlsVisible(false);
            pnlLoanHistoryView.Visible = true;
            pnlLoanHistoryView.BringToFront();
            btnHistory.Text = "📝 Đăng ký khoản vay";
            await LoadUserHistoryAsync();
        }

        private void CloseHistoryView()
        {
            pnlLoanHistoryView.Visible = false;
            SetCalcControlsVisible(true);
            btnHistory.Text = "📋 Lịch sử vay của tôi";
        }

        private void SetupHistoryGridView()
        {
            dgvHistory.AutoGenerateColumns = false;
            dgvHistory.Columns.Clear();
            dgvHistory.ScrollBars = ScrollBars.Vertical;

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHistMa",
                HeaderText = "Mã hồ sơ",
                DataPropertyName = "MaKhoanVay",
                Width = 90
            });

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHistNgay",
                HeaderText = "Ngày ĐK",
                DataPropertyName = "FormattedNgayDangKy",
                Width = 100
            });

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHistSoTien",
                HeaderText = "Số tiền vay",
                DataPropertyName = "FormattedSoTienVay",
                Width = 85,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHistKyHan",
                HeaderText = "Kỳ hạn",
                DataPropertyName = "FormattedTerm",
                Width = 60,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHistTongNo",
                HeaderText = "Tổng nợ",
                DataPropertyName = "FormattedTongPhaiTra",
                Width = 85,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHistTrangThai",
                HeaderText = "Trạng thái",
                DataPropertyName = "TrangThai",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 60,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvHistory.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvHistory.Rows.Count) return;
                if (dgvHistory.Columns[e.ColumnIndex].Name == "colHistTrangThai" && e.Value != null)
                {
                    string status = e.Value.ToString();
                    if (status == "Đã duyệt")
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(16, 185, 129);
                        e.CellStyle.Font = new Font(dgvHistory.Font, FontStyle.Bold);
                    }
                    else if (status == "Từ chối" || status == "Đã từ chối")
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                        e.CellStyle.Font = new Font(dgvHistory.Font, FontStyle.Bold);
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(217, 119, 6);
                        e.CellStyle.Font = new Font(dgvHistory.Font, FontStyle.Bold);
                    }
                }
            };

            dgvHistory.SelectionChanged += (s, e) =>
            {
                if (dgvHistory.SelectedRows.Count > 0)
                {
                    var loan = dgvHistory.SelectedRows[0].DataBoundItem as BorrowingLoan;
                    if (loan != null)
                    {
                        DisplayHistoryLoanDetail(loan);
                    }
                }
            };
        }

        private async Task LoadUserHistoryAsync()
        {
            var loans = await _borrowingService.GetUserLoansAsync(CurrentSession.MaKH);
            dgvHistory.DataSource = null;
            dgvHistory.DataSource = loans;

            if (loans != null && loans.Count > 0)
            {
                pnlHistDetailCard.Visible = true;
                dgvHistory.Rows[0].Selected = true;
                DisplayHistoryLoanDetail(loans[0]);
            }
            else
            {
                pnlHistDetailCard.Visible = false;
            }
        }

        private void DisplayHistoryLoanDetail(BorrowingLoan loan)
        {
            pnlHistDetailCard.Visible = true;
            lblHistSum1Value.Text = loan.FormattedSoTienVay;
            lblHistSum2Value.Text = $"{loan.LaiSuat:N2}%/ngày";
            lblHistSum3Value.Text = loan.FormattedNgayDaoHan;
            lblHistSumInterestDetail.Text = $"Tiền lãi, phí dự tính ({loan.ThoiGianVay} ngày): + {loan.FormattedTienLai}";
            txtHistDebtAmount.Text = $"{loan.TongPhaiTra:N0}";

            if (loan.TrangThai == "Đã duyệt")
            {
                pnlHistDebtBox.FillColor = Color.FromArgb(236, 253, 245);
                pnlHistDebtBox.BorderColor = Color.FromArgb(167, 243, 208);
                lblHistDebtHint.ForeColor = Color.FromArgb(6, 95, 70);
                lblHistDebtHint.Text = $"✔ Hồ sơ đã được duyệt & giải ngân vào STK: {loan.SoTaiKhoan}";
            }
            else if (loan.TrangThai == "Từ chối" || loan.TrangThai == "Đã từ chối")
            {
                pnlHistDebtBox.FillColor = Color.FromArgb(254, 242, 242);
                pnlHistDebtBox.BorderColor = Color.FromArgb(254, 202, 202);
                lblHistDebtHint.ForeColor = Color.FromArgb(153, 27, 27);
                lblHistDebtHint.Text = $"✖ Hồ sơ bị từ chối xét duyệt bởi ban quản trị.";
            }
            else
            {
                pnlHistDebtBox.FillColor = Color.FromArgb(255, 247, 237);
                pnlHistDebtBox.BorderColor = Color.FromArgb(254, 215, 170);
                lblHistDebtHint.ForeColor = Color.FromArgb(180, 83, 9);
                lblHistDebtHint.Text = $"⚠  Số nợ được tính tự động từ khoản vay + lãi suất × thời gian vay";
            }
        }

        #endregion
    }
}
