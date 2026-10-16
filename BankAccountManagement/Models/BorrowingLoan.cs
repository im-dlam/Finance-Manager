using System;

namespace BankAccountManagement.Models
{
    /// <summary>
    /// Đại diện cho một hồ sơ khoản vay vốn trong hệ thống.
    /// </summary>
    public class BorrowingLoan
    {
        public string MaKhoanVay { get; set; }
        public string MaKH { get; set; }
        public string HoTen { get; set; }
        public string TenDangNhap { get; set; }
        public string SoTaiKhoan { get; set; }
        public string TenNganHang { get; set; }
        public decimal SoTienVay { get; set; }
        public int ThoiGianVay { get; set; } // Ngày
        public decimal LaiSuat { get; set; } // % / ngày hoặc % / tháng
        public decimal TienLai { get; set; }
        public decimal TongPhaiTra { get; set; }
        public DateTime NgayDangKy { get; set; }
        public DateTime NgayDaoHan { get; set; }
        public string TrangThai { get; set; } // "Chờ xét duyệt", "Đã duyệt", "Đã từ chối", "Đã tất toán"
        public string GhiChu { get; set; }
        public DateTime? NgayDuyet { get; set; }
        public string NguoiDuyet { get; set; }

        public string FormattedSoTienVay => $"{SoTienVay:N0} đ";
        public string FormattedTongPhaiTra => $"{TongPhaiTra:N0} đ";
        public string FormattedTienLai => $"{TienLai:N0} đ";
        public string FormattedLaiSuat => $"{LaiSuat:N2}%/ngày ({(LaiSuat * 30):N1}%/tháng)";
        public string FormattedNgayDangKy => NgayDangKy.ToString("dd/MM/yyyy HH:mm");
        public string FormattedNgayDaoHan => NgayDaoHan.ToString("dd/MM/yyyy");

        public string FormattedLoanAmount => FormattedSoTienVay;
        public string FormattedTotalPay => FormattedTongPhaiTra;
        public string FormattedInterest => FormattedTienLai;
        public string FormattedTerm => $"{ThoiGianVay} ngày";
        public string FormattedRegisterDate => NgayDangKy.ToString("dd/MM/yyyy");
        public string FormattedCustomer => string.IsNullOrWhiteSpace(HoTen) ? MaKH : $"{HoTen} ({MaKH})";
        public string FormattedNgayDuyet => NgayDuyet.HasValue ? NgayDuyet.Value.ToString("dd/MM/yyyy") : "-";

        public string TinhTrangHan
        {
            get
            {
                if (TrangThai == "Đã tất toán") return "Đã tất toán";
                if (TrangThai != "Đã duyệt") return "-";
                var diff = (NgayDaoHan.Date - DateTime.Today).Days;
                if (diff < 0) return $"Quá hạn {Math.Abs(diff)} ngày";
                if (diff == 0) return "Đến hạn hôm nay";
                return $"Còn {diff} ngày";
            }
        }
    }

    /// <summary>
    /// Kết quả tính toán lãi suất và số nợ phải trả theo thời gian thực từ BorrowingService.
    /// </summary>
    public class LoanCalculationResult
    {
        public decimal SoTienVay { get; set; }
        public int ThoiGianVay { get; set; }
        public decimal LaiSuatNgay { get; set; }
        public decimal TienLai { get; set; }
        public decimal TongPhaiTra { get; set; }
        public DateTime NgayDangKy { get; set; }
        public DateTime NgayDaoHan { get; set; }

        public string FormattedSoTienVay => $"{SoTienVay:N0} đ";
        public string FormattedTienLai => $"{TienLai:N0} đ";
        public string FormattedTongPhaiTra => $"{TongPhaiTra:N0} đ";
        public string FormattedLaiSuat => $"{LaiSuatNgay:N2}%/ngày";
        public string FormattedNgayDaoHan => NgayDaoHan.ToString("dd/MM/yyyy");
    }

    /// <summary>
    /// Thống kê tổng quan các chỉ số hồ sơ vay vốn trong hệ thống phục vụ Ban Quản Trị.
    /// </summary>
    public class BorrowingStatistics
    {
        public int TongHoSo { get; set; }
        public int ChoDuyet { get; set; }
        public int DaDuyet { get; set; }
        public int TuChoi { get; set; }
        public int DaTatToan { get; set; }
        public decimal TongTienDaGiaiNgan { get; set; }
        public decimal TongDuNoCanThuHoi { get; set; }
    }
}
