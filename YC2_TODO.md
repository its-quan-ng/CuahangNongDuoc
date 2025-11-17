# 🔥 YC2: XUẤT KHO FIFO - TODO LIST

**Trạng thái:** 60% hoàn thành
**Deadline:** 16/11/2025

---

## ✅ ĐÃ XONG (Task 1-4)

### Task 1: Database ✅
- THAM_SO có 4 cột mới (đã có sẵn)

### Task 2: ThamSo.cs ✅
```csharp
// Thêm 4 properties:
ThamSo.PhuongPhapXuatKho       // "FIFO" | "CHI_DINH"
ThamSo.PhuongPhapTinhGiaXuat   // "AVERAGE" | "FIFO"
ThamSo.TuDongPhanLo             // bool
ThamSo.HienThiLoPhieuXuat       // bool
```

### Task 3: frmCauHinhKho.cs ✅
- Form cấu hình (Admin only)
- Menu: Tùy chỉnh → Cấu hình xuất kho

### Task 4: Logic FIFO Core ✅
**MaSanPhanFactory.cs:**
```csharp
public DataTable LayDanhSachLoConHang(int idSanPham)
// → Query lô còn hàng, ORDER BY NGAY_HET_HAN, NGAY_NHAP
```

**MaSanPhamController.cs:**
```csharp
public IList<MaSanPham> ChonLoFIFO(int idSanPham, int soLuongCan)
// → Chọn lô tự động, hết hạn sớm nhất trước

public long TinhGiaXuat(int idSanPham)
// → Gọi TinhGiaBinhQuanGiaQuyen() hoặc TinhGiaFIFO()

private long TinhGiaBinhQuanGiaQuyen(int idSanPham)
// → Weighted average: SUM(qty×price) / SUM(qty)

private long TinhGiaFIFO(int idSanPham)
// → Giá lô đầu tiên
```

---

## ⏳ CÒN LẠI (Task 5-6) - LÀM TIẾP

### 🔴 Task 5: Áp Dụng FIFO vào Form Bán Hàng (1 NGÀY)

#### **File 1: frmBanLe.cs**

**Vị trí sửa:** Event khi chọn sản phẩm (cmbSanPham_SelectedIndexChanged)

**Logic cần thêm:**
```csharp
// Khi user chọn sản phẩm + nhập số lượng:

// B1: Đọc cấu hình
if (ThamSo.TuDongPhanLo)
{
    // B2: Gọi FIFO tự động
    int idSanPham = Convert.ToInt32(cmbSanPham.SelectedValue);
    int soLuong = Convert.ToInt32(numSoLuong.Value);

    IList<MaSanPham> danhSachLo = ctrlMaSanPham.ChonLoFIFO(idSanPham, soLuong);

    // B3: Add từng lô vào DataGridView chi tiết
    foreach (MaSanPham lo in danhSachLo)
    {
        // Add row vào dgvChiTiet
        // Columns: ID_MA_SAN_PHAM, SO_LUONG, DON_GIA, THANH_TIEN
    }
}
else
{
    // B2: User chọn lô thủ công (logic cũ)
    // Hiển thị danh sách lô available
    // User chọn lô cụ thể
}

// B4: Tính giá xuất
long giaXuat = ctrlMaSanPham.TinhGiaXuat(idSanPham);
```

**Controls cần thêm/sửa:**
- DataGridView chi tiết phải có cột: ID_MA_SAN_PHAM (số lô)
- Label hiển thị giá xuất

**Khi lưu phiếu bán:**
```csharp
// Lưu CHI_TIET_PHIEU_BAN
foreach (DataGridViewRow row in dgvChiTiet.Rows)
{
    String idMaSanPham = row.Cells["ID_MA_SAN_PHAM"].Value;
    int soLuong = row.Cells["SO_LUONG"].Value;

    // INSERT vào CHI_TIET_PHIEU_BAN
    // UPDATE MA_SAN_PHAM: Giảm SO_LUONG
}
```

#### **File 2: frmBanSi.cs**
- Logic GIỐNG Y HỆT frmBanLe.cs
- Copy paste code ở trên, test lại

---

### 🔴 Task 6: Sửa Report Hiển Thị Lô (0.5 NGÀY)

#### **File: Report/rptPhieuBan.rdlc**

