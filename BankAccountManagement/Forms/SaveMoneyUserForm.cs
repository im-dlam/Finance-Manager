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
    public partial class SaveMoneyUserForm : Form
    {
        private readonly SaveMoneyService _service;
        private List<TaiKhoan> _userAccounts = new List<TaiKhoan>();
        private TaiKhoan _selectedAccount = null;
        private List<SoTietKiem> _userSavingsList = new List<SoTietKiem>();
        private SoTietKiem _currentStep3Savings = null;

        private int _currentStep = 1;
        private decimal _currentAmount = 10000000m;
        private int _selectedTerm = 6;
        private bool _isFormattingText = false;

        public SaveMoneyUserForm()
        {
            InitializeComponent();
            _service = SaveMoneyService.Instance;

            SetupHistoryGrid();
            WireEvents();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadInitialDataAsync();
        }

        #region INITIAL DATA LOADING

        private async Task LoadInitialDataAsync()
        {
            string maKH = CurrentSession.MaKH;
            if (string.IsNullOrWhiteSpace(maKH))
            {
                MessageBox.Show("Vui lòng cập nhật hồ sơ khách hàng trước khi sử dụng tính năng gửi tiết kiệm.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 1. Tải danh sách kỳ hạn vào ComboBox ở Bước 1 và Bước 2
            var packages = _service.GetPackages();
            cboStep1Term.DataSource = null;
            cboStep1Term.DisplayMember = "TenGoi";
            cboStep1Term.ValueMember = "KyHanThang";
            cboStep1Term.DataSource = new List<SavingsPackage>(packages);

            cboStep2Term.DataSource = null;
            cboStep2Term.DisplayMember = "TenGoi";
            cboStep2Term.ValueMember = "KyHanThang";
            cboStep2Term.DataSource = new List<SavingsPackage>(packages);

            // Mặc định chọn gói 6 tháng (như Ảnh 1)
            SetSelectedTerm(6);

            // 2. Tải danh sách tài khoản thanh toán nguồn
            await LoadUserAccountsAsync();

            // 3. Tải danh sách các sổ tiết kiệm của người dùng
            await LoadUserSavingsHistoryAsync();

            // 4. Mặc định vào Bước 1
            ShowStep(1);
            RecalculateStep1();
        }

        private async Task LoadUserAccountsAsync()
        {
            string maKH = CurrentSession.MaKH;
            _userAccounts = await _service.GetUserPaymentAccountsAsync(maKH);

            cboStep1Account.Items.Clear();
            if (_userAccounts.Count == 0)
            {
                cboStep1Account.Items.Add("Chưa có tài khoản thanh toán hợp lệ");
                cboStep1Account.SelectedIndex = 0;
                lblStep1AvailBalance.Text = "0 đ";
                lblStep2AvailValue.Text = "0 đ";
                btnStep1Next.Enabled = false;
                return;
            }

            foreach (var acc in _userAccounts)
            {
                cboStep1Account.Items.Add($"{acc.SoTaiKhoan} ({acc.MaNganHang} - Dư: {acc.SoDuHienTai:N0} đ)");
            }

            cboStep1Account.SelectedIndex = 0;
            _selectedAccount = _userAccounts[0];
            btnStep1Next.Enabled = true;

            UpdateAvailableBalanceLabel();
        }

        private void UpdateAvailableBalanceLabel()
        {
            if (_selectedAccount != null)
            {
                lblStep1AvailBalance.Text = $"{_selectedAccount.SoDuHienTai:N0} đ";
                lblStep2AvailValue.Text = $"{_selectedAccount.SoDuHienTai:N0} đ";
            }
        }

        private async Task LoadUserSavingsHistoryAsync()
        {
            string maKH = CurrentSession.MaKH;
            _userSavingsList = await _service.GetUserSavingsListAsync(maKH);

            dgvUserSavings.DataSource = null;
            dgvUserSavings.DataSource = _userSavingsList;

            // Thống kê nhanh trên banner
            decimal totalBalance = 0;
            decimal totalInterest = 0;
            int activeCount = 0;

            foreach (var s in _userSavingsList)
            {
                if (s.TrangThai == "Đang gửi")
                {
                    totalBalance += s.SoTienGui;
                    totalInterest += s.TienLaiDuTinh;
                    activeCount++;
                }
            }

            lblSumBalance.Text = $"{totalBalance:N0} đ";
            lblSumInterest.Text = $"{totalInterest:N0} đ";
            lblSumCount.Text = $"{activeCount} sổ";
        }

        #endregion

        #region UI SETUP & STEP MANAGEMENT

        private void WireEvents()
        {
            // Chuyển tài khoản nguồn
            cboStep1Account.SelectedIndexChanged += (s, e) =>
            {
                if (cboStep1Account.SelectedIndex >= 0 && cboStep1Account.SelectedIndex < _userAccounts.Count)
                {
                    _selectedAccount = _userAccounts[cboStep1Account.SelectedIndex];
                    UpdateAvailableBalanceLabel();
                    RecalculateStep1();
                }
            };

            // Thay đổi số tiền nhập ở Bước 1
            txtStep1Amount.Leave += (s, e) =>
            {
                if (_isFormattingText) return;
                string clean = txtStep1Amount.Text.Replace(".", "").Replace(",", "").Trim();
                if (decimal.TryParse(clean, out decimal val) && val >= 0)
                {
                    _currentAmount = val;
                }
                FormatAmountText();
                RecalculateStep1();
            };

            txtStep1Amount.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    pnlAmountInput.Focus();
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };

            // Thay đổi kỳ hạn
            cboStep1Term.SelectedIndexChanged += (s, e) =>
            {
                if (cboStep1Term.SelectedValue is int term)
                {
                    _selectedTerm = term;
                    RecalculateStep1();
                }
            };

            cboStep2Term.SelectedIndexChanged += (s, e) =>
            {
                if (cboStep2Term.SelectedValue is int term)
                {
                    _selectedTerm = term;
                    RecalculateStep2();
                }
            };

            // Nút điều hướng các bước
            btnStep1Next.Click += (s, e) => GoToStep2();
            btnPhoneBack.Click += (s, e) => HandleBackButton();

            btnStep2Confirm.UseMnemonic = false;
            btnStep2Confirm.Text = "Xác nhận mở sổ";
            btnStep2Confirm.Click += async (s, e) => await HandleConfirmAndOpenAsync();

            btnStep3Close.Click += (s, e) =>
            {
                ShowStep(1);
                RecalculateStep1();
            };

            btnStep3Settle.Click += async (s, e) => await HandleSettleCurrentSavingsAsync();
            btnStep3DownloadReceipt.Click += (s, e) => ShowReceiptDialog(_currentStep3Savings);

            // Nút ở cột bên trái
            btnNewSavings.Click += (s, e) =>
            {
                ShowStep(1);
                txtStep1Amount.Text = "10.000.000";
                _currentAmount = 10000000m;
                SetSelectedTerm(6);
                RecalculateStep1();
            };

            // Vẽ thanh tiến trình Bước 2
            pnlStep2Progress.Paint += PnlStep2Progress_Paint;

            // Bấm chọn sổ trong bảng bên trái
            dgvUserSavings.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex < _userSavingsList.Count)
                {
                    var selected = _userSavingsList[e.RowIndex];
                    DisplaySavingsInStep3(selected);
                }
            };

            // Tự động căn giữa biểu tượng và co dãn các nút ở Bước 3 khi phóng to
            pnlStep3.Resize += (s, e) =>
            {
                pnlStep3CheckIcon.Left = (pnlStep3.ClientSize.Width - pnlStep3CheckIcon.Width) / 2;
            };

            pnlStep3Buttons.Resize += (s, e) =>
            {
                int half = (pnlStep3Buttons.ClientSize.Width - 12) / 2;
                if (half > 60)
                {
                    btnStep3Settle.Width = half;
                    btnStep3Close.Width = half;
                }
            };
        }

        private void SetSelectedTerm(int months)
        {
            var packages = _service.GetPackages();
            for (int i = 0; i < packages.Count; i++)
            {
                if (packages[i].KyHanThang == months)
                {
                    cboStep1Term.SelectedIndex = i;
                    cboStep2Term.SelectedIndex = i;
                    _selectedTerm = months;
                    break;
                }
            }
        }

        private void FormatAmountText()
        {
            _isFormattingText = true;
            txtStep1Amount.Text = $"{_currentAmount:N0}";
            _isFormattingText = false;
        }

        private void ShowStep(int step)
        {
            _currentStep = step;
            pnlStep1.Visible = (step == 1);
            pnlStep2.Visible = (step == 2);
            pnlStep3.Visible = (step == 3);

            if (step == 1)
            {
                lblPhoneNavTitle.Text = "Mở tài khoản tiết kiệm";
                lblPhoneNavSubtitle.Text = "Gửi trực tuyến lãi suất ưu đãi";
                btnPhoneBack.Visible = false;
            }
            else if (step == 2)
            {
                lblPhoneNavTitle.Text = "MỞ TÀI KHOẢN TIẾT KIỆM";
                lblPhoneNavSubtitle.Text = "Bước 2/3: Xem lại & Mở TKTK";
                btnPhoneBack.Visible = true;
                pnlStep2Progress.Invalidate();
            }
            else if (step == 3)
            {
                lblPhoneNavTitle.Text = "Mở Tài Khoản Tiết Kiệm";
                lblPhoneNavSubtitle.Text = "Bước 3/3: Chi tiết TKTK";
                btnPhoneBack.Visible = true;
                pnlStep3CheckIcon.Left = (pnlStep3.ClientSize.Width - pnlStep3CheckIcon.Width) / 2;
                int half = (pnlStep3Buttons.ClientSize.Width - 12) / 2;
                if (half > 60)
                {
                    btnStep3Settle.Width = half;
                    btnStep3Close.Width = half;
                }
            }
        }

        private void HandleBackButton()
        {
            if (_currentStep == 2)
            {
                ShowStep(1);
            }
            else if (_currentStep == 3)
            {
                ShowStep(1);
            }
        }

        private void RecalculateStep1()
        {
            var calc = _service.CalculateSavings(_currentAmount, _selectedTerm);
            lblStep1RateValue.Text = calc.FormattedLaiSuat;
            lblStep1EstInterestValue.Text = calc.FormattedTienLai;
        }

        private void GoToStep2()
        {
            if (_selectedAccount == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản thanh toán nguồn để trích tiền.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_currentAmount < 1000000m)
            {
                MessageBox.Show("Số tiền gửi tiết kiệm tối thiểu là 1.000.000 đ.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_selectedAccount.SoDuHienTai < _currentAmount)
            {
                MessageBox.Show($"Số dư khả dụng ({_selectedAccount.SoDuHienTai:N0} đ) không đủ để mở sổ tiết kiệm {_currentAmount:N0} đ.\n" +
                                "Vui lòng giảm số tiền gửi hoặc nạp thêm tiền vào tài khoản.",
                                "Số dư không đủ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Đồng bộ sang bước 2 (Ảnh 2)
            SetSelectedTerm(_selectedTerm);
            cboStep2Renewal.SelectedIndex = 0; // Tái tục gốc & lãi (Lãi gộp)
            RecalculateStep2();
            ShowStep(2);
        }

        private void RecalculateStep2()
        {
            var calc = _service.CalculateSavings(_currentAmount, _selectedTerm);

            lblStep2TotalAmount.Text = calc.FormattedTongTien;
            lblStep2PrincipalValue.Text = calc.FormattedSoTienGui;
            lblStep2RateValue.Text = calc.FormattedLaiSuat;
            lblStep2DueValue.Text = calc.FormattedNgayDaoHan;
            lblStep2AvailValue.Text = _selectedAccount != null ? $"{_selectedAccount.SoDuHienTai:N0} đ" : "0 đ";
        }

        private void PnlStep2Progress_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int w = pnlStep2Progress.Width;
            int y = pnlStep2Progress.Height / 2;

            int dot1X = w / 4;
            int dot2X = w / 2;
            int dot3X = (w * 3) / 4;

            // Đường nối 1 -> 2 (Đã xong: màu xanh ngọc)
            using (var p1 = new Pen(Color.FromArgb(16, 185, 129), 3))
            {
                g.DrawLine(p1, dot1X, y, dot2X, y);
            }

            // Đường nối 2 -> 3 (Chưa tới: màu xám đậm / xanh đậm)
            using (var p2 = new Pen(Color.FromArgb(51, 65, 85), 3))
            {
                g.DrawLine(p2, dot2X, y, dot3X, y);
            }

            // Chấm 1 (Hoàn thành - Xanh lá)
            using (var b1 = new SolidBrush(Color.FromArgb(16, 185, 129)))
            {
                g.FillEllipse(b1, dot1X - 6, y - 6, 12, 12);
            }

            // Chấm 2 (Đang ở đây - Vòng tròn phát sáng xanh + chấm trắng giữa)
            using (var haloBrush = new SolidBrush(Color.FromArgb(50, 16, 185, 129)))
            {
                g.FillEllipse(haloBrush, dot2X - 10, y - 10, 20, 20);
            }
            using (var borderPen = new Pen(Color.FromArgb(16, 185, 129), 2))
            {
                g.DrawEllipse(borderPen, dot2X - 8, y - 8, 16, 16);
            }
            using (var b2 = new SolidBrush(Color.White))
            {
                g.FillEllipse(b2, dot2X - 4, y - 4, 8, 8);
            }

            // Chấm 3 (Chưa hoàn thành - Xám)
            using (var b3 = new SolidBrush(Color.FromArgb(100, 116, 139)))
            {
                g.FillEllipse(b3, dot3X - 5, y - 5, 10, 10);
            }
        }

        #endregion

        #region CONFIRM & SETTLEMENT LOGIC

        private async Task HandleConfirmAndOpenAsync()
        {
            if (!chkStep2Agree.Checked)
            {
                MessageBox.Show("Vui lòng đồng ý với Điều khoản & Điều kiện gửi tiết kiệm trực tuyến để tiếp tục.",
                    "Xác nhận điều khoản", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            btnStep2Confirm.Enabled = false;
            btnStep2Confirm.Text = "Đang xử lý...";

            string hinhThuc = cboStep2Renewal.SelectedItem?.ToString() ?? "Tự động tái tục gốc";

            var result = await _service.OpenSavingsAccountAsync(
                CurrentSession.MaKH,
                _selectedAccount.SoTaiKhoan,
                _currentAmount,
                _selectedTerm,
                hinhThuc,
                CurrentSession.TenDangNhap);

            btnStep2Confirm.Enabled = true;
            btnStep2Confirm.Text = "Xác nhận mở sổ";

            if (result.Success && result.Savings != null)
            {
                // Hiển thị sang Bước 3 (Ảnh 3)
                DisplaySavingsInStep3(result.Savings);

                // Tải lại dữ liệu tài khoản và lịch sử
                await LoadUserAccountsAsync();
                await LoadUserSavingsHistoryAsync();
            }
            else
            {
                MessageBox.Show(result.Message, "Không thể mở tài khoản", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DisplaySavingsInStep3(SoTietKiem savings)
        {
            _currentStep3Savings = savings;

            lblStep3CodeValue.Text = savings.MaSoTKTK;
            lblStep3PrincipalValue.Text = savings.FormattedSoTienGui;
            lblStep3TermValue.Text = savings.FormattedKyHan;
            lblStep3RateValue.Text = savings.FormattedLaiSuat;
            lblStep3RenewalValue.Text = savings.HinhThucDaoHan;
            lblStep3OpenDateValue.Text = savings.FormattedNgayMo;
            lblStep3DueDateValue.Text = savings.FormattedNgayDaoHan;
            lblStep3AccountValue.Text = savings.FormattedMaskedSTK;

            if (savings.TrangThai == "Đã tất toán")
            {
                lblStep3SuccessTitle.Text = "Tài khoản tiết kiệm đã tất toán";
                lblStep3SuccessSub.Text = $"Đã hoàn trả vốn và lãi vào tài khoản nguồn lúc {savings.FormattedNgayTatToan}";
                pnlStep3CheckIcon.FillColor = Color.FromArgb(59, 130, 246);
                btnStep3Settle.Enabled = false;
                btnStep3Settle.Text = "✔ Đã tất toán";
            }
            else
            {
                lblStep3SuccessTitle.Text = "Tài khoản tiết kiệm đã mở thành công";
                lblStep3SuccessSub.Text = "Hệ thống đã trích tiền thành công từ tài khoản nguồn";
                pnlStep3CheckIcon.FillColor = Color.FromArgb(34, 197, 94);
                btnStep3Settle.Enabled = true;
                btnStep3Settle.Text = "Tất toán";
            }

            ShowStep(3);
        }

        private async Task HandleSettleCurrentSavingsAsync()
        {
            if (_currentStep3Savings == null) return;

            if (_currentStep3Savings.TrangThai == "Đã tất toán")
            {
                MessageBox.Show("Tài khoản tiết kiệm này đã được tất toán thành công trước đó.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DateTime today = DateTime.Today;
            bool isMatured = today >= _currentStep3Savings.NgayDaoHan.Date;

            string confirmMsg = isMatured
                ? $"XÁC NHẬN TẤT TOÁN ĐÚNG HẠN:\n\n" +
                  $"• Mã sổ TKTK: {_currentStep3Savings.MaSoTKTK}\n" +
                  $"• Tiền gốc: {_currentStep3Savings.FormattedSoTienGui}\n" +
                  $"• Tiền lãi dự tính ({_currentStep3Savings.FormattedKyHan}): {_currentStep3Savings.FormattedTienLaiDuTinh}\n" +
                  $"• Tổng nhận về tài khoản: {_currentStep3Savings.FormattedTongTienDaoHan}\n\n" +
                  $"Hệ thống sẽ hoàn trả đầy đủ gốc và lãi vào tài khoản {_currentStep3Savings.SoTaiKhoanTrich}.\n" +
                  $"Bạn có chắc chắn muốn tất toán sổ này không?"
                : $"CẢNH BÁO TẤT TOÁN TRƯỚC HẠN:\n\n" +
                  $"• Sổ TKTK: {_currentStep3Savings.MaSoTKTK}\n" +
                  $"• Ngày đáo hạn quy định: {_currentStep3Savings.FormattedNgayDaoHan} (còn lại {(_currentStep3Savings.NgayDaoHan.Date - today).Days} ngày)\n" +
                  $"• Do rút trước hạn, bạn sẽ hưởng lãi suất không kỳ hạn (0.20%/năm) theo số ngày thực gửi.\n\n" +
                  $"Bạn có chắc chắn muốn tiến hành tất toán trước hạn không?";

            var confirm = MessageBox.Show(confirmMsg, "Xác nhận tất toán TKTK", MessageBoxButtons.YesNo,
                isMatured ? MessageBoxIcon.Question : MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            btnStep3Settle.Enabled = false;
            btnStep3Settle.Text = "Đang xử lý...";

            var result = await _service.SettleSavingsAccountAsync(_currentStep3Savings.MaSoTKTK, CurrentSession.TenDangNhap);

            btnStep3Settle.Enabled = true;

            if (result.Success)
            {
                MessageBox.Show(result.Message, "Tất toán thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _currentStep3Savings.TrangThai = "Đã tất toán";
                _currentStep3Savings.NgayTatToan = DateTime.Now;
                _currentStep3Savings.TongTienThucNhan = result.RefundTotal;
                _currentStep3Savings.TienLaiThucNhan = result.RefundInterest;

                DisplaySavingsInStep3(_currentStep3Savings);

                await LoadUserAccountsAsync();
                await LoadUserSavingsHistoryAsync();
            }
            else
            {
                btnStep3Settle.Text = "Tất toán";
                MessageBox.Show(result.Message, "Không thể tất toán", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowReceiptDialog(SoTietKiem s)
        {
            if (s == null) return;

            string info =
                $"==============================================\n" +
                $"   NGÂN HÀNG KỸ THUẬT SỐ - GIẤY CHỨNG NHẬN TKTK   \n" +
                $"==============================================\n\n" +
                $"• MÃ SỔ TIẾT KIỆM:       {s.MaSoTKTK}\n" +
                $"• CHỦ SỔ TIẾT KIỆM:      {s.FormattedCustomer}\n" +
                $"• TÀI KHOẢN TRÍCH TIỀN:  {s.SoTaiKhoanTrich}\n" +
                $"• SỐ TIỀN GỐC GỬI:       {s.FormattedSoTienGui}\n" +
                $"• KỲ HẠN GỬI:            {s.FormattedKyHan}\n" +
                $"• LÃI SUẤT ÁP DỤNG:      {s.FormattedLaiSuat}\n" +
                $"• HÌNH THỨC ĐÁO HẠN:     {s.HinhThucDaoHan}\n" +
                $"• NGÀY MỞ SỔ:            {s.FormattedNgayMo}\n" +
                $"• NGÀY ĐÁO HẠN DỰ KIẾN:  {s.FormattedNgayDaoHan}\n" +
                $"• TIỀN LÃI DỰ TÍNH:      {s.FormattedTienLaiDuTinh}\n" +
                $"• TỔNG NHẬN ĐÁO HẠN:     {s.FormattedTongTienDaoHan}\n" +
                $"• TRẠNG THÁI HIỆN TẠI:   {s.TrangThai}\n\n" +
                $"==============================================\n" +
                $"Biên lai điện tử có giá trị pháp lý tương đương sổ giấy.\n" +
                $"Mọi thắc mắc xin liên hệ tổng đài hỗ trợ 24/7.";

            MessageBox.Show(info, "Biên lai chứng nhận Sổ tiết kiệm điện tử", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region HISTORY GRID VIEW SETUP

        private void SetupHistoryGrid()
        {
            try
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                    null, dgvUserSavings, new object[] { true });
            }
            catch { }

            dgvUserSavings.AutoGenerateColumns = false;
            dgvUserSavings.Columns.Clear();
            dgvUserSavings.RowTemplate.Height = 36;
            dgvUserSavings.ColumnHeadersHeight = 34;

            dgvUserSavings.EnableHeadersVisualStyles = false;
            dgvUserSavings.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(248, 250, 252),
                ForeColor = Color.FromArgb(71, 85, 105),
                Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                SelectionBackColor = Color.FromArgb(248, 250, 252),
                SelectionForeColor = Color.FromArgb(71, 85, 105)
            };

            dgvUserSavings.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(15, 23, 42),
                Font = new Font("Segoe UI", 8.5F),
                SelectionBackColor = Color.FromArgb(241, 245, 249),
                SelectionForeColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(2, 0, 2, 0)
            };

            dgvUserSavings.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(250, 252, 254),
                ForeColor = Color.FromArgb(15, 23, 42),
                Font = new Font("Segoe UI", 8.5F),
                SelectionBackColor = Color.FromArgb(241, 245, 249),
                SelectionForeColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(2, 0, 2, 0)
            };

            // 1. Mã Sổ
            dgvUserSavings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaSo",
                HeaderText = "Mã TKTK",
                DataPropertyName = "MaSoTKTK",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI Semibold", 8.25F),
                    ForeColor = Color.FromArgb(30, 41, 59)
                }
            });

            // 2. Tiền gửi
            dgvUserSavings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSoTien",
                HeaderText = "Tiền gửi",
                DataPropertyName = "FormattedSoTienGui",
                Width = 105,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI Semibold", 8.5F),
                    ForeColor = Color.FromArgb(22, 163, 74)
                }
            });

            // 3. Kỳ hạn
            dgvUserSavings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colKyHan",
                HeaderText = "Kỳ hạn",
                DataPropertyName = "FormattedKyHan",
                Width = 70,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(100, 116, 139)
                }
            });

            // 4. Lãi suất
            dgvUserSavings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colLaiSuat",
                HeaderText = "Lãi suất",
                DataPropertyName = "FormattedLaiSuat",
                Width = 85,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(15, 23, 42)
                }
            });

            // 5. Tiền lãi dự tính
            dgvUserSavings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTienLai",
                HeaderText = "Lãi dự tính",
                DataPropertyName = "FormattedTienLaiDuTinh",
                Width = 95,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI Bold", 8.5F),
                    ForeColor = Color.FromArgb(217, 119, 6)
                }
            });

            // 6. Ngày đáo hạn
            dgvUserSavings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNgayDaoHan",
                HeaderText = "Đáo hạn",
                DataPropertyName = "FormattedNgayDaoHan",
                Width = 85,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(100, 116, 139)
                }
            });

            // 7. Trạng thái (Pill Badge)
            dgvUserSavings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTrangThai",
                HeaderText = "Trạng thái",
                DataPropertyName = "TrangThai",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 95,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            // Vẽ Pill Badge trạng thái và thanh viền chọn
            dgvUserSavings.CellPainting += DgvUserSavings_CellPainting;
        }

        private void DgvUserSavings_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            bool isSelected = (e.State & DataGridViewElementStates.Selected) != 0;

            // Xử lý vẽ pill badge cho cột Trạng thái
            if (e.ColumnIndex >= 0 && dgvUserSavings.Columns[e.ColumnIndex].Name == "colTrangThai")
            {
                e.Handled = true;

                Color rowBg = isSelected
                    ? Color.FromArgb(241, 245, 249)
                    : (e.RowIndex % 2 == 0 ? Color.White : Color.FromArgb(250, 252, 254));

                using (var bgBrush = new SolidBrush(rowBg))
                {
                    e.Graphics.FillRectangle(bgBrush, e.CellBounds);
                }

                using (var linePen = new Pen(Color.FromArgb(241, 245, 249)))
                {
                    e.Graphics.DrawLine(linePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
                }

                if (e.Value != null)
                {
                    string status = e.Value.ToString();
                    Color pillBg, pillBorder, pillText;
                    string pillLabel;

                    if (status == "Đang gửi")
                    {
                        pillBg = Color.FromArgb(236, 253, 245);
                        pillBorder = Color.FromArgb(167, 243, 208);
                        pillText = Color.FromArgb(22, 163, 74);
                        pillLabel = "✔ Đang gửi";
                    }
                    else
                    {
                        pillBg = Color.FromArgb(239, 246, 255);
                        pillBorder = Color.FromArgb(191, 219, 254);
                        pillText = Color.FromArgb(37, 99, 235);
                        pillLabel = "✔ Đã tất toán";
                    }

                    using (var pillFont = new Font("Segoe UI", 7.5F, FontStyle.Bold))
                    {
                        var measured = TextRenderer.MeasureText(pillLabel, pillFont);
                        int pillH = 22;
                        int pillW = Math.Min(e.CellBounds.Width - 6, Math.Max(78, measured.Width + 10));
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
            // Vẽ thanh chỉ báo màu xanh dương ở mép trái hàng đang chọn
            else if (e.ColumnIndex == 0 && isSelected)
            {
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);
                using (var indicatorBrush = new SolidBrush(Color.FromArgb(37, 99, 235)))
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

        #endregion
    }
}
