using System;
using System.Drawing;

namespace BankAccountManagement.Models
{
    /// <summary>
    /// Định nghĩa bảng màu và phong cách giao diện theo từng Hạng thành viên ngân hàng (User Tier Theme).
    /// Lấy cảm hứng từ các ứng dụng ngân hàng số cao cấp như Vietcombank Priority, MB Private, Techcombank Private và VPBank Diamond.
    /// Cung cấp 4 phân hạng: Diamond (Kim Cương), Gold (Vàng), Silver (Bạc) và Standard (Tiêu Chuẩn).
    /// </summary>
    public class UserTierTheme
    {
        // ============================================================
        //  CÁC HẰNG SỐ PHÂN HẠNG THEO SỐ DƯ TÀI KHOẢN (TIER THRESHOLDS)
        // ============================================================

        /// <summary>Hạn mức số dư tối thiểu để đạt Hạng Kim Cương (>= 1 tỷ VNĐ).</summary>
        public const decimal TIER_DIAMOND_MIN = 1000000000m;

        /// <summary>Hạn mức số dư tối thiểu để đạt Hạng Vàng (>= 500 triệu VNĐ đến dưới 1 tỷ VNĐ).</summary>
        public const decimal TIER_GOLD_MIN    = 500000000m;

        /// <summary>Hạn mức số dư tối thiểu để đạt Hạng Bạc (>= 200 triệu VNĐ đến dưới 500 triệu VNĐ).</summary>
        public const decimal TIER_SILVER_MIN  = 200000000m;

        // Dưới 200 triệu: Mặc định xếp vào Hạng Tiêu Chuẩn (< 200.000.000 đ)

        /// <summary>
        /// Xác định Hạng thành viên dựa trên tổng số dư tài khoản theo đúng quy định:
        /// - Diamond (Kim Cương): Trên 1 tỷ (>= 1.000.000.000 đ)
        /// - Gold (Vàng): Từ 500 triệu đến dưới 1 tỷ (500.000.000 đ - 1.000.000.000 đ)
        /// - Silver (Bạc): Từ 200 triệu đến dưới 500 triệu (200.000.000 đ - 500.000.000 đ)
        /// - Standard (Tiêu chuẩn): Dưới 200 triệu (&lt; 200.000.000 đ - Người mới bắt đầu)
        /// </summary>
        /// <param name="balance">Tổng số dư khả dụng trên tất cả các tài khoản của khách hàng.</param>
        /// <returns>Tên chuỗi đại diện cho hạng thành viên.</returns>
        public static string DetermineTierByBalance(decimal balance)
        {
            // BƯỚC 1: Kiểm tra ngưỡng số dư cao nhất (Hạng Kim Cương)
            if (balance >= TIER_DIAMOND_MIN)
                return "Hạng Kim Cương";

            // BƯỚC 2: Kiểm tra ngưỡng số dư hạng nhì (Hạng Vàng)
            if (balance >= TIER_GOLD_MIN)
                return "Hạng Vàng";

            // BƯỚC 3: Kiểm tra ngưỡng số dư hạng ba (Hạng Bạc)
            if (balance >= TIER_SILVER_MIN)
                return "Hạng Bạc";

            // BƯỚC 4: Các trường hợp còn lại mặc định thuộc Hạng Tiêu Chuẩn
            return "Hạng Tiêu Chuẩn";
        }

        /// <summary>
        /// Tự động cập nhật thuộc tính CurrentSession.SelectedUserTier nếu số dư thực tế chuyển sang phân hạng mới.
        /// </summary>
        /// <param name="balance">Tổng số dư thực tế trong cơ sở dữ liệu.</param>
        /// <returns>True nếu hạng thành viên bị thay đổi và kích hoạt làm mới giao diện, False nếu giữ nguyên.</returns>
        public static bool UpdateSessionTierByBalance(decimal balance)
        {
            // BƯỚC 1: Nếu người dùng đang bật cờ kiểm thử thủ công qua ComboBox, bỏ qua việc tự động ghi đè
            if (BankAccountManagement.Services.CurrentSession.IsManualTierOverride)
            {
                return false;
            }

            // BƯỚC 2: Tính toán hạng tương ứng với số dư hiện tại
            string targetTier = DetermineTierByBalance(balance);

            // BƯỚC 3: Nếu hạng mới khác với hạng đang lưu trong CurrentSession, thực hiện cập nhật
            if (BankAccountManagement.Services.CurrentSession.SelectedUserTier != targetTier)
            {
                BankAccountManagement.Services.CurrentSession.SelectedUserTier = targetTier;
                return true;
            }

            return false;
        }

