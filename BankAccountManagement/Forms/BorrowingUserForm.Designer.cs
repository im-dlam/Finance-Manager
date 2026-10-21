namespace BankAccountManagement.Forms
{
    partial class BorrowingUserForm
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblHeaderSub = new System.Windows.Forms.Label();
            this.lblAccountSelect = new System.Windows.Forms.Label();
            this.cboAccount = new System.Windows.Forms.ComboBox();
            this.btnHistory = new BankAccountManagement.Forms.RoundedButton();

            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnRegister = new BankAccountManagement.Forms.RoundedButton();

            this.pnlContent = new System.Windows.Forms.Panel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();

            // Left card
            this.pnlLeftCard = new BankAccountManagement.Forms.RoundedPanel();
            this.lblVayTitle = new System.Windows.Forms.Label();
            this.txtVayAmount = new System.Windows.Forms.TextBox();
            this.lblVayUnit = new System.Windows.Forms.Label();
            this.pnlVayInputBox = new BankAccountManagement.Forms.RoundedPanel();
            this.trackVayBg = new BankAccountManagement.Forms.RoundedPanel();
            this.trackVayFill = new BankAccountManagement.Forms.RoundedPanel();
            this.thumbVay = new BankAccountManagement.Forms.RoundedPanel();
            this.lblVayMin = new System.Windows.Forms.Label();
            this.lblVayMax = new System.Windows.Forms.Label();

            this.lblTimeTitle = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.lblTimeUnit = new System.Windows.Forms.Label();
            this.pnlTimeInputBox = new BankAccountManagement.Forms.RoundedPanel();
            this.trackTimeBg = new BankAccountManagement.Forms.RoundedPanel();
            this.trackTimeFill = new BankAccountManagement.Forms.RoundedPanel();
            this.thumbTime = new BankAccountManagement.Forms.RoundedPanel();
            this.lblTimeMin = new System.Windows.Forms.Label();
            this.lblTimeMax = new System.Windows.Forms.Label();

            // Right card
            this.pnlRightCard = new BankAccountManagement.Forms.RoundedPanel();
            this.pnlSummary = new BankAccountManagement.Forms.RoundedPanel();
            this.lblSum1Value = new System.Windows.Forms.Label();
            this.lblSum1Title = new System.Windows.Forms.Label();
            this.lblSum2Value = new System.Windows.Forms.Label();
            this.lblSum2Title = new System.Windows.Forms.Label();
            this.lblSum3Value = new System.Windows.Forms.Label();
            this.lblSum3Title = new System.Windows.Forms.Label();
            this.lblSumInterestDetail = new System.Windows.Forms.Label();

            this.pnlRedDot = new BankAccountManagement.Forms.RoundedPanel();
            this.lblPayTitle = new System.Windows.Forms.Label();
            this.lblPayDesc = new System.Windows.Forms.Label();

            this.lblDebtTitle = new System.Windows.Forms.Label();
            this.pnlDebtInputBox = new BankAccountManagement.Forms.RoundedPanel();
            this.txtDebtAmount = new System.Windows.Forms.TextBox();
            this.lblDebtUnit = new System.Windows.Forms.Label();

            this.pnlDebtBox = new BankAccountManagement.Forms.RoundedPanel();
            this.lblDebtHint = new System.Windows.Forms.Label();

            this.chkAgree = new System.Windows.Forms.CheckBox();
            this.lblAgreeText = new System.Windows.Forms.Label();

            // History View
            this.pnlLoanHistoryView = new BankAccountManagement.Forms.RoundedPanel();
            this.lblHistHeaderTitle = new System.Windows.Forms.Label();
            this.lblHistHeaderSub = new System.Windows.Forms.Label();
            this.pnlHistGridWrapper = new BankAccountManagement.Forms.RoundedPanel();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.pnlHistDetailCard = new BankAccountManagement.Forms.RoundedPanel();
            this.pnlHistSummary = new BankAccountManagement.Forms.RoundedPanel();
            this.lblHistSum1Value = new System.Windows.Forms.Label();
            this.lblHistSum1Title = new System.Windows.Forms.Label();
            this.lblHistSum2Value = new System.Windows.Forms.Label();
            this.lblHistSum2Title = new System.Windows.Forms.Label();
            this.lblHistSum3Value = new System.Windows.Forms.Label();
            this.lblHistSum3Title = new System.Windows.Forms.Label();
            this.lblHistSumInterestDetail = new System.Windows.Forms.Label();
            this.pnlHistRedDot = new BankAccountManagement.Forms.RoundedPanel();
            this.lblHistPayTitle = new System.Windows.Forms.Label();
            this.lblHistPayDesc = new System.Windows.Forms.Label();
            this.lblHistDebtTitle = new System.Windows.Forms.Label();
            this.pnlHistDebtInputBox = new BankAccountManagement.Forms.RoundedPanel();
            this.txtHistDebtAmount = new System.Windows.Forms.TextBox();
            this.lblHistDebtUnit = new System.Windows.Forms.Label();
            this.pnlHistDebtBox = new BankAccountManagement.Forms.RoundedPanel();
            this.lblHistDebtHint = new System.Windows.Forms.Label();
            this.btnPayLoan = new BankAccountManagement.Forms.RoundedButton();
            this.btnCloseHistory = new BankAccountManagement.Forms.RoundedButton();

            this.pnlHeader.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.pnlLeftCard.SuspendLayout();
            this.pnlVayInputBox.SuspendLayout();
            this.pnlTimeInputBox.SuspendLayout();
            this.pnlRightCard.SuspendLayout();
            this.pnlSummary.SuspendLayout();
            this.pnlDebtInputBox.SuspendLayout();
            this.pnlDebtBox.SuspendLayout();
            this.pnlLoanHistoryView.SuspendLayout();
            this.pnlHistDetailCard.SuspendLayout();
            this.pnlHistSummary.SuspendLayout();
            this.pnlHistDebtInputBox.SuspendLayout();
            this.pnlHistDebtBox.SuspendLayout();
            this.SuspendLayout();

            // =============================================================
            // pnlHeader
            // =============================================================
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.btnHistory);
            this.pnlHeader.Controls.Add(this.cboAccount);
            this.pnlHeader.Controls.Add(this.lblAccountSelect);
            this.pnlHeader.Controls.Add(this.lblHeaderSub);
            this.pnlHeader.Controls.Add(this.lblHeaderTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 85);
            this.pnlHeader.TabIndex = 0;

            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblHeaderTitle.Location = new System.Drawing.Point(24, 14);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(320, 32);
            this.lblHeaderTitle.Text = "Thủ tục đăng ký đơn giản";

            this.lblHeaderSub.AutoSize = true;
            this.lblHeaderSub.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblHeaderSub.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblHeaderSub.Location = new System.Drawing.Point(26, 50);
            this.lblHeaderSub.Name = "lblHeaderSub";
            this.lblHeaderSub.Size = new System.Drawing.Size(102, 17);
            this.lblHeaderSub.Text = "Giải ngân nhanh";

            // Tài khoản nhận giải ngân
            this.lblAccountSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAccountSelect.AutoSize = true;
            this.lblAccountSelect.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblAccountSelect.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.lblAccountSelect.Location = new System.Drawing.Point(620, 18);
            this.lblAccountSelect.Name = "lblAccountSelect";
            this.lblAccountSelect.Size = new System.Drawing.Size(176, 17);
            this.lblAccountSelect.Text = "Tài khoản nhận giải ngân:";

            this.cboAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAccount.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboAccount.Location = new System.Drawing.Point(623, 42);
            this.cboAccount.Name = "cboAccount";
            this.cboAccount.Size = new System.Drawing.Size(370, 25);
            this.cboAccount.TabIndex = 2;

            this.btnHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistory.BorderRadius = 8;
            this.btnHistory.BorderSize = 1;
            this.btnHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnHistory.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.btnHistory.HoverBorder = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnHistory.HoverColor = System.Drawing.Color.FromArgb(253, 242, 248);
            this.btnHistory.Location = new System.Drawing.Point(1010, 36);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.NormalBorder = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnHistory.NormalColor = System.Drawing.Color.White;
            this.btnHistory.PressedColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.btnHistory.Size = new System.Drawing.Size(165, 34);
            this.btnHistory.TabIndex = 3;
            this.btnHistory.Text = "📋 Lịch sử vay của tôi";
            this.btnHistory.TextColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.btnHistory.UseVisualStyleBackColor = false;

            // =============================================================
            // pnlFooter
            // =============================================================
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.btnRegister);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 680);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1200, 85);
            this.pnlFooter.TabIndex = 2;

            this.btnRegister.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRegister.BorderRadius = 12;
            this.btnRegister.BorderSize = 0;
            this.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.HoverColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.btnRegister.Location = new System.Drawing.Point(460, 18);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.NormalColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnRegister.PressedColor = System.Drawing.Color.FromArgb(30, 64, 175);
            this.btnRegister.Size = new System.Drawing.Size(280, 48);
            this.btnRegister.TabIndex = 0;
            this.btnRegister.Text = "Đăng ký ngay";
            this.btnRegister.TextColor = System.Drawing.Color.White;
            this.btnRegister.UseVisualStyleBackColor = false;

            // =============================================================
            // pnlContent
            // =============================================================
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.pnlContent.Controls.Add(this.tlpMain);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 85);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(24, 20, 24, 20);
            this.pnlContent.Size = new System.Drawing.Size(1200, 595);
            this.pnlContent.TabIndex = 1;

            // =============================================================
            // tlpMain
            // =============================================================
            this.tlpMain.BackColor = System.Drawing.Color.Transparent;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46F));
            this.tlpMain.Controls.Add(this.pnlLeftCard, 0, 0);
            this.tlpMain.Controls.Add(this.pnlRightCard, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(24, 20);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1152, 555);
            this.tlpMain.TabIndex = 0;

            // =============================================================
            // pnlLeftCard
            // =============================================================
            this.pnlLeftCard.BackColor = System.Drawing.Color.Transparent;
            this.pnlLeftCard.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlLeftCard.BorderRadius = 16;
            this.pnlLeftCard.BorderSize = 1;
            this.pnlLeftCard.FillColor = System.Drawing.Color.White;
            this.pnlLeftCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeftCard.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.pnlLeftCard.Name = "pnlLeftCard";
            this.pnlLeftCard.Controls.Add(this.thumbVay);
            this.pnlLeftCard.Controls.Add(this.thumbTime);
            this.pnlLeftCard.Controls.Add(this.lblVayTitle);
            this.pnlLeftCard.Controls.Add(this.pnlVayInputBox);
            this.pnlLeftCard.Controls.Add(this.trackVayFill);
            this.pnlLeftCard.Controls.Add(this.trackVayBg);
            this.pnlLeftCard.Controls.Add(this.lblVayMin);
            this.pnlLeftCard.Controls.Add(this.lblVayMax);
            this.pnlLeftCard.Controls.Add(this.lblTimeTitle);
            this.pnlLeftCard.Controls.Add(this.pnlTimeInputBox);
            this.pnlLeftCard.Controls.Add(this.trackTimeFill);
            this.pnlLeftCard.Controls.Add(this.trackTimeBg);
            this.pnlLeftCard.Controls.Add(this.lblTimeMin);
            this.pnlLeftCard.Controls.Add(this.lblTimeMax);

            // --- Tôi muốn vay ---
            this.lblVayTitle.AutoSize = true;
            this.lblVayTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblVayTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblVayTitle.Location = new System.Drawing.Point(32, 28);
            this.lblVayTitle.Name = "lblVayTitle";
            this.lblVayTitle.Size = new System.Drawing.Size(94, 19);
            this.lblVayTitle.Text = "Tôi muốn vay";

            // Ô nhập số tiền vay bo tròn - viền đậm rõ nét
            this.pnlVayInputBox.BorderColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.pnlVayInputBox.BorderRadius = 10;
            this.pnlVayInputBox.BorderSize = 2;
            this.pnlVayInputBox.FillColor = System.Drawing.Color.White;
            this.pnlVayInputBox.Location = new System.Drawing.Point(32, 54);
            this.pnlVayInputBox.Name = "pnlVayInputBox";
            this.pnlVayInputBox.Size = new System.Drawing.Size(320, 42);
            this.pnlVayInputBox.Controls.Add(this.txtVayAmount);
            this.pnlVayInputBox.Controls.Add(this.lblVayUnit);

            this.txtVayAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtVayAmount.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.txtVayAmount.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtVayAmount.Location = new System.Drawing.Point(12, 8);
            this.txtVayAmount.Name = "txtVayAmount";
            this.txtVayAmount.Size = new System.Drawing.Size(265, 24);
            this.txtVayAmount.Text = "1.000.000";
            this.txtVayAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            this.lblVayUnit.AutoSize = true;
            this.lblVayUnit.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblVayUnit.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblVayUnit.Location = new System.Drawing.Point(285, 8);
            this.lblVayUnit.Name = "lblVayUnit";
            this.lblVayUnit.Size = new System.Drawing.Size(22, 25);
            this.lblVayUnit.Text = "đ";

            // Thanh trượt khoản vay
            this.trackVayBg.BorderRadius = 4;
            this.trackVayBg.BorderSize = 0;
            this.trackVayBg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackVayBg.FillColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.trackVayBg.Location = new System.Drawing.Point(32, 126);
            this.trackVayBg.Name = "trackVayBg";
            this.trackVayBg.Size = new System.Drawing.Size(520, 8);

            this.trackVayFill.BorderRadius = 4;
            this.trackVayFill.BorderSize = 0;
            this.trackVayFill.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackVayFill.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.trackVayFill.Location = new System.Drawing.Point(32, 126);
            this.trackVayFill.Name = "trackVayFill";
            this.trackVayFill.Size = new System.Drawing.Size(60, 8);

            this.thumbVay.BorderColor = System.Drawing.Color.White;
            this.thumbVay.BorderRadius = 12;
            this.thumbVay.BorderSize = 3;
            this.thumbVay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.thumbVay.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.thumbVay.Location = new System.Drawing.Point(75, 118);
            this.thumbVay.Name = "thumbVay";
            this.thumbVay.Size = new System.Drawing.Size(24, 24);

            this.lblVayMin.AutoSize = true;
            this.lblVayMin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblVayMin.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblVayMin.Location = new System.Drawing.Point(32, 146);
            this.lblVayMin.Name = "lblVayMin";
            this.lblVayMin.Size = new System.Drawing.Size(49, 15);
            this.lblVayMin.Text = "100.000";

            this.lblVayMax.AutoSize = true;
            this.lblVayMax.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblVayMax.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblVayMax.Location = new System.Drawing.Point(485, 146);
            this.lblVayMax.Name = "lblVayMax";
            this.lblVayMax.Size = new System.Drawing.Size(65, 15);
            this.lblVayMax.Text = "10.000.000";

            // --- Thời gian vay ---
            this.lblTimeTitle.AutoSize = true;
            this.lblTimeTitle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblTimeTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblTimeTitle.Location = new System.Drawing.Point(32, 206);
            this.lblTimeTitle.Name = "lblTimeTitle";
            this.lblTimeTitle.Size = new System.Drawing.Size(95, 19);
            this.lblTimeTitle.Text = "Thời gian vay";

            this.pnlTimeInputBox.BorderColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.pnlTimeInputBox.BorderRadius = 10;
            this.pnlTimeInputBox.BorderSize = 2;
            this.pnlTimeInputBox.FillColor = System.Drawing.Color.White;
            this.pnlTimeInputBox.Location = new System.Drawing.Point(32, 232);
            this.pnlTimeInputBox.Name = "pnlTimeInputBox";
            this.pnlTimeInputBox.Size = new System.Drawing.Size(320, 42);
            this.pnlTimeInputBox.Controls.Add(this.txtTime);
            this.pnlTimeInputBox.Controls.Add(this.lblTimeUnit);

            this.txtTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTime.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.txtTime.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtTime.Location = new System.Drawing.Point(12, 8);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(250, 24);
            this.txtTime.Text = "30";
            this.txtTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            this.lblTimeUnit.AutoSize = true;
            this.lblTimeUnit.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblTimeUnit.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTimeUnit.Location = new System.Drawing.Point(268, 9);
            this.lblTimeUnit.Name = "lblTimeUnit";
            this.lblTimeUnit.Size = new System.Drawing.Size(46, 21);
            this.lblTimeUnit.Text = "ngày";

            // Thanh trượt thời gian vay
            this.trackTimeBg.BorderRadius = 4;
            this.trackTimeBg.BorderSize = 0;
            this.trackTimeBg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackTimeBg.FillColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.trackTimeBg.Location = new System.Drawing.Point(32, 304);
            this.trackTimeBg.Name = "trackTimeBg";
            this.trackTimeBg.Size = new System.Drawing.Size(520, 8);

            this.trackTimeFill.BorderRadius = 4;
            this.trackTimeFill.BorderSize = 0;
            this.trackTimeFill.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackTimeFill.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.trackTimeFill.Location = new System.Drawing.Point(32, 304);
            this.trackTimeFill.Name = "trackTimeFill";
            this.trackTimeFill.Size = new System.Drawing.Size(500, 8);

            this.thumbTime.BorderColor = System.Drawing.Color.White;
            this.thumbTime.BorderRadius = 12;
            this.thumbTime.BorderSize = 3;
            this.thumbTime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.thumbTime.FillColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.thumbTime.Location = new System.Drawing.Point(515, 296);
            this.thumbTime.Name = "thumbTime";
            this.thumbTime.Size = new System.Drawing.Size(24, 24);

            this.lblTimeMin.AutoSize = true;
            this.lblTimeMin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTimeMin.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblTimeMin.Location = new System.Drawing.Point(32, 324);
            this.lblTimeMin.Name = "lblTimeMin";
            this.lblTimeMin.Size = new System.Drawing.Size(40, 15);
            this.lblTimeMin.Text = "7 ngày";

            this.lblTimeMax.AutoSize = true;
            this.lblTimeMax.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTimeMax.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblTimeMax.Location = new System.Drawing.Point(505, 324);
            this.lblTimeMax.Name = "lblTimeMax";
            this.lblTimeMax.Size = new System.Drawing.Size(48, 15);
            this.lblTimeMax.Text = "30 ngày";

            // =============================================================
            // pnlRightCard
            // =============================================================
            this.pnlRightCard.BackColor = System.Drawing.Color.Transparent;
            this.pnlRightCard.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlRightCard.BorderRadius = 16;
            this.pnlRightCard.BorderSize = 1;
            this.pnlRightCard.FillColor = System.Drawing.Color.White;
            this.pnlRightCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRightCard.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.pnlRightCard.Name = "pnlRightCard";
            this.pnlRightCard.Controls.Add(this.pnlLoanHistoryView);
            this.pnlRightCard.Controls.Add(this.pnlSummary);
            this.pnlRightCard.Controls.Add(this.pnlRedDot);
            this.pnlRightCard.Controls.Add(this.lblPayTitle);
            this.pnlRightCard.Controls.Add(this.lblPayDesc);
            this.pnlRightCard.Controls.Add(this.lblDebtTitle);
            this.pnlRightCard.Controls.Add(this.pnlDebtInputBox);
            this.pnlRightCard.Controls.Add(this.pnlDebtBox);
            this.pnlRightCard.Controls.Add(this.chkAgree);
            this.pnlRightCard.Controls.Add(this.lblAgreeText);

            // =============================================================
            // pnlSummary — Bảng trống được điền đầy đủ Lãi suất & Thông tin
            // =============================================================
            this.pnlSummary.BorderColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.pnlSummary.BorderRadius = 12;
            this.pnlSummary.BorderSize = 1;
            this.pnlSummary.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.pnlSummary.Location = new System.Drawing.Point(24, 20);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(480, 105);
            this.pnlSummary.Controls.Add(this.lblSum1Value);
            this.pnlSummary.Controls.Add(this.lblSum1Title);
            this.pnlSummary.Controls.Add(this.lblSum2Value);
            this.pnlSummary.Controls.Add(this.lblSum2Title);
            this.pnlSummary.Controls.Add(this.lblSum3Value);
            this.pnlSummary.Controls.Add(this.lblSum3Title);
            this.pnlSummary.Controls.Add(this.lblSumInterestDetail);

            // Cột 1: Khoản vay
            this.lblSum1Value.AutoSize = true;
            this.lblSum1Value.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSum1Value.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblSum1Value.Location = new System.Drawing.Point(16, 16);
            this.lblSum1Value.Name = "lblSum1Value";
            this.lblSum1Value.Size = new System.Drawing.Size(94, 20);
            this.lblSum1Value.Text = "1.000.000 đ";

            this.lblSum1Title.AutoSize = true;
            this.lblSum1Title.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSum1Title.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSum1Title.Location = new System.Drawing.Point(16, 40);
            this.lblSum1Title.Name = "lblSum1Title";
            this.lblSum1Title.Size = new System.Drawing.Size(60, 15);
            this.lblSum1Title.Text = "Khoản vay";

            // Cột 2: Lãi suất ⭐
            this.lblSum2Value.AutoSize = true;
            this.lblSum2Value.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSum2Value.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblSum2Value.Location = new System.Drawing.Point(165, 16);
            this.lblSum2Value.Name = "lblSum2Value";
            this.lblSum2Value.Size = new System.Drawing.Size(89, 20);
            this.lblSum2Value.Text = "1.55%/ngày";

            this.lblSum2Title.AutoSize = true;
            this.lblSum2Title.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSum2Title.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSum2Title.Location = new System.Drawing.Point(165, 40);
            this.lblSum2Title.Name = "lblSum2Title";
            this.lblSum2Title.Size = new System.Drawing.Size(86, 15);
            this.lblSum2Title.Text = "Lãi suất ưu đãi";

            // Cột 3: Ngày đáo hạn
            this.lblSum3Value.AutoSize = true;
            this.lblSum3Value.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSum3Value.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblSum3Value.Location = new System.Drawing.Point(320, 16);
            this.lblSum3Value.Name = "lblSum3Value";
            this.lblSum3Value.Size = new System.Drawing.Size(87, 20);
            this.lblSum3Value.Text = "15/11/2026";

            this.lblSum3Title.AutoSize = true;
            this.lblSum3Title.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSum3Title.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSum3Title.Location = new System.Drawing.Point(320, 40);
            this.lblSum3Title.Name = "lblSum3Title";
            this.lblSum3Title.Size = new System.Drawing.Size(80, 15);
            this.lblSum3Title.Text = "Ngày đáo hạn";

            // Dòng chi tiết lãi
            this.lblSumInterestDetail.AutoSize = true;
            this.lblSumInterestDetail.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblSumInterestDetail.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblSumInterestDetail.Location = new System.Drawing.Point(16, 72);
            this.lblSumInterestDetail.Name = "lblSumInterestDetail";
            this.lblSumInterestDetail.Size = new System.Drawing.Size(275, 15);
            this.lblSumInterestDetail.Text = "Tiền lãi & phí dự tính (30 ngày): + 465.000 đ";

            // --- Nợ phải trả section ---
            this.pnlRedDot.BorderRadius = 5;
            this.pnlRedDot.BorderSize = 0;
            this.pnlRedDot.FillColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.pnlRedDot.Location = new System.Drawing.Point(24, 142);
            this.pnlRedDot.Name = "pnlRedDot";
            this.pnlRedDot.Size = new System.Drawing.Size(10, 10);

            this.lblPayTitle.AutoSize = true;
            this.lblPayTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPayTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblPayTitle.Location = new System.Drawing.Point(40, 137);
            this.lblPayTitle.Name = "lblPayTitle";
            this.lblPayTitle.Size = new System.Drawing.Size(168, 20);
            this.lblPayTitle.Text = "Số tiền phải trả của bạn";

            this.lblPayDesc.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.lblPayDesc.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblPayDesc.Location = new System.Drawing.Point(24, 164);
            this.lblPayDesc.Name = "lblPayDesc";
            this.lblPayDesc.Size = new System.Drawing.Size(480, 36);
            this.lblPayDesc.Text = "Ngày đến hạn có thể rơi vào ngày lễ (không làm việc) và sẽ được dời lại ngày làm việc gần nhất.";

            // Ghi số nợ phải trả
            this.lblDebtTitle.AutoSize = true;
            this.lblDebtTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDebtTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblDebtTitle.Location = new System.Drawing.Point(24, 210);
            this.lblDebtTitle.Name = "lblDebtTitle";
            this.lblDebtTitle.Size = new System.Drawing.Size(126, 19);
            this.lblDebtTitle.Text = "Ghi số nợ phải trả";

            this.pnlDebtInputBox.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.pnlDebtInputBox.BorderRadius = 8;
            this.pnlDebtInputBox.BorderSize = 1;
            this.pnlDebtInputBox.FillColor = System.Drawing.Color.White;
            this.pnlDebtInputBox.Location = new System.Drawing.Point(24, 236);
            this.pnlDebtInputBox.Name = "pnlDebtInputBox";
            this.pnlDebtInputBox.Size = new System.Drawing.Size(480, 44);
            this.pnlDebtInputBox.Controls.Add(this.txtDebtAmount);
            this.pnlDebtInputBox.Controls.Add(this.lblDebtUnit);

            this.txtDebtAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDebtAmount.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.txtDebtAmount.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.txtDebtAmount.Location = new System.Drawing.Point(12, 7);
            this.txtDebtAmount.Name = "txtDebtAmount";
            this.txtDebtAmount.ReadOnly = true;
            this.txtDebtAmount.Size = new System.Drawing.Size(415, 29);
            this.txtDebtAmount.Text = "1.465.000";
            this.txtDebtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            this.lblDebtUnit.AutoSize = true;
            this.lblDebtUnit.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblDebtUnit.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblDebtUnit.Location = new System.Drawing.Point(435, 7);
            this.lblDebtUnit.Name = "lblDebtUnit";
            this.lblDebtUnit.Size = new System.Drawing.Size(26, 30);
            this.lblDebtUnit.Text = "đ";

            // Hint box cam
            this.pnlDebtBox.BorderColor = System.Drawing.Color.FromArgb(254, 215, 170);
            this.pnlDebtBox.BorderRadius = 8;
            this.pnlDebtBox.BorderSize = 1;
            this.pnlDebtBox.FillColor = System.Drawing.Color.FromArgb(255, 247, 237);
            this.pnlDebtBox.Location = new System.Drawing.Point(24, 290);
            this.pnlDebtBox.Name = "pnlDebtBox";
            this.pnlDebtBox.Size = new System.Drawing.Size(480, 38);
            this.pnlDebtBox.Controls.Add(this.lblDebtHint);

            this.lblDebtHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDebtHint.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.lblDebtHint.ForeColor = System.Drawing.Color.FromArgb(180, 83, 9);
            this.lblDebtHint.Location = new System.Drawing.Point(0, 0);
            this.lblDebtHint.Name = "lblDebtHint";
            this.lblDebtHint.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.lblDebtHint.Size = new System.Drawing.Size(480, 38);
            this.lblDebtHint.Text = "⚠  Số nợ được tính tự động từ khoản vay + lãi suất × thời gian vay";
            this.lblDebtHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // Checkbox CIC
            this.chkAgree.Checked = true;
            this.chkAgree.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAgree.Location = new System.Drawing.Point(24, 344);
            this.chkAgree.Name = "chkAgree";
            this.chkAgree.Size = new System.Drawing.Size(20, 20);
            this.chkAgree.TabIndex = 8;
            this.chkAgree.UseVisualStyleBackColor = true;

            this.lblAgreeText.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAgreeText.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblAgreeText.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblAgreeText.Location = new System.Drawing.Point(50, 342);
            this.lblAgreeText.Name = "lblAgreeText";
            this.lblAgreeText.Size = new System.Drawing.Size(454, 60);
            this.lblAgreeText.Text = "Chúng tôi có thể xác minh thông tin của bạn và báo cáo các khoản vay của bạn cho Trung tâm Thông tin Tín dụng Quốc gia Việt Nam (CIC) và các tổ chức tín dụng khác.";

            // =============================================================
            // pnlLoanHistoryView — Giao diện Lịch sử hiển thị trực tiếp trong Thẻ phải
            // =============================================================
            this.pnlLoanHistoryView.BackColor = System.Drawing.Color.White;
            this.pnlLoanHistoryView.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlLoanHistoryView.BorderRadius = 16;
            this.pnlLoanHistoryView.BorderSize = 0;
            this.pnlLoanHistoryView.Controls.Add(this.lblHistHeaderTitle);
            this.pnlLoanHistoryView.Controls.Add(this.lblHistHeaderSub);
            this.pnlLoanHistoryView.Controls.Add(this.pnlHistGridWrapper);
            this.pnlLoanHistoryView.Controls.Add(this.pnlHistDetailCard);
            this.pnlLoanHistoryView.Controls.Add(this.btnPayLoan);
            this.pnlLoanHistoryView.Controls.Add(this.btnCloseHistory);
            this.pnlLoanHistoryView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLoanHistoryView.FillColor = System.Drawing.Color.White;
            this.pnlLoanHistoryView.Location = new System.Drawing.Point(0, 0);
            this.pnlLoanHistoryView.Name = "pnlLoanHistoryView";
            this.pnlLoanHistoryView.Size = new System.Drawing.Size(530, 555);
            this.pnlLoanHistoryView.TabIndex = 10;
            this.pnlLoanHistoryView.Visible = false;

            // lblHistHeaderTitle
            this.lblHistHeaderTitle.AutoSize = true;
            this.lblHistHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblHistHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblHistHeaderTitle.Location = new System.Drawing.Point(20, 14);
            this.lblHistHeaderTitle.Name = "lblHistHeaderTitle";
            this.lblHistHeaderTitle.Size = new System.Drawing.Size(262, 21);
            this.lblHistHeaderTitle.TabIndex = 0;
            this.lblHistHeaderTitle.Text = "Danh sách hồ sơ vay vốn của bạn";

            // lblHistHeaderSub
            this.lblHistHeaderSub.AutoSize = true;
            this.lblHistHeaderSub.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblHistHeaderSub.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblHistHeaderSub.Location = new System.Drawing.Point(20, 36);
            this.lblHistHeaderSub.Name = "lblHistHeaderSub";
            this.lblHistHeaderSub.Size = new System.Drawing.Size(248, 15);
            this.lblHistHeaderSub.TabIndex = 1;
            this.lblHistHeaderSub.Text = "Bấm chọn hồ sơ trong bảng để xem chi tiết bên dưới";

            // pnlHistGridWrapper — Khung viền bo góc chuẩn app hiện đại
            this.pnlHistGridWrapper.BackColor = System.Drawing.Color.White;
            this.pnlHistGridWrapper.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlHistGridWrapper.BorderRadius = 12;
            this.pnlHistGridWrapper.BorderSize = 1;
            this.pnlHistGridWrapper.Controls.Add(this.dgvHistory);
            this.pnlHistGridWrapper.FillColor = System.Drawing.Color.White;
            this.pnlHistGridWrapper.Location = new System.Drawing.Point(20, 56);
            this.pnlHistGridWrapper.Name = "pnlHistGridWrapper";
            this.pnlHistGridWrapper.Padding = new System.Windows.Forms.Padding(1);
            this.pnlHistGridWrapper.Size = new System.Drawing.Size(480, 118);
            this.pnlHistGridWrapper.TabIndex = 2;

            // dgvHistory — Bảng không viền đen thô, đồng bộ phong cách ứng dụng
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.AllowUserToResizeRows = false;
            this.dgvHistory.AllowUserToResizeColumns = false;
            this.dgvHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHistory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvHistory.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvHistory.ColumnHeadersHeight = 32;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistory.EnableHeadersVisualStyles = false;
            this.dgvHistory.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.dgvHistory.GridColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.dgvHistory.Location = new System.Drawing.Point(1, 1);
            this.dgvHistory.MultiSelect = false;
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.RowTemplate.Height = 32;
            this.dgvHistory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.Size = new System.Drawing.Size(478, 116);
            this.dgvHistory.TabIndex = 0;

            // pnlHistDetailCard
            this.pnlHistDetailCard.BackColor = System.Drawing.Color.Transparent;
            this.pnlHistDetailCard.BorderColor = System.Drawing.Color.Transparent;
            this.pnlHistDetailCard.BorderSize = 0;
            this.pnlHistDetailCard.Controls.Add(this.pnlHistSummary);
            this.pnlHistDetailCard.Controls.Add(this.pnlHistRedDot);
            this.pnlHistDetailCard.Controls.Add(this.lblHistPayTitle);
            this.pnlHistDetailCard.Controls.Add(this.lblHistPayDesc);
            this.pnlHistDetailCard.Controls.Add(this.lblHistDebtTitle);
            this.pnlHistDetailCard.Controls.Add(this.pnlHistDebtInputBox);
            this.pnlHistDetailCard.Controls.Add(this.pnlHistDebtBox);
            this.pnlHistDetailCard.FillColor = System.Drawing.Color.White;
            this.pnlHistDetailCard.Location = new System.Drawing.Point(20, 182);
            this.pnlHistDetailCard.Name = "pnlHistDetailCard";
            this.pnlHistDetailCard.Size = new System.Drawing.Size(480, 290);
            this.pnlHistDetailCard.TabIndex = 3;

            // pnlHistSummary (Ảnh 2 top box)
            this.pnlHistSummary.BorderColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.pnlHistSummary.BorderRadius = 12;
            this.pnlHistSummary.BorderSize = 1;
            this.pnlHistSummary.Controls.Add(this.lblHistSum1Value);
            this.pnlHistSummary.Controls.Add(this.lblHistSum1Title);
            this.pnlHistSummary.Controls.Add(this.lblHistSum2Value);
            this.pnlHistSummary.Controls.Add(this.lblHistSum2Title);
            this.pnlHistSummary.Controls.Add(this.lblHistSum3Value);
            this.pnlHistSummary.Controls.Add(this.lblHistSum3Title);
            this.pnlHistSummary.Controls.Add(this.lblHistSumInterestDetail);
            this.pnlHistSummary.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.pnlHistSummary.Location = new System.Drawing.Point(0, 0);
            this.pnlHistSummary.Name = "pnlHistSummary";
            this.pnlHistSummary.Size = new System.Drawing.Size(480, 95);
            this.pnlHistSummary.TabIndex = 0;

            this.lblHistSum1Value.AutoSize = true;
            this.lblHistSum1Value.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHistSum1Value.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblHistSum1Value.Location = new System.Drawing.Point(16, 12);
            this.lblHistSum1Value.Name = "lblHistSum1Value";
            this.lblHistSum1Value.Size = new System.Drawing.Size(94, 20);
            this.lblHistSum1Value.Text = "7,100,000 đ";

            this.lblHistSum1Title.AutoSize = true;
            this.lblHistSum1Title.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblHistSum1Title.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblHistSum1Title.Location = new System.Drawing.Point(16, 36);
            this.lblHistSum1Title.Name = "lblHistSum1Title";
            this.lblHistSum1Title.Size = new System.Drawing.Size(60, 15);
            this.lblHistSum1Title.Text = "Khoản vay";

            this.lblHistSum2Value.AutoSize = true;
            this.lblHistSum2Value.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHistSum2Value.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblHistSum2Value.Location = new System.Drawing.Point(165, 12);
            this.lblHistSum2Value.Name = "lblHistSum2Value";
            this.lblHistSum2Value.Size = new System.Drawing.Size(89, 20);
            this.lblHistSum2Value.Text = "1.55%/ngày";

            this.lblHistSum2Title.AutoSize = true;
            this.lblHistSum2Title.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblHistSum2Title.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblHistSum2Title.Location = new System.Drawing.Point(165, 36);
            this.lblHistSum2Title.Name = "lblHistSum2Title";
            this.lblHistSum2Title.Size = new System.Drawing.Size(86, 15);
            this.lblHistSum2Title.Text = "Lãi suất ưu đãi";

            this.lblHistSum3Value.AutoSize = true;
            this.lblHistSum3Value.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHistSum3Value.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblHistSum3Value.Location = new System.Drawing.Point(320, 12);
            this.lblHistSum3Value.Name = "lblHistSum3Value";
            this.lblHistSum3Value.Size = new System.Drawing.Size(87, 20);
            this.lblHistSum3Value.Text = "06/11/2026";

            this.lblHistSum3Title.AutoSize = true;
            this.lblHistSum3Title.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblHistSum3Title.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblHistSum3Title.Location = new System.Drawing.Point(320, 36);
            this.lblHistSum3Title.Name = "lblHistSum3Title";
            this.lblHistSum3Title.Size = new System.Drawing.Size(80, 15);
            this.lblHistSum3Title.Text = "Ngày đáo hạn";

            this.lblHistSumInterestDetail.AutoSize = true;
            this.lblHistSumInterestDetail.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblHistSumInterestDetail.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblHistSumInterestDetail.Location = new System.Drawing.Point(16, 66);
            this.lblHistSumInterestDetail.Name = "lblHistSumInterestDetail";
            this.lblHistSumInterestDetail.Size = new System.Drawing.Size(265, 15);
            this.lblHistSumInterestDetail.Text = "Tiền lãi, phí dự tính (21 ngày): + 2,311,050 đ";

            // pnlHistRedDot
            this.pnlHistRedDot.BorderRadius = 5;
            this.pnlHistRedDot.BorderSize = 0;
            this.pnlHistRedDot.FillColor = System.Drawing.Color.FromArgb(239, 68, 68);
            this.pnlHistRedDot.Location = new System.Drawing.Point(0, 108);
            this.pnlHistRedDot.Name = "pnlHistRedDot";
            this.pnlHistRedDot.Size = new System.Drawing.Size(10, 10);

            // lblHistPayTitle
            this.lblHistPayTitle.AutoSize = true;
            this.lblHistPayTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHistPayTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblHistPayTitle.Location = new System.Drawing.Point(16, 103);
            this.lblHistPayTitle.Name = "lblHistPayTitle";
            this.lblHistPayTitle.Size = new System.Drawing.Size(168, 20);
            this.lblHistPayTitle.Text = "Số tiền phải trả của bạn";

            // lblHistPayDesc
            this.lblHistPayDesc.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblHistPayDesc.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblHistPayDesc.Location = new System.Drawing.Point(0, 126);
            this.lblHistPayDesc.Name = "lblHistPayDesc";
            this.lblHistPayDesc.Size = new System.Drawing.Size(480, 20);
            this.lblHistPayDesc.Text = "Ngày đến hạn có thể rơi vào ngày lễ (không làm việc) và sẽ được dời lại ngày làm việc gần nhất.";

            // lblHistDebtTitle
            this.lblHistDebtTitle.AutoSize = true;
            this.lblHistDebtTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHistDebtTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblHistDebtTitle.Location = new System.Drawing.Point(0, 150);
            this.lblHistDebtTitle.Name = "lblHistDebtTitle";
            this.lblHistDebtTitle.Size = new System.Drawing.Size(126, 19);
            this.lblHistDebtTitle.Text = "Ghi số nợ phải trả";

            // pnlHistDebtInputBox
            this.pnlHistDebtInputBox.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.pnlHistDebtInputBox.BorderRadius = 8;
            this.pnlHistDebtInputBox.BorderSize = 1;
            this.pnlHistDebtInputBox.Controls.Add(this.txtHistDebtAmount);
            this.pnlHistDebtInputBox.Controls.Add(this.lblHistDebtUnit);
            this.pnlHistDebtInputBox.FillColor = System.Drawing.Color.White;
            this.pnlHistDebtInputBox.Location = new System.Drawing.Point(0, 172);
            this.pnlHistDebtInputBox.Name = "pnlHistDebtInputBox";
            this.pnlHistDebtInputBox.Size = new System.Drawing.Size(480, 42);
            this.pnlHistDebtInputBox.TabIndex = 4;

            this.txtHistDebtAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHistDebtAmount.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.txtHistDebtAmount.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.txtHistDebtAmount.Location = new System.Drawing.Point(12, 6);
            this.txtHistDebtAmount.Name = "txtHistDebtAmount";
            this.txtHistDebtAmount.ReadOnly = true;
            this.txtHistDebtAmount.Size = new System.Drawing.Size(415, 29);
            this.txtHistDebtAmount.Text = "9,411,050";
            this.txtHistDebtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            this.lblHistDebtUnit.AutoSize = true;
            this.lblHistDebtUnit.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHistDebtUnit.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblHistDebtUnit.Location = new System.Drawing.Point(435, 6);
            this.lblHistDebtUnit.Name = "lblHistDebtUnit";
            this.lblHistDebtUnit.Size = new System.Drawing.Size(26, 30);
            this.lblHistDebtUnit.Text = "đ";

            // pnlHistDebtBox
            this.pnlHistDebtBox.BorderColor = System.Drawing.Color.FromArgb(254, 215, 170);
            this.pnlHistDebtBox.BorderRadius = 8;
            this.pnlHistDebtBox.BorderSize = 1;
            this.pnlHistDebtBox.Controls.Add(this.lblHistDebtHint);
            this.pnlHistDebtBox.FillColor = System.Drawing.Color.FromArgb(255, 247, 237);
            this.pnlHistDebtBox.Location = new System.Drawing.Point(0, 222);
            this.pnlHistDebtBox.Name = "pnlHistDebtBox";
            this.pnlHistDebtBox.Size = new System.Drawing.Size(480, 36);
            this.pnlHistDebtBox.TabIndex = 5;

            this.lblHistDebtHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHistDebtHint.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblHistDebtHint.ForeColor = System.Drawing.Color.FromArgb(180, 83, 9);
            this.lblHistDebtHint.Location = new System.Drawing.Point(0, 0);
            this.lblHistDebtHint.Name = "lblHistDebtHint";
            this.lblHistDebtHint.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblHistDebtHint.Size = new System.Drawing.Size(480, 36);
            this.lblHistDebtHint.Text = "⚠  Số nợ được tính tự động từ khoản vay + lãi suất × thời gian vay";
            this.lblHistDebtHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // btnPayLoan
            this.btnPayLoan.BorderRadius = 8;
            this.btnPayLoan.BorderSize = 0;
            this.btnPayLoan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPayLoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayLoan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnPayLoan.ForeColor = System.Drawing.Color.White;
            this.btnPayLoan.HoverBorder = System.Drawing.Color.Transparent;
            this.btnPayLoan.HoverColor = System.Drawing.Color.FromArgb(21, 128, 61);
            this.btnPayLoan.Location = new System.Drawing.Point(170, 485);
            this.btnPayLoan.Name = "btnPayLoan";
            this.btnPayLoan.NormalBorder = System.Drawing.Color.Transparent;
            this.btnPayLoan.NormalColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.btnPayLoan.PressedBorder = System.Drawing.Color.Transparent;
            this.btnPayLoan.PressedColor = System.Drawing.Color.FromArgb(20, 83, 45);
            this.btnPayLoan.Size = new System.Drawing.Size(195, 36);
            this.btnPayLoan.TabIndex = 5;
            this.btnPayLoan.Text = "💳 Thanh toán khoản vay";
            this.btnPayLoan.TextColor = System.Drawing.Color.White;
            this.btnPayLoan.UseVisualStyleBackColor = false;

            // btnCloseHistory
            this.btnCloseHistory.BorderRadius = 8;
            this.btnCloseHistory.BorderSize = 1;
            this.btnCloseHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseHistory.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCloseHistory.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.btnCloseHistory.HoverBorder = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnCloseHistory.HoverColor = System.Drawing.Color.FromArgb(253, 242, 248);
            this.btnCloseHistory.Location = new System.Drawing.Point(380, 485);
            this.btnCloseHistory.Name = "btnCloseHistory";
            this.btnCloseHistory.NormalBorder = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnCloseHistory.NormalColor = System.Drawing.Color.White;
            this.btnCloseHistory.PressedColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.btnCloseHistory.Size = new System.Drawing.Size(120, 36);
            this.btnCloseHistory.TabIndex = 6;
            this.btnCloseHistory.Text = "Đóng";
            this.btnCloseHistory.TextColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.btnCloseHistory.UseVisualStyleBackColor = false;

            // =============================================================
            // BorrowingUserForm
            // =============================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize = new System.Drawing.Size(1200, 765);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BorrowingUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vay nhanh";

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.pnlLeftCard.ResumeLayout(false);
            this.pnlLeftCard.PerformLayout();
            this.pnlVayInputBox.ResumeLayout(false);
            this.pnlVayInputBox.PerformLayout();
            this.pnlTimeInputBox.ResumeLayout(false);
            this.pnlTimeInputBox.PerformLayout();
            this.pnlRightCard.ResumeLayout(false);
            this.pnlRightCard.PerformLayout();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.pnlDebtInputBox.ResumeLayout(false);
            this.pnlDebtInputBox.PerformLayout();
            this.pnlDebtBox.ResumeLayout(false);
            this.pnlHistDebtBox.ResumeLayout(false);
            this.pnlHistDebtInputBox.ResumeLayout(false);
            this.pnlHistDebtInputBox.PerformLayout();
            this.pnlHistSummary.ResumeLayout(false);
            this.pnlHistSummary.PerformLayout();
            this.pnlHistDetailCard.ResumeLayout(false);
            this.pnlHistDetailCard.PerformLayout();
            this.pnlHistGridWrapper.ResumeLayout(false);
            this.pnlLoanHistoryView.ResumeLayout(false);
            this.pnlLoanHistoryView.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSub;
        private System.Windows.Forms.Label lblAccountSelect;
        private System.Windows.Forms.ComboBox cboAccount;
        private BankAccountManagement.Forms.RoundedButton btnHistory;

        private System.Windows.Forms.Panel pnlFooter;
        private BankAccountManagement.Forms.RoundedButton btnRegister;

        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TableLayoutPanel tlpMain;

        // Left card
        private BankAccountManagement.Forms.RoundedPanel pnlLeftCard;
        private System.Windows.Forms.Label lblVayTitle;
        private BankAccountManagement.Forms.RoundedPanel pnlVayInputBox;
        private System.Windows.Forms.TextBox txtVayAmount;
        private System.Windows.Forms.Label lblVayUnit;
        private BankAccountManagement.Forms.RoundedPanel trackVayBg;
        private BankAccountManagement.Forms.RoundedPanel trackVayFill;
        private BankAccountManagement.Forms.RoundedPanel thumbVay;
        private System.Windows.Forms.Label lblVayMin;
        private System.Windows.Forms.Label lblVayMax;

        private System.Windows.Forms.Label lblTimeTitle;
        private BankAccountManagement.Forms.RoundedPanel pnlTimeInputBox;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Label lblTimeUnit;
        private BankAccountManagement.Forms.RoundedPanel trackTimeBg;
        private BankAccountManagement.Forms.RoundedPanel trackTimeFill;
        private BankAccountManagement.Forms.RoundedPanel thumbTime;
        private System.Windows.Forms.Label lblTimeMin;
        private System.Windows.Forms.Label lblTimeMax;

        // Right card
        private BankAccountManagement.Forms.RoundedPanel pnlRightCard;
        private BankAccountManagement.Forms.RoundedPanel pnlSummary;
        private System.Windows.Forms.Label lblSum1Value;
        private System.Windows.Forms.Label lblSum1Title;
        private System.Windows.Forms.Label lblSum2Value;
        private System.Windows.Forms.Label lblSum2Title;
        private System.Windows.Forms.Label lblSum3Value;
        private System.Windows.Forms.Label lblSum3Title;
        private System.Windows.Forms.Label lblSumInterestDetail;

        private BankAccountManagement.Forms.RoundedPanel pnlRedDot;
        private System.Windows.Forms.Label lblPayTitle;
        private System.Windows.Forms.Label lblPayDesc;

        private System.Windows.Forms.Label lblDebtTitle;
        private BankAccountManagement.Forms.RoundedPanel pnlDebtInputBox;
        private System.Windows.Forms.TextBox txtDebtAmount;
        private System.Windows.Forms.Label lblDebtUnit;

        private BankAccountManagement.Forms.RoundedPanel pnlDebtBox;
        private System.Windows.Forms.Label lblDebtHint;

        private System.Windows.Forms.CheckBox chkAgree;
        private System.Windows.Forms.Label lblAgreeText;

        // History View (Embedded in Right Card)
        private BankAccountManagement.Forms.RoundedPanel pnlLoanHistoryView;
        private System.Windows.Forms.Label lblHistHeaderTitle;
        private System.Windows.Forms.Label lblHistHeaderSub;
        private BankAccountManagement.Forms.RoundedPanel pnlHistGridWrapper;
        private System.Windows.Forms.DataGridView dgvHistory;
        private BankAccountManagement.Forms.RoundedPanel pnlHistDetailCard;
        private BankAccountManagement.Forms.RoundedPanel pnlHistSummary;
        private System.Windows.Forms.Label lblHistSum1Value;
        private System.Windows.Forms.Label lblHistSum1Title;
        private System.Windows.Forms.Label lblHistSum2Value;
        private System.Windows.Forms.Label lblHistSum2Title;
        private System.Windows.Forms.Label lblHistSum3Value;
        private System.Windows.Forms.Label lblHistSum3Title;
        private System.Windows.Forms.Label lblHistSumInterestDetail;
        private BankAccountManagement.Forms.RoundedPanel pnlHistRedDot;
        private System.Windows.Forms.Label lblHistPayTitle;
        private System.Windows.Forms.Label lblHistPayDesc;
        private System.Windows.Forms.Label lblHistDebtTitle;
        private BankAccountManagement.Forms.RoundedPanel pnlHistDebtInputBox;
        private System.Windows.Forms.TextBox txtHistDebtAmount;
        private System.Windows.Forms.Label lblHistDebtUnit;
        private BankAccountManagement.Forms.RoundedPanel pnlHistDebtBox;
        private System.Windows.Forms.Label lblHistDebtHint;
        private BankAccountManagement.Forms.RoundedButton btnPayLoan;
        private BankAccountManagement.Forms.RoundedButton btnCloseHistory;
    }
}
