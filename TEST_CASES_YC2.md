# TEST CASES CHO YC2 - CẤU HÌNH XUẤT KHO

**Yêu cầu kiểm thử:**
1. Cấu hình phương pháp xuất kho (FIFO/Chỉ định)
2. Tự động chọn lô theo ngày hết hạn (FEFO)
3. Tính giá xuất theo phương pháp (Bình quân gia quyền/FIFO)
4. Hiển thị lô và ngày hết hạn trong phiếu bán
5. Tự động trừ tồn kho sau khi lưu

---

## CHUẨN BỊ DỮ LIỆU TEST

### Sản Phẩm Test: "Thuốc trừ sâu ABC"

**Thông tin sản phẩm:**
- ID: 1
- Tên: Thuốc trừ sâu ABC
- Đơn vị tính: Chai
- Giá bán lẻ: 50,000 VNĐ
- Giá bán sỉ: 45,000 VNĐ

**Dữ liệu lô nhập (setup trong database):**

```sql
-- Xóa dữ liệu cũ (nếu có)
DELETE FROM CHI_TIET_PHIEU_BAN WHERE ID_MA_SAN_PHAM IN (
    SELECT ID FROM MA_SAN_PHAM WHERE ID_SAN_PHAM = 1
)
DELETE FROM MA_SAN_PHAM WHERE ID_SAN_PHAM = 1

-- Thêm 3 lô với thông tin khác nhau
INSERT INTO MA_SAN_PHAM (ID, ID_SAN_PHAM, ID_PHIEU_NHAP, SO_LUONG, GIA_NHAP, NGAY_NHAP, NGAY_SAN_XUAT, NGAY_HET_HAN)
VALUES
-- Lô 1: Hết hạn sớm nhất (2025-12-31), giá nhập thấp
(101, 1, 1, 50, 30000, '2025-11-01', '2025-10-01', '2025-12-31'),

-- Lô 2: Hết hạn giữa (2026-03-31), giá nhập trung bình
(102, 1, 2, 80, 35000, '2025-11-05', '2025-10-05', '2026-03-31'),

-- Lô 3: Hết hạn muộn nhất (2026-06-30), giá nhập cao
(103, 1, 3, 100, 40000, '2025-11-10', '2025-10-10', '2026-06-30')
```

**Tổng tồn kho ban đầu:**
- Lô 1 (LOT-1-001): 50 chai @ 30,000 VNĐ - HSD: 31/12/2025
- Lô 2 (LOT-2-001): 80 chai @ 35,000 VNĐ - HSD: 31/03/2026
- Lô 3 (LOT-3-001): 100 chai @ 40,000 VNĐ - HSD: 30/06/2026
- **Tổng:** 230 chai

**Giá bình quân gia quyền:**
```
Weighted Average = (50×30,000 + 80×35,000 + 100×40,000) / (50+80+100)
                 = (1,500,000 + 2,800,000 + 4,000,000) / 230
                 = 8,300,000 / 230
                 = 36,087 VNĐ/chai
```

---

## TEST SUITE 1: CẤU HÌNH PHƯƠNG PHÁP XUẤT KHO

### TC1.1: Kiểm tra cấu hình mặc định

**Mục đích:** Xác nhận hệ thống khởi động với cấu hình đúng

**Bước thực hiện:**
1. Mở form Cấu hình (nếu có) hoặc kiểm tra database
2. Kiểm tra giá trị `PHUONG_PHAP_XUAT_KHO` trong table `THAM_SO`

**Query kiểm tra:**
```sql
SELECT PHUONG_PHAP_XUAT_KHO, PHUONG_PHAP_TINH_GIA_XUAT
FROM THAM_SO
```

**Kỳ vọng:**
- `PHUONG_PHAP_XUAT_KHO` = 'FIFO'
- `PHUONG_PHAP_TINH_GIA_XUAT` = 'Average'

**Kết quả:** ☐ Pass ☐ Fail

---

