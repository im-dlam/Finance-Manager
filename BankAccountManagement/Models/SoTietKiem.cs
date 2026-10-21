using System;

namespace BankAccountManagement.Models
{
    /// <summary>
    /// Đại diện cho một Sổ / Tài khoản tiết kiệm trực tuyến trong hệ thống.
    /// </summary>
    public class SoTietKiem
    {
        public string MaSoTKTK { get; set; }           // Ví dụ: TK-83102228
        public string MaKH { get; set; }               // Mã khách hàng
        public string HoTen { get; set; }              // Tên khách hàng
        public string TenDangNhap { get; set; }        // Username
        public string SoTaiKhoanTrich { get; set; }    // Số tài khoản nguồn trích nợ
        public decimal SoTienGui { get; set; }         // Tiền gốc gửi
        public int KyHanThang { get; set; }            // Kỳ hạn (tháng): 1, 3, 6, 12, ...
        public decimal LaiSuatNam { get; set; }        // Lãi suất (%/năm), ví dụ: 4.75, 6.75
        public decimal TienLaiDuTinh { get; set; }     // Tiền lãi dự tính khi đáo hạn
        public decimal TongTienDaoHanDuTinh { get; set; } // Tổng tiền nhận khi đáo hạn (Gốc + Lãi)
        public string HinhThucDaoHan { get; set; }     // "Tự động tái tục gốc", "Tái tục gốc & lãi (Lãi gộp)", "Tất toán về tài khoản"
        public DateTime NgayMo { get; set; }           // Ngày mở sổ
        public DateTime NgayDaoHan { get; set; }       // Ngày đáo hạn
        public string TrangThai { get; set; }          // "Đang gửi", "Đã tất toán"
        public DateTime? NgayTatToan { get; set; }     // Ngày thực hiện tất toán
        public decimal? TienLaiThucNhan { get; set; }  // Tiền lãi thực nhận
        public decimal? TongTienThucNhan { get; set; } // Tổng tiền thực nhận (Gốc + Lãi thực)
        public string GhiChu { get; set; }

        // Formatted Helpers phục vụ hiển thị UI
        public string FormattedSoTienGui => $"{SoTienGui:N0} đ";
        public string FormattedTienLaiDuTinh => $"{TienLaiDuTinh:N0} đ";
        public string FormattedTongTienDaoHan => $"{TongTienDaoHanDuTinh:N0} đ";
        public string FormattedLaiSuat => $"{LaiSuatNam:N2}% / năm";
        public string FormattedKyHan => $"{KyHanThang} tháng";
        public string FormattedNgayMo => NgayMo.ToString("dd/MM/yyyy");
        public string FormattedNgayDaoHan => NgayDaoHan.ToString("dd/MM/yyyy");
        public string FormattedNgayTatToan => NgayTatToan.HasValue ? NgayTatToan.Value.ToString("dd/MM/yyyy HH:mm") : "-";
        public string FormattedCustomer => string.IsNullOrWhiteSpace(HoTen) ? MaKH : $"{HoTen} ({MaKH})";
        
        public string FormattedMaskedSTK
        {
            get
            {
                if (string.IsNullOrEmpty(SoTaiKhoanTrich)) return "";
                if (SoTaiKhoanTrich.Length > 7)
                    return SoTaiKhoanTrich.Substring(0, 7) + "...";
                return SoTaiKhoanTrich;
            }
        }

        public string TinhTrangDaoHan
        {
            get
            {
                if (TrangThai == "Đã tất toán") return "Đã tất toán";
                var diff = (NgayDaoHan.Date - DateTime.Today).Days;
                if (diff < 0) return $"Quá hạn {Math.Abs(diff)} ngày";
                if (diff == 0) return "Đến hạn hôm nay";
                return $"Còn {diff} ngày";
            }
        }
    }

    /// <summary>
    /// Gói kỳ hạn gửi tiết kiệm và lãi suất niêm yết của ngân hàng.
    /// </summary>
    public class SavingsPackage
    {
        public int KyHanThang { get; set; }
        public decimal LaiSuatNam { get; set; }
        public string TenGoi => $"{KyHanThang} tháng ({LaiSuatNam:N2}%/năm)";

        public override string ToString()
        {
            return TenGoi;
        }
    }

    /// <summary>
    /// Kết quả tính toán lãi suất và số tiền đáo hạn dự tính thời gian thực.
    /// </summary>
    public class SavingsCalculationResult
    {
        public decimal SoTienGui { get; set; }
        public int KyHanThang { get; set; }
        public decimal LaiSuatNam { get; set; }
        public DateTime NgayMo { get; set; }
        public DateTime NgayDaoHan { get; set; }
        public int SoNgay { get; set; }
        public decimal TienLaiDuTinh { get; set; }
        public decimal TongTienDaoHanDuTinh { get; set; }

        public string FormattedSoTienGui => $"{SoTienGui:N0} đ";
        public string FormattedTienLai => $"{TienLaiDuTinh:N0} đ";
        public string FormattedTongTien => $"{TongTienDaoHanDuTinh:N0} đ";
        public string FormattedLaiSuat => $"{LaiSuatNam:N2}% / năm";
        public string FormattedNgayDaoHan => NgayDaoHan.ToString("dd/MM/yyyy");
    }

    /// <summary>
    /// Thông tin hồ sơ tiết kiệm của khách hàng phục vụ cột thông tin bên Admin.
    /// </summary>
    public class CustomerSavingsProfile
    {
        public string MaKH { get; set; }
        public string HoTen { get; set; }
        public string TenDangNhap { get; set; }
        public string CMND_CCCD { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public decimal TongSoDuKhaDung { get; set; }
        public int TongSoTKTK { get; set; }
        public int SoDangGui { get; set; }
        public int SoDaTatToan { get; set; }
        public decimal TongTienDangGui { get; set; }
        public decimal TongTienLaiDuTinh { get; set; }

        public string FormattedTongTienDangGui => $"{TongTienDangGui:N0} đ";
        public string FormattedTongSoDu => $"{TongSoDuKhaDung:N0} đ";
    }

    /// <summary>
    /// Các chỉ số tổng quan hệ thống huy động vốn tiết kiệm dành cho Admin.
    /// </summary>
    public class SavingsAdminOverview
    {
        public int TongSoSo { get; set; }
        public int SoDangGui { get; set; }
        public int SoDaTatToan { get; set; }
        public decimal TongTienDangHuyDong { get; set; }
        public decimal TongTienDaTatToan { get; set; }
        public decimal TongTienLaiDuTinh { get; set; }

        public string FormattedDangHuyDong => $"{TongTienDangHuyDong:N0} đ";
        public string FormattedDaTatToan => $"{TongTienDaTatToan:N0} đ";
        public string FormattedLaiDuTinh => $"{TongTienLaiDuTinh:N0} đ";
    }
}