**Bước 1: Sửa DataSet / Query**
```sql
-- Query cũ:
SELECT CTPB.*, SP.TEN_SAN_PHAM
FROM CHI_TIET_PHIEU_BAN CTPB
INNER JOIN SAN_PHAM SP ON ...

-- Query mới (thêm JOIN):
SELECT CTPB.*, SP.TEN_SAN_PHAM,
       MSP.ID AS SO_LO,
       MSP.NGAY_HET_HAN
FROM CHI_TIET_PHIEU_BAN CTPB
INNER JOIN SAN_PHAM SP ON ...
INNER JOIN MA_SAN_PHAM MSP ON CTPB.ID_MA_SAN_PHAM = MSP.ID
```

**Bước 2: Thêm columns vào Report**
- Column mới: "Số Lô" (ID)
- Column mới: "Ngày HSD" (NGAY_HET_HAN)

**Bước 3: Conditional Visibility**
```csharp
// Chỉ hiển thị lô nếu cấu hình bật
=IIF(ThamSo.HienThiLoPhieuXuat, "Visible", "Hidden")
```

---

## 🧪 TEST CASES

### TC1: FIFO Tự Động
```
Setup:
  - Cấu hình: FIFO, Tự động phân lô = true
  - Database:
    L1 (HSD: 01/06/2025, 5 cái)
    L2 (HSD: 01/03/2025, 8 cái) ← Hết hạn sớm nhất
    L3 (HSD: 01/12/2025, 10 cái)

Action:
  - Bán 10 cái

Expected:
  - Hệ thống tự chọn:
    L2: 8 cái
    L1: 2 cái
  - DataGridView hiển thị 2 dòng
  - In phiếu: Thấy 2 số lô
```

### TC2: Chỉ Định Thủ Công
```
Setup:
  - Cấu hình: Chỉ định, Tự động = false

Action:
  - Bán 10 cái

Expected:
  - Hệ thống hiển thị danh sách lô available
  - User chọn L3: 10 cái
  - Lưu thành công
```

### TC3: Tính Giá Average
```
Setup:
  - Cấu hình: Tính giá = AVERAGE
  - Database:
    L1: 5 cái × 10,000
    L2: 10 cái × 12,000

Expected:
  - Giá xuất = (50,000 + 120,000) / 15 = 11,333
```

### TC4: Tính Giá FIFO
```
Setup:
  - Cấu hình: Tính giá = FIFO
  - Database như TC3

Expected:
  - Giá xuất = Giá lô đầu tiên = 10,000
```

---

## 📝 CHECKLIST HOÀN THÀNH

**Code:**
- [ ] frmBanLe.cs - Thêm logic FIFO
- [ ] frmBanSi.cs - Thêm logic FIFO
- [ ] Test với TuDongPhanLo = true
- [ ] Test với TuDongPhanLo = false
- [ ] Lưu CHI_TIET_PHIEU_BAN đúng (ID_MA_SAN_PHAM)
- [ ] Giảm SO_LUONG trong MA_SAN_PHAM khi bán

**Report:**
- [ ] rptPhieuBan.rdlc - Thêm cột Số Lô
- [ ] rptPhieuBan.rdlc - Thêm cột Ngày HSD
- [ ] Query JOIN với MA_SAN_PHAM
- [ ] Test in phiếu: Thấy số lô

**Testing:**
- [ ] TC1: FIFO tự động OK
- [ ] TC2: Chỉ định thủ công OK
- [ ] TC3: Tính giá Average OK
- [ ] TC4: Tính giá FIFO OK
- [ ] Không crash khi hết hàng
- [ ] Không crash khi NULL

---

## 💡 GHI NHỚ

**Tên methods quan trọng:**
```csharp
ThamSo.TuDongPhanLo                        // Check có tự động không
ThamSo.PhuongPhapTinhGiaXuat               // Check tính giá kiểu gì

ctrlMaSanPham.ChonLoFIFO(id, qty)          // Chọn lô tự động
ctrlMaSanPham.TinhGiaXuat(id)              // Tính giá
```

**Cột database:**
```
CHI_TIET_PHIEU_BAN.ID_MA_SAN_PHAM  → Lưu số lô
MA_SAN_PHAM.SO_LUONG                → Giảm khi bán
```

**Order quan trọng:**
```sql
ORDER BY NGAY_HET_HAN ASC, NGAY_NHAP ASC
-- ↑ Hết hạn sớm nhất, nhập trước xuất trước
```

---

**File tạo:** 14/11/2025
**Mục đích:** Quick reference cho Task 5-6
**Next session:** Làm Task 5 (frmBanLe, frmBanSi)