### TC1.2: Thay đổi cấu hình xuất kho

**Mục đích:** Kiểm tra có thể thay đổi cấu hình được không

**Bước thực hiện:**
1. Sửa cấu hình bằng code hoặc SQL:
```csharp
ThamSo.PhuongPhapXuatKho = "CHI_DINH";
```

Hoặc:
```sql
UPDATE THAM_SO SET PHUONG_PHAP_XUAT_KHO = 'CHI_DINH'
```

2. Kiểm tra lại:
```sql
SELECT PHUONG_PHAP_XUAT_KHO FROM THAM_SO
```

**Kỳ vọng:**
- Giá trị đã thay đổi thành 'CHI_DINH'

**Kết quả:** ☐ Pass ☐ Fail

---

## TEST SUITE 2: FEFO - CHỌN LÔ TỰ ĐỘNG

**Thiết lập:**
```sql
UPDATE THAM_SO SET PHUONG_PHAP_XUAT_KHO = 'FIFO'
```

Đảm bảo dữ liệu test đã được setup (3 lô như trên).

---

### TC2.1: Xuất ít hơn lô đầu tiên (Happy Path)

**Mục đích:** Kiểm tra hệ thống chọn đúng lô hết hạn sớm nhất

**Bước thực hiện:**
1. Mở form `frmBanLe` hoặc `frmBanSi`
2. Chọn khách hàng bất kỳ
3. Chọn sản phẩm "Thuốc trừ sâu ABC"
4. Nhập số lượng: **20 chai**
5. Nhập giá bán: 50,000 VNĐ
6. Bấm "Thêm"
7. Kiểm tra DataGridView chi tiết phiếu bán

**Kỳ vọng:**
- Có 1 dòng mới trong DataGridView
- Cột "Lô": LOT-1-001 (hoặc mã lô tương ứng với lô 1)
- Cột "Số lượng": 20
- Cột "Ngày hết hạn": 31/12/2025
- KHÔNG có lỗi/cảnh báo

**Query kiểm tra sau khi Lưu:**
```sql
-- Kiểm tra chi tiết phiếu bán
SELECT PB.ID, PB.NGAY_BAN, CT.ID_MA_SAN_PHAM, CT.SO_LUONG, CT.DON_GIA,
       MSP.MA_SO, MSP.NGAY_HET_HAN
FROM PHIEU_BAN PB
JOIN CHI_TIET_PHIEU_BAN CT ON PB.ID = CT.ID_PHIEU_BAN
JOIN MA_SAN_PHAM MSP ON CT.ID_MA_SAN_PHAM = MSP.ID
WHERE PB.ID = (SELECT MAX(ID) FROM PHIEU_BAN)

-- Kiểm tra tồn kho lô 1
SELECT ID, SO_LUONG FROM MA_SAN_PHAM WHERE ID = 101
```

**Kỳ vọng sau khi Lưu:**
- Chi tiết phiếu bán: 1 dòng, lô 101, số lượng 20
- Tồn lô 1: **30 chai** (50 - 20)
- Tồn lô 2: **80 chai** (không đổi)
- Tồn lô 3: **100 chai** (không đổi)

**Kết quả:** ☐ Pass ☐ Fail

**Ghi chú:**
___________________________________________

---

### TC2.2: Xuất hết lô đầu tiên

**Mục đích:** Kiểm tra xuất đúng số lượng bằng tồn kho lô

**Bước thực hiện:**
1. Tạo phiếu bán mới
2. Chọn sản phẩm "Thuốc trừ sâu ABC"
3. Nhập số lượng: **50 chai** (bằng tồn lô 1)
4. Bấm "Thêm"

**Kỳ vọng trước khi Lưu:**
- Có 1 dòng trong DataGridView
- Lô: LOT-1-001
- Số lượng: 50

**Kỳ vọng sau khi Lưu:**
```sql
SELECT SO_LUONG FROM MA_SAN_PHAM WHERE ID = 101
```
- Tồn lô 1: **0 chai** (50 - 50)
- Tồn lô 2: 80 chai (không đổi)
- Tồn lô 3: 100 chai (không đổi)

