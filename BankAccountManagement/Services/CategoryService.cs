using BankAccountManagement.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BankAccountManagement.Services
{
    /// <summary>
    /// Service cung cấp danh mục (combobox datasource).
    /// 
    /// Schema v2.0:
    /// - Bảng khách hàng: ThongTinKhachHang (cột tên: HoTen, không phải TenKH).
    /// - Các bảng khác giữ nguyên tên.
    /// </summary>
    public class DanhMucService
    {
        private readonly string _connectionString;

        public DanhMucService()
        {
            DatabaseConfig config = DatabaseConfigManager.Load();
            _connectionString = DatabaseService.BuildConnectionString(config);
        }

        // ============================================================
        //  DANH MỤC KHÁCH HÀNG
        //  ⚠ Cột tên là HoTen (không phải TenKH)
        // ============================================================
        public List<(string Ma, string Ten)> GetKhachHang()
            => QueryPairs(@"
                SELECT MaKH, HoTen 
                FROM   dbo.ThongTinKhachHang 
                WHERE  TrangThai = N'Active'
                ORDER  BY HoTen");

        // ============================================================
        //  DANH MỤC NGÂN HÀNG
        // ============================================================
        public List<(string Ma, string Ten)> GetNganHang()
            => QueryPairs(@"
                SELECT MaNganHang, TenNganHang 
                FROM   dbo.NganHang 
                ORDER  BY TenNganHang");

        // ============================================================
        //  DANH MỤC LOẠI TIỀN
        // ============================================================
        public List<(string Ma, string Ten)> GetLoaiTien()
            => QueryPairs(@"
                SELECT MaLoaiTien, TenLoaiTien 
                FROM   dbo.LoaiTien 
                ORDER  BY TenLoaiTien");

        // ============================================================
        //  DANH MỤC LOẠI GIAO DỊCH (bonus — nếu cần dùng)
        // ============================================================
        public List<(string Ma, string Ten)> GetLoaiGiaoDich()
            => QueryPairs(@"
                SELECT MIN(CAST(MaLoaiGD AS VARCHAR(10))), TenLoaiGD 
                FROM   dbo.LoaiGiaoDich 
                GROUP  BY TenLoaiGD
                ORDER  BY TenLoaiGD");

        // ============================================================
        //  HELPER
        // ============================================================
        private List<(string Ma, string Ten)> QueryPairs(string sql)
        {
            var result = new List<(string, string)>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string ma = reader.IsDBNull(0) ? "" : reader.GetValue(0).ToString();
                        string ten = reader.IsDBNull(1) ? "" : reader.GetString(1);
                        result.Add((ma, ten));
                    }
                }
            }
            return result;
        }
    }
}