-- ============================================================
-- BANK MANAGEMENT - DATABASE SCHEMA
-- Version: 3.0 (Pure DDL - No Seed Data)
-- Compatible with SQL Server 2016 / 2019 / 2022 / Express
-- Compatible with ADO.NET SqlCommand (No GO statements)
-- ============================================================

SET NOCOUNT ON;

PRINT N'>>> Initializing BankManagement Schema <<<';

-- ============================================================
-- 1. LOẠI TIỀN (Currencies)
-- ============================================================
IF OBJECT_ID(N'dbo.LoaiTien', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.LoaiTien (
        MaLoaiTien   VARCHAR(10)   NOT NULL,
        TenLoaiTien  NVARCHAR(50)  NOT NULL,
        QuyDoi       DECIMAL(18,6) NOT NULL
            CONSTRAINT DF_LoaiTien_QuyDoi DEFAULT 1,

        CONSTRAINT PK_LoaiTien PRIMARY KEY (MaLoaiTien),
        CONSTRAINT CK_LoaiTien_QuyDoi CHECK (QuyDoi > 0)
    );
END;
ELSE
BEGIN
    IF COL_LENGTH(N'dbo.LoaiTien', N'QuyDoi') IS NULL
    BEGIN
        ALTER TABLE dbo.LoaiTien ADD QuyDoi DECIMAL(18,6) NOT NULL CONSTRAINT DF_LoaiTien_QuyDoi DEFAULT 1;
    END;
END;

-- ============================================================
-- 2. NGÂN HÀNG (Banks)
-- ============================================================
IF OBJECT_ID(N'dbo.NganHang', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.NganHang (
        MaNganHang   VARCHAR(20)   NOT NULL,
        TenNganHang  NVARCHAR(100) NOT NULL,
        ChiNhanh     NVARCHAR(100) NULL,
        SwiftCode    VARCHAR(20)   NULL,

        CONSTRAINT PK_NganHang PRIMARY KEY (MaNganHang)
    );
END;

-- ============================================================
-- 3. KHÁCH HÀNG (Core Customers)
-- ============================================================
IF OBJECT_ID(N'dbo.KhachHang', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.KhachHang (
        MaKH         VARCHAR(20)   NOT NULL,
        HoTen        NVARCHAR(100) NOT NULL,
        CMND_CCCD    VARCHAR(20)   NOT NULL,
        NgaySinh     DATE          NULL,
        GioiTinh     NVARCHAR(10)  NULL,
        SoDienThoai  VARCHAR(15)   NULL,
        Email        VARCHAR(100)  NULL,
        DiaChi       NVARCHAR(255) NULL,
        TrangThai    NVARCHAR(50)  NULL
            CONSTRAINT DF_KhachHang_TrangThai DEFAULT N'Active',
        NgayTao      DATETIME      NULL
            CONSTRAINT DF_KhachHang_NgayTao DEFAULT GETDATE(),

        CONSTRAINT PK_KhachHang PRIMARY KEY (MaKH),
        CONSTRAINT UQ_KhachHang_CMND UNIQUE (CMND_CCCD)
    );
END;

-- ============================================================
-- 4. THÔNG TIN KHÁCH HÀNG (Detailed Customer Profiles)
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

        CONSTRAINT PK_ThongTinKhachHang PRIMARY KEY (MaKH),
        CONSTRAINT UQ_ThongTinKhachHang_CMND UNIQUE (CMND_CCCD),
        CONSTRAINT CK_ThongTinKhachHang_TrangThai CHECK (TrangThai IN (N'Active', N'Locked'))
    );
END;

-- ============================================================
-- 5. NGƯỜI DÙNG (User Accounts)
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
        Email         VARCHAR(100)  NULL,
        TrangThai     NVARCHAR(50)  NOT NULL
            CONSTRAINT DF_NguoiDung_TrangThai DEFAULT N'Active',
        NgayTao       DATETIME      NOT NULL
            CONSTRAINT DF_NguoiDung_NgayTao DEFAULT GETDATE(),
        LanDangNhap   DATETIME      NULL,

        CONSTRAINT PK_NguoiDung PRIMARY KEY (MaNguoiDung),
        CONSTRAINT UQ_NguoiDung_TenDangNhap UNIQUE (TenDangNhap),
        CONSTRAINT CK_NguoiDung_Quyen CHECK (Quyen IN ('Admin', 'Member')),
        CONSTRAINT CK_NguoiDung_TrangThai CHECK (TrangThai IN (N'Active', N'Locked'))
    );
END;
ELSE
BEGIN
    IF COL_LENGTH(N'dbo.NguoiDung', N'MaKH') IS NULL
        ALTER TABLE dbo.NguoiDung ADD MaKH VARCHAR(20) NULL;
    IF COL_LENGTH(N'dbo.NguoiDung', N'HoTen') IS NULL
        ALTER TABLE dbo.NguoiDung ADD HoTen NVARCHAR(100) NULL;
    IF COL_LENGTH(N'dbo.NguoiDung', N'Email') IS NULL
        ALTER TABLE dbo.NguoiDung ADD Email VARCHAR(100) NULL;
    IF COL_LENGTH(N'dbo.NguoiDung', N'TrangThai') IS NULL
        ALTER TABLE dbo.NguoiDung ADD TrangThai NVARCHAR(50) NOT NULL CONSTRAINT DF_NguoiDung_TrangThai DEFAULT N'Active';
    IF COL_LENGTH(N'dbo.NguoiDung', N'NgayTao') IS NULL
        ALTER TABLE dbo.NguoiDung ADD NgayTao DATETIME NOT NULL CONSTRAINT DF_NguoiDung_NgayTao DEFAULT GETDATE();
    IF COL_LENGTH(N'dbo.NguoiDung', N'LanDangNhap') IS NULL
        ALTER TABLE dbo.NguoiDung ADD LanDangNhap DATETIME NULL;
END;

-- ============================================================
-- 6. TÀI KHOẢN NGÂN HÀNG (Bank Accounts)
-- ============================================================
IF OBJECT_ID(N'dbo.TaiKhoan', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.TaiKhoan (
        SoTaiKhoan   VARCHAR(30)   NOT NULL,
        MaKH         VARCHAR(20)   NULL,
        MaNganHang   VARCHAR(20)   NULL,
        MaLoaiTien   VARCHAR(10)   NULL,
        SoDuBanDau   DECIMAL(18,2) NULL
            CONSTRAINT DF_TaiKhoan_SoDuBanDau DEFAULT 0,
        SoDuHienTai  DECIMAL(18,2) NULL
            CONSTRAINT DF_TaiKhoan_SoDuHienTai DEFAULT 0,
        NgayMo       DATETIME      NULL
            CONSTRAINT DF_TaiKhoan_NgayMo DEFAULT GETDATE(),
        TrangThai    NVARCHAR(50)  NULL
            CONSTRAINT DF_TaiKhoan_TrangThai DEFAULT 'Active',

        CONSTRAINT PK_TaiKhoan PRIMARY KEY (SoTaiKhoan),
        CONSTRAINT FK_TaiKhoan_KhachHang FOREIGN KEY (MaKH) REFERENCES dbo.KhachHang(MaKH),
        CONSTRAINT FK_TaiKhoan_NganHang FOREIGN KEY (MaNganHang) REFERENCES dbo.NganHang(MaNganHang),
        CONSTRAINT FK_TaiKhoan_LoaiTien FOREIGN KEY (MaLoaiTien) REFERENCES dbo.LoaiTien(MaLoaiTien),
        CONSTRAINT CK_TaiKhoan_SoDuHienTai CHECK (SoDuHienTai >= 0)
    );
END;

-- ============================================================
-- 7. LOẠI GIAO DỊCH (Transaction Types)
-- ============================================================
IF OBJECT_ID(N'dbo.LoaiGiaoDich', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.LoaiGiaoDich (
        MaLoaiGD   INT IDENTITY(1,1) NOT NULL,
        TenLoaiGD  NVARCHAR(100) NOT NULL,
        PhanLoai   VARCHAR(10)   NOT NULL,

        CONSTRAINT PK_LoaiGiaoDich PRIMARY KEY (MaLoaiGD),
        CONSTRAINT CK_LoaiGiaoDich_PhanLoai CHECK (PhanLoai IN ('INCOME', 'EXPENSE'))
    );
END;

-- ============================================================
-- 8. GIAO DỊCH (Transactions - Includes LoaiTien)
-- ============================================================
IF OBJECT_ID(N'dbo.GiaoDich', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.GiaoDich (
        MaGiaoDich    VARCHAR(30)   NOT NULL,
        SoTaiKhoan    VARCHAR(30)   NULL,
        MaLoaiGD      INT           NULL,
        SoTien        DECIMAL(18,2) NOT NULL,
        NgayGiaoDich  DATETIME      NULL
            CONSTRAINT DF_GiaoDich_NgayGiaoDich DEFAULT GETDATE(),
        NoiDung       NVARCHAR(255) NULL,
        TrangThai     NVARCHAR(50)  NULL
            CONSTRAINT DF_GiaoDich_TrangThai DEFAULT 'Success',
        LoaiTien      VARCHAR(10)   NULL
            CONSTRAINT DF_GiaoDich_LoaiTien DEFAULT 'VND',
        SoTienGoc     DECIMAL(18,2) NULL,
        MaLoaiTien    VARCHAR(10)   NULL,

        CONSTRAINT PK_GiaoDich PRIMARY KEY (MaGiaoDich),
        CONSTRAINT FK_GiaoDich_TaiKhoan FOREIGN KEY (SoTaiKhoan) REFERENCES dbo.TaiKhoan(SoTaiKhoan),
        CONSTRAINT FK_GiaoDich_LoaiGD FOREIGN KEY (MaLoaiGD) REFERENCES dbo.LoaiGiaoDich(MaLoaiGD)
    );
END;
ELSE
BEGIN
    -- Migration checks for existing databases: ensure all columns exist
    IF COL_LENGTH(N'dbo.GiaoDich', N'LoaiTien') IS NULL
    BEGIN
        ALTER TABLE dbo.GiaoDich ADD LoaiTien VARCHAR(10) NULL CONSTRAINT DF_GiaoDich_LoaiTien DEFAULT 'VND';
    END;
    IF COL_LENGTH(N'dbo.GiaoDich', N'SoTienGoc') IS NULL
    BEGIN
        ALTER TABLE dbo.GiaoDich ADD SoTienGoc DECIMAL(18,2) NULL;
    END;
    IF COL_LENGTH(N'dbo.GiaoDich', N'MaLoaiTien') IS NULL
    BEGIN
        ALTER TABLE dbo.GiaoDich ADD MaLoaiTien VARCHAR(10) NULL;
    END;
END;

-- ============================================================
-- 9. KHOẢN VAY (Loans)
-- ============================================================
IF OBJECT_ID(N'dbo.KhoanVay', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.KhoanVay (
        MaKhoanVay   VARCHAR(30)   NOT NULL,
        MaKH         VARCHAR(20)   NOT NULL,
        SoTaiKhoan   VARCHAR(30)   NOT NULL,
        SoTienVay    DECIMAL(18,2) NOT NULL,
        ThoiGianVay  INT           NOT NULL,
        LaiSuat      DECIMAL(5,2)  NOT NULL
            CONSTRAINT DF_KhoanVay_LaiSuat DEFAULT 1.55,
        TienLai      DECIMAL(18,2) NOT NULL
            CONSTRAINT DF_KhoanVay_TienLai DEFAULT 0,
        TongPhaiTra  DECIMAL(18,2) NOT NULL
            CONSTRAINT DF_KhoanVay_TongPhaiTra DEFAULT 0,
        NgayDangKy   DATETIME      NOT NULL
            CONSTRAINT DF_KhoanVay_NgayDangKy DEFAULT GETDATE(),
        NgayDaoHan   DATETIME      NOT NULL,
        TrangThai    NVARCHAR(50)  NOT NULL
            CONSTRAINT DF_KhoanVay_TrangThai DEFAULT N'Chờ xét duyệt',
        GhiChu       NVARCHAR(500) NULL,
        NgayDuyet    DATETIME      NULL,
        NguoiDuyet   NVARCHAR(50)  NULL,

        CONSTRAINT PK_KhoanVay PRIMARY KEY (MaKhoanVay)
    );
END;

-- ============================================================
-- 10. SỔ TIẾT KIỆM (Savings Accounts)
-- ============================================================
IF OBJECT_ID(N'dbo.SoTietKiem', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.SoTietKiem (
        MaSoTKTK              VARCHAR(30)   NOT NULL,
        MaKH                  VARCHAR(20)   NOT NULL,
        SoTaiKhoanTrich       VARCHAR(30)   NOT NULL,
        SoTienGui             DECIMAL(18,2) NOT NULL,
        KyHanThang            INT           NOT NULL,
        LaiSuatNam            DECIMAL(5,2)  NOT NULL,
        TienLaiDuTinh         DECIMAL(18,2) NOT NULL,
        TongTienDaoHanDuTinh  DECIMAL(18,2) NOT NULL,
        HinhThucDaoHan        NVARCHAR(100) NOT NULL
            CONSTRAINT DF_SoTietKiem_HinhThuc DEFAULT N'Tự động tái tục gốc',
        NgayMo                DATETIME      NOT NULL
            CONSTRAINT DF_SoTietKiem_NgayMo DEFAULT GETDATE(),
        NgayDaoHan            DATETIME      NOT NULL,
        TrangThai             NVARCHAR(50)  NOT NULL
            CONSTRAINT DF_SoTietKiem_TrangThai DEFAULT N'Đang gửi',
        NgayTatToan           DATETIME      NULL,
        TienLaiThucNhan       DECIMAL(18,2) NULL,
        TongTienThucNhan      DECIMAL(18,2) NULL,
        GhiChu                NVARCHAR(500) NULL,

        CONSTRAINT PK_SoTietKiem PRIMARY KEY (MaSoTKTK)
    );
END;

-- ============================================================
-- 11. HẠN MỨC CHI TIÊU (Spending Limits)
-- ============================================================
IF OBJECT_ID(N'dbo.HanMucChiTieu', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.HanMucChiTieu (
        MaKH             VARCHAR(30)   NOT NULL,
        HanMucNgay       DECIMAL(18,2) NOT NULL
            CONSTRAINT DF_HanMucChiTieu_HanMucNgay DEFAULT 2000000,
        HanMucTuan       DECIMAL(18,2) NOT NULL
            CONSTRAINT DF_HanMucChiTieu_HanMucTuan DEFAULT 10000000,
        CanhBaoVuotMuc   BIT           NOT NULL
            CONSTRAINT DF_HanMucChiTieu_CanhBao DEFAULT 1,
        NgayCapNhat      DATETIME      NOT NULL
            CONSTRAINT DF_HanMucChiTieu_NgayCapNhat DEFAULT GETDATE(),

        CONSTRAINT PK_HanMucChiTieu PRIMARY KEY (MaKH)
    );
END;

-- ============================================================
-- 12. SẢN PHẨM MUA SẮM (Shopping Products)
-- ============================================================
IF OBJECT_ID(N'dbo.SanPham', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.SanPham (
        MaSP         INT IDENTITY(1,1) NOT NULL,
        TenSP        NVARCHAR(250) NOT NULL,
        GiaBan       DECIMAL(18,2) NOT NULL,
        HinhAnh      NVARCHAR(500) NULL,
        MoTa         NVARCHAR(MAX) NULL,
        ThongSo      NVARCHAR(MAX) NULL,
        SoLuongTon   INT           NOT NULL
            CONSTRAINT DF_SanPham_SoLuongTon DEFAULT 100,
        TrangThai    NVARCHAR(50)  NOT NULL
            CONSTRAINT DF_SanPham_TrangThai DEFAULT 'Active',
        NgayTao      DATETIME      NULL
            CONSTRAINT DF_SanPham_NgayTao DEFAULT GETDATE(),

        CONSTRAINT PK_SanPham PRIMARY KEY (MaSP)
    );
END;

-- ============================================================
-- 13. NHẬT KÝ HOẠT ĐỘNG (Audit Logs)
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
        ThoiGian     DATETIME       NOT NULL
            CONSTRAINT DF_NhatKyHoatDong_ThoiGian DEFAULT GETDATE(),

        CONSTRAINT PK_NhatKyHoatDong PRIMARY KEY (MaLog),
        CONSTRAINT FK_NhatKyHoatDong_NguoiDung FOREIGN KEY (MaNguoiDung) REFERENCES dbo.NguoiDung(MaNguoiDung)
    );
END;

-- ============================================================
-- 14. INDEXES FOR PERFORMANCE
-- ============================================================
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_ThongTinKhachHang_SoDienThoai' AND object_id = OBJECT_ID(N'dbo.ThongTinKhachHang'))
    CREATE INDEX IX_ThongTinKhachHang_SoDienThoai ON dbo.ThongTinKhachHang(SoDienThoai);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_ThongTinKhachHang_Email' AND object_id = OBJECT_ID(N'dbo.ThongTinKhachHang'))
    CREATE INDEX IX_ThongTinKhachHang_Email ON dbo.ThongTinKhachHang(Email);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_KhachHang_SoDienThoai' AND object_id = OBJECT_ID(N'dbo.KhachHang'))
    CREATE INDEX IX_KhachHang_SoDienThoai ON dbo.KhachHang(SoDienThoai);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_KhachHang_Email' AND object_id = OBJECT_ID(N'dbo.KhachHang'))
    CREATE INDEX IX_KhachHang_Email ON dbo.KhachHang(Email);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_NguoiDung_Quyen_TrangThai' AND object_id = OBJECT_ID(N'dbo.NguoiDung'))
    CREATE INDEX IX_NguoiDung_Quyen_TrangThai ON dbo.NguoiDung(Quyen, TrangThai);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_NguoiDung_MaKH' AND object_id = OBJECT_ID(N'dbo.NguoiDung'))
    CREATE INDEX IX_NguoiDung_MaKH ON dbo.NguoiDung(MaKH);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_TaiKhoan_MaKH' AND object_id = OBJECT_ID(N'dbo.TaiKhoan'))
    CREATE INDEX IX_TaiKhoan_MaKH ON dbo.TaiKhoan(MaKH);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_TaiKhoan_TrangThai' AND object_id = OBJECT_ID(N'dbo.TaiKhoan'))
    CREATE INDEX IX_TaiKhoan_TrangThai ON dbo.TaiKhoan(TrangThai);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_GiaoDich_SoTaiKhoan_Ngay' AND object_id = OBJECT_ID(N'dbo.GiaoDich'))
    CREATE INDEX IX_GiaoDich_SoTaiKhoan_Ngay ON dbo.GiaoDich(SoTaiKhoan, NgayGiaoDich DESC);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_GiaoDich_TrangThai_Ngay' AND object_id = OBJECT_ID(N'dbo.GiaoDich'))
    CREATE INDEX IX_GiaoDich_TrangThai_Ngay ON dbo.GiaoDich(TrangThai, NgayGiaoDich DESC);

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_NhatKyHoatDong_NguoiDung_ThoiGian' AND object_id = OBJECT_ID(N'dbo.NhatKyHoatDong'))
    CREATE INDEX IX_NhatKyHoatDong_NguoiDung_ThoiGian ON dbo.NhatKyHoatDong(MaNguoiDung, ThoiGian DESC);

PRINT N'>>> BankManagement Schema initialization completed successfully. <<<';
