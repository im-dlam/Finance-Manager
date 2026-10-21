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
        public static string MaNganHang => User?.MaNganHang;
        public static string TenNganHang => User?.TenNganHang;

        /// <summary>
        /// Cờ đánh dấu người dùng đang tự chọn hạng trên TopBar ComboBox để kiểm thử giao diện.
        /// Khi true, hệ thống không tự động ghi đè hạng dựa trên số dư tài khoản.
        /// </summary>
        public static bool IsManualTierOverride { get; set; } = false;

        /// <summary>Hạng thành viên hiện tại (Mặc định cho người mới bắt đầu: Hạng Tiêu Chuẩn - Dưới 200 triệu).</summary>
        private static string _selectedUserTier = "Hạng Tiêu Chuẩn";
        public static event Action<string> OnTierChanged;

        /// <summary>Hạng thành viên hiện tại (Mặc định cho người mới bắt đầu: Hạng Tiêu Chuẩn - Dưới 200 triệu).</summary>
        public static string SelectedUserTier
        {
            get => _selectedUserTier;
            set
            {
                if (_selectedUserTier != value)
                {
                    _selectedUserTier = value;
                    OnTierChanged?.Invoke(value);
                }
            }
        }

        /// <summary>
        /// Lấy tổng số dư thực tế tất cả tài khoản của người dùng hiện tại từ CSDL.
        /// </summary>
        public static decimal GetUserTotalBalance()
        {
            if (User == null) return 0m;
            try
            {
                string maKH = User.MaKH ?? "";
                string uName = User.TenDangNhap ?? "";
                DatabaseConfig config = DatabaseConfigManager.Load();
                using (var conn = DatabaseService.CreateConnection(config))
                {
                    conn.Open();
                    const string sql = @"
                        SELECT ISNULL(SUM(TK.SoDuHienTai), 0)
                        FROM dbo.TaiKhoan TK
                        WHERE (TK.MaKH = @MaKH OR TK.MaKH IN (SELECT ND.MaKH FROM dbo.NguoiDung ND WHERE ND.TenDangNhap = @User AND ND.MaKH IS NOT NULL))
                          AND (TK.TrangThai = 'Active' OR TK.TrangThai = N'Đang hoạt động' OR TK.TrangThai IS NULL)";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        cmd.Parameters.AddWithValue("@User", uName);
                        object res = cmd.ExecuteScalar();
                        return (res != null && res != DBNull.Value) ? Convert.ToDecimal(res) : 0m;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[Session] GetUserTotalBalance error: {ex.Message}");
                return 0m;
            }
        }

        /// <summary>
        /// Cập nhật hạng thành viên theo khoảng số dư thực tế trong CSDL.
        /// </summary>
        public static void RefreshTierByBalance()
        {
            if (!IsMember) return;
            decimal totalBalance = GetUserTotalBalance();
            UserTierTheme.UpdateSessionTierByBalance(totalBalance);
        }

        /// <summary>True nếu user đã có hồ sơ KH đầy đủ và hợp lệ (không phải dummy).</summary>
        public static bool HasCustomerInfo =>
            User != null && (User.IsAdmin || (User.HasCompleteProfile && !string.IsNullOrWhiteSpace(User.MaKH)));

        /// <summary>True nếu user cần nhập hồ sơ (Member + đăng nhập lần đầu hoặc chưa đủ thông tin).</summary>
        public static bool NeedsCustomerInfo =>
            User != null && !User.IsAdmin && (User.IsFirstLogin || !User.HasCompleteProfile || string.IsNullOrWhiteSpace(User.MaKH));

        public static void SetUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            User = user;
            IsManualTierOverride = false; // Người mới bắt đầu hoặc vừa đăng nhập: tự động theo số dư
            decimal initBal = GetUserTotalBalance();
            _selectedUserTier = UserTierTheme.DetermineTierByBalance(initBal);
            System.Diagnostics.Debug.WriteLine(
                $"[Session] SetUser: {user.TenDangNhap} | " +
                $"Quyen={user.Quyen} | MaKH={user.MaKH ?? "NULL"} | Tier={_selectedUserTier} | IsFirstLogin={user.IsFirstLogin} | HasCompleteProfile={user.HasCompleteProfile}");
        }

        public static void Clear()
        {
            System.Diagnostics.Debug.WriteLine($"[Session] Clear {User?.TenDangNhap}");
            User = null;
            IsManualTierOverride = false;
            _selectedUserTier = "Hạng Tiêu Chuẩn";
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
                    SELECT  nd.MaNguoiDung,
                            nd.TenDangNhap,
                            nd.Quyen,
                            nd.TrangThai,
                            nd.MaKH,
                            nd.HoTen,
                            nd.Email,
                            nd.LanDangNhap,
                            kh.MaKH      AS MaKHReal,
                            kh.CMND_CCCD,
                            kh.SoDienThoai,
                            kh.DiaChi,
                            kh.NgaySinh
                    FROM    dbo.NguoiDung nd
                    LEFT JOIN dbo.ThongTinKhachHang kh 
                           ON kh.MaKH = nd.MaKH
                    WHERE   nd.MaNguoiDung = @Id";

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
                                DebugLog($"Refresh: user #{User.MaNguoiDung} không tồn tại — clear");
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

                            bool isFirstLogin = reader.IsDBNull(reader.GetOrdinal("LanDangNhap"));
                            User.LanDangNhap = isFirstLogin ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("LanDangNhap"));
                            User.IsFirstLogin = isFirstLogin;

                            string maKHReal = reader.IsDBNull(reader.GetOrdinal("MaKHReal")) ? null : reader.GetString(reader.GetOrdinal("MaKHReal"));
                            string cmnd = reader.IsDBNull(reader.GetOrdinal("CMND_CCCD")) ? null : reader.GetString(reader.GetOrdinal("CMND_CCCD"));
                            string sdt = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? null : reader.GetString(reader.GetOrdinal("SoDienThoai"));
                            string diaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? null : reader.GetString(reader.GetOrdinal("DiaChi"));
                            bool hasDob = !reader.IsDBNull(reader.GetOrdinal("NgaySinh"));

                            bool isDummyCccd = !string.IsNullOrWhiteSpace(cmnd) && (cmnd.StartsWith("001200") && cmnd.Length == 12);
                            bool isDummyPhone = sdt == "0901234567";

                            User.HasCompleteProfile = !string.IsNullOrWhiteSpace(maKHReal)
                                                   && !string.IsNullOrWhiteSpace(cmnd)
                                                   && !isDummyCccd
                                                   && !string.IsNullOrWhiteSpace(sdt)
                                                   && !isDummyPhone
                                                   && !string.IsNullOrWhiteSpace(diaChi)
                                                   && hasDob;
                        }
                    }
                }

                DebugLog($"Refresh OK: {User.TenDangNhap} | MaKH={User.MaKH ?? "NULL"} | HasCompleteProfile={User.HasCompleteProfile}");
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
        /// <param name="maKH">Mã KH vừa tạo/cập nhật.</param>
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
            User.IsFirstLogin = false;
            User.HasCompleteProfile = true;

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