        // ============================================================
        //  THÔNG TIN ĐỊNH DANH VÀ NỘI DUNG ƯU ĐÃI THEO HẠNG
        // ============================================================

        /// <summary>Mã khóa định danh của hạng: "Diamond", "Gold", "Silver", hoặc "Standard".</summary>
        public string TierKey { get; set; }

        /// <summary>Tên hiển thị ngắn gọn của hạng (Ví dụ: "Hạng Kim Cương").</summary>
        public string TierName { get; set; }

        /// <summary>Huy hiệu huy chương hiển thị trên thẻ ATM (Ví dụ: "💎 VIP DIAMOND").</summary>
        public string BadgeText { get; set; }

        /// <summary>Dòng phụ đề thương hiệu dưới tên thẻ (Ví dụ: "PRIVATE BANKING BLACK &amp; GOLD").</summary>
        public string CardSubtitle { get; set; }

        /// <summary>Tên hiển thị đầy đủ kèm ngưỡng điều kiện (Ví dụ: "💎 Hạng Kim Cương (> 1 Tỷ)").</summary>
        public string TierDisplayName { get; set; }

        /// <summary>Mô tả ngắn gọn về đặc quyền quyền lợi dành cho hội viên (Ví dụ: "✨ Miễn 100% phí GD &amp; Hoàn 2%").</summary>
        public string TierPerkDescription { get; set; }

        /// <summary>Màu sắc chữ của nhãn tên phân hạng.</summary>
        public Color TierLabelColor { get; set; }

        /// <summary>Tiêu đề của khung quảng cáo / ưu đãi độc quyền.</summary>
        public string OfferTitle { get; set; }

        /// <summary>Nội dung chi tiết của ưu đãi dành cho hạng thành viên này.</summary>
        public string OfferDescription { get; set; }

        // ============================================================
        //  1. CẤU HÌNH MÀU THANH ĐIỀU HƯỚNG BÊN TRÁI (SIDEBAR)
        // ============================================================

        /// <summary>Màu nền chính của thanh menu Sidebar.</summary>
        public Color SidebarBg { get; set; }

        /// <summary>Màu nền khu vực Logo / Header phía trên Sidebar.</summary>
        public Color SidebarHeaderBg { get; set; }

        /// <summary>Màu đường viền phân cách Sidebar với màn hình làm việc.</summary>
        public Color SidebarBorderColor { get; set; }

        /// <summary>Màu sắc của chữ logo thương hiệu ngân hàng.</summary>
        public Color SidebarLogoColor { get; set; }

        /// <summary>Màu chữ hiển thị trên các nút menu bình thường.</summary>
        public Color SidebarButtonText { get; set; }

        /// <summary>Màu nền khi rê chuột (Hover) qua nút menu.</summary>
        public Color SidebarButtonHover { get; set; }

        /// <summary>Màu nền khi nút menu đang được chọn (Active).</summary>
        public Color SidebarButtonActive { get; set; }

        /// <summary>Màu chữ khi nút menu đang được chọn (Active).</summary>
        public Color SidebarButtonActiveText { get; set; }

        // ============================================================
        //  2. CẤU HÌNH MÀU THANH TIÊU ĐỀ TRÊN CÙNG (TOPBAR)
        // ============================================================

        /// <summary>Màu nền của thanh TopBar.</summary>
        public Color TopBarBg { get; set; }

        /// <summary>Màu đường viền phía dưới TopBar.</summary>
        public Color TopBarBorderColor { get; set; }

