namespace BankAccountManagement
{
    partial class AddTransactionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblMyAccount = new System.Windows.Forms.Label();
            this.cboMyAccount = new System.Windows.Forms.ComboBox();
            this.lblSoDuThucTe = new System.Windows.Forms.Label();
            this.txtSoDuThucTe = new System.Windows.Forms.TextBox();
            this.lblRecentTransfers = new System.Windows.Forms.Label();
            this.cboRecentTransfers = new System.Windows.Forms.ComboBox();
            this.lblNgay = new System.Windows.Forms.Label();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.lblNoiDung = new System.Windows.Forms.Label();
            this.txtNoiDung = new System.Windows.Forms.TextBox();
            this.lblNganHang = new System.Windows.Forms.Label();
            this.cboNganHang = new System.Windows.Forms.ComboBox();
            this.lblSoTaiKhoan = new System.Windows.Forms.Label();
            this.txtSoTaiKhoan = new System.Windows.Forms.TextBox();
            this.lblNguoiNhan = new System.Windows.Forms.Label();
            this.txtNguoiNhan = new System.Windows.Forms.TextBox();
            this.lblThaoTac = new System.Windows.Forms.Label();
            this.cboThaoTac = new System.Windows.Forms.ComboBox();
            this.lblDanhMuc = new System.Windows.Forms.Label();
            this.cboDanhMuc = new System.Windows.Forms.ComboBox();
            this.lblLoaiGD = new System.Windows.Forms.Label();
            this.cboLoaiGD = new System.Windows.Forms.ComboBox();
            this.lblLoaiTien = new System.Windows.Forms.Label();
            this.cboLoaiTien = new System.Windows.Forms.ComboBox();
            this.lblSoTien = new System.Windows.Forms.Label();
            this.txtSoTien = new System.Windows.Forms.TextBox();
            this.lblQuyDoi = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(540, 50);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(15, 12);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(186, 25);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Thêm giao dịch mới";
            // 
            // lblMyAccount
            // 
            this.lblMyAccount.AutoSize = true;
            this.lblMyAccount.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblMyAccount.Location = new System.Drawing.Point(22, 66);
            this.lblMyAccount.Name = "lblMyAccount";
            this.lblMyAccount.Size = new System.Drawing.Size(126, 17);
            this.lblMyAccount.TabIndex = 1;
            this.lblMyAccount.Text = "Tài khoản của mình:";
            // 
            // cboMyAccount
            // 
            this.cboMyAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMyAccount.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboMyAccount.FormattingEnabled = true;
            this.cboMyAccount.Location = new System.Drawing.Point(175, 62);
            this.cboMyAccount.Name = "cboMyAccount";
            this.cboMyAccount.Size = new System.Drawing.Size(335, 25);
            this.cboMyAccount.TabIndex = 2;
            this.cboMyAccount.SelectedIndexChanged += new System.EventHandler(this.CboMyAccount_SelectedIndexChanged);
            // 
            // lblSoDuThucTe
            // 
            this.lblSoDuThucTe.AutoSize = true;
            this.lblSoDuThucTe.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSoDuThucTe.Location = new System.Drawing.Point(22, 102);
            this.lblSoDuThucTe.Name = "lblSoDuThucTe";
            this.lblSoDuThucTe.Size = new System.Drawing.Size(89, 17);
            this.lblSoDuThucTe.TabIndex = 3;
            this.lblSoDuThucTe.Text = "Số dư thực tế:";
            // 
            // txtSoDuThucTe
            // 
            this.txtSoDuThucTe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.txtSoDuThucTe.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtSoDuThucTe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.txtSoDuThucTe.Location = new System.Drawing.Point(175, 98);
            this.txtSoDuThucTe.Name = "txtSoDuThucTe";
            this.txtSoDuThucTe.ReadOnly = true;
            this.txtSoDuThucTe.Size = new System.Drawing.Size(335, 26);
            this.txtSoDuThucTe.TabIndex = 4;
            this.txtSoDuThucTe.Text = "0 VND";
            // 
            // lblRecentTransfers
            // 
            this.lblRecentTransfers.AutoSize = true;
            this.lblRecentTransfers.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblRecentTransfers.Location = new System.Drawing.Point(22, 138);
            this.lblRecentTransfers.Name = "lblRecentTransfers";
            this.lblRecentTransfers.Size = new System.Drawing.Size(95, 17);
            this.lblRecentTransfers.TabIndex = 5;
            this.lblRecentTransfers.Text = "GD mẫu trước:";
            // 
            // cboRecentTransfers
            // 
            this.cboRecentTransfers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRecentTransfers.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboRecentTransfers.FormattingEnabled = true;
            this.cboRecentTransfers.Location = new System.Drawing.Point(175, 134);
            this.cboRecentTransfers.Name = "cboRecentTransfers";
            this.cboRecentTransfers.Size = new System.Drawing.Size(335, 25);
            this.cboRecentTransfers.TabIndex = 6;
            this.cboRecentTransfers.SelectedIndexChanged += new System.EventHandler(this.CboRecentTransfers_SelectedIndexChanged);
            // 
            // lblNgay
            // 
            this.lblNgay.AutoSize = true;
            this.lblNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNgay.Location = new System.Drawing.Point(22, 174);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(100, 17);
            this.lblNgay.TabIndex = 7;
            this.lblNgay.Text = "Ngày giao dịch:";
            // 
            // dtpNgay
            // 
            this.dtpNgay.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgay.Location = new System.Drawing.Point(175, 170);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(335, 24);
            this.dtpNgay.TabIndex = 8;
            // 
            // lblNoiDung
            // 
            this.lblNoiDung.AutoSize = true;
            this.lblNoiDung.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNoiDung.Location = new System.Drawing.Point(22, 210);
            this.lblNoiDung.Name = "lblNoiDung";
            this.lblNoiDung.Size = new System.Drawing.Size(65, 17);
            this.lblNoiDung.TabIndex = 9;
            this.lblNoiDung.Text = "Nội dung:";
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtNoiDung.Location = new System.Drawing.Point(175, 206);
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(335, 40);
            this.txtNoiDung.TabIndex = 10;
            // 
            // lblNganHang
            // 
            this.lblNganHang.AutoSize = true;
            this.lblNganHang.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNganHang.Location = new System.Drawing.Point(22, 256);
            this.lblNganHang.Name = "lblNganHang";
            this.lblNganHang.Size = new System.Drawing.Size(147, 17);
            this.lblNganHang.TabIndex = 11;
            this.lblNganHang.Text = "Ngân hàng người nhận:";
            // 
            // cboNganHang
            // 
            this.cboNganHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNganHang.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboNganHang.FormattingEnabled = true;
            this.cboNganHang.Location = new System.Drawing.Point(175, 252);
            this.cboNganHang.Name = "cboNganHang";
            this.cboNganHang.Size = new System.Drawing.Size(335, 25);
            this.cboNganHang.TabIndex = 12;
            // 
            // lblSoTaiKhoan
            // 
            this.lblSoTaiKhoan.AutoSize = true;
            this.lblSoTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSoTaiKhoan.Location = new System.Drawing.Point(22, 292);
            this.lblSoTaiKhoan.Name = "lblSoTaiKhoan";
            this.lblSoTaiKhoan.Size = new System.Drawing.Size(83, 17);
            this.lblSoTaiKhoan.TabIndex = 13;
            this.lblSoTaiKhoan.Text = "Số tài khoản:";
            // 
            // txtSoTaiKhoan
            // 
            this.txtSoTaiKhoan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSoTaiKhoan.Location = new System.Drawing.Point(175, 288);
            this.txtSoTaiKhoan.Name = "txtSoTaiKhoan";
            this.txtSoTaiKhoan.Size = new System.Drawing.Size(335, 24);
            this.txtSoTaiKhoan.TabIndex = 14;
            this.txtSoTaiKhoan.TextChanged += new System.EventHandler(this.TxtSoTaiKhoan_TextChanged);
            this.txtSoTaiKhoan.Leave += new System.EventHandler(this.TxtSoTaiKhoan_Leave);
            // 
            // lblNguoiNhan
            // 
            this.lblNguoiNhan.AutoSize = true;
            this.lblNguoiNhan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNguoiNhan.Location = new System.Drawing.Point(22, 328);
            this.lblNguoiNhan.Name = "lblNguoiNhan";
            this.lblNguoiNhan.Size = new System.Drawing.Size(104, 17);
            this.lblNguoiNhan.TabIndex = 15;
            this.lblNguoiNhan.Text = "Tên người nhận:";
            // 
            // txtNguoiNhan
            // 
            this.txtNguoiNhan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.txtNguoiNhan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtNguoiNhan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.txtNguoiNhan.Location = new System.Drawing.Point(175, 324);
            this.txtNguoiNhan.Name = "txtNguoiNhan";
            this.txtNguoiNhan.ReadOnly = true;
            this.txtNguoiNhan.Size = new System.Drawing.Size(335, 24);
            this.txtNguoiNhan.TabIndex = 16;
            // 
            // lblThaoTac
            // 
            this.lblThaoTac.AutoSize = true;
            this.lblThaoTac.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblThaoTac.Location = new System.Drawing.Point(22, 364);
            this.lblThaoTac.Name = "lblThaoTac";
            this.lblThaoTac.Size = new System.Drawing.Size(61, 17);
            this.lblThaoTac.TabIndex = 17;
            this.lblThaoTac.Text = "Thao tác:";
            // 
            // cboThaoTac
            // 
            this.cboThaoTac.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThaoTac.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboThaoTac.FormattingEnabled = true;
            this.cboThaoTac.Location = new System.Drawing.Point(175, 360);
            this.cboThaoTac.Name = "cboThaoTac";
            this.cboThaoTac.Size = new System.Drawing.Size(335, 25);
            this.cboThaoTac.TabIndex = 18;
            this.cboThaoTac.SelectedIndexChanged += new System.EventHandler(this.CboThaoTac_SelectedIndexChanged);
            // 
            // lblDanhMuc
            // 
            this.lblDanhMuc.AutoSize = true;
            this.lblDanhMuc.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDanhMuc.Location = new System.Drawing.Point(22, 400);
            this.lblDanhMuc.Name = "lblDanhMuc";
            this.lblDanhMuc.Size = new System.Drawing.Size(69, 17);
            this.lblDanhMuc.TabIndex = 19;
            this.lblDanhMuc.Text = "Danh mục:";
            // 
            // cboDanhMuc
            // 
            this.cboDanhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDanhMuc.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboDanhMuc.FormattingEnabled = true;
            this.cboDanhMuc.Location = new System.Drawing.Point(175, 396);
            this.cboDanhMuc.Name = "cboDanhMuc";
            this.cboDanhMuc.Size = new System.Drawing.Size(335, 25);
            this.cboDanhMuc.TabIndex = 20;
            // 
            // lblLoaiGD
            // 
            this.lblLoaiGD.AutoSize = true;
            this.lblLoaiGD.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblLoaiGD.Location = new System.Drawing.Point(22, 400);
            this.lblLoaiGD.Name = "lblLoaiGD";
            this.lblLoaiGD.Size = new System.Drawing.Size(34, 17);
            this.lblLoaiGD.TabIndex = 21;
            this.lblLoaiGD.Text = "Loại:";
            this.lblLoaiGD.Visible = false;
            // 
            // cboLoaiGD
            // 
            this.cboLoaiGD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiGD.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboLoaiGD.FormattingEnabled = true;
            this.cboLoaiGD.Location = new System.Drawing.Point(175, 396);
            this.cboLoaiGD.Name = "cboLoaiGD";
            this.cboLoaiGD.Size = new System.Drawing.Size(335, 25);
            this.cboLoaiGD.TabIndex = 22;
            this.cboLoaiGD.Visible = false;
            this.cboLoaiGD.SelectedIndexChanged += new System.EventHandler(this.CboLoaiGD_SelectedIndexChanged);
            // 
            // lblLoaiTien
            // 
            this.lblLoaiTien.AutoSize = true;
            this.lblLoaiTien.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblLoaiTien.Location = new System.Drawing.Point(22, 436);
            this.lblLoaiTien.Name = "lblLoaiTien";
            this.lblLoaiTien.Size = new System.Drawing.Size(61, 17);
            this.lblLoaiTien.TabIndex = 23;
            this.lblLoaiTien.Text = "Loại tiền:";
            // 
            // cboLoaiTien
            // 
            this.cboLoaiTien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiTien.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboLoaiTien.FormattingEnabled = true;
            this.cboLoaiTien.Location = new System.Drawing.Point(175, 432);
            this.cboLoaiTien.Name = "cboLoaiTien";
            this.cboLoaiTien.Size = new System.Drawing.Size(335, 25);
            this.cboLoaiTien.TabIndex = 24;
            this.cboLoaiTien.SelectedIndexChanged += new System.EventHandler(this.CboLoaiTien_SelectedIndexChanged);
            // 
            // lblSoTien
            // 
            this.lblSoTien.AutoSize = true;
            this.lblSoTien.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSoTien.Location = new System.Drawing.Point(22, 472);
            this.lblSoTien.Name = "lblSoTien";
            this.lblSoTien.Size = new System.Drawing.Size(78, 17);
            this.lblSoTien.TabIndex = 25;
            this.lblSoTien.Text = "Số tiền gốc:";
            // 
            // txtSoTien
            // 
            this.txtSoTien.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSoTien.Location = new System.Drawing.Point(175, 468);
            this.txtSoTien.Name = "txtSoTien";
            this.txtSoTien.Size = new System.Drawing.Size(335, 24);
            this.txtSoTien.TabIndex = 26;
            this.txtSoTien.Text = "0";
            this.txtSoTien.TextChanged += new System.EventHandler(this.TxtSoTien_TextChanged);
            // 
            // lblQuyDoi
            // 
            this.lblQuyDoi.AutoSize = true;
            this.lblQuyDoi.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblQuyDoi.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblQuyDoi.Location = new System.Drawing.Point(175, 494);
            this.lblQuyDoi.Name = "lblQuyDoi";
            this.lblQuyDoi.Size = new System.Drawing.Size(92, 15);
            this.lblQuyDoi.TabIndex = 27;
            this.lblQuyDoi.Text = "Quy đổi: 0 VND";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(255)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(300, 524);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 36);
            this.btnSave.TabIndex = 28;
            this.btnSave.Text = "Thực hiện";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(415, 524);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 36);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // AddTransactionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(540, 590);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblQuyDoi);
            this.Controls.Add(this.txtSoTien);
            this.Controls.Add(this.lblSoTien);
            this.Controls.Add(this.cboLoaiTien);
            this.Controls.Add(this.lblLoaiTien);
            this.Controls.Add(this.cboLoaiGD);
            this.Controls.Add(this.lblLoaiGD);
            this.Controls.Add(this.cboDanhMuc);
            this.Controls.Add(this.lblDanhMuc);
            this.Controls.Add(this.cboThaoTac);
            this.Controls.Add(this.lblThaoTac);
            this.Controls.Add(this.txtNguoiNhan);
            this.Controls.Add(this.lblNguoiNhan);
            this.Controls.Add(this.txtSoTaiKhoan);
            this.Controls.Add(this.lblSoTaiKhoan);
            this.Controls.Add(this.cboNganHang);
            this.Controls.Add(this.lblNganHang);
            this.Controls.Add(this.txtNoiDung);
            this.Controls.Add(this.lblNoiDung);
            this.Controls.Add(this.dtpNgay);
            this.Controls.Add(this.lblNgay);
            this.Controls.Add(this.cboRecentTransfers);
            this.Controls.Add(this.lblRecentTransfers);
            this.Controls.Add(this.txtSoDuThucTe);
            this.Controls.Add(this.lblSoDuThucTe);
            this.Controls.Add(this.cboMyAccount);
            this.Controls.Add(this.lblMyAccount);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddTransactionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm giao dịch mới";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblMyAccount;
        private System.Windows.Forms.ComboBox cboMyAccount;
        private System.Windows.Forms.Label lblSoDuThucTe;
        private System.Windows.Forms.TextBox txtSoDuThucTe;
        private System.Windows.Forms.Label lblRecentTransfers;
        private System.Windows.Forms.ComboBox cboRecentTransfers;
        private System.Windows.Forms.Label lblNgay;
        private System.Windows.Forms.DateTimePicker dtpNgay;
        private System.Windows.Forms.Label lblNoiDung;
        private System.Windows.Forms.TextBox txtNoiDung;
        private System.Windows.Forms.Label lblNganHang;
        private System.Windows.Forms.ComboBox cboNganHang;
        private System.Windows.Forms.Label lblSoTaiKhoan;
        private System.Windows.Forms.TextBox txtSoTaiKhoan;
        private System.Windows.Forms.Label lblNguoiNhan;
        private System.Windows.Forms.TextBox txtNguoiNhan;
        private System.Windows.Forms.Label lblThaoTac;
        private System.Windows.Forms.ComboBox cboThaoTac;
        private System.Windows.Forms.Label lblDanhMuc;
        private System.Windows.Forms.ComboBox cboDanhMuc;
        private System.Windows.Forms.Label lblLoaiGD;
        private System.Windows.Forms.ComboBox cboLoaiGD;
        private System.Windows.Forms.Label lblLoaiTien;
        private System.Windows.Forms.ComboBox cboLoaiTien;
        private System.Windows.Forms.Label lblSoTien;
        private System.Windows.Forms.TextBox txtSoTien;
        private System.Windows.Forms.Label lblQuyDoi;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
