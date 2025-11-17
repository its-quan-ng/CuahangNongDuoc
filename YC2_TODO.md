# 🔥 YC2: XUẤT KHO FIFO + STRATEGY PATTERN - TODO

**Trạng thái:** 75% hoàn thành
**Deadline:** 17/11/2025 (còn 3 ngày)
**Session:** 17/11/2025 00:30

---

## ✅ ĐÃ HOÀN THÀNH (75%)

### 1. Database + Config ✅
- THAM_SO có 4 cột: `PHUONG_PHAP_XUAT_KHO`, `PHUONG_PHAP_TINH_GIA_XUAT`, `TU_DONG_PHAN_LO`, `HIEN_THI_LO_PHIEU_XUAT`
- ThamSo.cs có 4 properties tương ứng (get/set)
- frmCauHinh.cs: Form cấu hình (Admin thay đổi)

### 2. Strategy Pattern (6 files) ✅

**Vị trí:** `D:\Workspace\CHND\CHND\Strategy\`

```
Strategy/
├── IXuatKhoStrategy.cs              ✅ Interface chọn lô
├── FifoXuatKhoStrategy.cs           ✅ Xuất lô cũ trước (NGAY_NHAP ASC)
├── ChiDinhXuatKhoStrategy.cs        ✅ User chọn lô (return empty, validate)
├── ITinhGiaXuatStrategy.cs          ✅ Interface tính giá
├── WeightedAverageGiaStrategy.cs    ✅ Bình quân gia quyền: SUM(qty×price)/SUM(qty)
└── FifoGiaStrategy.cs               ✅ Giá lô đầu tiên
```

**Lưu ý:**
- `FifoXuatKhoStrategy.cs` dùng `factory.DanhsachMaSanPham(idSanPham)` (method có sẵn)
- Tất cả strategies đã implement đúng interface

### 3. Controller Methods ✅

**Vị trí:** `D:\Workspace\CHND\CHND\Controller\MaSanPhamController.cs`

**Đã thêm:**
```csharp
using CuahangNongduoc.Strategy;  // ← Import namespace

private IXuatKhoStrategy TaoXuatKhoStrategy()
// → Đọc ThamSo.PhuongPhapXuatKho → Return FIFO/CHI_DINH strategy

private ITinhGiaXuatStrategy TaoTinhGiaStrategy()
// → Đọc ThamSo.PhuongPhapTinhGiaXuat → Return Average/FIFO strategy

public XuatKhoResult XuatKho(int idSanPham, int soLuongCanXuat)
// → Orchestrate: Chọn lô + Tính giá + Return result
// ⚠️ CẦN SỬA: Bỏ Bước 4 (cập nhật database) - Xem phần TODO
```

### 4. Result Class ✅

**Vị trí:** `D:\Workspace\CHND\CHND\Controller\MaSanPhamController.cs` (cuối file)

```csharp
public class XuatKhoResult
{
    public IList<MaSanPham> DanhSachLoXuat { get; set; }  // Lô nào, bao nhiêu
    public long GiaXuat { get; set; }                      // Giá trung bình
    public bool ThanhCong { get; set; }                    // Success/Fail
    public string ErrorMessage { get; set; }               // Lỗi gì (nếu fail)
}
```

---

## ⏳ CẦN LÀM TIẾP (25% còn lại)

### 🔴 TASK 1: Sửa Method XuatKho() (5 phút)

**File:** `D:\Workspace\CHND\CHND\Controller\MaSanPhamController.cs`

**Vấn đề hiện tại:**
```csharp
// ❌ ĐANG SAI - Bước 4 cập nhật database ngay
public XuatKhoResult XuatKho(int idSanPham, int soLuongCanXuat)
{
    // Bước 1-3: OK

    // ❌ BƯỚC 4: XÓA ĐOẠN NÀY
    foreach (var maSp in danhSachLoXuat)
    {
        MaSanPhamFactory.CapNhatSoLuong(maSp.Id, -maSp.SoLuong);
    }

    return result;
}
```

**Cần sửa:**
- XÓA Bước 4 (cập nhật database)
- Method chỉ TRẢ VỀ thông tin, KHÔNG sửa database
- Database sẽ được cập nhật khi user bấm "Lưu" ở form

**Lý do:**
- User có thể thêm → xóa → thêm lại trên form trước khi lưu
- Nếu cập nhật ngay → Database thay đổi loạn xạ

---

### 🔴 TASK 2: Sửa frmBanLe.cs (30 phút)

**File:** `D:\Workspace\CHND\CHND\frmBanLe.cs`

**Method cần sửa:** `btnThem_Click()` (line ~110-130)

**Logic CŨ (đang dùng):**
```csharp
// ❌ User tự chọn LÔ từ ComboBox
string idLo = cmbMaSanPham.SelectedValue.ToString();
int soLuong = (int)numSoLuong.Value;
long donGia = (long)numDonGia.Value;

