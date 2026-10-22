using System;
using System.Collections.Generic;

namespace BankAccountManagement.Models
{
    /// <summary>
    /// Thực thể cấu hình hạn mức chi tiêu cá nhân được lưu trữ trong bảng dbo.HanMucChiTieu.
    /// Giúp người dùng đặt ngưỡng giới hạn chi tiêu trong ngày và trong tuần để quản lý tài chính an toàn.
    /// </summary>
    public class SpendingLimit
    {
        /// <summary>
        /// Mã khách hàng sở hữu cấu hình hạn mức chi tiêu (Khóa chính).
        /// </summary>
        public string MaKH { get; set; }

        /// <summary>
        /// Ngưỡng hạn mức chi tiêu tối đa trong một ngày (Mặc định: 2,000,000 VNĐ).
        /// </summary>
        public decimal HanMucNgay { get; set; } = 2000000m;

        /// <summary>
        /// Ngưỡng hạn mức chi tiêu tối đa trong một tuần (Mặc định: 10,000,000 VNĐ).
        /// </summary>
        public decimal HanMucTuan { get; set; } = 10000000m;

        /// <summary>
        /// Cờ bật/tắt tính năng thông báo cảnh báo khi chi tiêu sắp chạm hoặc vượt hạn mức.
        /// </summary>
        public bool CanhBaoVuotMuc { get; set; } = true;

        /// <summary>
        /// Thời điểm thiết lập hoặc cập nhật cấu hình hạn mức gần nhất.
        /// </summary>
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;
    }

    /// <summary>
    /// Đối tượng tổng hợp tình hình chi tiêu thực tế so sánh với hạn mức đã cài đặt (theo ngày và tuần).
    /// Cung cấp tỷ lệ phần trăm tiến độ, số tiền còn lại và thông điệp cảnh báo thời gian thực.
    /// </summary>
    public class SpendingStats
    {
        /// <summary>Hạn mức chi tiêu ngày đã cấu hình.</summary>
        public decimal HanMucNgay { get; set; } = 2000000m;

        /// <summary>Tổng số tiền đã chi tiêu trong ngày hôm nay.</summary>
        public decimal DaChiHomNay { get; set; } = 0m;

        /// <summary>Số tiền còn lại được phép chi trong ngày: Hạn mức - Đã chi (tối thiểu là 0).</summary>
        public decimal ConLaiHomNay => Math.Max(0m, HanMucNgay - DaChiHomNay);

        /// <summary>Tỷ lệ phần trăm chi tiêu ngày so với hạn mức (tối đa 100%).</summary>
        public int PhanTramNgay => HanMucNgay > 0 ? (int)Math.Min(100, Math.Round((DaChiHomNay / HanMucNgay) * 100)) : 0;

        /// <summary>Cờ đánh dấu người dùng đã vượt hạn mức ngày.</summary>
        public bool VuotMucNgay => DaChiHomNay > HanMucNgay;

        /// <summary>Hạn mức chi tiêu tuần đã cấu hình.</summary>
        public decimal HanMucTuan { get; set; } = 10000000m;

        /// <summary>Tổng số tiền đã chi tiêu trong tuần hiện tại.</summary>
        public decimal DaChiTuanNay { get; set; } = 0m;

        /// <summary>Số tiền còn lại được phép chi trong tuần: Hạn mức - Đã chi (tối thiểu là 0).</summary>
        public decimal ConLaiTuanNay => Math.Max(0m, HanMucTuan - DaChiTuanNay);

        /// <summary>Tỷ lệ phần trăm chi tiêu tuần so với hạn mức (tối đa 100%).</summary>
        public int PhanTramTuan => HanMucTuan > 0 ? (int)Math.Min(100, Math.Round((DaChiTuanNay / HanMucTuan) * 100)) : 0;

        /// <summary>Cờ đánh dấu người dùng đã vượt hạn mức tuần.</summary>
        public bool VuotMucTuan => DaChiTuanNay > HanMucTuan;

        /// <summary>Cờ cấu hình cho phép hiển thị cảnh báo khi chi tiêu vượt ngưỡng.</summary>
        public bool CanhBaoVuotMuc { get; set; } = true;

        /// <summary>Chuỗi định dạng số tiền đã chi trong ngày.</summary>
        public string FormattedDaChiHomNay => $"{DaChiHomNay:N0} đ";

