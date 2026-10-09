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
    public partial class AccountForm : Form
    {
        
        private readonly AccountService _AccountService = new AccountService();
        private bool _isAddNew = false;

        // ✅ Lưu lại giá trị gốc khi chọn dòng → tránh mất data khi field bị disabled
        private string _currentMaKH = "";
        private decimal _currentSoDuHienTai = 0;
        private decimal _currentSoDuBanDau = 0;

        // ✅ Danh sách gốc để lọc tìm kiếm trong bộ nhớ
        private List<TaiKhoan> _allAccounts = new List<TaiKhoan>();

        // ══════════════════════════════════════════
        // KẾT NỐI DB
        // ══════════════════════════════════════════
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

        // ══════════════════════════════════════════
        // KHỞI TẠO
        // ══════════════════════════════════════════
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

            // ✅ Tìm kiếm khi gõ và khi nhấn Enter
            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.KeyDown += txtSearch_KeyDown;
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            Console.WriteLine($"CurrentSession.IsAdmin: {CurrentSession.IsAdmin}");

            if (CurrentSession.IsAdmin)
                return;
            try
            {
                LoadComboBoxData();
                LoadData();
                SetControlState(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi động: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ══════════════════════════════════════════
        // ĐỔ DỮ LIỆU COMBOBOX
        // ══════════════════════════════════════════
        private void LoadComboBoxData()
        {
            DataTable dtKH;
            if (CurrentSession.IsMember && !string.IsNullOrEmpty(CurrentSession.MaKH))
            {
                // Member: Chỉ tải duy nhất thông tin của chính mình
                dtKH = GetDataTable(
                    $"SELECT MaKH, HoTen FROM KhachHang WHERE MaKH = '{CurrentSession.MaKH.Replace("'", "''")}'");
            }
            else
            {
                // Admin: Tải toàn bộ khách hàng Active
                dtKH = GetDataTable(
                    "SELECT MaKH, HoTen FROM KhachHang WHERE TrangThai = 'Active' ORDER BY HoTen");
            }

            cboCustomer.DataSource = dtKH;
            cboCustomer.DisplayMember = "HoTen";
            cboCustomer.ValueMember = "MaKH";

            if (CurrentSession.IsMember && !string.IsNullOrEmpty(CurrentSession.MaKH))
            {
                cboCustomer.SelectedValue = CurrentSession.MaKH;
            }

            DataTable dtNH = GetDataTable(
                "SELECT MaNganHang, TenNganHang FROM NganHang ORDER BY TenNganHang");
            cboBank.DataSource = dtNH;
            cboBank.DisplayMember = "TenNganHang";
            cboBank.ValueMember = "MaNganHang";

            DataTable dtLT = GetDataTable(
                "SELECT MaLoaiTien, TenLoaiTien FROM LoaiTien ORDER BY TenLoaiTien");
            cboCurrency.DataSource = dtLT;
            cboCurrency.DisplayMember = "TenLoaiTien";
            cboCurrency.ValueMember = "MaLoaiTien";

            cboStatus.Items.Clear();
            cboStatus.Items.Add("Active");
            cboStatus.Items.Add("Closed");
            cboStatus.SelectedIndex = 0;
        }

        // ══════════════════════════════════════════
        // TẢI DANH SÁCH TÀI KHOẢN
        // ══════════════════════════════════════════
        private void LoadData(string keyword = "")
        {
            try
            {
                if (CurrentSession.IsMember && !string.IsNullOrEmpty(CurrentSession.MaKH))
                {
                    // Member: Chỉ lấy danh sách tài khoản của chính mình
                    _allAccounts = _AccountService.GetActiveTaiKhoan()?
                        .Where(tk => string.Equals(tk.MaKH, CurrentSession.MaKH, StringComparison.OrdinalIgnoreCase))
                        .ToList() ?? new List<TaiKhoan>();
                }
                else
                {
                    // Admin: Lấy toàn bộ tài khoản trong hệ thống
                    _allAccounts = _AccountService.GetActiveTaiKhoan() ?? new List<TaiKhoan>();
                }

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

        // ══════════════════════════════════════════
        // TÌM KIẾM / LỌC
        // ══════════════════════════════════════════
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
                // Tra tên từ mã → tìm được theo tên KH / tên ngân hàng / tên loại tiền
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

            dgvAccounts.DataSource = null;      // reset để grid luôn refresh
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

        // Bỏ dấu tiếng Việt + chữ thường → gõ "nguyen" vẫn ra "Nguyễn"
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
                e.SuppressKeyPress = true;   // tắt tiếng "bíp"
                ApplyFilter(txtSearch.Text);
            }
        }

        // ══════════════════════════════════════════
        // TRẠNG THÁI CONTROLS
        // ══════════════════════════════════════════
        private void SetControlState(bool isEditing)
        {
            // Chỉ mở khi THÊM MỚI (nếu là Member thì KHÔNG cho đổi khách hàng khác)
            txtAccountNumber.Enabled = isEditing && _isAddNew;
            cboCustomer.Enabled = isEditing && _isAddNew && !CurrentSession.IsMember;
            txtBalance.Enabled = isEditing && _isAddNew;

            // Mở khi THÊM MỚI hoặc CHỈNH SỬA
            cboBank.Enabled = isEditing;
            cboCurrency.Enabled = isEditing;
            dtpOpenDate.Enabled = isEditing;
            cboStatus.Enabled = isEditing;

            if (isEditing && _isAddNew)        // ── Đang THÊM MỚI
            {
                btnAdd.Text = "💾 Lưu tài khoản";
                btnAdd.Enabled = true;
                btnEdit.Text = "❌ Hủy";
                btnEdit.Enabled = true;
                btnDelete.Enabled = false;
            }
            else if (isEditing && !_isAddNew)  // ── Đang CHỈNH SỬA
            {
                btnAdd.Text = "💾 Lưu thay đổi";
                btnAdd.Enabled = true;
                btnEdit.Text = "❌ Hủy chỉnh sửa";
                btnEdit.Enabled = true;
                btnDelete.Enabled = false;
            }
            else                               // ── Chế độ XEM
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

            if (CurrentSession.IsMember && !string.IsNullOrEmpty(CurrentSession.MaKH))
            {
                cboCustomer.SelectedValue = CurrentSession.MaKH;
            }
            else if (cboCustomer.Items.Count > 0)
            {
                cboCustomer.SelectedIndex = 0;
            }

            if (cboBank.Items.Count > 0) cboBank.SelectedIndex = 0;
            if (cboCurrency.Items.Count > 0) cboCurrency.SelectedIndex = 0;
            if (cboStatus.Items.Count > 0) cboStatus.SelectedIndex = 0;
            dtpOpenDate.Value = DateTime.Now;

            // Reset giá trị gốc
            _currentMaKH = (CurrentSession.IsMember && !string.IsNullOrEmpty(CurrentSession.MaKH)) ? CurrentSession.MaKH : "";
            _currentSoDuHienTai = 0;
            _currentSoDuBanDau = 0;
        }

        // ══════════════════════════════════════════
        // SỰ KIỆN BUTTONS
        // ══════════════════════════════════════════
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
                LoadData(txtSearch.Text);   // ✅ Giữ kết quả tìm kiếm
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
                        LoadData(txtSearch.Text);   // ✅ Giữ kết quả tìm kiếm
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
            txtSearch.TextChanged -= txtSearch_TextChanged;  // tránh lọc thừa khi xóa ô tìm
            txtSearch.Clear();
            txtSearch.TextChanged += txtSearch_TextChanged;

            _isAddNew = false;
            ClearInputs();
            LoadData();
            SetControlState(false);
        }

        // ══════════════════════════════════════════
        // CLICK VÀO DÒNG DATAGRIDVIEW
        // ══════════════════════════════════════════
        private void dgvAccounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvAccounts.CurrentRow == null) return;

            DataGridViewRow row = dgvAccounts.CurrentRow;

            // ✅ Lưu lại giá trị gốc trước khi gán lên form
            _currentMaKH = row.Cells["colCustomer"].Value?.ToString() ?? "";
            _currentSoDuHienTai = Convert.ToDecimal(row.Cells["colBalance"].Value ?? 0);
            _currentSoDuBanDau = _currentSoDuHienTai; // fallback nếu không có cột riêng
            txtAccountNumber.Text = row.Cells["colAccountNumber"].Value?.ToString();
            txtBalance.Text = _currentSoDuHienTai.ToString();

            if (CurrentSession.IsMember && !string.IsNullOrEmpty(CurrentSession.MaKH))
            {
                cboCustomer.SelectedValue = CurrentSession.MaKH;
            }
            else
            {
                cboCustomer.SelectedValue = row.Cells["colCustomer"].Value;
            }

            cboBank.SelectedValue = row.Cells["colBank"].Value;
            var currencyValue = row.Cells["colCurrency"].Value;

            if (currencyValue != null && currencyValue != DBNull.Value)
            {
                cboCurrency.SelectedValue = currencyValue;
            }
            else
            {
                cboCurrency.SelectedIndex = -1; // Xóa lựa chọn nếu dữ liệu null
            }

            if (row.Cells["colOpenDate"].Value != null)
                dtpOpenDate.Value = Convert.ToDateTime(row.Cells["colOpenDate"].Value);

            cboStatus.Text = row.Cells["colStatus"].Value?.ToString();

            _isAddNew = false;
            SetControlState(false);
        }

        // ══════════════════════════════════════════
        // LƯU (THÊM MỚI / CẬP NHẬT)
        // ══════════════════════════════════════════
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
                // ── THÊM MỚI: validate và lấy số dư từ form
                if (!decimal.TryParse(txtBalance.Text, out decimal soDuMoi) || soDuMoi < 0)
                {
                    MessageBox.Show("Số dư ban đầu không hợp lệ!", "Lỗi nhập liệu",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtBalance.Focus();
                    return;
                }

                string maKH = (CurrentSession.IsMember && !string.IsNullOrEmpty(CurrentSession.MaKH))
                    ? CurrentSession.MaKH
                    : cboCustomer.SelectedValue?.ToString();

                tk = new TaiKhoan
                {
                    SoTaiKhoan = txtAccountNumber.Text.Trim(),
                    MaKH = maKH,
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
                // ── CHỈNH SỬA: dùng giá trị gốc cho các field bị disabled
                tk = new TaiKhoan
                {
                    SoTaiKhoan = txtAccountNumber.Text.Trim(),
                    MaKH = _currentMaKH,               // ✅ Dùng giá trị gốc
                    MaNganHang = cboBank.SelectedValue?.ToString(),
                    MaLoaiTien = cboCurrency.SelectedValue?.ToString(),
                    SoDuBanDau = _currentSoDuBanDau,         // ✅ Giữ nguyên số dư ban đầu
                    SoDuHienTai = _currentSoDuHienTai,        // ✅ Giữ nguyên số dư hiện tại
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
                    LoadData(txtSearch.Text);   // ✅ Giữ kết quả tìm kiếm
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