USE [QLCHNongDuoc]
GO

-- =============================================
-- 1. TẠO TABLE KHUYEN_MAI
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'KHUYEN_MAI')
BEGIN
    CREATE TABLE [dbo].[KHUYEN_MAI](
        [ID] [int] IDENTITY(1,1) NOT NULL,
        [TEN_KHUYEN_MAI] [nvarchar](255) NOT NULL,
        [TY_LE_GIAM] [decimal](5, 2) NOT NULL,           -- % giảm (0-100)
        [DIEU_KIEN_LOAI] [varchar](20) NOT NULL,         -- 'TONG_TIEN' hoặc 'SO_LUONG'
        [DIEU_KIEN_GIA_TRI] [decimal](18, 2) NOT NULL,   -- Giá trị điều kiện
        [TU_NGAY] [datetime] NOT NULL,
        [DEN_NGAY] [datetime] NOT NULL,
        [TRANG_THAI] [bit] NOT NULL DEFAULT 1,           -- 1: Hoạt động, 0: Ngừng
        [GHI_CHU] [nvarchar](500) NULL,

        CONSTRAINT [PK_KHUYEN_MAI] PRIMARY KEY CLUSTERED ([ID] ASC)
    )

    PRINT 'Đã tạo table KHUYEN_MAI'
END
ELSE
BEGIN
    PRINT 'Table KHUYEN_MAI đã tồn tại'
END
GO

-- =============================================
-- 2. THÊM CỘT CHIET_KHAU VÀO PHIEU_BAN
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[PHIEU_BAN]')
               AND name = 'CHIET_KHAU')
BEGIN
    ALTER TABLE [dbo].[PHIEU_BAN]
    ADD [CHIET_KHAU] [decimal](5, 2) NULL DEFAULT 0

    PRINT 'Đã thêm cột CHIET_KHAU vào PHIEU_BAN'
END
ELSE
BEGIN
    PRINT 'Cột CHIET_KHAU đã tồn tại'
END
GO

-- =============================================
-- 3. THÊM CỘT ID_KHUYEN_MAI VÀO PHIEU_BAN (FK)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.columns
               WHERE object_id = OBJECT_ID(N'[dbo].[PHIEU_BAN]')
               AND name = 'ID_KHUYEN_MAI')
BEGIN
    ALTER TABLE [dbo].[PHIEU_BAN]
    ADD [ID_KHUYEN_MAI] [int] NULL

    -- Thêm Foreign Key
    ALTER TABLE [dbo].[PHIEU_BAN]
    ADD CONSTRAINT [FK_PHIEU_BAN_KHUYEN_MAI]
    FOREIGN KEY ([ID_KHUYEN_MAI]) REFERENCES [dbo].[KHUYEN_MAI]([ID])

    PRINT 'Đã thêm cột ID_KHUYEN_MAI và Foreign Key vào PHIEU_BAN'
END
ELSE
BEGIN
    PRINT 'Cột ID_KHUYEN_MAI đã tồn tại'
END
GO

-- =============================================
-- 4. UPDATE GIÁ TRỊ MẶC ĐỊNH CHO PHIẾU CŨ
-- =============================================
UPDATE [dbo].[PHIEU_BAN]
SET CHIET_KHAU = 0
WHERE CHIET_KHAU IS NULL
GO

PRINT 'Đã cập nhật giá trị mặc định cho phiếu cũ'
GO

-- =============================================
-- 5. INSERT DỮ LIỆU MẪU
-- =============================================
IF NOT EXISTS (SELECT * FROM [dbo].[KHUYEN_MAI])
BEGIN
    INSERT INTO [dbo].[KHUYEN_MAI]
        ([TEN_KHUYEN_MAI], [TY_LE_GIAM], [DIEU_KIEN_LOAI], [DIEU_KIEN_GIA_TRI], [TU_NGAY], [DEN_NGAY], [TRANG_THAI], [GHI_CHU])
    VALUES
        (N'Black Friday 2025', 15.00, 'TONG_TIEN', 2000000, '2025-11-20', '2025-11-30', 1, N'Mua từ 2 triệu giảm 15%'),
        (N'Khuyến mãi Tết 2026', 10.00, 'TONG_TIEN', 1000000, '2026-01-20', '2026-02-10', 1, N'Mua từ 1 triệu giảm 10%'),
        (N'Mua nhiều giảm giá', 5.00, 'SO_LUONG', 10, '2025-01-01', '2026-12-31', 1, N'Mua từ 10 sản phẩm giảm 5%')

    PRINT 'Đã thêm 3 chương trình khuyến mãi mẫu'
END
ELSE
BEGIN
    PRINT 'Đã có dữ liệu khuyến mãi'
END
GO

-- =============================================
-- 6. KIỂM TRA KẾT QUẢ
-- =============================================
SELECT COUNT(*) AS [So_Chuong_Trinh_KM] FROM [dbo].[KHUYEN_MAI]
GO

PRINT '====================================='
PRINT 'MIGRATION HOÀN TẤT!'
PRINT 'Table KHUYEN_MAI: OK'
PRINT 'PHIEU_BAN.CHIET_KHAU: OK'
PRINT 'PHIEU_BAN.ID_KHUYEN_MAI: OK'
PRINT '====================================='
GO
