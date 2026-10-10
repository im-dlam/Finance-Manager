using BankAccountManagement.Models;
using System;

namespace BankAccountManagement.Services
{
    public static class CurrentSession
    {
        /// <summary>User hiện tại – null nếu chưa đăng nhập.</summary>
        public static User User { get; private set; }

        /// <summary>True nếu đã đăng nhập thành công.</summary>
        public static bool IsLoggedIn => User != null;

        /// <summary>True nếu người dùng hiện tại là Admin.</summary>
        public static bool IsAdmin => User != null && string.Equals(User.Quyen, "Admin", StringComparison.OrdinalIgnoreCase);

        /// <summary>True nếu người dùng hiện tại là Member.</summary>
        public static bool IsMember => User != null && !IsAdmin;

        /// <summary>Mã người dùng hiện tại.</summary>
        public static int MaNguoiDung => User?.MaNguoiDung ?? 0;

        /// <summary>Mã khách hàng liên kết.</summary>
        public static string MaKH => User?.MaKH;

        /// <summary>Tên đăng nhập hiện tại.</summary>
        public static string TenDangNhap => User?.TenDangNhap;

        /// <summary>Gán thông tin user sau khi đăng nhập thành công.</summary>
        public static void SetUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            User = user;
        }

        /// <summary>Xóa thông tin phiên khi đăng xuất.</summary>
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