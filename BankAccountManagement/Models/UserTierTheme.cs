using System;
using System.Drawing;

namespace BankAccountManagement.Models
{
    /// <summary>
    /// Định nghĩa bảng màu và phong cách giao diện theo từng Hạng thành viên (Tương tự các App ngân hàng Vietcombank Priority, MB Private, Techcombank, VPBank Diamond).
    /// </summary>
    public class UserTierTheme
    {
        // ==========================================
        // CÁC HẰNG SỐ PHÂN HẠNG THEO SỐ DƯ
        // ==========================================
        public const decimal TIER_DIAMOND_MIN = 1000000000m; // Trên 1 tỷ (>= 1.000.000.000 đ)
        public const decimal TIER_GOLD_MIN    = 500000000m;  // 500 triệu - 1 tỷ (500.000.000 đ)
        public const decimal TIER_SILVER_MIN  = 200000000m;  // 200 triệu - 500 triệu (200.000.000 đ)
        // Dưới 200 triệu: Hạng Tiêu Chuẩn (< 200.000.000 đ)

        /// <summary>
        /// Xác định Hạng thành viên dựa trên số dư tài khoản theo đúng quy định:
        /// - Diamond (Kim Cương): Trên 1 tỷ (>= 1.000.000.000 đ)
        /// - Gold (Vàng): Từ 500 triệu đến dưới 1 tỷ (500.000.000 đ - 1.000.000.000 đ)
        /// - Silver (Bạc): Từ 200 triệu đến dưới 500 triệu (200.000.000 đ - 500.000.000 đ)
        /// - Standard (Tiêu chuẩn): Dưới 200 triệu (< 200.000.000 đ - Người mới bắt đầu)
        /// </summary>
        public static string DetermineTierByBalance(decimal balance)
        {
            if (balance >= TIER_DIAMOND_MIN)
                return "Hạng Kim Cương";
            if (balance >= TIER_GOLD_MIN)
                return "Hạng Vàng";
            if (balance >= TIER_SILVER_MIN)
                return "Hạng Bạc";
            return "Hạng Tiêu Chuẩn";
        }

        /// <summary>
        /// Cập nhật CurrentSession.SelectedUserTier nếu số dư chuyển sang khoảng phân hạng mới.
        /// Trả về true nếu hạng bị thay đổi.
        /// Lưu ý: Nếu người dùng đang bật chế độ kiểm thử hạng qua TopBar (IsManualTierOverride == true),
        /// hệ thống sẽ KHÔNG tự động ghi đè hạng.
        /// </summary>
        public static bool UpdateSessionTierByBalance(decimal balance)
        {
            if (BankAccountManagement.Services.CurrentSession.IsManualTierOverride)
            {
                return false; // Người dùng đang chủ động chọn hạng để test qua ComboBox
            }

            string targetTier = DetermineTierByBalance(balance);
            if (BankAccountManagement.Services.CurrentSession.SelectedUserTier != targetTier)
            {
                BankAccountManagement.Services.CurrentSession.SelectedUserTier = targetTier;
                return true;
            }
            return false;
        }

        public string TierKey { get; set; }           // "Diamond", "Gold", "Silver", "Standard"
        public string TierName { get; set; }          // "Hạng Kim Cương", "Hạng Vàng", ...
        public string BadgeText { get; set; }         // "💎 VIP DIAMOND", "🥇 GOLD VIP", ...
        public string CardSubtitle { get; set; }      // "PRIVATE BANKING BLACK & GOLD", ...
        public string TierDisplayName { get; set; }   // "💎 Hạng Kim Cương (> 1 Tỷ)"
        public string TierPerkDescription { get; set; } // "✨ Miễn 100% phí GD & Hoàn 2%"
        public Color TierLabelColor { get; set; }
        public string OfferTitle { get; set; }
        public string OfferDescription { get; set; }

        // ==========================================
        // 1. THANH MENU BÊN TRÁI (SIDEBAR)
        // ==========================================
        public Color SidebarBg { get; set; }
        public Color SidebarHeaderBg { get; set; }
        public Color SidebarBorderColor { get; set; }
        public Color SidebarLogoColor { get; set; }
        public Color SidebarButtonText { get; set; }
        public Color SidebarButtonHover { get; set; }
        public Color SidebarButtonActive { get; set; }
        public Color SidebarButtonActiveText { get; set; }

