using System;

namespace BankAccountManagement.Models
{
    /// <summary>
    /// Thực thể đại diện cho một Sổ / Tài khoản tiết kiệm trực tuyến trong hệ thống ngân hàng (bảng dbo.SoTietKiem).
    /// Quản lý thông tin gửi tiết kiệm có kỳ hạn, tính toán tiền lãi dự tính và trạng thái đáo hạn.
    /// </summary>
    public class SoTietKiem
    {
        /// <summary>Mã số sổ tiết kiệm định danh duy nhất (Ví dụ: "TK-83102228").</summary>
        public string MaSoTKTK { get; set; }

        /// <summary>Mã khách hàng đứng tên chủ sổ tiết kiệm.</summary>
        public string MaKH { get; set; }

        /// <summary>Họ và tên của khách hàng gửi tiền.</summary>
        public string HoTen { get; set; }

        /// <summary>Tên tài khoản người dùng đăng ký gửi tiết kiệm.</summary>
        public string TenDangNhap { get; set; }

        /// <summary>Số tài khoản thanh toán nguồn bị trích nợ khi mở sổ.</summary>
        public string SoTaiKhoanTrich { get; set; }

        /// <summary>Số tiền gốc khách hàng gửi vào sổ tiết kiệm (VNĐ).</summary>
        public decimal SoTienGui { get; set; }

        /// <summary>Kỳ hạn gửi tiết kiệm tính theo số tháng (Ví dụ: 1, 3, 6, 12 tháng).</summary>
        public int KyHanThang { get; set; }

        /// <summary>Lãi suất huy động niêm yết theo năm (%/năm, ví dụ: 5.5%/năm).</summary>
        public decimal LaiSuatNam { get; set; }

        /// <summary>Số tiền lãi dự kiến nhận được khi đáo hạn theo đúng kỳ hạn đã chọn.</summary>
        public decimal TienLaiDuTinh { get; set; }

        /// <summary>Tổng số tiền gốc và lãi dự kiến nhận được khi đáo hạn (Gốc + Lãi dự tính).</summary>
        public decimal TongTienDaoHanDuTinh { get; set; }

        /// <summary>
        /// Phương thức xử lý khi sổ đến ngày đáo hạn: 
        /// "Tự động tái tục gốc", "Tái tục gốc &amp; lãi (Lãi gộp)", hoặc "Tất toán về tài khoản".
        /// </summary>
        public string HinhThucDaoHan { get; set; }

        /// <summary>Thời điểm mở sổ tiết kiệm.</summary>
        public DateTime NgayMo { get; set; }

        /// <summary>Thời điểm đáo hạn sổ tiết kiệm.</summary>
        public DateTime NgayDaoHan { get; set; }

        /// <summary>Trạng thái hiện tại của sổ tiết kiệm: "Đang gửi" hoặc "Đã tất toán".</summary>
        public string TrangThai { get; set; }

        /// <summary>Thời điểm thực tế khách hàng thực hiện tất toán sổ (null nếu vẫn đang gửi).</summary>
        public DateTime? NgayTatToan { get; set; }

        /// <summary>Số tiền lãi thực tế khách hàng được chi trả khi tất toán (có thể tính theo lãi không kỳ hạn nếu rút trước hạn).</summary>
        public decimal? TienLaiThucNhan { get; set; }

        /// <summary>Tổng số tiền thực tế nhận về tài khoản khi tất toán (Gốc + Lãi thực nhận).</summary>
        public decimal? TongTienThucNhan { get; set; }

        /// <summary>Ghi chú bổ sung của khách hàng hoặc nhân viên ngân hàng.</summary>
        public string GhiChu { get; set; }

        // ============================================================
        //  CÁC THUỘC TÍNH HỖ TRỢ ĐỊNH DẠNG HIỂN THỊ (HELPERS UI)
        // ============================================================

        /// <summary>Chuỗi định dạng số tiền gốc gửi kèm đơn vị VNĐ.</summary>
        public string FormattedSoTienGui => $"{SoTienGui:N0} đ";

        /// <summary>Chuỗi định dạng tiền lãi dự tính kèm đơn vị VNĐ.</summary>
        public string FormattedTienLaiDuTinh => $"{TienLaiDuTinh:N0} đ";

        /// <summary>Chuỗi định dạng tổng tiền đáo hạn dự tính kèm đơn vị VNĐ.</summary>
        public string FormattedTongTienDaoHan => $"{TongTienDaoHanDuTinh:N0} đ";

        /// <summary>Chuỗi định dạng tỷ lệ lãi suất theo năm (Ví dụ: "5.50% / năm").</summary>
        public string FormattedLaiSuat => $"{LaiSuatNam:N2}% / năm";

        /// <summary>Chuỗi định dạng kỳ hạn gửi (Ví dụ: "6 tháng").</summary>
        public string FormattedKyHan => $"{KyHanThang} tháng";

