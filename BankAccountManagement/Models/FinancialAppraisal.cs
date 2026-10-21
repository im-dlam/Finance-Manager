using System;

namespace BankAccountManagement.Models
{
    /// <summary>
    /// Đối tượng chứa kết quả thẩm định năng lực tài chính của khách hàng trong 1 quý (90 ngày gần nhất).
    /// Hỗ trợ ban quản trị ngân hàng (Admin) đánh giá khả năng thanh toán trước khi phê duyệt hồ sơ vay vốn.
    /// </summary>
    public class FinancialAppraisal
    {
        /// <summary>
        /// Mã định danh khách hàng được thẩm định (Khóa ngoại tham chiếu ThongTinKhachHang).
        /// </summary>
        public string MaKH { get; set; }

        /// <summary>
        /// Họ và tên đầy đủ của khách hàng.
        /// </summary>
        public string HoTen { get; set; }

        /// <summary>
        /// Tên tài khoản đăng nhập của khách hàng trong hệ thống.
        /// </summary>
        public string TenDangNhap { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng chính được dùng để trích xuất dòng tiền và số dư.
        /// </summary>
        public string SoTaiKhoan { get; set; }

        /// <summary>
        /// Tên ngân hàng nơi khách hàng mở tài khoản chính.
        /// </summary>
        public string TenNganHang { get; set; }

        /// <summary>
        /// Số dư khả dụng hiện tại của tài khoản khách hàng (VNĐ).
        /// </summary>
        public decimal SoDuHienTai { get; set; }

        // ============================================================
        //  THỐNG KÊ DÒNG TIỀN QUÝ (90 NGÀY GẦN NHẤT)
        // ============================================================

        /// <summary>
        /// Tổng số tiền thu vào (nạp tiền, nhận chuyển khoản) trong 90 ngày qua (VNĐ).
        /// </summary>
        public decimal TongThuQuy { get; set; }

        /// <summary>
        /// Tổng số tiền chi ra (rút tiền, chuyển khoản đi, mua sắm) trong 90 ngày qua (VNĐ).
        /// </summary>
        public decimal TongChiQuy { get; set; }

        /// <summary>
        /// Chênh lệch dòng tiền ròng thu chi trong quý: Tổng Thu - Tổng Chi (VNĐ).
        /// </summary>
        public decimal ChenhLechThuChi => TongThuQuy - TongChiQuy;

        /// <summary>
        /// Tổng số lượng giao dịch tài chính phát sinh trong 90 ngày qua.
        /// </summary>
        public int SoGiaoDichQuy { get; set; }

        // ============================================================
        //  KẾT LUẬN VÀ GHI CHÚ ĐÁNH GIÁ CỦA HỆ THỐNG / ADMIN
        // ============================================================

        /// <summary>
        /// Kết luận xếp loại sơ bộ năng lực tín dụng (Ví dụ: "Tốt", "Khả quan", "Cần cân nhắc", "Rủi ro cao").
        /// </summary>
        public string DanhGia { get; set; }

        /// <summary>
        /// Ghi chú chi tiết phân tích lý do đưa ra đánh giá trên.
        /// </summary>
        public string GhiChuChiTiet { get; set; }

        // ============================================================
        //  CÁC THUỘC TÍNH HỖ TRỢ ĐỊNH DẠNG HIỂN THỊ (UI FORMATTERS)
        // ============================================================

        /// <summary>Chuỗi định dạng số dư hiện tại kèm đơn vị tiền tệ.</summary>
        public string FormattedSoDuHienTai => $"{SoDuHienTai:N0} đ";

        /// <summary>Chuỗi định dạng tổng thu trong quý kèm dấu cộng (+) thể hiện dòng tiền vào.</summary>
        public string FormattedTongThuQuy => $"+ {TongThuQuy:N0} đ";

        /// <summary>Chuỗi định dạng tổng chi trong quý kèm dấu trừ (-) thể hiện dòng tiền ra.</summary>
        public string FormattedTongChiQuy => $"- {TongChiQuy:N0} đ";

        /// <summary>Chuỗi định dạng dòng tiền ròng kèm dấu (+ / -) tương ứng.</summary>
        public string FormattedChenhLech => (ChenhLechThuChi >= 0 ? "+ " : "- ") + $"{Math.Abs(ChenhLechThuChi):N0} đ";

        /// <summary>Bí danh cho FormattedSoDuHienTai phục vụ binding giao diện.</summary>
        public string FormattedBalance => FormattedSoDuHienTai;

        /// <summary>Bí danh cho FormattedTongThuQuy phục vụ binding giao diện.</summary>
        public string FormattedIncome => FormattedTongThuQuy;

        /// <summary>Bí danh cho FormattedTongChiQuy phục vụ binding giao diện.</summary>
        public string FormattedExpense => FormattedTongChiQuy;

        /// <summary>Bí danh cho FormattedChenhLech phục vụ binding giao diện.</summary>
        public string FormattedNetCashFlow => FormattedChenhLech;
    }
}