        /// <summary>Màu chữ và biểu tượng hiển thị trên TopBar.</summary>
        public Color TopBarTextColor { get; set; }

        // ============================================================
        //  3. CẤU HÌNH MÀU NỀN TỔNG THỂ ỨNG DỤNG (APP BACKGROUND)
        // ============================================================

        /// <summary>Màu nền phía sau các Form và Panel làm việc chính.</summary>
        public Color AppBackground { get; set; }

        // ============================================================
        //  4. CẤU HÌNH THẺ NGÂN HÀNG SỐ VIP (DIGITAL BANK CARD)
        // ============================================================

        /// <summary>Màu nền thẻ ngân hàng số (thường là hiệu ứng gradient giả lập hoặc đơn sắc).</summary>
        public Color CardBackground { get; set; }

        /// <summary>Màu đường viền bao quanh thẻ ATM số.</summary>
        public Color CardBorder { get; set; }

        /// <summary>Độ dày của đường viền thẻ ATM (1px hoặc 2px).</summary>
        public int CardBorderSize { get; set; } = 1;

        /// <summary>Màu chữ chính trên thẻ (số thẻ, họ tên, số dư lớn).</summary>
        public Color PrimaryTextColor { get; set; }

        /// <summary>Màu chữ phụ trên thẻ (nhãn phụ đề, ngày hết hạn).</summary>
        public Color SecondaryTextColor { get; set; }

        /// <summary>Màu chữ mờ (subtle) cho các thông tin bảo mật hoặc ghi chú nhỏ.</summary>
        public Color SubtleTextColor { get; set; }

        /// <summary>Màu nhấn (Accent) nổi bật dùng cho chip thẻ, biểu tượng trang trí.</summary>
        public Color AccentColor { get; set; }

        /// <summary>Màu chữ tương phản hiển thị trên nền AccentColor.</summary>
        public Color AccentTextColor { get; set; }

        /// <summary>Màu nền huy hiệu Badge trên thẻ.</summary>
        public Color BadgeBackground { get; set; }

        /// <summary>Màu chữ trên huy hiệu Badge.</summary>
        public Color BadgeTextColor { get; set; }

        /// <summary>Màu nền của nút thao tác nhanh trên thẻ.</summary>
        public Color ActionButtonBg { get; set; }

        /// <summary>Màu nền khi rê chuột vào nút thao tác nhanh.</summary>
        public Color ActionButtonHover { get; set; }

        /// <summary>Màu đường viền của nút thao tác nhanh trên thẻ.</summary>
        public Color ActionButtonBorder { get; set; }

        /// <summary>Màu chữ của nút thao tác nhanh trên thẻ.</summary>
        public Color ActionButtonText { get; set; }

        // ============================================================
        //  5. CẤU HÌNH BỀ MẶT THẺ THỐNG KÊ CHI TIÊU & DƯ NỢ (CARD SURFACE)
        // ============================================================

        /// <summary>Màu nền của các khung card thống kê nội dung.</summary>
        public Color CardSurfaceBg { get; set; }

        /// <summary>Màu đường viền của các khung card nội dung.</summary>
        public Color CardSurfaceBorder { get; set; }

        /// <summary>Màu tiêu đề của khung card nội dung.</summary>
        public Color CardSurfaceTitle { get; set; }

        /// <summary>Màu chữ nội dung bên trong khung card.</summary>
        public Color CardSurfaceText { get; set; }

        /// <summary>Màu chữ phụ bên trong khung card.</summary>
        public Color CardSurfaceSubText { get; set; }

        /// <summary>Màu chữ điểm nhấn nổi bật trên card.</summary>
        public Color CardSurfaceHighlight { get; set; }

        /// <summary>Màu nền đường ray thanh tiến độ (Progress Bar Track).</summary>
        public Color CardProgressTrackBg { get; set; }

        /// <summary>Màu vạch tiến độ chi tiêu (Progress Bar Fill).</summary>
        public Color CardProgressBar { get; set; }

        /// <summary>Màu nền nút bấm nổi bật trong khung card.</summary>
        public Color ButtonAccentBg { get; set; }

