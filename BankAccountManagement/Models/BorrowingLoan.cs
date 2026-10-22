using System;

namespace BankAccountManagement.Models
{
    /// <summary>
    /// Thực thể đại diện cho một hồ sơ khoản vay vốn trực tuyến trong hệ thống ngân hàng.
    /// Chứa đầy đủ thông tin về khách hàng vay, số tiền, lãi suất, thời hạn và trạng thái phê duyệt.
    /// </summary>
    public class BorrowingLoan
    {
        /// <summary>
        /// Mã định danh duy nhất của hồ sơ khoản vay (Khóa chính, ví dụ: "VAY-00123").
        /// </summary>
        public string MaKhoanVay { get; set; }

        /// <summary>
        /// Mã khách hàng đăng ký vay vốn (Liên kết bảng ThongTinKhachHang).
        /// </summary>
        public string MaKH { get; set; }

        /// <summary>
        /// Họ và tên của khách hàng vay vốn.
        /// </summary>
        public string HoTen { get; set; }

        /// <summary>
        /// Tên tài khoản đăng nhập của người vay trong hệ thống.
        /// </summary>
        public string TenDangNhap { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng nhận tiền giải ngân khi hồ sơ được phê duyệt.
        /// </summary>
        public string SoTaiKhoan { get; set; }

        /// <summary>
        /// Tên ngân hàng liên kết mở tài khoản.
        /// </summary>
        public string TenNganHang { get; set; }

        /// <summary>
        /// Số tiền gốc khách hàng đăng ký vay (Đơn vị: VNĐ).
        /// </summary>
        public decimal SoTienVay { get; set; }

        /// <summary>
        /// Thời hạn vay vốn tính theo đơn vị Ngày (Ví dụ: 30 ngày, 60 ngày, 90 ngày).
        /// </summary>
        public int ThoiGianVay { get; set; }

        /// <summary>
        /// Lãi suất cho vay theo ngày (%/ngày, ví dụ 0.05%/ngày tương đương 1.5%/tháng).
        /// </summary>
        public decimal LaiSuat { get; set; }

        /// <summary>
        /// Tổng số tiền lãi phải trả dự tính khi đến hạn tất toán.
        /// Công thức: Tiền Lãi = Số Tiền Vay * (Lãi Suất / 100) * Thời Gian Vay (ngày).
        /// </summary>
        public decimal TienLai { get; set; }

        /// <summary>
        /// Tổng số tiền nợ khách hàng cần thanh toán khi đáo hạn: Gốc + Lãi.
        /// </summary>
        public decimal TongPhaiTra { get; set; }

        /// <summary>
        /// Thời điểm khách hàng tạo đơn đăng ký vay vốn trên ứng dụng.
        /// </summary>
        public DateTime NgayDangKy { get; set; }

        /// <summary>
        /// Ngày đáo hạn hợp đồng vay vốn (Ngày đăng ký + Thời gian vay).
        /// </summary>
        public DateTime NgayDaoHan { get; set; }

        /// <summary>
        /// Trạng thái hồ sơ: "Chờ xét duyệt", "Đã duyệt", "Đã từ chối", "Đã tất toán".
        /// </summary>
        public string TrangThai { get; set; }

        /// <summary>
        /// Ghi chú mục đích vay hoặc lý do từ chối giải ngân từ Ban quản trị.
        /// </summary>
        public string GhiChu { get; set; }

        /// <summary>
        /// Thời điểm quản trị viên (Admin) phê duyệt hoặc từ chối hồ sơ (null nếu chưa duyệt).
        /// </summary>
        public DateTime? NgayDuyet { get; set; }

        /// <summary>
        /// Tên đăng nhập của quản trị viên đã thực hiện xét duyệt hồ sơ này.
        /// </summary>
        public string NguoiDuyet { get; set; }

        // ============================================================
        //  CÁC THUỘC TÍNH HỖ TRỢ HIỂN THỊ ĐỊNH DẠNG (FORMATTED HELPERS)
        // ============================================================

        /// <summary>Chuỗi định dạng số tiền vay kèm đơn vị VNĐ (Ví dụ: 10,000,000 đ).</summary>
        public string FormattedSoTienVay => $"{SoTienVay:N0} đ";

        /// <summary>Chuỗi định dạng tổng nợ phải trả kèm đơn vị VNĐ (Ví dụ: 10,450,000 đ).</summary>
        public string FormattedTongPhaiTra => $"{TongPhaiTra:N0} đ";

        /// <summary>Chuỗi định dạng tiền lãi vay kèm đơn vị VNĐ (Ví dụ: 450,000 đ).</summary>
        public string FormattedTienLai => $"{TienLai:N0} đ";

        /// <summary>Chuỗi định dạng lãi suất theo ngày và quy đổi tháng (Ví dụ: 0.05%/ngày (1.5%/tháng)).</summary>
        public string FormattedLaiSuat => $"{LaiSuat:N2}%/ngày ({(LaiSuat * 30):N1}%/tháng)";

        /// <summary>Chuỗi định dạng ngày đăng ký theo mẫu dd/MM/yyyy HH:mm.</summary>
        public string FormattedNgayDangKy => NgayDangKy.ToString("dd/MM/yyyy HH:mm");

        /// <summary>Chuỗi định dạng ngày đáo hạn theo mẫu dd/MM/yyyy.</summary>
        public string FormattedNgayDaoHan => NgayDaoHan.ToString("dd/MM/yyyy");

        /// <summary>Bí danh cho FormattedSoTienVay tương thích các binding giao diện cũ.</summary>
        public string FormattedLoanAmount => FormattedSoTienVay;

        /// <summary>Bí danh cho FormattedTongPhaiTra tương thích các binding giao diện cũ.</summary>
        public string FormattedTotalPay => FormattedTongPhaiTra;

        /// <summary>Bí danh cho FormattedTienLai tương thích các binding giao diện cũ.</summary>
        public string FormattedInterest => FormattedTienLai;

        /// <summary>Chuỗi hiển thị thời hạn vay (Ví dụ: "30 ngày").</summary>
        public string FormattedTerm => $"{ThoiGianVay} ngày";

        /// <summary>Chuỗi ngày đăng ký theo định dạng ngắn dd/MM/yyyy.</summary>
        public string FormattedRegisterDate => NgayDangKy.ToString("dd/MM/yyyy");

        /// <summary>Chuỗi hiển thị tên và mã khách hàng (Ví dụ: "Nguyễn Văn A (KH001)").</summary>
        public string FormattedCustomer => string.IsNullOrWhiteSpace(HoTen) ? MaKH : $"{HoTen} ({MaKH})";

        /// <summary>Chuỗi ngày phê duyệt hoặc dấu gạch nối "-" nếu chưa được duyệt.</summary>
        public string FormattedNgayDuyet => NgayDuyet.HasValue ? NgayDuyet.Value.ToString("dd/MM/yyyy") : "-";

        /// <summary>
        /// Tình trạng hạn khoản vay: "Đã tất toán", "Quá hạn X ngày", "Đến hạn hôm nay", hoặc "Còn X ngày".
        /// </summary>
        public string TinhTrangHan
        {
            get
            {
                // BƯỚC 1: Nếu khoản vay đã thanh toán xong, trả về "Đã tất toán"
                if (TrangThai == "Đã tất toán") return "Đã tất toán";

                // BƯỚC 2: Nếu chưa được phê duyệt, chưa bắt đầu tính ngày đáo hạn
                if (TrangThai != "Đã duyệt") return "-";

                // BƯỚC 3: Tính khoảng cách số ngày giữa Ngày đáo hạn và Ngày hôm nay
                var diff = (NgayDaoHan.Date - DateTime.Today).Days;
                if (diff < 0) return $"Quá hạn {Math.Abs(diff)} ngày";
                if (diff == 0) return "Đến hạn hôm nay";
                return $"Còn {diff} ngày";
            }
        }
    }

