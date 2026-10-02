namespace BankAccountManagement.Forms
{
    partial class BankForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle rowStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle altRowStyle = new System.Windows.Forms.DataGridViewCellStyle();

            // ===== HEADER =====
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlHeaderBorder = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();

            // ===== LEFT CARD =====
            this.pnlFormCard = new System.Windows.Forms.Panel();          // có AutoScroll
            this.pnlFormContent = new System.Windows.Forms.Panel();       // panel con cố định chiều cao
            this.lblFormAccent = new System.Windows.Forms.Panel();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.pnlFormDivider1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.pnlFormDivider2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();   // Thêm ngân hàng
            this.button4 = new System.Windows.Forms.Button();   // Sửa ngân hàng
            this.button3 = new System.Windows.Forms.Button();   // Xóa ngân hàng

            // ===== RIGHT CARD =====
            this.pnlListCard = new System.Windows.Forms.Panel();
            this.lblListAccent = new System.Windows.Forms.Panel();
            this.lblListTitle = new System.Windows.Forms.Label();
            this.lblListCount = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();   // Làm mới (đặt trên header danh sách)
            this.pnlListDivider = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MNH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TNH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SC = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.pnlHeader.SuspendLayout();
            this.pnlFormCard.SuspendLayout();
            this.pnlFormContent.SuspendLayout();
            this.pnlListCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // ============================================================
            // HEADER (trắng)
            // ============================================================
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.pnlHeaderBorder);
            this.pnlHeader.Controls.Add(this.label2);
            this.pnlHeader.Controls.Add(this.label3);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1323, 90);
            this.pnlHeader.TabIndex = 0;

            this.pnlHeaderBorder.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlHeaderBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlHeaderBorder.Location = new System.Drawing.Point(0, 89);
            this.pnlHeaderBorder.Name = "pnlHeaderBorder";
            this.pnlHeaderBorder.Size = new System.Drawing.Size(1323, 1);
            this.pnlHeaderBorder.TabIndex = 99;

            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.label2.Location = new System.Drawing.Point(35, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(280, 41);
            this.label2.TabIndex = 1;
            this.label2.Text = "QUẢN LÝ NGÂN HÀNG";

            this.label3.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.label3.Location = new System.Drawing.Point(38, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 4);
            this.label3.TabIndex = 2;

            // ============================================================
            // LEFT CARD — có scroll riêng
            // ============================================================
            this.pnlFormCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlFormCard.AutoScroll = true;                                // ⬅ scroll khi cần
            this.pnlFormCard.BackColor = System.Drawing.Color.White;
            this.pnlFormCard.Controls.Add(this.pnlFormContent);
            this.pnlFormCard.Location = new System.Drawing.Point(25, 115);
            this.pnlFormCard.Name = "pnlFormCard";
            this.pnlFormCard.Size = new System.Drawing.Size(440, 640);
            this.pnlFormCard.TabIndex = 1;

            // ============================================================
            // pnlFormContent — chiều cao cố định 500, chứa toàn bộ control
            // ============================================================
            this.pnlFormContent.BackColor = System.Drawing.Color.White;
            this.pnlFormContent.Location = new System.Drawing.Point(0, 0);
            this.pnlFormContent.Name = "pnlFormContent";
            this.pnlFormContent.Size = new System.Drawing.Size(415, 500);       // ⬅ chiều cao cố định
            this.pnlFormContent.TabIndex = 0;

            // --- Accent bar ---
            this.lblFormAccent.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.lblFormAccent.Location = new System.Drawing.Point(30, 22);
            this.lblFormAccent.Name = "lblFormAccent";
            this.lblFormAccent.Size = new System.Drawing.Size(5, 24);
            this.lblFormAccent.TabIndex = 0;

            // --- Section title ---
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblFormTitle.Location = new System.Drawing.Point(42, 20);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(220, 30);
            this.lblFormTitle.TabIndex = 1;
            this.lblFormTitle.Text = "Thông tin ngân hàng";

            // --- Divider ---
            this.pnlFormDivider1.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlFormDivider1.Location = new System.Drawing.Point(30, 60);
            this.pnlFormDivider1.Name = "pnlFormDivider1";
            this.pnlFormDivider1.Size = new System.Drawing.Size(355, 1);
            this.pnlFormDivider1.TabIndex = 2;

            // --- Mã ngân hàng ---
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.label4.Location = new System.Drawing.Point(30, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "Mã ngân hàng (*)";

            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.textBox1.Location = new System.Drawing.Point(30, 104);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(355, 31);
            this.textBox1.TabIndex = 4;

            // --- Tên ngân hàng ---
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.label6.Location = new System.Drawing.Point(30, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 21);
            this.label6.TabIndex = 5;
            this.label6.Text = "Tên ngân hàng (*)";

            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.textBox3.Location = new System.Drawing.Point(30, 170);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(355, 31);
            this.textBox3.TabIndex = 6;

            // --- Chi nhánh ---
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.label5.Location = new System.Drawing.Point(30, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 21);
            this.label5.TabIndex = 7;
            this.label5.Text = "Chi nhánh";

            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.textBox4.Location = new System.Drawing.Point(30, 236);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(355, 31);
            this.textBox4.TabIndex = 8;

            // --- Swift code ---
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.label7.Location = new System.Drawing.Point(30, 280);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 21);
            this.label7.TabIndex = 9;
            this.label7.Text = "Swift code";

            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.textBox2.Location = new System.Drawing.Point(30, 302);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(355, 31);
            this.textBox2.TabIndex = 10;

            // --- Divider 2 ---
            this.pnlFormDivider2.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlFormDivider2.Location = new System.Drawing.Point(30, 360);
            this.pnlFormDivider2.Name = "pnlFormDivider2";
            this.pnlFormDivider2.Size = new System.Drawing.Size(355, 1);
            this.pnlFormDivider2.TabIndex = 11;

            // ================== 3 NÚT TRONG FORM ==================
            // ---- button1 = Thêm ngân hàng (Primary xanh, full width) ----
            this.button1.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(29, 78, 216);
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(30, 380);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(355, 44);
            this.button1.TabIndex = 12;
            this.button1.Text = "+  Thêm ngân hàng";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);

            // ---- button4 = Sửa ngân hàng (Outline xanh) ----
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(239, 246, 255);
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button4.ForeColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.button4.Location = new System.Drawing.Point(30, 434);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(172, 42);
            this.button4.TabIndex = 13;
            this.button4.Text = "Sửa ngân hàng";
            this.button4.UseVisualStyleBackColor = false;

            // ---- button3 = Xóa ngân hàng (Outline đỏ) ----
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(254, 242, 242);
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.FromArgb(220, 38, 38);
            this.button3.Location = new System.Drawing.Point(213, 434);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(172, 42);
            this.button3.TabIndex = 14;
            this.button3.Text = "Xóa ngân hàng";
            this.button3.UseVisualStyleBackColor = false;

            // Add tất cả control vào pnlFormContent
            this.pnlFormContent.Controls.Add(this.lblFormAccent);
            this.pnlFormContent.Controls.Add(this.lblFormTitle);
            this.pnlFormContent.Controls.Add(this.pnlFormDivider1);
            this.pnlFormContent.Controls.Add(this.label4);
            this.pnlFormContent.Controls.Add(this.textBox1);
            this.pnlFormContent.Controls.Add(this.label6);
            this.pnlFormContent.Controls.Add(this.textBox3);
            this.pnlFormContent.Controls.Add(this.label5);
            this.pnlFormContent.Controls.Add(this.textBox4);
            this.pnlFormContent.Controls.Add(this.label7);
            this.pnlFormContent.Controls.Add(this.textBox2);
            this.pnlFormContent.Controls.Add(this.pnlFormDivider2);
            this.pnlFormContent.Controls.Add(this.button1);
            this.pnlFormContent.Controls.Add(this.button4);
            this.pnlFormContent.Controls.Add(this.button3);

            // ============================================================
            // RIGHT CARD — chỉ có danh sách + nút Làm mới
            // ============================================================
            this.pnlListCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlListCard.BackColor = System.Drawing.Color.White;
            this.pnlListCard.Controls.Add(this.lblListAccent);
            this.pnlListCard.Controls.Add(this.lblListTitle);
            this.pnlListCard.Controls.Add(this.lblListCount);
            this.pnlListCard.Controls.Add(this.button2);
            this.pnlListCard.Controls.Add(this.pnlListDivider);
            this.pnlListCard.Controls.Add(this.dataGridView1);
            this.pnlListCard.Location = new System.Drawing.Point(490, 115);
            this.pnlListCard.Name = "pnlListCard";
            this.pnlListCard.Size = new System.Drawing.Size(808, 640);
            this.pnlListCard.TabIndex = 2;

            // Accent bar
            this.lblListAccent.BackColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.lblListAccent.Location = new System.Drawing.Point(30, 22);
            this.lblListAccent.Name = "lblListAccent";
            this.lblListAccent.Size = new System.Drawing.Size(5, 24);
            this.lblListAccent.TabIndex = 0;

            // Section title
            this.lblListTitle.AutoSize = true;
            this.lblListTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblListTitle.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.lblListTitle.Location = new System.Drawing.Point(42, 20);
            this.lblListTitle.Name = "lblListTitle";
            this.lblListTitle.Size = new System.Drawing.Size(220, 30);
            this.lblListTitle.TabIndex = 1;
            this.lblListTitle.Text = "Danh sách ngân hàng";

            // Count badge (Anchor Top|Right)
            this.lblListCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListCount.AutoSize = true;
            this.lblListCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblListCount.ForeColor = System.Drawing.Color.FromArgb(22, 163, 74);
            this.lblListCount.Location = new System.Drawing.Point(540, 28);
            this.lblListCount.Name = "lblListCount";
            this.lblListCount.Size = new System.Drawing.Size(90, 23);
            this.lblListCount.TabIndex = 2;
            this.lblListCount.Text = "0 ngân hàng";
            this.lblListCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // ---- button2 = Làm mới (Xám nhạt, trên header danh sách) ----
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(203, 213, 225);
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            this.button2.Location = new System.Drawing.Point(660, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 34);
            this.button2.TabIndex = 3;
            this.button2.Text = "Làm mới";
            this.button2.UseVisualStyleBackColor = false;

            // Divider
            this.pnlListDivider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlListDivider.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.pnlListDivider.Location = new System.Drawing.Point(30, 70);
            this.pnlListDivider.Name = "pnlListDivider";
            this.pnlListDivider.Size = new System.Drawing.Size(750, 1);
            this.pnlListDivider.TabIndex = 4;

            // ============================================================
            // DATAGRIDVIEW
            // ============================================================
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;

            headerStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            headerStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            headerStyle.ForeColor = System.Drawing.Color.White;
            headerStyle.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            headerStyle.SelectionBackColor = System.Drawing.Color.FromArgb(51, 65, 85);
            headerStyle.SelectionForeColor = System.Drawing.Color.White;
            headerStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dataGridView1.ColumnHeadersHeight = 46;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.STT, this.MNH, this.TNH, this.CN, this.SC});

            rowStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            rowStyle.BackColor = System.Drawing.Color.White;
            rowStyle.Font = new System.Drawing.Font("Segoe UI", 10F);
            rowStyle.ForeColor = System.Drawing.Color.FromArgb(51, 65, 85);
            rowStyle.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            rowStyle.SelectionBackColor = System.Drawing.Color.FromArgb(219, 234, 254);
            rowStyle.SelectionForeColor = System.Drawing.Color.FromArgb(30, 58, 138);
            this.dataGridView1.DefaultCellStyle = rowStyle;

            altRowStyle.BackColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.dataGridView1.AlternatingRowsDefaultCellStyle = altRowStyle;

            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.dataGridView1.Location = new System.Drawing.Point(30, 90);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 40;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(750, 520);
            this.dataGridView1.TabIndex = 5;

            // Columns
            this.STT.HeaderText = "STT";
            this.STT.MinimumWidth = 6;
            this.STT.Name = "STT";
            this.STT.FillWeight = 50F;

            this.MNH.HeaderText = "Mã ngân hàng";
            this.MNH.MinimumWidth = 6;
            this.MNH.Name = "MNH";
            this.MNH.FillWeight = 120F;

            this.TNH.HeaderText = "Tên ngân hàng";
            this.TNH.MinimumWidth = 6;
            this.TNH.Name = "TNH";
            this.TNH.FillWeight = 160F;

            this.CN.HeaderText = "Chi nhánh";
            this.CN.MinimumWidth = 6;
            this.CN.Name = "CN";
            this.CN.FillWeight = 130F;

            this.SC.HeaderText = "Swift Code";
            this.SC.MinimumWidth = 6;
            this.SC.Name = "SC";
            this.SC.FillWeight = 110F;

            // ============================================================
            // FORM
            // ============================================================
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.ClientSize = new System.Drawing.Size(1323, 780);
            this.Controls.Add(this.pnlListCard);
            this.Controls.Add(this.pnlFormCard);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "BankForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BankForm";

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFormCard.ResumeLayout(false);
            this.pnlFormContent.ResumeLayout(false);
            this.pnlFormContent.PerformLayout();
            this.pnlListCard.ResumeLayout(false);
            this.pnlListCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlHeaderBorder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.Panel pnlFormCard;
        private System.Windows.Forms.Panel pnlFormContent;
        private System.Windows.Forms.Panel lblFormAccent;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Panel pnlFormDivider1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Panel pnlFormDivider2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;

        private System.Windows.Forms.Panel pnlListCard;
        private System.Windows.Forms.Panel lblListAccent;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.Label lblListCount;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel pnlListDivider;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MNH;
        private System.Windows.Forms.DataGridViewTextBoxColumn TNH;
        private System.Windows.Forms.DataGridViewTextBoxColumn CN;
        private System.Windows.Forms.DataGridViewTextBoxColumn SC;
    }
}