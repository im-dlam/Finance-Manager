using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BankAccountManagement.Services;

namespace BankAccountManagement.Forms
{
    public partial class CustomerForm : Form
    {
        private CustomerService _customerService = new CustomerService();
        private bool isAddNew = false;

        public CustomerForm()
        {
            InitializeComponent();
            
            // Đăng ký các sự kiện (Events) cho các nút bấm và lưới dữ liệu
            this.Load += CustomerForm_Load;
            btnSearch.Click += BtnSearch_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            dgvCustomers.CellClick += DgvCustomers_CellClick;
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            // Vừa vào thì khóa các ô nhập liệu lại
            SetControlState(false);
            // Load dữ liệu lên grid
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = _customerService.GetAllCustomers();
                
                // Bỏ tự động tạo cột thừa
                dgvCustomers.AutoGenerateColumns = false;
                
                // Trói đúng tên cột trong SQL vào các cột trên giao diện
                if(dgvCustomers.Columns.Count > 0)
                {
                    dgvCustomers.Columns["colMaKH"].DataPropertyName = "MaKH";
                    dgvCustomers.Columns["colHoTen"].DataPropertyName = "HoTen";
                    dgvCustomers.Columns["colCMND"].DataPropertyName = "CMND_CCCD";
                    dgvCustomers.Columns["colNgaySinh"].DataPropertyName = "NgaySinh";
                    dgvCustomers.Columns["colGioiTinh"].DataPropertyName = "GioiTinh";
                    dgvCustomers.Columns["colSDT"].DataPropertyName = "SoDienThoai";
                    dgvCustomers.Columns["colEmail"].DataPropertyName = "Email";
                    dgvCustomers.Columns["colDiaChi"].DataPropertyName = "DiaChi";
                }

                // Đổ dữ liệu
                dgvCustomers.DataSource = dt;
                lblRecordCount.Text = $"{dt.Rows.Count} bản ghi";

                // Load 4 thẻ thống kê
                LoadStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStatistics()
        {
            try
            {
                DataTable dtStats = _customerService.GetCustomerStatistics();
                if (dtStats.Rows.Count > 0)
                {
                    lblStat1Value.Text = dtStats.Rows[0]["TongKhachHang"].ToString();
                    lblStat2Value.Text = dtStats.Rows[0]["DangHoatDong"].ToString();
                    lblStat3Value.Text = dtStats.Rows[0]["MoiTrongThang"].ToString();
                    lblStat4Value.Text = dtStats.Rows[0]["TaiKhoanDaMo"].ToString();
                }
            }
            catch { }
        }

        // Hàm bật/tắt các ô nhập liệu
        private void SetControlState(bool isEditing)
        {
            txtMaKH.Enabled = isEditing && isAddNew; // Sửa thì không cho sửa Mã KH
            txtMaKH.ReadOnly = !(isEditing && isAddNew); // Cho phép gõ khi thêm mới
            txtHoTen.Enabled = isEditing;
            txtCMND.Enabled = isEditing;
            dtpNgaySinh.Enabled = isEditing;
            cboGioiTinh.Enabled = isEditing;
            txtSDT.Enabled = isEditing;
            txtEmail.Enabled = isEditing;
            txtDiaChi.Enabled = isEditing;

            btnSave.Enabled = isEditing;
            btnCancel.Enabled = isEditing;

            btnAdd.Enabled = !isEditing;
            btnEdit.Enabled = !isEditing;
            btnDelete.Enabled = !isEditing;
        }

        private void ClearInputs()
        {
            txtMaKH.Clear();
            txtHoTen.Clear();
            txtCMND.Clear();
            dtpNgaySinh.Value = DateTime.Now;
            cboGioiTinh.SelectedIndex = -1;
            txtSDT.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
        }

        // Sự kiện click vào 1 dòng trong DataGridView
        private void DgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];
                txtMaKH.Text = row.Cells["colMaKH"].Value?.ToString();
                txtHoTen.Text = row.Cells["colHoTen"].Value?.ToString();
                txtCMND.Text = row.Cells["colCMND"].Value?.ToString();
                
                if (DateTime.TryParse(row.Cells["colNgaySinh"].Value?.ToString(), out DateTime ns))
                    dtpNgaySinh.Value = ns;
                else
                    dtpNgaySinh.Value = DateTime.Now;

                cboGioiTinh.Text = row.Cells["colGioiTinh"].Value?.ToString();
                txtSDT.Text = row.Cells["colSDT"].Value?.ToString();
                txtEmail.Text = row.Cells["colEmail"].Value?.ToString();
                txtDiaChi.Text = row.Cells["colDiaChi"].Value?.ToString();
            }
        }

        // Nút Tìm kiếm
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim();
                DataTable dt = _customerService.SearchCustomers(keyword);
                dgvCustomers.DataSource = dt;
                lblRecordCount.Text = $"{dt.Rows.Count} bản ghi";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        // Nút Refresh
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadData();
            ClearInputs();
            SetControlState(false);
        }

        // Nút Thêm mới
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            isAddNew = true;
            ClearInputs();
            SetControlState(true);
            txtMaKH.Focus();
        }


        // Nút Sửa
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn một khách hàng bên trái để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            isAddNew = false;
            SetControlState(true);
        }

        // Nút Xóa
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bool success = _customerService.DeleteCustomer(txtMaKH.Text);
                    if (success)
                    {
                        MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                }
                catch (SqlException ex) when (ex.Number == 547)
                {
                    MessageBox.Show("Không thể xóa khách hàng này vì khách hàng đang có Tài khoản hoặc Lịch sử giao dịch!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Nút Lưu lại
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text) || string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtCMND.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã KH, Họ Tên và CMND/CCCD!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = false;
                if (isAddNew)
                {
                    success = _customerService.AddCustomer(
                        txtMaKH.Text.Trim(), txtHoTen.Text.Trim(), txtCMND.Text.Trim(),
                        dtpNgaySinh.Value, cboGioiTinh.Text, txtSDT.Text.Trim(),
                        txtEmail.Text.Trim(), txtDiaChi.Text.Trim()
                    );
                }
                else
                {
                    success = _customerService.UpdateCustomer(
                        txtMaKH.Text.Trim(), txtHoTen.Text.Trim(), txtCMND.Text.Trim(),
                        dtpNgaySinh.Value, cboGioiTinh.Text, txtSDT.Text.Trim(),
                        txtEmail.Text.Trim(), txtDiaChi.Text.Trim()
                    );
                }

                if (success)
                {
                    MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetControlState(false);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Không thể lưu. Vui lòng kiểm tra lại (có thể trùng Mã KH hoặc CMND).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (System.Data.SqlClient.SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                    MessageBox.Show("Mã khách hàng đã tồn tại! Vui lòng nhập mã khác.", "Lỗi trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show("Số CMND/CCCD đã tồn tại! Vui lòng kiểm tra lại.", "Lỗi trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Hủy bỏ
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            SetControlState(false);
            ClearInputs();
            if (dgvCustomers.CurrentRow != null)
            {
                DgvCustomers_CellClick(dgvCustomers, new DataGridViewCellEventArgs(0, dgvCustomers.CurrentRow.Index));
            }
        }
    }

}
