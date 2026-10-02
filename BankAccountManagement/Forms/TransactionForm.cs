using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using BankAccountManagement.Services;

namespace BankAccountManagement
{
    public partial class TransactionForm : Form
    {
        private readonly TransactionService _transactionService;

        public TransactionForm()
        {
            InitializeComponent();
            _transactionService = new TransactionService();

            // Tự động gán sự kiện Click cho các nút trong code (không lo lỗi Properties)
            BindButtonEvents();
        }

        private void BindButtonEvents()
        {
            // Gán sự kiện nút Search
            Control[] btnsSearch = this.Controls.Find("buttonSearch", true);
            if (btnsSearch.Length > 0 && btnsSearch[0] is Button btnSearch)
                btnSearch.Click += buttonSearch_Click;

            // Gán sự kiện nút Refresh
            Control[] btnsRefresh = this.Controls.Find("buttonRefresh", true);
            if (btnsRefresh.Length > 0 && btnsRefresh[0] is Button btnRefresh)
                btnRefresh.Click += buttonRefresh_Click;

            // Gán sự kiện nút Export
            Control[] btnsExport = this.Controls.Find("buttonExport", true);
            if (btnsExport.Length > 0 && btnsExport[0] is Button btnExport)
                btnExport.Click += buttonExport_Click;

            // Gán sự kiện nút Add
            Control[] btnsAdd = this.Controls.Find("buttonAdd", true);
            if (btnsAdd.Length == 0) btnsAdd = this.Controls.Find("buttonAddTransaction", true);
            if (btnsAdd.Length > 0 && btnsAdd[0] is Button btnAdd)
                btnAdd.Click += buttonAdd_Click;
        }

        private void TransactionForm_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadStatistics();
        }

        #region 1. Tải dữ liệu & Thống kê Cards

        private void LoadData()
        {
            try
            {
                DataTable dt = _transactionService.GetAllTransactions();
                dataGridViewTransactions.DataSource = dt;

                if (dataGridViewTransactions.Columns["SoTien"] != null)
                {
                    dataGridViewTransactions.Columns["SoTien"].DefaultCellStyle.Format = "N0";
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
                DataTable dtStats = _transactionService.GetTransactionStatistics();
                if (dtStats != null && dtStats.Rows.Count > 0)
                {
                    DataRow row = dtStats.Rows[0];

                    decimal tongThu = row["TongTienGui"] != DBNull.Value ? Convert.ToDecimal(row["TongTienGui"]) : 0;
                    decimal tongChi = row["TongTienRut"] != DBNull.Value ? Convert.ToDecimal(row["TongTienRut"]) : 0;
                    int tongGiaoDich = row["TongGiaoDich"] != DBNull.Value ? Convert.ToInt32(row["TongGiaoDich"]) : 0;

                    Control[] lblThu = this.Controls.Find("lblTongThu", true);
                    if (lblThu.Length > 0) lblThu[0].Text = $"+ {tongThu:N0} đ";

                    Control[] lblChi = this.Controls.Find("lblTongChi", true);
                    if (lblChi.Length > 0) lblChi[0].Text = $"- {tongChi:N0} đ";

                    Control[] lblSoGD = this.Controls.Find("lblSoGiaoDich", true);
                    if (lblSoGD.Length > 0) lblSoGD[0].Text = tongGiaoDich.ToString();
                }
            }
            catch
            {
                // Bỏ qua nếu chưa đặt tên Control Label
            }
        }

        #endregion

        #region 2. Xử lý các Nút Thao Tác (Button Events)

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = GetSearchKeyword();

                if (string.IsNullOrEmpty(keyword))
                {
                    LoadData();
                }
                else
                {
                    DataTable dt = _transactionService.SearchTransactions(keyword);
                    dataGridViewTransactions.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm giao dịch: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            SetSearchKeyword("");
            LoadData();
            LoadStatistics();
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
            // 1. Tạo overlay làm mờ nền xung quanh
            using (Form overlay = new Form())
            {
                overlay.StartPosition = FormStartPosition.Manual;
                overlay.FormBorderStyle = FormBorderStyle.None;
                overlay.Opacity = 0.50d;
                overlay.BackColor = System.Drawing.Color.Black;
                overlay.Size = this.Size;
                overlay.Location = this.Location;
                overlay.ShowInTaskbar = false;
                overlay.Show(this);

                AddTransactionForm addForm = new AddTransactionForm();

                addForm.StartPosition = FormStartPosition.CenterParent;

                DialogResult result = addForm.ShowDialog(overlay);

                if (result == DialogResult.OK)
                {
                    LoadData();      
                    LoadStatistics();
                }
                addForm.Dispose();
                overlay.Close();
            }
        }

        #endregion

        #region 3. Hàm Hỗ Trợ Đọc/Xóa Ô Tìm Kiếm Linh Hoạt

        private string GetSearchKeyword()
        {
            string[] possibleNames = { "textBoxSearch", "txtSearch", "txtTimKiem" };
            foreach (string name in possibleNames)
            {
                Control[] controls = this.Controls.Find(name, true);
                if (controls.Length > 0 && controls[0] is TextBox txt)
                {
                    return txt.Text.Trim();
                }
            }
            return "";
        }

        private void SetSearchKeyword(string value)
        {
            string[] possibleNames = { "textBoxSearch", "txtSearch", "txtTimKiem" };
            foreach (string name in possibleNames)
            {
                Control[] controls = this.Controls.Find(name, true);
                if (controls.Length > 0 && controls[0] is TextBox txt)
                {
                    txt.Text = value;
                    break;
                }
            }
        }

        #endregion
    }
}