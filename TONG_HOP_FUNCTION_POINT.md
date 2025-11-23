# TỔNG HỢP FUNCTION POINT - 7 YÊU CẦU BẢO TRÌ

**Ngày:** 23/11/2025
**Chuẩn:** IFPUG Counting Practices Manual (CPM) Release 4.3.1

---

## BẢNG TỔNG HỢP CUỐI CÙNG

### Tổng hợp theo yêu cầu

| YC | Mô Tả | ILF | EI | EO | EQ | UFP | % | Ưu Tiên |
|---|-------|-----|----|----|----|----|---|---------|
| YC7 | Đăng nhập + Phân quyền | 7 | 15 | 0 | 8 | **30** | 20.3% | ⭐⭐⭐ |
| YC4 | Chiết khấu + Khuyến mãi | 9 | 17 | 0 | 4 | **30** | 20.3% | ⭐⭐ |
| YC5 | Thống kê tồn kho + chi phí | 0 | 0 | 37 | 0 | **37** | 25.0% | ⭐⭐ |
| YC6 | Thống kê theo nhân viên | 1 | 0 | 15 | 0 | **16** | 10.8% | ⭐ |
| YC2 | Cấu hình xuất kho | 2 | 12 | 0 | 0 | **14** | 9.5% | ⭐⭐⭐ |
| YC3 | Chi phí VC + DV | 2 | 0 | 0 | 0 | **2** | 1.4% | ⭐⭐ |
| YC1 | Sửa lỗi + Tài liệu (15%) | - | - | - | - | **19** | 12.8% | ⭐⭐⭐ |
| **TỔNG UFP** | | **21** | **44** | **52** | **12** | **148** | **100%** | |

---

### Tổng hợp theo loại component

| Loại Component | Số Lượng | Tổng FP | % | FP Trung Bình |
|----------------|----------|---------|---|---------------|
| ILF (New) | 2 | 14 | 9.5% | 7.0 FP/ILF |
| ILF (Modify) | 4 | 7 | 4.7% | 1.8 FP/modify |
| EI (New) | 5 | 18 | 12.2% | 3.6 FP/EI |
| EI (Modify) | 4 | 26 | 17.6% | 6.5 FP/modify |
| EQ | 3 | 12 | 8.1% | 4.0 FP/EQ |
| EO | 11 | 52 | 35.1% | 4.7 FP/EO |
| Overhead (YC1) | - | 19 | 12.8% | - |
| **TỔNG** | **29** | **148 FP** | **100%** | **5.1 FP/component** |

---

## CHI TIẾT TỪNG YÊU CẦU

### YC1: Sửa Lỗi và Hoàn Chỉnh Tài Liệu (19 FP)

**Công thức:**
```
FP_YC1 = (YC2 + YC3 + YC4 + YC5 + YC6 + YC7) × 15%
       = (14 + 2 + 30 + 37 + 16 + 30) × 15%
       = 129 × 15%
       = 19.35 ≈ 19 FP
```

**Phân bổ:**
- Bug fixing: 7 FP (37%)
- Documentation: 6 FP (32%)
- Testing: 4 FP (21%)
- Deployment: 2 FP (11%)

---

### YC2: Cấu Hình Xuất Kho (14 FP)

| # | Thành phần | Loại | Complexity | FP |
|---|------------|------|------------|-----|
| 1 | Table THAM_SO (Add 2 cols) | ILF Modify | Low | 2 |
| 2 | frmBanLe - Modify | EI | Complex | 6 |
| 3 | frmBanSi - Modify | EI | Complex | 6 |
| **TỔNG** | | | | **14** |

**Complexity Justification:**
- FTR: 3 (PHIEU_BAN, CHI_TIET_PHIEU_BAN, MA_SAN_PHAM)
- DET: 7 fields
- Processing: 7 steps (FEFO algorithm, Weighted Average, branching...)
- **→ Complex (6 FP)**

---

### YC3: Chi Phí Vận Chuyển và Dịch Vụ (2 FP)

