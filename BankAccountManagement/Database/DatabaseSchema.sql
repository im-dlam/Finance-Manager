CREATE TABLE VaiTro (
    MaVaiTro    INT IDENTITY(1,1) PRIMARY KEY,
    TenVaiTro   VARCHAR(20)  UNIQUE NOT NULL,
    MoTa        NVARCHAR(255) NULL,
    NgayTao     DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT CK_VaiTro_Ten CHECK (TenVaiTro IN ('Admin', 'Member'))
);

-- ============================================================
--  2. BẢNG KHÁCH HÀNG
-- ============================================================
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

-- ============================================================
--  3. BẢNG NGÂN HÀNG
-- ============================================================
CREATE TABLE NganHang (
    MaNganHang   VARCHAR(20)   PRIMARY KEY,
    TenNganHang  NVARCHAR(100) NOT NULL,
    ChiNhanh     NVARCHAR(100) NULL,
    SwiftCode    VARCHAR(20)   NULL
);

-- ============================================================
--  4. BẢNG LOẠI TIỀN
-- ============================================================
CREATE TABLE LoaiTien (
    MaLoaiTien   VARCHAR(10)   PRIMARY KEY,
    TenLoaiTien  NVARCHAR(50)  NOT NULL,
    QuyDoi       DECIMAL(18,6) NOT NULL DEFAULT 1
);

-- ============================================================
--  5. BẢNG TÀI KHOẢN
-- ============================================================
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

-- ============================================================
--  6. BẢNG LOẠI GIAO DỊCH
-- ============================================================
CREATE TABLE LoaiGiaoDich (
    MaLoaiGD   INT IDENTITY(1,1) PRIMARY KEY,
    TenLoaiGD  NVARCHAR(100) NOT NULL,
    PhanLoai   VARCHAR(10)   NOT NULL,

    CONSTRAINT CK_LoaiGiaoDich_PhanLoai CHECK (PhanLoai IN ('INCOME', 'EXPENSE'))
);

-- ============================================================
--  7. BẢNG GIAO DỊCH
-- ============================================================
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

-- ============================================================
--  8. BẢNG NGƯỜI DÙNG
-- ============================================================
CREATE TABLE NguoiDung (
    MaNguoiDung   INT IDENTITY(1,1) PRIMARY KEY,
    TenDangNhap   VARCHAR(50)   UNIQUE NOT NULL,
    MatKhau       VARCHAR(255)  NOT NULL,
    MaVaiTro      INT           NOT NULL,
    MaKH          VARCHAR(20)   NULL,
    HoTen         NVARCHAR(100) NULL,
    Email         VARCHAR(100)  NULL,
    TrangThai     NVARCHAR(50)  NOT NULL DEFAULT N'Active',
    NgayTao       DATETIME      NOT NULL DEFAULT GETDATE(),
    LanDangNhap   DATETIME      NULL,

    CONSTRAINT FK_NguoiDung_VaiTro    FOREIGN KEY (MaVaiTro) REFERENCES VaiTro(MaVaiTro),
    CONSTRAINT FK_NguoiDung_KhachHang FOREIGN KEY (MaKH)     REFERENCES KhachHang(MaKH),
    CONSTRAINT CK_NguoiDung_TrangThai CHECK (TrangThai IN (N'Active', N'Locked'))
);

-- ============================================================
--  9. BẢNG NHẬT KÝ HOẠT ĐỘNG
-- ============================================================
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

-- ============================================================
--  10. INDEXES
-- ============================================================
CREATE INDEX idx_khachhang_sdt       ON KhachHang(SoDienThoai);
CREATE INDEX idx_khachhang_email     ON KhachHang(Email);
CREATE INDEX idx_khachhang_cccd      ON KhachHang(CMND_CCCD);

CREATE INDEX idx_nguoidung_vaitro    ON NguoiDung(MaVaiTro);
CREATE INDEX idx_nguoidung_email     ON NguoiDung(Email);
CREATE INDEX idx_nguoidung_makh      ON NguoiDung(MaKH);
CREATE INDEX idx_nguoidung_trangthai ON NguoiDung(TrangThai);

CREATE INDEX idx_taikhoan_trangthai  ON TaiKhoan(TrangThai);
CREATE INDEX idx_taikhoan_makh       ON TaiKhoan(MaKH);

CREATE INDEX idx_giaodich_ngay       ON GiaoDich(NgayGiaoDich);
CREATE INDEX idx_giaodich_trangthai  ON GiaoDich(TrangThai);
CREATE INDEX idx_giaodich_sotaikhoan ON GiaoDich(SoTaiKhoan);

CREATE INDEX idx_nhatky_nguoidung    ON NhatKyHoatDong(MaNguoiDung);
CREATE INDEX idx_nhatky_thoigian     ON NhatKyHoatDong(ThoiGian);
CREATE INDEX idx_nhatky_hanhdong     ON NhatKyHoatDong(HanhDong);