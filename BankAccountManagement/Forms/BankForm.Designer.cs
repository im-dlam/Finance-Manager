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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlHeaderBorder = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlFormCard = new System.Windows.Forms.Panel();
            this.pnlFormContent = new System.Windows.Forms.Panel();
            this.lblFormAccent = new System.Windows.Forms.Panel();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.pnlFormDivider1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBankCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBranch = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSwiftCode = new System.Windows.Forms.TextBox();
            this.pnlFormDivider2 = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pnlListCard = new System.Windows.Forms.Panel();
            this.lblListAccent = new System.Windows.Forms.Panel();
            this.lblListTitle = new System.Windows.Forms.Label();
            this.lblListCount = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.pnlListDivider = new System.Windows.Forms.Panel();
            this.dgvBank = new System.Windows.Forms.DataGridView();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MNH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TNH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlHeader.SuspendLayout();
            this.pnlFormCard.SuspendLayout();
            this.pnlFormContent.SuspendLayout();
            this.pnlListCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBank)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.pnlHeaderBorder);
            this.pnlHeader.Controls.Add(this.label2);
            this.pnlHeader.Controls.Add(this.label3);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1323, 90);
            this.pnlHeader.TabIndex = 0;
            // 
            // pnlHeaderBorder
            // 
            this.pnlHeaderBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlHeaderBorder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlHeaderBorder.Location = new System.Drawing.Point(0, 89);
            this.pnlHeaderBorder.Name = "pnlHeaderBorder";
            this.pnlHeaderBorder.Size = new System.Drawing.Size(1323, 1);
            this.pnlHeaderBorder.TabIndex = 99;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.label2.Location = new System.Drawing.Point(35, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(343, 41);
            this.label2.TabIndex = 1;
            this.label2.Text = "QUẢN LÝ NGÂN HÀNG";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.label3.Location = new System.Drawing.Point(38, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 4);
            this.label3.TabIndex = 2;
            // 
            // pnlFormCard
            // 
            this.pnlFormCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlFormCard.AutoScroll = true;
            this.pnlFormCard.BackColor = System.Drawing.Color.White;
            this.pnlFormCard.Controls.Add(this.pnlFormContent);
            this.pnlFormCard.Location = new System.Drawing.Point(25, 115);
            this.pnlFormCard.Name = "pnlFormCard";
            this.pnlFormCard.Size = new System.Drawing.Size(440, 640);
            this.pnlFormCard.TabIndex = 1;
            // 
            // pnlFormContent
            // 
            this.pnlFormContent.BackColor = System.Drawing.Color.White;
            this.pnlFormContent.Controls.Add(this.lblFormAccent);
            this.pnlFormContent.Controls.Add(this.lblFormTitle);
            this.pnlFormContent.Controls.Add(this.pnlFormDivider1);
            this.pnlFormContent.Controls.Add(this.label4);
            this.pnlFormContent.Controls.Add(this.txtBankCode);
            this.pnlFormContent.Controls.Add(this.label6);
            this.pnlFormContent.Controls.Add(this.txtBankName);
            this.pnlFormContent.Controls.Add(this.label5);
            this.pnlFormContent.Controls.Add(this.txtBranch);
            this.pnlFormContent.Controls.Add(this.label7);
            this.pnlFormContent.Controls.Add(this.txtSwiftCode);
            this.pnlFormContent.Controls.Add(this.pnlFormDivider2);
            this.pnlFormContent.Controls.Add(this.btnAdd);
            this.pnlFormContent.Controls.Add(this.btnEdit);
            this.pnlFormContent.Controls.Add(this.btnDelete);
            this.pnlFormContent.Location = new System.Drawing.Point(0, 0);
            this.pnlFormContent.Name = "pnlFormContent";
            this.pnlFormContent.Size = new System.Drawing.Size(415, 500);
            this.pnlFormContent.TabIndex = 0;
            // 
            // lblFormAccent
            // 
            this.lblFormAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblFormAccent.Location = new System.Drawing.Point(30, 22);
            this.lblFormAccent.Name = "lblFormAccent";
            this.lblFormAccent.Size = new System.Drawing.Size(5, 24);
            this.lblFormAccent.TabIndex = 0;
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblFormTitle.Location = new System.Drawing.Point(42, 20);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(229, 30);
            this.lblFormTitle.TabIndex = 1;
            this.lblFormTitle.Text = "Thông tin ngân hàng";
            // 
            // pnlFormDivider1
            // 
            this.pnlFormDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlFormDivider1.Location = new System.Drawing.Point(30, 60);
            this.pnlFormDivider1.Name = "pnlFormDivider1";
            this.pnlFormDivider1.Size = new System.Drawing.Size(355, 1);
            this.pnlFormDivider1.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label4.Location = new System.Drawing.Point(30, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "Mã ngân hàng (*)";
            // 
            // txtBankCode
            // 
            this.txtBankCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBankCode.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtBankCode.Location = new System.Drawing.Point(30, 104);
            this.txtBankCode.Name = "txtBankCode";
            this.txtBankCode.Size = new System.Drawing.Size(355, 31);
            this.txtBankCode.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label6.Location = new System.Drawing.Point(30, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(146, 21);
            this.label6.TabIndex = 5;
            this.label6.Text = "Tên ngân hàng (*)";
            // 
            // txtBankName
            // 
            this.txtBankName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBankName.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtBankName.Location = new System.Drawing.Point(30, 170);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(355, 31);
            this.txtBankName.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label5.Location = new System.Drawing.Point(30, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 21);
            this.label5.TabIndex = 7;
            this.label5.Text = "Chi nhánh";
            // 
            // txtBranch
            // 
            this.txtBranch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBranch.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtBranch.Location = new System.Drawing.Point(30, 236);
            this.txtBranch.Name = "txtBranch";
            this.txtBranch.Size = new System.Drawing.Size(355, 31);
            this.txtBranch.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.label7.Location = new System.Drawing.Point(30, 280);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 21);
            this.label7.TabIndex = 9;
            this.label7.Text = "Swift code";
            // 
            // txtSwiftCode
            // 
            this.txtSwiftCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSwiftCode.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtSwiftCode.Location = new System.Drawing.Point(30, 302);
            this.txtSwiftCode.Name = "txtSwiftCode";
            this.txtSwiftCode.Size = new System.Drawing.Size(355, 31);
            this.txtSwiftCode.TabIndex = 10;
            // 
            // pnlFormDivider2
            // 
            this.pnlFormDivider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlFormDivider2.Location = new System.Drawing.Point(30, 360);
            this.pnlFormDivider2.Name = "pnlFormDivider2";
            this.pnlFormDivider2.Size = new System.Drawing.Size(355, 1);
            this.pnlFormDivider2.TabIndex = 11;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(30, 380);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(355, 44);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "+  Thêm ngân hàng";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.White;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnEdit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnEdit.Location = new System.Drawing.Point(30, 434);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(172, 42);
            this.btnEdit.TabIndex = 13;
            this.btnEdit.Text = "Sửa ngân hàng";
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.White;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnDelete.Location = new System.Drawing.Point(213, 434);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(172, 42);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "Xóa ngân hàng";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // pnlListCard
            // 
            this.pnlListCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlListCard.BackColor = System.Drawing.Color.White;
            this.pnlListCard.Controls.Add(this.lblListAccent);
            this.pnlListCard.Controls.Add(this.lblListTitle);
            this.pnlListCard.Controls.Add(this.lblListCount);
            this.pnlListCard.Controls.Add(this.btnReset);
            this.pnlListCard.Controls.Add(this.pnlListDivider);
            this.pnlListCard.Controls.Add(this.dgvBank);
            this.pnlListCard.Location = new System.Drawing.Point(490, 115);
            this.pnlListCard.Name = "pnlListCard";
            this.pnlListCard.Size = new System.Drawing.Size(808, 640);
            this.pnlListCard.TabIndex = 2;
            // 
            // lblListAccent
            // 
            this.lblListAccent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblListAccent.Location = new System.Drawing.Point(30, 22);
            this.lblListAccent.Name = "lblListAccent";
            this.lblListAccent.Size = new System.Drawing.Size(5, 24);
            this.lblListAccent.TabIndex = 0;
            // 
            // lblListTitle
            // 
            this.lblListTitle.AutoSize = true;
            this.lblListTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblListTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblListTitle.Location = new System.Drawing.Point(42, 20);
            this.lblListTitle.Name = "lblListTitle";
            this.lblListTitle.Size = new System.Drawing.Size(235, 30);
            this.lblListTitle.TabIndex = 1;
            this.lblListTitle.Text = "Danh sách ngân hàng";
            // 
            // lblListCount
            // 
            this.lblListCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblListCount.AutoSize = true;
            this.lblListCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblListCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblListCount.Location = new System.Drawing.Point(540, 28);
            this.lblListCount.Name = "lblListCount";
            this.lblListCount.Size = new System.Drawing.Size(110, 23);
            this.lblListCount.TabIndex = 2;
            this.lblListCount.Text = "0 ngân hàng";
            this.lblListCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnReset.Location = new System.Drawing.Point(660, 22);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(120, 34);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Làm mới";
            this.btnReset.UseVisualStyleBackColor = false;
            // 
            // pnlListDivider
            // 
            this.pnlListDivider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlListDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlListDivider.Location = new System.Drawing.Point(30, 70);
            this.pnlListDivider.Name = "pnlListDivider";
            this.pnlListDivider.Size = new System.Drawing.Size(750, 1);
            this.pnlListDivider.TabIndex = 4;
            // 
            // dgvBank
            // 
            this.dgvBank.AllowUserToAddRows = false;
            this.dgvBank.AllowUserToDeleteRows = false;
            this.dgvBank.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.dgvBank.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBank.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBank.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBank.BackgroundColor = System.Drawing.Color.White;
            this.dgvBank.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvBank.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvBank.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBank.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBank.ColumnHeadersHeight = 46;
            this.dgvBank.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBank.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.MNH,
            this.TNH,
            this.CN,
            this.SC});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBank.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvBank.EnableHeadersVisualStyles = false;
            this.dgvBank.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.dgvBank.Location = new System.Drawing.Point(30, 95);
            this.dgvBank.MultiSelect = false;
            this.dgvBank.Name = "dgvBank";
            this.dgvBank.ReadOnly = true;
            this.dgvBank.RowHeadersVisible = false;
            this.dgvBank.RowHeadersWidth = 51;
            this.dgvBank.RowTemplate.Height = 40;
            this.dgvBank.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBank.Size = new System.Drawing.Size(750, 520);
            this.dgvBank.TabIndex = 5;
            // 
            // STT
            // 
            this.STT.FillWeight = 50F;
            this.STT.HeaderText = "STT";
            this.STT.MinimumWidth = 6;
            this.STT.Name = "STT";
            this.STT.ReadOnly = true;
            // 
            // MNH
            // 
            this.MNH.FillWeight = 120F;
            this.MNH.HeaderText = "Mã ngân hàng";
            this.MNH.MinimumWidth = 6;
            this.MNH.Name = "MNH";
            this.MNH.ReadOnly = true;
            // 
            // TNH
            // 
            this.TNH.FillWeight = 160F;
            this.TNH.HeaderText = "Tên ngân hàng";
            this.TNH.MinimumWidth = 6;
            this.TNH.Name = "TNH";
            this.TNH.ReadOnly = true;
            // 
            // CN
            // 
            this.CN.FillWeight = 130F;
            this.CN.HeaderText = "Chi nhánh";
            this.CN.MinimumWidth = 6;
            this.CN.Name = "CN";
            this.CN.ReadOnly = true;
            // 
            // SC
            // 
            this.SC.FillWeight = 110F;
            this.SC.HeaderText = "Swift Code";
            this.SC.MinimumWidth = 6;
            this.SC.Name = "SC";
            this.SC.ReadOnly = true;
            // 
            // BankForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
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
            ((System.ComponentModel.ISupportInitialize)(this.dgvBank)).EndInit();
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
        private System.Windows.Forms.TextBox txtBankCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBranch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSwiftCode;
        private System.Windows.Forms.Panel pnlFormDivider2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;

        private System.Windows.Forms.Panel pnlListCard;
        private System.Windows.Forms.Panel lblListAccent;
        private System.Windows.Forms.Label lblListTitle;
        private System.Windows.Forms.Label lblListCount;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel pnlListDivider;
        private System.Windows.Forms.DataGridView dgvBank;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn MNH;
        private System.Windows.Forms.DataGridViewTextBoxColumn TNH;
        private System.Windows.Forms.DataGridViewTextBoxColumn CN;
        private System.Windows.Forms.DataGridViewTextBoxColumn SC;
    }
}