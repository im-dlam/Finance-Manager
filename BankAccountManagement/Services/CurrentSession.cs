using BankAccountManagement.Models;
using System;
using System.Data.SqlClient;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Lớp quản lý phiên làm việc hiện tại của người dùng (Current Session).
    /// Đây là một static class tồn tại xuyên suốt vòng đời của ứng dụng Windows Forms:
    /// - Lưu trữ thông tin tài khoản người dùng đã đăng nhập thành công.
    /// - Quản lý phân quyền truy cập (Admin vs Khách hàng/Member).
    /// - Theo dõi hạng hội viên (Standard, Silver, Gold, Diamond) theo thời gian thực.
    /// - Hỗ trợ làm mới dữ liệu hồ sơ cá nhân và kiểm tra an toàn phân quyền.
    /// </summary>
    public static class CurrentSession
    {
        // ============================================================
        //  TRẠNG THÁI NGƯỜI DÙNG ĐĂNG NHẬP
        // ============================================================

        /// <summary>
        /// Thực thể thông tin người dùng hiện đang đăng nhập vào phần mềm (Private Set để đảm bảo tính đóng gói).
        /// </summary>
        public static User User { get; private set; }

        /// <summary>
        /// Kiểm tra xem hiện tại đã có người dùng đăng nhập vào hệ thống hay chưa (True nếu User != null).
        /// </summary>
        public static bool IsLoggedIn => User != null;

        /// <summary>
        /// Kiểm tra xem người dùng hiện tại có phải là Quản trị viên hệ thống (Admin) hay không.
        /// </summary>
        public static bool IsAdmin => User?.IsAdmin == true;

        /// <summary>
        /// Kiểm tra xem người dùng hiện tại có phải là Khách hàng thông thường (Member) hay không.
        /// </summary>
        public static bool IsMember => User != null && !IsAdmin;

        /// <summary>Mã định danh người dùng hiện tại (bảng NguoiDung).</summary>
        public static int MaNguoiDung => User?.MaNguoiDung ?? 0;

        /// <summary>Tên tài khoản đăng nhập hiện tại.</summary>
        public static string TenDangNhap => User?.TenDangNhap;

        /// <summary>Mã khách hàng liên kết của người dùng (bảng ThongTinKhachHang).</summary>
        public static string MaKH => User?.MaKH;

        /// <summary>Họ và tên của người dùng.</summary>
        public static string HoTen => User?.HoTen;

        /// <summary>Địa chỉ Email của người dùng.</summary>
        public static string Email => User?.Email;

        /// <summary>Mã ngân hàng liên kết mặc định của người dùng.</summary>
        public static string MaNganHang => User?.MaNganHang;

        /// <summary>Tên ngân hàng liên kết tương ứng.</summary>
        public static string TenNganHang => User?.TenNganHang;

        // ============================================================
        //  QUẢN LÝ HẠNG THÀNH VIÊN VÀ GIAO DIỆN THEO HẠNG (TIER THEME)
        // ============================================================

        /// <summary>
        /// Cờ đánh dấu người dùng đang tự chọn hạng trên ComboBox TopBar để kiểm thử giao diện.
        /// Khi True, hệ thống sẽ KHÔNG tự động ghi đè hạng dựa trên số dư thực tế trong CSDL.
        /// </summary>
        public static bool IsManualTierOverride { get; set; } = false;

        /// <summary>Biến lưu trữ phân hạng thành viên hiện tại.</summary>
        private static string _selectedUserTier = "Hạng Tiêu Chuẩn";

        /// <summary>
        /// Sự kiện kích hoạt khi hạng thành viên thay đổi, giúp các Form con đăng ký lắng nghe và đổi màu giao diện tức thì.
        /// </summary>
        public static event Action<string> OnTierChanged;

        /// <summary>
        /// Hạng thành viên hiện tại của phiên làm việc (Mặc định cho người mới bắt đầu: Hạng Tiêu Chuẩn - Dưới 200 triệu).
        /// Khi giá trị này thay đổi, sự kiện OnTierChanged sẽ được phát tín hiệu tự động.
        /// </summary>
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
        /// Truy vấn tổng số dư thực tế của tất cả các tài khoản thanh toán thuộc sở hữu của người dùng hiện tại từ CSDL.
        /// </summary>
        /// <returns>Tổng số dư tiền gửi khả dụng (VNĐ).</returns>
        public static decimal GetUserTotalBalance()
        {
            // BƯỚC 1: Nếu chưa đăng nhập, trả về 0đ
            if (User == null) return 0m;

            try
            {
                string maKH = User.MaKH ?? "";
                string uName = User.TenDangNhap ?? "";

                // BƯỚC 2: Tải cấu hình và khởi tạo kết nối CSDL SQL Server
                DatabaseConfig config = DatabaseConfigManager.Load();
                using (var conn = DatabaseService.CreateConnection(config))
                {
                    conn.Open();

                    // BƯỚC 3: Dùng câu lệnh SQL Parameterized Query để tổng hợp số dư.
                    // Sử dụng Parameter (@MaKH, @User) giúp triệt tiêu hoàn toàn nguy cơ tấn công SQL Injection.
                    const string sql = @"
                        SELECT ISNULL(SUM(TK.SoDuHienTai), 0)
                        FROM dbo.TaiKhoan TK
                        WHERE (TK.MaKH = @MaKH OR TK.MaKH IN (SELECT ND.MaKH FROM dbo.NguoiDung ND WHERE ND.TenDangNhap = @User AND ND.MaKH IS NOT NULL))
                          AND (TK.TrangThai = 'Active' OR TK.TrangThai = N'Đang hoạt động' OR TK.TrangThai IS NULL)";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        cmd.Parameters.AddWithValue("@User", uName);

                        // BƯỚC 4: ExecuteScalar thực thi truy vấn và trả về ô dữ liệu đầu tiên (kết quả SUM)
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
        /// Làm mới và cập nhật phân hạng thành viên trong CurrentSession dựa trên số dư thực tế mới nhất từ CSDL.
        /// </summary>
        public static void RefreshTierByBalance()
        {
            if (!IsMember) return;
            decimal totalBalance = GetUserTotalBalance();
            UserTierTheme.UpdateSessionTierByBalance(totalBalance);
        }

        // ============================================================
        //  KIỂM TRA HỒ SƠ KHÁCH HÀNG (CUSTOMER PROFILE STATUS)
        // ============================================================

        /// <summary>
        /// Kiểm tra xem người dùng đã có hồ sơ khách hàng đầy đủ và hợp lệ hay chưa (Admin luôn là True).
        /// </summary>
        public static bool HasCustomerInfo =>
            User != null && (User.IsAdmin || (User.HasCompleteProfile && !string.IsNullOrWhiteSpace(User.MaKH)));

        /// <summary>
        /// Kiểm tra xem người dùng có cần phải mở hộp thoại nhập thông tin hồ sơ khách hàng hay không.
        /// Áp dụng cho tài khoản Member mới đăng nhập lần đầu hoặc thông tin còn thiếu sót/dữ liệu giả lập (dummy).
        /// </summary>
        public static bool NeedsCustomerInfo =>
            User != null && !User.IsAdmin && (User.IsFirstLogin || !User.HasCompleteProfile || string.IsNullOrWhiteSpace(User.MaKH));

        // ============================================================
        //  CÁC THAO TÁC QUẢN LÝ PHIÊN (SESSION LIFECYCLE)
        // ============================================================

        /// <summary>
        /// Thiết lập phiên làm việc với đối tượng User sau khi đăng nhập hoặc đăng ký thành công.
        /// </summary>
        /// <param name="user">Thực thể User người dùng hợp lệ.</param>
        /// <exception cref="ArgumentNullException">Ném ra ngoại lệ nếu tham số user bị null.</exception>
        public static void SetUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            // BƯỚC 1: Gán thông tin người dùng vào Session
            User = user;
            IsManualTierOverride = false; // Thiết lập mặc định: tự động tính hạng theo số dư

            // BƯỚC 2: Tính toán số dư và xác định hạng thành viên tương ứng
            decimal initBal = GetUserTotalBalance();
            _selectedUserTier = UserTierTheme.DetermineTierByBalance(initBal);

            // BƯỚC 3: Ghi log debug kiểm tra trạng thái đăng nhập
            System.Diagnostics.Debug.WriteLine(
                $"[Session] SetUser: {user.TenDangNhap} | " +
                $"Quyen={user.Quyen} | MaKH={user.MaKH ?? "NULL"} | Tier={_selectedUserTier} | IsFirstLogin={user.IsFirstLogin} | HasCompleteProfile={user.HasCompleteProfile}");
        }

        /// <summary>
        /// Xóa sạch phiên làm việc khi người dùng Đăng xuất (Logout), khôi phục trạng thái mặc định ban đầu.
        /// </summary>
        public static void Clear()
        {
            System.Diagnostics.Debug.WriteLine($"[Session] Clear {User?.TenDangNhap}");
            User = null;
            IsManualTierOverride = false;
            _selectedUserTier = "Hạng Tiêu Chuẩn";
        }

        /// <summary>
        /// Kiểm tra điều kiện bảo mật quyền Quản trị viên (Admin Guard).
        /// Nếu người dùng hiện tại không phải Admin, phương thức lập tức ném ra ngoại lệ từ chối truy cập.
        /// </summary>
        /// <exception cref="UnauthorizedAccessException">Chỉ có tài khoản Admin mới được thực hiện thao tác này.</exception>
        public static void RequireAdmin()
        {
            if (!IsAdmin)
            {
                throw new UnauthorizedAccessException("Chỉ Quản trị viên (Admin) mới có quyền thực hiện thao tác này.");
            }
        }

        /// <summary>
        /// Tải lại toàn bộ thông tin mới nhất của người dùng hiện tại từ cơ sở dữ liệu.
        /// Cập nhật đồng bộ các trường của đối tượng <see cref="User"/> mà không làm mất tham chiếu.
        /// </summary>
        /// <returns>True nếu làm mới thành công; False nếu người dùng không tồn tại hoặc có lỗi kết nối.</returns>
        public static bool Refresh()
        {
            // BƯỚC 1: Nếu chưa đăng nhập, không có dữ liệu để làm mới
            if (User == null)
            {
                DebugLog("Refresh: Bỏ qua — Chưa có phiên đăng nhập");
                return false;
            }

            try
            {
                // BƯỚC 2: Câu lệnh SQL LEFT JOIN giữa NguoiDung và ThongTinKhachHang để lấy toàn diện dữ liệu mới nhất
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
                        // Parameter hóa để chống SQL Injection
                        cmd.Parameters.AddWithValue("@Id", User.MaNguoiDung);

                        // BƯỚC 3: Sử dụng SqlDataReader để đọc dữ liệu trả về từ máy chủ
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                DebugLog($"Refresh: Người dùng #{User.MaNguoiDung} không còn tồn tại trong DB — Hủy phiên");
                                User = null;
                                return false;
                            }

                            // BƯỚC 4: Cập nhật từng thuộc tính của đối tượng User hiện tại
                            User.TenDangNhap = reader.GetString(reader.GetOrdinal("TenDangNhap"));
                            User.Quyen = reader.GetString(reader.GetOrdinal("Quyen"));
                            User.TrangThai = reader.GetString(reader.GetOrdinal("TrangThai"));

                            User.MaKH = reader.IsDBNull(reader.GetOrdinal("MaKH"))
                                        ? null : reader.GetString(reader.GetOrdinal("MaKH"));

                            User.HoTen = reader.IsDBNull(reader.GetOrdinal("HoTen"))
                                        ? null : reader.GetString(reader.GetOrdinal("HoTen"));

                            User.Email = reader.IsDBNull(reader.GetOrdinal("Email"))
                                        ? null : reader.GetString(reader.GetOrdinal("Email"));

                            // Xử lý kiểm tra ngày đăng nhập đầu tiên
                            bool isFirstLogin = reader.IsDBNull(reader.GetOrdinal("LanDangNhap"));
                            User.LanDangNhap = isFirstLogin ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("LanDangNhap"));
                            User.IsFirstLogin = isFirstLogin;

                            // Đọc thông tin chi tiết khách hàng từ bảng ThongTinKhachHang
                            string maKHReal = reader.IsDBNull(reader.GetOrdinal("MaKHReal")) ? null : reader.GetString(reader.GetOrdinal("MaKHReal"));
                            string cmnd = reader.IsDBNull(reader.GetOrdinal("CMND_CCCD")) ? null : reader.GetString(reader.GetOrdinal("CMND_CCCD"));
                            string sdt = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? null : reader.GetString(reader.GetOrdinal("SoDienThoai"));
                            string diaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? null : reader.GetString(reader.GetOrdinal("DiaChi"));
                            bool hasDob = !reader.IsDBNull(reader.GetOrdinal("NgaySinh"));

                            // Nhận diện dữ liệu giả lập mẫu hệ thống tự sinh
                            bool isDummyCccd = !string.IsNullOrWhiteSpace(cmnd) && (cmnd.StartsWith("001200") && cmnd.Length == 12);
                            bool isDummyPhone = sdt == "0901234567";

                            // BƯỚC 5: Đánh giá xem hồ sơ đã thực sự hoàn chỉnh hay chưa
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

                DebugLog($"Refresh thành công: {User.TenDangNhap} | MaKH={User.MaKH ?? "NULL"} | HasCompleteProfile={User.HasCompleteProfile}");
                return true;
            }
            catch (Exception ex)
            {
                DebugLog($"Refresh thất bại với lỗi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Đánh dấu phiên làm việc của người dùng đã cập nhật đầy đủ hồ sơ khách hàng.
        /// Thường được gọi ngay sau khi <c>CustomerService.SaveCustomerInfo()</c> commit thành công trong Transaction.
        /// </summary>
        /// <param name="maKH">Mã khách hàng mới được tạo hoặc cập nhật.</param>
        /// <param name="hoTen">Họ và tên mới (tuỳ chọn, nếu null sẽ giữ nguyên tên cũ).</param>
        /// <param name="email">Địa chỉ Email mới (tuỳ chọn, nếu null sẽ giữ nguyên email cũ).</param>
        public static void MarkCustomerInfoCompleted(string maKH, string hoTen = null, string email = null)
        {
            if (User == null)
            {
                DebugLog("MarkCustomerInfoCompleted: Bỏ qua — Chưa có người dùng đăng nhập");
                return;
            }

            if (string.IsNullOrWhiteSpace(maKH))
            {
                DebugLog("MarkCustomerInfoCompleted: Bỏ qua do maKH rỗng");
                return;
            }

            // BƯỚC 1: Cập nhật cờ trạng thái hồ sơ trong bộ nhớ phiên
            User.MaKH = maKH;
            User.IsFirstLogin = false;
            User.HasCompleteProfile = true;

            // BƯỚC 2: Cập nhật họ tên và email nếu có thông tin mới
            if (!string.IsNullOrWhiteSpace(hoTen))
                User.HoTen = hoTen;

            if (!string.IsNullOrWhiteSpace(email))
                User.Email = email;

            DebugLog($"MarkCustomerInfoCompleted: {User.TenDangNhap} → MaKH={maKH}");
        }

        // ============================================================
        //  CÁC PHƯƠNG THỨC TIỆN ÍCH NỘI BỘ (INTERNAL HELPERS)
        // ============================================================

        /// <summary>
        /// Ghi thông tin nhật ký ra cửa sổ Output Debug của Visual Studio để theo dõi hoạt động của hệ thống.
        /// </summary>
        /// <param name="message">Nội dung thông điệp cần ghi log.</param>
        private static void DebugLog(string message)
        {
            System.Diagnostics.Debug.WriteLine($"[Session] {message}");
        }
    }
}
