using BankAccountManagement.Forms;
using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Cửa sổ Popup Giỏ hàng cho phép người dùng xem danh sách sản phẩm, chọn tài khoản ngân hàng,
    /// kiểm tra số dư thực tế và bấm thanh toán trực tiếp từ tài khoản ngân hàng.
    /// </summary>
    public class CartDialog : Form
    {
        private readonly ShopService _shopService;
        private Panel pnlItems;
        private Label lblTotalAmount;
        private Label lblAccountBalance;
        private Label lblWarning;
        private ComboBox cboAccount;
        private RoundedButton btnCheckout;

        private List<TaiKhoan> _userAccounts = new List<TaiKhoan>();
        private TaiKhoan _selectedAccount = null;

        /// <summary>Ghi nhận trạng thái thanh toán thành công (true nếu giao dịch trừ tiền đã hoàn tất).</summary>
        public bool PaymentSuccessful { get; private set; }

        /// <summary>Cờ đánh dấu yêu cầu mở màn hình thanh toán phụ (hỗ trợ tương thích mở rộng).</summary>
        public bool CheckoutRequested { get; set; }

        /// <summary>
        /// Khởi tạo cửa sổ Giỏ hàng Popup, nạp danh sách tài khoản ngân hàng và các mặt hàng đã chọn.
        /// </summary>
        public CartDialog()
        {
            _shopService = new ShopService();
            InitializeCustomComponent();
            LoadUserAccounts();
            LoadCartItems();
        }

        private void InitializeCustomComponent()
        {
            this.Size = new Size(800, 600);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

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
                Height = 52,
                BackColor = Color.FromArgb(99, 91, 255)
            };
            this.Controls.Add(pnlHeader);

            Label lblTitle = new Label
            {
                Text = "🛒  Giỏ hàng của bạn",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(18, 14)
            };
            pnlHeader.Controls.Add(lblTitle);

            Button btnClose = new Button
            {
                Text = "✕",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(38, 38),
                Location = new Point(pnlHeader.Width - 46, 7),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            pnlHeader.Controls.Add(btnClose);

            // 2. Bottom Bar (Thiết kế 2 hàng riêng biệt: Hàng 1 chọn tài khoản & tổng tiền, Hàng 2 các nút hành động)
            Panel pnlBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 125,
                BackColor = Color.FromArgb(248, 250, 252)
            };
            pnlBottom.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(226, 232, 240), 1))
                {
                    e.Graphics.DrawLine(pen, 0, 0, pnlBottom.Width, 0);
                }
            };
            this.Controls.Add(pnlBottom);

            // ===== HÀNG 1: TÀI KHOẢN THANH TOÁN & TỔNG TIỀN =====
            Label lblAccTitle = new Label
            {
                Text = "Tài khoản nguồn:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85),
                Location = new Point(20, 16),
                Size = new Size(130, 22)
            };
            pnlBottom.Controls.Add(lblAccTitle);

            cboAccount = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9.5F),
                Location = new Point(155, 12),
                Size = new Size(270, 28)
            };
            cboAccount.SelectedIndexChanged += CboAccount_SelectedIndexChanged;
            pnlBottom.Controls.Add(cboAccount);

            lblAccountBalance = new Label
            {
                Text = "Số dư: 0 đ",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(22, 163, 74),
                Location = new Point(435, 16),
                Size = new Size(140, 22)
            };
            pnlBottom.Controls.Add(lblAccountBalance);

            lblTotalAmount = new Label
            {
                Text = "Tổng cộng: 0 đ",
                Font = new Font("Segoe UI", 13.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 38, 38),
                TextAlign = ContentAlignment.MiddleRight,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Size = new Size(220, 28),
                Location = new Point(pnlBottom.Width - 235, 12)
            };
            pnlBottom.Controls.Add(lblTotalAmount);

            // ===== HÀNG 2: CÁC NÚT THAO TÁC & THANH TOÁN =====
            Button btnClear = new Button
            {
                Text = "🗑️ Xóa tất cả",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(220, 38, 38),
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(110, 38),
                Location = new Point(20, 60),
                Cursor = Cursors.Hand
            };
            btnClear.FlatAppearance.BorderColor = Color.FromArgb(254, 202, 202);
            btnClear.Click += (s, e) =>
            {
                if (ShoppingCartService.Items.Count == 0) return;
                var res = MessageBox.Show("Bạn có chắc chắn muốn làm trống toàn bộ giỏ hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    ShoppingCartService.Clear();
                    LoadCartItems();
                }
            };
            pnlBottom.Controls.Add(btnClear);

            RoundedButton btnContinue = new RoundedButton
            {
                Text = "Tiếp tục xem",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                NormalColor = Color.White,
                HoverColor = Color.FromArgb(241, 245, 249),
                PressedColor = Color.FromArgb(226, 232, 240),
                NormalBorder = Color.FromArgb(203, 213, 225),
                TextColor = Color.FromArgb(71, 85, 105),
                BorderSize = 1,
                BorderRadius = 0,
                Size = new Size(125, 42),
                Location = new Point(140, 58),
                Cursor = Cursors.Hand
            };
            btnContinue.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            pnlBottom.Controls.Add(btnContinue);

            lblWarning = new Label
            {
                Text = "⚠ Số dư không đủ để thanh toán!",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.FromArgb(220, 38, 38),
                Location = new Point(275, 68),
                Size = new Size(290, 24),
                Visible = false
            };
            pnlBottom.Controls.Add(lblWarning);

            btnCheckout = new RoundedButton
            {
                Text = "💳 Thanh toán ngay",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                NormalColor = Color.FromArgb(99, 91, 255),
                HoverColor = Color.FromArgb(79, 70, 229),
                PressedColor = Color.FromArgb(67, 56, 202),
                TextColor = Color.White,
                BorderSize = 0,
                BorderRadius = 0,
                Size = new Size(190, 44),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(pnlBottom.Width - 210, 56),
                Cursor = Cursors.Hand
            };
            btnCheckout.Click += BtnCheckout_Click;
            pnlBottom.Controls.Add(btnCheckout);

            // 3. Middle Items Area (Danh sách các sản phẩm)
            pnlItems = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20, 16, 20, 16),
                BackColor = Color.White
            };
            this.Controls.Add(pnlItems);
            pnlItems.BringToFront();
        }

        /// <summary>
        /// Nạp danh sách các tài khoản ngân hàng hoạt động của người dùng hiện tại vào ComboBox lựa chọn nguồn tiền.
        /// </summary>
        private async void LoadUserAccounts()
        {
            string maKH = CurrentSession.MaKH;
            _userAccounts = await _shopService.GetUserPaymentAccountsAsync(maKH);

            cboAccount.Items.Clear();
            if (_userAccounts.Count == 0)
            {
                cboAccount.Items.Add("Không có tài khoản");
                cboAccount.SelectedIndex = 0;
                btnCheckout.Enabled = false;
                lblAccountBalance.Text = "Số dư: 0 đ";
                lblAccountBalance.ForeColor = Color.FromArgb(220, 38, 38);
                return;
            }

            foreach (var acc in _userAccounts)
            {
                cboAccount.Items.Add($"{acc.SoTaiKhoan} ({acc.MaNganHang})");
            }
            cboAccount.SelectedIndex = 0;
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng đổi tài khoản thanh toán nguồn.
        /// </summary>
        private void CboAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAccount.SelectedIndex < 0 || cboAccount.SelectedIndex >= _userAccounts.Count) return;

            _selectedAccount = _userAccounts[cboAccount.SelectedIndex];
            lblAccountBalance.Text = $"Số dư: {_selectedAccount.SoDuHienTai:N0} đ";

            ValidateBalanceAndUI();
        }

        /// <summary>
        /// Kiểm tra số dư khả dụng so với tổng tiền giỏ hàng, cập nhật màu sắc và trạng thái nút thanh toán.
        /// </summary>
        private void ValidateBalanceAndUI()
        {
            decimal total = ShoppingCartService.TotalAmount;
            lblTotalAmount.Text = $"Tổng cộng: {total:N0} đ";

            if (_selectedAccount == null)
            {
                btnCheckout.Enabled = false;
                lblWarning.Visible = false;
                return;
            }

            if (_selectedAccount.SoDuHienTai < total)
            {
                lblWarning.Visible = true;
                lblAccountBalance.ForeColor = Color.FromArgb(220, 38, 38);
                btnCheckout.Enabled = false;
            }
            else
            {
                lblWarning.Visible = false;
                lblAccountBalance.ForeColor = Color.FromArgb(22, 163, 74);
                btnCheckout.Enabled = ShoppingCartService.Items.Count > 0;
            }
        }

        /// <summary>
        /// Tải và hiển thị danh sách các món hàng trong giỏ hàng vào bảng cuộn trung tâm.
        /// </summary>
        private void LoadCartItems()
        {
            pnlItems.Controls.Clear();
            var items = ShoppingCartService.Items;

            ValidateBalanceAndUI();

            if (items.Count == 0)
            {
                Panel pnlEmpty = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.Transparent
                };
                Label lblEmptyIcon = new Label
                {
                    Text = "🛒",
                    Font = new Font("Segoe UI Emoji", 48F),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Top,
                    Height = 120
                };
                Label lblEmptyMsg = new Label
                {
                    Text = "Giỏ hàng của bạn hiện đang trống\nHãy lựa chọn các sản phẩm công nghệ yêu thích nhé!",
                    Font = new Font("Segoe UI", 11F),
                    ForeColor = Color.FromArgb(100, 116, 139),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Top,
                    Height = 60
                };
                pnlEmpty.Controls.Add(lblEmptyMsg);
                pnlEmpty.Controls.Add(lblEmptyIcon);
                pnlItems.Controls.Add(pnlEmpty);
                return;
            }

            int top = 10;
            int itemWidth = pnlItems.ClientSize.Width - 45;
            if (itemWidth < 700) itemWidth = 700;

            foreach (var item in items)
            {
                Panel row = CreateCartRow(item, itemWidth);
                row.Location = new Point(10, top);
                pnlItems.Controls.Add(row);
                top += row.Height + 12;
            }
        }

        /// <summary>
        /// Tạo một hàng giao diện đại diện cho một sản phẩm trong giỏ hàng kèm nút tăng/giảm và nút xóa.
        /// </summary>
        /// <param name="item">Dòng sản phẩm trong giỏ hàng.</param>
        /// <param name="width">Độ rộng hàng cần vẽ.</param>
        /// <returns>Đối tượng Panel chứa toàn bộ UI dòng sản phẩm.</returns>
        private Panel CreateCartRow(CartItem item, int width)
        {
            RoundedPanel row = new RoundedPanel
            {
                Size = new Size(width, 76),
                FillColor = Color.FromArgb(248, 250, 252),
                BorderColor = Color.FromArgb(226, 232, 240),
                BorderRadius = 10,
                BorderSize = 1
            };

            // Thumbnail
            RoundedPanel pnlThumb = new RoundedPanel
            {
                Size = new Size(58, 58),
                Location = new Point(10, 9),
                FillColor = Color.White,
                BorderColor = Color.FromArgb(226, 232, 240),
                BorderRadius = 0,
                BorderSize = 1
            };
            row.Controls.Add(pnlThumb);

            bool hasImg = false;
            if (!string.IsNullOrWhiteSpace(item.Product.HinhAnh))
            {
                string candidate = item.Product.HinhAnh;
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
                            SizeMode = PictureBoxSizeMode.Zoom
                        };
                        pnlThumb.Controls.Add(pb);
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
                    Text = !string.IsNullOrWhiteSpace(item.Product.HinhAnh) ? item.Product.HinhAnh : "📱",
                    Font = new Font("Segoe UI Emoji", 20F),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                pnlThumb.Controls.Add(lblIcon);
            }

            // Tên sản phẩm & Đơn giá
            Label lblName = new Label
            {
                Text = item.Product.TenSP,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Location = new Point(80, 14),
                Size = new Size(250, 24),
                AutoEllipsis = true
            };
            row.Controls.Add(lblName);

            Label lblUnitPrice = new Label
            {
                Text = $"Đơn giá: {item.Product.FormattedPrice}",
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(80, 40),
                Size = new Size(200, 20)
            };
            row.Controls.Add(lblUnitPrice);

            // Nút điều chỉnh số lượng [-] [qty] [+]
            int qtyX = width - 330;
            Button btnMinus = new Button
            {
                Text = "-",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Size = new Size(28, 28),
                Location = new Point(qtyX, 24),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnMinus.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnMinus.Click += (s, e) =>
            {
                ShoppingCartService.UpdateQuantity(item.Product.MaSP, item.Quantity - 1);
                LoadCartItems();
            };
            row.Controls.Add(btnMinus);

            Label lblItemQty = new Label
            {
                Text = item.Quantity.ToString(),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(34, 28),
                Location = new Point(qtyX + 32, 24),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            row.Controls.Add(lblItemQty);

            Button btnPlus = new Button
            {
                Text = "+",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Size = new Size(28, 28),
                Location = new Point(qtyX + 70, 24),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnPlus.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnPlus.Click += (s, e) =>
            {
                if (item.Quantity < item.Product.SoLuongTon)
                {
                    ShoppingCartService.UpdateQuantity(item.Product.MaSP, item.Quantity + 1);
                    LoadCartItems();
                }
                else
                {
                    MessageBox.Show($"Kho chỉ còn tối đa {item.Product.SoLuongTon} sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            row.Controls.Add(btnPlus);

            // Thành tiền món hàng
            int priceX = width - 200;
            Label lblItemTotal = new Label
            {
                Text = item.FormattedTotalPrice,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 38, 38),
                TextAlign = ContentAlignment.MiddleRight,
                Location = new Point(priceX, 24),
                Size = new Size(135, 28)
            };
            row.Controls.Add(lblItemTotal);

            // Nút Xóa món
            Button btnDel = new Button
            {
                Text = "🗑️",
                Font = new Font("Segoe UI Emoji", 10F),
                ForeColor = Color.FromArgb(220, 38, 38),
                Size = new Size(32, 32),
                Location = new Point(width - 50, 22),
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnDel.FlatAppearance.BorderSize = 0;
            btnDel.Click += (s, e) =>
            {
                ShoppingCartService.RemoveItem(item.Product.MaSP);
                LoadCartItems();
            };
            row.Controls.Add(btnDel);

            return row;
        }

        /// <summary>
        /// Xử lý sự kiện bấm nút 'Thanh toán ngay':
        /// Kiểm tra số dư tài khoản, khởi tạo nội dung thanh toán liệt kê các sản phẩm,
        /// và gọi ShopService.ProcessPaymentAsync để trích nợ tài khoản và tạo giao dịch.
        /// </summary>
        private async void BtnCheckout_Click(object sender, EventArgs e)
        {
            var cartItems = ShoppingCartService.Items;
            if (cartItems.Count == 0)
            {
                MessageBox.Show("Giỏ hàng của bạn đang trống! Hãy chọn sản phẩm trước khi thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_selectedAccount == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản ngân hàng để thực hiện thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal totalAmount = ShoppingCartService.TotalAmount;

            // KIỂM TRA SỐ DƯ: NẾU SỐ DƯ KHÔNG ĐỦ THÌ TUYỆT ĐỐI KHÔNG CHO THANH TOÁN
            if (_selectedAccount.SoDuHienTai < totalAmount)
            {
                MessageBox.Show(
                    $"SỐ DƯ TÀI KHOẢN KHÔNG ĐỦ ĐỂ THANH TOÁN!\n\n" +
                    $"• Tài khoản: {_selectedAccount.SoTaiKhoan}\n" +
                    $"• Số dư hiện tại: {_selectedAccount.SoDuHienTai:N0} đ\n" +
                    $"• Cần thanh toán: {totalAmount:N0} đ\n\n" +
                    $"Vui lòng nạp thêm tiền vào tài khoản hoặc chọn tài khoản khác để tiếp tục.",
                    "Số dư không đủ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Tạo nội dung giao dịch liệt kê rõ ràng các sản phẩm đã mua
            var names = new List<string>();
            foreach (var ci in cartItems)
            {
                if (ci?.Product != null) names.Add($"{ci.Quantity}x {ci.Product.TenSP}");
            }
            string txNote = "Thanh toán mua sắm: " + string.Join(", ", names);
            if (txNote.Length > 250) txNote = txNote.Substring(0, 247) + "...";

            btnCheckout.Enabled = false;
            btnCheckout.Text = "Đang xử lý...";

            // Thực hiện trừ tiền trực tiếp trên tài khoản ngân hàng trong CSDL
            var result = await _shopService.ProcessPaymentAsync(
                _selectedAccount.SoTaiKhoan,
                totalAmount,
                txNote,
                cartItems
            );

            if (result.Success)
            {
                ShoppingCartService.Clear();
                this.PaymentSuccessful = true;

                MessageBox.Show(
                    $"THANH TOÁN THÀNH CÔNG!\n\n" +
                    $"• Mã giao dịch: {result.TransactionId}\n" +
                    $"• Tài khoản trích tiền: {_selectedAccount.SoTaiKhoan}\n" +
                    $"• Số tiền đã trừ: {totalAmount:N0} đ\n" +
                    $"• Số dư còn lại: {result.NewBalance:N0} đ\n" +
                    $"• Nội dung giao dịch: {txNote}\n\n" +
                    $"Đơn hàng đã được thanh toán và lưu vào lịch sử giao dịch thành công.",
                    "Hóa đơn thanh toán",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(result.Message, "Lỗi thanh toán", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnCheckout.Enabled = true;
                btnCheckout.Text = "💳 Thanh toán ngay";
            }
        }
    }
}
