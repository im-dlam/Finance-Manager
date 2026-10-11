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
        private bool isAdmin = false;

        public CustomerForm()
        {
            InitializeComponent();
            
            this.Load += CustomerForm_Load;
            btnSearch.Click += BtnSearch_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnSave.Click += BtnSave_Click;
                        btnCancel.Click += BtnCancel_Click;
            dgvCustomers.CellClick += DgvCustomers_CellClick;
            dgvCustomers.CellPainting += DgvCustomers_CellPainting;
        }

        private void CustomerForm_Load(object sender, EventArgs e) 
        { 
            isAdmin = BankAccountManagement.Services.CurrentSession.IsAdmin; 

            if (isAdmin) 
            { 
                lblGridTitle.Text = "Danh sách tài khoản (Người dùng)";
                lblDetailTitle.Text = "Thông tin tài khoản";
                lblDetailSubtitle.Text = "Quản lý thông tin tài khoản người dùng";
                
                lblMaKH.Text = "Mã ND";
                lblHoTen.Text = "Tên đăng nhập";
                lblCMND.Text = "Họ tên";
                lblNgaySinh.Visible = false;
                dtpNgaySinh.Visible = false;
                lblGioiTinh.Text = "Quyền";
                cboGioiTinh.Items.Clear();
                cboGioiTinh.Items.Add("Admin");
                cboGioiTinh.Items.Add("Member");
                // Đã cấu hình FlatStyle bên dưới
                lblSDT.Visible = false;
                txtSDT.Visible = false;
                lblGioiTinh.Top = 156;
                cboGioiTinh.Top = 172;
                lblEmail.Top = 208;
                txtEmail.Top = 224;
                lblEmail.Text = "Email";
                lblDiaChi.Visible = false;
                txtDiaChi.Visible = false;

                // Ẩn khoảng trống trong TableLayoutPanel
                tlpDetail.RowStyles[9].Height = 0;
                tlpDetail.RowStyles[10].Height = 0;
                tlpDetail.RowStyles[11].Height = 0;
                tlpDetail.RowStyles[15].Height = 0;
                tlpDetail.RowStyles[16].Height = 0;
                tlpDetail.RowStyles[17].Height = 0;
                tlpDetail.RowStyles[21].Height = 0;
                tlpDetail.RowStyles[22].Height = 0;

                // Tăng khoảng cách (gap) giữa các hàng còn lại cho hài hòa
                tlpDetail.RowStyles[2].Height = 16;
                tlpDetail.RowStyles[5].Height = 16;
                tlpDetail.RowStyles[8].Height = 16;
                tlpDetail.RowStyles[14].Height = 16;

                // Tăng chiều cao của các hàng chứa TextBox/ComboBox để vừa với Font size 14
                tlpDetail.RowStyles[1].Height = 40; // txtMaKH
                tlpDetail.RowStyles[4].Height = 40; // txtHoTen
                tlpDetail.RowStyles[7].Height = 40; // txtCMND
                tlpDetail.RowStyles[13].Height = 40; // cboGioiTinh
                tlpDetail.RowStyles[19].Height = 40; // txtEmail

                // Trang trí TextBox và ComboBox: Ẩn viền và tô màu nền xám nhạt (modern flat design)
                System.Drawing.Color modernBg = System.Drawing.Color.FromArgb(241, 245, 249); // Slate 100
                System.Drawing.Font modernFont = new System.Drawing.Font("Segoe UI", 14F);

                txtMaKH.BorderStyle = BorderStyle.None;
                txtMaKH.BackColor = modernBg;
                txtMaKH.Font = modernFont;

                txtHoTen.BorderStyle = BorderStyle.None;
                txtHoTen.BackColor = modernBg;
                txtHoTen.Font = modernFont;

                txtCMND.BorderStyle = BorderStyle.None;
                txtCMND.BackColor = modernBg;
                txtCMND.Font = modernFont;

                txtEmail.BorderStyle = BorderStyle.None;
                txtEmail.BackColor = modernBg;
                txtEmail.Font = modernFont;

                cboGioiTinh.FlatStyle = FlatStyle.Flat;
                cboGioiTinh.BackColor = modernBg;
                cboGioiTinh.Font = modernFont;

                DataGridViewTextBoxColumn colTrangThai = new DataGridViewTextBoxColumn(); 
                colTrangThai.Name = "colTrangThai"; 
                colTrangThai.DataPropertyName = "TrangThai"; 
                colTrangThai.HeaderText = "Trạng Thái"; 
                colTrangThai.Width = 100; 
                colTrangThai.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvCustomers.Columns.Add(colTrangThai); 

                DataGridViewButtonColumn btnLockColumn = new DataGridViewButtonColumn(); 
                btnLockColumn.Name = "colLock"; 
                btnLockColumn.HeaderText = "Khóa"; 
                btnLockColumn.Text = "Khóa"; 
                btnLockColumn.UseColumnTextForButtonValue = true; 
                btnLockColumn.Width = 80;
                btnLockColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; 
                btnLockColumn.FlatStyle = FlatStyle.Flat; 
                                                                                
                dgvCustomers.Columns.Add(btnLockColumn); 

                // Thêm cột spacer để cách 2 nút ra
                DataGridViewTextBoxColumn colSpacer = new DataGridViewTextBoxColumn();
                colSpacer.Name = "colSpacer";
                colSpacer.Width = 10;
                colSpacer.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                colSpacer.ReadOnly = true;
                dgvCustomers.Columns.Add(colSpacer);

                DataGridViewButtonColumn btnUnlockColumn = new DataGridViewButtonColumn(); 
                btnUnlockColumn.Name = "colUnlock"; 
                btnUnlockColumn.HeaderText = "Mở"; 
                btnUnlockColumn.Text = "Mở"; 
                btnUnlockColumn.UseColumnTextForButtonValue = true; 
                btnUnlockColumn.Width = 80;
                btnUnlockColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; 
                btnUnlockColumn.FlatStyle = FlatStyle.Flat; 
                                                                                
                dgvCustomers.Columns.Add(btnUnlockColumn);
            } 
            else 
            {
                lblGridTitle.Text = "Danh sách Khách hàng";
                lblDetailTitle.Text = "Thông tin khách hàng";
                lblDetailSubtitle.Text = "Cập nhật thông tin khách hàng";
                
                dgvCustomers.Columns["colMaKH"].Visible = false;
                lblMaKH.Visible = false;
                txtMaKH.Visible = false;
            }

            SetControlState(false);
            LoadData();
        }

        private void SetControlState(bool editing)
        {
            txtMaKH.Enabled = editing && isAddNew && !isAdmin;
            txtHoTen.Enabled = editing && (!isAdmin || isAddNew);
            txtCMND.Enabled = editing;
            dtpNgaySinh.Enabled = editing;
            cboGioiTinh.Enabled = editing;
            txtSDT.Enabled = editing;
            txtEmail.Enabled = editing;
            txtDiaChi.Enabled = editing;

            btnEdit.Enabled = !editing;
            btnDelete.Enabled = !editing;
            btnSave.Enabled = editing;
            btnCancel.Enabled = editing;
            dgvCustomers.Enabled = !editing;
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = _customerService.GetAllData(isAdmin);
                dgvCustomers.AutoGenerateColumns = false;
                
                if (isAdmin)
                {
                    dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    
                    dgvCustomers.Columns["colMaKH"].DataPropertyName = "MaNguoiDung";
                    dgvCustomers.Columns["colMaKH"].HeaderText = "Mã ND";
                    dgvCustomers.Columns["colMaKH"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    dgvCustomers.Columns["colHoTen"].DataPropertyName = "TenDangNhap";
                    dgvCustomers.Columns["colHoTen"].HeaderText = "Tên đăng nhập";
                    dgvCustomers.Columns["colHoTen"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dgvCustomers.Columns["colCMND"].DataPropertyName = "HoTen";
                    dgvCustomers.Columns["colCMND"].HeaderText = "Họ tên";
                    dgvCustomers.Columns["colCMND"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dgvCustomers.Columns["colNgaySinh"].DataPropertyName = "Quyen";
                    dgvCustomers.Columns["colNgaySinh"].HeaderText = "Quyền";
                    dgvCustomers.Columns["colNgaySinh"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    dgvCustomers.Columns["colGioiTinh"].DataPropertyName = "Email";
                    dgvCustomers.Columns["colGioiTinh"].HeaderText = "Email";
                    dgvCustomers.Columns["colGioiTinh"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dgvCustomers.Columns["colSDT"].DataPropertyName = "NgayTao";
                    dgvCustomers.Columns["colSDT"].HeaderText = "Ngày tạo";
                    dgvCustomers.Columns["colSDT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    dgvCustomers.Columns["colEmail"].Visible = false;
                    dgvCustomers.Columns["colDiaChi"].Visible = false;
                }
                else
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

                dgvCustomers.DataSource = dt;
                lblRecordCount.Text = dt.Rows.Count.ToString() + " bản ghi";

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
                DataTable dtStats = _customerService.GetCustomerStatistics(isAdmin);
                if (dtStats.Rows.Count > 0)
                {
                    lblStat1Value.Text = dtStats.Rows[0]["TongKhachHang"].ToString();
                    lblStat2Value.Text = dtStats.Rows[0]["DangHoatDong"].ToString();
                    lblStat3Value.Text = dtStats.Rows[0]["MoiTrongThang"].ToString();
                    lblStat4Value.Text = dtStats.Rows[0]["TaiKhoanDaMo"].ToString();
                }
            }
            catch { /* bo qua loi thong ke */ }
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

                private void DgvCustomers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && isAdmin)
            {
                string colName = dgvCustomers.Columns[e.ColumnIndex].Name;
                if (colName == "colLock" || colName == "colUnlock")
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);
                    
                    bool isLock = (colName == "colLock");
                    System.Drawing.Color btnColor = isLock ? System.Drawing.Color.FromArgb(255, 128, 128) : System.Drawing.Color.FromArgb(37, 99, 235);
                    System.Drawing.Color textColor = isLock ? System.Drawing.Color.FromArgb(71, 85, 105) : System.Drawing.Color.White;
                    string text = isLock ? "Khóa" : "Mở";
                    
                    int margin = 5;
                    System.Drawing.Rectangle btnRect = new System.Drawing.Rectangle(e.CellBounds.X + margin, e.CellBounds.Y + margin, e.CellBounds.Width - (margin * 2), e.CellBounds.Height - (margin * 2));
                    
                    int radius = 10;
                    using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        path.AddArc(btnRect.X, btnRect.Y, radius, radius, 180, 90);
                        path.AddArc(btnRect.Right - radius, btnRect.Y, radius, radius, 270, 90);
                        path.AddArc(btnRect.Right - radius, btnRect.Bottom - radius, radius, radius, 0, 90);
                        path.AddArc(btnRect.X, btnRect.Bottom - radius, radius, radius, 90, 90);
                        path.CloseFigure();
                        
                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        using (System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(btnColor))
                        {
                            e.Graphics.FillPath(brush, path);
                        }
                    }
                    
                    TextRenderer.DrawText(e.Graphics, text, new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold), btnRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    
                    e.Handled = true;
                }
            }
        }

        private void DgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e) 
        { 
            if (e.RowIndex >= 0) 
            { 
                if (isAdmin && (dgvCustomers.Columns[e.ColumnIndex].Name == "colLock" || dgvCustomers.Columns[e.ColumnIndex].Name == "colUnlock")) 
                { 
                    string ma = dgvCustomers.Rows[e.RowIndex].Cells["colMaKH"].Value.ToString(); 
                    string hoTen = dgvCustomers.Rows[e.RowIndex].Cells["colCMND"].Value.ToString(); 
                    string trangThai = dgvCustomers.Rows[e.RowIndex].Cells["colTrangThai"].Value.ToString();
                    string colName = dgvCustomers.Columns[e.ColumnIndex].Name;

                    if (colName == "colLock")
                    {
                        if (trangThai == "Locked" || trangThai == "Inactive")
                        {
                            MessageBox.Show("Tài khoản này đã bị khóa từ trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (MessageBox.Show("Bạn có chắc chắn muốn KHÓA tài khoản '" + hoTen + "' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                        { 
                            try { 
                                _customerService.ToggleUserStatus(ma); 
                                MessageBox.Show("Khóa tài khoản thành công!", "Thông báo"); 
                                LoadData(); 
                            } catch (Exception ex) { 
                                MessageBox.Show("Lỗi: " + ex.Message); 
                            } 
                        }
                    }
                    else if (colName == "colUnlock")
                    {
                        if (trangThai == "Active")
                        {
                            MessageBox.Show("Tài khoản này đang hoạt động bình thường, không cần mở!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (MessageBox.Show("Bạn có chắc chắn muốn MỞ KHÓA tài khoản '" + hoTen + "' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                        { 
                            try { 
                                _customerService.ToggleUserStatus(ma); 
                                MessageBox.Show("Mở khóa tài khoản thành công!", "Thông báo"); 
                                LoadData(); 
                            } catch (Exception ex) { 
                                MessageBox.Show("Lỗi: " + ex.Message); 
                            } 
                        }
                    }
                    return; 
                }

                DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];
                if (isAdmin)
                {
                    txtMaKH.Text = row.Cells["colMaKH"].Value?.ToString();
                    txtHoTen.Text = row.Cells["colHoTen"].Value?.ToString();
                    txtCMND.Text = row.Cells["colCMND"].Value?.ToString();
                    cboGioiTinh.Text = row.Cells["colNgaySinh"].Value?.ToString();
                    txtEmail.Text = row.Cells["colGioiTinh"].Value?.ToString();
                }
                else
                {
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
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim();
                DataTable dt = _customerService.SearchData(keyword, isAdmin);
                dgvCustomers.DataSource = dt;
                lblRecordCount.Text = dt.Rows.Count.ToString() + " bản ghi";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadData();
            ClearInputs();
            SetControlState(false);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng chọn một mục bên trái để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            isAddNew = false;
            SetControlState(true);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaKH.Text) && !isAdmin)
            {
                MessageBox.Show("Vui lòng chọn mục để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bool success = isAdmin ? _customerService.DeleteAdminUser(txtMaKH.Text) : _customerService.DeleteCustomer(txtMaKH.Text);
                    if (success)
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                }
                catch (SqlException ex) when (ex.Number == 547)
                {
                    MessageBox.Show("Không thể xóa do ràng buộc dữ liệu (FK)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtCMND.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success = false;
                if (isAddNew)
                {
                    success = isAdmin 
                        ? _customerService.AddAdminUser(txtHoTen.Text.Trim(), txtCMND.Text.Trim(), cboGioiTinh.Text, txtEmail.Text.Trim())
                        : _customerService.AddCustomer(txtMaKH.Text.Trim(), txtHoTen.Text.Trim(), txtCMND.Text.Trim(), dtpNgaySinh.Value, cboGioiTinh.Text, txtSDT.Text.Trim(), txtEmail.Text.Trim(), txtDiaChi.Text.Trim());
                }
                else
                {
                    success = isAdmin
                        ? _customerService.UpdateAdminUser(txtMaKH.Text.Trim(), txtHoTen.Text.Trim(), txtCMND.Text.Trim(), cboGioiTinh.Text, txtEmail.Text.Trim())
                        : _customerService.UpdateCustomer(txtMaKH.Text.Trim(), txtHoTen.Text.Trim(), txtCMND.Text.Trim(), dtpNgaySinh.Value, cboGioiTinh.Text, txtSDT.Text.Trim(), txtEmail.Text.Trim(), txtDiaChi.Text.Trim());
                }

                if (success)
                {
                    MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetControlState(false);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Không thể lưu. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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











