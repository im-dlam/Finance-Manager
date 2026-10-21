using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
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

                if (!dataGridViewTransactions.Columns.Contains("colTienGoc"))
                {
                    DataGridViewTextBoxColumn colGoc =
                        new DataGridViewTextBoxColumn();

                    colGoc.Name = "colTienGoc";
                    colGoc.HeaderText = "Tiền gốc";
                    colGoc.DataPropertyName = "SoTienGoc";
                    colGoc.Width = 110;
                    colGoc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dataGridViewTransactions.Columns.Insert(4, colGoc);

                    DataGridViewTextBoxColumn colLoai =
                        new DataGridViewTextBoxColumn();

                    colLoai.Name = "colLoaiTien";
                    colLoai.HeaderText = "Loại tiền";
                    colLoai.DataPropertyName = "LoaiTien";
                    colLoai.Width = 85;
                    colLoai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dataGridViewTransactions.Columns.Insert(5, colLoai);
                }

                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn5"].DataPropertyName = "SoTien";
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn5"].HeaderText = "Quy đổi (VND)";
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn5"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn7"].DataPropertyName = "ThaoTac";
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn7"].HeaderText = "Thao tác";
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn6"].DataPropertyName = "TrangThai";

                dataGridViewTransactions.CellFormatting += dataGridViewTransactions_CellFormatting;
            }

            if (this.buttonSearch != null)
                this.buttonSearch.Click += buttonSearch_Click;

            if (this.buttonRefresh != null)
                this.buttonRefresh.Click += buttonRefresh_Click;

            if (this.buttonExport != null)
                this.buttonExport.Click += buttonExport_Click;

            if (this.buttonAdd != null)
                this.buttonAdd.Click += buttonAdd_Click;

            this.Load += TransactionForm_Load;
       
        }
        private void LoadComboBoxes()
        {
            try
            {
                DataTable dtAccounts =
                    _transactionService.GetAccountsList();

                if (CurrentSession.IsAdmin)
                {
                    DataRow allAccRow = dtAccounts.NewRow();
                    allAccRow["SoTaiKhoan"] = "Tất cả";
                    allAccRow["HienThi"] = "Tất cả";
                    dtAccounts.Rows.InsertAt(allAccRow, 0);

                    comboAccount.DataSource = dtAccounts;
                    comboAccount.DisplayMember = "HienThi";
                    comboAccount.ValueMember = "SoTaiKhoan";
                    comboAccount.DropDownStyle = ComboBoxStyle.DropDown;
                    comboAccount.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    comboAccount.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboAccount.SelectedIndex = 0;
                    comboAccount.Enabled = true;
                }
                else
                {
                    // Đối với User (Member): Mỗi người chỉ có duy nhất 1 tài khoản ngân hàng
                    comboAccount.DataSource = dtAccounts;
                    comboAccount.DisplayMember = "HienThi";
                    comboAccount.ValueMember = "SoTaiKhoan";
                    comboAccount.DropDownStyle = ComboBoxStyle.DropDownList;
                    if (dtAccounts.Rows.Count > 0)
                    {
                        comboAccount.SelectedIndex = 0;
                        comboAccount.Enabled = false; // Cố định hiển thị tài khoản của chính mình
                    }
                }

                DataTable dtTypes =
                    _transactionService.GetTransactionTypes();

                DataRow allTypeRow =
                    dtTypes.NewRow();

                allTypeRow["MaLoaiGD"] = 0;
                allTypeRow["TenLoaiGD"] = "Tất cả";

                dtTypes.Rows.InsertAt(allTypeRow, 0);

                comboType.DataSource = dtTypes;
                comboType.DisplayMember = "TenLoaiGD";
                comboType.ValueMember = "TenLoaiGD";
                comboType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tải bộ lọc giao dịch:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string GetFilterAccount()
        {
            if (!CurrentSession.IsAdmin)
            {
                if (comboAccount.SelectedValue != null && !string.IsNullOrEmpty(comboAccount.SelectedValue.ToString()))
                    return comboAccount.SelectedValue.ToString();
                if (comboAccount.SelectedItem is DataRowView drv)
                    return drv["SoTaiKhoan"]?.ToString() ?? "Tất cả";
            }

            if (comboAccount.SelectedIndex > 0)
                return comboAccount.SelectedValue?.ToString() ?? "Tất cả";

            string text = comboAccount.Text.Trim();
            if (string.IsNullOrEmpty(text) || text == "Tất cả")
                return "Tất cả";

            if (text.Contains(" - "))
            {
                string[] parts = text.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1) return parts[1].Trim();
            }
            return text;
        }

        private void LoadData()
        {
            try
            {
                string kw = textSearch.Text.Trim();
                string tk = GetFilterAccount();
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
                    if (col.Name == "PhanLoai" || col.Name == "MaLoaiTien") col.Visible = false;
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
                string tk = GetFilterAccount();
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

        private void dataGridViewTransactions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;

            string colName = dataGridViewTransactions.Columns[e.ColumnIndex].Name;

            // Định dạng tiền gốc (USD hiển thị 2 chữ số thập phân, VND hiển thị N0)
            if (colName == "colTienGoc" && decimal.TryParse(e.Value.ToString(), out decimal tienGoc))
            {
                var row = dataGridViewTransactions.Rows[e.RowIndex];
                string loaiTien = row.Cells["colLoaiTien"].Value?.ToString() ?? "VND";
                if (loaiTien.Equals("USD", StringComparison.OrdinalIgnoreCase))
                {
                    e.Value = tienGoc.ToString("N2");
                }
                else
                {
                    e.Value = tienGoc.ToString("N0");
                }
                e.FormattingApplied = true;
            }
            // Định dạng số tiền quy đổi (VND)
            else if (colName == "dataGridViewTextBoxColumn5" && decimal.TryParse(e.Value.ToString(), out decimal quyDoi))
            {
                e.Value = quyDoi.ToString("N0");
                e.FormattingApplied = true;
            }
            // Đảm bảo cột Loại tiền luôn hiển thị rõ VND / USD nếu null hoặc rỗng
            else if (colName == "colLoaiTien" && (e.Value == null || string.IsNullOrWhiteSpace(e.Value.ToString())))
            {
                e.Value = "VND";
                e.FormattingApplied = true;
            }
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
            List<CurrencyInfo> currencies = _transactionService.GetCurrencies();
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

                using (AddTransactionForm addForm = new AddTransactionForm(currencies))
                {
                    addForm.StartPosition = FormStartPosition.CenterParent;
                    DialogResult result = addForm.ShowDialog(overlay);

                    if (result == DialogResult.OK)
                    {
                        LoadData();      
                        LoadStatistics();
                        TransactionService.NotifyTransactionChanged();
                    }
                }
                overlay.Close();
            }
        }

        private void TransactionForm_Load(object sender, EventArgs e)
        {
            textSearch.Text = ""; // fix khong load
            LoadComboBoxes();
            LoadData();
            LoadStatistics();
        }
    }
}