namespace BankAccountManagement.Forms
{
    partial class CreditCardForm
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

            this.pnlAdminHeader = new System.Windows.Forms.Panel();
            this.pnlAdminHeaderBorder = new System.Windows.Forms.Panel();
            this.lblAdminTitle = new System.Windows.Forms.Label();
            this.lblAdminSubtitle = new System.Windows.Forms.Label();
            this.lblAdminSearch = new System.Windows.Forms.Label();
            this.txtAdminSearch = new System.Windows.Forms.TextBox();
            this.btnAdminSearch = new System.Windows.Forms.Button();

            // Admin: Khung bên trái (Thông tin chi tiết thẻ)
            this.pnlAdminFormCard = new System.Windows.Forms.Panel();
            this.pnlAdminFormContent = new System.Windows.Forms.Panel();
            this.lblAdminFormAccent = new System.Windows.Forms.Panel();
            this.lblAdminFormTitle = new System.Windows.Forms.Label();
            this.pnlAdminFormDivider1 = new System.Windows.Forms.Panel();

            this.lblAdminCardNumber = new System.Windows.Forms.Label();
            this.txtAdminCardNumber = new System.Windows.Forms.TextBox();
            this.lblAdminAccountNumber = new System.Windows.Forms.Label();
            this.txtAdminAccountNumber = new System.Windows.Forms.TextBox();
            this.lblAdminCustomerCode = new System.Windows.Forms.Label();
            this.txtAdminCustomerCode = new System.Windows.Forms.TextBox();
            this.lblAdminCustomerName = new System.Windows.Forms.Label();
            this.txtAdminCustomerName = new System.Windows.Forms.TextBox();
            this.lblAdminBank = new System.Windows.Forms.Label();
            this.txtAdminBank = new System.Windows.Forms.TextBox();
            this.lblAdminCardType = new System.Windows.Forms.Label();
            this.txtAdminCardType = new System.Windows.Forms.TextBox();
            this.lblAdminBalance = new System.Windows.Forms.Label();
            this.txtAdminBalance = new System.Windows.Forms.TextBox();
            this.lblAdminStatus = new System.Windows.Forms.Label();
            this.txtAdminStatus = new System.Windows.Forms.TextBox();
            this.lblAdminIssueDate = new System.Windows.Forms.Label();
            this.txtAdminIssueDate = new System.Windows.Forms.TextBox();
            this.lblAdminExpiryDate = new System.Windows.Forms.Label();
            this.txtAdminExpiryDate = new System.Windows.Forms.TextBox();

            this.btnAdminUnlock = new System.Windows.Forms.Button();
            this.btnAdminRefresh = new System.Windows.Forms.Button();

