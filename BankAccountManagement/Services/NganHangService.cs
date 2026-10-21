using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BankAccountManagement.Services
{
    public class NganHangService
    {
        /// <summary>
        /// Lấy toàn bộ danh sách ngân hàng công khai trực tiếp từ DB (dành cho Form Đăng ký / Đăng nhập).
        /// </summary>
        public static List<NganHang> GetPublicBanks()
        {
            var result = new List<NganHang>();
            try
            {
                DatabaseConfig config = DatabaseConfigManager.Load();
                using (var conn = DatabaseService.CreateConnection(config))
                {
                    conn.Open();
                    const string sql = @"
                        SELECT MaNganHang, TenNganHang, ChiNhanh, SwiftCode
                        FROM   dbo.NganHang
                        ORDER  BY CAST(SUBSTRING(MaNganHang, 3, 10) AS INT), MaNganHang;";

                    using (var cmd = new SqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
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
                System.Diagnostics.Debug.WriteLine("GetPublicBanks error: " + ex.Message);
            }
            return result;
        }
        private readonly string _connectionString;

        public NganHangService()
        {
            DatabaseConfig config = DatabaseConfigManager.Load();
            _connectionString = DatabaseService.BuildConnectionString(config);
        }

        // ─────────────────────────────────────────
        // 1. Lấy tất cả ngân hàng (Admin: tất cả; User: chỉ ngân hàng đang có tài khoản)
        // ─────────────────────────────────────────
        public List<NganHang> GetAllNganHang()
        {
            var result = new List<NganHang>();
            string sql;
            if (CurrentSession.IsAdmin || string.IsNullOrWhiteSpace(CurrentSession.MaKH))
            {
                sql = @"
                    SELECT MaNganHang, TenNganHang, ChiNhanh, SwiftCode
                    FROM   dbo.NganHang
                    ORDER  BY TenNganHang";
            }
            else
            {
                sql = @"
                    SELECT DISTINCT NH.MaNganHang, NH.TenNganHang, NH.ChiNhanh, NH.SwiftCode
                    FROM   dbo.NganHang NH
                    INNER JOIN dbo.TaiKhoan TK ON NH.MaNganHang = TK.MaNganHang
                    WHERE  TK.MaKH = @MaKH
                    ORDER  BY NH.TenNganHang";
            }

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        if (!CurrentSession.IsAdmin && !string.IsNullOrWhiteSpace(CurrentSession.MaKH))
                        {
                            cmd.Parameters.AddWithValue("@MaKH", CurrentSession.MaKH);
                        }

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

        // ─────────────────────────────────────────
        // 2. Tìm kiếm ngân hàng (có phân quyền)
        // ─────────────────────────────────────────
        public List<NganHang> SearchNganHang(string keyword)
        {
            var result = new List<NganHang>();
            string sql;
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
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
                        if (!CurrentSession.IsAdmin && !string.IsNullOrWhiteSpace(CurrentSession.MaKH))
                        {
                            cmd.Parameters.AddWithValue("@MaKH", CurrentSession.MaKH);
                        }

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

        // ─────────────────────────────────────────
        // 3. Thêm ngân hàng
        // ─────────────────────────────────────────
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
                if (ex is SqlException sqlEx && sqlEx.Number == 2627) // Unique constraint violation
                {
                    throw new Exception($"Mã ngân hàng '{nh.MaNganHang}' đã tồn tại. Vui lòng sử dụng mã khác.", ex);
                }

                throw new Exception("Lỗi thêm ngân hàng: " + ex.Message, ex);
            }
        }

        // ─────────────────────────────────────────
        // 4. Cập nhật ngân hàng
        // ─────────────────────────────────────────
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
                if(ex is SqlException sqlEx && sqlEx.Number == 547) // Foreign key violation
                {
                    throw new Exception($"Không thể cập nhật ngân hàng '{nh.MaNganHang}' vì có liên kết dữ liệu khác.", ex);
                }
                throw new Exception("Lỗi cập nhật ngân hàng: " + ex.Message, ex);
            }
        }

        // ─────────────────────────────────────────
        // 5. Xóa ngân hàng
        // ─────────────────────────────────────────
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
                if(ex is SqlException sqlEx && sqlEx.Number == 547) // Foreign key violation
                {
                    throw new Exception($"Không thể xóa ngân hàng '{maNganHang}' vì có liên kết dữ liệu khác.", ex);
                }
                throw new Exception("Lỗi xóa ngân hàng: " + ex.Message, ex);
            }
        }

        // ─────────────────────────────────────────
        // HELPER: Map SqlDataReader → NganHang
        // ─────────────────────────────────────────
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