**Kết quả:** ☐ Pass ☐ Fail

---

### TC2.3: Xuất nhiều hơn 1 lô - Phân bổ tự động

**Mục đích:** Kiểm tra hệ thống tự động lấy từ nhiều lô

**Bước thực hiện:**
1. Tạo phiếu bán mới
2. Chọn sản phẩm "Thuốc trừ sâu ABC"
3. Nhập số lượng: **100 chai**
4. Bấm "Thêm"

**Kỳ vọng trước khi Lưu:**
- Có **2 dòng** trong DataGridView:
  - Dòng 1: Lô 1 (LOT-1-001), Số lượng: **50**, HSD: 31/12/2025
  - Dòng 2: Lô 2 (LOT-2-001), Số lượng: **50**, HSD: 31/03/2026

**Giải thích:**
- Lô 1 còn 50 → Lấy hết 50
- Cần thêm 50 → Lấy từ lô 2 (hết hạn sớm thứ 2)

**Kỳ vọng sau khi Lưu:**
```sql
SELECT ID, SO_LUONG FROM MA_SAN_PHAM WHERE ID IN (101, 102, 103)
```
- Tồn lô 1: **0 chai** (hết)
- Tồn lô 2: **30 chai** (80 - 50)
- Tồn lô 3: **100 chai** (không đổi)

**Kết quả:** ☐ Pass ☐ Fail

---

### TC2.4: Xuất gần hết tồn kho - Edge case

**Mục đích:** Kiểm tra phân bổ qua 3 lô

**Bước thực hiện:**
1. Tạo phiếu bán mới
2. Nhập số lượng: **200 chai**
3. Bấm "Thêm"

**Kỳ vọng trước khi Lưu:**
- Có **3 dòng** trong DataGridView:
  - Dòng 1: Lô 1, SL: 50, HSD: 31/12/2025
  - Dòng 2: Lô 2, SL: 80, HSD: 31/03/2026
  - Dòng 3: Lô 3, SL: 70, HSD: 30/06/2026

**Kỳ vọng sau khi Lưu:**
- Tồn lô 1: 0 (hết)
- Tồn lô 2: 0 (hết)
- Tồn lô 3: **30 chai** (100 - 70)

**Kết quả:** ☐ Pass ☐ Fail

---

### TC2.5: Xuất nhiều hơn tồn kho - Error handling

**Mục đích:** Kiểm tra cảnh báo khi không đủ hàng

**Bước thực hiện:**
1. Tạo phiếu bán mới
2. Nhập số lượng: **250 chai** (nhiều hơn tổng tồn 230)
3. Bấm "Thêm"

**Kỳ vọng:**
- Hiển thị MessageBox cảnh báo:
  - Nội dung: "Không đủ hàng trong kho!"
  - Chi tiết: "Cần: 250 chai, Có sẵn: 230 chai, Thiếu: 20 chai"
- KHÔNG thêm dòng vào DataGridView
- Tồn kho KHÔNG thay đổi

**Kết quả:** ☐ Pass ☐ Fail

---

### TC2.6: Xuất sản phẩm chưa có lô - Error handling

**Mục đích:** Kiểm tra cảnh báo khi sản phẩm chưa nhập hàng

**Bước thực hiện:**
1. Thêm sản phẩm mới "Thuốc XYZ" (chưa có lô nào)
```sql
INSERT INTO SAN_PHAM (ID, TEN_SAN_PHAM, SO_LUONG, DON_GIA_NHAP, GIA_BAN_LE, GIA_BAN_SI, ID_DON_VI_TINH)
VALUES (999, 'Thuốc XYZ', 0, 0, 50000, 45000, 1)
```
2. Tạo phiếu bán, chọn "Thuốc XYZ"
3. Nhập số lượng: 10
4. Bấm "Thêm"

