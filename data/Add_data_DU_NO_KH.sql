USE [QLCHNongDuoc]
GO

-- =============================================
-- 1. DỌN DẸP DỮ LIỆU CŨ (Để tránh lỗi trùng lặp khi test nhiều lần)
-- =============================================
-- Xóa dữ liệu tổng hợp tháng 11 nếu đã có
DELETE FROM DU_NO_KH WHERE NAM = 2025 AND THANG = 11;

-- Xóa giao dịch tháng 11 (chọn khoảng ID giả định 9100-9200 để không xóa nhầm dữ liệu thật)
DELETE FROM PHIEU_BAN WHERE ID BETWEEN 9100 AND 9199;
DELETE FROM PHIEU_THANH_TOAN WHERE ID BETWEEN 9100 AND 9199;

-- =============================================
-- 2. CHỐT SỐ LIỆU THÁNG 10 (LÀM ĐẦU KỲ CHO THÁNG 11)
-- =============================================
-- Để nút "Tổng hợp" tháng 11 chạy đúng, hệ thống cần tìm thấy số liệu tháng 10.
-- Ta sẽ xóa và chèn cứng số liệu CUỐI KỲ tháng 10 vào bảng DU_NO_KH.

DELETE FROM DU_NO_KH WHERE NAM = 2025 AND THANG = 10;

-- KH 1 (Tèo): Cuối T10 nợ 1.300.000 -> Sang T11 đầu kỳ sẽ là 1.300.000
INSERT INTO DU_NO_KH (ID_KHACH_HANG, THANG, NAM, DAU_KY, PHAT_SINH, DA_TRA, CUOI_KY)
VALUES (1, 10, 2025, 1000000, 500000, 200000, 1300000);

-- KH 2 (Khang): Cuối T10 nợ 2.000.000 -> Sang T11 đầu kỳ sẽ là 2.000.000
INSERT INTO DU_NO_KH (ID_KHACH_HANG, THANG, NAM, DAU_KY, PHAT_SINH, DA_TRA, CUOI_KY)
VALUES (2, 10, 2025, 0, 2000000, 0, 2000000);

-- KH 3 (N&T): Cuối T10 nợ 0 -> Sang T11 đầu kỳ sẽ là 0
INSERT INTO DU_NO_KH (ID_KHACH_HANG, THANG, NAM, DAU_KY, PHAT_SINH, DA_TRA, CUOI_KY)
VALUES (3, 10, 2025, 500000, 0, 500000, 0);


-- =============================================
-- 3. TẠO GIAO DỊCH THÁNG 11/2025 (PHÁT SINH & ĐÃ TRẢ)
-- =============================================

-- --- KHÁCH HÀNG 1: TRẦN VĂN TÈO (ID 1) ---
-- Tình huống: Đang nợ 1.3tr. Mua thêm ít (200k) nhưng trả rất nhiều (1.5tr) -> Hết nợ.
INSERT INTO PHIEU_BAN (ID, ID_KHACH_HANG, NGAY_BAN, TONG_TIEN, DA_TRA, CON_NO)
VALUES (9101, 1, '2025-11-05', 200000, 0, 200000); -- Mua chịu 200k

INSERT INTO PHIEU_THANH_TOAN (ID, NGAY_THANH_TOAN, TONG_TIEN, ID_KHACH_HANG, GHI_CHU)
VALUES (9102, '2025-11-20', 1500000, 1, N'Thanh toán sạch nợ'); -- Trả 1.5tr


-- --- KHÁCH HÀNG 2: LÊ ANH KHANG (ID 2) ---
-- Tình huống: Đang nợ 2tr. Mua thêm 1tr. Không trả đồng nào -> Nợ chồng chất.
INSERT INTO PHIEU_BAN (ID, ID_KHACH_HANG, NGAY_BAN, TONG_TIEN, DA_TRA, CON_NO)
VALUES (9103, 2, '2025-11-10', 1000000, 0, 1000000); -- Mua chịu thêm 1tr


-- --- KHÁCH HÀNG 3: CỬA HÀNG N&T (ID 3) ---
-- Tình huống: Đang sạch nợ (0). Mua lô hàng lớn 3tr. Trả trước 1tr -> Còn nợ 2tr.
INSERT INTO PHIEU_BAN (ID, ID_KHACH_HANG, NGAY_BAN, TONG_TIEN, DA_TRA, CON_NO)
VALUES (9104, 3, '2025-11-15', 3000000, 0, 3000000); -- Mua chịu 3tr

INSERT INTO PHIEU_THANH_TOAN (ID, NGAY_THANH_TOAN, TONG_TIEN, ID_KHACH_HANG, GHI_CHU)
VALUES (9105, '2025-11-16', 1000000, 3, N'Trả bớt tiền hàng'); -- Trả 1tr

-- Cập nhật tham số ID để tránh lỗi trùng
UPDATE THAM_SO SET PHIEU_BAN = 9110, PHIEU_THANH_TOAN = 9110;

PRINT N'=== ĐÃ TẠO DỮ LIỆU TEST CHO THÁNG 11/2025 ===';


SELECT *
FROM DU_NO_KH;
