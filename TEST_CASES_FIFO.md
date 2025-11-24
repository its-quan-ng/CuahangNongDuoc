# KIỂM THỬ FIFO - TEST CASES CHÍNH

## 📦 CHUẨN BỊ DỮ LIỆU

### Chạy SQL Script

```bash
File: data/SampleData_TestFIFO.sql
Cách chạy: SSMS → Mở file → F5 (Execute)
```

### Dữ Liệu Đã Có

**Sản phẩm 101: Thuốc trừ sâu ABC**
- Tổng: 120 đơn vị
- Giá bán lẻ: 55,000đ

| Mã Lô | SL | Giá Nhập | Ngày Hết Hạn | Thứ tự FEFO |
|-------|----|---------:|---------------|-------------|
| TEST-101-L3 | 5 | 38,000 | 25/12/2025 | **1** (sớm nhất) |
| TEST-101-L1 | 10 | 40,000 | 15/01/2026 | **2** |
| TEST-101-L5 | 30 | 44,000 | 28/02/2026 | **3** |
| TEST-101-L2 | 25 | 42,000 | 20/03/2026 | 4 |
| TEST-101-L4 | 50 | 45,000 | 30/05/2026 | 5 (muộn nhất) |

**Sản phẩm 102: Phân bón XYZ**
- Tổng: 110 đơn vị
- Giá bán lẻ: 78,000đ

| Mã Lô | SL | Giá Nhập | Ngày Hết Hạn |
|-------|----|---------:|---------------|
| TEST-102-L3 | 20 | 60,000 | 10/01/2026 |
| TEST-102-L1 | 10 | 50,000 | 01/02/2026 |
| TEST-102-L2 | 80 | 70,000 | 15/03/2026 |

**Giá Weighted Average SP 102:**
```
= (20×60,000 + 10×50,000 + 80×70,000) / (20+10+80)
= (1,200,000 + 500,000 + 5,600,000) / 110
= 7,300,000 / 110
= 66,364đ
```

---

## ✅ WORKFLOW ĐÚNG

```
1. Menu "Bán hàng" → "Danh sách bán lẻ"
2. Click nút "Thêm" (toolbar)
3. → Form frmBanLe mở ra
4. Chọn khách hàng
5. Chọn sản phẩm từ cmbSanPham (ComboBox)
6. Nhập số lượng (numSoLuong)
7. Bấm "Thêm"
8. → Hệ thống TỰ ĐỘNG chọn lô theo FEFO
9. Bấm "Lưu"
```

---

## 🧪 TEST CASE 1: FEFO - Xuất 1 Lô

### Mục tiêu
Kiểm tra FEFO tự động chọn lô hết hạn SỚM NHẤT

### Các bước

1. Menu **"Bán hàng" → "Danh sách bán lẻ"**
2. Click nút **"Thêm"** (trên toolbar)
3. → frmBanLe mở ra
4. Chọn khách hàng bất kỳ
5. cmbSanPham: Chọn **"Thuốc trừ sâu ABC"**
6. numSoLuong: Nhập **5**
7. Click **"Thêm"**

### Kết quả mong đợi

✅ DataGridView hiển thị **1 dòng:**

| Sản phẩm | Mã lô | SL | Ngày hết hạn | Đơn giá | Thành tiền |
|----------|-------|----|--------------|---------|------------|
| Thuốc trừ sâu ABC | TEST-101-L3 | 5 | 25/12/2025 | 55,000 | 275,000 |

✅ Lô **TEST-101-L3** được chọn (hết hạn sớm nhất)

8. Click **"Lưu"**
9. MessageBox: "Lưu thành công"
10. Mở "Số lượng tồn" → Kiểm tra:
    - TEST-101-L3: **0** (5 → 0)
    - TEST-101-L1: **10** (không đổi)

---

## 🧪 TEST CASE 2: FEFO - Phân Bổ Nhiều Lô

### Mục tiêu
Kiểm tra FEFO tự động phân bổ qua nhiều lô

### Các bước

1-5. (Giống Test Case 1)
6. numSoLuong: Nhập **20** (nhiều hơn lô đầu)
7. Click **"Thêm"**

### Kết quả mong đợi

✅ DataGridView hiển thị **2 dòng:**

| Sản phẩm | Mã lô | SL | Ngày hết hạn | Đơn giá | Thành tiền |
|----------|-------|----|--------------|---------|------------|
| Thuốc trừ sâu ABC | TEST-101-L3 | 5 | 25/12/2025 | 55,000 | 275,000 |
| Thuốc trừ sâu ABC | TEST-101-L1 | 15 | 15/01/2026 | 55,000 | 825,000 |

**Tổng:** 1,100,000đ

✅ Tự động phân bổ:
- Lấy hết Lô L3 (5)
- Lấy thêm Lô L1 (15)
- Tổng = 20

