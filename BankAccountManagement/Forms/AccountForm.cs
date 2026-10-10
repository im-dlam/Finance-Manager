using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Form quản lý tài khoản ngân hàng.
    /// 
    /// Phân quyền:
    /// - Admin: thấy TẤT CẢ tài khoản, được thêm/sửa/đóng.
    /// - Member: CHỈ thấy tài khoản của chính mình, chỉ xem.
    /// </summary>
    public partial class AccountForm : Form
    {
        private readonly AccountService _AccountService = new AccountService();
        private bool _isAddNew = false;

        // ⭐ MaKH là string theo schema hiện tại (VARCHAR "KH000001")
        private string _currentMaKH = "";
        private decimal _currentSoDuHienTai = 0;
        private decimal _currentSoDuBanDau = 0;

        private List<TaiKhoan> _allAccounts = new List<TaiKhoan>();

        // ==========================================================
        //  KẾT NỐI DB
        // ==========================================================
        private string GetConnectionString()
        {
            DatabaseConfig config = DatabaseConfigManager.Load();
            return DatabaseService.BuildConnectionString(config);
        }

        private DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                using (SqlDataAdapter da = new SqlDataAdapter(sql, conn))
                    da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi DB: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        // ==========================================================
        //  KHỞI TẠO
        // ==========================================================
        public AccountForm()
        {
            InitializeComponent();
            this.Load += AccountForm_Load;
            dgvAccounts.CellClick += dgvAccounts_CellContentClick;
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnClear.Click += btnClear_Click;
            btnSearch.Click += btnSearch_Click;

            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.KeyDown += txtSearch_KeyDown;
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadComboBoxData();
                LoadData();
                ApplyRolePermissions();
                SetControlState(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi động: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Phân quyền UI: Member không được thêm/sửa/đóng tài khoản.
        /// </summary>
        private void ApplyRolePermissions()
        {
            if (CurrentSession.IsAdmin) return;

            // Member — chỉ xem
            btnAdd.Visible = false;
            btnEdit.Visible = false;
            btnDelete.Visible = false;
        }

        // ==========================================================
        //  ĐỔ DỮ LIỆU COMBOBOX
        // ==========================================================
        private void LoadComboBoxData()
        {
            // ⭐ Bảng mới: ThongTinKhachHang
            DataTable dtKH = GetDataTable(@"
                SELECT MaKH, HoTen 
                FROM   dbo.ThongTinKhachHang 
                WHERE  TrangThai = N'Active' 
                ORDER  BY HoTen");

            cboCustomer.DataSource = dtKH;
            cboCustomer.DisplayMember = "HoTen";
            cboCustomer.ValueMember = "MaKH";

            DataTable dtNH = GetDataTable(@"
                SELECT MaNganHang, TenNganHang 
                FROM   dbo.NganHang 
                ORDER  BY TenNganHang");
            cboBank.DataSource = dtNH;
            cboBank.DisplayMember = "TenNganHang";
            cboBank.ValueMember = "MaNganHang";

            DataTable dtLT = GetDataTable(@"
                SELECT MaLoaiTien, TenLoaiTien 
                FROM   dbo.LoaiTien 
                ORDER  BY TenLoaiTien");
            cboCurrency.DataSource = dtLT;
            cboCurrency.DisplayMember = "TenLoaiTien";
            cboCurrency.ValueMember = "MaLoaiTien";

            cboStatus.Items.Clear();
            cboStatus.Items.Add("Active");
            cboStatus.Items.Add("Closed");
            cboStatus.SelectedIndex = 0;
        }

        // ==========================================================
        //  TẢI DANH SÁCH TÀI KHOẢN
        // ==========================================================
        private void LoadData(string keyword = "")
        {
            try
            {
                var all = _AccountService.GetActiveTaiKhoan() ?? new List<TaiKhoan>();

                // ⭐ Phân quyền: Admin thấy hết, Member chỉ của mình
                if (!CurrentSession.IsAdmin)
                {
                    string myMaKH = CurrentSession.MaKH;
                    all = all.Where(tk =>
                        !string.IsNullOrEmpty(myMaKH) &&
                        string.Equals(tk.MaKH, myMaKH, StringComparison.OrdinalIgnoreCase)
                    ).ToList();
                }

                _allAccounts = all;

                dgvAccounts.AutoGenerateColumns = false;

                if (dgvAccounts.Columns.Count > 0)
                {
                    dgvAccounts.Columns["colAccountNumber"].DataPropertyName = "SoTaiKhoan";
                    dgvAccounts.Columns["colCustomer"].DataPropertyName = "MaKH";
                    dgvAccounts.Columns["colBank"].DataPropertyName = "MaNganHang";
                    dgvAccounts.Columns["colCurrency"].DataPropertyName = "MaLoaiTien";
                    dgvAccounts.Columns["colBalance"].DataPropertyName = "SoDuHienTai";
                    dgvAccounts.Columns["colOpenDate"].DataPropertyName = "NgayMo";
                    dgvAccounts.Columns["colStatus"].DataPropertyName = "TrangThai";
                }

                ApplyFilter(keyword);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================================
        //  TÌM KIẾM / LỌC
        // ==========================================================
        private void ApplyFilter(string keyword)
        {
            string key = Normalize(keyword);
            List<TaiKhoan> ketQua;

            if (key.Length == 0)
            {
                ketQua = _allAccounts;
            }
            else
            {
                var tenKH = BuildLookup(cboCustomer.DataSource as DataTable, "MaKH", "HoTen");
                var tenNH = BuildLookup(cboBank.DataSource as DataTable, "MaNganHang", "TenNganHang");
                var tenLT = BuildLookup(cboCurrency.DataSource as DataTable, "MaLoaiTien", "TenLoaiTien");

                ketQua = _allAccounts.Where(tk =>
                       Match(tk.SoTaiKhoan, key)
                    || Match(tk.MaKH, key)
                    || Match(GetValue(tenKH, tk.MaKH), key)
                    || Match(tk.MaNganHang, key)
                    || Match(GetValue(tenNH, tk.MaNganHang), key)
                    || Match(tk.MaLoaiTien, key)
                    || Match(GetValue(tenLT, tk.MaLoaiTien), key)
                    || Match(tk.TrangThai, key)
                ).ToList();
            }

            dgvAccounts.DataSource = null;
            dgvAccounts.DataSource = ketQua;

            lblListCount.Text = (key.Length > 0 && ketQua.Count == 0)
                ? "Không tìm thấy tài khoản phù hợp"
                : $"{ketQua.Count} tài khoản";
        }

        private static Dictionary<string, string> BuildLookup(DataTable dt, string keyCol, string valueCol)
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (dt == null) return dict;

            foreach (DataRow r in dt.Rows)
            {
                string k = r[keyCol]?.ToString();
                if (!string.IsNullOrEmpty(k) && !dict.ContainsKey(k))
                    dict[k] = r[valueCol]?.ToString() ?? "";
            }
            return dict;
        }

        private static string GetValue(Dictionary<string, string> dict, string key)
            => key != null && dict.TryGetValue(key, out string v) ? v : "";

        private static bool Match(string source, string normalizedKey)
            => !string.IsNullOrEmpty(source) && Normalize(source).Contains(normalizedKey);

        private static string Normalize(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return "";

            string s = text.Trim().ToLowerInvariant().Replace('đ', 'd');
            var sb = new StringBuilder();
            foreach (char c in s.Normalize(NormalizationForm.FormD))
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
            => ApplyFilter(txtSearch.Text);

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ApplyFilter(txtSearch.Text);
            }
        }

        // ==========================================================
        //  TRẠNG THÁI CONTROLS
        // ==========================================================
        private void SetControlState(bool isEditing)
        {
            // ⭐ Member không được vào chế độ edit
            if (!CurrentSession.IsAdmin)
            {
                txtAccountNumber.Enabled = false;
                cboCustomer.Enabled = false;
                txtBalance.Enabled = false;
                cboBank.Enabled = false;
                cboCurrency.Enabled = false;
                dtpOpenDate.Enabled = false;
                cboStatus.Enabled = false;
                return;
            }

            txtAccountNumber.Enabled = isEditing && _isAddNew;
            cboCustomer.Enabled = isEditing && _isAddNew;
            txtBalance.Enabled = isEditing && _isAddNew;

            cboBank.Enabled = isEditing;
            cboCurrency.Enabled = isEditing;
            dtpOpenDate.Enabled = isEditing;
            cboStatus.Enabled = isEditing;

            if (isEditing && _isAddNew)
            {
                btnAdd.Text = "💾 Lưu tài khoản";
                btnAdd.Enabled = true;
                btnEdit.Text = "❌ Hủy";
                btnEdit.Enabled = true;
                btnDelete.Enabled = false;
            }
            else if (isEditing && !_isAddNew)
            {
                btnAdd.Text = "💾 Lưu thay đổi";
                btnAdd.Enabled = true;
                btnEdit.Text = "❌ Hủy chỉnh sửa";
                btnEdit.Enabled = true;
                btnDelete.Enabled = false;
            }
            else
            {
                btnAdd.Text = "+ Thêm tài khoản mới";
                btnAdd.Enabled = true;
                btnEdit.Text = "✏️ Chỉnh sửa";
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void ClearInputs()
        {
            txtAccountNumber.Clear();
            txtBalance.Clear();
            if (cboCustomer.Items.Count > 0) cboCustomer.SelectedIndex = 0;
            if (cboBank.Items.Count > 0) cboBank.SelectedIndex = 0;
            if (cboCurrency.Items.Count > 0) cboCurrency.SelectedIndex = 0;
            if (cboStatus.Items.Count > 0) cboStatus.SelectedIndex = 0;
            dtpOpenDate.Value = DateTime.Now;

            _currentMaKH = "";
            _currentSoDuHienTai = 0;
            _currentSoDuBanDau = 0;
        }

        // ==========================================================
        //  SỰ KIỆN BUTTONS
        // ==========================================================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text.Contains("Thêm"))
            {
                _isAddNew = true;
                ClearInputs();
                SetControlState(true);
                txtAccountNumber.Focus();
            }
            else
            {
                ThucHienLuu();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text.Contains("Hủy"))
            {
                _isAddNew = false;
                ClearInputs();
                LoadData(txtSearch.Text);
                SetControlState(false);
                return;
            }

            if (string.IsNullOrEmpty(txtAccountNumber.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _isAddNew = false;
            SetControlState(true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAccountNumber.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần đóng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(
                    $"Xác nhận ĐÓNG tài khoản: {txtAccountNumber.Text}?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                try
                {
                    if (_AccountService.CloseTaiKhoan(txtAccountNumber.Text))
                    {
                        MessageBox.Show("Đóng tài khoản thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData(txtSearch.Text);
                        ClearInputs();
                        SetControlState(false);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
            => ApplyFilter(txtSearch.Text);

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.TextChanged -= txtSearch_TextChanged;
            txtSearch.Clear();
            txtSearch.TextChanged += txtSearch_TextChanged;

            _isAddNew = false;
            ClearInputs();
            LoadData();
            SetControlState(false);
        }

        // ==========================================================
        //  CLICK VÀO DÒNG DATAGRIDVIEW
        // ==========================================================
        private void dgvAccounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvAccounts.CurrentRow == null) return;

            DataGridViewRow row = dgvAccounts.CurrentRow;

            _currentMaKH = row.Cells["colCustomer"].Value?.ToString() ?? "";
            _currentSoDuHienTai = Convert.ToDecimal(row.Cells["colBalance"].Value ?? 0);
            _currentSoDuBanDau = _currentSoDuHienTai;
            txtAccountNumber.Text = row.Cells["colAccountNumber"].Value?.ToString();
            txtBalance.Text = _currentSoDuHienTai.ToString();
            cboCustomer.SelectedValue = row.Cells["colCustomer"].Value;
            cboBank.SelectedValue = row.Cells["colBank"].Value;

            var currencyValue = row.Cells["colCurrency"].Value;
            if (currencyValue != null && currencyValue != DBNull.Value)
                cboCurrency.SelectedValue = currencyValue;
            else
                cboCurrency.SelectedIndex = -1;

            if (row.Cells["colOpenDate"].Value != null)
                dtpOpenDate.Value = Convert.ToDateTime(row.Cells["colOpenDate"].Value);

            cboStatus.Text = row.Cells["colStatus"].Value?.ToString();

            _isAddNew = false;
            SetControlState(false);
        }

        // ==========================================================
        //  LƯU
        // ==========================================================
        private void ThucHienLuu()
        {
            if (string.IsNullOrWhiteSpace(txtAccountNumber.Text))
            {
                MessageBox.Show("Vui lòng nhập Số tài khoản!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAccountNumber.Focus();
                return;
            }

            TaiKhoan tk;

            if (_isAddNew)
            {
                if (!decimal.TryParse(txtBalance.Text, out decimal soDuMoi) || soDuMoi < 0)
                {
                    MessageBox.Show("Số dư ban đầu không hợp lệ!", "Lỗi nhập liệu",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBalance.Focus();
                    return;
                }

                tk = new TaiKhoan
                {
                    SoTaiKhoan = txtAccountNumber.Text.Trim(),
                    MaKH = cboCustomer.SelectedValue?.ToString(),
                    MaNganHang = cboBank.SelectedValue?.ToString(),
                    MaLoaiTien = cboCurrency.SelectedValue?.ToString(),
                    SoDuBanDau = soDuMoi,
                    SoDuHienTai = soDuMoi,
                    NgayMo = dtpOpenDate.Value,
                    TrangThai = cboStatus.Text
                };
            }
            else
            {
                tk = new TaiKhoan
                {
                    SoTaiKhoan = txtAccountNumber.Text.Trim(),
                    MaKH = _currentMaKH,
                    MaNganHang = cboBank.SelectedValue?.ToString(),
                    MaLoaiTien = cboCurrency.SelectedValue?.ToString(),
                    SoDuBanDau = _currentSoDuBanDau,
                    SoDuHienTai = _currentSoDuHienTai,
                    NgayMo = dtpOpenDate.Value,
                    TrangThai = cboStatus.Text
                };
            }

            try
            {
                bool ok = _isAddNew
                    ? _AccountService.AddTaiKhoan(tk)
                    : _AccountService.UpdateTaiKhoan(tk);

                if (ok)
                {
                    string msg = _isAddNew ? "Thêm tài khoản thành công!" : "Cập nhật thành công!";
                    MessageBox.Show(msg, "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(txtSearch.Text);
                    ClearInputs();
                    _isAddNew = false;
                    SetControlState(false);
                }
                else
                {
                    MessageBox.Show("Lưu thất bại! Số tài khoản có thể đã trùng.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}