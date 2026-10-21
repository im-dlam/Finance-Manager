namespace BankAccountManagement.Forms
{
    partial class SignupForm
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
            this.pnlSide = new System.Windows.Forms.Panel();
            this.lblSideLogo = new System.Windows.Forms.Label();
            this.lblSideTitle = new System.Windows.Forms.Label();
            this.lblSideDesc = new System.Windows.Forms.Label();
            this.lblSideFooter = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblBrand = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblNameCaption = new System.Windows.Forms.Label();
            this.pnlName = new System.Windows.Forms.Panel();
            this.lblNameIcon = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.pnlNameLine = new System.Windows.Forms.Panel();
            this.lblUserCaption = new System.Windows.Forms.Label();
            this.pnlUser = new System.Windows.Forms.Panel();
            this.lblUserIcon = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.pnlUserLine = new System.Windows.Forms.Panel();
            this.lblBankCaption = new System.Windows.Forms.Label();
            this.pnlBank = new System.Windows.Forms.Panel();
            this.lblBankIcon = new System.Windows.Forms.Label();
            this.cboBank = new System.Windows.Forms.ComboBox();
            this.pnlBankLine = new System.Windows.Forms.Panel();
            this.lblEmailCaption = new System.Windows.Forms.Label();
            this.pnlEmail = new System.Windows.Forms.Panel();
            this.lblEmailIcon = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.pnlEmailLine = new System.Windows.Forms.Panel();
            this.lblPassCaption = new System.Windows.Forms.Label();
            this.pnlPass = new System.Windows.Forms.Panel();
            this.btnTogglePass = new System.Windows.Forms.Button();
            this.lblPassIcon = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.pnlPassLine = new System.Windows.Forms.Panel();
            this.lblConfirmCaption = new System.Windows.Forms.Label();
            this.pnlConfirm = new System.Windows.Forms.Panel();
            this.btnToggleConfirm = new System.Windows.Forms.Button();
            this.lblConfirmIcon = new System.Windows.Forms.Label();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.pnlConfirmLine = new System.Windows.Forms.Panel();
            this.chkTerms = new System.Windows.Forms.CheckBox();
            this.lblError = new System.Windows.Forms.Label();
            this.btnSignup = new System.Windows.Forms.Button();
            this.lblFooter = new System.Windows.Forms.Label();
            this.linkLogin = new System.Windows.Forms.LinkLabel();
            this.pnlSide.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlName.SuspendLayout();
            this.pnlUser.SuspendLayout();
            this.pnlBank.SuspendLayout();
            this.pnlEmail.SuspendLayout();
            this.pnlPass.SuspendLayout();
            this.pnlConfirm.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSide
            // 
            this.pnlSide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(252)))));
            this.pnlSide.Controls.Add(this.lblSideLogo);
            this.pnlSide.Controls.Add(this.lblSideTitle);
            this.pnlSide.Controls.Add(this.lblSideDesc);
            this.pnlSide.Controls.Add(this.lblSideFooter);
            this.pnlSide.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSide.Location = new System.Drawing.Point(0, 0);
            this.pnlSide.Name = "pnlSide";
            this.pnlSide.Size = new System.Drawing.Size(420, 800);
            this.pnlSide.TabIndex = 0;
            this.pnlSide.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlSide_Paint);
            // 
            // lblSideLogo
            // 
            this.lblSideLogo.BackColor = System.Drawing.Color.Transparent;
            this.lblSideLogo.Font = new System.Drawing.Font("Segoe UI Emoji", 42F);
            this.lblSideLogo.ForeColor = System.Drawing.Color.White;
            this.lblSideLogo.Location = new System.Drawing.Point(50, 180);
            this.lblSideLogo.Name = "lblSideLogo";
            this.lblSideLogo.Size = new System.Drawing.Size(100, 90);
            this.lblSideLogo.TabIndex = 0;
            this.lblSideLogo.Text = "🏛";
            // 
            // lblSideTitle
            // 
            this.lblSideTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSideTitle.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblSideTitle.ForeColor = System.Drawing.Color.White;
            this.lblSideTitle.Location = new System.Drawing.Point(56, 280);
            this.lblSideTitle.Name = "lblSideTitle";
            this.lblSideTitle.Size = new System.Drawing.Size(320, 80);
            this.lblSideTitle.TabIndex = 1;
            this.lblSideTitle.Text = "Join Bank\r\nManagement";
            // 
            // lblSideDesc
            // 
            this.lblSideDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblSideDesc.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblSideDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.lblSideDesc.Location = new System.Drawing.Point(60, 380);
            this.lblSideDesc.Name = "lblSideDesc";
            this.lblSideDesc.Size = new System.Drawing.Size(310, 90);
            this.lblSideDesc.TabIndex = 2;
            this.lblSideDesc.Text = "Create your account in seconds and\r\nstart managing your money with a\r\nsecure, mod" +
    "ern banking experience.";
            // 
            // lblSideFooter
            // 
            this.lblSideFooter.BackColor = System.Drawing.Color.Transparent;
            this.lblSideFooter.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSideFooter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(160)))), ((int)(((byte)(220)))));
            this.lblSideFooter.Location = new System.Drawing.Point(60, 750);
            this.lblSideFooter.Name = "lblSideFooter";
            this.lblSideFooter.Size = new System.Drawing.Size(310, 20);
            this.lblSideFooter.TabIndex = 3;
            this.lblSideFooter.Text = "© 2025 Bank Management. All rights reserved.";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.btnClose);
            this.pnlMain.Controls.Add(this.lblBrand);
            this.pnlMain.Controls.Add(this.lblWelcome);
            this.pnlMain.Controls.Add(this.lblSubtitle);
            this.pnlMain.Controls.Add(this.lblNameCaption);
            this.pnlMain.Controls.Add(this.pnlName);
            this.pnlMain.Controls.Add(this.lblUserCaption);
            this.pnlMain.Controls.Add(this.pnlUser);
            this.pnlMain.Controls.Add(this.lblBankCaption);
            this.pnlMain.Controls.Add(this.pnlBank);
            this.pnlMain.Controls.Add(this.lblEmailCaption);
            this.pnlMain.Controls.Add(this.pnlEmail);
            this.pnlMain.Controls.Add(this.lblPassCaption);
            this.pnlMain.Controls.Add(this.pnlPass);
            this.pnlMain.Controls.Add(this.lblConfirmCaption);
            this.pnlMain.Controls.Add(this.pnlConfirm);
            this.pnlMain.Controls.Add(this.chkTerms);
            this.pnlMain.Controls.Add(this.lblError);
            this.pnlMain.Controls.Add(this.btnSignup);
            this.pnlMain.Controls.Add(this.lblFooter);
            this.pnlMain.Controls.Add(this.linkLogin);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(420, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(540, 800);
            this.pnlMain.TabIndex = 1;
            this.pnlMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragForm);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(90)))), ((int)(((byte)(110)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(180)))));
            this.btnClose.Location = new System.Drawing.Point(494, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(34, 34);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "✕";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lblBrand
            // 
            this.lblBrand.AutoSize = true;
            this.lblBrand.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBrand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(252)))));
            this.lblBrand.Location = new System.Drawing.Point(75, 26);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(205, 21);
            this.lblBrand.TabIndex = 1;
            this.lblBrand.Text = "🏦  BANK MANAGEMENT";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 21F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
            this.lblWelcome.Location = new System.Drawing.Point(72, 62);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(217, 38);
            this.lblWelcome.TabIndex = 2;
            this.lblWelcome.Text = "Create account";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(135)))), ((int)(((byte)(150)))));
            this.lblSubtitle.Location = new System.Drawing.Point(75, 105);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(216, 19);
            this.lblSubtitle.TabIndex = 3;
            this.lblSubtitle.Text = "Fill in the form below to get started";
            // 
            // lblNameCaption
            // 
            this.lblNameCaption.AutoSize = true;
            this.lblNameCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblNameCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(90)))));
            this.lblNameCaption.Location = new System.Drawing.Point(75, 142);
            this.lblNameCaption.Name = "lblNameCaption";
            this.lblNameCaption.Size = new System.Drawing.Size(78, 17);
            this.lblNameCaption.TabIndex = 4;
            this.lblNameCaption.Text = "FULL NAME";
            // 
            // pnlName
            // 
            this.pnlName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.pnlName.Controls.Add(this.lblNameIcon);
            this.pnlName.Controls.Add(this.txtFullName);
            this.pnlName.Controls.Add(this.pnlNameLine);
            this.pnlName.Location = new System.Drawing.Point(75, 164);
            this.pnlName.Name = "pnlName";
            this.pnlName.Size = new System.Drawing.Size(390, 42);
            this.pnlName.TabIndex = 5;
            // 
            // lblNameIcon
            // 
            this.lblNameIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 13F);
            this.lblNameIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(145)))), ((int)(((byte)(160)))));
            this.lblNameIcon.Location = new System.Drawing.Point(12, 0);
            this.lblNameIcon.Name = "lblNameIcon";
            this.lblNameIcon.Size = new System.Drawing.Size(36, 40);
            this.lblNameIcon.TabIndex = 0;
            this.lblNameIcon.Text = "📝";
            this.lblNameIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFullName
            // 
            this.txtFullName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.txtFullName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.txtFullName.Location = new System.Drawing.Point(54, 10);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(320, 22);
            this.txtFullName.TabIndex = 1;
            // 
            // pnlNameLine
            // 
            this.pnlNameLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(222)))), ((int)(((byte)(230)))));
            this.pnlNameLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNameLine.Location = new System.Drawing.Point(0, 40);
            this.pnlNameLine.Name = "pnlNameLine";
            this.pnlNameLine.Size = new System.Drawing.Size(390, 2);
            this.pnlNameLine.TabIndex = 2;
            // 
            // lblUserCaption
            // 
            this.lblUserCaption.AutoSize = true;
            this.lblUserCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblUserCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(90)))));
            this.lblUserCaption.Location = new System.Drawing.Point(75, 212);
            this.lblUserCaption.Name = "lblUserCaption";
            this.lblUserCaption.Size = new System.Drawing.Size(77, 17);
            this.lblUserCaption.TabIndex = 6;
            this.lblUserCaption.Text = "USERNAME";
            // 
            // pnlUser
            // 
            this.pnlUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.pnlUser.Controls.Add(this.lblUserIcon);
            this.pnlUser.Controls.Add(this.txtUsername);
            this.pnlUser.Controls.Add(this.pnlUserLine);
            this.pnlUser.Location = new System.Drawing.Point(75, 234);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(390, 42);
            this.pnlUser.TabIndex = 7;
            // 
            // lblUserIcon
            // 
            this.lblUserIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 13F);
            this.lblUserIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(145)))), ((int)(((byte)(160)))));
            this.lblUserIcon.Location = new System.Drawing.Point(12, 0);
            this.lblUserIcon.Name = "lblUserIcon";
            this.lblUserIcon.Size = new System.Drawing.Size(36, 40);
            this.lblUserIcon.TabIndex = 0;
            this.lblUserIcon.Text = "👤";
            this.lblUserIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.txtUsername.Location = new System.Drawing.Point(54, 10);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(320, 22);
            this.txtUsername.TabIndex = 1;
            // 
            // pnlUserLine
            // 
            this.pnlUserLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(222)))), ((int)(((byte)(230)))));
            this.pnlUserLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlUserLine.Location = new System.Drawing.Point(0, 40);
            this.pnlUserLine.Name = "pnlUserLine";
            this.pnlUserLine.Size = new System.Drawing.Size(390, 2);
            this.pnlUserLine.TabIndex = 2;
            // 
            // lblBankCaption
            // 
            this.lblBankCaption.AutoSize = true;
            this.lblBankCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblBankCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(90)))));
            this.lblBankCaption.Location = new System.Drawing.Point(75, 282);
            this.lblBankCaption.Name = "lblBankCaption";
            this.lblBankCaption.Size = new System.Drawing.Size(130, 17);
            this.lblBankCaption.TabIndex = 8;
            this.lblBankCaption.Text = "NGÂN HÀNG (BANK)";
            // 
            // pnlBank
            // 
            this.pnlBank.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.pnlBank.Controls.Add(this.lblBankIcon);
            this.pnlBank.Controls.Add(this.cboBank);
            this.pnlBank.Controls.Add(this.pnlBankLine);
            this.pnlBank.Location = new System.Drawing.Point(75, 304);
            this.pnlBank.Name = "pnlBank";
            this.pnlBank.Size = new System.Drawing.Size(390, 42);
            this.pnlBank.TabIndex = 9;
            // 
            // lblBankIcon
            // 
            this.lblBankIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 13F);
            this.lblBankIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(145)))), ((int)(((byte)(160)))));
            this.lblBankIcon.Location = new System.Drawing.Point(12, 0);
            this.lblBankIcon.Name = "lblBankIcon";
            this.lblBankIcon.Size = new System.Drawing.Size(36, 40);
            this.lblBankIcon.TabIndex = 0;
            this.lblBankIcon.Text = "🏦";
            this.lblBankIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboBank
            // 
            this.cboBank.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.cboBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBank.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboBank.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboBank.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.cboBank.FormattingEnabled = true;
            this.cboBank.Location = new System.Drawing.Point(54, 7);
            this.cboBank.Name = "cboBank";
            this.cboBank.Size = new System.Drawing.Size(324, 28);
            this.cboBank.TabIndex = 1;
            // 
            // pnlBankLine
            // 
            this.pnlBankLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(222)))), ((int)(((byte)(230)))));
            this.pnlBankLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBankLine.Location = new System.Drawing.Point(0, 40);
            this.pnlBankLine.Name = "pnlBankLine";
            this.pnlBankLine.Size = new System.Drawing.Size(390, 2);
            this.pnlBankLine.TabIndex = 2;
            // 
            // lblEmailCaption
            // 
            this.lblEmailCaption.AutoSize = true;
            this.lblEmailCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblEmailCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(90)))));
            this.lblEmailCaption.Location = new System.Drawing.Point(75, 352);
            this.lblEmailCaption.Name = "lblEmailCaption";
            this.lblEmailCaption.Size = new System.Drawing.Size(47, 17);
            this.lblEmailCaption.TabIndex = 10;
            this.lblEmailCaption.Text = "EMAIL";
            // 
            // pnlEmail
            // 
            this.pnlEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.pnlEmail.Controls.Add(this.lblEmailIcon);
            this.pnlEmail.Controls.Add(this.txtEmail);
            this.pnlEmail.Controls.Add(this.pnlEmailLine);
            this.pnlEmail.Location = new System.Drawing.Point(75, 374);
            this.pnlEmail.Name = "pnlEmail";
            this.pnlEmail.Size = new System.Drawing.Size(390, 42);
            this.pnlEmail.TabIndex = 11;
            // 
            // lblEmailIcon
            // 
            this.lblEmailIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 13F);
            this.lblEmailIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(145)))), ((int)(((byte)(160)))));
            this.lblEmailIcon.Location = new System.Drawing.Point(12, 0);
            this.lblEmailIcon.Name = "lblEmailIcon";
            this.lblEmailIcon.Size = new System.Drawing.Size(36, 40);
            this.lblEmailIcon.TabIndex = 0;
            this.lblEmailIcon.Text = "✉";
            this.lblEmailIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.txtEmail.Location = new System.Drawing.Point(54, 10);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(320, 22);
            this.txtEmail.TabIndex = 1;
            // 
            // pnlEmailLine
            // 
            this.pnlEmailLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(222)))), ((int)(((byte)(230)))));
            this.pnlEmailLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlEmailLine.Location = new System.Drawing.Point(0, 40);
            this.pnlEmailLine.Name = "pnlEmailLine";
            this.pnlEmailLine.Size = new System.Drawing.Size(390, 2);
            this.pnlEmailLine.TabIndex = 2;
            // 
            // lblPassCaption
            // 
            this.lblPassCaption.AutoSize = true;
            this.lblPassCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPassCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(90)))));
            this.lblPassCaption.Location = new System.Drawing.Point(75, 422);
            this.lblPassCaption.Name = "lblPassCaption";
            this.lblPassCaption.Size = new System.Drawing.Size(79, 17);
            this.lblPassCaption.TabIndex = 12;
            this.lblPassCaption.Text = "PASSWORD";
            // 
            // pnlPass
            // 
            this.pnlPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.pnlPass.Controls.Add(this.btnTogglePass);
            this.pnlPass.Controls.Add(this.lblPassIcon);
            this.pnlPass.Controls.Add(this.txtPassword);
            this.pnlPass.Controls.Add(this.pnlPassLine);
            this.pnlPass.Location = new System.Drawing.Point(75, 444);
            this.pnlPass.Name = "pnlPass";
            this.pnlPass.Size = new System.Drawing.Size(390, 42);
            this.pnlPass.TabIndex = 13;
            // 
            // btnTogglePass
            // 
            this.btnTogglePass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.btnTogglePass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTogglePass.FlatAppearance.BorderSize = 0;
            this.btnTogglePass.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.btnTogglePass.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.btnTogglePass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTogglePass.Font = new System.Drawing.Font("Segoe UI Emoji", 10F);
            this.btnTogglePass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(145)))), ((int)(((byte)(160)))));
            this.btnTogglePass.Location = new System.Drawing.Point(348, 3);
            this.btnTogglePass.Name = "btnTogglePass";
            this.btnTogglePass.Size = new System.Drawing.Size(36, 36);
            this.btnTogglePass.TabIndex = 2;
            this.btnTogglePass.Text = "👁";
            this.btnTogglePass.UseVisualStyleBackColor = false;
            // 
            // lblPassIcon
            // 
            this.lblPassIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 13F);
            this.lblPassIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(145)))), ((int)(((byte)(160)))));
            this.lblPassIcon.Location = new System.Drawing.Point(12, 0);
            this.lblPassIcon.Name = "lblPassIcon";
            this.lblPassIcon.Size = new System.Drawing.Size(36, 40);
            this.lblPassIcon.TabIndex = 0;
            this.lblPassIcon.Text = "🔒";
            this.lblPassIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.txtPassword.Location = new System.Drawing.Point(54, 10);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(280, 22);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // pnlPassLine
            // 
            this.pnlPassLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(222)))), ((int)(((byte)(230)))));
            this.pnlPassLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPassLine.Location = new System.Drawing.Point(0, 40);
            this.pnlPassLine.Name = "pnlPassLine";
            this.pnlPassLine.Size = new System.Drawing.Size(390, 2);
            this.pnlPassLine.TabIndex = 3;
            // 
            // lblConfirmCaption
            // 
            this.lblConfirmCaption.AutoSize = true;
            this.lblConfirmCaption.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblConfirmCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(90)))));
            this.lblConfirmCaption.Location = new System.Drawing.Point(75, 492);
            this.lblConfirmCaption.Name = "lblConfirmCaption";
            this.lblConfirmCaption.Size = new System.Drawing.Size(142, 17);
            this.lblConfirmCaption.TabIndex = 14;
            this.lblConfirmCaption.Text = "CONFIRM PASSWORD";
            // 
            // pnlConfirm
            // 
            this.pnlConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.pnlConfirm.Controls.Add(this.btnToggleConfirm);
            this.pnlConfirm.Controls.Add(this.lblConfirmIcon);
            this.pnlConfirm.Controls.Add(this.txtConfirm);
            this.pnlConfirm.Controls.Add(this.pnlConfirmLine);
            this.pnlConfirm.Location = new System.Drawing.Point(75, 514);
            this.pnlConfirm.Name = "pnlConfirm";
            this.pnlConfirm.Size = new System.Drawing.Size(390, 42);
            this.pnlConfirm.TabIndex = 15;
            // 
            // btnToggleConfirm
            // 
            this.btnToggleConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.btnToggleConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToggleConfirm.FlatAppearance.BorderSize = 0;
            this.btnToggleConfirm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.btnToggleConfirm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(238)))), ((int)(((byte)(245)))));
            this.btnToggleConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleConfirm.Font = new System.Drawing.Font("Segoe UI Emoji", 10F);
            this.btnToggleConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(145)))), ((int)(((byte)(160)))));
            this.btnToggleConfirm.Location = new System.Drawing.Point(348, 3);
            this.btnToggleConfirm.Name = "btnToggleConfirm";
            this.btnToggleConfirm.Size = new System.Drawing.Size(36, 36);
            this.btnToggleConfirm.TabIndex = 2;
            this.btnToggleConfirm.Text = "👁";
            this.btnToggleConfirm.UseVisualStyleBackColor = false;
            // 
            // lblConfirmIcon
            // 
            this.lblConfirmIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 13F);
            this.lblConfirmIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(145)))), ((int)(((byte)(160)))));
            this.lblConfirmIcon.Location = new System.Drawing.Point(12, 0);
            this.lblConfirmIcon.Name = "lblConfirmIcon";
            this.lblConfirmIcon.Size = new System.Drawing.Size(36, 40);
            this.lblConfirmIcon.TabIndex = 0;
            this.lblConfirmIcon.Text = "🔐";
            this.lblConfirmIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtConfirm
            // 
            this.txtConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.txtConfirm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConfirm.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.txtConfirm.Location = new System.Drawing.Point(54, 10);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new System.Drawing.Size(280, 22);
            this.txtConfirm.TabIndex = 1;
            this.txtConfirm.UseSystemPasswordChar = true;
            // 
            // pnlConfirmLine
            // 
            this.pnlConfirmLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(222)))), ((int)(((byte)(230)))));
            this.pnlConfirmLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlConfirmLine.Location = new System.Drawing.Point(0, 40);
            this.pnlConfirmLine.Name = "pnlConfirmLine";
            this.pnlConfirmLine.Size = new System.Drawing.Size(390, 2);
            this.pnlConfirmLine.TabIndex = 3;
            // 
            // chkTerms
            // 
            this.chkTerms.AutoSize = true;
            this.chkTerms.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkTerms.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkTerms.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(95)))), ((int)(((byte)(115)))));
            this.chkTerms.Location = new System.Drawing.Point(78, 575);
            this.chkTerms.Name = "chkTerms";
            this.chkTerms.Size = new System.Drawing.Size(313, 21);
            this.chkTerms.TabIndex = 16;
            this.chkTerms.Text = "I agree to the Terms of Service and Privacy Policy";
            this.chkTerms.UseVisualStyleBackColor = true;
            // 
            // lblError
            // 
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblError.Location = new System.Drawing.Point(75, 606);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(390, 20);
            this.lblError.TabIndex = 17;
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblError.Visible = false;
            // 
            // btnSignup
            // 
            this.btnSignup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(252)))));
            this.btnSignup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSignup.FlatAppearance.BorderSize = 0;
            this.btnSignup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(0)))), ((int)(((byte)(200)))));
            this.btnSignup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(0)))), ((int)(((byte)(230)))));
            this.btnSignup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSignup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnSignup.ForeColor = System.Drawing.Color.White;
            this.btnSignup.Location = new System.Drawing.Point(75, 638);
            this.btnSignup.Name = "btnSignup";
            this.btnSignup.Size = new System.Drawing.Size(390, 48);
            this.btnSignup.TabIndex = 18;
            this.btnSignup.Text = "SIGN UP";
            this.btnSignup.UseVisualStyleBackColor = false;
            // 
            // lblFooter
            // 
            this.lblFooter.AutoSize = true;
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFooter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(135)))), ((int)(((byte)(150)))));
            this.lblFooter.Location = new System.Drawing.Point(165, 715);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(156, 17);
            this.lblFooter.TabIndex = 19;
            this.lblFooter.Text = "Already have an account?";
            // 
            // linkLogin
            // 
            this.linkLogin.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(200)))));
            this.linkLogin.AutoSize = true;
            this.linkLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLogin.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.linkLogin.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLogin.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(252)))));
            this.linkLogin.Location = new System.Drawing.Point(330, 715);
            this.linkLogin.Name = "linkLogin";
            this.linkLogin.Size = new System.Drawing.Size(51, 17);
            this.linkLogin.TabIndex = 20;
            this.linkLogin.TabStop = true;
            this.linkLogin.Text = "Sign in";
            this.linkLogin.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(252)))));
            // 
            // SignupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(960, 800);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlSide);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SignupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bank Management - Signup";
            this.pnlSide.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlName.ResumeLayout(false);
            this.pnlName.PerformLayout();
            this.pnlUser.ResumeLayout(false);
            this.pnlUser.PerformLayout();
            this.pnlBank.ResumeLayout(false);
            this.pnlEmail.ResumeLayout(false);
            this.pnlEmail.PerformLayout();
            this.pnlPass.ResumeLayout(false);
            this.pnlPass.PerformLayout();
            this.pnlConfirm.ResumeLayout(false);
            this.pnlConfirm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSide;
        private System.Windows.Forms.Label lblSideLogo;
        private System.Windows.Forms.Label lblSideTitle;
        private System.Windows.Forms.Label lblSideDesc;
        private System.Windows.Forms.Label lblSideFooter;

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblSubtitle;

        private System.Windows.Forms.Label lblNameCaption;
        private System.Windows.Forms.Panel pnlName;
        private System.Windows.Forms.Label lblNameIcon;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Panel pnlNameLine;

        private System.Windows.Forms.Label lblUserCaption;
        private System.Windows.Forms.Panel pnlUser;
        private System.Windows.Forms.Label lblUserIcon;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Panel pnlUserLine;

        private System.Windows.Forms.Label lblBankCaption;
        private System.Windows.Forms.Panel pnlBank;
        private System.Windows.Forms.Label lblBankIcon;
        private System.Windows.Forms.ComboBox cboBank;
        private System.Windows.Forms.Panel pnlBankLine;

        private System.Windows.Forms.Label lblEmailCaption;
        private System.Windows.Forms.Panel pnlEmail;
        private System.Windows.Forms.Label lblEmailIcon;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Panel pnlEmailLine;

        private System.Windows.Forms.Label lblPassCaption;
        private System.Windows.Forms.Panel pnlPass;
        private System.Windows.Forms.Label lblPassIcon;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnTogglePass;
        private System.Windows.Forms.Panel pnlPassLine;

        private System.Windows.Forms.Label lblConfirmCaption;
        private System.Windows.Forms.Panel pnlConfirm;
        private System.Windows.Forms.Label lblConfirmIcon;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.Button btnToggleConfirm;
        private System.Windows.Forms.Panel pnlConfirmLine;

        private System.Windows.Forms.CheckBox chkTerms;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnSignup;
        private System.Windows.Forms.Label lblFooter;
        private System.Windows.Forms.LinkLabel linkLogin;
    }
}
