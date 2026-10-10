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
            System.Windows.Forms.DataGridViewCellStyle dgvHeaderStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvRowStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dgvAltRowStyle = new System.Windows.Forms.DataGridViewCellStyle();

            // ---------- HEADER ----------
            this.panelHeader = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblHeaderIcon = new System.Windows.Forms.Label();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();

            // ---------- FILTER ----------
            this.panelFilter = new BankAccountManagement.Forms.RoundedPanel();
            this.pnlSearchBox = new BankAccountManagement.Forms.RoundedPanel();
            this.textSearch = new System.Windows.Forms.TextBox();
            this.lblSearchIcon = new System.Windows.Forms.Label();
            this.labelSearch = new System.Windows.Forms.Label();
            this.labelAccount = new System.Windows.Forms.Label();
            this.comboAccount = new System.Windows.Forms.ComboBox();
            this.labelType = new System.Windows.Forms.Label();
            this.comboType = new System.Windows.Forms.ComboBox();
            this.buttonSearch = new BankAccountManagement.Forms.RoundedButton();
            this.buttonRefresh = new BankAccountManagement.Forms.RoundedButton();
            this.buttonExport = new BankAccountManagement.Forms.RoundedButton();
            this.buttonAdd = new BankAccountManagement.Forms.RoundedButton();

            // ---------- SUMMARY ----------
            this.panelSummary = new System.Windows.Forms.Panel();
            this.panelIncome = new BankAccountManagement.Forms.RoundedPanel();
            this.labelIncomeTitle = new System.Windows.Forms.Label();
            this.labelIncome = new System.Windows.Forms.Label();
            this.panelExpense = new BankAccountManagement.Forms.RoundedPanel();
            this.labelExpenseTitle = new System.Windows.Forms.Label();
            this.labelExpense = new System.Windows.Forms.Label();
            this.panelCount = new BankAccountManagement.Forms.RoundedPanel();
            this.labelCountTitle = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();

            // ---------- TABLE ----------
            this.panelTable = new BankAccountManagement.Forms.RoundedPanel();
            this.labelTableTitle = new System.Windows.Forms.Label();
            this.dataGridViewTransactions = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // ---------- FOOTER ----------
            this.panelFooter = new System.Windows.Forms.Panel();
            this.labelPageInfo = new System.Windows.Forms.Label();
            this.buttonPrevious = new BankAccountManagement.Forms.RoundedButton();
            this.labelPage = new System.Windows.Forms.Label();
            this.buttonNext = new BankAccountManagement.Forms.RoundedButton();

            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelFilter.SuspendLayout();
            this.pnlSearchBox.SuspendLayout();
            this.panelSummary.SuspendLayout();
            this.panelIncome.SuspendLayout();
            this.panelExpense.SuspendLayout();
            this.panelCount.SuspendLayout();
            this.panelTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTransactions)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();

            // =========================================================
            // HEADER
            // =========================================================
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

            // pictureBox1 — ẩn (giữ để code-behind không lỗi)
            this.pictureBox1.Location = new System.Drawing.Point(-100, -100);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1, 1);
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;

            // Icon đầu trang
            this.lblHeaderIcon.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblHeaderIcon.Font = new System.Drawing.Font("Segoe MDL2 Assets", 20F);
            this.lblHeaderIcon.ForeColor = System.Drawing.Color.White;
            this.lblHeaderIcon.Location = new System.Drawing.Point(24, 22);
            this.lblHeaderIcon.Name = "lblHeaderIcon";
            this.lblHeaderIcon.Size = new System.Drawing.Size(44, 44);
            this.lblHeaderIcon.TabIndex = 0;
            this.lblHeaderIcon.Text = "\uE8AB";
            this.lblHeaderIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 15.5F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.labelTitle.Location = new System.Drawing.Point(78, 16);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(110, 30);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Giao dịch";

            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.labelSubtitle.Location = new System.Drawing.Point(80, 50);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(285, 15);
            this.labelSubtitle.TabIndex = 2;
            this.labelSubtitle.Text = "Theo dõi và quản lý các giao dịch của tài khoản";

            // =========================================================
            // FILTER (RoundedPanel)
            // =========================================================
            this.panelFilter.BackColor = System.Drawing.Color.Transparent;
            this.panelFilter.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.panelFilter.BorderRadius = 14;
            this.panelFilter.BorderSize = 1;
            this.panelFilter.FillColor = System.Drawing.Color.White;
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
            this.panelFilter.Location = new System.Drawing.Point(20, 88);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1240, 132);
            this.panelFilter.TabIndex = 1;

            // labelSearch
            this.labelSearch.AutoSize = true;
            this.labelSearch.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.labelSearch.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.labelSearch.Location = new System.Drawing.Point(24, 16);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(56, 15);
            this.labelSearch.TabIndex = 0;
            this.labelSearch.Text = "TÌM KIẾM";

            // pnlSearchBox
            this.pnlSearchBox.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearchBox.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.pnlSearchBox.BorderRadius = 8;
            this.pnlSearchBox.BorderSize = 1;
            this.pnlSearchBox.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.pnlSearchBox.Controls.Add(this.textSearch);
            this.pnlSearchBox.Controls.Add(this.lblSearchIcon);
            this.pnlSearchBox.Location = new System.Drawing.Point(24, 38);
            this.pnlSearchBox.Name = "pnlSearchBox";
            this.pnlSearchBox.Size = new System.Drawing.Size(360, 38);
            this.pnlSearchBox.TabIndex = 1;

            this.lblSearchIcon.BackColor = System.Drawing.Color.Transparent;
            this.lblSearchIcon.Font = new System.Drawing.Font("Segoe MDL2 Assets", 11F);
            this.lblSearchIcon.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblSearchIcon.Location = new System.Drawing.Point(10, 8);
            this.lblSearchIcon.Name = "lblSearchIcon";
            this.lblSearchIcon.Size = new System.Drawing.Size(22, 22);
            this.lblSearchIcon.TabIndex = 0;
            this.lblSearchIcon.Text = "\uE721";
            this.lblSearchIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.textSearch.Anchor = System.Windows.Forms.AnchorStyles.Top
                                   | System.Windows.Forms.AnchorStyles.Left
                                   | System.Windows.Forms.AnchorStyles.Right;
            this.textSearch.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.textSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textSearch.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.textSearch.Location = new System.Drawing.Point(40, 11);
            this.textSearch.Name = "textSearch";
            this.textSearch.Size = new System.Drawing.Size(310, 18);
            this.textSearch.TabIndex = 1;
            this.textSearch.Text = "Nội dung, tài khoản, mã giao dịch...";

            // labelAccount
            this.labelAccount.AutoSize = true;
            this.labelAccount.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.labelAccount.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.labelAccount.Location = new System.Drawing.Point(400, 16);
            this.labelAccount.Name = "labelAccount";
            this.labelAccount.Size = new System.Drawing.Size(64, 15);
            this.labelAccount.TabIndex = 2;
            this.labelAccount.Text = "TÀI KHOẢN";

            // comboAccount
            this.comboAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboAccount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboAccount.FormattingEnabled = true;
            this.comboAccount.Items.AddRange(new object[] {
                "Tất cả tài khoản", "Vietcombank", "Techcombank", "BIDV"});
            this.comboAccount.Location = new System.Drawing.Point(400, 44);
            this.comboAccount.Name = "comboAccount";
            this.comboAccount.Size = new System.Drawing.Size(170, 25);
            this.comboAccount.TabIndex = 3;

            // labelType
            this.labelType.AutoSize = true;
            this.labelType.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.labelType.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.labelType.Location = new System.Drawing.Point(590, 16);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(96, 15);
            this.labelType.TabIndex = 4;
            this.labelType.Text = "LOẠI GIAO DỊCH";

            // comboType
            this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboType.FormattingEnabled = true;
            this.comboType.Items.AddRange(new object[] { "Tất cả", "Thu", "Chi" });
            this.comboType.Location = new System.Drawing.Point(590, 44);
            this.comboType.Name = "comboType";
            this.comboType.Size = new System.Drawing.Size(150, 25);
            this.comboType.TabIndex = 5;

            // --- buttonSearch ---
            this.buttonSearch.BackColor = System.Drawing.Color.Transparent;
            this.buttonSearch.BorderRadius = 8;
            this.buttonSearch.BorderSize = 0;
            this.buttonSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonSearch.ForeColor = System.Drawing.Color.White;
            this.buttonSearch.HoverBorder = System.Drawing.Color.Transparent;
            this.buttonSearch.HoverColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.buttonSearch.Location = new System.Drawing.Point(24, 84);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.NormalBorder = System.Drawing.Color.Transparent;
            this.buttonSearch.NormalColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.buttonSearch.PressedBorder = System.Drawing.Color.Transparent;
            this.buttonSearch.PressedColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.buttonSearch.Size = new System.Drawing.Size(100, 34);
            this.buttonSearch.TabIndex = 6;
            this.buttonSearch.Text = "Tìm kiếm";
            this.buttonSearch.TextColor = System.Drawing.Color.White;
            this.buttonSearch.UseVisualStyleBackColor = false;

            // --- buttonRefresh ---
            this.buttonRefresh.BackColor = System.Drawing.Color.Transparent;
            this.buttonRefresh.BorderRadius = 8;
            this.buttonRefresh.BorderSize = 0;
            this.buttonRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonRefresh.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.buttonRefresh.HoverBorder = System.Drawing.Color.Transparent;
            this.buttonRefresh.HoverColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.buttonRefresh.Location = new System.Drawing.Point(132, 84);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.NormalBorder = System.Drawing.Color.Transparent;
            this.buttonRefresh.NormalColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.buttonRefresh.PressedBorder = System.Drawing.Color.Transparent;
            this.buttonRefresh.PressedColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.buttonRefresh.Size = new System.Drawing.Size(100, 34);
            this.buttonRefresh.TabIndex = 7;
            this.buttonRefresh.Text = "Làm mới";
            this.buttonRefresh.TextColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.buttonRefresh.UseVisualStyleBackColor = false;

            // --- buttonExport ---
            this.buttonExport.BackColor = System.Drawing.Color.Transparent;
            this.buttonExport.BorderRadius = 8;
            this.buttonExport.BorderSize = 0;
            this.buttonExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExport.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonExport.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.buttonExport.HoverBorder = System.Drawing.Color.Transparent;
            this.buttonExport.HoverColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.buttonExport.Location = new System.Drawing.Point(240, 84);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.NormalBorder = System.Drawing.Color.Transparent;
            this.buttonExport.NormalColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.buttonExport.PressedBorder = System.Drawing.Color.Transparent;
            this.buttonExport.PressedColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.buttonExport.Size = new System.Drawing.Size(120, 34);
            this.buttonExport.TabIndex = 8;
            this.buttonExport.Text = "Xuất Excel";
            this.buttonExport.TextColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.buttonExport.UseVisualStyleBackColor = false;

            // --- buttonAdd (nổi bật, xanh lá) ---
            this.buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.buttonAdd.BackColor = System.Drawing.Color.Transparent;
            this.buttonAdd.BorderRadius = 8;
            this.buttonAdd.BorderSize = 0;
            this.buttonAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.HoverBorder = System.Drawing.Color.Transparent;
            this.buttonAdd.HoverColor = System.Drawing.Color.FromArgb(5, 150, 105);
            this.buttonAdd.Location = new System.Drawing.Point(1054, 84);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.NormalBorder = System.Drawing.Color.Transparent;
            this.buttonAdd.NormalColor = System.Drawing.Color.FromArgb(16, 185, 129);
            this.buttonAdd.PressedBorder = System.Drawing.Color.Transparent;
            this.buttonAdd.PressedColor = System.Drawing.Color.FromArgb(4, 120, 87);
            this.buttonAdd.Size = new System.Drawing.Size(162, 34);
            this.buttonAdd.TabIndex = 9;
            this.buttonAdd.Text = "+  Thêm giao dịch";
            this.buttonAdd.TextColor = System.Drawing.Color.White;
            this.buttonAdd.UseVisualStyleBackColor = false;

            // =========================================================
            // SUMMARY (3 stat cards)
            // =========================================================
            this.panelSummary.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.panelSummary.Controls.Add(this.panelCount);
            this.panelSummary.Controls.Add(this.panelExpense);
            this.panelSummary.Controls.Add(this.panelIncome);
            this.panelSummary.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSummary.Location = new System.Drawing.Point(20, 220);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Padding = new System.Windows.Forms.Padding(4, 16, 4, 16);
            this.panelSummary.Size = new System.Drawing.Size(1240, 110);
            this.panelSummary.TabIndex = 2;

            // --- panelIncome (xanh lá) ---
            this.panelIncome.BackColor = System.Drawing.Color.Transparent;
            this.panelIncome.BorderColor = System.Drawing.Color.FromArgb(209, 250, 229);
            this.panelIncome.BorderRadius = 14;
            this.panelIncome.BorderSize = 1;
            this.panelIncome.FillColor = System.Drawing.Color.White;
            this.panelIncome.Controls.Add(this.labelIncome);
            this.panelIncome.Controls.Add(this.labelIncomeTitle);
            this.panelIncome.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelIncome.Location = new System.Drawing.Point(4, 16);
            this.panelIncome.Name = "panelIncome";
            this.panelIncome.Size = new System.Drawing.Size(400, 78);
            this.panelIncome.TabIndex = 0;

            this.labelIncomeTitle.AutoSize = true;
            this.labelIncomeTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelIncomeTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.labelIncomeTitle.Location = new System.Drawing.Point(24, 14);
            this.labelIncomeTitle.Name = "labelIncomeTitle";
            this.labelIncomeTitle.Size = new System.Drawing.Size(62, 15);
            this.labelIncomeTitle.TabIndex = 0;
            this.labelIncomeTitle.Text = "TỔNG THU";

            this.labelIncome.AutoSize = true;
            this.labelIncome.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelIncome.ForeColor = System.Drawing.Color.FromArgb(5, 150, 105);
            this.labelIncome.Location = new System.Drawing.Point(22, 36);
            this.labelIncome.Name = "labelIncome";
            this.labelIncome.Size = new System.Drawing.Size(178, 32);
            this.labelIncome.TabIndex = 1;
            this.labelIncome.Text = "+ 4,700,000 ₫";

            // --- panelExpense (đỏ) ---
            this.panelExpense.BackColor = System.Drawing.Color.Transparent;
            this.panelExpense.BorderColor = System.Drawing.Color.FromArgb(254, 226, 226);
            this.panelExpense.BorderRadius = 14;
            this.panelExpense.BorderSize = 1;
            this.panelExpense.FillColor = System.Drawing.Color.White;
            this.panelExpense.Controls.Add(this.labelExpense);
            this.panelExpense.Controls.Add(this.labelExpenseTitle);
            this.panelExpense.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelExpense.Location = new System.Drawing.Point(404, 16);
            this.panelExpense.Name = "panelExpense";
            this.panelExpense.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.panelExpense.Size = new System.Drawing.Size(400, 78);
            this.panelExpense.TabIndex = 1;

            this.labelExpenseTitle.AutoSize = true;
            this.labelExpenseTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelExpenseTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.labelExpenseTitle.Location = new System.Drawing.Point(32, 14);
            this.labelExpenseTitle.Name = "labelExpenseTitle";
            this.labelExpenseTitle.Size = new System.Drawing.Size(58, 15);
            this.labelExpenseTitle.TabIndex = 0;
            this.labelExpenseTitle.Text = "TỔNG CHI";

            this.labelExpense.AutoSize = true;
            this.labelExpense.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelExpense.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.labelExpense.Location = new System.Drawing.Point(30, 36);
            this.labelExpense.Name = "labelExpense";
            this.labelExpense.Size = new System.Drawing.Size(182, 32);
            this.labelExpense.TabIndex = 1;
            this.labelExpense.Text = "- 2,050,000 ₫";

            // --- panelCount (xanh dương) ---
            this.panelCount.BackColor = System.Drawing.Color.Transparent;
            this.panelCount.BorderColor = System.Drawing.Color.FromArgb(219, 234, 254);
            this.panelCount.BorderRadius = 14;
            this.panelCount.BorderSize = 1;
            this.panelCount.FillColor = System.Drawing.Color.White;
            this.panelCount.Controls.Add(this.labelCount);
            this.panelCount.Controls.Add(this.labelCountTitle);
            this.panelCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelCount.Location = new System.Drawing.Point(804, 16);
            this.panelCount.Name = "panelCount";
            this.panelCount.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.panelCount.Size = new System.Drawing.Size(400, 78);
            this.panelCount.TabIndex = 2;

            this.labelCountTitle.AutoSize = true;
            this.labelCountTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.labelCountTitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.labelCountTitle.Location = new System.Drawing.Point(32, 14);
            this.labelCountTitle.Name = "labelCountTitle";
            this.labelCountTitle.Size = new System.Drawing.Size(78, 15);
            this.labelCountTitle.TabIndex = 0;
            this.labelCountTitle.Text = "SỐ GIAO DỊCH";

            this.labelCount.AutoSize = true;
            this.labelCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.labelCount.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.labelCount.Location = new System.Drawing.Point(30, 36);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(28, 32);
            this.labelCount.TabIndex = 1;
            this.labelCount.Text = "8";

            // =========================================================
            // TABLE (RoundedPanel)
            // =========================================================
            this.panelTable.BackColor = System.Drawing.Color.Transparent;
            this.panelTable.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.panelTable.BorderRadius = 14;
            this.panelTable.BorderSize = 1;
            this.panelTable.FillColor = System.Drawing.Color.White;
            this.panelTable.Controls.Add(this.dataGridViewTransactions);
            this.panelTable.Controls.Add(this.panelFooter);
            this.panelTable.Controls.Add(this.labelTableTitle);
            this.panelTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTable.Location = new System.Drawing.Point(20, 330);
            this.panelTable.Name = "panelTable";
            this.panelTable.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelTable.Size = new System.Drawing.Size(1240, 396);
            this.panelTable.TabIndex = 3;

            this.labelTableTitle.AutoSize = true;
            this.labelTableTitle.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.labelTableTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.labelTableTitle.Location = new System.Drawing.Point(20, 18);
            this.labelTableTitle.Name = "labelTableTitle";
            this.labelTableTitle.Size = new System.Drawing.Size(155, 23);
            this.labelTableTitle.TabIndex = 0;
            this.labelTableTitle.Text = "Danh sách giao dịch";

            // DATA GRID
            this.dataGridViewTransactions.AllowUserToAddRows = false;
            this.dataGridViewTransactions.AllowUserToDeleteRows = false;
            this.dataGridViewTransactions.AllowUserToResizeRows = false;
            this.dataGridViewTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            dgvAltRowStyle.BackColor = System.Drawing.Color.FromArgb(252, 253, 255);
            this.dataGridViewTransactions.AlternatingRowsDefaultCellStyle = dgvAltRowStyle;
            this.dataGridViewTransactions.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewTransactions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewTransactions.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewTransactions.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;

            dgvHeaderStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvHeaderStyle.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dgvHeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dgvHeaderStyle.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            dgvHeaderStyle.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            dgvHeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            dgvHeaderStyle.SelectionForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            dgvHeaderStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTransactions.ColumnHeadersDefaultCellStyle = dgvHeaderStyle;
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

            dgvRowStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dgvRowStyle.BackColor = System.Drawing.Color.White;
            dgvRowStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dgvRowStyle.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dgvRowStyle.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            dgvRowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(239, 246, 255);
            dgvRowStyle.SelectionForeColor = System.Drawing.Color.FromArgb(30, 64, 175);
            dgvRowStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTransactions.DefaultCellStyle = dgvRowStyle;

            this.dataGridViewTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTransactions.EnableHeadersVisualStyles = false;
            this.dataGridViewTransactions.GridColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.dataGridViewTransactions.Location = new System.Drawing.Point(2, 58);
            this.dataGridViewTransactions.MultiSelect = false;
            this.dataGridViewTransactions.Name = "dataGridViewTransactions";
            this.dataGridViewTransactions.ReadOnly = true;
            this.dataGridViewTransactions.RowHeadersVisible = false;
            this.dataGridViewTransactions.RowHeadersWidth = 62;
            this.dataGridViewTransactions.RowTemplate.Height = 46;
            this.dataGridViewTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTransactions.Size = new System.Drawing.Size(1236, 282);
            this.dataGridViewTransactions.TabIndex = 1;
            this.dataGridViewTransactions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTransactions_CellContentClick);

            this.dataGridViewTextBoxColumn1.HeaderText = "NGÀY GIAO DỊCH";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;

            this.dataGridViewTextBoxColumn2.HeaderText = "NỘI DUNG";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;

            this.dataGridViewTextBoxColumn3.HeaderText = "TÀI KHOẢN";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;

            this.dataGridViewTextBoxColumn4.HeaderText = "LOẠI";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;

            this.dataGridViewTextBoxColumn5.HeaderText = "SỐ TIỀN";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;

            this.dataGridViewTextBoxColumn7.HeaderText = "THAO TÁC";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;

            this.dataGridViewTextBoxColumn6.HeaderText = "TRẠNG THÁI";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;

            // =========================================================
            // FOOTER (paging)
            // =========================================================
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.buttonNext);
            this.panelFooter.Controls.Add(this.labelPage);
            this.panelFooter.Controls.Add(this.buttonPrevious);
            this.panelFooter.Controls.Add(this.labelPageInfo);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(2, 340);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1236, 54);
            this.panelFooter.TabIndex = 2;

            this.labelPageInfo.AutoSize = true;
            this.labelPageInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.labelPageInfo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.labelPageInfo.Location = new System.Drawing.Point(18, 20);
            this.labelPageInfo.Name = "labelPageInfo";
            this.labelPageInfo.Size = new System.Drawing.Size(168, 15);
            this.labelPageInfo.TabIndex = 0;
            this.labelPageInfo.Text = "Hiển thị 1 - 8 / 24 giao dịch";

            // --- buttonPrevious ---
            this.buttonPrevious.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.buttonPrevious.BackColor = System.Drawing.Color.Transparent;
            this.buttonPrevious.BorderRadius = 8;
            this.buttonPrevious.BorderSize = 0;
            this.buttonPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrevious.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.buttonPrevious.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.buttonPrevious.HoverBorder = System.Drawing.Color.Transparent;
            this.buttonPrevious.HoverColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.buttonPrevious.Location = new System.Drawing.Point(1046, 10);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.NormalBorder = System.Drawing.Color.Transparent;
            this.buttonPrevious.NormalColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.buttonPrevious.PressedBorder = System.Drawing.Color.Transparent;
            this.buttonPrevious.PressedColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.buttonPrevious.Size = new System.Drawing.Size(40, 34);
            this.buttonPrevious.TabIndex = 1;
            this.buttonPrevious.Text = "‹";
            this.buttonPrevious.TextColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.buttonPrevious.UseVisualStyleBackColor = false;

            // labelPage
            this.labelPage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.labelPage.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.labelPage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.labelPage.ForeColor = System.Drawing.Color.White;
            this.labelPage.Location = new System.Drawing.Point(1094, 10);
            this.labelPage.Name = "labelPage";
            this.labelPage.Size = new System.Drawing.Size(40, 34);
            this.labelPage.TabIndex = 2;
            this.labelPage.Text = "1";
            this.labelPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // --- buttonNext ---
            this.buttonNext.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.buttonNext.BackColor = System.Drawing.Color.Transparent;
            this.buttonNext.BorderRadius = 8;
            this.buttonNext.BorderSize = 0;
            this.buttonNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNext.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.buttonNext.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.buttonNext.HoverBorder = System.Drawing.Color.Transparent;
            this.buttonNext.HoverColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.buttonNext.Location = new System.Drawing.Point(1142, 10);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.NormalBorder = System.Drawing.Color.Transparent;
            this.buttonNext.NormalColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.buttonNext.PressedBorder = System.Drawing.Color.Transparent;
            this.buttonNext.PressedColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.buttonNext.Size = new System.Drawing.Size(40, 34);
            this.buttonNext.TabIndex = 3;
            this.buttonNext.Text = "›";
            this.buttonNext.TextColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.buttonNext.UseVisualStyleBackColor = false;

            // =========================================================
            // FORM
            // =========================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
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
            this.panelIncome.ResumeLayout(false);
            this.panelIncome.PerformLayout();
            this.panelExpense.ResumeLayout(false);
            this.panelExpense.PerformLayout();
            this.panelCount.ResumeLayout(false);
            this.panelCount.PerformLayout();
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