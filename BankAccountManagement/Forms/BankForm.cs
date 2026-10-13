using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    public partial class BankForm : Form
    {
        private readonly NganHangService _nganHangService = new NganHangService();
        private bool _isAddNew = false;

        public BankForm()
        {
            InitializeComponent();
            this.Load += BankForm_Load;
            btnAdd.Click += BtnAdd_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnReset.Click += BtnReset_Click;
            dgvBank.CellClick += DgvBank_CellClick;
        }

        // ══════════════════════════════════════════
        // KHỞI ĐỘNG
        // ══════════════════════════════════════════
        private void BankForm_Load(object sender, EventArgs e)
        {
            try
            {
                ApplyRolePermissions();
                LoadData();
                SetControlState(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi động: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyRolePermissions()
        {
            if (!CurrentSession.IsAdmin)
            {
                btnAdd.Visible = true;
                btnEdit.Visible = false;
                btnDelete.Visible = true;
            }
            else
            {
                btnAdd.Visible = true;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
        }

        // ══════════════════════════════════════════
        // TẢI DỮ LIỆU LÊN LƯỚI
        // ══════════════════════════════════════════
        private void LoadData(string keyword = "")
        {
            try
            {
                List<NganHang> danhSach = string.IsNullOrWhiteSpace(keyword)
                    ? _nganHangService.GetAllNganHang()
                    : _nganHangService.SearchNganHang(keyword);

                dgvBank.Rows.Clear();

                for (int i = 0; i < danhSach.Count; i++)
                {
                    dgvBank.Rows.Add(
                        i + 1,                        // STT
                        danhSach[i].MaNganHang,       // MNH
                        danhSach[i].TenNganHang,      // TNH
                        danhSach[i].ChiNhanh,         // CN
                        danhSach[i].SwiftCode         // SC
                    );
                }
                lblListCount.Text = $"{danhSach.Count} ngân hàng";
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
            bool coTheNhapMa = isEditing && _isAddNew;

            // Mã ngân hàng: chỉ nhập khi THÊM MỚI
            txtBankCode.ReadOnly = !coTheNhapMa;
            txtBankCode.BackColor = coTheNhapMa
                ? System.Drawing.Color.White
                : System.Drawing.Color.FromArgb(240, 240, 240);

            // Các ô còn lại
            txtBankName.ReadOnly = !isEditing;
            txtBankName.BackColor = isEditing
                ? System.Drawing.Color.White
                : System.Drawing.Color.FromArgb(240, 240, 240);

            txtBranch.ReadOnly = !isEditing;
            txtBranch.BackColor = isEditing
                ? System.Drawing.Color.White
                : System.Drawing.Color.FromArgb(240, 240, 240);

            txtSwiftCode.ReadOnly = !isEditing;
            txtSwiftCode.BackColor = isEditing
                ? System.Drawing.Color.White
                : System.Drawing.Color.FromArgb(240, 240, 240);

            // Nút Thêm toggle
            btnAdd.Visible = true;
            btnAdd.Text = isEditing && _isAddNew
                ? "💾 Lưu ngân hàng"
                : "+  Thêm ngân hàng";

            if (CurrentSession.IsAdmin)
            {
                // Nút Sửa toggle (Chỉ hiển thị cho Admin)
                btnEdit.Visible = true;
                btnEdit.Location = new System.Drawing.Point(30, 434);
                btnEdit.Size = new System.Drawing.Size(172, 42);
                btnEdit.Text = isEditing && !_isAddNew
                    ? "💾 Lưu chỉnh sửa"
                    : "✎  Sửa ngân hàng";

                // Nút Xóa
                btnDelete.Visible = true;
                btnDelete.Location = new System.Drawing.Point(213, 434);
                btnDelete.Size = new System.Drawing.Size(172, 42);
            }
            else
            {
                // Khi là User: chỉ có Thêm và Xóa, ẩn nút Sửa hoàn toàn
                btnEdit.Visible = false;

                btnDelete.Visible = true;
                btnDelete.Location = new System.Drawing.Point(30, 434);
                btnDelete.Size = new System.Drawing.Size(355, 42);
            }

            // Khóa nút khi đang nhập
            btnDelete.Enabled = !isEditing;
            btnReset.Enabled = true;
        }

        private void ClearInputs()
        {
            txtBankCode.Clear();
            txtBankName.Clear();
            txtBranch.Clear();
            txtSwiftCode.Clear();
        }

        // ══════════════════════════════════════════
        // CLICK VÀO DÒNG LƯỚI
        // ══════════════════════════════════════════
        private void DgvBank_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvBank.Rows[e.RowIndex];
            txtBankCode.Text = row.Cells["MNH"].Value?.ToString();
            txtBankName.Text = row.Cells["TNH"].Value?.ToString();
            txtBranch.Text = row.Cells["CN"].Value?.ToString();
            txtSwiftCode.Text = row.Cells["SC"].Value?.ToString();

            // Reset về trạng thái xem
            _isAddNew = false;
            SetControlState(false);
        }

        // ══════════════════════════════════════════
        // NÚT THÊM / LƯU (toggle)
        // ══════════════════════════════════════════
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text.Contains("Thêm"))
            {
                // → Chuyển sang chế độ THÊM MỚI
                _isAddNew = true;
                ClearInputs();
                SetControlState(true);
                txtBankCode.ReadOnly = false;
                txtBankCode.BackColor = System.Drawing.Color.White;
                txtBankCode.Focus();
            }
            else
            {
                // → Lưu thêm mới
                ThucHienLuu();
            }
        }

        // ══════════════════════════════════════════
        // NÚT SỬA / LƯU (toggle)
        // ══════════════════════════════════════════
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text.Contains("Sửa"))
            {
                // → Kiểm tra đã chọn chưa
                if (string.IsNullOrEmpty(txtBankCode.Text))
                {
                    MessageBox.Show("Vui lòng chọn ngân hàng từ danh sách để sửa!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // → Chuyển sang chế độ SỬA
                _isAddNew = false;
                SetControlState(true);
                txtBankName.Focus();
            }
            else
            {
                // → Lưu chỉnh sửa
                ThucHienLuu();
            }
        }

        // ══════════════════════════════════════════
        // NÚT XÓA
        // ══════════════════════════════════════════
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBankCode.Text))
            {
                MessageBox.Show("Vui lòng chọn ngân hàng cần xóa!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(
                    $"Xác nhận xóa ngân hàng:\n{txtBankCode.Text} - {txtBankName.Text}?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                try
                {
                    if (_nganHangService.DeleteNganHang(txtBankCode.Text))
                    {
                        MessageBox.Show("Xóa ngân hàng thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                        SetControlState(false);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("547") || ex.Message.Contains("REFERENCE"))
                        MessageBox.Show(
                            "Không thể xóa!\nNgân hàng này đang có tài khoản liên kết.",
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ══════════════════════════════════════════
        // NÚT LÀM MỚI
        // ══════════════════════════════════════════
        private void BtnReset_Click(object sender, EventArgs e)
        {
            ClearInputs();
            LoadData();
            _isAddNew = false;
            SetControlState(false);
        }

        // ══════════════════════════════════════════
        // XỬ LÝ LƯU (THÊM / SỬA)
        // ══════════════════════════════════════════
        private void ThucHienLuu()
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtBankCode.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã ngân hàng!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBankCode.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBankName.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên ngân hàng!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBankName.Focus();
                return;
            }

            NganHang nh = new NganHang
            {
                MaNganHang = txtBankCode.Text.Trim(),
                TenNganHang = txtBankName.Text.Trim(),
                ChiNhanh = txtBranch.Text.Trim(),
                SwiftCode = txtSwiftCode.Text.Trim()
            };

            try
            {
                bool ok = _isAddNew
                    ? _nganHangService.AddNganHang(nh)
                    : _nganHangService.UpdateNganHang(nh);

                if (ok)
                {
                    MessageBox.Show(
                        _isAddNew ? "Thêm ngân hàng thành công!" : "Cập nhật thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputs();
                    SetControlState(false);
                }
                else
                {
                    MessageBox.Show("Lưu thất bại! Mã ngân hàng có thể đã trùng.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

