-- =============================================
-- DỮ LIỆU TEST CHỨC NĂNG DƯ NỢ KHÁCH HÀNG
-- Test với Tháng 11/2025
-- =============================================

USE [QLCHNongDuoc]
GO

-- Xóa dữ liệu test cũ (nếu có)
DELETE FROM [DU_NO_KH] WHERE ID_KHACH_HANG >= 100
DELETE FROM [PHIEU_THANH_TOAN] WHERE ID >= 2001
DELETE FROM [CHI_TIET_PHIEU_BAN] WHERE ID_PHIEU_BAN >= 1001
DELETE FROM [PHIEU_BAN] WHERE ID >= 1001
DELETE FROM [KHACH_HANG] WHERE ID >= 100
GO

-- =============================================
-- 1. TẠO KHÁCH HÀNG TEST
-- =============================================

-- KH 100: Có dư nợ tháng trước
INSERT INTO [KHACH_HANG] (ID, HO_TEN, DIA_CHI, DIEN_THOAI, LOAI_KH)
VALUES (100, N'Nguyễn Văn A', N'123 Lê Lợi, Q1, TP.HCM', '0901234567', 0)

-- KH 101: KHÁCH MỚI - không có dữ liệu tháng trước (trường hợp chính cần test)
INSERT INTO [KHACH_HANG] (ID, HO_TEN, DIA_CHI, DIEN_THOAI, LOAI_KH)
VALUES (101, N'Trần Thị B', N'456 Nguyễn Huệ, Q1, TP.HCM', '0912345678', 0)

-- KH 102: Khách sỉ - dư nợ lớn
INSERT INTO [KHACH_HANG] (ID, HO_TEN, DIA_CHI, DIEN_THOAI, LOAI_KH)
VALUES (102, N'Công ty ABC', N'789 Hai Bà Trưng, Q3, TP.HCM', '0923456789', 1)

-- KH 103: Trả ngay khi mua
INSERT INTO [KHACH_HANG] (ID, HO_TEN, DIA_CHI, DIEN_THOAI, LOAI_KH)
VALUES (103, N'Lê Văn C', N'321 CMT8, Q10, TP.HCM', '0934567890', 0)

-- KH 104: Dư có (trả thừa)
INSERT INTO [KHACH_HANG] (ID, HO_TEN, DIA_CHI, DIEN_THOAI, LOAI_KH)
VALUES (104, N'Phạm Thị D', N'654 Võ Văn Tần, Q3, TP.HCM', '0945678901', 0)
GO

-- =============================================
-- 2. DƯ NỢ THÁNG TRƯỚC (Tháng 10/2025)
-- =============================================

-- KH 100: Nợ cuối kỳ 1 triệu
INSERT INTO [DU_NO_KH] (ID_KHACH_HANG, THANG, NAM, DAU_KY, PHAT_SINH, DA_TRA, CUOI_KY)
VALUES (100, 10, 2025, 500000, 2000000, 1500000, 1000000)

-- KH 102: Nợ cuối kỳ 5 triệu
INSERT INTO [DU_NO_KH] (ID_KHACH_HANG, THANG, NAM, DAU_KY, PHAT_SINH, DA_TRA, CUOI_KY)
VALUES (102, 10, 2025, 3000000, 8000000, 6000000, 5000000)

-- KH 103: Đã trả hết
INSERT INTO [DU_NO_KH] (ID_KHACH_HANG, THANG, NAM, DAU_KY, PHAT_SINH, DA_TRA, CUOI_KY)
VALUES (103, 10, 2025, 1000000, 2000000, 3000000, 0)

-- KH 104: Dư có -500k
INSERT INTO [DU_NO_KH] (ID_KHACH_HANG, THANG, NAM, DAU_KY, PHAT_SINH, DA_TRA, CUOI_KY)
VALUES (104, 10, 2025, 0, 1500000, 2000000, -500000)

-- *** LƯU Ý: KH 101 KHÔNG có dữ liệu tháng 10 (khách mới) ***
GO

-- =============================================
-- 3. PHIẾU BÁN THÁNG 11/2025
-- =============================================

-- KH 100: Mua 3 lần
INSERT INTO [PHIEU_BAN] (ID, ID_KHACH_HANG, NGAY_BAN, TONG_TIEN, DA_TRA, CON_NO)
VALUES 
(1001, 100, '2025-11-05', 1500000, 500000, 1000000),
(1002, 100, '2025-11-12', 2000000, 0, 2000000),
(1003, 100, '2025-11-18', 1500000, 500000, 1000000)

