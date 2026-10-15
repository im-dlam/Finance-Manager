using BankAccountManagement.Forms;
using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Giao diện Mua sắm (Shopping) dành cho Khách hàng (User/Member).
    /// Hỗ trợ lưới sản phẩm responsive tự động co giãn, xem chi tiết, giỏ hàng popup và thanh toán.
    /// </summary>
    public partial class ShopUserForm : Form
    {
        private readonly ShopService _shopService;
        private List<Product> _allProducts = new List<Product>();
        private List<Product> _displayedProducts = new List<Product>();

        private int _currentCardWidth = 320;
        private const int CardHeight = 310;
        private const int CardMargin = 12;

        /// <summary>
        /// Khởi tạo giao diện Mua sắm (Shopping) dành cho khách hàng với tính năng responsive layout và giỏ hàng thời gian thực.
        /// </summary>
        public ShopUserForm()
        {
            InitializeComponent();
            _shopService = new ShopService();

            this.Load += ShopUserForm_Load;
            this.flpProducts.SizeChanged += FlpProducts_SizeChanged;

            txtSearch.TextChanged += TxtSearch_TextChanged;
            btnCart.Click += BtnCart_Click;

            ShoppingCartService.OnCartChanged += UpdateCartButtonBadge;
            this.FormClosed += (s, e) => ShoppingCartService.OnCartChanged -= UpdateCartButtonBadge;
        }

        /// <summary>
        /// Xử lý sự kiện khi Form tải lên: nạp danh sách sản phẩm và cập nhật huy hiệu số lượng giỏ hàng.
        /// </summary>
        private async void ShopUserForm_Load(object sender, EventArgs e)
        {
            UpdateCartButtonBadge();
            await LoadProductsAsync();
        }

        /// <summary>
        /// Cập nhật số lượng mặt hàng hiển thị trên nút Giỏ hàng góc trên bên phải.
        /// </summary>
        private void UpdateCartButtonBadge()
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;
            this.BeginInvoke((MethodInvoker)delegate
            {
                btnCart.Text = $"🛒  Giỏ hàng ({ShoppingCartService.TotalCount})";
            });
        }

        /// <summary>
        /// Tải danh sách sản phẩm đang mở bán từ cơ sở dữ liệu qua ShopService.
        /// </summary>
        private async Task LoadProductsAsync()
        {
            try
            {
                _allProducts = await _shopService.GetAllProductsAsync(activeOnly: true);
                _displayedProducts = _allProducts.ToList();
                RenderProducts();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ShopUserForm] Load error: {ex.Message}");
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(keyword))
            {
                _displayedProducts = _allProducts.ToList();
            }
            else
            {
                _displayedProducts = _allProducts.Where(p =>
                    (p.TenSP != null && p.TenSP.ToLower().Contains(keyword)) ||
                    (p.MoTa != null && p.MoTa.ToLower().Contains(keyword)) ||
                    (p.ThongSo != null && p.ThongSo.ToLower().Contains(keyword))
                ).ToList();
            }
            RenderProducts();
        }

        private void FlpProducts_SizeChanged(object sender, EventArgs e)
        {
            RecalculateResponsiveLayout();
        }

        /// <summary>
        /// Tự động tính toán số cột và chiều rộng thẻ card dựa trên kích thước khung hiển thị,
        /// giúp giao diện luôn thẳng đều, không bị khoảng trống xấu khi phóng to toàn màn hình.
        /// </summary>
        private void RecalculateResponsiveLayout()
        {
            int scrollbarWidth = SystemInformation.VerticalScrollBarWidth + 10;
            int availableWidth = flpProducts.ClientSize.Width - flpProducts.Padding.Horizontal - scrollbarWidth;
            if (availableWidth <= 200) return;

            // Xác định số cột tối ưu (ít nhất 2 cột, tiêu chuẩn 3 cột, màn lớn 4 - 5 cột)
            int minCardWidth = 270;
            int cols = Math.Max(2, availableWidth / minCardWidth);

            // Tính lại chiều rộng thẻ chính xác từng pixel
            int totalGaps = (cols - 1) * (CardMargin * 2);
            int newWidth = (availableWidth - totalGaps) / cols;

            if (newWidth < 250) newWidth = 250;
            _currentCardWidth = newWidth;

            // Cập nhật lại kích thước của các thẻ hiện tại
            flpProducts.SuspendLayout();
            foreach (Control ctrl in flpProducts.Controls)
            {
                if (ctrl is RoundedPanel card)
                {
                    card.Width = _currentCardWidth;
                    // Cập nhật chiều rộng của các control con bên trong card
                    AdjustCardChildren(card, _currentCardWidth);
                }
            }
            flpProducts.ResumeLayout(true);
        }

        private void AdjustCardChildren(RoundedPanel card, int cardWidth)
        {
            int innerWidth = cardWidth - 24;
            foreach (Control c in card.Controls)
            {
                if (c.Name == "pnlImg")
                {
                    c.Width = innerWidth;
                }
                else if (c.Name == "lblName")
                {
                    c.Width = innerWidth;
                }
                else if (c.Name == "pnlActions")
                {
                    c.Width = innerWidth;
                    int btnBuyWidth = 85;
                    int btnCartWidth = innerWidth - btnBuyWidth - 8;
                    foreach (Control b in c.Controls)
                    {
                        if (b.Name == "btnCartItem") b.Width = Math.Max(btnCartWidth, 90);
                        if (b.Name == "btnBuyItem")
                        {
                            b.Width = btnBuyWidth;
                            b.Left = innerWidth - btnBuyWidth;
                        }
                    }
                }
            }
        }

        private void RenderProducts()
        {
            flpProducts.SuspendLayout();
            flpProducts.Controls.Clear();

            if (_displayedProducts.Count == 0)
            {
                Panel pnlEmpty = new Panel
                {
                    Size = new Size(flpProducts.ClientSize.Width - 50, 200),
                    BackColor = Color.Transparent
                };
                Label lbl = new Label
                {
                    Text = "Không tìm thấy sản phẩm nào phù hợp.",
                    Font = new Font("Segoe UI", 11F),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill
                };
                pnlEmpty.Controls.Add(lbl);
                flpProducts.Controls.Add(pnlEmpty);
                flpProducts.ResumeLayout(true);
                return;
            }

            RecalculateResponsiveLayout();

            foreach (var p in _displayedProducts)
            {
                var card = CreateProductCard(p, _currentCardWidth);
                flpProducts.Controls.Add(card);
            }

            flpProducts.ResumeLayout(true);
        }

        private RoundedPanel CreateProductCard(Product p, int width)
        {
            int innerWidth = width - 24;

            RoundedPanel card = new RoundedPanel
            {
                Size = new Size(width, CardHeight),
                Margin = new Padding(CardMargin),
                FillColor = Color.White,
                BorderColor = Color.FromArgb(226, 232, 240),
                BorderRadius = 14,
                BorderSize = 1,
                Tag = p
            };

            // 1. Box hình ảnh / Icon
            RoundedPanel pnlImg = new RoundedPanel
            {
                Name = "pnlImg",
                Location = new Point(12, 12),
                Size = new Size(innerWidth, 150),
                FillColor = Color.FromArgb(241, 245, 249),
                BorderColor = Color.FromArgb(241, 245, 249),
                BorderRadius = 10,
                BorderSize = 1,
                Cursor = Cursors.Hand
            };
            pnlImg.Click += (s, e) => OpenProductDetails(p);

            bool hasImg = false;
            if (!string.IsNullOrWhiteSpace(p.HinhAnh))
            {
                string candidate = p.HinhAnh;
                if (!Path.IsPathRooted(candidate))
                {
                    candidate = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, candidate);
                }
                if (File.Exists(candidate))
                {
                    try
                    {
                        PictureBox pb = new PictureBox
                        {
                            Dock = DockStyle.Fill,
                            Image = Image.FromFile(candidate),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Cursor = Cursors.Hand
                        };
                        pb.Click += (s, e) => OpenProductDetails(p);
                        pnlImg.Controls.Add(pb);
                        hasImg = true;
                    }
                    catch { }
                }
            }

            if (!hasImg)
            {
                Label lblIcon = new Label
                {
                    Dock = DockStyle.Fill,
                    Text = !string.IsNullOrWhiteSpace(p.HinhAnh) ? p.HinhAnh : "📱",
                    Font = new Font("Segoe UI Emoji", 46F),
                    ForeColor = Color.FromArgb(71, 85, 105),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Cursor = Cursors.Hand
                };
                lblIcon.Click += (s, e) => OpenProductDetails(p);
                pnlImg.Controls.Add(lblIcon);
            }
            card.Controls.Add(pnlImg);

            // 2. Tên sản phẩm
            Label lblName = new Label
            {
                Name = "lblName",
                Text = p.TenSP,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(12, 168),
                Size = new Size(innerWidth, 38),
                AutoEllipsis = true,
                Cursor = Cursors.Hand
            };
            lblName.Click += (s, e) => OpenProductDetails(p);
            card.Controls.Add(lblName);

            // 3. Tồn kho
            Label lblStock = new Label
            {
                Text = p.IsInStock ? $"Còn {p.SoLuongTon} SP" : "Hết hàng",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                ForeColor = p.IsInStock ? Color.FromArgb(22, 163, 74) : Color.FromArgb(220, 38, 38),
                Location = new Point(12, 208),
                Size = new Size(innerWidth, 16)
            };
            card.Controls.Add(lblStock);

            // 4. Giá bán
            Label lblPrice = new Label
            {
                Text = p.FormattedPrice,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 38, 38),
                Location = new Point(12, 226),
                Size = new Size(innerWidth, 24)
            };
            card.Controls.Add(lblPrice);

            // 5. Khung chứa 2 nút: "Thêm giỏ" & "Mua ngay"
            Panel pnlActions = new Panel
            {
                Name = "pnlActions",
                Location = new Point(12, 258),
                Size = new Size(innerWidth, 38),
                BackColor = Color.Transparent
            };
            card.Controls.Add(pnlActions);

            int btnBuyWidth = 85;
            int btnCartWidth = innerWidth - btnBuyWidth - 8;

            // Nút Thêm giỏ
            RoundedButton btnCartItem = new RoundedButton
            {
                Name = "btnCartItem",
                Text = "Thêm giỏ",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                NormalColor = Color.White,
                HoverColor = Color.FromArgb(245, 243, 255),
                PressedColor = Color.FromArgb(237, 233, 254),
                NormalBorder = Color.FromArgb(99, 91, 255),
                TextColor = Color.FromArgb(99, 91, 255),
                BorderSize = 1,
                BorderRadius = 8,
                Location = new Point(0, 0),
                Size = new Size(btnCartWidth, 36),
                Cursor = Cursors.Hand
            };
            btnCartItem.Click += async (s, e) =>
            {
                ShoppingCartService.AddItem(p, 1);
                btnCartItem.Text = "✓ Đã thêm";
                btnCartItem.NormalColor = Color.FromArgb(236, 253, 245);
                btnCartItem.NormalBorder = Color.FromArgb(16, 185, 129);
                btnCartItem.TextColor = Color.FromArgb(16, 185, 129);
                await Task.Delay(1000);
                if (!btnCartItem.IsDisposed)
                {
                    btnCartItem.Text = "Thêm giỏ";
                    btnCartItem.NormalColor = Color.White;
                    btnCartItem.NormalBorder = Color.FromArgb(99, 91, 255);
                    btnCartItem.TextColor = Color.FromArgb(99, 91, 255);
                }
            };
            pnlActions.Controls.Add(btnCartItem);

            // Nút Mua ngay
            RoundedButton btnBuyItem = new RoundedButton
            {
                Name = "btnBuyItem",
                Text = "Mua ngay",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                NormalColor = Color.FromArgb(99, 91, 255),
                HoverColor = Color.FromArgb(79, 70, 229),
                PressedColor = Color.FromArgb(67, 56, 202),
                TextColor = Color.White,
                BorderSize = 0,
                BorderRadius = 8,
                Location = new Point(innerWidth - btnBuyWidth, 0),
                Size = new Size(btnBuyWidth, 36),
                Cursor = Cursors.Hand
            };
            btnBuyItem.Click += (s, e) =>
            {
                DirectCheckoutProduct(p, 1);
            };
            pnlActions.Controls.Add(btnBuyItem);

            return card;
        }

        /// <summary>
        /// Mở hộp thoại xem chi tiết sản phẩm và xử lý khi người dùng chọn 'Mua ngay'.
        /// </summary>
        /// <param name="p">Đối tượng sản phẩm cần xem chi tiết.</param>
        private void OpenProductDetails(Product p)
        {
            using (var dlg = new ProductDetailDialog(p))
            {
                var res = dlg.ShowDialog(this);
                if (res == DialogResult.OK && dlg.BuyNowRequested)
                {
                    DirectCheckoutProduct(p, dlg.SelectedQuantity);
                }
            }
        }

        /// <summary>
        /// Thực hiện thanh toán trực tiếp một sản phẩm (tính năng 'Mua ngay') mà không cần qua giỏ hàng.
        /// </summary>
        /// <param name="p">Sản phẩm khách chọn mua.</param>
        /// <param name="qty">Số lượng mua.</param>
        private void DirectCheckoutProduct(Product p, int qty)
        {
            var singleList = new List<CartItem>
            {
                new CartItem { Product = p, Quantity = qty }
            };

            using (var checkoutDlg = new ShopCheckoutDialog(singleList))
            {
                var res = checkoutDlg.ShowDialog(this);
                if (res == DialogResult.OK && checkoutDlg.PaymentSuccessful)
                {
                    // Tải lại dữ liệu để cập nhật số lượng tồn kho mới nhất
                    _ = LoadProductsAsync();
                }
            }
        }

        /// <summary>
        /// Mở cửa sổ popup Giỏ hàng (CartDialog) cho phép kiểm tra số dư và thanh toán toàn bộ giỏ hàng.
        /// </summary>
        private void BtnCart_Click(object sender, EventArgs e)
        {
            using (var cartDlg = new CartDialog())
            {
                var res = cartDlg.ShowDialog(this);
                if (res == DialogResult.OK && cartDlg.PaymentSuccessful)
                {
                    _ = LoadProductsAsync();
                }
            }
        }
    }
}
