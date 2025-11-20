USE [QLCHNongDuoc]
GO

DECLARE @IdDonViTinh INT, @IdPhieuNhap INT

-- Lấy đơn vị tính đầu tiên trong bảng
SELECT TOP 1 @IdDonViTinh = ID FROM DON_VI_TINH
IF @IdDonViTinh IS NULL
BEGIN
    PRINT 'Lỗi: Không có đơn vị tính nào trong database!'
    RETURN
END

-- Lấy phiếu nhập đầu tiên hoặc tạo phiếu nhập test
SELECT TOP 1 @IdPhieuNhap = ID FROM PHIEU_NHAP ORDER BY ID DESC
IF @IdPhieuNhap IS NULL
BEGIN
    -- Tạo phiếu nhập test nếu chưa có
    DECLARE @IdNCC INT
    SELECT TOP 1 @IdNCC = ID FROM NHA_CUNG_CAP

    INSERT INTO PHIEU_NHAP (ID, ID_NHA_CUNG_CAP, NGAY_NHAP, TONG_TIEN, DA_TRA, CON_NO)
    VALUES (9999, @IdNCC, '2025-11-01', 0, 0, 0)

    SET @IdPhieuNhap = 9999
END

PRINT 'Sử dụng: Đơn vị tính ID = ' + CAST(@IdDonViTinh AS VARCHAR)
PRINT 'Sử dụng: Phiếu nhập ID = ' + CAST(@IdPhieuNhap AS VARCHAR)
PRINT ''

-- Xóa nếu đã tồn tại
DELETE FROM MA_SAN_PHAM WHERE ID LIKE 'TEST-1%'
DELETE FROM SAN_PHAM WHERE ID IN (101, 102)

-- Sản phẩm TEST 1: Thuốc trừ sâu ABC
INSERT INTO SAN_PHAM (ID, TEN_SAN_PHAM, SO_LUONG, DON_GIA_NHAP, GIA_BAN_SI, GIA_BAN_LE, ID_DON_VI_TINH)
VALUES (101, N'Thuốc trừ sâu ABC', 120, 42000, 48000, 55000, @IdDonViTinh);

-- Sản phẩm TEST 2: Phân bón XYZ
INSERT INTO SAN_PHAM (ID, TEN_SAN_PHAM, SO_LUONG, DON_GIA_NHAP, GIA_BAN_SI, GIA_BAN_LE, ID_DON_VI_TINH)
VALUES (102, N'Phân bón XYZ', 110, 66364, 72000, 78000, @IdDonViTinh);

GO

-- =============================================
-- 3. LẤY LẠI BIẾN (sau GO)
-- =============================================
DECLARE @IdPhieuNhap INT
SELECT TOP 1 @IdPhieuNhap = ID FROM PHIEU_NHAP ORDER BY ID DESC

GO

-- Lấy ID phiếu nhập có sẵn
DECLARE @IdPhieuNhap INT
SELECT TOP 1 @IdPhieuNhap = ID FROM PHIEU_NHAP ORDER BY ID DESC

IF @IdPhieuNhap IS NULL
BEGIN
    PRINT 'Lỗi: Không có phiếu nhập nào! Vui lòng tạo phiếu nhập trước.'
    RETURN
END

PRINT 'Sử dụng Phiếu nhập ID = ' + CAST(@IdPhieuNhap AS VARCHAR)
PRINT ''

-- TEST CASE 1: Sản phẩm 101 - 5 lô với ngày hết hạn khác nhau
-- Lô 1: Hết hạn SỚM NHẤT (tháng 1/2026) - SL: 10
INSERT INTO MA_SAN_PHAM (ID, ID_SAN_PHAM, ID_PHIEU_NHAP, SO_LUONG, DON_GIA_NHAP, NGAY_NHAP, NGAY_SAN_XUAT, NGAY_HET_HAN)
VALUES ('TEST-101-L1', 101, @IdPhieuNhap, 10, 40000, '2025-11-01', '2025-10-01', '2026-01-15');

-- Lô 2: Hết hạn SAU (tháng 3/2026) - SL: 25
INSERT INTO MA_SAN_PHAM (ID, ID_SAN_PHAM, ID_PHIEU_NHAP, SO_LUONG, DON_GIA_NHAP, NGAY_NHAP, NGAY_SAN_XUAT, NGAY_HET_HAN)
VALUES ('TEST-101-L2', 101, @IdPhieuNhap, 25, 42000, '2025-11-05', '2025-10-05', '2026-03-20');

-- Lô 3: Hết hạn GẦN NHẤT (tháng 12/2025) - SL: 5 ← Test: Không đủ số lượng
INSERT INTO MA_SAN_PHAM (ID, ID_SAN_PHAM, ID_PHIEU_NHAP, SO_LUONG, DON_GIA_NHAP, NGAY_NHAP, NGAY_SAN_XUAT, NGAY_HET_HAN)
VALUES ('TEST-101-L3', 101, @IdPhieuNhap, 5, 38000, '2025-11-10', '2025-10-10', '2025-12-25');

-- Lô 4: Hết hạn SAU (tháng 5/2026) - SL: 50
INSERT INTO MA_SAN_PHAM (ID, ID_SAN_PHAM, ID_PHIEU_NHAP, SO_LUONG, DON_GIA_NHAP, NGAY_NHAP, NGAY_SAN_XUAT, NGAY_HET_HAN)
VALUES ('TEST-101-L4', 101, @IdPhieuNhap, 50, 45000, '2025-11-15', '2025-10-15', '2026-05-30');