    /// <summary>
    /// Đối tượng chứa kết quả tính toán lãi suất và số nợ phải trả theo thời gian thực (Real-time Calculator).
    /// </summary>
    public class LoanCalculationResult
    {
        /// <summary>Số tiền gốc dự kiến vay (VNĐ).</summary>
        public decimal SoTienVay { get; set; }

        /// <summary>Thời gian vay (tính theo ngày).</summary>
        public int ThoiGianVay { get; set; }

        /// <summary>Lãi suất áp dụng theo ngày (%/ngày).</summary>
        public decimal LaiSuatNgay { get; set; }

        /// <summary>Tiền lãi ước tính phải trả khi đến hạn tất toán.</summary>
        public decimal TienLai { get; set; }

        /// <summary>Tổng số tiền phải trả (Gốc + Lãi).</summary>
        public decimal TongPhaiTra { get; set; }

        /// <summary>Thời điểm bắt đầu đăng ký vay.</summary>
        public DateTime NgayDangKy { get; set; }

        /// <summary>Thời điểm dự kiến đáo hạn khoản vay.</summary>
        public DateTime NgayDaoHan { get; set; }

        /// <summary>Chuỗi định dạng số tiền vay.</summary>
        public string FormattedSoTienVay => $"{SoTienVay:N0} đ";

        /// <summary>Chuỗi định dạng tiền lãi.</summary>
        public string FormattedTienLai => $"{TienLai:N0} đ";

        /// <summary>Chuỗi định dạng tổng nợ phải trả.</summary>
        public string FormattedTongPhaiTra => $"{TongPhaiTra:N0} đ";

        /// <summary>Chuỗi định dạng lãi suất ngày.</summary>
        public string FormattedLaiSuat => $"{LaiSuatNgay:N2}%/ngày";

        /// <summary>Chuỗi định dạng ngày đáo hạn.</summary>
        public string FormattedNgayDaoHan => NgayDaoHan.ToString("dd/MM/yyyy");
    }

    /// <summary>
    /// Đối tượng thống kê tổng quan các chỉ số hồ sơ vay vốn trong toàn hệ thống phục vụ Ban Quản Trị (Admin Dashboard).
    /// </summary>
    public class BorrowingStatistics
    {
        /// <summary>Tổng số lượng hồ sơ vay vốn đã ghi nhận trong hệ thống.</summary>
        public int TongHoSo { get; set; }

        /// <summary>Số lượng hồ sơ đang ở trạng thái 'Chờ xét duyệt'.</summary>
        public int ChoDuyet { get; set; }

        /// <summary>Số lượng hồ sơ đã được phê duyệt và giải ngân.</summary>
        public int DaDuyet { get; set; }

        /// <summary>Số lượng hồ sơ đã bị từ chối giải ngân.</summary>
        public int TuChoi { get; set; }

        /// <summary>Số lượng hồ sơ vay đã được người dùng hoàn tất thanh toán (Đã tất toán).</summary>
        public int DaTatToan { get; set; }

        /// <summary>Tổng số tiền gốc ngân hàng đã giải ngân cho các hồ sơ hợp lệ (VNĐ).</summary>
        public decimal TongTienDaGiaiNgan { get; set; }

        /// <summary>Tổng dư nợ còn lại cần thu hồi (gồm gốc + lãi của các hồ sơ Đã duyệt chưa tất toán).</summary>
        public decimal TongDuNoCanThuHoi { get; set; }
    }
}
