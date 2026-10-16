using BankAccountManagement.Forms;
using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Giao diện Quản lý Sản phẩm Mua sắm dành cho Quản trị viên (Admin).
    /// Hỗ trợ thêm mới sản phẩm, chọn ảnh từ máy, đặt giá bán, tồn kho, sửa và xóa sản phẩm.
    /// </summary>
    public partial class ShopAdminForm : Form
    {
        private readonly ShopService _shopService;
        private List<Product> _allProducts = new List<Product>();
        private List<Product> _filteredProducts = new List<Product>();

        private int _selectedProductId = 0;
        private string _currentImagePath = "📱";

        /// <summary>
        /// Khởi tạo màn hình Quản lý Sản phẩm dành cho Admin (CRUD sản phẩm, chọn hình ảnh, thiết lập giá và tồn kho).
        /// </summary>
        public ShopAdminForm()
        {
            InitializeComponent();
            _shopService = new ShopService();

            SetupDataGridViewColumns();
            WireEvents();

            this.Load += ShopAdminForm_Load;
        }

        private void SetupDataGridViewColumns()
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Columns.Clear();

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMaSP",
                HeaderText = "Mã SP",
                DataPropertyName = "MaSP",
                Width = 65
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTenSP",
                HeaderText = "Tên sản phẩm",
                DataPropertyName = "TenSP",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colGiaBan",
                HeaderText = "Giá bán",
                DataPropertyName = "FormattedPrice",
                Width = 125
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTonKho",
                HeaderText = "Tồn kho",
                DataPropertyName = "SoLuongTon",
                Width = 85
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTrangThai",
                HeaderText = "Trạng thái",
                DataPropertyName = "TrangThai",
                Width = 90
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNgayTao",
                HeaderText = "Ngày tạo",
                DataPropertyName = "NgayTao",
                Width = 95,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
            });
        }

        private void WireEvents()
        {
            btnAddNew.Click += (s, e) => ResetEditor();
            btnRefresh.Click += async (s, e) => await LoadProductsAsync();
            btnReset.Click += (s, e) => ResetEditor();

            txtSearch.TextChanged += TxtSearch_TextChanged;
            dgvProducts.SelectionChanged += DgvProducts_SelectionChanged;

            btnChooseImg.Click += BtnChooseImg_Click;
            btnSave.Click += BtnSave_Click;
            btnDelete.Click += BtnDelete_Click;

            // Format giá tiền khi người dùng gõ
            txtGiaBan.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            };
        }

        private async void ShopAdminForm_Load(object sender, EventArgs e)
        {
            cboTrangThai.SelectedIndex = 0; // Active
            ResetEditor();
            await LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                _allProducts = await _shopService.GetAllProductsAsync(activeOnly: false);
                ApplyFilter();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ShopAdminForm] Load error: {ex.Message}");
            }
        }

        private void ApplyFilter()
        {
            string kw = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(kw))
            {
                _filteredProducts = _allProducts.ToList();
            }
            else
            {
                _filteredProducts = _allProducts.Where(p =>
                    (p.TenSP != null && p.TenSP.ToLower().Contains(kw)) ||
                    p.MaSP.ToString().Contains(kw) ||
                    (p.MoTa != null && p.MoTa.ToLower().Contains(kw))
                ).ToList();
            }

            dgvProducts.DataSource = null;
            dgvProducts.DataSource = _filteredProducts;

            if (_filteredProducts.Count > 0)
            {
                // Chọn lại sản phẩm đang chỉnh sửa nếu còn trong danh sách
                var match = _filteredProducts.FirstOrDefault(p => p.MaSP == _selectedProductId);
                if (match != null)
                {
                    int index = _filteredProducts.IndexOf(match);
                    dgvProducts.Rows[index].Selected = true;
                }
                else
                {
                    dgvProducts.Rows[0].Selected = true;
                }
            }
            else
            {
                ResetEditor();
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void DgvProducts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0) return;

            var row = dgvProducts.SelectedRows[0];
            if (row.DataBoundItem is Product p)
            {
                _selectedProductId = p.MaSP;
                lblEditorTitle.Text = $"Chỉnh sửa SP #{p.MaSP}";
                txtTenSP.Text = p.TenSP;
                txtGiaBan.Text = p.GiaBan.ToString("N0", CultureInfo.InvariantCulture);
                nudTonKho.Value = Math.Max(0, Math.Min(nudTonKho.Maximum, p.SoLuongTon));

                cboTrangThai.SelectedItem = (p.TrangThai == "Active" || p.TrangThai == "Hoạt động") ? "Active" : "Inactive";
                txtThongSo.Text = p.ThongSo ?? "";
                txtMoTa.Text = p.MoTa ?? "";

                _currentImagePath = p.HinhAnh;
                DisplayPreviewImage(_currentImagePath);

                btnDelete.Enabled = true;
                btnSave.Text = "💾  Cập nhật sản phẩm";
            }
        }

        private void DisplayPreviewImage(string pathOrEmoji)
        {
            picPreview.Image = null;
            picPreview.Visible = false;
            lblEmojiPreview.Visible = true;

            if (string.IsNullOrWhiteSpace(pathOrEmoji))
            {
                lblEmojiPreview.Text = "📱";
                return;
            }

            string candidate = pathOrEmoji;
            if (!Path.IsPathRooted(candidate))
            {
                candidate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, candidate);
            }

            if (File.Exists(candidate))
            {
                try
                {
                    using (var fs = new FileStream(candidate, FileMode.Open, FileAccess.Read))
                    {
                        picPreview.Image = Image.FromStream(fs);
                    }
                    picPreview.Visible = true;
                    lblEmojiPreview.Visible = false;
                    return;
                }
                catch { }
            }

            // Nếu không phải file ảnh, hiển thị emoji/icon
            lblEmojiPreview.Text = pathOrEmoji.Length <= 4 ? pathOrEmoji : "📦";
        }

        /// <summary>
        /// Mở hộp thoại chọn tệp ảnh từ máy tính, sao chép vào thư mục Images/Products và cập nhật xem trước.
        /// </summary>
        private void BtnChooseImg_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh sản phẩm";
                ofd.Filter = "Ảnh sản phẩm (*.png;*.jpg;*.jpeg;*.webp)|*.png;*.jpg;*.jpeg;*.webp|Tất cả tệp (*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string targetDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "Products");
                        if (!Directory.Exists(targetDir))
                        {
                            Directory.CreateDirectory(targetDir);
                        }

                        string ext = Path.GetExtension(ofd.FileName);
                        string fileName = $"prod_{DateTime.Now:yyyyMMdd_HHmmss}_{new Random().Next(100, 999)}{ext}";
                        string destPath = Path.Combine(targetDir, fileName);

                        File.Copy(ofd.FileName, destPath, overwrite: true);

                        // Lưu đường dẫn tương đối để dễ quản lý
                        _currentImagePath = Path.Combine("Images", "Products", fileName);
                        DisplayPreviewImage(_currentImagePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Không thể tải ảnh: {ex.Message}", "Lỗi tải ảnh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Lưu thông tin sản phẩm: Thêm mới nếu chưa chọn sản phẩm nào, hoặc Cập nhật nếu đang sửa sản phẩm có sẵn.
        /// </summary>
        private async void BtnSave_Click(object sender, EventArgs e)
        {
            string name = txtTenSP.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSP.Focus();
                return;
            }

            string rawPrice = txtGiaBan.Text.Replace(",", "").Replace(".", "").Trim();
            if (!decimal.TryParse(rawPrice, out decimal price) || price <= 0)
            {
                MessageBox.Show("Giá bán không hợp lệ. Vui lòng nhập số tiền lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaBan.Focus();
                return;
            }

            int stock = (int)nudTonKho.Value;
            string status = cboTrangThai.SelectedItem?.ToString() ?? "Active";
            string specs = txtThongSo.Text.Trim();
            string desc = txtMoTa.Text.Trim();

            var p = new Product
            {
                MaSP = _selectedProductId,
                TenSP = name,
                GiaBan = price,
                SoLuongTon = stock,
                TrangThai = status,
                ThongSo = specs,
                MoTa = desc,
                HinhAnh = _currentImagePath
            };

            btnSave.Enabled = false;
            bool success;

            if (_selectedProductId == 0)
            {
                // Thêm mới sản phẩm
                success = await _shopService.AddProductAsync(p);
                if (success)
                {
                    MessageBox.Show($"Đã thêm sản phẩm \"{name}\" thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetEditor();
                    await LoadProductsAsync();
                }
                else
                {
                    MessageBox.Show("Không thể thêm sản phẩm. Vui lòng kiểm tra lại cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Cập nhật sản phẩm
                success = await _shopService.UpdateProductAsync(p);
                if (success)
                {
                    MessageBox.Show($"Đã cập nhật sản phẩm \"{name}\" thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadProductsAsync();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật sản phẩm. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            btnSave.Enabled = true;
        }

        /// <summary>
        /// Xóa vĩnh viễn sản phẩm đang chọn sau khi xác nhận với người dùng.
        /// </summary>
        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedProductId <= 0) return;

            string prodName = txtTenSP.Text.Trim();
            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sản phẩm #{_selectedProductId} (\"{prodName}\") khỏi hệ thống?\nThao tác này không thể hoàn tác.",
                "Xác nhận xóa sản phẩm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                btnDelete.Enabled = false;
                bool success = await _shopService.DeleteProductAsync(_selectedProductId);
                if (success)
                {
                    MessageBox.Show($"Đã xóa sản phẩm \"{prodName}\" thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetEditor();
                    await LoadProductsAsync();
                }
                else
                {
                    MessageBox.Show("Không thể xóa sản phẩm. Có thể có dữ liệu liên quan.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDelete.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Đặt lại toàn bộ các ô nhập liệu về trạng thái thêm mới ban đầu.
        /// </summary>
        private void ResetEditor()
        {
            _selectedProductId = 0;
            lblEditorTitle.Text = "Thêm sản phẩm mới";
            txtTenSP.Text = "";
            txtGiaBan.Text = "";
            nudTonKho.Value = 100;
            cboTrangThai.SelectedIndex = 0; // Active
            txtThongSo.Text = "";
            txtMoTa.Text = "";
            _currentImagePath = "📱";
            DisplayPreviewImage(_currentImagePath);

            btnDelete.Enabled = false;
            btnSave.Text = "💾  Lưu sản phẩm mới";
            txtTenSP.Focus();
        }
    }
}
