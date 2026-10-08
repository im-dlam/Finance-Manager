-- ============================================================
--  BANK MANAGEMENT – Database Schema
--  Idempotent: chạy được nhiều lần không lỗi
--  Database mặc định: BankM36
-- ============================================================

PRINT '>>> Running BankM36 Schema <<<';

-- ============================================================
--  1. BẢNG KHÁCH HÀNG
-- ============================================================
IF OBJECT_ID('KhachHang', 'U') IS NULL
BEGIN
    CREATE TABLE KhachHang (
        MaKH         VARCHAR(20)   PRIMARY KEY,
        HoTen        NVARCHAR(100) NOT NULL,
        CMND_CCCD    VARCHAR(20)   UNIQUE NOT NULL,
        NgaySinh     DATE          NULL,
        GioiTinh     NVARCHAR(10)  NULL,
        SoDienThoai  VARCHAR(15)   NULL,
        Email        VARCHAR(100)  NULL,
        DiaChi       NVARCHAR(255) NULL,
        TrangThai    NVARCHAR(50)  NOT NULL DEFAULT N'Active',
        NgayTao      DATETIME      NOT NULL DEFAULT GETDATE()
    );
END

-- ============================================================
--  2. BẢNG NGÂN HÀNG
-- ============================================================
IF OBJECT_ID('NganHang', 'U') IS NULL
BEGIN
    CREATE TABLE NganHang (
        MaNganHang   VARCHAR(20)   PRIMARY KEY,
        TenNganHang  NVARCHAR(100) NOT NULL,
        ChiNhanh     NVARCHAR(100) NULL,
        SwiftCode    VARCHAR(20)   NULL
    );
END

-- ============================================================
--  3. BẢNG LOẠI TIỀN
-- ============================================================
IF OBJECT_ID('LoaiTien', 'U') IS NULL
BEGIN
    CREATE TABLE LoaiTien (
        MaLoaiTien   VARCHAR(10)   PRIMARY KEY,
        TenLoaiTien  NVARCHAR(50)  NOT NULL,
        QuyDoi       DECIMAL(18,6) NOT NULL DEFAULT 1
    );
END

-- ============================================================
--  4. BẢNG TÀI KHOẢN
-- ============================================================
IF OBJECT_ID('TaiKhoan', 'U') IS NULL
BEGIN
    CREATE TABLE TaiKhoan (
        SoTaiKhoan     VARCHAR(30)   PRIMARY KEY,
        MaKH           VARCHAR(20)   NOT NULL,
        MaNganHang     VARCHAR(20)   NOT NULL,
        MaLoaiTien     VARCHAR(10)   NOT NULL,
        SoDuBanDau     DECIMAL(18,2) NOT NULL DEFAULT 0,
        SoDuHienTai    DECIMAL(18,2) NOT NULL DEFAULT 0,
        NgayMo         DATETIME      NOT NULL DEFAULT GETDATE(),
        TrangThai      NVARCHAR(50)  NOT NULL DEFAULT N'Active',

        CONSTRAINT FK_TaiKhoan_KhachHang FOREIGN KEY (MaKH)       REFERENCES KhachHang(MaKH),
        CONSTRAINT FK_TaiKhoan_NganHang  FOREIGN KEY (MaNganHang) REFERENCES NganHang(MaNganHang),
        CONSTRAINT FK_TaiKhoan_LoaiTien  FOREIGN KEY (MaLoaiTien) REFERENCES LoaiTien(MaLoaiTien),
        CONSTRAINT CK_TaiKhoan_SoDu      CHECK (SoDuHienTai >= 0)
    );
END

-- ============================================================
--  5. BẢNG LOẠI GIAO DỊCH
-- ============================================================
IF OBJECT_ID('LoaiGiaoDich', 'U') IS NULL
BEGIN
    CREATE TABLE LoaiGiaoDich (
        MaLoaiGD   INT IDENTITY(1,1) PRIMARY KEY,
        TenLoaiGD  NVARCHAR(100) NOT NULL,
        PhanLoai   VARCHAR(10)   NOT NULL,

        CONSTRAINT CK_LoaiGiaoDich_PhanLoai CHECK (PhanLoai IN ('INCOME', 'EXPENSE'))
    );
END

-- ============================================================
--  6. BẢNG GIAO DỊCH
-- ============================================================
IF OBJECT_ID('GiaoDich', 'U') IS NULL
BEGIN
    CREATE TABLE GiaoDich (
        MaGiaoDich    VARCHAR(30)   PRIMARY KEY,
        SoTaiKhoan    VARCHAR(30)   NOT NULL,
        MaLoaiGD      INT           NOT NULL,
        SoTien        DECIMAL(18,2) NOT NULL,
        SoTienGoc     DECIMAL(18,2) NULL,
        LoaiTien      VARCHAR(10)   NOT NULL DEFAULT 'VND',
        NgayGiaoDich  DATETIME      NOT NULL DEFAULT GETDATE(),
        NoiDung       NVARCHAR(255) NULL,
        TrangThai     NVARCHAR(50)  NOT NULL DEFAULT N'Success',

        CONSTRAINT FK_GiaoDich_TaiKhoan FOREIGN KEY (SoTaiKhoan) REFERENCES TaiKhoan(SoTaiKhoan),
        CONSTRAINT FK_GiaoDich_LoaiGD   FOREIGN KEY (MaLoaiGD)   REFERENCES LoaiGiaoDich(MaLoaiGD),
        CONSTRAINT CK_GiaoDich_SoTien   CHECK (SoTien > 0)
    );
