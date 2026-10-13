using BankAccountManagement.Models;
using System;
using System.Data.SqlClient;

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

        // ============================================================
        /// <summary>
        /// Load lại thông tin user hiện tại từ DB.
        /// Cập nhật lại toàn bộ property của <see cref="User"/>.
        /// </summary>
        /// <returns>True nếu refresh thành công, False nếu user không tồn tại hoặc lỗi.</returns>
        public static bool Refresh()
        {
            if (User == null)
            {
                DebugLog("Refresh: bỏ qua — chưa login");
                return false;
            }

            try
            {
                const string sql = @"
                    SELECT  MaNguoiDung,
                            TenDangNhap,
                            Quyen,
                            TrangThai,
                            MaKH,
                            HoTen,
                            Email
                    FROM    dbo.NguoiDung
                    WHERE   MaNguoiDung = @Id";

                DatabaseConfig config = DatabaseConfigManager.Load();

                using (var conn = DatabaseService.CreateConnection(config))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", User.MaNguoiDung);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                DebugLog($"Refresh: user #{User.MaNguoiDung} không tồn tại → clear");
                                User = null;
                                return false;
                            }

                            // Cập nhật lại từng property (giữ nguyên reference User)
                            User.TenDangNhap = reader.GetString(reader.GetOrdinal("TenDangNhap"));
                            User.Quyen = reader.GetString(reader.GetOrdinal("Quyen"));
                            User.TrangThai = reader.GetString(reader.GetOrdinal("TrangThai"));

                            User.MaKH = reader.IsDBNull(reader.GetOrdinal("MaKH"))
                                        ? null : reader.GetString(reader.GetOrdinal("MaKH"));

                            User.HoTen = reader.IsDBNull(reader.GetOrdinal("HoTen"))
                                        ? null : reader.GetString(reader.GetOrdinal("HoTen"));

                            User.Email = reader.IsDBNull(reader.GetOrdinal("Email"))
                                        ? null : reader.GetString(reader.GetOrdinal("Email"));
                        }
                    }
                }

                DebugLog($"Refresh OK: {User.TenDangNhap} | MaKH={User.MaKH ?? "NULL"}");
                return true;
            }
            catch (Exception ex)
            {
                DebugLog($"Refresh lỗi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Đánh dấu user đã hoàn tất hồ sơ khách hàng.
        /// Gọi ngay trong <c>CustomerService.SaveCustomerInfo()</c> sau khi COMMIT.
        /// </summary>
        /// <param name="maKH">Mã KH vừa tạo (VD: "KH000001").</param>
        /// <param name="hoTen">Họ tên (optional — nếu null sẽ giữ nguyên).</param>
        /// <param name="email">Email (optional).</param>
        public static void MarkCustomerInfoCompleted(string maKH, string hoTen = null, string email = null)
        {
            if (User == null)
            {
                DebugLog("MarkCustomerInfoCompleted: bỏ qua — chưa login");
                return;
            }

            if (string.IsNullOrWhiteSpace(maKH))
            {
                DebugLog("MarkCustomerInfoCompleted: maKH rỗng — bỏ qua");
                return;
            }

            User.MaKH = maKH;

            if (!string.IsNullOrWhiteSpace(hoTen))
                User.HoTen = hoTen;

            if (!string.IsNullOrWhiteSpace(email))
                User.Email = email;

            DebugLog($"MarkCustomerInfoCompleted: {User.TenDangNhap} → MaKH={maKH}");
        }

        // ============================================================
        //  HELPER
        // ============================================================
        private static void DebugLog(string message)
        {
            System.Diagnostics.Debug.WriteLine($"[Session] {message}");
        }
    }
}