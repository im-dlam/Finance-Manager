using System;
using System.Collections.Generic;

namespace BankAccountManagement.Models
{
    /// <summary>
    /// Cấu hình hạn mức chi tiêu được lưu trữ trong dbo.HanMucChiTieu.
    /// </summary>
    public class SpendingLimit
    {
        public string MaKH { get; set; }
        public decimal HanMucNgay { get; set; } = 2000000m; // Mặc định 2 triệu / ngày
        public decimal HanMucTuan { get; set; } = 10000000m; // Mặc định 10 triệu / tuần
        public bool CanhBaoVuotMuc { get; set; } = true;
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;
    }

    /// <summary>
    /// Thống kê chi tiêu thực tế so với hạn mức (ngày và tuần).
    /// </summary>
    public class SpendingStats
    {
        public decimal HanMucNgay { get; set; } = 2000000m;
        public decimal DaChiHomNay { get; set; } = 0m;
        public decimal ConLaiHomNay => Math.Max(0m, HanMucNgay - DaChiHomNay);
        public int PhanTramNgay => HanMucNgay > 0 ? (int)Math.Min(100, Math.Round((DaChiHomNay / HanMucNgay) * 100)) : 0;
        public bool VuotMucNgay => DaChiHomNay > HanMucNgay;

        public decimal HanMucTuan { get; set; } = 10000000m;
        public decimal DaChiTuanNay { get; set; } = 0m;
        public decimal ConLaiTuanNay => Math.Max(0m, HanMucTuan - DaChiTuanNay);
        public int PhanTramTuan => HanMucTuan > 0 ? (int)Math.Min(100, Math.Round((DaChiTuanNay / HanMucTuan) * 100)) : 0;
        public bool VuotMucTuan => DaChiTuanNay > HanMucTuan;

        public bool CanhBaoVuotMuc { get; set; } = true;

        public string FormattedDaChiHomNay => $"{DaChiHomNay:N0} đ";
        public string FormattedHanMucNgay => $"{HanMucNgay:N0} đ";
        public string FormattedConLaiHomNay => $"{ConLaiHomNay:N0} đ";

        public string FormattedDaChiTuanNay => $"{DaChiTuanNay:N0} đ";
        public string FormattedHanMucTuan => $"{HanMucTuan:N0} đ";
        public string FormattedConLaiTuanNay => $"{ConLaiTuanNay:N0} đ";

        public string TrangThaiText
        {
            get
            {
                if (VuotMucNgay || VuotMucTuan) return "⛔ Đã vượt quá hạn mức chi tiêu!";
                if (PhanTramNgay >= 80 || PhanTramTuan >= 80) return "⚠ Cảnh báo: Sắp chạm ngưỡng chi tiêu!";
                return "✔ Đang trong giới hạn chi tiêu an toàn";
            }
        }
    }

    /// <summary>
    /// Tổng hợp dữ liệu trang chủ phía User.
    /// </summary>
    public class UserHomeSummary
    {
        public string HoTen { get; set; } = "";
        public string TenDangNhap { get; set; } = "";
        public string MaKH { get; set; } = "";
        public string HangThanhVien { get; set; } = "Hạng Titan";
        public string SoTaiKhoanChinh { get; set; } = "";
        
        public string SoTaiKhoanMasked
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SoTaiKhoanChinh)) return "Chưa có tài khoản";
                string stk = SoTaiKhoanChinh.Trim();
                if (stk.Length <= 6) return stk;
                return stk.Substring(0, 6) + new string('*', Math.Max(4, stk.Length - 6));
            }
        }

        public decimal TongSoDu { get; set; } = 0m;
        public string FormattedTongSoDu => $"{TongSoDu:N0} đ";

        public BorrowingLoan KhoanVaySapToi { get; set; }
        public SpendingStats Spending { get; set; } = new SpendingStats();
        public List<TaiKhoan> DanhSachTaiKhoan { get; set; } = new List<TaiKhoan>();
    }
}
