using BankAccountManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BankAccountManagement.Services
{
    public class AccountService
    {
        private readonly string _connectionString;

        public AccountService()
        {
            DatabaseConfig config = DatabaseConfigManager.Load();
            _connectionString = DatabaseService.BuildConnectionString(config);
        }

        // ─────────────────────────────────────────
        // 1. Lấy tất cả tài khoản Active
        // ─────────────────────────────────────────
        public List<TaiKhoan> GetActiveTaiKhoan()
        {
            var result = new List<TaiKhoan>();
            const string sql = @"
                SELECT SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien,
                       SoDuBanDau, SoDuHienTai, NgayMo, TrangThai
                FROM   TaiKhoan
                WHERE  TrangThai = 'Active'
                ORDER  BY NgayMo DESC";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                            result.Add(MapReader(reader));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy danh sách tài khoản: " + ex.Message, ex);
            }

            return result;
        }

        // ─────────────────────────────────────────
        // 2. Tìm kiếm theo Số tài khoản (SQL LIKE)
        // ─────────────────────────────────────────
        public List<TaiKhoan> SearchTaiKhoan(string keyword)
        {
            var result = new List<TaiKhoan>();
            const string sql = @"
                SELECT SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien,
                       SoDuBanDau, SoDuHienTai, NgayMo, TrangThai
                FROM   TaiKhoan
                WHERE  TrangThai = 'Active'
                  AND  SoTaiKhoan LIKE @Keyword
                ORDER  BY NgayMo DESC";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
                        using (var reader = cmd.ExecuteReader())
                            while (reader.Read())
                                result.Add(MapReader(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm kiếm tài khoản: " + ex.Message, ex);
            }

            return result;
        }

        // ─────────────────────────────────────────
        // 3. Lấy 1 tài khoản theo Số tài khoản
        // ─────────────────────────────────────────
        public TaiKhoan GetBySoTaiKhoan(string soTaiKhoan)
        {
            const string sql = @"
                SELECT SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien,
                       SoDuBanDau, SoDuHienTai, NgayMo, TrangThai
                FROM   TaiKhoan
                WHERE  SoTaiKhoan = @SoTaiKhoan";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                        using (var reader = cmd.ExecuteReader())
                            return reader.Read() ? MapReader(reader) : null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy tài khoản: " + ex.Message, ex);
            }
        }

        // ─────────────────────────────────────────
        // 4. Lấy danh sách theo Mã khách hàng
        // ─────────────────────────────────────────
        public List<TaiKhoan> GetByMaKH(string maKH)
        {
            var result = new List<TaiKhoan>();
            const string sql = @"
                SELECT SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien,
                       SoDuBanDau, SoDuHienTai, NgayMo, TrangThai
                FROM   TaiKhoan
                WHERE  MaKH = @MaKH
                ORDER  BY NgayMo DESC";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKH", maKH);
                        using (var reader = cmd.ExecuteReader())
                            while (reader.Read())
                                result.Add(MapReader(reader));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lấy tài khoản theo KH: " + ex.Message, ex);
            }

            return result;
        }

        // ─────────────────────────────────────────
        // 5. Thêm mới tài khoản
        // ─────────────────────────────────────────
        public bool AddTaiKhoan(TaiKhoan tk)
        {
            const string sql = @"
                INSERT INTO TaiKhoan
                    (SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien,
                     SoDuBanDau, SoDuHienTai, NgayMo, TrangThai)
                VALUES
                    (@SoTaiKhoan, @MaKH, @MaNganHang, @MaLoaiTien,
                     @SoDuBanDau, @SoDuBanDau, @NgayMo, 'Active')";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@SoTaiKhoan", tk.SoTaiKhoan);
                        cmd.Parameters.AddWithValue("@MaKH", tk.MaKH);
                        cmd.Parameters.AddWithValue("@MaNganHang", tk.MaNganHang);
                        cmd.Parameters.AddWithValue("@MaLoaiTien", (object)tk.MaLoaiTien ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SoDuBanDau", tk.SoDuBanDau);
                        cmd.Parameters.AddWithValue("@NgayMo",
                            tk.NgayMo == DateTime.MinValue ? DateTime.Now : tk.NgayMo);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm tài khoản: " + ex.Message, ex);
            }
        }

        // ─────────────────────────────────────────
        // 6. Cập nhật tài khoản
        // ─────────────────────────────────────────
        public bool UpdateTaiKhoan(TaiKhoan tk)
        {
            const string sql = @"
                UPDATE TaiKhoan
                SET    MaKH       = @MaKH,
                       MaNganHang = @MaNganHang,
                       MaLoaiTien = @MaLoaiTien,
                       TrangThai  = @TrangThai
                WHERE  SoTaiKhoan = @SoTaiKhoan";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@SoTaiKhoan", tk.SoTaiKhoan);
                        cmd.Parameters.AddWithValue("@MaKH", tk.MaKH);
                        cmd.Parameters.AddWithValue("@MaNganHang", tk.MaNganHang);
                        cmd.Parameters.AddWithValue("@MaLoaiTien", (object)tk.MaLoaiTien ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TrangThai", tk.TrangThai);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi cập nhật tài khoản: " + ex.Message, ex);
            }
        }

        // ─────────────────────────────────────────
        // 7. Đóng tài khoản (xóa mềm)
        // ─────────────────────────────────────────
        public bool CloseTaiKhoan(string soTaiKhoan)
        {
            const string sql = @"
                UPDATE TaiKhoan
                SET    TrangThai = 'Closed'
                WHERE  SoTaiKhoan = @SoTaiKhoan";

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@SoTaiKhoan", soTaiKhoan);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đóng tài khoản: " + ex.Message, ex);
            }
        }

        // ─────────────────────────────────────────
        // HELPER: Map SqlDataReader → TaiKhoan
        // ─────────────────────────────────────────
        private static TaiKhoan MapReader(SqlDataReader reader)
        {
            return new TaiKhoan
            {
                SoTaiKhoan = reader["SoTaiKhoan"].ToString(),
                MaKH = reader["MaKH"].ToString(),
                MaNganHang = reader["MaNganHang"].ToString(),
                MaLoaiTien = reader["MaLoaiTien"] as string,
                SoDuBanDau = Convert.ToDecimal(reader["SoDuBanDau"]),
                SoDuHienTai = Convert.ToDecimal(reader["SoDuHienTai"]),
                NgayMo = Convert.ToDateTime(reader["NgayMo"]),
                TrangThai = reader["TrangThai"].ToString()
            };
        }
    }
}