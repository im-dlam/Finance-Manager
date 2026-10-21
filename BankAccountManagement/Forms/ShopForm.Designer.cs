namespace BankAccountManagement.Forms
{
    partial class ShopForm
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
            // ============ HEADER ============
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnCart = new BankAccountManagement.Forms.RoundedButton();

            // ============ PRODUCTS GRID ============
            this.tlpProducts = new System.Windows.Forms.TableLayoutPanel();

            // ---- CARD 1 ----
            this.card1 = new BankAccountManagement.Forms.RoundedPanel();
            this.img1 = new BankAccountManagement.Forms.RoundedPanel();
            this.lblImg1 = new System.Windows.Forms.Label();
            this.lblName1 = new System.Windows.Forms.Label();
            this.lblPrice1 = new System.Windows.Forms.Label();
            this.btnCart1 = new BankAccountManagement.Forms.RoundedButton();
            this.btnBuy1 = new BankAccountManagement.Forms.RoundedButton();

            // ---- CARD 2 ----
            this.card2 = new BankAccountManagement.Forms.RoundedPanel();
            this.img2 = new BankAccountManagement.Forms.RoundedPanel();
            this.lblImg2 = new System.Windows.Forms.Label();
            this.lblName2 = new System.Windows.Forms.Label();
            this.lblPrice2 = new System.Windows.Forms.Label();
            this.btnCart2 = new BankAccountManagement.Forms.RoundedButton();
            this.btnBuy2 = new BankAccountManagement.Forms.RoundedButton();

            // ---- CARD 3 ----
            this.card3 = new BankAccountManagement.Forms.RoundedPanel();
            this.img3 = new BankAccountManagement.Forms.RoundedPanel();
            this.lblImg3 = new System.Windows.Forms.Label();
            this.lblName3 = new System.Windows.Forms.Label();
            this.lblPrice3 = new System.Windows.Forms.Label();
            this.btnCart3 = new BankAccountManagement.Forms.RoundedButton();
            this.btnBuy3 = new BankAccountManagement.Forms.RoundedButton();

            // ---- CARD 4 ----
            this.card4 = new BankAccountManagement.Forms.RoundedPanel();
            this.img4 = new BankAccountManagement.Forms.RoundedPanel();
            this.lblImg4 = new System.Windows.Forms.Label();
            this.lblName4 = new System.Windows.Forms.Label();
            this.lblPrice4 = new System.Windows.Forms.Label();
            this.btnCart4 = new BankAccountManagement.Forms.RoundedButton();
            this.btnBuy4 = new BankAccountManagement.Forms.RoundedButton();

            // ---- CARD 5 ----
            this.card5 = new BankAccountManagement.Forms.RoundedPanel();
            this.img5 = new BankAccountManagement.Forms.RoundedPanel();
            this.lblImg5 = new System.Windows.Forms.Label();
            this.lblName5 = new System.Windows.Forms.Label();
            this.lblPrice5 = new System.Windows.Forms.Label();
            this.btnCart5 = new BankAccountManagement.Forms.RoundedButton();
            this.btnBuy5 = new BankAccountManagement.Forms.RoundedButton();

            // ---- CARD 6 ----
            this.card6 = new BankAccountManagement.Forms.RoundedPanel();
            this.img6 = new BankAccountManagement.Forms.RoundedPanel();
            this.lblImg6 = new System.Windows.Forms.Label();
            this.lblName6 = new System.Windows.Forms.Label();
            this.lblPrice6 = new System.Windows.Forms.Label();
            this.btnCart6 = new BankAccountManagement.Forms.RoundedButton();
            this.btnBuy6 = new BankAccountManagement.Forms.RoundedButton();

            this.pnlHeader.SuspendLayout();
            this.tlpProducts.SuspendLayout();
            this.card1.SuspendLayout();
            this.card2.SuspendLayout();
            this.card3.SuspendLayout();
            this.card4.SuspendLayout();
            this.card5.SuspendLayout();
            this.card6.SuspendLayout();
            this.SuspendLayout();

            // =============================================================
            // pnlHeader
            // =============================================================
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.btnCart);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
            this.pnlHeader.Size = new System.Drawing.Size(1080, 76);
            this.pnlHeader.TabIndex = 0;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblTitle.Location = new System.Drawing.Point(24, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(118, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Shopping";

            // btnCart — "🛒 Giỏ hàng"
            this.btnCart.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnCart.BackColor = System.Drawing.Color.Transparent;
            this.btnCart.BorderRadius = 0;
            this.btnCart.BorderSize = 0;
            this.btnCart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCart.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCart.ForeColor = System.Drawing.Color.White;
            this.btnCart.HoverBorder = System.Drawing.Color.Transparent;
            this.btnCart.HoverColor = System.Drawing.Color.FromArgb(79, 70, 229);
            this.btnCart.Location = new System.Drawing.Point(900, 18);
            this.btnCart.Name = "btnCart";
            this.btnCart.NormalBorder = System.Drawing.Color.Transparent;
            this.btnCart.NormalColor = System.Drawing.Color.FromArgb(99, 91, 255);
            this.btnCart.PressedBorder = System.Drawing.Color.Transparent;
            this.btnCart.PressedColor = System.Drawing.Color.FromArgb(67, 56, 202);
            this.btnCart.Size = new System.Drawing.Size(156, 40);
            this.btnCart.TabIndex = 1;
            this.btnCart.Text = "🛒  Giỏ hàng";
            this.btnCart.TextColor = System.Drawing.Color.White;
            this.btnCart.UseVisualStyleBackColor = false;

            // =============================================================
            // tlpProducts — Grid 3 cột x 2 hàng
            // =============================================================
            this.tlpProducts.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.tlpProducts.ColumnCount = 3;
            this.tlpProducts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this.tlpProducts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this.tlpProducts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333F));
            this.tlpProducts.Controls.Add(this.card1, 0, 0);
            this.tlpProducts.Controls.Add(this.card2, 1, 0);
            this.tlpProducts.Controls.Add(this.card3, 2, 0);
            this.tlpProducts.Controls.Add(this.card4, 0, 1);
            this.tlpProducts.Controls.Add(this.card5, 1, 1);
            this.tlpProducts.Controls.Add(this.card6, 2, 1);
            this.tlpProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpProducts.Location = new System.Drawing.Point(0, 76);
            this.tlpProducts.Name = "tlpProducts";
            this.tlpProducts.Padding = new System.Windows.Forms.Padding(20);
            this.tlpProducts.RowCount = 2;
            this.tlpProducts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProducts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpProducts.Size = new System.Drawing.Size(1080, 644);
            this.tlpProducts.TabIndex = 1;

            // =============================================================
            // CARD 1
            // =============================================================
            this.card1.BackColor = System.Drawing.Color.Transparent;
            this.card1.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.card1.BorderRadius = 14;
            this.card1.BorderSize = 1;
            this.card1.FillColor = System.Drawing.Color.White;
            this.card1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card1.Margin = new System.Windows.Forms.Padding(8);
            this.card1.Controls.Add(this.img1);
            this.card1.Controls.Add(this.lblName1);
            this.card1.Controls.Add(this.lblPrice1);
            this.card1.Controls.Add(this.btnCart1);
            this.card1.Controls.Add(this.btnBuy1);
            this.card1.Name = "card1";

            // img1
            this.img1.BackColor = System.Drawing.Color.Transparent;
            this.img1.BorderColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.img1.BorderRadius = 10;
            this.img1.BorderSize = 1;
            this.img1.FillColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.img1.Controls.Add(this.lblImg1);
            this.img1.Location = new System.Drawing.Point(16, 16);
            this.img1.Name = "img1";
            this.img1.Size = new System.Drawing.Size(280, 150);
            this.img1.TabIndex = 0;

            this.lblImg1.BackColor = System.Drawing.Color.Transparent;
            this.lblImg1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblImg1.Font = new System.Drawing.Font("Segoe UI Emoji", 60F);
            this.lblImg1.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblImg1.Name = "lblImg1";
            this.lblImg1.Size = new System.Drawing.Size(280, 150);
            this.lblImg1.TabIndex = 0;
            this.lblImg1.Text = "📱";
            this.lblImg1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblName1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName1.BackColor = System.Drawing.Color.Transparent;
            this.lblName1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblName1.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblName1.Location = new System.Drawing.Point(16, 178);
            this.lblName1.Name = "lblName1";
            this.lblName1.Size = new System.Drawing.Size(280, 34);
            this.lblName1.TabIndex = 1;
            this.lblName1.Text = "Điện thoại iPhone 13 64GB";

            this.lblPrice1.AutoSize = true;
            this.lblPrice1.BackColor = System.Drawing.Color.Transparent;
            this.lblPrice1.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPrice1.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblPrice1.Location = new System.Drawing.Point(16, 218);
            this.lblPrice1.Name = "lblPrice1";
            this.lblPrice1.Size = new System.Drawing.Size(130, 25);
            this.lblPrice1.TabIndex = 2;
            this.lblPrice1.Text = "10.990.000 đ";

            // btnCart1 — "Thêm giỏ"
            this.btnCart1.BackColor = System.Drawing.Color.Transparent;
            this.btnCart1.BorderRadius = 0;
            this.btnCart1.BorderSize = 1;
            this.btnCart1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCart1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCart1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCart1.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnCart1.HoverBorder = System.Drawing.Color.FromArgb(99, 91, 255);
            this.btnCart1.HoverColor = System.Drawing.Color.FromArgb(238, 242, 255);
            this.btnCart1.Location = new System.Drawing.Point(16, 254);
            this.btnCart1.Name = "btnCart1";
            this.btnCart1.NormalBorder = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnCart1.NormalColor = System.Drawing.Color.White;
            this.btnCart1.PressedBorder = System.Drawing.Color.FromArgb(79, 70, 229);
            this.btnCart1.PressedColor = System.Drawing.Color.FromArgb(224, 231, 255);
            this.btnCart1.Size = new System.Drawing.Size(140, 36);
            this.btnCart1.TabIndex = 3;
            this.btnCart1.Text = "Thêm giỏ";
            this.btnCart1.TextColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnCart1.UseVisualStyleBackColor = false;

            // btnBuy1 — "Mua"
            this.btnBuy1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnBuy1.BackColor = System.Drawing.Color.Transparent;
            this.btnBuy1.BorderRadius = 0;
            this.btnBuy1.BorderSize = 0;
            this.btnBuy1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuy1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuy1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuy1.ForeColor = System.Drawing.Color.White;
            this.btnBuy1.HoverBorder = System.Drawing.Color.Transparent;
            this.btnBuy1.HoverColor = System.Drawing.Color.FromArgb(79, 70, 229);
            this.btnBuy1.Location = new System.Drawing.Point(168, 254);
            this.btnBuy1.Name = "btnBuy1";
            this.btnBuy1.NormalBorder = System.Drawing.Color.Transparent;
            this.btnBuy1.NormalColor = System.Drawing.Color.FromArgb(99, 91, 255);
            this.btnBuy1.PressedBorder = System.Drawing.Color.Transparent;
            this.btnBuy1.PressedColor = System.Drawing.Color.FromArgb(67, 56, 202);
            this.btnBuy1.Size = new System.Drawing.Size(128, 36);
            this.btnBuy1.TabIndex = 4;
            this.btnBuy1.Text = "Mua";
            this.btnBuy1.TextColor = System.Drawing.Color.White;
            this.btnBuy1.UseVisualStyleBackColor = false;

            // =============================================================
            // CARD 2 — Laptop
            // =============================================================
            this.card2.BackColor = System.Drawing.Color.Transparent;
            this.card2.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.card2.BorderRadius = 14;
            this.card2.BorderSize = 1;
            this.card2.FillColor = System.Drawing.Color.White;
            this.card2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card2.Margin = new System.Windows.Forms.Padding(8);
            this.card2.Controls.Add(this.img2);
            this.card2.Controls.Add(this.lblName2);
            this.card2.Controls.Add(this.lblPrice2);
            this.card2.Controls.Add(this.btnCart2);
            this.card2.Controls.Add(this.btnBuy2);
            this.card2.Name = "card2";

            this.img2.BackColor = System.Drawing.Color.Transparent;
            this.img2.BorderColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.img2.BorderRadius = 10;
            this.img2.BorderSize = 1;
            this.img2.FillColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.img2.Controls.Add(this.lblImg2);
            this.img2.Location = new System.Drawing.Point(16, 16);
            this.img2.Name = "img2";
            this.img2.Size = new System.Drawing.Size(280, 150);
            this.img2.TabIndex = 0;

            this.lblImg2.BackColor = System.Drawing.Color.Transparent;
            this.lblImg2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblImg2.Font = new System.Drawing.Font("Segoe UI Emoji", 60F);
            this.lblImg2.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblImg2.Name = "lblImg2";
            this.lblImg2.Size = new System.Drawing.Size(280, 150);
            this.lblImg2.TabIndex = 0;
            this.lblImg2.Text = "💻";
            this.lblImg2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblName2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName2.BackColor = System.Drawing.Color.Transparent;
            this.lblName2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblName2.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblName2.Location = new System.Drawing.Point(16, 178);
            this.lblName2.Name = "lblName2";
            this.lblName2.Size = new System.Drawing.Size(280, 34);
            this.lblName2.TabIndex = 1;
            this.lblName2.Text = "MacBook Air M1 256GB";

            this.lblPrice2.AutoSize = true;
            this.lblPrice2.BackColor = System.Drawing.Color.Transparent;
            this.lblPrice2.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPrice2.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblPrice2.Location = new System.Drawing.Point(16, 218);
            this.lblPrice2.Name = "lblPrice2";
            this.lblPrice2.Size = new System.Drawing.Size(130, 25);
            this.lblPrice2.TabIndex = 2;
            this.lblPrice2.Text = "20.990.000 đ";

            this.btnCart2.BackColor = System.Drawing.Color.Transparent;
            this.btnCart2.BorderRadius = 0;
            this.btnCart2.BorderSize = 1;
            this.btnCart2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCart2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCart2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCart2.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnCart2.HoverBorder = System.Drawing.Color.FromArgb(99, 91, 255);
            this.btnCart2.HoverColor = System.Drawing.Color.FromArgb(238, 242, 255);
            this.btnCart2.Location = new System.Drawing.Point(16, 254);
            this.btnCart2.Name = "btnCart2";
            this.btnCart2.NormalBorder = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnCart2.NormalColor = System.Drawing.Color.White;
            this.btnCart2.PressedBorder = System.Drawing.Color.FromArgb(79, 70, 229);
            this.btnCart2.PressedColor = System.Drawing.Color.FromArgb(224, 231, 255);
            this.btnCart2.Size = new System.Drawing.Size(140, 36);
            this.btnCart2.TabIndex = 3;
            this.btnCart2.Text = "Thêm giỏ";
            this.btnCart2.TextColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnCart2.UseVisualStyleBackColor = false;

            this.btnBuy2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnBuy2.BackColor = System.Drawing.Color.Transparent;
            this.btnBuy2.BorderRadius = 0;
            this.btnBuy2.BorderSize = 0;
            this.btnBuy2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuy2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuy2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuy2.ForeColor = System.Drawing.Color.White;
            this.btnBuy2.HoverBorder = System.Drawing.Color.Transparent;
            this.btnBuy2.HoverColor = System.Drawing.Color.FromArgb(79, 70, 229);
            this.btnBuy2.Location = new System.Drawing.Point(168, 254);
            this.btnBuy2.Name = "btnBuy2";
            this.btnBuy2.NormalBorder = System.Drawing.Color.Transparent;
            this.btnBuy2.NormalColor = System.Drawing.Color.FromArgb(99, 91, 255);
            this.btnBuy2.PressedBorder = System.Drawing.Color.Transparent;
            this.btnBuy2.PressedColor = System.Drawing.Color.FromArgb(67, 56, 202);
            this.btnBuy2.Size = new System.Drawing.Size(128, 36);
            this.btnBuy2.TabIndex = 4;
            this.btnBuy2.Text = "Mua";
            this.btnBuy2.TextColor = System.Drawing.Color.White;
            this.btnBuy2.UseVisualStyleBackColor = false;

            // =============================================================
            // CARD 3 — Tai nghe
            // =============================================================
            this.card3.BackColor = System.Drawing.Color.Transparent;
            this.card3.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.card3.BorderRadius = 14;
            this.card3.BorderSize = 1;
            this.card3.FillColor = System.Drawing.Color.White;
            this.card3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card3.Margin = new System.Windows.Forms.Padding(8);
            this.card3.Controls.Add(this.img3);
            this.card3.Controls.Add(this.lblName3);
            this.card3.Controls.Add(this.lblPrice3);
            this.card3.Controls.Add(this.btnCart3);
            this.card3.Controls.Add(this.btnBuy3);
            this.card3.Name = "card3";

            this.img3.BackColor = System.Drawing.Color.Transparent;
            this.img3.BorderColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.img3.BorderRadius = 10;
            this.img3.BorderSize = 1;
            this.img3.FillColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.img3.Controls.Add(this.lblImg3);
            this.img3.Location = new System.Drawing.Point(16, 16);
            this.img3.Name = "img3";
            this.img3.Size = new System.Drawing.Size(280, 150);
            this.img3.TabIndex = 0;

            this.lblImg3.BackColor = System.Drawing.Color.Transparent;
            this.lblImg3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblImg3.Font = new System.Drawing.Font("Segoe UI Emoji", 60F);
            this.lblImg3.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblImg3.Name = "lblImg3";
            this.lblImg3.Size = new System.Drawing.Size(280, 150);
            this.lblImg3.TabIndex = 0;
            this.lblImg3.Text = "🎧";
            this.lblImg3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblName3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName3.BackColor = System.Drawing.Color.Transparent;
            this.lblName3.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblName3.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblName3.Location = new System.Drawing.Point(16, 178);
            this.lblName3.Name = "lblName3";
            this.lblName3.Size = new System.Drawing.Size(280, 34);
            this.lblName3.TabIndex = 1;
            this.lblName3.Text = "Tai nghe Bluetooth";

            this.lblPrice3.AutoSize = true;
            this.lblPrice3.BackColor = System.Drawing.Color.Transparent;
            this.lblPrice3.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPrice3.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblPrice3.Location = new System.Drawing.Point(16, 218);
            this.lblPrice3.Name = "lblPrice3";
            this.lblPrice3.Size = new System.Drawing.Size(120, 25);
            this.lblPrice3.TabIndex = 2;
            this.lblPrice3.Text = "2.990.000 đ";

            this.btnCart3.BackColor = System.Drawing.Color.Transparent;
            this.btnCart3.BorderRadius = 0;
            this.btnCart3.BorderSize = 1;
            this.btnCart3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCart3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCart3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCart3.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnCart3.HoverBorder = System.Drawing.Color.FromArgb(99, 91, 255);
            this.btnCart3.HoverColor = System.Drawing.Color.FromArgb(238, 242, 255);
            this.btnCart3.Location = new System.Drawing.Point(16, 254);
            this.btnCart3.Name = "btnCart3";
            this.btnCart3.NormalBorder = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnCart3.NormalColor = System.Drawing.Color.White;
            this.btnCart3.PressedBorder = System.Drawing.Color.FromArgb(79, 70, 229);
            this.btnCart3.PressedColor = System.Drawing.Color.FromArgb(224, 231, 255);
            this.btnCart3.Size = new System.Drawing.Size(140, 36);
            this.btnCart3.TabIndex = 3;
            this.btnCart3.Text = "Thêm giỏ";
            this.btnCart3.TextColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnCart3.UseVisualStyleBackColor = false;

            this.btnBuy3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnBuy3.BackColor = System.Drawing.Color.Transparent;
            this.btnBuy3.BorderRadius = 0;
            this.btnBuy3.BorderSize = 0;
            this.btnBuy3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuy3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuy3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuy3.ForeColor = System.Drawing.Color.White;
            this.btnBuy3.HoverBorder = System.Drawing.Color.Transparent;
            this.btnBuy3.HoverColor = System.Drawing.Color.FromArgb(79, 70, 229);
            this.btnBuy3.Location = new System.Drawing.Point(168, 254);
            this.btnBuy3.Name = "btnBuy3";
            this.btnBuy3.NormalBorder = System.Drawing.Color.Transparent;
            this.btnBuy3.NormalColor = System.Drawing.Color.FromArgb(99, 91, 255);
            this.btnBuy3.PressedBorder = System.Drawing.Color.Transparent;
            this.btnBuy3.PressedColor = System.Drawing.Color.FromArgb(67, 56, 202);
            this.btnBuy3.Size = new System.Drawing.Size(128, 36);
            this.btnBuy3.TabIndex = 4;
            this.btnBuy3.Text = "Mua";
            this.btnBuy3.TextColor = System.Drawing.Color.White;
            this.btnBuy3.UseVisualStyleBackColor = false;

            // =============================================================
            // CARD 4 — Đồng hồ
            // =============================================================
            this.card4.BackColor = System.Drawing.Color.Transparent;
            this.card4.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.card4.BorderRadius = 14;
            this.card4.BorderSize = 1;
            this.card4.FillColor = System.Drawing.Color.White;
            this.card4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card4.Margin = new System.Windows.Forms.Padding(8);
            this.card4.Controls.Add(this.img4);
            this.card4.Controls.Add(this.lblName4);
            this.card4.Controls.Add(this.lblPrice4);
            this.card4.Controls.Add(this.btnCart4);
            this.card4.Controls.Add(this.btnBuy4);
            this.card4.Name = "card4";

            this.img4.BackColor = System.Drawing.Color.Transparent;
            this.img4.BorderColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.img4.BorderRadius = 10;
            this.img4.BorderSize = 1;
            this.img4.FillColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.img4.Controls.Add(this.lblImg4);
            this.img4.Location = new System.Drawing.Point(16, 16);
            this.img4.Name = "img4";
            this.img4.Size = new System.Drawing.Size(280, 150);
            this.img4.TabIndex = 0;

            this.lblImg4.BackColor = System.Drawing.Color.Transparent;
            this.lblImg4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblImg4.Font = new System.Drawing.Font("Segoe UI Emoji", 60F);
            this.lblImg4.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblImg4.Name = "lblImg4";
            this.lblImg4.Size = new System.Drawing.Size(280, 150);
            this.lblImg4.TabIndex = 0;
            this.lblImg4.Text = "⌚";
            this.lblImg4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblName4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName4.BackColor = System.Drawing.Color.Transparent;
            this.lblName4.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblName4.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblName4.Location = new System.Drawing.Point(16, 178);
            this.lblName4.Name = "lblName4";
            this.lblName4.Size = new System.Drawing.Size(280, 34);
            this.lblName4.TabIndex = 1;
            this.lblName4.Text = "Apple Watch Series 8";

            this.lblPrice4.AutoSize = true;
            this.lblPrice4.BackColor = System.Drawing.Color.Transparent;
            this.lblPrice4.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPrice4.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblPrice4.Location = new System.Drawing.Point(16, 218);
            this.lblPrice4.Name = "lblPrice4";
            this.lblPrice4.Size = new System.Drawing.Size(130, 25);
            this.lblPrice4.TabIndex = 2;
            this.lblPrice4.Text = "12.990.000 đ";

            this.btnCart4.BackColor = System.Drawing.Color.Transparent;
            this.btnCart4.BorderRadius = 0;
            this.btnCart4.BorderSize = 1;
            this.btnCart4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCart4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCart4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCart4.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnCart4.HoverBorder = System.Drawing.Color.FromArgb(99, 91, 255);
            this.btnCart4.HoverColor = System.Drawing.Color.FromArgb(238, 242, 255);
            this.btnCart4.Location = new System.Drawing.Point(16, 254);
            this.btnCart4.Name = "btnCart4";
            this.btnCart4.NormalBorder = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnCart4.NormalColor = System.Drawing.Color.White;
            this.btnCart4.PressedBorder = System.Drawing.Color.FromArgb(79, 70, 229);
            this.btnCart4.PressedColor = System.Drawing.Color.FromArgb(224, 231, 255);
            this.btnCart4.Size = new System.Drawing.Size(140, 36);
            this.btnCart4.TabIndex = 3;
            this.btnCart4.Text = "Thêm giỏ";
            this.btnCart4.TextColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnCart4.UseVisualStyleBackColor = false;

            this.btnBuy4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnBuy4.BackColor = System.Drawing.Color.Transparent;
            this.btnBuy4.BorderRadius = 0;
            this.btnBuy4.BorderSize = 0;
            this.btnBuy4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuy4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuy4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuy4.ForeColor = System.Drawing.Color.White;
            this.btnBuy4.HoverBorder = System.Drawing.Color.Transparent;
            this.btnBuy4.HoverColor = System.Drawing.Color.FromArgb(79, 70, 229);
            this.btnBuy4.Location = new System.Drawing.Point(168, 254);
            this.btnBuy4.Name = "btnBuy4";
            this.btnBuy4.NormalBorder = System.Drawing.Color.Transparent;
            this.btnBuy4.NormalColor = System.Drawing.Color.FromArgb(99, 91, 255);
            this.btnBuy4.PressedBorder = System.Drawing.Color.Transparent;
            this.btnBuy4.PressedColor = System.Drawing.Color.FromArgb(67, 56, 202);
            this.btnBuy4.Size = new System.Drawing.Size(128, 36);
            this.btnBuy4.TabIndex = 4;
            this.btnBuy4.Text = "Mua";
            this.btnBuy4.TextColor = System.Drawing.Color.White;
            this.btnBuy4.UseVisualStyleBackColor = false;

            // =============================================================
            // CARD 5 — AirPods
            // =============================================================
            this.card5.BackColor = System.Drawing.Color.Transparent;
            this.card5.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.card5.BorderRadius = 14;
            this.card5.BorderSize = 1;
            this.card5.FillColor = System.Drawing.Color.White;
            this.card5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card5.Margin = new System.Windows.Forms.Padding(8);
            this.card5.Controls.Add(this.img5);
            this.card5.Controls.Add(this.lblName5);
            this.card5.Controls.Add(this.lblPrice5);
            this.card5.Controls.Add(this.btnCart5);
            this.card5.Controls.Add(this.btnBuy5);
            this.card5.Name = "card5";

            this.img5.BackColor = System.Drawing.Color.Transparent;
            this.img5.BorderColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.img5.BorderRadius = 10;
            this.img5.BorderSize = 1;
            this.img5.FillColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.img5.Controls.Add(this.lblImg5);
            this.img5.Location = new System.Drawing.Point(16, 16);
            this.img5.Name = "img5";
            this.img5.Size = new System.Drawing.Size(280, 150);
            this.img5.TabIndex = 0;

            this.lblImg5.BackColor = System.Drawing.Color.Transparent;
            this.lblImg5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblImg5.Font = new System.Drawing.Font("Segoe UI Emoji", 60F);
            this.lblImg5.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblImg5.Name = "lblImg5";
            this.lblImg5.Size = new System.Drawing.Size(280, 150);
            this.lblImg5.TabIndex = 0;
            this.lblImg5.Text = "🎵";
            this.lblImg5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblName5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName5.BackColor = System.Drawing.Color.Transparent;
            this.lblName5.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblName5.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblName5.Location = new System.Drawing.Point(16, 178);
            this.lblName5.Name = "lblName5";
            this.lblName5.Size = new System.Drawing.Size(280, 34);
            this.lblName5.TabIndex = 1;
            this.lblName5.Text = "AirPods Pro 2";

            this.lblPrice5.AutoSize = true;
            this.lblPrice5.BackColor = System.Drawing.Color.Transparent;
            this.lblPrice5.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPrice5.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblPrice5.Location = new System.Drawing.Point(16, 218);
            this.lblPrice5.Name = "lblPrice5";
            this.lblPrice5.Size = new System.Drawing.Size(120, 25);
            this.lblPrice5.TabIndex = 2;
            this.lblPrice5.Text = "5.990.000 đ";

            this.btnCart5.BackColor = System.Drawing.Color.Transparent;
            this.btnCart5.BorderRadius = 0;
            this.btnCart5.BorderSize = 1;
            this.btnCart5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCart5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCart5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCart5.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnCart5.HoverBorder = System.Drawing.Color.FromArgb(99, 91, 255);
            this.btnCart5.HoverColor = System.Drawing.Color.FromArgb(238, 242, 255);
            this.btnCart5.Location = new System.Drawing.Point(16, 254);
            this.btnCart5.Name = "btnCart5";
            this.btnCart5.NormalBorder = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnCart5.NormalColor = System.Drawing.Color.White;
            this.btnCart5.PressedBorder = System.Drawing.Color.FromArgb(79, 70, 229);
            this.btnCart5.PressedColor = System.Drawing.Color.FromArgb(224, 231, 255);
            this.btnCart5.Size = new System.Drawing.Size(140, 36);
            this.btnCart5.TabIndex = 3;
            this.btnCart5.Text = "Thêm giỏ";
            this.btnCart5.TextColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnCart5.UseVisualStyleBackColor = false;

            this.btnBuy5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnBuy5.BackColor = System.Drawing.Color.Transparent;
            this.btnBuy5.BorderRadius = 0;
            this.btnBuy5.BorderSize = 0;
            this.btnBuy5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuy5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuy5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuy5.ForeColor = System.Drawing.Color.White;
            this.btnBuy5.HoverBorder = System.Drawing.Color.Transparent;
            this.btnBuy5.HoverColor = System.Drawing.Color.FromArgb(79, 70, 229);
            this.btnBuy5.Location = new System.Drawing.Point(168, 254);
            this.btnBuy5.Name = "btnBuy5";
            this.btnBuy5.NormalBorder = System.Drawing.Color.Transparent;
            this.btnBuy5.NormalColor = System.Drawing.Color.FromArgb(99, 91, 255);
            this.btnBuy5.PressedBorder = System.Drawing.Color.Transparent;
            this.btnBuy5.PressedColor = System.Drawing.Color.FromArgb(67, 56, 202);
            this.btnBuy5.Size = new System.Drawing.Size(128, 36);
            this.btnBuy5.TabIndex = 4;
            this.btnBuy5.Text = "Mua";
            this.btnBuy5.TextColor = System.Drawing.Color.White;
            this.btnBuy5.UseVisualStyleBackColor = false;

            // =============================================================
            // CARD 6 — Điện thoại khác
            // =============================================================
            this.card6.BackColor = System.Drawing.Color.Transparent;
            this.card6.BorderColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.card6.BorderRadius = 14;
            this.card6.BorderSize = 1;
            this.card6.FillColor = System.Drawing.Color.White;
            this.card6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.card6.Margin = new System.Windows.Forms.Padding(8);
            this.card6.Controls.Add(this.img6);
            this.card6.Controls.Add(this.lblName6);
            this.card6.Controls.Add(this.lblPrice6);
            this.card6.Controls.Add(this.btnCart6);
            this.card6.Controls.Add(this.btnBuy6);
            this.card6.Name = "card6";

            this.img6.BackColor = System.Drawing.Color.Transparent;
            this.img6.BorderColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.img6.BorderRadius = 10;
            this.img6.BorderSize = 1;
            this.img6.FillColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.img6.Controls.Add(this.lblImg6);
            this.img6.Location = new System.Drawing.Point(16, 16);
            this.img6.Name = "img6";
            this.img6.Size = new System.Drawing.Size(280, 150);
            this.img6.TabIndex = 0;

            this.lblImg6.BackColor = System.Drawing.Color.Transparent;
            this.lblImg6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblImg6.Font = new System.Drawing.Font("Segoe UI Emoji", 60F);
            this.lblImg6.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblImg6.Name = "lblImg6";
            this.lblImg6.Size = new System.Drawing.Size(280, 150);
            this.lblImg6.TabIndex = 0;
            this.lblImg6.Text = "📱";
            this.lblImg6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblName6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName6.BackColor = System.Drawing.Color.Transparent;
            this.lblName6.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblName6.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblName6.Location = new System.Drawing.Point(16, 178);
            this.lblName6.Name = "lblName6";
            this.lblName6.Size = new System.Drawing.Size(280, 34);
            this.lblName6.TabIndex = 1;
            this.lblName6.Text = "Samsung Galaxy S23 Ultra";

            this.lblPrice6.AutoSize = true;
            this.lblPrice6.BackColor = System.Drawing.Color.Transparent;
            this.lblPrice6.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblPrice6.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.lblPrice6.Location = new System.Drawing.Point(16, 218);
            this.lblPrice6.Name = "lblPrice6";
            this.lblPrice6.Size = new System.Drawing.Size(130, 25);
            this.lblPrice6.TabIndex = 2;
            this.lblPrice6.Text = "23.990.000 đ";

            this.btnCart6.BackColor = System.Drawing.Color.Transparent;
            this.btnCart6.BorderRadius = 0;
            this.btnCart6.BorderSize = 1;
            this.btnCart6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCart6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCart6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCart6.ForeColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnCart6.HoverBorder = System.Drawing.Color.FromArgb(99, 91, 255);
            this.btnCart6.HoverColor = System.Drawing.Color.FromArgb(238, 242, 255);
            this.btnCart6.Location = new System.Drawing.Point(16, 254);
            this.btnCart6.Name = "btnCart6";
            this.btnCart6.NormalBorder = System.Drawing.Color.FromArgb(203, 213, 225);
            this.btnCart6.NormalColor = System.Drawing.Color.White;
            this.btnCart6.PressedBorder = System.Drawing.Color.FromArgb(79, 70, 229);
            this.btnCart6.PressedColor = System.Drawing.Color.FromArgb(224, 231, 255);
            this.btnCart6.Size = new System.Drawing.Size(140, 36);
            this.btnCart6.TabIndex = 3;
            this.btnCart6.Text = "Thêm giỏ";
            this.btnCart6.TextColor = System.Drawing.Color.FromArgb(30, 41, 59);
            this.btnCart6.UseVisualStyleBackColor = false;

            this.btnBuy6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnBuy6.BackColor = System.Drawing.Color.Transparent;
            this.btnBuy6.BorderRadius = 0;
            this.btnBuy6.BorderSize = 0;
            this.btnBuy6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuy6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuy6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuy6.ForeColor = System.Drawing.Color.White;
            this.btnBuy6.HoverBorder = System.Drawing.Color.Transparent;
            this.btnBuy6.HoverColor = System.Drawing.Color.FromArgb(79, 70, 229);
            this.btnBuy6.Location = new System.Drawing.Point(168, 254);
            this.btnBuy6.Name = "btnBuy6";
            this.btnBuy6.NormalBorder = System.Drawing.Color.Transparent;
            this.btnBuy6.NormalColor = System.Drawing.Color.FromArgb(99, 91, 255);
            this.btnBuy6.PressedBorder = System.Drawing.Color.Transparent;
            this.btnBuy6.PressedColor = System.Drawing.Color.FromArgb(67, 56, 202);
            this.btnBuy6.Size = new System.Drawing.Size(128, 36);
            this.btnBuy6.TabIndex = 4;
            this.btnBuy6.Text = "Mua";
            this.btnBuy6.TextColor = System.Drawing.Color.White;
            this.btnBuy6.UseVisualStyleBackColor = false;

            // =============================================================
            // ShoppingForm
            // =============================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.ClientSize = new System.Drawing.Size(1080, 720);
            this.Controls.Add(this.tlpProducts);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(900, 640);
            this.Name = "ShoppingForm";
            this.Text = "Shopping";

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tlpProducts.ResumeLayout(false);
            this.card1.ResumeLayout(false);
            this.card1.PerformLayout();
            this.card2.ResumeLayout(false);
            this.card2.PerformLayout();
            this.card3.ResumeLayout(false);
            this.card3.PerformLayout();
            this.card4.ResumeLayout(false);
            this.card4.PerformLayout();
            this.card5.ResumeLayout(false);
            this.card5.PerformLayout();
            this.card6.ResumeLayout(false);
            this.card6.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        // ---------- Fields ----------
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private BankAccountManagement.Forms.RoundedButton btnCart;

        private System.Windows.Forms.TableLayoutPanel tlpProducts;

        // Card 1
        private BankAccountManagement.Forms.RoundedPanel card1;
        private BankAccountManagement.Forms.RoundedPanel img1;
        private System.Windows.Forms.Label lblImg1;
        private System.Windows.Forms.Label lblName1;
        private System.Windows.Forms.Label lblPrice1;
        private BankAccountManagement.Forms.RoundedButton btnCart1;
        private BankAccountManagement.Forms.RoundedButton btnBuy1;

        // Card 2
        private BankAccountManagement.Forms.RoundedPanel card2;
        private BankAccountManagement.Forms.RoundedPanel img2;
        private System.Windows.Forms.Label lblImg2;
        private System.Windows.Forms.Label lblName2;
        private System.Windows.Forms.Label lblPrice2;
        private BankAccountManagement.Forms.RoundedButton btnCart2;
        private BankAccountManagement.Forms.RoundedButton btnBuy2;

        // Card 3
        private BankAccountManagement.Forms.RoundedPanel card3;
        private BankAccountManagement.Forms.RoundedPanel img3;
        private System.Windows.Forms.Label lblImg3;
        private System.Windows.Forms.Label lblName3;
        private System.Windows.Forms.Label lblPrice3;
        private BankAccountManagement.Forms.RoundedButton btnCart3;
        private BankAccountManagement.Forms.RoundedButton btnBuy3;

        // Card 4
        private BankAccountManagement.Forms.RoundedPanel card4;
        private BankAccountManagement.Forms.RoundedPanel img4;
        private System.Windows.Forms.Label lblImg4;
        private System.Windows.Forms.Label lblName4;
        private System.Windows.Forms.Label lblPrice4;
        private BankAccountManagement.Forms.RoundedButton btnCart4;
        private BankAccountManagement.Forms.RoundedButton btnBuy4;

        // Card 5
        private BankAccountManagement.Forms.RoundedPanel card5;
        private BankAccountManagement.Forms.RoundedPanel img5;
        private System.Windows.Forms.Label lblImg5;
        private System.Windows.Forms.Label lblName5;
        private System.Windows.Forms.Label lblPrice5;
        private BankAccountManagement.Forms.RoundedButton btnCart5;
        private BankAccountManagement.Forms.RoundedButton btnBuy5;

        // Card 6
        private BankAccountManagement.Forms.RoundedPanel card6;
        private BankAccountManagement.Forms.RoundedPanel img6;
        private System.Windows.Forms.Label lblImg6;
        private System.Windows.Forms.Label lblName6;
        private System.Windows.Forms.Label lblPrice6;
        private BankAccountManagement.Forms.RoundedButton btnCart6;
        private BankAccountManagement.Forms.RoundedButton btnBuy6;
    }
}