DataRow row = ctrlChiTiet.NewRow();
row["ID_MA_SAN_PHAM"] = idLo;  // ← User chọn lô thủ công
row["SO_LUONG"] = soLuong;
row["DON_GIA"] = donGia;
row["THANH_TIEN"] = soLuong * donGia;
ctrlChiTiet.Add(row);
```

**Logic MỚI (cần sửa thành):**
```csharp
// ✅ Gọi Strategy Pattern
int idSanPham = (int)cmbSanPham.SelectedValue;  // ← Chọn SẢN PHẨM, không phải lô
int soLuong = (int)numSoLuong.Value;

MaSanPhamController ctrl = new MaSanPhamController();
XuatKhoResult result = ctrl.XuatKho(idSanPham, soLuong);

if (!result.ThanhCong)
{
    MessageBox.Show(result.ErrorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    return;
}

// Thêm TỪNG LÔ vào DataGridView (có thể nhiều lô)
foreach (var maSp in result.DanhSachLoXuat)
{
    DataRow row = ctrlChiTiet.NewRow();
    row["ID_PHIEU_BAN"] = txtMaPhieu.Text;
    row["ID_MA_SAN_PHAM"] = maSp.Id;           // ← Lô do Strategy chọn
    row["SO_LUONG"] = maSp.SoLuong;
    row["DON_GIA"] = result.GiaXuat;           // ← Giá do Strategy tính
    row["THANH_TIEN"] = maSp.SoLuong * result.GiaXuat;
    ctrlChiTiet.Add(row);
}

// Cập nhật tổng tiền
numTongTien.Value += result.DanhSachLoXuat.Sum(m => m.SoLuong) * result.GiaXuat;
```

**Lưu ý:**
- Cần import: `using System.Linq;` (để dùng `.Sum()`)
- `btnLuu_Click()` KHÔNG cần sửa (vẫn cập nhật database như cũ)

---

### 🔴 TASK 3: Sửa frmBanSi.cs (10 phút)

**File:** `D:\Workspace\CHND\CHND\frmBanSi.cs`

**Method cần sửa:** `btnThem_Click()` (line ~110-130)

**Logic:** GIỐNG Y HỆT frmBanLe.cs Task 2
- Copy code từ frmBanLe đã sửa
- Thay tên controls nếu khác (txtMaPhieu, numTongTien...)

---

### 🔴 TASK 4: Test Tích Hợp (30 phút)

**TC1: FIFO Tự Động**
```
Setup:
  - Admin vào frmCauHinh → Chọn FIFO, Average
  - Database có:
    Lô 1: 30 chai, ngày nhập: 01/01/2025, giá 10,000đ
    Lô 2: 50 chai, ngày nhập: 05/01/2025, giá 11,000đ
    Lô 3: 70 chai, ngày nhập: 10/01/2025, giá 12,000đ

Action:
  - User bán 100 chai sản phẩm này

Expected:
  - DataGridView hiển thị 3 dòng:
    Lô 1: 30 chai × 10,900đ
    Lô 2: 50 chai × 10,900đ
    Lô 3: 20 chai × 10,900đ
  - Giá: 10,900đ = (30×10k + 50×11k + 20×12k) / 100 (Weighted Average)
  - Bấm Lưu → Database cập nhật đúng
```

**TC2: FIFO Price**
```
Setup:
  - Admin vào frmCauHinh → Chọn FIFO, FIFO Price
  - Database như TC1

Expected:
  - Giá: 10,000đ (giá lô 1 - lô đầu tiên)
```

**TC3: Không Đủ Hàng**
```
Setup:
  - Database chỉ có 50 chai

Action:
  - User bán 100 chai

Expected:
  - MessageBox: "Không đủ hàng trong kho! Tồn: 50, Cần xuất: 100"
  - Không thêm vào DataGridView
```

---

## 📝 CHECKLIST HOÀN THÀNH

**Code:**
- [x] Strategy Pattern: 6 files
- [x] MaSanPhamController: 3 factory methods
- [x] XuatKhoResult class
- [ ] Sửa XuatKho() - bỏ Bước 4
- [ ] Sửa frmBanLe.cs - btnThem_Click()
- [ ] Sửa frmBanSi.cs - btnThem_Click()

**Testing:**
- [ ] TC1: FIFO + Weighted Average
- [ ] TC2: FIFO + FIFO Price
- [ ] TC3: Không đủ hàng
- [ ] Admin đổi config → Hành vi thay đổi

**Optional (nếu còn thời gian):**
- [ ] Sửa report: Hiển thị số lô trong phiếu bán
- [ ] UI: Ẩn/hiện ComboBox chọn lô theo config

---

## 💡 GHI NHỚ

### **Kiến Trúc Strategy Pattern:**

```
Admin thay đổi config (frmCauHinh)
         ↓
    THAM_SO table
         ↓
    ThamSo.cs (properties)
         ↓
MaSanPhamController.TaoXuatKhoStrategy()
         ↓
    IXuatKhoStrategy instance (FIFO hoặc CHI_DINH)
         ↓
    ChonLoXuat() → Return danh sách lô
         ↓
    ITinhGiaXuatStrategy.TinhGiaXuat() → Return giá
         ↓
    XuatKhoResult → Form nhận kết quả
         ↓
    Form thêm vào DataGridView
         ↓
    User bấm Lưu → Cập nhật database
```

### **Flow QUAN TRỌNG:**

1. Method `XuatKho()` CHỈ TRẢ VỀ thông tin (KHÔNG sửa database)
2. Form nhận result → Hiển thị trên DataGridView
3. User bấm "Lưu" → Form mới cập nhật database

### **Files Đã Tạo:**

```
Strategy/
├── IXuatKhoStrategy.cs
├── FifoXuatKhoStrategy.cs
├── ChiDinhXuatKhoStrategy.cs
├── ITinhGiaXuatStrategy.cs
├── WeightedAverageGiaStrategy.cs
└── FifoGiaStrategy.cs
```

### **Công Thức Tính Giá:**

```
Weighted Average = SUM(SoLuong × GiaNhap) / SUM(SoLuong)
FIFO Price = GiaNhap của lô đầu tiên (danhSachLoXuat[0])
```

---

## 🚨 LƯU Ý QUAN TRỌNG

1. **KHÔNG cập nhật database trong method XuatKho()**
   - Lý do: User có thể thêm/xóa nhiều lần trước khi lưu

2. **Method XuatKho() KHÔNG dư thừa**
   - Form CẦN gọi để có FIFO tự động
   - Strategy Pattern cốt lõi nằm ở đây

3. **XuatKhoResult class KHÔNG dư thừa**
   - Form cần DanhSachLoXuat để hiển thị
   - Form cần GiaXuat để tính tiền
   - Form cần check ThanhCong/ErrorMessage

4. **Form bán hàng CẦN SỬA**
   - Logic cũ: User chọn LÔ
   - Logic mới: User chọn SẢN PHẨM → Strategy tự chọn lô

---

**Cập nhật:** 17/11/2025 00:30
**Next session:** Làm Task 1-4 (sửa controller + form)
**Ước lượng:** 1-2 giờ hoàn thành tất cả
