using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace BankAccountManagement
{
    /// <summary>
    /// Form tạo và thực hiện giao dịch tài chính mới (Add Transaction Form).
    /// <para>
    /// Hỗ trợ cả 2 chế độ nghiệp vụ:
    /// - Quản trị viên (Admin): Thực hiện Nạp tiền, Rút tiền, Chuyển khoản hoặc Thanh toán cho bất kỳ tài khoản nào trên toàn hệ thống.
    /// - Khách hàng (Member):
    ///   + Tự động cố định tài khoản chuyển tiền chính của bản thân kèm số dư thực tế theo thời gian thực.
    ///   + Nhập STK người nhận tự động tra cứu tên chủ tài khoản và ngân hàng liên kết.
    ///   + Kiểm tra số dư nghiêm ngặt (không cho phép giao dịch vượt quá số dư khả dụng).
    ///   + Thực hiện giao dịch 2 bên (Trừ tiền bên gửi - Tạo giao dịch Chi; Cộng tiền bên nhận - Tạo giao dịch Thu) thông qua TransactionService.TransferMoney.
    ///   + Hỗ trợ giao dịch đa tiền tệ (VND, USD...) với bảng tỷ giá quy đổi tự động.
    /// </para>
    /// </summary>
    public class AddTransactionForm : Form
    {
        #region 1. CÁC BIẾN THÀNH PHẦN VÀ ĐIỀU KHIỂN

        private TransactionService _transactionService;
        private List<CurrencyInfo> _currencies;

        private Label lblHeader;

        // Tài khoản nguồn của người dùng đăng nhập (Member)
        private Label lblMyAccount;
        private ComboBox cboMyAccount;
        private Label lblSoDuThucTe;
        private TextBox txtSoDuThucTe;

        // Mẫu giao dịch trước đó để điền nhanh
        private ComboBox cboRecentTransfers;
        private DateTimePicker dtpNgay;
        private TextBox txtNoiDung;

        // Thông tin người nhận
        private Label lblNganHang;
        private ComboBox cboNganHang;
        private TextBox txtSoTaiKhoan;
        private TextBox txtNguoiNhan;

        // Loại và Thao tác
        private ComboBox cboThaoTac;
        private Label lblLoaiGD;
        private ComboBox cboLoaiGD;

        // Danh mục chi tiêu cho Khách hàng
        private Label lblDanhMuc;
        private ComboBox cboDanhMuc;

        // Tiền tệ và Quy đổi
        private Label lblLoaiTien;
        private ComboBox cboLoaiTien;
        private TextBox txtSoTien;
        private Label lblQuyDoi;

        private Button btnSave;
        private Button btnCancel;

        private bool _isSyncingType = false;

        private string _selectedSenderAccount = "";
        private decimal _selectedSenderBalance = 0m;
        private string _selectedSenderCurrency = "VND";
        private decimal _selectedSenderQuyDoi = 1m;

        #endregion

        #region 2. KHỞI TẠO VÀ BỐ CỤC GIAO DIỆN (INITIALIZATION & LAYOUT)

        public AddTransactionForm() : this(null) { }

        /// <summary>
        /// Hàm dựng khởi tạo Form với danh sách các loại tiền tệ hỗ trợ.
        /// </summary>
        /// <param name="currencies">Danh sách tiền tệ (VND, USD...). Nếu null sẽ tự truy vấn từ TransactionService.</param>
        public AddTransactionForm(List<CurrencyInfo> currencies)
        {
            _transactionService = new TransactionService();
            _currencies = currencies;
            if (_currencies == null || _currencies.Count == 0)
            {
                try
                {
                    _currencies = _transactionService.GetCurrencies();
                }
                catch { }
            }

            InitializeCustomComponent();
            LoadDropdowns();
            LoadActualBalance();
        }

        /// <summary>
        /// Khởi tạo và căn chỉnh bố cục các điều khiển giao diện hoàn toàn bằng mã C# thuần.
        /// </summary>
        private void InitializeCustomComponent()
        {
            this.Text = "Thêm giao dịch mới";
            int formHeight = CurrentSession.IsAdmin ? 520 : 590;
            this.Size = new Size(540, formHeight);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            this.SuspendLayout();

            // 1. Tiêu đề trên cùng (Blue Header)
            Panel pnlHeader = new Panel
            {
                BackColor = Color.FromArgb(51, 102, 255),
                Dock = DockStyle.Top,
                Height = 50
            };
            this.Controls.Add(pnlHeader);

            lblHeader = new Label
            {
                Text = "Thêm giao dịch mới",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(15, 12)
            };
            pnlHeader.Controls.Add(lblHeader);

            int startY = 62;
            int gapY = 36;
            int labelX = 22;
            int inputX = 175;
            int inputWidth = 335;

            // 2. Tài khoản nguồn của chính mình (Chỉ hiển thị cho Member)
            if (!CurrentSession.IsAdmin)
            {
                lblMyAccount = AddLabel("Tài khoản của mình:", labelX, startY);
                cboMyAccount = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(inputX, startY),
                    Width = inputWidth,
                    Font = new Font("Segoe UI", 9.5F)
                };
                cboMyAccount.SelectedIndexChanged += CboMyAccount_SelectedIndexChanged;
                this.Controls.Add(cboMyAccount);

                startY += gapY;

                lblSoDuThucTe = AddLabel("Số dư thực tế:", labelX, startY);
                txtSoDuThucTe = new TextBox
                {
                    ReadOnly = true,
                    BackColor = Color.FromArgb(243, 244, 246),
                    ForeColor = Color.FromArgb(16, 185, 129),
                    Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                    Location = new Point(inputX, startY),
                    Width = inputWidth,
                    Text = "0 VND"
                };
                this.Controls.Add(txtSoDuThucTe);

                startY += gapY;
            }

            // 3. Mẫu giao dịch chuyển tiền trước đó (chọn nhanh)
            AddLabel("GD mẫu trước:", labelX, startY);
            cboRecentTransfers = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(inputX, startY),
                Width = inputWidth,
                Font = new Font("Segoe UI", 9.5F)
            };
            cboRecentTransfers.SelectedIndexChanged += CboRecentTransfers_SelectedIndexChanged;
            this.Controls.Add(cboRecentTransfers);

            // 4. Ngày thực hiện giao dịch
            startY += gapY;
            AddLabel("Ngày giao dịch:", labelX, startY);
            dtpNgay = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Location = new Point(inputX, startY),
                Width = inputWidth,
                Font = new Font("Segoe UI", 9.5F)
            };
            this.Controls.Add(dtpNgay);

            // 5. Nội dung giao dịch
            startY += gapY;
            AddLabel("Nội dung:", labelX, startY);
            txtNoiDung = new TextBox
            {
                Multiline = true,
                Location = new Point(inputX, startY),
                Width = inputWidth,
                Height = 40,
                Font = new Font("Segoe UI", 9.5F)
            };
            this.Controls.Add(txtNoiDung);

            // 6. Ngân hàng người nhận
            startY += 46;
            lblNganHang = AddLabel(CurrentSession.IsAdmin ? "Ngân hàng:" : "Ngân hàng người nhận:", labelX, startY);
            cboNganHang = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(inputX, startY),
                Width = inputWidth,
                Font = new Font("Segoe UI", 9.5F)
            };
            this.Controls.Add(cboNganHang);

            // 7. Số tài khoản người nhận
            startY += gapY;
            AddLabel("Số tài khoản:", labelX, startY);
            txtSoTaiKhoan = new TextBox
            {
                Location = new Point(inputX, startY),
                Width = inputWidth,
                Font = new Font("Segoe UI", 9.5F)
            };
            txtSoTaiKhoan.TextChanged += TxtSoTaiKhoan_TextChanged;
            txtSoTaiKhoan.Leave += TxtSoTaiKhoan_Leave;
            this.Controls.Add(txtSoTaiKhoan);

            // 8. Tên người nhận (tự động tra cứu)
            startY += gapY;
            AddLabel("Tên người nhận:", labelX, startY);
            txtNguoiNhan = new TextBox
            {
                ReadOnly = true,
                BackColor = Color.FromArgb(243, 244, 246),
                ForeColor = Color.FromArgb(16, 185, 129),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Location = new Point(inputX, startY),
                Width = inputWidth
            };
            this.Controls.Add(txtNguoiNhan);

            // 9. Thao tác (Chuyển khoản, Rút tiền, Nạp tiền...)
            startY += gapY;
            AddLabel("Thao tác:", labelX, startY);
            cboThaoTac = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(inputX, startY),
                Width = inputWidth,
                Font = new Font("Segoe UI", 9.5F)
            };
            cboThaoTac.SelectedIndexChanged += CboThaoTac_SelectedIndexChanged;
            this.Controls.Add(cboThaoTac);

            // 10. Loại giao dịch (Admin) hoặc Danh mục chi tiêu (Member)
            if (CurrentSession.IsAdmin)
            {
                startY += gapY;
                lblLoaiGD = AddLabel("Loại:", labelX, startY);
                cboLoaiGD = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(inputX, startY),
                    Width = inputWidth,
                    Font = new Font("Segoe UI", 9.5F)
                };
                cboLoaiGD.SelectedIndexChanged += CboLoaiGD_SelectedIndexChanged;
                this.Controls.Add(cboLoaiGD);
            }
            else
            {
                startY += gapY;
                lblDanhMuc = AddLabel("Danh mục:", labelX, startY);
                cboDanhMuc = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(inputX, startY),
                    Width = inputWidth,
                    Font = new Font("Segoe UI", 9.5F)
                };
                this.Controls.Add(cboDanhMuc);
            }

            // 11. Loại tiền tệ (VND / USD / EUR...)
            startY += gapY;
            lblLoaiTien = AddLabel("Loại tiền:", labelX, startY);
            cboLoaiTien = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(inputX, startY),
                Width = inputWidth,
                Font = new Font("Segoe UI", 9.5F)
            };
            cboLoaiTien.SelectedIndexChanged += (s, e) => UpdateConvertedAmount();
            this.Controls.Add(cboLoaiTien);

            // 12. Số tiền gốc
            startY += gapY;
            AddLabel("Số tiền gốc:", labelX, startY);
            txtSoTien = new TextBox
            {
                Location = new Point(inputX, startY),
                Width = inputWidth,
                Font = new Font("Segoe UI", 9.5F),
                Text = "0",
                TextAlign = HorizontalAlignment.Left
            };
            txtSoTien.TextChanged += (s, e) => UpdateConvertedAmount();
            this.Controls.Add(txtSoTien);

            // 13. Nhãn hiển thị số tiền quy đổi sang VND
            startY += 26;
            lblQuyDoi = new Label
            {
                Text = "Quy đổi: 0 VND",
                ForeColor = Color.ForestGreen,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                Location = new Point(inputX, startY),
                AutoSize = true
            };
            this.Controls.Add(lblQuyDoi);

            // 14. Nút bấm Thực hiện & Hủy bỏ
            startY += 30;
            btnSave = new Button
            {
                Text = "Thực hiện",
                BackColor = Color.FromArgb(51, 102, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(105, 36),
                Location = new Point(300, startY),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            btnCancel = new Button
            {
                Text = "Hủy bỏ",
                BackColor = Color.White,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(95, 36),
                Location = new Point(415, startY),
                Font = new Font("Segoe UI", 9.5F),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderColor = Color.LightGray;
            btnCancel.Click += (s, e) => ConfirmCancel();
            this.Controls.Add(btnCancel);

            this.ResumeLayout(false);
        }

        private Label AddLabel(string text, int x, int y)
        {
            Label lbl = new Label
            {
                Text = text,
                Location = new Point(x, y + 4),
                Font = new Font("Segoe UI", 9.5F),
                AutoSize = true
            };
            this.Controls.Add(lbl);
            return lbl;
        }

        #endregion

        #region 3. NẠP DỮ LIỆU DANH MỤC & MẪU (DROPDOWNS & PREFILL)

        /// <summary>
        /// Nạp các dữ liệu ngân hàng, tài khoản nguồn, danh mục và tiền tệ vào ComboBox.
        /// </summary>
        private void LoadDropdowns()
        {
            try
            {
                // 1. Tài khoản của mình (Khách hàng)
                if (!CurrentSession.IsAdmin && cboMyAccount != null)
                {
                    DataTable dtMyAccounts = _transactionService.GetUserAccountsWithBankInfo(CurrentSession.MaKH);
                    cboMyAccount.DataSource = dtMyAccounts;
                    cboMyAccount.DisplayMember = "HienThi";
                    cboMyAccount.ValueMember = "SoTaiKhoan";
                    if (dtMyAccounts != null && dtMyAccounts.Rows.Count > 0)
                    {
                        cboMyAccount.SelectedIndex = 0;
                        cboMyAccount.Enabled = false;
                    }
                    UpdateSenderAccountFromSelection();
                }

                // 2. Danh sách tất cả ngân hàng đối tác trong hệ thống
                DataTable dtAllBanks = _transactionService.GetBanksList();
                cboNganHang.DataSource = dtAllBanks;
                cboNganHang.DisplayMember = "HienThi";
                cboNganHang.ValueMember = "MaNganHang";

                // 3. Danh sách các thao tác khả dụng
                cboThaoTac.Items.Clear();
                if (CurrentSession.IsAdmin)
                {
                    cboThaoTac.Items.AddRange(new string[] { "Nạp tiền", "Rút tiền", "Chuyển khoản", "Thanh toán" });
                    cboThaoTac.SelectedIndex = 0;
                }
                else
                {
                    cboThaoTac.Items.AddRange(new string[] { "Chuyển khoản", "Thanh toán", "Rút tiền" });
                    cboThaoTac.SelectedIndex = 0;
                }

                // 4. Danh sách loại giao dịch (Dành cho Admin)
                if (CurrentSession.IsAdmin && cboLoaiGD != null)
                {
                    DataTable dtTypes = _transactionService.GetTransactionTypes();
                    cboLoaiGD.DataSource = dtTypes;
                    cboLoaiGD.DisplayMember = "TenLoaiGD";
                    cboLoaiGD.ValueMember = "MaLoaiGD";

                    _isSyncingType = true;
                    try
                    {
                        cboLoaiGD.SelectedValue = 1;
                    }
                    catch { }
                    finally
                    {
                        _isSyncingType = false;
                    }
                }

                // 5. Danh mục phân loại chi tiêu cá nhân (Dành cho Member)
                if (!CurrentSession.IsAdmin && cboDanhMuc != null)
                {
                    cboDanhMuc.Items.Clear();
                    cboDanhMuc.Items.AddRange(new string[] {
                        "Ăn uống",
                        "Mua sắm",
                        "Vui chơi & Giải trí",
                        "Hóa đơn & Tiện ích",
                        "Đi lại & Xăng xe",
                        "Học tập & Giáo dục",
                        "Sức khỏe & Y tế",
                        "Nhà cửa & Đời sống",
                        "Tiết kiệm & Đầu tư",
                        "Chi tiêu khác"
                    });
                    cboDanhMuc.SelectedIndex = 0;
                }

                // 6. Danh sách tiền tệ (VND, USD, EUR...)
                cboLoaiTien.DataSource = new List<CurrencyInfo>(_currencies);
                cboLoaiTien.DisplayMember = "MaLoaiTien";
                cboLoaiTien.ValueMember = "MaLoaiTien";
                try { cboLoaiTien.SelectedValue = "VND"; } catch { }

                // 7. Tải danh sách giao dịch mẫu gần đây để điền nhanh
                LoadRecentTransfers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh mục giao dịch: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboMyAccount_SelectedIndexChanged(object sender, EventArgs e) => UpdateSenderAccountFromSelection();

        /// <summary>
        /// Cập nhật thông tin tài khoản chuyển tiền và số dư thực tế dựa trên tài khoản đang được chọn.
        /// </summary>
        private void UpdateSenderAccountFromSelection()
        {
            if (CurrentSession.IsAdmin || cboMyAccount == null) return;
            DataRow row = null;
            if (cboMyAccount.SelectedItem is DataRowView drv)
            {
                row = drv.Row;
            }
            else if (cboMyAccount.DataSource is DataTable dt && dt.Rows.Count > 0)
            {
                row = dt.Rows[0];
            }

            if (row != null)
            {
                _selectedSenderAccount = row["SoTaiKhoan"]?.ToString() ?? "";
                _selectedSenderBalance = row["SoDuHienTai"] != DBNull.Value ? Convert.ToDecimal(row["SoDuHienTai"]) : 0m;
                _selectedSenderCurrency = row["MaLoaiTien"]?.ToString() ?? "VND";
                _selectedSenderQuyDoi = row["QuyDoi"] != DBNull.Value ? Convert.ToDecimal(row["QuyDoi"]) : 1m;
                if (_selectedSenderQuyDoi <= 1 && !_selectedSenderCurrency.Equals("VND", StringComparison.OrdinalIgnoreCase))
                {
                    var matchedCurr = _currencies?.FirstOrDefault(c => string.Equals(c.MaLoaiTien, _selectedSenderCurrency, StringComparison.OrdinalIgnoreCase));
                    if (matchedCurr != null && matchedCurr.QuyDoi > 0)
                        _selectedSenderQuyDoi = matchedCurr.QuyDoi;
                    else if (_selectedSenderCurrency.Equals("USD", StringComparison.OrdinalIgnoreCase))
                        _selectedSenderQuyDoi = 25400m;
                }

                if (txtSoDuThucTe != null)
                {
                    decimal senderRate = _selectedSenderQuyDoi > 0 ? _selectedSenderQuyDoi : 1m;
                    decimal displayVND = (_selectedSenderCurrency == "VND")
                        ? _selectedSenderBalance
                        : (_selectedSenderBalance * senderRate);
                    txtSoDuThucTe.Text = $"{displayVND:N0} VND";
                }

                string tt = cboThaoTac?.SelectedItem?.ToString() ?? "";
                if (tt == "Rút tiền")
                {
                    txtSoTaiKhoan.Text = _selectedSenderAccount;
                    txtNguoiNhan.Text = CurrentSession.HoTen ?? "";
                }

                UpdateConvertedAmount();
            }
        }

        /// <summary>
        /// Lấy lại số dư tài khoản người gửi trực tiếp từ cơ sở dữ liệu để đảm bảo dữ liệu mới nhất.
        /// </summary>
        private void RefreshSenderBalance()
        {
            if (CurrentSession.IsAdmin || string.IsNullOrEmpty(_selectedSenderAccount)) return;
            try
            {
                decimal bal = _transactionService.GetAccountBalance(_selectedSenderAccount);
                CurrencyInfo cur = _transactionService.GetAccountCurrency(_selectedSenderAccount);
                _selectedSenderBalance = bal;
                if (cur != null)
                {
                    _selectedSenderCurrency = cur.MaLoaiTien;
                    decimal qd = cur.QuyDoi;
                    if (qd <= 1 && !_selectedSenderCurrency.Equals("VND", StringComparison.OrdinalIgnoreCase))
                    {
                        var matchedCurr = _currencies?.FirstOrDefault(c => string.Equals(c.MaLoaiTien, _selectedSenderCurrency, StringComparison.OrdinalIgnoreCase));
                        if (matchedCurr != null && matchedCurr.QuyDoi > 0)
                            qd = matchedCurr.QuyDoi;
                        else if (_selectedSenderCurrency.Equals("USD", StringComparison.OrdinalIgnoreCase))
                            qd = 25400m;
                    }
                    _selectedSenderQuyDoi = qd > 0 ? qd : 1m;
                }
                if (txtSoDuThucTe != null)
                {
                    decimal senderRate = _selectedSenderQuyDoi > 0 ? _selectedSenderQuyDoi : 1m;
                    decimal displayVND = (_selectedSenderCurrency == "VND")
                        ? _selectedSenderBalance
                        : (_selectedSenderBalance * senderRate);
                    txtSoDuThucTe.Text = $"{displayVND:N0} VND";
                }
            }
            catch { }
        }

        private void LoadActualBalance()
        {
            if (CurrentSession.IsAdmin || txtSoDuThucTe == null) return;
            UpdateSenderAccountFromSelection();
        }

        /// <summary>
        /// Nạp các giao dịch đã thực hiện thành công gần đây để người dùng chọn nhanh.
        /// </summary>
        private void LoadRecentTransfers()
        {
            try
            {
                DataTable dtRecent = _transactionService.GetRecentTransfers(CurrentSession.MaKH);
                DataRow blankRow = dtRecent.NewRow();
                blankRow["MaGiaoDich"] = "";
                blankRow["HienThi"] = "-- Chọn để điền nhanh --";
                dtRecent.Rows.InsertAt(blankRow, 0);

                cboRecentTransfers.DataSource = dtRecent;
                cboRecentTransfers.DisplayMember = "HienThi";
                cboRecentTransfers.ValueMember = "MaGiaoDich";
                cboRecentTransfers.SelectedIndex = 0;
            }
            catch { }
        }

        private void CboRecentTransfers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRecentTransfers.SelectedIndex <= 0) return;

            DataRowView drv = cboRecentTransfers.SelectedItem as DataRowView;
            if (drv != null)
            {
                string stk = drv["SoTaiKhoan"]?.ToString() ?? "";
                string maNganHang = drv["MaNganHang"]?.ToString() ?? "";
                string noiDung = drv["NoiDung"]?.ToString() ?? "";
                string maLoaiTien = drv["MaLoaiTien"]?.ToString() ?? "";
                decimal soTienGoc = drv["SoTienGoc"] != DBNull.Value ? Convert.ToDecimal(drv["SoTienGoc"]) : 0m;

                if (!CurrentSession.IsAdmin)
                {
                    string targetStk = "";
                    int idx = noiDung.IndexOf("Chuyển tiền đến ");
                    if (idx >= 0)
                    {
                        string sub = noiDung.Substring(idx + 16).Trim();
                        targetStk = sub.Split(' ')[0];
                    }

                    txtSoTaiKhoan.Text = !string.IsNullOrEmpty(targetStk) ? targetStk : stk;
                    txtNoiDung.Text = noiDung;
                    txtSoTien.Text = soTienGoc.ToString("N0");

                    if (!string.IsNullOrEmpty(stk) && cboMyAccount != null)
                    {
                        cboMyAccount.SelectedValue = stk;
                    }

                    if (!string.IsNullOrEmpty(maLoaiTien))
                    {
                        cboLoaiTien.SelectedValue = maLoaiTien;
                    }

                    cboThaoTac.SelectedItem = "Chuyển khoản";
                }
                else
                {
                    txtSoTaiKhoan.Text = stk;
                    txtNoiDung.Text = noiDung;
                    txtSoTien.Text = soTienGoc.ToString("N0");

                    if (!string.IsNullOrEmpty(maNganHang))
                    {
                        cboNganHang.SelectedValue = maNganHang;
                    }

                    if (!string.IsNullOrEmpty(maLoaiTien))
                    {
                        cboLoaiTien.SelectedValue = maLoaiTien;
                    }

                    cboThaoTac.SelectedItem = "Chuyển khoản";
                }

                string ownerName = _transactionService.GetAccountOwnerName(txtSoTaiKhoan.Text.Trim());
                if (!string.IsNullOrEmpty(ownerName))
                {
                    txtNguoiNhan.Text = ownerName;
                    txtNguoiNhan.ForeColor = Color.FromArgb(16, 185, 129);
                }

                UpdateConvertedAmount();
            }
        }

        #endregion

        #region 4. TRA CỨU TỰ ĐỘNG VÀ ĐỒNG BỘ THAO TÁC (AUTO LOOKUP & SYNC)

        /// <summary>
        /// Tự động tra cứu tên chủ tài khoản và mã ngân hàng khi người dùng gõ số tài khoản.
        /// </summary>
        private void TxtSoTaiKhoan_TextChanged(object sender, EventArgs e)
        {
            string stk = txtSoTaiKhoan.Text.Trim();
            if (string.IsNullOrEmpty(stk))
            {
                txtNguoiNhan.Text = "";
                return;
            }

            string ownerName = _transactionService.GetAccountOwnerName(stk);
            if (!string.IsNullOrEmpty(ownerName))
            {
                txtNguoiNhan.Text = ownerName;
                txtNguoiNhan.ForeColor = Color.FromArgb(16, 185, 129);
            }
            else
            {
                txtNguoiNhan.Text = "";
            }

            string bankCode = _transactionService.GetAccountBank(stk);
            if (!string.IsNullOrEmpty(bankCode) && cboNganHang != null)
            {
                cboNganHang.SelectedValue = bankCode;
            }

            if (CurrentSession.IsAdmin)
            {
                CurrencyInfo cur = _transactionService.GetAccountCurrency(stk);
                if (cur != null)
                {
                    cboLoaiTien.SelectedValue = cur.MaLoaiTien;
                    UpdateConvertedAmount();
                }
            }
        }

        private void TxtSoTaiKhoan_Leave(object sender, EventArgs e)
        {
            string stk = txtSoTaiKhoan.Text.Trim();
            if (!string.IsNullOrEmpty(stk) && string.IsNullOrEmpty(txtNguoiNhan.Text))
            {
                txtNguoiNhan.Text = "(Không tìm thấy tài khoản)";
                txtNguoiNhan.ForeColor = Color.Crimson;
            }
        }

        /// <summary>
        /// Đồng bộ thao tác người dùng với loại giao dịch tương ứng trong CSDL.
        /// </summary>
        private void CboThaoTac_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isSyncingType) return;
            _isSyncingType = true;
            try
            {
                string tt = cboThaoTac.SelectedItem?.ToString() ?? "";
                if (cboLoaiGD != null)
                {
                    if (tt == "Nạp tiền")
                        cboLoaiGD.SelectedValue = 1;
                    else if (tt == "Rút tiền")
                        cboLoaiGD.SelectedValue = 2;
                    else if (tt == "Thanh toán")
                        cboLoaiGD.SelectedValue = 2;
                    else if (tt == "Chuyển khoản")
                        cboLoaiGD.SelectedValue = 4;
                }

                if (!CurrentSession.IsAdmin)
                {
                    if (tt == "Rút tiền")
                    {
                        txtSoTaiKhoan.Text = _selectedSenderAccount;
                        txtNguoiNhan.Text = CurrentSession.HoTen ?? "";
                        txtSoTaiKhoan.ReadOnly = true;
                    }
                    else
                    {
                        txtSoTaiKhoan.ReadOnly = false;
                        if (txtSoTaiKhoan.Text == _selectedSenderAccount)
                        {
                            txtSoTaiKhoan.Text = "";
                            txtNguoiNhan.Text = "";
                        }
                    }
                }
            }
            catch { }
            finally { _isSyncingType = false; }
        }

        private void CboLoaiGD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isSyncingType || cboLoaiGD == null) return;
            _isSyncingType = true;
            try
            {
                if (cboLoaiGD.SelectedValue != null && int.TryParse(cboLoaiGD.SelectedValue.ToString(), out int maLoai))
                {
                    if (maLoai == 1 && CurrentSession.IsAdmin) cboThaoTac.SelectedItem = "Nạp tiền";
                    else if (maLoai == 2) cboThaoTac.SelectedItem = "Rút tiền";
                    else if (maLoai == 3 || maLoai == 4) cboThaoTac.SelectedItem = "Chuyển khoản";
                }
            }
            catch { }
            finally { _isSyncingType = false; }
        }

        #endregion

        #region 5. TÍNH TOÁN TIỀN TỆ VÀ QUY ĐỔI REAL-TIME (CURRENCY CALCULATION)

        /// <summary>
        /// Phân tích cú pháp chuỗi số tiền người dùng nhập vào, xử lý linh hoạt dấu phẩy (,) và dấu chấm (.) ngăn cách hàng nghìn.
        /// </summary>
        private decimal ParseAmount(string text, string currency)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0m;
            string clean = text.Trim().Replace(" ", "");

            if (clean.Contains(",") && clean.Contains("."))
            {
                if (clean.LastIndexOf('.') > clean.LastIndexOf(','))
                {
                    clean = clean.Replace(",", "");
                }
                else
                {
                    clean = clean.Replace(".", "").Replace(",", ".");
                }
            }
            else if (clean.Contains(","))
            {
                if (currency.Equals("VND", StringComparison.OrdinalIgnoreCase) ||
                    currency.Equals("JPY", StringComparison.OrdinalIgnoreCase) ||
                    currency.Equals("KRW", StringComparison.OrdinalIgnoreCase))
                {
                    clean = clean.Replace(",", "");
                }
                else
                {
                    int commaCount = clean.Count(c => c == ',');
                    if (commaCount > 1)
                    {
                        clean = clean.Replace(",", "");
                    }
                    else
                    {
                        int idx = clean.IndexOf(',');
                        if (clean.Length - idx - 1 == 3 && idx <= 3)
                            clean = clean.Replace(",", "");
                        else
                            clean = clean.Replace(",", ".");
                    }
                }
            }
            else if (clean.Contains("."))
            {
                if (currency.Equals("VND", StringComparison.OrdinalIgnoreCase) ||
                    currency.Equals("JPY", StringComparison.OrdinalIgnoreCase) ||
                    currency.Equals("KRW", StringComparison.OrdinalIgnoreCase))
                {
                    clean = clean.Replace(".", "");
                }
            }

            if (decimal.TryParse(clean, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal val))
                return val;

            return 0m;
        }

        /// <summary>
        /// Cập nhật hiển thị số tiền quy đổi sang VND và kiểm tra cảnh báo vượt quá số dư khả dụng.
        /// </summary>
        private void UpdateConvertedAmount()
        {
            if (cboLoaiTien == null || txtSoTien == null || lblQuyDoi == null) return;

            string currencyCode = "VND";
            if (cboLoaiTien.SelectedItem is CurrencyInfo ci && !string.IsNullOrEmpty(ci.MaLoaiTien))
            {
                currencyCode = ci.MaLoaiTien;
            }
            else if (cboLoaiTien.SelectedValue != null)
            {
                currencyCode = cboLoaiTien.SelectedValue.ToString();
            }

            decimal soTienGoc = ParseAmount(txtSoTien.Text, currencyCode);
            if (soTienGoc <= 0)
            {
                lblQuyDoi.Text = "Quy đổi: 0 VND";
                lblQuyDoi.ForeColor = Color.ForestGreen;
                lblQuyDoi.Tag = 0m;
                if (!CurrentSession.IsAdmin && btnSave != null) btnSave.Enabled = true;
                return;
            }

            decimal tyGia = 1m;
            if (cboLoaiTien.SelectedItem is CurrencyInfo ciItem && !string.IsNullOrEmpty(ciItem.MaLoaiTien))
            {
                if (ciItem.QuyDoi > 0) tyGia = ciItem.QuyDoi;
            }
            else
            {
                var matchedCurr = _currencies?.FirstOrDefault(c => string.Equals(c.MaLoaiTien, currencyCode, StringComparison.OrdinalIgnoreCase));
                if (matchedCurr != null && matchedCurr.QuyDoi > 0) tyGia = matchedCurr.QuyDoi;
            }

            if (currencyCode.Equals("USD", StringComparison.OrdinalIgnoreCase) && tyGia <= 1m)
            {
                tyGia = 25400m;
            }

            decimal soTienVND = soTienGoc * tyGia;
            lblQuyDoi.Tag = soTienVND;

            // KIỂM TRA SỐ DƯ REAL-TIME CHO USER: KHÔNG ĐƯỢC PHÉP VƯỢT QUÁ SỐ DƯ
            if (!CurrentSession.IsAdmin && !string.IsNullOrEmpty(_selectedSenderAccount))
            {
                decimal senderRate = _selectedSenderQuyDoi > 0 ? _selectedSenderQuyDoi : 1m;
                if (_selectedSenderCurrency.Equals("USD", StringComparison.OrdinalIgnoreCase) && senderRate <= 1m)
                {
                    senderRate = 25400m;
                }

                decimal availableVND = (_selectedSenderCurrency == "VND")
                    ? _selectedSenderBalance
                    : (_selectedSenderBalance * senderRate);

                if (soTienVND > availableVND)
                {
                    lblQuyDoi.ForeColor = Color.Crimson;
                    lblQuyDoi.Text = $"Quy đổi: {soTienVND:N0} VND  ⚠️ (VƯỢT QUÁ SỐ DƯ HIỆN TẠI: {availableVND:N0} VND)";
                    if (btnSave != null) btnSave.Enabled = false;
                    return;
                }
                else
                {
                    if (btnSave != null) btnSave.Enabled = true;
                }
            }

            lblQuyDoi.ForeColor = Color.ForestGreen;
            if (!currencyCode.Equals("VND", StringComparison.OrdinalIgnoreCase) && tyGia != 1m)
            {
                lblQuyDoi.Text = $"Quy đổi: {soTienVND:N0} VND  (Tỷ giá: 1 {currencyCode} = {tyGia:N0} VND)";
            }
            else
            {
                lblQuyDoi.Text = $"Quy đổi: {soTienVND:N0} VND";
            }
        }

        #endregion

        #region 6. THỰC HIỆN GIAO DỊCH (TRANSACTION EXECUTION)

        /// <summary>
        /// Xử lý nút "Thực hiện": Xác thực hợp lệ, gọi dịch vụ thực thi giao dịch và đóng Form khi thành công.
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string tkNhan = txtSoTaiKhoan.Text.Trim();
                string thaoTac = cboThaoTac.SelectedItem?.ToString() ?? "";

                string currencyCode = "VND";
                if (cboLoaiTien.SelectedItem is CurrencyInfo ci && !string.IsNullOrEmpty(ci.MaLoaiTien))
                {
                    currencyCode = ci.MaLoaiTien;
                }
                else if (cboLoaiTien.SelectedValue != null)
                {
                    currencyCode = cboLoaiTien.SelectedValue.ToString();
                }

                decimal soTienGoc = ParseAmount(txtSoTien.Text, currencyCode);
                if (soTienGoc <= 0)
                {
                    MessageBox.Show("Số tiền không hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoTien.Focus();
                    return;
                }

                string loaiTien = currencyCode;
                decimal quyDoi = 1m;
                if (cboLoaiTien.SelectedItem is CurrencyInfo curr && curr.QuyDoi > 0)
                {
                    quyDoi = curr.QuyDoi;
                }
                else
                {
                    var matchedCurr = _currencies?.FirstOrDefault(c => string.Equals(c.MaLoaiTien, currencyCode, StringComparison.OrdinalIgnoreCase));
                    if (matchedCurr != null && matchedCurr.QuyDoi > 0)
                        quyDoi = matchedCurr.QuyDoi;
                }

                if (currencyCode.Equals("USD", StringComparison.OrdinalIgnoreCase) && quyDoi <= 1m)
                {
                    quyDoi = 25400m;
                }
                decimal soTienVND = soTienGoc * quyDoi;

                string maGD = "GD" + DateTime.Now.ToString("yyyyMMddHHmmss");
                const string hardcodedTrangThai = "Success";

                // BƯỚC 1: XỬ LÝ CHO KHÁCH HÀNG (MEMBER) - GIAO DỊCH CHUYỂN TIỀN HAI CHIỀU
                if (!CurrentSession.IsAdmin)
                {
                    RefreshSenderBalance();

                    if (string.IsNullOrEmpty(_selectedSenderAccount))
                    {
                        MessageBox.Show("Bạn chưa có tài khoản ngân hàng nào để thực hiện giao dịch!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    decimal senderRate = _selectedSenderQuyDoi > 0 ? _selectedSenderQuyDoi : 1m;
                    if (_selectedSenderCurrency.Equals("USD", StringComparison.OrdinalIgnoreCase) && senderRate <= 1m)
                    {
                        senderRate = 25400m;
                    }

                    decimal senderVndBalance = (_selectedSenderCurrency == "VND")
                        ? _selectedSenderBalance
                        : (_selectedSenderBalance * senderRate);

                    if (soTienVND > senderVndBalance)
                    {
                        MessageBox.Show($"Số tiền giao dịch ({soTienVND:N0} VND) không được vượt quá số dư hiện tại ({senderVndBalance:N0} VND) của tài khoản!",
                            "Cảnh báo số dư", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSoTien.Focus();
                        return;
                    }

                    string danhMuc = cboDanhMuc?.SelectedItem?.ToString() ?? "Chi tiêu khác";
                    string noiDungUser = txtNoiDung.Text.Trim();

                    // Xử lý rút tiền mặt
                    if (thaoTac == "Rút tiền")
                    {
                        string noiDungRut = $"[{danhMuc}] Rút tiền mặt tại ATM" + (string.IsNullOrWhiteSpace(noiDungUser) ? "" : $" - {noiDungUser}");
                        bool ok = _transactionService.AddTransaction(
                            maGD, _selectedSenderAccount, 2, soTienGoc, loaiTien, soTienVND, noiDungRut, dtpNgay.Value, hardcodedTrangThai);

                        if (ok)
                        {
                            MessageBox.Show("Rút tiền thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Lỗi khi rút tiền!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return;
                    }

                    // Xử lý chuyển khoản hoặc thanh toán
                    if (string.IsNullOrWhiteSpace(tkNhan))
                    {
                        MessageBox.Show("Vui lòng nhập số tài khoản người nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSoTaiKhoan.Focus();
                        return;
                    }

                    if (tkNhan == _selectedSenderAccount)
                    {
                        MessageBox.Show("Tài khoản người nhận không được trùng với tài khoản chuyển tiền!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSoTaiKhoan.Focus();
                        return;
                    }

                    string recipientName = _transactionService.GetAccountOwnerName(tkNhan);
                    if (string.IsNullOrEmpty(recipientName))
                    {
                        MessageBox.Show("Số tài khoản người nhận không tồn tại trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSoTaiKhoan.Focus();
                        return;
                    }

                    string senderName = _transactionService.GetAccountOwnerName(_selectedSenderAccount);
                    string noiDungGui = $"[{danhMuc}] Chuyển tiền đến {tkNhan} ({recipientName})" + (string.IsNullOrWhiteSpace(noiDungUser) ? "" : $" - {noiDungUser}");
                    string noiDungNhan = $"Nhận tiền từ {_selectedSenderAccount} ({senderName})" + (string.IsNullOrWhiteSpace(noiDungUser) ? "" : $" - {noiDungUser}");

                    bool okTransfer = _transactionService.TransferMoney(
                        maGD, _selectedSenderAccount, tkNhan, soTienGoc, loaiTien, soTienVND,
                        noiDungGui, noiDungNhan, dtpNgay.Value, hardcodedTrangThai);

                    if (okTransfer)
                    {
                        MessageBox.Show("Thực hiện giao dịch thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi khi thực hiện giao dịch!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return;
                }

                // BƯỚC 2: XỬ LÝ CHO QUẢN TRỊ VIÊN (ADMIN) - GIAO DỊCH ĐƠN PHƯƠNG
                if (string.IsNullOrWhiteSpace(tkNhan))
                {
                    MessageBox.Show("Vui lòng nhập số tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoTaiKhoan.Focus();
                    return;
                }

                string ownerName = _transactionService.GetAccountOwnerName(tkNhan);
                if (string.IsNullOrEmpty(ownerName))
                {
                    MessageBox.Show("Số tài khoản không tồn tại trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoTaiKhoan.Focus();
                    return;
                }

                int maLoai = 1;
                if (cboLoaiGD != null && cboLoaiGD.SelectedValue != null && int.TryParse(cboLoaiGD.SelectedValue.ToString(), out int selectedMaLoai))
                {
                    maLoai = selectedMaLoai;
                }
                else
                {
                    if (thaoTac == "Nạp tiền") maLoai = 1;
                    else if (thaoTac == "Rút tiền") maLoai = 2;
                    else if (thaoTac == "Chuyển khoản" || thaoTac == "Thanh toán") maLoai = 4;
                }

                if ((thaoTac == "Chuyển khoản" || thaoTac == "Thanh toán") && maLoai == 3)
                {
                    maLoai = 4;
                }

                string noiDungAdmin = txtNoiDung.Text.Trim();
                if (string.IsNullOrWhiteSpace(noiDungAdmin))
                {
                    noiDungAdmin = thaoTac + " vào tài khoản";
                }

                bool success = _transactionService.AddTransaction(
                    maGD, tkNhan, maLoai, soTienGoc, loaiTien, soTienVND, noiDungAdmin, dtpNgay.Value, hardcodedTrangThai);

                if (success)
                {
                    MessageBox.Show("Thực hiện giao dịch thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Lỗi khi thêm giao dịch!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfirmCancel()
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy bỏ thao tác này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        #endregion

        #region 7. VẼ BO TRÒN VIỀN FORM (GDI+ DRAWING)

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            GraphicsPath path = new GraphicsPath();
            int radius = 15;
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(this.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(this.Width - radius, this.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, this.Height - radius, radius, radius, 90, 90);
            this.Region = new Region(path);

            using (Pen pen = new Pen(Color.LightGray, 1))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        #endregion
    }
}
