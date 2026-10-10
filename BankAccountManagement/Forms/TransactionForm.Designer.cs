using BankAccountManagement.Forms;   // để dùng RoundedPanel / RoundedButton

namespace BankAccountManagement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeaderIcon = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelFilter = new BankAccountManagement.Forms.RoundedPanel();
            this.buttonAdd = new BankAccountManagement.Forms.RoundedButton();
            this.buttonExport = new BankAccountManagement.Forms.RoundedButton();
            this.buttonRefresh = new BankAccountManagement.Forms.RoundedButton();
            this.buttonSearch = new BankAccountManagement.Forms.RoundedButton();
            this.comboType = new System.Windows.Forms.ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.comboAccount = new System.Windows.Forms.ComboBox();
            this.labelAccount = new System.Windows.Forms.Label();
            this.pnlSearchBox = new BankAccountManagement.Forms.RoundedPanel();
            this.textSearch = new System.Windows.Forms.TextBox();
            this.lblSearchIcon = new System.Windows.Forms.Label();
            this.labelSearch = new System.Windows.Forms.Label();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.panelCount = new BankAccountManagement.Forms.RoundedPanel();
            this.labelCount = new System.Windows.Forms.Label();
            this.labelCountTitle = new System.Windows.Forms.Label();
            this.panelExpense = new BankAccountManagement.Forms.RoundedPanel();
            this.labelExpense = new System.Windows.Forms.Label();
            this.labelExpenseTitle = new System.Windows.Forms.Label();
            this.panelIncome = new BankAccountManagement.Forms.RoundedPanel();
            this.labelIncome = new System.Windows.Forms.Label();
            this.labelIncomeTitle = new System.Windows.Forms.Label();
            this.panelTable = new BankAccountManagement.Forms.RoundedPanel();
            this.dataGridViewTransactions = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.buttonNext = new BankAccountManagement.Forms.RoundedButton();
            this.labelPage = new System.Windows.Forms.Label();
            this.buttonPrevious = new BankAccountManagement.Forms.RoundedButton();
            this.labelPageInfo = new System.Windows.Forms.Label();
            this.labelTableTitle = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelFilter.SuspendLayout();
            this.pnlSearchBox.SuspendLayout();
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
            this.panelHeader.Controls.Add(this.lblHeaderIcon);
            this.panelHeader.Controls.Add(this.pictureBox1);
            this.panelHeader.Controls.Add(this.labelSubtitle);
            this.panelHeader.Controls.Add(this.labelTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1280, 88);
            this.panelHeader.TabIndex = 0;
            // 
            // lblHeaderIcon
            // 
            this.lblHeaderIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblHeaderIcon.Font = new System.Drawing.Font("Segoe MDL2 Assets", 20F);
            this.lblHeaderIcon.ForeColor = System.Drawing.Color.White;
            this.lblHeaderIcon.Location = new System.Drawing.Point(24, 22);
            this.lblHeaderIcon.Name = "lblHeaderIcon";
            this.lblHeaderIcon.Size = new System.Drawing.Size(44, 44);
            this.lblHeaderIcon.TabIndex = 0;
            this.lblHeaderIcon.Text = "";
            this.lblHeaderIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(-100, -100);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1, 1);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // labelSubtitle
            // 
            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelSubtitle.Location = new System.Drawing.Point(80, 50);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(258, 15);
            this.labelSubtitle.TabIndex = 2;
            this.labelSubtitle.Text = "Theo dõi và quản lý các giao dịch của tài khoản";
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 15.5F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.labelTitle.Location = new System.Drawing.Point(78, 16);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(106, 30);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Giao dịch";
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.White;
            this.panelFilter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.panelFilter.BorderRadius = 14;
            this.panelFilter.BorderSize = 1;
            this.panelFilter.Controls.Add(this.buttonAdd);
            this.panelFilter.Controls.Add(this.buttonExport);
            this.panelFilter.Controls.Add(this.buttonRefresh);
            this.panelFilter.Controls.Add(this.buttonSearch);
            this.panelFilter.Controls.Add(this.comboType);
            this.panelFilter.Controls.Add(this.labelType);
            this.panelFilter.Controls.Add(this.comboAccount);
            this.panelFilter.Controls.Add(this.labelAccount);
            this.panelFilter.Controls.Add(this.pnlSearchBox);
            this.panelFilter.Controls.Add(this.labelSearch);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.FillColor = System.Drawing.Color.White;
            this.panelFilter.Location = new System.Drawing.Point(0, 88);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1280, 132);
            this.panelFilter.TabIndex = 1;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.BackColor = System.Drawing.Color.Transparent;
            this.buttonAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.HoverBorder = System.Drawing.Color.Transparent;
            this.buttonAdd.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.buttonAdd.Location = new System.Drawing.Point(1094, 84);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.NormalBorder = System.Drawing.Color.Transparent;
            this.buttonAdd.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.buttonAdd.PressedBorder = System.Drawing.Color.Transparent;
            this.buttonAdd.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(120)))), ((int)(((byte)(87)))));
            this.buttonAdd.Size = new System.Drawing.Size(162, 34);
            this.buttonAdd.TabIndex = 9;
            this.buttonAdd.Text = "+  Thêm giao dịch";
            this.buttonAdd.TextColor = System.Drawing.Color.White;
            this.buttonAdd.UseVisualStyleBackColor = false;
            // 
            // buttonExport
            // 
            this.buttonExport.BackColor = System.Drawing.Color.Transparent;
            this.buttonExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExport.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.buttonExport.HoverBorder = System.Drawing.Color.Transparent;
            this.buttonExport.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.buttonExport.Location = new System.Drawing.Point(240, 84);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.NormalBorder = System.Drawing.Color.Transparent;
            this.buttonExport.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.buttonExport.PressedBorder = System.Drawing.Color.Transparent;
            this.buttonExport.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.buttonExport.Size = new System.Drawing.Size(120, 34);
            this.buttonExport.TabIndex = 8;
            this.buttonExport.Text = "Xuất Excel";
            this.buttonExport.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.buttonExport.UseVisualStyleBackColor = false;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.Color.Transparent;
            this.buttonRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.buttonRefresh.HoverBorder = System.Drawing.Color.Transparent;
            this.buttonRefresh.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.buttonRefresh.Location = new System.Drawing.Point(132, 84);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.NormalBorder = System.Drawing.Color.Transparent;
            this.buttonRefresh.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.buttonRefresh.PressedBorder = System.Drawing.Color.Transparent;
            this.buttonRefresh.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.buttonRefresh.Size = new System.Drawing.Size(100, 34);
            this.buttonRefresh.TabIndex = 7;
            this.buttonRefresh.Text = "Làm mới";
            this.buttonRefresh.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.buttonRefresh.UseVisualStyleBackColor = false;
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.Color.Transparent;
            this.buttonSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonSearch.ForeColor = System.Drawing.Color.White;
            this.buttonSearch.HoverBorder = System.Drawing.Color.Transparent;
            this.buttonSearch.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.buttonSearch.Location = new System.Drawing.Point(24, 84);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.NormalBorder = System.Drawing.Color.Transparent;
            this.buttonSearch.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.buttonSearch.PressedBorder = System.Drawing.Color.Transparent;
            this.buttonSearch.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.buttonSearch.Size = new System.Drawing.Size(100, 34);
            this.buttonSearch.TabIndex = 6;
            this.buttonSearch.Text = "Tìm kiếm";
            this.buttonSearch.TextColor = System.Drawing.Color.White;
            this.buttonSearch.UseVisualStyleBackColor = false;
            // 
            // comboType
            // 
            this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboType.FormattingEnabled = true;
            this.comboType.Items.AddRange(new object[] {
            "Tất cả",
            "Thu",
            "Chi"});
            this.comboType.Location = new System.Drawing.Point(590, 44);
            this.comboType.Name = "comboType";
            this.comboType.Size = new System.Drawing.Size(150, 25);
            this.comboType.TabIndex = 5;
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.labelType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelType.Location = new System.Drawing.Point(590, 16);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(99, 15);
            this.labelType.TabIndex = 4;
            this.labelType.Text = "LOẠI GIAO DỊCH";
            // 
            // comboAccount
            // 
            this.comboAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboAccount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboAccount.FormattingEnabled = true;
            this.comboAccount.Items.AddRange(new object[] {
            "Tất cả tài khoản",
            "Vietcombank",
            "Techcombank",
            "BIDV"});
            this.comboAccount.Location = new System.Drawing.Point(400, 44);
            this.comboAccount.Name = "comboAccount";
            this.comboAccount.Size = new System.Drawing.Size(170, 25);
            this.comboAccount.TabIndex = 3;
            // 
            // labelAccount
            // 
            this.labelAccount.AutoSize = true;
            this.labelAccount.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.labelAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelAccount.Location = new System.Drawing.Point(400, 16);
            this.labelAccount.Name = "labelAccount";
            this.labelAccount.Size = new System.Drawing.Size(71, 15);
            this.labelAccount.TabIndex = 2;
            this.labelAccount.Text = "TÀI KHOẢN";
            // 
            // pnlSearchBox
            // 
            this.pnlSearchBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlSearchBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.pnlSearchBox.BorderRadius = 8;
            this.pnlSearchBox.BorderSize = 1;
            this.pnlSearchBox.Controls.Add(this.textSearch);
            this.pnlSearchBox.Controls.Add(this.lblSearchIcon);
            this.pnlSearchBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlSearchBox.Location = new System.Drawing.Point(24, 38);
            this.pnlSearchBox.Name = "pnlSearchBox";
            this.pnlSearchBox.Size = new System.Drawing.Size(360, 38);
            this.pnlSearchBox.TabIndex = 1;
            // 
            // textSearch
            // 
            this.textSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.textSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.textSearch.Location = new System.Drawing.Point(40, 11);
            this.textSearch.Name = "textSearch";
            this.textSearch.Size = new System.Drawing.Size(310, 18);
            this.textSearch.TabIndex = 1;
            this.textSearch.Text = "Nội dung, tài khoản, mã giao dịch...";
            // 
            // lblSearchIcon
            // 
            this.lblSearchIcon.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchIcon.Font = new System.Drawing.Font("Segoe MDL2 Assets", 11F);
            this.lblSearchIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSearchIcon.Location = new System.Drawing.Point(10, 8);
            this.lblSearchIcon.Name = "lblSearchIcon";
            this.lblSearchIcon.Size = new System.Drawing.Size(22, 22);
            this.lblSearchIcon.TabIndex = 0;
            this.lblSearchIcon.Text = "";
            this.lblSearchIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.labelSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelSearch.Location = new System.Drawing.Point(24, 16);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(61, 15);
            this.labelSearch.TabIndex = 0;
            this.labelSearch.Text = "TÌM KIẾM";
            // 
            // panelSummary
            // 
            this.panelSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.panelSummary.Controls.Add(this.panelCount);
            this.panelSummary.Controls.Add(this.panelExpense);
            this.panelSummary.Controls.Add(this.panelIncome);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSummary.Location = new System.Drawing.Point(0, 220);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Padding = new System.Windows.Forms.Padding(4, 16, 4, 16);
            this.panelSummary.Size = new System.Drawing.Size(1280, 110);
            this.panelSummary.TabIndex = 2;
            // 
            // panelCount
            // 
            this.panelCount.BackColor = System.Drawing.Color.White;
            this.panelCount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.panelCount.BorderRadius = 14;
            this.panelCount.BorderSize = 1;
            this.panelCount.Controls.Add(this.labelCount);
            this.panelCount.Controls.Add(this.labelCountTitle);
            this.panelCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelCount.FillColor = System.Drawing.Color.White;
            this.panelCount.Location = new System.Drawing.Point(804, 16);
            this.panelCount.Name = "panelCount";
            this.panelCount.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.panelCount.Size = new System.Drawing.Size(400, 78);
            this.panelCount.TabIndex = 2;
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.labelCount.Location = new System.Drawing.Point(30, 36);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(28, 32);
            this.labelCount.TabIndex = 1;
            this.labelCount.Text = "8";
            // 
            // labelCountTitle
            // 
            this.labelCountTitle.AutoSize = true;
            this.labelCountTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelCountTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelCountTitle.Location = new System.Drawing.Point(32, 14);
            this.labelCountTitle.Name = "labelCountTitle";
            this.labelCountTitle.Size = new System.Drawing.Size(88, 15);
            this.labelCountTitle.TabIndex = 0;
            this.labelCountTitle.Text = "SỐ GIAO DỊCH";
            // 
            // panelExpense
            // 
            this.panelExpense.BackColor = System.Drawing.Color.White;
            this.panelExpense.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.panelExpense.BorderRadius = 14;
            this.panelExpense.BorderSize = 1;
            this.panelExpense.Controls.Add(this.labelExpense);
            this.panelExpense.Controls.Add(this.labelExpenseTitle);
            this.panelExpense.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelExpense.FillColor = System.Drawing.Color.White;
            this.panelExpense.Location = new System.Drawing.Point(404, 16);
            this.panelExpense.Name = "panelExpense";
            this.panelExpense.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.panelExpense.Size = new System.Drawing.Size(400, 78);
            this.panelExpense.TabIndex = 1;
            // 
            // labelExpense
            // 
            this.labelExpense.AutoSize = true;
            this.labelExpense.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelExpense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.labelExpense.Location = new System.Drawing.Point(30, 36);
            this.labelExpense.Name = "labelExpense";
            this.labelExpense.Size = new System.Drawing.Size(166, 32);
            this.labelExpense.TabIndex = 1;
            this.labelExpense.Text = "- 2,050,000 ₫";
            // 
            // labelExpenseTitle
            // 
            this.labelExpenseTitle.AutoSize = true;
            this.labelExpenseTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelExpenseTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelExpenseTitle.Location = new System.Drawing.Point(32, 14);
            this.labelExpenseTitle.Name = "labelExpenseTitle";
            this.labelExpenseTitle.Size = new System.Drawing.Size(64, 15);
            this.labelExpenseTitle.TabIndex = 0;
            this.labelExpenseTitle.Text = "TỔNG CHI";
            // 
            // panelIncome
            // 
            this.panelIncome.BackColor = System.Drawing.Color.White;
            this.panelIncome.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(250)))), ((int)(((byte)(229)))));
            this.panelIncome.BorderRadius = 14;
            this.panelIncome.BorderSize = 1;
            this.panelIncome.Controls.Add(this.labelIncome);
            this.panelIncome.Controls.Add(this.labelIncomeTitle);
            this.panelIncome.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelIncome.FillColor = System.Drawing.Color.White;
            this.panelIncome.Location = new System.Drawing.Point(4, 16);
            this.panelIncome.Name = "panelIncome";
            this.panelIncome.Size = new System.Drawing.Size(400, 78);
            this.panelIncome.TabIndex = 0;
            // 
            // labelIncome
            // 
            this.labelIncome.AutoSize = true;
            this.labelIncome.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelIncome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.labelIncome.Location = new System.Drawing.Point(22, 36);
            this.labelIncome.Name = "labelIncome";
            this.labelIncome.Size = new System.Drawing.Size(173, 32);
            this.labelIncome.TabIndex = 1;
            this.labelIncome.Text = "+ 4,700,000 ₫";
            // 
            // labelIncomeTitle
            // 
            this.labelIncomeTitle.AutoSize = true;
            this.labelIncomeTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelIncomeTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelIncomeTitle.Location = new System.Drawing.Point(24, 14);
            this.labelIncomeTitle.Name = "labelIncomeTitle";
            this.labelIncomeTitle.Size = new System.Drawing.Size(69, 15);
            this.labelIncomeTitle.TabIndex = 0;
            this.labelIncomeTitle.Text = "TỔNG THU";
            // 
            // panelTable
            // 
            this.panelTable.BackColor = System.Drawing.Color.White;
            this.panelTable.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.panelTable.BorderRadius = 14;
            this.panelTable.BorderSize = 1;
            this.panelTable.Controls.Add(this.dataGridViewTransactions);
            this.panelTable.Controls.Add(this.panelFooter);
            this.panelTable.Controls.Add(this.labelTableTitle);
            this.panelTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTable.FillColor = System.Drawing.Color.White;
            this.panelTable.Location = new System.Drawing.Point(0, 330);
            this.panelTable.Name = "panelTable";
            this.panelTable.Padding = new System.Windows.Forms.Padding(2);
            this.panelTable.Size = new System.Drawing.Size(1280, 416);
            this.panelTable.TabIndex = 3;
            // 
            // dataGridViewTransactions
            // 
            this.dataGridViewTransactions.AllowUserToAddRows = false;
            this.dataGridViewTransactions.AllowUserToDeleteRows = false;
            this.dataGridViewTransactions.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(253)))), ((int)(((byte)(255)))));
            this.dataGridViewTransactions.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTransactions.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewTransactions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewTransactions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewTransactions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTransactions.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTransactions.ColumnHeadersHeight = 46;
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
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTransactions.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTransactions.EnableHeadersVisualStyles = false;
            this.dataGridViewTransactions.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dataGridViewTransactions.Location = new System.Drawing.Point(2, 2);
            this.dataGridViewTransactions.MultiSelect = false;
            this.dataGridViewTransactions.Name = "dataGridViewTransactions";
            this.dataGridViewTransactions.ReadOnly = true;
            this.dataGridViewTransactions.RowHeadersVisible = false;
            this.dataGridViewTransactions.RowHeadersWidth = 62;
            this.dataGridViewTransactions.RowTemplate.Height = 46;
            this.dataGridViewTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTransactions.Size = new System.Drawing.Size(1276, 358);
            this.dataGridViewTransactions.TabIndex = 1;
            this.dataGridViewTransactions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTransactions_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "NGÀY GIAO DỊCH";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "NỘI DUNG";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "TÀI KHOẢN";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "LOẠI";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "SỐ TIỀN";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "THAO TÁC";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "TRẠNG THÁI";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.buttonNext);
            this.panelFooter.Controls.Add(this.labelPage);
            this.panelFooter.Controls.Add(this.buttonPrevious);
            this.panelFooter.Controls.Add(this.labelPageInfo);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(2, 360);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1276, 54);
            this.panelFooter.TabIndex = 2;
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.BackColor = System.Drawing.Color.Transparent;
            this.buttonNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNext.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.buttonNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.buttonNext.HoverBorder = System.Drawing.Color.Transparent;
            this.buttonNext.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.buttonNext.Location = new System.Drawing.Point(1182, 10);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.NormalBorder = System.Drawing.Color.Transparent;
            this.buttonNext.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.buttonNext.PressedBorder = System.Drawing.Color.Transparent;
            this.buttonNext.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.buttonNext.Size = new System.Drawing.Size(40, 34);
            this.buttonNext.TabIndex = 3;
            this.buttonNext.Text = "›";
            this.buttonNext.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.buttonNext.UseVisualStyleBackColor = false;
            // 
            // labelPage
            // 
            this.labelPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.labelPage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelPage.ForeColor = System.Drawing.Color.White;
            this.labelPage.Location = new System.Drawing.Point(1134, 10);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(40, 34);
            this.labelPage.TabIndex = 2;
            this.labelPage.Text = "1";
            this.labelPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrevious.BackColor = System.Drawing.Color.Transparent;
            this.buttonPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrevious.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.buttonPrevious.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.buttonPrevious.HoverBorder = System.Drawing.Color.Transparent;
            this.buttonPrevious.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.buttonPrevious.Location = new System.Drawing.Point(1086, 10);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.NormalBorder = System.Drawing.Color.Transparent;
            this.buttonPrevious.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.buttonPrevious.PressedBorder = System.Drawing.Color.Transparent;
            this.buttonPrevious.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.buttonPrevious.Size = new System.Drawing.Size(40, 34);
            this.buttonPrevious.TabIndex = 1;
            this.buttonPrevious.Text = "‹";
            this.buttonPrevious.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.buttonPrevious.UseVisualStyleBackColor = false;
            // 
            // labelPageInfo
            // 
            this.labelPageInfo.AutoSize = true;
            this.labelPageInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelPageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.labelPageInfo.Location = new System.Drawing.Point(18, 20);
            this.labelPageInfo.Name = "labelPageInfo";
            this.labelPageInfo.Size = new System.Drawing.Size(150, 15);
            this.labelPageInfo.TabIndex = 0;
            this.labelPageInfo.Text = "Hiển thị 1 - 8 / 24 giao dịch";
            // 
            // labelTableTitle
            // 
            this.labelTableTitle.AutoSize = true;
            this.labelTableTitle.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.labelTableTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.labelTableTitle.Location = new System.Drawing.Point(20, 18);
            this.labelTableTitle.Name = "labelTableTitle";
            this.labelTableTitle.Size = new System.Drawing.Size(170, 23);
            this.labelTableTitle.TabIndex = 0;
            this.labelTableTitle.Text = "Danh sách giao dịch";
            // 
            // TransactionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1280, 746);
            this.Controls.Add(this.panelTable);
            this.Controls.Add(this.panelSummary);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(1000, 640);
            this.Name = "TransactionForm";
            this.Text = "Giao dịch";
            this.Load += new System.EventHandler(this.TransactionForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            this.pnlSearchBox.ResumeLayout(false);
            this.pnlSearchBox.PerformLayout();
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

        // --------- Fields ---------
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelSubtitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblHeaderIcon;

        private BankAccountManagement.Forms.RoundedPanel panelFilter;
        private System.Windows.Forms.Label labelSearch;
        private BankAccountManagement.Forms.RoundedPanel pnlSearchBox;
        private System.Windows.Forms.Label lblSearchIcon;
        private System.Windows.Forms.TextBox textSearch;
        private System.Windows.Forms.Label labelAccount;
        private System.Windows.Forms.ComboBox comboAccount;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox comboType;
        private BankAccountManagement.Forms.RoundedButton buttonSearch;
        private BankAccountManagement.Forms.RoundedButton buttonRefresh;
        private BankAccountManagement.Forms.RoundedButton buttonExport;
        private BankAccountManagement.Forms.RoundedButton buttonAdd;

        private System.Windows.Forms.Panel panelSummary;
        private BankAccountManagement.Forms.RoundedPanel panelIncome;
        private System.Windows.Forms.Label labelIncomeTitle;
        private System.Windows.Forms.Label labelIncome;
        private BankAccountManagement.Forms.RoundedPanel panelExpense;
        private System.Windows.Forms.Label labelExpenseTitle;
        private System.Windows.Forms.Label labelExpense;
        private BankAccountManagement.Forms.RoundedPanel panelCount;
        private System.Windows.Forms.Label labelCountTitle;
        private System.Windows.Forms.Label labelCount;

        private BankAccountManagement.Forms.RoundedPanel panelTable;
        private System.Windows.Forms.Label labelTableTitle;
        private System.Windows.Forms.DataGridView dataGridViewTransactions;

        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Label labelPageInfo;
        private BankAccountManagement.Forms.RoundedButton buttonPrevious;
        private System.Windows.Forms.Label labelPage;
        private BankAccountManagement.Forms.RoundedButton buttonNext;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    }
}