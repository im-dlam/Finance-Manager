namespace BankAccountManagement.Forms
{
    partial class DashboardForm
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.roundedPanel1 = new BankAccountManagement.Forms.RoundedPanel();
            this.SuspendLayout();
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.BorderRadius = 20;
            this.roundedPanel1.Location = new System.Drawing.Point(269, 87);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.Size = new System.Drawing.Size(200, 100);
            this.roundedPanel1.TabIndex = 0;
            this.roundedPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.roundedPanel1_Paint);
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1026, 590);
            this.Controls.Add(this.roundedPanel1);
            this.Name = "DashboardForm";
            this.Text = "DashboardForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private RoundedPanel roundedPanel1;
    }
}