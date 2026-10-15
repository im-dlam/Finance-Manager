-- ============================================================================
-- SCRIPT: Xóa toàn bộ dữ liệu user cũ, giữ lại Admin và tạo 20 Users mới
-- Database: BankManagement
-- ============================================================================

USE BankManagement;
GO

SET NOCOUNT ON;

BEGIN TRANSACTION;

BEGIN TRY
    PRINT N'Bắt đầu dọn dẹp dữ liệu cũ của user...';

    -- 1. Xác định danh sách MaKH của Admin (nếu có)
    DECLARE @AdminMaKH TABLE (MaKH VARCHAR(20));
    INSERT INTO @AdminMaKH (MaKH)
    SELECT DISTINCT MaKH 
    FROM dbo.NguoiDung 
    WHERE Quyen = 'Admin' AND MaKH IS NOT NULL AND MaKH <> '';

    -- 2. Xóa nhật ký hoạt động của non-admin users
    DELETE FROM dbo.NhatKyHoatDong 
    WHERE MaNguoiDung IN (SELECT MaNguoiDung FROM dbo.NguoiDung WHERE Quyen <> 'Admin');

    -- 3. Xóa giao dịch của các tài khoản không thuộc Admin
    DELETE FROM dbo.GiaoDich 
    WHERE SoTaiKhoan IN (
        SELECT SoTaiKhoan FROM dbo.TaiKhoan 
        WHERE MaKH NOT IN (SELECT MaKH FROM @AdminMaKH) OR MaKH IS NULL
    );

    -- 4. Xóa tài khoản của non-admin
    DELETE FROM dbo.TaiKhoan 
    WHERE MaKH NOT IN (SELECT MaKH FROM @AdminMaKH) OR MaKH IS NULL;

    -- 5. Xóa dữ liệu khách hàng không thuộc Admin trong cả 2 bảng KhachHang và ThongTinKhachHang
    DELETE FROM dbo.KhachHang 
    WHERE MaKH NOT IN (SELECT MaKH FROM @AdminMaKH);

    DELETE FROM dbo.ThongTinKhachHang 
    WHERE MaKH NOT IN (SELECT MaKH FROM @AdminMaKH);

    -- 6. Xóa toàn bộ người dùng non-admin
    DELETE FROM dbo.NguoiDung 
    WHERE Quyen <> 'Admin';

    -- 6.1. Cập nhật tên tiếng Việt chuẩn cho Admin
    UPDATE dbo.NguoiDung SET HoTen = N'Quản trị viên' WHERE TenDangNhap = 'admin';
    UPDATE dbo.NguoiDung SET HoTen = N'Vũ Đại D' WHERE TenDangNhap = 'nhanvien_d';
    UPDATE dbo.ThongTinKhachHang SET HoTen = N'Quản trị viên' WHERE MaKH = 'KH000005';
    UPDATE dbo.KhachHang SET HoTen = N'Quản trị viên' WHERE MaKH = 'KH000005';

    PRINT N'Đã dọn dẹp xong dữ liệu cũ. Bắt đầu tạo 20 users mới...';

    -- Bảng tạm chứa thông tin 20 users
    DECLARE @UsersTable TABLE (
        Id INT IDENTITY(1,1),
        Username VARCHAR(50),
        FullName NVARCHAR(100),
        Email VARCHAR(100),
        Phone VARCHAR(20),
        Cccd VARCHAR(20),
        Gender NVARCHAR(10),
        Dob DATE,
        Address NVARCHAR(200),
        MaKH VARCHAR(20),
        HasKYC BIT,
        AccountCount INT
    );

    INSERT INTO @UsersTable (Username, FullName, Email, Phone, Cccd, Gender, Dob, Address, MaKH, HasKYC, AccountCount)
    VALUES
    ('user01', N'Nguyễn Văn An', 'an.nv@gmail.com', '0901111001', '001090000001', N'Nam', '1992-03-15', N'12 Kim Mã, Ba Đình, Hà Nội', 'KH100001', 1, 2),
    ('user02', N'Trần Thị Bích', 'bich.tt@gmail.com', '0901111002', '001090000002', N'Nữ', '1995-07-22', N'45 Lê Lợi, Quận 1, TP.HCM', 'KH100002', 1, 1),
    ('user03', N'Lê Hoàng Cường', 'cuong.lh@gmail.com', '0901111003', '001090000003', N'Nam', '1989-11-05', N'88 Nguyễn Huệ, Quận 1, TP.HCM', 'KH100003', 1, 2),
    ('user04', N'Phạm Minh Dũng', 'dung.pm@gmail.com', '0901111004', '001090000004', N'Nam', '1994-01-30', N'15 Hoàng Diệu, Hải Châu, Đà Nẵng', 'KH100004', 1, 1),
    ('user05', N'Hoàng Thu Giang', 'giang.ht@gmail.com', '0901111005', '001090000005', N'Nữ', '1998-09-18', N'77 Cầu Giấy, Cầu Giấy, Hà Nội', 'KH100005', 1, 2),
    ('user06', N'Đỗ Hải Đăng', 'dang.dh@gmail.com', '0901111006', '001090000006', N'Nam', '1991-04-12', N'23 Hùng Vương, Nha Trang, Khánh Hòa', 'KH100006', 1, 1),
    ('user07', N'Vũ Mai Hương', 'huong.vm@gmail.com', '0901111007', '001090000007', N'Nữ', '1996-12-25', N'102 Trần Phú, Hà Đông, Hà Nội', 'KH100007', 1, 2),
    ('user08', N'Bùi Quốc Huy', 'huy.bq@gmail.com', '0901111008', '001090000008', N'Nam', '1993-08-08', N'56 Bạch Đằng, Hải Châu, Đà Nẵng', 'KH100008', 1, 1),
    ('user09', N'Ngô Phương Linh', 'linh.np@gmail.com', '0901111009', '001090000009', N'Nữ', '1997-05-14', N'34 Điện Biên Phủ, Bình Thạnh, TP.HCM', 'KH100009', 1, 2),
    ('user10', N'Đặng Thành Nam', 'nam.dt@gmail.com', '0901111010', '001090000010', N'Nam', '1990-10-10', N'19 Tràng Thi, Hoàn Kiếm, Hà Nội', 'KH100010', 1, 1),
    ('user11', N'Đoàn Khánh Linh', 'linh.dk@gmail.com', '0901111011', '001090000011', N'Nữ', '1999-02-17', N'82 Võ Văn Kiệt, Quận 5, TP.HCM', 'KH100011', 1, 2),
    ('user12', N'Trịnh Văn Phúc', 'phuc.tv@gmail.com', '0901111012', '001090000012', N'Nam', '1987-06-20', N'63 Quang Trung, Hà Đông, Hà Nội', 'KH100012', 1, 1),
    ('user13', N'Lâm Gia Hân', 'han.lg@gmail.com', '0901111013', '001090000013', N'Nữ', '2000-08-30', N'11 Hai Bà Trưng, Quận 3, TP.HCM', 'KH100013', 1, 1),
    ('user14', N'Phan Đức Trọng', 'trong.pd@gmail.com', '0901111014', '001090000014', N'Nam', '1993-03-03', N'90 Pasteur, Quận 1, TP.HCM', 'KH100014', 1, 2),
    ('user15', N'Cao Thùy Trang', 'trang.ct@gmail.com', '0901111015', '001090000015', N'Nữ', '1996-11-11', N'47 Lý Thường Kiệt, Hoàn Kiếm, Hà Nội', 'KH100015', 1, 1),
    ('user16', N'Dương Hữu Nghĩa', 'nghia.dh@gmail.com', '0901111016', '001090000016', N'Nam', '1991-09-09', N'150 Cách Mạng Tháng 8, Quận 10, TP.HCM', 'KH100016', 1, 2),
    ('user17', N'Mai Tuấn Kiệt', 'kiet.mt@gmail.com', '0901111017', '001090000017', N'Nam', '1994-04-04', N'28 Hoàng Hoa Thám, Ba Đình, Hà Nội', 'KH100017', 1, 1),
    ('user18', N'Đinh Bảo Ngọc', 'ngoc.db@gmail.com', '0901111018', '001090000018', N'Nữ', '1998-01-28', N'66 Phan Đăng Lưu, Phú Nhuận, TP.HCM', 'KH100018', 1, 2),
    ('user19', N'Hà Trọng Khang', 'khang.ht@gmail.com', '0901111019', '001090000019', N'Nam', '1995-10-15', N'55 Nguyễn Thị Minh Khai, Quận 1, TP.HCM', NULL, 0, 0),
    ('user20', N'Lương Minh Quân', 'quan.lm@gmail.com', '0901111020', '001090000020', N'Nam', '1992-12-12', N'99 Tôn Đức Thắng, Đống Đa, Hà Nội', 'KH100020', 1, 0);

    DECLARE @i INT = 1;
    DECLARE @total INT = (SELECT COUNT(*) FROM @UsersTable);

    WHILE @i <= @total
    BEGIN
        DECLARE @Username VARCHAR(50), @FullName NVARCHAR(100), @Email VARCHAR(100), @Phone VARCHAR(20);
        DECLARE @Cccd VARCHAR(20), @Gender NVARCHAR(10), @Dob DATE, @Address NVARCHAR(200);
        DECLARE @MaKH VARCHAR(20), @HasKYC BIT, @AccountCount INT;

        SELECT 
            @Username = Username, @FullName = FullName, @Email = Email, @Phone = Phone,
            @Cccd = Cccd, @Gender = Gender, @Dob = Dob, @Address = Address,
            @MaKH = MaKH, @HasKYC = HasKYC, @AccountCount = AccountCount
        FROM @UsersTable WHERE Id = @i;

        -- 1. Thêm vào KhachHang & ThongTinKhachHang nếu có KYC
        IF @HasKYC = 1 AND @MaKH IS NOT NULL
        BEGIN
            INSERT INTO dbo.KhachHang (MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, TrangThai, NgayTao)
            VALUES (@MaKH, @FullName, @Cccd, @Dob, @Gender, @Phone, @Email, @Address, N'Active', GETDATE());

            INSERT INTO dbo.ThongTinKhachHang (MaKH, HoTen, CMND_CCCD, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, TrangThai, NgayTao)
            VALUES (@MaKH, @FullName, @Cccd, @Dob, @Gender, @Phone, @Email, @Address, N'Active', SYSDATETIME());
        END

        -- 2. Thêm vào NguoiDung
        INSERT INTO dbo.NguoiDung (TenDangNhap, MatKhau, Quyen, MaKH, HoTen, Email, TrangThai, NgayTao)
        VALUES (@Username, '123456', 'Member', @MaKH, @FullName, @Email, 'Active', GETDATE());

        -- 3. Tạo tài khoản ngân hàng và giao dịch nếu AccountCount > 0
        IF @AccountCount >= 1
        BEGIN
            -- Tài khoản 1: Luôn là VND
            DECLARE @Stk1 VARCHAR(30) = '10' + RIGHT('00000000' + CAST(@i AS VARCHAR(10)), 8);
            DECLARE @Bank1 VARCHAR(20) = CASE (@i % 4)
                WHEN 1 THEN 'NH01' -- Vietcombank
                WHEN 2 THEN 'NH02' -- Techcombank
                WHEN 3 THEN 'NH03' -- BIDV
                ELSE 'NH04'        -- MBBank
            END;
            DECLARE @InitBalance1 DECIMAL(18,2) = 10000000.00 + (@i * 2500000.00);

            INSERT INTO dbo.TaiKhoan (SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien, SoDuBanDau, SoDuHienTai, NgayMo, TrangThai)
            VALUES (@Stk1, @MaKH, @Bank1, 'VND', @InitBalance1, @InitBalance1 + 3500000.00, DATEADD(MONTH, -(@i % 6), GETDATE()), 'Active');

            -- Thêm 2 giao dịch cho tài khoản 1
            -- GD 1: Nạp tiền (INCOME)
            DECLARE @Mgd1 VARCHAR(30) = 'GD' + CAST(@i AS VARCHAR(10)) + '01';
            INSERT INTO dbo.GiaoDich (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, SoTienGoc, MaLoaiTien, LoaiTien, NgayGiaoDich, NoiDung, TrangThai)
            VALUES (@Mgd1, @Stk1, 1, 5000000.00, 5000000.00, 'VND', 'VND', DATEADD(DAY, -15, GETDATE()), N'Nạp tiền lương vào tài khoản', 'Success');

            -- GD 2: Chuyển khoản (EXPENSE)
            DECLARE @Mgd2 VARCHAR(30) = 'GD' + CAST(@i AS VARCHAR(10)) + '02';
            INSERT INTO dbo.GiaoDich (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, SoTienGoc, MaLoaiTien, LoaiTien, NgayGiaoDich, NoiDung, TrangThai)
            VALUES (@Mgd2, @Stk1, 3, 1500000.00, 1500000.00, 'VND', 'VND', DATEADD(DAY, -5, GETDATE()), N'Chuyển tiền mua sắm trực tuyến', 'Success');
        END

        IF @AccountCount >= 2
        BEGIN
            -- Tài khoản 2: Luân phiên VND hoặc USD
            DECLARE @Stk2 VARCHAR(30) = '20' + RIGHT('00000000' + CAST(@i AS VARCHAR(10)), 8);
            DECLARE @Bank2 VARCHAR(20) = CASE ((@i + 1) % 4)
                WHEN 1 THEN 'NH01'
                WHEN 2 THEN 'NH02'
                WHEN 3 THEN 'NH03'
                ELSE 'NH04'
            END;
            DECLARE @IsUsd BIT = CASE WHEN (@i % 2 = 0) THEN 1 ELSE 0 END;
            DECLARE @Currency VARCHAR(10) = CASE WHEN @IsUsd = 1 THEN 'USD' ELSE 'VND' END;
            DECLARE @InitBalance2 DECIMAL(18,2) = CASE WHEN @IsUsd = 1 THEN 1500.00 ELSE 15000000.00 END;
            DECLARE @CurrentBal2 DECIMAL(18,2) = CASE WHEN @IsUsd = 1 THEN 1200.00 ELSE 12000000.00 END;

            INSERT INTO dbo.TaiKhoan (SoTaiKhoan, MaKH, MaNganHang, MaLoaiTien, SoDuBanDau, SoDuHienTai, NgayMo, TrangThai)
            VALUES (@Stk2, @MaKH, @Bank2, @Currency, @InitBalance2, @CurrentBal2, DATEADD(MONTH, -(@i % 3), GETDATE()), 'Active');

            -- Thêm 1 giao dịch rút tiền cho tài khoản 2
            DECLARE @Mgd3 VARCHAR(30) = 'GD' + CAST(@i AS VARCHAR(10)) + '03';
            DECLARE @AmtGoc DECIMAL(18,2) = CASE WHEN @IsUsd = 1 THEN 300.00 ELSE 3000000.00 END;
            DECLARE @AmtVnd DECIMAL(18,2) = CASE WHEN @IsUsd = 1 THEN 7620000.00 ELSE 3000000.00 END;

            INSERT INTO dbo.GiaoDich (MaGiaoDich, SoTaiKhoan, MaLoaiGD, SoTien, SoTienGoc, MaLoaiTien, LoaiTien, NgayGiaoDich, NoiDung, TrangThai)
            VALUES (@Mgd3, @Stk2, 2, @AmtVnd, @AmtGoc, @Currency, @Currency, DATEADD(DAY, -2, GETDATE()), N'Rút tiền mặt tại ATM', 'Success');
        END

        SET @i = @i + 1;
    END;

    COMMIT TRANSACTION;
    PRINT N'>>> HOÀN TẤT THÀNH CÔNG: Đã tạo 20 users (user01 - user20) và giữ nguyên Admin! <<<';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    PRINT N'LỖI: ' + ERROR_MESSAGE();
    THROW;
END CATCH;
GO