            // Admin: Khung bên phải (Danh sách thẻ)
            this.pnlAdminListCard = new System.Windows.Forms.Panel();
            this.lblAdminListAccent = new System.Windows.Forms.Panel();
            this.lblAdminListTitle = new System.Windows.Forms.Label();
            this.lblAdminListCount = new System.Windows.Forms.Label();
            this.pnlAdminListDivider = new System.Windows.Forms.Panel();
            this.dgvAdminCards = new System.Windows.Forms.DataGridView();
            this.colAdminCardNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdminAccountNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdminCustomerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdminCustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdminBank = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdminCardType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdminBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdminIssueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdminExpiryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAdminStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.pnlAdminHeader.SuspendLayout();
            this.pnlAdminFormCard.SuspendLayout();
            this.pnlAdminFormContent.SuspendLayout();
            this.pnlAdminListCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdminCards)).BeginInit();
            this.SuspendLayout();

            // --- pnlAdminHeader ---
            this.pnlAdminHeader.BackColor = System.Drawing.Color.White;
            this.pnlAdminHeader.Controls.Add(this.pnlAdminHeaderBorder);
            this.pnlAdminHeader.Controls.Add(this.lblAdminTitle);
            this.pnlAdminHeader.Controls.Add(this.lblAdminSubtitle);
            this.pnlAdminHeader.Controls.Add(this.lblAdminSearch);
            this.pnlAdminHeader.Controls.Add(this.txtAdminSearch);
            this.pnlAdminHeader.Controls.Add(this.btnAdminSearch);
            this.pnlAdminHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAdminHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlAdminHeader.Name = "pnlAdminHeader";
            this.pnlAdminHeader.Size = new System.Drawing.Size(1300, 80);
            this.pnlAdminHeader.TabIndex = 0;

            this.pnlAdminHeaderBorder.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlAdminHeaderBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAdminHeaderBorder.Location = new System.Drawing.Point(0, 79);
            this.pnlAdminHeaderBorder.Name = "pnlAdminHeaderBorder";
            this.pnlAdminHeaderBorder.Size = new System.Drawing.Size(1300, 1);
            this.pnlAdminHeaderBorder.TabIndex = 5;

            this.lblAdminTitle.AutoSize = true;
            this.lblAdminTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblAdminTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblAdminTitle.Location = new System.Drawing.Point(24, 14);
            this.lblAdminTitle.Name = "lblAdminTitle";
            this.lblAdminTitle.Size = new System.Drawing.Size(306, 30);
            this.lblAdminTitle.TabIndex = 0;
            this.lblAdminTitle.Text = "QUẢN LÝ THẺ NGÂN HÀNG";

            this.lblAdminSubtitle.AutoSize = true;
            this.lblAdminSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAdminSubtitle.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblAdminSubtitle.Location = new System.Drawing.Point(26, 48);
            this.lblAdminSubtitle.Name = "lblAdminSubtitle";
            this.lblAdminSubtitle.Size = new System.Drawing.Size(300, 15);
            this.lblAdminSubtitle.TabIndex = 1;
            this.lblAdminSubtitle.Text = "Theo dõi và quản lý thẻ ngân hàng của khách hàng";

            this.lblAdminSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAdminSearch.AutoSize = true;
            this.lblAdminSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblAdminSearch.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblAdminSearch.Location = new System.Drawing.Point(850, 28);
            this.lblAdminSearch.Name = "lblAdminSearch";
            this.lblAdminSearch.Size = new System.Drawing.Size(65, 17);
            this.lblAdminSearch.TabIndex = 2;
            this.lblAdminSearch.Text = "Tìm kiếm:";

            this.txtAdminSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAdminSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdminSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtAdminSearch.Location = new System.Drawing.Point(920, 26);
            this.txtAdminSearch.Name = "txtAdminSearch";
            this.txtAdminSearch.Size = new System.Drawing.Size(250, 24);
            this.txtAdminSearch.TabIndex = 3;

            this.btnAdminSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdminSearch.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.btnAdminSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdminSearch.FlatAppearance.BorderSize = 0;
            this.btnAdminSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdminSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAdminSearch.ForeColor = System.Drawing.Color.White;
            this.btnAdminSearch.Location = new System.Drawing.Point(1180, 24);
            this.btnAdminSearch.Name = "btnAdminSearch";
            this.btnAdminSearch.Size = new System.Drawing.Size(90, 28);
            this.btnAdminSearch.TabIndex = 4;
            this.btnAdminSearch.Text = "Tìm";
            this.btnAdminSearch.UseVisualStyleBackColor = false;

            // --- pnlAdminFormCard (Trái: Chi tiết thẻ) ---
            this.pnlAdminFormCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlAdminFormCard.AutoScroll = true;
            this.pnlAdminFormCard.BackColor = System.Drawing.Color.White;
            this.pnlAdminFormCard.Controls.Add(this.pnlAdminFormContent);
            this.pnlAdminFormCard.Location = new System.Drawing.Point(20, 95);
            this.pnlAdminFormCard.Name = "pnlAdminFormCard";
            this.pnlAdminFormCard.Size = new System.Drawing.Size(360, 635);
            this.pnlAdminFormCard.TabIndex = 1;

            this.pnlAdminFormContent.BackColor = System.Drawing.Color.White;
            this.pnlAdminFormContent.Controls.Add(this.lblAdminFormAccent);
            this.pnlAdminFormContent.Controls.Add(this.lblAdminFormTitle);
            this.pnlAdminFormContent.Controls.Add(this.pnlAdminFormDivider1);
            this.pnlAdminFormContent.Controls.Add(this.lblAdminCardNumber);
            this.pnlAdminFormContent.Controls.Add(this.txtAdminCardNumber);
            this.pnlAdminFormContent.Controls.Add(this.lblAdminAccountNumber);
            this.pnlAdminFormContent.Controls.Add(this.txtAdminAccountNumber);
            this.pnlAdminFormContent.Controls.Add(this.lblAdminCustomerCode);
            this.pnlAdminFormContent.Controls.Add(this.txtAdminCustomerCode);
            this.pnlAdminFormContent.Controls.Add(this.lblAdminCustomerName);
            this.pnlAdminFormContent.Controls.Add(this.txtAdminCustomerName);
            this.pnlAdminFormContent.Controls.Add(this.lblAdminBank);
            this.pnlAdminFormContent.Controls.Add(this.txtAdminBank);
            this.pnlAdminFormContent.Controls.Add(this.lblAdminCardType);
            this.pnlAdminFormContent.Controls.Add(this.txtAdminCardType);
            this.pnlAdminFormContent.Controls.Add(this.lblAdminBalance);
            this.pnlAdminFormContent.Controls.Add(this.txtAdminBalance);
            this.pnlAdminFormContent.Controls.Add(this.lblAdminStatus);
            this.pnlAdminFormContent.Controls.Add(this.txtAdminStatus);
            this.pnlAdminFormContent.Controls.Add(this.lblAdminIssueDate);
            this.pnlAdminFormContent.Controls.Add(this.txtAdminIssueDate);
            this.pnlAdminFormContent.Controls.Add(this.lblAdminExpiryDate);
            this.pnlAdminFormContent.Controls.Add(this.txtAdminExpiryDate);
            this.pnlAdminFormContent.Controls.Add(this.btnAdminUnlock);
            this.pnlAdminFormContent.Controls.Add(this.btnAdminRefresh);
            this.pnlAdminFormContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAdminFormContent.Location = new System.Drawing.Point(0, 0);
            this.pnlAdminFormContent.Name = "pnlAdminFormContent";
            this.pnlAdminFormContent.Size = new System.Drawing.Size(340, 710);
            this.pnlAdminFormContent.TabIndex = 0;

            this.lblAdminFormAccent.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblAdminFormAccent.Location = new System.Drawing.Point(20, 18);
            this.lblAdminFormAccent.Name = "lblAdminFormAccent";
            this.lblAdminFormAccent.Size = new System.Drawing.Size(4, 22);
            this.lblAdminFormAccent.TabIndex = 0;

            this.lblAdminFormTitle.AutoSize = true;
            this.lblAdminFormTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblAdminFormTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblAdminFormTitle.Location = new System.Drawing.Point(30, 18);
            this.lblAdminFormTitle.Name = "lblAdminFormTitle";
            this.lblAdminFormTitle.Size = new System.Drawing.Size(184, 21);
            this.lblAdminFormTitle.TabIndex = 1;
            this.lblAdminFormTitle.Text = "Thông tin thẻ ngân hàng";

            this.pnlAdminFormDivider1.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlAdminFormDivider1.Location = new System.Drawing.Point(20, 50);
            this.pnlAdminFormDivider1.Name = "pnlAdminFormDivider1";
            this.pnlAdminFormDivider1.Size = new System.Drawing.Size(300, 1);
            this.pnlAdminFormDivider1.TabIndex = 2;

            // Field: Số thẻ
            this.lblAdminCardNumber.AutoSize = true;
            this.lblAdminCardNumber.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAdminCardNumber.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblAdminCardNumber.Location = new System.Drawing.Point(20, 62);
            this.lblAdminCardNumber.Name = "lblAdminCardNumber";
            this.lblAdminCardNumber.Size = new System.Drawing.Size(57, 15);
            this.lblAdminCardNumber.TabIndex = 3;
            this.lblAdminCardNumber.Text = "Số thẻ (*)";

            this.txtAdminCardNumber.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtAdminCardNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdminCardNumber.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtAdminCardNumber.Location = new System.Drawing.Point(20, 80);
            this.txtAdminCardNumber.Name = "txtAdminCardNumber";
            this.txtAdminCardNumber.ReadOnly = true;
            this.txtAdminCardNumber.Size = new System.Drawing.Size(300, 24);
            this.txtAdminCardNumber.TabIndex = 4;

            // Field: Số tài khoản
            this.lblAdminAccountNumber.AutoSize = true;
            this.lblAdminAccountNumber.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAdminAccountNumber.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblAdminAccountNumber.Location = new System.Drawing.Point(20, 110);
            this.lblAdminAccountNumber.Name = "lblAdminAccountNumber";
            this.lblAdminAccountNumber.Size = new System.Drawing.Size(117, 15);
            this.lblAdminAccountNumber.TabIndex = 5;
            this.lblAdminAccountNumber.Text = "Số tài khoản liên kết";

            this.txtAdminAccountNumber.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtAdminAccountNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdminAccountNumber.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtAdminAccountNumber.Location = new System.Drawing.Point(20, 128);
            this.txtAdminAccountNumber.Name = "txtAdminAccountNumber";
            this.txtAdminAccountNumber.ReadOnly = true;
            this.txtAdminAccountNumber.Size = new System.Drawing.Size(300, 24);
            this.txtAdminAccountNumber.TabIndex = 6;

            // Field: Mã khách hàng
            this.lblAdminCustomerCode.AutoSize = true;
            this.lblAdminCustomerCode.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAdminCustomerCode.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblAdminCustomerCode.Location = new System.Drawing.Point(20, 158);
            this.lblAdminCustomerCode.Name = "lblAdminCustomerCode";
            this.lblAdminCustomerCode.Size = new System.Drawing.Size(107, 15);
            this.lblAdminCustomerCode.TabIndex = 7;
            this.lblAdminCustomerCode.Text = "Mã Khách Hàng (*)";

            this.txtAdminCustomerCode.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtAdminCustomerCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdminCustomerCode.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtAdminCustomerCode.Location = new System.Drawing.Point(20, 176);
            this.txtAdminCustomerCode.Name = "txtAdminCustomerCode";
            this.txtAdminCustomerCode.ReadOnly = true;
            this.txtAdminCustomerCode.Size = new System.Drawing.Size(300, 24);
            this.txtAdminCustomerCode.TabIndex = 8;

            // Field: Khách hàng
            this.lblAdminCustomerName.AutoSize = true;
            this.lblAdminCustomerName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAdminCustomerName.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblAdminCustomerName.Location = new System.Drawing.Point(20, 206);
            this.lblAdminCustomerName.Name = "lblAdminCustomerName";
            this.lblAdminCustomerName.Size = new System.Drawing.Size(89, 15);
            this.lblAdminCustomerName.TabIndex = 9;
            this.lblAdminCustomerName.Text = "Khách hàng (*)";

            this.txtAdminCustomerName.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtAdminCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdminCustomerName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtAdminCustomerName.Location = new System.Drawing.Point(20, 224);
            this.txtAdminCustomerName.Name = "txtAdminCustomerName";
            this.txtAdminCustomerName.ReadOnly = true;
            this.txtAdminCustomerName.Size = new System.Drawing.Size(300, 24);
            this.txtAdminCustomerName.TabIndex = 10;

            // Field: Ngân hàng
            this.lblAdminBank.AutoSize = true;
            this.lblAdminBank.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAdminBank.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblAdminBank.Location = new System.Drawing.Point(20, 254);
            this.lblAdminBank.Name = "lblAdminBank";
            this.lblAdminBank.Size = new System.Drawing.Size(84, 15);
            this.lblAdminBank.TabIndex = 11;
            this.lblAdminBank.Text = "Ngân hàng (*)";

            this.txtAdminBank.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtAdminBank.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdminBank.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtAdminBank.Location = new System.Drawing.Point(20, 272);
            this.txtAdminBank.Name = "txtAdminBank";
            this.txtAdminBank.ReadOnly = true;
            this.txtAdminBank.Size = new System.Drawing.Size(300, 24);
            this.txtAdminBank.TabIndex = 12;

            // Field: Loại thẻ
            this.lblAdminCardType.AutoSize = true;
            this.lblAdminCardType.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAdminCardType.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblAdminCardType.Location = new System.Drawing.Point(20, 302);
            this.lblAdminCardType.Name = "lblAdminCardType";
            this.lblAdminCardType.Size = new System.Drawing.Size(51, 15);
            this.lblAdminCardType.TabIndex = 13;
            this.lblAdminCardType.Text = "Loại thẻ";

            this.txtAdminCardType.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtAdminCardType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdminCardType.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtAdminCardType.Location = new System.Drawing.Point(20, 320);
            this.txtAdminCardType.Name = "txtAdminCardType";
            this.txtAdminCardType.ReadOnly = true;
            this.txtAdminCardType.Size = new System.Drawing.Size(300, 24);
            this.txtAdminCardType.TabIndex = 14;

            // Field: Số dư
            this.lblAdminBalance.AutoSize = true;
            this.lblAdminBalance.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAdminBalance.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblAdminBalance.Location = new System.Drawing.Point(20, 350);
            this.lblAdminBalance.Name = "lblAdminBalance";
            this.lblAdminBalance.Size = new System.Drawing.Size(40, 15);
            this.lblAdminBalance.TabIndex = 15;
            this.lblAdminBalance.Text = "Số dư";

            this.txtAdminBalance.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtAdminBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdminBalance.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtAdminBalance.Location = new System.Drawing.Point(20, 368);
            this.txtAdminBalance.Name = "txtAdminBalance";
            this.txtAdminBalance.ReadOnly = true;
            this.txtAdminBalance.Size = new System.Drawing.Size(300, 24);
            this.txtAdminBalance.TabIndex = 16;

            // Field: Trạng thái
            this.lblAdminStatus.AutoSize = true;
            this.lblAdminStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAdminStatus.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblAdminStatus.Location = new System.Drawing.Point(20, 398);
            this.lblAdminStatus.Name = "lblAdminStatus";
            this.lblAdminStatus.Size = new System.Drawing.Size(61, 15);
            this.lblAdminStatus.TabIndex = 17;
            this.lblAdminStatus.Text = "Trạng thái";

            this.txtAdminStatus.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtAdminStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdminStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.txtAdminStatus.Location = new System.Drawing.Point(20, 416);
            this.txtAdminStatus.Name = "txtAdminStatus";
            this.txtAdminStatus.ReadOnly = true;
            this.txtAdminStatus.Size = new System.Drawing.Size(300, 24);
            this.txtAdminStatus.TabIndex = 18;

            // Field: Ngày cấp
            this.lblAdminIssueDate.AutoSize = true;
            this.lblAdminIssueDate.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAdminIssueDate.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblAdminIssueDate.Location = new System.Drawing.Point(20, 446);
            this.lblAdminIssueDate.Name = "lblAdminIssueDate";
            this.lblAdminIssueDate.Size = new System.Drawing.Size(57, 15);
            this.lblAdminIssueDate.TabIndex = 19;
            this.lblAdminIssueDate.Text = "Ngày cấp";

            this.txtAdminIssueDate.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtAdminIssueDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdminIssueDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtAdminIssueDate.Location = new System.Drawing.Point(20, 464);
            this.txtAdminIssueDate.Name = "txtAdminIssueDate";
            this.txtAdminIssueDate.ReadOnly = true;
            this.txtAdminIssueDate.Size = new System.Drawing.Size(142, 24);
            this.txtAdminIssueDate.TabIndex = 20;

            // Field: Ngày hết hạn
            this.lblAdminExpiryDate.AutoSize = true;
            this.lblAdminExpiryDate.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblAdminExpiryDate.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblAdminExpiryDate.Location = new System.Drawing.Point(178, 446);
            this.lblAdminExpiryDate.Name = "lblAdminExpiryDate";
            this.lblAdminExpiryDate.Size = new System.Drawing.Size(78, 15);
            this.lblAdminExpiryDate.TabIndex = 21;
            this.lblAdminExpiryDate.Text = "Ngày hết hạn";

            this.txtAdminExpiryDate.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.txtAdminExpiryDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdminExpiryDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtAdminExpiryDate.Location = new System.Drawing.Point(178, 464);
            this.txtAdminExpiryDate.Name = "txtAdminExpiryDate";
            this.txtAdminExpiryDate.ReadOnly = true;
            this.txtAdminExpiryDate.Size = new System.Drawing.Size(142, 24);
            this.txtAdminExpiryDate.TabIndex = 22;

            // Buttons: Mở khóa thẻ / Làm mới
            this.btnAdminUnlock.BackColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.btnAdminUnlock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdminUnlock.FlatAppearance.BorderSize = 0;
            this.btnAdminUnlock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdminUnlock.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAdminUnlock.ForeColor = System.Drawing.Color.White;
            this.btnAdminUnlock.Location = new System.Drawing.Point(20, 508);
            this.btnAdminUnlock.Name = "btnAdminUnlock";
            this.btnAdminUnlock.Size = new System.Drawing.Size(300, 38);
            this.btnAdminUnlock.TabIndex = 23;
            this.btnAdminUnlock.Text = "🔓  Mở khóa thẻ";
            this.btnAdminUnlock.UseVisualStyleBackColor = false;

            this.btnAdminRefresh.BackColor = System.Drawing.Color.White;
            this.btnAdminRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdminRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnAdminRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdminRefresh.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAdminRefresh.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.btnAdminRefresh.Location = new System.Drawing.Point(20, 554);
            this.btnAdminRefresh.Name = "btnAdminRefresh";
            this.btnAdminRefresh.Size = new System.Drawing.Size(300, 34);
            this.btnAdminRefresh.TabIndex = 24;
            this.btnAdminRefresh.Text = "Làm mới dữ liệu";
            this.btnAdminRefresh.UseVisualStyleBackColor = false;

            // --- pnlAdminListCard (Phải: Bảng danh sách thẻ) ---
            this.pnlAdminListCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAdminListCard.BackColor = System.Drawing.Color.White;
            this.pnlAdminListCard.Controls.Add(this.lblAdminListAccent);
            this.pnlAdminListCard.Controls.Add(this.lblAdminListTitle);
            this.pnlAdminListCard.Controls.Add(this.lblAdminListCount);
            this.pnlAdminListCard.Controls.Add(this.pnlAdminListDivider);
            this.pnlAdminListCard.Controls.Add(this.dgvAdminCards);
            this.pnlAdminListCard.Location = new System.Drawing.Point(400, 95);
            this.pnlAdminListCard.Name = "pnlAdminListCard";
            this.pnlAdminListCard.Size = new System.Drawing.Size(880, 635);
            this.pnlAdminListCard.TabIndex = 2;

            this.lblAdminListAccent.BackColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.lblAdminListAccent.Location = new System.Drawing.Point(24, 18);
            this.lblAdminListAccent.Name = "lblAdminListAccent";
            this.lblAdminListAccent.Size = new System.Drawing.Size(4, 22);
            this.lblAdminListAccent.TabIndex = 0;

            this.lblAdminListTitle.AutoSize = true;
            this.lblAdminListTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblAdminListTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblAdminListTitle.Location = new System.Drawing.Point(34, 18);
            this.lblAdminListTitle.Name = "lblAdminListTitle";
            this.lblAdminListTitle.Size = new System.Drawing.Size(188, 21);
            this.lblAdminListTitle.TabIndex = 1;
            this.lblAdminListTitle.Text = "Danh sách thẻ ngân hàng";

            this.lblAdminListCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAdminListCount.AutoSize = true;
            this.lblAdminListCount.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblAdminListCount.ForeColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.lblAdminListCount.Location = new System.Drawing.Point(740, 20);
            this.lblAdminListCount.Name = "lblAdminListCount";
            this.lblAdminListCount.Size = new System.Drawing.Size(103, 17);
            this.lblAdminListCount.TabIndex = 2;
            this.lblAdminListCount.Text = "0 thẻ ngân hàng";
            this.lblAdminListCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.pnlAdminListDivider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAdminListDivider.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlAdminListDivider.Location = new System.Drawing.Point(24, 50);
            this.pnlAdminListDivider.Name = "pnlAdminListDivider";
            this.pnlAdminListDivider.Size = new System.Drawing.Size(832, 1);
            this.pnlAdminListDivider.TabIndex = 3;

            // DataGridView: dgvAdminCards
            this.dgvAdminCards.AllowUserToAddRows = false;
            this.dgvAdminCards.AllowUserToDeleteRows = false;
            this.dgvAdminCards.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.dgvAdminCards.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAdminCards.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAdminCards.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAdminCards.BackgroundColor = System.Drawing.Color.White;
            this.dgvAdminCards.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAdminCards.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAdminCards.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAdminCards.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAdminCards.ColumnHeadersHeight = 40;
            this.dgvAdminCards.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAdminCards.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colAdminCardNumber,
                this.colAdminAccountNumber,
                this.colAdminCustomerCode,
                this.colAdminCustomerName,
                this.colAdminBank,
                this.colAdminCardType,
                this.colAdminBalance,
                this.colAdminIssueDate,
                this.colAdminExpiryDate,
                this.colAdminStatus
            });
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.25F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAdminCards.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAdminCards.EnableHeadersVisualStyles = false;
            this.dgvAdminCards.GridColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.dgvAdminCards.Location = new System.Drawing.Point(24, 60);
            this.dgvAdminCards.MultiSelect = false;
            this.dgvAdminCards.Name = "dgvAdminCards";
            this.dgvAdminCards.ReadOnly = true;
            this.dgvAdminCards.RowHeadersVisible = false;
            this.dgvAdminCards.RowTemplate.Height = 36;
            this.dgvAdminCards.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAdminCards.Size = new System.Drawing.Size(832, 555);
            this.dgvAdminCards.TabIndex = 4;

            // Columns
            this.colAdminCardNumber.DataPropertyName = "FullCardNumber";
            this.colAdminCardNumber.FillWeight = 115;
            this.colAdminCardNumber.HeaderText = "Số thẻ";
            this.colAdminCardNumber.Name = "colAdminCardNumber";
            this.colAdminCardNumber.ReadOnly = true;

            this.colAdminAccountNumber.DataPropertyName = "AccountNumber";
            this.colAdminAccountNumber.FillWeight = 85;
            this.colAdminAccountNumber.HeaderText = "Số tài khoản";
            this.colAdminAccountNumber.Name = "colAdminAccountNumber";
            this.colAdminAccountNumber.ReadOnly = true;

            this.colAdminCustomerCode.DataPropertyName = "CustomerId";
            this.colAdminCustomerCode.FillWeight = 75;
            this.colAdminCustomerCode.HeaderText = "Mã KH";
            this.colAdminCustomerCode.Name = "colAdminCustomerCode";
            this.colAdminCustomerCode.ReadOnly = true;

            this.colAdminCustomerName.DataPropertyName = "CardHolderName";
            this.colAdminCustomerName.FillWeight = 110;
            this.colAdminCustomerName.HeaderText = "Khách hàng";
            this.colAdminCustomerName.Name = "colAdminCustomerName";
            this.colAdminCustomerName.ReadOnly = true;

            this.colAdminBank.DataPropertyName = "BankName";
            this.colAdminBank.FillWeight = 85;
            this.colAdminBank.HeaderText = "Ngân hàng";
            this.colAdminBank.Name = "colAdminBank";
            this.colAdminBank.ReadOnly = true;

            this.colAdminCardType.DataPropertyName = "CardType";
            this.colAdminCardType.FillWeight = 80;
            this.colAdminCardType.HeaderText = "Loại thẻ";
            this.colAdminCardType.Name = "colAdminCardType";
            this.colAdminCardType.ReadOnly = true;

            this.colAdminBalance.DataPropertyName = "Balance";
            this.colAdminBalance.FillWeight = 90;
            this.colAdminBalance.HeaderText = "Số dư";
            this.colAdminBalance.Name = "colAdminBalance";
            this.colAdminBalance.ReadOnly = true;

            this.colAdminIssueDate.DataPropertyName = "IssueDate";
            this.colAdminIssueDate.FillWeight = 80;
            this.colAdminIssueDate.HeaderText = "Ngày cấp";
            this.colAdminIssueDate.Name = "colAdminIssueDate";
            this.colAdminIssueDate.ReadOnly = true;

            this.colAdminExpiryDate.DataPropertyName = "ExpiryDate";
            this.colAdminExpiryDate.FillWeight = 80;
            this.colAdminExpiryDate.HeaderText = "Hết hạn";
            this.colAdminExpiryDate.Name = "colAdminExpiryDate";
            this.colAdminExpiryDate.ReadOnly = true;

            this.colAdminStatus.DataPropertyName = "Status";
            this.colAdminStatus.FillWeight = 80;
            this.colAdminStatus.HeaderText = "Trạng thái";
            this.colAdminStatus.Name = "colAdminStatus";
            this.colAdminStatus.ReadOnly = true;

            // Form Setup
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.ClientSize = new System.Drawing.Size(1300, 750);
            this.Controls.Add(this.pnlAdminListCard);
            this.Controls.Add(this.pnlAdminFormCard);
            this.Controls.Add(this.pnlAdminHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CreditCardForm";
            this.Text = "Quản lý thẻ ngân hàng";

            this.pnlAdminHeader.ResumeLayout(false);
            this.pnlAdminHeader.PerformLayout();
            this.pnlAdminFormCard.ResumeLayout(false);
            this.pnlAdminFormContent.ResumeLayout(false);
            this.pnlAdminFormContent.PerformLayout();
            this.pnlAdminListCard.ResumeLayout(false);
            this.pnlAdminListCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdminCards)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlAdminHeader;
        private System.Windows.Forms.Panel pnlAdminHeaderBorder;
        private System.Windows.Forms.Label lblAdminTitle;
        private System.Windows.Forms.Label lblAdminSubtitle;
        private System.Windows.Forms.Label lblAdminSearch;
        private System.Windows.Forms.TextBox txtAdminSearch;
        private System.Windows.Forms.Button btnAdminSearch;

        // Admin Left Form
        private System.Windows.Forms.Panel pnlAdminFormCard;
        private System.Windows.Forms.Panel pnlAdminFormContent;
        private System.Windows.Forms.Panel lblAdminFormAccent;
        private System.Windows.Forms.Label lblAdminFormTitle;
        private System.Windows.Forms.Panel pnlAdminFormDivider1;

        private System.Windows.Forms.Label lblAdminCardNumber;
        private System.Windows.Forms.TextBox txtAdminCardNumber;
        private System.Windows.Forms.Label lblAdminAccountNumber;
        private System.Windows.Forms.TextBox txtAdminAccountNumber;
        private System.Windows.Forms.Label lblAdminCustomerCode;
        private System.Windows.Forms.TextBox txtAdminCustomerCode;
        private System.Windows.Forms.Label lblAdminCustomerName;
        private System.Windows.Forms.TextBox txtAdminCustomerName;
        private System.Windows.Forms.Label lblAdminBank;
        private System.Windows.Forms.TextBox txtAdminBank;
        private System.Windows.Forms.Label lblAdminCardType;
        private System.Windows.Forms.TextBox txtAdminCardType;
        private System.Windows.Forms.Label lblAdminBalance;
        private System.Windows.Forms.TextBox txtAdminBalance;
        private System.Windows.Forms.Label lblAdminStatus;
        private System.Windows.Forms.TextBox txtAdminStatus;
        private System.Windows.Forms.Label lblAdminIssueDate;
        private System.Windows.Forms.TextBox txtAdminIssueDate;
        private System.Windows.Forms.Label lblAdminExpiryDate;
        private System.Windows.Forms.TextBox txtAdminExpiryDate;

        private System.Windows.Forms.Button btnAdminUnlock;
        private System.Windows.Forms.Button btnAdminRefresh;

        // Admin Right Grid
        private System.Windows.Forms.Panel pnlAdminListCard;
        private System.Windows.Forms.Panel lblAdminListAccent;
        private System.Windows.Forms.Label lblAdminListTitle;
        private System.Windows.Forms.Label lblAdminListCount;
        private System.Windows.Forms.Panel pnlAdminListDivider;
        private System.Windows.Forms.DataGridView dgvAdminCards;

        private System.Windows.Forms.DataGridViewTextBoxColumn colAdminCardNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdminAccountNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdminCustomerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdminCustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdminBank;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdminCardType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdminBalance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdminIssueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdminExpiryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAdminStatus;
    }
}