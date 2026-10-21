namespace BankAccountManagement.Forms
{
    partial class LoginForm
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
            this.lblUserCaption = new System.Windows.Forms.Label();
            this.pnlUser = new System.Windows.Forms.Panel();
            this.lblUserIcon = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.pnlUserLine = new System.Windows.Forms.Panel();
            this.lblPassCaption = new System.Windows.Forms.Label();
            this.pnlPass = new System.Windows.Forms.Panel();
            this.btnTogglePass = new System.Windows.Forms.Button();
            this.lblPassIcon = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.pnlPassLine = new System.Windows.Forms.Panel();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.lblError = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblFooter = new System.Windows.Forms.Label();
            this.linkSignup = new System.Windows.Forms.LinkLabel();
            this.pnlSide.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlUser.SuspendLayout();
            this.pnlPass.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSide
            // 
            this.pnlSide.Controls.Add(this.lblSideLogo);
            this.pnlSide.Controls.Add(this.lblSideTitle);
            this.pnlSide.Controls.Add(this.lblSideDesc);
            this.pnlSide.Controls.Add(this.lblSideFooter);
            this.pnlSide.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSide.Location = new System.Drawing.Point(0, 0);
            this.pnlSide.Name = "pnlSide";
            this.pnlSide.Size = new System.Drawing.Size(420, 600);
            this.pnlSide.TabIndex = 0;
            this.pnlSide.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlSide_Paint);
            this.pnlSide.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragForm);
            // 
            // lblSideLogo
            // 
            this.lblSideLogo.BackColor = System.Drawing.Color.Transparent;
            this.lblSideLogo.Font = new System.Drawing.Font("Segoe UI Emoji", 44F, System.Drawing.FontStyle.Bold);
            this.lblSideLogo.ForeColor = System.Drawing.Color.White;
            this.lblSideLogo.Location = new System.Drawing.Point(60, 150);
            this.lblSideLogo.Name = "lblSideLogo";
            this.lblSideLogo.Size = new System.Drawing.Size(90, 90);
            this.lblSideLogo.TabIndex = 0;
            this.lblSideLogo.Text = "🏦";
            this.lblSideLogo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSideTitle
            // 
            this.lblSideTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSideTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSideTitle.ForeColor = System.Drawing.Color.White;
            this.lblSideTitle.Location = new System.Drawing.Point(56, 260);
            this.lblSideTitle.Name = "lblSideTitle";
            this.lblSideTitle.Size = new System.Drawing.Size(320, 80);
            this.lblSideTitle.TabIndex = 1;
            this.lblSideTitle.Text = "Bank\r\nManagement";
            // 
            // lblSideDesc
            // 
            this.lblSideDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblSideDesc.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblSideDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.lblSideDesc.Location = new System.Drawing.Point(60, 350);
            this.lblSideDesc.Name = "lblSideDesc";
            this.lblSideDesc.Size = new System.Drawing.Size(310, 60);
            this.lblSideDesc.TabIndex = 2;
            this.lblSideDesc.Text = "Manage your accounts, transfer money,\r\nand track your transactions securely.";
            // 
            // lblSideFooter
            // 
            this.lblSideFooter.BackColor = System.Drawing.Color.Transparent;
            this.lblSideFooter.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSideFooter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(160)))), ((int)(((byte)(220)))));
            this.lblSideFooter.Location = new System.Drawing.Point(60, 550);
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
            this.pnlMain.Controls.Add(this.lblUserCaption);
            this.pnlMain.Controls.Add(this.pnlUser);
            this.pnlMain.Controls.Add(this.lblPassCaption);
            this.pnlMain.Controls.Add(this.pnlPass);
            this.pnlMain.Controls.Add(this.chkRemember);
            this.pnlMain.Controls.Add(this.lblError);
            this.pnlMain.Controls.Add(this.btnLogin);
            this.pnlMain.Controls.Add(this.lblFooter);
            this.pnlMain.Controls.Add(this.linkSignup);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(420, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(540, 600);
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
            this.lblBrand.Location = new System.Drawing.Point(75, 40);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(205, 21);
            this.lblBrand.TabIndex = 1;
            this.lblBrand.Text = "🏦  BANK MANAGEMENT";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(45)))));
            this.lblWelcome.Location = new System.Drawing.Point(72, 110);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(221, 41);
            this.lblWelcome.TabIndex = 2;
            this.lblWelcome.Text = "Welcome back";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(135)))), ((int)(((byte)(150)))));
            this.lblSubtitle.Location = new System.Drawing.Point(75, 160);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(225, 19);
            this.lblSubtitle.TabIndex = 3;
            this.lblSubtitle.Text = "Sign in to your account to continue";
            // 
            // lblUserCaption
            // 
            this.lblUserCaption.AutoSize = true;
            this.lblUserCaption.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUserCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(90)))));
            this.lblUserCaption.Location = new System.Drawing.Point(75, 225);
            this.lblUserCaption.Name = "lblUserCaption";
            this.lblUserCaption.Size = new System.Drawing.Size(84, 19);
            this.lblUserCaption.TabIndex = 4;
            this.lblUserCaption.Text = "USERNAME";
            // 
            // pnlUser
            // 
            this.pnlUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.pnlUser.Controls.Add(this.lblUserIcon);
            this.pnlUser.Controls.Add(this.txtUsername);
            this.pnlUser.Controls.Add(this.pnlUserLine);
            this.pnlUser.Location = new System.Drawing.Point(75, 250);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(390, 48);
            this.pnlUser.TabIndex = 5;
            // 
            // lblUserIcon
            // 
            this.lblUserIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 13F);
            this.lblUserIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(145)))), ((int)(((byte)(160)))));
            this.lblUserIcon.Location = new System.Drawing.Point(12, 0);
            this.lblUserIcon.Name = "lblUserIcon";
            this.lblUserIcon.Size = new System.Drawing.Size(36, 46);
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
            this.txtUsername.Location = new System.Drawing.Point(54, 13);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(320, 22);
            this.txtUsername.TabIndex = 1;
            // 
            // pnlUserLine
            // 
            this.pnlUserLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(222)))), ((int)(((byte)(230)))));
            this.pnlUserLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlUserLine.Location = new System.Drawing.Point(0, 46);
            this.pnlUserLine.Name = "pnlUserLine";
            this.pnlUserLine.Size = new System.Drawing.Size(390, 2);
            this.pnlUserLine.TabIndex = 2;
            // 
            // lblPassCaption
            // 
            this.lblPassCaption.AutoSize = true;
            this.lblPassCaption.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPassCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(75)))), ((int)(((byte)(90)))));
            this.lblPassCaption.Location = new System.Drawing.Point(75, 320);
            this.lblPassCaption.Name = "lblPassCaption";
            this.lblPassCaption.Size = new System.Drawing.Size(87, 19);
            this.lblPassCaption.TabIndex = 6;
            this.lblPassCaption.Text = "PASSWORD";
            // 
            // pnlPass
            // 
            this.pnlPass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.pnlPass.Controls.Add(this.btnTogglePass);
            this.pnlPass.Controls.Add(this.lblPassIcon);
            this.pnlPass.Controls.Add(this.txtPassword);
            this.pnlPass.Controls.Add(this.pnlPassLine);
            this.pnlPass.Location = new System.Drawing.Point(75, 345);
            this.pnlPass.Name = "pnlPass";
            this.pnlPass.Size = new System.Drawing.Size(390, 48);
            this.pnlPass.TabIndex = 7;
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
            this.btnTogglePass.Location = new System.Drawing.Point(348, 6);
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
            this.lblPassIcon.Size = new System.Drawing.Size(36, 46);
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
            this.txtPassword.Location = new System.Drawing.Point(54, 13);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(280, 22);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // pnlPassLine
            // 
            this.pnlPassLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(222)))), ((int)(((byte)(230)))));
            this.pnlPassLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPassLine.Location = new System.Drawing.Point(0, 46);
            this.pnlPassLine.Name = "pnlPassLine";
            this.pnlPassLine.Size = new System.Drawing.Size(390, 2);
            this.pnlPassLine.TabIndex = 3;
            // 
            // chkRemember
            // 
            this.chkRemember.AutoSize = true;
            this.chkRemember.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkRemember.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkRemember.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(95)))), ((int)(((byte)(115)))));
            this.chkRemember.Location = new System.Drawing.Point(78, 410);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new System.Drawing.Size(113, 21);
            this.chkRemember.TabIndex = 8;
            this.chkRemember.Text = "Remember me";
            this.chkRemember.UseVisualStyleBackColor = true;
            // 
            // lblError
            // 
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblError.Location = new System.Drawing.Point(75, 445);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(390, 20);
            this.lblError.TabIndex = 10;
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblError.Visible = false;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(252)))));
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(0)))), ((int)(((byte)(200)))));
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(0)))), ((int)(((byte)(230)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(75, 480);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(390, 48);
            this.btnLogin.TabIndex = 11;
            this.btnLogin.Text = "SIGN IN";
            this.btnLogin.UseVisualStyleBackColor = false;
            // 
            // lblFooter
            // 
            this.lblFooter.AutoSize = true;
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFooter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(135)))), ((int)(((byte)(150)))));
            this.lblFooter.Location = new System.Drawing.Point(160, 555);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(143, 17);
            this.lblFooter.TabIndex = 12;
            this.lblFooter.Text = "Don\'t have an account?";
            // 
            // linkSignup
            // 
            this.linkSignup.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(200)))));
            this.linkSignup.AutoSize = true;
            this.linkSignup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkSignup.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.linkSignup.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkSignup.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(252)))));
            this.linkSignup.Location = new System.Drawing.Point(305, 555);
            this.linkSignup.Name = "linkSignup";
            this.linkSignup.Size = new System.Drawing.Size(85, 17);
            this.linkSignup.TabIndex = 13;
            this.linkSignup.TabStop = true;
            this.linkSignup.Text = "Sign up now";
            this.linkSignup.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(0)))), ((int)(((byte)(252)))));
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(960, 600);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlSide);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bank Management";
            this.pnlSide.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlUser.ResumeLayout(false);
            this.pnlUser.PerformLayout();
            this.pnlPass.ResumeLayout(false);
            this.pnlPass.PerformLayout();
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

        private System.Windows.Forms.Label lblUserCaption;
        private System.Windows.Forms.Panel pnlUser;
        private System.Windows.Forms.Label lblUserIcon;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Panel pnlUserLine;

        private System.Windows.Forms.Label lblPassCaption;
        private System.Windows.Forms.Panel pnlPass;
        private System.Windows.Forms.Label lblPassIcon;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnTogglePass;
        private System.Windows.Forms.Panel pnlPassLine;

        private System.Windows.Forms.CheckBox chkRemember;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblFooter;
        private System.Windows.Forms.LinkLabel linkSignup;
    }
}