**Kỳ vọng:**
- MessageBox cảnh báo: "Sản phẩm chưa có lô hàng trong kho!"
- KHÔNG thêm dòng vào DataGridView

**Kết quả:** ☐ Pass ☐ Fail

---

## TEST SUITE 3: TÍNH GIÁ XUẤT

**Reset dữ liệu:** Chạy lại script setup ở đầu file để có đủ 3 lô.

---

### TC3.1: Tính giá bình quân gia quyền

**Mục đích:** Kiểm tra công thức weighted average

**Thiết lập:**
```sql
UPDATE THAM_SO SET PHUONG_PHAP_TINH_GIA_XUAT = 'Average'
```

**Bước thực hiện:**
1. Mở form bán hàng
2. Chọn sản phẩm "Thuốc trừ sâu ABC"
3. Quan sát trường "Giá xuất" (txtGiaBQGQ hoặc tương tự)

**Kỳ vọng:**
- Giá xuất hiển thị: **36,087 VNĐ** (hoặc 36,086-36,088 do làm tròn)

**Công thức kiểm tra:**
```
(50×30,000 + 80×35,000 + 100×40,000) / 230 = 36,087 VNĐ
```

**Query kiểm tra:**
```sql
SELECT
    SUM(SO_LUONG * GIA_NHAP) / SUM(SO_LUONG) AS GIA_BINH_QUAN
FROM MA_SAN_PHAM
WHERE ID_SAN_PHAM = 1 AND SO_LUONG > 0
```

**Kết quả:** ☐ Pass ☐ Fail

**Giá trị thực tế:** ___________

---

### TC3.2: Tính giá theo FIFO

**Mục đích:** Kiểm tra lấy giá lô đầu tiên

**Thiết lập:**
```sql
UPDATE THAM_SO SET PHUONG_PHAP_TINH_GIA_XUAT = 'FIFO'
```

**Bước thực hiện:**
1. Mở form bán hàng
2. Chọn sản phẩm "Thuốc trừ sâu ABC"
3. Quan sát trường "Giá xuất"

**Kỳ vọng:**
- Giá xuất hiển thị: **30,000 VNĐ** (giá lô 1 - hết hạn sớm nhất)

**Kết quả:** ☐ Pass ☐ Fail

---

### TC3.3: Giá xuất thay đổi sau khi xuất hết lô đầu

**Mục đích:** Kiểm tra giá FIFO cập nhật động

**Điều kiện tiên quyết:** Lô 1 đã hết (SO_LUONG = 0)

**Bước thực hiện:**
1. Xuất hết lô 1 (50 chai)
2. Tạo phiếu bán mới
3. Chọn sản phẩm "Thuốc trừ sâu ABC"
4. Quan sát giá xuất

**Kỳ vọng (mode FIFO):**
- Giá xuất: **35,000 VNĐ** (giá lô 2 - bây giờ là lô sớm nhất)

**Kỳ vọng (mode Average):**
- Giá xuất: **38,125 VNĐ**
```
(80×35,000 + 100×40,000) / (80+100) = 38,125 VNĐ
```

**Kết quả:** ☐ Pass ☐ Fail

---

## TEST SUITE 4: HIỂN THỊ TRONG PHIẾU BÁN

### TC4.1: Kiểm tra DataGridView có cột Lô và HSD

**Mục đích:** Xác nhận UI hiển thị đầy đủ thông tin

**Bước thực hiện:**
1. Mở form bán hàng (frmBanLe hoặc frmBanSi)
2. Kiểm tra DataGridView chi tiết phiếu bán

**Kỳ vọng:**
- DataGridView có các cột:
  - ☐ Tên sản phẩm
  - ☐ Mã lô / Số lô
  - ☐ Ngày hết hạn
  - ☐ Số lượng
  - ☐ Đơn giá
  - ☐ Thành tiền

**Kết quả:** ☐ Pass ☐ Fail

---

### TC4.2: Thông tin lô hiển thị đúng trong DataGridView

