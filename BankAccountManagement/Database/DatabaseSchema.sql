

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
    TenLoaiTien NVARCHAR(50) NOT NULL,
    QuyDoi DECIMAL(18, 6) NOT NULL DEFAULT 1
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
    SoTienGoc DECIMAL(18, 2),
    LoaiTien VARCHAR(10) DEFAULT 'VND',
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

-- Index
CREATE INDEX idx_khachhang_sdt       ON KhachHang(SoDienThoai);
CREATE INDEX idx_khachhang_email     ON KhachHang(Email);
CREATE INDEX idx_taikhoan_trangthai  ON TaiKhoan(TrangThai);
CREATE INDEX idx_giaodich_ngay       ON GiaoDich(NgayGiaoDich);
CREATE INDEX idx_giaodich_trangthai  ON GiaoDich(TrangThai);
CREATE INDEX idx_taikhoan_makh       ON TaiKhoan(MaKH);
CREATE INDEX idx_giaodich_sotaikhoan ON GiaoDich(SoTaiKhoan);