        /// <summary>Chuỗi định dạng hạn mức chi tiêu trong ngày.</summary>
        public string FormattedHanMucNgay => $"{HanMucNgay:N0} đ";

        /// <summary>Chuỗi định dạng số tiền còn lại trong ngày.</summary>
        public string FormattedConLaiHomNay => $"{ConLaiHomNay:N0} đ";

        /// <summary>Chuỗi định dạng số tiền đã chi trong tuần.</summary>
        public string FormattedDaChiTuanNay => $"{DaChiTuanNay:N0} đ";

        /// <summary>Chuỗi định dạng hạn mức chi tiêu trong tuần.</summary>
        public string FormattedHanMucTuan => $"{HanMucTuan:N0} đ";

        /// <summary>Chuỗi định dạng số tiền còn lại trong tuần.</summary>
        public string FormattedConLaiTuanNay => $"{ConLaiTuanNay:N0} đ";

        /// <summary>
        /// Chuỗi thông báo trạng thái trực quan kèm biểu tượng icon:
        /// - Vượt hạn mức: "⛔ Đã vượt quá hạn mức chi tiêu!"
        /// - Đạt từ 80% trở lên: "⚠ Cảnh báo: Sắp chạm ngưỡng chi tiêu!"
        /// - Dưới 80%: "✔ Đang trong giới hạn chi tiêu an toàn"
        /// </summary>
        public string TrangThaiText
        {
            get
            {
                // BƯỚC 1: Kiểm tra xem đã vượt mức chi tiêu của ngày hoặc tuần chưa
                if (VuotMucNgay || VuotMucTuan) return "⛔ Đã vượt quá hạn mức chi tiêu!";

                // BƯỚC 2: Kiểm tra cảnh báo sớm nếu tiến độ chi tiêu đã chạm 80% trở lên
                if (PhanTramNgay >= 80 || PhanTramTuan >= 80) return "⚠ Cảnh báo: Sắp chạm ngưỡng chi tiêu!";

                // BƯỚC 3: Nếu mức chi tiêu vẫn nằm trong vùng an toàn
                return "✔ Đang trong giới hạn chi tiêu an toàn";
            }
        }
    }

    /// <summary>
    /// Đối tượng tổng hợp toàn bộ dữ liệu tổng quan cho trang chủ phía khách hàng (User Home Dashboard).
    /// </summary>
    public class UserHomeSummary
    {
        /// <summary>Họ và tên của người dùng.</summary>
        public string HoTen { get; set; } = "";

        /// <summary>Tên tài khoản đăng nhập.</summary>
        public string TenDangNhap { get; set; } = "";

        /// <summary>Mã khách hàng.</summary>
        public string MaKH { get; set; } = "";

        /// <summary>Hạng thành viên của người dùng (Ví dụ: "Hạng Tiêu Chuẩn", "Hạng Bạc", "Hạng Vàng", "Hạng Kim Cương").</summary>
        public string HangThanhVien { get; set; } = "Hạng Tiêu Chuẩn";

        /// <summary>Số tài khoản ngân hàng chính của người dùng.</summary>
        public string SoTaiKhoanChinh { get; set; } = "";

        /// <summary>
        /// Số tài khoản được che dấu bảo mật: Chỉ hiển thị 6 số đầu và che phần đuôi bằng dấu sao '*'.
        /// </summary>
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

        /// <summary>Tổng số dư khả dụng trên toàn bộ các tài khoản thanh toán của người dùng (VNĐ).</summary>
        public decimal TongSoDu { get; set; } = 0m;

        /// <summary>Chuỗi định dạng tổng số dư kèm đơn vị VNĐ.</summary>
        public string FormattedTongSoDu => $"{TongSoDu:N0} đ";

        /// <summary>Hồ sơ khoản vay sắp tới hạn thanh toán gần nhất (null nếu không có khoản vay nợ).</summary>
        public BorrowingLoan KhoanVaySapToi { get; set; }

        /// <summary>Thống kê chi tiêu thực tế so với hạn mức.</summary>
        public SpendingStats Spending { get; set; } = new SpendingStats();

        /// <summary>Danh sách tất cả các tài khoản thanh toán của khách hàng.</summary>
        public List<TaiKhoan> DanhSachTaiKhoan { get; set; } = new List<TaiKhoan>();
    }
}
