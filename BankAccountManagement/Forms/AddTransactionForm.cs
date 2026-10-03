using BankAccountManagement.Services;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BankAccountManagement
{
    public class AddTransactionForm : Form
    {
        private TransactionService _transactionService;
        
        // Controls
        private DateTimePicker dtpNgay;
        private TextBox txtNoiDung;
        private ComboBox cboTaiKhoan;
        private ComboBox cboLoaiGD;
        private TextBox txtSoTien;
        private ComboBox cboThaoTac;
        private ComboBox cboTrangThai;
        private Button btnSave;
        private Button btnCancel;

        public AddTransactionForm()
        {
            _transactionService = new TransactionService();
            InitializeComponent();
            LoadDropdowns();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Size = new Size(500, 520);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            // Header Panel
            Panel pnlHeader = new Panel();
            pnlHeader.Height = 50;
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.BackColor = Color.FromArgb(51, 102, 255); // Blue
            
            Label lblTitle = new Label();
            lblTitle.Text = "Thêm Giao dịch Mới";
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTitle.Location = new Point(15, 12);
            lblTitle.AutoSize = true;
            pnlHeader.Controls.Add(lblTitle);

            Label lblClose = new Label();
            lblClose.Text = "X";
            lblClose.ForeColor = Color.White;
            lblClose.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblClose.Location = new Point(460, 12);
            lblClose.AutoSize = true;
            lblClose.Cursor = Cursors.Hand;
            lblClose.Click += (s, e) => { ConfirmCancel(); };
            pnlHeader.Controls.Add(lblClose);
            this.Controls.Add(pnlHeader);

            int startY = 70;
            int gapY = 45;
            int labelX = 30;
            int inputX = 150;
            int inputWidth = 300;

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
            this.Controls.Add(cboTaiKhoan);

            // 4. Loại giao dịch
            startY += gapY;
            AddLabel("Loại:", labelX, startY);
            cboLoaiGD = new ComboBox();
            cboLoaiGD.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiGD.Location = new Point(inputX, startY);
            cboLoaiGD.Width = inputWidth;
            cboLoaiGD.Font = new Font("Segoe UI", 10);
            this.Controls.Add(cboLoaiGD);

            // 5. Số tiền
            startY += gapY;
            AddLabel("Số tiền:", labelX, startY);
            txtSoTien = new TextBox();
            txtSoTien.Location = new Point(inputX, startY);
            txtSoTien.Width = inputWidth;
            txtSoTien.Font = new Font("Segoe UI", 10);
            txtSoTien.Text = "0";
            txtSoTien.TextAlign = HorizontalAlignment.Right;
            this.Controls.Add(txtSoTien);

            // 6. Thao tác
            startY += gapY;
            AddLabel("Thao tác:", labelX, startY);
            cboThaoTac = new ComboBox();
            cboThaoTac.Items.AddRange(new string[] { "Chuyển khoản", "Thanh toán", "Rút tiền", "Nạp tiền" });
            cboThaoTac.DropDownStyle = ComboBoxStyle.DropDownList;
            cboThaoTac.Location = new Point(inputX, startY);
            cboThaoTac.Width = inputWidth;
            cboThaoTac.Font = new Font("Segoe UI", 10);
            cboThaoTac.SelectedIndex = 0;
            this.Controls.Add(cboThaoTac);

            // 7. Trạng thái
            startY += gapY;
            AddLabel("Trạng thái:", labelX, startY);
            cboTrangThai = new ComboBox();
            cboTrangThai.Items.AddRange(new string[] { "Success", "Pending", "Failed" });
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.Location = new Point(inputX, startY);
            cboTrangThai.Width = inputWidth;
            cboTrangThai.Font = new Font("Segoe UI", 10);
            cboTrangThai.SelectedIndex = 0;
            this.Controls.Add(cboTrangThai);

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
            }
            catch { }
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
                int maLoai = Convert.ToInt32(cboLoaiGD.SelectedValue);
                decimal soTien = 0;
                
                if (!decimal.TryParse(txtSoTien.Text.Replace(",", ""), out soTien) || soTien <= 0)
                {
                    MessageBox.Show("Số tiền không hợp lệ!"); return;
                }

                string maGD = "GD" + DateTime.Now.ToString("yyyyMMddHHmmss");
                
                bool success = _transactionService.AddTransaction(
                    maGD, tk, maLoai, soTien, txtNoiDung.Text, dtpNgay.Value, cboTrangThai.SelectedItem.ToString());

                if (success)
                {
                    MessageBox.Show("Thêm giao dịch thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Lỗi khi thêm giao dịch (có thể do không đủ số dư)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // Vẽ bo góc (Rounded Corners)
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

            // Viền nhạt
            Pen pen = new Pen(Color.LightGray, 1);
            e.Graphics.DrawPath(pen, path);
        }
    }
}
