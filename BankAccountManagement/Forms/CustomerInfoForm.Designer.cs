namespace BankAccountManagement.Forms
{
    partial class CustomerInfoForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblBreadcrumb = new System.Windows.Forms.Label();
            this.lblHeaderSub = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblHeaderIcon = new System.Windows.Forms.Label();
            this.panelBody = new System.Windows.Forms.Panel();
            this.panelCard = new System.Windows.Forms.Panel();
            this.tblFields = new System.Windows.Forms.TableLayoutPanel();
            this.pnlFullName = new System.Windows.Forms.Panel();
            this.pnlFullNameBox = new System.Windows.Forms.Panel();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.pnlCccd = new System.Windows.Forms.Panel();
            this.pnlCccdBox = new System.Windows.Forms.Panel();
            this.txtCccd = new System.Windows.Forms.TextBox();
            this.lblCccd = new System.Windows.Forms.Label();
            this.pnlDob = new System.Windows.Forms.Panel();
            this.pnlDobBox = new System.Windows.Forms.Panel();
            this.dtpDob = new System.Windows.Forms.DateTimePicker();
            this.lblDob = new System.Windows.Forms.Label();
            this.pnlGender = new System.Windows.Forms.Panel();
            this.pnlGenderBox = new System.Windows.Forms.Panel();
            this.cboGender = new System.Windows.Forms.ComboBox();
            this.lblGender = new System.Windows.Forms.Label();
            this.pnlPhone = new System.Windows.Forms.Panel();
            this.pnlPhoneBox = new System.Windows.Forms.Panel();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.pnlEmail = new System.Windows.Forms.Panel();
            this.pnlEmailBox = new System.Windows.Forms.Panel();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.pnlAddress = new System.Windows.Forms.Panel();
            this.pnlAddressBox = new System.Windows.Forms.Panel();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.pnlDivider = new System.Windows.Forms.Panel();
            this.pnlCardHeader = new System.Windows.Forms.Panel();
            this.lblSectionDesc = new System.Windows.Forms.Label();
            this.lblSectionTitle = new System.Windows.Forms.Label();
            this.pnlSectionIcon = new System.Windows.Forms.Panel();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.pnlFooterDivider = new System.Windows.Forms.Panel();
            this.lblError = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.panelCard.SuspendLayout();
            this.tblFields.SuspendLayout();
            this.pnlFullName.SuspendLayout();
            this.pnlFullNameBox.SuspendLayout();
            this.pnlCccd.SuspendLayout();
            this.pnlCccdBox.SuspendLayout();
            this.pnlDob.SuspendLayout();
            this.pnlDobBox.SuspendLayout();
            this.pnlGender.SuspendLayout();
            this.pnlGenderBox.SuspendLayout();
            this.pnlPhone.SuspendLayout();
            this.pnlPhoneBox.SuspendLayout();
            this.pnlEmail.SuspendLayout();
            this.pnlEmailBox.SuspendLayout();
            this.pnlAddress.SuspendLayout();
            this.pnlAddressBox.SuspendLayout();
            this.pnlCardHeader.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.panelHeader.Controls.Add(this.lblBreadcrumb);
            this.panelHeader.Controls.Add(this.lblHeaderSub);
            this.panelHeader.Controls.Add(this.lblHeaderTitle);
            this.panelHeader.Controls.Add(this.lblHeaderIcon);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1040, 84);
            this.panelHeader.TabIndex = 0;
            // 
            // lblBreadcrumb
            // 
            this.lblBreadcrumb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBreadcrumb.AutoSize = true;
            this.lblBreadcrumb.BackColor = System.Drawing.Color.Transparent;
            this.lblBreadcrumb.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblBreadcrumb.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblBreadcrumb.Location = new System.Drawing.Point(760, 34);
            this.lblBreadcrumb.Name = "lblBreadcrumb";
            this.lblBreadcrumb.Size = new System.Drawing.Size(237, 13);
            this.lblBreadcrumb.TabIndex = 3;
            this.lblBreadcrumb.Text = "TRANG CHỦ   ›   KHÁCH HÀNG   ›   THÔNG TIN";
            // 
            // lblHeaderSub
            // 
            this.lblHeaderSub.AutoSize = true;
            this.lblHeaderSub.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHeaderSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblHeaderSub.Location = new System.Drawing.Point(54, 52);
            this.lblHeaderSub.Name = "lblHeaderSub";
            this.lblHeaderSub.Size = new System.Drawing.Size(331, 15);
            this.lblHeaderSub.TabIndex = 2;
            this.lblHeaderSub.Text = "Vui lòng điền đầy đủ thông tin để hoàn tất đăng ký tài khoản.";
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(52, 18);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(239, 31);
            this.lblHeaderTitle.TabIndex = 1;
            this.lblHeaderTitle.Text = "Thông tin khách hàng";
            // 
            // lblHeaderIcon
            // 
            this.lblHeaderIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.lblHeaderIcon.Location = new System.Drawing.Point(36, 22);
            this.lblHeaderIcon.Name = "lblHeaderIcon";
            this.lblHeaderIcon.Size = new System.Drawing.Size(4, 40);
            this.lblHeaderIcon.TabIndex = 0;
            // 
            // panelBody
            // 
            this.panelBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelBody.Controls.Add(this.panelCard);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 84);
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new System.Windows.Forms.Padding(32, 28, 32, 24);
            this.panelBody.Size = new System.Drawing.Size(1040, 584);
            this.panelBody.TabIndex = 1;
            // 
            // panelCard
            // 
            this.panelCard.BackColor = System.Drawing.Color.White;
            this.panelCard.Controls.Add(this.tblFields);
            this.panelCard.Controls.Add(this.pnlDivider);
            this.panelCard.Controls.Add(this.pnlCardHeader);
            this.panelCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCard.Location = new System.Drawing.Point(32, 28);
            this.panelCard.Name = "panelCard";
            this.panelCard.Padding = new System.Windows.Forms.Padding(32, 28, 32, 28);
            this.panelCard.Size = new System.Drawing.Size(976, 532);
            this.panelCard.TabIndex = 0;
            // 
            // tblFields
            // 
            this.tblFields.ColumnCount = 2;
            this.tblFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblFields.Controls.Add(this.pnlFullName, 0, 0);
            this.tblFields.Controls.Add(this.pnlCccd, 1, 0);
            this.tblFields.Controls.Add(this.pnlDob, 0, 1);
            this.tblFields.Controls.Add(this.pnlGender, 1, 1);
            this.tblFields.Controls.Add(this.pnlPhone, 0, 2);
            this.tblFields.Controls.Add(this.pnlEmail, 1, 2);
            this.tblFields.Controls.Add(this.pnlAddress, 0, 3);
            this.tblFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFields.Location = new System.Drawing.Point(32, 85);
            this.tblFields.Name = "tblFields";
            this.tblFields.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.tblFields.RowCount = 4;
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tblFields.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFields.Size = new System.Drawing.Size(912, 419);
            this.tblFields.TabIndex = 2;
            // 
            // pnlFullName
            // 
            this.pnlFullName.Controls.Add(this.pnlFullNameBox);
            this.pnlFullName.Controls.Add(this.lblFullName);
            this.pnlFullName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFullName.Location = new System.Drawing.Point(0, 20);
            this.pnlFullName.Margin = new System.Windows.Forms.Padding(0, 0, 8, 16);
            this.pnlFullName.Name = "pnlFullName";
            this.pnlFullName.Size = new System.Drawing.Size(448, 64);
            this.pnlFullName.TabIndex = 0;
            // 
            // pnlFullNameBox
            // 
            this.pnlFullNameBox.BackColor = System.Drawing.Color.White;
            this.pnlFullNameBox.Controls.Add(this.txtFullName);
            this.pnlFullNameBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFullNameBox.Location = new System.Drawing.Point(0, 22);
            this.pnlFullNameBox.Name = "pnlFullNameBox";
            this.pnlFullNameBox.Size = new System.Drawing.Size(448, 42);
            this.pnlFullNameBox.TabIndex = 1;
            this.pnlFullNameBox.Paint += new System.Windows.Forms.PaintEventHandler(this.InputBox_Paint);
            // 
            // txtFullName
            // 
            this.txtFullName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFullName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtFullName.Location = new System.Drawing.Point(12, 11);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(424, 19);
            this.txtFullName.TabIndex = 0;
            // 
            // lblFullName
            // 
            this.lblFullName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFullName.Location = new System.Drawing.Point(0, 0);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(448, 22);
            this.lblFullName.TabIndex = 0;
            this.lblFullName.Text = "HỌ VÀ TÊN *";
            this.lblFullName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlCccd
            // 
            this.pnlCccd.Controls.Add(this.pnlCccdBox);
            this.pnlCccd.Controls.Add(this.lblCccd);
            this.pnlCccd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCccd.Location = new System.Drawing.Point(464, 20);
            this.pnlCccd.Margin = new System.Windows.Forms.Padding(8, 0, 0, 16);
            this.pnlCccd.Name = "pnlCccd";
            this.pnlCccd.Size = new System.Drawing.Size(448, 64);
            this.pnlCccd.TabIndex = 1;
            // 
            // pnlCccdBox
            // 
            this.pnlCccdBox.BackColor = System.Drawing.Color.White;
            this.pnlCccdBox.Controls.Add(this.txtCccd);
            this.pnlCccdBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCccdBox.Location = new System.Drawing.Point(0, 22);
            this.pnlCccdBox.Name = "pnlCccdBox";
            this.pnlCccdBox.Size = new System.Drawing.Size(448, 42);
            this.pnlCccdBox.TabIndex = 1;
            this.pnlCccdBox.Paint += new System.Windows.Forms.PaintEventHandler(this.InputBox_Paint);
            // 
            // txtCccd
            // 
            this.txtCccd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCccd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCccd.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtCccd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtCccd.Location = new System.Drawing.Point(12, 11);
            this.txtCccd.MaxLength = 20;
            this.txtCccd.Name = "txtCccd";
            this.txtCccd.Size = new System.Drawing.Size(424, 19);
            this.txtCccd.TabIndex = 0;
            // 
            // lblCccd
            // 
            this.lblCccd.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCccd.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblCccd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCccd.Location = new System.Drawing.Point(0, 0);
            this.lblCccd.Name = "lblCccd";
            this.lblCccd.Size = new System.Drawing.Size(448, 22);
            this.lblCccd.TabIndex = 0;
            this.lblCccd.Text = "CMND / CCCD *";
            this.lblCccd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlDob
            // 
            this.pnlDob.Controls.Add(this.pnlDobBox);
            this.pnlDob.Controls.Add(this.lblDob);
            this.pnlDob.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDob.Location = new System.Drawing.Point(0, 100);
            this.pnlDob.Margin = new System.Windows.Forms.Padding(0, 0, 8, 16);
            this.pnlDob.Name = "pnlDob";
            this.pnlDob.Size = new System.Drawing.Size(448, 64);
            this.pnlDob.TabIndex = 2;
            // 
            // pnlDobBox
            // 
            this.pnlDobBox.BackColor = System.Drawing.Color.White;
            this.pnlDobBox.Controls.Add(this.dtpDob);
            this.pnlDobBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDobBox.Location = new System.Drawing.Point(0, 22);
            this.pnlDobBox.Name = "pnlDobBox";
            this.pnlDobBox.Size = new System.Drawing.Size(448, 42);
            this.pnlDobBox.TabIndex = 1;
            this.pnlDobBox.Paint += new System.Windows.Forms.PaintEventHandler(this.InputBox_Paint);
            // 
            // dtpDob
            // 
            this.dtpDob.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpDob.CustomFormat = "dd/MM/yyyy";
            this.dtpDob.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.dtpDob.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDob.Location = new System.Drawing.Point(12, 9);
            this.dtpDob.Name = "dtpDob";
            this.dtpDob.Size = new System.Drawing.Size(424, 26);
            this.dtpDob.TabIndex = 0;
            this.dtpDob.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            // 
            // lblDob
            // 
            this.lblDob.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDob.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDob.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDob.Location = new System.Drawing.Point(0, 0);
            this.lblDob.Name = "lblDob";
            this.lblDob.Size = new System.Drawing.Size(448, 22);
            this.lblDob.TabIndex = 0;
            this.lblDob.Text = "NGÀY SINH *";
            this.lblDob.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlGender
            // 
            this.pnlGender.Controls.Add(this.pnlGenderBox);
            this.pnlGender.Controls.Add(this.lblGender);
            this.pnlGender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGender.Location = new System.Drawing.Point(464, 100);
            this.pnlGender.Margin = new System.Windows.Forms.Padding(8, 0, 0, 16);
            this.pnlGender.Name = "pnlGender";
            this.pnlGender.Size = new System.Drawing.Size(448, 64);
            this.pnlGender.TabIndex = 3;
            // 
            // pnlGenderBox
            // 
            this.pnlGenderBox.BackColor = System.Drawing.Color.White;
            this.pnlGenderBox.Controls.Add(this.cboGender);
            this.pnlGenderBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGenderBox.Location = new System.Drawing.Point(0, 22);
            this.pnlGenderBox.Name = "pnlGenderBox";
            this.pnlGenderBox.Size = new System.Drawing.Size(448, 42);
            this.pnlGenderBox.TabIndex = 1;
            this.pnlGenderBox.Paint += new System.Windows.Forms.PaintEventHandler(this.InputBox_Paint);
            // 
            // cboGender
            // 
            this.cboGender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboGender.BackColor = System.Drawing.Color.White;
            this.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboGender.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.cboGender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cboGender.FormattingEnabled = true;
            this.cboGender.Items.AddRange(new object[] {
            "Nam",
            "Nữ",
            "Khác"});
            this.cboGender.Location = new System.Drawing.Point(10, 8);
            this.cboGender.Name = "cboGender";
            this.cboGender.Size = new System.Drawing.Size(428, 27);
            this.cboGender.TabIndex = 0;
            this.cboGender.SelectedIndex = 0;
            // 
            // lblGender
            // 
            this.lblGender.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGender.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblGender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblGender.Location = new System.Drawing.Point(0, 0);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(448, 22);
            this.lblGender.TabIndex = 0;
            this.lblGender.Text = "GIỚI TÍNH *";
            this.lblGender.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlPhone
            // 
            this.pnlPhone.Controls.Add(this.pnlPhoneBox);
            this.pnlPhone.Controls.Add(this.lblPhone);
            this.pnlPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPhone.Location = new System.Drawing.Point(0, 180);
            this.pnlPhone.Margin = new System.Windows.Forms.Padding(0, 0, 8, 16);
            this.pnlPhone.Name = "pnlPhone";
            this.pnlPhone.Size = new System.Drawing.Size(448, 64);
            this.pnlPhone.TabIndex = 4;
            // 
            // pnlPhoneBox
            // 
            this.pnlPhoneBox.BackColor = System.Drawing.Color.White;
            this.pnlPhoneBox.Controls.Add(this.txtPhone);
            this.pnlPhoneBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPhoneBox.Location = new System.Drawing.Point(0, 22);
            this.pnlPhoneBox.Name = "pnlPhoneBox";
            this.pnlPhoneBox.Size = new System.Drawing.Size(448, 42);
            this.pnlPhoneBox.TabIndex = 1;
            this.pnlPhoneBox.Paint += new System.Windows.Forms.PaintEventHandler(this.InputBox_Paint);
            // 
            // txtPhone
            // 
            this.txtPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtPhone.Location = new System.Drawing.Point(12, 11);
            this.txtPhone.MaxLength = 15;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(424, 19);
            this.txtPhone.TabIndex = 0;
            // 
            // lblPhone
            // 
            this.lblPhone.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPhone.Location = new System.Drawing.Point(0, 0);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(448, 22);
            this.lblPhone.TabIndex = 0;
            this.lblPhone.Text = "SỐ ĐIỆN THOẠI *";
            this.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlEmail
            // 
            this.pnlEmail.Controls.Add(this.pnlEmailBox);
            this.pnlEmail.Controls.Add(this.lblEmail);
            this.pnlEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEmail.Location = new System.Drawing.Point(464, 180);
            this.pnlEmail.Margin = new System.Windows.Forms.Padding(8, 0, 0, 16);
            this.pnlEmail.Name = "pnlEmail";
            this.pnlEmail.Size = new System.Drawing.Size(448, 64);
            this.pnlEmail.TabIndex = 5;
            // 
            // pnlEmailBox
            // 
            this.pnlEmailBox.BackColor = System.Drawing.Color.White;
            this.pnlEmailBox.Controls.Add(this.txtEmail);
            this.pnlEmailBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEmailBox.Location = new System.Drawing.Point(0, 22);
            this.pnlEmailBox.Name = "pnlEmailBox";
            this.pnlEmailBox.Size = new System.Drawing.Size(448, 42);
            this.pnlEmailBox.TabIndex = 1;
            this.pnlEmailBox.Paint += new System.Windows.Forms.PaintEventHandler(this.InputBox_Paint);
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtEmail.Location = new System.Drawing.Point(12, 11);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(424, 19);
            this.txtEmail.TabIndex = 0;
            // 
            // lblEmail
            // 
            this.lblEmail.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblEmail.Location = new System.Drawing.Point(0, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(448, 22);
            this.lblEmail.TabIndex = 0;
            this.lblEmail.Text = "EMAIL *";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlAddress
            // 
            this.tblFields.SetColumnSpan(this.pnlAddress, 2);
            this.pnlAddress.Controls.Add(this.pnlAddressBox);
            this.pnlAddress.Controls.Add(this.lblAddress);
            this.pnlAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAddress.Location = new System.Drawing.Point(0, 260);
            this.pnlAddress.Margin = new System.Windows.Forms.Padding(0);
            this.pnlAddress.Name = "pnlAddress";
            this.pnlAddress.Size = new System.Drawing.Size(912, 159);
            this.pnlAddress.TabIndex = 6;
            // 
            // pnlAddressBox
            // 
            this.pnlAddressBox.BackColor = System.Drawing.Color.White;
            this.pnlAddressBox.Controls.Add(this.txtAddress);
            this.pnlAddressBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAddressBox.Location = new System.Drawing.Point(0, 22);
            this.pnlAddressBox.Name = "pnlAddressBox";
            this.pnlAddressBox.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.pnlAddressBox.Size = new System.Drawing.Size(912, 137);
            this.pnlAddressBox.TabIndex = 1;
            this.pnlAddressBox.Paint += new System.Windows.Forms.PaintEventHandler(this.InputBox_Paint);
            // 
            // txtAddress
            // 
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtAddress.Location = new System.Drawing.Point(12, 10);
            this.txtAddress.MaxLength = 255;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAddress.Size = new System.Drawing.Size(888, 117);
            this.txtAddress.TabIndex = 0;
            // 
            // lblAddress
            // 
            this.lblAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblAddress.Location = new System.Drawing.Point(0, 0);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(912, 22);
            this.lblAddress.TabIndex = 0;
            this.lblAddress.Text = "ĐỊA CHỈ *";
            this.lblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlDivider
            // 
            this.pnlDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDivider.Location = new System.Drawing.Point(32, 84);
            this.pnlDivider.Name = "pnlDivider";
            this.pnlDivider.Size = new System.Drawing.Size(912, 1);
            this.pnlDivider.TabIndex = 1;
            // 
            // pnlCardHeader
            // 
            this.pnlCardHeader.Controls.Add(this.lblSectionDesc);
            this.pnlCardHeader.Controls.Add(this.lblSectionTitle);
            this.pnlCardHeader.Controls.Add(this.pnlSectionIcon);
            this.pnlCardHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCardHeader.Location = new System.Drawing.Point(32, 28);
            this.pnlCardHeader.Name = "pnlCardHeader";
            this.pnlCardHeader.Size = new System.Drawing.Size(912, 56);
            this.pnlCardHeader.TabIndex = 0;
            // 
            // lblSectionDesc
            // 
            this.lblSectionDesc.AutoSize = true;
            this.lblSectionDesc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSectionDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSectionDesc.Location = new System.Drawing.Point(17, 30);
            this.lblSectionDesc.Name = "lblSectionDesc";
            this.lblSectionDesc.Size = new System.Drawing.Size(183, 15);
            this.lblSectionDesc.TabIndex = 2;
            this.lblSectionDesc.Text = "Các trường có dấu (*) là bắt buộc";
            // 
            // lblSectionTitle
            // 
            this.lblSectionTitle.AutoSize = true;
            this.lblSectionTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblSectionTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSectionTitle.Location = new System.Drawing.Point(16, 2);
            this.lblSectionTitle.Name = "lblSectionTitle";
            this.lblSectionTitle.Size = new System.Drawing.Size(161, 25);
            this.lblSectionTitle.TabIndex = 1;
            this.lblSectionTitle.Text = "Thông tin cá nhân";
            // 
            // pnlSectionIcon
            // 
            this.pnlSectionIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.pnlSectionIcon.Location = new System.Drawing.Point(0, 6);
            this.pnlSectionIcon.Name = "pnlSectionIcon";
            this.pnlSectionIcon.Size = new System.Drawing.Size(4, 32);
            this.pnlSectionIcon.TabIndex = 0;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.pnlFooterDivider);
            this.panelFooter.Controls.Add(this.lblError);
            this.panelFooter.Controls.Add(this.btnCancel);
            this.panelFooter.Controls.Add(this.btnSave);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 668);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1040, 72);
            this.panelFooter.TabIndex = 2;
            // 
            // pnlFooterDivider
            // 
            this.pnlFooterDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlFooterDivider.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFooterDivider.Location = new System.Drawing.Point(0, 0);
            this.pnlFooterDivider.Name = "pnlFooterDivider";
            this.pnlFooterDivider.Size = new System.Drawing.Size(1040, 1);
            this.pnlFooterDivider.TabIndex = 0;
            // 
            // lblError
            // 
            this.lblError.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblError.Location = new System.Drawing.Point(32, 24);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(600, 24);
            this.lblError.TabIndex = 1;
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblError.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnCancel.Location = new System.Drawing.Point(736, 16);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 40);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(56)))), ((int)(((byte)(202)))));
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(848, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(160, 40);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "LƯU THÔNG TIN";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // CustomerInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1040, 740);
            this.Controls.Add(this.panelBody);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelHeader);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(900, 660);
            this.Name = "CustomerInfoForm";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelBody.ResumeLayout(false);
            this.panelCard.ResumeLayout(false);
            this.tblFields.ResumeLayout(false);
            this.pnlFullName.ResumeLayout(false);
            this.pnlFullNameBox.ResumeLayout(false);
            this.pnlFullNameBox.PerformLayout();
            this.pnlCccd.ResumeLayout(false);
            this.pnlCccdBox.ResumeLayout(false);
            this.pnlCccdBox.PerformLayout();
            this.pnlDob.ResumeLayout(false);
            this.pnlDobBox.ResumeLayout(false);
            this.pnlGender.ResumeLayout(false);
            this.pnlGenderBox.ResumeLayout(false);
            this.pnlPhone.ResumeLayout(false);
            this.pnlPhoneBox.ResumeLayout(false);
            this.pnlPhoneBox.PerformLayout();
            this.pnlEmail.ResumeLayout(false);
            this.pnlEmailBox.ResumeLayout(false);
            this.pnlEmailBox.PerformLayout();
            this.pnlAddress.ResumeLayout(false);
            this.pnlAddressBox.ResumeLayout(false);
            this.pnlAddressBox.PerformLayout();
            this.pnlCardHeader.ResumeLayout(false);
            this.pnlCardHeader.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeaderIcon;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSub;
        private System.Windows.Forms.Label lblBreadcrumb;

        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Panel panelCard;
        private System.Windows.Forms.Panel pnlCardHeader;
        private System.Windows.Forms.Panel pnlSectionIcon;
        private System.Windows.Forms.Label lblSectionTitle;
        private System.Windows.Forms.Label lblSectionDesc;
        private System.Windows.Forms.Panel pnlDivider;
        private System.Windows.Forms.TableLayoutPanel tblFields;

        private System.Windows.Forms.Panel pnlFullName;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Panel pnlFullNameBox;
        private System.Windows.Forms.TextBox txtFullName;

        private System.Windows.Forms.Panel pnlCccd;
        private System.Windows.Forms.Label lblCccd;
        private System.Windows.Forms.Panel pnlCccdBox;
        private System.Windows.Forms.TextBox txtCccd;

        private System.Windows.Forms.Panel pnlDob;
        private System.Windows.Forms.Label lblDob;
        private System.Windows.Forms.Panel pnlDobBox;
        private System.Windows.Forms.DateTimePicker dtpDob;

        private System.Windows.Forms.Panel pnlGender;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Panel pnlGenderBox;
        private System.Windows.Forms.ComboBox cboGender;

        private System.Windows.Forms.Panel pnlPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Panel pnlPhoneBox;
        private System.Windows.Forms.TextBox txtPhone;

        private System.Windows.Forms.Panel pnlEmail;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Panel pnlEmailBox;
        private System.Windows.Forms.TextBox txtEmail;

        private System.Windows.Forms.Panel pnlAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Panel pnlAddressBox;
        private System.Windows.Forms.TextBox txtAddress;

        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Panel pnlFooterDivider;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}