END

-- ============================================================
--  7. BẢNG NGƯỜI DÙNG
-- ============================================================
IF OBJECT_ID('NguoiDung', 'U') IS NULL
BEGIN
    CREATE TABLE NguoiDung (
        MaNguoiDung   INT IDENTITY(1,1) PRIMARY KEY,
        TenDangNhap   VARCHAR(50)   UNIQUE NOT NULL,
        MatKhau       VARCHAR(255)  NOT NULL,
        Quyen         VARCHAR(20)   NOT NULL DEFAULT 'Member',
        MaKH          VARCHAR(20)   NULL,
        HoTen         NVARCHAR(100) NULL,
        Email         VARCHAR(100)  NULL,
        TrangThai     NVARCHAR(50)  NOT NULL DEFAULT N'Active',
        NgayTao       DATETIME      NOT NULL DEFAULT GETDATE(),
        LanDangNhap   DATETIME      NULL,

        CONSTRAINT FK_NguoiDung_KhachHang FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
        CONSTRAINT CK_NguoiDung_Quyen     CHECK (Quyen IN ('Admin', 'Member')),
        CONSTRAINT CK_NguoiDung_TrangThai CHECK (TrangThai IN (N'Active', N'Locked'))
    );
END

-- ============================================================
--  8. BẢNG NHẬT KÝ HOẠT ĐỘNG
-- ============================================================
IF OBJECT_ID('NhatKyHoatDong', 'U') IS NULL
BEGIN
    CREATE TABLE NhatKyHoatDong (
        MaLog         BIGINT IDENTITY(1,1) PRIMARY KEY,
        MaNguoiDung   INT            NULL,
        HanhDong      NVARCHAR(100)  NOT NULL,
        DoiTuong      NVARCHAR(100)  NULL,
        DoiTuongID    VARCHAR(50)    NULL,
        ChiTiet       NVARCHAR(500)  NULL,
        DiaChiIP      VARCHAR(45)    NULL,
        ThoiGian      DATETIME       NOT NULL DEFAULT GETDATE(),

        CONSTRAINT FK_NhatKy_NguoiDung FOREIGN KEY (MaNguoiDung) REFERENCES NguoiDung(MaNguoiDung)
    );
END

-- ============================================================
--  9. INDEXES
-- ============================================================

-- KhachHang
IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = 'idx_khachhang_sdt'
                 AND object_id = OBJECT_ID('KhachHang'))
    CREATE INDEX idx_khachhang_sdt ON KhachHang(SoDienThoai);

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = 'idx_khachhang_email'
                 AND object_id = OBJECT_ID('KhachHang'))
    CREATE INDEX idx_khachhang_email ON KhachHang(Email);

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = 'idx_khachhang_cccd'
                 AND object_id = OBJECT_ID('KhachHang'))
    CREATE INDEX idx_khachhang_cccd ON KhachHang(CMND_CCCD);

-- NguoiDung
IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = 'idx_nguoidung_quyen'
                 AND object_id = OBJECT_ID('NguoiDung'))
    CREATE INDEX idx_nguoidung_quyen ON NguoiDung(Quyen);

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = 'idx_nguoidung_email'
                 AND object_id = OBJECT_ID('NguoiDung'))
    CREATE INDEX idx_nguoidung_email ON NguoiDung(Email);

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = 'idx_nguoidung_makh'
                 AND object_id = OBJECT_ID('NguoiDung'))
    CREATE INDEX idx_nguoidung_makh ON NguoiDung(MaKH);

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = 'idx_nguoidung_trangthai'
                 AND object_id = OBJECT_ID('NguoiDung'))
    CREATE INDEX idx_nguoidung_trangthai ON NguoiDung(TrangThai);

-- TaiKhoan
IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = 'idx_taikhoan_trangthai'
                 AND object_id = OBJECT_ID('TaiKhoan'))
    CREATE INDEX idx_taikhoan_trangthai ON TaiKhoan(TrangThai);

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = 'idx_taikhoan_makh'
                 AND object_id = OBJECT_ID('TaiKhoan'))
    CREATE INDEX idx_taikhoan_makh ON TaiKhoan(MaKH);

-- GiaoDich
IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = 'idx_giaodich_ngay'
                 AND object_id = OBJECT_ID('GiaoDich'))
    CREATE INDEX idx_giaodich_ngay ON GiaoDich(NgayGiaoDich);

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = 'idx_giaodich_trangthai'
                 AND object_id = OBJECT_ID('GiaoDich'))
    CREATE INDEX idx_giaodich_trangthai ON GiaoDich(TrangThai);

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = 'idx_giaodich_sotaikhoan'
                 AND object_id = OBJECT_ID('GiaoDich'))
    CREATE INDEX idx_giaodich_sotaikhoan ON GiaoDich(SoTaiKhoan);

-- NhatKyHoatDong
IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = 'idx_nhatky_nguoidung'
                 AND object_id = OBJECT_ID('NhatKyHoatDong'))
    CREATE INDEX idx_nhatky_nguoidung ON NhatKyHoatDong(MaNguoiDung);

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = 'idx_nhatky_thoigian'
                 AND object_id = OBJECT_ID('NhatKyHoatDong'))
    CREATE INDEX idx_nhatky_thoigian ON NhatKyHoatDong(ThoiGian);

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = 'idx_nhatky_hanhdong'
                 AND object_id = OBJECT_ID('NhatKyHoatDong'))
    CREATE INDEX idx_nhatky_hanhdong ON NhatKyHoatDong(HanhDong);
