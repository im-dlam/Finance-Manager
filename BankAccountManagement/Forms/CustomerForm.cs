using System;

using System.Collections.Generic;

using System.ComponentModel;

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

        private static readonly System.Drawing.Font _dgvBtnFont = new System.Drawing.Font("Segoe UI", 8.5f, System.Drawing.FontStyle.Bold);

        private int _lastSplitWidth = -1;

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
            dgvCustomers.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvCustomers.AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
            dgvCustomers.GridColor = System.Drawing.Color.White;
            dgvCustomers.RowPrePaint += (s, ev) => { ev.PaintParts &= ~DataGridViewPaintParts.Focus; };

        }

        private void CustomerForm_Load(object sender, EventArgs e) 

        { 

            isAdmin = BankAccountManagement.Services.CurrentSession.IsAdmin; 

            pnlToolbar.Visible = true;

            pnlToolbar.Height = 56;

            pnlStats.Visible = true;

            pnlStats.Height = 88;

            if (isAdmin) 

            { 

                lblGridTitle.Text = "Danh sách tài khoản (Người dùng)";

                lblDetailTitle.Text = "Thông tin tài khoản";

                lblDetailSubtitle.Text = "Quản lý thông tin tài khoản người dùng";

                lblStat1Title.Text = "TỔNG NGƯỜI DÙNG";

                lblMaKH.Text = "Mã ND";

                lblHoTen.Text = "Tên đăng nhập";

                lblCMND.Text = "Họ tên";

                lblNgaySinh.Visible = false;

                dtpNgaySinh.Visible = false;

                lblGioiTinh.Text = "Quyền";

                cboGioiTinh.Items.Clear();

                cboGioiTinh.Items.Add("Admin");

                cboGioiTinh.Items.Add("Member");

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

                // Chiều cao các hàng chứa TextBox/ComboBox

                tlpDetail.RowStyles[1].Height = 36; // txtMaKH

                tlpDetail.RowStyles[4].Height = 36; // txtHoTen

                tlpDetail.RowStyles[7].Height = 36; // txtCMND

                tlpDetail.RowStyles[13].Height = 36; // cboGioiTinh

                tlpDetail.RowStyles[19].Height = 36; // txtEmail

                // Trang trí TextBox và ComboBox: Ẩn viền và tô màu nền xám nhạt (modern flat design)

                System.Drawing.Color modernBg = System.Drawing.Color.FromArgb(241, 245, 249); // Slate 100

                System.Drawing.Font modernFont = new System.Drawing.Font("Segoe UI", 12F);

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

                pnlDetailBanner.BackColor = System.Drawing.Color.White;

                pnlDetailBanner.FillColor = System.Drawing.Color.White;

                pnlDetailBanner.BorderColor = System.Drawing.Color.Transparent;

                pnlDetailBanner.BorderSize = 0;

                btnCancel.NormalColor = System.Drawing.Color.FromArgb(226, 232, 240);

                btnCancel.HoverColor = System.Drawing.Color.FromArgb(203, 213, 225);

                btnCancel.PressedColor = System.Drawing.Color.FromArgb(148, 163, 184);

                btnCancel.TextColor = System.Drawing.Color.FromArgb(71, 85, 105);

                btnCancel.Text = "Hủy bỏ";

                btnSave.NormalColor = System.Drawing.Color.FromArgb(226, 232, 240);

                btnSave.HoverColor = System.Drawing.Color.FromArgb(203, 213, 225);

                btnSave.PressedColor = System.Drawing.Color.FromArgb(148, 163, 184);

                btnSave.TextColor = System.Drawing.Color.FromArgb(71, 85, 105);

                btnSave.Text = "Lưu lại";

                if (!dgvCustomers.Columns.Contains("colTrangThai"))

                {

                    DataGridViewTextBoxColumn colTrangThai = new DataGridViewTextBoxColumn(); 

                    colTrangThai.Name = "colTrangThai"; 

                    colTrangThai.DataPropertyName = "TrangThai"; 

                    colTrangThai.HeaderText = "Trạng Thái"; 

                    colTrangThai.Width = 75; 

                    colTrangThai.MinimumWidth = 68;

                    colTrangThai.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                    dgvCustomers.Columns.Add(colTrangThai); 

                }

                if (!dgvCustomers.Columns.Contains("colLock"))

                {

                    DataGridViewButtonColumn btnLockColumn = new DataGridViewButtonColumn(); 

                    btnLockColumn.Name = "colLock"; 

                    btnLockColumn.HeaderText = "Khóa"; 

                    btnLockColumn.Text = "Khóa"; 

                    btnLockColumn.UseColumnTextForButtonValue = true; 

                    btnLockColumn.Width = 48;

                    btnLockColumn.MinimumWidth = 46;

                    btnLockColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; 

                    btnLockColumn.FlatStyle = FlatStyle.Flat; 

                    btnLockColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgvCustomers.Columns.Add(btnLockColumn); 

                }

                if (!dgvCustomers.Columns.Contains("colUnlock"))

                {

                    DataGridViewButtonColumn btnUnlockColumn = new DataGridViewButtonColumn(); 

                    btnUnlockColumn.Name = "colUnlock"; 

                    btnUnlockColumn.HeaderText = "Mở"; 

                    btnUnlockColumn.Text = "Mở"; 

                    btnUnlockColumn.UseColumnTextForButtonValue = true; 

                    btnUnlockColumn.Width = 48;

                    btnUnlockColumn.MinimumWidth = 46;

                    btnUnlockColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; 

                    btnUnlockColumn.FlatStyle = FlatStyle.Flat; 

                    btnUnlockColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgvCustomers.Columns.Add(btnUnlockColumn); 

                }

            } 

            else 

            {

                lblStat1Title.Text = "TỔNG KHÁCH HÀNG";

                lblGridTitle.Text = "Danh sách Khách hàng";

                lblDetailTitle.Text = "Thông tin khách hàng";

                lblDetailSubtitle.Text = "Cập nhật thông tin khách hàng";

                dgvCustomers.Columns["colMaKH"].Visible = false;

                lblMaKH.Visible = false;

                txtMaKH.Visible = false;

            }

            SetDoubleBuffered(this, true);

            SetDoubleBuffered(dgvCustomers, true);

            SetDoubleBuffered(pnlGridCard, true);

            SetDoubleBuffered(pnlDetailCard, true);

            txtSearch.KeyDown += (s, ev) => {

                if (ev.KeyCode == Keys.Enter) {

                    ev.SuppressKeyPress = true;

                    BtnSearch_Click(s, ev);

                }

            };

            AdjustSplitter();

            this.Resize += (s, ev) => AdjustSplitter();

            SetControlState(false);

            LoadData();

                        // Select default row (first row or row 3 if multiple exist)
            if (dgvCustomers.Rows.Count > 0)
            {
                try
                {
                    int selectIdx = (isAdmin && dgvCustomers.Rows.Count > 3) ? 3 : 0;
                    dgvCustomers.ClearSelection();
                    dgvCustomers.Rows[selectIdx].Selected = true;
                    DgvCustomers_CellClick(dgvCustomers, new DataGridViewCellEventArgs(0, selectIdx));
                }
                catch { }
            }


        }

        private void SetControlState(bool editing)

        {

            txtMaKH.ReadOnly = true;

            txtHoTen.ReadOnly = !editing;

            txtCMND.ReadOnly = !editing;

            dtpNgaySinh.Enabled = editing;

            cboGioiTinh.Enabled = editing;

            txtSDT.ReadOnly = !editing;

            txtEmail.ReadOnly = !editing;

            txtDiaChi.ReadOnly = !editing;

            btnEdit.Enabled = !editing;

            btnDelete.Enabled = !editing;

            btnSave.Enabled = editing;

            btnCancel.Enabled = editing;

            dgvCustomers.Enabled = !editing;

            if (editing)

            {

                btnSave.NormalColor = System.Drawing.Color.FromArgb(37, 99, 235);

                btnSave.TextColor = System.Drawing.Color.White;

                btnCancel.NormalColor = System.Drawing.Color.FromArgb(241, 245, 249);

                btnCancel.TextColor = System.Drawing.Color.FromArgb(71, 85, 105);

            }

            else

            {

                btnSave.NormalColor = System.Drawing.Color.FromArgb(226, 232, 240);

                btnSave.TextColor = System.Drawing.Color.FromArgb(71, 85, 105);

                btnCancel.NormalColor = System.Drawing.Color.FromArgb(226, 232, 240);

                btnCancel.TextColor = System.Drawing.Color.FromArgb(71, 85, 105);

            }

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

                    dgvCustomers.Columns["colMaKH"].Width = 48;

                    dgvCustomers.Columns["colMaKH"].MinimumWidth = 45;

                    dgvCustomers.Columns["colMaKH"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                    dgvCustomers.Columns["colHoTen"].DataPropertyName = "TenDangNhap";

                    dgvCustomers.Columns["colHoTen"].HeaderText = "Tên đăng nhập";

                    dgvCustomers.Columns["colHoTen"].MinimumWidth = 92;

                    dgvCustomers.Columns["colHoTen"].FillWeight = 34F;

                    dgvCustomers.Columns["colHoTen"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dgvCustomers.Columns["colCMND"].DataPropertyName = "HoTen";

                    dgvCustomers.Columns["colCMND"].HeaderText = "Họ tên";

                    dgvCustomers.Columns["colCMND"].MinimumWidth = 80;

                    dgvCustomers.Columns["colCMND"].FillWeight = 32F;

                    dgvCustomers.Columns["colCMND"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dgvCustomers.Columns["colNgaySinh"].DataPropertyName = "Quyen";

                    dgvCustomers.Columns["colNgaySinh"].HeaderText = "Quyền";

                    dgvCustomers.Columns["colNgaySinh"].Width = 55;

                    dgvCustomers.Columns["colNgaySinh"].MinimumWidth = 50;

                    dgvCustomers.Columns["colNgaySinh"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                    dgvCustomers.Columns["colGioiTinh"].DataPropertyName = "Email";

                    dgvCustomers.Columns["colGioiTinh"].HeaderText = "Email";

                    dgvCustomers.Columns["colGioiTinh"].MinimumWidth = 85;

                    dgvCustomers.Columns["colGioiTinh"].FillWeight = 34F;

                    dgvCustomers.Columns["colGioiTinh"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    dgvCustomers.Columns["colSDT"].DataPropertyName = "NgayTao";

                    dgvCustomers.Columns["colSDT"].HeaderText = "Ngày tạo";

                    dgvCustomers.Columns["colSDT"].Width = 120;

                    dgvCustomers.Columns["colSDT"].MinimumWidth = 110;

                    dgvCustomers.Columns["colSDT"].DefaultCellStyle.Format = "M/d/yyyy h:mm tt";

                    dgvCustomers.Columns["colSDT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                    dgvCustomers.Columns["colEmail"].Visible = false;

                    dgvCustomers.Columns["colDiaChi"].Visible = false;

                    if (dgvCustomers.Columns.Contains("colTrangThai"))

                    {

                        dgvCustomers.Columns["colTrangThai"].Width = 75;

                        dgvCustomers.Columns["colTrangThai"].MinimumWidth = 68;

                        dgvCustomers.Columns["colTrangThai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                    }

                    if (dgvCustomers.Columns.Contains("colLock"))

                    {

                        dgvCustomers.Columns["colLock"].Width = 48;

                        dgvCustomers.Columns["colLock"].MinimumWidth = 46;

                        dgvCustomers.Columns["colLock"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                        dgvCustomers.Columns["colLock"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    }

                    if (dgvCustomers.Columns.Contains("colUnlock"))

                    {

                        dgvCustomers.Columns["colUnlock"].Width = 48;

                        dgvCustomers.Columns["colUnlock"].MinimumWidth = 46;

                        dgvCustomers.Columns["colUnlock"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                        dgvCustomers.Columns["colUnlock"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    }

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
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string colName = dgvCustomers.Columns[e.ColumnIndex].Name;
                if (isAdmin && (colName == "colLock" || colName == "colUnlock"))
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.Background);

                    bool isLock = (colName == "colLock");
                    System.Drawing.Color btnColor = isLock ? System.Drawing.Color.FromArgb(254, 129, 127) : System.Drawing.Color.FromArgb(37, 99, 235);
                    System.Drawing.Color textColor = System.Drawing.Color.White;
                    string text = isLock ? "Khóa" : "Mở";
                    int btnWidth = 40;
                    int btnHeight = 25;
                    int btnX = e.CellBounds.X + (e.CellBounds.Width - btnWidth) / 2;
                    int btnY = e.CellBounds.Y + (e.CellBounds.Height - btnHeight) / 2;
                    System.Drawing.Rectangle btnRect = new System.Drawing.Rectangle(btnX, btnY, btnWidth, btnHeight);
                    int radius = 7;

                    using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        path.AddArc(btnRect.X, btnRect.Y, radius, radius, 180, 90);
                        path.AddArc(btnRect.Right - radius, btnRect.Y, radius, radius, 270, 90);
                        path.AddArc(btnRect.Right - radius, btnRect.Bottom - radius, radius, radius, 0, 90);
                        path.AddArc(btnRect.X, btnRect.Bottom - radius, radius, radius, 90, 90);
                        path.CloseFigure();

                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                        using (System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(btnColor))
                        {
                            e.Graphics.FillPath(brush, path);
                        }
                    }

                    TextRenderer.DrawText(e.Graphics, text, _dgvBtnFont, btnRect, textColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
                    e.Handled = true;
                }
                else
                {
                    // Loại bỏ hoàn toàn vạch kẻ ô (Border) và viền focus (Focus) để bảng phẳng và sạch sẽ
                    DataGridViewPaintParts parts = e.PaintParts & ~DataGridViewPaintParts.Border & ~DataGridViewPaintParts.Focus;
                    e.Paint(e.CellBounds, parts);
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

                                _customerService.SetUserStatus(ma, "Locked"); 

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

                                _customerService.SetUserStatus(ma, "Active"); 

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

        private void AdjustSplitter()

        {

            if (splitMain == null || splitMain.IsDisposed || splitMain.Width <= 0) return;

            try

            {

                int totalWidth = splitMain.Width;

                if (totalWidth == _lastSplitWidth) return;

                _lastSplitWidth = totalWidth;

                int desiredPanel2Width = 340; // Thẻ thông tin bên phải rộng 340px (card 318px), vừa vặn các trường và nút bấm

                int minPanel1 = Math.Max(380, splitMain.Panel1MinSize);

                int minPanel2 = Math.Max(280, splitMain.Panel2MinSize);

                int splitterW = splitMain.SplitterWidth;

                int targetDistance = totalWidth - desiredPanel2Width - splitterW;

                if (targetDistance < minPanel1)

                {

                    targetDistance = totalWidth - minPanel2 - splitterW;

                }

                if (targetDistance >= minPanel1 && targetDistance <= totalWidth - minPanel2)

                {

                    if (splitMain.SplitterDistance != targetDistance)

                    {

                        splitMain.SplitterDistance = targetDistance;

                    }

                }

            }

            catch { }

        }

        private static void SetDoubleBuffered(Control control, bool enable = true)

        {

            if (control == null) return;

            try

            {

                var prop = typeof(Control).GetProperty("DoubleBuffered",

                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                prop?.SetValue(control, enable, null);

            }

            catch { }

        }

    }

}
