namespace BankAccountManagement.Forms
{
    partial class SpendingLimitDialog
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
            this.pnlDialogCard = new BankAccountManagement.Forms.RoundedPanel();
            this.btnCancel = new BankAccountManagement.Forms.RoundedButton();
            this.btnSave = new BankAccountManagement.Forms.RoundedButton();
            this.chkAlert = new System.Windows.Forms.CheckBox();
            this.pnlWeeklyChips = new System.Windows.Forms.Panel();
            this.btnW20M = new BankAccountManagement.Forms.RoundedButton();
            this.btnW10M = new BankAccountManagement.Forms.RoundedButton();
            this.btnW5M = new BankAccountManagement.Forms.RoundedButton();
            this.btnW3M = new BankAccountManagement.Forms.RoundedButton();
            this.pnlWeeklyBox = new BankAccountManagement.Forms.RoundedPanel();
            this.lblWeeklyUnit = new System.Windows.Forms.Label();
            this.txtWeeklyLimit = new System.Windows.Forms.TextBox();
            this.lblWeeklyTitle = new System.Windows.Forms.Label();
            this.pnlDailyChips = new System.Windows.Forms.Panel();
            this.btnD5M = new BankAccountManagement.Forms.RoundedButton();
            this.btnD2M = new BankAccountManagement.Forms.RoundedButton();
            this.btnD1M = new BankAccountManagement.Forms.RoundedButton();
            this.btnD500k = new BankAccountManagement.Forms.RoundedButton();
            this.pnlDailyBox = new BankAccountManagement.Forms.RoundedPanel();
            this.lblDailyUnit = new System.Windows.Forms.Label();
            this.txtDailyLimit = new System.Windows.Forms.TextBox();
            this.lblDailyTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlIconBadge = new BankAccountManagement.Forms.RoundedPanel();
            this.lblIcon = new System.Windows.Forms.Label();
            this.pnlDialogCard.SuspendLayout();
            this.pnlWeeklyChips.SuspendLayout();
            this.pnlWeeklyBox.SuspendLayout();
            this.pnlDailyChips.SuspendLayout();
            this.pnlDailyBox.SuspendLayout();
            this.pnlIconBadge.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDialogCard
            // 
            this.pnlDialogCard.BackColor = System.Drawing.Color.White;
            this.pnlDialogCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.pnlDialogCard.BorderRadius = 18;
            this.pnlDialogCard.BorderSize = 1;
            this.pnlDialogCard.Controls.Add(this.btnCancel);
            this.pnlDialogCard.Controls.Add(this.btnSave);
            this.pnlDialogCard.Controls.Add(this.chkAlert);
            this.pnlDialogCard.Controls.Add(this.pnlWeeklyChips);
            this.pnlDialogCard.Controls.Add(this.pnlWeeklyBox);
            this.pnlDialogCard.Controls.Add(this.lblWeeklyTitle);
            this.pnlDialogCard.Controls.Add(this.pnlDailyChips);
            this.pnlDialogCard.Controls.Add(this.pnlDailyBox);
            this.pnlDialogCard.Controls.Add(this.lblDailyTitle);
            this.pnlDialogCard.Controls.Add(this.lblSubtitle);
            this.pnlDialogCard.Controls.Add(this.lblTitle);
            this.pnlDialogCard.Controls.Add(this.btnClose);
            this.pnlDialogCard.Controls.Add(this.pnlIconBadge);
            this.pnlDialogCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDialogCard.FillColor = System.Drawing.Color.White;
            this.pnlDialogCard.Location = new System.Drawing.Point(0, 0);
            this.pnlDialogCard.Name = "pnlDialogCard";
            this.pnlDialogCard.Padding = new System.Windows.Forms.Padding(24);
            this.pnlDialogCard.Size = new System.Drawing.Size(460, 540);
            this.pnlDialogCard.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.BorderRadius = 10;
            this.btnCancel.BorderSize = 1;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCancel.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnCancel.Location = new System.Drawing.Point(24, 466);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.NormalBorder = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnCancel.NormalColor = System.Drawing.Color.White;
            this.btnCancel.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCancel.Size = new System.Drawing.Size(130, 44);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Hủy bỏ";
            this.btnCancel.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            // 
            // btnSave
            // 
            this.btnSave.BorderRadius = 10;
            this.btnSave.BorderSize = 0;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnSave.Location = new System.Drawing.Point(164, 466);
            this.btnSave.Name = "btnSave";
            this.btnSave.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSave.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnSave.Size = new System.Drawing.Size(272, 44);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "💾 Lưu hạn mức chi tiêu";
            this.btnSave.TextColor = System.Drawing.Color.White;
            // 
            // chkAlert
            // 
            this.chkAlert.AutoSize = true;
            this.chkAlert.Checked = true;
            this.chkAlert.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAlert.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkAlert.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkAlert.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.chkAlert.Location = new System.Drawing.Point(26, 422);
            this.chkAlert.Name = "chkAlert";
            this.chkAlert.Size = new System.Drawing.Size(306, 19);
            this.chkAlert.TabIndex = 10;
            this.chkAlert.Text = "Bật cảnh báo màu sắc khi chi tiêu vượt quá 80% hạn mức";
            this.chkAlert.UseVisualStyleBackColor = true;
            // 
            // pnlWeeklyChips
            // 
            this.pnlWeeklyChips.Controls.Add(this.btnW20M);
            this.pnlWeeklyChips.Controls.Add(this.btnW10M);
            this.pnlWeeklyChips.Controls.Add(this.btnW5M);
            this.pnlWeeklyChips.Controls.Add(this.btnW3M);
            this.pnlWeeklyChips.Location = new System.Drawing.Point(24, 366);
            this.pnlWeeklyChips.Name = "pnlWeeklyChips";
            this.pnlWeeklyChips.Size = new System.Drawing.Size(412, 34);
            this.pnlWeeklyChips.TabIndex = 9;
            // 
            // btnW20M
            // 
            this.btnW20M.BorderRadius = 6;
            this.btnW20M.BorderSize = 1;
            this.btnW20M.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnW20M.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnW20M.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.btnW20M.Location = new System.Drawing.Point(312, 0);
            this.btnW20M.Name = "btnW20M";
            this.btnW20M.NormalBorder = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnW20M.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnW20M.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.btnW20M.Size = new System.Drawing.Size(96, 32);
            this.btnW20M.TabIndex = 3;
            this.btnW20M.Text = "20 triệu";
            this.btnW20M.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            // 
            // btnW10M
            // 
            this.btnW10M.BorderRadius = 6;
            this.btnW10M.BorderSize = 1;
            this.btnW10M.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnW10M.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnW10M.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.btnW10M.Location = new System.Drawing.Point(208, 0);
            this.btnW10M.Name = "btnW10M";
            this.btnW10M.NormalBorder = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnW10M.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnW10M.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.btnW10M.Size = new System.Drawing.Size(96, 32);
            this.btnW10M.TabIndex = 2;
            this.btnW10M.Text = "10 triệu";
            this.btnW10M.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            // 
            // btnW5M
            // 
            this.btnW5M.BorderRadius = 6;
            this.btnW5M.BorderSize = 1;
            this.btnW5M.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnW5M.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnW5M.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.btnW5M.Location = new System.Drawing.Point(104, 0);
            this.btnW5M.Name = "btnW5M";
            this.btnW5M.NormalBorder = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnW5M.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnW5M.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.btnW5M.Size = new System.Drawing.Size(96, 32);
            this.btnW5M.TabIndex = 1;
            this.btnW5M.Text = "5 triệu";
            this.btnW5M.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            // 
            // btnW3M
            // 
            this.btnW3M.BorderRadius = 6;
            this.btnW3M.BorderSize = 1;
            this.btnW3M.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnW3M.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnW3M.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.btnW3M.Location = new System.Drawing.Point(0, 0);
            this.btnW3M.Name = "btnW3M";
            this.btnW3M.NormalBorder = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnW3M.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnW3M.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.btnW3M.Size = new System.Drawing.Size(96, 32);
            this.btnW3M.TabIndex = 0;
            this.btnW3M.Text = "3 triệu";
            this.btnW3M.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            // 
            // pnlWeeklyBox
            // 
            this.pnlWeeklyBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlWeeklyBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.pnlWeeklyBox.BorderRadius = 10;
            this.pnlWeeklyBox.BorderSize = 1;
            this.pnlWeeklyBox.Controls.Add(this.lblWeeklyUnit);
            this.pnlWeeklyBox.Controls.Add(this.txtWeeklyLimit);
            this.pnlWeeklyBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlWeeklyBox.Location = new System.Drawing.Point(24, 314);
            this.pnlWeeklyBox.Name = "pnlWeeklyBox";
            this.pnlWeeklyBox.Size = new System.Drawing.Size(412, 46);
            this.pnlWeeklyBox.TabIndex = 8;
            // 
            // lblWeeklyUnit
            // 
            this.lblWeeklyUnit.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblWeeklyUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblWeeklyUnit.Location = new System.Drawing.Point(360, 11);
            this.lblWeeklyUnit.Name = "lblWeeklyUnit";
            this.lblWeeklyUnit.Size = new System.Drawing.Size(42, 24);
            this.lblWeeklyUnit.TabIndex = 1;
            this.lblWeeklyUnit.Text = "VNĐ";
            this.lblWeeklyUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtWeeklyLimit
            // 
            this.txtWeeklyLimit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtWeeklyLimit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWeeklyLimit.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.txtWeeklyLimit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.txtWeeklyLimit.Location = new System.Drawing.Point(16, 11);
            this.txtWeeklyLimit.Name = "txtWeeklyLimit";
            this.txtWeeklyLimit.Size = new System.Drawing.Size(340, 24);
            this.txtWeeklyLimit.TabIndex = 0;
            this.txtWeeklyLimit.Text = "10,000,000";
            // 
            // lblWeeklyTitle
            // 
            this.lblWeeklyTitle.AutoSize = true;
            this.lblWeeklyTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblWeeklyTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblWeeklyTitle.Location = new System.Drawing.Point(24, 290);
            this.lblWeeklyTitle.Name = "lblWeeklyTitle";
            this.lblWeeklyTitle.Size = new System.Drawing.Size(206, 17);
            this.lblWeeklyTitle.TabIndex = 7;
            this.lblWeeklyTitle.Text = "Hạn mức chi tiêu trong 1 tuần:";
            // 
            // pnlDailyChips
            // 
            this.pnlDailyChips.Controls.Add(this.btnD5M);
            this.pnlDailyChips.Controls.Add(this.btnD2M);
            this.pnlDailyChips.Controls.Add(this.btnD1M);
            this.pnlDailyChips.Controls.Add(this.btnD500k);
            this.pnlDailyChips.Location = new System.Drawing.Point(24, 234);
            this.pnlDailyChips.Name = "pnlDailyChips";
            this.pnlDailyChips.Size = new System.Drawing.Size(412, 34);
            this.pnlDailyChips.TabIndex = 6;
            // 
            // btnD5M
            // 
            this.btnD5M.BorderRadius = 6;
            this.btnD5M.BorderSize = 1;
            this.btnD5M.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnD5M.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnD5M.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.btnD5M.Location = new System.Drawing.Point(312, 0);
            this.btnD5M.Name = "btnD5M";
            this.btnD5M.NormalBorder = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnD5M.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnD5M.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.btnD5M.Size = new System.Drawing.Size(96, 32);
            this.btnD5M.TabIndex = 3;
            this.btnD5M.Text = "5 triệu";
            this.btnD5M.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            // 
            // btnD2M
            // 
            this.btnD2M.BorderRadius = 6;
            this.btnD2M.BorderSize = 1;
            this.btnD2M.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnD2M.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnD2M.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.btnD2M.Location = new System.Drawing.Point(208, 0);
            this.btnD2M.Name = "btnD2M";
            this.btnD2M.NormalBorder = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnD2M.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnD2M.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.btnD2M.Size = new System.Drawing.Size(96, 32);
            this.btnD2M.TabIndex = 2;
            this.btnD2M.Text = "2 triệu";
            this.btnD2M.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            // 
            // btnD1M
            // 
            this.btnD1M.BorderRadius = 6;
            this.btnD1M.BorderSize = 1;
            this.btnD1M.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnD1M.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnD1M.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.btnD1M.Location = new System.Drawing.Point(104, 0);
            this.btnD1M.Name = "btnD1M";
            this.btnD1M.NormalBorder = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnD1M.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnD1M.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.btnD1M.Size = new System.Drawing.Size(96, 32);
            this.btnD1M.TabIndex = 1;
            this.btnD1M.Text = "1 triệu";
            this.btnD1M.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            // 
            // btnD500k
            // 
            this.btnD500k.BorderRadius = 6;
            this.btnD500k.BorderSize = 1;
            this.btnD500k.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnD500k.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnD500k.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.btnD500k.Location = new System.Drawing.Point(0, 0);
            this.btnD500k.Name = "btnD500k";
            this.btnD500k.NormalBorder = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnD500k.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnD500k.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.btnD500k.Size = new System.Drawing.Size(96, 32);
            this.btnD500k.TabIndex = 0;
            this.btnD500k.Text = "500.000 đ";
            this.btnD500k.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            // 
            // pnlDailyBox
            // 
            this.pnlDailyBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlDailyBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.pnlDailyBox.BorderRadius = 10;
            this.pnlDailyBox.BorderSize = 1;
            this.pnlDailyBox.Controls.Add(this.lblDailyUnit);
            this.pnlDailyBox.Controls.Add(this.txtDailyLimit);
            this.pnlDailyBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlDailyBox.Location = new System.Drawing.Point(24, 182);
            this.pnlDailyBox.Name = "pnlDailyBox";
            this.pnlDailyBox.Size = new System.Drawing.Size(412, 46);
            this.pnlDailyBox.TabIndex = 5;
            // 
            // lblDailyUnit
            // 
            this.lblDailyUnit.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDailyUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDailyUnit.Location = new System.Drawing.Point(360, 11);
            this.lblDailyUnit.Name = "lblDailyUnit";
            this.lblDailyUnit.Size = new System.Drawing.Size(42, 24);
            this.lblDailyUnit.TabIndex = 1;
            this.lblDailyUnit.Text = "VNĐ";
            this.lblDailyUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDailyLimit
            // 
            this.txtDailyLimit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.txtDailyLimit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDailyLimit.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.txtDailyLimit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.txtDailyLimit.Location = new System.Drawing.Point(16, 11);
            this.txtDailyLimit.Name = "txtDailyLimit";
            this.txtDailyLimit.Size = new System.Drawing.Size(340, 24);
            this.txtDailyLimit.TabIndex = 0;
            this.txtDailyLimit.Text = "2,000,000";
            // 
            // lblDailyTitle
            // 
            this.lblDailyTitle.AutoSize = true;
            this.lblDailyTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDailyTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblDailyTitle.Location = new System.Drawing.Point(24, 158);
            this.lblDailyTitle.Name = "lblDailyTitle";
            this.lblDailyTitle.Size = new System.Drawing.Size(207, 17);
            this.lblDailyTitle.TabIndex = 4;
            this.lblDailyTitle.Text = "Hạn mức chi tiêu trong 1 ngày:";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubtitle.Location = new System.Drawing.Point(82, 54);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(326, 42);
            this.lblSubtitle.TabIndex = 3;
            this.lblSubtitle.Text = "Cài đặt ngân sách chi tiêu tối đa mỗi ngày và mỗi tuần để quản lý dòng tiền hiệu " +
    "quả hơn.";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(82, 28);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(193, 21);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Quản lý hạn mức chi tiêu";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnClose.Location = new System.Drawing.Point(414, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(32, 32);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "✕";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // pnlIconBadge
            // 
            this.pnlIconBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.pnlIconBadge.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.pnlIconBadge.BorderRadius = 14;
            this.pnlIconBadge.BorderSize = 1;
            this.pnlIconBadge.Controls.Add(this.lblIcon);
            this.pnlIconBadge.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.pnlIconBadge.Location = new System.Drawing.Point(24, 24);
            this.pnlIconBadge.Name = "pnlIconBadge";
            this.pnlIconBadge.Size = new System.Drawing.Size(48, 48);
            this.pnlIconBadge.TabIndex = 0;
            // 
            // lblIcon
            // 
            this.lblIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIcon.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.lblIcon.Location = new System.Drawing.Point(0, 0);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(48, 48);
            this.lblIcon.TabIndex = 0;
            this.lblIcon.Text = "💳";
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SpendingLimitDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(460, 540);
            this.Controls.Add(this.pnlDialogCard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpendingLimitDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thiết lập hạn mức chi tiêu";
            this.pnlDialogCard.ResumeLayout(false);
            this.pnlDialogCard.PerformLayout();
            this.pnlWeeklyChips.ResumeLayout(false);
            this.pnlWeeklyBox.ResumeLayout(false);
            this.pnlWeeklyBox.PerformLayout();
            this.pnlDailyChips.ResumeLayout(false);
            this.pnlDailyBox.ResumeLayout(false);
            this.pnlDailyBox.PerformLayout();
            this.pnlIconBadge.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BankAccountManagement.Forms.RoundedPanel pnlDialogCard;
        private BankAccountManagement.Forms.RoundedPanel pnlIconBadge;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblDailyTitle;
        private BankAccountManagement.Forms.RoundedPanel pnlDailyBox;
        private System.Windows.Forms.TextBox txtDailyLimit;
        private System.Windows.Forms.Label lblDailyUnit;
        private System.Windows.Forms.Panel pnlDailyChips;
        private BankAccountManagement.Forms.RoundedButton btnD500k;
        private BankAccountManagement.Forms.RoundedButton btnD1M;
        private BankAccountManagement.Forms.RoundedButton btnD2M;
        private BankAccountManagement.Forms.RoundedButton btnD5M;
        private System.Windows.Forms.Label lblWeeklyTitle;
        private BankAccountManagement.Forms.RoundedPanel pnlWeeklyBox;
        private System.Windows.Forms.TextBox txtWeeklyLimit;
        private System.Windows.Forms.Label lblWeeklyUnit;
        private System.Windows.Forms.Panel pnlWeeklyChips;
        private BankAccountManagement.Forms.RoundedButton btnW3M;
        private BankAccountManagement.Forms.RoundedButton btnW5M;
        private BankAccountManagement.Forms.RoundedButton btnW10M;
        private BankAccountManagement.Forms.RoundedButton btnW20M;
        private System.Windows.Forms.CheckBox chkAlert;
        private BankAccountManagement.Forms.RoundedButton btnSave;
        private BankAccountManagement.Forms.RoundedButton btnCancel;
    }
}
