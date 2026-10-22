using BankAccountManagement.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Form quản lý khách hàng / người dùng trong hệ thống Bank Account Management.
    /// Hỗ trợ hiển thị linh hoạt theo vai trò (Role-based UI):
    /// - Quyền Admin: Quản lý danh sách tài khoản người dùng hệ thống (Mã ND, Tên đăng nhập, Họ tên, Quyền, Email, Trạng thái, Khóa/Mở tài khoản).
    /// - Quyền Member: Quản lý hồ sơ thông tin cá nhân khách hàng (Mã KH, Họ tên, CMND/CCCD, Ngày sinh, Giới tính, SĐT, Email, Địa chỉ).
    /// </summary>
    public partial class CustomerForm : Form
    {
        #region KHAI BÁO BIẾN THÀNH VIÊN (MEMBER VARIABLES)

        /// <summary>Đối tượng service xử lý nghiệp vụ và truy vấn CSDL cho khách hàng.</summary>
        private readonly CustomerService _customerService = new CustomerService();

        /// <summary>Cờ đánh dấu trạng thái thêm mới (true) hay cập nhật (false).</summary>
        private bool isAddNew = false;

        /// <summary>Cờ kiểm tra xem phiên hiện tại có phải là Quản trị viên (Admin) hay không.</summary>
        private bool isAdmin = false;

        /// <summary>Font chữ dành riêng cho nút bấm "Khóa" / "Mở" vẽ tùy biến trên bảng DataGridView.</summary>
        private static readonly Font _dgvBtnFont = new Font("Segoe UI", 8.5f, FontStyle.Bold);

        /// <summary>Ghi nhớ độ rộng trước đó của SplitContainer để tránh tính toán lại không cần thiết.</summary>
        private int _lastSplitWidth = -1;

        #endregion

        #region KHỞI TẠO FORM & GẮN SỰ KIỆN (CONSTRUCTOR & SETUP)

        /// <summary>
        /// Khởi tạo màn hình Quản lý khách hàng / người dùng.
        /// </summary>
        public CustomerForm()
        {
            InitializeComponent();

            // Đăng ký sự kiện nạp form
            this.Load += CustomerForm_Load;

            // Đăng ký sự kiện cho các nút chức năng trên thanh công cụ và thẻ chi tiết
            btnSearch.Click += BtnSearch_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;

            // Đăng ký sự kiện click chọn hàng và vẽ giao diện tùy biến cho bảng DataGridView
            dgvCustomers.CellClick += DgvCustomers_CellClick;
            dgvCustomers.CellPainting += DgvCustomers_CellPainting;

            // Bỏ toàn bộ đường viền/vạch kẻ bảng (Borderless Design) để giao diện phẳng và thanh lịch
            dgvCustomers.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvCustomers.AdvancedCellBorderStyle.All = DataGridViewAdvancedCellBorderStyle.None;
            dgvCustomers.GridColor = Color.White;
            dgvCustomers.RowPrePaint += (s, ev) => { ev.PaintParts &= ~DataGridViewPaintParts.Focus; };
        }

        /// <summary>
        /// Sự kiện kích hoạt khi Form bắt đầu được tải lên bộ nhớ.
        /// Phân loại giao diện Admin vs Member, thiết lập độ rộng cột và nạp dữ liệu.
        /// </summary>
        private void CustomerForm_Load(object sender, EventArgs e)
        {
            // BƯỚC 1: Xác định quyền của người dùng hiện tại
            isAdmin = CurrentSession.IsAdmin;

            pnlToolbar.Visible = true;
            pnlToolbar.Height = 56;
            pnlStats.Visible = true;
            pnlStats.Height = 88;

            // BƯỚC 2: Cấu hình giao diện đặc thù theo vai trò
            if (isAdmin)
            {
                // Tiêu đề dành cho Admin
                lblGridTitle.Text = "Danh sách tài khoản (Người dùng)";
                lblDetailTitle.Text = "Thông tin tài khoản";
                lblDetailSubtitle.Text = "Quản lý thông tin tài khoản người dùng";
                lblStat1Title.Text = "TỔNG NGƯỜI DÙNG";

                // Nhãn các trường nhập liệu
                lblMaKH.Text = "Mã ND";
                lblHoTen.Text = "Tên đăng nhập";
                lblCMND.Text = "Họ tên";
                lblGioiTinh.Text = "Quyền";
                lblEmail.Text = "Email";

                // Ẩn các trường không áp dụng cho tài khoản đăng nhập hệ thống
                lblNgaySinh.Visible = false;
                dtpNgaySinh.Visible = false;
                lblSDT.Visible = false;
                txtSDT.Visible = false;
                lblDiaChi.Visible = false;
                txtDiaChi.Visible = false;

                // Tùy chọn quyền trong ComboBox
                cboGioiTinh.Items.Clear();
                cboGioiTinh.Items.Add("Admin");
                cboGioiTinh.Items.Add("Member");

                // Vị trí các control trong thẻ bên phải
                lblGioiTinh.Top = 156;
                cboGioiTinh.Top = 172;
                lblEmail.Top = 208;
                txtEmail.Top = 224;

                // Tối ưu khoảng cách hàng trong TableLayoutPanel (tlpDetail)
                tlpDetail.RowStyles[9].Height = 0;
                tlpDetail.RowStyles[10].Height = 0;
                tlpDetail.RowStyles[11].Height = 0;
                tlpDetail.RowStyles[15].Height = 0;
                tlpDetail.RowStyles[16].Height = 0;
                tlpDetail.RowStyles[17].Height = 0;
                tlpDetail.RowStyles[21].Height = 0;
                tlpDetail.RowStyles[22].Height = 0;

                tlpDetail.RowStyles[2].Height = 16;
                tlpDetail.RowStyles[5].Height = 16;
                tlpDetail.RowStyles[8].Height = 16;
                tlpDetail.RowStyles[14].Height = 16;

                tlpDetail.RowStyles[1].Height = 36; // txtMaKH
                tlpDetail.RowStyles[4].Height = 36; // txtHoTen
                tlpDetail.RowStyles[7].Height = 36; // txtCMND
                tlpDetail.RowStyles[13].Height = 36; // cboGioiTinh
                tlpDetail.RowStyles[19].Height = 36; // txtEmail

                // Màu nền phẳng hiện đại cho các TextBox nhập liệu
                Color modernBg = Color.FromArgb(241, 245, 249);
                Font modernFont = new Font("Segoe UI", 12F);

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

                pnlDetailBanner.BackColor = Color.White;
                pnlDetailBanner.FillColor = Color.White;
                pnlDetailBanner.BorderColor = Color.Transparent;
                pnlDetailBanner.BorderSize = 0;

                btnCancel.NormalColor = Color.FromArgb(226, 232, 240);
                btnCancel.HoverColor = Color.FromArgb(203, 213, 225);
                btnCancel.PressedColor = Color.FromArgb(148, 163, 184);
                btnCancel.TextColor = Color.FromArgb(71, 85, 105);
                btnCancel.Text = "Hủy bỏ";

                btnSave.NormalColor = Color.FromArgb(226, 232, 240);
                btnSave.HoverColor = Color.FromArgb(203, 213, 225);
                btnSave.PressedColor = Color.FromArgb(148, 163, 184);
                btnSave.TextColor = Color.FromArgb(71, 85, 105);
                btnSave.Text = "Lưu lại";

                // Bổ sung cột Trạng thái nếu chưa có
                if (!dgvCustomers.Columns.Contains("colTrangThai"))
                {
                    var colTrangThai = new DataGridViewTextBoxColumn
                    {
                        Name = "colTrangThai",
                        DataPropertyName = "TrangThai",
                        HeaderText = "Trạng Thái",
                        Width = 75,
                        MinimumWidth = 68,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    };
                    dgvCustomers.Columns.Add(colTrangThai);
                }

                // Bổ sung cột nút "Khóa"
                if (!dgvCustomers.Columns.Contains("colLock"))
                {
                    var btnLockColumn = new DataGridViewButtonColumn
                    {
                        Name = "colLock",
                        HeaderText = "Khóa",
                        Text = "Khóa",
                        UseColumnTextForButtonValue = true,
                        Width = 48,
                        MinimumWidth = 46,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                        FlatStyle = FlatStyle.Flat
                    };
                    btnLockColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvCustomers.Columns.Add(btnLockColumn);
                }

                // Bổ sung cột nút "Mở"
                if (!dgvCustomers.Columns.Contains("colUnlock"))
                {
                    var btnUnlockColumn = new DataGridViewButtonColumn
                    {
                        Name = "colUnlock",
                        HeaderText = "Mở",
                        Text = "Mở",
                        UseColumnTextForButtonValue = true,
                        Width = 48,
                        MinimumWidth = 46,
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                        FlatStyle = FlatStyle.Flat
                    };
                    btnUnlockColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvCustomers.Columns.Add(btnUnlockColumn);
                }
            }
            else
            {
                // Cấu hình dành riêng cho người dùng thông thường (Member)
                lblStat1Title.Text = "TỔNG KHÁCH HÀNG";
                lblGridTitle.Text = "Danh sách Khách hàng";
                lblDetailTitle.Text = "Thông tin khách hàng";
                lblDetailSubtitle.Text = "Cập nhật thông tin khách hàng";

                dgvCustomers.Columns["colMaKH"].Visible = false;
                lblMaKH.Visible = false;
                txtMaKH.Visible = false;
            }

            // BƯỚC 3: Bật bộ đệm kép (DoubleBuffer) để giao diện cuộn mượt mà
            SetDoubleBuffered(this, true);
            SetDoubleBuffered(dgvCustomers, true);
            SetDoubleBuffered(pnlGridCard, true);
            SetDoubleBuffered(pnlDetailCard, true);

            // BƯỚC 4: Nhấn Enter tại ô tìm kiếm để kích hoạt tìm kiếm
            txtSearch.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter)
                {
                    ev.SuppressKeyPress = true;
                    BtnSearch_Click(s, ev);
                }
            };

            // BƯỚC 5: Điều chỉnh khoảng cách SplitContainer theo kích cỡ màn hình
            AdjustSplitter();
            this.Resize += (s, ev) => AdjustSplitter();

            // Khóa trạng thái sửa ban đầu
            SetControlState(false);

            // Tải dữ liệu từ CSDL
            LoadData();

            // Tự động chọn dòng mặc định
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

        #endregion

        #region QUẢN LÝ TRẠNG THÁI CONTROL (UI STATE)

        /// <summary>
        /// Bật/Tắt trạng thái cho phép chỉnh sửa của các trường thông tin và nút thao tác.
        /// </summary>
        /// <param name="editing">true: đang ở chế độ chỉnh sửa; false: chỉ xem.</param>
        private void SetControlState(bool editing)
        {
            txtMaKH.ReadOnly = true; // Khóa luôn mã định danh không cho sửa tay
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

            // Cập nhật màu sắc trực quan cho nút Lưu / Hủy
            if (editing)
            {
                btnSave.NormalColor = Color.FromArgb(37, 99, 235); // Xanh dương chủ đạo
                btnSave.TextColor = Color.White;
                btnCancel.NormalColor = Color.FromArgb(241, 245, 249);
                btnCancel.TextColor = Color.FromArgb(71, 85, 105);
            }
            else
            {
                btnSave.NormalColor = Color.FromArgb(226, 232, 240); // Xám nhạt vô hiệu
                btnSave.TextColor = Color.FromArgb(71, 85, 105);
                btnCancel.NormalColor = Color.FromArgb(226, 232, 240);
                btnCancel.TextColor = Color.FromArgb(71, 85, 105);
            }
        }

        /// <summary>Xóa sạch nội dung trong tất cả các ô nhập liệu.</summary>
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

        #endregion

        #region TẢI DỮ LIỆU & THỐNG KÊ (DATA ACCESS)

        /// <summary>
        /// Nạp toàn bộ dữ liệu từ Cơ sở dữ liệu và cấu hình hiển thị các cột trên DataGridView.
        /// </summary>
        private void LoadData()
        {
            try
            {
                DataTable dt = _customerService.GetAllData(isAdmin);
                dgvCustomers.AutoGenerateColumns = false;

                if (isAdmin)
                {
                    dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    // Ánh xạ cột cho tài khoản hệ thống (Admin view)
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
                    // Ánh xạ cột cho thông tin khách hàng (Member view)
                    dgvCustomers.Columns["colMaKH"].DataPropertyName = "MaKH";
                    dgvCustomers.Columns["colHoTen"].DataPropertyName = "HoTen";
                    dgvCustomers.Columns["colCMND"].DataPropertyName = "CMND_CCCD";
                    dgvCustomers.Columns["colNgaySinh"].DataPropertyName = "NgaySinh";
                    dgvCustomers.Columns["colGioiTinh"].DataPropertyName = "GioiTinh";
                    dgvCustomers.Columns["colSDT"].DataPropertyName = "SoDienThoai";
                    dgvCustomers.Columns["colEmail"].DataPropertyName = "Email";
                    dgvCustomers.Columns["colDiaChi"].DataPropertyName = "DiaChi";
                }

                // Gán nguồn dữ liệu vào bảng
                dgvCustomers.DataSource = dt;
                lblRecordCount.Text = dt.Rows.Count.ToString() + " bản ghi";

                // Tải số liệu thống kê nhanh trên các thẻ card đầu trang
                LoadStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tải số liệu thống kê tổng khách hàng, đang hoạt động, mới trong tháng lên các thẻ card phía trên.
        /// </summary>
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
            catch
            {
                // Bỏ qua lỗi thống kê nhẹ để không làm gián đoạn luồng chính
            }
        }

        #endregion

        #region VẼ TÙY BIẾN Ô BẢNG & NÚT KHÓA/MỞ (CUSTOM CELL PAINTING)

        /// <summary>
        /// Sự kiện can thiệp trực tiếp vào chu trình vẽ ô (CellPainting) của DataGridView:
        /// 1. Loại bỏ đường viền (Border) và khung Focus cho tất cả các ô để bảng phẳng hoàn toàn.
        /// 2. Vẽ nút bo tròn "Khóa" (đỏ san hô) và "Mở" (xanh dương) sắc nét cho Admin.
        /// </summary>
        private void DgvCustomers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string colName = dgvCustomers.Columns[e.ColumnIndex].Name;

                // Xử lý vẽ nút Khóa và Mở
                if (isAdmin && (colName == "colLock" || colName == "colUnlock"))
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.Background);

                    bool isLock = (colName == "colLock");
                    Color btnColor = isLock ? Color.FromArgb(254, 129, 127) : Color.FromArgb(37, 99, 235);
                    Color textColor = Color.White;
                    string text = isLock ? "Khóa" : "Mở";
                    int btnWidth = 40;
                    int btnHeight = 25;
                    int btnX = e.CellBounds.X + (e.CellBounds.Width - btnWidth) / 2;
                    int btnY = e.CellBounds.Y + (e.CellBounds.Height - btnHeight) / 2;
                    var btnRect = new Rectangle(btnX, btnY, btnWidth, btnHeight);
                    int radius = 7;

                    // Vẽ hình chữ nhật bo góc làm nền nút
                    using (var path = new GraphicsPath())
                    {
                        path.AddArc(btnRect.X, btnRect.Y, radius, radius, 180, 90);
                        path.AddArc(btnRect.Right - radius, btnRect.Y, radius, radius, 270, 90);
                        path.AddArc(btnRect.Right - radius, btnRect.Bottom - radius, radius, radius, 0, 90);
                        path.AddArc(btnRect.X, btnRect.Bottom - radius, radius, radius, 90, 90);
                        path.CloseFigure();

                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                        using (var brush = new SolidBrush(btnColor))
                        {
                            e.Graphics.FillPath(brush, path);
                        }
                    }

                    // Vẽ chữ nằm căn giữa nút
                    TextRenderer.DrawText(e.Graphics, text, _dgvBtnFont, btnRect, textColor,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);

                    e.Handled = true; // Báo cho hệ thống biết ô này đã được tự vẽ hoàn chỉnh
                }
                else
                {
                    // Loại bỏ hoàn toàn vạch kẻ ô (Border) và viền focus (Focus) để bảng sạch đẹp
                    DataGridViewPaintParts parts = e.PaintParts & ~DataGridViewPaintParts.Border & ~DataGridViewPaintParts.Focus;
                    e.Paint(e.CellBounds, parts);
                    e.Handled = true;
                }
            }
        }

        #endregion

        #region XỬ LÝ CHỌN DÒNG & THAO TÁC KHÓA/MỞ (ROW CLICK & ACTIONS)

        /// <summary>
        /// Xử lý khi người dùng click vào một ô trong bảng:
        /// - Nếu click vào nút Khóa/Mở: xác nhận và thay đổi trạng thái trong CSDL.
        /// - Nếu click vào dòng thông thường: hiển thị dữ liệu chi tiết lên thẻ bên phải.
        /// </summary>
        private void DgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // 1. Kiểm tra nếu click vào cột nút Khóa hoặc Mở
                if (isAdmin && (dgvCustomers.Columns[e.ColumnIndex].Name == "colLock" || dgvCustomers.Columns[e.ColumnIndex].Name == "colUnlock"))
                {
                    string ma = dgvCustomers.Rows[e.RowIndex].Cells["colMaKH"].Value?.ToString();
                    string hoTen = dgvCustomers.Rows[e.RowIndex].Cells["colCMND"].Value?.ToString();
                    string trangThai = dgvCustomers.Rows[e.RowIndex].Cells["colTrangThai"].Value?.ToString();
                    string colName = dgvCustomers.Columns[e.ColumnIndex].Name;

                    if (colName == "colLock")
                    {
                        if (trangThai == "Locked" || trangThai == "Inactive")
                        {
                            MessageBox.Show("Tài khoản này đã bị khóa từ trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (MessageBox.Show($"Bạn có chắc chắn muốn KHÓA tài khoản '{hoTen}' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try
                            {
                                _customerService.SetUserStatus(ma, "Locked");
                                MessageBox.Show("Khóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                        if (MessageBox.Show($"Bạn có chắc chắn muốn MỞ KHÓA tài khoản '{hoTen}' không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try
                            {
                                _customerService.SetUserStatus(ma, "Active");
                                MessageBox.Show("Mở khóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    return;
                }

                // 2. Click dòng thông thường: Đổ dữ liệu lên các ô chi tiết bên phải
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

        #endregion

        #region THAO TÁC TÌM KIẾM & LÀM MỚI (SEARCH & REFRESH)

        /// <summary>
        /// Tìm kiếm khách hàng / tài khoản theo từ khóa (họ tên, username, email, mã ND).
        /// </summary>
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
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Làm mới dữ liệu và xóa bộ lọc tìm kiếm.
        /// </summary>
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadData();
            ClearInputs();
            SetControlState(false);
        }

        #endregion

        #region THÊM, SỬA, XÓA, LƯU (CRUD OPERATIONS)

        /// <summary>
        /// Chuyển giao diện sang chế độ Sửa thông tin mục đang chọn.
        /// </summary>
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

        /// <summary>
        /// Xóa mục đang chọn khỏi cơ sở dữ liệu sau khi người dùng xác nhận.
        /// </summary>
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
                    // Mã lỗi 547: Vi phạm ràng buộc khóa ngoại Foreign Key (dữ liệu đang có liên kết với bảng khác)
                    MessageBox.Show("Không thể xóa do ràng buộc dữ liệu (FK)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Lưu thông tin vừa thêm mới hoặc chỉnh sửa vào CSDL.
        /// </summary>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtCMND.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool success;
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

        /// <summary>
        /// Hủy bỏ chế độ sửa và hoàn nguyên dữ liệu hiển thị.
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            SetControlState(false);
            ClearInputs();
            if (dgvCustomers.CurrentRow != null)
            {
                DgvCustomers_CellClick(dgvCustomers, new DataGridViewCellEventArgs(0, dgvCustomers.CurrentRow.Index));
            }
        }

        #endregion

        #region ĐIỀU CHỈNH LAYOUT & TỐI ƯU HIỆU NĂNG (RESPONSIVE & PERFORMANCE)

        /// <summary>
        /// Tự động tính toán vị trí thanh chia (SplitterDistance) để thẻ thông tin bên phải rộng cố định 340px,
        /// còn bảng danh sách bên trái co giãn linh hoạt theo độ rộng màn hình.
        /// </summary>
        private void AdjustSplitter()
        {
            if (splitMain == null || splitMain.IsDisposed || splitMain.Width <= 0) return;

            try
            {
                int totalWidth = splitMain.Width;
                if (totalWidth == _lastSplitWidth) return;
                _lastSplitWidth = totalWidth;

                int desiredPanel2Width = 340; // Thẻ thông tin bên phải rộng 340px vừa vặn
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

        /// <summary>
        /// Bật thuộc tính DoubleBuffered thông qua Reflection cho các control WinForms để chống nháy hình.
        /// </summary>
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

        #endregion
    }
}
