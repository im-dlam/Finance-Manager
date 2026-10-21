using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Dịch vụ quản lý thông tin các Ngân hàng liên kết trong hệ thống (Bank Management Service).
    /// Chịu trách nhiệm:
    /// - Cung cấp danh sách ngân hàng công khai cho màn hình Đăng ký/Đăng nhập.
    /// - Quản lý danh mục ngân hàng theo phân quyền (Admin thấy tất cả, Member thấy ngân hàng mình mở tài khoản).
    /// - Thực hiện các nghiệp vụ CRUD (Thêm, Sửa, Xóa, Tìm kiếm) với cơ chế bẫy lỗi ràng buộc toàn vẹn dữ liệu (Khóa chính, Khóa ngoại).
    /// </summary>
    public class NganHangService
    {
        /// <summary>
        /// Chuỗi kết nối cơ sở dữ liệu SQL Server.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Lấy toàn bộ danh sách các ngân hàng công khai trực tiếp từ CSDL.
        /// Thường được gọi tại màn hình Đăng ký tài khoản (SignupForm) hoặc Đăng nhập để người dùng chọn chi nhánh ban đầu.
        /// </summary>
        /// <returns>Danh sách các đối tượng NganHang được sắp xếp theo thứ tự số mã ngân hàng.</returns>
        public static List<NganHang> GetPublicBanks()
        {
            var result = new List<NganHang>();
            try
            {
                // BƯỚC 1: Tải cấu hình và khởi tạo kết nối CSDL
                DatabaseConfig config = DatabaseConfigManager.Load();
                using (var conn = DatabaseService.CreateConnection(config))
                {
                    conn.Open();

                    // BƯỚC 2: Câu lệnh SQL lấy thông tin ngân hàng và sắp xếp tăng dần theo mã số
                    const string sql = @"
                        SELECT MaNganHang, TenNganHang, ChiNhanh, SwiftCode
                        FROM   dbo.NganHang
                        ORDER  BY CAST(SUBSTRING(MaNganHang, 3, 10) AS INT), MaNganHang;";

                    using (var cmd = new SqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        // BƯỚC 3: Đọc từng dòng kết quả và khởi tạo thực thể NganHang
                        while (reader.Read())
                        {
                            result.Add(new NganHang
                            {
                                MaNganHang = reader["MaNganHang"]?.ToString(),
                                TenNganHang = reader["TenNganHang"]?.ToString(),
                                ChiNhanh = reader["ChiNhanh"]?.ToString(),
                                SwiftCode = reader["SwiftCode"]?.ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("[NganHangService] GetPublicBanks error: " + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Hàm khởi tạo của NganHangService: Tự động nạp cấu hình kết nối từ database.ini.
        /// </summary>
        public NganHangService()
        {
            DatabaseConfig config = DatabaseConfigManager.Load();
            _connectionString = DatabaseService.BuildConnectionString(config);
        }

        // ============================================================
        //  1. LẤY TẤT CẢ NGÂN HÀNG (CÓ PHÂN QUYỀN ADMIN VS MEMBER)
        // ============================================================

        /// <summary>
        /// Lấy danh sách ngân hàng theo phân quyền của người dùng hiện tại:
        /// - Quản trị viên (Admin): Xem toàn bộ ngân hàng có trong hệ thống.
        /// - Khách hàng (Member): Chỉ xem các ngân hàng mà bản thân đang mở tài khoản thanh toán.
        /// </summary>
        /// <returns>Danh sách đối tượng NganHang phù hợp với phân quyền.</returns>
        /// <exception cref="Exception">Ném ra ngoại lệ nếu gặp lỗi truy vấn dữ liệu từ CSDL.</exception>
        public List<NganHang> GetAllNganHang()
        {
            var result = new List<NganHang>();
            string sql;

            // BƯỚC 1: Kiểm tra phân quyền từ CurrentSession để xây dựng câu truy vấn SQL phù hợp
            if (CurrentSession.IsAdmin || string.IsNullOrWhiteSpace(CurrentSession.MaKH))
            {
                // Với Quản trị viên: Lấy toàn bộ danh mục ngân hàng
                sql = @"
                    SELECT MaNganHang, TenNganHang, ChiNhanh, SwiftCode
                    FROM   dbo.NganHang
                    ORDER  BY TenNganHang";
            }
            else
            {
                // Với Khách hàng: Chỉ lấy các ngân hàng có tài khoản thuộc sở hữu của khách hàng đó (INNER JOIN với TaiKhoan)
                sql = @"
                    SELECT DISTINCT NH.MaNganHang, NH.TenNganHang, NH.ChiNhanh, NH.SwiftCode
                    FROM   dbo.NganHang NH
                    INNER JOIN dbo.TaiKhoan TK ON NH.MaNganHang = TK.MaNganHang
                    WHERE  TK.MaKH = @MaKH
                    ORDER  BY NH.TenNganHang";
            }

            try
            {
                // BƯỚC 2: Mở kết nối và thực thi truy vấn
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        // Nếu là Member, truyền tham số @MaKH để chống SQL Injection
                        if (!CurrentSession.IsAdmin && !string.IsNullOrWhiteSpace(CurrentSession.MaKH))
                        {
                            cmd.Parameters.AddWithValue("@MaKH", CurrentSession.MaKH);
                        }

                        // BƯỚC 3: Dùng SqlDataReader để đọc kết quả tuần tự từng dòng
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                result.Add(MapReader(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách ngân hàng: " + ex.Message, ex);
            }

            return result;
        }

        // ============================================================
        //  2. TÌM KIẾM NGÂN HÀNG THEO TỪ KHÓA
        // ============================================================

        /// <summary>
        /// Tìm kiếm ngân hàng theo từ khóa kết hợp phân quyền bảo mật:
        /// Tìm kiếm tương đối trên cả Mã, Tên ngân hàng, Chi nhánh và mã SwiftCode.
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm do người dùng nhập vào.</param>
        /// <returns>Danh sách ngân hàng thỏa mãn điều kiện tìm kiếm.</returns>
        /// <exception cref="Exception">Ném ra ngoại lệ nếu truy vấn thất bại.</exception>
        public List<NganHang> SearchNganHang(string keyword)
        {
            var result = new List<NganHang>();
            string sql;

            // BƯỚC 1: Xây dựng câu truy vấn dựa trên vai trò của người dùng
            if (CurrentSession.IsAdmin || string.IsNullOrWhiteSpace(CurrentSession.MaKH))
            {
                sql = @"
                    SELECT MaNganHang, TenNganHang, ChiNhanh, SwiftCode
                    FROM   dbo.NganHang
                    WHERE  (MaNganHang  LIKE @Keyword
                       OR  TenNganHang LIKE @Keyword
                       OR  ChiNhanh    LIKE @Keyword
                       OR  SwiftCode   LIKE @Keyword)
                    ORDER  BY TenNganHang";
            }
            else
            {
                sql = @"
                    SELECT DISTINCT NH.MaNganHang, NH.TenNganHang, NH.ChiNhanh, NH.SwiftCode
                    FROM   dbo.NganHang NH
                    INNER JOIN dbo.TaiKhoan TK ON NH.MaNganHang = TK.MaNganHang
                    WHERE  TK.MaKH = @MaKH
                      AND  (NH.MaNganHang  LIKE @Keyword
                       OR  NH.TenNganHang LIKE @Keyword
                       OR  NH.ChiNhanh    LIKE @Keyword
                       OR  NH.SwiftCode   LIKE @Keyword)
                    ORDER  BY NH.TenNganHang";
            }

            try
            {
                // BƯỚC 2: Khởi tạo kết nối và gán tham số Parameterized an toàn
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        // Dùng cú pháp %keyword% cho toán tử LIKE trong SQL
                        cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");

                        if (!CurrentSession.IsAdmin && !string.IsNullOrWhiteSpace(CurrentSession.MaKH))
                        {
                            cmd.Parameters.AddWithValue("@MaKH", CurrentSession.MaKH);
                        }

                        // BƯỚC 3: Đọc dữ liệu với SqlDataReader
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                result.Add(MapReader(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm kiếm ngân hàng: " + ex.Message, ex);
            }

            return result;
        }

        // ============================================================
        //  3. THÊM MỚI NGÂN HÀNG (DÀNH CHO ADMIN)
        // ============================================================

        /// <summary>
        /// Thêm mới một ngân hàng đối tác vào bảng dbo.NganHang.
        /// </summary>
        /// <param name="nh">Đối tượng NganHang chứa thông tin cần lưu.</param>
        /// <returns>True nếu thêm thành công (số dòng bị tác động > 0); ngược lại là False.</returns>
        /// <exception cref="Exception">Ném ngoại lệ kèm thông báo nếu mã ngân hàng đã trùng lặp (Mã lỗi 2627).</exception>
        public bool AddNganHang(NganHang nh)
        {
            const string sql = @"
                INSERT INTO NganHang (MaNganHang, TenNganHang, ChiNhanh, SwiftCode)
                VALUES (@MaNganHang, @TenNganHang, @ChiNhanh, @SwiftCode)";
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        // Gán tham số chống SQL Injection; sử dụng DBNull.Value nếu giá trị null
                        cmd.Parameters.AddWithValue("@MaNganHang", nh.MaNganHang);
                        cmd.Parameters.AddWithValue("@TenNganHang", nh.TenNganHang);
                        cmd.Parameters.AddWithValue("@ChiNhanh", (object)nh.ChiNhanh ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SwiftCode", (object)nh.SwiftCode ?? DBNull.Value);

                        // ExecuteNonQuery thực thi lệnh INSERT/UPDATE/DELETE và trả về số dòng bị ảnh hưởng
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Bắt lỗi vi phạm ràng buộc khóa chính duy nhất (PRIMARY KEY / UNIQUE VIOLATION: Error 2627)
                if (ex is SqlException sqlEx && sqlEx.Number == 2627)
                {
                    throw new Exception($"Mã ngân hàng '{nh.MaNganHang}' đã tồn tại trong hệ thống. Vui lòng sử dụng mã khác.", ex);
                }

                throw new Exception("Lỗi thêm ngân hàng: " + ex.Message, ex);
            }
        }

        // ============================================================
        //  4. CẬP NHẬT THÔNG TIN NGÂN HÀNG
        // ============================================================

        /// <summary>
        /// Cập nhật tên, chi nhánh hoặc mã SwiftCode của ngân hàng dựa theo Mã ngân hàng (Khóa chính).
        /// </summary>
        /// <param name="nh">Đối tượng NganHang chứa thông tin đã sửa đổi.</param>
        /// <returns>True nếu cập nhật thành công; ngược lại là False.</returns>
        /// <exception cref="Exception">Ném ngoại lệ thông báo nếu phát sinh xung đột khóa ngoại (Mã lỗi 547).</exception>
        public bool UpdateNganHang(NganHang nh)
        {
            const string sql = @"
                UPDATE NganHang
                SET    TenNganHang = @TenNganHang,
                       ChiNhanh    = @ChiNhanh,
                       SwiftCode   = @SwiftCode
                WHERE  MaNganHang  = @MaNganHang";
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNganHang", nh.MaNganHang);
                        cmd.Parameters.AddWithValue("@TenNganHang", nh.TenNganHang);
                        cmd.Parameters.AddWithValue("@ChiNhanh", (object)nh.ChiNhanh ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SwiftCode", (object)nh.SwiftCode ?? DBNull.Value);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Bắt lỗi vi phạm ràng buộc khóa ngoại (FOREIGN KEY VIOLATION: Error 547)
                if (ex is SqlException sqlEx && sqlEx.Number == 547)
                {
                    throw new Exception($"Không thể cập nhật ngân hàng '{nh.MaNganHang}' vì có dữ liệu tài khoản khác đang liên kết.", ex);
                }

                throw new Exception("Lỗi cập nhật ngân hàng: " + ex.Message, ex);
            }
        }

        // ============================================================
        //  5. XÓA NGÂN HÀNG
        // ============================================================

        /// <summary>
        /// Xóa một ngân hàng khỏi hệ thống theo Mã ngân hàng.
        /// Chú ý: Chỉ có thể xóa nếu ngân hàng đó chưa từng được mở tài khoản nào liên kết.
        /// </summary>
        /// <param name="maNganHang">Mã ngân hàng cần xóa.</param>
        /// <returns>True nếu xóa thành công; False nếu không tìm thấy.</returns>
        /// <exception cref="Exception">Ném ngoại lệ nếu ngân hàng đang có tài khoản liên kết (Ràng buộc toàn vẹn khóa ngoại 547).</exception>
        public bool DeleteNganHang(string maNganHang)
        {
            const string sql = "DELETE FROM NganHang WHERE MaNganHang = @MaNganHang";
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaNganHang", maNganHang);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Bắt lỗi vi phạm ràng buộc khóa ngoại nếu ngân hàng đang được bảng TaiKhoan tham chiếu
                if (ex is SqlException sqlEx && sqlEx.Number == 547)
                {
                    throw new Exception($"Không thể xóa ngân hàng '{maNganHang}' vì đang có tài khoản khách hàng liên kết với ngân hàng này.", ex);
                }

                throw new Exception("Lỗi xóa ngân hàng: " + ex.Message, ex);
            }
        }

        // ============================================================
        //  PHƯƠNG THỨC TIỆN ÍCH ÁNH XẠ DỮ LIỆU (OBJECT-RELATIONAL MAPPING)
        // ============================================================

        /// <summary>
        /// Ánh xạ từ dòng dữ liệu hiện tại của SqlDataReader thành đối tượng thực thể <see cref="NganHang"/>.
        /// </summary>
        /// <param name="reader">Đối tượng SqlDataReader đang ở vị trí dòng dữ liệu hợp lệ.</param>
        /// <returns>Đối tượng NganHang với đầy đủ các thuộc tính.</returns>
        private static NganHang MapReader(SqlDataReader reader)
        {
            return new NganHang
            {
                MaNganHang = reader["MaNganHang"].ToString(),
                TenNganHang = reader["TenNganHang"].ToString(),
                ChiNhanh = reader["ChiNhanh"] as string,
                SwiftCode = reader["SwiftCode"] as string
            };
        }
    }
}
