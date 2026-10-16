namespace BankAccountManagement.Forms
{
    partial class SaveMoneyAdminForm
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
            this.pnlAdminRoot = new System.Windows.Forms.Panel();
            this.tblAdminMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeftCustomer = new System.Windows.Forms.Panel();
            this.pnlCustWrapper = new BankAccountManagement.Forms.RoundedPanel();
            this.pnlCustomerDetailBox = new BankAccountManagement.Forms.RoundedPanel();
            this.pnlCustActionButtons = new System.Windows.Forms.Panel();
            this.btnClearCustomerFilter = new BankAccountManagement.Forms.RoundedButton();
            this.lblCustSavingsCount = new System.Windows.Forms.Label();
            this.lblCustSavingsCountTitle = new System.Windows.Forms.Label();
            this.lblCustTotalSavings = new System.Windows.Forms.Label();
            this.lblCustTotalSavingsTitle = new System.Windows.Forms.Label();
            this.lblCustBalance = new System.Windows.Forms.Label();
            this.lblCustBalanceTitle = new System.Windows.Forms.Label();
            this.lblCustPhone = new System.Windows.Forms.Label();
            this.lblCustPhoneTitle = new System.Windows.Forms.Label();
            this.lblCustCccd = new System.Windows.Forms.Label();
            this.lblCustCccdTitle = new System.Windows.Forms.Label();
            this.lblCustUsername = new System.Windows.Forms.Label();
            this.lblCustUsernameTitle = new System.Windows.Forms.Label();
            this.lblCustHeaderCode = new System.Windows.Forms.Label();
            this.lblCustHeaderName = new System.Windows.Forms.Label();
            this.pnlCustAvatar = new BankAccountManagement.Forms.RoundedPanel();
            this.lblCustAvatarText = new System.Windows.Forms.Label();
            this.pnlCustGridWrapper = new BankAccountManagement.Forms.RoundedPanel();
            this.dgvCustomers = new System.Windows.Forms.DataGridView();
            this.pnlCustSearch = new BankAccountManagement.Forms.RoundedPanel();
            this.txtSearchCustomer = new System.Windows.Forms.TextBox();
            this.lblCustSearchIcon = new System.Windows.Forms.Label();
            this.lblCustPanelTitle = new System.Windows.Forms.Label();
            this.pnlRightSavings = new System.Windows.Forms.Panel();
            this.pnlSavingsWrapper = new BankAccountManagement.Forms.RoundedPanel();
            this.pnlAdminActionBar = new System.Windows.Forms.Panel();
            this.btnAdminExport = new BankAccountManagement.Forms.RoundedButton();
            this.btnAdminViewReceipt = new BankAccountManagement.Forms.RoundedButton();
            this.btnAdminSettle = new BankAccountManagement.Forms.RoundedButton();
            this.lblSelectedSummary = new System.Windows.Forms.Label();
            this.pnlSavingsGridWrapper = new BankAccountManagement.Forms.RoundedPanel();
            this.dgvAdminSavings = new System.Windows.Forms.DataGridView();
            this.pnlSavingsFilterBar = new System.Windows.Forms.Panel();
            this.btnRefreshSavings = new BankAccountManagement.Forms.RoundedButton();
            this.pnlSavingsSearch = new BankAccountManagement.Forms.RoundedPanel();
            this.txtSearchSavings = new System.Windows.Forms.TextBox();
            this.lblSavingsSearchIcon = new System.Windows.Forms.Label();
            this.btnTabSettled = new BankAccountManagement.Forms.RoundedButton();
            this.btnTabActive = new BankAccountManagement.Forms.RoundedButton();
            this.btnTabAll = new BankAccountManagement.Forms.RoundedButton();
            this.lblSavingsPanelTitle = new System.Windows.Forms.Label();
            this.pnlTopBanner = new BankAccountManagement.Forms.RoundedPanel();
            this.tblMetricsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pnlMetric4 = new BankAccountManagement.Forms.RoundedPanel();
            this.lblMetric4Value = new System.Windows.Forms.Label();
            this.lblMetric4Title = new System.Windows.Forms.Label();
            this.pnlMetric3 = new BankAccountManagement.Forms.RoundedPanel();
            this.lblMetric3Value = new System.Windows.Forms.Label();
            this.lblMetric3Title = new System.Windows.Forms.Label();
            this.pnlMetric2 = new BankAccountManagement.Forms.RoundedPanel();
            this.lblMetric2Value = new System.Windows.Forms.Label();
            this.lblMetric2Title = new System.Windows.Forms.Label();
            this.pnlMetric1 = new BankAccountManagement.Forms.RoundedPanel();
            this.lblMetric1Value = new System.Windows.Forms.Label();
            this.lblMetric1Title = new System.Windows.Forms.Label();
            this.pnlAdminRoot.SuspendLayout();
            this.tblAdminMain.SuspendLayout();
            this.pnlLeftCustomer.SuspendLayout();
            this.pnlCustWrapper.SuspendLayout();
            this.pnlCustomerDetailBox.SuspendLayout();
            this.pnlCustActionButtons.SuspendLayout();
            this.pnlCustAvatar.SuspendLayout();
            this.pnlCustGridWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).BeginInit();
            this.pnlCustSearch.SuspendLayout();
            this.pnlRightSavings.SuspendLayout();
            this.pnlSavingsWrapper.SuspendLayout();
            this.pnlAdminActionBar.SuspendLayout();
            this.pnlSavingsGridWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdminSavings)).BeginInit();
            this.pnlSavingsFilterBar.SuspendLayout();
            this.pnlSavingsSearch.SuspendLayout();
            this.pnlTopBanner.SuspendLayout();
            this.tblMetricsLayout.SuspendLayout();
            this.pnlMetric4.SuspendLayout();
            this.pnlMetric3.SuspendLayout();
            this.pnlMetric2.SuspendLayout();
            this.pnlMetric1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlAdminRoot
            // 
            this.pnlAdminRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlAdminRoot.Controls.Add(this.tblAdminMain);
            this.pnlAdminRoot.Controls.Add(this.pnlTopBanner);
            this.pnlAdminRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAdminRoot.Location = new System.Drawing.Point(0, 0);
            this.pnlAdminRoot.Name = "pnlAdminRoot";
            this.pnlAdminRoot.Padding = new System.Windows.Forms.Padding(16);
            this.pnlAdminRoot.Size = new System.Drawing.Size(1120, 720);
            this.pnlAdminRoot.TabIndex = 0;
            // 
            // tblAdminMain
            // 
            this.tblAdminMain.ColumnCount = 2;
            this.tblAdminMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 360F));
            this.tblAdminMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAdminMain.Controls.Add(this.pnlLeftCustomer, 0, 0);
            this.tblAdminMain.Controls.Add(this.pnlRightSavings, 1, 0);
            this.tblAdminMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblAdminMain.Location = new System.Drawing.Point(16, 106);
            this.tblAdminMain.Name = "tblAdminMain";
            this.tblAdminMain.RowCount = 1;
            this.tblAdminMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblAdminMain.Size = new System.Drawing.Size(1088, 598);
            this.tblAdminMain.TabIndex = 1;
            // 
            // pnlLeftCustomer
            // 
            this.pnlLeftCustomer.Controls.Add(this.pnlCustWrapper);
            this.pnlLeftCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeftCustomer.Location = new System.Drawing.Point(0, 0);
            this.pnlLeftCustomer.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.pnlLeftCustomer.Name = "pnlLeftCustomer";
            this.pnlLeftCustomer.Size = new System.Drawing.Size(348, 598);
            this.pnlLeftCustomer.TabIndex = 0;
            // 
            // pnlCustWrapper
            // 
            this.pnlCustWrapper.BackColor = System.Drawing.Color.Transparent;
            this.pnlCustWrapper.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlCustWrapper.BorderRadius = 16;
            this.pnlCustWrapper.BorderSize = 1;
            this.pnlCustWrapper.Controls.Add(this.pnlCustomerDetailBox);
            this.pnlCustWrapper.Controls.Add(this.pnlCustGridWrapper);
            this.pnlCustWrapper.Controls.Add(this.pnlCustSearch);
            this.pnlCustWrapper.Controls.Add(this.lblCustPanelTitle);
            this.pnlCustWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCustWrapper.FillColor = System.Drawing.Color.White;
            this.pnlCustWrapper.Location = new System.Drawing.Point(0, 0);
            this.pnlCustWrapper.Name = "pnlCustWrapper";
            this.pnlCustWrapper.Padding = new System.Windows.Forms.Padding(14);
            this.pnlCustWrapper.Size = new System.Drawing.Size(348, 598);
            this.pnlCustWrapper.TabIndex = 0;
            // 
            // pnlCustomerDetailBox
            // 
            this.pnlCustomerDetailBox.BackColor = System.Drawing.Color.Transparent;
            this.pnlCustomerDetailBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlCustomerDetailBox.BorderRadius = 12;
            this.pnlCustomerDetailBox.BorderSize = 1;
            this.pnlCustomerDetailBox.Controls.Add(this.pnlCustActionButtons);
            this.pnlCustomerDetailBox.Controls.Add(this.lblCustSavingsCount);
            this.pnlCustomerDetailBox.Controls.Add(this.lblCustSavingsCountTitle);
            this.pnlCustomerDetailBox.Controls.Add(this.lblCustTotalSavings);
            this.pnlCustomerDetailBox.Controls.Add(this.lblCustTotalSavingsTitle);
            this.pnlCustomerDetailBox.Controls.Add(this.lblCustBalance);
            this.pnlCustomerDetailBox.Controls.Add(this.lblCustBalanceTitle);
            this.pnlCustomerDetailBox.Controls.Add(this.lblCustPhone);
            this.pnlCustomerDetailBox.Controls.Add(this.lblCustPhoneTitle);
            this.pnlCustomerDetailBox.Controls.Add(this.lblCustCccd);
            this.pnlCustomerDetailBox.Controls.Add(this.lblCustCccdTitle);
            this.pnlCustomerDetailBox.Controls.Add(this.lblCustUsername);
            this.pnlCustomerDetailBox.Controls.Add(this.lblCustUsernameTitle);
            this.pnlCustomerDetailBox.Controls.Add(this.lblCustHeaderCode);
            this.pnlCustomerDetailBox.Controls.Add(this.lblCustHeaderName);
            this.pnlCustomerDetailBox.Controls.Add(this.pnlCustAvatar);
            this.pnlCustomerDetailBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCustomerDetailBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlCustomerDetailBox.Location = new System.Drawing.Point(14, 210);
            this.pnlCustomerDetailBox.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.pnlCustomerDetailBox.Name = "pnlCustomerDetailBox";
            this.pnlCustomerDetailBox.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.pnlCustomerDetailBox.Size = new System.Drawing.Size(320, 374);
            this.pnlCustomerDetailBox.TabIndex = 3;
            // 
            // pnlCustActionButtons
            // 
            this.pnlCustActionButtons.BackColor = System.Drawing.Color.Transparent;
            this.pnlCustActionButtons.Controls.Add(this.btnClearCustomerFilter);
            this.pnlCustActionButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCustActionButtons.Location = new System.Drawing.Point(12, 320);
            this.pnlCustActionButtons.Name = "pnlCustActionButtons";
            this.pnlCustActionButtons.Size = new System.Drawing.Size(296, 44);
            this.pnlCustActionButtons.TabIndex = 15;
            // 
            // btnClearCustomerFilter
            // 
            this.btnClearCustomerFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearCustomerFilter.BorderRadius = 10;
            this.btnClearCustomerFilter.BorderSize = 1;
            this.btnClearCustomerFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearCustomerFilter.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnClearCustomerFilter.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnClearCustomerFilter.Location = new System.Drawing.Point(0, 0);
            this.btnClearCustomerFilter.Name = "btnClearCustomerFilter";
            this.btnClearCustomerFilter.NormalBorder = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnClearCustomerFilter.NormalColor = System.Drawing.Color.White;
            this.btnClearCustomerFilter.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnClearCustomerFilter.Size = new System.Drawing.Size(296, 40);
            this.btnClearCustomerFilter.TabIndex = 0;
            this.btnClearCustomerFilter.Text = "🔄 Xem tất cả khách hàng";
            this.btnClearCustomerFilter.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            // 
            // 
            // lblCustSavingsCount
            // 
            this.lblCustSavingsCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCustSavingsCount.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblCustSavingsCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCustSavingsCount.Location = new System.Drawing.Point(140, 168);
            this.lblCustSavingsCount.Name = "lblCustSavingsCount";
            this.lblCustSavingsCount.Size = new System.Drawing.Size(168, 16);
            this.lblCustSavingsCount.TabIndex = 14;
            this.lblCustSavingsCount.Text = "0 đang gửi / 0 tất toán";
            this.lblCustSavingsCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCustSavingsCountTitle
            // 
            this.lblCustSavingsCountTitle.AutoSize = true;
            this.lblCustSavingsCountTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCustSavingsCountTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCustSavingsCountTitle.Location = new System.Drawing.Point(12, 168);
            this.lblCustSavingsCountTitle.Name = "lblCustSavingsCountTitle";
            this.lblCustSavingsCountTitle.Size = new System.Drawing.Size(84, 15);
            this.lblCustSavingsCountTitle.TabIndex = 13;
            this.lblCustSavingsCountTitle.Text = "Số sổ TKTK";
            // 
            // lblCustTotalSavings
            // 
            this.lblCustTotalSavings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCustTotalSavings.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblCustTotalSavings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblCustTotalSavings.Location = new System.Drawing.Point(140, 146);
            this.lblCustTotalSavings.Name = "lblCustTotalSavings";
            this.lblCustTotalSavings.Size = new System.Drawing.Size(168, 18);
            this.lblCustTotalSavings.TabIndex = 12;
            this.lblCustTotalSavings.Text = "0 đ";
            this.lblCustTotalSavings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCustTotalSavingsTitle
            // 
            this.lblCustTotalSavingsTitle.AutoSize = true;
            this.lblCustTotalSavingsTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCustTotalSavingsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCustTotalSavingsTitle.Location = new System.Drawing.Point(12, 146);
            this.lblCustTotalSavingsTitle.Name = "lblCustTotalSavingsTitle";
            this.lblCustTotalSavingsTitle.Size = new System.Drawing.Size(107, 15);
            this.lblCustTotalSavingsTitle.TabIndex = 11;
            this.lblCustTotalSavingsTitle.Text = "Tổng tiền đang gửi";
            // 
            // lblCustBalance
            // 
            this.lblCustBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCustBalance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCustBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.lblCustBalance.Location = new System.Drawing.Point(140, 124);
            this.lblCustBalance.Name = "lblCustBalance";
            this.lblCustBalance.Size = new System.Drawing.Size(168, 16);
            this.lblCustBalance.TabIndex = 10;
            this.lblCustBalance.Text = "0 đ";
            this.lblCustBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCustBalanceTitle
            // 
            this.lblCustBalanceTitle.AutoSize = true;
            this.lblCustBalanceTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCustBalanceTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCustBalanceTitle.Location = new System.Drawing.Point(12, 124);
            this.lblCustBalanceTitle.Name = "lblCustBalanceTitle";
            this.lblCustBalanceTitle.Size = new System.Drawing.Size(105, 15);
            this.lblCustBalanceTitle.TabIndex = 9;
            this.lblCustBalanceTitle.Text = "Số dư khả dụng";
            // 
            // lblCustPhone
            // 
            this.lblCustPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCustPhone.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblCustPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCustPhone.Location = new System.Drawing.Point(140, 102);
            this.lblCustPhone.Name = "lblCustPhone";
            this.lblCustPhone.Size = new System.Drawing.Size(168, 16);
            this.lblCustPhone.TabIndex = 8;
            this.lblCustPhone.Text = "-";
            this.lblCustPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCustPhoneTitle
            // 
            this.lblCustPhoneTitle.AutoSize = true;
            this.lblCustPhoneTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCustPhoneTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCustPhoneTitle.Location = new System.Drawing.Point(12, 102);
            this.lblCustPhoneTitle.Name = "lblCustPhoneTitle";
            this.lblCustPhoneTitle.Size = new System.Drawing.Size(76, 15);
            this.lblCustPhoneTitle.TabIndex = 7;
            this.lblCustPhoneTitle.Text = "Số điện thoại";
            // 
            // lblCustCccd
            // 
            this.lblCustCccd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCustCccd.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblCustCccd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCustCccd.Location = new System.Drawing.Point(140, 80);
            this.lblCustCccd.Name = "lblCustCccd";
            this.lblCustCccd.Size = new System.Drawing.Size(168, 16);
            this.lblCustCccd.TabIndex = 6;
            this.lblCustCccd.Text = "-";
            this.lblCustCccd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCustCccdTitle
            // 
            this.lblCustCccdTitle.AutoSize = true;
            this.lblCustCccdTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCustCccdTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCustCccdTitle.Location = new System.Drawing.Point(12, 80);
            this.lblCustCccdTitle.Name = "lblCustCccdTitle";
            this.lblCustCccdTitle.Size = new System.Drawing.Size(79, 15);
            this.lblCustCccdTitle.TabIndex = 5;
            this.lblCustCccdTitle.Text = "CMND / CCCD";
            // 
            // lblCustUsername
            // 
            this.lblCustUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCustUsername.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblCustUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCustUsername.Location = new System.Drawing.Point(140, 58);
            this.lblCustUsername.Name = "lblCustUsername";
            this.lblCustUsername.Size = new System.Drawing.Size(168, 16);
            this.lblCustUsername.TabIndex = 4;
            this.lblCustUsername.Text = "-";
            this.lblCustUsername.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCustUsernameTitle
            // 
            this.lblCustUsernameTitle.AutoSize = true;
            this.lblCustUsernameTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCustUsernameTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCustUsernameTitle.Location = new System.Drawing.Point(12, 58);
            this.lblCustUsernameTitle.Name = "lblCustUsernameTitle";
            this.lblCustUsernameTitle.Size = new System.Drawing.Size(86, 15);
            this.lblCustUsernameTitle.TabIndex = 3;
            this.lblCustUsernameTitle.Text = "Tên đăng nhập";
            // 
            // lblCustHeaderCode
            // 
            this.lblCustHeaderCode.AutoSize = true;
            this.lblCustHeaderCode.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblCustHeaderCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblCustHeaderCode.Location = new System.Drawing.Point(58, 30);
            this.lblCustHeaderCode.Name = "lblCustHeaderCode";
            this.lblCustHeaderCode.Size = new System.Drawing.Size(76, 13);
            this.lblCustHeaderCode.TabIndex = 2;
            this.lblCustHeaderCode.Text = "Mã KH: KH001";
            // 
            // lblCustHeaderName
            // 
            this.lblCustHeaderName.AutoSize = true;
            this.lblCustHeaderName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCustHeaderName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCustHeaderName.Location = new System.Drawing.Point(58, 10);
            this.lblCustHeaderName.Name = "lblCustHeaderName";
            this.lblCustHeaderName.Size = new System.Drawing.Size(147, 19);
            this.lblCustHeaderName.TabIndex = 1;
            this.lblCustHeaderName.Text = "Chưa chọn khách hàng";
            // 
            // pnlCustAvatar
            // 
            this.pnlCustAvatar.BackColor = System.Drawing.Color.Transparent;
            this.pnlCustAvatar.BorderColor = System.Drawing.Color.Transparent;
            this.pnlCustAvatar.BorderRadius = 20;
            this.pnlCustAvatar.BorderSize = 0;
            this.pnlCustAvatar.Controls.Add(this.lblCustAvatarText);
            this.pnlCustAvatar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.pnlCustAvatar.Location = new System.Drawing.Point(12, 10);
            this.pnlCustAvatar.Name = "pnlCustAvatar";
            this.pnlCustAvatar.Size = new System.Drawing.Size(40, 40);
            this.pnlCustAvatar.TabIndex = 0;
            // 
            // lblCustAvatarText
            // 
            this.lblCustAvatarText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCustAvatarText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCustAvatarText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblCustAvatarText.Location = new System.Drawing.Point(0, 0);
            this.lblCustAvatarText.Name = "lblCustAvatarText";
            this.lblCustAvatarText.Size = new System.Drawing.Size(40, 40);
            this.lblCustAvatarText.TabIndex = 0;
            this.lblCustAvatarText.Text = "KH";
            this.lblCustAvatarText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCustGridWrapper
            // 
            this.pnlCustGridWrapper.BackColor = System.Drawing.Color.White;
            this.pnlCustGridWrapper.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlCustGridWrapper.BorderRadius = 10;
            this.pnlCustGridWrapper.BorderSize = 1;
            this.pnlCustGridWrapper.Controls.Add(this.dgvCustomers);
            this.pnlCustGridWrapper.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCustGridWrapper.FillColor = System.Drawing.Color.White;
            this.pnlCustGridWrapper.Location = new System.Drawing.Point(14, 82);
            this.pnlCustGridWrapper.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.pnlCustGridWrapper.Name = "pnlCustGridWrapper";
            this.pnlCustGridWrapper.Padding = new System.Windows.Forms.Padding(1);
            this.pnlCustGridWrapper.Size = new System.Drawing.Size(320, 120);
            this.pnlCustGridWrapper.TabIndex = 2;
            // 
            // dgvCustomers
            // 
            this.dgvCustomers.AllowUserToAddRows = false;
            this.dgvCustomers.AllowUserToDeleteRows = false;
            this.dgvCustomers.AllowUserToResizeRows = false;
            this.dgvCustomers.BackgroundColor = System.Drawing.Color.White;
            this.dgvCustomers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCustomers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCustomers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvCustomers.ColumnHeadersHeight = 30;
            this.dgvCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCustomers.EnableHeadersVisualStyles = false;
            this.dgvCustomers.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.dgvCustomers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvCustomers.Location = new System.Drawing.Point(1, 1);
            this.dgvCustomers.MultiSelect = false;
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.ReadOnly = true;
            this.dgvCustomers.RowHeadersVisible = false;
            this.dgvCustomers.RowTemplate.Height = 30;
            this.dgvCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCustomers.Size = new System.Drawing.Size(318, 118);
            this.dgvCustomers.TabIndex = 0;
            // 
            // pnlCustSearch
            // 
            this.pnlCustSearch.BackColor = System.Drawing.Color.White;
            this.pnlCustSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.pnlCustSearch.BorderRadius = 8;
            this.pnlCustSearch.BorderSize = 1;
            this.pnlCustSearch.Controls.Add(this.lblCustSearchIcon);
            this.pnlCustSearch.Controls.Add(this.txtSearchCustomer);
            this.pnlCustSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCustSearch.FillColor = System.Drawing.Color.White;
            this.pnlCustSearch.Location = new System.Drawing.Point(14, 44);
            this.pnlCustSearch.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.pnlCustSearch.Name = "pnlCustSearch";
            this.pnlCustSearch.Size = new System.Drawing.Size(320, 36);
            this.pnlCustSearch.TabIndex = 1;
            // 
            // lblCustSearchIcon
            // 
            this.lblCustSearchIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 10F);
            this.lblCustSearchIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblCustSearchIcon.Location = new System.Drawing.Point(8, 8);
            this.lblCustSearchIcon.Name = "lblCustSearchIcon";
            this.lblCustSearchIcon.Size = new System.Drawing.Size(22, 20);
            this.lblCustSearchIcon.TabIndex = 0;
            this.lblCustSearchIcon.Text = "🔍";
            this.lblCustSearchIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSearchCustomer
            // 
            this.txtSearchCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchCustomer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchCustomer.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearchCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtSearchCustomer.Location = new System.Drawing.Point(34, 9);
            this.txtSearchCustomer.Name = "txtSearchCustomer";
            this.txtSearchCustomer.Size = new System.Drawing.Size(276, 17);
            this.txtSearchCustomer.TabIndex = 1;
            // 
            // lblCustPanelTitle
            // 
            this.lblCustPanelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCustPanelTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCustPanelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblCustPanelTitle.Location = new System.Drawing.Point(14, 14);
            this.lblCustPanelTitle.Name = "lblCustPanelTitle";
            this.lblCustPanelTitle.Size = new System.Drawing.Size(320, 30);
            this.lblCustPanelTitle.TabIndex = 0;
            this.lblCustPanelTitle.Text = "Thông tin khách hàng";
            // 
            // pnlRightSavings
            // 
            this.pnlRightSavings.Controls.Add(this.pnlSavingsWrapper);
            this.pnlRightSavings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRightSavings.Location = new System.Drawing.Point(360, 0);
            this.pnlRightSavings.Margin = new System.Windows.Forms.Padding(0);
            this.pnlRightSavings.Name = "pnlRightSavings";
            this.pnlRightSavings.Size = new System.Drawing.Size(728, 598);
            this.pnlRightSavings.TabIndex = 1;
            // 
            // pnlSavingsWrapper
            // 
            this.pnlSavingsWrapper.BackColor = System.Drawing.Color.Transparent;
            this.pnlSavingsWrapper.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlSavingsWrapper.BorderRadius = 16;
            this.pnlSavingsWrapper.BorderSize = 1;
            this.pnlSavingsWrapper.Controls.Add(this.pnlAdminActionBar);
            this.pnlSavingsWrapper.Controls.Add(this.pnlSavingsGridWrapper);
            this.pnlSavingsWrapper.Controls.Add(this.pnlSavingsFilterBar);
            this.pnlSavingsWrapper.Controls.Add(this.lblSavingsPanelTitle);
            this.pnlSavingsWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSavingsWrapper.FillColor = System.Drawing.Color.White;
            this.pnlSavingsWrapper.Location = new System.Drawing.Point(0, 0);
            this.pnlSavingsWrapper.Name = "pnlSavingsWrapper";
            this.pnlSavingsWrapper.Padding = new System.Windows.Forms.Padding(16);
            this.pnlSavingsWrapper.Size = new System.Drawing.Size(728, 598);
            this.pnlSavingsWrapper.TabIndex = 0;
            // 
            // pnlAdminActionBar
            // 
            this.pnlAdminActionBar.Controls.Add(this.btnAdminExport);
            this.pnlAdminActionBar.Controls.Add(this.btnAdminViewReceipt);
            this.pnlAdminActionBar.Controls.Add(this.btnAdminSettle);
            this.pnlAdminActionBar.Controls.Add(this.lblSelectedSummary);
            this.pnlAdminActionBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAdminActionBar.Location = new System.Drawing.Point(16, 534);
            this.pnlAdminActionBar.Name = "pnlAdminActionBar";
            this.pnlAdminActionBar.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.pnlAdminActionBar.Size = new System.Drawing.Size(696, 48);
            this.pnlAdminActionBar.TabIndex = 3;
            // 
            // btnAdminExport
            // 
            this.btnAdminExport.BorderRadius = 8;
            this.btnAdminExport.BorderSize = 1;
            this.btnAdminExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdminExport.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAdminExport.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnAdminExport.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnAdminExport.Location = new System.Drawing.Point(316, 6);
            this.btnAdminExport.Name = "btnAdminExport";
            this.btnAdminExport.NormalBorder = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnAdminExport.NormalColor = System.Drawing.Color.White;
            this.btnAdminExport.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnAdminExport.Size = new System.Drawing.Size(100, 42);
            this.btnAdminExport.TabIndex = 3;
            this.btnAdminExport.Text = "📊 Xuất dữ liệu";
            this.btnAdminExport.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            // 
            // btnAdminViewReceipt
            // 
            this.btnAdminViewReceipt.BorderRadius = 8;
            this.btnAdminViewReceipt.BorderSize = 1;
            this.btnAdminViewReceipt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdminViewReceipt.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAdminViewReceipt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAdminViewReceipt.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnAdminViewReceipt.Location = new System.Drawing.Point(416, 6);
            this.btnAdminViewReceipt.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnAdminViewReceipt.Name = "btnAdminViewReceipt";
            this.btnAdminViewReceipt.NormalBorder = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnAdminViewReceipt.NormalColor = System.Drawing.Color.White;
            this.btnAdminViewReceipt.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnAdminViewReceipt.Size = new System.Drawing.Size(120, 42);
            this.btnAdminViewReceipt.TabIndex = 2;
            this.btnAdminViewReceipt.Text = "📄 Xem chứng nhận";
            this.btnAdminViewReceipt.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            // 
            // btnAdminSettle
            // 
            this.btnAdminSettle.BorderRadius = 8;
            this.btnAdminSettle.BorderSize = 0;
            this.btnAdminSettle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdminSettle.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAdminSettle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdminSettle.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.btnAdminSettle.Location = new System.Drawing.Point(536, 6);
            this.btnAdminSettle.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.btnAdminSettle.Name = "btnAdminSettle";
            this.btnAdminSettle.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnAdminSettle.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(83)))), ((int)(((byte)(45)))));
            this.btnAdminSettle.Size = new System.Drawing.Size(160, 42);
            this.btnAdminSettle.TabIndex = 1;
            this.btnAdminSettle.Text = "💰 Tất toán sổ TKTK";
            this.btnAdminSettle.TextColor = System.Drawing.Color.White;
            // 
            // lblSelectedSummary
            // 
            this.lblSelectedSummary.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSelectedSummary.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSelectedSummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblSelectedSummary.Location = new System.Drawing.Point(0, 6);
            this.lblSelectedSummary.Name = "lblSelectedSummary";
            this.lblSelectedSummary.Size = new System.Drawing.Size(310, 42);
            this.lblSelectedSummary.TabIndex = 0;
            this.lblSelectedSummary.Text = "Chọn một sổ tiết kiệm trong bảng để xem chi tiết hoặc thực hiện thao tác.";
            this.lblSelectedSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSavingsGridWrapper
            // 
            this.pnlSavingsGridWrapper.BackColor = System.Drawing.Color.White;
            this.pnlSavingsGridWrapper.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlSavingsGridWrapper.BorderRadius = 12;
            this.pnlSavingsGridWrapper.BorderSize = 1;
            this.pnlSavingsGridWrapper.Controls.Add(this.dgvAdminSavings);
            this.pnlSavingsGridWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSavingsGridWrapper.FillColor = System.Drawing.Color.White;
            this.pnlSavingsGridWrapper.Location = new System.Drawing.Point(16, 92);
            this.pnlSavingsGridWrapper.Name = "pnlSavingsGridWrapper";
            this.pnlSavingsGridWrapper.Padding = new System.Windows.Forms.Padding(1);
            this.pnlSavingsGridWrapper.Size = new System.Drawing.Size(696, 442);
            this.pnlSavingsGridWrapper.TabIndex = 2;
            // 
            // dgvAdminSavings
            // 
            this.dgvAdminSavings.AllowUserToAddRows = false;
            this.dgvAdminSavings.AllowUserToDeleteRows = false;
            this.dgvAdminSavings.AllowUserToResizeRows = false;
            this.dgvAdminSavings.BackgroundColor = System.Drawing.Color.White;
            this.dgvAdminSavings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAdminSavings.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvAdminSavings.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvAdminSavings.ColumnHeadersHeight = 36;
            this.dgvAdminSavings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAdminSavings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAdminSavings.EnableHeadersVisualStyles = false;
            this.dgvAdminSavings.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.dgvAdminSavings.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvAdminSavings.Location = new System.Drawing.Point(1, 1);
            this.dgvAdminSavings.MultiSelect = false;
            this.dgvAdminSavings.Name = "dgvAdminSavings";
            this.dgvAdminSavings.ReadOnly = true;
            this.dgvAdminSavings.RowHeadersVisible = false;
            this.dgvAdminSavings.RowTemplate.Height = 36;
            this.dgvAdminSavings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAdminSavings.Size = new System.Drawing.Size(694, 440);
            this.dgvAdminSavings.TabIndex = 0;
            // 
            // pnlSavingsFilterBar
            // 
            this.pnlSavingsFilterBar.Controls.Add(this.btnRefreshSavings);
            this.pnlSavingsFilterBar.Controls.Add(this.pnlSavingsSearch);
            this.pnlSavingsFilterBar.Controls.Add(this.btnTabSettled);
            this.pnlSavingsFilterBar.Controls.Add(this.btnTabActive);
            this.pnlSavingsFilterBar.Controls.Add(this.btnTabAll);
            this.pnlSavingsFilterBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSavingsFilterBar.Location = new System.Drawing.Point(16, 46);
            this.pnlSavingsFilterBar.Name = "pnlSavingsFilterBar";
            this.pnlSavingsFilterBar.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.pnlSavingsFilterBar.Size = new System.Drawing.Size(696, 46);
            this.pnlSavingsFilterBar.TabIndex = 1;
            // 
            // btnRefreshSavings
            // 
            this.btnRefreshSavings.BorderRadius = 8;
            this.btnRefreshSavings.BorderSize = 1;
            this.btnRefreshSavings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshSavings.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnRefreshSavings.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnRefreshSavings.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnRefreshSavings.Location = new System.Drawing.Point(606, 0);
            this.btnRefreshSavings.Name = "btnRefreshSavings";
            this.btnRefreshSavings.NormalBorder = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnRefreshSavings.NormalColor = System.Drawing.Color.White;
            this.btnRefreshSavings.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnRefreshSavings.Size = new System.Drawing.Size(90, 36);
            this.btnRefreshSavings.TabIndex = 4;
            this.btnRefreshSavings.Text = "🔄 Làm mới";
            this.btnRefreshSavings.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            // 
            // pnlSavingsSearch
            // 
            this.pnlSavingsSearch.BackColor = System.Drawing.Color.White;
            this.pnlSavingsSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.pnlSavingsSearch.BorderRadius = 8;
            this.pnlSavingsSearch.BorderSize = 1;
            this.pnlSavingsSearch.Controls.Add(this.lblSavingsSearchIcon);
            this.pnlSavingsSearch.Controls.Add(this.txtSearchSavings);
            this.pnlSavingsSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSavingsSearch.FillColor = System.Drawing.Color.White;
            this.pnlSavingsSearch.Location = new System.Drawing.Point(370, 0);
            this.pnlSavingsSearch.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.pnlSavingsSearch.Name = "pnlSavingsSearch";
            this.pnlSavingsSearch.Size = new System.Drawing.Size(236, 36);
            this.pnlSavingsSearch.TabIndex = 3;
            // 
            // lblSavingsSearchIcon
            // 
            this.lblSavingsSearchIcon.Font = new System.Drawing.Font("Segoe UI Emoji", 10F);
            this.lblSavingsSearchIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSavingsSearchIcon.Location = new System.Drawing.Point(8, 8);
            this.lblSavingsSearchIcon.Name = "lblSavingsSearchIcon";
            this.lblSavingsSearchIcon.Size = new System.Drawing.Size(22, 20);
            this.lblSavingsSearchIcon.TabIndex = 0;
            this.lblSavingsSearchIcon.Text = "🔍";
            this.lblSavingsSearchIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSearchSavings
            // 
            this.txtSearchSavings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchSavings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearchSavings.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtSearchSavings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtSearchSavings.Location = new System.Drawing.Point(34, 9);
            this.txtSearchSavings.Name = "txtSearchSavings";
            this.txtSearchSavings.Size = new System.Drawing.Size(192, 17);
            this.txtSearchSavings.TabIndex = 1;
            // 
            // btnTabSettled
            // 
            this.btnTabSettled.BorderRadius = 8;
            this.btnTabSettled.BorderSize = 1;
            this.btnTabSettled.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabSettled.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnTabSettled.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnTabSettled.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnTabSettled.Location = new System.Drawing.Point(190, 0);
            this.btnTabSettled.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.btnTabSettled.Name = "btnTabSettled";
            this.btnTabSettled.NormalBorder = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnTabSettled.NormalColor = System.Drawing.Color.White;
            this.btnTabSettled.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnTabSettled.Size = new System.Drawing.Size(100, 36);
            this.btnTabSettled.TabIndex = 2;
            this.btnTabSettled.Text = "Đã tất toán";
            this.btnTabSettled.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            // 
            // btnTabActive
            // 
            this.btnTabActive.BorderRadius = 8;
            this.btnTabActive.BorderSize = 1;
            this.btnTabActive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabActive.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnTabActive.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnTabActive.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnTabActive.Location = new System.Drawing.Point(85, 0);
            this.btnTabActive.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.btnTabActive.Name = "btnTabActive";
            this.btnTabActive.NormalBorder = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnTabActive.NormalColor = System.Drawing.Color.White;
            this.btnTabActive.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnTabActive.Size = new System.Drawing.Size(105, 36);
            this.btnTabActive.TabIndex = 1;
            this.btnTabActive.Text = "Đang gửi";
            this.btnTabActive.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            // 
            // btnTabAll
            // 
            this.btnTabAll.BorderRadius = 8;
            this.btnTabAll.BorderSize = 0;
            this.btnTabAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTabAll.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnTabAll.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnTabAll.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnTabAll.Location = new System.Drawing.Point(0, 0);
            this.btnTabAll.Name = "btnTabAll";
            this.btnTabAll.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnTabAll.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnTabAll.Size = new System.Drawing.Size(85, 36);
            this.btnTabAll.TabIndex = 0;
            this.btnTabAll.Text = "Tất cả";
            this.btnTabAll.TextColor = System.Drawing.Color.White;
            // 
            // lblSavingsPanelTitle
            // 
            this.lblSavingsPanelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSavingsPanelTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSavingsPanelTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSavingsPanelTitle.Location = new System.Drawing.Point(16, 16);
            this.lblSavingsPanelTitle.Name = "lblSavingsPanelTitle";
            this.lblSavingsPanelTitle.Size = new System.Drawing.Size(696, 32);
            this.lblSavingsPanelTitle.TabIndex = 0;
            this.lblSavingsPanelTitle.Text = "Danh sách sổ tiết kiệm";
            this.lblSavingsPanelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlTopBanner
            // 
            this.pnlTopBanner.BackColor = System.Drawing.Color.Transparent;
            this.pnlTopBanner.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlTopBanner.BorderRadius = 16;
            this.pnlTopBanner.BorderSize = 1;
            this.pnlTopBanner.Controls.Add(this.tblMetricsLayout);
            this.pnlTopBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBanner.FillColor = System.Drawing.Color.White;
            this.pnlTopBanner.Location = new System.Drawing.Point(16, 16);
            this.pnlTopBanner.Margin = new System.Windows.Forms.Padding(0, 0, 0, 14);
            this.pnlTopBanner.Name = "pnlTopBanner";
            this.pnlTopBanner.Padding = new System.Windows.Forms.Padding(12);
            this.pnlTopBanner.Size = new System.Drawing.Size(1088, 90);
            this.pnlTopBanner.TabIndex = 0;
            // 
            // tblMetricsLayout
            // 
            this.tblMetricsLayout.ColumnCount = 4;
            this.tblMetricsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblMetricsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblMetricsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblMetricsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblMetricsLayout.Controls.Add(this.pnlMetric1, 0, 0);
            this.tblMetricsLayout.Controls.Add(this.pnlMetric2, 1, 0);
            this.tblMetricsLayout.Controls.Add(this.pnlMetric3, 2, 0);
            this.tblMetricsLayout.Controls.Add(this.pnlMetric4, 3, 0);
            this.tblMetricsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMetricsLayout.Location = new System.Drawing.Point(12, 12);
            this.tblMetricsLayout.Name = "tblMetricsLayout";
            this.tblMetricsLayout.RowCount = 1;
            this.tblMetricsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMetricsLayout.Size = new System.Drawing.Size(1064, 66);
            this.tblMetricsLayout.TabIndex = 0;
            // 
            // pnlMetric4
            // 
            this.pnlMetric4.BackColor = System.Drawing.Color.Transparent;
            this.pnlMetric4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(240)))), ((int)(((byte)(138)))));
            this.pnlMetric4.BorderRadius = 12;
            this.pnlMetric4.BorderSize = 1;
            this.pnlMetric4.Controls.Add(this.lblMetric4Value);
            this.pnlMetric4.Controls.Add(this.lblMetric4Title);
            this.pnlMetric4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMetric4.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(235)))));
            this.pnlMetric4.Location = new System.Drawing.Point(798, 0);
            this.pnlMetric4.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.pnlMetric4.Name = "pnlMetric4";
            this.pnlMetric4.Padding = new System.Windows.Forms.Padding(12);
            this.pnlMetric4.Size = new System.Drawing.Size(266, 66);
            this.pnlMetric4.TabIndex = 3;
            // 
            // lblMetric4Value
            // 
            this.lblMetric4Value.AutoSize = true;
            this.lblMetric4Value.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblMetric4Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.lblMetric4Value.Location = new System.Drawing.Point(10, 32);
            this.lblMetric4Value.Name = "lblMetric4Value";
            this.lblMetric4Value.Size = new System.Drawing.Size(42, 25);
            this.lblMetric4Value.TabIndex = 1;
            this.lblMetric4Value.Text = "0 đ";
            // 
            // lblMetric4Title
            // 
            this.lblMetric4Title.AutoSize = true;
            this.lblMetric4Title.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblMetric4Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(9)))));
            this.lblMetric4Title.Location = new System.Drawing.Point(10, 10);
            this.lblMetric4Title.Name = "lblMetric4Title";
            this.lblMetric4Title.Size = new System.Drawing.Size(127, 15);
            this.lblMetric4Title.TabIndex = 0;
            this.lblMetric4Title.Text = "Dự tính lãi phải trả (Vốn)";
            // 
            // pnlMetric3
            // 
            this.pnlMetric3.BackColor = System.Drawing.Color.Transparent;
            this.pnlMetric3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlMetric3.BorderRadius = 12;
            this.pnlMetric3.BorderSize = 1;
            this.pnlMetric3.Controls.Add(this.lblMetric3Value);
            this.pnlMetric3.Controls.Add(this.lblMetric3Title);
            this.pnlMetric3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMetric3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlMetric3.Location = new System.Drawing.Point(532, 0);
            this.pnlMetric3.Margin = new System.Windows.Forms.Padding(4, 0, 8, 0);
            this.pnlMetric3.Name = "pnlMetric3";
            this.pnlMetric3.Padding = new System.Windows.Forms.Padding(12);
            this.pnlMetric3.Size = new System.Drawing.Size(262, 66);
            this.pnlMetric3.TabIndex = 2;
            // 
            // lblMetric3Value
            // 
            this.lblMetric3Value.AutoSize = true;
            this.lblMetric3Value.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblMetric3Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblMetric3Value.Location = new System.Drawing.Point(10, 32);
            this.lblMetric3Value.Name = "lblMetric3Value";
            this.lblMetric3Value.Size = new System.Drawing.Size(46, 25);
            this.lblMetric3Value.TabIndex = 1;
            this.lblMetric3Value.Text = "0 sổ";
            // 
            // lblMetric3Title
            // 
            this.lblMetric3Title.AutoSize = true;
            this.lblMetric3Title.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblMetric3Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMetric3Title.Location = new System.Drawing.Point(10, 10);
            this.lblMetric3Title.Name = "lblMetric3Title";
            this.lblMetric3Title.Size = new System.Drawing.Size(95, 15);
            this.lblMetric3Title.TabIndex = 0;
            this.lblMetric3Title.Text = "Số sổ đã tất toán";
            // 
            // pnlMetric2
            // 
            this.pnlMetric2.BackColor = System.Drawing.Color.Transparent;
            this.pnlMetric2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(254)))));
            this.pnlMetric2.BorderRadius = 12;
            this.pnlMetric2.BorderSize = 1;
            this.pnlMetric2.Controls.Add(this.lblMetric2Value);
            this.pnlMetric2.Controls.Add(this.lblMetric2Title);
            this.pnlMetric2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMetric2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.pnlMetric2.Location = new System.Drawing.Point(266, 0);
            this.pnlMetric2.Margin = new System.Windows.Forms.Padding(4, 0, 8, 0);
            this.pnlMetric2.Name = "pnlMetric2";
            this.pnlMetric2.Padding = new System.Windows.Forms.Padding(12);
            this.pnlMetric2.Size = new System.Drawing.Size(262, 66);
            this.pnlMetric2.TabIndex = 1;
            // 
            // lblMetric2Value
            // 
            this.lblMetric2Value.AutoSize = true;
            this.lblMetric2Value.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblMetric2Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblMetric2Value.Location = new System.Drawing.Point(10, 32);
            this.lblMetric2Value.Name = "lblMetric2Value";
            this.lblMetric2Value.Size = new System.Drawing.Size(46, 25);
            this.lblMetric2Value.TabIndex = 1;
            this.lblMetric2Value.Text = "0 sổ";
            // 
            // lblMetric2Title
            // 
            this.lblMetric2Title.AutoSize = true;
            this.lblMetric2Title.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblMetric2Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblMetric2Title.Location = new System.Drawing.Point(10, 10);
            this.lblMetric2Title.Name = "lblMetric2Title";
            this.lblMetric2Title.Size = new System.Drawing.Size(102, 15);
            this.lblMetric2Title.TabIndex = 0;
            this.lblMetric2Title.Text = "Số sổ TKTK đang gửi";
            // 
            // pnlMetric1
            // 
            this.pnlMetric1.BackColor = System.Drawing.Color.Transparent;
            this.pnlMetric1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(243)))), ((int)(((byte)(208)))));
            this.pnlMetric1.BorderRadius = 12;
            this.pnlMetric1.BorderSize = 1;
            this.pnlMetric1.Controls.Add(this.lblMetric1Value);
            this.pnlMetric1.Controls.Add(this.lblMetric1Title);
            this.pnlMetric1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMetric1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(253)))), ((int)(((byte)(245)))));
            this.pnlMetric1.Location = new System.Drawing.Point(0, 0);
            this.pnlMetric1.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.pnlMetric1.Name = "pnlMetric1";
            this.pnlMetric1.Padding = new System.Windows.Forms.Padding(12);
            this.pnlMetric1.Size = new System.Drawing.Size(262, 66);
            this.pnlMetric1.TabIndex = 0;
            // 
            // lblMetric1Value
            // 
            this.lblMetric1Value.AutoSize = true;
            this.lblMetric1Value.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblMetric1Value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblMetric1Value.Location = new System.Drawing.Point(10, 32);
            this.lblMetric1Value.Name = "lblMetric1Value";
            this.lblMetric1Value.Size = new System.Drawing.Size(46, 25);
            this.lblMetric1Value.TabIndex = 1;
            this.lblMetric1Value.Text = "0 đ";
            // 
            // lblMetric1Title
            // 
            this.lblMetric1Title.AutoSize = true;
            this.lblMetric1Title.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblMetric1Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(95)))), ((int)(((byte)(70)))));
            this.lblMetric1Title.Location = new System.Drawing.Point(10, 10);
            this.lblMetric1Title.Name = "lblMetric1Title";
            this.lblMetric1Title.Size = new System.Drawing.Size(149, 15);
            this.lblMetric1Title.TabIndex = 0;
            this.lblMetric1Title.Text = "Tổng tiền đang huy động";
            // 
            // SaveMoneyAdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1120, 720);
            this.Controls.Add(this.pnlAdminRoot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SaveMoneyAdminForm";
            this.Text = "SaveMoneyAdminForm";
            this.pnlAdminRoot.ResumeLayout(false);
            this.tblAdminMain.ResumeLayout(false);
            this.pnlLeftCustomer.ResumeLayout(false);
            this.pnlCustWrapper.ResumeLayout(false);
            this.pnlCustomerDetailBox.ResumeLayout(false);
            this.pnlCustomerDetailBox.PerformLayout();
            this.pnlCustActionButtons.ResumeLayout(false);
            this.pnlCustAvatar.ResumeLayout(false);
            this.pnlCustGridWrapper.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomers)).EndInit();
            this.pnlCustSearch.ResumeLayout(false);
            this.pnlCustSearch.PerformLayout();
            this.pnlRightSavings.ResumeLayout(false);
            this.pnlSavingsWrapper.ResumeLayout(false);
            this.pnlAdminActionBar.ResumeLayout(false);
            this.pnlSavingsGridWrapper.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAdminSavings)).EndInit();
            this.pnlSavingsFilterBar.ResumeLayout(false);
            this.pnlSavingsSearch.ResumeLayout(false);
            this.pnlSavingsSearch.PerformLayout();
            this.pnlTopBanner.ResumeLayout(false);
            this.tblMetricsLayout.ResumeLayout(false);
            this.pnlMetric4.ResumeLayout(false);
            this.pnlMetric4.PerformLayout();
            this.pnlMetric3.ResumeLayout(false);
            this.pnlMetric3.PerformLayout();
            this.pnlMetric2.ResumeLayout(false);
            this.pnlMetric2.PerformLayout();
            this.pnlMetric1.ResumeLayout(false);
            this.pnlMetric1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlAdminRoot;
        private BankAccountManagement.Forms.RoundedPanel pnlTopBanner;
        private System.Windows.Forms.TableLayoutPanel tblMetricsLayout;
        private BankAccountManagement.Forms.RoundedPanel pnlMetric1;
        private System.Windows.Forms.Label lblMetric1Title;
        private System.Windows.Forms.Label lblMetric1Value;
        private BankAccountManagement.Forms.RoundedPanel pnlMetric2;
        private System.Windows.Forms.Label lblMetric2Value;
        private System.Windows.Forms.Label lblMetric2Title;
        private BankAccountManagement.Forms.RoundedPanel pnlMetric3;
        private System.Windows.Forms.Label lblMetric3Value;
        private System.Windows.Forms.Label lblMetric3Title;
        private BankAccountManagement.Forms.RoundedPanel pnlMetric4;
        private System.Windows.Forms.Label lblMetric4Value;
        private System.Windows.Forms.Label lblMetric4Title;
        private System.Windows.Forms.TableLayoutPanel tblAdminMain;
        private System.Windows.Forms.Panel pnlLeftCustomer;
        private System.Windows.Forms.Panel pnlRightSavings;
        private BankAccountManagement.Forms.RoundedPanel pnlCustWrapper;
        private System.Windows.Forms.Label lblCustPanelTitle;
        private BankAccountManagement.Forms.RoundedPanel pnlCustSearch;
        private System.Windows.Forms.Label lblCustSearchIcon;
        private System.Windows.Forms.TextBox txtSearchCustomer;
        private BankAccountManagement.Forms.RoundedPanel pnlCustGridWrapper;
        private System.Windows.Forms.DataGridView dgvCustomers;
        private BankAccountManagement.Forms.RoundedPanel pnlCustomerDetailBox;
        private BankAccountManagement.Forms.RoundedPanel pnlCustAvatar;
        private System.Windows.Forms.Label lblCustAvatarText;
        private System.Windows.Forms.Label lblCustHeaderName;
        private System.Windows.Forms.Label lblCustHeaderCode;
        private System.Windows.Forms.Label lblCustUsernameTitle;
        private System.Windows.Forms.Label lblCustUsername;
        private System.Windows.Forms.Label lblCustCccdTitle;
        private System.Windows.Forms.Label lblCustCccd;
        private System.Windows.Forms.Label lblCustPhoneTitle;
        private System.Windows.Forms.Label lblCustPhone;
        private System.Windows.Forms.Label lblCustBalanceTitle;
        private System.Windows.Forms.Label lblCustBalance;
        private System.Windows.Forms.Label lblCustTotalSavingsTitle;
        private System.Windows.Forms.Label lblCustTotalSavings;
        private System.Windows.Forms.Label lblCustSavingsCountTitle;
        private System.Windows.Forms.Label lblCustSavingsCount;
        private System.Windows.Forms.Panel pnlCustActionButtons;
        private BankAccountManagement.Forms.RoundedButton btnClearCustomerFilter;
        private BankAccountManagement.Forms.RoundedPanel pnlSavingsWrapper;
        private System.Windows.Forms.Label lblSavingsPanelTitle;
        private System.Windows.Forms.Panel pnlSavingsFilterBar;
        private BankAccountManagement.Forms.RoundedButton btnTabAll;
        private BankAccountManagement.Forms.RoundedButton btnTabActive;
        private BankAccountManagement.Forms.RoundedButton btnTabSettled;
        private BankAccountManagement.Forms.RoundedPanel pnlSavingsSearch;
        private System.Windows.Forms.TextBox txtSearchSavings;
        private System.Windows.Forms.Label lblSavingsSearchIcon;
        private BankAccountManagement.Forms.RoundedButton btnRefreshSavings;
        private BankAccountManagement.Forms.RoundedPanel pnlSavingsGridWrapper;
        private System.Windows.Forms.DataGridView dgvAdminSavings;
        private System.Windows.Forms.Panel pnlAdminActionBar;
        private System.Windows.Forms.Label lblSelectedSummary;
        private BankAccountManagement.Forms.RoundedButton btnAdminSettle;
        private BankAccountManagement.Forms.RoundedButton btnAdminViewReceipt;
        private BankAccountManagement.Forms.RoundedButton btnAdminExport;
    }
}
