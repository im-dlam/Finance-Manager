using System;

namespace BankAccountManagement.Models
{
    /// <summary>
    /// Kết quả thẩm định tài chính trong 1 quý (90 ngày gần nhất) của khách hàng
    /// dành cho Admin xem xét, đánh giá năng lực chi trả khi xét duyệt khoản vay.
    /// </summary>
    public class FinancialAppraisal
    {
        public string MaKH { get; set; }
        public string HoTen { get; set; }
        public string TenDangNhap { get; set; }
        public string SoTaiKhoan { get; set; }
        public string TenNganHang { get; set; }
        public decimal SoDuHienTai { get; set; }

        // Thống kê trong 1 quý (3 tháng / 90 ngày gần nhất)
        public decimal TongThuQuy { get; set; }
        public decimal TongChiQuy { get; set; }
        public decimal ChenhLechThuChi => TongThuQuy - TongChiQuy;
        public int SoGiaoDichQuy { get; set; }

        // Đánh giá sơ bộ
        public string DanhGia { get; set; }
        public string GhiChuChiTiet { get; set; }

        public string FormattedSoDuHienTai => $"{SoDuHienTai:N0} đ";
        public string FormattedTongThuQuy => $"+ {TongThuQuy:N0} đ";
        public string FormattedTongChiQuy => $"- {TongChiQuy:N0} đ";
        public string FormattedChenhLech => (ChenhLechThuChi >= 0 ? "+ " : "- ") + $"{Math.Abs(ChenhLechThuChi):N0} đ";

        public string FormattedBalance => FormattedSoDuHienTai;
        public string FormattedIncome => FormattedTongThuQuy;
        public string FormattedExpense => FormattedTongChiQuy;
        public string FormattedNetCashFlow => FormattedChenhLech;
    }
}
