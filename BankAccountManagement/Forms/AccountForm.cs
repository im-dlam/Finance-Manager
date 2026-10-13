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
    /// - Member: chỉ xem, thêm, sửa và đóng tài khoản của chính mình.
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
        private Dictionary<string, string> _customerNames =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

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
            dgvAccounts.CellFormatting += dgvAccounts_CellFormatting;
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnClear.Click += btnClear_Click;
            btnSearch.Click += btnSearch_Click;

            txtSearch.TextChanged += txtSearch_TextChanged;
            txtSearch.KeyDown += txtSearch_KeyDown;

            // ⭐ Đồng bộ hai combobox khách hàng
            cboCustomer.SelectedIndexChanged += cboCustomer_SelectedIndexChanged;
            cboCustomerCode.SelectedIndexChanged += cboCustomerCode_SelectedIndexChanged;
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            if (CurrentSession.IsAdmin)
            {
                lblSubtitle.Text = "Theo dõi và quản lý tài khoản ngân hàng của khách hàng";
            }
            else if (!string.IsNullOrWhiteSpace(CurrentSession.MaKH))
            {
                lblSubtitle.Text = "Quản lý tài khoản ngân hàng của bạn";
            }
            else
            {
                lblSubtitle.Text = "Bạn chưa hoàn tất hồ sơ khách hàng. Vui lòng cập nhật thông tin cá nhân.";
            }

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
        /// Admin và Member hợp lệ đều được quản lý tài khoản theo phạm vi của mình.
        /// </summary>
        private void ApplyRolePermissions()
        {
            bool canManage = CanManageAccounts;
            btnAdd.Visible = canManage;
            btnEdit.Visible = canManage;
            btnDelete.Visible = canManage;
        }

        private bool CanManageAccounts => CurrentSession.IsAdmin ||
            (CurrentSession.IsMember && !string.IsNullOrWhiteSpace(CurrentSession.MaKH));
        // ==========================================================
        //  ĐỔ DỮ LIỆU COMBOBOX
        // ==========================================================
        private void LoadComboBoxData()
        {
            DataTable dtKH;
            if (!CurrentSession.IsAdmin)
            {
                if (!string.IsNullOrWhiteSpace(CurrentSession.MaKH))
                {
                    // Member: Chỉ tải duy nhất thông tin của chính mình
                    dtKH = GetDataTable(
                        $"SELECT MaKH, HoTen FROM dbo.ThongTinKhachHang WHERE MaKH = '{CurrentSession.MaKH.Replace("'", "''")}'");
                }
                else
                {
                    dtKH = new DataTable();
                    dtKH.Columns.Add("MaKH", typeof(string));
                    dtKH.Columns.Add("HoTen", typeof(string));
                }
            }
            else
            {
                // Admin: Tải toàn bộ khách hàng Active
                dtKH = GetDataTable(
                    "SELECT MaKH, HoTen FROM dbo.ThongTinKhachHang WHERE TrangThai = N'Active' ORDER BY HoTen");
            }

            BindCustomers(dtKH);

            if (CurrentSession.IsMember && !string.IsNullOrEmpty(CurrentSession.MaKH))
            {
                SetCustomerSelection(CurrentSession.MaKH);
            }

            DataTable dtNH = GetDataTable(
                "SELECT MaNganHang, TenNganHang FROM dbo.NganHang ORDER BY TenNganHang");
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
        //  ĐỒNG BỘ HAI COMBOBOX KHÁCH HÀNG
        // ==========================================================
        private bool _syncingCustomer = false; // tránh vòng lặp sự kiện
        private bool _customersBound = false;

        /// <summary>
        /// Gắn dữ liệu và chỉ đồng bộ khi cả hai combobox đã sẵn sàng.
        /// </summary>
        private void BindCustomers(DataTable customers)
        {
            _customersBound = false;

            if (customers == null)
                throw new ArgumentNullException(nameof(customers));

            if (!customers.Columns.Contains("MaKH") ||
                !customers.Columns.Contains("HoTen"))
            {
                throw new InvalidOperationException(
                    "Dữ liệu khách hàng phải có hai cột MaKH và HoTen.");
            }

            _syncingCustomer = true;
            try
            {
                cboCustomer.DataSource = null;
                cboCustomerCode.DataSource = null;

                cboCustomer.DisplayMember = "HoTen";
                cboCustomer.ValueMember = "MaKH";
                cboCustomer.DataSource = customers;

                cboCustomerCode.DisplayMember = "MaKH";
                cboCustomerCode.ValueMember = "MaKH";
                // Nguồn riêng để hai combobox có vị trí chọn độc lập.
                cboCustomerCode.DataSource = customers.Copy();

                _customerNames = BuildLookup(customers, "MaKH", "HoTen");
                _customersBound = true;
            }
            finally
            {
                _syncingCustomer = false;
            }

            SetCustomerSelection(cboCustomer.SelectedValue);
        }

        private void SetCustomerSelection(object maKH)
        {
            if (_syncingCustomer || !_customersBound) return;

            // Không gán SelectedValue khi nguồn dữ liệu hoặc ValueMember chưa sẵn sàng.
            if (cboCustomer.DataSource == null ||
                cboCustomerCode.DataSource == null ||
                string.IsNullOrEmpty(cboCustomer.ValueMember) ||
                string.IsNullOrEmpty(cboCustomerCode.ValueMember))
            {
                return;
            }

            if (maKH is DataRowView row)
                maKH = row["MaKH"];

            if (maKH == null || maKH == DBNull.Value) return;

            _syncingCustomer = true;
            try
            {
                cboCustomer.SelectedValue = maKH;
                cboCustomerCode.SelectedValue = maKH;
            }
            finally
            {
                _syncingCustomer = false;
            }
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_syncingCustomer || !_customersBound) return;
            if (cboCustomer.SelectedValue != null)
                SetCustomerSelection(cboCustomer.SelectedValue);
        }

        private void cboCustomerCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_syncingCustomer || !_customersBound) return;
            if (cboCustomerCode.SelectedValue != null)
                SetCustomerSelection(cboCustomerCode.SelectedValue);
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
                    ConfigureCustomerColumns();
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

        private void ConfigureCustomerColumns()
        {
            DataGridViewColumn nameColumn = dgvAccounts.Columns["colCustomer"];
            if (nameColumn == null)
                throw new InvalidOperationException("Thiếu cột colCustomer trong bảng tài khoản.");

            // Tái sử dụng cột mã đã có trong Designer, kể cả khi tên control khác.
            DataGridViewColumn codeColumn = dgvAccounts.Columns
                .Cast<DataGridViewColumn>()
                .FirstOrDefault(column => column != nameColumn &&
                    (column.Name == "colCustomerCode" ||
                     new string(Normalize(column.HeaderText)
                         .Where(c => !char.IsWhiteSpace(c)).ToArray()) == "makhachhang"));

            if (codeColumn == null)
            {
                codeColumn = new DataGridViewTextBoxColumn { Name = "colCustomerCode" };
                dgvAccounts.Columns.Add(codeColumn);
            }

            codeColumn.HeaderText = "Mã khách hàng";
            codeColumn.DataPropertyName = "MaKH";
            codeColumn.Visible = true;
            codeColumn.ReadOnly = true;
            codeColumn.MinimumWidth = 110;
            codeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            codeColumn.DisplayIndex = Math.Max(0, nameColumn.DisplayIndex - 1);

            nameColumn.HeaderText = "Khách hàng";
            nameColumn.DataPropertyName = "MaKH";
            nameColumn.Visible = true;
            nameColumn.ReadOnly = true;
            nameColumn.MinimumWidth = 160;
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            nameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dgvAccounts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
                dgvAccounts.Columns[e.ColumnIndex].Name != "colCustomer") return;

            TaiKhoan account = dgvAccounts.Rows[e.RowIndex].DataBoundItem as TaiKhoan;
            string customerName;
            e.Value = account != null && account.MaKH != null &&
                _customerNames.TryGetValue(account.MaKH, out customerName)
                ? customerName : "Chưa có tên khách hàng";
            e.FormattingApplied = true;
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
            if (!CanManageAccounts)
            {
                txtAccountNumber.Enabled = false;
                cboCustomer.Enabled = false;
                cboCustomerCode.Enabled = false;
                txtBalance.Enabled = false;
                cboBank.Enabled = false;
                cboCurrency.Enabled = false;
                dtpOpenDate.Enabled = false;
                cboStatus.Enabled = false;
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                return;
            }

            // Chỉ mở khi THÊM MỚI
            txtAccountNumber.Enabled = isEditing && _isAddNew;
            // Member không được đổi chủ tài khoản sang khách hàng khác.
            cboCustomer.Enabled = isEditing && _isAddNew && CurrentSession.IsAdmin;
            cboCustomerCode.Enabled = isEditing && _isAddNew && CurrentSession.IsAdmin;
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

            if (CurrentSession.IsMember && !string.IsNullOrEmpty(CurrentSession.MaKH))
            {
                SetCustomerSelection(CurrentSession.MaKH);
            }
            else if (cboCustomer.Items.Count > 0)
            {
                cboCustomer.SelectedIndex = 0;
                cboCustomerCode.SelectedIndex = 0;
            }

            if (cboBank.Items.Count > 0) cboBank.SelectedIndex = 0;
            if (cboCurrency.Items.Count > 0) cboCurrency.SelectedIndex = 0;
            if (cboStatus.Items.Count > 0) cboStatus.SelectedIndex = 0;
            dtpOpenDate.Value = DateTime.Now;

            _currentMaKH = CurrentSession.IsMember && !string.IsNullOrEmpty(CurrentSession.MaKH)
                            ? CurrentSession.MaKH : "";
            _currentSoDuHienTai = 0;
            _currentSoDuBanDau = 0;
        }

        // ==========================================================
        //  SỰ KIỆN BUTTONS
        // ==========================================================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!CanManageAccounts) return;
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
            if (!CanManageAccounts) return;
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
            if (!CanManageAccounts) return;
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
                    else
                    {
                        MessageBox.Show("Không thể đóng: tài khoản không tồn tại hoặc không thuộc quyền quản lý của bạn.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            DataGridViewRow row = dgvAccounts.Rows[e.RowIndex];
            TaiKhoan account = row.DataBoundItem as TaiKhoan;
            if (account == null) return;

            _currentMaKH = account.MaKH ?? "";
            _currentSoDuHienTai = Convert.ToDecimal(row.Cells["colBalance"].Value ?? 0);
            _currentSoDuBanDau = _currentSoDuHienTai;

            txtAccountNumber.Text = row.Cells["colAccountNumber"].Value?.ToString();
            txtBalance.Text = _currentSoDuHienTai.ToString();

            // ⭐ Đồng bộ cả hai combobox khách hàng
            SetCustomerSelection(
                CurrentSession.IsMember && !string.IsNullOrEmpty(CurrentSession.MaKH)
                    ? (object)CurrentSession.MaKH
                    : account.MaKH);

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
            if (!CanManageAccounts) return;
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

                // ⭐ Lấy MaKH từ cboCustomerCode (hoặc cboCustomer — cùng giá trị)
                string maKH = CurrentSession.IsMember && !string.IsNullOrEmpty(CurrentSession.MaKH)
                    ? CurrentSession.MaKH
                    : cboCustomerCode.SelectedValue?.ToString()
                      ?? cboCustomer.SelectedValue?.ToString();

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
                    MessageBox.Show("Lưu thất bại! Tài khoản không tồn tại hoặc không thuộc quyền quản lý của bạn.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void lblSubtitle_Click(object sender, EventArgs e)
        {

        }
    }
}



  