        /// <summary>Chuỗi định dạng ngày mở sổ (dd/MM/yyyy).</summary>
        public string FormattedNgayMo => NgayMo.ToString("dd/MM/yyyy");

        /// <summary>Chuỗi định dạng ngày đáo hạn (dd/MM/yyyy).</summary>
        public string FormattedNgayDaoHan => NgayDaoHan.ToString("dd/MM/yyyy");

        /// <summary>Chuỗi định dạng ngày tất toán thực tế hoặc dấu gạch ngang "-" nếu chưa tất toán.</summary>
        public string FormattedNgayTatToan => NgayTatToan.HasValue ? NgayTatToan.Value.ToString("dd/MM/yyyy HH:mm") : "-";

        /// <summary>Chuỗi định dạng hiển thị họ tên kèm mã khách hàng.</summary>
        public string FormattedCustomer => string.IsNullOrWhiteSpace(HoTen) ? MaKH : $"{HoTen} ({MaKH})";

        /// <summary>
        /// Chuỗi hiển thị số tài khoản trích nợ được rút gọn bảo mật dạng "1234567...".
        /// </summary>
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

        /// <summary>
        /// Tình trạng đáo hạn của sổ: "Đã tất toán", "Quá hạn X ngày", "Đến hạn hôm nay", hoặc "Còn X ngày".
        /// </summary>
        public string TinhTrangDaoHan
        {
            get
            {
                // BƯỚC 1: Nếu sổ đã được tất toán rút tiền thì trả về trạng thái hoàn thành
                if (TrangThai == "Đã tất toán") return "Đã tất toán";

                // BƯỚC 2: Tính số ngày còn lại đến thời điểm đáo hạn
                var diff = (NgayDaoHan.Date - DateTime.Today).Days;
                if (diff < 0) return $"Quá hạn {Math.Abs(diff)} ngày";
                if (diff == 0) return "Đến hạn hôm nay";
                return $"Còn {diff} ngày";
            }
        }
    }

    /// <summary>
    /// Đối tượng thể hiện gói kỳ hạn gửi tiết kiệm và lãi suất niêm yết tương ứng.
    /// Thường được nạp vào ComboBox để người dùng lựa chọn gói gửi mong muốn.
    /// </summary>
    public class SavingsPackage
    {
        /// <summary>Kỳ hạn gửi tính theo số tháng.</summary>
        public int KyHanThang { get; set; }

        /// <summary>Mức lãi suất niêm yết tính theo năm (%/năm).</summary>
        public decimal LaiSuatNam { get; set; }

        /// <summary>Tên hiển thị trực quan của gói kỳ hạn (Ví dụ: "6 tháng (5.50%/năm)").</summary>
        public string TenGoi => $"{KyHanThang} tháng ({LaiSuatNam:N2}%/năm)";

        /// <summary>
        /// Ghi đè phương thức ToString để hiển thị trực tiếp lên ComboBox trong Windows Forms.
        /// </summary>
        /// <returns>Tên hiển thị của gói tiết kiệm.</returns>
        public override string ToString()
        {
            return TenGoi;
        }
    }

    /// <summary>
    /// Kết quả tính toán lãi suất và số tiền đáo hạn dự tính thời gian thực khi khách hàng nhập số tiền gửi và chọn kỳ hạn.
    /// </summary>
    public class SavingsCalculationResult
    {
        /// <summary>Số tiền gốc dự kiến gửi tiết kiệm (VNĐ).</summary>
        public decimal SoTienGui { get; set; }

        /// <summary>Kỳ hạn gửi (tháng).</summary>
        public int KyHanThang { get; set; }

        /// <summary>Lãi suất áp dụng theo năm (%/năm).</summary>
        public decimal LaiSuatNam { get; set; }

        /// <summary>Ngày bắt đầu mở sổ tiết kiệm.</summary>
        public DateTime NgayMo { get; set; }

        /// <summary>Ngày đáo hạn sổ tiết kiệm.</summary>
        public DateTime NgayDaoHan { get; set; }

        /// <summary>Tổng số ngày thực tế trong kỳ hạn gửi (tính chính xác theo lịch từng tháng).</summary>
        public int SoNgay { get; set; }

        /// <summary>
        /// Tiền lãi dự tính khi đến hạn tất toán.
        /// Công thức chuẩn ngân hàng: Tiền Lãi = Số Tiền Gửi * (Lãi Suất / 100) * (Số Ngày / 365).
        /// </summary>
        public decimal TienLaiDuTinh { get; set; }

        /// <summary>Tổng tiền dự kiến thu về khi đáo hạn (Gốc + Tiền lãi dự tính).</summary>
        public decimal TongTienDaoHanDuTinh { get; set; }

        /// <summary>Chuỗi định dạng tiền gốc gửi.</summary>
        public string FormattedSoTienGui => $"{SoTienGui:N0} đ";

