using BankAccountManagement.Models;
using System;

namespace BankAccountManagement.Services
{
    public static class CurrentSession
    {
        public static User User { get; private set; }

        public static bool IsLoggedIn => User != null;
        public static bool IsAdmin => User?.IsAdmin == true;
        public static bool IsMember => User != null && !IsAdmin;

        public static int MaNguoiDung => User?.MaNguoiDung ?? 0;
        public static string TenDangNhap => User?.TenDangNhap;
        public static string MaKH => User?.MaKH;
        public static string HoTen => User?.HoTen;
        public static string Email => User?.Email;

        /// <summary>True nếu user đã có hồ sơ KH (đã nhập ở CustomerInfoForm).</summary>
        public static bool HasCustomerInfo =>
            User != null && !string.IsNullOrWhiteSpace(User.MaKH);

        /// <summary>True nếu user cần nhập hồ sơ (Member + chưa có MaKH).</summary>
        public static bool NeedsCustomerInfo =>
            User != null && !User.IsAdmin && !HasCustomerInfo;

        public static void SetUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            User = user;
            System.Diagnostics.Debug.WriteLine(
                $"[Session] SetUser: {user.TenDangNhap} | " +
                $"Quyen={user.Quyen} | MaKH={user.MaKH ?? "NULL"}");
        }

        public static void Clear()
        {
            System.Diagnostics.Debug.WriteLine($"[Session] Clear {User?.TenDangNhap}");
            User = null;
        }

        public static void RequireAdmin()
        {
            if (!IsAdmin) throw new UnauthorizedAccessException(
                "Chỉ Admin mới được thực hiện.");
        }
    }
}