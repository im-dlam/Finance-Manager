using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BankAccountManagement
{
    public class AddTransactionForm : Form
    {
        private TransactionService _transactionService;
        private List<CurrencyInfo> _currencies;

        private Label lblHeader;

        // 1. Trên số dư là tài khoản ngân hàng của mình (User)
        private Label lblMyAccount;
        private ComboBox cboMyAccount;
        private Label lblSoDuThucTe;
        private TextBox txtSoDuThucTe;

        private ComboBox cboRecentTransfers;
        private DateTimePicker dtpNgay;
        private TextBox txtNoiDung;

        // 2. Ngân hàng người nhận (Lấy tất cả ngân hàng của admin)
        private Label lblNganHang;
        private ComboBox cboNganHang;
        private TextBox txtSoTaiKhoan;
        private TextBox txtNguoiNhan;

        private ComboBox cboThaoTac;
        private Label lblLoaiGD;
        private ComboBox cboLoaiGD;

        // Danh mục cho User
        private Label lblDanhMuc;
        private ComboBox cboDanhMuc;

        // 3. Ở dưới trước nhập số tiền có loại tiền là vnd hay usd
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

        public AddTransactionForm(List<CurrencyInfo> currencies)
        {
            _transactionService = new TransactionService();
            _currencies = currencies;

            InitializeCustomComponent();
            LoadDropdowns();
            LoadActualBalance();
        }

        private void InitializeCustomComponent()
        {
            this.Text = "Thêm giao dịch mới";
            int formHeight = CurrentSession.IsAdmin ? 520 : 590;
            this.Size = new Size(540, formHeight);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            this.SuspendLayout();

            // 1. Header (Blue Header)
            Panel pnlHeader = new Panel();
            pnlHeader.BackColor = Color.FromArgb(51, 102, 255);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 50;
            this.Controls.Add(pnlHeader);

            lblHeader = new Label();
            lblHeader.Text = "Thêm giao dịch mới";
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblHeader.AutoSize = true;
            lblHeader.Location = new Point(15, 12);
            pnlHeader.Controls.Add(lblHeader);

            int startY = 62;
            int gapY = 36;
            int labelX = 22;
            int inputX = 175;
            int inputWidth = 335;

            // 2. Trên số dư là tài khoản ngân hàng của mình (Chỉ User mới có)
            if (!CurrentSession.IsAdmin)
            {
                lblMyAccount = AddLabel("Tài khoản của mình:", labelX, startY);
                cboMyAccount = new ComboBox();
                cboMyAccount.DropDownStyle = ComboBoxStyle.DropDownList;
                cboMyAccount.Location = new Point(inputX, startY);
                cboMyAccount.Width = inputWidth;
                cboMyAccount.Font = new Font("Segoe UI", 9.5F);
                cboMyAccount.SelectedIndexChanged += CboMyAccount_SelectedIndexChanged;
                this.Controls.Add(cboMyAccount);

                startY += gapY;

                lblSoDuThucTe = AddLabel("Số dư thực tế:", labelX, startY);
                txtSoDuThucTe = new TextBox();
                txtSoDuThucTe.ReadOnly = true;
                txtSoDuThucTe.BackColor = Color.FromArgb(243, 244, 246);
                txtSoDuThucTe.ForeColor = Color.FromArgb(16, 185, 129); // Xanh lá cây tươi đậm
                txtSoDuThucTe.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
                txtSoDuThucTe.Location = new Point(inputX, startY);
                txtSoDuThucTe.Width = inputWidth;
                txtSoDuThucTe.Text = "0 VND";
                this.Controls.Add(txtSoDuThucTe);

                startY += gapY;
            }

            // 3. Chọn nhanh từ giao dịch mẫu đã chuyển thành công trước đó
            AddLabel("GD mẫu trước:", labelX, startY);
            cboRecentTransfers = new ComboBox();
            cboRecentTransfers.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRecentTransfers.Location = new Point(inputX, startY);
            cboRecentTransfers.Width = inputWidth;
            cboRecentTransfers.Font = new Font("Segoe UI", 9.5F);
            cboRecentTransfers.SelectedIndexChanged += CboRecentTransfers_SelectedIndexChanged;
            this.Controls.Add(cboRecentTransfers);

            // 4. Ngày giao dịch
            startY += gapY;
            AddLabel("Ngày giao dịch:", labelX, startY);
            dtpNgay = new DateTimePicker();
            dtpNgay.Format = DateTimePickerFormat.Short;
            dtpNgay.Location = new Point(inputX, startY);
            dtpNgay.Width = inputWidth;
            dtpNgay.Font = new Font("Segoe UI", 9.5F);
            this.Controls.Add(dtpNgay);

            // 5. Nội dung
            startY += gapY;
            AddLabel("Nội dung:", labelX, startY);
            txtNoiDung = new TextBox();
            txtNoiDung.Multiline = true;
            txtNoiDung.Location = new Point(inputX, startY);
            txtNoiDung.Width = inputWidth;
            txtNoiDung.Height = 40;
            txtNoiDung.Font = new Font("Segoe UI", 9.5F);
            this.Controls.Add(txtNoiDung);

            // 6. Ngân hàng người nhận (Lấy tất cả ngân hàng của admin)
            startY += 46;
            lblNganHang = AddLabel(CurrentSession.IsAdmin ? "Ngân hàng:" : "Ngân hàng người nhận:", labelX, startY);
            cboNganHang = new ComboBox();
            cboNganHang.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNganHang.Location = new Point(inputX, startY);
            cboNganHang.Width = inputWidth;
            cboNganHang.Font = new Font("Segoe UI", 9.5F);
            this.Controls.Add(cboNganHang);

            // 7. Số tài khoản
            startY += gapY;
            AddLabel("Số tài khoản:", labelX, startY);
            txtSoTaiKhoan = new TextBox();
            txtSoTaiKhoan.Location = new Point(inputX, startY);
            txtSoTaiKhoan.Width = inputWidth;
            txtSoTaiKhoan.Font = new Font("Segoe UI", 9.5F);
            txtSoTaiKhoan.TextChanged += TxtSoTaiKhoan_TextChanged;
            txtSoTaiKhoan.Leave += TxtSoTaiKhoan_Leave;
            this.Controls.Add(txtSoTaiKhoan);

            // 8. Tên người nhận
            startY += gapY;
            AddLabel("Tên người nhận:", labelX, startY);
            txtNguoiNhan = new TextBox();
            txtNguoiNhan.ReadOnly = true;
            txtNguoiNhan.BackColor = Color.FromArgb(243, 244, 246);
            txtNguoiNhan.ForeColor = Color.FromArgb(16, 185, 129);
            txtNguoiNhan.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            txtNguoiNhan.Location = new Point(inputX, startY);
            txtNguoiNhan.Width = inputWidth;
            this.Controls.Add(txtNguoiNhan);

            // 9. Thao tác
            startY += gapY;
            AddLabel("Thao tác:", labelX, startY);
            cboThaoTac = new ComboBox();
            cboThaoTac.DropDownStyle = ComboBoxStyle.DropDownList;
            cboThaoTac.Location = new Point(inputX, startY);
            cboThaoTac.Width = inputWidth;
            cboThaoTac.Font = new Font("Segoe UI", 9.5F);
            cboThaoTac.SelectedIndexChanged += CboThaoTac_SelectedIndexChanged;
            this.Controls.Add(cboThaoTac);

            // 10. Loại giao dịch (Admin) hoặc Danh mục (User)
            if (CurrentSession.IsAdmin)
            {
                startY += gapY;
                lblLoaiGD = AddLabel("Loại:", labelX, startY);
                cboLoaiGD = new ComboBox();
                cboLoaiGD.DropDownStyle = ComboBoxStyle.DropDownList;
                cboLoaiGD.Location = new Point(inputX, startY);
                cboLoaiGD.Width = inputWidth;
                cboLoaiGD.Font = new Font("Segoe UI", 9.5F);
                cboLoaiGD.SelectedIndexChanged += CboLoaiGD_SelectedIndexChanged;
                this.Controls.Add(cboLoaiGD);
            }
            else
            {
                startY += gapY;
                lblDanhMuc = AddLabel("Danh mục:", labelX, startY);
                cboDanhMuc = new ComboBox();
                cboDanhMuc.DropDownStyle = ComboBoxStyle.DropDownList;
                cboDanhMuc.Location = new Point(inputX, startY);
                cboDanhMuc.Width = inputWidth;
                cboDanhMuc.Font = new Font("Segoe UI", 9.5F);
                this.Controls.Add(cboDanhMuc);
            }

            // 11. Ở dưới trước nhập số tiền có loại tiền là vnd hay usd (Cả User lẫn Admin)
            startY += gapY;
            lblLoaiTien = AddLabel("Loại tiền:", labelX, startY);
            cboLoaiTien = new ComboBox();
            cboLoaiTien.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiTien.Location = new Point(inputX, startY);
            cboLoaiTien.Width = inputWidth;
            cboLoaiTien.Font = new Font("Segoe UI", 9.5F);
            cboLoaiTien.SelectedIndexChanged += (s, e) => { UpdateConvertedAmount(); };
            this.Controls.Add(cboLoaiTien);

            // 12. Số tiền gốc
            startY += gapY;
            AddLabel("Số tiền gốc:", labelX, startY);
            txtSoTien = new TextBox();
            txtSoTien.Location = new Point(inputX, startY);
            txtSoTien.Width = inputWidth;
            txtSoTien.Font = new Font("Segoe UI", 9.5F);
            txtSoTien.Text = "0";
            txtSoTien.TextAlign = HorizontalAlignment.Left;
            txtSoTien.TextChanged += (s, e) => { UpdateConvertedAmount(); };
            this.Controls.Add(txtSoTien);

            // Label Quy Đổi
            startY += 26;
            lblQuyDoi = new Label();
            lblQuyDoi.Text = "Quy đổi: 0 VND";
            lblQuyDoi.ForeColor = Color.ForestGreen;
            lblQuyDoi.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            lblQuyDoi.Location = new Point(inputX, startY);
            lblQuyDoi.AutoSize = true;
            this.Controls.Add(lblQuyDoi);

            // Buttons
            startY += 30;
            btnSave = new Button();
            btnSave.Text = "Thực hiện";
            btnSave.BackColor = Color.FromArgb(51, 102, 255);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Size = new Size(105, 36);
            btnSave.Location = new Point(300, startY);
            btnSave.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSave.Cursor = Cursors.Hand;
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            btnCancel = new Button();
            btnCancel.Text = "Hủy bỏ";
            btnCancel.BackColor = Color.White;
            btnCancel.ForeColor = Color.Black;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderColor = Color.LightGray;
            btnCancel.Size = new Size(95, 36);
            btnCancel.Location = new Point(415, startY);
            btnCancel.Font = new Font("Segoe UI", 9.5F);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Click += (s, e) => { ConfirmCancel(); };
            this.Controls.Add(btnCancel);

            this.ResumeLayout(false);
        }

        private Label AddLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Location = new Point(x, y + 4);
            lbl.Font = new Font("Segoe UI", 9.5F);
            lbl.AutoSize = true;
            this.Controls.Add(lbl);
            return lbl;
        }

        private void LoadDropdowns()
        {
            try
            {
                // 1. Tài khoản của mình (User)
                if (!CurrentSession.IsAdmin && cboMyAccount != null)
                {
                    DataTable dtMyAccounts = _transactionService.GetUserAccountsWithBankInfo(CurrentSession.MaKH);
                    cboMyAccount.DataSource = dtMyAccounts;
                    cboMyAccount.DisplayMember = "HienThi";
                    cboMyAccount.ValueMember = "SoTaiKhoan";
                    if (dtMyAccounts != null && dtMyAccounts.Rows.Count > 0)
                    {
                        cboMyAccount.SelectedIndex = 0;
                        // 1 người dùng chỉ có 1 tài khoản ngân hàng: tự động lấy luôn và khóa combobox
                        cboMyAccount.Enabled = false;
                    }
                    UpdateSenderAccountFromSelection();
                }

                // 2. Ngân hàng người nhận: LẤY TẤT CẢ NGÂN HÀNG CỦA ADMIN
                DataTable dtAllBanks = _transactionService.GetBanksList();
                cboNganHang.DataSource = dtAllBanks;
                cboNganHang.DisplayMember = "HienThi";
                cboNganHang.ValueMember = "MaNganHang";

                // 3. Thao tác: Admin có "Nạp tiền", User thì bỏ "Nạp tiền"
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

                // 4. Danh sách loại giao dịch (Chỉ khi là Admin)
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

                // 5. Danh mục (User)
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

                // 6. Loại tiền: VND hay USD (CẢ USER VÀ ADMIN ĐỀU CÓ)
                cboLoaiTien.DataSource = new List<CurrencyInfo>(_currencies);
                cboLoaiTien.DisplayMember = "MaLoaiTien";
                cboLoaiTien.ValueMember = "MaLoaiTien";
                try { cboLoaiTien.SelectedValue = "VND"; } catch { }

                // 7. GD mẫu trước
                LoadRecentTransfers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải dữ liệu: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CboMyAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSenderAccountFromSelection();
        }

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
                if (_selectedSenderCurrency.Equals("USD", StringComparison.OrdinalIgnoreCase) && _selectedSenderQuyDoi <= 1)
                {
                    _selectedSenderQuyDoi = 25400m;
                }

                if (txtSoDuThucTe != null)
                {
                    decimal displayVND = (_selectedSenderCurrency == "VND")
                        ? _selectedSenderBalance
                        : (_selectedSenderBalance * _selectedSenderQuyDoi);
                    txtSoDuThucTe.Text = $"{displayVND:N0} VND";
                }

                // Nếu đang ở thao tác Rút tiền thì tự động điền STK của chính mình
                string tt = cboThaoTac?.SelectedItem?.ToString() ?? "";
                if (tt == "Rút tiền")
                {
                    txtSoTaiKhoan.Text = _selectedSenderAccount;
                    txtNguoiNhan.Text = CurrentSession.HoTen ?? "";
                }

                UpdateConvertedAmount();
            }
        }

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
                    _selectedSenderQuyDoi = (cur.MaLoaiTien.Equals("USD", StringComparison.OrdinalIgnoreCase) && cur.QuyDoi <= 1) ? 25400m : cur.QuyDoi;
                }
                if (txtSoDuThucTe != null)
                {
                    decimal displayVND = (_selectedSenderCurrency == "VND")
                        ? _selectedSenderBalance
                        : (_selectedSenderBalance * _selectedSenderQuyDoi);
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
            catch
            {
                // Bỏ qua nếu lỗi
            }
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
                    // Với User: tìm STK nhận từ nội dung chuyển tiền trước đó
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

                    // Chọn tài khoản nguồn của mình
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

                // Cập nhật tên người nhận
                string ownerName = _transactionService.GetAccountOwnerName(txtSoTaiKhoan.Text.Trim());
                if (!string.IsNullOrEmpty(ownerName))
                {
                    txtNguoiNhan.Text = ownerName;
                    txtNguoiNhan.ForeColor = Color.FromArgb(16, 185, 129);
                }

                UpdateConvertedAmount();
            }
        }

        private void TxtSoTaiKhoan_TextChanged(object sender, EventArgs e)
        {
            string stk = txtSoTaiKhoan.Text.Trim();
            if (string.IsNullOrEmpty(stk))
            {
                txtNguoiNhan.Text = "";
                return;
            }

            // Tự động nhận diện tên người nhận
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

            // Tự động nhận diện ngân hàng người nhận
            string bankCode = _transactionService.GetAccountBank(stk);
            if (!string.IsNullOrEmpty(bankCode) && cboNganHang != null)
            {
                cboNganHang.SelectedValue = bankCode;
            }

            // Nếu là Admin thì tự động cập nhật loại tiền tệ
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
                    else if (tt == "Chuyển khoản" || tt == "Thanh toán")
                    {
                        // Khi Admin thực hiện thao tác Chuyển khoản đến số tài khoản người nhận,
                        // đối với tài khoản đích đó thì đây là khoản THU (Nhận chuyển khoản - MaLoaiGD = 4)
                        cboLoaiGD.SelectedValue = 4;
                    }
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

        private decimal ParseAmount(string text, string currency)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0m;
            string clean = text.Trim();
            if (currency.Equals("USD", StringComparison.OrdinalIgnoreCase))
            {
                clean = clean.Replace(",", "");
                if (decimal.TryParse(clean, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal val))
                    return val;
            }
            else
            {
                clean = clean.Replace(".", "").Replace(",", "");
                if (decimal.TryParse(clean, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal val))
                    return val;
            }
            return 0m;
        }

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
            if (currencyCode.Equals("USD", StringComparison.OrdinalIgnoreCase))
            {
                tyGia = 25400m;
                if (cboLoaiTien.SelectedItem is CurrencyInfo curr && curr.QuyDoi > 1)
                {
                    tyGia = curr.QuyDoi;
                }
            }

            decimal soTienVND = soTienGoc * tyGia;
            lblQuyDoi.Tag = soTienVND;

            // KIỂM TRA SỐ DƯ REAL-TIME CHO USER: MỌI GIAO DỊCH KHÔNG THỂ QUÁ SỐ DƯ TÀI KHOẢN
            if (!CurrentSession.IsAdmin && !string.IsNullOrEmpty(_selectedSenderAccount))
            {
                decimal availableVND = (_selectedSenderCurrency == "VND")
                    ? _selectedSenderBalance
                    : (_selectedSenderBalance * (_selectedSenderQuyDoi > 1 ? _selectedSenderQuyDoi : 25400m));

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
            if (currencyCode.Equals("USD", StringComparison.OrdinalIgnoreCase))
            {
                lblQuyDoi.Text = $"Quy đổi: {soTienVND:N0} VND  (Tỷ giá: 1 USD = {tyGia:N0} VND)";
            }
            else
            {
                lblQuyDoi.Text = $"Quy đổi: {soTienGoc:N0} VND";
            }
        }

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
                if (currencyCode.Equals("USD", StringComparison.OrdinalIgnoreCase))
                {
                    if (cboLoaiTien.SelectedItem is CurrencyInfo curr && curr.QuyDoi > 1)
                        quyDoi = curr.QuyDoi;
                    else
                        quyDoi = 25400m;
                }
                decimal soTienVND = soTienGoc * quyDoi;

                string maGD = "GD" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string hardcodedTrangThai = "Success";

                // ========================================================
                // 1. KHI ĐĂNG NHẬP LÀ USER (GIAO DỊCH 2 BÊN)
                // ========================================================
                if (!CurrentSession.IsAdmin)
                {
                    RefreshSenderBalance(); // Cập nhật số dư mới nhất từ DB

                    if (string.IsNullOrEmpty(_selectedSenderAccount))
                    {
                        MessageBox.Show("Bạn chưa có tài khoản ngân hàng nào để thực hiện giao dịch!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    decimal senderVndBalance = (_selectedSenderCurrency == "VND")
                        ? _selectedSenderBalance
                        : (_selectedSenderBalance * (_selectedSenderQuyDoi > 1 ? _selectedSenderQuyDoi : 25400m));

                    // Kiểm tra số dư tài khoản gửi của User: MỌI GIAO DỊCH KHÔNG THỂ QUÁ SỐ DƯ TÀI KHOẢN
                    if (soTienVND > senderVndBalance)
                    {
                        MessageBox.Show($"Số tiền giao dịch ({soTienVND:N0} VND) không được vượt quá số dư hiện tại ({senderVndBalance:N0} VND) của tài khoản!", 
                            "Cảnh báo số dư", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSoTien.Focus();
                        return;
                    }

                    string danhMuc = cboDanhMuc?.SelectedItem?.ToString() ?? "Chi tiêu khác";
                    string noiDungUser = txtNoiDung.Text.Trim();

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

                    // Chuyển khoản hoặc Thanh toán
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

                // ========================================================
                // 2. KHI ĐĂNG NHẬP LÀ ADMIN (GIỮ NGUYÊN FULL QUYỀN)
                // ========================================================
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

                // Khi Admin chuyển tiền tới tài khoản này, tài khoản này là bên nhận tiền (Thu),
                // vì vậy nếu thao tác là Chuyển khoản và maLoai đang là 3 (Chi) thì phải đổi thành 4 (Nhận chuyển khoản - Thu)
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
    }
}