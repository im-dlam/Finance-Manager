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
        private DateTimePicker dtpNgay;
        private TextBox txtNoiDung;
        private ComboBox cboTaiKhoan;
        private ComboBox cboLoaiGD;
        private TextBox txtSoTien;
        private ComboBox cboThaoTac;
        private ComboBox cboLoaiTien;
        private Button btnSave;
        private Button btnCancel;
        
        // Them label so du va quy doi
        private Label lblSoDu;
        private Label lblQuyDoi;
        private bool _isSyncing = false;

        public AddTransactionForm(List<CurrencyInfo> currencies)
        {
            _transactionService = new TransactionService();
            _currencies = currencies;

            InitializeCustomComponent();
            LoadDropdowns();
        }

        private void InitializeCustomComponent()
        {
            this.Text = "Thêm giao dịch mới";
            this.Size = new Size(500, 650);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            this.SuspendLayout();

            // Header (Blue Header)
            Panel pnlHeader = new Panel();
            pnlHeader.BackColor = Color.FromArgb(51, 102, 255);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 50;
            this.Controls.Add(pnlHeader);

            lblHeader = new Label();
            lblHeader.Text = "Thêm giao dịch mới";
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblHeader.AutoSize = true;
            lblHeader.Location = new Point(15, 12);
            pnlHeader.Controls.Add(lblHeader);

            int startY = 70;
            int gapY = 50;
            int labelX = 20;
            int inputX = 140;
            int inputWidth = 330;

            // 1. Ngày giao dịch
            AddLabel("Ngày giao dịch:", labelX, startY);
            dtpNgay = new DateTimePicker();
            dtpNgay.Format = DateTimePickerFormat.Short;
            dtpNgay.Location = new Point(inputX, startY);
            dtpNgay.Width = inputWidth;
            dtpNgay.Font = new Font("Segoe UI", 10);
            this.Controls.Add(dtpNgay);

            // 2. Nội dung
            startY += gapY;
            AddLabel("Nội dung:", labelX, startY);
            txtNoiDung = new TextBox();
            txtNoiDung.Multiline = true;
            txtNoiDung.Location = new Point(inputX, startY);
            txtNoiDung.Width = inputWidth;
            txtNoiDung.Height = 60;
            txtNoiDung.Font = new Font("Segoe UI", 10);
            this.Controls.Add(txtNoiDung);

            // 3. Tài khoản
            startY += gapY + 25;
            AddLabel("Tài khoản:", labelX, startY);
            cboTaiKhoan = new ComboBox();
            cboTaiKhoan.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTaiKhoan.Location = new Point(inputX, startY);
            cboTaiKhoan.Width = inputWidth;
            cboTaiKhoan.Font = new Font("Segoe UI", 10);
            cboTaiKhoan.SelectedIndexChanged += (s, e) => { UpdateBalance(); };
            this.Controls.Add(cboTaiKhoan);
            
            // Label So Du
            startY += 30;
            lblSoDu = new Label();
            lblSoDu.Text = "Số dư hiện tại: 0 VND";
            lblSoDu.ForeColor = Color.DarkGray;
            lblSoDu.Font = new Font("Segoe UI", 9, FontStyle.Italic);
            lblSoDu.Location = new Point(inputX, startY);
            lblSoDu.AutoSize = true;
            this.Controls.Add(lblSoDu);

            // 4. Loại giao dịch (Thu/Chi)
            startY += 30;
            AddLabel("Loại:", labelX, startY);
            cboLoaiGD = new ComboBox();
            cboLoaiGD.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiGD.Location = new Point(inputX, startY);
            cboLoaiGD.Width = inputWidth;
            cboLoaiGD.Font = new Font("Segoe UI", 10);
            cboLoaiGD.SelectedIndexChanged += (s, e) => { OnLoaiGDChanged(); };
            this.Controls.Add(cboLoaiGD);

            // Loại Tiền
            startY += gapY;
            AddLabel("Loại tiền:", labelX, startY);
            cboLoaiTien = new ComboBox();
            //cboLoaiTien.Items.AddRange(new string[] { "VND", "USD", "EUR", "GBP", "JPY" });
            cboLoaiTien.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiTien.Location = new Point(inputX, startY);
            cboLoaiTien.Width = inputWidth;
            cboLoaiTien.Font = new Font("Segoe UI", 10);
            //cboLoaiTien.SelectedIndex = 0;
            cboLoaiTien.SelectedIndexChanged += (s, e) => { UpdateConvertedAmount(); };
            this.Controls.Add(cboLoaiTien);

            // 5. Số tiền
            startY += gapY;
            AddLabel("Số tiền gốc:", labelX, startY);
            txtSoTien = new TextBox();
            txtSoTien.Location = new Point(inputX, startY);
            txtSoTien.Width = inputWidth;
            txtSoTien.Font = new Font("Segoe UI", 10);
            txtSoTien.Text = "0";
            txtSoTien.TextAlign = HorizontalAlignment.Left;
            txtSoTien.TextChanged += (s, e) => { UpdateConvertedAmount(); };
            this.Controls.Add(txtSoTien);
            
            // Label Quy Doi
            startY += 30;
            lblQuyDoi = new Label();
            lblQuyDoi.Text = "Quy đổi: 0 VND";
            lblQuyDoi.ForeColor = Color.ForestGreen;
            lblQuyDoi.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblQuyDoi.Location = new Point(inputX, startY);
            lblQuyDoi.AutoSize = true;
            this.Controls.Add(lblQuyDoi);

            // 6. Thao tác
            startY += 40;
            AddLabel("Thao tác:", labelX, startY);
            cboThaoTac = new ComboBox();
            cboThaoTac.Items.AddRange(new string[] { "Nạp tiền", "Rút tiền", "Chuyển khoản", "Thanh toán" });
            cboThaoTac.DropDownStyle = ComboBoxStyle.DropDownList;
            cboThaoTac.Location = new Point(inputX, startY);
            cboThaoTac.Width = inputWidth;
            cboThaoTac.Font = new Font("Segoe UI", 10);
            cboThaoTac.SelectedIndex = 0;
            cboThaoTac.SelectedIndexChanged += (s, e) => { OnThaoTacChanged(); };
            this.Controls.Add(cboThaoTac);

            // Buttons
            startY += gapY + 20;
            
            btnSave = new Button();
            btnSave.Text = "Thêm";
            btnSave.BackColor = Color.FromArgb(51, 102, 255);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Size = new Size(80, 35);
            btnSave.Location = new Point(300, startY);
            btnSave.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnSave.Cursor = Cursors.Hand;
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            btnCancel = new Button();
            btnCancel.Text = "Hủy bỏ";
            btnCancel.BackColor = Color.White;
            btnCancel.ForeColor = Color.Black;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderColor = Color.LightGray;
            btnCancel.Size = new Size(80, 35);
            btnCancel.Location = new Point(390, startY);
            btnCancel.Font = new Font("Segoe UI", 10);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Click += (s, e) => { ConfirmCancel(); };
            this.Controls.Add(btnCancel);

            this.ResumeLayout(false);
        }

        private void AddLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Location = new Point(x, y + 3);
            lbl.Font = new Font("Segoe UI", 10);
            lbl.AutoSize = true;
            this.Controls.Add(lbl);
        }

        private void LoadDropdowns()
        {
            try
            {
                cboTaiKhoan.DataSource = _transactionService.GetAccountsList();
                cboTaiKhoan.DisplayMember = "HienThi";
                cboTaiKhoan.ValueMember = "SoTaiKhoan";

                cboLoaiGD.DataSource = _transactionService.GetTransactionTypes();
                cboLoaiGD.DisplayMember = "TenLoaiGD";
                cboLoaiGD.ValueMember = "MaLoaiGD";

                cboLoaiTien.DataSource = _currencies;
                cboLoaiTien.DisplayMember = "TenLoaiTien";
                cboLoaiTien.ValueMember = "MaLoaiTien";

                // Khởi tạo đồng bộ ban đầu theo Thao tác
                OnThaoTacChanged();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải dữ liệu giao dịch: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void OnThaoTacChanged()
        {
            if (_isSyncing) return;
            _isSyncing = true;
            try
            {
                string thaoTac = cboThaoTac.SelectedItem?.ToString() ?? "";
                bool isDeposit = thaoTac.Equals("Nạp tiền", StringComparison.OrdinalIgnoreCase);

                // Khi nạp tiền: KHÔNG CẦN SET / HIỂN THỊ SỐ DƯ
                if (isDeposit)
                {
                    lblSoDu.Visible = false;
                }
                else
                {
                    lblSoDu.Visible = true;
                    UpdateBalance();
                }

                // Đồng bộ sang cboLoaiGD
                string target = isDeposit ? "Nạp tiền" : (thaoTac == "Rút tiền" ? "Rút tiền" : "Chuyển khoản");
                for (int i = 0; i < cboLoaiGD.Items.Count; i++)
                {
                    if (cboLoaiGD.Items[i] is DataRowView row)
                    {
                        if (row["TenLoaiGD"].ToString().Equals(target, StringComparison.OrdinalIgnoreCase))
                        {
                            cboLoaiGD.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            finally
            {
                _isSyncing = false;
            }
        }

        private void OnLoaiGDChanged()
        {
            if (_isSyncing) return;
            _isSyncing = true;
            try
            {
                if (cboLoaiGD.SelectedItem is DataRowView row)
                {
                    string tenLoai = row["TenLoaiGD"]?.ToString() ?? "";
                    bool isDeposit = tenLoai.Equals("Nạp tiền", StringComparison.OrdinalIgnoreCase);

                    if (isDeposit)
                    {
                        lblSoDu.Visible = false;
                        if (cboThaoTac.SelectedItem?.ToString() != "Nạp tiền")
                            cboThaoTac.SelectedItem = "Nạp tiền";
                    }
                    else
                    {
                        lblSoDu.Visible = true;
                        UpdateBalance();
                        string target = (tenLoai.Equals("Rút tiền", StringComparison.OrdinalIgnoreCase)) ? "Rút tiền" : "Chuyển khoản";
                        if (cboThaoTac.SelectedItem?.ToString() != target)
                            cboThaoTac.SelectedItem = target;
                    }
                }
            }
            finally
            {
                _isSyncing = false;
            }
        }

        /// <summary>
        /// Update the balance currency when the selected account changes.
        /// </summary>
        private void UpdateBalance()
        {
            if (cboTaiKhoan.SelectedValue != null &&
                cboTaiKhoan.SelectedValue is string tk)
            {
                decimal bal =
                    _transactionService.GetAccountBalance(tk);

                CurrencyInfo currency =
                    _transactionService.GetAccountCurrency(tk);

                if (currency != null)
                {
                    lblSoDu.Text =
                        $"Số dư hiện tại: {bal:N2} {currency.MaLoaiTien}";

                    lblSoDu.Tag = bal;

                    if (cboLoaiTien.DataSource != null)
                    {
                        cboLoaiTien.SelectedValue = currency.MaLoaiTien;
                    }
                }

                string thaoTac = cboThaoTac?.SelectedItem?.ToString() ?? "";
                if (thaoTac.Equals("Nạp tiền", StringComparison.OrdinalIgnoreCase))
                {
                    lblSoDu.Visible = false;
                }
                else
                {
                    lblSoDu.Visible = true;
                }
            }
        }

        private void UpdateConvertedAmount()
        {
            decimal soTienGoc;

            if (!decimal.TryParse(
                txtSoTien.Text.Replace(",", "").Replace(".", ""),
                out soTienGoc))
            {
                lblQuyDoi.Text = "Quy đổi: 0 VND";
                lblQuyDoi.Tag = 0m;
                return;
            }

            CurrencyInfo selectedCurrency =
                cboLoaiTien.SelectedItem as CurrencyInfo;

            if (selectedCurrency == null)
            {
                lblQuyDoi.Text = "Quy đổi: 0 VND";
                lblQuyDoi.Tag = 0m;
                return;
            }

            decimal rate = (selectedCurrency.MaLoaiTien.Equals("USD", StringComparison.OrdinalIgnoreCase) && selectedCurrency.QuyDoi <= 1m) ? 25400m : (selectedCurrency.QuyDoi > 0 ? selectedCurrency.QuyDoi : 1m);
            decimal soTienVND = soTienGoc * rate;

            lblQuyDoi.Text =
                $"Quy đổi: {soTienVND:N0} VND";

            lblQuyDoi.Tag = soTienVND;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboTaiKhoan.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn tài khoản!"); return;
                }
                
                string tk = cboTaiKhoan.SelectedValue.ToString();
                decimal soTienGoc = 0;
                
                if (!decimal.TryParse(txtSoTien.Text.Replace(",", "").Replace(".", ""), out soTienGoc) || soTienGoc <= 0)
                {
                    MessageBox.Show("Số tiền gốc không hợp lệ!"); return;
                }

                CurrencyInfo selectedCurrency =
                    cboLoaiTien.SelectedItem as CurrencyInfo;

                if (selectedCurrency == null)
                {
                    MessageBox.Show("Vui lòng chọn loại tiền!");
                    return;
                }

                string loaiTien = selectedCurrency.MaLoaiTien;

                decimal rate = (selectedCurrency.MaLoaiTien.Equals("USD", StringComparison.OrdinalIgnoreCase) && selectedCurrency.QuyDoi <= 1m) ? 25400m : (selectedCurrency.QuyDoi > 0 ? selectedCurrency.QuyDoi : 1m);
                decimal soTienVND = soTienGoc * rate;

                // Kiểm tra thao tác
                if (cboThaoTac.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn thao tác!"); return;
                }
                string thaoTac = cboThaoTac.SelectedItem.ToString();
                bool isDeposit = thaoTac.Equals("Nạp tiền", StringComparison.OrdinalIgnoreCase);

                // Lấy đúng mã loại giao dịch (MaLoaiGD) tương ứng với thao tác
                int maLoai = 0;
                if (isDeposit)
                {
                    maLoai = _transactionService.GetTransactionTypeIdByName("Nạp tiền");
                }
                else if (thaoTac.Equals("Rút tiền", StringComparison.OrdinalIgnoreCase))
                {
                    maLoai = _transactionService.GetTransactionTypeIdByName("Rút tiền");
                }
                else
                {
                    maLoai = _transactionService.GetTransactionTypeIdByName("Chuyển khoản");
                }

                if (maLoai <= 0 && cboLoaiGD.SelectedValue != null)
                {
                    maLoai = Convert.ToInt32(cboLoaiGD.SelectedValue);
                }

                // ============================================================
                //  KIỂM TRA SỐ DƯ:
                //  - Khi chọn NẠP TIỀN: Tuyệt đối KHÔNG xét số dư (bỏ qua).
                //  - Khi RÚT TIỀN / CHUYỂN KHOẢN / THANH TOÁN: Mới xét số dư.
                // ============================================================
                if (!isDeposit)
                {
                    decimal currentBal = lblSoDu.Tag != null ? (decimal)lblSoDu.Tag : 0m;
                    BankAccountManagement.Models.CurrencyInfo accCurrency = _transactionService.GetAccountCurrency(tk);
                    decimal accRate = accCurrency != null ? accCurrency.QuyDoi : 1m;
                    if (accCurrency != null && accCurrency.MaLoaiTien.Equals("USD", StringComparison.OrdinalIgnoreCase) && accRate <= 1m)
                        accRate = 25400m;

                    decimal currentBalVND = currentBal * accRate;

                    if (soTienVND > currentBalVND)
                    {
                        MessageBox.Show("Số tiền quy đổi vượt quá số dư hiện tại trong tài khoản!", "Lỗi số dư", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                string maGD = "GD" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string hardcodedTrangThai = "Success";

                bool success = _transactionService.AddTransaction(
                    maGD, tk, maLoai, soTienGoc, loaiTien, soTienVND, txtNoiDung.Text, dtpNgay.Value, hardcodedTrangThai);

                if (success)
                {
                    MessageBox.Show("Thêm giao dịch thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Lỗi: " + ex.Message);
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

            Pen pen = new Pen(Color.LightGray, 1);
            e.Graphics.DrawPath(pen, path);
        }
    }
}