        /// <summary>Chuỗi định dạng tiền lãi dự tính.</summary>
        public string FormattedTienLai => $"{TienLaiDuTinh:N0} đ";

        /// <summary>Chuỗi định dạng tổng tiền đáo hạn.</summary>
        public string FormattedTongTien => $"{TongTienDaoHanDuTinh:N0} đ";

        /// <summary>Chuỗi định dạng lãi suất theo năm.</summary>
        public string FormattedLaiSuat => $"{LaiSuatNam:N2}% / năm";

        /// <summary>Chuỗi định dạng ngày đáo hạn.</summary>
        public string FormattedNgayDaoHan => NgayDaoHan.ToString("dd/MM/yyyy");
    }

    /// <summary>
    /// Đối tượng tổng hợp hồ sơ gửi tiết kiệm của một khách hàng phục vụ tra cứu thông tin chi tiết bên Admin.
    /// </summary>
    public class CustomerSavingsProfile
    {
        /// <summary>Mã khách hàng.</summary>
        public string MaKH { get; set; }

        /// <summary>Họ và tên khách hàng.</summary>
        public string HoTen { get; set; }

        /// <summary>Tên tài khoản đăng nhập hệ thống.</summary>
        public string TenDangNhap { get; set; }

        /// <summary>Số Căn cước công dân / CMND của khách hàng.</summary>
        public string CMND_CCCD { get; set; }

        /// <summary>Số điện thoại liên lạc.</summary>
        public string SoDienThoai { get; set; }

        /// <summary>Địa chỉ thư điện tử Email.</summary>
        public string Email { get; set; }

        /// <summary>Địa chỉ cư trú thường trú của khách hàng.</summary>
        public string DiaChi { get; set; }

        /// <summary>Tổng số dư khả dụng trên toàn bộ tài khoản thanh toán của khách hàng.</summary>
        public decimal TongSoDuKhaDung { get; set; }

        /// <summary>Tổng số lượng sổ tiết kiệm khách hàng từng mở.</summary>
        public int TongSoTKTK { get; set; }

        /// <summary>Số lượng sổ tiết kiệm đang có hiệu lực (Đang gửi).</summary>
        public int SoDangGui { get; set; }

        /// <summary>Số lượng sổ tiết kiệm đã hoàn tất tất toán.</summary>
        public int SoDaTatToan { get; set; }

        /// <summary>Tổng số tiền gốc khách hàng đang gửi trong các sổ có hiệu lực.</summary>
        public decimal TongTienDangGui { get; set; }

        /// <summary>Tổng số tiền lãi dự tính sẽ nhận khi các sổ tiết kiệm đáo hạn.</summary>
        public decimal TongTienLaiDuTinh { get; set; }

        /// <summary>Chuỗi định dạng tổng tiền tiết kiệm đang gửi.</summary>
        public string FormattedTongTienDangGui => $"{TongTienDangGui:N0} đ";

        /// <summary>Chuỗi định dạng tổng số dư tài khoản thanh toán khả dụng.</summary>
        public string FormattedTongSoDu => $"{TongSoDuKhaDung:N0} đ";
    }

    /// <summary>
    /// Đối tượng tổng hợp các chỉ số huy động vốn tiết kiệm trên toàn ngân hàng dành cho Quản trị viên (Admin Overview).
    /// </summary>
    public class SavingsAdminOverview
    {
        /// <summary>Tổng số lượng sổ tiết kiệm đã tạo từ trước tới nay.</summary>
        public int TongSoSo { get; set; }

        /// <summary>Số lượng sổ tiết kiệm hiện đang hoạt động gửi tiền.</summary>
        public int SoDangGui { get; set; }

        /// <summary>Số lượng sổ tiết kiệm đã hoàn thành tất toán.</summary>
        public int SoDaTatToan { get; set; }

        /// <summary>Tổng số tiền gốc ngân hàng đang huy động từ toàn bộ các sổ đang gửi (VNĐ).</summary>
        public decimal TongTienDangHuyDong { get; set; }

        /// <summary>Tổng số tiền gốc đã được tất toán hoàn trả cho khách hàng (VNĐ).</summary>
        public decimal TongTienDaTatToan { get; set; }

        /// <summary>Tổng số tiền lãi ngân hàng dự kiến phải chi trả khi các sổ đáo hạn (VNĐ).</summary>
        public decimal TongTienLaiDuTinh { get; set; }

        /// <summary>Chuỗi định dạng tổng vốn đang huy động.</summary>
        public string FormattedDangHuyDong => $"{TongTienDangHuyDong:N0} đ";

        /// <summary>Chuỗi định dạng tổng tiền đã tất toán.</summary>
        public string FormattedDaTatToan => $"{TongTienDaTatToan:N0} đ";

        /// <summary>Chuỗi định dạng tổng lãi dự tính phải chi trả.</summary>
        public string FormattedLaiDuTinh => $"{TongTienLaiDuTinh:N0} đ";
    }
}
