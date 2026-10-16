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
            this.btnHistory.HoverBorder = System.Drawing.Color.FromArgb(190, 24, 93);
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
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(24, 14, 24, 18);
            this.pnlFooter.Size = new System.Drawing.Size(1200, 85);
            this.pnlFooter.TabIndex = 2;

            this.btnRegister.BorderRadius = 14;
            this.btnRegister.BorderSize = 0;
            this.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.HoverColor = System.Drawing.Color.FromArgb(219, 39, 119);
            this.btnRegister.Location = new System.Drawing.Point(24, 14);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.NormalColor = System.Drawing.Color.FromArgb(190, 24, 93);
            this.btnRegister.PressedColor = System.Drawing.Color.FromArgb(157, 23, 77);
            this.btnRegister.Size = new System.Drawing.Size(1152, 53);
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
            this.pnlLeftCard.Controls.Add(this.lblVayTitle);
            this.pnlLeftCard.Controls.Add(this.pnlVayInputBox);
            this.pnlLeftCard.Controls.Add(this.trackVayBg);
            this.pnlLeftCard.Controls.Add(this.trackVayFill);
            this.pnlLeftCard.Controls.Add(this.thumbVay);
            this.pnlLeftCard.Controls.Add(this.lblVayMin);
            this.pnlLeftCard.Controls.Add(this.lblVayMax);
            this.pnlLeftCard.Controls.Add(this.lblTimeTitle);
            this.pnlLeftCard.Controls.Add(this.pnlTimeInputBox);
            this.pnlLeftCard.Controls.Add(this.trackTimeBg);
            this.pnlLeftCard.Controls.Add(this.trackTimeFill);
            this.pnlLeftCard.Controls.Add(this.thumbTime);
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

            // Ô nhập số tiền vay bo tròn
            this.pnlVayInputBox.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.pnlVayInputBox.BorderRadius = 8;
            this.pnlVayInputBox.BorderSize = 1;
            this.pnlVayInputBox.FillColor = System.Drawing.Color.White;
            this.pnlVayInputBox.Location = new System.Drawing.Point(32, 54);
            this.pnlVayInputBox.Name = "pnlVayInputBox";
            this.pnlVayInputBox.Size = new System.Drawing.Size(320, 38);
            this.pnlVayInputBox.Controls.Add(this.txtVayAmount);
            this.pnlVayInputBox.Controls.Add(this.lblVayUnit);

            this.txtVayAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtVayAmount.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.txtVayAmount.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtVayAmount.Location = new System.Drawing.Point(12, 6);
            this.txtVayAmount.Name = "txtVayAmount";
            this.txtVayAmount.Size = new System.Drawing.Size(265, 24);
            this.txtVayAmount.Text = "1.000.000";
            this.txtVayAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            this.lblVayUnit.AutoSize = true;
            this.lblVayUnit.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblVayUnit.ForeColor = System.Drawing.Color.FromArgb(190, 24, 93);
            this.lblVayUnit.Location = new System.Drawing.Point(285, 6);
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
            this.trackVayBg.Size = new System.Drawing.Size(530, 8);

            this.trackVayFill.BorderRadius = 4;
            this.trackVayFill.BorderSize = 0;
            this.trackVayFill.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackVayFill.FillColor = System.Drawing.Color.FromArgb(190, 24, 93);
            this.trackVayFill.Location = new System.Drawing.Point(32, 126);
            this.trackVayFill.Name = "trackVayFill";
            this.trackVayFill.Size = new System.Drawing.Size(60, 8);

            this.thumbVay.BorderColor = System.Drawing.Color.White;
            this.thumbVay.BorderRadius = 12;
            this.thumbVay.BorderSize = 3;
            this.thumbVay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.thumbVay.FillColor = System.Drawing.Color.FromArgb(190, 24, 93);
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
            this.lblVayMax.Location = new System.Drawing.Point(500, 146);
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

            this.pnlTimeInputBox.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.pnlTimeInputBox.BorderRadius = 8;
            this.pnlTimeInputBox.BorderSize = 1;
            this.pnlTimeInputBox.FillColor = System.Drawing.Color.White;
            this.pnlTimeInputBox.Location = new System.Drawing.Point(32, 232);
            this.pnlTimeInputBox.Name = "pnlTimeInputBox";
            this.pnlTimeInputBox.Size = new System.Drawing.Size(320, 38);
            this.pnlTimeInputBox.Controls.Add(this.txtTime);
            this.pnlTimeInputBox.Controls.Add(this.lblTimeUnit);

            this.txtTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTime.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.txtTime.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.txtTime.Location = new System.Drawing.Point(12, 6);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(250, 24);
            this.txtTime.Text = "30";
            this.txtTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            this.lblTimeUnit.AutoSize = true;
            this.lblTimeUnit.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblTimeUnit.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTimeUnit.Location = new System.Drawing.Point(268, 7);
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
            this.trackTimeBg.Size = new System.Drawing.Size(530, 8);

            this.trackTimeFill.BorderRadius = 4;
            this.trackTimeFill.BorderSize = 0;
            this.trackTimeFill.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackTimeFill.FillColor = System.Drawing.Color.FromArgb(190, 24, 93);
            this.trackTimeFill.Location = new System.Drawing.Point(32, 304);
            this.trackTimeFill.Name = "trackTimeFill";
            this.trackTimeFill.Size = new System.Drawing.Size(500, 8);

            this.thumbTime.BorderColor = System.Drawing.Color.White;
            this.thumbTime.BorderRadius = 12;
            this.thumbTime.BorderSize = 3;
            this.thumbTime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.thumbTime.FillColor = System.Drawing.Color.FromArgb(190, 24, 93);
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
            this.lblTimeMax.Location = new System.Drawing.Point(515, 324);
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
            this.lblSum2Value.ForeColor = System.Drawing.Color.FromArgb(190, 24, 93);
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
            this.lblSumInterestDetail.ForeColor = System.Drawing.Color.FromArgb(190, 24, 93);
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
            this.txtDebtAmount.ForeColor = System.Drawing.Color.FromArgb(190, 24, 93);
            this.txtDebtAmount.Location = new System.Drawing.Point(12, 7);
            this.txtDebtAmount.Name = "txtDebtAmount";
            this.txtDebtAmount.ReadOnly = true;
            this.txtDebtAmount.Size = new System.Drawing.Size(415, 29);
            this.txtDebtAmount.Text = "1.465.000";
            this.txtDebtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            this.lblDebtUnit.AutoSize = true;
            this.lblDebtUnit.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblDebtUnit.ForeColor = System.Drawing.Color.FromArgb(190, 24, 93);
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
    }
}
