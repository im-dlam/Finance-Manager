using BankAccountManagement.Services;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BankAccountManagement
{
    public partial class TransactionForm : Form
    {
        private TransactionService _transactionService;

        public TransactionForm()
        {
            InitializeComponent();
            _transactionService = new TransactionService();

            dataGridViewTransactions.AutoGenerateColumns = false;
            if (dataGridViewTransactions.Columns.Count >= 7)
            {
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn1"].DataPropertyName = "NgayGiaoDich";
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn2"].DataPropertyName = "NoiDung";
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn3"].DataPropertyName = "SoTaiKhoan";
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn4"].DataPropertyName = "Loai";
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn5"].DataPropertyName = "SoTien";
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn7"].DataPropertyName = "ThaoTac";
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn6"].DataPropertyName = "TrangThai";
            }

            LoadComboBoxes();
            LoadData();
            LoadStatistics();
            
            if (this.buttonSearch != null) this.buttonSearch.Click += buttonSearch_Click;
            if (this.buttonRefresh != null) this.buttonRefresh.Click += buttonRefresh_Click;
            if (this.buttonExport != null) this.buttonExport.Click += buttonExport_Click;
            if (this.buttonAdd != null) this.buttonAdd.Click += buttonAdd_Click;
        }

        private void LoadComboBoxes()
        {
            try
            {
                DataTable dtAccounts = _transactionService.GetAccountsList();
                DataRow allAccRow = dtAccounts.NewRow();
                allAccRow["SoTaiKhoan"] = "Tất cả";
                allAccRow["HienThi"] = "Tất cả";
                dtAccounts.Rows.InsertAt(allAccRow, 0);
                comboAccount.DataSource = dtAccounts;
                comboAccount.DisplayMember = "HienThi";
                comboAccount.ValueMember = "SoTaiKhoan";
                comboAccount.SelectedIndex = 0;

                DataTable dtTypes = _transactionService.GetTransactionTypes();
                DataRow allTypeRow = dtTypes.NewRow();
                allTypeRow["MaLoaiGD"] = 0;
                allTypeRow["TenLoaiGD"] = "Tất cả";
                dtTypes.Rows.InsertAt(allTypeRow, 0);
                comboType.DataSource = dtTypes;
                comboType.DisplayMember = "TenLoaiGD";
                comboType.ValueMember = "TenLoaiGD";
                comboType.SelectedIndex = 0;
            }
            catch { }
        }

        private void LoadData()
        {
            try
            {
                string kw = textSearch.Text.Trim();
                string tk = comboAccount.SelectedIndex <= 0 ? "Tất cả" : comboAccount.SelectedValue?.ToString();
                string loai = comboType.SelectedIndex <= 0 ? "Tất cả" : comboType.SelectedValue?.ToString();

                DataTable dt;
                if (string.IsNullOrEmpty(kw) && tk == "Tất cả" && loai == "Tất cả")
                    dt = _transactionService.GetAllTransactions();
                else
                    dt = _transactionService.SearchTransactions(kw, tk, loai);

                dataGridViewTransactions.DataSource = dt;

                if (dataGridViewTransactions.Columns["SoTien"] != null)
                {
                    dataGridViewTransactions.Columns["SoTien"].DefaultCellStyle.Format = "N0";
                }
                
                // Căn chỉnh giao diện Grid
                foreach (DataGridViewColumn col in dataGridViewTransactions.Columns)
                {
                    if (col.Name == "PhanLoai") col.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách giao dịch: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStatistics()
        {
            try
            {
                string kw = textSearch.Text.Trim();
                string tk = comboAccount.SelectedIndex <= 0 ? "Tất cả" : comboAccount.SelectedValue?.ToString();
                string loai = comboType.SelectedIndex <= 0 ? "Tất cả" : comboType.SelectedValue?.ToString();

                DataTable dtStats = _transactionService.GetTransactionStatistics(kw, tk, loai);
                if (dtStats != null && dtStats.Rows.Count > 0)
                {
                    DataRow row = dtStats.Rows[0];
                    decimal tongThu = row["TongThu"] != DBNull.Value ? Convert.ToDecimal(row["TongThu"]) : 0;
                    decimal tongChi = row["TongChi"] != DBNull.Value ? Convert.ToDecimal(row["TongChi"]) : 0;
                    int tongGiaoDich = row["TongGiaoDich"] != DBNull.Value ? Convert.ToInt32(row["TongGiaoDich"]) : 0;

                    labelIncome.Text = $"+ {tongThu:N0} đ";
                    labelIncome.ForeColor = Color.Green;
                    
                    labelExpense.Text = $"- {tongChi:N0} đ";
                    labelExpense.ForeColor = Color.Red;
                    
                    labelCount.Text = tongGiaoDich.ToString();
                }
            }
            catch { }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            LoadData();
            LoadStatistics();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            textSearch.Text = "";
            if (comboAccount.Items.Count > 0) comboAccount.SelectedIndex = 0;
            if (comboType.Items.Count > 0) comboType.SelectedIndex = 0;
            
            LoadData();
            LoadStatistics();
        }

        private void dataGridViewTransactions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional: Handle cell clicks if needed (e.g. for "Thao tác")
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewTransactions.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu giao dịch để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "File CSV (*.csv)|*.csv|Tất cả file (*.*)|*.*";
                    sfd.FileName = $"DanhSachGiaoDich_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        StringBuilder sb = new StringBuilder();

                        for (int i = 0; i < dataGridViewTransactions.Columns.Count; i++)
                        {
                            if (dataGridViewTransactions.Columns[i].Visible)
                            {
                                sb.Append(dataGridViewTransactions.Columns[i].HeaderText + (i == dataGridViewTransactions.Columns.Count - 1 ? "" : ","));
                            }
                        }
                        sb.AppendLine();

                        foreach (DataGridViewRow row in dataGridViewTransactions.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                for (int i = 0; i < dataGridViewTransactions.Columns.Count; i++)
                                {
                                    if (dataGridViewTransactions.Columns[i].Visible)
                                    {
                                        string cellValue = row.Cells[i].Value != null ? row.Cells[i].Value.ToString().Replace(",", " ") : "";
                                        sb.Append(cellValue + (i == dataGridViewTransactions.Columns.Count - 1 ? "" : ","));
                                    }
                                }
                                sb.AppendLine();
                            }
                        }

                        File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                        MessageBox.Show("Xuất danh sách giao dịch thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // Tìm cửa sổ gốc (MainForm) để bao phủ toàn bộ màn hình, bao gồm cả menu trái
            Form mainForm = Application.OpenForms["MainForm"];
            if (mainForm == null) mainForm = this.FindForm() ?? this;

            using (Form overlay = new Form())
            {
                overlay.StartPosition = FormStartPosition.Manual;
                overlay.FormBorderStyle = FormBorderStyle.None;
                overlay.Opacity = 0.65d; // Tăng độ tối lên một chút để làm mờ nền rõ hơn
                overlay.BackColor = Color.Black;
                
                // Phủ full toàn bộ cửa sổ gốc
                overlay.Size = mainForm.Size;
                overlay.Location = mainForm.Location;
                
                overlay.ShowInTaskbar = false;
                overlay.Show(mainForm);

                using (AddTransactionForm addForm = new AddTransactionForm())
                {
                    addForm.StartPosition = FormStartPosition.CenterParent;
                    DialogResult result = addForm.ShowDialog(overlay);

                    if (result == DialogResult.OK)
                    {
                        LoadData();      
                        LoadStatistics();
                    }
                }
                overlay.Close();
            }
        }

        private void buttonRefresh_Click_1(object sender, EventArgs e)
        {

        }
    }
}