-- Lô 5: Hết hạn TRUNG BÌNH (tháng 2/2026) - SL: 30
INSERT INTO MA_SAN_PHAM (ID, ID_SAN_PHAM, ID_PHIEU_NHAP, SO_LUONG, DON_GIA_NHAP, NGAY_NHAP, NGAY_SAN_XUAT, NGAY_HET_HAN)
VALUES ('TEST-101-L5', 101, @IdPhieuNhap, 30, 44000, '2025-11-20', '2025-10-20', '2026-02-28');

-- TEST CASE 2: Sản phẩm 102 - 3 lô với giá nhập KHÁC NHAU
-- Lô 1: Giá thấp, số lượng ít
INSERT INTO MA_SAN_PHAM (ID, ID_SAN_PHAM, ID_PHIEU_NHAP, SO_LUONG, DON_GIA_NHAP, NGAY_NHAP, NGAY_SAN_XUAT, NGAY_HET_HAN)
VALUES ('TEST-102-L1', 102, @IdPhieuNhap, 10, 50000, '2025-11-01', '2025-10-01', '2026-02-01');

-- Lô 2: Giá cao, số lượng nhiều
INSERT INTO MA_SAN_PHAM (ID, ID_SAN_PHAM, ID_PHIEU_NHAP, SO_LUONG, DON_GIA_NHAP, NGAY_NHAP, NGAY_SAN_XUAT, NGAY_HET_HAN)
VALUES ('TEST-102-L2', 102, @IdPhieuNhap, 80, 70000, '2025-11-05', '2025-10-05', '2026-03-15');

-- Lô 3: Giá trung bình, số lượng vừa
INSERT INTO MA_SAN_PHAM (ID, ID_SAN_PHAM, ID_PHIEU_NHAP, SO_LUONG, DON_GIA_NHAP, NGAY_NHAP, NGAY_SAN_XUAT, NGAY_HET_HAN)
VALUES ('TEST-102-L3', 102, @IdPhieuNhap, 20, 60000, '2025-11-10', '2025-10-10', '2026-01-10');

GO

-- =============================================
-- 3. CẬP NHẬT TỔNG SỐ LƯỢNG TRONG SAN_PHAM
-- =============================================

UPDATE SAN_PHAM
SET SO_LUONG = (SELECT SUM(SO_LUONG) FROM MA_SAN_PHAM WHERE ID_SAN_PHAM = 101)
WHERE ID = 101;

UPDATE SAN_PHAM
SET SO_LUONG = (SELECT SUM(SO_LUONG) FROM MA_SAN_PHAM WHERE ID_SAN_PHAM = 102)
WHERE ID = 102;

GO

PRINT '========================================='
PRINT 'SAMPLE DATA ĐÃ TẠO XONG!'
PRINT '========================================='
PRINT ''

-- Sản phẩm 101: Test FEFO (Lô hết hạn sớm nhất)
PRINT 'Sản phẩm 101 - Thuốc trừ sâu ABC:'
PRINT '  → 5 lô, sắp xếp theo ngày hết hạn:'
SELECT ID, SO_LUONG, DON_GIA_NHAP,
       CONVERT(varchar, NGAY_HET_HAN, 103) AS NGAY_HET_HAN
FROM MA_SAN_PHAM
WHERE ID_SAN_PHAM = 101
ORDER BY NGAY_HET_HAN ASC, NGAY_NHAP ASC;

PRINT ''

-- Sản phẩm 102: Test Weighted Average
PRINT 'Sản phẩm 102 - Phân bón XYZ:'
PRINT '  → 3 lô, giá nhập khác nhau:'
SELECT ID, SO_LUONG, DON_GIA_NHAP,
       (SO_LUONG * DON_GIA_NHAP) AS THANH_TIEN,
       CONVERT(varchar, NGAY_HET_HAN, 103) AS NGAY_HET_HAN
FROM MA_SAN_PHAM
WHERE ID_SAN_PHAM = 102
ORDER BY NGAY_HET_HAN ASC;

-- Tính weighted average
DECLARE @TongTien BIGINT, @TongSL INT, @GiaTB INT
SELECT @TongTien = SUM(SO_LUONG * DON_GIA_NHAP),
       @TongSL = SUM(SO_LUONG)
FROM MA_SAN_PHAM WHERE ID_SAN_PHAM = 102

SET @GiaTB = @TongTien / @TongSL

PRINT ''
PRINT 'Giá bình quân gia quyền (Weighted Average):'
PRINT '  → Tổng tiền: ' + CAST(@TongTien AS VARCHAR) + 'đ'
PRINT '  → Tổng SL: ' + CAST(@TongSL AS VARCHAR)
PRINT '  → Giá TB: ' + CAST(@GiaTB AS VARCHAR) + 'đ/đơn vị'

PRINT ''
PRINT '========================================='
PRINT 'TEST CASES:'
PRINT '========================================='
PRINT '1. FEFO Test (SP 101):'
PRINT '   → Mua 20 cái → Lấy lô TEST-101-L3 (5) + TEST-101-L1 (15)'
PRINT '   → Mua 40 cái → Lấy L3(5) + L1(10) + L5(25) = 3 lô'
PRINT ''
PRINT '2. Weighted Average Test (SP 102):'
PRINT '   → Simple avg: (50000+70000+60000)/3 = 60,000đ (SAI!)'
PRINT '   → Weighted avg: ' + CAST(@GiaTB AS VARCHAR) + 'đ (ĐÚNG!)'
PRINT ''
PRINT '========================================='

GO