**Bước thực hiện:**
1. Thêm sản phẩm vào phiếu bán (số lượng 20)
2. Kiểm tra dòng vừa thêm

**Kỳ vọng:**
- Cột "Mã lô": Hiển thị "LOT-1-001" (không phải ID số)
- Cột "Ngày HSD": Hiển thị "31/12/2025" (định dạng dd/MM/yyyy)
- Cột "Số lượng": 20
- Dữ liệu rõ ràng, dễ đọc

**Kết quả:** ☐ Pass ☐ Fail

---

### TC4.3: In phiếu bán có thông tin lô

**Mục đích:** Kiểm tra report (nếu có)

**Bước thực hiện:**
1. Tạo và lưu phiếu bán
2. Bấm "In phiếu" hoặc "Preview"

**Kỳ vọng (nếu có chức năng in):**
- Report hiển thị:
  - ☐ Mã lô cho từng sản phẩm
  - ☐ Ngày hết hạn
  - ☐ Số lượng từng lô (nếu xuất nhiều lô)

**Kết quả:** ☐ Pass ☐ Fail ☐ N/A (chưa implement)

---

## TEST SUITE 5: TỰ ĐỘNG TRỪ TỒN KHO

### TC5.1: Tồn kho giảm đúng sau khi Lưu

**Bước thực hiện:**
1. Kiểm tra tồn kho trước khi bán:
```sql
SELECT ID, SO_LUONG FROM MA_SAN_PHAM WHERE ID_SAN_PHAM = 1
```
Ghi lại: Lô 1 = _____, Lô 2 = _____, Lô 3 = _____

2. Tạo phiếu bán: 70 chai
3. Bấm "Lưu"
4. Kiểm tra lại tồn kho

**Kỳ vọng:**
- Trước: Lô 1 = 50, Lô 2 = 80, Lô 3 = 100
- Sau: Lô 1 = 0 (50-50), Lô 2 = 60 (80-20), Lô 3 = 100 (không đổi)

**Kết quả:** ☐ Pass ☐ Fail

**Tồn kho thực tế sau khi Lưu:**
- Lô 1: _____
- Lô 2: _____
- Lô 3: _____

---

### TC5.2: Tồn kho KHÔNG thay đổi nếu chưa Lưu

**Bước thực hiện:**
1. Kiểm tra tồn kho
2. Thêm sản phẩm vào phiếu (chưa Lưu)
3. Kiểm tra lại tồn kho
4. Bấm "Hủy" hoặc đóng form

**Kỳ vọng:**
- Tồn kho KHÔNG thay đổi khi chưa Lưu
- Tồn kho KHÔNG thay đổi khi Hủy

**Kết quả:** ☐ Pass ☐ Fail

---

### TC5.3: Tồn kho đúng với nhiều giao dịch liên tiếp

**Bước thực hiện:**
1. Phiếu bán #1: Xuất 30 chai
2. Lưu → Check tồn
3. Phiếu bán #2: Xuất 40 chai
4. Lưu → Check tồn
5. Phiếu bán #3: Xuất 50 chai
6. Lưu → Check tồn

**Kỳ vọng:**
- Sau phiếu #1: Lô 1 = 20, Lô 2 = 80, Lô 3 = 100
- Sau phiếu #2: Lô 1 = 0, Lô 2 = 60, Lô 3 = 100
- Sau phiếu #3: Lô 1 = 0, Lô 2 = 10, Lô 3 = 100

**Kết quả:** ☐ Pass ☐ Fail

---

## TEST SUITE 6: CHẾ ĐỘ CHỈ ĐỊNH (Optional)

**Thiết lập:**
```sql
UPDATE THAM_SO SET PHUONG_PHAP_XUAT_KHO = 'CHI_DINH'
```

### TC6.1: User có thể chọn lô thủ công

**Bước thực hiện:**
1. Mở form bán hàng
2. Chọn sản phẩm
3. Kiểm tra xem có ComboBox/List để chọn lô không

