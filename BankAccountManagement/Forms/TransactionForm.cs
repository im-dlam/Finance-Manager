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
    /// <summary>
    /// Form Quản lý Lịch sử giao dịch tài chính (Transaction History Form).
    /// <para>
    /// Các chức năng chính:
    /// - Xem danh sách biến động số dư: Thu (+) màu xanh lá cây, Chi (-) màu đỏ tươi.
    /// - Hỗ trợ đa tiền tệ (VND, USD, EUR, JPY, GBP, CNY...) với quy đổi tỷ giá thời gian thực.
    /// - Tìm kiếm và lọc giao dịch theo từ khóa, số tài khoản đích, và loại giao dịch (Nạp tiền, Chuyển khoản, Rút tiền, Thanh toán nợ...).
    /// - Thống kê tức thì: Tổng tiền Thu vào, Tổng tiền Chi ra, và Tổng số lượt giao dịch phát sinh.
    /// - Xuất danh sách giao dịch ra tệp tin CSV chuẩn mã hóa UTF-8.
    /// - Nút "Thực hiện giao dịch mới" kích hoạt Form AddTransactionForm kèm hiệu ứng làm tối nền (Dimming Overlay).
    /// </para>
    /// </summary>
    public partial class TransactionForm : Form
    {
        #region 1. CÁC BIẾN VÀ DỊCH VỤ

        /// <summary>Dịch vụ xử lý nghiệp vụ truy vấn, thống kê và thêm mới giao dịch.</summary>
        private readonly TransactionService _transactionService;

        #endregion

        #region 2. KHỞI TẠO VÀ CẤU HÌNH BẢNG (INITIALIZATION & COLUMN SETUP)

        /// <summary>
        /// Hàm dựng khởi tạo các điều khiển, ánh xạ cột dữ liệu DataPropertyName và đăng ký các sự kiện.
        /// </summary>
        public TransactionForm()
        {
            InitializeComponent();

            _transactionService = new TransactionService();

            // Cấu hình DataGridView không tự sinh cột lộn xộn
            dataGridViewTransactions.AutoGenerateColumns = false;

            if (dataGridViewTransactions.Columns.Count >= 7)
            {
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn1"].DataPropertyName = "NgayGiaoDich";
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn2"].DataPropertyName = "NoiDung";
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn3"].DataPropertyName = "SoTaiKhoan";
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn4"].DataPropertyName = "Loai";

                // Bổ sung 2 cột Tiền gốc và Loại tiền tệ (nếu chưa có trong thiết kế Designer)
                if (!dataGridViewTransactions.Columns.Contains("colTienGoc"))
                {
                    DataGridViewTextBoxColumn colGoc = new DataGridViewTextBoxColumn
                    {
                        Name = "colTienGoc",
                        HeaderText = "Tiền gốc",
                        DataPropertyName = "SoTienGoc",
                        Width = 110
                    };
                    colGoc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridViewTransactions.Columns.Insert(4, colGoc);

                    DataGridViewTextBoxColumn colLoai = new DataGridViewTextBoxColumn
                    {
                        Name = "colLoaiTien",
                        HeaderText = "Loại tiền",
                        DataPropertyName = "LoaiTien",
                        Width = 85
                    };
                    colLoai.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dataGridViewTransactions.Columns.Insert(5, colLoai);
                }

                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn5"].DataPropertyName = "SoTien";
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn5"].HeaderText = "Quy đổi (VND)";
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn5"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn7"].DataPropertyName = "ThaoTac";
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn7"].HeaderText = "Thao tác";
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn7"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn6"].DataPropertyName = "TrangThai";
                dataGridViewTransactions.Columns["dataGridViewTextBoxColumn6"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Đăng ký sự kiện tô màu và định dạng số tiền trên ô bảng
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

        /// <summary>
        /// Tải dữ liệu ban đầu khi mở màn hình Lịch sử giao dịch.
        /// </summary>
        private void TransactionForm_Load(object sender, EventArgs e)
        {
            textSearch.Text = "";
            LoadComboBoxes();
            LoadData();
            LoadStatistics();
        }

        #endregion

        #region 3. NẠP DỮ LIỆU BỘ LỌC COMBOBOX (FILTER DATA)

        /// <summary>
        /// Nạp danh sách tài khoản và các loại giao dịch vào bộ chọn lọc trên giao diện.
        /// </summary>
        private void LoadComboBoxes()
        {
            try
            {
                DataTable dtAccounts = _transactionService.GetAccountsList();

                if (CurrentSession.IsAdmin)
                {
                    DataRow allAccRow = dtAccounts.NewRow();
                    allAccRow["SoTaiKhoan"] = "Tất cả";
                    allAccRow["HienThi"] = "Tất cả tài khoản";
                    dtAccounts.Rows.InsertAt(allAccRow, 0);

                    comboAccount.DataSource = dtAccounts;
                    comboAccount.DisplayMember = "HienThi";
                    comboAccount.ValueMember = "SoTaiKhoan";
                    comboAccount.DropDownStyle = ComboBoxStyle.DropDownList;
                    comboAccount.SelectedIndex = 0;
                    comboAccount.Enabled = true;
                }
                else
                {
                    // Đối với Khách hàng (Member): Cố định hiển thị tài khoản của chính mình
                    comboAccount.DataSource = dtAccounts;
                    comboAccount.DisplayMember = "HienThi";
                    comboAccount.ValueMember = "SoTaiKhoan";
                    comboAccount.DropDownStyle = ComboBoxStyle.DropDownList;
                    if (dtAccounts.Rows.Count > 0)
                    {
                        comboAccount.SelectedIndex = 0;
                        comboAccount.Enabled = false;
                    }
                }

                DataTable dtTypes = _transactionService.GetTransactionTypes();

                DataRow allTypeRow = dtTypes.NewRow();
                allTypeRow["MaLoaiGD"] = 0;
                allTypeRow["TenLoaiGD"] = "Tất cả";
                dtTypes.Rows.InsertAt(allTypeRow, 0);

                DataRow initRow = dtTypes.NewRow();
                initRow["MaLoaiGD"] = 99;
                initRow["TenLoaiGD"] = "Khởi tạo";
                dtTypes.Rows.Add(initRow);

                comboType.DataSource = dtTypes;
                comboType.DisplayMember = "TenLoaiGD";
                comboType.ValueMember = "TenLoaiGD";
                comboType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải bộ lọc giao dịch:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lấy số tài khoản được chọn trong ComboBox để lọc kết quả truy vấn.
        /// </summary>
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
            if (string.IsNullOrEmpty(text) || text == "Tất cả" || text == "Tất cả tài khoản")
                return "Tất cả";

            if (text.Contains(" - "))
            {
                string[] parts = text.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 1) return parts[1].Trim();
            }
            return text;
        }

        #endregion

        #region 4. TRUY VẤN VÀ THỐNG KÊ GIAO DỊCH (QUERY & STATS)

        /// <summary>
        /// Tải danh sách giao dịch từ CSDL theo các điều kiện lọc và gán nguồn vào DataGridView.
        /// </summary>
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

                // Ẩn các cột trung gian không cần trình chiếu cho người dùng
                foreach (DataGridViewColumn col in dataGridViewTransactions.Columns)
                {
                    if (col.Name == "PhanLoai" || col.Name == "MaLoaiTien") col.Visible = false;
                }

                if (dt != null)
                {
                    labelPageInfo.Text = $"Hiển thị {dt.Rows.Count} giao dịch";
                    lblTableHint.Text = $"• Tìm thấy {dt.Rows.Count} giao dịch phù hợp";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách giao dịch: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nạp tổng tiền Thu, Tổng tiền Chi và Tổng số lượng giao dịch vào các thẻ tóm tắt trên cùng.
        /// </summary>
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

                    labelIncome.Text = $"+ {tongThu:N0} ₫";
                    labelExpense.Text = $"- {tongChi:N0} ₫";
                    labelCount.Text = $"{tongGiaoDich:N0}";
                }
            }
            catch { }
        }

        #endregion

        #region 5. ĐỊNH DẠNG Ô VÀ MÀU SẮC TRỰC QUAN (CELL FORMATTING)

        /// <summary>
        /// Tùy biến hiển thị cho từng ô dữ liệu trên bảng:
        /// - Phân loại Thu (xanh lá) / Chi (đỏ).
        /// - Tiền tệ quy đổi kèm ký hiệu (+ / -) và đơn vị (₫).
        /// - Trạng thái: ● Thành công (xanh), ● Đang chờ (vàng cam), ● Thất bại (đỏ).
        /// </summary>
        private void dataGridViewTransactions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;

            string colName = dataGridViewTransactions.Columns[e.ColumnIndex].Name;
            var row = dataGridViewTransactions.Rows[e.RowIndex];

            // 1. Cột Loại giao dịch (Thu / Chi)
            if (colName == "dataGridViewTextBoxColumn4")
            {
                string loai = e.Value?.ToString() ?? "";
                if (loai.Equals("Thu", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74);
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
                else if (loai.Equals("Chi", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
            }
            // 2. Cột Tiền gốc ngoại tệ
            else if (colName == "colTienGoc" && decimal.TryParse(e.Value.ToString(), out decimal tienGoc))
            {
                string loaiTien = row.Cells["colLoaiTien"].Value?.ToString() ?? "VND";
                if (loaiTien.Equals("VND", StringComparison.OrdinalIgnoreCase) ||
                    loaiTien.Equals("JPY", StringComparison.OrdinalIgnoreCase) ||
                    loaiTien.Equals("KRW", StringComparison.OrdinalIgnoreCase))
                {
                    e.Value = tienGoc.ToString("N0");
                }
                else
                {
                    e.Value = (tienGoc % 1 == 0) ? tienGoc.ToString("N0") : tienGoc.ToString("N2");
                }
                e.FormattingApplied = true;
            }
            // 3. Cột Tiền quy đổi VND
            else if (colName == "dataGridViewTextBoxColumn5" && decimal.TryParse(e.Value.ToString(), out decimal quyDoi))
            {
                string loai = row.Cells["dataGridViewTextBoxColumn4"].Value?.ToString() ?? "";
                if (loai.Equals("Thu", StringComparison.OrdinalIgnoreCase))
                {
                    e.Value = $"+ {quyDoi:N0} ₫";
                    e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74);
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
                else if (loai.Equals("Chi", StringComparison.OrdinalIgnoreCase))
                {
                    e.Value = $"- {quyDoi:N0} ₫";
                    e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                    e.CellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
                else
                {
                    e.Value = $"{quyDoi:N0} ₫";
                }
                e.FormattingApplied = true;
            }
            // 4. Cột Đơn vị tiền tệ
            else if (colName == "colLoaiTien" && (e.Value == null || string.IsNullOrWhiteSpace(e.Value.ToString())))
            {
                e.Value = "VND";
                e.FormattingApplied = true;
            }
            // 5. Cột Trạng thái giao dịch
            else if (colName == "dataGridViewTextBoxColumn6")
            {
                string status = e.Value?.ToString() ?? "";
                if (status.Equals("Success", StringComparison.OrdinalIgnoreCase) || status.Contains("Thành công"))
                {
                    e.Value = "● Thành công";
                    e.CellStyle.ForeColor = Color.FromArgb(22, 163, 74);
                    e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    e.FormattingApplied = true;
                }
                else if (status.Equals("Pending", StringComparison.OrdinalIgnoreCase) || status.Contains("chờ"))
                {
                    e.Value = "● Đang chờ";
                    e.CellStyle.ForeColor = Color.FromArgb(217, 119, 6);
                    e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    e.FormattingApplied = true;
                }
                else if (status.Equals("Failed", StringComparison.OrdinalIgnoreCase) || status.Contains("bại"))
                {
                    e.Value = "● Thất bại";
                    e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                    e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                    e.FormattingApplied = true;
                }
            }
        }

        #endregion

        #region 6. XỬ LÝ SỰ KIỆN NÚT BẤM (BUTTON ACTIONS)

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

        /// <summary>
        /// Xuất danh sách giao dịch đang hiển thị ra tệp tin CSV với chuẩn mã hóa UTF-8.
        /// </summary>
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

                        // Dòng tiêu đề cột (Header row)
                        for (int i = 0; i < dataGridViewTransactions.Columns.Count; i++)
                        {
                            if (dataGridViewTransactions.Columns[i].Visible)
                            {
                                sb.Append(dataGridViewTransactions.Columns[i].HeaderText + (i == dataGridViewTransactions.Columns.Count - 1 ? "" : ","));
                            }
                        }
                        sb.AppendLine();

                        // Các dòng dữ liệu bản ghi (Data rows)
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

        /// <summary>
        /// Mở Form tạo giao dịch mới (AddTransactionForm) với hiệu ứng làm mờ tối nền (Modal Overlay).
        /// </summary>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Form mainForm = Application.OpenForms["MainForm"];
            if (mainForm == null) mainForm = this.FindForm() ?? this;

            List<CurrencyInfo> currencies = _transactionService.GetCurrencies();

            // Tạo một Form overlay màu đen trong suốt (Opacity 65%) phủ kín cửa sổ chính
            using (Form overlay = new Form())
            {
                overlay.StartPosition = FormStartPosition.Manual;
                overlay.FormBorderStyle = FormBorderStyle.None;
                overlay.Opacity = 0.65d;
                overlay.BackColor = Color.Black;
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

        private void dataGridViewTransactions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void CardBorder_Paint(object sender, PaintEventArgs e)
        {
            if (sender is Control ctrl)
            {
                using (Pen pen = new Pen(Color.FromArgb(226, 232, 240), 1))
                {
                    Rectangle rect = ctrl.ClientRectangle;
                    rect.Width -= 1;
                    rect.Height -= 1;
                    e.Graphics.DrawRectangle(pen, rect);
                }
            }
        }

        private void PanelBottomBorder_Paint(object sender, PaintEventArgs e)
        {
            if (sender is Control ctrl)
            {
                using (Pen pen = new Pen(Color.FromArgb(226, 232, 240), 1))
                {
                    e.Graphics.DrawLine(pen, 0, ctrl.Height - 1, ctrl.Width, ctrl.Height - 1);
                }
            }
        }

        #endregion
    }
}