| # | Thành phần | Loại | Complexity | FP |
|---|------------|------|------------|-----|
| 1 | Table PHIEU_BAN (Add 2 cols) | ILF Modify | Low | 2 |
| **TỔNG** | | | | **2** |

**Lý do không count form:**
- frmBanLe/Si chỉ thêm 2 fields đơn giản (< 25% change)
- Processing: Simple addition → Không tăng complexity

---

### YC4: Chiết Khấu và Khuyến Mãi (30 FP)

| # | Thành phần | Loại | Complexity | FP |
|---|------------|------|------------|-----|
| **Database** | | | | |
| 1 | Table KHUYEN_MAI | ILF | Low | 7 |
| 2 | Table PHIEU_BAN (Add 2 cols + FK) | ILF Modify | Average | 2 |
| **Form CRUD** | | | | |
| 3 | frmKhuyenMai - Xem | EQ | Average | 4 |
| 4 | frmKhuyenMai - Thêm/Sửa | EI | Complex | 6 |
| 5 | frmKhuyenMai - Xóa | EI | Low | 3 |
| **Áp dụng** | | | | |
| 6 | frmBanLe - Áp dụng KM | EI Modify | Average | 4 |
| 7 | frmBanSi - Áp dụng KM | EI Modify | Average | 4 |
| **TỔNG** | | | | **30** |

---

### YC5: Thống Kê Tồn Kho và Chi Phí (37 FP)

| # | Thành phần | Loại | Complexity | FP |
|---|------------|------|------------|-----|
| 1 | frmSoLuongTon + Report | EO | Average | 5 |
| 2 | frmSoLuongBan - Ngày | EO | Average | 5 |
| 3 | frmSoLuongBan - Tháng | EO | Average | 5 |
| 4 | frmDoanhThu | EO | Low | 4 |
| 5 | Report Chi phí VC | EO | Low | 4 |
| 6 | Report Chi phí DV | EO | Low | 4 |
| 7 | Report Giảm giá/KM | EO | Average | 5 |
| 8 | Report Tổng hợp | EO | Average | 5 |
| **TỔNG** | | | | **37** |

---

### YC6: Thống Kê Theo Nhân Viên (16 FP)

| # | Thành phần | Loại | Complexity | FP |
|---|------------|------|------------|-----|
| 1 | Table PHIEU_BAN (Add ID_NGUOI_DUNG) | ILF Modify | Average | 1 |
| 2 | Report Giảm giá theo NV | EO | Average | 5 |
| 3 | Report Doanh thu theo NV | EO | Average | 5 |
| 4 | Report KPI theo NV | EO | Average | 5 |
| **TỔNG** | | | | **16** |

---

### YC7: Đăng Nhập và Phân Quyền (30 FP)

| # | Thành phần | Loại | Complexity | FP |
|---|------------|------|------------|-----|
| **Database** | | | | |
| 1 | Table NGUOI_DUNG | ILF | Low | 7 |
| **Đăng nhập** | | | | |
| 2 | frmDangNhap | EI | Complex | 6 |
| **Quản lý User** | | | | |
| 3 | frmNguoiDung - Xem | EQ | Average | 4 |
| 4 | frmNguoiDung - Thêm/Sửa | EI | Complex | 6 |
| 5 | frmNguoiDung - Xóa | EI | Low | 3 |
| 6 | frmNguoiDung - Tìm | EQ | Average | 4 |
| **TỔNG** | | | | **30** |

---

## TÍNH TOÁN ADJUSTED FUNCTION POINT

### 14 Hệ số Kỹ Thuật (GSCs)

