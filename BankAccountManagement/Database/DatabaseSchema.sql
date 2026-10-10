-- ============================================================
-- BANK MANAGEMENT - BankM36
-- Schema version: 2.0
-- Bảng khách hàng: ThongTinKhachHang
-- ============================================================

SET NOCOUNT ON;

PRINT N'>>> Initializing BankM36 Schema <<<';

-- ============================================================
-- 1. THÔNG TIN KHÁCH HÀNG
-- ============================================================

IF OBJECT_ID(N'dbo.ThongTinKhachHang', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.ThongTinKhachHang (
        MaKH         VARCHAR(20)   NOT NULL,
        HoTen        NVARCHAR(100) NOT NULL,
        CMND_CCCD    VARCHAR(20)   NOT NULL,
        NgaySinh     DATE          NULL,
        GioiTinh     NVARCHAR(20)  NULL,
        SoDienThoai  VARCHAR(15)   NULL,
        Email        VARCHAR(254)  NULL,
        DiaChi       NVARCHAR(255) NULL,
        TrangThai    NVARCHAR(20)  NOT NULL
            CONSTRAINT DF_ThongTinKhachHang_TrangThai DEFAULT N'Active',
        NgayTao      DATETIME2(0)  NOT NULL
            CONSTRAINT DF_ThongTinKhachHang_NgayTao DEFAULT SYSDATETIME(),

        CONSTRAINT PK_ThongTinKhachHang
            PRIMARY KEY (MaKH),

        CONSTRAINT UQ_ThongTinKhachHang_CMND
            UNIQUE (CMND_CCCD),

        CONSTRAINT CK_ThongTinKhachHang_TrangThai
            CHECK (TrangThai IN (N'Active', N'Locked'))
    );
END;

-- ============================================================
-- 2. NGƯỜI DÙNG
-- ============================================================

IF OBJECT_ID(N'dbo.NguoiDung', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.NguoiDung (
        MaNguoiDung   INT IDENTITY(1,1) NOT NULL,
        TenDangNhap   VARCHAR(50)   NOT NULL,
        MatKhau       VARCHAR(255)  NOT NULL,
        Quyen         VARCHAR(20)   NOT NULL
            CONSTRAINT DF_NguoiDung_Quyen DEFAULT 'Member',
        MaKH          VARCHAR(20)   NULL,
        HoTen         NVARCHAR(100) NULL,
        Email         VARCHAR(254)  NULL,
        TrangThai     NVARCHAR(20)  NOT NULL
            CONSTRAINT DF_NguoiDung_TrangThai DEFAULT N'Active',
        NgayTao       DATETIME2(0)  NOT NULL
            CONSTRAINT DF_NguoiDung_NgayTao DEFAULT SYSDATETIME(),
        LanDangNhap   DATETIME2(0)  NULL,

        CONSTRAINT PK_NguoiDung
            PRIMARY KEY (MaNguoiDung),

        CONSTRAINT UQ_NguoiDung_TenDangNhap
            UNIQUE (TenDangNhap),

        CONSTRAINT CK_NguoiDung_Quyen
            CHECK (Quyen IN ('Admin', 'Member')),

        CONSTRAINT CK_NguoiDung_TrangThai
            CHECK (TrangThai IN (N'Active', N'Locked')),

        CONSTRAINT FK_NguoiDung_ThongTinKhachHang
            FOREIGN KEY (MaKH)
            REFERENCES dbo.ThongTinKhachHang(MaKH)
    );
END;

-- ============================================================
-- 3. NGÂN HÀNG
-- ============================================================

IF OBJECT_ID(N'dbo.NganHang', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.NganHang (
        MaNganHang   VARCHAR(20)   NOT NULL,
        TenNganHang  NVARCHAR(100) NOT NULL,
        ChiNhanh     NVARCHAR(100) NULL,
        SwiftCode    VARCHAR(20)   NULL,

        CONSTRAINT PK_NganHang
            PRIMARY KEY (MaNganHang)
    );
END;

-- ============================================================
-- 4. LOẠI TIỀN
-- ============================================================

IF OBJECT_ID(N'dbo.LoaiTien', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.LoaiTien (
        MaLoaiTien   VARCHAR(10)   NOT NULL,
        TenLoaiTien  NVARCHAR(50)  NOT NULL,
        QuyDoi       DECIMAL(18,6) NOT NULL
            CONSTRAINT DF_LoaiTien_QuyDoi DEFAULT 1,

        CONSTRAINT PK_LoaiTien
            PRIMARY KEY (MaLoaiTien),

        CONSTRAINT CK_LoaiTien_QuyDoi
            CHECK (QuyDoi > 0)
    );
END;

-- ============================================================
-- 5. TÀI KHOẢN
-- ============================================================

IF OBJECT_ID(N'dbo.TaiKhoan', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.TaiKhoan (
        SoTaiKhoan   VARCHAR(30)   NOT NULL,
        MaKH         VARCHAR(20)   NOT NULL,
        MaNganHang   VARCHAR(20)   NOT NULL,
        MaLoaiTien   VARCHAR(10)   NOT NULL,
        SoDuBanDau   DECIMAL(18,2) NOT NULL
            CONSTRAINT DF_TaiKhoan_SoDuBanDau DEFAULT 0,
        SoDuHienTai  DECIMAL(18,2) NOT NULL
            CONSTRAINT DF_TaiKhoan_SoDuHienTai DEFAULT 0,
        NgayMo       DATETIME2(0)  NOT NULL
            CONSTRAINT DF_TaiKhoan_NgayMo DEFAULT SYSDATETIME(),
        TrangThai    NVARCHAR(20)  NOT NULL
            CONSTRAINT DF_TaiKhoan_TrangThai DEFAULT N'Active',

        CONSTRAINT PK_TaiKhoan
            PRIMARY KEY (SoTaiKhoan),

        CONSTRAINT FK_TaiKhoan_ThongTinKhachHang
            FOREIGN KEY (MaKH)
            REFERENCES dbo.ThongTinKhachHang(MaKH),

        CONSTRAINT FK_TaiKhoan_NganHang
            FOREIGN KEY (MaNganHang)
            REFERENCES dbo.NganHang(MaNganHang),

        CONSTRAINT FK_TaiKhoan_LoaiTien
            FOREIGN KEY (MaLoaiTien)
            REFERENCES dbo.LoaiTien(MaLoaiTien),

        CONSTRAINT CK_TaiKhoan_SoDuBanDau
            CHECK (SoDuBanDau >= 0),

        CONSTRAINT CK_TaiKhoan_SoDuHienTai
            CHECK (SoDuHienTai >= 0),

        CONSTRAINT CK_TaiKhoan_TrangThai
            CHECK (TrangThai IN (N'Active', N'Locked', N'Closed'))
    );
END;

-- ============================================================
-- 6. LOẠI GIAO DỊCH
-- ============================================================

IF OBJECT_ID(N'dbo.LoaiGiaoDich', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.LoaiGiaoDich (
        MaLoaiGD   INT IDENTITY(1,1) NOT NULL,
        TenLoaiGD  NVARCHAR(100) NOT NULL,
        PhanLoai   VARCHAR(10)   NOT NULL,

        CONSTRAINT PK_LoaiGiaoDich
            PRIMARY KEY (MaLoaiGD),

        CONSTRAINT CK_LoaiGiaoDich_PhanLoai
            CHECK (PhanLoai IN ('INCOME', 'EXPENSE'))
    );
END;

-- ============================================================
-- 7. GIAO DỊCH
-- ============================================================

IF OBJECT_ID(N'dbo.GiaoDich', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.GiaoDich (
        MaGiaoDich    VARCHAR(30)   NOT NULL,
        SoTaiKhoan    VARCHAR(30)   NOT NULL,
        MaLoaiGD      INT           NOT NULL,
        MaLoaiTien    VARCHAR(10)   NOT NULL,
        SoTien        DECIMAL(18,2) NOT NULL,
        SoTienGoc     DECIMAL(18,2) NULL,
        NgayGiaoDich  DATETIME2(0)  NOT NULL
            CONSTRAINT DF_GiaoDich_NgayGiaoDich DEFAULT SYSDATETIME(),
        NoiDung       NVARCHAR(255) NULL,
        TrangThai     NVARCHAR(20)  NOT NULL
            CONSTRAINT DF_GiaoDich_TrangThai DEFAULT N'Success',

        CONSTRAINT PK_GiaoDich
            PRIMARY KEY (MaGiaoDich),

        CONSTRAINT FK_GiaoDich_TaiKhoan
            FOREIGN KEY (SoTaiKhoan)
            REFERENCES dbo.TaiKhoan(SoTaiKhoan),

        CONSTRAINT FK_GiaoDich_LoaiGD
            FOREIGN KEY (MaLoaiGD)
            REFERENCES dbo.LoaiGiaoDich(MaLoaiGD),

        CONSTRAINT FK_GiaoDich_LoaiTien
            FOREIGN KEY (MaLoaiTien)
            REFERENCES dbo.LoaiTien(MaLoaiTien),

        CONSTRAINT CK_GiaoDich_SoTien
            CHECK (SoTien > 0),

        CONSTRAINT CK_GiaoDich_SoTienGoc
            CHECK (SoTienGoc IS NULL OR SoTienGoc > 0),

        CONSTRAINT CK_GiaoDich_TrangThai
            CHECK (TrangThai IN (N'Success', N'Pending', N'Failed', N'Cancelled'))
    );
END;

-- ============================================================
-- 8. NHẬT KÝ HOẠT ĐỘNG
-- ============================================================

IF OBJECT_ID(N'dbo.NhatKyHoatDong', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.NhatKyHoatDong (
        MaLog        BIGINT IDENTITY(1,1) NOT NULL,
        MaNguoiDung  INT            NULL,
        HanhDong     NVARCHAR(100)  NOT NULL,
        DoiTuong     NVARCHAR(100)  NULL,
        DoiTuongID   VARCHAR(50)    NULL,
        ChiTiet      NVARCHAR(500)  NULL,
        DiaChiIP     VARCHAR(45)    NULL,
        ThoiGian     DATETIME2(0)   NOT NULL
            CONSTRAINT DF_NhatKyHoatDong_ThoiGian DEFAULT SYSDATETIME(),

        CONSTRAINT PK_NhatKyHoatDong
            PRIMARY KEY (MaLog),

        CONSTRAINT FK_NhatKyHoatDong_NguoiDung
            FOREIGN KEY (MaNguoiDung)
            REFERENCES dbo.NguoiDung(MaNguoiDung)
    );
END;

-- ============================================================
-- 9. INDEXES
-- ============================================================

IF NOT EXISTS (SELECT 1 FROM sys.indexes
    WHERE name = N'IX_ThongTinKhachHang_SoDienThoai'
      AND object_id = OBJECT_ID(N'dbo.ThongTinKhachHang'))
    CREATE INDEX IX_ThongTinKhachHang_SoDienThoai ON dbo.ThongTinKhachHang(SoDienThoai);

IF NOT EXISTS (SELECT 1 FROM sys.indexes
    WHERE name = N'IX_ThongTinKhachHang_Email'
      AND object_id = OBJECT_ID(N'dbo.ThongTinKhachHang'))
    CREATE INDEX IX_ThongTinKhachHang_Email ON dbo.ThongTinKhachHang(Email);

IF NOT EXISTS (SELECT 1 FROM sys.indexes
    WHERE name = N'IX_NguoiDung_Quyen_TrangThai'
      AND object_id = OBJECT_ID(N'dbo.NguoiDung'))
    CREATE INDEX IX_NguoiDung_Quyen_TrangThai ON dbo.NguoiDung(Quyen, TrangThai);

IF NOT EXISTS (SELECT 1 FROM sys.indexes
    WHERE name = N'IX_NguoiDung_MaKH'
      AND object_id = OBJECT_ID(N'dbo.NguoiDung'))
    CREATE INDEX IX_NguoiDung_MaKH ON dbo.NguoiDung(MaKH);

IF NOT EXISTS (SELECT 1 FROM sys.indexes
    WHERE name = N'IX_TaiKhoan_MaKH'
      AND object_id = OBJECT_ID(N'dbo.TaiKhoan'))
    CREATE INDEX IX_TaiKhoan_MaKH ON dbo.TaiKhoan(MaKH);

IF NOT EXISTS (SELECT 1 FROM sys.indexes
    WHERE name = N'IX_TaiKhoan_TrangThai'
      AND object_id = OBJECT_ID(N'dbo.TaiKhoan'))
    CREATE INDEX IX_TaiKhoan_TrangThai ON dbo.TaiKhoan(TrangThai);

IF NOT EXISTS (SELECT 1 FROM sys.indexes
    WHERE name = N'IX_GiaoDich_SoTaiKhoan_Ngay'
      AND object_id = OBJECT_ID(N'dbo.GiaoDich'))
    CREATE INDEX IX_GiaoDich_SoTaiKhoan_Ngay
        ON dbo.GiaoDich(SoTaiKhoan, NgayGiaoDich DESC);

IF NOT EXISTS (SELECT 1 FROM sys.indexes
    WHERE name = N'IX_GiaoDich_TrangThai_Ngay'
      AND object_id = OBJECT_ID(N'dbo.GiaoDich'))
    CREATE INDEX IX_GiaoDich_TrangThai_Ngay
        ON dbo.GiaoDich(TrangThai, NgayGiaoDich DESC);

IF NOT EXISTS (SELECT 1 FROM sys.indexes
    WHERE name = N'IX_NhatKyHoatDong_NguoiDung_ThoiGian'
      AND object_id = OBJECT_ID(N'dbo.NhatKyHoatDong'))
    CREATE INDEX IX_NhatKyHoatDong_NguoiDung_ThoiGian
        ON dbo.NhatKyHoatDong(MaNguoiDung, ThoiGian DESC);