8. Click **"Lưu"**
9. Kiểm tra tồn:
    - TEST-101-L3: **0** (hết)
    - TEST-101-L1: **0** (10 → 0, còn lại 10-15=-5, tức là hết luôn. À không, 10 - 15 không được, nên chỉ lấy 10 thôi... Khoan, script nói: "Mua 20 cái → Lấy lô TEST-101-L3 (5) + TEST-101-L1 (15)"... Hmm, L1 có 10, nhưng cần 15? Không ổn.)

Khoan, để tôi kiểm tra lại logic. Nếu:
- L3: 5
- L1: 10
- Tổng L3+L1 = 15 (không phải 20!)

Nếu bán 20 thì phải:
- L3: 5
- L1: 10
- L5: 5
= 20 total

Nhưng script nói "Mua 20 cái → Lấy lô TEST-101-L3 (5) + TEST-101-L1 (15)". Có nghĩa là L1 có 15? Không, L1 chỉ có 10 theo INSERT.

Có thể script comment bị nhầm. Hãy dựa vào data INSERT thực tế:
- L3: 5
- L1: 10
- L5: 30
- L2: 25
- L4: 50

Nếu bán 20:
- L3: 5
- L1: 10
- L5: 5
= 20

Hãy sửa lại test case.

### Kết quả mong đợi

✅ DataGridView hiển thị **3 dòng:**

| Sản phẩm | Mã lô | SL | Ngày hết hạn | Đơn giá | Thành tiền |
|----------|-------|----|--------------|---------|------------|
| Thuốc trừ sâu ABC | TEST-101-L3 | 5 | 25/12/2025 | 55,000 | 275,000 |
| Thuốc trừ sâu ABC | TEST-101-L1 | 10 | 15/01/2026 | 55,000 | 550,000 |
| Thuốc trừ sâu ABC | TEST-101-L5 | 5 | 28/02/2026 | 55,000 | 275,000 |

**Tổng:** 1,100,000đ

✅ Tự động phân bổ theo FEFO:
- Lấy hết Lô L3 (5) - hết hạn 25/12/2025
- Lấy hết Lô L1 (10) - hết hạn 15/01/2026
- Lấy thêm Lô L5 (5) - hết hạn 28/02/2026
- Tổng = 20

8. Click **"Lưu"**
9. Kiểm tra tồn:
    - TEST-101-L3: **0** (hết)
    - TEST-101-L1: **0** (hết)
    - TEST-101-L5: **25** (30 - 5)

---

## 🧪 TEST CASE 3: Weighted Average

### Mục tiêu
Kiểm tra tính giá xuất theo Weighted Average (Bình quân gia quyền)

### Cấu hình
- PHUONG_PHAP_TINH_GIA_XUAT = **'Average'**

### Các bước

1-4. (Giống Test Case 1)
5. cmbSanPham: Chọn **"Phân bón XYZ"**
6. Kiểm tra txtGiaBQGQ (hoặc tương tự)

### Kết quả mong đợi

✅ Label hiển thị: **"Giá BQGQ:"**
✅ Giá xuất hiển thị: **66,364đ**

**Công thức đã tính:**
```
Lô L3: 20 × 60,000 = 1,200,000
Lô L1: 10 × 50,000 =   500,000
Lô L2: 80 × 70,000 = 5,600,000
─────────────────────────────────
Tổng:  110         = 7,300,000

Weighted Avg = 7,300,000 / 110 = 66,364đ
```

✅ Tooltip: "Giá xuất tính theo bình quân gia quyền..."

---

## 🧪 TEST CASE 4: FIFO Costing (Tính giá theo FIFO)

### Mục tiêu
Kiểm tra tính giá xuất theo FIFO Costing (giá lô đầu tiên)

### Cấu hình
- PHUONG_PHAP_TINH_GIA_XUAT = **'FIFO'**

### Các bước

1. Đổi cấu hình:
   ```sql
   UPDATE THAM_SO SET PHUONG_PHAP_TINH_GIA_XUAT = 'FIFO'
   ```
2. Đóng và mở lại frmBanLe
3. Chọn **"Phân bón XYZ"**
4. Kiểm tra giá xuất

### Kết quả mong đợi

✅ Label hiển thị: **"Giá FIFO:"**
✅ Giá xuất hiển thị: **60,000đ** (giá của lô L3 - hết hạn sớm nhất)
✅ Tooltip: "Giá xuất lấy theo giá nhập của lô xuất đầu tiên (FIFO)"

---

## 🧪 TEST CASE 5: Không Đủ Tồn Kho

### Mục tiêu
Kiểm tra cảnh báo khi không đủ hàng

### Các bước

