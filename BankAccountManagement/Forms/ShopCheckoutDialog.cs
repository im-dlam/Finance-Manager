using BankAccountManagement.Forms;
using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Cửa sổ Popup xác nhận thanh toán đơn hàng (thiết kế theo phong cách giao dịch ngân hàng).
    /// </summary>
    public class ShopCheckoutDialog : Form
    {
        private readonly ShopService _shopService;
        private readonly List<CartItem> _itemsToCheckout;
        private readonly decimal _totalAmount;

        private ComboBox cboAccount;
        private TextBox txtCurrentBalance;
        private TextBox txtAfterBalance;
        private TextBox txtNote;
        private Label lblTotalDisplay;
        private Label lblWarning;
        private RoundedButton btnConfirm;

        private List<TaiKhoan> _userAccounts = new List<TaiKhoan>();
        private TaiKhoan _selectedAccount = null;

        /// <summary>Ghi nhận trạng thái thanh toán thành công (true nếu tiền đã được trừ và giao dịch đã lưu).</summary>
        public bool PaymentSuccessful { get; private set; }

        /// <summary>
        /// Khởi tạo cửa sổ popup xác nhận thanh toán đơn hàng (hỗ trợ cho cả Mua ngay và Thanh toán giỏ hàng).
        /// </summary>
        /// <param name="items">Danh sách các sản phẩm và số lượng cần thanh toán.</param>
        public ShopCheckoutDialog(List<CartItem> items)
        {
            _shopService = new ShopService();
            _itemsToCheckout = items != null && items.Count > 0 ? items : ShoppingCartService.Items;
            _totalAmount = _itemsToCheckout.Sum(i => i.TotalPrice);

            InitializeCustomComponent();
            LoadUserAccounts();
        }

        private void InitializeCustomComponent()
        {
            this.Size = new Size(560, 620);
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

            // 1. Header Bar (Giống Transaction)
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 52,
                BackColor = Color.FromArgb(51, 102, 255)
            };
            this.Controls.Add(pnlHeader);

            Label lblTitle = new Label
            {
                Text = "💳  Xác nhận thanh toán đơn hàng",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(16, 14)
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
                Location = new Point(pnlHeader.Width - 44, 7),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            pnlHeader.Controls.Add(btnClose);

            // 2. Body Area
            int startY = 70;
            int gapY = 46;
            int labelX = 28;
            int inputX = 180;
            int inputWidth = 340;

            // Tài khoản nguồn
            Label lblAccTitle = new Label
            {
                Text = "Tài khoản nguồn:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85),
                Location = new Point(labelX, startY),
                Size = new Size(145, 26)
            };
            this.Controls.Add(lblAccTitle);

            cboAccount = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(inputX, startY - 2),
                Size = new Size(inputWidth, 30)
            };
            cboAccount.SelectedIndexChanged += CboAccount_SelectedIndexChanged;
            this.Controls.Add(cboAccount);

            // Số dư hiện tại
            startY += gapY;
            Label lblBalanceTitle = new Label
            {
                Text = "Số dư hiện tại:",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(labelX, startY),
                Size = new Size(145, 26)
            };
            this.Controls.Add(lblBalanceTitle);

            txtCurrentBalance = new TextBox
            {
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                BackColor = Color.FromArgb(241, 245, 249),
                ReadOnly = true,
                Location = new Point(inputX, startY - 2),
                Size = new Size(inputWidth, 28)
            };
            this.Controls.Add(txtCurrentBalance);

            // Tổng số tiền thanh toán
            startY += gapY;
            Label lblTotalTitle = new Label
            {
                Text = "Số tiền thanh toán:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 38, 38),
                Location = new Point(labelX, startY),
                Size = new Size(145, 26)
            };
            this.Controls.Add(lblTotalTitle);

            lblTotalDisplay = new Label
            {
                Text = $"{_totalAmount:N0} VND",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 38, 38),
                Location = new Point(inputX, startY - 4),
                Size = new Size(inputWidth, 30)
            };
            this.Controls.Add(lblTotalDisplay);

            // Số dư sau khi thanh toán
            startY += gapY;
            Label lblAfterTitle = new Label
            {
                Text = "Số dư sau GD:",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(labelX, startY),
                Size = new Size(145, 26)
            };
            this.Controls.Add(lblAfterTitle);

            txtAfterBalance = new TextBox
            {
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BackColor = Color.FromArgb(241, 245, 249),
                ReadOnly = true,
                Location = new Point(inputX, startY - 2),
                Size = new Size(inputWidth, 28)
            };
            this.Controls.Add(txtAfterBalance);

            // Chi tiết đơn hàng (Danh sách tóm tắt)
            startY += gapY;
            Label lblItemsTitle = new Label
            {
                Text = "Sản phẩm mua:",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(labelX, startY),
                Size = new Size(145, 26)
            };
            this.Controls.Add(lblItemsTitle);

            ListBox lstItems = new ListBox
            {
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.FromArgb(248, 250, 252),
                Location = new Point(inputX, startY),
                Size = new Size(inputWidth, 75),
                BorderStyle = BorderStyle.FixedSingle
            };
            foreach (var it in _itemsToCheckout)
            {
                lstItems.Items.Add($"{it.Quantity}x {it.Product.TenSP} - {it.FormattedTotalPrice}");
            }
            this.Controls.Add(lstItems);

            // Nội dung thanh toán
            startY += 85;
            Label lblNoteTitle = new Label
            {
                Text = "Nội dung thanh toán:",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(labelX, startY),
                Size = new Size(145, 26)
            };
            this.Controls.Add(lblNoteTitle);

            var names = new List<string>();
            foreach (var ci in _itemsToCheckout)
            {
                if (ci?.Product != null) names.Add($"{ci.Quantity}x {ci.Product.TenSP}");
            }
            string defaultSummary = names.Count > 0
                ? "Thanh toán mua sắm: " + string.Join(", ", names)
                : "Thanh toán mua sắm đơn hàng";
            if (defaultSummary.Length > 250) defaultSummary = defaultSummary.Substring(0, 247) + "...";

            txtNote = new TextBox
            {
                Font = new Font("Segoe UI", 9.5F),
                Text = defaultSummary,
                Location = new Point(inputX, startY - 2),
                Size = new Size(inputWidth, 28)
            };
            this.Controls.Add(txtNote);

            // Cảnh báo số dư không đủ (nếu có)
            startY += gapY;
            lblWarning = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.FromArgb(220, 38, 38),
                Location = new Point(labelX, startY),
                Size = new Size(490, 36)
            };
            this.Controls.Add(lblWarning);

            // Nút bấm hành động
            startY += 40;
            RoundedButton btnCancel = new RoundedButton
            {
                Text = "Hủy bỏ",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                NormalColor = Color.White,
                HoverColor = Color.FromArgb(241, 245, 249),
                PressedColor = Color.FromArgb(226, 232, 240),
                NormalBorder = Color.FromArgb(203, 213, 225),
                TextColor = Color.FromArgb(71, 85, 105),
                BorderSize = 1,
                BorderRadius = 8,
                Size = new Size(140, 44),
                Location = new Point(140, startY),
                Cursor = Cursors.Hand
            };
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            this.Controls.Add(btnCancel);

            btnConfirm = new RoundedButton
            {
                Text = "✔ Xác nhận thanh toán",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                NormalColor = Color.FromArgb(51, 102, 255),
                HoverColor = Color.FromArgb(37, 99, 235),
                PressedColor = Color.FromArgb(29, 78, 216),
                TextColor = Color.White,
                BorderSize = 0,
                BorderRadius = 8,
                Size = new Size(200, 44),
                Location = new Point(295, startY),
                Cursor = Cursors.Hand
            };
            btnConfirm.Click += BtnConfirm_Click;
            this.Controls.Add(btnConfirm);
        }

        /// <summary>
        /// Nạp danh sách các tài khoản ngân hàng hoạt động của khách hàng vào ComboBox.
        /// </summary>
        private async void LoadUserAccounts()
        {
            string maKH = CurrentSession.MaKH;
            _userAccounts = await _shopService.GetUserPaymentAccountsAsync(maKH);

            cboAccount.Items.Clear();
            if (_userAccounts.Count == 0)
            {
                cboAccount.Items.Add("Không có tài khoản khả dụng");
                cboAccount.SelectedIndex = 0;
                btnConfirm.Enabled = false;
                lblWarning.Text = "Bạn chưa có tài khoản ngân hàng hoạt động để thanh toán.";
                return;
            }

            foreach (var acc in _userAccounts)
            {
                cboAccount.Items.Add($"{acc.SoTaiKhoan} ({acc.MaNganHang} - {acc.SoDuHienTai:N0} VND)");
            }
            cboAccount.SelectedIndex = 0;
        }

        /// <summary>
        /// Tính toán số dư còn lại sau giao dịch và cảnh báo nếu số dư không đủ.
        /// </summary>
        private void CboAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAccount.SelectedIndex < 0 || cboAccount.SelectedIndex >= _userAccounts.Count) return;

            _selectedAccount = _userAccounts[cboAccount.SelectedIndex];
            txtCurrentBalance.Text = $"{_selectedAccount.SoDuHienTai:N0} VND";

            decimal remaining = _selectedAccount.SoDuHienTai - _totalAmount;
            txtAfterBalance.Text = $"{remaining:N0} VND";

            if (remaining < 0)
            {
                txtAfterBalance.ForeColor = Color.FromArgb(220, 38, 38);
                lblWarning.Text = "⚠ Số dư trong tài khoản không đủ để thực hiện thanh toán này!";
                btnConfirm.Enabled = false;
            }
            else
            {
                txtAfterBalance.ForeColor = Color.FromArgb(22, 163, 74);
                lblWarning.Text = "";
                btnConfirm.Enabled = true;
            }
        }

        /// <summary>
        /// Xử lý bấm nút xác nhận thanh toán đơn hàng:
        /// Gọi ShopService.ProcessPaymentAsync để trích tiền tài khoản, ghi nhận vào dbo.GiaoDich và giảm số lượng tồn kho.
        /// </summary>
        private async void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (_selectedAccount == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnConfirm.Enabled = false;
            btnConfirm.Text = "Đang xử lý...";

            var result = await _shopService.ProcessPaymentAsync(
                _selectedAccount.SoTaiKhoan,
                _totalAmount,
                txtNote.Text.Trim(),
                _itemsToCheckout
            );

            if (result.Success)
            {
                ShoppingCartService.Clear();
                this.PaymentSuccessful = true;

                MessageBox.Show(
                    $"THANH TOÁN THÀNH CÔNG!\n\n" +
                    $"• Mã giao dịch: {result.TransactionId}\n" +
                    $"• Tài khoản trích tiền: {_selectedAccount.SoTaiKhoan}\n" +
                    $"• Số tiền đã thanh toán: {_totalAmount:N0} VND\n" +
                    $"• Số dư còn lại: {result.NewBalance:N0} VND\n\n" +
                    $"Đơn hàng của bạn đã được ghi nhận vào hệ thống.",
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
                btnConfirm.Enabled = true;
                btnConfirm.Text = "✔ Xác nhận thanh toán";
            }
        }
    }
}
