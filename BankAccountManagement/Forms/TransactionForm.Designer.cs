namespace BankAccountManagement
{
    partial class TransactionForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeaderIcon = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.tblSummary = new System.Windows.Forms.TableLayoutPanel();
            this.panelIncome = new System.Windows.Forms.Panel();
            this.lblIncomeBadge = new System.Windows.Forms.Label();
            this.lblIncomeSub = new System.Windows.Forms.Label();
            this.labelIncome = new System.Windows.Forms.Label();
            this.labelIncomeTitle = new System.Windows.Forms.Label();
            this.panelExpense = new System.Windows.Forms.Panel();
            this.lblExpenseBadge = new System.Windows.Forms.Label();
            this.lblExpenseSub = new System.Windows.Forms.Label();
            this.labelExpense = new System.Windows.Forms.Label();
            this.labelExpenseTitle = new System.Windows.Forms.Label();
            this.panelCount = new System.Windows.Forms.Panel();
            this.lblCountBadge = new System.Windows.Forms.Label();
            this.lblCountSub = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.labelCountTitle = new System.Windows.Forms.Label();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.pnlFilterCard = new System.Windows.Forms.Panel();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.comboType = new System.Windows.Forms.ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.comboAccount = new System.Windows.Forms.ComboBox();
            this.labelAccount = new System.Windows.Forms.Label();
            this.textSearch = new System.Windows.Forms.TextBox();
            this.labelSearch = new System.Windows.Forms.Label();
            this.panelTable = new System.Windows.Forms.Panel();
            this.pnlGridCard = new System.Windows.Forms.Panel();
            this.dataGridViewTransactions = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.labelPageInfo = new System.Windows.Forms.Label();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.labelPage = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.pnlTableTop = new System.Windows.Forms.Panel();
            this.lblTableHint = new System.Windows.Forms.Label();
            this.labelTableTitle = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelSummary.SuspendLayout();
            this.tblSummary.SuspendLayout();
            this.panelIncome.SuspendLayout();
            this.panelExpense.SuspendLayout();
            this.panelCount.SuspendLayout();
            this.panelFilter.SuspendLayout();
            this.pnlFilterCard.SuspendLayout();
            this.panelTable.SuspendLayout();
            this.pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.pnlTableTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblHeaderIcon);
            this.panelHeader.Controls.Add(this.labelTitle);
            this.panelHeader.Controls.Add(this.labelSubtitle);
            this.panelHeader.Controls.Add(this.buttonExport);
            this.panelHeader.Controls.Add(this.buttonAdd);
            this.panelHeader.Controls.Add(this.pictureBox1);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(24, 0, 24, 0);
            this.panelHeader.Size = new System.Drawing.Size(1100, 75);
            this.panelHeader.TabIndex = 0;
            this.panelHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelBottomBorder_Paint);
            // 
            // lblHeaderIcon
            // 
            this.lblHeaderIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.lblHeaderIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 16F);
            this.lblHeaderIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.lblHeaderIcon.Location = new System.Drawing.Point(24, 16);
            this.lblHeaderIcon.Name = "lblHeaderIcon";
            this.lblHeaderIcon.Size = new System.Drawing.Size(44, 44);
            this.lblHeaderIcon.TabIndex = 0;
            this.lblHeaderIcon.Text = "💳";
            this.lblHeaderIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.labelTitle.Location = new System.Drawing.Point(78, 12);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(188, 30);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Lịch sử Giao dịch";
            // 
            // labelSubtitle
            // 
            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelSubtitle.Location = new System.Drawing.Point(80, 44);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(325, 15);
            this.labelSubtitle.TabIndex = 2;
            this.labelSubtitle.Text = "Theo dõi dòng tiền thu - chi và quản lý lịch sử biến động số dư";
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.buttonExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.buttonExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExport.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.buttonExport.Location = new System.Drawing.Point(784, 19);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(120, 38);
            this.buttonExport.TabIndex = 4;
            this.buttonExport.Text = "📥  Xuất Excel";
            this.buttonExport.UseVisualStyleBackColor = false;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.buttonAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.Location = new System.Drawing.Point(916, 19);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(160, 38);
            this.buttonAdd.TabIndex = 5;
            this.buttonAdd.Text = "➕  Thêm giao dịch";
            this.buttonAdd.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1, 1);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // panelSummary
            // 
            this.panelSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelSummary.Controls.Add(this.tblSummary);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSummary.Location = new System.Drawing.Point(0, 75);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Padding = new System.Windows.Forms.Padding(24, 16, 24, 8);
            this.panelSummary.Size = new System.Drawing.Size(1100, 110);
            this.panelSummary.TabIndex = 1;
            // 
            // tblSummary
            // 
            this.tblSummary.ColumnCount = 3;
            this.tblSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblSummary.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tblSummary.Controls.Add(this.panelIncome, 0, 0);
            this.tblSummary.Controls.Add(this.panelExpense, 1, 0);
            this.tblSummary.Controls.Add(this.panelCount, 2, 0);
            this.tblSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblSummary.Location = new System.Drawing.Point(24, 16);
            this.tblSummary.Name = "tblSummary";
            this.tblSummary.RowCount = 1;
            this.tblSummary.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSummary.Size = new System.Drawing.Size(1052, 86);
            this.tblSummary.TabIndex = 0;
            // 
            // panelIncome
            // 
            this.panelIncome.BackColor = System.Drawing.Color.White;
            this.panelIncome.Controls.Add(this.lblIncomeBadge);
            this.panelIncome.Controls.Add(this.lblIncomeSub);
            this.panelIncome.Controls.Add(this.labelIncome);
            this.panelIncome.Controls.Add(this.labelIncomeTitle);
            this.panelIncome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelIncome.Location = new System.Drawing.Point(0, 0);
            this.panelIncome.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.panelIncome.Name = "panelIncome";
            this.panelIncome.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.panelIncome.Size = new System.Drawing.Size(342, 86);
            this.panelIncome.TabIndex = 0;
            this.panelIncome.Paint += new System.Windows.Forms.PaintEventHandler(this.CardBorder_Paint);
            // 
            // lblIncomeBadge
            // 
            this.lblIncomeBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIncomeBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.lblIncomeBadge.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblIncomeBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblIncomeBadge.Location = new System.Drawing.Point(292, 12);
            this.lblIncomeBadge.Name = "lblIncomeBadge";
            this.lblIncomeBadge.Size = new System.Drawing.Size(34, 34);
            this.lblIncomeBadge.TabIndex = 3;
            this.lblIncomeBadge.Text = "⬇️";
            this.lblIncomeBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIncomeSub
            // 
            this.lblIncomeSub.AutoSize = true;
            this.lblIncomeSub.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblIncomeSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblIncomeSub.Location = new System.Drawing.Point(16, 61);
            this.lblIncomeSub.Name = "lblIncomeSub";
            this.lblIncomeSub.Size = new System.Drawing.Size(133, 13);
            this.lblIncomeSub.TabIndex = 2;
            this.lblIncomeSub.Text = "Tiền nạp & nhận chuyển khoản";
            // 
            // labelIncome
            // 
            this.labelIncome.AutoSize = true;
            this.labelIncome.Font = new System.Drawing.Font("Segoe UI", 14.5F, System.Drawing.FontStyle.Bold);
            this.labelIncome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.labelIncome.Location = new System.Drawing.Point(14, 31);
            this.labelIncome.Name = "labelIncome";
            this.labelIncome.Size = new System.Drawing.Size(63, 28);
            this.labelIncome.TabIndex = 1;
            this.labelIncome.Text = "+ 0 ₫";
            // 
            // labelIncomeTitle
            // 
            this.labelIncomeTitle.AutoSize = true;
            this.labelIncomeTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.labelIncomeTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelIncomeTitle.Location = new System.Drawing.Point(16, 12);
            this.labelIncomeTitle.Name = "labelIncomeTitle";
            this.labelIncomeTitle.Size = new System.Drawing.Size(107, 15);
            this.labelIncomeTitle.TabIndex = 0;
            this.labelIncomeTitle.Text = "TỔNG TIỀN THU VÀO";
            // 
            // panelExpense
            // 
            this.panelExpense.BackColor = System.Drawing.Color.White;
            this.panelExpense.Controls.Add(this.lblExpenseBadge);
            this.panelExpense.Controls.Add(this.lblExpenseSub);
            this.panelExpense.Controls.Add(this.labelExpense);
            this.panelExpense.Controls.Add(this.labelExpenseTitle);
            this.panelExpense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExpense.Location = new System.Drawing.Point(354, 0);
            this.panelExpense.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.panelExpense.Name = "panelExpense";
            this.panelExpense.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.panelExpense.Size = new System.Drawing.Size(342, 86);
            this.panelExpense.TabIndex = 1;
            this.panelExpense.Paint += new System.Windows.Forms.PaintEventHandler(this.CardBorder_Paint);
            // 
            // lblExpenseBadge
            // 
            this.lblExpenseBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExpenseBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.lblExpenseBadge.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblExpenseBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblExpenseBadge.Location = new System.Drawing.Point(292, 12);
            this.lblExpenseBadge.Name = "lblExpenseBadge";
            this.lblExpenseBadge.Size = new System.Drawing.Size(34, 34);
            this.lblExpenseBadge.TabIndex = 3;
            this.lblExpenseBadge.Text = "⬆️";
            this.lblExpenseBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblExpenseSub
            // 
            this.lblExpenseSub.AutoSize = true;
            this.lblExpenseSub.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblExpenseSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblExpenseSub.Location = new System.Drawing.Point(16, 61);
            this.lblExpenseSub.Name = "lblExpenseSub";
            this.lblExpenseSub.Size = new System.Drawing.Size(130, 13);
            this.lblExpenseSub.TabIndex = 2;
            this.lblExpenseSub.Text = "Chuyển tiền & thanh toán";
            // 
            // labelExpense
            // 
            this.labelExpense.AutoSize = true;
            this.labelExpense.Font = new System.Drawing.Font("Segoe UI", 14.5F, System.Drawing.FontStyle.Bold);
            this.labelExpense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.labelExpense.Location = new System.Drawing.Point(14, 31);
            this.labelExpense.Name = "labelExpense";
            this.labelExpense.Size = new System.Drawing.Size(56, 28);
            this.labelExpense.TabIndex = 1;
            this.labelExpense.Text = "- 0 ₫";
            // 
            // labelExpenseTitle
            // 
            this.labelExpenseTitle.AutoSize = true;
            this.labelExpenseTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.labelExpenseTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelExpenseTitle.Location = new System.Drawing.Point(16, 12);
            this.labelExpenseTitle.Name = "labelExpenseTitle";
            this.labelExpenseTitle.Size = new System.Drawing.Size(107, 15);
            this.labelExpenseTitle.TabIndex = 0;
            this.labelExpenseTitle.Text = "TỔNG TIỀN CHI RA";
            // 
            // panelCount
            // 
            this.panelCount.BackColor = System.Drawing.Color.White;
            this.panelCount.Controls.Add(this.lblCountBadge);
            this.panelCount.Controls.Add(this.lblCountSub);
            this.panelCount.Controls.Add(this.labelCount);
            this.panelCount.Controls.Add(this.labelCountTitle);
            this.panelCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCount.Location = new System.Drawing.Point(704, 0);
            this.panelCount.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.panelCount.Name = "panelCount";
            this.panelCount.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.panelCount.Size = new System.Drawing.Size(348, 86);
            this.panelCount.TabIndex = 2;
            this.panelCount.Paint += new System.Windows.Forms.PaintEventHandler(this.CardBorder_Paint);
            // 
            // lblCountBadge
            // 
            this.lblCountBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCountBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.lblCountBadge.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblCountBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblCountBadge.Location = new System.Drawing.Point(298, 12);
            this.lblCountBadge.Name = "lblCountBadge";
            this.lblCountBadge.Size = new System.Drawing.Size(34, 34);
            this.lblCountBadge.TabIndex = 3;
            this.lblCountBadge.Text = "📑";
            this.lblCountBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCountSub
            // 
            this.lblCountSub.AutoSize = true;
            this.lblCountSub.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblCountSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCountSub.Location = new System.Drawing.Point(16, 61);
            this.lblCountSub.Name = "lblCountSub";
            this.lblCountSub.Size = new System.Drawing.Size(120, 13);
            this.lblCountSub.TabIndex = 2;
            this.lblCountSub.Text = "Ghi nhận thành công";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Font = new System.Drawing.Font("Segoe UI", 14.5F, System.Drawing.FontStyle.Bold);
            this.labelCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.labelCount.Location = new System.Drawing.Point(14, 31);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(24, 28);
            this.labelCount.TabIndex = 1;
            this.labelCount.Text = "0";
            // 
            // labelCountTitle
            // 
            this.labelCountTitle.AutoSize = true;
            this.labelCountTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.labelCountTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelCountTitle.Location = new System.Drawing.Point(16, 12);
            this.labelCountTitle.Name = "labelCountTitle";
            this.labelCountTitle.Size = new System.Drawing.Size(117, 15);
            this.labelCountTitle.TabIndex = 0;
            this.labelCountTitle.Text = "SỐ LƯỢNG GIAO DỊCH";
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelFilter.Controls.Add(this.pnlFilterCard);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 185);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Padding = new System.Windows.Forms.Padding(24, 4, 24, 8);
            this.panelFilter.Size = new System.Drawing.Size(1100, 76);
            this.panelFilter.TabIndex = 2;
            // 
            // pnlFilterCard
            // 
            this.pnlFilterCard.BackColor = System.Drawing.Color.White;
            this.pnlFilterCard.Controls.Add(this.buttonRefresh);
            this.pnlFilterCard.Controls.Add(this.buttonSearch);
            this.pnlFilterCard.Controls.Add(this.comboType);
            this.pnlFilterCard.Controls.Add(this.labelType);
            this.pnlFilterCard.Controls.Add(this.comboAccount);
            this.pnlFilterCard.Controls.Add(this.labelAccount);
            this.pnlFilterCard.Controls.Add(this.textSearch);
            this.pnlFilterCard.Controls.Add(this.labelSearch);
            this.pnlFilterCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFilterCard.Location = new System.Drawing.Point(24, 4);
            this.pnlFilterCard.Name = "pnlFilterCard";
            this.pnlFilterCard.Size = new System.Drawing.Size(1052, 64);
            this.pnlFilterCard.TabIndex = 0;
            this.pnlFilterCard.Paint += new System.Windows.Forms.PaintEventHandler(this.CardBorder_Paint);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.buttonRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.buttonRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.buttonRefresh.Location = new System.Drawing.Point(744, 22);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(95, 30);
            this.buttonRefresh.TabIndex = 7;
            this.buttonRefresh.Text = "Làm mới";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.buttonSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSearch.FlatAppearance.BorderSize = 0;
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSearch.ForeColor = System.Drawing.Color.White;
            this.buttonSearch.Location = new System.Drawing.Point(642, 22);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(95, 30);
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
            this.comboType.Location = new System.Drawing.Point(490, 25);
            this.comboType.Name = "comboType";
            this.comboType.Size = new System.Drawing.Size(135, 25);
            this.comboType.TabIndex = 5;
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.labelType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelType.Location = new System.Drawing.Point(490, 8);
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
            this.comboAccount.Location = new System.Drawing.Point(300, 25);
            this.comboAccount.Name = "comboAccount";
            this.comboAccount.Size = new System.Drawing.Size(175, 25);
            this.comboAccount.TabIndex = 3;
            // 
            // labelAccount
            // 
            this.labelAccount.AutoSize = true;
            this.labelAccount.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.labelAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelAccount.Location = new System.Drawing.Point(300, 8);
            this.labelAccount.Name = "labelAccount";
            this.labelAccount.Size = new System.Drawing.Size(57, 15);
            this.labelAccount.TabIndex = 2;
            this.labelAccount.Text = "Tài khoản";
            // 
            // textSearch
            // 
            this.textSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.textSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.textSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.textSearch.Location = new System.Drawing.Point(16, 26);
            this.textSearch.Name = "textSearch";
            this.textSearch.Size = new System.Drawing.Size(268, 24);
            this.textSearch.TabIndex = 1;
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.labelSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelSearch.Location = new System.Drawing.Point(16, 8);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(107, 15);
            this.labelSearch.TabIndex = 0;
            this.labelSearch.Text = "Tìm kiếm giao dịch";
            // 
            // panelTable
            // 
            this.panelTable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelTable.Controls.Add(this.pnlGridCard);
            this.panelTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTable.Location = new System.Drawing.Point(0, 261);
            this.panelTable.Name = "panelTable";
            this.panelTable.Padding = new System.Windows.Forms.Padding(24, 4, 24, 16);
            this.panelTable.Size = new System.Drawing.Size(1100, 419);
            this.panelTable.TabIndex = 3;
            // 
            // pnlGridCard
            // 
            this.pnlGridCard.BackColor = System.Drawing.Color.White;
            this.pnlGridCard.Controls.Add(this.dataGridViewTransactions);
            this.pnlGridCard.Controls.Add(this.panelFooter);
            this.pnlGridCard.Controls.Add(this.pnlTableTop);
            this.pnlGridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridCard.Location = new System.Drawing.Point(24, 4);
            this.pnlGridCard.Name = "pnlGridCard";
            this.pnlGridCard.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.pnlGridCard.Size = new System.Drawing.Size(1052, 399);
            this.pnlGridCard.TabIndex = 0;
            this.pnlGridCard.Paint += new System.Windows.Forms.PaintEventHandler(this.CardBorder_Paint);
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
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
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
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTransactions.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTransactions.EnableHeadersVisualStyles = false;
            this.dataGridViewTransactions.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dataGridViewTransactions.Location = new System.Drawing.Point(12, 50);
            this.dataGridViewTransactions.MultiSelect = false;
            this.dataGridViewTransactions.Name = "dataGridViewTransactions";
            this.dataGridViewTransactions.ReadOnly = true;
            this.dataGridViewTransactions.RowHeadersVisible = false;
            this.dataGridViewTransactions.RowHeadersWidth = 30;
            this.dataGridViewTransactions.RowTemplate.Height = 44;
            this.dataGridViewTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTransactions.Size = new System.Drawing.Size(1028, 301);
            this.dataGridViewTransactions.TabIndex = 1;
            this.dataGridViewTransactions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTransactions_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Ngày giao dịch";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 140;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 140;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Nội dung";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 180;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Tài khoản";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 110;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 110;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Loại";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 75;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 75;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Số tiền";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 130;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 130;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Thao tác";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 100;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Trạng thái";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 110;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 110;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.labelPageInfo);
            this.panelFooter.Controls.Add(this.buttonPrevious);
            this.panelFooter.Controls.Add(this.labelPage);
            this.panelFooter.Controls.Add(this.buttonNext);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(12, 351);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1028, 38);
            this.panelFooter.TabIndex = 2;
            // 
            // labelPageInfo
            // 
            this.labelPageInfo.AutoSize = true;
            this.labelPageInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelPageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelPageInfo.Location = new System.Drawing.Point(4, 12);
            this.labelPageInfo.Name = "labelPageInfo";
            this.labelPageInfo.Size = new System.Drawing.Size(147, 15);
            this.labelPageInfo.TabIndex = 0;
            this.labelPageInfo.Text = "Hiển thị tất cả giao dịch";
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrevious.BackColor = System.Drawing.Color.White;
            this.buttonPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPrevious.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.buttonPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrevious.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.buttonPrevious.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.buttonPrevious.Location = new System.Drawing.Point(908, 4);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(36, 30);
            this.buttonPrevious.TabIndex = 1;
            this.buttonPrevious.Text = "‹";
            this.buttonPrevious.UseVisualStyleBackColor = false;
            // 
            // labelPage
            // 
            this.labelPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.labelPage.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.labelPage.ForeColor = System.Drawing.Color.White;
            this.labelPage.Location = new System.Drawing.Point(948, 4);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(34, 30);
            this.labelPage.TabIndex = 2;
            this.labelPage.Text = "1";
            this.labelPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.BackColor = System.Drawing.Color.White;
            this.buttonNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.buttonNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNext.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.buttonNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.buttonNext.Location = new System.Drawing.Point(986, 4);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(36, 30);
            this.buttonNext.TabIndex = 3;
            this.buttonNext.Text = "›";
            this.buttonNext.UseVisualStyleBackColor = false;
            // 
            // pnlTableTop
            // 
            this.pnlTableTop.BackColor = System.Drawing.Color.White;
            this.pnlTableTop.Controls.Add(this.lblTableHint);
            this.pnlTableTop.Controls.Add(this.labelTableTitle);
            this.pnlTableTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTableTop.Location = new System.Drawing.Point(12, 10);
            this.pnlTableTop.Name = "pnlTableTop";
            this.pnlTableTop.Size = new System.Drawing.Size(1028, 40);
            this.pnlTableTop.TabIndex = 0;
            // 
            // lblTableHint
            // 
            this.lblTableHint.AutoSize = true;
            this.lblTableHint.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTableHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblTableHint.Location = new System.Drawing.Point(164, 13);
            this.lblTableHint.Name = "lblTableHint";
            this.lblTableHint.Size = new System.Drawing.Size(202, 15);
            this.lblTableHint.TabIndex = 1;
            this.lblTableHint.Text = "• Tự động sắp xếp thời gian mới nhất";
            // 
            // labelTableTitle
            // 
            this.labelTableTitle.AutoSize = true;
            this.labelTableTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.labelTableTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.labelTableTitle.Location = new System.Drawing.Point(4, 8);
            this.labelTableTitle.Name = "labelTableTitle";
            this.labelTableTitle.Size = new System.Drawing.Size(157, 21);
            this.labelTableTitle.TabIndex = 0;
            this.labelTableTitle.Text = "Danh sách giao dịch";
            // 
            // TransactionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.panelTable);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelSummary);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TransactionForm";
            this.Text = "Giao dịch";
            this.Load += new System.EventHandler(this.TransactionForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelSummary.ResumeLayout(false);
            this.tblSummary.ResumeLayout(false);
            this.panelIncome.ResumeLayout(false);
            this.panelIncome.PerformLayout();
            this.panelExpense.ResumeLayout(false);
            this.panelExpense.PerformLayout();
            this.panelCount.ResumeLayout(false);
            this.panelCount.PerformLayout();
            this.panelFilter.ResumeLayout(false);
            this.pnlFilterCard.ResumeLayout(false);
            this.pnlFilterCard.PerformLayout();
            this.panelTable.ResumeLayout(false);
            this.pnlGridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.pnlTableTop.ResumeLayout(false);
            this.pnlTableTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeaderIcon;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelSubtitle;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.PictureBox pictureBox1;

        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.TableLayoutPanel tblSummary;
        private System.Windows.Forms.Panel panelIncome;
        private System.Windows.Forms.Label labelIncomeTitle;
        private System.Windows.Forms.Label labelIncome;
        private System.Windows.Forms.Label lblIncomeSub;
        private System.Windows.Forms.Label lblIncomeBadge;
        private System.Windows.Forms.Panel panelExpense;
        private System.Windows.Forms.Label labelExpenseTitle;
        private System.Windows.Forms.Label labelExpense;
        private System.Windows.Forms.Label lblExpenseSub;
        private System.Windows.Forms.Label lblExpenseBadge;
        private System.Windows.Forms.Panel panelCount;
        private System.Windows.Forms.Label labelCountTitle;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Label lblCountSub;
        private System.Windows.Forms.Label lblCountBadge;

        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.Panel pnlFilterCard;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.TextBox textSearch;
        private System.Windows.Forms.Label labelAccount;
        private System.Windows.Forms.ComboBox comboAccount;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox comboType;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonRefresh;

        private System.Windows.Forms.Panel panelTable;
        private System.Windows.Forms.Panel pnlGridCard;
        private System.Windows.Forms.Panel pnlTableTop;
        private System.Windows.Forms.Label labelTableTitle;
        private System.Windows.Forms.Label lblTableHint;
        private System.Windows.Forms.DataGridView dataGridViewTransactions;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Label labelPageInfo;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Label labelPage;
        private System.Windows.Forms.Button buttonNext;
    }
}