        /// <summary>Màu chữ trên nút bấm nổi bật.</summary>
        public Color ButtonAccentText { get; set; }

        /// <summary>Màu nền khi rê chuột vào nút bấm nổi bật.</summary>
        public Color ButtonAccentHover { get; set; }

        /// <summary>
        /// Lấy đối tượng giao diện UserTierTheme tương ứng với mã hạng hoặc tên phân hạng.
        /// </summary>
        /// <param name="tierKeyOrName">Tên hạng (ví dụ: "Diamond", "Gold", "Silver", "Standard", "Hạng Kim Cương",...).</param>
        /// <returns>Đối tượng UserTierTheme chứa đầy đủ mã màu đã được thiết kế tinh chỉnh.</returns>
        public static UserTierTheme GetTheme(string tierKeyOrName)
        {
            if (string.IsNullOrWhiteSpace(tierKeyOrName)) tierKeyOrName = "Standard";
            string lower = tierKeyOrName.ToLower();

            // ========================================================
            // BƯỚC 1: HẠNG KIM CƯƠNG (DIAMOND / PRIORITY) - ĐEN CHỮ VÀNG VIP
            // ========================================================
            if (lower.Contains("kim cuong") || lower.Contains("kim c") || lower.Contains("diamond") || 
                lower.Contains("priority") || lower.Contains("den") || lower.Contains("black"))
            {
                return new UserTierTheme
                {
                    TierKey = "Diamond",
                    TierName = "Hạng Kim Cương",
                    BadgeText = "💎 VIP DIAMOND",
                    CardSubtitle = "PRIVATE BANKING BLACK & GOLD",
                    TierDisplayName = "💎 Hạng Kim Cương (> 1 Tỷ)",
                    TierPerkDescription = "✨ Miễn 100% phí GD & Hoàn 2%",
                    TierLabelColor = Color.FromArgb(202, 138, 4),
                    OfferTitle = "💎 ĐẶC QUYỀN HỘI VIÊN KIM CƯƠNG",
                    OfferDescription = "Hạn mức vay vốn khả dụng lên đến 50,000,000 đ với lãi suất ưu đãi giải ngân ngay trong 5 phút.",

                    // Sidebar - Đen tuyền sang trọng và viền chữ vàng hoàng kim
                    SidebarBg = Color.FromArgb(12, 12, 16),
                    SidebarHeaderBg = Color.FromArgb(8, 8, 12),
                    SidebarBorderColor = Color.FromArgb(45, 38, 20),
                    SidebarLogoColor = Color.FromArgb(250, 204, 21),
                    SidebarButtonText = Color.FromArgb(228, 228, 231),
                    SidebarButtonHover = Color.FromArgb(28, 25, 23),
                    SidebarButtonActive = Color.FromArgb(42, 35, 12),
                    SidebarButtonActiveText = Color.FromArgb(250, 204, 21),

                    // TopBar - Đồng bộ tone nền tối viền vàng
                    TopBarBg = Color.FromArgb(12, 12, 16),
                    TopBarBorderColor = Color.FromArgb(202, 138, 4),
                    TopBarTextColor = Color.FromArgb(250, 204, 21),

                    // App Background
                    AppBackground = Color.FromArgb(18, 18, 23),

                    // Digital Bank Card (Đen tuyền Jet Black, viền vàng hoàng kim)
                    CardBackground = Color.FromArgb(10, 10, 12),
                    CardBorder = Color.FromArgb(234, 179, 8),
                    CardBorderSize = 2,
                    PrimaryTextColor = Color.FromArgb(250, 204, 21),
                    SecondaryTextColor = Color.FromArgb(253, 230, 138),
                    SubtleTextColor = Color.FromArgb(202, 138, 4),
                    AccentColor = Color.FromArgb(234, 179, 8),
                    AccentTextColor = Color.FromArgb(10, 10, 12),
                    BadgeBackground = Color.FromArgb(28, 25, 23),
                    BadgeTextColor = Color.FromArgb(250, 204, 21),
                    ActionButtonBg = Color.FromArgb(24, 24, 27),
                    ActionButtonHover = Color.FromArgb(39, 39, 42),
                    ActionButtonBorder = Color.FromArgb(234, 179, 8),
                    ActionButtonText = Color.FromArgb(250, 204, 21),

                    // Card Surface
                    CardSurfaceBg = Color.FromArgb(24, 24, 30),
                    CardSurfaceBorder = Color.FromArgb(55, 46, 20),
                    CardSurfaceTitle = Color.FromArgb(250, 204, 21),
                    CardSurfaceText = Color.FromArgb(228, 228, 231),
                    CardSurfaceSubText = Color.FromArgb(161, 161, 170),
                    CardSurfaceHighlight = Color.FromArgb(254, 240, 138),
                    CardProgressTrackBg = Color.FromArgb(39, 39, 42),
                    CardProgressBar = Color.FromArgb(234, 179, 8),
                    ButtonAccentBg = Color.FromArgb(234, 179, 8),
                    ButtonAccentText = Color.FromArgb(10, 10, 13),
                    ButtonAccentHover = Color.FromArgb(250, 204, 21)
                };
            }

            // ========================================================
            // BƯỚC 2: HẠNG VÀNG (GOLD VIP) - TONE HOÀNG GIA VÀNG NÂU ESPRESSO
            // ========================================================
            if (lower.Contains("vang") || lower.Contains("v") || lower.Contains("gold"))
            {
                return new UserTierTheme
                {
                    TierKey = "Gold",
                    TierName = "Hạng Vàng",
                    BadgeText = "🥇 GOLD VIP",
                    CardSubtitle = "ROYAL GOLD PRIVILEGE",
                    TierDisplayName = "🥇 Hạng Vàng (500Tr - 1 Tỷ)",
                    TierPerkDescription = "✨ Ưu tiên GD & Hoàn 1.5%",
                    TierLabelColor = Color.FromArgb(180, 83, 9),
                    OfferTitle = "🥇 ĐẶC QUYỀN HỘI VIÊN VÀNG",
                    OfferDescription = "Hạn mức vay vốn khả dụng lên đến 30,000,000 đ với lãi suất ưu đãi chỉ từ 0.04%/ngày.",

                    // Sidebar - Tối ấm sang trọng (Dark Charcoal Warm Amber)
                    SidebarBg = Color.FromArgb(24, 20, 18),
                    SidebarHeaderBg = Color.FromArgb(18, 14, 12),
                    SidebarBorderColor = Color.FromArgb(120, 53, 15),
                    SidebarLogoColor = Color.FromArgb(245, 158, 11),
                    SidebarButtonText = Color.FromArgb(254, 243, 199),
                    SidebarButtonHover = Color.FromArgb(44, 34, 28),
                    SidebarButtonActive = Color.FromArgb(60, 42, 20),
                    SidebarButtonActiveText = Color.FromArgb(251, 191, 36),

                    TopBarBg = Color.FromArgb(24, 20, 18),
                    TopBarBorderColor = Color.FromArgb(180, 83, 9),
                    TopBarTextColor = Color.FromArgb(245, 158, 11),

                    AppBackground = Color.FromArgb(28, 22, 18),

                    // Digital Bank Card (Espresso Nâu Vàng Hoàng Gia)
                    CardBackground = Color.FromArgb(41, 26, 12),
                    CardBorder = Color.FromArgb(217, 119, 6),
                    CardBorderSize = 2,
                    PrimaryTextColor = Color.FromArgb(254, 243, 199),
                    SecondaryTextColor = Color.FromArgb(253, 230, 138),
                    SubtleTextColor = Color.FromArgb(180, 83, 9),
                    AccentColor = Color.FromArgb(245, 158, 11),
                    AccentTextColor = Color.FromArgb(41, 26, 12),
                    BadgeBackground = Color.FromArgb(69, 26, 3),
                    BadgeTextColor = Color.FromArgb(254, 243, 199),
                    ActionButtonBg = Color.FromArgb(69, 26, 3),
                    ActionButtonHover = Color.FromArgb(120, 53, 15),
                    ActionButtonBorder = Color.FromArgb(217, 119, 6),
                    ActionButtonText = Color.FromArgb(254, 243, 199),

                    CardSurfaceBg = Color.FromArgb(45, 28, 13),
                    CardSurfaceBorder = Color.FromArgb(120, 53, 15),
                    CardSurfaceTitle = Color.FromArgb(251, 191, 36),
                    CardSurfaceText = Color.FromArgb(254, 243, 199),
                    CardSurfaceSubText = Color.FromArgb(217, 119, 6),
                    CardSurfaceHighlight = Color.FromArgb(253, 230, 138),
                    CardProgressTrackBg = Color.FromArgb(60, 36, 16),
                    CardProgressBar = Color.FromArgb(245, 158, 11),
                    ButtonAccentBg = Color.FromArgb(245, 158, 11),
                    ButtonAccentText = Color.FromArgb(45, 16, 2),
                    ButtonAccentHover = Color.FromArgb(251, 191, 36)
                };
            }

            // ========================================================
            // BƯỚC 3: HẠNG BẠC (SILVER PRIVILEGE) - TONE BẠC TITAN METALLIC
            // ========================================================
            if (lower.Contains("bac") || lower.Contains("b") || lower.Contains("silver") || lower.Contains("titan"))
            {
                return new UserTierTheme
                {
                    TierKey = "Silver",
                    TierName = "Hạng Bạc",
                    BadgeText = "🥈 SILVER VIP",
                    CardSubtitle = "TITANIUM METALLIC CARD",
                    TierDisplayName = "🥈 Hạng Bạc (200Tr - 500Tr)",
                    TierPerkDescription = "✨ Miễn phí CK & Tích điểm GD",
                    TierLabelColor = Color.FromArgb(71, 85, 105),
                    OfferTitle = "🥈 ĐẶC QUYỀN HỘI VIÊN BẠC",
                    OfferDescription = "Hạn mức vay vốn khả dụng lên đến 20,000,000 đ với thủ tục xét duyệt nhanh chóng.",

                    // Sidebar - Deep Slate 900
                    SidebarBg = Color.FromArgb(15, 23, 42),
                    SidebarHeaderBg = Color.FromArgb(10, 15, 28),
                    SidebarBorderColor = Color.FromArgb(51, 65, 85),
                    SidebarLogoColor = Color.FromArgb(226, 232, 240),
                    SidebarButtonText = Color.FromArgb(203, 213, 225),
                    SidebarButtonHover = Color.FromArgb(30, 41, 59),
                    SidebarButtonActive = Color.FromArgb(51, 65, 85),
                    SidebarButtonActiveText = Color.FromArgb(255, 255, 255),

                    TopBarBg = Color.FromArgb(15, 23, 42),
                    TopBarBorderColor = Color.FromArgb(71, 85, 105),
                    TopBarTextColor = Color.FromArgb(203, 213, 225),

                    AppBackground = Color.FromArgb(17, 24, 39),

                    // Digital Bank Card (Titanium Silver Slate 800)
                    CardBackground = Color.FromArgb(30, 41, 59),
                    CardBorder = Color.FromArgb(148, 163, 184),
                    CardBorderSize = 2,
                    PrimaryTextColor = Color.FromArgb(248, 250, 252),
                    SecondaryTextColor = Color.FromArgb(203, 213, 225),
                    SubtleTextColor = Color.FromArgb(148, 163, 184),
                    AccentColor = Color.FromArgb(148, 163, 184),
                    AccentTextColor = Color.FromArgb(15, 23, 42),
                    BadgeBackground = Color.FromArgb(51, 65, 85),
                    BadgeTextColor = Color.FromArgb(241, 245, 249),
                    ActionButtonBg = Color.FromArgb(51, 65, 85),
                    ActionButtonHover = Color.FromArgb(71, 85, 105),
                    ActionButtonBorder = Color.FromArgb(148, 163, 184),
                    ActionButtonText = Color.FromArgb(248, 250, 252),

                    CardSurfaceBg = Color.FromArgb(30, 41, 59),
                    CardSurfaceBorder = Color.FromArgb(71, 85, 105),
                    CardSurfaceTitle = Color.FromArgb(241, 245, 249),
                    CardSurfaceText = Color.FromArgb(226, 232, 240),
                    CardSurfaceSubText = Color.FromArgb(148, 163, 184),
                    CardSurfaceHighlight = Color.FromArgb(255, 255, 255),
                    CardProgressTrackBg = Color.FromArgb(51, 65, 85),
                    CardProgressBar = Color.FromArgb(148, 163, 184),
                    ButtonAccentBg = Color.FromArgb(148, 163, 184),
                    ButtonAccentText = Color.FromArgb(15, 23, 42),
                    ButtonAccentHover = Color.FromArgb(203, 213, 225)
                };
            }

            // ========================================================
            // BƯỚC 4: HẠNG TIÊU CHUẨN (STANDARD CLASSIC) - NAVY TÍM TRUYỀN THỐNG
            // ========================================================
            return new UserTierTheme
            {
                TierKey = "Standard",
                TierName = "Hạng Tiêu Chuẩn",
                BadgeText = "⭐ STANDARD",
                CardSubtitle = "STANDARD DIGITAL CARD",
                TierDisplayName = "⭐ Hạng Tiêu Chuẩn (< 200Tr)",
                TierPerkDescription = "✨ Miễn phí duy trì & Bảo mật 24/7",
                TierLabelColor = Color.FromArgb(37, 99, 235),
                OfferTitle = "⭐ TIỆN ÍCH DÀNH CHO BẠN",
                OfferDescription = "Đăng ký gói vay tiêu dùng linh hoạt từ 5,000,000 đ đến 15,000,000 đ.",

                // Màu tím truyền thống gốc của phần mềm
                SidebarBg = Color.FromArgb(64, 64, 122),
                SidebarHeaderBg = Color.FromArgb(44, 44, 84),
                SidebarBorderColor = Color.FromArgb(44, 44, 84),
                SidebarLogoColor = Color.White,
                SidebarButtonText = Color.White,
                SidebarButtonHover = Color.FromArgb(71, 71, 135),
                SidebarButtonActive = Color.FromArgb(44, 44, 84),
                SidebarButtonActiveText = Color.White,

                TopBarBg = Color.FromArgb(64, 64, 122),
                TopBarBorderColor = Color.FromArgb(44, 44, 84),
                TopBarTextColor = Color.White,

                AppBackground = Color.FromArgb(248, 250, 252),

                // Digital Bank Card (Classic Deep Navy Slate 900)
                CardBackground = Color.FromArgb(15, 23, 42),
                CardBorder = Color.FromArgb(51, 65, 85),
                CardBorderSize = 1,
                PrimaryTextColor = Color.White,
                SecondaryTextColor = Color.FromArgb(191, 219, 254),
                SubtleTextColor = Color.FromArgb(100, 116, 139),
                AccentColor = Color.FromArgb(37, 99, 235),
                AccentTextColor = Color.White,
                BadgeBackground = Color.FromArgb(30, 41, 59),
                BadgeTextColor = Color.FromArgb(191, 219, 254),
                ActionButtonBg = Color.FromArgb(30, 41, 59),
                ActionButtonHover = Color.FromArgb(51, 65, 85),
                ActionButtonBorder = Color.FromArgb(71, 85, 105),
                ActionButtonText = Color.White,

                CardSurfaceBg = Color.White,
                CardSurfaceBorder = Color.FromArgb(226, 232, 240),
                CardSurfaceTitle = Color.FromArgb(15, 23, 42),
                CardSurfaceText = Color.FromArgb(71, 85, 105),
                CardSurfaceSubText = Color.FromArgb(100, 116, 139),
                CardSurfaceHighlight = Color.FromArgb(15, 23, 42),
                CardProgressTrackBg = Color.FromArgb(241, 245, 249),
                CardProgressBar = Color.FromArgb(37, 99, 235),
                ButtonAccentBg = Color.FromArgb(15, 23, 42),
                ButtonAccentText = Color.White,
                ButtonAccentHover = Color.FromArgb(30, 41, 59)
            };
        }
    }
}
