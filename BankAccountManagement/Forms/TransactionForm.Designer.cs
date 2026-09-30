namespace BankAccountManagement.Forms
{
    partial class TransactionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransactionForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.comboType = new System.Windows.Forms.ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.comboAccount = new System.Windows.Forms.ComboBox();
            this.labelAccount = new System.Windows.Forms.Label();
            this.textSearch = new System.Windows.Forms.TextBox();
            this.labelSearch = new System.Windows.Forms.Label();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.panelCount = new System.Windows.Forms.Panel();
            this.labelCount = new System.Windows.Forms.Label();
            this.labelCountTitle = new System.Windows.Forms.Label();
            this.panelExpense = new System.Windows.Forms.Panel();
            this.labelExpense = new System.Windows.Forms.Label();
            this.labelExpenseTitle = new System.Windows.Forms.Label();
            this.panelIncome = new System.Windows.Forms.Panel();
            this.labelIncome = new System.Windows.Forms.Label();
            this.labelIncomeTitle = new System.Windows.Forms.Label();
            this.panelTable = new System.Windows.Forms.Panel();
            this.dataGridViewTransactions = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelTableTitle = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.buttonNext = new System.Windows.Forms.Button();
            this.labelPage = new System.Windows.Forms.Label();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.labelPageInfo = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelFilter.SuspendLayout();
            this.panelSummary.SuspendLayout();
            this.panelCount.SuspendLayout();
            this.panelExpense.SuspendLayout();
            this.panelIncome.SuspendLayout();
            this.panelTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.pictureBox1);
            this.panelHeader.Controls.Add(this.labelSubtitle);
            this.panelHeader.Controls.Add(this.labelTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1100, 86);
            this.panelHeader.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 52);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // labelSubtitle
            // 
            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.labelSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(116)))), ((int)(((byte)(135)))));
            this.labelSubtitle.Location = new System.Drawing.Point(93, 47);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(285, 17);
            this.labelSubtitle.TabIndex = 2;
            this.labelSubtitle.Text = "Theo dõi và quản lý các giao dịch của tài khoản";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(37)))), ((int)(((byte)(54)))));
            this.labelTitle.Location = new System.Drawing.Point(91, 12);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(116, 32);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Giao dịch";
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.White;
            this.panelFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFilter.Controls.Add(this.buttonAdd);
            this.panelFilter.Controls.Add(this.buttonExport);
            this.panelFilter.Controls.Add(this.buttonRefresh);
            this.panelFilter.Controls.Add(this.buttonSearch);
            this.panelFilter.Controls.Add(this.comboType);
            this.panelFilter.Controls.Add(this.labelType);
            this.panelFilter.Controls.Add(this.comboAccount);
            this.panelFilter.Controls.Add(this.labelAccount);
            this.panelFilter.Controls.Add(this.textSearch);
            this.panelFilter.Controls.Add(this.labelSearch);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 86);
            this.panelFilter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1100, 118);
            this.panelFilter.TabIndex = 1;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(105)))), ((int)(((byte)(224)))));
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.Location = new System.Drawing.Point(916, 69);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(162, 34);
            this.buttonAdd.TabIndex = 9;
            this.buttonAdd.Text = "+  Thêm giao dịch";
            this.buttonAdd.UseVisualStyleBackColor = false;
            // 
            // buttonExport
            // 
            this.buttonExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.buttonExport.FlatAppearance.BorderSize = 0;
            this.buttonExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExport.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(37)))), ((int)(((byte)(54)))));
            this.buttonExport.Location = new System.Drawing.Point(229, 69);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(105, 34);
            this.buttonExport.TabIndex = 8;
            this.buttonExport.Text = "Xuất Excel";
            this.buttonExport.UseVisualStyleBackColor = false;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(241)))), ((int)(((byte)(249)))));
            this.buttonRefresh.FlatAppearance.BorderSize = 0;
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(37)))), ((int)(((byte)(54)))));
            this.buttonRefresh.Location = new System.Drawing.Point(126, 69);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(95, 34);
            this.buttonRefresh.TabIndex = 7;
            this.buttonRefresh.Text = "Làm mới";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(105)))), ((int)(((byte)(224)))));
            this.buttonSearch.FlatAppearance.BorderSize = 0;
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonSearch.ForeColor = System.Drawing.Color.White;
            this.buttonSearch.Location = new System.Drawing.Point(18, 69);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(100, 34);
            this.buttonSearch.TabIndex = 6;
            this.buttonSearch.Text = "Tìm kiếm";
            this.buttonSearch.UseVisualStyleBackColor = false;
            // 
            // comboType
            // 
            this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.comboType.FormattingEnabled = true;
            this.comboType.Items.AddRange(new object[] {
            "Tất cả",
            "Thu",
            "Chi"});
            this.comboType.Location = new System.Drawing.Point(559, 31);
            this.comboType.Name = "comboType";
            this.comboType.Size = new System.Drawing.Size(135, 25);
            this.comboType.TabIndex = 5;
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.labelType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(72)))), ((int)(((byte)(90)))));
            this.labelType.Location = new System.Drawing.Point(562, 10);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(81, 15);
            this.labelType.TabIndex = 4;
            this.labelType.Text = "Loại giao dịch";
            // 
            // comboAccount
            // 
            this.comboAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAccount.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.comboAccount.FormattingEnabled = true;
            this.comboAccount.Items.AddRange(new object[] {
            "Tất cả tài khoản",
            "Vietcombank",
            "Techcombank",
            "BIDV"});
            this.comboAccount.Location = new System.Drawing.Point(394, 31);
            this.comboAccount.Name = "comboAccount";
            this.comboAccount.Size = new System.Drawing.Size(150, 25);
            this.comboAccount.TabIndex = 3;
            // 
            // labelAccount
            // 
            this.labelAccount.AutoSize = true;
            this.labelAccount.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.labelAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(72)))), ((int)(((byte)(90)))));
            this.labelAccount.Location = new System.Drawing.Point(397, 10);
            this.labelAccount.Name = "labelAccount";
            this.labelAccount.Size = new System.Drawing.Size(58, 15);
            this.labelAccount.TabIndex = 2;
            this.labelAccount.Text = "Tài khoản";
            // 
            // textSearch
            // 
            this.textSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(116)))), ((int)(((byte)(132)))));
            this.textSearch.Location = new System.Drawing.Point(18, 31);
            this.textSearch.Name = "textSearch";
            this.textSearch.Size = new System.Drawing.Size(360, 25);
            this.textSearch.TabIndex = 1;
            this.textSearch.Text = "   Nội dung, tài khoản, mã giao dịch...";
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.labelSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(72)))), ((int)(((byte)(90)))));
            this.labelSearch.Location = new System.Drawing.Point(15, 10);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(57, 15);
            this.labelSearch.TabIndex = 0;
            this.labelSearch.Text = "Tìm kiếm";
            // 
            // panelSummary
            // 
            this.panelSummary.BackColor = System.Drawing.Color.White;
            this.panelSummary.Controls.Add(this.panelCount);
            this.panelSummary.Controls.Add(this.panelExpense);
            this.panelSummary.Controls.Add(this.panelIncome);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSummary.Location = new System.Drawing.Point(0, 204);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.panelSummary.Size = new System.Drawing.Size(1100, 92);
            this.panelSummary.TabIndex = 2;
            // 
            // panelCount
            // 
            this.panelCount.BackColor = System.Drawing.Color.White;
            this.panelCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCount.Controls.Add(this.labelCount);
            this.panelCount.Controls.Add(this.labelCountTitle);
            this.panelCount.Location = new System.Drawing.Point(668, 10);
            this.panelCount.Name = "panelCount";
            this.panelCount.Size = new System.Drawing.Size(310, 70);
            this.panelCount.TabIndex = 2;
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.labelCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(37)))), ((int)(((byte)(54)))));
            this.labelCount.Location = new System.Drawing.Point(14, 33);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(22, 25);
            this.labelCount.TabIndex = 1;
            this.labelCount.Text = "8";
            // 
            // labelCountTitle
            // 
            this.labelCountTitle.AutoSize = true;
            this.labelCountTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelCountTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(116)))), ((int)(((byte)(135)))));
            this.labelCountTitle.Location = new System.Drawing.Point(15, 10);
            this.labelCountTitle.Name = "labelCountTitle";
            this.labelCountTitle.Size = new System.Drawing.Size(72, 15);
            this.labelCountTitle.TabIndex = 0;
            this.labelCountTitle.Text = "Số giao dịch";
            // 
            // panelExpense
            // 
            this.panelExpense.BackColor = System.Drawing.Color.White;
            this.panelExpense.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelExpense.Controls.Add(this.labelExpense);
            this.panelExpense.Controls.Add(this.labelExpenseTitle);
            this.panelExpense.Location = new System.Drawing.Point(342, 10);
            this.panelExpense.Name = "panelExpense";
            this.panelExpense.Size = new System.Drawing.Size(310, 70);
            this.panelExpense.TabIndex = 1;
            // 
            // labelExpense
            // 
            this.labelExpense.AutoSize = true;
            this.labelExpense.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.labelExpense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.labelExpense.Location = new System.Drawing.Point(14, 33);
            this.labelExpense.Name = "labelExpense";
            this.labelExpense.Size = new System.Drawing.Size(118, 25);
            this.labelExpense.TabIndex = 1;
            this.labelExpense.Text = "- 2,050,000 ₫";
            // 
            // labelExpenseTitle
            // 
            this.labelExpenseTitle.AutoSize = true;
            this.labelExpenseTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelExpenseTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(116)))), ((int)(((byte)(135)))));
            this.labelExpenseTitle.Location = new System.Drawing.Point(15, 10);
            this.labelExpenseTitle.Name = "labelExpenseTitle";
            this.labelExpenseTitle.Size = new System.Drawing.Size(54, 15);
            this.labelExpenseTitle.TabIndex = 0;
            this.labelExpenseTitle.Text = "Tổng chi";
            // 
            // panelIncome
            // 
            this.panelIncome.BackColor = System.Drawing.Color.White;
            this.panelIncome.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelIncome.Controls.Add(this.labelIncome);
            this.panelIncome.Controls.Add(this.labelIncomeTitle);
            this.panelIncome.Location = new System.Drawing.Point(16, 10);
            this.panelIncome.Name = "panelIncome";
            this.panelIncome.Size = new System.Drawing.Size(310, 70);
            this.panelIncome.TabIndex = 0;
            // 
            // labelIncome
            // 
            this.labelIncome.AutoSize = true;
            this.labelIncome.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.labelIncome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(160)))), ((int)(((byte)(105)))));
            this.labelIncome.Location = new System.Drawing.Point(14, 33);
            this.labelIncome.Name = "labelIncome";
            this.labelIncome.Size = new System.Drawing.Size(124, 25);
            this.labelIncome.TabIndex = 1;
            this.labelIncome.Text = "+ 4,700,000 ₫";
            // 
            // labelIncomeTitle
            // 
            this.labelIncomeTitle.AutoSize = true;
            this.labelIncomeTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelIncomeTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(116)))), ((int)(((byte)(135)))));
            this.labelIncomeTitle.Location = new System.Drawing.Point(15, 10);
            this.labelIncomeTitle.Name = "labelIncomeTitle";
            this.labelIncomeTitle.Size = new System.Drawing.Size(56, 15);
            this.labelIncomeTitle.TabIndex = 0;
            this.labelIncomeTitle.Text = "Tổng thu";
            // 
            // panelTable
            // 
            this.panelTable.BackColor = System.Drawing.Color.White;
            this.panelTable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTable.Controls.Add(this.dataGridViewTransactions);
            this.panelTable.Controls.Add(this.labelTableTitle);
            this.panelTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTable.Location = new System.Drawing.Point(0, 296);
            this.panelTable.Name = "panelTable";
            this.panelTable.Padding = new System.Windows.Forms.Padding(14, 42, 14, 52);
            this.panelTable.Size = new System.Drawing.Size(1100, 384);
            this.panelTable.TabIndex = 3;
            // 
            // dataGridViewTransactions
            // 
            this.dataGridViewTransactions.AllowUserToAddRows = false;
            this.dataGridViewTransactions.AllowUserToDeleteRows = false;
            this.dataGridViewTransactions.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(253)))));
            this.dataGridViewTransactions.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTransactions.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewTransactions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewTransactions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewTransactions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(55)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTransactions.ColumnHeadersHeight = 42;
            this.dataGridViewTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewTransactions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn6});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(57)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(240)))), ((int)(((byte)(253)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(55)))), ((int)(((byte)(92)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTransactions.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTransactions.EnableHeadersVisualStyles = false;
            this.dataGridViewTransactions.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(234)))), ((int)(((byte)(240)))));
            this.dataGridViewTransactions.Location = new System.Drawing.Point(14, 42);
            this.dataGridViewTransactions.MultiSelect = false;
            this.dataGridViewTransactions.Name = "dataGridViewTransactions";
            this.dataGridViewTransactions.ReadOnly = true;
            this.dataGridViewTransactions.RowHeadersVisible = false;
            this.dataGridViewTransactions.RowTemplate.Height = 44;
            this.dataGridViewTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTransactions.Size = new System.Drawing.Size(1070, 288);
            this.dataGridViewTransactions.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Ngày giao dịch";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Nội dung";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Tài khoản";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Loại";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Số tiền";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Thao tác";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.HeaderText = "Trạng thái";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // labelTableTitle
            // 
            this.labelTableTitle.AutoSize = true;
            this.labelTableTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.labelTableTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(37)))), ((int)(((byte)(54)))));
            this.labelTableTitle.Location = new System.Drawing.Point(14, 19);
            this.labelTableTitle.Name = "labelTableTitle";
            this.labelTableTitle.Size = new System.Drawing.Size(147, 20);
            this.labelTableTitle.TabIndex = 0;
            this.labelTableTitle.Text = "Danh sách giao dịch";
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.buttonNext);
            this.panelFooter.Controls.Add(this.labelPage);
            this.panelFooter.Controls.Add(this.buttonPrevious);
            this.panelFooter.Controls.Add(this.labelPageInfo);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(14, 332);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1070, 50);
            this.panelFooter.TabIndex = 2;
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.BackColor = System.Drawing.Color.White;
            this.buttonNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
            this.buttonNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNext.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.buttonNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(55)))), ((int)(((byte)(92)))));
            this.buttonNext.Location = new System.Drawing.Point(986, 8);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(42, 32);
            this.buttonNext.TabIndex = 3;
            this.buttonNext.Text = "›";
            this.buttonNext.UseVisualStyleBackColor = false;
            // 
            // labelPage
            // 
            this.labelPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(105)))), ((int)(((byte)(224)))));
            this.labelPage.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.labelPage.ForeColor = System.Drawing.Color.White;
            this.labelPage.Location = new System.Drawing.Point(947, 8);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(34, 32);
            this.labelPage.TabIndex = 2;
            this.labelPage.Text = "1";
            this.labelPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrevious.BackColor = System.Drawing.Color.White;
            this.buttonPrevious.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(218)))), ((int)(((byte)(230)))));
            this.buttonPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrevious.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.buttonPrevious.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(55)))), ((int)(((byte)(92)))));
            this.buttonPrevious.Location = new System.Drawing.Point(900, 8);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(42, 32);
            this.buttonPrevious.TabIndex = 1;
            this.buttonPrevious.Text = "‹";
            this.buttonPrevious.UseVisualStyleBackColor = false;
            // 
            // labelPageInfo
            // 
            this.labelPageInfo.AutoSize = true;
            this.labelPageInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelPageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(103)))), ((int)(((byte)(116)))), ((int)(((byte)(135)))));
            this.labelPageInfo.Location = new System.Drawing.Point(0, 17);
            this.labelPageInfo.Name = "labelPageInfo";
            this.labelPageInfo.Size = new System.Drawing.Size(150, 15);
            this.labelPageInfo.TabIndex = 0;
            this.labelPageInfo.Text = "Hiển thị 1 - 8 / 24 giao dịch";
            // 
            // TransactionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.panelTable);
            this.Controls.Add(this.panelSummary);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TransactionForm";
            this.Text = "Giao dịch";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.panelSummary.ResumeLayout(false);
            this.panelCount.ResumeLayout(false);
            this.panelCount.PerformLayout();
            this.panelExpense.ResumeLayout(false);
            this.panelExpense.PerformLayout();
            this.panelIncome.ResumeLayout(false);
            this.panelIncome.PerformLayout();
            this.panelTable.ResumeLayout(false);
            this.panelTable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelSubtitle;

        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.TextBox textSearch;
        private System.Windows.Forms.Label labelAccount;
        private System.Windows.Forms.ComboBox comboAccount;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox comboType;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonAdd;

        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Panel panelIncome;
        private System.Windows.Forms.Label labelIncomeTitle;
        private System.Windows.Forms.Label labelIncome;
        private System.Windows.Forms.Panel panelExpense;
        private System.Windows.Forms.Label labelExpenseTitle;
        private System.Windows.Forms.Label labelExpense;
        private System.Windows.Forms.Panel panelCount;
        private System.Windows.Forms.Label labelCountTitle;
        private System.Windows.Forms.Label labelCount;

        private System.Windows.Forms.Panel panelTable;
        private System.Windows.Forms.Label labelTableTitle;
        private System.Windows.Forms.DataGridView dataGridViewTransactions;

        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Label labelPageInfo;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    }
}