using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Service trung tâm xử lý toàn bộ nghiệp vụ Mua sắm (Shopping):
    /// - Quản lý sản phẩm (CRUD, tìm kiếm, lọc)
    /// - Quản lý giỏ hàng và phiên mua sắm
    /// - Xử lý thanh toán đơn hàng, trích xuất số dư tài khoản và lưu lịch sử giao dịch
    /// </summary>
    public class ShopService
    {
        private readonly DatabaseConfig _config;

        public ShopService()
        {
            _config = DatabaseConfigManager.Load();
        }

        public ShopService(DatabaseConfig config)
        {
            _config = config ?? DatabaseConfigManager.Load();
        }

        private string GetConnectionString()
        {
            return DatabaseService.BuildConnectionString(_config);
        }

        // =====================================================================
        // 1. CÁC PHƯƠNG THỨC XỬ LÝ SẢN PHẨM (PRODUCTS)
        // =====================================================================

        /// <summary>
        /// Lấy toàn bộ danh sách sản phẩm từ bảng dbo.SanPham trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="activeOnly">Nếu là true: chỉ lọc các sản phẩm đang có trạng thái 'Active' / 'Hoạt động' (dành cho người dùng thông thường).</param>
        /// <returns>Danh sách các đối tượng <see cref="Product"/> đã được sắp xếp giảm dần theo mã sản phẩm.</returns>
        public async Task<List<Product>> GetAllProductsAsync(bool activeOnly = false)
        {
            var list = new List<Product>();
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    string sql = @"
                        SELECT MaSP, TenSP, GiaBan, HinhAnh, MoTa, ThongSo, SoLuongTon, TrangThai, NgayTao
                        FROM dbo.SanPham ";

                    if (activeOnly)
                    {
                        sql += " WHERE TrangThai = 'Active' OR TrangThai = N'Hoạt động' ";
                    }

                    sql += " ORDER BY MaSP DESC";

                    using (var cmd = new SqlCommand(sql, conn))
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(ReadProductFromReader(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ShopService] GetAllProductsAsync error: {ex.Message}");
            }
            return list;
        }

        /// <summary>
        /// Tìm kiếm sản phẩm theo từ khóa (tên sản phẩm, mô tả hoặc mã số sản phẩm).
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm do người dùng nhập vào ô tra cứu.</param>
        /// <param name="activeOnly">Nếu là true: chỉ tìm các sản phẩm đang có trạng thái hoạt động.</param>
        /// <returns>Danh sách sản phẩm thỏa mãn điều kiện tìm kiếm.</returns>
        public async Task<List<Product>> SearchProductsAsync(string keyword, bool activeOnly = false)
        {
            var list = new List<Product>();
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    string sql = @"
                        SELECT MaSP, TenSP, GiaBan, HinhAnh, MoTa, ThongSo, SoLuongTon, TrangThai, NgayTao
                        FROM dbo.SanPham
                        WHERE (TenSP LIKE @Keyword OR MoTa LIKE @Keyword OR CAST(MaSP AS VARCHAR(20)) LIKE @Keyword) ";

                    if (activeOnly)
                    {
                        sql += " AND (TrangThai = 'Active' OR TrangThai = N'Hoạt động') ";
                    }

                    sql += " ORDER BY MaSP DESC";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Keyword", "%" + (keyword ?? "").Trim() + "%");
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(ReadProductFromReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ShopService] SearchProductsAsync error: {ex.Message}");
            }
            return list;
        }

        /// <summary>
        /// Truy vấn thông tin chi tiết của một sản phẩm theo khóa chính MaSP.
        /// </summary>
        /// <param name="maSP">Mã định danh sản phẩm cần lấy thông tin.</param>
        /// <returns>Đối tượng <see cref="Product"/> nếu tìm thấy; ngược lại trả về null.</returns>
        public async Task<Product> GetProductByIdAsync(int maSP)
        {
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    const string sql = @"
                        SELECT MaSP, TenSP, GiaBan, HinhAnh, MoTa, ThongSo, SoLuongTon, TrangThai, NgayTao
                        FROM dbo.SanPham
                        WHERE MaSP = @MaSP";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", maSP);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return ReadProductFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ShopService] GetProductByIdAsync error: {ex.Message}");
            }
            return null;
        }

        /// <summary>
        /// Thêm mới một sản phẩm vào cơ sở dữ liệu (chức năng quản trị viên).
        /// </summary>
        /// <param name="p">Đối tượng sản phẩm chứa đầy đủ các trường thông tin cần lưu.</param>
        /// <returns>True nếu ghi nhận dữ liệu thành công; ngược lại trả về False.</returns>
        public async Task<bool> AddProductAsync(Product p)
        {
            if (p == null) return false;
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    const string sql = @"
                        INSERT INTO dbo.SanPham 
                            (TenSP, GiaBan, HinhAnh, MoTa, ThongSo, SoLuongTon, TrangThai, NgayTao)
                        VALUES 
                            (@TenSP, @GiaBan, @HinhAnh, @MoTa, @ThongSo, @SoLuongTon, @TrangThai, GETDATE())";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenSP", p.TenSP ?? "Sản phẩm mới");
                        cmd.Parameters.AddWithValue("@GiaBan", p.GiaBan);
                        cmd.Parameters.AddWithValue("@HinhAnh", (object)p.HinhAnh ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MoTa", (object)p.MoTa ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ThongSo", (object)p.ThongSo ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SoLuongTon", p.SoLuongTon);
                        cmd.Parameters.AddWithValue("@TrangThai", string.IsNullOrWhiteSpace(p.TrangThai) ? "Active" : p.TrangThai);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ShopService] AddProductAsync error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Cập nhật thông tin chi tiết hoặc giá bán của một sản phẩm đã có (chức năng quản trị viên).
        /// </summary>
        /// <param name="p">Đối tượng sản phẩm mang thông tin sửa đổi và mã MaSP hợp lệ.</param>
        /// <returns>True nếu cập nhật thành công ít nhất một bản ghi; ngược lại trả về False.</returns>
        public async Task<bool> UpdateProductAsync(Product p)
        {
            if (p == null || p.MaSP <= 0) return false;
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    const string sql = @"
                        UPDATE dbo.SanPham
                        SET TenSP = @TenSP,
                            GiaBan = @GiaBan,
                            HinhAnh = @HinhAnh,
                            MoTa = @MoTa,
                            ThongSo = @ThongSo,
                            SoLuongTon = @SoLuongTon,
                            TrangThai = @TrangThai
                        WHERE MaSP = @MaSP";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", p.MaSP);
                        cmd.Parameters.AddWithValue("@TenSP", p.TenSP ?? "Sản phẩm");
                        cmd.Parameters.AddWithValue("@GiaBan", p.GiaBan);
                        cmd.Parameters.AddWithValue("@HinhAnh", (object)p.HinhAnh ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MoTa", (object)p.MoTa ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@ThongSo", (object)p.ThongSo ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SoLuongTon", p.SoLuongTon);
                        cmd.Parameters.AddWithValue("@TrangThai", string.IsNullOrWhiteSpace(p.TrangThai) ? "Active" : p.TrangThai);

                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ShopService] UpdateProductAsync error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Xóa bỏ vĩnh viễn một sản phẩm khỏi danh mục hệ thống (chức năng quản trị viên).
        /// </summary>
        /// <param name="maSP">Mã định danh sản phẩm cần xóa.</param>
        /// <returns>True nếu xóa thành công; ngược lại trả về False.</returns>
        public async Task<bool> DeleteProductAsync(int maSP)
        {
            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    const string sql = "DELETE FROM dbo.SanPham WHERE MaSP = @MaSP";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaSP", maSP);
                        int rows = await cmd.ExecuteNonQueryAsync();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ShopService] DeleteProductAsync error: {ex.Message}");
                return false;
            }
        }

        // =====================================================================
        // 2. CÁC PHƯƠNG THỨC THANH TOÁN & GIAO DỊCH NGÂN HÀNG
        // =====================================================================

        /// <summary>
        /// Lấy danh sách các tài khoản thanh toán đang ở trạng thái hoạt động của khách hàng.
        /// </summary>
        /// <param name="maKH">Mã định danh khách hàng cần tra cứu tài khoản.</param>
        /// <returns>Danh sách các đối tượng <see cref="TaiKhoan"/> có số dư hiện tại và thông tin ngân hàng.</returns>
        public async Task<List<TaiKhoan>> GetUserPaymentAccountsAsync(string maKH)
        {
            var accounts = new List<TaiKhoan>();
            if (string.IsNullOrWhiteSpace(maKH)) return accounts;

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    const string sql = @"
                        SELECT tk.SoTaiKhoan, tk.MaKH, tk.MaNganHang, tk.MaLoaiTien,
                               tk.SoDuBanDau, tk.SoDuHienTai, tk.NgayMo, tk.TrangThai,
                               nh.TenNganHang
                        FROM dbo.TaiKhoan tk
                        LEFT JOIN dbo.NganHang nh ON tk.MaNganHang = nh.MaNganHang
                        WHERE tk.MaKH = @MaKH AND (tk.TrangThai = 'Active' OR tk.TrangThai = N'Hoạt động')
                        ORDER BY tk.NgayMo DESC";

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                accounts.Add(new TaiKhoan
                                {
                                    SoTaiKhoan = reader["SoTaiKhoan"]?.ToString(),
                                    MaKH = reader["MaKH"]?.ToString(),
                                    MaNganHang = reader["MaNganHang"]?.ToString(),
                                    MaLoaiTien = reader["MaLoaiTien"]?.ToString() ?? "VND",
                                    SoDuBanDau = reader["SoDuBanDau"] != DBNull.Value ? Convert.ToDecimal(reader["SoDuBanDau"]) : 0m,
                                    SoDuHienTai = reader["SoDuHienTai"] != DBNull.Value ? Convert.ToDecimal(reader["SoDuHienTai"]) : 0m,
                                    NgayMo = reader["NgayMo"] != DBNull.Value ? Convert.ToDateTime(reader["NgayMo"]) : DateTime.Now,
                                    TrangThai = reader["TrangThai"]?.ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ShopService] GetUserPaymentAccountsAsync error: {ex.Message}");
            }

            return accounts;
        }

        /// <summary>
        /// Thực hiện quy trình thanh toán đơn hàng bằng tài khoản ngân hàng trong một SQL Transaction toàn vẹn:
        /// <list type="bullet">
        ///   <item><description>Khóa dòng và kiểm tra số dư thực tế của tài khoản nguồn.</description></item>
        ///   <item><description>Chặn thanh toán và Rollback nếu số dư tài khoản nhỏ hơn tổng giá trị đơn hàng.</description></item>
        ///   <item><description>Khấu trừ số dư tài khoản trực tiếp (SoDuHienTai = SoDuHienTai - totalAmount).</description></item>
        ///   <item><description>Tạo bản ghi lịch sử giao dịch chi tiêu trong bảng dbo.GiaoDich kèm danh sách sản phẩm.</description></item>
        ///   <item><description>Tự động cập nhật giảm số lượng hàng tồn kho (SoLuongTon) trong bảng dbo.SanPham.</description></item>
        /// </list>
        /// </summary>
        /// <param name="soTaiKhoan">Số tài khoản ngân hàng của khách hàng dùng để trích tiền.</param>
        /// <param name="totalAmount">Tổng số tiền cần thanh toán cho đơn hàng (VNĐ).</param>
        /// <param name="note">Nội dung ghi chú giao dịch (nếu để trống, hệ thống sẽ tự động tổng hợp danh sách tên sản phẩm).</param>
        /// <param name="cartItems">Danh sách các mặt hàng và số lượng mua trong giỏ hàng.</param>
        /// <returns>Bộ giá trị Tuple gồm: (Success: trạng thái thành công, Message: thông báo kết quả/lỗi, TransactionId: mã giao dịch GDxxx, NewBalance: số dư sau khi trừ tiền).</returns>
        public async Task<(bool Success, string Message, string TransactionId, decimal NewBalance)> ProcessPaymentAsync(
            string soTaiKhoan, decimal totalAmount, string note, List<CartItem> cartItems)
        {
            if (string.IsNullOrWhiteSpace(soTaiKhoan))
            {
                return (false, "Vui lòng chọn tài khoản ngân hàng để thanh toán.", "", 0m);
            }

            if (totalAmount <= 0)
            {
                return (false, "Số tiền thanh toán không hợp lệ.", "", 0m);
            }

            try
            {
                using (var conn = new SqlConnection(GetConnectionString()))
                {
                    await conn.OpenAsync();
                    using (var tran = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Kiểm tra tài khoản và số dư thực tế
                            decimal currentBalance = 0m;
                            const string checkSql = @"
                                SELECT SoDuHienTai, TrangThai
                                FROM dbo.TaiKhoan WITH (UPDLOCK, ROWLOCK)
                                WHERE SoTaiKhoan = @SoTaiKhoan";

                            using (var cmdCheck = new SqlCommand(checkSql, conn, tran))
                            {
                                cmdCheck.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                                using (var reader = await cmdCheck.ExecuteReaderAsync())
                                {
                                    if (!await reader.ReadAsync())
                                    {
                                        tran.Rollback();
                                        return (false, "Tài khoản thanh toán không tồn tại.", "", 0m);
                                    }

                                    string status = reader["TrangThai"]?.ToString();
                                    if (status != "Active" && status != "Hoạt động")
                                    {
                                        tran.Rollback();
                                        return (false, "Tài khoản đang bị khóa hoặc ngừng hoạt động.", "", 0m);
                                    }

                                    currentBalance = reader["SoDuHienTai"] != DBNull.Value
                                        ? Convert.ToDecimal(reader["SoDuHienTai"])
                                        : 0m;
                                }
                            }

                            if (currentBalance < totalAmount)
                            {
                                tran.Rollback();
                                return (false, $"Số dư tài khoản không đủ để thanh toán. Số dư hiện có: {currentBalance:N0} đ, Cần thanh toán: {totalAmount:N0} đ.", "", currentBalance);
                            }

                            decimal newBalance = currentBalance - totalAmount;

                            // 2. Trừ số dư tài khoản
                            const string deductSql = @"
                                UPDATE dbo.TaiKhoan
                                SET SoDuHienTai = @NewBalance
                                WHERE SoTaiKhoan = @SoTaiKhoan";

                            using (var cmdDeduct = new SqlCommand(deductSql, conn, tran))
                            {
                                cmdDeduct.Parameters.AddWithValue("@NewBalance", newBalance);
                                cmdDeduct.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                                await cmdDeduct.ExecuteNonQueryAsync();
                            }

                            // 3. Tạo mã giao dịch và ghi nhận vào bảng GiaoDich (MaLoaiGD = 2: Rút tiền/Chi tiêu)
                            string maGD = "GD" + DateTime.Now.ToString("yyMMddHHmmss") + (new Random().Next(100, 999));
                            string desc = !string.IsNullOrWhiteSpace(note) ? note : "";
                            if (string.IsNullOrWhiteSpace(desc))
                            {
                                if (cartItems != null && cartItems.Count > 0)
                                {
                                    var names = new List<string>();
                                    foreach (var ci in cartItems)
                                    {
                                        if (ci?.Product != null) names.Add($"{ci.Quantity}x {ci.Product.TenSP}");
                                    }
                                    desc = "Thanh toán mua sắm: " + string.Join(", ", names);
                                }
                                else
                                {
                                    desc = "Thanh toán mua sắm đơn hàng";
                                }
                            }
                            if (desc.Length > 250)
                            {
                                desc = desc.Substring(0, 247) + "...";
                            }

                            const string txSql = @"
                                INSERT INTO dbo.GiaoDich
                                    (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, SoTienGoc,
                                     MaLoaiTien, LoaiTien, NgayGiaoDich, NoiDung, TrangThai)
                                VALUES
                                    (@MaGD, @SoTaiKhoan, 2, @SoTien, @SoTienGoc,
                                     'VND', 'VND', GETDATE(), @NoiDung, 'Success')";

                            using (var cmdTx = new SqlCommand(txSql, conn, tran))
                            {
                                cmdTx.Parameters.AddWithValue("@MaGD", maGD);
                                cmdTx.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                                cmdTx.Parameters.AddWithValue("@SoTien", totalAmount);
                                cmdTx.Parameters.AddWithValue("@SoTienGoc", totalAmount);
                                cmdTx.Parameters.AddWithValue("@NoiDung", desc);
                                await cmdTx.ExecuteNonQueryAsync();
                            }

                            // 4. Cập nhật giảm số lượng tồn kho sản phẩm
                            if (cartItems != null)
                            {
                                const string stockSql = @"
                                    UPDATE dbo.SanPham
                                    SET SoLuongTon = CASE WHEN SoLuongTon >= @Qty THEN SoLuongTon - @Qty ELSE 0 END
                                    WHERE MaSP = @MaSP";

                                foreach (var item in cartItems)
                                {
                                    if (item.Product != null && item.Quantity > 0)
                                    {
                                        using (var cmdStock = new SqlCommand(stockSql, conn, tran))
                                        {
                                            cmdStock.Parameters.AddWithValue("@Qty", item.Quantity);
                                            cmdStock.Parameters.AddWithValue("@MaSP", item.Product.MaSP);
                                            await cmdStock.ExecuteNonQueryAsync();
                                        }
                                    }
                                }
                            }

                            tran.Commit();

                            // Báo hiệu cập nhật giao dịch cho toàn bộ hệ thống
                            TransactionService.NotifyTransactionChanged();

                            return (true, "Thanh toán đơn hàng thành công!", maGD, newBalance);
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ShopService] ProcessPaymentAsync error: {ex.Message}");
                return (false, "Lỗi trong quá trình thanh toán: " + ex.Message, "", 0m);
            }
        }

        // =====================================================================
        // 3. TIỆN ÍCH GIỎ HÀNG NHANH (CONVENIENCE CART APIS)
        // =====================================================================

        /// <summary>Danh sách các mặt hàng đang có trong giỏ hàng hiện tại.</summary>
        public static List<CartItem> CartItems => ShoppingCartService.Items;

        /// <summary>Tổng số lượng các mặt hàng trong giỏ hàng.</summary>
        public static int CartTotalCount => ShoppingCartService.TotalCount;

        /// <summary>Tổng tiền cần thanh toán cho toàn bộ giỏ hàng (VNĐ).</summary>
        public static decimal CartTotalAmount => ShoppingCartService.TotalAmount;

        /// <summary>
        /// Thêm một sản phẩm vào giỏ hàng với số lượng mong muốn.
        /// </summary>
        /// <param name="product">Sản phẩm cần thêm.</param>
        /// <param name="quantity">Số lượng cần mua (mặc định là 1).</param>
        public static void AddToCart(Product product, int quantity = 1)
        {
            ShoppingCartService.AddItem(product, quantity);
        }

        /// <summary>
        /// Cập nhật lại số lượng của một sản phẩm trong giỏ hàng.
        /// </summary>
        /// <param name="maSP">Mã sản phẩm cần cập nhật.</param>
        /// <param name="quantity">Số lượng mới (nếu nhỏ hơn hoặc bằng 0 thì sản phẩm sẽ bị xóa khỏi giỏ).</param>
        public static void UpdateCartQuantity(int maSP, int quantity)
        {
            ShoppingCartService.UpdateQuantity(maSP, quantity);
        }

        /// <summary>
        /// Xóa bỏ một sản phẩm khỏi giỏ hàng.
        /// </summary>
        /// <param name="maSP">Mã sản phẩm cần xóa.</param>
        public static void RemoveFromCart(int maSP)
        {
            ShoppingCartService.RemoveItem(maSP);
        }

        /// <summary>
        /// Xóa sạch tất cả các sản phẩm khỏi giỏ hàng.
        /// </summary>
        public static void ClearCart()
        {
            ShoppingCartService.Clear();
        }

        private Product ReadProductFromReader(SqlDataReader reader)
        {
            return new Product
            {
                MaSP = reader["MaSP"] != DBNull.Value ? Convert.ToInt32(reader["MaSP"]) : 0,
                TenSP = reader["TenSP"]?.ToString() ?? string.Empty,
                GiaBan = reader["GiaBan"] != DBNull.Value ? Convert.ToDecimal(reader["GiaBan"]) : 0m,
                HinhAnh = reader["HinhAnh"]?.ToString(),
                MoTa = reader["MoTa"]?.ToString(),
                ThongSo = reader["ThongSo"]?.ToString(),
                SoLuongTon = reader["SoLuongTon"] != DBNull.Value ? Convert.ToInt32(reader["SoLuongTon"]) : 0,
                TrangThai = reader["TrangThai"]?.ToString() ?? "Active",
                NgayTao = reader["NgayTao"] != DBNull.Value ? Convert.ToDateTime(reader["NgayTao"]) : DateTime.Now
            };
        }
    }
}
