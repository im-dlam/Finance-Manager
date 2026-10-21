namespace BankAccountManagement.Forms
{
    partial class DashboardUserForm
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
            this.tblBottomCards = new System.Windows.Forms.TableLayoutPanel();
            this.pnlSpendingCard = new BankAccountManagement.Forms.RoundedPanel();
            this.lblSpendingStatusBadge = new System.Windows.Forms.Label();
            this.pnlWeeklyProgressTrack = new BankAccountManagement.Forms.RoundedPanel();
            this.pnlWeeklyProgressBar = new BankAccountManagement.Forms.RoundedPanel();
            this.lblWeeklyRemaining = new System.Windows.Forms.Label();
            this.lblWeeklySpent = new System.Windows.Forms.Label();
            this.pnlDailyProgressTrack = new BankAccountManagement.Forms.RoundedPanel();
            this.pnlDailyProgressBar = new BankAccountManagement.Forms.RoundedPanel();
            this.lblDailyRemaining = new System.Windows.Forms.Label();
            this.lblDailySpent = new System.Windows.Forms.Label();
            this.pnlSpendingHeader = new System.Windows.Forms.Panel();
            this.btnOpenLimitDialog = new BankAccountManagement.Forms.RoundedButton();
            this.lblSpendingTitle = new System.Windows.Forms.Label();
            this.pnlDebtCard = new BankAccountManagement.Forms.RoundedPanel();
            this.btnPayDebt = new BankAccountManagement.Forms.RoundedButton();
            this.lblDebtStatus = new System.Windows.Forms.Label();
            this.lblDebtAmount = new System.Windows.Forms.Label();
            this.lblDebtDueDate = new System.Windows.Forms.Label();
            this.lblDebtTitle = new System.Windows.Forms.Label();
            this.pnlDarkCard = new BankAccountManagement.Forms.RoundedPanel();
            this.pnlNavActions = new System.Windows.Forms.Panel();
            this.lblNavDivider = new System.Windows.Forms.Label();
            this.btnNavCards = new System.Windows.Forms.Button();
            this.btnNavHistory = new System.Windows.Forms.Button();
            this.pnlSwitchRow = new System.Windows.Forms.Panel();
            this.lblSwitchInfo = new System.Windows.Forms.Label();
            this.pnlAccountInfo = new System.Windows.Forms.Panel();
            this.btnToggleBalance = new System.Windows.Forms.Button();
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblBalanceTitle = new System.Windows.Forms.Label();
            this.btnCopyStk = new System.Windows.Forms.Button();
            this.lblAccountNumber = new System.Windows.Forms.Label();
            this.lblAccountNumberTitle = new System.Windows.Forms.Label();
            this.pnlDarkHeader = new System.Windows.Forms.Panel();
            this.lblUserTier = new System.Windows.Forms.Label();
            this.pnlNameBadge = new BankAccountManagement.Forms.RoundedPanel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.pnlAvatar = new BankAccountManagement.Forms.RoundedPanel();
            this.lblAvatar = new System.Windows.Forms.Label();
            this.lblMainTitle = new System.Windows.Forms.Label();
            this.pnlRoot.SuspendLayout();
            this.tblBottomCards.SuspendLayout();
            this.pnlSpendingCard.SuspendLayout();
            this.pnlWeeklyProgressTrack.SuspendLayout();
            this.pnlDailyProgressTrack.SuspendLayout();
            this.pnlSpendingHeader.SuspendLayout();
            this.pnlDebtCard.SuspendLayout();
            this.pnlDarkCard.SuspendLayout();
            this.pnlNavActions.SuspendLayout();
            this.pnlSwitchRow.SuspendLayout();
            this.pnlAccountInfo.SuspendLayout();
            this.pnlDarkHeader.SuspendLayout();
            this.pnlNameBadge.SuspendLayout();
            this.pnlAvatar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRoot
            // 
            this.pnlRoot.AutoScroll = true;
            this.pnlRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlRoot.Controls.Add(this.tblBottomCards);
            this.pnlRoot.Controls.Add(this.pnlDarkCard);
            this.pnlRoot.Controls.Add(this.lblMainTitle);
            this.pnlRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRoot.Location = new System.Drawing.Point(0, 0);
            this.pnlRoot.Name = "pnlRoot";
            this.pnlRoot.Padding = new System.Windows.Forms.Padding(24, 18, 24, 20);
            this.pnlRoot.Size = new System.Drawing.Size(1000, 680);
            this.pnlRoot.TabIndex = 0;
            // 
            // tblBottomCards
            // 
            this.tblBottomCards.ColumnCount = 2;
            this.tblBottomCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblBottomCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblBottomCards.Controls.Add(this.pnlSpendingCard, 1, 0);
            this.tblBottomCards.Controls.Add(this.pnlDebtCard, 0, 0);
            this.tblBottomCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblBottomCards.Location = new System.Drawing.Point(24, 280);
            this.tblBottomCards.Name = "tblBottomCards";
            this.tblBottomCards.RowCount = 1;
            this.tblBottomCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblBottomCards.Size = new System.Drawing.Size(952, 380);
            this.tblBottomCards.TabIndex = 2;
            // 
            // pnlSpendingCard
            // 
            this.pnlSpendingCard.BackColor = System.Drawing.Color.Transparent;
            this.pnlSpendingCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlSpendingCard.BorderRadius = 14;
            this.pnlSpendingCard.BorderSize = 1;
            this.pnlSpendingCard.Controls.Add(this.lblSpendingStatusBadge);
            this.pnlSpendingCard.Controls.Add(this.pnlWeeklyProgressTrack);
            this.pnlSpendingCard.Controls.Add(this.lblWeeklyRemaining);
            this.pnlSpendingCard.Controls.Add(this.lblWeeklySpent);
            this.pnlSpendingCard.Controls.Add(this.pnlDailyProgressTrack);
            this.pnlSpendingCard.Controls.Add(this.lblDailyRemaining);
            this.pnlSpendingCard.Controls.Add(this.lblDailySpent);
            this.pnlSpendingCard.Controls.Add(this.pnlSpendingHeader);
            this.pnlSpendingCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSpendingCard.FillColor = System.Drawing.Color.White;
            this.pnlSpendingCard.Location = new System.Drawing.Point(484, 0);
            this.pnlSpendingCard.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.pnlSpendingCard.Name = "pnlSpendingCard";
            this.pnlSpendingCard.Padding = new System.Windows.Forms.Padding(18, 16, 18, 16);
            this.pnlSpendingCard.Size = new System.Drawing.Size(468, 380);
            this.pnlSpendingCard.TabIndex = 1;
            // 
            // lblSpendingStatusBadge
            // 
            this.lblSpendingStatusBadge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSpendingStatusBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.lblSpendingStatusBadge.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSpendingStatusBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblSpendingStatusBadge.Location = new System.Drawing.Point(18, 230);
            this.lblSpendingStatusBadge.Name = "lblSpendingStatusBadge";
            this.lblSpendingStatusBadge.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.lblSpendingStatusBadge.Size = new System.Drawing.Size(432, 38);
            this.lblSpendingStatusBadge.TabIndex = 7;
            this.lblSpendingStatusBadge.Text = "✔ Đang trong giới hạn chi tiêu an toàn";
            this.lblSpendingStatusBadge.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlWeeklyProgressTrack
            // 
            this.pnlWeeklyProgressTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlWeeklyProgressTrack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlWeeklyProgressTrack.BorderColor = System.Drawing.Color.Transparent;
            this.pnlWeeklyProgressTrack.BorderRadius = 4;
            this.pnlWeeklyProgressTrack.BorderSize = 0;
            this.pnlWeeklyProgressTrack.Controls.Add(this.pnlWeeklyProgressBar);
            this.pnlWeeklyProgressTrack.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlWeeklyProgressTrack.Location = new System.Drawing.Point(18, 194);
            this.pnlWeeklyProgressTrack.Name = "pnlWeeklyProgressTrack";
            this.pnlWeeklyProgressTrack.Size = new System.Drawing.Size(432, 10);
            this.pnlWeeklyProgressTrack.TabIndex = 6;
            // 
            // pnlWeeklyProgressBar
            // 
            this.pnlWeeklyProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.pnlWeeklyProgressBar.BorderColor = System.Drawing.Color.Transparent;
            this.pnlWeeklyProgressBar.BorderRadius = 4;
            this.pnlWeeklyProgressBar.BorderSize = 0;
            this.pnlWeeklyProgressBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlWeeklyProgressBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.pnlWeeklyProgressBar.Location = new System.Drawing.Point(0, 0);
            this.pnlWeeklyProgressBar.Name = "pnlWeeklyProgressBar";
            this.pnlWeeklyProgressBar.Size = new System.Drawing.Size(120, 10);
            this.pnlWeeklyProgressBar.TabIndex = 0;
            // 
            // lblWeeklyRemaining
            // 
            this.lblWeeklyRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWeeklyRemaining.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWeeklyRemaining.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblWeeklyRemaining.Location = new System.Drawing.Point(234, 166);
            this.lblWeeklyRemaining.Name = "lblWeeklyRemaining";
            this.lblWeeklyRemaining.Size = new System.Drawing.Size(216, 22);
            this.lblWeeklyRemaining.TabIndex = 5;
            this.lblWeeklyRemaining.Text = "Còn lại: 10,000,000 đ";
            this.lblWeeklyRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblWeeklySpent
            // 
            this.lblWeeklySpent.AutoSize = true;
            this.lblWeeklySpent.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblWeeklySpent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblWeeklySpent.Location = new System.Drawing.Point(18, 168);
            this.lblWeeklySpent.Name = "lblWeeklySpent";
            this.lblWeeklySpent.Size = new System.Drawing.Size(190, 17);
            this.lblWeeklySpent.TabIndex = 4;
            this.lblWeeklySpent.Text = "Đã chi: 0 đ / 10,000,000 đ (Tuần)";
            // 
            // pnlDailyProgressTrack
            // 
            this.pnlDailyProgressTrack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDailyProgressTrack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlDailyProgressTrack.BorderColor = System.Drawing.Color.Transparent;
            this.pnlDailyProgressTrack.BorderRadius = 4;
            this.pnlDailyProgressTrack.BorderSize = 0;
            this.pnlDailyProgressTrack.Controls.Add(this.pnlDailyProgressBar);
            this.pnlDailyProgressTrack.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlDailyProgressTrack.Location = new System.Drawing.Point(18, 126);
            this.pnlDailyProgressTrack.Name = "pnlDailyProgressTrack";
            this.pnlDailyProgressTrack.Size = new System.Drawing.Size(432, 10);
            this.pnlDailyProgressTrack.TabIndex = 3;
            // 
            // pnlDailyProgressBar
            // 
            this.pnlDailyProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.pnlDailyProgressBar.BorderColor = System.Drawing.Color.Transparent;
            this.pnlDailyProgressBar.BorderRadius = 4;
            this.pnlDailyProgressBar.BorderSize = 0;
            this.pnlDailyProgressBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlDailyProgressBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.pnlDailyProgressBar.Location = new System.Drawing.Point(0, 0);
            this.pnlDailyProgressBar.Name = "pnlDailyProgressBar";
            this.pnlDailyProgressBar.Size = new System.Drawing.Size(80, 10);
            this.pnlDailyProgressBar.TabIndex = 0;
            // 
            // lblDailyRemaining
            // 
            this.lblDailyRemaining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDailyRemaining.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDailyRemaining.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDailyRemaining.Location = new System.Drawing.Point(234, 98);
            this.lblDailyRemaining.Name = "lblDailyRemaining";
            this.lblDailyRemaining.Size = new System.Drawing.Size(216, 22);
            this.lblDailyRemaining.TabIndex = 2;
            this.lblDailyRemaining.Text = "Còn lại: 2,000,000 đ";
            this.lblDailyRemaining.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDailySpent
            // 
            this.lblDailySpent.AutoSize = true;
            this.lblDailySpent.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblDailySpent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblDailySpent.Location = new System.Drawing.Point(18, 100);
            this.lblDailySpent.Name = "lblDailySpent";
            this.lblDailySpent.Size = new System.Drawing.Size(182, 17);
            this.lblDailySpent.TabIndex = 1;
            this.lblDailySpent.Text = "Đã chi: 0 đ / 2,000,000 đ (Ngày)";
            // 
            // pnlSpendingHeader
            // 
            this.pnlSpendingHeader.Controls.Add(this.btnOpenLimitDialog);
            this.pnlSpendingHeader.Controls.Add(this.lblSpendingTitle);
            this.pnlSpendingHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSpendingHeader.Location = new System.Drawing.Point(18, 16);
            this.pnlSpendingHeader.Name = "pnlSpendingHeader";
            this.pnlSpendingHeader.Size = new System.Drawing.Size(432, 60);
            this.pnlSpendingHeader.TabIndex = 0;
            // 
            // btnOpenLimitDialog
            // 
            this.btnOpenLimitDialog.BorderRadius = 8;
            this.btnOpenLimitDialog.BorderSize = 1;
            this.btnOpenLimitDialog.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenLimitDialog.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnOpenLimitDialog.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnOpenLimitDialog.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.btnOpenLimitDialog.Location = new System.Drawing.Point(392, 0);
            this.btnOpenLimitDialog.Name = "btnOpenLimitDialog";
            this.btnOpenLimitDialog.NormalBorder = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnOpenLimitDialog.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.btnOpenLimitDialog.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.btnOpenLimitDialog.Size = new System.Drawing.Size(40, 60);
            this.btnOpenLimitDialog.TabIndex = 1;
            this.btnOpenLimitDialog.Text = "⚙";
            this.btnOpenLimitDialog.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            // 
            // lblSpendingTitle
            // 
            this.lblSpendingTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSpendingTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblSpendingTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSpendingTitle.Location = new System.Drawing.Point(0, 0);
            this.lblSpendingTitle.Name = "lblSpendingTitle";
            this.lblSpendingTitle.Size = new System.Drawing.Size(260, 60);
            this.lblSpendingTitle.TabIndex = 0;
            this.lblSpendingTitle.Text = "QUẢN LÝ CHI TIÊU";
            this.lblSpendingTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlDebtCard
            // 
            this.pnlDebtCard.BackColor = System.Drawing.Color.Transparent;
            this.pnlDebtCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlDebtCard.BorderRadius = 14;
            this.pnlDebtCard.BorderSize = 1;
            this.pnlDebtCard.Controls.Add(this.btnPayDebt);
            this.pnlDebtCard.Controls.Add(this.lblDebtStatus);
            this.pnlDebtCard.Controls.Add(this.lblDebtAmount);
            this.pnlDebtCard.Controls.Add(this.lblDebtDueDate);
            this.pnlDebtCard.Controls.Add(this.lblDebtTitle);
            this.pnlDebtCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDebtCard.FillColor = System.Drawing.Color.White;
            this.pnlDebtCard.Location = new System.Drawing.Point(0, 0);
            this.pnlDebtCard.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.pnlDebtCard.Name = "pnlDebtCard";
            this.pnlDebtCard.Padding = new System.Windows.Forms.Padding(18, 16, 18, 16);
            this.pnlDebtCard.Size = new System.Drawing.Size(468, 380);
            this.pnlDebtCard.TabIndex = 0;
            // 
            // btnPayDebt
            // 
            this.btnPayDebt.BorderRadius = 10;
            this.btnPayDebt.BorderSize = 0;
            this.btnPayDebt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPayDebt.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPayDebt.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPayDebt.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnPayDebt.Location = new System.Drawing.Point(18, 322);
            this.btnPayDebt.Name = "btnPayDebt";
            this.btnPayDebt.NormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnPayDebt.PressedColor = System.Drawing.Color.Black;
            this.btnPayDebt.Size = new System.Drawing.Size(432, 42);
            this.btnPayDebt.TabIndex = 4;
            this.btnPayDebt.Text = "Thanh toán ngay";
            this.btnPayDebt.TextColor = System.Drawing.Color.White;
            // 
            // lblDebtStatus
            // 
            this.lblDebtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDebtStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDebtStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(29)))), ((int)(((byte)(72)))));
            this.lblDebtStatus.Location = new System.Drawing.Point(18, 150);
            this.lblDebtStatus.Name = "lblDebtStatus";
            this.lblDebtStatus.Size = new System.Drawing.Size(432, 24);
            this.lblDebtStatus.TabIndex = 3;
            this.lblDebtStatus.Text = "- Trạng thái: Chưa thanh toán";
            // 
            // lblDebtAmount
            // 
            this.lblDebtAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDebtAmount.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDebtAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblDebtAmount.Location = new System.Drawing.Point(18, 114);
            this.lblDebtAmount.Name = "lblDebtAmount";
            this.lblDebtAmount.Size = new System.Drawing.Size(432, 24);
            this.lblDebtAmount.TabIndex = 2;
            this.lblDebtAmount.Text = "- Số tiền: 1,463,000 đ";
            // 
            // lblDebtDueDate
            // 
            this.lblDebtDueDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDebtDueDate.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDebtDueDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblDebtDueDate.Location = new System.Drawing.Point(18, 78);
            this.lblDebtDueDate.Name = "lblDebtDueDate";
            this.lblDebtDueDate.Size = new System.Drawing.Size(432, 24);
            this.lblDebtDueDate.TabIndex = 1;
            this.lblDebtDueDate.Text = "Kỳ hạn tiếp theo: 25/11/2026";
            // 
            // lblDebtTitle
            // 
            this.lblDebtTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDebtTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.lblDebtTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblDebtTitle.Location = new System.Drawing.Point(18, 16);
            this.lblDebtTitle.Name = "lblDebtTitle";
            this.lblDebtTitle.Size = new System.Drawing.Size(432, 48);
            this.lblDebtTitle.TabIndex = 0;
            this.lblDebtTitle.Text = "KỲ HẠN TRẢ NỢ";
            this.lblDebtTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlDarkCard
            // 
            this.pnlDarkCard.BackColor = System.Drawing.Color.Transparent;
            this.pnlDarkCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlDarkCard.BorderRadius = 14;
            this.pnlDarkCard.BorderSize = 1;
            this.pnlDarkCard.Controls.Add(this.pnlNavActions);
            this.pnlDarkCard.Controls.Add(this.pnlSwitchRow);
            this.pnlDarkCard.Controls.Add(this.pnlAccountInfo);
            this.pnlDarkCard.Controls.Add(this.pnlDarkHeader);
            this.pnlDarkCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDarkCard.FillColor = System.Drawing.Color.White;
            this.pnlDarkCard.Location = new System.Drawing.Point(24, 54);
            this.pnlDarkCard.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.pnlDarkCard.Name = "pnlDarkCard";
            this.pnlDarkCard.Padding = new System.Windows.Forms.Padding(18, 14, 18, 14);
            this.pnlDarkCard.Size = new System.Drawing.Size(952, 226);
            this.pnlDarkCard.TabIndex = 1;
            // 
            // pnlNavActions
            // 
            this.pnlNavActions.Controls.Add(this.lblNavDivider);
            this.pnlNavActions.Controls.Add(this.btnNavCards);
            this.pnlNavActions.Controls.Add(this.btnNavHistory);
            this.pnlNavActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNavActions.Location = new System.Drawing.Point(18, 172);
            this.pnlNavActions.Name = "pnlNavActions";
            this.pnlNavActions.Size = new System.Drawing.Size(916, 40);
            this.pnlNavActions.TabIndex = 3;
            // 
            // lblNavDivider
            // 
            this.lblNavDivider.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblNavDivider.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblNavDivider.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblNavDivider.Location = new System.Drawing.Point(448, 6);
            this.lblNavDivider.Name = "lblNavDivider";
            this.lblNavDivider.Size = new System.Drawing.Size(20, 28);
            this.lblNavDivider.TabIndex = 2;
            this.lblNavDivider.Text = "|";
            this.lblNavDivider.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNavCards
            // 
            this.btnNavCards.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnNavCards.BackColor = System.Drawing.Color.Transparent;
            this.btnNavCards.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavCards.FlatAppearance.BorderSize = 0;
            this.btnNavCards.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNavCards.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNavCards.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavCards.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNavCards.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnNavCards.Location = new System.Drawing.Point(490, 4);
            this.btnNavCards.Name = "btnNavCards";
            this.btnNavCards.Size = new System.Drawing.Size(220, 32);
            this.btnNavCards.TabIndex = 1;
            this.btnNavCards.Text = "💳 Tài khoản & Thẻ";
            this.btnNavCards.UseMnemonic = false;
            this.btnNavCards.UseVisualStyleBackColor = false;
            // 
            // btnNavHistory
            // 
            this.btnNavHistory.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnNavHistory.BackColor = System.Drawing.Color.Transparent;
            this.btnNavHistory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNavHistory.FlatAppearance.BorderSize = 0;
            this.btnNavHistory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNavHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNavHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNavHistory.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNavHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnNavHistory.Location = new System.Drawing.Point(206, 4);
            this.btnNavHistory.Name = "btnNavHistory";
            this.btnNavHistory.Size = new System.Drawing.Size(220, 32);
            this.btnNavHistory.TabIndex = 0;
            this.btnNavHistory.Text = "🕒 Lịch sử giao dịch";
            this.btnNavHistory.UseMnemonic = false;
            this.btnNavHistory.UseVisualStyleBackColor = false;
            // 
            // pnlSwitchRow
            // 
            this.pnlSwitchRow.Controls.Add(this.lblSwitchInfo);
            this.pnlSwitchRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSwitchRow.Location = new System.Drawing.Point(18, 126);
            this.pnlSwitchRow.Name = "pnlSwitchRow";
            this.pnlSwitchRow.Size = new System.Drawing.Size(916, 28);
            this.pnlSwitchRow.TabIndex = 2;
            // 
            // lblSwitchInfo
            // 
            this.lblSwitchInfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSwitchInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSwitchInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSwitchInfo.Location = new System.Drawing.Point(0, 0);
            this.lblSwitchInfo.Name = "lblSwitchInfo";
            this.lblSwitchInfo.Size = new System.Drawing.Size(320, 28);
            this.lblSwitchInfo.TabIndex = 0;
            this.lblSwitchInfo.Text = "ⓘ Tài khoản ngân hàng chính";
            this.lblSwitchInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlAccountInfo
            // 
            this.pnlAccountInfo.Controls.Add(this.btnToggleBalance);
            this.pnlAccountInfo.Controls.Add(this.lblBalance);
            this.pnlAccountInfo.Controls.Add(this.lblBalanceTitle);
            this.pnlAccountInfo.Controls.Add(this.btnCopyStk);
            this.pnlAccountInfo.Controls.Add(this.lblAccountNumber);
            this.pnlAccountInfo.Controls.Add(this.lblAccountNumberTitle);
            this.pnlAccountInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAccountInfo.Location = new System.Drawing.Point(18, 64);
            this.pnlAccountInfo.Name = "pnlAccountInfo";
            this.pnlAccountInfo.Size = new System.Drawing.Size(916, 62);
            this.pnlAccountInfo.TabIndex = 1;
            // 
            // btnToggleBalance
            // 
            this.btnToggleBalance.BackColor = System.Drawing.Color.Transparent;
            this.btnToggleBalance.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToggleBalance.FlatAppearance.BorderSize = 0;
            this.btnToggleBalance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleBalance.Font = new System.Drawing.Font("Segoe UI Emoji", 10F);
            this.btnToggleBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnToggleBalance.Location = new System.Drawing.Point(260, 32);
            this.btnToggleBalance.Name = "btnToggleBalance";
            this.btnToggleBalance.Size = new System.Drawing.Size(28, 24);
            this.btnToggleBalance.TabIndex = 5;
            this.btnToggleBalance.Text = "👁";
            this.btnToggleBalance.UseVisualStyleBackColor = false;
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblBalance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblBalance.Location = new System.Drawing.Point(95, 33);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(82, 21);
            this.lblBalance.TabIndex = 4;
            this.lblBalance.Text = "•••••••••";
            // 
            // lblBalanceTitle
            // 
            this.lblBalanceTitle.AutoSize = true;
            this.lblBalanceTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblBalanceTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblBalanceTitle.Location = new System.Drawing.Point(0, 36);
            this.lblBalanceTitle.Name = "lblBalanceTitle";
            this.lblBalanceTitle.Size = new System.Drawing.Size(43, 17);
            this.lblBalanceTitle.TabIndex = 3;
            this.lblBalanceTitle.Text = "Số dư:";
            // 
            // btnCopyStk
            // 
            this.btnCopyStk.BackColor = System.Drawing.Color.Transparent;
            this.btnCopyStk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopyStk.FlatAppearance.BorderSize = 0;
            this.btnCopyStk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyStk.Font = new System.Drawing.Font("Segoe UI Emoji", 9.5F);
            this.btnCopyStk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnCopyStk.Location = new System.Drawing.Point(260, 4);
            this.btnCopyStk.Name = "btnCopyStk";
            this.btnCopyStk.Size = new System.Drawing.Size(26, 24);
            this.btnCopyStk.TabIndex = 2;
            this.btnCopyStk.Text = "📋";
            this.btnCopyStk.UseVisualStyleBackColor = false;
            // 
            // lblAccountNumber
            // 
            this.lblAccountNumber.AutoSize = true;
            this.lblAccountNumber.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAccountNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblAccountNumber.Location = new System.Drawing.Point(95, 6);
            this.lblAccountNumber.Name = "lblAccountNumber";
            this.lblAccountNumber.Size = new System.Drawing.Size(100, 20);
            this.lblAccountNumber.TabIndex = 1;
            this.lblAccountNumber.Text = "1000000001";
            // 
            // lblAccountNumberTitle
            // 
            this.lblAccountNumberTitle.AutoSize = true;
            this.lblAccountNumberTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblAccountNumberTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblAccountNumberTitle.Location = new System.Drawing.Point(0, 8);
            this.lblAccountNumberTitle.Name = "lblAccountNumberTitle";
            this.lblAccountNumberTitle.Size = new System.Drawing.Size(82, 17);
            this.lblAccountNumberTitle.TabIndex = 0;
            this.lblAccountNumberTitle.Text = "Số tài khoản:";
            // 
            // pnlDarkHeader
            // 
            this.pnlDarkHeader.Controls.Add(this.lblUserTier);
            this.pnlDarkHeader.Controls.Add(this.pnlNameBadge);
            this.pnlDarkHeader.Controls.Add(this.pnlAvatar);
            this.pnlDarkHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDarkHeader.Location = new System.Drawing.Point(18, 14);
            this.pnlDarkHeader.Name = "pnlDarkHeader";
            this.pnlDarkHeader.Size = new System.Drawing.Size(916, 50);
            this.pnlDarkHeader.TabIndex = 0;
            // 
            // lblUserTier
            // 
            this.lblUserTier.AutoSize = true;
            this.lblUserTier.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblUserTier.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblUserTier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblUserTier.Location = new System.Drawing.Point(56, 27);
            this.lblUserTier.Name = "lblUserTier";
            this.lblUserTier.Size = new System.Drawing.Size(125, 17);
            this.lblUserTier.TabIndex = 2;
            this.lblUserTier.Text = "Hạng Tiêu Chuẩn >";
            // 
            // pnlNameBadge
            // 
            this.pnlNameBadge.BackColor = System.Drawing.Color.Transparent;
            this.pnlNameBadge.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlNameBadge.BorderRadius = 8;
            this.pnlNameBadge.BorderSize = 1;
            this.pnlNameBadge.Controls.Add(this.lblUserName);
            this.pnlNameBadge.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlNameBadge.Location = new System.Drawing.Point(54, 2);
            this.pnlNameBadge.Name = "pnlNameBadge";
            this.pnlNameBadge.Padding = new System.Windows.Forms.Padding(8, 2, 8, 2);
            this.pnlNameBadge.Size = new System.Drawing.Size(260, 24);
            this.pnlNameBadge.TabIndex = 1;
            // 
            // lblUserName
            // 
            this.lblUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblUserName.Location = new System.Drawing.Point(8, 2);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(244, 20);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "NGƯỜI DÙNG";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlAvatar
            // 
            this.pnlAvatar.BackColor = System.Drawing.Color.Transparent;
            this.pnlAvatar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(210)))), ((int)(((byte)(254)))));
            this.pnlAvatar.BorderRadius = 22;
            this.pnlAvatar.BorderSize = 1;
            this.pnlAvatar.Controls.Add(this.lblAvatar);
            this.pnlAvatar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAvatar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.pnlAvatar.Location = new System.Drawing.Point(0, 0);
            this.pnlAvatar.Name = "pnlAvatar";
            this.pnlAvatar.Size = new System.Drawing.Size(44, 50);
            this.pnlAvatar.TabIndex = 0;
            // 
            // lblAvatar
            // 
            this.lblAvatar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvatar.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblAvatar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblAvatar.Location = new System.Drawing.Point(0, 0);
            this.lblAvatar.Name = "lblAvatar";
            this.lblAvatar.Size = new System.Drawing.Size(44, 50);
            this.lblAvatar.TabIndex = 0;
            this.lblAvatar.Text = "D";
            this.lblAvatar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMainTitle
            // 
            this.lblMainTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMainTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblMainTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblMainTitle.Location = new System.Drawing.Point(24, 18);
            this.lblMainTitle.Name = "lblMainTitle";
            this.lblMainTitle.Size = new System.Drawing.Size(952, 36);
            this.lblMainTitle.TabIndex = 0;
            this.lblMainTitle.Text = "Trang Chủ";
            this.lblMainTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DashboardUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1000, 680);
            this.Controls.Add(this.pnlRoot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DashboardUserForm";
            this.Text = "DashboardUserForm";
            this.pnlRoot.ResumeLayout(false);
            this.tblBottomCards.ResumeLayout(false);
            this.pnlSpendingCard.ResumeLayout(false);
            this.pnlSpendingCard.PerformLayout();
            this.pnlWeeklyProgressTrack.ResumeLayout(false);
            this.pnlDailyProgressTrack.ResumeLayout(false);
            this.pnlSpendingHeader.ResumeLayout(false);
            this.pnlDebtCard.ResumeLayout(false);
            this.pnlDarkCard.ResumeLayout(false);
            this.pnlNavActions.ResumeLayout(false);
            this.pnlSwitchRow.ResumeLayout(false);
            this.pnlAccountInfo.ResumeLayout(false);
            this.pnlAccountInfo.PerformLayout();
            this.pnlDarkHeader.ResumeLayout(false);
            this.pnlDarkHeader.PerformLayout();
            this.pnlNameBadge.ResumeLayout(false);
            this.pnlAvatar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlRoot;
        private System.Windows.Forms.Label lblMainTitle;
        private BankAccountManagement.Forms.RoundedPanel pnlDarkCard;
        private System.Windows.Forms.TableLayoutPanel tblBottomCards;
        private BankAccountManagement.Forms.RoundedPanel pnlDebtCard;
        private BankAccountManagement.Forms.RoundedPanel pnlSpendingCard;
        private System.Windows.Forms.Panel pnlDarkHeader;
        private BankAccountManagement.Forms.RoundedPanel pnlAvatar;
        private System.Windows.Forms.Label lblAvatar;
        private BankAccountManagement.Forms.RoundedPanel pnlNameBadge;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblUserTier;
        private System.Windows.Forms.Panel pnlAccountInfo;
        private System.Windows.Forms.Label lblAccountNumberTitle;
        private System.Windows.Forms.Label lblAccountNumber;
        private System.Windows.Forms.Button btnCopyStk;
        private System.Windows.Forms.Label lblBalanceTitle;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Button btnToggleBalance;
        private System.Windows.Forms.Panel pnlSwitchRow;
        private System.Windows.Forms.Label lblSwitchInfo;
        private System.Windows.Forms.Panel pnlNavActions;
        private System.Windows.Forms.Button btnNavHistory;
        private System.Windows.Forms.Button btnNavCards;
        private System.Windows.Forms.Label lblNavDivider;
        private System.Windows.Forms.Label lblDebtTitle;
        private System.Windows.Forms.Label lblDebtDueDate;
        private System.Windows.Forms.Label lblDebtAmount;
        private System.Windows.Forms.Label lblDebtStatus;
        private BankAccountManagement.Forms.RoundedButton btnPayDebt;
        private System.Windows.Forms.Panel pnlSpendingHeader;
        private System.Windows.Forms.Label lblSpendingTitle;
        private BankAccountManagement.Forms.RoundedButton btnOpenLimitDialog;
        private System.Windows.Forms.Label lblDailySpent;
        private System.Windows.Forms.Label lblDailyRemaining;
        private BankAccountManagement.Forms.RoundedPanel pnlDailyProgressTrack;
        private BankAccountManagement.Forms.RoundedPanel pnlDailyProgressBar;
        private System.Windows.Forms.Label lblWeeklySpent;
        private System.Windows.Forms.Label lblWeeklyRemaining;
        private BankAccountManagement.Forms.RoundedPanel pnlWeeklyProgressTrack;
        private BankAccountManagement.Forms.RoundedPanel pnlWeeklyProgressBar;
        private System.Windows.Forms.Label lblSpendingStatusBadge;
    }
}
