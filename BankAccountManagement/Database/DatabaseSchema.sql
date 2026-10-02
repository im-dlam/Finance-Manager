-- ============================================
-- BankAccountManagement - Database Schema
-- ============================================

-- 1. Bảng Khách hàng
CREATE TABLE KhachHang (
    MaKH VARCHAR(20) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    CMND_CCCD VARCHAR(20) UNIQUE NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    SoDienThoai VARCHAR(15),
    Email VARCHAR(100),
    DiaChi NVARCHAR(255),
    TrangThai NVARCHAR(50) DEFAULT 'Active',
    NgayTao DATETIME DEFAULT GETDATE()
);

-- 2. Bảng Ngân hàng
CREATE TABLE NganHang (
    MaNganHang VARCHAR(20) PRIMARY KEY,
    TenNganHang NVARCHAR(100) NOT NULL,
    ChiNhanh NVARCHAR(100),
    SwiftCode VARCHAR(20)
);

-- 3. Bảng Loại tiền
CREATE TABLE LoaiTien (
    MaLoaiTien VARCHAR(10) PRIMARY KEY,
    TenLoaiTien NVARCHAR(50) NOT NULL
);

-- 4. Bảng Tài khoản
CREATE TABLE TaiKhoan (
    SoTaiKhoan VARCHAR(30) PRIMARY KEY,
    MaKH VARCHAR(20),
    MaNganHang VARCHAR(20),
    MaLoaiTien VARCHAR(10),
    SoDuBanDau DECIMAL(18, 2) DEFAULT 0,
    SoDuHienTai DECIMAL(18, 2) DEFAULT 0,
    NgayMo DATETIME DEFAULT GETDATE(),
    TrangThai NVARCHAR(50) DEFAULT 'Active',

    FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    FOREIGN KEY (MaNganHang) REFERENCES NganHang(MaNganHang),
    FOREIGN KEY (MaLoaiTien) REFERENCES LoaiTien(MaLoaiTien)
);

-- 5. Bảng Loại giao dịch
CREATE TABLE LoaiGiaoDich (
    MaLoaiGD INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiGD NVARCHAR(100) NOT NULL,
    PhanLoai VARCHAR(10) NOT NULL,

    CONSTRAINT CK_LoaiGiaoDich_PhanLoai
        CHECK (PhanLoai IN ('INCOME', 'EXPENSE'))
);

-- 6. Bảng Giao dịch
CREATE TABLE GiaoDich (
    MaGiaoDich VARCHAR(30) PRIMARY KEY,
    SoTaiKhoan VARCHAR(30),
    MaLoaiGD INT,
    SoTien DECIMAL(18, 2) NOT NULL,
    NgayGiaoDich DATETIME DEFAULT GETDATE(),
    NoiDung NVARCHAR(255),
    TrangThai NVARCHAR(50) DEFAULT 'Success',

    FOREIGN KEY (SoTaiKhoan) REFERENCES TaiKhoan(SoTaiKhoan),
    FOREIGN KEY (MaLoaiGD) REFERENCES LoaiGiaoDich(MaLoaiGD)
);

-- 7. Bảng Người dùng
CREATE TABLE NguoiDung (
    MaNguoiDung INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap VARCHAR(50) UNIQUE NOT NULL,
    MatKhau VARCHAR(255) NOT NULL,
    Quyen NVARCHAR(50) DEFAULT 'Staff'
);

-- ============================================
-- Index
-- ============================================
CREATE INDEX idx_khachhang_sdt       ON KhachHang(SoDienThoai);
CREATE INDEX idx_khachhang_email     ON KhachHang(Email);
CREATE INDEX idx_taikhoan_trangthai  ON TaiKhoan(TrangThai);
CREATE INDEX idx_giaodich_ngay       ON GiaoDich(NgayGiaoDich);
CREATE INDEX idx_giaodich_trangthai  ON GiaoDich(TrangThai);
CREATE INDEX idx_taikhoan_makh       ON TaiKhoan(MaKH);
CREATE INDEX idx_giaodich_sotaikhoan ON GiaoDich(SoTaiKhoan);

-- ============================================
-- Dữ liệu mẫu
-- ============================================

-- Loại tiền
INSERT INTO LoaiTien (MaLoaiTien, TenLoaiTien) VALUES
('VND', N'Việt Nam Đồng'),
('USD', N'Đô la Mỹ'),
('EUR', N'Euro');

-- Ngân hàng
INSERT INTO NganHang (MaNganHang, TenNganHang, ChiNhanh, SwiftCode) VALUES
('VCB',  N'Vietcombank',          N'Hà Nội',  'BFTVVNVX'),
('TCB',  N'Techcombank',          N'TP.HCM',  'VTCBVNVX'),
('BIDV', N'BIDV',                 N'Đà Nẵng', 'BIDVVNVX'),
('MB',   N'MB Bank',              N'Hà Nội',  'MSCBVNVX'),
('ACB',  N'Á Châu Bank',          N'TP.HCM',  'ASCBVNVX');

-- Loại giao dịch
INSERT INTO LoaiGiaoDich (TenLoaiGD, PhanLoai) VALUES
(N'Gửi tiền',       'INCOME'),
(N'Rút tiền',       'EXPENSE'),
(N'Chuyển khoản',   'EXPENSE'),
(N'Nhận chuyển khoản', 'INCOME'),
(N'Thanh toán',     'EXPENSE');

-- Khách hàng mẫu
INSERT INTO KhachHang (MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi) VALUES
('KH001', N'Nguyễn Văn An',    '001234567890', '1990-05-15', N'Nam', '0901234567', 'an@gmail.com',    N'Hà Nội'),
('KH002', N'Trần Thị Bình',    '001234567891', '1995-08-20', N'Nữ',  '0912345678', 'binh@gmail.com',  N'TP.HCM'),
('KH003', N'Lê Văn Cường',     '001234567892', '1988-03-10', N'Nam', '0923456789', 'cuong@gmail.com', N'Đà Nẵng'),
('KH004', N'Phạm Thị Dung',    '001234567893', '1992-11-25', N'Nữ',  '0934567890', 'dung@gmail.com',  N'Hải Phòng'),
('KH005', N'Hoàng Văn Em',     '001234567894', '1985-07-08', N'Nam', '0945678901', 'em@gmail.com',    N'Cần Thơ');

-- Tài khoản mẫu
INSERT INTO TaiKhoan (SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien, SoDuBanDau, SoDuHienTai) VALUES
('TK00001', 'KH001', 'VCB',  'VND', 5000000,   5000000),
('TK00002', 'KH002', 'TCB',  'VND', 10000000,  10000000),
('TK00003', 'KH003', 'BIDV', 'USD', 500,        500),
('TK00004', 'KH004', 'MB',   'VND', 3000000,   3000000),
('TK00005', 'KH005', 'ACB',  'EUR', 200,        200);

-- Người dùng (mật khẩu: admin123)
INSERT INTO NguoiDung (TenDangNhap, MatKhau, Quyen) VALUES
('admin', 'admin123', 'Admin'),
('staff', 'staff123', 'Staff');
