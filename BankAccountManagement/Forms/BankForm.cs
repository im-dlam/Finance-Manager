using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Form Quản lý danh mục Ngân hàng đối tác và liên kết hệ thống (Bank Management Form).
    /// <para>
    /// Cho phép Quản trị viên (Admin):
    /// - Xem danh sách các ngân hàng đối tác (Mã ngân hàng, Tên ngân hàng, Chi nhánh, SWIFT Code).
    /// - Thêm mới đối tác ngân hàng vào hệ thống với mã định danh duy nhất.
    /// - Chỉnh sửa thông tin chi nhánh, mã giao dịch quốc tế SWIFT Code của ngân hàng.
    /// - Xóa ngân hàng với cơ chế bảo vệ ràng buộc toàn vẹn khóa ngoại (Foreign Key Constraint 547).
    /// </para>
    /// </summary>
    public partial class BankForm : Form
    {
        #region 1. CÁC BIẾN TRẠNG THÁI VÀ DỊCH VỤ

        /// <summary>Dịch vụ xử lý nghiệp vụ ngân hàng (truy vấn, thêm mới, cập nhật, xóa).</summary>
        private readonly NganHangService _nganHangService = new NganHangService();

        /// <summary>Cờ đánh dấu trạng thái thao tác: true nếu đang thêm ngân hàng mới, false nếu đang chỉnh sửa ngân hàng cũ.</summary>
        private bool _isAddNew = false;

        #endregion

        #region 2. KHỞI TẠO VÀ SỰ KIỆN FORM LOAD

        /// <summary>
        /// Hàm dựng khởi tạo các điều khiển và đăng ký các sự kiện tương tác người dùng.
        /// </summary>
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

        /// <summary>
        /// Sự kiện xảy ra khi mở Form: Thiết lập quyền truy cập, tải danh sách ngân hàng và đặt trạng thái điều khiển ban đầu.
        /// </summary>
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
                MessageBox.Show("Lỗi khởi động giao diện ngân hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Phân quyền người dùng: Quản trị viên được dùng đầy đủ tính năng Thêm/Sửa/Xóa.
        /// </summary>
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

        #endregion

        #region 3. TẢI VÀ HIỂN THỊ DỮ LIỆU LÊN LƯỚI (DATA BINDING)

        /// <summary>
        /// Tải danh sách ngân hàng từ CSDL và hiển thị lên bảng DataGridView.
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm (tùy chọn, để trống sẽ lấy tất cả).</param>
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
                        i + 1,                        // Số thứ tự hiển thị (STT)
                        danhSach[i].MaNganHang,       // Mã ngân hàng (MNH)
                        danhSach[i].TenNganHang,      // Tên ngân hàng (TNH)
                        danhSach[i].ChiNhanh,         // Chi nhánh (CN)
                        danhSach[i].SwiftCode         // Mã giao dịch quốc tế (SC)
                    );
                }
                lblListCount.Text = $"{danhSach.Count} ngân hàng";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách ngân hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 4. QUẢN LÝ TRẠNG THÁI ĐIỀU KHIỂN (UI STATE MACHINE)

        /// <summary>
        /// Thiết lập trạng thái ReadOnly và màu nền của các ô TextBox tùy theo chế độ (Xem / Thêm mới / Chỉnh sửa).
        /// </summary>
        /// <param name="isEditing">True nếu đang ở trạng thái nhập liệu (Thêm hoặc Sửa), False nếu ở trạng thái chỉ xem.</param>
        private void SetControlState(bool isEditing)
        {
            bool coTheNhapMa = isEditing && _isAddNew;

            // Mã ngân hàng: Khóa chính (Primary Key), chỉ cho phép nhập khi tạo mới, không cho sửa khi chỉnh sửa bản ghi cũ
            txtBankCode.ReadOnly = !coTheNhapMa;
            txtBankCode.BackColor = coTheNhapMa
                ? Color.White
                : Color.FromArgb(240, 240, 240);

            // Các ô thông tin bổ sung: Cho phép chỉnh sửa khi isEditing = true
            txtBankName.ReadOnly = !isEditing;
            txtBankName.BackColor = isEditing ? Color.White : Color.FromArgb(240, 240, 240);

            txtBranch.ReadOnly = !isEditing;
            txtBranch.BackColor = isEditing ? Color.White : Color.FromArgb(240, 240, 240);

            txtSwiftCode.ReadOnly = !isEditing;
            txtSwiftCode.BackColor = isEditing ? Color.White : Color.FromArgb(240, 240, 240);

            // Nút "Thêm": Chuyển đổi linh hoạt giữa chức năng Mở form thêm và Lưu thêm mới
            btnAdd.Visible = true;
            btnAdd.Text = isEditing && _isAddNew ? "💾 Lưu ngân hàng" : "+  Thêm ngân hàng";

            if (CurrentSession.IsAdmin)
            {
                btnEdit.Visible = true;
                btnEdit.Location = new Point(30, 434);
                btnEdit.Size = new Size(172, 42);
                btnEdit.Text = isEditing && !_isAddNew ? "💾 Lưu chỉnh sửa" : "✎  Sửa ngân hàng";

                btnDelete.Visible = true;
                btnDelete.Location = new Point(213, 434);
                btnDelete.Size = new Size(172, 42);
            }
            else
            {
                btnEdit.Visible = false;
                btnDelete.Visible = true;
                btnDelete.Location = new Point(30, 434);
                btnDelete.Size = new Size(355, 42);
            }

            // Khi đang nhập liệu thì khóa nút Xóa để tránh xung đột thao tác
            btnDelete.Enabled = !isEditing;
            btnReset.Enabled = true;
        }

        /// <summary>
        /// Xóa trắng nội dung trên các ô nhập liệu.
        /// </summary>
        private void ClearInputs()
        {
            txtBankCode.Clear();
            txtBankName.Clear();
            txtBranch.Clear();
            txtSwiftCode.Clear();
        }

        #endregion

        #region 5. XỬ LÝ SỰ KIỆN TƯƠNG TÁC NGƯỜI DÙNG (USER ACTIONS)

        /// <summary>
        /// Khi người dùng nhấp vào một dòng trên bảng danh sách, hiển thị thông tin ngân hàng lên các ô nhập liệu.
        /// </summary>
        private void DgvBank_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvBank.Rows[e.RowIndex];
            txtBankCode.Text = row.Cells["MNH"].Value?.ToString();
            txtBankName.Text = row.Cells["TNH"].Value?.ToString();
            txtBranch.Text = row.Cells["CN"].Value?.ToString();
            txtSwiftCode.Text = row.Cells["SC"].Value?.ToString();

            // Khôi phục về chế độ xem an toàn
            _isAddNew = false;
            SetControlState(false);
        }

        /// <summary>
        /// Xử lý nút "Thêm": Nhấp lần 1 để chuyển sang chế độ nhập mới, nhấp lần 2 để lưu bản ghi vào CSDL.
        /// </summary>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text.Contains("Thêm"))
            {
                _isAddNew = true;
                ClearInputs();
                SetControlState(true);
                txtBankCode.ReadOnly = false;
                txtBankCode.BackColor = Color.White;
                txtBankCode.Focus();
            }
            else
            {
                ThucHienLuu();
            }
        }

        /// <summary>
        /// Xử lý nút "Sửa": Nhấp lần 1 để mở khóa chỉnh sửa tên, chi nhánh; nhấp lần 2 để lưu thay đổi.
        /// </summary>
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text.Contains("Sửa"))
            {
                if (string.IsNullOrEmpty(txtBankCode.Text))
                {
                    MessageBox.Show("Vui lòng chọn ngân hàng từ danh sách để sửa!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _isAddNew = false;
                SetControlState(true);
                txtBankName.Focus();
            }
            else
            {
                ThucHienLuu();
            }
        }

        /// <summary>
        /// Xử lý nút "Xóa": Xác nhận xóa ngân hàng và kiểm tra ràng buộc toàn vẹn khóa ngoại CSDL.
        /// </summary>
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
                    // Mã lỗi SQL 547 là lỗi vi phạm ràng buộc khóa ngoại (FOREIGN KEY REFERENCE)
                    if (ex.Message.Contains("547") || ex.Message.Contains("REFERENCE"))
                    {
                        MessageBox.Show(
                            "Không thể xóa ngân hàng này!\nĐang có tài khoản hoặc người dùng liên kết với ngân hàng này trong hệ thống.",
                            "Cảnh báo ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Xử lý nút "Làm mới": Xóa sạch các ô nhập liệu và tải lại danh sách đầy đủ từ CSDL.
        /// </summary>
        private void BtnReset_Click(object sender, EventArgs e)
        {
            ClearInputs();
            LoadData();
            _isAddNew = false;
            SetControlState(false);
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ dữ liệu và gọi Service để lưu bản ghi (Thêm mới hoặc Cập nhật).
        /// </summary>
        private void ThucHienLuu()
        {
            // BƯỚC 1: Kiểm tra tính hợp lệ dữ liệu đầu vào
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

            // BƯỚC 2: Khởi tạo đối tượng Model NganHang
            NganHang nh = new NganHang
            {
                MaNganHang = txtBankCode.Text.Trim(),
                TenNganHang = txtBankName.Text.Trim(),
                ChiNhanh = txtBranch.Text.Trim(),
                SwiftCode = txtSwiftCode.Text.Trim()
            };

            // BƯỚC 3: Gọi Service thực thi và xử lý kết quả trả về
            try
            {
                bool ok = _isAddNew
                    ? _nganHangService.AddNganHang(nh)
                    : _nganHangService.UpdateNganHang(nh);

                if (ok)
                {
                    MessageBox.Show(
                        _isAddNew ? "Thêm ngân hàng thành công!" : "Cập nhật ngân hàng thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearInputs();
                    SetControlState(false);
                }
                else
                {
                    MessageBox.Show("Lưu thất bại! Mã ngân hàng có thể đã trùng lặp trong hệ thống.", "Lỗi",
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

        #endregion
    }
}