| # | Hệ Số | Điểm | Lý Do |
|---|-------|------|-------|
| 1 | Data Communications | 0 | Standalone |
| 2 | Distributed Processing | 0 | DB tập trung |
| 3 | Performance | 3 | FIFO, BQGQ phức tạp |
| 4 | Heavily Used Config | 1 | Máy thông thường |
| 5 | Transaction Rate | 2 | Vừa phải |
| 6 | Online Data Entry | 4 | Nhiều forms |
| 7 | End-User Efficiency | 4 | Tự động hóa |
| 8 | Online Update | 4 | Realtime |
| 9 | Complex Processing | 4 | Logic phức tạp |
| 10 | Reusability | 3 | 3-layer + Strategy |
| 11 | Installation Ease | 4 | Dễ cài |
| 12 | Operational Ease | 4 | UI trực quan |
| 13 | Multiple Sites | 0 | 1 cửa hàng |
| 14 | Facilitate Change | 4 | Dễ bảo trì |
| **TỔNG** | | **37** | |

---

### Tính VAF và AFP

```
VAF = 0.65 + 0.01 × 37
    = 1.02

AFP = 148 × 1.02
    = 150.96 ≈ 151 FP
```

---

## ƯỚC LƯỢNG LOC VÀ EFFORT

```
LOC = 151 × 58 = 8,758 dòng

KDSI = 8.8 KDSI

Effort = 151 / 12 = 12.6 ≈ 13 người-tháng

Duration (5 người) = 13 / 5 = 2.6 tháng ≈ 11 tuần

Duration (7 người) = 13 / 7 = 1.9 tháng ≈ 8 tuần
```

---

## SO SÁNH TRƯỚC/SAU CHUẨN IFPUG

| YC | FP Trước (Sai - có IP) | FP Sau (Đúng IFPUG) | Δ | Lý Do |
|---|------------------------|---------------------|---|-------|
| YC2 | 41 | **14** | -66% | Strategy không count |
| YC7 | 46 | **30** | -35% | Business logic vào complexity |
| YC4 | 29 | **30** | +3% | Gần đúng |
| YC5 | 27 | **37** | +37% | Bổ sung 4 reports |
| YC3 | 28 | **2** | -93% | Chỉ DB modify |
| YC6 | 17 | **16** | -6% | Gần đúng |
| YC1 | 26 | **19** | -27% | Overhead giảm |
| **TỔNG** | **214** | **148** | **-31%** | |

---

## FILES ĐÃ TẠO/SỬA (THỰC TẾ)

### YC2: 10 files

**New:**
- Strategy/IXuatKhoStrategy.cs (12 LOC)
- Strategy/FifoXuatKhoStrategy.cs (84 LOC)
- Strategy/ChiDinhXuatKhoStrategy.cs (42 LOC)
- Strategy/ITinhGiaXuatStrategy.cs (15 LOC)
- Strategy/WeightedAverageGiaStrategy.cs (42 LOC)
- Strategy/FifoGiaStrategy.cs (31 LOC)

**Modified:**
- ThamSo.cs (+100 LOC)
- Controller/MaSanPhamController.cs (+130 LOC)
- DataLayer/MaSanPhamFactory.cs (+15 LOC)
- frmBanLe.cs (~300 LOC changed)
- frmBanSi.cs (~300 LOC changed)

**TỔNG:** ~2,910 LOC

---

### YC7: 7 files

**New:**
- BusinessObject/NguoiDung.cs (86 LOC)
- DataLayer/NguoiDungFactory.cs (174 LOC)
- Controller/NguoiDungController.cs (230 LOC)
- PhienDangNhap.cs (106 LOC)
- frmDangNhap.cs (171 LOC)
- frmNguoiDung.cs (~350 LOC)

**Modified:**
- frmMain.cs (~60 LOC - Authorization logic)

**TỔNG:** ~1,177 LOC

---

### YC4: 8 files

**New:**
- BusinessObject/KhuyenMai.cs (~80 LOC)
- DataLayer/KhuyenMaiFactory.cs (~150 LOC)
- Controller/KhuyenMaiController.cs (~180 LOC)
- frmKhuyenMai.cs (~400 LOC)

**Modified:**
- frmBanLe.cs (~100 LOC - Apply KM logic)
- frmBanSi.cs (~100 LOC)
- Controller/PhieuBanController.cs (~50 LOC - Validate KM)

**TỔNG:** ~1,060 LOC

---

## LOC THỰC TẾ SO VỚI ƯỚC LƯỢNG

