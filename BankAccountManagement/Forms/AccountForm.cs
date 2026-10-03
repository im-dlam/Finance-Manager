using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
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
            DataTable dtKH = GetDataTable(
                "SELECT MaKH, HoTen FROM KhachHang WHERE TrangThai = 'Active' ORDER BY HoTen");
            cboCustomer.DataSource = dtKH;
            cboCustomer.DisplayMember = "HoTen";
            cboCustomer.ValueMember = "MaKH";

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
                List<TaiKhoan> danhSach = string.IsNullOrWhiteSpace(keyword)
                    ? _AccountService.GetActiveTaiKhoan()
                    : _AccountService.SearchTaiKhoan(keyword);

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

                dgvAccounts.DataSource = danhSach;
                lblListCount.Text = $"{danhSach.Count} tài khoản";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ══════════════════════════════════════════
        // TRẠNG THÁI CONTROLS
        // ══════════════════════════════════════════
        private void SetControlState(bool isEditing)
        {
            // Chỉ mở khi THÊM MỚI
            txtAccountNumber.Enabled = isEditing && _isAddNew;
            cboCustomer.Enabled = isEditing && _isAddNew;
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
            if (cboCustomer.Items.Count > 0) cboCustomer.SelectedIndex = 0;
            if (cboBank.Items.Count > 0) cboBank.SelectedIndex = 0;
            if (cboCurrency.Items.Count > 0) cboCurrency.SelectedIndex = 0;
            if (cboStatus.Items.Count > 0) cboStatus.SelectedIndex = 0;
            dtpOpenDate.Value = DateTime.Now;

            // Reset giá trị gốc
            _currentMaKH = "";
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
                LoadData();
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
                        LoadData();
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
            => LoadData(txtSearch.Text.Trim());

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
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
            cboCustomer.SelectedValue = row.Cells["colCustomer"].Value;
            cboBank.SelectedValue = row.Cells["colBank"].Value;
            cboCurrency.SelectedValue = row.Cells["colCurrency"].Value;

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
                    LoadData();
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