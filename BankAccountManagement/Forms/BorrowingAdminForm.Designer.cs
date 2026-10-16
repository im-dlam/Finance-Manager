namespace BankAccountManagement.Forms
{
    partial class BorrowingAdminForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnRefresh = new BankAccountManagement.Forms.RoundedButton();
            this.lblStatBadge = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.pnlSearchBox = new BankAccountManagement.Forms.RoundedPanel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.cboFilterStatus = new System.Windows.Forms.ComboBox();
            this.lblFilterStatus = new System.Windows.Forms.Label();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeftCard = new BankAccountManagement.Forms.RoundedPanel();
            this.dgvLoans = new System.Windows.Forms.DataGridView();
            this.lblGridSub = new System.Windows.Forms.Label();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.pnlRightCard = new BankAccountManagement.Forms.RoundedPanel();
            this.pnlAppraisalScroll = new System.Windows.Forms.Panel();
            this.pnlAppraisalContent = new System.Windows.Forms.Panel();
            this.pnlActionsRow = new System.Windows.Forms.Panel();
            this.tlpActionButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnApprove = new BankAccountManagement.Forms.RoundedButton();
            this.btnReject = new BankAccountManagement.Forms.RoundedButton();
            this.pnlEvaluation = new BankAccountManagement.Forms.RoundedPanel();
            this.lblEvalNote = new System.Windows.Forms.Label();
            this.lblEvalBadge = new System.Windows.Forms.Label();
            this.lblEvalTitle = new System.Windows.Forms.Label();
            this.pnlLoanDetail = new BankAccountManagement.Forms.RoundedPanel();
            this.lblStatusBadge = new System.Windows.Forms.Label();
            this.lblTotalDebtVal = new System.Windows.Forms.Label();
            this.lblTotalDebtLabel = new System.Windows.Forms.Label();
            this.lblTermInterestVal = new System.Windows.Forms.Label();
            this.lblTermInterestLabel = new System.Windows.Forms.Label();
            this.lblLoanAmountVal = new System.Windows.Forms.Label();
            this.lblLoanAmountLabel = new System.Windows.Forms.Label();
            this.lblLoanIdVal = new System.Windows.Forms.Label();
            this.lblLoanIdLabel = new System.Windows.Forms.Label();
            this.lblLoanHeader = new System.Windows.Forms.Label();
            this.pnlQuarterStats = new BankAccountManagement.Forms.RoundedPanel();
            this.lblSoGDVal = new System.Windows.Forms.Label();
            this.lblSoGDLabel = new System.Windows.Forms.Label();
            this.lblChenhLechVal = new System.Windows.Forms.Label();
            this.lblChenhLechLabel = new System.Windows.Forms.Label();
            this.lblChiQuyVal = new System.Windows.Forms.Label();
            this.lblChiQuyLabel = new System.Windows.Forms.Label();
            this.lblThuQuyVal = new System.Windows.Forms.Label();
            this.lblThuQuyLabel = new System.Windows.Forms.Label();
            this.lblQuarterHeader = new System.Windows.Forms.Label();
            this.pnlCustInfo = new BankAccountManagement.Forms.RoundedPanel();
            this.pnlBalanceCard = new BankAccountManagement.Forms.RoundedPanel();
            this.lblBalanceVal = new System.Windows.Forms.Label();
            this.lblBalanceLabel = new System.Windows.Forms.Label();
            this.lblUserVal = new System.Windows.Forms.Label();
            this.lblUserLabel = new System.Windows.Forms.Label();
            this.lblMaKHVal = new System.Windows.Forms.Label();
            this.lblMaKHLabel = new System.Windows.Forms.Label();
            this.lblAccVal = new System.Windows.Forms.Label();
            this.lblAccLabel = new System.Windows.Forms.Label();
            this.lblCustHeader = new System.Windows.Forms.Label();
            this.lblAppraisalSub = new System.Windows.Forms.Label();
            this.lblAppraisalTitle = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlSearchBox.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.pnlLeftCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoans)).BeginInit();
            this.pnlRightCard.SuspendLayout();
            this.pnlAppraisalScroll.SuspendLayout();
            this.pnlAppraisalContent.SuspendLayout();
            this.pnlActionsRow.SuspendLayout();
            this.tlpActionButtons.SuspendLayout();
            this.pnlEvaluation.SuspendLayout();
            this.pnlLoanDetail.SuspendLayout();
            this.pnlQuarterStats.SuspendLayout();
            this.pnlCustInfo.SuspendLayout();
            this.pnlBalanceCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.btnRefresh);
            this.pnlHeader.Controls.Add(this.lblStatBadge);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 75);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.HoverBorder = System.Drawing.Color.Transparent;
            this.btnRefresh.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnRefresh.Location = new System.Drawing.Point(1065, 18);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.NormalBorder = System.Drawing.Color.Transparent;
            this.btnRefresh.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnRefresh.PressedBorder = System.Drawing.Color.Transparent;
            this.btnRefresh.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.btnRefresh.Size = new System.Drawing.Size(110, 38);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "🔄 Làm mới";
            this.btnRefresh.TextColor = System.Drawing.Color.White;
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // lblStatBadge
            // 
            this.lblStatBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.lblStatBadge.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStatBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblStatBadge.Location = new System.Drawing.Point(740, 18);
            this.lblStatBadge.Name = "lblStatBadge";
            this.lblStatBadge.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.lblStatBadge.Size = new System.Drawing.Size(310, 38);
            this.lblStatBadge.TabIndex = 2;
            this.lblStatBadge.Text = "Hồ sơ: 0 | Chờ duyệt: 0 | Đã duyệt: 0";
            this.lblStatBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(24, 45);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(731, 25);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Thẩm định năng lực tài chính, tổng thu chi 1 quý (90 ngày) và phê duyệt giải ngân" +
    " trực tiếp";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(22, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(701, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Hệ Thống Thẩm Định & Xét Duyệt Khoản Vay";
            this.lblTitle.UseMnemonic = false;
            // 
            // pnlFilter
            // 
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlFilter.Controls.Add(this.pnlSearchBox);
            this.pnlFilter.Controls.Add(this.lblSearch);
            this.pnlFilter.Controls.Add(this.cboFilterStatus);
            this.pnlFilter.Controls.Add(this.lblFilterStatus);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 75);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1200, 50);
            this.pnlFilter.TabIndex = 1;
            // 
            // pnlSearchBox
            // 
            this.pnlSearchBox.BackColor = System.Drawing.Color.White;
            this.pnlSearchBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.pnlSearchBox.BorderRadius = 6;
            this.pnlSearchBox.BorderSize = 1;
            this.pnlSearchBox.Controls.Add(this.txtSearch);
            this.pnlSearchBox.FillColor = System.Drawing.Color.White;
            this.pnlSearchBox.Location = new System.Drawing.Point(335, 10);
            this.pnlSearchBox.Name = "pnlSearchBox";
            this.pnlSearchBox.Size = new System.Drawing.Size(360, 30);
            this.pnlSearchBox.TabIndex = 3;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.txtSearch.Location = new System.Drawing.Point(8, 6);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(342, 26);
            this.txtSearch.TabIndex = 0;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSearch.Location = new System.Drawing.Point(270, 17);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(95, 25);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "Tìm kiếm:";
            // 
            // cboFilterStatus
            // 
            this.cboFilterStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilterStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboFilterStatus.FormattingEnabled = true;
            this.cboFilterStatus.Items.AddRange(new object[] {
            "Tất cả",
            "Chờ xét duyệt",
            "Đã duyệt",
            "Đã từ chối"});
            this.cboFilterStatus.Location = new System.Drawing.Point(95, 12);
            this.cboFilterStatus.Name = "cboFilterStatus";
            this.cboFilterStatus.Size = new System.Drawing.Size(150, 33);
            this.cboFilterStatus.TabIndex = 1;
            // 
            // lblFilterStatus
            // 
            this.lblFilterStatus.AutoSize = true;
            this.lblFilterStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFilterStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblFilterStatus.Location = new System.Drawing.Point(24, 17);
            this.lblFilterStatus.Name = "lblFilterStatus";
            this.lblFilterStatus.Size = new System.Drawing.Size(104, 25);
            this.lblFilterStatus.TabIndex = 0;
            this.lblFilterStatus.Text = "Trạng thái:";
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49F));
            this.tlpMain.Controls.Add(this.pnlLeftCard, 0, 0);
            this.tlpMain.Controls.Add(this.pnlRightCard, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 125);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.Padding = new System.Windows.Forms.Padding(16, 12, 16, 16);
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1200, 625);
            this.tlpMain.TabIndex = 2;
            // 
            // pnlLeftCard
            // 
            this.pnlLeftCard.BackColor = System.Drawing.Color.White;
            this.pnlLeftCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlLeftCard.BorderRadius = 12;
            this.pnlLeftCard.BorderSize = 1;
            this.pnlLeftCard.Controls.Add(this.dgvLoans);
            this.pnlLeftCard.Controls.Add(this.lblGridSub);
            this.pnlLeftCard.Controls.Add(this.lblGridTitle);
            this.pnlLeftCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeftCard.FillColor = System.Drawing.Color.White;
            this.pnlLeftCard.Location = new System.Drawing.Point(19, 15);
            this.pnlLeftCard.Name = "pnlLeftCard";
            this.pnlLeftCard.Padding = new System.Windows.Forms.Padding(14);
            this.pnlLeftCard.Size = new System.Drawing.Size(589, 591);
            this.pnlLeftCard.TabIndex = 0;
            // 
            // dgvLoans
            // 
            this.dgvLoans.AllowUserToAddRows = false;
            this.dgvLoans.AllowUserToDeleteRows = false;
            this.dgvLoans.AllowUserToResizeRows = false;
            this.dgvLoans.BackgroundColor = System.Drawing.Color.White;
            this.dgvLoans.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLoans.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLoans.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLoans.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLoans.ColumnHeadersHeight = 36;
            this.dgvLoans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(4);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLoans.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLoans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLoans.EnableHeadersVisualStyles = false;
            this.dgvLoans.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvLoans.Location = new System.Drawing.Point(14, 83);
            this.dgvLoans.MultiSelect = false;
            this.dgvLoans.Name = "dgvLoans";
            this.dgvLoans.ReadOnly = true;
            this.dgvLoans.RowHeadersVisible = false;
            this.dgvLoans.RowHeadersWidth = 62;
            this.dgvLoans.RowTemplate.Height = 36;
            this.dgvLoans.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvLoans.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLoans.Size = new System.Drawing.Size(561, 494);
            this.dgvLoans.TabIndex = 2;
            // 
            // lblGridSub
            // 
            this.lblGridSub.AutoSize = true;
            this.lblGridSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGridSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblGridSub.Location = new System.Drawing.Point(14, 46);
            this.lblGridSub.Name = "lblGridSub";
            this.lblGridSub.Padding = new System.Windows.Forms.Padding(0, 4, 0, 8);
            this.lblGridSub.Size = new System.Drawing.Size(672, 37);
            this.lblGridSub.TabIndex = 1;
            this.lblGridSub.Text = "Nhấp chọn một hồ sơ để xem chi tiết đối soát tài khoản và thẩm định thu chi 1 quý" +
    "";
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.AutoSize = true;
            this.lblGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblGridTitle.Location = new System.Drawing.Point(14, 14);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(406, 32);
            this.lblGridTitle.TabIndex = 0;
            this.lblGridTitle.Text = "DANH SÁCH HỒ SƠ ĐĂNG KÝ VAY";
            this.lblGridTitle.UseMnemonic = false;
            // 
            // pnlRightCard
            // 
            this.pnlRightCard.BackColor = System.Drawing.Color.White;
            this.pnlRightCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlRightCard.BorderRadius = 12;
            this.pnlRightCard.BorderSize = 1;
            this.pnlRightCard.Controls.Add(this.pnlAppraisalScroll);
            this.pnlRightCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRightCard.FillColor = System.Drawing.Color.White;
            this.pnlRightCard.Location = new System.Drawing.Point(614, 15);
            this.pnlRightCard.Name = "pnlRightCard";
            this.pnlRightCard.Padding = new System.Windows.Forms.Padding(12);
            this.pnlRightCard.Size = new System.Drawing.Size(567, 591);
            this.pnlRightCard.TabIndex = 1;
            // 
            // pnlAppraisalScroll
            // 
            this.pnlAppraisalScroll.AutoScroll = true;
            this.pnlAppraisalScroll.Controls.Add(this.pnlAppraisalContent);
            this.pnlAppraisalScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAppraisalScroll.Location = new System.Drawing.Point(12, 12);
            this.pnlAppraisalScroll.Name = "pnlAppraisalScroll";
            this.pnlAppraisalScroll.Size = new System.Drawing.Size(543, 567);
            this.pnlAppraisalScroll.TabIndex = 0;
            // 
            // pnlAppraisalContent
            // 
            this.pnlAppraisalContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAppraisalContent.Controls.Add(this.pnlActionsRow);
            this.pnlAppraisalContent.Controls.Add(this.pnlEvaluation);
            this.pnlAppraisalContent.Controls.Add(this.pnlLoanDetail);
            this.pnlAppraisalContent.Controls.Add(this.pnlQuarterStats);
            this.pnlAppraisalContent.Controls.Add(this.pnlCustInfo);
            this.pnlAppraisalContent.Controls.Add(this.lblAppraisalSub);
            this.pnlAppraisalContent.Controls.Add(this.lblAppraisalTitle);
            this.pnlAppraisalContent.Location = new System.Drawing.Point(2, 2);
            this.pnlAppraisalContent.Name = "pnlAppraisalContent";
            this.pnlAppraisalContent.Size = new System.Drawing.Size(521, 560);
            this.pnlAppraisalContent.TabIndex = 0;
            // 
            // pnlActionsRow
            // 
            this.pnlActionsRow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlActionsRow.Controls.Add(this.tlpActionButtons);
            this.pnlActionsRow.Location = new System.Drawing.Point(2, 492);
            this.pnlActionsRow.Name = "pnlActionsRow";
            this.pnlActionsRow.Size = new System.Drawing.Size(517, 52);
            this.pnlActionsRow.TabIndex = 6;
            // 
            // tlpActionButtons
            // 
            this.tlpActionButtons.ColumnCount = 2;
            this.tlpActionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpActionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpActionButtons.Controls.Add(this.btnApprove, 0, 0);
            this.tlpActionButtons.Controls.Add(this.btnReject, 1, 0);
            this.tlpActionButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpActionButtons.Location = new System.Drawing.Point(0, 0);
            this.tlpActionButtons.Name = "tlpActionButtons";
            this.tlpActionButtons.RowCount = 1;
            this.tlpActionButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpActionButtons.Size = new System.Drawing.Size(517, 52);
            this.tlpActionButtons.TabIndex = 0;
            // 
            // btnApprove
            // 
            this.btnApprove.BackColor = System.Drawing.Color.Transparent;
            this.btnApprove.BorderRadius = 10;
            this.btnApprove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApprove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnApprove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApprove.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnApprove.ForeColor = System.Drawing.Color.White;
            this.btnApprove.HoverBorder = System.Drawing.Color.Transparent;
            this.btnApprove.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnApprove.Location = new System.Drawing.Point(0, 4);
            this.btnApprove.Margin = new System.Windows.Forms.Padding(0, 4, 6, 4);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.NormalBorder = System.Drawing.Color.Transparent;
            this.btnApprove.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnApprove.PressedBorder = System.Drawing.Color.Transparent;
            this.btnApprove.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(83)))), ((int)(((byte)(45)))));
            this.btnApprove.Size = new System.Drawing.Size(252, 44);
            this.btnApprove.TabIndex = 0;
            this.btnApprove.Text = "✔ Xác nhận";
            this.btnApprove.TextColor = System.Drawing.Color.White;
            this.btnApprove.UseVisualStyleBackColor = false;
            // 
            // btnReject
            // 
            this.btnReject.BackColor = System.Drawing.Color.Transparent;
            this.btnReject.BorderRadius = 10;
            this.btnReject.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReject.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnReject.ForeColor = System.Drawing.Color.White;
            this.btnReject.HoverBorder = System.Drawing.Color.Transparent;
            this.btnReject.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.btnReject.Location = new System.Drawing.Point(264, 4);
            this.btnReject.Margin = new System.Windows.Forms.Padding(6, 4, 0, 4);
            this.btnReject.Name = "btnReject";
            this.btnReject.NormalBorder = System.Drawing.Color.Transparent;
            this.btnReject.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnReject.PressedBorder = System.Drawing.Color.Transparent;
            this.btnReject.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnReject.Size = new System.Drawing.Size(253, 44);
            this.btnReject.TabIndex = 1;
            this.btnReject.Text = "✖ Không xác nhận";
            this.btnReject.TextColor = System.Drawing.Color.White;
            this.btnReject.UseVisualStyleBackColor = false;
            // 
            // pnlEvaluation
            // 
            this.pnlEvaluation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlEvaluation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.pnlEvaluation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(247)))), ((int)(((byte)(208)))));
            this.pnlEvaluation.BorderRadius = 8;
            this.pnlEvaluation.BorderSize = 1;
            this.pnlEvaluation.Controls.Add(this.lblEvalNote);
            this.pnlEvaluation.Controls.Add(this.lblEvalBadge);
            this.pnlEvaluation.Controls.Add(this.lblEvalTitle);
            this.pnlEvaluation.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.pnlEvaluation.Location = new System.Drawing.Point(2, 410);
            this.pnlEvaluation.Name = "pnlEvaluation";
            this.pnlEvaluation.Size = new System.Drawing.Size(517, 72);
            this.pnlEvaluation.TabIndex = 5;
            // 
            // lblEvalNote
            // 
            this.lblEvalNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEvalNote.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblEvalNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblEvalNote.Location = new System.Drawing.Point(8, 28);
            this.lblEvalNote.Name = "lblEvalNote";
            this.lblEvalNote.Size = new System.Drawing.Size(499, 38);
            this.lblEvalNote.TabIndex = 2;
            this.lblEvalNote.Text = "Vui lòng chọn hồ sơ vay trong danh sách bên trái để tiến hành đối soát số liệu.";
            // 
            // lblEvalBadge
            // 
            this.lblEvalBadge.AutoSize = true;
            this.lblEvalBadge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEvalBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.lblEvalBadge.Location = new System.Drawing.Point(195, 7);
            this.lblEvalBadge.Name = "lblEvalBadge";
            this.lblEvalBadge.Size = new System.Drawing.Size(288, 25);
            this.lblEvalBadge.TabIndex = 1;
            this.lblEvalBadge.Text = "Chưa có dữ liệu thẩm định hồ sơ";
            // 
            // lblEvalTitle
            // 
            this.lblEvalTitle.AutoSize = true;
            this.lblEvalTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEvalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.lblEvalTitle.Location = new System.Drawing.Point(8, 7);
            this.lblEvalTitle.Name = "lblEvalTitle";
            this.lblEvalTitle.Size = new System.Drawing.Size(304, 25);
            this.lblEvalTitle.TabIndex = 0;
            this.lblEvalTitle.Text = "ĐÁNH GIÁ NĂNG LỰC TÀI CHÍNH:";
            this.lblEvalTitle.UseMnemonic = false;
            // 
            // pnlLoanDetail
            // 
            this.pnlLoanDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLoanDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlLoanDetail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlLoanDetail.BorderRadius = 8;
            this.pnlLoanDetail.BorderSize = 1;
            this.pnlLoanDetail.Controls.Add(this.lblStatusBadge);
            this.pnlLoanDetail.Controls.Add(this.lblTotalDebtVal);
            this.pnlLoanDetail.Controls.Add(this.lblTotalDebtLabel);
            this.pnlLoanDetail.Controls.Add(this.lblTermInterestVal);
            this.pnlLoanDetail.Controls.Add(this.lblTermInterestLabel);
            this.pnlLoanDetail.Controls.Add(this.lblLoanAmountVal);
            this.pnlLoanDetail.Controls.Add(this.lblLoanAmountLabel);
            this.pnlLoanDetail.Controls.Add(this.lblLoanIdVal);
            this.pnlLoanDetail.Controls.Add(this.lblLoanIdLabel);
            this.pnlLoanDetail.Controls.Add(this.lblLoanHeader);
            this.pnlLoanDetail.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlLoanDetail.Location = new System.Drawing.Point(2, 290);
            this.pnlLoanDetail.Name = "pnlLoanDetail";
            this.pnlLoanDetail.Size = new System.Drawing.Size(517, 110);
            this.pnlLoanDetail.TabIndex = 4;
            // 
            // lblStatusBadge
            // 
            this.lblStatusBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(243)))), ((int)(((byte)(199)))));
            this.lblStatusBadge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatusBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(9)))));
            this.lblStatusBadge.Location = new System.Drawing.Point(378, 8);
            this.lblStatusBadge.Name = "lblStatusBadge";
            this.lblStatusBadge.Size = new System.Drawing.Size(128, 28);
            this.lblStatusBadge.TabIndex = 9;
            this.lblStatusBadge.Text = "Chờ xét duyệt";
            this.lblStatusBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalDebtVal
            // 
            this.lblTotalDebtVal.AutoSize = true;
            this.lblTotalDebtVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalDebtVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblTotalDebtVal.Location = new System.Drawing.Point(105, 86);
            this.lblTotalDebtVal.Name = "lblTotalDebtVal";
            this.lblTotalDebtVal.Size = new System.Drawing.Size(43, 28);
            this.lblTotalDebtVal.TabIndex = 8;
            this.lblTotalDebtVal.Text = "0 đ";
            // 
            // lblTotalDebtLabel
            // 
            this.lblTotalDebtLabel.AutoSize = true;
            this.lblTotalDebtLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotalDebtLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTotalDebtLabel.Location = new System.Drawing.Point(8, 88);
            this.lblTotalDebtLabel.Name = "lblTotalDebtLabel";
            this.lblTotalDebtLabel.Size = new System.Drawing.Size(148, 25);
            this.lblTotalDebtLabel.TabIndex = 7;
            this.lblTotalDebtLabel.Text = "Tổng nợ phải trả:";
            // 
            // lblTermInterestVal
            // 
            this.lblTermInterestVal.AutoSize = true;
            this.lblTermInterestVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTermInterestVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTermInterestVal.Location = new System.Drawing.Point(105, 68);
            this.lblTermInterestVal.Name = "lblTermInterestVal";
            this.lblTermInterestVal.Size = new System.Drawing.Size(33, 25);
            this.lblTermInterestVal.TabIndex = 6;
            this.lblTermInterestVal.Text = "---";
            // 
            // lblTermInterestLabel
            // 
            this.lblTermInterestLabel.AutoSize = true;
            this.lblTermInterestLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTermInterestLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTermInterestLabel.Location = new System.Drawing.Point(8, 68);
            this.lblTermInterestLabel.Name = "lblTermInterestLabel";
            this.lblTermInterestLabel.Size = new System.Drawing.Size(146, 25);
            this.lblTermInterestLabel.TabIndex = 5;
            this.lblTermInterestLabel.Text = "Kỳ hạn & Tiền lãi:";
            this.lblTermInterestLabel.UseMnemonic = false;
            // 
            // lblLoanAmountVal
            // 
            this.lblLoanAmountVal.AutoSize = true;
            this.lblLoanAmountVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLoanAmountVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblLoanAmountVal.Location = new System.Drawing.Point(105, 46);
            this.lblLoanAmountVal.Name = "lblLoanAmountVal";
            this.lblLoanAmountVal.Size = new System.Drawing.Size(43, 28);
            this.lblLoanAmountVal.TabIndex = 4;
            this.lblLoanAmountVal.Text = "0 đ";
            // 
            // lblLoanAmountLabel
            // 
            this.lblLoanAmountLabel.AutoSize = true;
            this.lblLoanAmountLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLoanAmountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblLoanAmountLabel.Location = new System.Drawing.Point(8, 48);
            this.lblLoanAmountLabel.Name = "lblLoanAmountLabel";
            this.lblLoanAmountLabel.Size = new System.Drawing.Size(103, 25);
            this.lblLoanAmountLabel.TabIndex = 3;
            this.lblLoanAmountLabel.Text = "Số tiền vay:";
            // 
            // lblLoanIdVal
            // 
            this.lblLoanIdVal.AutoSize = true;
            this.lblLoanIdVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLoanIdVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblLoanIdVal.Location = new System.Drawing.Point(105, 28);
            this.lblLoanIdVal.Name = "lblLoanIdVal";
            this.lblLoanIdVal.Size = new System.Drawing.Size(33, 25);
            this.lblLoanIdVal.TabIndex = 2;
            this.lblLoanIdVal.Text = "---";
            // 
            // lblLoanIdLabel
            // 
            this.lblLoanIdLabel.AutoSize = true;
            this.lblLoanIdLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLoanIdLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblLoanIdLabel.Location = new System.Drawing.Point(8, 28);
            this.lblLoanIdLabel.Name = "lblLoanIdLabel";
            this.lblLoanIdLabel.Size = new System.Drawing.Size(127, 25);
            this.lblLoanIdLabel.TabIndex = 1;
            this.lblLoanIdLabel.Text = "Mã khoản vay:";
            // 
            // lblLoanHeader
            // 
            this.lblLoanHeader.AutoSize = true;
            this.lblLoanHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLoanHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblLoanHeader.Location = new System.Drawing.Point(8, 7);
            this.lblLoanHeader.Name = "lblLoanHeader";
            this.lblLoanHeader.Size = new System.Drawing.Size(278, 25);
            this.lblLoanHeader.TabIndex = 0;
            this.lblLoanHeader.Text = "CHI TIẾT KHOẢN VAY YÊU CẦU";
            this.lblLoanHeader.UseMnemonic = false;
            // 
            // pnlQuarterStats
            // 
            this.pnlQuarterStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlQuarterStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.pnlQuarterStats.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(230)))), ((int)(((byte)(253)))));
            this.pnlQuarterStats.BorderRadius = 8;
            this.pnlQuarterStats.BorderSize = 1;
            this.pnlQuarterStats.Controls.Add(this.lblSoGDVal);
            this.pnlQuarterStats.Controls.Add(this.lblSoGDLabel);
            this.pnlQuarterStats.Controls.Add(this.lblChenhLechVal);
            this.pnlQuarterStats.Controls.Add(this.lblChenhLechLabel);
            this.pnlQuarterStats.Controls.Add(this.lblChiQuyVal);
            this.pnlQuarterStats.Controls.Add(this.lblChiQuyLabel);
            this.pnlQuarterStats.Controls.Add(this.lblThuQuyVal);
            this.pnlQuarterStats.Controls.Add(this.lblThuQuyLabel);
            this.pnlQuarterStats.Controls.Add(this.lblQuarterHeader);
            this.pnlQuarterStats.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.pnlQuarterStats.Location = new System.Drawing.Point(2, 160);
            this.pnlQuarterStats.Name = "pnlQuarterStats";
            this.pnlQuarterStats.Size = new System.Drawing.Size(517, 122);
            this.pnlQuarterStats.TabIndex = 3;
            // 
            // lblSoGDVal
            // 
            this.lblSoGDVal.AutoSize = true;
            this.lblSoGDVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoGDVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblSoGDVal.Location = new System.Drawing.Point(160, 100);
            this.lblSoGDVal.Name = "lblSoGDVal";
            this.lblSoGDVal.Size = new System.Drawing.Size(207, 25);
            this.lblSoGDVal.TabIndex = 8;
            this.lblSoGDVal.Text = "0 giao dịch thành công";
            // 
            // lblSoGDLabel
            // 
            this.lblSoGDLabel.AutoSize = true;
            this.lblSoGDLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoGDLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSoGDLabel.Location = new System.Drawing.Point(8, 100);
            this.lblSoGDLabel.Name = "lblSoGDLabel";
            this.lblSoGDLabel.Size = new System.Drawing.Size(174, 25);
            this.lblSoGDLabel.TabIndex = 7;
            this.lblSoGDLabel.Text = "Giao dịch trong quý:";
            // 
            // lblChenhLechVal
            // 
            this.lblChenhLechVal.AutoSize = true;
            this.lblChenhLechVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblChenhLechVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.lblChenhLechVal.Location = new System.Drawing.Point(160, 75);
            this.lblChenhLechVal.Name = "lblChenhLechVal";
            this.lblChenhLechVal.Size = new System.Drawing.Size(43, 28);
            this.lblChenhLechVal.TabIndex = 6;
            this.lblChenhLechVal.Text = "0 đ";
            // 
            // lblChenhLechLabel
            // 
            this.lblChenhLechLabel.AutoSize = true;
            this.lblChenhLechLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblChenhLechLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblChenhLechLabel.Location = new System.Drawing.Point(8, 78);
            this.lblChenhLechLabel.Name = "lblChenhLechLabel";
            this.lblChenhLechLabel.Size = new System.Drawing.Size(214, 25);
            this.lblChenhLechLabel.TabIndex = 5;
            this.lblChenhLechLabel.Text = "Dòng tiền ròng (Thu-Chi):";
            this.lblChenhLechLabel.UseMnemonic = false;
            // 
            // lblChiQuyVal
            // 
            this.lblChiQuyVal.AutoSize = true;
            this.lblChiQuyVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblChiQuyVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblChiQuyVal.Location = new System.Drawing.Point(160, 51);
            this.lblChiQuyVal.Name = "lblChiQuyVal";
            this.lblChiQuyVal.Size = new System.Drawing.Size(57, 28);
            this.lblChiQuyVal.TabIndex = 4;
            this.lblChiQuyVal.Text = "- 0 đ";
            // 
            // lblChiQuyLabel
            // 
            this.lblChiQuyLabel.AutoSize = true;
            this.lblChiQuyLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblChiQuyLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblChiQuyLabel.Location = new System.Drawing.Point(8, 54);
            this.lblChiQuyLabel.Name = "lblChiQuyLabel";
            this.lblChiQuyLabel.Size = new System.Drawing.Size(183, 25);
            this.lblChiQuyLabel.TabIndex = 3;
            this.lblChiQuyLabel.Text = "Tổng chi trong 1 quý:";
            // 
            // lblThuQuyVal
            // 
            this.lblThuQuyVal.AutoSize = true;
            this.lblThuQuyVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblThuQuyVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblThuQuyVal.Location = new System.Drawing.Point(160, 27);
            this.lblThuQuyVal.Name = "lblThuQuyVal";
            this.lblThuQuyVal.Size = new System.Drawing.Size(63, 28);
            this.lblThuQuyVal.TabIndex = 2;
            this.lblThuQuyVal.Text = "+ 0 đ";
            // 
            // lblThuQuyLabel
            // 
            this.lblThuQuyLabel.AutoSize = true;
            this.lblThuQuyLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblThuQuyLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblThuQuyLabel.Location = new System.Drawing.Point(8, 30);
            this.lblThuQuyLabel.Name = "lblThuQuyLabel";
            this.lblThuQuyLabel.Size = new System.Drawing.Size(187, 25);
            this.lblThuQuyLabel.TabIndex = 1;
            this.lblThuQuyLabel.Text = "Tổng thu trong 1 quý:";
            // 
            // lblQuarterHeader
            // 
            this.lblQuarterHeader.AutoSize = true;
            this.lblQuarterHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblQuarterHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(105)))), ((int)(((byte)(161)))));
            this.lblQuarterHeader.Location = new System.Drawing.Point(8, 7);
            this.lblQuarterHeader.Name = "lblQuarterHeader";
            this.lblQuarterHeader.Size = new System.Drawing.Size(451, 25);
            this.lblQuarterHeader.TabIndex = 0;
            this.lblQuarterHeader.Text = "BÁO CÁO TỔNG THU CHI TRONG 1 QUÝ (90 NGÀY)";
            this.lblQuarterHeader.UseMnemonic = false;
            // 
            // pnlCustInfo
            // 
            this.pnlCustInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCustInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlCustInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlCustInfo.BorderRadius = 8;
            this.pnlCustInfo.BorderSize = 1;
            this.pnlCustInfo.Controls.Add(this.pnlBalanceCard);
            this.pnlCustInfo.Controls.Add(this.lblUserVal);
            this.pnlCustInfo.Controls.Add(this.lblUserLabel);
            this.pnlCustInfo.Controls.Add(this.lblMaKHVal);
            this.pnlCustInfo.Controls.Add(this.lblMaKHLabel);
            this.pnlCustInfo.Controls.Add(this.lblAccVal);
            this.pnlCustInfo.Controls.Add(this.lblAccLabel);
            this.pnlCustInfo.Controls.Add(this.lblCustHeader);
            this.pnlCustInfo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlCustInfo.Location = new System.Drawing.Point(2, 48);
            this.pnlCustInfo.Name = "pnlCustInfo";
            this.pnlCustInfo.Size = new System.Drawing.Size(517, 105);
            this.pnlCustInfo.TabIndex = 2;
            // 
            // pnlBalanceCard
            // 
            this.pnlBalanceCard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBalanceCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.pnlBalanceCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(230)))), ((int)(((byte)(253)))));
            this.pnlBalanceCard.BorderRadius = 6;
            this.pnlBalanceCard.BorderSize = 1;
            this.pnlBalanceCard.Controls.Add(this.lblBalanceVal);
            this.pnlBalanceCard.Controls.Add(this.lblBalanceLabel);
            this.pnlBalanceCard.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.pnlBalanceCard.Location = new System.Drawing.Point(343, 20);
            this.pnlBalanceCard.Name = "pnlBalanceCard";
            this.pnlBalanceCard.Size = new System.Drawing.Size(166, 68);
            this.pnlBalanceCard.TabIndex = 7;
            // 
            // lblBalanceVal
            // 
            this.lblBalanceVal.AutoSize = true;
            this.lblBalanceVal.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblBalanceVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(132)))), ((int)(((byte)(199)))));
            this.lblBalanceVal.Location = new System.Drawing.Point(8, 32);
            this.lblBalanceVal.Name = "lblBalanceVal";
            this.lblBalanceVal.Size = new System.Drawing.Size(77, 30);
            this.lblBalanceVal.TabIndex = 1;
            this.lblBalanceVal.Text = "0 VND";
            // 
            // lblBalanceLabel
            // 
            this.lblBalanceLabel.AutoSize = true;
            this.lblBalanceLabel.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblBalanceLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(105)))), ((int)(((byte)(161)))));
            this.lblBalanceLabel.Location = new System.Drawing.Point(8, 8);
            this.lblBalanceLabel.Name = "lblBalanceLabel";
            this.lblBalanceLabel.Size = new System.Drawing.Size(135, 23);
            this.lblBalanceLabel.TabIndex = 0;
            this.lblBalanceLabel.Text = "Số dư khả dụng:";
            // 
            // lblUserVal
            // 
            this.lblUserVal.AutoSize = true;
            this.lblUserVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUserVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblUserVal.Location = new System.Drawing.Point(105, 75);
            this.lblUserVal.Name = "lblUserVal";
            this.lblUserVal.Size = new System.Drawing.Size(33, 25);
            this.lblUserVal.TabIndex = 6;
            this.lblUserVal.Text = "---";
            // 
            // lblUserLabel
            // 
            this.lblUserLabel.AutoSize = true;
            this.lblUserLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUserLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblUserLabel.Location = new System.Drawing.Point(8, 75);
            this.lblUserLabel.Name = "lblUserLabel";
            this.lblUserLabel.Size = new System.Drawing.Size(141, 25);
            this.lblUserLabel.TabIndex = 5;
            this.lblUserLabel.Text = "Tên người dùng:";
            // 
            // lblMaKHVal
            // 
            this.lblMaKHVal.AutoSize = true;
            this.lblMaKHVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaKHVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblMaKHVal.Location = new System.Drawing.Point(105, 52);
            this.lblMaKHVal.Name = "lblMaKHVal";
            this.lblMaKHVal.Size = new System.Drawing.Size(33, 25);
            this.lblMaKHVal.TabIndex = 4;
            this.lblMaKHVal.Text = "---";
            // 
            // lblMaKHLabel
            // 
            this.lblMaKHLabel.AutoSize = true;
            this.lblMaKHLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMaKHLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMaKHLabel.Location = new System.Drawing.Point(8, 52);
            this.lblMaKHLabel.Name = "lblMaKHLabel";
            this.lblMaKHLabel.Size = new System.Drawing.Size(137, 25);
            this.lblMaKHLabel.TabIndex = 3;
            this.lblMaKHLabel.Text = "Mã khách hàng:";
            // 
            // lblAccVal
            // 
            this.lblAccVal.AutoSize = true;
            this.lblAccVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAccVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblAccVal.Location = new System.Drawing.Point(105, 29);
            this.lblAccVal.Name = "lblAccVal";
            this.lblAccVal.Size = new System.Drawing.Size(33, 25);
            this.lblAccVal.TabIndex = 2;
            this.lblAccVal.Text = "---";
            // 
            // lblAccLabel
            // 
            this.lblAccLabel.AutoSize = true;
            this.lblAccLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAccLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAccLabel.Location = new System.Drawing.Point(8, 29);
            this.lblAccLabel.Name = "lblAccLabel";
            this.lblAccLabel.Size = new System.Drawing.Size(119, 25);
            this.lblAccLabel.TabIndex = 1;
            this.lblAccLabel.Text = "Mã tài khoản:";
            // 
            // lblCustHeader
            // 
            this.lblCustHeader.AutoSize = true;
            this.lblCustHeader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCustHeader.Location = new System.Drawing.Point(8, 7);
            this.lblCustHeader.Name = "lblCustHeader";
            this.lblCustHeader.Size = new System.Drawing.Size(368, 25);
            this.lblCustHeader.TabIndex = 0;
            this.lblCustHeader.Text = "THÔNG TIN KHÁCH HÀNG & TÀI KHOẢN";
            this.lblCustHeader.UseMnemonic = false;
            // 
            // lblAppraisalSub
            // 
            this.lblAppraisalSub.AutoSize = true;
            this.lblAppraisalSub.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAppraisalSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAppraisalSub.Location = new System.Drawing.Point(3, 26);
            this.lblAppraisalSub.Name = "lblAppraisalSub";
            this.lblAppraisalSub.Size = new System.Drawing.Size(642, 25);
            this.lblAppraisalSub.TabIndex = 1;
            this.lblAppraisalSub.Text = "Hệ thống tự động phân tích dòng tiền 1 quý (90 ngày) & số dư khả dụng thực tế";
            // 
            // lblAppraisalTitle
            // 
            this.lblAppraisalTitle.AutoSize = true;
            this.lblAppraisalTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAppraisalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblAppraisalTitle.Location = new System.Drawing.Point(2, 2);
            this.lblAppraisalTitle.Name = "lblAppraisalTitle";
            this.lblAppraisalTitle.Size = new System.Drawing.Size(504, 32);
            this.lblAppraisalTitle.TabIndex = 0;
            this.lblAppraisalTitle.Text = "KẾT QUẢ THẨM ĐỊNH TÀI CHÍNH & HỒ SƠ";
            this.lblAppraisalTitle.UseMnemonic = false;
            // 
            // BorrowingAdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1200, 750);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "BorrowingAdminForm";
            this.Text = "Quản lý & Thẩm định Hồ sơ Vay Vốn (Admin)";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlSearchBox.ResumeLayout(false);
            this.pnlSearchBox.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.pnlLeftCard.ResumeLayout(false);
            this.pnlLeftCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoans)).EndInit();
            this.pnlRightCard.ResumeLayout(false);
            this.pnlAppraisalScroll.ResumeLayout(false);
            this.pnlAppraisalContent.ResumeLayout(false);
            this.pnlAppraisalContent.PerformLayout();
            this.pnlActionsRow.ResumeLayout(false);
            this.tlpActionButtons.ResumeLayout(false);
            this.pnlEvaluation.ResumeLayout(false);
            this.pnlEvaluation.PerformLayout();
            this.pnlLoanDetail.ResumeLayout(false);
            this.pnlLoanDetail.PerformLayout();
            this.pnlQuarterStats.ResumeLayout(false);
            this.pnlQuarterStats.PerformLayout();
            this.pnlCustInfo.ResumeLayout(false);
            this.pnlCustInfo.PerformLayout();
            this.pnlBalanceCard.ResumeLayout(false);
            this.pnlBalanceCard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblStatBadge;
        private BankAccountManagement.Forms.RoundedButton btnRefresh;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblFilterStatus;
        private System.Windows.Forms.ComboBox cboFilterStatus;
        private System.Windows.Forms.Label lblSearch;
        private BankAccountManagement.Forms.RoundedPanel pnlSearchBox;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private BankAccountManagement.Forms.RoundedPanel pnlLeftCard;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.Label lblGridSub;
        private System.Windows.Forms.DataGridView dgvLoans;
        private BankAccountManagement.Forms.RoundedPanel pnlRightCard;
        private System.Windows.Forms.Panel pnlAppraisalScroll;
        private System.Windows.Forms.Panel pnlAppraisalContent;
        private System.Windows.Forms.Label lblAppraisalTitle;
        private System.Windows.Forms.Label lblAppraisalSub;
        private BankAccountManagement.Forms.RoundedPanel pnlCustInfo;
        private System.Windows.Forms.Label lblCustHeader;
        private System.Windows.Forms.Label lblAccLabel;
        private System.Windows.Forms.Label lblAccVal;
        private System.Windows.Forms.Label lblMaKHLabel;
        private System.Windows.Forms.Label lblMaKHVal;
        private System.Windows.Forms.Label lblUserLabel;
        private System.Windows.Forms.Label lblUserVal;
        private BankAccountManagement.Forms.RoundedPanel pnlBalanceCard;
        private System.Windows.Forms.Label lblBalanceLabel;
        private System.Windows.Forms.Label lblBalanceVal;
        private BankAccountManagement.Forms.RoundedPanel pnlQuarterStats;
        private System.Windows.Forms.Label lblQuarterHeader;
        private System.Windows.Forms.Label lblThuQuyLabel;
        private System.Windows.Forms.Label lblThuQuyVal;
        private System.Windows.Forms.Label lblChiQuyLabel;
        private System.Windows.Forms.Label lblChiQuyVal;
        private System.Windows.Forms.Label lblChenhLechLabel;
        private System.Windows.Forms.Label lblChenhLechVal;
        private System.Windows.Forms.Label lblSoGDLabel;
        private System.Windows.Forms.Label lblSoGDVal;
        private BankAccountManagement.Forms.RoundedPanel pnlLoanDetail;
        private System.Windows.Forms.Label lblLoanHeader;
        private System.Windows.Forms.Label lblLoanIdLabel;
        private System.Windows.Forms.Label lblLoanIdVal;
        private System.Windows.Forms.Label lblLoanAmountLabel;
        private System.Windows.Forms.Label lblLoanAmountVal;
        private System.Windows.Forms.Label lblTermInterestLabel;
        private System.Windows.Forms.Label lblTermInterestVal;
        private System.Windows.Forms.Label lblTotalDebtLabel;
        private System.Windows.Forms.Label lblTotalDebtVal;
        private System.Windows.Forms.Label lblStatusBadge;
        private BankAccountManagement.Forms.RoundedPanel pnlEvaluation;
        private System.Windows.Forms.Label lblEvalTitle;
        private System.Windows.Forms.Label lblEvalBadge;
        private System.Windows.Forms.Label lblEvalNote;
        private System.Windows.Forms.Panel pnlActionsRow;
        private System.Windows.Forms.TableLayoutPanel tlpActionButtons;
        private BankAccountManagement.Forms.RoundedButton btnApprove;
        private BankAccountManagement.Forms.RoundedButton btnReject;
    }
}