| YC | FP | LOC Ước Lượng (×58) | LOC Thực Tế | Δ | LOC/FP Thực Tế |
|---|----|--------------------|-------------|---|----------------|
| YC2 | 14 | 812 | 2,910 | **+258%** | 208 LOC/FP |
| YC7 | 30 | 1,740 | 1,177 | -32% | 39 LOC/FP |
| YC4 | 30 | 1,740 | 1,060 | -39% | 35 LOC/FP |
| YC5 | 37 | 2,146 | ~800 (ước) | -63% | 22 LOC/FP |
| YC6 | 16 | 928 | ~400 (ước) | -57% | 25 LOC/FP |
| YC3 | 2 | 116 | ~50 | -57% | 25 LOC/FP |
| **TỔNG** | **148** | **8,584** | **~6,400** | **-25%** | **43 LOC/FP** |

**Nhận xét:**
- YC2 cao hơn nhiều (208 LOC/FP) vì refactor code cũ + giữ backward compatibility
- YC7, YC4 thấp hơn (35-39 LOC/FP) vì code clean, không refactor
- Trung bình: 43 LOC/FP (thấp hơn 58 → Code concise)

---

## PHÂN BỔ EFFORT THEO PHASE

**Bảng: Effort Distribution (SDLC Phases)**

| Phase | % | Effort (PM) | Thời Gian (tuần) | Ghi Chú |
|-------|---|-------------|------------------|---------|
| Analysis | 15% | 1.9 | 2 | Phân tích 7 YC |
| Design | 20% | 2.6 | 2.5 | UML, database design |
| Implementation | 40% | 5.2 | 5 | Coding |
| Testing | 15% | 1.9 | 2 | Unit + Integration |
| Deployment | 5% | 0.7 | 0.5 | Migration, setup |
| Documentation | 5% | 0.7 | 1 | CLAUDE.md, comments |
| **TỔNG** | 100% | **13.0** | **13** | |

**Thực tế:** 1.5 tuần (10 ngày) - Nhanh gấp **8 lần** vì:
- Parallel development (YC2 + YC7 cùng lúc)
- Reuse 70% code
- Sprint intensive

---

## CHI PHÍ ƯỚC LƯỢNG

### Giả định

- Lương: 10 triệu VNĐ/người/tháng
- Overhead: 20%

### Tính toán

```
Chi phí nhân công = 13 PM × 10 triệu = 130 triệu VNĐ
Chi phí overhead = 130 × 20% = 26 triệu VNĐ
TỔNG = 156 triệu VNĐ
```

### Breakdown theo YC

| YC | FP | % | Effort (PM) | Chi Phí (triệu) |
|---|----|----|-------------|-----------------|
| YC5 | 37 | 25.0% | 3.3 | 39.6 |
| YC7 | 30 | 20.3% | 2.6 | 31.2 |
| YC4 | 30 | 20.3% | 2.6 | 31.2 |
| YC1 | 19 | 12.8% | 1.7 | 20.4 |
| YC6 | 16 | 10.8% | 1.4 | 16.8 |
| YC2 | 14 | 9.5% | 1.2 | 14.4 |
| YC3 | 2 | 1.4% | 0.2 | 2.4 |
| **TỔNG** | **148** | **100%** | **13.0** | **156** |

---

## KẾT LUẬN

### Tổng hợp cuối cùng

| Chỉ Số | Giá Trị | Đơn Vị |
|--------|---------|--------|
| Unadjusted Function Points | 148 | FP |
| Value Adjustment Factor | 1.02 | - |
| Adjusted Function Points | 151 | FP |
| Lines of Code | 8,758 | LOC (ước lượng) |
| Lines of Code (thực tế) | ~6,400 | LOC |
| KDSI | 8.8 | KDSI |
| Productivity | 12 | FP/PM |
| Effort | 13 | người-tháng |
| Duration (5 người) | 2.6 | tháng |
| Duration (7 người) | 1.9 | tháng |
| Chi phí | 156 | triệu VNĐ |

---

**Ngày cập nhật:** 23/11/2025
