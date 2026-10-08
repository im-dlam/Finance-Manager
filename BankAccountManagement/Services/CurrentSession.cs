using BankAccountManagement.Models;
using System;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Lưu thông tin user đang đăng nhập trong suốt vòng đời ứng dụng.
    /// Dùng static để mọi service/form truy cập được mà không cần truyền tham số.
    /// 
    /// Vòng đời:
    /// - Set 1 lần khi LoginForm login thành công.
    /// - Đọc từ bất kỳ đâu (service, form, helper).
    /// - Clear khi Logout.
    /// </summary>
    public static class CurrentSession
    {
        /// <summary>User hiện tại – null nếu chưa đăng nhập.</summary>
        public static User User { get; private set; }

        /// <summary>True nếu đã đăng nhập.</summary>
        public static bool IsLoggedIn => User != null;

        /// <summary>True nếu user hiện tại là Admin.</summary>
        public static bool IsAdmin => User != null && string.Equals(User.Quyen, "Admin", StringComparison.OrdinalIgnoreCase);

        /// <summary>True nếu user hiện tại là Member.</summary>
        public static bool IsMember => User != null && !IsAdmin;

        /// <summary>Mã người dùng hiện tại (0 nếu chưa login).</summary>
        public static int MaNguoiDung => User?.MaNguoiDung ?? 0;

        /// <summary>Mã khách hàng liên kết (null nếu Admin hoặc chưa link).</summary>
        public static string MaKH => User?.MaKH;

        /// <summary>Tên đăng nhập hiện tại.</summary>
        public static string TenDangNhap => User?.TenDangNhap;

        /// <summary>
        /// Gán user sau khi login thành công.
        /// Chỉ cho phép set 1 lần – tránh code vô tình ghi đè.
        /// </summary>
        public static void SetUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            User = user;
        }

        /// <summary>
        /// Xóa session khi logout.
        /// Gọi trong LoginForm.ResetLoginForm() hoặc MainForm.btnLogout_Click().
        /// </summary>
        public static void Clear()
        {
            User = null;
        }

        public static void RequireAdmin()
        {
            if (!IsAdmin)
                throw new UnauthorizedAccessException(
                    "Chỉ quản trị viên mới được thực hiện thao tác này.");
        }
    }
}