namespace BankAccountManagement.Forms
{
    partial class CreditCardUserForm
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
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlCreditCard = new BankAccountManagement.Forms.RoundedPanel();
            this.lblCardBrand = new System.Windows.Forms.Label();
            this.lblChip = new System.Windows.Forms.Label();
            this.lblCardNumber = new System.Windows.Forms.Label();
            this.lblHolderTitle = new System.Windows.Forms.Label();
            this.lblHolderName = new System.Windows.Forms.Label();
            this.lblExpiryTitle = new System.Windows.Forms.Label();
            this.lblExpiryValue = new System.Windows.Forms.Label();
            this.pnlLogoCircle1 = new System.Windows.Forms.Panel();
            this.pnlLogoCircle2 = new System.Windows.Forms.Panel();
            this.lblStatusTitle = new System.Windows.Forms.Label();
            this.lblStatusValue = new System.Windows.Forms.Label();
            this.pnlToggleTrack = new BankAccountManagement.Forms.RoundedPanel();
            this.pnlToggleKnob = new BankAccountManagement.Forms.RoundedPanel();
            this.btnViewCardInfo = new BankAccountManagement.Forms.RoundedButton();
            this.pnlInfoCard = new BankAccountManagement.Forms.RoundedPanel();
            this.lblInfoTitle = new System.Windows.Forms.Label();
            this.lblNumTitle = new System.Windows.Forms.Label();
            this.lblNumValue = new System.Windows.Forms.Label();
            this.lblNumStatus = new System.Windows.Forms.Label();
            this.lblBalTitle = new System.Windows.Forms.Label();
            this.lblBalValue = new System.Windows.Forms.Label();
            this.lblBalDate = new System.Windows.Forms.Label();
            this.lblStmtTitle = new System.Windows.Forms.Label();
            this.lblStmtValue = new System.Windows.Forms.Label();
            this.lblStmtDate = new System.Windows.Forms.Label();
            this.lblDueTitle = new System.Windows.Forms.Label();
            this.lblDueValue = new System.Windows.Forms.Label();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.lblStateTitle = new System.Windows.Forms.Label();
            this.lblStateValue = new System.Windows.Forms.Label();
            this.lblStateStatus = new System.Windows.Forms.Label();
            this.pnlLeft.SuspendLayout();
            this.pnlCreditCard.SuspendLayout();
            this.pnlToggleTrack.SuspendLayout();
            this.pnlInfoCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.Transparent;
            this.pnlLeft.Controls.Add(this.pnlCreditCard);
            this.pnlLeft.Controls.Add(this.lblStatusTitle);
            this.pnlLeft.Controls.Add(this.lblStatusValue);
            this.pnlLeft.Controls.Add(this.pnlToggleTrack);
            this.pnlLeft.Controls.Add(this.btnViewCardInfo);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(24, 24);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(380, 702);
            this.pnlLeft.TabIndex = 0;
            // 
            // pnlCreditCard
            // 
            this.pnlCreditCard.BackColor = System.Drawing.Color.Transparent;
            this.pnlCreditCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.pnlCreditCard.BorderRadius = 20;
            this.pnlCreditCard.BorderSize = 0;
            this.pnlCreditCard.Controls.Add(this.lblCardBrand);
            this.pnlCreditCard.Controls.Add(this.lblChip);
            this.pnlCreditCard.Controls.Add(this.lblCardNumber);
            this.pnlCreditCard.Controls.Add(this.lblHolderTitle);
            this.pnlCreditCard.Controls.Add(this.lblHolderName);
            this.pnlCreditCard.Controls.Add(this.lblExpiryTitle);
            this.pnlCreditCard.Controls.Add(this.lblExpiryValue);
            this.pnlCreditCard.Controls.Add(this.pnlLogoCircle2);
            this.pnlCreditCard.Controls.Add(this.pnlLogoCircle1);
            this.pnlCreditCard.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.pnlCreditCard.Location = new System.Drawing.Point(0, 0);
            this.pnlCreditCard.Name = "pnlCreditCard";
            this.pnlCreditCard.Size = new System.Drawing.Size(380, 220);
            this.pnlCreditCard.TabIndex = 0;
            // 
            // lblCardBrand
            // 
            this.lblCardBrand.AutoSize = true;
            this.lblCardBrand.BackColor = System.Drawing.Color.Transparent;
            this.lblCardBrand.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblCardBrand.ForeColor = System.Drawing.Color.White;
            this.lblCardBrand.Location = new System.Drawing.Point(24, 20);
            this.lblCardBrand.Name = "lblCardBrand";
            this.lblCardBrand.Size = new System.Drawing.Size(95, 21);
            this.lblCardBrand.TabIndex = 0;
            this.lblCardBrand.Text = "Thẻ ghi nợ";
            // 
            // lblChip
            // 
            this.lblChip.BackColor = System.Drawing.Color.Transparent;
            this.lblChip.Font = new System.Drawing.Font("Segoe UI Emoji", 20F);
            this.lblChip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(204)))), ((int)(((byte)(21)))));
            this.lblChip.Location = new System.Drawing.Point(24, 60);
            this.lblChip.Name = "lblChip";
            this.lblChip.Size = new System.Drawing.Size(40, 40);
            this.lblChip.TabIndex = 1;
            this.lblChip.Text = "💳";
            this.lblChip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCardNumber
            // 
            this.lblCardNumber.AutoSize = true;
            this.lblCardNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblCardNumber.Font = new System.Drawing.Font("Consolas", 15.5F, System.Drawing.FontStyle.Bold);
            this.lblCardNumber.ForeColor = System.Drawing.Color.White;
            this.lblCardNumber.Location = new System.Drawing.Point(24, 118);
            this.lblCardNumber.Name = "lblCardNumber";
            this.lblCardNumber.Size = new System.Drawing.Size(228, 24);
            this.lblCardNumber.TabIndex = 2;
            this.lblCardNumber.Text = "4213 **** **** 9876";
            // 
            // lblHolderTitle
            // 
            this.lblHolderTitle.AutoSize = true;
            this.lblHolderTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblHolderTitle.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblHolderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblHolderTitle.Location = new System.Drawing.Point(24, 160);
            this.lblHolderTitle.Name = "lblHolderTitle";
            this.lblHolderTitle.Size = new System.Drawing.Size(49, 12);
            this.lblHolderTitle.TabIndex = 3;
            this.lblHolderTitle.Text = "CHỦ THẺ";
            // 
            // lblHolderName
            // 
            this.lblHolderName.AutoSize = true;
            this.lblHolderName.BackColor = System.Drawing.Color.Transparent;
            this.lblHolderName.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblHolderName.ForeColor = System.Drawing.Color.White;
            this.lblHolderName.Location = new System.Drawing.Point(24, 175);
            this.lblHolderName.Name = "lblHolderName";
            this.lblHolderName.Size = new System.Drawing.Size(92, 19);
            this.lblHolderName.TabIndex = 4;
            this.lblHolderName.Text = "PHONG HOÁ";
            // 
            // lblExpiryTitle
            // 
            this.lblExpiryTitle.AutoSize = true;
            this.lblExpiryTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblExpiryTitle.Font = new System.Drawing.Font("Segoe UI", 7.5F);
            this.lblExpiryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.lblExpiryTitle.Location = new System.Drawing.Point(180, 160);
            this.lblExpiryTitle.Name = "lblExpiryTitle";
            this.lblExpiryTitle.Size = new System.Drawing.Size(46, 12);
            this.lblExpiryTitle.TabIndex = 5;
            this.lblExpiryTitle.Text = "HẾT HẠN";
            // 
            // lblExpiryValue
            // 
            this.lblExpiryValue.AutoSize = true;
            this.lblExpiryValue.BackColor = System.Drawing.Color.Transparent;
            this.lblExpiryValue.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblExpiryValue.ForeColor = System.Drawing.Color.White;
            this.lblExpiryValue.Location = new System.Drawing.Point(180, 175);
            this.lblExpiryValue.Name = "lblExpiryValue";
            this.lblExpiryValue.Size = new System.Drawing.Size(41, 19);
            this.lblExpiryValue.TabIndex = 6;
            this.lblExpiryValue.Text = "12/28";
            // 
            // pnlLogoCircle1
            // 
            this.pnlLogoCircle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.pnlLogoCircle1.Location = new System.Drawing.Point(292, 156);
            this.pnlLogoCircle1.Name = "pnlLogoCircle1";
            this.pnlLogoCircle1.Size = new System.Drawing.Size(34, 34);
            this.pnlLogoCircle1.TabIndex = 7;
            // 
            // pnlLogoCircle2
            // 
            this.pnlLogoCircle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(204)))), ((int)(((byte)(21)))));
            this.pnlLogoCircle2.Location = new System.Drawing.Point(318, 156);
            this.pnlLogoCircle2.Name = "pnlLogoCircle2";
            this.pnlLogoCircle2.Size = new System.Drawing.Size(34, 34);
            this.pnlLogoCircle2.TabIndex = 8;
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.AutoSize = true;
            this.lblStatusTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatusTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStatusTitle.Location = new System.Drawing.Point(4, 242);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(73, 19);
            this.lblStatusTitle.TabIndex = 1;
            this.lblStatusTitle.Text = "Trạng thái";
            // 
            // lblStatusValue
            // 
            this.lblStatusValue.AutoSize = true;
            this.lblStatusValue.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.lblStatusValue.Location = new System.Drawing.Point(4, 268);
            this.lblStatusValue.Name = "lblStatusValue";
            this.lblStatusValue.Size = new System.Drawing.Size(104, 17);
            this.lblStatusValue.TabIndex = 2;
            this.lblStatusValue.Text = "Đang hoạt động";
            // 
            // pnlToggleTrack
            // 
            this.pnlToggleTrack.BackColor = System.Drawing.Color.Transparent;
            this.pnlToggleTrack.BorderColor = System.Drawing.Color.Transparent;
            this.pnlToggleTrack.BorderRadius = 16;
            this.pnlToggleTrack.BorderSize = 0;
            this.pnlToggleTrack.Controls.Add(this.pnlToggleKnob);
            this.pnlToggleTrack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlToggleTrack.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.pnlToggleTrack.Location = new System.Drawing.Point(300, 248);
            this.pnlToggleTrack.Name = "pnlToggleTrack";
            this.pnlToggleTrack.Size = new System.Drawing.Size(60, 30);
            this.pnlToggleTrack.TabIndex = 3;
            // 
            // pnlToggleKnob
            // 
            this.pnlToggleKnob.BackColor = System.Drawing.Color.Transparent;
            this.pnlToggleKnob.BorderColor = System.Drawing.Color.Transparent;
            this.pnlToggleKnob.BorderRadius = 12;
            this.pnlToggleKnob.BorderSize = 0;
            this.pnlToggleKnob.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlToggleKnob.FillColor = System.Drawing.Color.White;
            this.pnlToggleKnob.Location = new System.Drawing.Point(32, 3);
            this.pnlToggleKnob.Name = "pnlToggleKnob";
            this.pnlToggleKnob.Size = new System.Drawing.Size(24, 24);
            this.pnlToggleKnob.TabIndex = 0;
            // 
            // btnViewCardInfo
            // 
            this.btnViewCardInfo.BackColor = System.Drawing.Color.Transparent;
            this.btnViewCardInfo.BorderRadius = 10;
            this.btnViewCardInfo.BorderSize = 0;
            this.btnViewCardInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewCardInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewCardInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnViewCardInfo.ForeColor = System.Drawing.Color.White;
            this.btnViewCardInfo.HoverBorder = System.Drawing.Color.Transparent;
            this.btnViewCardInfo.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnViewCardInfo.Location = new System.Drawing.Point(4, 308);
            this.btnViewCardInfo.Name = "btnViewCardInfo";
            this.btnViewCardInfo.NormalBorder = System.Drawing.Color.Transparent;
            this.btnViewCardInfo.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnViewCardInfo.PressedBorder = System.Drawing.Color.Transparent;
            this.btnViewCardInfo.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnViewCardInfo.Size = new System.Drawing.Size(356, 44);
            this.btnViewCardInfo.TabIndex = 4;
            this.btnViewCardInfo.Text = "Xem thông tin thẻ";
            this.btnViewCardInfo.TextColor = System.Drawing.Color.White;
            this.btnViewCardInfo.UseVisualStyleBackColor = false;
            // 
            // pnlInfoCard
            // 
            this.pnlInfoCard.BackColor = System.Drawing.Color.Transparent;
            this.pnlInfoCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlInfoCard.BorderRadius = 16;
            this.pnlInfoCard.BorderSize = 1;
            this.pnlInfoCard.Controls.Add(this.lblInfoTitle);
            this.pnlInfoCard.Controls.Add(this.lblNumTitle);
            this.pnlInfoCard.Controls.Add(this.lblNumValue);
            this.pnlInfoCard.Controls.Add(this.lblNumStatus);
            this.pnlInfoCard.Controls.Add(this.lblBalTitle);
            this.pnlInfoCard.Controls.Add(this.lblBalValue);
            this.pnlInfoCard.Controls.Add(this.lblBalDate);
            this.pnlInfoCard.Controls.Add(this.lblStmtTitle);
            this.pnlInfoCard.Controls.Add(this.lblStmtValue);
            this.pnlInfoCard.Controls.Add(this.lblStmtDate);
            this.pnlInfoCard.Controls.Add(this.lblDueTitle);
            this.pnlInfoCard.Controls.Add(this.lblDueValue);
            this.pnlInfoCard.Controls.Add(this.lblDueDate);
            this.pnlInfoCard.Controls.Add(this.lblStateTitle);
            this.pnlInfoCard.Controls.Add(this.lblStateValue);
            this.pnlInfoCard.Controls.Add(this.lblStateStatus);
            this.pnlInfoCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfoCard.FillColor = System.Drawing.Color.White;
            this.pnlInfoCard.Location = new System.Drawing.Point(404, 24);
            this.pnlInfoCard.Name = "pnlInfoCard";
            this.pnlInfoCard.Padding = new System.Windows.Forms.Padding(24);
            this.pnlInfoCard.Size = new System.Drawing.Size(872, 702);
            this.pnlInfoCard.TabIndex = 1;
            // 
            // lblInfoTitle
            // 
            this.lblInfoTitle.AutoSize = true;
            this.lblInfoTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblInfoTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblInfoTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblInfoTitle.Location = new System.Drawing.Point(24, 24);
            this.lblInfoTitle.Name = "lblInfoTitle";
            this.lblInfoTitle.Size = new System.Drawing.Size(126, 25);
            this.lblInfoTitle.TabIndex = 0;
            this.lblInfoTitle.Text = "Thông tin thẻ";
            // 
            // lblNumTitle
            // 
            this.lblNumTitle.AutoSize = true;
            this.lblNumTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblNumTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblNumTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblNumTitle.Location = new System.Drawing.Point(24, 80);
            this.lblNumTitle.Name = "lblNumTitle";
            this.lblNumTitle.Size = new System.Drawing.Size(46, 17);
            this.lblNumTitle.TabIndex = 1;
            this.lblNumTitle.Text = "Số thẻ";
            // 
            // lblNumValue
            // 
            this.lblNumValue.AutoSize = true;
            this.lblNumValue.BackColor = System.Drawing.Color.Transparent;
            this.lblNumValue.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblNumValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblNumValue.Location = new System.Drawing.Point(180, 80);
            this.lblNumValue.Name = "lblNumValue";
            this.lblNumValue.Size = new System.Drawing.Size(127, 17);
            this.lblNumValue.TabIndex = 2;
            this.lblNumValue.Text = "4213 **** **** 9876";
            // 
            // lblNumStatus
            // 
            this.lblNumStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumStatus.AutoSize = true;
            this.lblNumStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblNumStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblNumStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.lblNumStatus.Location = new System.Drawing.Point(720, 80);
            this.lblNumStatus.Name = "lblNumStatus";
            this.lblNumStatus.Size = new System.Drawing.Size(104, 17);
            this.lblNumStatus.TabIndex = 3;
            this.lblNumStatus.Text = "Đang hoạt động";
            this.lblNumStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBalTitle
            // 
            this.lblBalTitle.AutoSize = true;
            this.lblBalTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblBalTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblBalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblBalTitle.Location = new System.Drawing.Point(24, 135);
            this.lblBalTitle.Name = "lblBalTitle";
            this.lblBalTitle.Size = new System.Drawing.Size(42, 17);
            this.lblBalTitle.TabIndex = 4;
            this.lblBalTitle.Text = "Số dư";
            // 
            // lblBalValue
            // 
            this.lblBalValue.AutoSize = true;
            this.lblBalValue.BackColor = System.Drawing.Color.Transparent;
            this.lblBalValue.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblBalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblBalValue.Location = new System.Drawing.Point(180, 135);
            this.lblBalValue.Name = "lblBalValue";
            this.lblBalValue.Size = new System.Drawing.Size(92, 17);
            this.lblBalValue.TabIndex = 5;
            this.lblBalValue.Text = "**** **** ****";
            // 
            // lblBalDate
            // 
            this.lblBalDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBalDate.AutoSize = true;
            this.lblBalDate.BackColor = System.Drawing.Color.Transparent;
            this.lblBalDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblBalDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblBalDate.Location = new System.Drawing.Point(720, 135);
            this.lblBalDate.Name = "lblBalDate";
            this.lblBalDate.Size = new System.Drawing.Size(74, 17);
            this.lblBalDate.TabIndex = 6;
            this.lblBalDate.Text = "15/10/2026";
            this.lblBalDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStmtTitle
            // 
            this.lblStmtTitle.AutoSize = true;
            this.lblStmtTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblStmtTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblStmtTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStmtTitle.Location = new System.Drawing.Point(24, 190);
            this.lblStmtTitle.Name = "lblStmtTitle";
            this.lblStmtTitle.Size = new System.Drawing.Size(84, 17);
            this.lblStmtTitle.TabIndex = 7;
            this.lblStmtTitle.Text = "Ngày cấp thẻ";
            // 
            // lblStmtValue
            // 
            this.lblStmtValue.AutoSize = true;
            this.lblStmtValue.BackColor = System.Drawing.Color.Transparent;
            this.lblStmtValue.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStmtValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStmtValue.Location = new System.Drawing.Point(180, 190);
            this.lblStmtValue.Name = "lblStmtValue";
            this.lblStmtValue.Size = new System.Drawing.Size(76, 17);
            this.lblStmtValue.TabIndex = 8;
            this.lblStmtValue.Text = "27/08/2023";
            // 
            // lblStmtDate
            // 
            this.lblStmtDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStmtDate.AutoSize = true;
            this.lblStmtDate.BackColor = System.Drawing.Color.Transparent;
            this.lblStmtDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblStmtDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStmtDate.Location = new System.Drawing.Point(720, 190);
            this.lblStmtDate.Name = "lblStmtDate";
            this.lblStmtDate.Size = new System.Drawing.Size(83, 17);
            this.lblStmtDate.TabIndex = 9;
            this.lblStmtDate.Text = "Vietcombank";
            this.lblStmtDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDueTitle
            // 
            this.lblDueTitle.AutoSize = true;
            this.lblDueTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblDueTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDueTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDueTitle.Location = new System.Drawing.Point(24, 245);
            this.lblDueTitle.Name = "lblDueTitle";
            this.lblDueTitle.Size = new System.Drawing.Size(107, 17);
            this.lblDueTitle.TabIndex = 10;
            this.lblDueTitle.Text = "Ngày hết hạn thẻ";
            // 
            // lblDueValue
            // 
            this.lblDueValue.AutoSize = true;
            this.lblDueValue.BackColor = System.Drawing.Color.Transparent;
            this.lblDueValue.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDueValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblDueValue.Location = new System.Drawing.Point(180, 245);
            this.lblDueValue.Name = "lblDueValue";
            this.lblDueValue.Size = new System.Drawing.Size(76, 17);
            this.lblDueValue.TabIndex = 11;
            this.lblDueValue.Text = "27/08/2028";
            // 
            // lblDueDate
            // 
            this.lblDueDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDueDate.AutoSize = true;
            this.lblDueDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDueDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDueDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblDueDate.Location = new System.Drawing.Point(720, 245);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(70, 17);
            this.lblDueDate.TabIndex = 12;
            this.lblDueDate.Text = "Hạn 5 năm";
            this.lblDueDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStateTitle
            // 
            this.lblStateTitle.AutoSize = true;
            this.lblStateTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblStateTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblStateTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStateTitle.Location = new System.Drawing.Point(24, 300);
            this.lblStateTitle.Name = "lblStateTitle";
            this.lblStateTitle.Size = new System.Drawing.Size(66, 17);
            this.lblStateTitle.TabIndex = 13;
            this.lblStateTitle.Text = "Trạng thái";
            // 
            // lblStateValue
            // 
            this.lblStateValue.AutoSize = true;
            this.lblStateValue.BackColor = System.Drawing.Color.Transparent;
            this.lblStateValue.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStateValue.Location = new System.Drawing.Point(180, 300);
            this.lblStateValue.Name = "lblStateValue";
            this.lblStateValue.Size = new System.Drawing.Size(74, 17);
            this.lblStateValue.TabIndex = 14;
            this.lblStateValue.Text = "15/10/2026";
            // 
            // lblStateStatus
            // 
            this.lblStateStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStateStatus.AutoSize = true;
            this.lblStateStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStateStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStateStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.lblStateStatus.Location = new System.Drawing.Point(720, 300);
            this.lblStateStatus.Name = "lblStateStatus";
            this.lblStateStatus.Size = new System.Drawing.Size(104, 17);
            this.lblStateStatus.TabIndex = 15;
            this.lblStateStatus.Text = "Đang hoạt động";
            this.lblStateStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CreditCardUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1300, 750);
            this.Controls.Add(this.pnlInfoCard);
            this.Controls.Add(this.pnlLeft);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CreditCardUserForm";
            this.Padding = new System.Windows.Forms.Padding(24);
            this.Text = "Thẻ của tôi";
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.pnlCreditCard.ResumeLayout(false);
            this.pnlCreditCard.PerformLayout();
            this.pnlToggleTrack.ResumeLayout(false);
            this.pnlInfoCard.ResumeLayout(false);
            this.pnlInfoCard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLeft;
        private BankAccountManagement.Forms.RoundedPanel pnlCreditCard;
        private System.Windows.Forms.Label lblCardBrand;
        private System.Windows.Forms.Label lblChip;
        private System.Windows.Forms.Label lblCardNumber;
        private System.Windows.Forms.Label lblHolderTitle;
        private System.Windows.Forms.Label lblHolderName;
        private System.Windows.Forms.Label lblExpiryTitle;
        private System.Windows.Forms.Label lblExpiryValue;
        private System.Windows.Forms.Panel pnlLogoCircle1;
        private System.Windows.Forms.Panel pnlLogoCircle2;

        private System.Windows.Forms.Label lblStatusTitle;
        private System.Windows.Forms.Label lblStatusValue;
        private BankAccountManagement.Forms.RoundedPanel pnlToggleTrack;
        private BankAccountManagement.Forms.RoundedPanel pnlToggleKnob;
        private BankAccountManagement.Forms.RoundedButton btnViewCardInfo;

        private BankAccountManagement.Forms.RoundedPanel pnlInfoCard;
        private System.Windows.Forms.Label lblInfoTitle;
        private System.Windows.Forms.Label lblNumTitle;
        private System.Windows.Forms.Label lblNumValue;
        private System.Windows.Forms.Label lblNumStatus;
        private System.Windows.Forms.Label lblBalTitle;
        private System.Windows.Forms.Label lblBalValue;
        private System.Windows.Forms.Label lblBalDate;
        private System.Windows.Forms.Label lblStmtTitle;
        private System.Windows.Forms.Label lblStmtValue;
        private System.Windows.Forms.Label lblStmtDate;
        private System.Windows.Forms.Label lblDueTitle;
        private System.Windows.Forms.Label lblDueValue;
        private System.Windows.Forms.Label lblDueDate;
        private System.Windows.Forms.Label lblStateTitle;
        private System.Windows.Forms.Label lblStateValue;
        private System.Windows.Forms.Label lblStateStatus;
    }
}
