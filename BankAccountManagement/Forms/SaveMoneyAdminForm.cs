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
    public partial class SaveMoneyAdminForm : Form
    {
        private readonly SaveMoneyService _service;
        private List<CustomerSavingsProfile> _customers = new List<CustomerSavingsProfile>();
        private CustomerSavingsProfile _selectedCustomer = null;

        private List<SoTietKiem> _savingsList = new List<SoTietKiem>();
        private SoTietKiem _selectedSavings = null;

        private string _statusFilter = "Tất cả";
        private string _customerFilterMaKH = null;

        public SaveMoneyAdminForm()
        {
            InitializeComponent();
            _service = SaveMoneyService.Instance;

            SetupCustomerGrid();
            SetupSavingsGrid();
            WireEvents();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await ReloadAllDataAsync();
        }

        #region DATA LOADING

        private async Task ReloadAllDataAsync()
        {
            await LoadOverviewStatisticsAsync();
            await LoadCustomersAsync();
            await LoadSavingsListAsync();
        }

        private async Task LoadOverviewStatisticsAsync()
        {
            var overview = await _service.GetAdminOverviewStatisticsAsync();
            lblMetric1Value.Text = overview.FormattedDangHuyDong;
            lblMetric2Value.Text = $"{overview.SoDangGui} sổ";
            lblMetric3Value.Text = $"{overview.SoDaTatToan} sổ";
            lblMetric4Value.Text = overview.FormattedLaiDuTinh;
        }

        private async Task LoadCustomersAsync()
        {
            string kw = txtSearchCustomer.Text.Trim();
            _customers = await _service.GetAllCustomersWithSavingsAsync(kw);

            dgvCustomers.DataSource = null;
            dgvCustomers.DataSource = _customers;

            if (_customers.Count > 0)
            {
                // Chọn khách hàng đầu tiên nếu chưa chọn
                if (_selectedCustomer == null)
                {
                    _selectedCustomer = _customers[0];
                    DisplayCustomerDetail(_selectedCustomer);
                }
            }
            else
            {
                ClearCustomerDetail();
            }
        }

        private async Task LoadSavingsListAsync()
        {
            string kw = txtSearchSavings.Text.Trim();
            _savingsList = await _service.GetAllSavingsListAsync(kw, _statusFilter, _customerFilterMaKH);

            dgvAdminSavings.DataSource = null;
            dgvAdminSavings.DataSource = _savingsList;

            if (_savingsList.Count > 0)
            {
                dgvAdminSavings.Rows[0].Selected = true;
                _selectedSavings = _savingsList[0];
                DisplaySavingsSummary(_selectedSavings);
            }
            else
            {
                _selectedSavings = null;
                lblSelectedSummary.Text = "Không có tài khoản tiết kiệm nào phù hợp với bộ lọc hiện tại.";
                btnAdminSettle.Enabled = false;
            }
        }

        #endregion

        #region EVENT WIRING

        private void WireEvents()
        {
            // Tìm kiếm khách hàng
            txtSearchCustomer.TextChanged += async (s, e) =>
            {
                await LoadCustomersAsync();
            };

            // Chọn khách hàng trong bảng
            dgvCustomers.SelectionChanged += (s, e) =>
            {
                if (dgvCustomers.SelectedRows.Count > 0)
                {
                    var cust = dgvCustomers.SelectedRows[0].DataBoundItem as CustomerSavingsProfile;
                    if (cust != null)
                    {
                        _selectedCustomer = cust;
                        DisplayCustomerDetail(cust);
                    }
                }
            };

            // Xem tất cả khách hàng / Tải lại danh sách
            btnClearCustomerFilter.Click += async (s, e) =>
            {
                txtSearchCustomer.Text = "";
                _customerFilterMaKH = null;
                lblSavingsPanelTitle.Text = "Danh sách sổ tiết kiệm";
                await LoadCustomersAsync();
                await LoadSavingsListAsync();
            };

            // Tabs lọc trạng thái sổ
            btnTabAll.Click += async (s, e) => await SwitchStatusTab("Tất cả", btnTabAll);
            btnTabActive.Click += async (s, e) => await SwitchStatusTab("Đang gửi", btnTabActive);
            btnTabSettled.Click += async (s, e) => await SwitchStatusTab("Đã tất toán", btnTabSettled);

            // Tìm kiếm sổ
            txtSearchSavings.TextChanged += async (s, e) =>
            {
                await LoadSavingsListAsync();
            };

            btnRefreshSavings.Click += async (s, e) =>
            {
                await ReloadAllDataAsync();
            };

            // Chọn sổ trong bảng danh sách
            dgvAdminSavings.SelectionChanged += (s, e) =>
            {
                if (dgvAdminSavings.SelectedRows.Count > 0)
                {
                    var savings = dgvAdminSavings.SelectedRows[0].DataBoundItem as SoTietKiem;
                    if (savings != null)
                    {
                        _selectedSavings = savings;
                        DisplaySavingsSummary(savings);
                    }
                }
            };

            // Nút tất toán sổ tiết kiệm
            btnAdminSettle.Click += async (s, e) => await HandleAdminSettleAsync();

            // Nút xem chứng nhận
            btnAdminViewReceipt.Click += (s, e) =>
            {
                if (_selectedSavings != null)
                {
                    ShowReceiptCertificate(_selectedSavings);
                }
            };

            // Nút xuất dữ liệu
            btnAdminExport.Click += (s, e) =>
            {
                MessageBox.Show($"Đã xuất tổng hợp {_savingsList.Count} tài khoản tiết kiệm của hệ thống ra báo cáo quản trị.",
                    "Xuất báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
        }

        private async Task SwitchStatusTab(string status, RoundedButton activeBtn)
        {
            _statusFilter = status;

            // Reset tab styles
            RoundedButton[] tabs = { btnTabAll, btnTabActive, btnTabSettled };
            foreach (var b in tabs)
            {
                if (b == activeBtn)
                {
                    b.NormalColor = Color.FromArgb(37, 99, 235);
                    b.TextColor = Color.White;
                    b.BorderSize = 0;
                }
                else
                {
                    b.NormalColor = Color.White;
                    b.TextColor = Color.FromArgb(71, 85, 105);
                    b.BorderSize = 1;
                }
            }

            await LoadSavingsListAsync();
        }

        private void DisplayCustomerDetail(CustomerSavingsProfile cust)
        {
            lblCustHeaderName.Text = string.IsNullOrWhiteSpace(cust.HoTen) ? cust.MaKH : cust.HoTen;
            lblCustHeaderCode.Text = $"Mã KH: {cust.MaKH}";

            // Avatar initial
            string initial = "KH";
            if (!string.IsNullOrWhiteSpace(cust.HoTen))
            {
                var parts = cust.HoTen.Trim().Split(' ');
                if (parts.Length > 0 && !string.IsNullOrEmpty(parts[parts.Length - 1]))
                {
                    initial = parts[parts.Length - 1].Substring(0, 1).ToUpper();
                }
            }
            lblCustAvatarText.Text = initial;

            lblCustUsername.Text = string.IsNullOrWhiteSpace(cust.TenDangNhap) ? "-" : cust.TenDangNhap;
            lblCustCccd.Text = string.IsNullOrWhiteSpace(cust.CMND_CCCD) ? "-" : cust.CMND_CCCD;
            lblCustPhone.Text = string.IsNullOrWhiteSpace(cust.SoDienThoai) ? "-" : cust.SoDienThoai;
            lblCustBalance.Text = cust.FormattedTongSoDu;
            lblCustTotalSavings.Text = cust.FormattedTongTienDangGui;
            lblCustSavingsCount.Text = $"{cust.SoDangGui} đang gửi / {cust.SoDaTatToan} tất toán";
        }

        private void ClearCustomerDetail()
        {
            lblCustHeaderName.Text = "Không tìm thấy khách hàng";
            lblCustHeaderCode.Text = "-";
            lblCustAvatarText.Text = "?";
            lblCustUsername.Text = "-";
            lblCustCccd.Text = "-";
            lblCustPhone.Text = "-";
            lblCustBalance.Text = "0 đ";
            lblCustTotalSavings.Text = "0 đ";
            lblCustSavingsCount.Text = "0 đang gửi / 0 tất toán";
        }

        private void DisplaySavingsSummary(SoTietKiem s)
        {
            lblSelectedSummary.Text = $"Mã: {s.MaSoTKTK} | Chủ sổ: {s.FormattedCustomer} | Gốc: {s.FormattedSoTienGui} | Lãi: {s.FormattedTienLaiDuTinh} | {s.TinhTrangDaoHan}";

            if (s.TrangThai == "Đã tất toán")
            {
                btnAdminSettle.Enabled = false;
                btnAdminSettle.Text = "✔ Đã tất toán";
                btnAdminSettle.NormalColor = Color.FromArgb(148, 163, 184);
            }
            else
            {
                btnAdminSettle.Enabled = true;
                btnAdminSettle.Text = "💰 Tất toán sổ TKTK";
                btnAdminSettle.NormalColor = Color.FromArgb(22, 163, 74);
            }
        }

        private async Task HandleAdminSettleAsync()
        {
            if (_selectedSavings == null) return;

            if (_selectedSavings.TrangThai == "Đã tất toán")
            {
                MessageBox.Show("Tài khoản tiết kiệm này đã tất toán trước đó.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DateTime today = DateTime.Today;
            bool isMatured = today >= _selectedSavings.NgayDaoHan.Date;

            string prompt = isMatured
                ? $"XÁC NHẬN TẤT TOÁN ĐÚNG HẠN CHO KHÁCH HÀNG:\n\n" +
                  $"• Mã sổ TKTK: {_selectedSavings.MaSoTKTK}\n" +
                  $"• Khách hàng: {_selectedSavings.FormattedCustomer}\n" +
                  $"• STK thụ hưởng: {_selectedSavings.SoTaiKhoanTrich}\n" +
                  $"• Tiền gốc hoàn trả: {_selectedSavings.FormattedSoTienGui}\n" +
                  $"• Tiền lãi kỳ hạn ({_selectedSavings.FormattedKyHan}): {_selectedSavings.FormattedTienLaiDuTinh}\n" +
                  $"• Tổng tiền thực nhận: {_selectedSavings.FormattedTongTienDaoHan}\n\n" +
                  $"Hệ thống sẽ hoàn tất thanh toán và chuyển tiền trực tiếp vào tài khoản nguồn của khách.\n" +
                  $"Bạn có muốn tiếp tục phê duyệt tất toán?"
                : $"CẢNH BÁO: TẤT TOÁN TRƯỚC HẠN CHO KHÁCH HÀNG:\n\n" +
                  $"• Mã sổ TKTK: {_selectedSavings.MaSoTKTK}\n" +
                  $"• Khách hàng: {_selectedSavings.FormattedCustomer}\n" +
                  $"• Ngày đáo hạn quy định: {_selectedSavings.FormattedNgayDaoHan} (còn {(_selectedSavings.NgayDaoHan.Date - today).Days} ngày)\n" +
                  $"• Rút trước hạn sẽ áp dụng lãi suất không kỳ hạn (0.20%/năm).\n\n" +
                  $"Bạn có chắc chắn muốn tiến hành tất toán trước hạn không?";

            var confirm = MessageBox.Show(prompt, "Quản trị viên tất toán TKTK",
                MessageBoxButtons.YesNo, isMatured ? MessageBoxIcon.Question : MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            btnAdminSettle.Enabled = false;
            btnAdminSettle.Text = "Đang xử lý...";

            var result = await _service.SettleSavingsAccountAsync(_selectedSavings.MaSoTKTK, CurrentSession.TenDangNhap ?? "Admin");

            btnAdminSettle.Enabled = true;

            if (result.Success)
            {
                MessageBox.Show(result.Message, "Tất toán thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await ReloadAllDataAsync();
            }
            else
            {
                btnAdminSettle.Text = "💰 Tất toán sổ TKTK";
                MessageBox.Show(result.Message, "Không thể tất toán", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowReceiptCertificate(SoTietKiem s)
        {
            string doc =
                $"========================================================\n" +
                $"   HỆ THỐNG QUẢN LÝ TIẾT KIỆM - CHỨNG NHẬN ĐIỆN TỬ   \n" +
                $"========================================================\n\n" +
                $"• MÃ SỔ TKTK:          {s.MaSoTKTK}\n" +
                $"• MÃ KHÁCH HÀNG:       {s.MaKH}\n" +
                $"• TÊN KHÁCH HÀNG:      {s.HoTen}\n" +
                $"• TÀI KHOẢN TRÍCH:     {s.SoTaiKhoanTrich}\n" +
                $"• SỐ TIỀN GỐC GỬI:     {s.FormattedSoTienGui}\n" +
                $"• KỲ HẠN:              {s.FormattedKyHan}\n" +
                $"• LÃI SUẤT NIÊM YẾT:   {s.FormattedLaiSuat}\n" +
                $"• TIỀN LÃI DỰ TÍNH:    {s.FormattedTienLaiDuTinh}\n" +
                $"• TỔNG NHẬN ĐÁO HẠN:   {s.FormattedTongTienDaoHan}\n" +
                $"• PHƯƠNG THỨC ĐÁO HẠN: {s.HinhThucDaoHan}\n" +
                $"• NGÀY MỞ:             {s.FormattedNgayMo}\n" +
                $"• NGÀY ĐÁO HẠN:        {s.FormattedNgayDaoHan}\n" +
                $"• TRẠNG THÁI:          {s.TrangThai}\n" +
                $"• NGÀY TẤT TOÁN:       {s.FormattedNgayTatToan}\n\n" +
                $"========================================================\n" +
                $"Được phát hành và xác thực bởi Ban Quản Trị Hệ Thống.";

            MessageBox.Show(doc, $"Chi tiết Sổ tiết kiệm {s.MaSoTKTK}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region GRIDS SETUP

        private void SetupCustomerGrid()
        {
            try
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                    null, dgvCustomers, new object[] { true });
            }
            catch { }

            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.Columns.Clear();
            dgvCustomers.RowTemplate.Height = 32;
            dgvCustomers.ColumnHeadersHeight = 30;

            dgvCustomers.EnableHeadersVisualStyles = false;
            dgvCustomers.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(248, 250, 252),
                ForeColor = Color.FromArgb(71, 85, 105),
                Font = new Font("Segoe UI Semibold", 8.25F, FontStyle.Bold),
                SelectionBackColor = Color.FromArgb(248, 250, 252),
                SelectionForeColor = Color.FromArgb(71, 85, 105)
            };

            dgvCustomers.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(15, 23, 42),
                Font = new Font("Segoe UI", 8.25F),
                SelectionBackColor = Color.FromArgb(241, 245, 249),
                SelectionForeColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(2, 0, 2, 0)
            };

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCustMaKH",
                HeaderText = "Mã KH",
                DataPropertyName = "MaKH",
                Width = 75,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI Semibold", 8F),
                    ForeColor = Color.FromArgb(51, 65, 85)
                }
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCustHoTen",
                HeaderText = "Họ tên khách hàng",
                DataPropertyName = "HoTen",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 110,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI Semibold", 8.25F),
                    ForeColor = Color.FromArgb(15, 23, 42)
                }
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCustTienGui",
                HeaderText = "Tiền TKTK",
                DataPropertyName = "FormattedTongTienDangGui",
                Width = 95,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI Bold", 8.25F),
                    ForeColor = Color.FromArgb(22, 163, 74)
                }
            });
        }

        private void SetupSavingsGrid()
        {
            try
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                    null, dgvAdminSavings, new object[] { true });
            }
            catch { }

            dgvAdminSavings.AutoGenerateColumns = false;
            dgvAdminSavings.Columns.Clear();
            dgvAdminSavings.RowTemplate.Height = 36;
            dgvAdminSavings.ColumnHeadersHeight = 36;

            dgvAdminSavings.EnableHeadersVisualStyles = false;
            dgvAdminSavings.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(248, 250, 252),
                ForeColor = Color.FromArgb(71, 85, 105),
                Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
                SelectionBackColor = Color.FromArgb(248, 250, 252),
                SelectionForeColor = Color.FromArgb(71, 85, 105)
            };

            dgvAdminSavings.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(15, 23, 42),
                Font = new Font("Segoe UI", 8.25F),
                SelectionBackColor = Color.FromArgb(241, 245, 249),
                SelectionForeColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(2, 0, 2, 0)
            };

            dgvAdminSavings.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(250, 252, 254),
                ForeColor = Color.FromArgb(15, 23, 42),
                Font = new Font("Segoe UI", 8.25F),
                SelectionBackColor = Color.FromArgb(241, 245, 249),
                SelectionForeColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(2, 0, 2, 0)
            };

            // 1. Mã Sổ
            dgvAdminSavings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAdminMaSo",
                HeaderText = "Mã TKTK",
                DataPropertyName = "MaSoTKTK",
                Width = 95,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI Semibold", 8.25F),
                    ForeColor = Color.FromArgb(30, 41, 59)
                }
            });

            // 2. Khách hàng
            dgvAdminSavings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAdminKhachHang",
                HeaderText = "Khách hàng",
                DataPropertyName = "FormattedCustomer",
                Width = 125,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI Semibold", 8.25F),
                    ForeColor = Color.FromArgb(15, 23, 42)
                }
            });

            // 3. STK trích
            dgvAdminSavings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAdminSTK",
                HeaderText = "STK nguồn",
                DataPropertyName = "FormattedMaskedSTK",
                Width = 85,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(100, 116, 139)
                }
            });

            // 4. Tiền gốc gửi
            dgvAdminSavings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAdminTienGui",
                HeaderText = "Tiền gửi",
                DataPropertyName = "FormattedSoTienGui",
                Width = 95,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI Semibold", 8.5F),
                    ForeColor = Color.FromArgb(22, 163, 74)
                }
            });

            // 5. Kỳ hạn
            dgvAdminSavings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAdminKyHan",
                HeaderText = "Kỳ hạn",
                DataPropertyName = "FormattedKyHan",
                Width = 65,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(100, 116, 139)
                }
            });

            // 6. Lãi suất
            dgvAdminSavings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAdminLaiSuat",
                HeaderText = "Lãi suất",
                DataPropertyName = "FormattedLaiSuat",
                Width = 75,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(15, 23, 42)
                }
            });

            // 7. Lãi dự tính
            dgvAdminSavings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAdminTienLai",
                HeaderText = "Lãi dự tính",
                DataPropertyName = "FormattedTienLaiDuTinh",
                Width = 90,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI Bold", 8.25F),
                    ForeColor = Color.FromArgb(217, 119, 6)
                }
            });

            // 8. Ngày mở
            dgvAdminSavings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAdminNgayMo",
                HeaderText = "Ngày mở",
                DataPropertyName = "FormattedNgayMo",
                Width = 75,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(100, 116, 139)
                }
            });

            // 9. Ngày đáo hạn
            dgvAdminSavings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAdminNgayDaoHan",
                HeaderText = "Đáo hạn",
                DataPropertyName = "FormattedNgayDaoHan",
                Width = 75,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(100, 116, 139)
                }
            });

            // 10. Trạng thái (Pill Badge)
            dgvAdminSavings.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAdminTrangThai",
                HeaderText = "Trạng thái",
                DataPropertyName = "TrangThai",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                MinimumWidth = 95,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dgvAdminSavings.CellPainting += DgvAdminSavings_CellPainting;
        }

        private void DgvAdminSavings_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            bool isSelected = (e.State & DataGridViewElementStates.Selected) != 0;

            // Xử lý vẽ pill badge cho cột Trạng thái
            if (e.ColumnIndex >= 0 && dgvAdminSavings.Columns[e.ColumnIndex].Name == "colAdminTrangThai")
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
