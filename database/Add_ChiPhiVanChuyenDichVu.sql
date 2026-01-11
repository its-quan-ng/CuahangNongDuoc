USE [QLCHNongDuoc]
GO

-- Kiểm tra và thêm cột CHI_PHI_VAN_CHUYEN
IF NOT EXISTS (
    SELECT * FROM sys.columns
    WHERE object_id = OBJECT_ID('[dbo].[PHIEU_BAN]')
    AND name = 'CHI_PHI_VAN_CHUYEN'
)
BEGIN
    PRINT 'Thêm cột CHI_PHI_VAN_CHUYEN vào PHIEU_BAN...'
    ALTER TABLE [dbo].[PHIEU_BAN]
    ADD [CHI_PHI_VAN_CHUYEN] [decimal](18, 0) NULL DEFAULT 0
    PRINT 'Đã thêm cột CHI_PHI_VAN_CHUYEN thành công!'
END
ELSE
BEGIN
    PRINT 'Cột CHI_PHI_VAN_CHUYEN đã tồn tại.'
END
GO

-- Kiểm tra và thêm cột CHI_PHI_DICH_VU
IF NOT EXISTS (
    SELECT * FROM sys.columns
    WHERE object_id = OBJECT_ID('[dbo].[PHIEU_BAN]')
    AND name = 'CHI_PHI_DICH_VU'
)
BEGIN
    PRINT 'Thêm cột CHI_PHI_DICH_VU vào PHIEU_BAN...'
    ALTER TABLE [dbo].[PHIEU_BAN]
    ADD [CHI_PHI_DICH_VU] [decimal](18, 0) NULL DEFAULT 0
    PRINT 'Đã thêm cột CHI_PHI_DICH_VU thành công!'
END
ELSE
BEGIN
    PRINT 'Cột CHI_PHI_DICH_VU đã tồn tại.'
END
GO
