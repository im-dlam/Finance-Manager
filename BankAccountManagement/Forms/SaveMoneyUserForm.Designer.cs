namespace BankAccountManagement.Forms
{
    partial class SaveMoneyUserForm
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
            this.pnlRoot = new System.Windows.Forms.Panel();
            this.tblMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeftOverview = new System.Windows.Forms.Panel();
            this.pnlHistoryCard = new BankAccountManagement.Forms.RoundedPanel();
            this.pnlGridWrapper = new BankAccountManagement.Forms.RoundedPanel();
            this.dgvUserSavings = new System.Windows.Forms.DataGridView();
            this.lblHistoryTitle = new System.Windows.Forms.Label();
            this.pnlSummaryBanner = new BankAccountManagement.Forms.RoundedPanel();
            this.btnNewSavings = new BankAccountManagement.Forms.RoundedButton();
            this.btnRefreshUser = new BankAccountManagement.Forms.RoundedButton();
            this.lblSumInterest = new System.Windows.Forms.Label();
            this.lblSumInterestTitle = new System.Windows.Forms.Label();
            this.lblSumCount = new System.Windows.Forms.Label();
            this.lblSumCountTitle = new System.Windows.Forms.Label();
            this.lblSumBalance = new System.Windows.Forms.Label();
            this.lblSumBalanceTitle = new System.Windows.Forms.Label();
            this.lblBannerHeader = new System.Windows.Forms.Label();
            this.pnlRightPhone = new System.Windows.Forms.Panel();
            this.pnlPhoneChassis = new BankAccountManagement.Forms.RoundedPanel();
            this.pnlPhoneScreen = new System.Windows.Forms.Panel();
            this.pnlStepContainer = new System.Windows.Forms.Panel();
            this.pnlStep1 = new System.Windows.Forms.Panel();
            this.btnStep1Next = new BankAccountManagement.Forms.RoundedButton();
            this.pnlStep1PreviewCard = new BankAccountManagement.Forms.RoundedPanel();
            this.lblStep1EstInterestValue = new System.Windows.Forms.Label();
            this.lblStep1EstInterestTitle = new System.Windows.Forms.Label();
            this.lblStep1RateValue = new System.Windows.Forms.Label();
            this.lblStep1RateTitle = new System.Windows.Forms.Label();
            this.lblStep1PreviewHeader = new System.Windows.Forms.Label();
            this.pnlTermDropdown = new BankAccountManagement.Forms.RoundedPanel();
            this.cboStep1Term = new System.Windows.Forms.ComboBox();
            this.lblStep1TermTitle = new System.Windows.Forms.Label();
            this.pnlAmountInput = new BankAccountManagement.Forms.RoundedPanel();
            this.txtStep1Amount = new System.Windows.Forms.TextBox();
            this.lblStep1AmountUnit = new System.Windows.Forms.Label();
            this.lblStep1AmountTitle = new System.Windows.Forms.Label();
            this.pnlStep1HeaderCard = new BankAccountManagement.Forms.RoundedPanel();
            this.lblStep1AvailBalance = new System.Windows.Forms.Label();
            this.lblStep1AvailTitle = new System.Windows.Forms.Label();
            this.pnlStep1Banner = new BankAccountManagement.Forms.RoundedPanel();
            this.lblStep1BannerText = new System.Windows.Forms.Label();
            this.pnlStep1AccountChoice = new BankAccountManagement.Forms.RoundedPanel();
            this.cboStep1Account = new System.Windows.Forms.ComboBox();
            this.lblStep1AccountTitle = new System.Windows.Forms.Label();
            this.pnlStep2 = new System.Windows.Forms.Panel();
            this.btnStep2Confirm = new BankAccountManagement.Forms.RoundedButton();
            this.chkStep2Agree = new System.Windows.Forms.CheckBox();
            this.lblStep2AgreeText = new System.Windows.Forms.Label();
            this.pnlStep2ReviewCard = new BankAccountManagement.Forms.RoundedPanel();
            this.lblStep2DueValue = new System.Windows.Forms.Label();
            this.lblStep2DueTitle = new System.Windows.Forms.Label();
            this.lblStep2AvailValue = new System.Windows.Forms.Label();
            this.lblStep2AvailTitle = new System.Windows.Forms.Label();
            this.pnlStep2RenewalMethod = new BankAccountManagement.Forms.RoundedPanel();
            this.cboStep2Renewal = new System.Windows.Forms.ComboBox();
            this.lblStep2RenewalTitle = new System.Windows.Forms.Label();
            this.lblStep2RateValue = new System.Windows.Forms.Label();
            this.lblStep2RateTitle = new System.Windows.Forms.Label();
            this.pnlStep2TermBox = new BankAccountManagement.Forms.RoundedPanel();
            this.cboStep2Term = new System.Windows.Forms.ComboBox();
            this.lblStep2TermTitle = new System.Windows.Forms.Label();
            this.lblStep2PrincipalValue = new System.Windows.Forms.Label();
            this.lblStep2PrincipalTitle = new System.Windows.Forms.Label();
            this.lblStep2TotalSubtitle = new System.Windows.Forms.Label();
            this.lblStep2TotalAmount = new System.Windows.Forms.Label();
            this.lblStep2TotalTitle = new System.Windows.Forms.Label();
            this.pnlStep2Banner = new BankAccountManagement.Forms.RoundedPanel();
            this.lblStep2BannerText = new System.Windows.Forms.Label();
            this.pnlStep2Progress = new System.Windows.Forms.Panel();
            this.pnlStep3 = new System.Windows.Forms.Panel();
            this.pnlStep3Buttons = new System.Windows.Forms.Panel();
            this.btnStep3Close = new BankAccountManagement.Forms.RoundedButton();
            this.btnStep3Settle = new BankAccountManagement.Forms.RoundedButton();
            this.btnStep3DownloadReceipt = new BankAccountManagement.Forms.RoundedButton();
            this.pnlStep3Notif = new BankAccountManagement.Forms.RoundedPanel();
            this.lblStep3Notif = new System.Windows.Forms.Label();
            this.pnlStep3ReceiptCard = new BankAccountManagement.Forms.RoundedPanel();
            this.lblStep3AccountValue = new System.Windows.Forms.Label();
            this.lblStep3AccountTitle = new System.Windows.Forms.Label();
            this.lblStep3DueDateValue = new System.Windows.Forms.Label();
            this.lblStep3DueDateTitle = new System.Windows.Forms.Label();
            this.lblStep3OpenDateValue = new System.Windows.Forms.Label();
            this.lblStep3OpenDateTitle = new System.Windows.Forms.Label();
            this.lblStep3RenewalValue = new System.Windows.Forms.Label();
            this.lblStep3RenewalTitle = new System.Windows.Forms.Label();
            this.lblStep3RateValue = new System.Windows.Forms.Label();
            this.lblStep3RateTitle = new System.Windows.Forms.Label();
            this.lblStep3TermValue = new System.Windows.Forms.Label();
            this.lblStep3TermTitle = new System.Windows.Forms.Label();
            this.lblStep3PrincipalValue = new System.Windows.Forms.Label();
            this.lblStep3PrincipalTitle = new System.Windows.Forms.Label();
            this.lblStep3CodeValue = new System.Windows.Forms.Label();
            this.lblStep3CodeTitle = new System.Windows.Forms.Label();
            this.lblStep3SuccessSub = new System.Windows.Forms.Label();
            this.lblStep3SuccessTitle = new System.Windows.Forms.Label();
            this.pnlStep3CheckIcon = new BankAccountManagement.Forms.RoundedPanel();
            this.lblStep3CheckMark = new System.Windows.Forms.Label();
            this.pnlStep3Banner = new BankAccountManagement.Forms.RoundedPanel();
            this.lblStep3BannerText = new System.Windows.Forms.Label();
            this.pnlPhoneTopNav = new System.Windows.Forms.Panel();
            this.btnPhoneRightIcon = new System.Windows.Forms.Button();
            this.pnlTopNavTitleWrapper = new System.Windows.Forms.Panel();
            this.lblPhoneNavSubtitle = new System.Windows.Forms.Label();
            this.lblPhoneNavTitle = new System.Windows.Forms.Label();
            this.btnPhoneBack = new System.Windows.Forms.Button();
            this.lblBannerDiamond = new System.Windows.Forms.Label();
            this.pnlRoot.SuspendLayout();
            this.tblMainLayout.SuspendLayout();
            this.pnlLeftOverview.SuspendLayout();
            this.pnlHistoryCard.SuspendLayout();
            this.pnlGridWrapper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserSavings)).BeginInit();
            this.pnlSummaryBanner.SuspendLayout();
            this.pnlRightPhone.SuspendLayout();
            this.pnlPhoneChassis.SuspendLayout();
            this.pnlPhoneScreen.SuspendLayout();
            this.pnlStepContainer.SuspendLayout();
            this.pnlStep1.SuspendLayout();
            this.pnlStep1PreviewCard.SuspendLayout();
            this.pnlTermDropdown.SuspendLayout();
            this.pnlAmountInput.SuspendLayout();
            this.pnlStep1HeaderCard.SuspendLayout();
            this.pnlStep1Banner.SuspendLayout();
            this.pnlStep1AccountChoice.SuspendLayout();
            this.pnlStep2.SuspendLayout();
            this.pnlStep2ReviewCard.SuspendLayout();
            this.pnlStep2RenewalMethod.SuspendLayout();
            this.pnlStep2TermBox.SuspendLayout();
            this.pnlStep2Banner.SuspendLayout();
            this.pnlStep3.SuspendLayout();
            this.pnlStep3Buttons.SuspendLayout();
            this.pnlStep3Notif.SuspendLayout();
            this.pnlStep3ReceiptCard.SuspendLayout();
            this.pnlStep3CheckIcon.SuspendLayout();
            this.pnlStep3Banner.SuspendLayout();
            this.pnlPhoneTopNav.SuspendLayout();
            this.pnlTopNavTitleWrapper.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRoot
            // 
            this.pnlRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlRoot.Controls.Add(this.tblMainLayout);
            this.pnlRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRoot.Location = new System.Drawing.Point(0, 0);
            this.pnlRoot.Name = "pnlRoot";
            this.pnlRoot.Padding = new System.Windows.Forms.Padding(16);
            this.pnlRoot.Size = new System.Drawing.Size(1120, 720);
            this.pnlRoot.TabIndex = 0;
            // 
            // tblMainLayout
            // 
            this.tblMainLayout.ColumnCount = 2;
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMainLayout.Controls.Add(this.pnlLeftOverview, 0, 0);
            this.tblMainLayout.Controls.Add(this.pnlRightPhone, 1, 0);
            this.tblMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMainLayout.Location = new System.Drawing.Point(16, 16);
            this.tblMainLayout.Name = "tblMainLayout";
            this.tblMainLayout.RowCount = 1;
            this.tblMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMainLayout.Size = new System.Drawing.Size(1088, 688);
            this.tblMainLayout.TabIndex = 0;
            // 
            // pnlLeftOverview
            // 
            this.pnlLeftOverview.Controls.Add(this.pnlHistoryCard);
            this.pnlLeftOverview.Controls.Add(this.pnlSummaryBanner);
            this.pnlLeftOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeftOverview.Location = new System.Drawing.Point(0, 0);
            this.pnlLeftOverview.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.pnlLeftOverview.Name = "pnlLeftOverview";
            this.pnlLeftOverview.Size = new System.Drawing.Size(532, 688);
            this.pnlLeftOverview.TabIndex = 0;
            // 
            // pnlHistoryCard
            // 
            this.pnlHistoryCard.BackColor = System.Drawing.Color.Transparent;
            this.pnlHistoryCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlHistoryCard.BorderRadius = 16;
            this.pnlHistoryCard.BorderSize = 1;
            this.pnlHistoryCard.Controls.Add(this.pnlGridWrapper);
            this.pnlHistoryCard.Controls.Add(this.lblHistoryTitle);
            this.pnlHistoryCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHistoryCard.FillColor = System.Drawing.Color.White;
            this.pnlHistoryCard.Location = new System.Drawing.Point(0, 164);
            this.pnlHistoryCard.Margin = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.pnlHistoryCard.Name = "pnlHistoryCard";
            this.pnlHistoryCard.Padding = new System.Windows.Forms.Padding(16);
            this.pnlHistoryCard.Size = new System.Drawing.Size(532, 524);
            this.pnlHistoryCard.TabIndex = 1;
            // 
            // pnlGridWrapper
            // 
            this.pnlGridWrapper.BackColor = System.Drawing.Color.White;
            this.pnlGridWrapper.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlGridWrapper.BorderRadius = 12;
            this.pnlGridWrapper.BorderSize = 1;
            this.pnlGridWrapper.Controls.Add(this.dgvUserSavings);
            this.pnlGridWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridWrapper.FillColor = System.Drawing.Color.White;
            this.pnlGridWrapper.Location = new System.Drawing.Point(16, 46);
            this.pnlGridWrapper.Name = "pnlGridWrapper";
            this.pnlGridWrapper.Padding = new System.Windows.Forms.Padding(1);
            this.pnlGridWrapper.Size = new System.Drawing.Size(500, 462);
            this.pnlGridWrapper.TabIndex = 1;
            // 
            // dgvUserSavings
            // 
            this.dgvUserSavings.AllowUserToAddRows = false;
            this.dgvUserSavings.AllowUserToDeleteRows = false;
            this.dgvUserSavings.AllowUserToResizeRows = false;
            this.dgvUserSavings.BackgroundColor = System.Drawing.Color.White;
            this.dgvUserSavings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUserSavings.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvUserSavings.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvUserSavings.ColumnHeadersHeight = 34;
            this.dgvUserSavings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvUserSavings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUserSavings.EnableHeadersVisualStyles = false;
            this.dgvUserSavings.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.dgvUserSavings.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvUserSavings.Location = new System.Drawing.Point(1, 1);
            this.dgvUserSavings.MultiSelect = false;
            this.dgvUserSavings.Name = "dgvUserSavings";
            this.dgvUserSavings.ReadOnly = true;
            this.dgvUserSavings.RowHeadersVisible = false;
            this.dgvUserSavings.RowTemplate.Height = 36;
            this.dgvUserSavings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUserSavings.Size = new System.Drawing.Size(498, 460);
            this.dgvUserSavings.TabIndex = 0;
            // 
            // lblHistoryTitle
            // 
            this.lblHistoryTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHistoryTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblHistoryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblHistoryTitle.Location = new System.Drawing.Point(16, 16);
            this.lblHistoryTitle.Name = "lblHistoryTitle";
            this.lblHistoryTitle.Size = new System.Drawing.Size(500, 30);
            this.lblHistoryTitle.TabIndex = 0;
            this.lblHistoryTitle.Text = "📋 Danh sách sổ tiết kiệm của bạn (Bấm để xem chi tiết & tất toán)";
            // 
            // pnlSummaryBanner
            // 
            this.pnlSummaryBanner.BackColor = System.Drawing.Color.Transparent;
            this.pnlSummaryBanner.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlSummaryBanner.BorderRadius = 16;
            this.pnlSummaryBanner.BorderSize = 1;
            this.pnlSummaryBanner.Controls.Add(this.btnNewSavings);
            this.pnlSummaryBanner.Controls.Add(this.btnRefreshUser);
            this.pnlSummaryBanner.Controls.Add(this.lblSumInterest);
            this.pnlSummaryBanner.Controls.Add(this.lblSumInterestTitle);
            this.pnlSummaryBanner.Controls.Add(this.lblSumCount);
            this.pnlSummaryBanner.Controls.Add(this.lblSumCountTitle);
            this.pnlSummaryBanner.Controls.Add(this.lblSumBalance);
            this.pnlSummaryBanner.Controls.Add(this.lblSumBalanceTitle);
            this.pnlSummaryBanner.Controls.Add(this.lblBannerDiamond);
            this.pnlSummaryBanner.Controls.Add(this.lblBannerHeader);
            this.pnlSummaryBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSummaryBanner.FillColor = System.Drawing.Color.White;
            this.pnlSummaryBanner.Location = new System.Drawing.Point(0, 0);
            this.pnlSummaryBanner.Name = "pnlSummaryBanner";
            this.pnlSummaryBanner.Size = new System.Drawing.Size(532, 164);
            this.pnlSummaryBanner.TabIndex = 0;
            // 
            // btnNewSavings
            // 
            this.btnNewSavings.BorderRadius = 10;
            this.btnNewSavings.BorderSize = 0;
            this.btnNewSavings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNewSavings.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNewSavings.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnNewSavings.Location = new System.Drawing.Point(448, 18);
            this.btnNewSavings.Name = "btnNewSavings";
            this.btnNewSavings.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnNewSavings.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.btnNewSavings.Size = new System.Drawing.Size(176, 36);
            this.btnNewSavings.TabIndex = 8;
            this.btnNewSavings.Text = "➕ Mở tài khoản mới";
            this.btnNewSavings.TextColor = System.Drawing.Color.White;
            // 
            // btnRefreshUser
            // 
            this.btnRefreshUser.BorderRadius = 8;
            this.btnRefreshUser.BorderSize = 1;
            this.btnRefreshUser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshUser.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnRefreshUser.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnRefreshUser.Location = new System.Drawing.Point(344, 18);
            this.btnRefreshUser.Name = "btnRefreshUser";
            this.btnRefreshUser.NormalBorder = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnRefreshUser.NormalColor = System.Drawing.Color.White;
            this.btnRefreshUser.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnRefreshUser.Size = new System.Drawing.Size(96, 36);
            this.btnRefreshUser.TabIndex = 7;
            this.btnRefreshUser.Text = "🔄 Làm mới";
            this.btnRefreshUser.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            // 
            // lblSumInterest
            // 
            this.lblSumInterest.AutoSize = true;
            this.lblSumInterest.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblSumInterest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.lblSumInterest.Location = new System.Drawing.Point(443, 108);
            this.lblSumInterest.Name = "lblSumInterest";
            this.lblSumInterest.Size = new System.Drawing.Size(46, 28);
            this.lblSumInterest.TabIndex = 6;
            this.lblSumInterest.Text = "0 đ";
            // 
            // lblSumInterestTitle
            // 
            this.lblSumInterestTitle.AutoSize = true;
            this.lblSumInterestTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSumInterestTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSumInterestTitle.Location = new System.Drawing.Point(445, 85);
            this.lblSumInterestTitle.Name = "lblSumInterestTitle";
            this.lblSumInterestTitle.Size = new System.Drawing.Size(127, 15);
            this.lblSumInterestTitle.TabIndex = 5;
            this.lblSumInterestTitle.Text = "Tiền lãi dự tính tích luỹ";
            // 
            // lblSumCount
            // 
            this.lblSumCount.AutoSize = true;
            this.lblSumCount.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblSumCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSumCount.Location = new System.Drawing.Point(260, 108);
            this.lblSumCount.Name = "lblSumCount";
            this.lblSumCount.Size = new System.Drawing.Size(53, 28);
            this.lblSumCount.TabIndex = 4;
            this.lblSumCount.Text = "0 sổ";
            // 
            // lblSumCountTitle
            // 
            this.lblSumCountTitle.AutoSize = true;
            this.lblSumCountTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSumCountTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSumCountTitle.Location = new System.Drawing.Point(262, 85);
            this.lblSumCountTitle.Name = "lblSumCountTitle";
            this.lblSumCountTitle.Size = new System.Drawing.Size(100, 15);
            this.lblSumCountTitle.TabIndex = 3;
            this.lblSumCountTitle.Text = "Sổ đang hoạt động";
            // 
            // lblSumBalance
            // 
            this.lblSumBalance.AutoSize = true;
            this.lblSumBalance.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblSumBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblSumBalance.Location = new System.Drawing.Point(20, 106);
            this.lblSumBalance.Name = "lblSumBalance";
            this.lblSumBalance.Size = new System.Drawing.Size(50, 30);
            this.lblSumBalance.TabIndex = 2;
            this.lblSumBalance.Text = "0 đ";
            // 
            // lblSumBalanceTitle
            // 
            this.lblSumBalanceTitle.AutoSize = true;
            this.lblSumBalanceTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSumBalanceTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSumBalanceTitle.Location = new System.Drawing.Point(22, 85);
            this.lblSumBalanceTitle.Name = "lblSumBalanceTitle";
            this.lblSumBalanceTitle.Size = new System.Drawing.Size(126, 15);
            this.lblSumBalanceTitle.TabIndex = 1;
            this.lblSumBalanceTitle.Text = "Tổng tiền đang gửi TKTK";
            // 
            // lblBannerDiamond
            // 
            this.lblBannerDiamond.AutoSize = true;
            this.lblBannerDiamond.Font = new System.Drawing.Font("Segoe UI Emoji", 13F);
            this.lblBannerDiamond.Location = new System.Drawing.Point(20, 20);
            this.lblBannerDiamond.Name = "lblBannerDiamond";
            this.lblBannerDiamond.Size = new System.Drawing.Size(26, 25);
            this.lblBannerDiamond.TabIndex = 9;
            this.lblBannerDiamond.Text = "💎";
            // 
            // lblBannerHeader
            // 
            this.lblBannerHeader.AutoSize = true;
            this.lblBannerHeader.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblBannerHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblBannerHeader.Location = new System.Drawing.Point(48, 20);
            this.lblBannerHeader.Name = "lblBannerHeader";
            this.lblBannerHeader.Size = new System.Drawing.Size(240, 25);
            this.lblBannerHeader.TabIndex = 0;
            this.lblBannerHeader.Text = "Tiết kiệm thông minh 24/7";
            // 
            // pnlRightPhone
            // 
            this.pnlRightPhone.Controls.Add(this.pnlPhoneChassis);
            this.pnlRightPhone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRightPhone.Location = new System.Drawing.Point(544, 0);
            this.pnlRightPhone.Margin = new System.Windows.Forms.Padding(0);
            this.pnlRightPhone.Name = "pnlRightPhone";
            this.pnlRightPhone.Size = new System.Drawing.Size(544, 688);
            this.pnlRightPhone.TabIndex = 1;
            // 
            // pnlPhoneChassis
            // 
            this.pnlPhoneChassis.BackColor = System.Drawing.Color.Transparent;
            this.pnlPhoneChassis.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlPhoneChassis.BorderRadius = 16;
            this.pnlPhoneChassis.BorderSize = 1;
            this.pnlPhoneChassis.Controls.Add(this.pnlPhoneScreen);
            this.pnlPhoneChassis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPhoneChassis.FillColor = System.Drawing.Color.White;
            this.pnlPhoneChassis.Location = new System.Drawing.Point(0, 0);
            this.pnlPhoneChassis.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPhoneChassis.Name = "pnlPhoneChassis";
            this.pnlPhoneChassis.Padding = new System.Windows.Forms.Padding(16);
            this.pnlPhoneChassis.Size = new System.Drawing.Size(544, 688);
            this.pnlPhoneChassis.TabIndex = 0;
            // 
            // pnlPhoneScreen
            // 
            this.pnlPhoneScreen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlPhoneScreen.Controls.Add(this.pnlStepContainer);
            this.pnlPhoneScreen.Controls.Add(this.pnlPhoneTopNav);
            this.pnlPhoneScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPhoneScreen.Location = new System.Drawing.Point(16, 16);
            this.pnlPhoneScreen.Name = "pnlPhoneScreen";
            this.pnlPhoneScreen.Size = new System.Drawing.Size(512, 656);
            this.pnlPhoneScreen.TabIndex = 0;
            // 
            // pnlStepContainer
            // 
            this.pnlStepContainer.Controls.Add(this.pnlStep1);
            this.pnlStepContainer.Controls.Add(this.pnlStep2);
            this.pnlStepContainer.Controls.Add(this.pnlStep3);
            this.pnlStepContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStepContainer.Location = new System.Drawing.Point(0, 56);
            this.pnlStepContainer.Name = "pnlStepContainer";
            this.pnlStepContainer.Size = new System.Drawing.Size(512, 600);
            this.pnlStepContainer.TabIndex = 2;
            // 
            // pnlStep1
            // 
            this.pnlStep1.AutoScroll = true;
            this.pnlStep1.Controls.Add(this.btnStep1Next);
            this.pnlStep1.Controls.Add(this.pnlStep1PreviewCard);
            this.pnlStep1.Controls.Add(this.pnlTermDropdown);
            this.pnlStep1.Controls.Add(this.lblStep1TermTitle);
            this.pnlStep1.Controls.Add(this.pnlAmountInput);
            this.pnlStep1.Controls.Add(this.lblStep1AmountTitle);
            this.pnlStep1.Controls.Add(this.pnlStep1HeaderCard);
            this.pnlStep1.Controls.Add(this.pnlStep1AccountChoice);
            this.pnlStep1.Controls.Add(this.lblStep1AccountTitle);
            this.pnlStep1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStep1.Location = new System.Drawing.Point(0, 0);
            this.pnlStep1.Name = "pnlStep1";
            this.pnlStep1.Padding = new System.Windows.Forms.Padding(16, 8, 16, 16);
            this.pnlStep1.Size = new System.Drawing.Size(370, 588);
            this.pnlStep1.TabIndex = 0;
            // 
            // btnStep1Next
            // 
            this.btnStep1Next.BorderRadius = 12;
            this.btnStep1Next.BorderSize = 0;
            this.btnStep1Next.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStep1Next.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnStep1Next.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.btnStep1Next.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnStep1Next.Location = new System.Drawing.Point(16, 522);
            this.btnStep1Next.Name = "btnStep1Next";
            this.btnStep1Next.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnStep1Next.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(6)))), ((int)(((byte)(23)))));
            this.btnStep1Next.Size = new System.Drawing.Size(338, 50);
            this.btnStep1Next.TabIndex = 6;
            this.btnStep1Next.Text = "Tiếp tục";
            this.btnStep1Next.TextColor = System.Drawing.Color.White;
            // 
            // pnlStep1PreviewCard
            // 
            this.pnlStep1PreviewCard.BackColor = System.Drawing.Color.Transparent;
            this.pnlStep1PreviewCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlStep1PreviewCard.BorderRadius = 12;
            this.pnlStep1PreviewCard.BorderSize = 1;
            this.pnlStep1PreviewCard.Controls.Add(this.lblStep1EstInterestValue);
            this.pnlStep1PreviewCard.Controls.Add(this.lblStep1EstInterestTitle);
            this.pnlStep1PreviewCard.Controls.Add(this.lblStep1RateValue);
            this.pnlStep1PreviewCard.Controls.Add(this.lblStep1RateTitle);
            this.pnlStep1PreviewCard.Controls.Add(this.lblStep1PreviewHeader);
            this.pnlStep1PreviewCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStep1PreviewCard.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlStep1PreviewCard.Location = new System.Drawing.Point(16, 386);
            this.pnlStep1PreviewCard.Margin = new System.Windows.Forms.Padding(0, 14, 0, 0);
            this.pnlStep1PreviewCard.Name = "pnlStep1PreviewCard";
            this.pnlStep1PreviewCard.Padding = new System.Windows.Forms.Padding(12);
            this.pnlStep1PreviewCard.Size = new System.Drawing.Size(338, 102);
            this.pnlStep1PreviewCard.TabIndex = 5;
            // 
            // lblStep1EstInterestValue
            // 
            this.lblStep1EstInterestValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStep1EstInterestValue.Font = new System.Drawing.Font("Segoe UI", 10.5F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblStep1EstInterestValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep1EstInterestValue.Location = new System.Drawing.Point(170, 68);
            this.lblStep1EstInterestValue.Name = "lblStep1EstInterestValue";
            this.lblStep1EstInterestValue.Size = new System.Drawing.Size(156, 20);
            this.lblStep1EstInterestValue.TabIndex = 4;
            this.lblStep1EstInterestValue.Text = "337.500 đ";
            this.lblStep1EstInterestValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStep1EstInterestTitle
            // 
            this.lblStep1EstInterestTitle.AutoSize = true;
            this.lblStep1EstInterestTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStep1EstInterestTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStep1EstInterestTitle.Location = new System.Drawing.Point(12, 70);
            this.lblStep1EstInterestTitle.Name = "lblStep1EstInterestTitle";
            this.lblStep1EstInterestTitle.Size = new System.Drawing.Size(94, 15);
            this.lblStep1EstInterestTitle.TabIndex = 3;
            this.lblStep1EstInterestTitle.Text = "Tiền lãi dự tính";
            // 
            // lblStep1RateValue
            // 
            this.lblStep1RateValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStep1RateValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStep1RateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep1RateValue.Location = new System.Drawing.Point(170, 38);
            this.lblStep1RateValue.Name = "lblStep1RateValue";
            this.lblStep1RateValue.Size = new System.Drawing.Size(156, 18);
            this.lblStep1RateValue.TabIndex = 2;
            this.lblStep1RateValue.Text = "6,75% / năm";
            this.lblStep1RateValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStep1RateTitle
            // 
            this.lblStep1RateTitle.AutoSize = true;
            this.lblStep1RateTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStep1RateTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStep1RateTitle.Location = new System.Drawing.Point(12, 40);
            this.lblStep1RateTitle.Name = "lblStep1RateTitle";
            this.lblStep1RateTitle.Size = new System.Drawing.Size(49, 15);
            this.lblStep1RateTitle.TabIndex = 1;
            this.lblStep1RateTitle.Text = "Lãi suất";
            // 
            // lblStep1PreviewHeader
            // 
            this.lblStep1PreviewHeader.AutoSize = true;
            this.lblStep1PreviewHeader.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStep1PreviewHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep1PreviewHeader.Location = new System.Drawing.Point(12, 10);
            this.lblStep1PreviewHeader.Name = "lblStep1PreviewHeader";
            this.lblStep1PreviewHeader.Size = new System.Drawing.Size(126, 17);
            this.lblStep1PreviewHeader.TabIndex = 0;
            this.lblStep1PreviewHeader.Text = "Xem lãi suất dự tính";
            // 
            // pnlTermDropdown
            // 
            this.pnlTermDropdown.BackColor = System.Drawing.Color.White;
            this.pnlTermDropdown.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.pnlTermDropdown.BorderRadius = 10;
            this.pnlTermDropdown.BorderSize = 1;
            this.pnlTermDropdown.Controls.Add(this.cboStep1Term);
            this.pnlTermDropdown.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTermDropdown.FillColor = System.Drawing.Color.White;
            this.pnlTermDropdown.Location = new System.Drawing.Point(16, 342);
            this.pnlTermDropdown.Margin = new System.Windows.Forms.Padding(0, 4, 0, 10);
            this.pnlTermDropdown.Name = "pnlTermDropdown";
            this.pnlTermDropdown.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.pnlTermDropdown.Size = new System.Drawing.Size(338, 44);
            this.pnlTermDropdown.TabIndex = 4;
            // 
            // cboStep1Term
            // 
            this.cboStep1Term.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboStep1Term.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStep1Term.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboStep1Term.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.cboStep1Term.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cboStep1Term.FormattingEnabled = true;
            this.cboStep1Term.Location = new System.Drawing.Point(10, 8);
            this.cboStep1Term.Name = "cboStep1Term";
            this.cboStep1Term.Size = new System.Drawing.Size(318, 25);
            this.cboStep1Term.TabIndex = 0;
            // 
            // lblStep1TermTitle
            // 
            this.lblStep1TermTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStep1TermTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStep1TermTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep1TermTitle.Location = new System.Drawing.Point(16, 318);
            this.lblStep1TermTitle.Name = "lblStep1TermTitle";
            this.lblStep1TermTitle.Size = new System.Drawing.Size(338, 24);
            this.lblStep1TermTitle.TabIndex = 3;
            this.lblStep1TermTitle.Text = "Chọn kỳ hạn";
            this.lblStep1TermTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // pnlAmountInput
            // 
            this.pnlAmountInput.BackColor = System.Drawing.Color.White;
            this.pnlAmountInput.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.pnlAmountInput.BorderRadius = 10;
            this.pnlAmountInput.BorderSize = 1;
            this.pnlAmountInput.Controls.Add(this.txtStep1Amount);
            this.pnlAmountInput.Controls.Add(this.lblStep1AmountUnit);
            this.pnlAmountInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAmountInput.FillColor = System.Drawing.Color.White;
            this.pnlAmountInput.Location = new System.Drawing.Point(16, 274);
            this.pnlAmountInput.Margin = new System.Windows.Forms.Padding(0, 4, 0, 10);
            this.pnlAmountInput.Name = "pnlAmountInput";
            this.pnlAmountInput.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlAmountInput.Size = new System.Drawing.Size(338, 44);
            this.pnlAmountInput.TabIndex = 2;
            // 
            // txtStep1Amount
            // 
            this.txtStep1Amount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStep1Amount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStep1Amount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtStep1Amount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtStep1Amount.Location = new System.Drawing.Point(12, 8);
            this.txtStep1Amount.Name = "txtStep1Amount";
            this.txtStep1Amount.Size = new System.Drawing.Size(288, 22);
            this.txtStep1Amount.TabIndex = 0;
            this.txtStep1Amount.Text = "10.000.000";
            // 
            // lblStep1AmountUnit
            // 
            this.lblStep1AmountUnit.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStep1AmountUnit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular);
            this.lblStep1AmountUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblStep1AmountUnit.Location = new System.Drawing.Point(300, 8);
            this.lblStep1AmountUnit.Name = "lblStep1AmountUnit";
            this.lblStep1AmountUnit.Size = new System.Drawing.Size(26, 28);
            this.lblStep1AmountUnit.TabIndex = 1;
            this.lblStep1AmountUnit.Text = "đ";
            this.lblStep1AmountUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStep1AmountTitle
            // 
            this.lblStep1AmountTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStep1AmountTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStep1AmountTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep1AmountTitle.Location = new System.Drawing.Point(16, 250);
            this.lblStep1AmountTitle.Name = "lblStep1AmountTitle";
            this.lblStep1AmountTitle.Size = new System.Drawing.Size(338, 24);
            this.lblStep1AmountTitle.TabIndex = 1;
            this.lblStep1AmountTitle.Text = "Số tiền gửi";
            this.lblStep1AmountTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // pnlStep1HeaderCard
            // 
            this.pnlStep1HeaderCard.BackColor = System.Drawing.Color.Transparent;
            this.pnlStep1HeaderCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.pnlStep1HeaderCard.BorderRadius = 14;
            this.pnlStep1HeaderCard.BorderSize = 1;
            this.pnlStep1HeaderCard.Controls.Add(this.lblStep1AvailBalance);
            this.pnlStep1HeaderCard.Controls.Add(this.lblStep1AvailTitle);
            this.pnlStep1HeaderCard.Controls.Add(this.pnlStep1Banner);
            this.pnlStep1HeaderCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStep1HeaderCard.FillColor = System.Drawing.Color.White;
            this.pnlStep1HeaderCard.Location = new System.Drawing.Point(16, 134);
            this.pnlStep1HeaderCard.Name = "pnlStep1HeaderCard";
            this.pnlStep1HeaderCard.Size = new System.Drawing.Size(338, 116);
            this.pnlStep1HeaderCard.TabIndex = 0;
            // 
            // lblStep1AvailBalance
            // 
            this.lblStep1AvailBalance.AutoSize = true;
            this.lblStep1AvailBalance.Font = new System.Drawing.Font("Segoe UI", 17F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblStep1AvailBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.lblStep1AvailBalance.Location = new System.Drawing.Point(16, 72);
            this.lblStep1AvailBalance.Name = "lblStep1AvailBalance";
            this.lblStep1AvailBalance.Size = new System.Drawing.Size(161, 31);
            this.lblStep1AvailBalance.TabIndex = 2;
            this.lblStep1AvailBalance.Text = "10.182.377 đ";
            // 
            // lblStep1AvailTitle
            // 
            this.lblStep1AvailTitle.AutoSize = true;
            this.lblStep1AvailTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStep1AvailTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStep1AvailTitle.Location = new System.Drawing.Point(18, 52);
            this.lblStep1AvailTitle.Name = "lblStep1AvailTitle";
            this.lblStep1AvailTitle.Size = new System.Drawing.Size(89, 15);
            this.lblStep1AvailTitle.TabIndex = 1;
            this.lblStep1AvailTitle.Text = "Số dư khả dụng";
            // 
            // pnlStep1Banner
            // 
            this.pnlStep1Banner.BackColor = System.Drawing.Color.Transparent;
            this.pnlStep1Banner.BorderColor = System.Drawing.Color.Transparent;
            this.pnlStep1Banner.BorderRadius = 14;
            this.pnlStep1Banner.BorderSize = 0;
            this.pnlStep1Banner.Controls.Add(this.lblStep1BannerText);
            this.pnlStep1Banner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStep1Banner.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            this.pnlStep1Banner.Location = new System.Drawing.Point(0, 0);
            this.pnlStep1Banner.Name = "pnlStep1Banner";
            this.pnlStep1Banner.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.pnlStep1Banner.Size = new System.Drawing.Size(338, 42);
            this.pnlStep1Banner.TabIndex = 0;
            // 
            // lblStep1BannerText
            // 
            this.lblStep1BannerText.AutoSize = true;
            this.lblStep1BannerText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblStep1BannerText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblStep1BannerText.Location = new System.Drawing.Point(14, 11);
            this.lblStep1BannerText.Name = "lblStep1BannerText";
            this.lblStep1BannerText.Size = new System.Drawing.Size(127, 20);
            this.lblStep1BannerText.TabIndex = 0;
            this.lblStep1BannerText.Text = "1. Nhập tiền gửi";
            // 
            // pnlStep1AccountChoice
            // 
            this.pnlStep1AccountChoice.BackColor = System.Drawing.Color.White;
            this.pnlStep1AccountChoice.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.pnlStep1AccountChoice.BorderRadius = 10;
            this.pnlStep1AccountChoice.BorderSize = 1;
            this.pnlStep1AccountChoice.Controls.Add(this.cboStep1Account);
            this.pnlStep1AccountChoice.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStep1AccountChoice.FillColor = System.Drawing.Color.White;
            this.pnlStep1AccountChoice.Location = new System.Drawing.Point(16, 32);
            this.pnlStep1AccountChoice.Margin = new System.Windows.Forms.Padding(0, 4, 0, 10);
            this.pnlStep1AccountChoice.Name = "pnlStep1AccountChoice";
            this.pnlStep1AccountChoice.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.pnlStep1AccountChoice.Size = new System.Drawing.Size(338, 44);
            this.pnlStep1AccountChoice.TabIndex = 8;
            // 
            // cboStep1Account
            // 
            this.cboStep1Account.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboStep1Account.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStep1Account.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboStep1Account.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.cboStep1Account.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cboStep1Account.FormattingEnabled = true;
            this.cboStep1Account.Location = new System.Drawing.Point(10, 8);
            this.cboStep1Account.Name = "cboStep1Account";
            this.cboStep1Account.Size = new System.Drawing.Size(318, 23);
            this.cboStep1Account.TabIndex = 0;
            // 
            // lblStep1AccountTitle
            // 
            this.lblStep1AccountTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStep1AccountTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStep1AccountTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep1AccountTitle.Location = new System.Drawing.Point(16, 8);
            this.lblStep1AccountTitle.Name = "lblStep1AccountTitle";
            this.lblStep1AccountTitle.Size = new System.Drawing.Size(338, 24);
            this.lblStep1AccountTitle.TabIndex = 7;
            this.lblStep1AccountTitle.Text = "Tài khoản thanh toán trích tiền";
            this.lblStep1AccountTitle.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // pnlStep2
            // 
            this.pnlStep2.AutoScroll = true;
            this.pnlStep2.Controls.Add(this.btnStep2Confirm);
            this.pnlStep2.Controls.Add(this.chkStep2Agree);
            this.pnlStep2.Controls.Add(this.lblStep2AgreeText);
            this.pnlStep2.Controls.Add(this.pnlStep2ReviewCard);
            this.pnlStep2.Controls.Add(this.pnlStep2Progress);
            this.pnlStep2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStep2.Location = new System.Drawing.Point(0, 0);
            this.pnlStep2.Name = "pnlStep2";
            this.pnlStep2.Padding = new System.Windows.Forms.Padding(16, 6, 16, 16);
            this.pnlStep2.Size = new System.Drawing.Size(370, 588);
            this.pnlStep2.TabIndex = 1;
            this.pnlStep2.Visible = false;
            // 
            // btnStep2Confirm
            // 
            this.btnStep2Confirm.BorderRadius = 12;
            this.btnStep2Confirm.BorderSize = 0;
            this.btnStep2Confirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStep2Confirm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnStep2Confirm.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.btnStep2Confirm.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(18)))), ((int)(((byte)(60)))));
            this.btnStep2Confirm.Location = new System.Drawing.Point(16, 522);
            this.btnStep2Confirm.Name = "btnStep2Confirm";
            this.btnStep2Confirm.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(29)))), ((int)(((byte)(72)))));
            this.btnStep2Confirm.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(18)))), ((int)(((byte)(57)))));
            this.btnStep2Confirm.Size = new System.Drawing.Size(338, 50);
            this.btnStep2Confirm.TabIndex = 4;
            this.btnStep2Confirm.Text = "Xác nhận & Mở ✨";
            this.btnStep2Confirm.TextColor = System.Drawing.Color.White;
            // 
            // chkStep2Agree
            // 
            this.chkStep2Agree.Checked = true;
            this.chkStep2Agree.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStep2Agree.Location = new System.Drawing.Point(18, 466);
            this.chkStep2Agree.Name = "chkStep2Agree";
            this.chkStep2Agree.Size = new System.Drawing.Size(20, 20);
            this.chkStep2Agree.TabIndex = 2;
            this.chkStep2Agree.UseVisualStyleBackColor = true;
            // 
            // lblStep2AgreeText
            // 
            this.lblStep2AgreeText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStep2AgreeText.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStep2AgreeText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblStep2AgreeText.Location = new System.Drawing.Point(42, 464);
            this.lblStep2AgreeText.Name = "lblStep2AgreeText";
            this.lblStep2AgreeText.Size = new System.Drawing.Size(312, 40);
            this.lblStep2AgreeText.TabIndex = 3;
            this.lblStep2AgreeText.Text = "Tôi đã đọc và đồng ý với Điều khoản & Điều kiện gửi tiết kiệm trực tuyến";
            // 
            // pnlStep2ReviewCard
            // 
            this.pnlStep2ReviewCard.BackColor = System.Drawing.Color.Transparent;
            this.pnlStep2ReviewCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlStep2ReviewCard.BorderRadius = 14;
            this.pnlStep2ReviewCard.BorderSize = 1;
            this.pnlStep2ReviewCard.Controls.Add(this.lblStep2DueValue);
            this.pnlStep2ReviewCard.Controls.Add(this.lblStep2DueTitle);
            this.pnlStep2ReviewCard.Controls.Add(this.lblStep2AvailValue);
            this.pnlStep2ReviewCard.Controls.Add(this.lblStep2AvailTitle);
            this.pnlStep2ReviewCard.Controls.Add(this.pnlStep2RenewalMethod);
            this.pnlStep2ReviewCard.Controls.Add(this.lblStep2RenewalTitle);
            this.pnlStep2ReviewCard.Controls.Add(this.lblStep2RateValue);
            this.pnlStep2ReviewCard.Controls.Add(this.lblStep2RateTitle);
            this.pnlStep2ReviewCard.Controls.Add(this.pnlStep2TermBox);
            this.pnlStep2ReviewCard.Controls.Add(this.lblStep2TermTitle);
            this.pnlStep2ReviewCard.Controls.Add(this.lblStep2PrincipalValue);
            this.pnlStep2ReviewCard.Controls.Add(this.lblStep2PrincipalTitle);
            this.pnlStep2ReviewCard.Controls.Add(this.lblStep2TotalSubtitle);
            this.pnlStep2ReviewCard.Controls.Add(this.lblStep2TotalAmount);
            this.pnlStep2ReviewCard.Controls.Add(this.lblStep2TotalTitle);
            this.pnlStep2ReviewCard.Controls.Add(this.pnlStep2Banner);
            this.pnlStep2ReviewCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStep2ReviewCard.FillColor = System.Drawing.Color.White;
            this.pnlStep2ReviewCard.Location = new System.Drawing.Point(16, 32);
            this.pnlStep2ReviewCard.Name = "pnlStep2ReviewCard";
            this.pnlStep2ReviewCard.Size = new System.Drawing.Size(338, 420);
            this.pnlStep2ReviewCard.TabIndex = 1;
            // 
            // lblStep2DueValue
            // 
            this.lblStep2DueValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStep2DueValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStep2DueValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep2DueValue.Location = new System.Drawing.Point(170, 382);
            this.lblStep2DueValue.Name = "lblStep2DueValue";
            this.lblStep2DueValue.Size = new System.Drawing.Size(154, 20);
            this.lblStep2DueValue.TabIndex = 15;
            this.lblStep2DueValue.Text = "27/11/2026";
            this.lblStep2DueValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStep2DueTitle
            // 
            this.lblStep2DueTitle.AutoSize = true;
            this.lblStep2DueTitle.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.lblStep2DueTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStep2DueTitle.Location = new System.Drawing.Point(14, 384);
            this.lblStep2DueTitle.Name = "lblStep2DueTitle";
            this.lblStep2DueTitle.Size = new System.Drawing.Size(133, 15);
            this.lblStep2DueTitle.TabIndex = 14;
            this.lblStep2DueTitle.Text = "Ngày đáo hạn (dự kiến)";
            // 
            // lblStep2AvailValue
            // 
            this.lblStep2AvailValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStep2AvailValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStep2AvailValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep2AvailValue.Location = new System.Drawing.Point(170, 350);
            this.lblStep2AvailValue.Name = "lblStep2AvailValue";
            this.lblStep2AvailValue.Size = new System.Drawing.Size(154, 20);
            this.lblStep2AvailValue.TabIndex = 13;
            this.lblStep2AvailValue.Text = "10.182.377 đ";
            this.lblStep2AvailValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStep2AvailTitle
            // 
            this.lblStep2AvailTitle.AutoSize = true;
            this.lblStep2AvailTitle.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.lblStep2AvailTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStep2AvailTitle.Location = new System.Drawing.Point(14, 352);
            this.lblStep2AvailTitle.Name = "lblStep2AvailTitle";
            this.lblStep2AvailTitle.Size = new System.Drawing.Size(89, 15);
            this.lblStep2AvailTitle.TabIndex = 12;
            this.lblStep2AvailTitle.Text = "Số dư khả dụng";
            // 
            // pnlStep2RenewalMethod
            // 
            this.pnlStep2RenewalMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStep2RenewalMethod.BackColor = System.Drawing.Color.White;
            this.pnlStep2RenewalMethod.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.pnlStep2RenewalMethod.BorderRadius = 8;
            this.pnlStep2RenewalMethod.BorderSize = 1;
            this.pnlStep2RenewalMethod.Controls.Add(this.cboStep2Renewal);
            this.pnlStep2RenewalMethod.FillColor = System.Drawing.Color.White;
            this.pnlStep2RenewalMethod.Location = new System.Drawing.Point(135, 298);
            this.pnlStep2RenewalMethod.Name = "pnlStep2RenewalMethod";
            this.pnlStep2RenewalMethod.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.pnlStep2RenewalMethod.Size = new System.Drawing.Size(189, 36);
            this.pnlStep2RenewalMethod.TabIndex = 11;
            // 
            // cboStep2Renewal
            // 
            this.cboStep2Renewal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboStep2Renewal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStep2Renewal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboStep2Renewal.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.cboStep2Renewal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cboStep2Renewal.FormattingEnabled = true;
            this.cboStep2Renewal.Items.AddRange(new object[] {
            "Tái tục gốc & lãi (Lãi gộp)",
            "Tự động tái tục gốc",
            "Tất toán về tài khoản"});
            this.cboStep2Renewal.Location = new System.Drawing.Point(6, 4);
            this.cboStep2Renewal.Name = "cboStep2Renewal";
            this.cboStep2Renewal.Size = new System.Drawing.Size(177, 21);
            this.cboStep2Renewal.TabIndex = 0;
            // 
            // lblStep2RenewalTitle
            // 
            this.lblStep2RenewalTitle.AutoSize = true;
            this.lblStep2RenewalTitle.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.lblStep2RenewalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStep2RenewalTitle.Location = new System.Drawing.Point(14, 308);
            this.lblStep2RenewalTitle.Name = "lblStep2RenewalTitle";
            this.lblStep2RenewalTitle.Size = new System.Drawing.Size(107, 15);
            this.lblStep2RenewalTitle.TabIndex = 10;
            this.lblStep2RenewalTitle.Text = "Hình thức đáo hạn";
            // 
            // lblStep2RateValue
            // 
            this.lblStep2RateValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStep2RateValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStep2RateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep2RateValue.Location = new System.Drawing.Point(170, 264);
            this.lblStep2RateValue.Name = "lblStep2RateValue";
            this.lblStep2RateValue.Size = new System.Drawing.Size(154, 20);
            this.lblStep2RateValue.TabIndex = 9;
            this.lblStep2RateValue.Text = "4.75% / năm";
            this.lblStep2RateValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStep2RateTitle
            // 
            this.lblStep2RateTitle.AutoSize = true;
            this.lblStep2RateTitle.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.lblStep2RateTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStep2RateTitle.Location = new System.Drawing.Point(14, 266);
            this.lblStep2RateTitle.Name = "lblStep2RateTitle";
            this.lblStep2RateTitle.Size = new System.Drawing.Size(103, 15);
            this.lblStep2RateTitle.TabIndex = 8;
            this.lblStep2RateTitle.Text = "Lãi suất áp dụng";
            // 
            // pnlStep2TermBox
            // 
            this.pnlStep2TermBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStep2TermBox.BackColor = System.Drawing.Color.White;
            this.pnlStep2TermBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.pnlStep2TermBox.BorderRadius = 8;
            this.pnlStep2TermBox.BorderSize = 1;
            this.pnlStep2TermBox.Controls.Add(this.cboStep2Term);
            this.pnlStep2TermBox.FillColor = System.Drawing.Color.White;
            this.pnlStep2TermBox.Location = new System.Drawing.Point(18, 214);
            this.pnlStep2TermBox.Name = "pnlStep2TermBox";
            this.pnlStep2TermBox.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.pnlStep2TermBox.Size = new System.Drawing.Size(306, 36);
            this.pnlStep2TermBox.TabIndex = 7;
            // 
            // cboStep2Term
            // 
            this.cboStep2Term.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboStep2Term.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStep2Term.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboStep2Term.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboStep2Term.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cboStep2Term.FormattingEnabled = true;
            this.cboStep2Term.Location = new System.Drawing.Point(8, 4);
            this.cboStep2Term.Name = "cboStep2Term";
            this.cboStep2Term.Size = new System.Drawing.Size(290, 23);
            this.cboStep2Term.TabIndex = 0;
            // 
            // lblStep2TermTitle
            // 
            this.lblStep2TermTitle.AutoSize = true;
            this.lblStep2TermTitle.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.lblStep2TermTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStep2TermTitle.Location = new System.Drawing.Point(14, 194);
            this.lblStep2TermTitle.Name = "lblStep2TermTitle";
            this.lblStep2TermTitle.Size = new System.Drawing.Size(46, 15);
            this.lblStep2TermTitle.TabIndex = 6;
            this.lblStep2TermTitle.Text = "Kỳ hạn";
            // 
            // lblStep2PrincipalValue
            // 
            this.lblStep2PrincipalValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStep2PrincipalValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStep2PrincipalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep2PrincipalValue.Location = new System.Drawing.Point(170, 160);
            this.lblStep2PrincipalValue.Name = "lblStep2PrincipalValue";
            this.lblStep2PrincipalValue.Size = new System.Drawing.Size(154, 20);
            this.lblStep2PrincipalValue.TabIndex = 5;
            this.lblStep2PrincipalValue.Text = "10.000.000 đ";
            this.lblStep2PrincipalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStep2PrincipalTitle
            // 
            this.lblStep2PrincipalTitle.AutoSize = true;
            this.lblStep2PrincipalTitle.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.lblStep2PrincipalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStep2PrincipalTitle.Location = new System.Drawing.Point(14, 162);
            this.lblStep2PrincipalTitle.Name = "lblStep2PrincipalTitle";
            this.lblStep2PrincipalTitle.Size = new System.Drawing.Size(70, 15);
            this.lblStep2PrincipalTitle.TabIndex = 4;
            this.lblStep2PrincipalTitle.Text = "Số tiền gốc";
            // 
            // lblStep2TotalSubtitle
            // 
            this.lblStep2TotalSubtitle.AutoSize = true;
            this.lblStep2TotalSubtitle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblStep2TotalSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStep2TotalSubtitle.Location = new System.Drawing.Point(14, 118);
            this.lblStep2TotalSubtitle.Name = "lblStep2TotalSubtitle";
            this.lblStep2TotalSubtitle.Size = new System.Drawing.Size(183, 13);
            this.lblStep2TotalSubtitle.TabIndex = 3;
            this.lblStep2TotalSubtitle.Text = "Số tiền nhận khi đáo hạn (dự tính)";
            // 
            // lblStep2TotalAmount
            // 
            this.lblStep2TotalAmount.AutoSize = true;
            this.lblStep2TotalAmount.Font = new System.Drawing.Font("Segoe UI", 17F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.lblStep2TotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.lblStep2TotalAmount.Location = new System.Drawing.Point(12, 82);
            this.lblStep2TotalAmount.Name = "lblStep2TotalAmount";
            this.lblStep2TotalAmount.Size = new System.Drawing.Size(161, 31);
            this.lblStep2TotalAmount.TabIndex = 2;
            this.lblStep2TotalAmount.Text = "10.119.726 đ";
            // 
            // lblStep2TotalTitle
            // 
            this.lblStep2TotalTitle.AutoSize = true;
            this.lblStep2TotalTitle.Font = new System.Drawing.Font("Segoe UI", 8.75F, System.Drawing.FontStyle.Bold);
            this.lblStep2TotalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep2TotalTitle.Location = new System.Drawing.Point(14, 58);
            this.lblStep2TotalTitle.Name = "lblStep2TotalTitle";
            this.lblStep2TotalTitle.Size = new System.Drawing.Size(222, 15);
            this.lblStep2TotalTitle.TabIndex = 1;
            this.lblStep2TotalTitle.Text = "Số tiền đáo hạn dự kiến (Lãi gộp)";
            // 
            // pnlStep2Banner
            // 
            this.pnlStep2Banner.BackColor = System.Drawing.Color.Transparent;
            this.pnlStep2Banner.BorderColor = System.Drawing.Color.Transparent;
            this.pnlStep2Banner.BorderRadius = 14;
            this.pnlStep2Banner.BorderSize = 0;
            this.pnlStep2Banner.Controls.Add(this.lblStep2BannerText);
            this.pnlStep2Banner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStep2Banner.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.pnlStep2Banner.Location = new System.Drawing.Point(0, 0);
            this.pnlStep2Banner.Name = "pnlStep2Banner";
            this.pnlStep2Banner.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.pnlStep2Banner.Size = new System.Drawing.Size(338, 42);
            this.pnlStep2Banner.TabIndex = 0;
            // 
            // lblStep2BannerText
            // 
            this.lblStep2BannerText.AutoSize = true;
            this.lblStep2BannerText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblStep2BannerText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(95)))), ((int)(((byte)(70)))));
            this.lblStep2BannerText.Location = new System.Drawing.Point(14, 11);
            this.lblStep2BannerText.Name = "lblStep2BannerText";
            this.lblStep2BannerText.Size = new System.Drawing.Size(183, 20);
            this.lblStep2BannerText.TabIndex = 0;
            this.lblStep2BannerText.Text = "2. Xem lại & Mở TKTK";
            // 
            // pnlStep2Progress
            // 
            this.pnlStep2Progress.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStep2Progress.Location = new System.Drawing.Point(16, 6);
            this.pnlStep2Progress.Name = "pnlStep2Progress";
            this.pnlStep2Progress.Size = new System.Drawing.Size(338, 26);
            this.pnlStep2Progress.TabIndex = 0;
            // 
            // pnlStep3
            // 
            this.pnlStep3.AutoScroll = true;
            this.pnlStep3.Controls.Add(this.pnlStep3Buttons);
            this.pnlStep3.Controls.Add(this.btnStep3DownloadReceipt);
            this.pnlStep3.Controls.Add(this.pnlStep3Notif);
            this.pnlStep3.Controls.Add(this.pnlStep3ReceiptCard);
            this.pnlStep3.Controls.Add(this.lblStep3SuccessSub);
            this.pnlStep3.Controls.Add(this.lblStep3SuccessTitle);
            this.pnlStep3.Controls.Add(this.pnlStep3CheckIcon);
            this.pnlStep3.Controls.Add(this.pnlStep3Banner);
            this.pnlStep3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlStep3.Location = new System.Drawing.Point(0, 0);
            this.pnlStep3.Name = "pnlStep3";
            this.pnlStep3.Padding = new System.Windows.Forms.Padding(16, 8, 16, 16);
            this.pnlStep3.Size = new System.Drawing.Size(370, 588);
            this.pnlStep3.TabIndex = 2;
            this.pnlStep3.Visible = false;
            // 
            // pnlStep3Buttons
            // 
            this.pnlStep3Buttons.Controls.Add(this.btnStep3Close);
            this.pnlStep3Buttons.Controls.Add(this.btnStep3Settle);
            this.pnlStep3Buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStep3Buttons.Location = new System.Drawing.Point(16, 524);
            this.pnlStep3Buttons.Name = "pnlStep3Buttons";
            this.pnlStep3Buttons.Size = new System.Drawing.Size(338, 48);
            this.pnlStep3Buttons.TabIndex = 7;
            // 
            // btnStep3Close
            // 
            this.btnStep3Close.BorderRadius = 10;
            this.btnStep3Close.BorderSize = 0;
            this.btnStep3Close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStep3Close.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnStep3Close.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnStep3Close.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(18)))), ((int)(((byte)(60)))));
            this.btnStep3Close.Location = new System.Drawing.Point(176, 0);
            this.btnStep3Close.Name = "btnStep3Close";
            this.btnStep3Close.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(29)))), ((int)(((byte)(72)))));
            this.btnStep3Close.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(18)))), ((int)(((byte)(57)))));
            this.btnStep3Close.Size = new System.Drawing.Size(162, 48);
            this.btnStep3Close.TabIndex = 1;
            this.btnStep3Close.Text = "Đóng";
            this.btnStep3Close.TextColor = System.Drawing.Color.White;
            // 
            // btnStep3Settle
            // 
            this.btnStep3Settle.BorderRadius = 10;
            this.btnStep3Settle.BorderSize = 1;
            this.btnStep3Settle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStep3Settle.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnStep3Settle.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnStep3Settle.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnStep3Settle.Location = new System.Drawing.Point(0, 0);
            this.btnStep3Settle.Name = "btnStep3Settle";
            this.btnStep3Settle.NormalBorder = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnStep3Settle.NormalColor = System.Drawing.Color.White;
            this.btnStep3Settle.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnStep3Settle.Size = new System.Drawing.Size(162, 48);
            this.btnStep3Settle.TabIndex = 0;
            this.btnStep3Settle.Text = "Tất toán";
            this.btnStep3Settle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            // 
            // btnStep3DownloadReceipt
            // 
            this.btnStep3DownloadReceipt.BorderRadius = 10;
            this.btnStep3DownloadReceipt.BorderSize = 0;
            this.btnStep3DownloadReceipt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStep3DownloadReceipt.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnStep3DownloadReceipt.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnStep3DownloadReceipt.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnStep3DownloadReceipt.Location = new System.Drawing.Point(16, 466);
            this.btnStep3DownloadReceipt.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.btnStep3DownloadReceipt.Name = "btnStep3DownloadReceipt";
            this.btnStep3DownloadReceipt.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnStep3DownloadReceipt.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(6)))), ((int)(((byte)(23)))));
            this.btnStep3DownloadReceipt.Size = new System.Drawing.Size(338, 46);
            this.btnStep3DownloadReceipt.TabIndex = 6;
            this.btnStep3DownloadReceipt.Text = "Tải biên lai";
            this.btnStep3DownloadReceipt.TextColor = System.Drawing.Color.White;
            // 
            // pnlStep3Notif
            // 
            this.pnlStep3Notif.BackColor = System.Drawing.Color.Transparent;
            this.pnlStep3Notif.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlStep3Notif.BorderRadius = 8;
            this.pnlStep3Notif.BorderSize = 1;
            this.pnlStep3Notif.Controls.Add(this.lblStep3Notif);
            this.pnlStep3Notif.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStep3Notif.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlStep3Notif.Location = new System.Drawing.Point(16, 422);
            this.pnlStep3Notif.Margin = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.pnlStep3Notif.Name = "pnlStep3Notif";
            this.pnlStep3Notif.Padding = new System.Windows.Forms.Padding(8);
            this.pnlStep3Notif.Size = new System.Drawing.Size(338, 38);
            this.pnlStep3Notif.TabIndex = 5;
            // 
            // lblStep3Notif
            // 
            this.lblStep3Notif.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStep3Notif.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblStep3Notif.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStep3Notif.Location = new System.Drawing.Point(8, 8);
            this.lblStep3Notif.Name = "lblStep3Notif";
            this.lblStep3Notif.Size = new System.Drawing.Size(322, 22);
            this.lblStep3Notif.TabIndex = 0;
            this.lblStep3Notif.Text = "📄 Tài khoản tiết kiệm đã được kích hoạt thành công";
            this.lblStep3Notif.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlStep3ReceiptCard
            // 
            this.pnlStep3ReceiptCard.BackColor = System.Drawing.Color.Transparent;
            this.pnlStep3ReceiptCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlStep3ReceiptCard.BorderRadius = 12;
            this.pnlStep3ReceiptCard.BorderSize = 1;
            this.pnlStep3ReceiptCard.Controls.Add(this.lblStep3AccountValue);
            this.pnlStep3ReceiptCard.Controls.Add(this.lblStep3AccountTitle);
            this.pnlStep3ReceiptCard.Controls.Add(this.lblStep3DueDateValue);
            this.pnlStep3ReceiptCard.Controls.Add(this.lblStep3DueDateTitle);
            this.pnlStep3ReceiptCard.Controls.Add(this.lblStep3OpenDateValue);
            this.pnlStep3ReceiptCard.Controls.Add(this.lblStep3OpenDateTitle);
            this.pnlStep3ReceiptCard.Controls.Add(this.lblStep3RenewalValue);
            this.pnlStep3ReceiptCard.Controls.Add(this.lblStep3RenewalTitle);
            this.pnlStep3ReceiptCard.Controls.Add(this.lblStep3RateValue);
            this.pnlStep3ReceiptCard.Controls.Add(this.lblStep3RateTitle);
            this.pnlStep3ReceiptCard.Controls.Add(this.lblStep3TermValue);
            this.pnlStep3ReceiptCard.Controls.Add(this.lblStep3TermTitle);
            this.pnlStep3ReceiptCard.Controls.Add(this.lblStep3PrincipalValue);
            this.pnlStep3ReceiptCard.Controls.Add(this.lblStep3PrincipalTitle);
            this.pnlStep3ReceiptCard.Controls.Add(this.lblStep3CodeValue);
            this.pnlStep3ReceiptCard.Controls.Add(this.lblStep3CodeTitle);
            this.pnlStep3ReceiptCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStep3ReceiptCard.FillColor = System.Drawing.Color.White;
            this.pnlStep3ReceiptCard.Location = new System.Drawing.Point(16, 172);
            this.pnlStep3ReceiptCard.Name = "pnlStep3ReceiptCard";
            this.pnlStep3ReceiptCard.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.pnlStep3ReceiptCard.Size = new System.Drawing.Size(338, 250);
            this.pnlStep3ReceiptCard.TabIndex = 4;
            // 
            // lblStep3AccountValue
            // 
            this.lblStep3AccountValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStep3AccountValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStep3AccountValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep3AccountValue.Location = new System.Drawing.Point(170, 220);
            this.lblStep3AccountValue.Name = "lblStep3AccountValue";
            this.lblStep3AccountValue.Size = new System.Drawing.Size(156, 18);
            this.lblStep3AccountValue.TabIndex = 15;
            this.lblStep3AccountValue.Text = "0851000...";
            this.lblStep3AccountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStep3AccountTitle
            // 
            this.lblStep3AccountTitle.AutoSize = true;
            this.lblStep3AccountTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStep3AccountTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStep3AccountTitle.Location = new System.Drawing.Point(12, 222);
            this.lblStep3AccountTitle.Name = "lblStep3AccountTitle";
            this.lblStep3AccountTitle.Size = new System.Drawing.Size(109, 15);
            this.lblStep3AccountTitle.TabIndex = 14;
            this.lblStep3AccountTitle.Text = "Tài khoản trích nợ";
            // 
            // lblStep3DueDateValue
            // 
            this.lblStep3DueDateValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStep3DueDateValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStep3DueDateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep3DueDateValue.Location = new System.Drawing.Point(170, 190);
            this.lblStep3DueDateValue.Name = "lblStep3DueDateValue";
            this.lblStep3DueDateValue.Size = new System.Drawing.Size(156, 18);
            this.lblStep3DueDateValue.TabIndex = 13;
            this.lblStep3DueDateValue.Text = "25/02/2027";
            this.lblStep3DueDateValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStep3DueDateTitle
            // 
            this.lblStep3DueDateTitle.AutoSize = true;
            this.lblStep3DueDateTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStep3DueDateTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStep3DueDateTitle.Location = new System.Drawing.Point(12, 192);
            this.lblStep3DueDateTitle.Name = "lblStep3DueDateTitle";
            this.lblStep3DueDateTitle.Size = new System.Drawing.Size(83, 15);
            this.lblStep3DueDateTitle.TabIndex = 12;
            this.lblStep3DueDateTitle.Text = "Ngày đáo hạn";
            // 
            // lblStep3OpenDateValue
            // 
            this.lblStep3OpenDateValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStep3OpenDateValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStep3OpenDateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep3OpenDateValue.Location = new System.Drawing.Point(170, 160);
            this.lblStep3OpenDateValue.Name = "lblStep3OpenDateValue";
            this.lblStep3OpenDateValue.Size = new System.Drawing.Size(156, 18);
            this.lblStep3OpenDateValue.TabIndex = 11;
            this.lblStep3OpenDateValue.Text = "25/11/2026";
            this.lblStep3OpenDateValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStep3OpenDateTitle
            // 
            this.lblStep3OpenDateTitle.AutoSize = true;
            this.lblStep3OpenDateTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStep3OpenDateTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStep3OpenDateTitle.Location = new System.Drawing.Point(12, 162);
            this.lblStep3OpenDateTitle.Name = "lblStep3OpenDateTitle";
            this.lblStep3OpenDateTitle.Size = new System.Drawing.Size(56, 15);
            this.lblStep3OpenDateTitle.TabIndex = 10;
            this.lblStep3OpenDateTitle.Text = "Ngày mở";
            // 
            // lblStep3RenewalValue
            // 
            this.lblStep3RenewalValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStep3RenewalValue.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblStep3RenewalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep3RenewalValue.Location = new System.Drawing.Point(150, 130);
            this.lblStep3RenewalValue.Name = "lblStep3RenewalValue";
            this.lblStep3RenewalValue.Size = new System.Drawing.Size(176, 18);
            this.lblStep3RenewalValue.TabIndex = 9;
            this.lblStep3RenewalValue.Text = "Tự động tái tục gốc";
            this.lblStep3RenewalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStep3RenewalTitle
            // 
            this.lblStep3RenewalTitle.AutoSize = true;
            this.lblStep3RenewalTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStep3RenewalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStep3RenewalTitle.Location = new System.Drawing.Point(12, 132);
            this.lblStep3RenewalTitle.Name = "lblStep3RenewalTitle";
            this.lblStep3RenewalTitle.Size = new System.Drawing.Size(126, 15);
            this.lblStep3RenewalTitle.TabIndex = 8;
            this.lblStep3RenewalTitle.Text = "Phương thức đáo hạn";
            // 
            // lblStep3RateValue
            // 
            this.lblStep3RateValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStep3RateValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStep3RateValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep3RateValue.Location = new System.Drawing.Point(170, 100);
            this.lblStep3RateValue.Name = "lblStep3RateValue";
            this.lblStep3RateValue.Size = new System.Drawing.Size(156, 18);
            this.lblStep3RateValue.TabIndex = 7;
            this.lblStep3RateValue.Text = "4.75% / năm";
            this.lblStep3RateValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStep3RateTitle
            // 
            this.lblStep3RateTitle.AutoSize = true;
            this.lblStep3RateTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStep3RateTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStep3RateTitle.Location = new System.Drawing.Point(12, 102);
            this.lblStep3RateTitle.Name = "lblStep3RateTitle";
            this.lblStep3RateTitle.Size = new System.Drawing.Size(49, 15);
            this.lblStep3RateTitle.TabIndex = 6;
            this.lblStep3RateTitle.Text = "Lãi suất";
            // 
            // lblStep3TermValue
            // 
            this.lblStep3TermValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStep3TermValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStep3TermValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep3TermValue.Location = new System.Drawing.Point(170, 70);
            this.lblStep3TermValue.Name = "lblStep3TermValue";
            this.lblStep3TermValue.Size = new System.Drawing.Size(156, 18);
            this.lblStep3TermValue.TabIndex = 5;
            this.lblStep3TermValue.Text = "3 tháng";
            this.lblStep3TermValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStep3TermTitle
            // 
            this.lblStep3TermTitle.AutoSize = true;
            this.lblStep3TermTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStep3TermTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStep3TermTitle.Location = new System.Drawing.Point(12, 72);
            this.lblStep3TermTitle.Name = "lblStep3TermTitle";
            this.lblStep3TermTitle.Size = new System.Drawing.Size(46, 15);
            this.lblStep3TermTitle.TabIndex = 4;
            this.lblStep3TermTitle.Text = "Kỳ hạn";
            // 
            // lblStep3PrincipalValue
            // 
            this.lblStep3PrincipalValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStep3PrincipalValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblStep3PrincipalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblStep3PrincipalValue.Location = new System.Drawing.Point(170, 40);
            this.lblStep3PrincipalValue.Name = "lblStep3PrincipalValue";
            this.lblStep3PrincipalValue.Size = new System.Drawing.Size(156, 20);
            this.lblStep3PrincipalValue.TabIndex = 3;
            this.lblStep3PrincipalValue.Text = "10.000.000 đ";
            this.lblStep3PrincipalValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStep3PrincipalTitle
            // 
            this.lblStep3PrincipalTitle.AutoSize = true;
            this.lblStep3PrincipalTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStep3PrincipalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStep3PrincipalTitle.Location = new System.Drawing.Point(12, 42);
            this.lblStep3PrincipalTitle.Name = "lblStep3PrincipalTitle";
            this.lblStep3PrincipalTitle.Size = new System.Drawing.Size(76, 15);
            this.lblStep3PrincipalTitle.TabIndex = 2;
            this.lblStep3PrincipalTitle.Text = "Tiền gốc gửi";
            // 
            // lblStep3CodeValue
            // 
            this.lblStep3CodeValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStep3CodeValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStep3CodeValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep3CodeValue.Location = new System.Drawing.Point(170, 10);
            this.lblStep3CodeValue.Name = "lblStep3CodeValue";
            this.lblStep3CodeValue.Size = new System.Drawing.Size(156, 20);
            this.lblStep3CodeValue.TabIndex = 1;
            this.lblStep3CodeValue.Text = "TK-83102228";
            this.lblStep3CodeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStep3CodeTitle
            // 
            this.lblStep3CodeTitle.AutoSize = true;
            this.lblStep3CodeTitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStep3CodeTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStep3CodeTitle.Location = new System.Drawing.Point(12, 12);
            this.lblStep3CodeTitle.Name = "lblStep3CodeTitle";
            this.lblStep3CodeTitle.Size = new System.Drawing.Size(117, 15);
            this.lblStep3CodeTitle.TabIndex = 0;
            this.lblStep3CodeTitle.Text = "Số tài khoản TKTK";
            // 
            // lblStep3SuccessSub
            // 
            this.lblStep3SuccessSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStep3SuccessSub.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStep3SuccessSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStep3SuccessSub.Location = new System.Drawing.Point(16, 146);
            this.lblStep3SuccessSub.Name = "lblStep3SuccessSub";
            this.lblStep3SuccessSub.Size = new System.Drawing.Size(338, 26);
            this.lblStep3SuccessSub.TabIndex = 3;
            this.lblStep3SuccessSub.Text = "Hệ thống đã trích tiền thành công từ tài khoản nguồn";
            this.lblStep3SuccessSub.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblStep3SuccessTitle
            // 
            this.lblStep3SuccessTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStep3SuccessTitle.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblStep3SuccessTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblStep3SuccessTitle.Location = new System.Drawing.Point(16, 114);
            this.lblStep3SuccessTitle.Name = "lblStep3SuccessTitle";
            this.lblStep3SuccessTitle.Size = new System.Drawing.Size(338, 32);
            this.lblStep3SuccessTitle.TabIndex = 2;
            this.lblStep3SuccessTitle.Text = "Tài khoản tiết kiệm đã mở thành công";
            this.lblStep3SuccessTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStep3CheckIcon
            // 
            this.pnlStep3CheckIcon.BackColor = System.Drawing.Color.Transparent;
            this.pnlStep3CheckIcon.BorderColor = System.Drawing.Color.Transparent;
            this.pnlStep3CheckIcon.BorderRadius = 26;
            this.pnlStep3CheckIcon.BorderSize = 0;
            this.pnlStep3CheckIcon.Controls.Add(this.lblStep3CheckMark);
            this.pnlStep3CheckIcon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.pnlStep3CheckIcon.Location = new System.Drawing.Point(159, 54);
            this.pnlStep3CheckIcon.Name = "pnlStep3CheckIcon";
            this.pnlStep3CheckIcon.Size = new System.Drawing.Size(52, 52);
            this.pnlStep3CheckIcon.TabIndex = 1;
            // 
            // lblStep3CheckMark
            // 
            this.lblStep3CheckMark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStep3CheckMark.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblStep3CheckMark.ForeColor = System.Drawing.Color.White;
            this.lblStep3CheckMark.Location = new System.Drawing.Point(0, 0);
            this.lblStep3CheckMark.Name = "lblStep3CheckMark";
            this.lblStep3CheckMark.Size = new System.Drawing.Size(52, 52);
            this.lblStep3CheckMark.TabIndex = 0;
            this.lblStep3CheckMark.Text = "✔";
            this.lblStep3CheckMark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlStep3Banner
            // 
            this.pnlStep3Banner.BackColor = System.Drawing.Color.Transparent;
            this.pnlStep3Banner.BorderColor = System.Drawing.Color.Transparent;
            this.pnlStep3Banner.BorderRadius = 14;
            this.pnlStep3Banner.BorderSize = 0;
            this.pnlStep3Banner.Controls.Add(this.lblStep3BannerText);
            this.pnlStep3Banner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStep3Banner.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            this.pnlStep3Banner.Location = new System.Drawing.Point(16, 8);
            this.pnlStep3Banner.Name = "pnlStep3Banner";
            this.pnlStep3Banner.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.pnlStep3Banner.Size = new System.Drawing.Size(338, 42);
            this.pnlStep3Banner.TabIndex = 0;
            // 
            // lblStep3BannerText
            // 
            this.lblStep3BannerText.AutoSize = true;
            this.lblStep3BannerText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblStep3BannerText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblStep3BannerText.Location = new System.Drawing.Point(14, 11);
            this.lblStep3BannerText.Name = "lblStep3BannerText";
            this.lblStep3BannerText.Size = new System.Drawing.Size(123, 20);
            this.lblStep3BannerText.TabIndex = 0;
            this.lblStep3BannerText.Text = "3. Chi tiết TKTK";
            // 
            // 
            // pnlPhoneTopNav
            // 
            this.pnlPhoneTopNav.Controls.Add(this.pnlTopNavTitleWrapper);
            this.pnlPhoneTopNav.Controls.Add(this.btnPhoneRightIcon);
            this.pnlPhoneTopNav.Controls.Add(this.btnPhoneBack);
            this.pnlPhoneTopNav.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPhoneTopNav.Location = new System.Drawing.Point(0, 0);
            this.pnlPhoneTopNav.Name = "pnlPhoneTopNav";
            this.pnlPhoneTopNav.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.pnlPhoneTopNav.Size = new System.Drawing.Size(512, 56);
            this.pnlPhoneTopNav.TabIndex = 1;
            // 
            // btnPhoneRightIcon
            // 
            this.btnPhoneRightIcon.BackColor = System.Drawing.Color.Transparent;
            this.btnPhoneRightIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPhoneRightIcon.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPhoneRightIcon.FlatAppearance.BorderSize = 0;
            this.btnPhoneRightIcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPhoneRightIcon.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnPhoneRightIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnPhoneRightIcon.Location = new System.Drawing.Point(472, 0);
            this.btnPhoneRightIcon.Name = "btnPhoneRightIcon";
            this.btnPhoneRightIcon.Size = new System.Drawing.Size(40, 48);
            this.btnPhoneRightIcon.TabIndex = 3;
            this.btnPhoneRightIcon.Text = "📄";
            this.btnPhoneRightIcon.UseVisualStyleBackColor = false;
            // 
            // btnPhoneBack
            // 
            this.btnPhoneBack.BackColor = System.Drawing.Color.Transparent;
            this.btnPhoneBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPhoneBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPhoneBack.FlatAppearance.BorderSize = 0;
            this.btnPhoneBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPhoneBack.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnPhoneBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnPhoneBack.Location = new System.Drawing.Point(0, 0);
            this.btnPhoneBack.Name = "btnPhoneBack";
            this.btnPhoneBack.Size = new System.Drawing.Size(40, 48);
            this.btnPhoneBack.TabIndex = 0;
            this.btnPhoneBack.Text = "←";
            this.btnPhoneBack.UseVisualStyleBackColor = false;
            // 
            // pnlTopNavTitleWrapper
            // 
            this.pnlTopNavTitleWrapper.Controls.Add(this.lblPhoneNavSubtitle);
            this.pnlTopNavTitleWrapper.Controls.Add(this.lblPhoneNavTitle);
            this.pnlTopNavTitleWrapper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTopNavTitleWrapper.Location = new System.Drawing.Point(40, 0);
            this.pnlTopNavTitleWrapper.Name = "pnlTopNavTitleWrapper";
            this.pnlTopNavTitleWrapper.Padding = new System.Windows.Forms.Padding(8, 2, 8, 0);
            this.pnlTopNavTitleWrapper.Size = new System.Drawing.Size(432, 48);
            this.pnlTopNavTitleWrapper.TabIndex = 2;
            // 
            // lblPhoneNavTitle
            // 
            this.lblPhoneNavTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPhoneNavTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPhoneNavTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblPhoneNavTitle.Location = new System.Drawing.Point(8, 2);
            this.lblPhoneNavTitle.Name = "lblPhoneNavTitle";
            this.lblPhoneNavTitle.Size = new System.Drawing.Size(416, 24);
            this.lblPhoneNavTitle.TabIndex = 1;
            this.lblPhoneNavTitle.Text = "Mở tài khoản tiết kiệm";
            this.lblPhoneNavTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPhoneNavSubtitle
            // 
            this.lblPhoneNavSubtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPhoneNavSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblPhoneNavSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPhoneNavSubtitle.Location = new System.Drawing.Point(8, 26);
            this.lblPhoneNavSubtitle.Name = "lblPhoneNavSubtitle";
            this.lblPhoneNavSubtitle.Size = new System.Drawing.Size(416, 18);
            this.lblPhoneNavSubtitle.TabIndex = 2;
            this.lblPhoneNavSubtitle.Text = "Gửi trực tuyến lãi suất ưu đãi - An toàn & tiện lợi 24/7";
            this.lblPhoneNavSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SaveMoneyUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1120, 720);
            this.Controls.Add(this.pnlRoot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SaveMoneyUserForm";
            this.Text = "SaveMoneyUserForm";
            this.pnlRoot.ResumeLayout(false);
            this.tblMainLayout.ResumeLayout(false);
            this.pnlLeftOverview.ResumeLayout(false);
            this.pnlHistoryCard.ResumeLayout(false);
            this.pnlGridWrapper.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserSavings)).EndInit();
            this.pnlSummaryBanner.ResumeLayout(false);
            this.pnlSummaryBanner.PerformLayout();
            this.pnlRightPhone.ResumeLayout(false);
            this.pnlPhoneChassis.ResumeLayout(false);
            this.pnlPhoneScreen.ResumeLayout(false);
            this.pnlStepContainer.ResumeLayout(false);
            this.pnlStep1.ResumeLayout(false);
            this.pnlStep1PreviewCard.ResumeLayout(false);
            this.pnlStep1PreviewCard.PerformLayout();
            this.pnlTermDropdown.ResumeLayout(false);
            this.pnlAmountInput.ResumeLayout(false);
            this.pnlAmountInput.PerformLayout();
            this.pnlStep1HeaderCard.ResumeLayout(false);
            this.pnlStep1HeaderCard.PerformLayout();
            this.pnlStep1Banner.ResumeLayout(false);
            this.pnlStep1Banner.PerformLayout();
            this.pnlStep1AccountChoice.ResumeLayout(false);
            this.pnlStep2.ResumeLayout(false);
            this.pnlStep2ReviewCard.ResumeLayout(false);
            this.pnlStep2ReviewCard.PerformLayout();
            this.pnlStep2RenewalMethod.ResumeLayout(false);
            this.pnlStep2TermBox.ResumeLayout(false);
            this.pnlStep2Banner.ResumeLayout(false);
            this.pnlStep2Banner.PerformLayout();
            this.pnlStep3.ResumeLayout(false);
            this.pnlStep3Buttons.ResumeLayout(false);
            this.pnlStep3Notif.ResumeLayout(false);
            this.pnlStep3ReceiptCard.ResumeLayout(false);
            this.pnlStep3ReceiptCard.PerformLayout();
            this.pnlStep3CheckIcon.ResumeLayout(false);
            this.pnlStep3Banner.ResumeLayout(false);
            this.pnlStep3Banner.PerformLayout();
            this.pnlPhoneTopNav.ResumeLayout(false);
            this.pnlTopNavTitleWrapper.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlRoot;
        private System.Windows.Forms.TableLayoutPanel tblMainLayout;
        private System.Windows.Forms.Panel pnlLeftOverview;
        private System.Windows.Forms.Panel pnlRightPhone;
        private BankAccountManagement.Forms.RoundedPanel pnlPhoneChassis;
        private System.Windows.Forms.Panel pnlPhoneScreen;
        private System.Windows.Forms.Panel pnlTopNavTitleWrapper;
        private System.Windows.Forms.Label lblBannerDiamond;
        private System.Windows.Forms.Panel pnlPhoneTopNav;
        private System.Windows.Forms.Button btnPhoneBack;
        private System.Windows.Forms.Label lblPhoneNavTitle;
        private System.Windows.Forms.Label lblPhoneNavSubtitle;
        private System.Windows.Forms.Button btnPhoneRightIcon;
        private System.Windows.Forms.Panel pnlStepContainer;
        private System.Windows.Forms.Panel pnlStep1;
        private System.Windows.Forms.Panel pnlStep2;
        private System.Windows.Forms.Panel pnlStep3;
        private BankAccountManagement.Forms.RoundedPanel pnlStep1HeaderCard;
        private BankAccountManagement.Forms.RoundedPanel pnlStep1Banner;
        private System.Windows.Forms.Label lblStep1BannerText;
        private System.Windows.Forms.Label lblStep1AvailTitle;
        private System.Windows.Forms.Label lblStep1AvailBalance;
        private System.Windows.Forms.Label lblStep1AmountTitle;
        private BankAccountManagement.Forms.RoundedPanel pnlAmountInput;
        private System.Windows.Forms.TextBox txtStep1Amount;
        private System.Windows.Forms.Label lblStep1AmountUnit;
        private System.Windows.Forms.Label lblStep1TermTitle;
        private BankAccountManagement.Forms.RoundedPanel pnlTermDropdown;
        private System.Windows.Forms.ComboBox cboStep1Term;
        private BankAccountManagement.Forms.RoundedPanel pnlStep1PreviewCard;
        private System.Windows.Forms.Label lblStep1PreviewHeader;
        private System.Windows.Forms.Label lblStep1RateTitle;
        private System.Windows.Forms.Label lblStep1RateValue;
        private System.Windows.Forms.Label lblStep1EstInterestTitle;
        private System.Windows.Forms.Label lblStep1EstInterestValue;
        private BankAccountManagement.Forms.RoundedButton btnStep1Next;
        private BankAccountManagement.Forms.RoundedPanel pnlStep1AccountChoice;
        private System.Windows.Forms.ComboBox cboStep1Account;
        private System.Windows.Forms.Label lblStep1AccountTitle;
        private System.Windows.Forms.Panel pnlStep2Progress;
        private BankAccountManagement.Forms.RoundedPanel pnlStep2ReviewCard;
        private BankAccountManagement.Forms.RoundedPanel pnlStep2Banner;
        private System.Windows.Forms.Label lblStep2BannerText;
        private System.Windows.Forms.Label lblStep2TotalTitle;
        private System.Windows.Forms.Label lblStep2TotalAmount;
        private System.Windows.Forms.Label lblStep2TotalSubtitle;
        private System.Windows.Forms.Label lblStep2PrincipalTitle;
        private System.Windows.Forms.Label lblStep2PrincipalValue;
        private System.Windows.Forms.Label lblStep2TermTitle;
        private BankAccountManagement.Forms.RoundedPanel pnlStep2TermBox;
        private System.Windows.Forms.ComboBox cboStep2Term;
        private System.Windows.Forms.Label lblStep2RateTitle;
        private System.Windows.Forms.Label lblStep2RateValue;
        private System.Windows.Forms.Label lblStep2RenewalTitle;
        private BankAccountManagement.Forms.RoundedPanel pnlStep2RenewalMethod;
        private System.Windows.Forms.ComboBox cboStep2Renewal;
        private System.Windows.Forms.Label lblStep2AvailTitle;
        private System.Windows.Forms.Label lblStep2AvailValue;
        private System.Windows.Forms.Label lblStep2DueTitle;
        private System.Windows.Forms.Label lblStep2DueValue;
        private System.Windows.Forms.CheckBox chkStep2Agree;
        private System.Windows.Forms.Label lblStep2AgreeText;
        private BankAccountManagement.Forms.RoundedButton btnStep2Confirm;
        private BankAccountManagement.Forms.RoundedPanel pnlStep3Banner;
        private System.Windows.Forms.Label lblStep3BannerText;
        private BankAccountManagement.Forms.RoundedPanel pnlStep3CheckIcon;
        private System.Windows.Forms.Label lblStep3CheckMark;
        private System.Windows.Forms.Label lblStep3SuccessTitle;
        private System.Windows.Forms.Label lblStep3SuccessSub;
        private BankAccountManagement.Forms.RoundedPanel pnlStep3ReceiptCard;
        private System.Windows.Forms.Label lblStep3CodeTitle;
        private System.Windows.Forms.Label lblStep3CodeValue;
        private System.Windows.Forms.Label lblStep3PrincipalTitle;
        private System.Windows.Forms.Label lblStep3PrincipalValue;
        private System.Windows.Forms.Label lblStep3TermTitle;
        private System.Windows.Forms.Label lblStep3TermValue;
        private System.Windows.Forms.Label lblStep3RateTitle;
        private System.Windows.Forms.Label lblStep3RateValue;
        private System.Windows.Forms.Label lblStep3RenewalTitle;
        private System.Windows.Forms.Label lblStep3RenewalValue;
        private System.Windows.Forms.Label lblStep3OpenDateTitle;
        private System.Windows.Forms.Label lblStep3OpenDateValue;
        private System.Windows.Forms.Label lblStep3DueDateTitle;
        private System.Windows.Forms.Label lblStep3DueDateValue;
        private System.Windows.Forms.Label lblStep3AccountTitle;
        private System.Windows.Forms.Label lblStep3AccountValue;
        private BankAccountManagement.Forms.RoundedPanel pnlStep3Notif;
        private System.Windows.Forms.Label lblStep3Notif;
        private BankAccountManagement.Forms.RoundedButton btnStep3DownloadReceipt;
        private System.Windows.Forms.Panel pnlStep3Buttons;
        private BankAccountManagement.Forms.RoundedButton btnStep3Settle;
        private BankAccountManagement.Forms.RoundedButton btnStep3Close;
        private BankAccountManagement.Forms.RoundedPanel pnlSummaryBanner;
        private System.Windows.Forms.Label lblBannerHeader;
        private System.Windows.Forms.Label lblSumBalanceTitle;
        private System.Windows.Forms.Label lblSumBalance;
        private System.Windows.Forms.Label lblSumCountTitle;
        private System.Windows.Forms.Label lblSumCount;
        private System.Windows.Forms.Label lblSumInterestTitle;
        private System.Windows.Forms.Label lblSumInterest;
        private BankAccountManagement.Forms.RoundedButton btnNewSavings;
        private BankAccountManagement.Forms.RoundedButton btnRefreshUser;
        private BankAccountManagement.Forms.RoundedPanel pnlHistoryCard;
        private System.Windows.Forms.Label lblHistoryTitle;
        private BankAccountManagement.Forms.RoundedPanel pnlGridWrapper;
        private System.Windows.Forms.DataGridView dgvUserSavings;
    }
}
