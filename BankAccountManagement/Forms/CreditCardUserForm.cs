using BankAccountManagement.Models;
using BankAccountManagement.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankAccountManagement.Forms
{
    /// <summary>
    /// Form giao diện thẻ ngân hàng trực quan dành riêng cho Khách hàng (User/Member).
    /// </summary>
    public partial class CreditCardUserForm : Form
    {
        private readonly CreditService _creditService;
        private List<CreditCardInfo> _userCards = new List<CreditCardInfo>();
        private int _currentUserCardIndex = 0;
        private bool _isInfoRevealed = false;
        private bool _isUpdatingStatus = false;

        /// <summary>
        /// Khởi tạo Form giao diện thẻ ngân hàng của Khách hàng.
        /// </summary>
        public CreditCardUserForm()
        {
            InitializeComponent();
            _creditService = new CreditService();

            btnViewCardInfo.Click += BtnViewCardInfo_Click;
            pnlToggleTrack.Click += async (s, e) => await ToggleCardLockAsync();
            pnlToggleKnob.Click += async (s, e) => await ToggleCardLockAsync();

            this.Load += CreditCardUserForm_Load;
        }

        private async void CreditCardUserForm_Load(object sender, EventArgs e)
        {
            await LoadUserCardDataAsync();
        }

        private async Task LoadUserCardDataAsync()
        {
            try
            {
                _userCards = await _creditService.GetCardsAsync(CurrentSession.MaKH, false);
                if (_userCards == null || _userCards.Count == 0)
                {
                    _userCards = _creditService.GetDefaultCards(CurrentSession.HoTen);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[CreditCardUserForm] Lỗi tải dữ liệu thẻ: {ex.Message}");
                _userCards = _creditService.GetDefaultCards(CurrentSession.HoTen);
            }

            _currentUserCardIndex = 0;
            _isInfoRevealed = false;
            btnViewCardInfo.Text = "Xem thông tin thẻ";
            DisplayCurrentUserCard();
        }

        private void DisplayCurrentUserCard()
        {
            if (_userCards == null || _userCards.Count == 0 || _currentUserCardIndex >= _userCards.Count)
            {
                return;
            }

            var card = _userCards[_currentUserCardIndex];

            lblCardBrand.Text = card.CardType;
            lblHolderName.Text = card.CardHolderName;
            lblExpiryValue.Text = card.ExpiryDate.ToString("MM/yy");

            lblCardNumber.Text = _isInfoRevealed ? card.FullCardNumber : card.MaskedCardNumber;
            lblNumValue.Text = _isInfoRevealed ? card.FullCardNumber : card.MaskedCardNumber;

            lblBalValue.Text = _isInfoRevealed ? _creditService.FormatCurrency(card.Balance, card.Currency) : "**** **** ****";
            lblBalDate.Text = DateTime.Today.ToString("dd/MM/yyyy");

            lblStmtValue.Text = card.IssueDate.ToString("dd/MM/yyyy");
            lblStmtDate.Text = card.BankName;

            lblDueValue.Text = card.ExpiryDate.ToString("dd/MM/yyyy");
            lblDueDate.Text = "Hạn 5 năm";

            lblStateValue.Text = DateTime.Today.ToString("dd/MM/yyyy");
            UpdateStatusUI(card.IsActive);
        }

        private void UpdateStatusUI(bool isActive)
        {
            if (isActive)
            {
                lblStatusValue.Text = $"Đang hoạt động (ngày {DateTime.Today:dd/MM/yyyy})";
                lblStatusValue.ForeColor = Color.FromArgb(5, 150, 105);

                lblNumStatus.Text = "Đang hoạt động";
                lblNumStatus.ForeColor = Color.FromArgb(5, 150, 105);

                lblStateStatus.Text = "Đang hoạt động";
                lblStateStatus.ForeColor = Color.FromArgb(5, 150, 105);

                pnlToggleTrack.FillColor = Color.FromArgb(16, 185, 129);
                pnlToggleKnob.Location = new Point(32, 3);
            }
            else
            {
                lblStatusValue.Text = $"Đã khóa an toàn (ngày {DateTime.Today:dd/MM/yyyy})";
                lblStatusValue.ForeColor = Color.FromArgb(220, 38, 38);

                lblNumStatus.Text = "Đã khóa";
                lblNumStatus.ForeColor = Color.FromArgb(220, 38, 38);

                lblStateStatus.Text = "Đã khóa";
                lblStateStatus.ForeColor = Color.FromArgb(220, 38, 38);

                pnlToggleTrack.FillColor = Color.FromArgb(203, 213, 225);
                pnlToggleKnob.Location = new Point(4, 3);
            }
        }

        private async Task ToggleCardLockAsync()
        {
            if (_isUpdatingStatus || _userCards == null || _userCards.Count == 0) return;

            var card = _userCards[_currentUserCardIndex];
            bool newIsLocked = card.IsActive;

            _isUpdatingStatus = true;
            try
            {
                bool success = await _creditService.UpdateCardStatusAsync(card.AccountNumber, newIsLocked);
                if (success)
                {
                    card.Status = newIsLocked ? "Locked" : "Active";
                    UpdateStatusUI(card.IsActive);

                    string msg = newIsLocked
                        ? $"Thẻ ghi nợ ({card.MaskedCardNumber}) đã được KHÓA an toàn."
                        : $"Thẻ ghi nợ ({card.MaskedCardNumber}) đã được MỞ KHÓA thành công.";

                    MessageBox.Show(msg, "Thông báo trạng thái thẻ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật trạng thái thẻ. Vui lòng kiểm tra lại kết nối cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi cập nhật trạng thái: {ex.Message}", "Lỗi ngoại lệ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isUpdatingStatus = false;
            }
        }

        private void BtnViewCardInfo_Click(object sender, EventArgs e)
        {
            if (_userCards == null || _userCards.Count == 0) return;

            var card = _userCards[_currentUserCardIndex];
            _isInfoRevealed = !_isInfoRevealed;

            btnViewCardInfo.Text = _isInfoRevealed ? "Ẩn thông tin thẻ" : "Xem thông tin thẻ";

            lblCardNumber.Text = _isInfoRevealed ? card.FullCardNumber : card.MaskedCardNumber;
            lblNumValue.Text = _isInfoRevealed ? card.FullCardNumber : card.MaskedCardNumber;
            lblBalValue.Text = _isInfoRevealed ? _creditService.FormatCurrency(card.Balance, card.Currency) : "**** **** ****";

            if (_isInfoRevealed)
            {
                MessageBox.Show(
                    $"THÔNG TIN CHI TIẾT THẺ GHI NỢ\n\n" +
                    $"• Chủ thẻ: {card.CardHolderName}\n" +
                    $"• Số thẻ: {card.FullCardNumber}\n" +
                    $"• Mã bảo mật CVV: {card.CVV}\n" +
                    $"• Ngày cấp thẻ: {card.IssueDate:dd/MM/yyyy}\n" +
                    $"• Ngày hết hạn: {card.ExpiryDate:dd/MM/yyyy}\n" +
                    $"• Tài khoản liên kết: {card.AccountNumber}\n" +
                    $"• Ngân hàng: {card.BankName}\n" +
                    $"• Số dư hiện tại: {_creditService.FormatCurrency(card.Balance, card.Currency)}",
                    "Thông tin bảo mật thẻ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}
