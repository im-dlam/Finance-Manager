using BankAccountManagement.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Dịch vụ Xác thực và Quản lý Đăng nhập / Đăng ký (Authentication Service).
    /// <para>
    /// Các chức năng chính:
    /// - Xác thực thông tin đăng nhập (Username / Password) chống tấn công SQL Injection bằng Parameterized Query.
    /// - Kiểm tra trạng thái tài khoản người dùng ('Active' / 'Locked').
    /// - Liên kết hồ sơ nhân thân khách hàng (bảng ThongTinKhachHang) với tài khoản đăng nhập (bảng NguoiDung).
    /// - Tự động kích hoạt quy trình khởi tạo tài khoản ngân hàng mặc định nếu khách hàng đã hoàn thiện hồ sơ định danh (KYC).
    /// - Ghi nhận thời điểm đăng nhập gần nhất (LanDangNhap) phục vụ kiểm toán hệ thống.
    /// - Xử lý đăng ký thành viên mới (Role 'Member') kèm ghi nhận nhật ký hoạt động (NhatKyHoatDong) an toàn qua SqlTransaction.
    /// </para>
    /// </summary>
    public static class AuthService
    {
        #region 1. KẾT NỐI CƠ SỞ DỮ LIỆU

        /// <summary>
        /// Mở và trả về một đối tượng SqlConnection tới CSDL SQL Server dựa trên cấu hình hiện tại.
        /// </summary>
        private static SqlConnection OpenConnection()
        {
            DatabaseConfig config = DatabaseConfigManager.Load();
            var conn = DatabaseService.CreateConnection(config);
            conn.Open();
            return conn;
        }

        #endregion

        #region 2. ĐĂNG NHẬP HỆ THỐNG (LOGIN)

        /// <summary>
        /// Xác thực thông tin đăng nhập của người dùng.
        /// </summary>
        /// <param name="username">Tên đăng nhập của tài khoản.</param>
        /// <param name="password">Mật khẩu đăng nhập (dạng văn bản thuần).</param>
        /// <param name="selectedBank">Mã ngân hàng người dùng chọn tại màn hình đăng nhập (tùy chọn).</param>
        /// <returns>Đối tượng AuthResult chứa cờ Success, thông báo Message và thực thể User đã đăng nhập.</returns>
        public static AuthResult Login(string username, string password, string selectedBank = null)
        {
            // BƯỚC 1: Kiểm tra tính hợp lệ của dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(username))
                return AuthResult.Fail("Vui lòng nhập tên đăng nhập.");

            if (string.IsNullOrWhiteSpace(password))
                return AuthResult.Fail("Vui lòng nhập mật khẩu.");

            // BƯỚC 2: Thực thi câu lệnh SQL Parameterized Query để xác thực người dùng và hồ sơ khách hàng liên kết
            const string sql = @"
                SELECT  nd.MaNguoiDung,
                        nd.TenDangNhap,
                        nd.Quyen,
                        nd.TrangThai,
                        nd.MaKH      AS MaKHLink,       -- Khóa ngoại MaKH trong bảng NguoiDung
                        nd.HoTen,
                        nd.Email,
                        nd.MaNganHang,
                        nh.TenNganHang,
                        nd.LanDangNhap,
                        kh.MaKH      AS MaKHReal,       -- Bản ghi thật trong ThongTinKhachHang (NULL nếu chưa có)
                        kh.HoTen     AS HoTenKH,
                        kh.Email     AS EmailKH,
                        kh.CMND_CCCD,
                        kh.SoDienThoai,
                        kh.DiaChi,
                        kh.NgaySinh
                FROM    dbo.NguoiDung nd
                LEFT JOIN dbo.ThongTinKhachHang kh ON kh.MaKH = nd.MaKH
                LEFT JOIN dbo.NganHang nh ON nh.MaNganHang = nd.MaNganHang
                WHERE   (nd.TenDangNhap = @TenDangNhap OR (@TenDangNhap = 'admin' AND nd.TenDangNhap = 'admin1'))
                    AND nd.MatKhau     = @MatKhau";

            try
            {
                using (var conn = OpenConnection())
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@TenDangNhap", SqlDbType.VarChar, 50).Value = username;
                    cmd.Parameters.Add("@MatKhau", SqlDbType.VarChar, 255).Value = password;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                            return AuthResult.Fail("Tên đăng nhập hoặc mật khẩu không đúng.");

                        string trangThai = reader.GetString(reader.GetOrdinal("TrangThai"));
                        if (trangThai != "Active")
                            return AuthResult.Fail("Tài khoản của bạn hiện đang bị khóa.");

                        string maKHReal = reader.IsDBNull(reader.GetOrdinal("MaKHReal"))
                            ? null
                            : reader.GetString(reader.GetOrdinal("MaKHReal"));

                        string maKHLink = reader.IsDBNull(reader.GetOrdinal("MaKHLink"))
                            ? null
                            : reader.GetString(reader.GetOrdinal("MaKHLink"));

                        string maKH = maKHReal ?? maKHLink;

                        // Ưu tiên Họ tên và Email từ hồ sơ ThongTinKhachHang nếu có
                        string hoTen = maKHReal != null && !reader.IsDBNull(reader.GetOrdinal("HoTenKH"))
                            ? reader.GetString(reader.GetOrdinal("HoTenKH"))
                            : (reader.IsDBNull(reader.GetOrdinal("HoTen")) ? null : reader.GetString(reader.GetOrdinal("HoTen")));

                        string email = maKHReal != null && !reader.IsDBNull(reader.GetOrdinal("EmailKH"))
                            ? reader.GetString(reader.GetOrdinal("EmailKH"))
                            : (reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString(reader.GetOrdinal("Email")));

                        bool isFirstLogin = reader.IsDBNull(reader.GetOrdinal("LanDangNhap"));
                        DateTime? lanDangNhap = isFirstLogin ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("LanDangNhap"));

                        string cmnd = reader.IsDBNull(reader.GetOrdinal("CMND_CCCD")) ? null : reader.GetString(reader.GetOrdinal("CMND_CCCD"));
                        string sdt = reader.IsDBNull(reader.GetOrdinal("SoDienThoai")) ? null : reader.GetString(reader.GetOrdinal("SoDienThoai"));
                        string diaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? null : reader.GetString(reader.GetOrdinal("DiaChi"));
                        bool hasDob = !reader.IsDBNull(reader.GetOrdinal("NgaySinh"));

                        bool isDummyCccd = !string.IsNullOrWhiteSpace(cmnd) && (cmnd.StartsWith("001200") && cmnd.Length == 12);
                        bool isDummyPhone = sdt == "0901234567";

                        // Kiểm tra khách hàng đã hoàn thiện định danh nhân thân thực tế chưa
                        bool hasCompleteProfile = !string.IsNullOrWhiteSpace(maKHReal)
                                               && !string.IsNullOrWhiteSpace(cmnd)
                                               && !isDummyCccd
                                               && !string.IsNullOrWhiteSpace(sdt)
                                               && !isDummyPhone
                                               && !string.IsNullOrWhiteSpace(diaChi)
                                               && hasDob;

                        string dbMaNganHang = reader.IsDBNull(reader.GetOrdinal("MaNganHang"))
                            ? null
                            : reader.GetString(reader.GetOrdinal("MaNganHang"));

                        string dbTenNganHang = reader.IsDBNull(reader.GetOrdinal("TenNganHang"))
                            ? null
                            : reader.GetString(reader.GetOrdinal("TenNganHang"));

                        string effectiveBank = !string.IsNullOrWhiteSpace(dbMaNganHang) ? dbMaNganHang : (selectedBank ?? "NH01");

                        var user = new User
                        {
                            MaNguoiDung = reader.GetInt32(reader.GetOrdinal("MaNguoiDung")),
                            TenDangNhap = reader.GetString(reader.GetOrdinal("TenDangNhap")),
                            Quyen = reader.GetString(reader.GetOrdinal("Quyen")),
                            TrangThai = trangThai,
                            MaKH = maKH,
                            HoTen = hoTen,
                            Email = email,
                            MaNganHang = effectiveBank,
                            TenNganHang = dbTenNganHang,
                            LanDangNhap = lanDangNhap,
                            IsFirstLogin = isFirstLogin,
                            HasCompleteProfile = hasCompleteProfile
                        };

                        reader.Close();

                        // BƯỚC 3: Nếu là khách hàng Member đã có hồ sơ, đảm bảo luôn có 1 tài khoản thanh toán sẵn sàng
                        if (!user.IsAdmin)
                        {
                            EnsureMemberHasAccount(conn, user);
                        }

                        // BƯỚC 4: Cập nhật thời điểm đăng nhập gần nhất
                        UpdateLastLogin(user.MaNguoiDung);

                        return AuthResult.Ok(user, $"Xin chào {user.TenDangNhap}!");
                    }
                }
            }
            catch (SqlException ex)
            {
                return AuthResult.Fail("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
            }
            catch (Exception ex)
            {
                return AuthResult.Fail("Lỗi hệ thống: " + ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật thời điểm đăng nhập gần nhất vào bảng NguoiDung.
        /// </summary>
        private static void UpdateLastLogin(int maNguoiDung)
        {
            try
            {
                const string sql = @"
                    UPDATE dbo.NguoiDung
                    SET    LanDangNhap = GETDATE()
                    WHERE  MaNguoiDung = @Id";

                using (var conn = OpenConnection())
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", maNguoiDung);
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }

        #endregion

        #region 3. ĐĂNG KÝ THÀNH VIÊN MỚI (REGISTRATION)

        /// <summary>
        /// Đăng ký tài khoản người dùng mới với vai trò mặc định là 'Member'.
        /// Sử dụng SqlTransaction để đảm bảo toàn vẹn dữ liệu giữa tài khoản và nhật ký kiểm toán.
        /// </summary>
        /// <param name="fullName">Họ và tên của người dùng.</param>
        /// <param name="username">Tên đăng nhập mong muốn.</param>
        /// <param name="email">Địa chỉ Email liên hệ.</param>
        /// <param name="password">Mật khẩu khởi tạo.</param>
        /// <param name="maNganHang">Mã ngân hàng mặc định liên kết (tùy chọn).</param>
        public static AuthResult Register(
            string fullName,
            string username,
            string email,
            string password,
            string maNganHang = null)
        {
            // BƯỚC 1: Kiểm tra tính hợp lệ dữ liệu đăng ký
            if (string.IsNullOrWhiteSpace(fullName))
                return AuthResult.Fail("Vui lòng nhập họ và tên.");

            if (string.IsNullOrWhiteSpace(username) || username.Length < 4)
                return AuthResult.Fail("Tên đăng nhập phải có ít nhất 4 ký tự.");

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                return AuthResult.Fail("Mật khẩu phải có ít nhất 6 ký tự.");

            try
            {
                using (var conn = OpenConnection())
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // BƯỚC 2: Kiểm tra trùng lặp tên đăng nhập
                        if (ExistsByUsername(conn, tran, username))
                            return AuthResult.Fail("Tên đăng nhập này đã được sử dụng. Vui lòng chọn tên khác.");

                        // BƯỚC 3: Thêm bản ghi người dùng mới
                        InsertUser(conn, tran, username, password, fullName, email, maNganHang);

                        // BƯỚC 4: Ghi nhật ký kiểm toán hệ thống
                        InsertAuditLog(conn, tran, username, fullName);

                        tran.Commit();

                        return AuthResult.Ok($"Đăng ký thành công! Bạn có thể đăng nhập bằng tài khoản '{username}'.");
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
            catch (SqlException ex)
            {
                return AuthResult.Fail("Lỗi cơ sở dữ liệu: " + ex.Message);
            }
            catch (Exception ex)
            {
                return AuthResult.Fail("Lỗi hệ thống: " + ex.Message);
            }
        }

        #endregion

        #region 4. TỰ ĐỘNG KHỞI TẠO TÀI KHOẢN NGÂN HÀNG (AUTO ACCOUNT PROVISIONING)

        /// <summary>
        /// Đảm bảo mỗi khách hàng Member đã có hồ sơ KYC hợp lệ luôn có đúng 1 tài khoản ngân hàng hoạt động.
        /// Sử dụng cấp độ cô lập giao dịch Serializable và khóa UPDLOCK, HOLDLOCK để ngăn chặn race-condition sinh trùng số tài khoản.
        /// </summary>
        private static void EnsureMemberHasAccount(SqlConnection conn, User user)
        {
            if (conn == null) throw new ArgumentNullException(nameof(conn));
            if (user == null) throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(user.MaKH) || !user.HasCompleteProfile)
                return;

            using (SqlTransaction tran = conn.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    string maKH = user.MaKH.Trim();

                    // BƯỚC 1: Xác nhận hồ sơ khách hàng thực sự tồn tại và đang hoạt động
                    using (var cmd = new SqlCommand(@"
                        SELECT COUNT(1)
                        FROM dbo.ThongTinKhachHang WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaKH = @MaKH
                          AND TrangThai = N'Active';", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        if (Convert.ToInt32(cmd.ExecuteScalar()) == 0)
                        {
                            tran.Commit();
                            return;
                        }
                    }

                    // BƯỚC 2: Kiểm tra nếu khách hàng đã có tài khoản đang hoạt động thì không tạo thêm
                    using (var cmd = new SqlCommand(@"
                        SELECT TOP (1) 1
                        FROM dbo.TaiKhoan WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaKH = @MaKH
                          AND TrangThai IN ('Active', N'Hoạt động');", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        if (cmd.ExecuteScalar() != null)
                        {
                            tran.Commit();
                            return;
                        }
                    }

                    // BƯỚC 3: Xác định mã ngân hàng liên kết
                    string maNganHang = user.MaNganHang;
                    if (string.IsNullOrWhiteSpace(maNganHang))
                    {
                        using (var cmd = new SqlCommand(@"
                            SELECT TOP (1) MaNganHang
                            FROM dbo.NganHang
                            ORDER BY CASE WHEN MaNganHang = 'NH01' THEN 0 ELSE 1 END, MaNganHang;", conn, tran))
                        {
                            object result = cmd.ExecuteScalar();
                            if (result == null || result == DBNull.Value)
                                throw new InvalidOperationException("Chưa có ngân hàng nào được cấu hình trong hệ thống.");
                            maNganHang = result.ToString();
                        }
                    }

                    // BƯỚC 4: Lấy mã loại tiền tệ mặc định (VND)
                    string maLoaiTien;
                    using (var cmd = new SqlCommand(@"
                        SELECT TOP (1) MaLoaiTien
                        FROM dbo.LoaiTien
                        WHERE MaLoaiTien = 'VND';", conn, tran))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result == null || result == DBNull.Value)
                            throw new InvalidOperationException("Chưa cấu hình loại tiền VND trong hệ thống.");
                        maLoaiTien = result.ToString();
                    }

                    // BƯỚC 5: Sinh số tài khoản mới duy nhất tự tăng
                    long maxSoTaiKhoan;
                    using (var cmd = new SqlCommand(@"
                        SELECT ISNULL(
                            MAX(TRY_CONVERT(BIGINT, SoTaiKhoan)),
                            CAST(
                                CASE 
                                    WHEN TRY_CONVERT(INT, REPLACE(REPLACE(@MaNganHang, 'NH', ''), 'nh', '')) IS NOT NULL
                                    THEN CAST(1000000000 + TRY_CONVERT(INT, REPLACE(REPLACE(@MaNganHang, 'NH', ''), 'nh', '')) * 10000000 AS BIGINT)
                                    ELSE 1000001000
                                END AS BIGINT
                            )
                        )
                        FROM dbo.TaiKhoan WITH (UPDLOCK, HOLDLOCK)
                        WHERE MaNganHang = @MaNganHang
                          AND TRY_CONVERT(BIGINT, SoTaiKhoan) IS NOT NULL;", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@MaNganHang", maNganHang);
                        maxSoTaiKhoan = Convert.ToInt64(cmd.ExecuteScalar());
                    }

                    if (maxSoTaiKhoan == long.MaxValue)
                        throw new InvalidOperationException("Không thể sinh số tài khoản mới.");

                    string soTaiKhoan = (maxSoTaiKhoan + 1).ToString();

                    // BƯỚC 6: Tạo tài khoản ngân hàng khởi đầu và giao dịch mở tài khoản
                    using (var cmd = new SqlCommand(@"
                        INSERT INTO dbo.TaiKhoan
                        (
                            SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien,
                            SoDuBanDau, SoDuHienTai, NgayMo, TrangThai
                        )
                        VALUES
                        (
                            @SoTaiKhoan, @MaKH, @MaNganHang, @MaLoaiTien,
                            50000000, 50000000, GETDATE(), 'Active'
                        );

                        DECLARE @MaGD VARCHAR(30) = 'GD' + CONVERT(VARCHAR(8), GETDATE(), 112) + RIGHT('000000' + CAST(ABS(CHECKSUM(NEWID())) % 1000000 AS VARCHAR(6)), 6);
                        INSERT INTO dbo.GiaoDich 
                            (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, SoTienGoc, 
                             MaLoaiTien, LoaiTien, NgayGiaoDich, NoiDung, TrangThai)
                        VALUES 
                            (@MaGD, @SoTaiKhoan, 1, 50000000, 50000000, 
                             @MaLoaiTien, @MaLoaiTien, GETDATE(), N'Ngân hàng khởi tạo tài khoản', 'Success');", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        cmd.Parameters.AddWithValue("@MaNganHang", maNganHang);
                        cmd.Parameters.AddWithValue("@MaLoaiTien", maLoaiTien);
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch
                {
                    try { tran.Rollback(); } catch { }
                    throw;
                }
            }
        }

        #endregion

        #region 5. TRỢ GIÚP NỘI BỘ (PRIVATE HELPERS)

        /// <summary>
        /// Kiểm tra tên đăng nhập đã tồn tại trong CSDL hay chưa.
        /// </summary>
        private static bool ExistsByUsername(SqlConnection conn, SqlTransaction tran, string username)
        {
            const string sql = "SELECT 1 FROM dbo.NguoiDung WHERE TenDangNhap = @U";
            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.AddWithValue("@U", username);
                return cmd.ExecuteScalar() != null;
            }
        }

        /// <summary>
        /// Chèn bản ghi người dùng mới vào bảng NguoiDung.
        /// </summary>
        private static void InsertUser(
            SqlConnection conn, SqlTransaction tran,
            string username, string password, string hoTen, string email, string maNganHang = null)
        {
            const string sql = @"
                INSERT INTO dbo.NguoiDung
                    (TenDangNhap, MatKhau, Quyen, MaKH, HoTen, Email, MaNganHang, TrangThai, LanDangNhap)
                OUTPUT INSERTED.MaNguoiDung
                VALUES
                    (@TenDangNhap, @MatKhau, 'Member', NULL, @HoTen, @Email, @MaNganHang, N'Active', NULL)";

            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.AddWithValue("@TenDangNhap", username);
                cmd.Parameters.AddWithValue("@MatKhau", password);
                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaNganHang", (object)maNganHang ?? DBNull.Value);

                object res = cmd.ExecuteScalar();
                if (res == null)
                    throw new Exception("Không thể tạo tài khoản người dùng.");
            }
        }

        /// <summary>
        /// Ghi nhận hoạt động đăng ký tài khoản vào bảng NhatKyHoatDong.
        /// </summary>
        private static void InsertAuditLog(
            SqlConnection conn, SqlTransaction tran,
            string username, string hoTen)
        {
            const string sql = @"
                INSERT INTO dbo.NhatKyHoatDong
                    (MaNguoiDung, HanhDong, DoiTuong, DoiTuongID, ChiTiet)
                SELECT MaNguoiDung, N'REGISTER', N'NguoiDung', @Username, @ChiTiet
                FROM   dbo.NguoiDung
                WHERE  TenDangNhap = @Username";

            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@ChiTiet", $"Đăng ký tài khoản Member mới: {hoTen}");
                cmd.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
