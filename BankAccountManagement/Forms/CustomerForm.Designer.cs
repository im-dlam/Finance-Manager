namespace BankAccountManagement.Forms
{
    partial class CustomerForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.pnlToolbarLeft = new System.Windows.Forms.Panel();
            this.btnSearch = new BankAccountManagement.Forms.RoundedButton();
            this.pnlSearchBox = new BankAccountManagement.Forms.RoundedPanel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearchIcon = new System.Windows.Forms.Label();
            this.pnlToolbarRight = new System.Windows.Forms.Panel();
            this.flpToolbarRight = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRefresh = new BankAccountManagement.Forms.RoundedButton();
            this.btnEdit = new BankAccountManagement.Forms.RoundedButton();
            this.btnDelete = new BankAccountManagement.Forms.RoundedButton();
            this.pnlStats = new System.Windows.Forms.Panel();
            this.tlpStats = new System.Windows.Forms.TableLayoutPanel();
            this.pnlStat1 = new BankAccountManagement.Forms.RoundedPanel();
            this.lblStat1Value = new System.Windows.Forms.Label();
            this.lblStat1Title = new System.Windows.Forms.Label();
            this.lblStat1Icon = new System.Windows.Forms.Label();
            this.pnlStat1Accent = new System.Windows.Forms.Panel();
            this.pnlStat2 = new BankAccountManagement.Forms.RoundedPanel();
            this.lblStat2Value = new System.Windows.Forms.Label();
            this.lblStat2Title = new System.Windows.Forms.Label();
            this.lblStat2Icon = new System.Windows.Forms.Label();
            this.pnlStat2Accent = new System.Windows.Forms.Panel();
            this.pnlStat3 = new BankAccountManagement.Forms.RoundedPanel();
            this.lblStat3Value = new System.Windows.Forms.Label();
            this.lblStat3Title = new System.Windows.Forms.Label();
            this.lblStat3Icon = new System.Windows.Forms.Label();
            this.pnlStat3Accent = new System.Windows.Forms.Panel();
            this.pnlStat4 = new BankAccountManagement.Forms.RoundedPanel();
            this.lblStat4Value = new System.Windows.Forms.Label();
            this.lblStat4Title = new System.Windows.Forms.Label();
            this.lblStat4Icon = new System.Windows.Forms.Label();
            this.pnlStat4Accent = new System.Windows.Forms.Panel();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblStatusDot = new System.Windows.Forms.Label();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.pnlGridCard = new BankAccountManagement.Forms.RoundedPanel();
            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            this.colMaKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoTen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCMND = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgaySinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGioiTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiaChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlGridHeader = new System.Windows.Forms.Panel();
            this.lblRecordCount = new System.Windows.Forms.Label();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.pnlDetailCard = new BankAccountManagement.Forms.RoundedPanel();
            this.pnlDetailScroll = new System.Windows.Forms.Panel();
            this.tlpDetail = new System.Windows.Forms.TableLayoutPanel();
            this.lblMaKH = new System.Windows.Forms.Label();
            this.txtMaKH = new System.Windows.Forms.TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblCMND = new System.Windows.Forms.Label();
            this.txtCMND = new System.Windows.Forms.TextBox();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.cboGioiTinh = new System.Windows.Forms.ComboBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.tlpDetailActions = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new BankAccountManagement.Forms.RoundedButton();
            this.btnSave = new BankAccountManagement.Forms.RoundedButton();
            this.pnlDetailBanner = new BankAccountManagement.Forms.RoundedPanel();
            this.lblDetailSubtitle = new System.Windows.Forms.Label();
            this.lblDetailTitle = new System.Windows.Forms.Label();
            this.pnlToolbar.SuspendLayout();
            this.pnlToolbarLeft.SuspendLayout();
            this.pnlSearchBox.SuspendLayout();
            this.pnlToolbarRight.SuspendLayout();
            this.flpToolbarRight.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.tlpStats.SuspendLayout();
            this.pnlStat1.SuspendLayout();
            this.pnlStat2.SuspendLayout();
            this.pnlStat3.SuspendLayout();
            this.pnlStat4.SuspendLayout();
            this.pnlStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.pnlGridHeader.SuspendLayout();
            this.pnlDetailCard.SuspendLayout();
            this.pnlDetailScroll.SuspendLayout();
            this.tlpDetail.SuspendLayout();
            this.tlpDetailActions.SuspendLayout();
            this.pnlDetailBanner.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Controls.Add(this.pnlToolbarLeft);
            this.pnlToolbar.Controls.Add(this.pnlToolbarRight);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(1440, 76);
            this.pnlToolbar.TabIndex = 0;
            // 
            // pnlToolbarLeft
            // 
            this.pnlToolbarLeft.BackColor = System.Drawing.Color.White;
            this.pnlToolbarLeft.Controls.Add(this.btnSearch);
            this.pnlToolbarLeft.Controls.Add(this.pnlSearchBox);
            this.pnlToolbarLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlToolbarLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlToolbarLeft.Name = "pnlToolbarLeft";
            this.pnlToolbarLeft.Size = new System.Drawing.Size(900, 76);
            this.pnlToolbarLeft.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BorderRadius = 10;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.HoverBorder = System.Drawing.Color.Transparent;
            this.btnSearch.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnSearch.Location = new System.Drawing.Point(376, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.NormalBorder = System.Drawing.Color.Transparent;
            this.btnSearch.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSearch.PressedBorder = System.Drawing.Color.Transparent;
            this.btnSearch.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnSearch.Size = new System.Drawing.Size(104, 40);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.TextColor = System.Drawing.Color.White;
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // pnlSearchBox
            // 
            this.pnlSearchBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlSearchBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlSearchBox.BorderRadius = 10;
            this.pnlSearchBox.BorderSize = 1;
            this.pnlSearchBox.Controls.Add(this.txtSearch);
            this.pnlSearchBox.Controls.Add(this.lblSearchIcon);
            this.pnlSearchBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlSearchBox.Location = new System.Drawing.Point(24, 20);
            this.pnlSearchBox.Name = "pnlSearchBox";
            this.pnlSearchBox.Size = new System.Drawing.Size(340, 40);
            this.pnlSearchBox.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtSearch.Location = new System.Drawing.Point(42, 11);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(288, 18);
            this.txtSearch.TabIndex = 1;
            // 
            // lblSearchIcon
            // 
            this.lblSearchIcon.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchIcon.Font = new System.Drawing.Font("Segoe MDL2 Assets", 11F);
            this.lblSearchIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSearchIcon.Location = new System.Drawing.Point(12, 9);
            this.lblSearchIcon.Name = "lblSearchIcon";
            this.lblSearchIcon.Size = new System.Drawing.Size(22, 22);
            this.lblSearchIcon.TabIndex = 0;
            this.lblSearchIcon.Text = "";
            this.lblSearchIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlToolbarRight
            // 
            this.pnlToolbarRight.BackColor = System.Drawing.Color.White;
            this.pnlToolbarRight.Controls.Add(this.flpToolbarRight);
            this.pnlToolbarRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlToolbarRight.Location = new System.Drawing.Point(900, 0);
            this.pnlToolbarRight.Name = "pnlToolbarRight";
            this.pnlToolbarRight.Size = new System.Drawing.Size(540, 76);
            this.pnlToolbarRight.TabIndex = 1;
            // 
            // flpToolbarRight
            // 
            this.flpToolbarRight.BackColor = System.Drawing.Color.White;
            this.flpToolbarRight.Controls.Add(this.btnRefresh);
            this.flpToolbarRight.Controls.Add(this.btnEdit);
            this.flpToolbarRight.Controls.Add(this.btnDelete);
            this.flpToolbarRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpToolbarRight.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpToolbarRight.Location = new System.Drawing.Point(0, 0);
            this.flpToolbarRight.Name = "flpToolbarRight";
            this.flpToolbarRight.Padding = new System.Windows.Forms.Padding(0, 20, 24, 0);
            this.flpToolbarRight.Size = new System.Drawing.Size(540, 76);
            this.flpToolbarRight.TabIndex = 0;
            this.flpToolbarRight.WrapContents = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BorderRadius = 10;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12F);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnRefresh.HoverBorder = System.Drawing.Color.Transparent;
            this.btnRefresh.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnRefresh.Location = new System.Drawing.Point(472, 20);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.NormalBorder = System.Drawing.Color.Transparent;
            this.btnRefresh.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnRefresh.PressedBorder = System.Drawing.Color.Transparent;
            this.btnRefresh.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnRefresh.Size = new System.Drawing.Size(44, 40);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "";
            this.btnRefresh.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Transparent;
            this.btnEdit.BorderRadius = 10;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnEdit.HoverBorder = System.Drawing.Color.Transparent;
            this.btnEdit.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnEdit.Location = new System.Drawing.Point(368, 20);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.NormalBorder = System.Drawing.Color.Transparent;
            this.btnEdit.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnEdit.PressedBorder = System.Drawing.Color.Transparent;
            this.btnEdit.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnEdit.Size = new System.Drawing.Size(96, 40);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BorderRadius = 10;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDelete.HoverBorder = System.Drawing.Color.Transparent;
            this.btnDelete.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnDelete.Location = new System.Drawing.Point(260, 20);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.NormalBorder = System.Drawing.Color.Transparent;
            this.btnDelete.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnDelete.PressedBorder = System.Drawing.Color.Transparent;
            this.btnDelete.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.btnDelete.Size = new System.Drawing.Size(100, 40);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // pnlStats
            // 
            this.pnlStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlStats.Controls.Add(this.tlpStats);
            this.pnlStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStats.Location = new System.Drawing.Point(0, 76);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Padding = new System.Windows.Forms.Padding(24, 20, 24, 8);
            this.pnlStats.Size = new System.Drawing.Size(1440, 140);
            this.pnlStats.TabIndex = 1;
            // 
            // tlpStats
            // 
            this.tlpStats.ColumnCount = 4;
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStats.Controls.Add(this.pnlStat1, 0, 0);
            this.tlpStats.Controls.Add(this.pnlStat2, 1, 0);
            this.tlpStats.Controls.Add(this.pnlStat3, 2, 0);
            this.tlpStats.Controls.Add(this.pnlStat4, 3, 0);
            this.tlpStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStats.Location = new System.Drawing.Point(24, 20);
            this.tlpStats.Name = "tlpStats";
            this.tlpStats.RowCount = 1;
            this.tlpStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStats.Size = new System.Drawing.Size(1392, 112);
            this.tlpStats.TabIndex = 0;
            // 
            // pnlStat1
            // 
            this.pnlStat1.BackColor = System.Drawing.Color.White;
            this.pnlStat1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.pnlStat1.BorderRadius = 16;
            this.pnlStat1.BorderSize = 1;
            this.pnlStat1.Controls.Add(this.lblStat1Value);
            this.pnlStat1.Controls.Add(this.lblStat1Title);
            this.pnlStat1.Controls.Add(this.lblStat1Icon);
            this.pnlStat1.Controls.Add(this.pnlStat1Accent);
            this.pnlStat1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStat1.FillColor = System.Drawing.Color.White;
            this.pnlStat1.Location = new System.Drawing.Point(0, 0);
            this.pnlStat1.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.pnlStat1.Name = "pnlStat1";
            this.pnlStat1.Size = new System.Drawing.Size(332, 112);
            this.pnlStat1.TabIndex = 0;
            // 
            // lblStat1Value
            // 
            this.lblStat1Value.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStat1Value.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblStat1Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStat1Value.Location = new System.Drawing.Point(104, 48);
            this.lblStat1Value.Name = "lblStat1Value";
            this.lblStat1Value.Size = new System.Drawing.Size(220, 44);
            this.lblStat1Value.TabIndex = 3;
            this.lblStat1Value.Text = "0";
            // 
            // lblStat1Title
            // 
            this.lblStat1Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStat1Title.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Bold);
            this.lblStat1Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStat1Title.Location = new System.Drawing.Point(104, 28);
            this.lblStat1Title.Name = "lblStat1Title";
            this.lblStat1Title.Size = new System.Drawing.Size(220, 18);
            this.lblStat1Title.TabIndex = 2;
            this.lblStat1Title.Text = "TỔNG NGƯỜI DÙNG";
            // 
            // lblStat1Icon
            // 
            this.lblStat1Icon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblStat1Icon.Font = new System.Drawing.Font("Segoe MDL2 Assets", 22F);
            this.lblStat1Icon.ForeColor = System.Drawing.Color.White;
            this.lblStat1Icon.Location = new System.Drawing.Point(28, 26);
            this.lblStat1Icon.Name = "lblStat1Icon";
            this.lblStat1Icon.Size = new System.Drawing.Size(60, 60);
            this.lblStat1Icon.TabIndex = 1;
            this.lblStat1Icon.Text = "";
            this.lblStat1Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStat1Accent
            // 
            this.pnlStat1Accent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.pnlStat1Accent.Location = new System.Drawing.Point(0, 16);
            this.pnlStat1Accent.Name = "pnlStat1Accent";
            this.pnlStat1Accent.Size = new System.Drawing.Size(5, 80);
            this.pnlStat1Accent.TabIndex = 0;
            // 
            // pnlStat2
            // 
            this.pnlStat2.BackColor = System.Drawing.Color.White;
            this.pnlStat2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(250)))), ((int)(((byte)(229)))));
            this.pnlStat2.BorderRadius = 16;
            this.pnlStat2.BorderSize = 1;
            this.pnlStat2.Controls.Add(this.lblStat2Value);
            this.pnlStat2.Controls.Add(this.lblStat2Title);
            this.pnlStat2.Controls.Add(this.lblStat2Icon);
            this.pnlStat2.Controls.Add(this.pnlStat2Accent);
            this.pnlStat2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStat2.FillColor = System.Drawing.Color.White;
            this.pnlStat2.Location = new System.Drawing.Point(348, 0);
            this.pnlStat2.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.pnlStat2.Name = "pnlStat2";
            this.pnlStat2.Size = new System.Drawing.Size(332, 112);
            this.pnlStat2.TabIndex = 1;
            // 
            // lblStat2Value
            // 
            this.lblStat2Value.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStat2Value.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblStat2Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStat2Value.Location = new System.Drawing.Point(104, 48);
            this.lblStat2Value.Name = "lblStat2Value";
            this.lblStat2Value.Size = new System.Drawing.Size(220, 44);
            this.lblStat2Value.TabIndex = 3;
            this.lblStat2Value.Text = "0";
            // 
            // lblStat2Title
            // 
            this.lblStat2Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStat2Title.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Bold);
            this.lblStat2Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStat2Title.Location = new System.Drawing.Point(104, 28);
            this.lblStat2Title.Name = "lblStat2Title";
            this.lblStat2Title.Size = new System.Drawing.Size(220, 18);
            this.lblStat2Title.TabIndex = 2;
            this.lblStat2Title.Text = "ĐANG HOẠT ĐỘNG";
            // 
            // lblStat2Icon
            // 
            this.lblStat2Icon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblStat2Icon.Font = new System.Drawing.Font("Segoe MDL2 Assets", 22F);
            this.lblStat2Icon.ForeColor = System.Drawing.Color.White;
            this.lblStat2Icon.Location = new System.Drawing.Point(28, 26);
            this.lblStat2Icon.Name = "lblStat2Icon";
            this.lblStat2Icon.Size = new System.Drawing.Size(60, 60);
            this.lblStat2Icon.TabIndex = 1;
            this.lblStat2Icon.Text = "";
            this.lblStat2Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStat2Accent
            // 
            this.pnlStat2Accent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.pnlStat2Accent.Location = new System.Drawing.Point(0, 16);
            this.pnlStat2Accent.Name = "pnlStat2Accent";
            this.pnlStat2Accent.Size = new System.Drawing.Size(5, 80);
            this.pnlStat2Accent.TabIndex = 0;
            // 
            // pnlStat3
            // 
            this.pnlStat3.BackColor = System.Drawing.Color.White;
            this.pnlStat3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(243)))), ((int)(((byte)(199)))));
            this.pnlStat3.BorderRadius = 16;
            this.pnlStat3.BorderSize = 1;
            this.pnlStat3.Controls.Add(this.lblStat3Value);
            this.pnlStat3.Controls.Add(this.lblStat3Title);
            this.pnlStat3.Controls.Add(this.lblStat3Icon);
            this.pnlStat3.Controls.Add(this.pnlStat3Accent);
            this.pnlStat3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStat3.FillColor = System.Drawing.Color.White;
            this.pnlStat3.Location = new System.Drawing.Point(696, 0);
            this.pnlStat3.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            this.pnlStat3.Name = "pnlStat3";
            this.pnlStat3.Size = new System.Drawing.Size(332, 112);
            this.pnlStat3.TabIndex = 2;
            // 
            // lblStat3Value
            // 
            this.lblStat3Value.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStat3Value.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblStat3Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStat3Value.Location = new System.Drawing.Point(104, 48);
            this.lblStat3Value.Name = "lblStat3Value";
            this.lblStat3Value.Size = new System.Drawing.Size(220, 44);
            this.lblStat3Value.TabIndex = 3;
            this.lblStat3Value.Text = "0";
            // 
            // lblStat3Title
            // 
            this.lblStat3Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStat3Title.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Bold);
            this.lblStat3Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStat3Title.Location = new System.Drawing.Point(104, 28);
            this.lblStat3Title.Name = "lblStat3Title";
            this.lblStat3Title.Size = new System.Drawing.Size(220, 18);
            this.lblStat3Title.TabIndex = 2;
            this.lblStat3Title.Text = "MỚI TRONG THÁNG";
            // 
            // lblStat3Icon
            // 
            this.lblStat3Icon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.lblStat3Icon.Font = new System.Drawing.Font("Segoe MDL2 Assets", 22F);
            this.lblStat3Icon.ForeColor = System.Drawing.Color.White;
            this.lblStat3Icon.Location = new System.Drawing.Point(28, 26);
            this.lblStat3Icon.Name = "lblStat3Icon";
            this.lblStat3Icon.Size = new System.Drawing.Size(60, 60);
            this.lblStat3Icon.TabIndex = 1;
            this.lblStat3Icon.Text = "";
            this.lblStat3Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStat3Accent
            // 
            this.pnlStat3Accent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.pnlStat3Accent.Location = new System.Drawing.Point(0, 16);
            this.pnlStat3Accent.Name = "pnlStat3Accent";
            this.pnlStat3Accent.Size = new System.Drawing.Size(5, 80);
            this.pnlStat3Accent.TabIndex = 0;
            // 
            // pnlStat4
            // 
            this.pnlStat4.BackColor = System.Drawing.Color.White;
            this.pnlStat4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(233)))), ((int)(((byte)(254)))));
            this.pnlStat4.BorderRadius = 16;
            this.pnlStat4.BorderSize = 1;
            this.pnlStat4.Controls.Add(this.lblStat4Value);
            this.pnlStat4.Controls.Add(this.lblStat4Title);
            this.pnlStat4.Controls.Add(this.lblStat4Icon);
            this.pnlStat4.Controls.Add(this.pnlStat4Accent);
            this.pnlStat4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStat4.FillColor = System.Drawing.Color.White;
            this.pnlStat4.Location = new System.Drawing.Point(1044, 0);
            this.pnlStat4.Margin = new System.Windows.Forms.Padding(0);
            this.pnlStat4.Name = "pnlStat4";
            this.pnlStat4.Size = new System.Drawing.Size(348, 112);
            this.pnlStat4.TabIndex = 3;
            // 
            // lblStat4Value
            // 
            this.lblStat4Value.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStat4Value.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblStat4Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStat4Value.Location = new System.Drawing.Point(104, 48);
            this.lblStat4Value.Name = "lblStat4Value";
            this.lblStat4Value.Size = new System.Drawing.Size(236, 44);
            this.lblStat4Value.TabIndex = 3;
            this.lblStat4Value.Text = "0";
            // 
            // lblStat4Title
            // 
            this.lblStat4Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStat4Title.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Bold);
            this.lblStat4Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStat4Title.Location = new System.Drawing.Point(104, 28);
            this.lblStat4Title.Name = "lblStat4Title";
            this.lblStat4Title.Size = new System.Drawing.Size(236, 18);
            this.lblStat4Title.TabIndex = 2;
            this.lblStat4Title.Text = "TÀI KHOẢN ĐÃ MỞ";
            // 
            // lblStat4Icon
            // 
            this.lblStat4Icon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(92)))), ((int)(((byte)(246)))));
            this.lblStat4Icon.Font = new System.Drawing.Font("Segoe MDL2 Assets", 22F);
            this.lblStat4Icon.ForeColor = System.Drawing.Color.White;
            this.lblStat4Icon.Location = new System.Drawing.Point(28, 26);
            this.lblStat4Icon.Name = "lblStat4Icon";
            this.lblStat4Icon.Size = new System.Drawing.Size(60, 60);
            this.lblStat4Icon.TabIndex = 1;
            this.lblStat4Icon.Text = "";
            this.lblStat4Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStat4Accent
            // 
            this.pnlStat4Accent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(92)))), ((int)(((byte)(246)))));
            this.pnlStat4Accent.Location = new System.Drawing.Point(0, 16);
            this.pnlStat4Accent.Name = "pnlStat4Accent";
            this.pnlStat4Accent.Size = new System.Drawing.Size(5, 80);
            this.pnlStat4Accent.TabIndex = 0;
            // 
            // pnlStatus
            // 
            this.pnlStatus.BackColor = System.Drawing.Color.White;
            this.pnlStatus.Controls.Add(this.lblStatus);
            this.pnlStatus.Controls.Add(this.lblStatusDot);
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatus.Location = new System.Drawing.Point(0, 840);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(1440, 32);
            this.pnlStatus.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStatus.Location = new System.Drawing.Point(44, 8);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(240, 15);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Sẵn sàng  ·  Kết nối cơ sở dữ liệu thành công";
            // 
            // lblStatusDot
            // 
            this.lblStatusDot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.lblStatusDot.Location = new System.Drawing.Point(24, 12);
            this.lblStatusDot.Name = "lblStatusDot";
            this.lblStatusDot.Size = new System.Drawing.Size(8, 8);
            this.lblStatusDot.TabIndex = 0;
            // 
            // splitMain
            // 
            this.splitMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 216);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.pnlGridCard);
            this.splitMain.Panel1.Padding = new System.Windows.Forms.Padding(24, 16, 8, 20);
            this.splitMain.Panel1MinSize = 480;
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlDetailCard);
            this.splitMain.Panel2.Padding = new System.Windows.Forms.Padding(8, 16, 24, 20);
            this.splitMain.Panel2MinSize = 380;
            this.splitMain.Size = new System.Drawing.Size(1440, 624);
            this.splitMain.SplitterDistance = 900;
            this.splitMain.SplitterWidth = 6;
            this.splitMain.TabIndex = 2;
            // 
            // pnlGridCard
            // 
            this.pnlGridCard.BackColor = System.Drawing.Color.White;
            this.pnlGridCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlGridCard.BorderRadius = 14;
            this.pnlGridCard.BorderSize = 1;
            this.pnlGridCard.Controls.Add(this.dgvCustomers);
            this.pnlGridCard.Controls.Add(this.pnlGridHeader);
            this.pnlGridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridCard.FillColor = System.Drawing.Color.White;
            this.pnlGridCard.Location = new System.Drawing.Point(24, 16);
            this.pnlGridCard.Name = "pnlGridCard";
            this.pnlGridCard.Padding = new System.Windows.Forms.Padding(2);
            this.pnlGridCard.Size = new System.Drawing.Size(868, 588);
            this.pnlGridCard.TabIndex = 0;
            // 
            // dgvCustomers
            // 
            this.dgvCustomers.AllowUserToAddRows = false;
            this.dgvCustomers.AllowUserToDeleteRows = false;
            this.dgvCustomers.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(253)))), ((int)(((byte)(255)))));
            this.dgvCustomers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCustomers.BackgroundColor = System.Drawing.Color.White;
            this.dgvCustomers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCustomers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCustomers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCustomers.ColumnHeadersHeight = 46;
            this.dgvCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCustomers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaKH,
            this.colHoTen,
            this.colCMND,
            this.colNgaySinh,
            this.colGioiTinh,
            this.colSDT,
            this.colEmail,
            this.colDiaChi});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomers.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomers.EnableHeadersVisualStyles = false;
            this.dgvCustomers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvCustomers.Location = new System.Drawing.Point(2, 66);
            this.dgvCustomers.MultiSelect = false;
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.ReadOnly = true;
            this.dgvCustomers.RowHeadersVisible = false;
            this.dgvCustomers.RowHeadersWidth = 62;
            this.dgvCustomers.RowTemplate.Height = 46;
            this.dgvCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomers.Size = new System.Drawing.Size(864, 520);
            this.dgvCustomers.TabIndex = 1;
            // 
            // colMaKH
            // 
            this.colMaKH.DataPropertyName = "MaKH";
            this.colMaKH.FillWeight = 8F;
            this.colMaKH.HeaderText = "MÃ KH";
            this.colMaKH.MinimumWidth = 8;
            this.colMaKH.Name = "colMaKH";
            this.colMaKH.ReadOnly = true;
            // 
            // colHoTen
            // 
            this.colHoTen.DataPropertyName = "HoTen";
            this.colHoTen.FillWeight = 20F;
            this.colHoTen.HeaderText = "HỌ VÀ TÊN";
            this.colHoTen.MinimumWidth = 8;
            this.colHoTen.Name = "colHoTen";
            this.colHoTen.ReadOnly = true;
            // 
            // colCMND
            // 
            this.colCMND.DataPropertyName = "CMND";
            this.colCMND.FillWeight = 13F;
            this.colCMND.HeaderText = "CMND/CCCD";
            this.colCMND.MinimumWidth = 8;
            this.colCMND.Name = "colCMND";
            this.colCMND.ReadOnly = true;
            // 
            // colNgaySinh
            // 
            this.colNgaySinh.DataPropertyName = "NgaySinh";
            this.colNgaySinh.FillWeight = 12F;
            this.colNgaySinh.HeaderText = "NGÀY SINH";
            this.colNgaySinh.MinimumWidth = 8;
            this.colNgaySinh.Name = "colNgaySinh";
            this.colNgaySinh.ReadOnly = true;
            // 
            // colGioiTinh
            // 
            this.colGioiTinh.DataPropertyName = "GioiTinh";
            this.colGioiTinh.FillWeight = 10F;
            this.colGioiTinh.HeaderText = "GIỚI TÍNH";
            this.colGioiTinh.MinimumWidth = 8;
            this.colGioiTinh.Name = "colGioiTinh";
            this.colGioiTinh.ReadOnly = true;
            // 
            // colSDT
            // 
            this.colSDT.DataPropertyName = "SDT";
            this.colSDT.FillWeight = 13F;
            this.colSDT.HeaderText = "SĐT";
            this.colSDT.MinimumWidth = 8;
            this.colSDT.Name = "colSDT";
            this.colSDT.ReadOnly = true;
            // 
            // colEmail
            // 
            this.colEmail.DataPropertyName = "Email";
            this.colEmail.FillWeight = 24F;
            this.colEmail.HeaderText = "EMAIL";
            this.colEmail.MinimumWidth = 8;
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;
            // 
            // colDiaChi
            // 
            this.colDiaChi.DataPropertyName = "DiaChi";
            this.colDiaChi.FillWeight = 22F;
            this.colDiaChi.HeaderText = "ĐỊA CHỈ";
            this.colDiaChi.MinimumWidth = 8;
            this.colDiaChi.Name = "colDiaChi";
            this.colDiaChi.ReadOnly = true;
            // 
            // pnlGridHeader
            // 
            this.pnlGridHeader.BackColor = System.Drawing.Color.White;
            this.pnlGridHeader.Controls.Add(this.lblRecordCount);
            this.pnlGridHeader.Controls.Add(this.lblGridTitle);
            this.pnlGridHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGridHeader.Location = new System.Drawing.Point(2, 2);
            this.pnlGridHeader.Name = "pnlGridHeader";
            this.pnlGridHeader.Size = new System.Drawing.Size(864, 64);
            this.pnlGridHeader.TabIndex = 0;
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecordCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRecordCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblRecordCount.Location = new System.Drawing.Point(614, 24);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(230, 19);
            this.lblRecordCount.TabIndex = 1;
            this.lblRecordCount.Text = "0 khách hàng";
            this.lblRecordCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.AutoSize = true;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblGridTitle.Location = new System.Drawing.Point(20, 20);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(188, 23);
            this.lblGridTitle.TabIndex = 0;
            this.lblGridTitle.Text = "Danh sách khách hàng";
            // 
            // pnlDetailCard
            // 
            this.pnlDetailCard.BackColor = System.Drawing.Color.White;
            this.pnlDetailCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlDetailCard.BorderRadius = 14;
            this.pnlDetailCard.BorderSize = 1;
            this.pnlDetailCard.Controls.Add(this.pnlDetailScroll);
            this.pnlDetailCard.Controls.Add(this.tlpDetailActions);
            this.pnlDetailCard.Controls.Add(this.pnlDetailBanner);
            this.pnlDetailCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetailCard.FillColor = System.Drawing.Color.White;
            this.pnlDetailCard.Location = new System.Drawing.Point(8, 16);
            this.pnlDetailCard.Name = "pnlDetailCard";
            this.pnlDetailCard.Padding = new System.Windows.Forms.Padding(2);
            this.pnlDetailCard.Size = new System.Drawing.Size(502, 588);
            this.pnlDetailCard.TabIndex = 0;
            // 
            // pnlDetailScroll
            // 
            this.pnlDetailScroll.AutoScroll = true;
            this.pnlDetailScroll.BackColor = System.Drawing.Color.White;
            this.pnlDetailScroll.Controls.Add(this.tlpDetail);
            this.pnlDetailScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetailScroll.Location = new System.Drawing.Point(2, 78);
            this.pnlDetailScroll.Name = "pnlDetailScroll";
            this.pnlDetailScroll.Padding = new System.Windows.Forms.Padding(20, 8, 20, 8);
            this.pnlDetailScroll.Size = new System.Drawing.Size(498, 434);
            this.pnlDetailScroll.TabIndex = 1;
            // 
            // tlpDetail
            // 
            this.tlpDetail.AutoSize = true;
            this.tlpDetail.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpDetail.ColumnCount = 1;
            this.tlpDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDetail.Controls.Add(this.lblMaKH, 0, 0);
            this.tlpDetail.Controls.Add(this.txtMaKH, 0, 1);
            this.tlpDetail.Controls.Add(this.lblHoTen, 0, 3);
            this.tlpDetail.Controls.Add(this.txtHoTen, 0, 4);
            this.tlpDetail.Controls.Add(this.lblCMND, 0, 6);
            this.tlpDetail.Controls.Add(this.txtCMND, 0, 7);
            this.tlpDetail.Controls.Add(this.lblNgaySinh, 0, 9);
            this.tlpDetail.Controls.Add(this.dtpNgaySinh, 0, 10);
            this.tlpDetail.Controls.Add(this.lblGioiTinh, 0, 12);
            this.tlpDetail.Controls.Add(this.cboGioiTinh, 0, 13);
            this.tlpDetail.Controls.Add(this.lblSDT, 0, 15);
            this.tlpDetail.Controls.Add(this.txtSDT, 0, 16);
            this.tlpDetail.Controls.Add(this.lblEmail, 0, 18);
            this.tlpDetail.Controls.Add(this.txtEmail, 0, 19);
            this.tlpDetail.Controls.Add(this.lblDiaChi, 0, 21);
            this.tlpDetail.Controls.Add(this.txtDiaChi, 0, 22);
            this.tlpDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpDetail.Location = new System.Drawing.Point(20, 8);
            this.tlpDetail.Name = "tlpDetail";
            this.tlpDetail.RowCount = 23;
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 6F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlpDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tlpDetail.Size = new System.Drawing.Size(441, 456);
            this.tlpDetail.TabIndex = 0;
            // 
            // lblMaKH
            // 
            this.lblMaKH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaKH.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblMaKH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMaKH.Location = new System.Drawing.Point(0, 0);
            this.lblMaKH.Margin = new System.Windows.Forms.Padding(0);
            this.lblMaKH.Name = "lblMaKH";
            this.lblMaKH.Size = new System.Drawing.Size(441, 16);
            this.lblMaKH.TabIndex = 0;
            this.lblMaKH.Text = "MÃ KHÁCH HÀNG";
            // 
            // txtMaKH
            // 
            this.txtMaKH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtMaKH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaKH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMaKH.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaKH.Location = new System.Drawing.Point(0, 16);
            this.txtMaKH.Margin = new System.Windows.Forms.Padding(0);
            this.txtMaKH.Name = "txtMaKH";
            this.txtMaKH.ReadOnly = true;
            this.txtMaKH.Size = new System.Drawing.Size(441, 25);
            this.txtMaKH.TabIndex = 1;
            this.txtMaKH.Text = " ";
            // 
            // lblHoTen
            // 
            this.lblHoTen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblHoTen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblHoTen.Location = new System.Drawing.Point(0, 52);
            this.lblHoTen.Margin = new System.Windows.Forms.Padding(0);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(441, 16);
            this.lblHoTen.TabIndex = 2;
            this.lblHoTen.Text = "HỌ VÀ TÊN";
            // 
            // txtHoTen
            // 
            this.txtHoTen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHoTen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHoTen.Location = new System.Drawing.Point(0, 68);
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(0);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(441, 25);
            this.txtHoTen.TabIndex = 3;
            // 
            // lblCMND
            // 
            this.lblCMND.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCMND.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblCMND.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCMND.Location = new System.Drawing.Point(0, 104);
            this.lblCMND.Margin = new System.Windows.Forms.Padding(0);
            this.lblCMND.Name = "lblCMND";
            this.lblCMND.Size = new System.Drawing.Size(441, 16);
            this.lblCMND.TabIndex = 4;
            this.lblCMND.Text = "CMND / CCCD";
            // 
            // txtCMND
            // 
            this.txtCMND.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCMND.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCMND.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCMND.Location = new System.Drawing.Point(0, 120);
            this.txtCMND.Margin = new System.Windows.Forms.Padding(0);
            this.txtCMND.Name = "txtCMND";
            this.txtCMND.Size = new System.Drawing.Size(441, 25);
            this.txtCMND.TabIndex = 5;
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNgaySinh.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblNgaySinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblNgaySinh.Location = new System.Drawing.Point(0, 156);
            this.lblNgaySinh.Margin = new System.Windows.Forms.Padding(0);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(441, 16);
            this.lblNgaySinh.TabIndex = 6;
            this.lblNgaySinh.Text = "NGÀY SINH";
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaySinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpNgaySinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgaySinh.Location = new System.Drawing.Point(0, 172);
            this.dtpNgaySinh.Margin = new System.Windows.Forms.Padding(0);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new System.Drawing.Size(441, 25);
            this.dtpNgaySinh.TabIndex = 7;
            // 
            // lblGioiTinh
            // 
            this.lblGioiTinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblGioiTinh.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblGioiTinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblGioiTinh.Location = new System.Drawing.Point(0, 208);
            this.lblGioiTinh.Margin = new System.Windows.Forms.Padding(0);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Size = new System.Drawing.Size(441, 16);
            this.lblGioiTinh.TabIndex = 8;
            this.lblGioiTinh.Text = "GIỚI TÍNH";
            // 
            // cboGioiTinh
            // 
            this.cboGioiTinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboGioiTinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGioiTinh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboGioiTinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboGioiTinh.Items.AddRange(new object[] {
            "Nam",
            "Nữ",
            "Khác"});
            this.cboGioiTinh.Location = new System.Drawing.Point(0, 224);
            this.cboGioiTinh.Margin = new System.Windows.Forms.Padding(0);
            this.cboGioiTinh.Name = "cboGioiTinh";
            this.cboGioiTinh.Size = new System.Drawing.Size(441, 25);
            this.cboGioiTinh.TabIndex = 9;
            // 
            // lblSDT
            // 
            this.lblSDT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSDT.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblSDT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSDT.Location = new System.Drawing.Point(0, 260);
            this.lblSDT.Margin = new System.Windows.Forms.Padding(0);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(441, 16);
            this.lblSDT.TabIndex = 10;
            this.lblSDT.Text = "SỐ ĐIỆN THOẠI";
            // 
            // txtSDT
            // 
            this.txtSDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSDT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSDT.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSDT.Location = new System.Drawing.Point(0, 276);
            this.txtSDT.Margin = new System.Windows.Forms.Padding(0);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(441, 25);
            this.txtSDT.TabIndex = 11;
            // 
            // lblEmail
            // 
            this.lblEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblEmail.Location = new System.Drawing.Point(0, 312);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(441, 16);
            this.lblEmail.TabIndex = 12;
            this.lblEmail.Text = "EMAIL";
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Location = new System.Drawing.Point(0, 328);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(0);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(441, 25);
            this.txtEmail.TabIndex = 13;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblDiaChi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDiaChi.Location = new System.Drawing.Point(0, 364);
            this.lblDiaChi.Margin = new System.Windows.Forms.Padding(0);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(441, 16);
            this.lblDiaChi.TabIndex = 14;
            this.lblDiaChi.Text = "ĐỊA CHỈ";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDiaChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDiaChi.Location = new System.Drawing.Point(0, 380);
            this.txtDiaChi.Margin = new System.Windows.Forms.Padding(0);
            this.txtDiaChi.Multiline = true;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDiaChi.Size = new System.Drawing.Size(441, 76);
            this.txtDiaChi.TabIndex = 15;
            // 
            // tlpDetailActions
            // 
            this.tlpDetailActions.BackColor = System.Drawing.Color.White;
            this.tlpDetailActions.ColumnCount = 2;
            this.tlpDetailActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDetailActions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDetailActions.Controls.Add(this.btnCancel, 0, 0);
            this.tlpDetailActions.Controls.Add(this.btnSave, 1, 0);
            this.tlpDetailActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpDetailActions.Location = new System.Drawing.Point(2, 512);
            this.tlpDetailActions.Name = "tlpDetailActions";
            this.tlpDetailActions.Padding = new System.Windows.Forms.Padding(20, 0, 20, 18);
            this.tlpDetailActions.RowCount = 1;
            this.tlpDetailActions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDetailActions.Size = new System.Drawing.Size(498, 74);
            this.tlpDetailActions.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BorderRadius = 10;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnCancel.HoverBorder = System.Drawing.Color.Transparent;
            this.btnCancel.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCancel.Location = new System.Drawing.Point(20, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalBorder = System.Drawing.Color.Transparent;
            this.btnCancel.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnCancel.PressedBorder = System.Drawing.Color.Transparent;
            this.btnCancel.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnCancel.Size = new System.Drawing.Size(223, 56);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BorderRadius = 10;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.HoverBorder = System.Drawing.Color.Transparent;
            this.btnSave.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.btnSave.Location = new System.Drawing.Point(255, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.NormalBorder = System.Drawing.Color.Transparent;
            this.btnSave.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSave.PressedBorder = System.Drawing.Color.Transparent;
            this.btnSave.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnSave.Size = new System.Drawing.Size(223, 56);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Lưu lại";
            this.btnSave.TextColor = System.Drawing.Color.White;
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // pnlDetailBanner
            // 
            this.pnlDetailBanner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlDetailBanner.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlDetailBanner.BorderRadius = 12;
            this.pnlDetailBanner.BorderSize = 0;
            this.pnlDetailBanner.Controls.Add(this.lblDetailSubtitle);
            this.pnlDetailBanner.Controls.Add(this.lblDetailTitle);
            this.pnlDetailBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDetailBanner.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlDetailBanner.Location = new System.Drawing.Point(2, 2);
            this.pnlDetailBanner.Name = "pnlDetailBanner";
            this.pnlDetailBanner.Padding = new System.Windows.Forms.Padding(20, 14, 20, 14);
            this.pnlDetailBanner.Size = new System.Drawing.Size(498, 76);
            this.pnlDetailBanner.TabIndex = 0;
            // 
            // lblDetailSubtitle
            // 
            this.lblDetailSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetailSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblDetailSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDetailSubtitle.Location = new System.Drawing.Point(20, 40);
            this.lblDetailSubtitle.Name = "lblDetailSubtitle";
            this.lblDetailSubtitle.Size = new System.Drawing.Size(458, 16);
            this.lblDetailSubtitle.TabIndex = 1;
            this.lblDetailSubtitle.Text = "Chọn một khách hàng để xem chi tiết";
            // 
            // lblDetailTitle
            // 
            this.lblDetailTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetailTitle.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblDetailTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblDetailTitle.Location = new System.Drawing.Point(20, 14);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Size = new System.Drawing.Size(458, 26);
            this.lblDetailTitle.TabIndex = 0;
            this.lblDetailTitle.Text = "Thông tin khách hàng";
            // 
            // CustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1440, 872);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlStatus);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.pnlToolbar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(1200, 720);
            this.Name = "CustomerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Khách hàng — Bank Account Management";
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbarLeft.ResumeLayout(false);
            this.pnlSearchBox.ResumeLayout(false);
            this.pnlSearchBox.PerformLayout();
            this.pnlToolbarRight.ResumeLayout(false);
            this.flpToolbarRight.ResumeLayout(false);
            this.pnlStats.ResumeLayout(false);
            this.tlpStats.ResumeLayout(false);
            this.pnlStat1.ResumeLayout(false);
            this.pnlStat2.ResumeLayout(false);
            this.pnlStat3.ResumeLayout(false);
            this.pnlStat4.ResumeLayout(false);
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pnlGridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.pnlGridHeader.ResumeLayout(false);
            this.pnlGridHeader.PerformLayout();
            this.pnlDetailCard.ResumeLayout(false);
            this.pnlDetailScroll.ResumeLayout(false);
            this.pnlDetailScroll.PerformLayout();
            this.tlpDetail.ResumeLayout(false);
            this.tlpDetail.PerformLayout();
            this.tlpDetailActions.ResumeLayout(false);
            this.pnlDetailBanner.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        // --------- Fields ---------
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Panel pnlToolbarLeft;
        private RoundedPanel pnlSearchBox;
        private System.Windows.Forms.Label lblSearchIcon;
        private System.Windows.Forms.TextBox txtSearch;
        private RoundedButton btnSearch;
        private System.Windows.Forms.Panel pnlToolbarRight;
        private System.Windows.Forms.FlowLayoutPanel flpToolbarRight;
        private RoundedButton btnRefresh;
        private RoundedButton btnDelete;
        private RoundedButton btnEdit;

        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.TableLayoutPanel tlpStats;

        private RoundedPanel pnlStat1;
        private System.Windows.Forms.Panel pnlStat1Accent;
        private System.Windows.Forms.Label lblStat1Icon;
        private System.Windows.Forms.Label lblStat1Title;
        private System.Windows.Forms.Label lblStat1Value;

        private RoundedPanel pnlStat2;
        private System.Windows.Forms.Panel pnlStat2Accent;
        private System.Windows.Forms.Label lblStat2Icon;
        private System.Windows.Forms.Label lblStat2Title;
        private System.Windows.Forms.Label lblStat2Value;

        private RoundedPanel pnlStat3;
        private System.Windows.Forms.Panel pnlStat3Accent;
        private System.Windows.Forms.Label lblStat3Icon;
        private System.Windows.Forms.Label lblStat3Title;
        private System.Windows.Forms.Label lblStat3Value;

        private RoundedPanel pnlStat4;
        private System.Windows.Forms.Panel pnlStat4Accent;
        private System.Windows.Forms.Label lblStat4Icon;
        private System.Windows.Forms.Label lblStat4Title;
        private System.Windows.Forms.Label lblStat4Value;

        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label lblStatusDot;
        private System.Windows.Forms.Label lblStatus;

        private System.Windows.Forms.SplitContainer splitMain;

        private RoundedPanel pnlGridCard;
        private System.Windows.Forms.Panel pnlGridHeader;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.Label lblRecordCount;
        private System.Windows.Forms.DataGridView dgvCustomers;

        private System.Windows.Forms.DataGridViewTextBoxColumn colMaKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoTen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCMND;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgaySinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGioiTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSDT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiaChi;

        private RoundedPanel pnlDetailCard;
        private RoundedPanel pnlDetailBanner;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.Label lblDetailSubtitle;
        private System.Windows.Forms.TableLayoutPanel tlpDetailActions;
        private RoundedButton btnSave;
        private RoundedButton btnCancel;
        private System.Windows.Forms.Panel pnlDetailScroll;
        private System.Windows.Forms.TableLayoutPanel tlpDetail;

        private System.Windows.Forms.Label lblMaKH;
        private System.Windows.Forms.TextBox txtMaKH;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblCMND;
        private System.Windows.Forms.TextBox txtCMND;
        private System.Windows.Forms.Label lblNgaySinh;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh;
        private System.Windows.Forms.Label lblGioiTinh;
        private System.Windows.Forms.ComboBox cboGioiTinh;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;
    }
}