        // ==========================================
        // 2. THANH TIÊU ĐỀ TRÊN CÙNG (TOPBAR)
        // ==========================================
        public Color TopBarBg { get; set; }
        public Color TopBarBorderColor { get; set; }
        public Color TopBarTextColor { get; set; }

        // ==========================================
        // 3. NỀN ỨNG DỤNG TỔNG THỂ (MAIN APP & FORM)
        // ==========================================
        public Color AppBackground { get; set; }

        // ==========================================
        // 4. THẺ NGÂN HÀNG VIP (DIGITAL BANK CARD)
        // ==========================================
        public Color CardBackground { get; set; }
        public Color CardBorder { get; set; }
        public int CardBorderSize { get; set; } = 1;
        public Color PrimaryTextColor { get; set; }    // Chữ số tài khoản, số dư, tên
        public Color SecondaryTextColor { get; set; }  // Chữ phụ, nhãn
        public Color SubtleTextColor { get; set; }     // Ghi chú nhỏ
        public Color AccentColor { get; set; }         // Nút bấm, icon, chip, vạch menu capsule
        public Color AccentTextColor { get; set; }     // Màu chữ trên accent
        public Color BadgeBackground { get; set; }
        public Color BadgeTextColor { get; set; }
        public Color ActionButtonBg { get; set; }
        public Color ActionButtonHover { get; set; }
        public Color ActionButtonBorder { get; set; }
        public Color ActionButtonText { get; set; }

        // ==========================================
        // 5. CÁC CARD BỀ MẶT NỘI DUNG (DEBT, SPENDING, FORM CARDS)
        // ==========================================
        public Color CardSurfaceBg { get; set; }
        public Color CardSurfaceBorder { get; set; }
        public Color CardSurfaceTitle { get; set; }
        public Color CardSurfaceText { get; set; }
        public Color CardSurfaceSubText { get; set; }
        public Color CardSurfaceHighlight { get; set; }
        public Color CardProgressTrackBg { get; set; }
        public Color CardProgressBar { get; set; }
        public Color ButtonAccentBg { get; set; }
        public Color ButtonAccentText { get; set; }
        public Color ButtonAccentHover { get; set; }

        public static UserTierTheme GetTheme(string tierKeyOrName)
        {
            if (string.IsNullOrWhiteSpace(tierKeyOrName)) tierKeyOrName = "Standard";
            string lower = tierKeyOrName.ToLower();

            // ========================================================
            // 1. HẠNG KIM CƯƠNG (DIAMOND / PRIORITY) - ĐEN CHỮ VÀNG VIP
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

                    // Sidebar - Đen tuyền sang trọng và viền chữ vàng
                    SidebarBg = Color.FromArgb(12, 12, 16),
                    SidebarHeaderBg = Color.FromArgb(8, 8, 12),
                    SidebarBorderColor = Color.FromArgb(45, 38, 20),
                    SidebarLogoColor = Color.FromArgb(250, 204, 21),
                    SidebarButtonText = Color.FromArgb(228, 228, 231),
                    SidebarButtonHover = Color.FromArgb(28, 25, 23),
                    SidebarButtonActive = Color.FromArgb(42, 35, 12),
                    SidebarButtonActiveText = Color.FromArgb(250, 204, 21),

                    // TopBar
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
            // 2. HẠNG VÀNG (GOLD VIP) - TONE HOÀNG GIA VÀNG NÂU ESPRESSO
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

                    // Digital Bank Card (Espresso Nâu Vàng Hoàng Gia theo đúng step 9552)
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
            // 3. HẠNG BẠC (SILVER PRIVILEGE) - TONE BẠC TITAN METALLIC
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

                    // Digital Bank Card (Titanium Silver Slate 800 theo đúng step 9552)
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
            // 4. HẠNG TIÊU CHUẨN (STANDARD CLASSIC) - NAVY TÍM TRUYỀN THỐNG
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

                // Digital Bank Card (Classic Deep Navy Slate 900 theo đúng step 9552)
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