-- KH 101: KHÁCH MỚI - mua lần đầu
INSERT INTO [PHIEU_BAN] (ID, ID_KHACH_HANG, NGAY_BAN, TONG_TIEN, DA_TRA, CON_NO)
VALUES 
(1004, 101, '2025-11-10', 3000000, 1000000, 2000000),
(1005, 101, '2025-11-15', 2500000, 500000, 2000000)

-- KH 102: Đơn lớn
INSERT INTO [PHIEU_BAN] (ID, ID_KHACH_HANG, NGAY_BAN, TONG_TIEN, DA_TRA, CON_NO)
VALUES 
(1006, 102, '2025-11-08', 15000000, 5000000, 10000000),
(1007, 102, '2025-11-20', 8000000, 2000000, 6000000)

-- KH 103: Trả ngay
INSERT INTO [PHIEU_BAN] (ID, ID_KHACH_HANG, NGAY_BAN, TONG_TIEN, DA_TRA, CON_NO)
VALUES 
(1008, 103, '2025-11-07', 1800000, 1800000, 0),
(1009, 103, '2025-11-14', 2200000, 2200000, 0)

-- KH 104: Mua ít
INSERT INTO [PHIEU_BAN] (ID, ID_KHACH_HANG, NGAY_BAN, TONG_TIEN, DA_TRA, CON_NO)
VALUES (1010, 104, '2025-11-11', 800000, 0, 800000)
GO

-- =============================================
-- 4. PHIẾU THANH TOÁN THÁNG 11/2025
-- =============================================

-- KH 100: Trả 2 lần
INSERT INTO [PHIEU_THANH_TOAN] (ID, ID_KHACH_HANG, NGAY_THANH_TOAN, TONG_TIEN, GHI_CHU)
VALUES 
(2001, 100, '2025-11-10', 1500000, N'Trả nợ tháng trước'),
(2002, 100, '2025-11-19', 1000000, N'Trả tiếp')

-- KH 101: KHÁCH MỚI - trả 1 lần
INSERT INTO [PHIEU_THANH_TOAN] (ID, ID_KHACH_HANG, NGAY_THANH_TOAN, TONG_TIEN, GHI_CHU)
VALUES (2003, 101, '2025-11-16', 2000000, N'Thanh toán')

-- KH 102: Trả nhiều lần
INSERT INTO [PHIEU_THANH_TOAN] (ID, ID_KHACH_HANG, NGAY_THANH_TOAN, TONG_TIEN, GHI_CHU)
VALUES 
(2004, 102, '2025-11-05', 3000000, N'Trả nợ cũ'),
(2005, 102, '2025-11-15', 5000000, N'Trả tiếp'),
(2006, 102, '2025-11-22', 4000000, N'Thanh toán đơn mới')

-- KH 104: Trả nhiều
INSERT INTO [PHIEU_THANH_TOAN] (ID, ID_KHACH_HANG, NGAY_THANH_TOAN, TONG_TIEN, GHI_CHU)
VALUES (2007, 104, '2025-11-12', 1000000, N'Trả nợ')
GO

-- =============================================
-- KẾT QUẢ DỰ KIẾN SAU KHI TỔNG HỢP THÁNG 11/2025
-- =============================================
/*
┌──────────────┬──────────┬───────────┬─────────┬──────────┬────────────────────┐
│ Khách Hàng   │ Đầu Kỳ  │ Phát Sinh │ Đã Trả  │ Cuối Kỳ │ Ghi Chú            │
├──────────────┼──────────┼───────────┼─────────┼──────────┼────────────────────┤
│ Nguyễn Văn A │ 1,000,000│ 4,000,000 │2,500,000│ 2,500,000│ Có nợ cũ           │
│ Trần Thị B   │         0│ 4,000,000 │2,000,000│ 2,000,000│ ⭐ KHÁCH MỚI       │
│ Công ty ABC  │ 5,000,000│16,000,000 │12,000,000│9,000,000│ Khách sỉ           │
│ Lê Văn C     │         0│         0 │        0│         0│ Không nợ           │
│ Phạm Thị D   │  -500,000│   800,000 │1,000,000│  -700,000│ Dư có              │
└──────────────┴──────────┴───────────┴─────────┴──────────┴────────────────────┘

CÁCH TEST:
1. Chạy script này
2. Mở form Dư Nợ Khách Hàng
3. Chọn Tháng: 11, Năm: 2025
4. Nhấn "Tổng Hợp"
5. Kiểm tra KH 101 (Trần Thị B) có Đầu kỳ = 0 (vì là khách mới)
*/

PRINT '✓ Đã tạo xong dữ liệu test'
PRINT '→ Chọn Tháng 11, Năm 2025 để test'
GO
