namespace BankAccountManagement.Forms
{
    partial class StatisticsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CleanupResources();
                if (components != null)
                {
                    components.Dispose();
                }
                if (chartTransactions != null) { chartTransactions.Dispose(); }
                if (chartAccountTypes != null) { chartAccountTypes.Dispose(); }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticsForm));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.pictureBoxHeaderIcon = new System.Windows.Forms.PictureBox();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.labelFrom = new System.Windows.Forms.Label();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.labelTo = new System.Windows.Forms.Label();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.buttonViewStats = new System.Windows.Forms.Button();
            this.panelKpi = new System.Windows.Forms.Panel();
            this.tableLayoutKpi = new System.Windows.Forms.TableLayoutPanel();
            this.panelKpi1 = new System.Windows.Forms.Panel();
            this.panelKpi1Icon = new System.Windows.Forms.Panel();
            this.labelKpi1Icon = new System.Windows.Forms.Label();
            this.labelKpi1Title = new System.Windows.Forms.Label();
            this.lblTotalAccounts = new System.Windows.Forms.Label();
            this.labelKpi1Sub = new System.Windows.Forms.Label();
            this.panelKpi2 = new System.Windows.Forms.Panel();
            this.panelKpi2Icon = new System.Windows.Forms.Panel();
            this.labelKpi2Icon = new System.Windows.Forms.Label();
            this.labelKpi2Title = new System.Windows.Forms.Label();
            this.lblTotalBalance = new System.Windows.Forms.Label();
            this.labelKpi2Sub = new System.Windows.Forms.Label();
            this.panelKpi3 = new System.Windows.Forms.Panel();
            this.panelKpi3Icon = new System.Windows.Forms.Panel();
            this.labelKpi3Icon = new System.Windows.Forms.Label();
            this.labelKpi3Title = new System.Windows.Forms.Label();
            this.lblTotalTransactions = new System.Windows.Forms.Label();
            this.labelKpi3Sub = new System.Windows.Forms.Label();
            this.panelKpi4 = new System.Windows.Forms.Panel();
            this.panelKpi4Icon = new System.Windows.Forms.Panel();
            this.labelKpi4Icon = new System.Windows.Forms.Label();
            this.labelKpi4Title = new System.Windows.Forms.Label();
            this.lblTotalCustomers = new System.Windows.Forms.Label();
            this.labelKpi4Sub = new System.Windows.Forms.Label();
            this.panelCharts = new System.Windows.Forms.Panel();
            this.tableLayoutCharts = new System.Windows.Forms.TableLayoutPanel();
            this.panelChartBar = new System.Windows.Forms.Panel();
            this.chartTransactions = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labelChartBarTitle = new System.Windows.Forms.Label();
            this.panelChartDonut = new System.Windows.Forms.Panel();
            this.chartAccountTypes = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labelChartDonutTitle = new System.Windows.Forms.Label();
            this.labelDonutCenter1 = new System.Windows.Forms.Label();
            this.labelDonutCenter2 = new System.Windows.Forms.Label();
            this.panelTopAccounts = new System.Windows.Forms.Panel();
            this.dgvTopAccounts = new System.Windows.Forms.DataGridView();
            this.colSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoTaiKhoan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChuTaiKhoan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoDu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelTopTitle = new System.Windows.Forms.Label();
            this.linkViewAll = new System.Windows.Forms.LinkLabel();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.labelInfoIcon = new System.Windows.Forms.Label();
            this.labelUpdatedAt = new System.Windows.Forms.Label();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeaderIcon)).BeginInit();
            this.panelFilter.SuspendLayout();
            this.panelKpi.SuspendLayout();
            this.tableLayoutKpi.SuspendLayout();
            this.panelKpi1.SuspendLayout();
            this.panelKpi1Icon.SuspendLayout();
            this.panelKpi2.SuspendLayout();
            this.panelKpi2Icon.SuspendLayout();
            this.panelKpi3.SuspendLayout();
            this.panelKpi3Icon.SuspendLayout();
            this.panelKpi4.SuspendLayout();
            this.panelKpi4Icon.SuspendLayout();
            this.panelCharts.SuspendLayout();
            this.tableLayoutCharts.SuspendLayout();
            this.panelChartBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTransactions)).BeginInit();
            this.panelChartDonut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAccountTypes)).BeginInit();
            this.panelTopAccounts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopAccounts)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.pictureBoxHeaderIcon);
            this.panelHeader.Controls.Add(this.labelTitle);
            this.panelHeader.Controls.Add(this.labelSubtitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1180, 82);
            this.panelHeader.TabIndex = 0;
            // 
            // pictureBoxHeaderIcon
            // 
            this.pictureBoxHeaderIcon.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxHeaderIcon.Image")));
            this.pictureBoxHeaderIcon.Location = new System.Drawing.Point(24, 21);
            this.pictureBoxHeaderIcon.Name = "pictureBoxHeaderIcon";
            this.pictureBoxHeaderIcon.Size = new System.Drawing.Size(38, 38);
            this.pictureBoxHeaderIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxHeaderIcon.TabIndex = 0;
            this.pictureBoxHeaderIcon.TabStop = false;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.labelTitle.Location = new System.Drawing.Point(74, 12);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(116, 32);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Thống kê";
            // 
            // labelSubtitle
            // 
            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.labelSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelSubtitle.Location = new System.Drawing.Point(76, 47);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(245, 17);
            this.labelSubtitle.TabIndex = 2;
            this.labelSubtitle.Text = "Theo dõi tình hình tài khoản và giao dịch";
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.White;
            this.panelFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFilter.Controls.Add(this.labelFrom);
            this.panelFilter.Controls.Add(this.dateFrom);
            this.panelFilter.Controls.Add(this.labelTo);
            this.panelFilter.Controls.Add(this.dateTo);
            this.panelFilter.Controls.Add(this.buttonViewStats);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 82);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1180, 88);
            this.panelFilter.TabIndex = 1;
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.labelFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.labelFrom.Location = new System.Drawing.Point(22, 13);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(50, 15);
            this.labelFrom.TabIndex = 0;
            this.labelFrom.Text = "Từ ngày";
            // 
            // dateFrom
            // 
            this.dateFrom.CustomFormat = "dd/MM/yyyy";
            this.dateFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateFrom.Location = new System.Drawing.Point(20, 35);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(180, 25);
            this.dateFrom.TabIndex = 1;
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.labelTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.labelTo.Location = new System.Drawing.Point(220, 13);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(58, 15);
            this.labelTo.TabIndex = 2;
            this.labelTo.Text = "Đến ngày";
            // 
            // dateTo
            // 
            this.dateTo.CustomFormat = "dd/MM/yyyy";
            this.dateTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTo.Location = new System.Drawing.Point(218, 35);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(180, 25);
            this.dateTo.TabIndex = 3;
            // 
            // buttonViewStats
            // 
            this.buttonViewStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.buttonViewStats.FlatAppearance.BorderSize = 0;
            this.buttonViewStats.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.buttonViewStats.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonViewStats.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonViewStats.ForeColor = System.Drawing.Color.White;
            this.buttonViewStats.Location = new System.Drawing.Point(418, 32);
            this.buttonViewStats.Name = "buttonViewStats";
            this.buttonViewStats.Size = new System.Drawing.Size(145, 32);
            this.buttonViewStats.TabIndex = 4;
            this.buttonViewStats.Text = "Xem thống kê";
            this.buttonViewStats.UseVisualStyleBackColor = false;
            this.buttonViewStats.Click += new System.EventHandler(this.buttonViewStats_Click);
            // 
            // panelKpi
            // 
            this.panelKpi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.panelKpi.Controls.Add(this.tableLayoutKpi);
            this.panelKpi.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelKpi.Location = new System.Drawing.Point(0, 170);
            this.panelKpi.Name = "panelKpi";
            this.panelKpi.Size = new System.Drawing.Size(1180, 120);
            this.panelKpi.TabIndex = 2;
            // 
            // tableLayoutKpi
            // 
            this.tableLayoutKpi.ColumnCount = 4;
            this.tableLayoutKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutKpi.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutKpi.Controls.Add(this.panelKpi1, 0, 0);
            this.tableLayoutKpi.Controls.Add(this.panelKpi2, 1, 0);
            this.tableLayoutKpi.Controls.Add(this.panelKpi3, 2, 0);
            this.tableLayoutKpi.Controls.Add(this.panelKpi4, 3, 0);
            this.tableLayoutKpi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutKpi.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutKpi.Name = "tableLayoutKpi";
            this.tableLayoutKpi.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.tableLayoutKpi.RowCount = 1;
            this.tableLayoutKpi.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutKpi.Size = new System.Drawing.Size(1180, 120);
            this.tableLayoutKpi.TabIndex = 0;
            // 
            // panelKpi1
            // 
            this.panelKpi1.BackColor = System.Drawing.Color.White;
            this.panelKpi1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelKpi1.Controls.Add(this.panelKpi1Icon);
            this.panelKpi1.Controls.Add(this.labelKpi1Title);
            this.panelKpi1.Controls.Add(this.lblTotalAccounts);
            this.panelKpi1.Controls.Add(this.labelKpi1Sub);
            this.panelKpi1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKpi1.Location = new System.Drawing.Point(20, 10);
            this.panelKpi1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.panelKpi1.Name = "panelKpi1";
            this.panelKpi1.Size = new System.Drawing.Size(276, 100);
            this.panelKpi1.TabIndex = 0;
            // 
            // panelKpi1Icon
            // 
            this.panelKpi1Icon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.panelKpi1Icon.Controls.Add(this.labelKpi1Icon);
            this.panelKpi1Icon.Location = new System.Drawing.Point(14, 19);
            this.panelKpi1Icon.Name = "panelKpi1Icon";
            this.panelKpi1Icon.Size = new System.Drawing.Size(54, 54);
            this.panelKpi1Icon.TabIndex = 0;
            // 
            // labelKpi1Icon
            // 
            this.labelKpi1Icon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelKpi1Icon.Font = new System.Drawing.Font("Segoe UI Symbol", 18F);
            this.labelKpi1Icon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.labelKpi1Icon.Location = new System.Drawing.Point(0, 0);
            this.labelKpi1Icon.Name = "labelKpi1Icon";
            this.labelKpi1Icon.Size = new System.Drawing.Size(54, 54);
            this.labelKpi1Icon.TabIndex = 0;
            this.labelKpi1Icon.Text = "♙";
            this.labelKpi1Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelKpi1Title
            // 
            this.labelKpi1Title.AutoSize = true;
            this.labelKpi1Title.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.labelKpi1Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelKpi1Title.Location = new System.Drawing.Point(82, 13);
            this.labelKpi1Title.Name = "labelKpi1Title";
            this.labelKpi1Title.Size = new System.Drawing.Size(102, 15);
            this.labelKpi1Title.TabIndex = 1;
            this.labelKpi1Title.Text = "Tổng số tài khoản";
            // 
            // lblTotalAccounts
            // 
            this.lblTotalAccounts.AutoSize = true;
            this.lblTotalAccounts.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold);
            this.lblTotalAccounts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblTotalAccounts.Location = new System.Drawing.Point(80, 31);
            this.lblTotalAccounts.Name = "lblTotalAccounts";
            this.lblTotalAccounts.Size = new System.Drawing.Size(27, 31);
            this.lblTotalAccounts.TabIndex = 2;
            this.lblTotalAccounts.Text = "0";
            // 
            // labelKpi1Sub
            // 
            this.labelKpi1Sub.AutoSize = true;
            this.labelKpi1Sub.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.labelKpi1Sub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.labelKpi1Sub.Location = new System.Drawing.Point(82, 66);
            this.labelKpi1Sub.Name = "labelKpi1Sub";
            this.labelKpi1Sub.Size = new System.Drawing.Size(56, 13);
            this.labelKpi1Sub.TabIndex = 3;
            this.labelKpi1Sub.Text = "Tài khoản";
            // 
            // panelKpi2
            // 
            this.panelKpi2.BackColor = System.Drawing.Color.White;
            this.panelKpi2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelKpi2.Controls.Add(this.panelKpi2Icon);
            this.panelKpi2.Controls.Add(this.labelKpi2Title);
            this.panelKpi2.Controls.Add(this.lblTotalBalance);
            this.panelKpi2.Controls.Add(this.labelKpi2Sub);
            this.panelKpi2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKpi2.Location = new System.Drawing.Point(308, 10);
            this.panelKpi2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.panelKpi2.Name = "panelKpi2";
            this.panelKpi2.Size = new System.Drawing.Size(276, 100);
            this.panelKpi2.TabIndex = 1;
            // 
            // panelKpi2Icon
            // 
            this.panelKpi2Icon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(253)))), ((int)(((byte)(245)))));
            this.panelKpi2Icon.Controls.Add(this.labelKpi2Icon);
            this.panelKpi2Icon.Location = new System.Drawing.Point(14, 19);
            this.panelKpi2Icon.Name = "panelKpi2Icon";
            this.panelKpi2Icon.Size = new System.Drawing.Size(54, 54);
            this.panelKpi2Icon.TabIndex = 0;
            // 
            // labelKpi2Icon
            // 
            this.labelKpi2Icon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelKpi2Icon.Font = new System.Drawing.Font("Segoe UI Symbol", 18F);
            this.labelKpi2Icon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.labelKpi2Icon.Location = new System.Drawing.Point(0, 0);
            this.labelKpi2Icon.Name = "labelKpi2Icon";
            this.labelKpi2Icon.Size = new System.Drawing.Size(54, 54);
            this.labelKpi2Icon.TabIndex = 0;
            this.labelKpi2Icon.Text = "₫";
            this.labelKpi2Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelKpi2Title
            // 
            this.labelKpi2Title.AutoSize = true;
            this.labelKpi2Title.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.labelKpi2Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelKpi2Title.Location = new System.Drawing.Point(82, 13);
            this.labelKpi2Title.Name = "labelKpi2Title";
            this.labelKpi2Title.Size = new System.Drawing.Size(67, 15);
            this.labelKpi2Title.TabIndex = 1;
            this.labelKpi2Title.Text = "Tổng số dư";
            // 
            // lblTotalBalance
            // 
            this.lblTotalBalance.AutoSize = true;
            this.lblTotalBalance.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold);
            this.lblTotalBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.lblTotalBalance.Location = new System.Drawing.Point(80, 32);
            this.lblTotalBalance.Name = "lblTotalBalance";
            this.lblTotalBalance.Size = new System.Drawing.Size(23, 28);
            this.lblTotalBalance.TabIndex = 2;
            this.lblTotalBalance.Text = "0";
            // 
            // labelKpi2Sub
            // 
            this.labelKpi2Sub.AutoSize = true;
            this.labelKpi2Sub.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.labelKpi2Sub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.labelKpi2Sub.Location = new System.Drawing.Point(82, 66);
            this.labelKpi2Sub.Name = "labelKpi2Sub";
            this.labelKpi2Sub.Size = new System.Drawing.Size(30, 13);
            this.labelKpi2Sub.TabIndex = 3;
            this.labelKpi2Sub.Text = "VND";
            // 
            // panelKpi3
            // 
            this.panelKpi3.BackColor = System.Drawing.Color.White;
            this.panelKpi3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelKpi3.Controls.Add(this.panelKpi3Icon);
            this.panelKpi3.Controls.Add(this.labelKpi3Title);
            this.panelKpi3.Controls.Add(this.lblTotalTransactions);
            this.panelKpi3.Controls.Add(this.labelKpi3Sub);
            this.panelKpi3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKpi3.Location = new System.Drawing.Point(596, 10);
            this.panelKpi3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.panelKpi3.Name = "panelKpi3";
            this.panelKpi3.Size = new System.Drawing.Size(276, 100);
            this.panelKpi3.TabIndex = 2;
            // 
            // panelKpi3Icon
            // 
            this.panelKpi3Icon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.panelKpi3Icon.Controls.Add(this.labelKpi3Icon);
            this.panelKpi3Icon.Location = new System.Drawing.Point(14, 19);
            this.panelKpi3Icon.Name = "panelKpi3Icon";
            this.panelKpi3Icon.Size = new System.Drawing.Size(54, 54);
            this.panelKpi3Icon.TabIndex = 0;
            // 
            // labelKpi3Icon
            // 
            this.labelKpi3Icon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelKpi3Icon.Font = new System.Drawing.Font("Segoe UI Symbol", 18F);
            this.labelKpi3Icon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.labelKpi3Icon.Location = new System.Drawing.Point(0, 0);
            this.labelKpi3Icon.Name = "labelKpi3Icon";
            this.labelKpi3Icon.Size = new System.Drawing.Size(54, 54);
            this.labelKpi3Icon.TabIndex = 0;
            this.labelKpi3Icon.Text = "⇄";
            this.labelKpi3Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelKpi3Title
            // 
            this.labelKpi3Title.AutoSize = true;
            this.labelKpi3Title.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.labelKpi3Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelKpi3Title.Location = new System.Drawing.Point(82, 13);
            this.labelKpi3Title.Name = "labelKpi3Title";
            this.labelKpi3Title.Size = new System.Drawing.Size(104, 15);
            this.labelKpi3Title.TabIndex = 1;
            this.labelKpi3Title.Text = "Giao dịch trong kỳ";
            // 
            // lblTotalTransactions
            // 
            this.lblTotalTransactions.AutoSize = true;
            this.lblTotalTransactions.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold);
            this.lblTotalTransactions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(58)))), ((int)(((byte)(237)))));
            this.lblTotalTransactions.Location = new System.Drawing.Point(80, 31);
            this.lblTotalTransactions.Name = "lblTotalTransactions";
            this.lblTotalTransactions.Size = new System.Drawing.Size(27, 31);
            this.lblTotalTransactions.TabIndex = 2;
            this.lblTotalTransactions.Text = "0";
            // 
            // labelKpi3Sub
            // 
            this.labelKpi3Sub.AutoSize = true;
            this.labelKpi3Sub.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.labelKpi3Sub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.labelKpi3Sub.Location = new System.Drawing.Point(82, 66);
            this.labelKpi3Sub.Name = "labelKpi3Sub";
            this.labelKpi3Sub.Size = new System.Drawing.Size(56, 13);
            this.labelKpi3Sub.TabIndex = 3;
            this.labelKpi3Sub.Text = "Giao dịch";
            // 
            // panelKpi4
            // 
            this.panelKpi4.BackColor = System.Drawing.Color.White;
            this.panelKpi4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelKpi4.Controls.Add(this.panelKpi4Icon);
            this.panelKpi4.Controls.Add(this.labelKpi4Title);
            this.panelKpi4.Controls.Add(this.lblTotalCustomers);
            this.panelKpi4.Controls.Add(this.labelKpi4Sub);
            this.panelKpi4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKpi4.Location = new System.Drawing.Point(884, 10);
            this.panelKpi4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.panelKpi4.Name = "panelKpi4";
            this.panelKpi4.Size = new System.Drawing.Size(276, 100);
            this.panelKpi4.TabIndex = 3;
            // 
            // panelKpi4Icon
            // 
            this.panelKpi4Icon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(247)))), ((int)(((byte)(237)))));
            this.panelKpi4Icon.Controls.Add(this.labelKpi4Icon);
            this.panelKpi4Icon.Location = new System.Drawing.Point(14, 19);
            this.panelKpi4Icon.Name = "panelKpi4Icon";
            this.panelKpi4Icon.Size = new System.Drawing.Size(54, 54);
            this.panelKpi4Icon.TabIndex = 0;
            // 
            // labelKpi4Icon
            // 
            this.labelKpi4Icon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelKpi4Icon.Font = new System.Drawing.Font("Segoe UI Symbol", 18F);
            this.labelKpi4Icon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.labelKpi4Icon.Location = new System.Drawing.Point(0, 0);
            this.labelKpi4Icon.Name = "labelKpi4Icon";
            this.labelKpi4Icon.Size = new System.Drawing.Size(54, 54);
            this.labelKpi4Icon.TabIndex = 0;
            this.labelKpi4Icon.Text = "♙";
            this.labelKpi4Icon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelKpi4Title
            // 
            this.labelKpi4Title.AutoSize = true;
            this.labelKpi4Title.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.labelKpi4Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelKpi4Title.Location = new System.Drawing.Point(82, 13);
            this.labelKpi4Title.Name = "labelKpi4Title";
            this.labelKpi4Title.Size = new System.Drawing.Size(70, 15);
            this.labelKpi4Title.TabIndex = 1;
            this.labelKpi4Title.Text = "Khách hàng";
            // 
            // lblTotalCustomers
            // 
            this.lblTotalCustomers.AutoSize = true;
            this.lblTotalCustomers.Font = new System.Drawing.Font("Segoe UI Semibold", 17F, System.Drawing.FontStyle.Bold);
            this.lblTotalCustomers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.lblTotalCustomers.Location = new System.Drawing.Point(80, 31);
            this.lblTotalCustomers.Name = "lblTotalCustomers";
            this.lblTotalCustomers.Size = new System.Drawing.Size(27, 31);
            this.lblTotalCustomers.TabIndex = 2;
            this.lblTotalCustomers.Text = "0";
            // 
            // labelKpi4Sub
            // 
            this.labelKpi4Sub.AutoSize = true;
            this.labelKpi4Sub.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.labelKpi4Sub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.labelKpi4Sub.Location = new System.Drawing.Point(82, 66);
            this.labelKpi4Sub.Name = "labelKpi4Sub";
            this.labelKpi4Sub.Size = new System.Drawing.Size(68, 13);
            this.labelKpi4Sub.TabIndex = 3;
            this.labelKpi4Sub.Text = "Khách hàng";
            // 
            // panelCharts
            // 
            this.panelCharts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.panelCharts.Controls.Add(this.tableLayoutCharts);
            this.panelCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCharts.Location = new System.Drawing.Point(0, 290);
            this.panelCharts.Name = "panelCharts";
            this.panelCharts.Size = new System.Drawing.Size(1180, 430);
            this.panelCharts.TabIndex = 3;
            // 
            // tableLayoutCharts
            // 
            this.tableLayoutCharts.ColumnCount = 3;
            this.tableLayoutCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutCharts.Controls.Add(this.panelChartBar, 0, 0);
            this.tableLayoutCharts.Controls.Add(this.panelChartDonut, 1, 0);
            this.tableLayoutCharts.Controls.Add(this.panelTopAccounts, 2, 0);
            this.tableLayoutCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutCharts.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutCharts.Name = "tableLayoutCharts";
            this.tableLayoutCharts.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.tableLayoutCharts.RowCount = 1;
            this.tableLayoutCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutCharts.Size = new System.Drawing.Size(1180, 430);
            this.tableLayoutCharts.TabIndex = 0;
            // 
            // panelChartBar
            // 
            this.panelChartBar.BackColor = System.Drawing.Color.White;
            this.panelChartBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChartBar.Controls.Add(this.chartTransactions);
            this.panelChartBar.Controls.Add(this.labelChartBarTitle);
            this.panelChartBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChartBar.Location = new System.Drawing.Point(20, 10);
            this.panelChartBar.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.panelChartBar.Name = "panelChartBar";
            this.panelChartBar.Padding = new System.Windows.Forms.Padding(12, 42, 12, 12);
            this.panelChartBar.Size = new System.Drawing.Size(448, 410);
            this.panelChartBar.TabIndex = 0;
            // 
            // chartTransactions
            // 
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 8F);
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 8F);
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            chartArea1.AxisY.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.BorderWidth = 0;
            chartArea1.Name = "ChartArea1";
            this.chartTransactions.ChartAreas.Add(chartArea1);
            this.chartTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartTransactions.Location = new System.Drawing.Point(12, 42);
            this.chartTransactions.Name = "chartTransactions";
            series1.ChartArea = "ChartArea1";
            series1.CustomProperties = "PointWidth=0.55";
            series1.Font = new System.Drawing.Font("Segoe UI", 8F);
            series1.IsValueShownAsLabel = true;
            series1.LabelForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            series1.Name = "Số giao dịch";
            this.chartTransactions.Series.Add(series1);
            this.chartTransactions.Size = new System.Drawing.Size(422, 354);
            this.chartTransactions.TabIndex = 1;
            // 
            // labelChartBarTitle
            // 
            this.labelChartBarTitle.AutoSize = true;
            this.labelChartBarTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.labelChartBarTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.labelChartBarTitle.Location = new System.Drawing.Point(16, 13);
            this.labelChartBarTitle.Name = "labelChartBarTitle";
            this.labelChartBarTitle.Size = new System.Drawing.Size(152, 20);
            this.labelChartBarTitle.TabIndex = 0;
            this.labelChartBarTitle.Text = "Giao dịch theo quý";
            // 
            // panelChartDonut
            // 
            this.panelChartDonut.BackColor = System.Drawing.Color.White;
            this.panelChartDonut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelChartDonut.Controls.Add(this.chartAccountTypes);
            this.panelChartDonut.Controls.Add(this.labelChartDonutTitle);
            this.panelChartDonut.Controls.Add(this.labelDonutCenter1);
            this.panelChartDonut.Controls.Add(this.labelDonutCenter2);
            this.panelChartDonut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChartDonut.Location = new System.Drawing.Point(480, 10);
            this.panelChartDonut.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.panelChartDonut.Name = "panelChartDonut";
            this.panelChartDonut.Padding = new System.Windows.Forms.Padding(12, 42, 12, 12);
            this.panelChartDonut.Size = new System.Drawing.Size(276, 410);
            this.panelChartDonut.TabIndex = 1;
            // 
            // chartAccountTypes
            // 
            chartArea2.BackColor = System.Drawing.Color.White;
            chartArea2.Name = "ChartArea1";
            this.chartAccountTypes.ChartAreas.Add(chartArea2);
            this.chartAccountTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Font = new System.Drawing.Font("Segoe UI", 8F);
            legend1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.chartAccountTypes.Legends.Add(legend1);
            this.chartAccountTypes.Location = new System.Drawing.Point(12, 42);
            this.chartAccountTypes.Name = "chartAccountTypes";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series2.CustomProperties = "DoughnutRadius=60";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartAccountTypes.Series.Add(series2);
            this.chartAccountTypes.Size = new System.Drawing.Size(250, 354);
            this.chartAccountTypes.TabIndex = 1;
            // 
            // labelChartDonutTitle
            // 
            this.labelChartDonutTitle.AutoSize = true;
            this.labelChartDonutTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.labelChartDonutTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.labelChartDonutTitle.Location = new System.Drawing.Point(16, 13);
            this.labelChartDonutTitle.Name = "labelChartDonutTitle";
            this.labelChartDonutTitle.Size = new System.Drawing.Size(141, 20);
            this.labelChartDonutTitle.TabIndex = 0;
            this.labelChartDonutTitle.Text = "Phân loại tài khoản";
            // 
            // labelDonutCenter1
            // 
            this.labelDonutCenter1.BackColor = System.Drawing.Color.Transparent;
            this.labelDonutCenter1.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.labelDonutCenter1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.labelDonutCenter1.Location = new System.Drawing.Point(86, 148);
            this.labelDonutCenter1.Name = "labelDonutCenter1";
            this.labelDonutCenter1.Size = new System.Drawing.Size(126, 26);
            this.labelDonutCenter1.TabIndex = 2;
            this.labelDonutCenter1.Text = "0";
            this.labelDonutCenter1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDonutCenter2
            // 
            this.labelDonutCenter2.BackColor = System.Drawing.Color.Transparent;
            this.labelDonutCenter2.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.labelDonutCenter2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelDonutCenter2.Location = new System.Drawing.Point(86, 173);
            this.labelDonutCenter2.Name = "labelDonutCenter2";
            this.labelDonutCenter2.Size = new System.Drawing.Size(126, 18);
            this.labelDonutCenter2.TabIndex = 3;
            this.labelDonutCenter2.Text = "Tổng";
            this.labelDonutCenter2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTopAccounts
            // 
            this.panelTopAccounts.BackColor = System.Drawing.Color.White;
            this.panelTopAccounts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTopAccounts.Controls.Add(this.dgvTopAccounts);
            this.panelTopAccounts.Controls.Add(this.labelTopTitle);
            this.panelTopAccounts.Controls.Add(this.linkViewAll);
            this.panelTopAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTopAccounts.Location = new System.Drawing.Point(768, 10);
            this.panelTopAccounts.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.panelTopAccounts.Name = "panelTopAccounts";
            this.panelTopAccounts.Padding = new System.Windows.Forms.Padding(12, 42, 12, 12);
            this.panelTopAccounts.Size = new System.Drawing.Size(392, 410);
            this.panelTopAccounts.TabIndex = 2;
            // 
            // dgvTopAccounts
            // 
            this.dgvTopAccounts.AllowUserToAddRows = false;
            this.dgvTopAccounts.AllowUserToDeleteRows = false;
            this.dgvTopAccounts.AllowUserToResizeColumns = false;
            this.dgvTopAccounts.AllowUserToResizeRows = false;
            this.dgvTopAccounts.BackgroundColor = System.Drawing.Color.White;
            this.dgvTopAccounts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTopAccounts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvTopAccounts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTopAccounts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTopAccounts.ColumnHeadersHeight = 34;
            this.dgvTopAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTopAccounts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSTT,
            this.colSoTaiKhoan,
            this.colChuTaiKhoan,
            this.colSoDu});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTopAccounts.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTopAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTopAccounts.EnableHeadersVisualStyles = false;
            this.dgvTopAccounts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvTopAccounts.Location = new System.Drawing.Point(12, 42);
            this.dgvTopAccounts.MultiSelect = false;
            this.dgvTopAccounts.Name = "dgvTopAccounts";
            this.dgvTopAccounts.ReadOnly = true;
            this.dgvTopAccounts.RowHeadersVisible = false;
            this.dgvTopAccounts.RowTemplate.Height = 38;
            this.dgvTopAccounts.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvTopAccounts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopAccounts.Size = new System.Drawing.Size(366, 354);
            this.dgvTopAccounts.TabIndex = 1;
            // 
            // colSTT
            // 
            this.colSTT.HeaderText = "STT";
            this.colSTT.Name = "colSTT";
            this.colSTT.ReadOnly = true;
            this.colSTT.Width = 60;
            // 
            // colSoTaiKhoan
            // 
            this.colSoTaiKhoan.HeaderText = "Số tài khoản";
            this.colSoTaiKhoan.Name = "colSoTaiKhoan";
            this.colSoTaiKhoan.ReadOnly = true;
            this.colSoTaiKhoan.Width = 102;
            // 
            // colChuTaiKhoan
            // 
            this.colChuTaiKhoan.HeaderText = "Chủ tài khoản";
            this.colChuTaiKhoan.Name = "colChuTaiKhoan";
            this.colChuTaiKhoan.ReadOnly = true;
            this.colChuTaiKhoan.Width = 105;
            // 
            // colSoDu
            // 
            this.colSoDu.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colSoDu.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSoDu.HeaderText = "Số dư";
            this.colSoDu.Name = "colSoDu";
            this.colSoDu.ReadOnly = true;
            // 
            // labelTopTitle
            // 
            this.labelTopTitle.AutoSize = true;
            this.labelTopTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.labelTopTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.labelTopTitle.Location = new System.Drawing.Point(14, 13);
            this.labelTopTitle.Name = "labelTopTitle";
            this.labelTopTitle.Size = new System.Drawing.Size(178, 20);
            this.labelTopTitle.TabIndex = 0;
            this.labelTopTitle.Text = "Top tài khoản theo số dư";
            // 
            // linkViewAll
            // 
            this.linkViewAll.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.linkViewAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkViewAll.AutoSize = true;
            this.linkViewAll.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F);
            this.linkViewAll.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.linkViewAll.Location = new System.Drawing.Point(14, 628);
            this.linkViewAll.Name = "linkViewAll";
            this.linkViewAll.Size = new System.Drawing.Size(76, 15);
            this.linkViewAll.TabIndex = 2;
            this.linkViewAll.TabStop = true;
            this.linkViewAll.Text = "Xem tất cả →";
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFooter.Controls.Add(this.labelInfoIcon);
            this.panelFooter.Controls.Add(this.labelUpdatedAt);
            this.panelFooter.Controls.Add(this.buttonRefresh);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 664);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1180, 56);
            this.panelFooter.TabIndex = 4;
            // 
            // labelInfoIcon
            // 
            this.labelInfoIcon.AutoSize = true;
            this.labelInfoIcon.Font = new System.Drawing.Font("Segoe UI Symbol", 12F);
            this.labelInfoIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.labelInfoIcon.Location = new System.Drawing.Point(20, 17);
            this.labelInfoIcon.Name = "labelInfoIcon";
            this.labelInfoIcon.Size = new System.Drawing.Size(24, 21);
            this.labelInfoIcon.TabIndex = 0;
            this.labelInfoIcon.Text = "ⓘ";
            // 
            // labelUpdatedAt
            // 
            this.labelUpdatedAt.AutoSize = true;
            this.labelUpdatedAt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelUpdatedAt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.labelUpdatedAt.Location = new System.Drawing.Point(47, 19);
            this.labelUpdatedAt.Name = "labelUpdatedAt";
            this.labelUpdatedAt.Size = new System.Drawing.Size(237, 15);
            this.labelUpdatedAt.TabIndex = 1;
            this.labelUpdatedAt.Text = "Dữ liệu được cập nhật đến 31/05/2025 10:30";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.buttonRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.buttonRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.buttonRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.buttonRefresh.Location = new System.Drawing.Point(1054, 12);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(108, 30);
            this.buttonRefresh.TabIndex = 2;
            this.buttonRefresh.Text = "↻  Làm mới";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1180, 720);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelCharts);
            this.Controls.Add(this.panelKpi);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(700, 480);
            this.Name = "StatisticsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê";
            this.Load += new System.EventHandler(this.StatisticsForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHeaderIcon)).EndInit();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelKpi.ResumeLayout(false);
            this.tableLayoutKpi.ResumeLayout(false);
            this.panelKpi1.ResumeLayout(false);
            this.panelKpi1.PerformLayout();
            this.panelKpi1Icon.ResumeLayout(false);
            this.panelKpi2.ResumeLayout(false);
            this.panelKpi2.PerformLayout();
            this.panelKpi2Icon.ResumeLayout(false);
            this.panelKpi3.ResumeLayout(false);
            this.panelKpi3.PerformLayout();
            this.panelKpi3Icon.ResumeLayout(false);
            this.panelKpi4.ResumeLayout(false);
            this.panelKpi4.PerformLayout();
            this.panelKpi4Icon.ResumeLayout(false);
            this.panelCharts.ResumeLayout(false);
            this.tableLayoutCharts.ResumeLayout(false);
            this.panelChartBar.ResumeLayout(false);
            this.panelChartBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTransactions)).EndInit();
            this.panelChartDonut.ResumeLayout(false);
            this.panelChartDonut.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartAccountTypes)).EndInit();
            this.panelTopAccounts.ResumeLayout(false);
            this.panelTopAccounts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopAccounts)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        // ============ FIELDS ============
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.PictureBox pictureBoxHeaderIcon;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelSubtitle;

        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.Button buttonViewStats;

        private System.Windows.Forms.Panel panelKpi;
        private System.Windows.Forms.TableLayoutPanel tableLayoutKpi;

        private System.Windows.Forms.Panel panelKpi1;
        private System.Windows.Forms.Panel panelKpi1Icon;
        private System.Windows.Forms.Label labelKpi1Icon;
        private System.Windows.Forms.Label labelKpi1Title;
        private System.Windows.Forms.Label lblTotalAccounts;
        private System.Windows.Forms.Label labelKpi1Sub;

        private System.Windows.Forms.Panel panelKpi2;
        private System.Windows.Forms.Panel panelKpi2Icon;
        private System.Windows.Forms.Label labelKpi2Icon;
        private System.Windows.Forms.Label labelKpi2Title;
        private System.Windows.Forms.Label lblTotalBalance;
        private System.Windows.Forms.Label labelKpi2Sub;

        private System.Windows.Forms.Panel panelKpi3;
        private System.Windows.Forms.Panel panelKpi3Icon;
        private System.Windows.Forms.Label labelKpi3Icon;
        private System.Windows.Forms.Label labelKpi3Title;
        private System.Windows.Forms.Label lblTotalTransactions;
        private System.Windows.Forms.Label labelKpi3Sub;

        private System.Windows.Forms.Panel panelKpi4;
        private System.Windows.Forms.Panel panelKpi4Icon;
        private System.Windows.Forms.Label labelKpi4Icon;
        private System.Windows.Forms.Label labelKpi4Title;
        private System.Windows.Forms.Label lblTotalCustomers;
        private System.Windows.Forms.Label labelKpi4Sub;

        private System.Windows.Forms.Panel panelCharts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutCharts;

        private System.Windows.Forms.Panel panelChartBar;
        private System.Windows.Forms.Label labelChartBarTitle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTransactions;

        private System.Windows.Forms.Panel panelChartDonut;
        private System.Windows.Forms.Label labelChartDonutTitle;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAccountTypes;
        private System.Windows.Forms.Label labelDonutCenter1;
        private System.Windows.Forms.Label labelDonutCenter2;

        private System.Windows.Forms.Panel panelTopAccounts;
        private System.Windows.Forms.Label labelTopTitle;
        private System.Windows.Forms.DataGridView dgvTopAccounts;
        private System.Windows.Forms.LinkLabel linkViewAll;

        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Label labelInfoIcon;
        private System.Windows.Forms.Label labelUpdatedAt;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoTaiKhoan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChuTaiKhoan;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoDu;
    }
}
