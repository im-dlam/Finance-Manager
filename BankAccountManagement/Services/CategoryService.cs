// Services/DanhMucService.cs
using BankAccountManagement.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BankAccountManagement.Services
{
    public class DanhMucService
    {
        private readonly string _connectionString;

        public DanhMucService()
        {
            DatabaseConfig config = DatabaseConfigManager.Load();
            _connectionString = DatabaseService.BuildConnectionString(config);
        }

        public List<(string Ma, string Ten)> GetKhachHang()
            => QueryPairs("SELECT MaKH, TenKH FROM KhachHang ORDER BY TenKH");

        public List<(string Ma, string Ten)> GetNganHang()
            => QueryPairs("SELECT MaNganHang, TenNganHang FROM NganHang ORDER BY TenNganHang");

        public List<(string Ma, string Ten)> GetLoaiTien()
            => QueryPairs("SELECT MaLoaiTien, TenLoaiTien FROM LoaiTien ORDER BY TenLoaiTien");

        private List<(string Ma, string Ten)> QueryPairs(string sql)
        {
            var result = new List<(string, string)>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        result.Add((reader.GetString(0), reader.GetString(1)));
            }
            return result;
        }
    }   // ✅ Chỉ 1 class, đóng đúng chỗ
}