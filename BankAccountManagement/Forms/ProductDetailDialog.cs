using BankAccountManagement.Forms;
using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Cửa sổ hiển thị thông tin chi tiết của một sản phẩm khi người dùng bấm vào thẻ sản phẩm.
    /// </summary>
    public class ProductDetailDialog : Form
    {
        private readonly Product _product;
        private int _quantity = 1;

        private Label lblQty;

        /// <summary>Cờ đánh dấu người dùng đã chọn hành động 'Mua ngay' (true để kích hoạt luồng thanh toán tức thì).</summary>
        public bool BuyNowRequested { get; private set; }

        /// <summary>Số lượng sản phẩm mà khách hàng đã chọn trên hộp thoại chi tiết.</summary>
        public int SelectedQuantity => _quantity;

        /// <summary>
        /// Khởi tạo hộp thoại chi tiết sản phẩm.
        /// </summary>
        /// <param name="product">Sản phẩm cần xem chi tiết thông số và giá bán.</param>
        public ProductDetailDialog(Product product)
        {
            _product = product ?? throw new ArgumentNullException(nameof(product));
            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {
            this.Size = new Size(680, 520);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            // Đường viền bo ngoài Form
            this.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(203, 213, 225), 1.5f))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
                }
            };

            // 1. Header Bar
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 48,
                BackColor = Color.FromArgb(99, 91, 255)
            };
            this.Controls.Add(pnlHeader);

            Label lblTitle = new Label
            {
                Text = "Chi tiết sản phẩm",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(16, 13)
            };
            pnlHeader.Controls.Add(lblTitle);

            Button btnClose = new Button
            {
                Text = "✕",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(36, 36),
                Location = new Point(pnlHeader.Width - 44, 6),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            pnlHeader.Controls.Add(btnClose);

            // 2. Left Column (Hình ảnh, tình trạng)
            Panel pnlLeft = new Panel
            {
                Location = new Point(24, 64),
                Size = new Size(250, 420),
                BackColor = Color.Transparent
            };
            this.Controls.Add(pnlLeft);

            RoundedPanel pnlImg = new RoundedPanel
            {
                Location = new Point(0, 0),
                Size = new Size(250, 230),
                FillColor = Color.FromArgb(241, 245, 249),
                BorderColor = Color.FromArgb(226, 232, 240),
                BorderRadius = 12,
                BorderSize = 1
            };
            pnlLeft.Controls.Add(pnlImg);

            // Kiểm tra xem hình ảnh là file hay icon
            bool hasRealImage = false;
            if (!string.IsNullOrWhiteSpace(_product.HinhAnh))
            {
                string candidatePath = _product.HinhAnh;
                if (!Path.IsPathRooted(candidatePath))
                {
                    candidatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, candidatePath);
                }

                if (File.Exists(candidatePath))
                {
                    try
                    {
                        PictureBox pb = new PictureBox
                        {
                            Dock = DockStyle.Fill,
                            Image = Image.FromFile(candidatePath),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            BackColor = Color.Transparent
                        };
                        pnlImg.Controls.Add(pb);
                        hasRealImage = true;
                    }
                    catch { }
                }
            }

            if (!hasRealImage)
            {
                Label lblIcon = new Label
                {
                    Dock = DockStyle.Fill,
                    Text = !string.IsNullOrWhiteSpace(_product.HinhAnh) ? _product.HinhAnh : "📦",
                    Font = new Font("Segoe UI Emoji", 56F),
                    ForeColor = Color.FromArgb(71, 85, 105),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.Transparent
                };
                pnlImg.Controls.Add(lblIcon);
            }

            Label lblStock = new Label
            {
                Text = _product.IsInStock ? $"● Còn hàng ({_product.SoLuongTon} sản phẩm)" : "● Tạm hết hàng",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = _product.IsInStock ? Color.FromArgb(22, 163, 74) : Color.FromArgb(220, 38, 38),
                AutoSize = true,
                Location = new Point(4, 245)
            };
            pnlLeft.Controls.Add(lblStock);

            Label lblBadge1 = new Label
            {
                Text = "✔ Bảo hành 12 tháng chính hãng",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 116, 139),
                AutoSize = true,
                Location = new Point(4, 275)
            };
            pnlLeft.Controls.Add(lblBadge1);

            Label lblBadge2 = new Label
            {
                Text = "✔ 1 đổi 1 trong 30 ngày nếu lỗi",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 116, 139),
                AutoSize = true,
                Location = new Point(4, 300)
            };
            pnlLeft.Controls.Add(lblBadge2);

            Label lblBadge3 = new Label
            {
                Text = "✔ Miễn phí giao hàng toàn quốc",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 116, 139),
                AutoSize = true,
                Location = new Point(4, 325)
            };
            pnlLeft.Controls.Add(lblBadge3);

            // 3. Right Column (Tên, Giá, Thông số, Mô tả, Số lượng, Nút bấm)
            Panel pnlRight = new Panel
            {
                Location = new Point(295, 64),
                Size = new Size(360, 430),
                BackColor = Color.Transparent
            };
            this.Controls.Add(pnlRight);

            Label lblName = new Label
            {
                Text = _product.TenSP,
                Font = new Font("Segoe UI", 13.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(0, 0),
                Size = new Size(360, 48)
            };
            pnlRight.Controls.Add(lblName);

            Label lblPrice = new Label
            {
                Text = _product.FormattedPrice,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 38, 38),
                AutoSize = true,
                Location = new Point(0, 52)
            };
            pnlRight.Controls.Add(lblPrice);

            // Thông số kỹ thuật
            Label lblSpecTitle = new Label
            {
                Text = "Thông số nổi bật:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85),
                AutoSize = true,
                Location = new Point(0, 95)
            };
            pnlRight.Controls.Add(lblSpecTitle);

            TextBox txtSpec = new TextBox
            {
                Text = !string.IsNullOrWhiteSpace(_product.ThongSo) ? _product.ThongSo : "Đang cập nhật...",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(71, 85, 105),
                BackColor = Color.FromArgb(248, 250, 252),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Location = new Point(0, 120),
                Size = new Size(355, 65),
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlRight.Controls.Add(txtSpec);

            // Mô tả
            Label lblDescTitle = new Label
            {
                Text = "Mô tả sản phẩm:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85),
                AutoSize = true,
                Location = new Point(0, 195)
            };
            pnlRight.Controls.Add(lblDescTitle);

            TextBox txtDesc = new TextBox
            {
                Text = !string.IsNullOrWhiteSpace(_product.MoTa) ? _product.MoTa : "Chưa có mô tả chi tiết.",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(71, 85, 105),
                BackColor = Color.FromArgb(248, 250, 252),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Location = new Point(0, 218),
                Size = new Size(355, 75),
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlRight.Controls.Add(txtDesc);

            // Bộ chọn số lượng
            Label lblQtyTitle = new Label
            {
                Text = "Số lượng:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85),
                AutoSize = true,
                Location = new Point(0, 310)
            };
            pnlRight.Controls.Add(lblQtyTitle);

            Button btnMinus = new Button
            {
                Text = "-",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(32, 30),
                Location = new Point(80, 306),
                BackColor = Color.FromArgb(241, 245, 249),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnMinus.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            pnlRight.Controls.Add(btnMinus);

            lblQty = new Label
            {
                Text = "1",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(36, 30),
                Location = new Point(116, 306),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlRight.Controls.Add(lblQty);

            Button btnPlus = new Button
            {
                Text = "+",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(32, 30),
                Location = new Point(156, 306),
                BackColor = Color.FromArgb(241, 245, 249),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnPlus.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            pnlRight.Controls.Add(btnPlus);

            btnMinus.Click += (s, e) =>
            {
                if (_quantity > 1)
                {
                    _quantity--;
                    lblQty.Text = _quantity.ToString();
                }
            };

            btnPlus.Click += (s, e) =>
            {
                if (_quantity < _product.SoLuongTon)
                {
                    _quantity++;
                    lblQty.Text = _quantity.ToString();
                }
                else
                {
                    MessageBox.Show($"Chỉ còn tối đa {_product.SoLuongTon} sản phẩm trong kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            // Nút Thêm vào giỏ
            RoundedButton btnAddToCart = new RoundedButton
            {
                Text = "🛒 Thêm vào giỏ",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                NormalColor = Color.White,
                HoverColor = Color.FromArgb(245, 243, 255),
                PressedColor = Color.FromArgb(237, 233, 254),
                NormalBorder = Color.FromArgb(99, 91, 255),
                TextColor = Color.FromArgb(99, 91, 255),
                BorderSize = 1,
                BorderRadius = 0,
                Size = new Size(160, 42),
                Location = new Point(0, 355),
                Cursor = Cursors.Hand
            };
            btnAddToCart.Click += (s, e) =>
            {
                ShoppingCartService.AddItem(_product, _quantity);
                MessageBox.Show($"Đã thêm {_quantity} x \"{_product.TenSP}\" vào giỏ hàng!", "Giỏ hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            };
            pnlRight.Controls.Add(btnAddToCart);

            // Nút Mua ngay
            RoundedButton btnBuyNow = new RoundedButton
            {
                Text = "⚡ Mua ngay",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                NormalColor = Color.FromArgb(99, 91, 255),
                HoverColor = Color.FromArgb(79, 70, 229),
                PressedColor = Color.FromArgb(67, 56, 202),
                TextColor = Color.White,
                BorderSize = 0,
                BorderRadius = 0,
                Size = new Size(185, 42),
                Location = new Point(170, 355),
                Cursor = Cursors.Hand
            };
            btnBuyNow.Click += (s, e) =>
            {
                this.BuyNowRequested = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            };
            pnlRight.Controls.Add(btnBuyNow);
        }
    }
}