**Kỳ vọng:**
- Có control cho phép user chọn lô
- Danh sách hiển thị các lô còn hàng
- Hiển thị: Mã lô, HSD, Tồn kho

**Kết quả:** ☐ Pass ☐ Fail ☐ N/A (chưa implement)

---

## TEST SUITE 7: PERFORMANCE & EDGE CASES

### TC7.1: Performance với nhiều lô

**Setup:**
```sql
-- Thêm 100 lô cho sản phẩm
DECLARE @i INT = 1
WHILE @i <= 100
BEGIN
    INSERT INTO MA_SAN_PHAM (ID_SAN_PHAM, SO_LUONG, GIA_NHAP, NGAY_HET_HAN)
    VALUES (1, 10, 30000 + @i * 100, DATEADD(DAY, @i, GETDATE()))
    SET @i = @i + 1
END
```

**Bước thực hiện:**
1. Chọn sản phẩm có 100 lô
2. Nhập số lượng: 500 chai
3. Đo thời gian từ lúc bấm "Thêm" đến khi hiển thị xong

**Kỳ vọng:**
- Thời gian < 2 giây
- Không bị lag, freeze UI
- Tất cả lô được chọn đúng thứ tự HSD

**Kết quả:** ☐ Pass ☐ Fail

**Thời gian thực tế:** _____ giây

---

### TC7.2: Xử lý số lượng thập phân (nếu có)

**Bước thực hiện:**
1. Nhập số lượng: 10.5 chai
2. Bấm "Thêm"

**Kỳ vọng:**
- Nếu hỗ trợ thập phân: Chấp nhận và lưu 10.5
- Nếu chỉ nguyên: Cảnh báo "Số lượng phải là số nguyên"

**Kết quả:** ☐ Pass ☐ Fail

---

### TC7.3: Concurrent access (nếu có nhiều user)

**Bước thực hiện:**
1. User A: Bắt đầu tạo phiếu bán 100 chai (chưa Lưu)
2. User B: Bắt đầu tạo phiếu bán 100 chai (chưa Lưu)
3. User A: Lưu → OK
4. User B: Lưu → ???

**Kỳ vọng:**
- User B: Cảnh báo "Không đủ hàng" (nếu tổng tồn < 200)
- Hoặc: Transaction rollback với thông báo lỗi

**Kết quả:** ☐ Pass ☐ Fail ☐ N/A (single user)

---

## TỔNG KẾT TEST

### Checklist Tổng Thể

**Chức năng cốt lõi:**
- [ ] TC2.1: Xuất ít hơn lô đầu
- [ ] TC2.3: Xuất nhiều lô
- [ ] TC2.5: Cảnh báo không đủ hàng
- [ ] TC3.1: Tính giá bình quân gia quyền
- [ ] TC3.2: Tính giá FIFO
- [ ] TC4.2: Hiển thị lô trong DataGridView
- [ ] TC5.1: Tự động trừ tồn

**UI/UX:**
- [ ] TC4.1: DataGridView có đủ cột
- [ ] Thông tin rõ ràng, dễ hiểu
- [ ] Không có lỗi hiển thị

**Error handling:**
- [ ] TC2.5: Xử lý không đủ hàng
- [ ] TC2.6: Xử lý chưa có lô
- [ ] MessageBox rõ ràng, hữu ích

**Data integrity:**
- [ ] Tồn kho chính xác
- [ ] Không mất dữ liệu
- [ ] Transaction an toàn

### Phân Loại Kết Quả

**Số test Pass:** _____ / 20

**Mức độ hoàn thành:**
- 18-20 Pass: ✅ Excellent - Đạt yêu cầu cao
- 15-17 Pass: ✅ Good - Đạt yêu cầu
- 12-14 Pass: ⚠️ Fair - Cần cải thiện
- < 12 Pass: ❌ Poor - Cần làm lại

### Bugs Phát Hiện