1-5. (Giống Test Case 1)
6. Chọn **"Thuốc trừ sâu ABC"**
7. numSoLuong: Nhập **150** (nhiều hơn tổng tồn 120)
8. Click **"Thêm"**

### Kết quả mong đợi

❌ MessageBox hiển thị lỗi:
```
Không đủ hàng trong kho!

Sản phẩm: Thuốc trừ sâu ABC
Số lượng cần: 150
Tồn kho khả dụng: 120
Thiếu: 30

Vui lòng nhập lại số lượng hoặc nhập thêm hàng.
```

✅ DataGridView KHÔNG có dòng mới
✅ numSoLuong được focus để user sửa

---

## 🧪 TEST CASE 6: CHI_DINH Mode (Chọn Lô Thủ Công)

### Mục tiêu
Kiểm tra mode CHI_DINH cho phép user chọn lô cụ thể

### Cấu hình
- PHUONG_PHAP_XUAT_KHO = **'CHI_DINH'**

### Các bước

1. Đổi cấu hình:
   ```sql
   UPDATE THAM_SO SET PHUONG_PHAP_XUAT_KHO = 'CHI_DINH'
   ```
2. Đóng và mở lại frmBanLe
3. Kiểm tra UI

### Kết quả mong đợi

✅ ComboBox **"Mã số"** hiển thị (không ẩn)
✅ Có thể chọn lô thủ công:
   - TEST-101-L1
   - TEST-101-L2
   - TEST-101-L3
   - TEST-101-L4
   - TEST-101-L5

4. Chọn sản phẩm **"Thuốc trừ sâu ABC"**
5. Chọn lô **TEST-101-L4** (lô hết hạn MUỘN NHẤT)
6. Nhập số lượng: 10
7. Click "Thêm"

✅ DataGridView hiển thị lô L4 (không phải L3)
✅ Xuất từ lô user chọn (CHI_DINH), không theo FEFO

---

## 🧪 TEST CASE 7: Tích Hợp - YC3 + YC4

### Mục tiêu
Test kết hợp FIFO + Chi phí VC + Chiết khấu

### Các bước

1-7. (Giống Test Case 2 - Bán 20 cái)
8. Trước khi "Lưu":
   - Chi phí vận chuyển: **50,000đ**
   - Chiết khấu: **10%**
9. Click "Lưu"

### Kết quả mong đợi

**Tính toán:**
```
Tổng tiền hàng:     1,100,000đ
Chi phí VC:            50,000đ
─────────────────────────────
Tạm tính:           1,150,000đ
Chiết khấu 10%:      -115,000đ
─────────────────────────────
TỔNG CỘNG:          1,035,000đ
```

✅ Tổng tiền cuối: **1,035,000đ**
✅ Database lưu đúng CHI_PHI_VAN_CHUYEN = 50000, CHIET_KHAU = 10.00

---

## 📝 CHECKLIST KIỂM THỬ

### Trước khi test
- [ ] Đã chạy **SampleData_TestFIFO.sql**
- [ ] Kiểm tra SP 101 có **120** đơn vị (5 lô)
- [ ] Kiểm tra SP 102 có **110** đơn vị (3 lô)
- [ ] Cấu hình FIFO + Average

### Test Cases
- [ ] TC1: FEFO chọn 1 lô (5 đơn vị)
- [ ] TC2: FEFO phân bổ 3 lô (20 đơn vị)
- [ ] TC3: Weighted Average = 66,364đ
- [ ] TC4: FIFO Costing = 60,000đ (giá lô đầu)
- [ ] TC5: Báo lỗi thiếu hàng (150 > 120)
- [ ] TC6: CHI_DINH mode cho chọn lô thủ công
- [ ] TC7: Tích hợp YC3+YC4

### Sau test
- [ ] Screenshot kết quả
- [ ] Kiểm tra tồn kho cập nhật đúng
- [ ] Reset data nếu cần (chạy lại SQL script)

---

## 🔄 RESET DỮ LIỆU

Nếu muốn test lại từ đầu:

```sql
-- Chạy lại file
data/SampleData_TestFIFO.sql
```

Script tự động:
- Xóa data test cũ (TEST-1%)
- Insert data mới
- Reset tồn kho

---

## 📸 GHI CHÚ CHO BÁO CÁO

### Screenshots cần chụp:

1. **Cấu hình THAM_SO** (FIFO, Average)
2. **Data trong MA_SAN_PHAM** (5 lô SP 101)
3. **Form frmBanLe** - Chọn sản phẩm
4. **DataGridView** sau khi Add - Hiển thị 3 lô
5. **Số lượng tồn** trước và sau khi bán
6. **Giá BQGQ** hiển thị 66,364đ
7. **MessageBox lỗi** thiếu hàng
8. **Mode CHI_DINH** - ComboBox mã lô hiển thị

---

**Hoàn thành!** 🎉
