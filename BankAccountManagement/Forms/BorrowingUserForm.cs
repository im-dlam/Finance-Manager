using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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

            pnlLeftCard.Resize += (s, e) => LayoutLeftCard();
            pnlRightCard.Resize += (s, e) => LayoutRightCard();
            pnlLoanHistoryView.Resize += (s, e) => LayoutHistoryView();
            this.Resize += (s, e) => {
                LayoutLeftCard();
                LayoutRightCard();
            };

            SetupHistoryGridView();
            WireSliderEvents();
            WireTextEvents();

            this.Load += async (s, e) => await InitializeDataAsync();
            this.lblAgreeText.Click += (s, e) => { chkAgree.Checked = !chkAgree.Checked; };
            this.btnRegister.Click += async (s, e) => await HandleRegisterLoanAsync();
            this.btnHistory.Click += async (s, e) => await ToggleHistoryViewAsync();
            this.btnPayLoan.Click += async (s, e) => await HandlePayLoanAsync();
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
            LayoutLeftCard();
            LayoutRightCard();
            UpdateCalculations();
            UpdateVaySliderUI();
            UpdateTimeSliderUI();
            thumbVay.IsCircle = true;
            thumbVay.BorderRadius = 12;
            thumbTime.IsCircle = true;
            thumbTime.BorderRadius = 12;
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
            LayoutHistoryView();
            await LoadUserHistoryAsync();
        }

        private void CloseHistoryView()
        {
            pnlLoanHistoryView.Visible = false;
            SetCalcControlsVisible(true);
            btnHistory.Text = "📋 Lịch sử vay của tôi";
            LayoutRightCard();
        }

        private void SetupHistoryGridView()
        {
            try
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                    null, dgvHistory, new object[] { true });
            }
            catch { }

            dgvHistory.AutoGenerateColumns = false;
            dgvHistory.Columns.Clear();
            dgvHistory.ScrollBars = ScrollBars.Vertical;
            dgvHistory.BorderStyle = BorderStyle.None;
            dgvHistory.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvHistory.GridColor = Color.FromArgb(241, 245, 249);
            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.MultiSelect = false;
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AllowUserToDeleteRows = false;
            dgvHistory.AllowUserToResizeRows = false;
            dgvHistory.AllowUserToResizeColumns = false;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.RowTemplate.Height = 32;

            // Tiêu đề cột (Header): Slate-50 sang trọng, font Semibold, chiều cao thoáng đãng
            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvHistory.ColumnHeadersHeight = 32;
            dgvHistory.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(248, 250, 252),
                ForeColor = Color.FromArgb(71, 85, 105),
                Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold),
                SelectionBackColor = Color.FromArgb(248, 250, 252),
                SelectionForeColor = Color.FromArgb(71, 85, 105),
                Padding = new Padding(2, 0, 2, 0)
            };

            // Dòng thường & Khi chọn: Sử dụng nền xám nhẹ Slate-100 thanh lịch, không dùng màu xanh đậm cũ
            dgvHistory.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(15, 23, 42),
                Font = new Font("Segoe UI", 8.25F),
                SelectionBackColor = Color.FromArgb(241, 245, 249),
                SelectionForeColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(2, 0, 2, 0)
            };

            dgvHistory.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(250, 252, 254),
                ForeColor = Color.FromArgb(15, 23, 42),
                Font = new Font("Segoe UI", 8.25F),
                SelectionBackColor = Color.FromArgb(241, 245, 249),
                SelectionForeColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(2, 0, 2, 0)
            };

            // 1. Mã hồ sơ
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHistMa",
                HeaderText = "Mã hồ sơ",
                DataPropertyName = "MaKhoanVay",
                FillWeight = 16,
                MinimumWidth = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI Semibold", 8F),
                    ForeColor = Color.FromArgb(51, 65, 85),
                    SelectionBackColor = Color.FromArgb(241, 245, 249),
                    SelectionForeColor = Color.FromArgb(15, 23, 42)
                }
            });

            // 2. Ngày vay
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHistNgay",
                HeaderText = "Ngày vay",
                DataPropertyName = "FormattedRegisterDate",
                FillWeight = 14,
                MinimumWidth = 70,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(100, 116, 139),
                    SelectionBackColor = Color.FromArgb(241, 245, 249),
                    SelectionForeColor = Color.FromArgb(15, 23, 42)
                }
            });

            // 3. Kỳ hạn
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHistKyHan",
                HeaderText = "Kỳ hạn",
                DataPropertyName = "FormattedTerm",
                FillWeight = 12,
                MinimumWidth = 50,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(100, 116, 139),
                    SelectionBackColor = Color.FromArgb(241, 245, 249),
                    SelectionForeColor = Color.FromArgb(15, 23, 42)
                }
            });

            // 4. Số tiền vay
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHistSoTien",
                HeaderText = "Tiền vay",
                DataPropertyName = "FormattedSoTienVay",
                FillWeight = 18,
                MinimumWidth = 75,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI Semibold", 8.25F),
                    ForeColor = Color.FromArgb(15, 23, 42),
                    SelectionBackColor = Color.FromArgb(241, 245, 249),
                    SelectionForeColor = Color.FromArgb(15, 23, 42)
                }
            });

            // 5. Tổng nợ
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHistTongNo",
                HeaderText = "Tổng nợ",
                DataPropertyName = "FormattedTongPhaiTra",
                FillWeight = 20,
                MinimumWidth = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI Bold", 8.25F),
                    ForeColor = Color.FromArgb(190, 24, 93),
                    SelectionBackColor = Color.FromArgb(241, 245, 249),
                    SelectionForeColor = Color.FromArgb(190, 24, 93)
                }
            });

            // 6. Trạng thái
            dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colHistTrangThai",
                HeaderText = "Trạng thái",
                DataPropertyName = "TrangThai",
                FillWeight = 20,
                MinimumWidth = 90,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    SelectionBackColor = Color.FromArgb(241, 245, 249)
                }
            });

            // Vẽ giao diện hiện đại: Thanh định vị hàng đang chọn và Pill Badge trạng thái
            dgvHistory.CellPainting += DgvHistory_CellPainting;

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

        private void DgvHistory_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            bool isSelected = (e.State & DataGridViewElementStates.Selected) != 0;

            // Xử lý vẽ cột Trạng thái dạng Pill Badge hiện đại
            if (e.ColumnIndex >= 0 && dgvHistory.Columns[e.ColumnIndex].Name == "colHistTrangThai")
            {
                e.Handled = true;

                // Nền hàng (màu thường hoặc màu khi được chọn)
                Color rowBg = isSelected
                    ? Color.FromArgb(241, 245, 249)
                    : (e.RowIndex % 2 == 0 ? Color.White : Color.FromArgb(250, 252, 254));

                using (var bgBrush = new SolidBrush(rowBg))
                {
                    e.Graphics.FillRectangle(bgBrush, e.CellBounds);
                }

                // Đường kẻ ngang mỏng phân cách hàng
                using (var linePen = new Pen(Color.FromArgb(241, 245, 249)))
                {
                    e.Graphics.DrawLine(linePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
                }

                if (e.Value != null)
                {
                    string status = e.Value.ToString();
                    Color pillBg, pillBorder, pillText;
                    string pillLabel;

                    if (status == "Đã duyệt")
                    {
                        pillBg = Color.FromArgb(236, 253, 245);
                        pillBorder = Color.FromArgb(167, 243, 208);
                        pillText = Color.FromArgb(22, 163, 74);
                        pillLabel = "✔ Đã duyệt";
                    }
                    else if (status == "Đã tất toán")
                    {
                        pillBg = Color.FromArgb(239, 246, 255);
                        pillBorder = Color.FromArgb(191, 219, 254);
                        pillText = Color.FromArgb(37, 99, 235);
                        pillLabel = "✔ Đã tất toán";
                    }
                    else if (status == "Từ chối" || status == "Đã từ chối")
                    {
                        pillBg = Color.FromArgb(254, 242, 242);
                        pillBorder = Color.FromArgb(254, 202, 202);
                        pillText = Color.FromArgb(220, 38, 38);
                        pillLabel = "✖ Từ chối";
                    }
                    else
                    {
                        pillBg = Color.FromArgb(255, 251, 235);
                        pillBorder = Color.FromArgb(254, 240, 138);
                        pillText = Color.FromArgb(217, 119, 6);
                        pillLabel = "⏳ Chờ duyệt";
                    }

                    using (var pillFont = new Font("Segoe UI", 7.5F, FontStyle.Bold))
                    {
                        var measured = TextRenderer.MeasureText(pillLabel, pillFont);
                        int pillH = 20;
                        int pillW = Math.Min(e.CellBounds.Width - 6, Math.Max(74, measured.Width + 8));
                        int pillX = e.CellBounds.X + (e.CellBounds.Width - pillW) / 2;
                        int pillY = e.CellBounds.Y + (e.CellBounds.Height - pillH) / 2;

                        var pillRect = new Rectangle(pillX, pillY, pillW, pillH);

                        var oldSmoothing = e.Graphics.SmoothingMode;
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                        using (var path = CreatePillPath(pillRect, 8))
                        {
                            using (var fillBrush = new SolidBrush(pillBg))
                                e.Graphics.FillPath(fillBrush, path);
                            using (var borderPen = new Pen(pillBorder, 1))
                                e.Graphics.DrawPath(borderPen, path);
                        }

                        e.Graphics.SmoothingMode = oldSmoothing;

                        TextRenderer.DrawText(e.Graphics, pillLabel, pillFont, pillRect, pillText,
                            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine);
                    }
                }
            }
            // Nếu là cột đầu tiên và hàng được chọn, vẽ thanh định vị hồng đậm ở mép trái
            else if (e.ColumnIndex == 0 && isSelected)
            {
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
                using (var indicatorBrush = new SolidBrush(Color.FromArgb(190, 24, 93)))
                {
                    e.Graphics.FillRectangle(indicatorBrush, e.CellBounds.Left, e.CellBounds.Top, 3, e.CellBounds.Height);
                }
                e.Handled = true;
            }
        }

        private static GraphicsPath CreatePillPath(Rectangle rect, int radius)
        {
            int d = Math.Min(radius * 2, Math.Min(rect.Width, rect.Height));
            var path = new GraphicsPath();
            if (d <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        private async Task LoadUserHistoryAsync()
        {
            var loans = await _borrowingService.GetUserLoansAsync(CurrentSession.MaKH);
            dgvHistory.DataSource = null;
            dgvHistory.DataSource = loans;

            if (loans != null && loans.Count > 0 && dgvHistory.Rows.Count > 0)
            {
                pnlHistDetailCard.Visible = true;
                dgvHistory.Rows[0].Selected = true;
                DisplayHistoryLoanDetail(loans[0]);
            }
            else
            {
                pnlHistDetailCard.Visible = false;
                btnPayLoan.Visible = false;
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
                lblHistDebtHint.Text = $"✔ Hồ sơ đã được giải ngân vào STK: {loan.SoTaiKhoan}. Vui lòng thanh toán trước ngày đáo hạn.";

                btnPayLoan.Visible = true;
                btnPayLoan.Enabled = true;
                btnPayLoan.Text = "💳 Thanh toán khoản vay";
                btnPayLoan.NormalColor = Color.FromArgb(22, 163, 74);
                btnPayLoan.HoverColor = Color.FromArgb(21, 128, 61);
            }
            else if (loan.TrangThai == "Đã tất toán")
            {
                pnlHistDebtBox.FillColor = Color.FromArgb(240, 253, 244);
                pnlHistDebtBox.BorderColor = Color.FromArgb(134, 239, 172);
                lblHistDebtHint.ForeColor = Color.FromArgb(21, 128, 61);
                lblHistDebtHint.Text = $"✔ Khoản vay đã được tất toán thành công đầy đủ gốc và lãi.";

                btnPayLoan.Visible = true;
                btnPayLoan.Enabled = false;
                btnPayLoan.Text = "✔ Đã tất toán";
                btnPayLoan.NormalColor = Color.FromArgb(148, 163, 184);
                btnPayLoan.HoverColor = Color.FromArgb(148, 163, 184);
            }
            else if (loan.TrangThai == "Từ chối" || loan.TrangThai == "Đã từ chối")
            {
                pnlHistDebtBox.FillColor = Color.FromArgb(254, 242, 242);
                pnlHistDebtBox.BorderColor = Color.FromArgb(254, 202, 202);
                lblHistDebtHint.ForeColor = Color.FromArgb(153, 27, 27);
                lblHistDebtHint.Text = $"✖ Hồ sơ bị từ chối xét duyệt bởi ban quản trị.";
                btnPayLoan.Visible = false;
            }
            else
            {
                pnlHistDebtBox.FillColor = Color.FromArgb(255, 247, 237);
                pnlHistDebtBox.BorderColor = Color.FromArgb(254, 215, 170);
                lblHistDebtHint.ForeColor = Color.FromArgb(180, 83, 9);
                lblHistDebtHint.Text = $"⏳ Hồ sơ đang chờ ban quản trị thẩm định và xét duyệt giải ngân.";
                btnPayLoan.Visible = false;
            }
        }

        private async Task HandlePayLoanAsync()
        {
            if (dgvHistory.SelectedRows.Count == 0) return;
            var loan = dgvHistory.SelectedRows[0].DataBoundItem as BorrowingLoan;
            if (loan == null) return;

            if (loan.TrangThai != "Đã duyệt")
            {
                MessageBox.Show("Chỉ có thể thanh toán khoản vay ở trạng thái 'Đã duyệt' (đang nợ).",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"XÁC NHẬN THANH TOÁN TẤT TOÁN KHOẢN VAY:\n\n" +
                $"• Mã khoản vay: {loan.MaKhoanVay}\n" +
                $"• Số tiền vay gốc: {loan.FormattedSoTienVay}\n" +
                $"• Tiền lãi ({loan.ThoiGianVay} ngày): {loan.FormattedTienLai}\n" +
                $"• Tổng nợ cần thanh toán: {loan.FormattedTongPhaiTra}\n" +
                $"• Tài khoản trích nợ: {loan.SoTaiKhoan}\n\n" +
                $"Hệ thống sẽ tự động trừ số dư tài khoản trên để hoàn tất thanh toán khoản vay này.\n" +
                $"Bạn có chắc chắn muốn tiến hành thanh toán ngay bây giờ không?",
                "Xác nhận thanh toán nợ",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            btnPayLoan.Enabled = false;
            btnPayLoan.Text = "Đang thanh toán...";

            var result = await _borrowingService.PayLoanAsync(loan.MaKhoanVay, CurrentSession.TenDangNhap);

            btnPayLoan.Enabled = true;

            if (result.Success)
            {
                MessageBox.Show(result.Message, "Thanh toán thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadUserAccountsAsync();
                await LoadUserHistoryAsync();
            }
            else
            {
                btnPayLoan.Text = "💳 Thanh toán khoản vay";
                MessageBox.Show(result.Message, "Không thể thanh toán", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region RESPONSIVE LAYOUT SCALING
        private void LayoutLeftCard()
        {
            if (pnlLeftCard == null || pnlVayInputBox == null || trackVayBg == null) return;
            int cardWidth = pnlLeftCard.ClientSize.Width;
            if (cardWidth <= 100) return;

            const int leftMargin = 32;
            int targetWidth = Math.Max(320, cardWidth - (leftMargin * 2));

            // 1. Khoản vay
            pnlVayInputBox.Width = targetWidth;
            lblVayUnit.Left = Math.Max(10, pnlVayInputBox.ClientSize.Width - lblVayUnit.Width - 12);
            txtVayAmount.Width = Math.Max(50, lblVayUnit.Left - 16);

            trackVayBg.Width = targetWidth;
            lblVayMax.Left = Math.Max(trackVayBg.Left + 50, trackVayBg.Right - lblVayMax.Width);

            // 2. Thời gian vay
            pnlTimeInputBox.Width = targetWidth;
            lblTimeUnit.Left = Math.Max(10, pnlTimeInputBox.ClientSize.Width - lblTimeUnit.Width - 12);
            txtTime.Width = Math.Max(50, lblTimeUnit.Left - 16);

            trackTimeBg.Width = targetWidth;
            lblTimeMax.Left = Math.Max(trackTimeBg.Left + 50, trackTimeBg.Right - lblTimeMax.Width);

            // Cập nhật lại vị trí slider & fill theo chiều rộng mới
            UpdateVaySliderUI();
            UpdateTimeSliderUI();
        }

        private void LayoutRightCard()
        {
            if (pnlRightCard == null || pnlSummary == null) return;
            int cardWidth = pnlRightCard.ClientSize.Width;
            if (cardWidth <= 100) return;

            const int leftMargin = 24;
            int rightWidth = Math.Max(320, cardWidth - (leftMargin * 2));

            // Bảng tóm tắt khoản vay
            pnlSummary.Width = rightWidth;
            int colWidth = Math.Max(80, (rightWidth - 32) / 3);
            lblSum1Value.Left = 16;
            lblSum1Title.Left = 16;
            lblSum2Value.Left = 16 + colWidth;
            lblSum2Title.Left = 16 + colWidth;
            lblSum3Value.Left = 16 + colWidth * 2;
            lblSum3Title.Left = 16 + colWidth * 2;
            lblSumInterestDetail.Left = 16;

            // Ghi sổ nợ phải trả
            lblPayDesc.Width = rightWidth;
            pnlDebtInputBox.Width = rightWidth;
            lblDebtUnit.Left = Math.Max(10, pnlDebtInputBox.ClientSize.Width - lblDebtUnit.Width - 16);
            txtDebtAmount.Width = Math.Max(50, lblDebtUnit.Left - 16);

            pnlDebtBox.Width = rightWidth;
            lblDebtHint.Width = rightWidth;
            lblAgreeText.Width = Math.Max(100, rightWidth - 30);

            LayoutHistoryView();
        }

        private void LayoutHistoryView()
        {
            if (pnlLoanHistoryView == null || pnlHistGridWrapper == null || pnlHistDetailCard == null) return;
            int cardWidth = pnlLoanHistoryView.ClientSize.Width;
            if (cardWidth <= 100) cardWidth = pnlRightCard?.ClientSize.Width ?? 0;
            if (cardWidth <= 100) return;

            int cardHeight = pnlLoanHistoryView.ClientSize.Height;
            if (cardHeight <= 100) cardHeight = pnlRightCard?.ClientSize.Height ?? 0;

            const int leftMargin = 20;
            int histWidth = Math.Max(320, cardWidth - (leftMargin * 2));

            pnlHistGridWrapper.Width = histWidth;
            pnlHistDetailCard.Width = histWidth;

            // Điều chỉnh chiều cao bảng lịch sử khi phóng to màn hình
            if (cardHeight > 560)
            {
                int extraHeight = cardHeight - 560;
                pnlHistGridWrapper.Height = Math.Min(220, 118 + extraHeight / 2);
                pnlHistDetailCard.Top = pnlHistGridWrapper.Bottom + 10;
            }
            else
            {
                pnlHistGridWrapper.Height = 118;
                pnlHistDetailCard.Top = 182;
            }

            // Bảng tóm tắt chi tiết lịch sử
            pnlHistSummary.Width = histWidth;
            int hColWidth = Math.Max(80, (histWidth - 32) / 3);
            lblHistSum1Value.Left = 16;
            lblHistSum1Title.Left = 16;
            lblHistSum2Value.Left = 16 + hColWidth;
            lblHistSum2Title.Left = 16 + hColWidth;
            lblHistSum3Value.Left = 16 + hColWidth * 2;
            lblHistSum3Title.Left = 16 + hColWidth * 2;
            lblHistSumInterestDetail.Left = 16;

            lblHistPayDesc.Width = histWidth;
            pnlHistDebtInputBox.Width = histWidth;
            lblHistDebtUnit.Left = Math.Max(10, pnlHistDebtInputBox.ClientSize.Width - lblHistDebtUnit.Width - 16);
            txtHistDebtAmount.Width = Math.Max(50, lblHistDebtUnit.Left - 16);

            pnlHistDebtBox.Width = histWidth;
            lblHistDebtHint.Width = histWidth;

            // Căn nút sang phải theo chiều rộng mới
            if (btnCloseHistory != null && btnPayLoan != null)
            {
                btnCloseHistory.Left = Math.Max(leftMargin, leftMargin + histWidth - btnCloseHistory.Width);
                btnPayLoan.Left = Math.Max(leftMargin, btnCloseHistory.Left - btnPayLoan.Width - 12);
                btnCloseHistory.Top = pnlHistDetailCard.Bottom + 12;
                btnPayLoan.Top = pnlHistDetailCard.Bottom + 12;
            }
        }
        #endregion
    }
}