| # | Test Case | Mô Tả Lỗi | Mức Độ | Trạng Thái |
|---|-----------|-----------|--------|-----------|
| 1 | | | ☐ Critical ☐ Major ☐ Minor | ☐ Open ☐ Fixed |
| 2 | | | ☐ Critical ☐ Major ☐ Minor | ☐ Open ☐ Fixed |
| 3 | | | ☐ Critical ☐ Major ☐ Minor | ☐ Open ☐ Fixed |

---

## HƯỚNG DẪN SỬ DỤNG

### Cách Test Hiệu Quả

1. **Setup môi trường sạch:**
   - Backup database trước khi test
   - Chạy script setup dữ liệu
   - Verify dữ liệu đã có đủ 3 lô

2. **Test từng suite theo thứ tự:**
   - Bắt đầu từ TC1 (cấu hình)
   - Tiếp tục TC2 (FEFO)
   - Sau đó TC3, TC4, TC5...

3. **Ghi chép chi tiết:**
   - Đánh dấu ☐ Pass / ☐ Fail
   - Ghi số liệu thực tế vào chỗ trống
   - Screenshot nếu có lỗi

4. **Reset dữ liệu giữa các test:**
   - Chạy lại script setup khi cần
   - Hoặc: Restore backup

5. **Tổng hợp kết quả:**
   - Điền vào bảng tổng kết
   - List tất cả bugs phát hiện
   - Báo cáo cho team

---

## SQL SCRIPTS HỮU ÍCH

### Kiểm tra tồn kho tổng thể
```sql
SELECT
    SP.TEN_SAN_PHAM,
    MSP.ID AS ID_LO,
    MSP.MA_SO AS MA_LO,
    MSP.SO_LUONG AS TON_KHO,
    MSP.GIA_NHAP,
    MSP.NGAY_HET_HAN,
    DATEDIFF(DAY, GETDATE(), MSP.NGAY_HET_HAN) AS SO_NGAY_CON_LAI
FROM MA_SAN_PHAM MSP
JOIN SAN_PHAM SP ON MSP.ID_SAN_PHAM = SP.ID
WHERE MSP.ID_SAN_PHAM = 1
  AND MSP.SO_LUONG > 0
ORDER BY MSP.NGAY_HET_HAN ASC
```

### Kiểm tra chi tiết phiếu bán mới nhất
```sql
SELECT TOP 1
    PB.ID AS ID_PHIEU,
    PB.NGAY_BAN,
    SP.TEN_SAN_PHAM,
    MSP.MA_SO AS MA_LO,
    MSP.NGAY_HET_HAN,
    CT.SO_LUONG,
    CT.DON_GIA,
    CT.THANH_TIEN
FROM PHIEU_BAN PB
JOIN CHI_TIET_PHIEU_BAN CT ON PB.ID = CT.ID_PHIEU_BAN
JOIN MA_SAN_PHAM MSP ON CT.ID_MA_SAN_PHAM = MSP.ID
JOIN SAN_PHAM SP ON MSP.ID_SAN_PHAM = SP.ID
ORDER BY PB.ID DESC
```

### Reset dữ liệu test (cẩn thận!)
```sql
-- Xóa tất cả phiếu bán test
DELETE FROM CHI_TIET_PHIEU_BAN
WHERE ID_PHIEU_BAN IN (
    SELECT ID FROM PHIEU_BAN WHERE NGAY_BAN >= '2025-11-18'
)

DELETE FROM PHIEU_BAN WHERE NGAY_BAN >= '2025-11-18'

-- Reset tồn kho về ban đầu
UPDATE MA_SAN_PHAM SET SO_LUONG = 50 WHERE ID = 101
UPDATE MA_SAN_PHAM SET SO_LUONG = 80 WHERE ID = 102
UPDATE MA_SAN_PHAM SET SO_LUONG = 100 WHERE ID = 103
```

---

**Người test:** _________________________
**Ngày test:** _________________________
**Kết quả:** ☐ Pass ☐ Fail
**Ghi chú:** _________________________
