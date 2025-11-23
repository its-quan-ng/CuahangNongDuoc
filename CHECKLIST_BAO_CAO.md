# CHECKLIST BÁO CÁO ĐỒ ÁN (200-300 TRANG)

**Ngày:** 23/11/2025

---

## ✅ PHẦN MỞ ĐẦU (10-15 trang)

- [ ] Trang bìa (tên trường, khoa, đề tài, sinh viên, giảng viên)
- [ ] Lời mở đầu (0.5-1 trang)
- [ ] Lời cảm ơn (0.5 trang)
- [ ] Tóm tắt đề tài (1-2 trang)
  - [ ] Tiếng Việt
  - [ ] Tiếng Anh (Abstract)
- [ ] Mục lục (2-3 trang)
- [ ] Danh sách hình (1-2 trang)
- [ ] Danh sách bảng (1-2 trang)
- [ ] Danh sách từ viết tắt (1 trang)

---

## ✅ PHẦN I: GIỚI THIỆU (20-30 trang)

### 1.1. Bối cảnh dự án (5-7 trang)
- [ ] Giới thiệu Công ty Nông Dược An Giang
- [ ] Lịch sử hệ thống (hệ thống cũ - Access DB)
- [ ] Nhu cầu bảo trì (tại sao cần bảo trì)

### 1.2. Mục tiêu (2-3 trang)
- [ ] Mục tiêu chính (sửa lỗi, nâng cấp)
- [ ] Mục tiêu phụ (ước lượng, tư vấn)

### 1.3. Phạm vi (3-5 trang)
- [ ] In-scope: 7 yêu cầu
- [ ] Out-of-scope: Web app, Cloud

### 1.4. Tổng quan hệ thống cũ (10-15 trang)
- [ ] Kiến trúc (3-layer)
- [ ] Database schema (15 tables) - ER Diagram
- [ ] Danh sách 32 forms
- [ ] Công nghệ stack (C#, WinForms, SQL Server)

---

## ⭐ PHẦN II: ÁP DỤNG ISO 14764 (40-50 trang)

### 2.1. Tổng quan ISO 14764 (5-7 trang)
- [ ] Giới thiệu chuẩn ISO/IEC/IEEE 14764
- [ ] 8 Activities (Bảng 2.1)
- [ ] Loại bảo trì (Corrective, Perfective, Adaptive, Preventive)

### 2.2. Áp dụng 8 Activities cho 7 YC (25-30 trang)

**Mỗi YC (YC1-YC7) cần có:**

#### YC2 (Ví dụ chi tiết - 5 trang):
- [ ] Activity 1: Process Implementation (0.5 trang)
  - [ ] Bảng môi trường (IDE, Git, SQL...)
- [ ] Activity 2: Problem & Modification Analysis (1.5 trang)
  - [ ] Biểu mẫu MR-002 (Modification Request)
  - [ ] Biểu mẫu IA-002 (Impact Analysis)
- [ ] Activity 3: Implementation (1 trang)
  - [ ] Bảng Implementation Plan
- [ ] Activity 4: Review/Acceptance (1 trang)
  - [ ] Code Review Checklist
  - [ ] UAT Report
- [ ] Activity 5: Migration (0.5 trang)
  - [ ] Migration Checklist
- [ ] Activity 7: Configuration Management (0.5 trang)
  - [ ] Git version control table
- [ ] Activity 8: Documentation (0.5 trang)
  - [ ] Documentation deliverables

#### YC7 (5 trang):
- [ ] 8 subsections tương tự YC2

#### YC1, YC3-YC6 (3-4 trang mỗi YC):
- [ ] Các activities chính (rút gọn hơn YC2)

### 2.3. Cải tiến qui trình (10-15 trang)
- [ ] Bảng 2.11: So sánh ISO chuẩn vs Thực tế
- [ ] Hình 2.3: Flowchart qui trình Agile-inspired
- [ ] Giải thích lý do customize (5-7 trang)
  - [ ] Timeline gấp
  - [ ] Team nhỏ
  - [ ] Trust-based
- [ ] Đánh giá hiệu quả (2-3 trang)
  - [ ] Nhanh hơn 50%
  - [ ] Trade-offs

### 2.4. Tổng hợp biểu mẫu (3-5 trang)
- [ ] 7 biểu mẫu MR
- [ ] 7 biểu mẫu IA
- [ ] Test reports
- [ ] Migration checklists

---

## ⭐ PHẦN III: ƯỚC LƯỢNG CHI PHÍ (30-40 trang)

### 3.1. Giới thiệu Function Point (5-7 trang)
- [ ] Tổng quan FPA
- [ ] 5 loại components (Bảng 3.1)
- [ ] Quy trình ước lượng (Flowchart)
- [ ] Complexity matrix (IFPUG tables)

### 3.2. Phân tích 7 YC (20-25 trang)

**Mỗi YC cần:**
- [ ] Mô tả yêu cầu (0.5 trang)
- [ ] Bảng phân tích FP chi tiết (1 trang)
  - [ ] FTR, DET, Complexity
  - [ ] Justification cho Complex
- [ ] Tổng hợp (0.5 trang)

**YC2 (3-4 trang):**
- [ ] Bảng 3.2: Chi tiết FP
- [ ] Complexity matrix cho frmBanLe
- [ ] Liệt kê 7 processing steps

**YC7 (3-4 trang):**
- [ ] Bảng 3.7: Chi tiết FP
- [ ] Justification cho frmDangNhap Complex

**YC3-YC6, YC1 (2-3 trang mỗi YC)**

### 3.3. Tổng hợp UFP (3-5 trang)
- [ ] Bảng 3.8: Tổng hợp theo YC
- [ ] Bảng 3.9: Tổng hợp theo loại component
- [ ] Pie chart phân bổ FP

### 3.4. Tính AFP (3-5 trang)
- [ ] Bảng 3.10: 14 GSCs
- [ ] Tính VAF
- [ ] Tính AFP

### 3.5. LOC, KDSI, Effort (3-5 trang)
- [ ] Bảng tra cứu LOC/FP
- [ ] Tính LOC, KDSI
- [ ] Tính Effort (12 FP/PM)
- [ ] Tính Duration

### 3.6. So sánh ước lượng vs thực tế (3-5 trang)
- [ ] Bảng comparison
- [ ] Giải thích chênh lệch (reuse, sprint...)
- [ ] Bài học

---

## ⭐ PHẦN IV: KỸ THUẬT BẢO TRÌ (40-60 trang)

### 4.1. Kỹ thuật hiểu biết và đảo ngược (15-20 trang)

#### 4.1.1. Lý thuyết (3-5 trang)
- [ ] Định nghĩa Program Comprehension
- [ ] Định nghĩa Reverse Engineering
- [ ] 3 loại RE (Data, Architecture, Design)
- [ ] Các kỹ thuật (Static, Dynamic, RE, Doc Mining)

#### 4.1.2. Công cụ (5-7 trang)
- [ ] **Alvota UML 2013** (3 trang)
  - [ ] Giới thiệu công cụ
  - [ ] Hướng dẫn sử dụng
  - [ ] Screenshot: Import project
  - [ ] Screenshot: Class Diagram generated
  - [ ] Screenshot: Export PNG
- [ ] **Doxygen** (2 trang)
  - [ ] Giới thiệu
  - [ ] Config Doxyfile
  - [ ] Screenshot: HTML output
- [ ] **VS2022 Debugger** (2 trang)
  - [ ] Breakpoints
  - [ ] Watch Window
  - [ ] Call Stack

#### 4.1.3. Áp dụng cho YC2 (7-10 trang)
- [ ] Static Analysis (2 trang)
  - [ ] Bảng 4.1: Kết quả tìm kiếm
  - [ ] Code snippet frmBanLe (before)
- [ ] Dynamic Analysis (2 trang)
  - [ ] Screenshot Debugger
  - [ ] Watch values
  - [ ] Ghi chép phát hiện
- [ ] Reverse ER Diagram (1 trang)
  - [ ] Screenshot SSMS Diagram
- [ ] Reverse Class Diagram (2 trang)
  - [ ] Screenshot Alvota output
  - [ ] Phân tích patterns
- [ ] Doxygen (1 trang)
  - [ ] Screenshot HTML docs
  - [ ] Call graph

#### 4.1.4. Áp dụng cho YC7 (3-5 trang - tóm tắt)
- [ ] Tìm hiểu authentication flow
- [ ] Reverse Table NGUOI_DUNG

---

### 4.2. Kỹ thuật tái kiến tạo (15-20 trang)

#### 4.2.1. Lý thuyết Refactoring (3-5 trang)
- [ ] Định nghĩa (Martin Fowler)
- [ ] Catalog of Refactorings
- [ ] Khi nào refactor
- [ ] Red-Green-Refactor cycle

#### 4.2.2. Áp dụng cho YC2 (10-12 trang)

**Refactoring 1: Extract Method (3-4 trang)**
- [ ] Code Before (150 lines)
- [ ] Code After (30 lines + 4 methods)
- [ ] Bảng 4.3: Metrics comparison
- [ ] Giải thích lợi ích

**Refactoring 2: Strategy Pattern (4-5 trang)**
- [ ] Code Before (conditional logic)
- [ ] Code After (Strategy interfaces + concrete)
- [ ] Bảng 4.4: Metrics
- [ ] UML Before/After

**Refactoring 3: Explaining Variable (2 trang)**
- [ ] Code Before/After
- [ ] Lợi ích

**Catalog tổng hợp (1 trang)**
- [ ] Bảng 4.5: Tất cả refactorings applied

#### 4.2.3. Áp dụng cho YC7 (3-5 trang)
- [ ] Extract Singleton (PhienDangNhap)
- [ ] Extract Method (MD5)

---

### 4.3. Kỹ thuật cải tiến/kết hợp (10-15 trang)

#### 4.3.1. Kết hợp RE + Refactoring (5-7 trang)
- [ ] Flowchart quy trình tích hợp
- [ ] Bảng 4.6: Timeline YC2 (RE → Refactor)
- [ ] Giải thích từng bước

#### 4.3.2. Kỹ thuật cải tiến (3-5 trang)
- [ ] Living Documentation (CLAUDE.md)
- [ ] Async Code Review (GitHub PR)
- [ ] Simplified templates

#### 4.3.3. So sánh hiệu quả (2-3 trang)
- [ ] Bảng 4.7: Chuẩn vs Cải tiến
- [ ] Đánh giá lợi ích, trade-offs

---

## ✅ PHẦN V: CÔNG CỤ BẢO TRÌ (20-30 trang)

### 5.1. Alvota UML 2013 (5-7 trang)
- [ ] Giới thiệu công cụ
- [ ] Tính năng chính
- [ ] Hướng dẫn cài đặt
- [ ] Hướng dẫn sử dụng (screenshots)
- [ ] Ưu/nhược điểm

### 5.2. Doxygen (4-6 trang)
- [ ] Giới thiệu
- [ ] Config file
- [ ] Generate docs
- [ ] Screenshots output

### 5.3. GitHub (5-7 trang)
- [ ] Git workflow
- [ ] Branch strategy
- [ ] Pull Request process
- [ ] Screenshots PR review

### 5.4. Visual Studio 2022 (6-8 trang)
- [ ] Tính năng IDE
- [ ] Debugger (breakpoints, watch)
- [ ] Code Metrics
- [ ] Refactoring tools

---

## ✅ PHẦN VI: KẾT QUẢ VÀ ĐÁNH GIÁ (20-30 trang)

### 6.1. Kết quả thực hiện (10-15 trang)
- [ ] Screenshots 7 forms đã làm
- [ ] Demo video (links)
- [ ] Test results (100% pass)

### 6.2. So sánh ước lượng vs thực tế (5-7 trang)
- [ ] Bảng comparison (FP, LOC, Effort)
- [ ] Giải thích (reuse, sprint...)

### 6.3. Đánh giá chất lượng (5-8 trang)
- [ ] Code metrics (complexity, coupling)
- [ ] Before/After refactoring
- [ ] Test coverage

---

## ✅ PHẦN VII: TƯ VẤN TƯƠNG LAI (YC8) (10-15 trang)

- [ ] Tính năng đề xuất (5-7 trang)
  - [ ] Web application
  - [ ] Mobile app
  - [ ] Báo cáo nâng cao (BI, Dashboard)
- [ ] Roadmap (2-3 trang)
- [ ] Ước lượng chi phí (3-5 trang)

---

## ✅ PHỤ LỤC (20-40 trang)

- [ ] **Appendix A: Source Code**
  - [ ] Strategy Pattern code (5 trang)
  - [ ] Controller code (5 trang)
  - [ ] Form code (quan trọng) (5 trang)
- [ ] **Appendix B: Database Schema**
  - [ ] Full SQL script (10 trang)
  - [ ] Migration scripts (5 trang)
- [ ] **Appendix C: User Manual**
  - [ ] Hướng dẫn sử dụng cho user (10 trang)
- [ ] **Appendix D: Test Cases**
  - [ ] 50+ test cases đầy đủ (10 trang)

---

## 📊 CHECKLIST HÌNH VẼ (Minimum 30 hình)

### PHẦN II: ISO 14764
- [ ] Hình 2.1: Flowchart 8 Activities ISO
- [ ] Hình 2.2: Mapping YC vào ISO
- [ ] Hình 2.3: Qui trình Agile-inspired

### PHẦN III: Function Point
- [ ] Hình 3.1: Quy trình FP Analysis
- [ ] Hình 3.2: Decision tree EI/EO/EQ
- [ ] Hình 3.3: Pie chart phân bổ FP
- [ ] Hình 3.4: Bar chart FP theo YC

### PHẦN IV: Kỹ thuật
- [ ] Hình 4.1: Wireframe frmBanLe (FIFO mode)
- [ ] Hình 4.2: Screenshot VS Debugger
- [ ] Hình 4.3: Class Diagram (Alvota)
- [ ] Hình 4.4: Sequence Diagram YC2
- [ ] Hình 4.5: ER Diagram (SSMS)
- [ ] Hình 4.6: Screenshot Doxygen
- [ ] Hình 4.7: Flowchart RE + Refactoring
- [ ] Hình 4.8-4.15: Code Before/After (8 hình)

### PHẦN V: Công cụ
- [ ] Hình 5.1-5.3: Alvota screenshots (3 hình)
- [ ] Hình 5.4-5.5: Doxygen screenshots (2 hình)
- [ ] Hình 5.6-5.7: GitHub PR (2 hình)
- [ ] Hình 5.8-5.10: VS2022 (3 hình)

**TỔNG:** 30+ hình ✅

---

## 📋 CHECKLIST BẢNG (Minimum 40 bảng)

### PHẦN II (15 bảng)
- [ ] Bảng 2.1: 8 Activities ISO
- [ ] Bảng 2.2: Mapping YC
- [ ] Bảng 2.3-2.9: YC2 (7 bảng)
- [ ] Bảng 2.10-2.12: YC7 (3 bảng)
- [ ] Bảng 2.13-...: YC khác (rút gọn)

### PHẦN III (15 bảng)
- [ ] Bảng 3.1: 5 Function Components
- [ ] Bảng 3.2-3.8: Chi tiết 7 YC (7 bảng)
- [ ] Bảng 3.9: Tổng hợp UFP theo YC
- [ ] Bảng 3.10: Tổng hợp theo component
- [ ] Bảng 3.11: 14 GSCs
- [ ] Bảng 3.12: LOC/FP benchmarks
- [ ] Bảng 3.13: Effort distribution
- [ ] Bảng 3.14: So sánh ước lượng vs thực tế
- [ ] Bảng 3.15: Chi phí breakdown

### PHẦN IV (10 bảng)
- [ ] Bảng 4.1: Kỹ thuật Program Comprehension
- [ ] Bảng 4.2: Metrics Extract Method
- [ ] Bảng 4.3: Metrics Strategy Pattern
- [ ] Bảng 4.4: Catalog Refactorings
- [ ] Bảng 4.5: Timeline RE+Refactoring
- [ ] Bảng 4.6: Chuẩn vs Cải tiến
- [ ] Bảng 4.7-4.10: YC7 refactorings

**TỔNG:** 40+ bảng ✅

---

## 📝 CHECKLIST BIỂU MẪU ISO (14 biểu mẫu)

- [ ] **MR-001:** Modification Request - YC1
- [ ] **MR-002:** Modification Request - YC2
- [ ] **MR-003:** Modification Request - YC3
- [ ] **MR-004:** Modification Request - YC4
- [ ] **MR-005:** Modification Request - YC5
- [ ] **MR-006:** Modification Request - YC6
- [ ] **MR-007:** Modification Request - YC7

- [ ] **IA-001:** Impact Analysis - YC1
- [ ] **IA-002:** Impact Analysis - YC2
- [ ] **IA-003:** Impact Analysis - YC3
- [ ] **IA-004:** Impact Analysis - YC4
- [ ] **IA-005:** Impact Analysis - YC5
- [ ] **IA-006:** Impact Analysis - YC6
- [ ] **IA-007:** Impact Analysis - YC7

---

## 🎯 TIẾN ĐỘ THỰC HIỆN

### Đã có (từ conversation):
- ✅ Phân tích FP đầy đủ 7 YC
- ✅ Số liệu chính xác (UFP = 148, AFP = 151)
- ✅ Hiểu rõ ISO 14764 (8 activities)
- ✅ Biểu mẫu mẫu (MR-002, IA-002)
- ✅ Code thực tế (YC2, YC7 done)

### Cần làm:
- [ ] **Screenshots công cụ** (Alvota, Doxygen, VS)
  - Chạy Alvota với code → Chụp màn hình
  - Generate Doxygen → Chụp HTML
  - Debug trong VS → Chụp Watch Window
- [ ] **Viết 6 biểu mẫu MR còn lại** (MR-001, 003-007)
- [ ] **Viết 6 biểu mẫu IA còn lại** (IA-001, 003-007)
- [ ] **Vẽ diagrams còn thiếu** (Flowcharts, Charts)
- [ ] **Format Word** (Times 13pt, heading styles...)

---

## ⏰ ƯỚC LƯỢNG THỜI GIAN HOÀN THÀNH BÁO CÁO

| Task | Thời Gian | Người Làm |
|------|-----------|-----------|
| Viết PHẦN I | 3-5 giờ | 1 người |
| Viết PHẦN II | 10-15 giờ | 2 người |
| Viết PHẦN III | 5-7 giờ | 1 người (đã có số liệu) |
| Viết PHẦN IV | 10-15 giờ | 2 người |
| Viết PHẦN V | 3-5 giờ | 1 người |
| Chạy công cụ + Screenshots | 5-7 giờ | 1 người |
| Format Word + Review | 5-7 giờ | 1 người |
| **TỔNG** | **41-61 giờ** | **≈ 5-8 ngày** (team 7 người) |

---

## 🚀 KHUYẾN NGHỊ

### Phân công

**Người 1 (Lead):** PHẦN II (ISO 14764) - Quan trọng nhất
**Người 2-3:** PHẦN IV (Kỹ thuật) - Chạy công cụ + Screenshots
**Người 4:** PHẦN III (FP) - Đã có số liệu, chỉ cần format
**Người 5:** PHẦN I + V (Giới thiệu + Công cụ)
**Người 6:** PHẦN VI + VII (Kết quả + Tư vấn)
**Người 7:** Review + Format toàn bộ

### Ưu tiên

1. **PHẦN II (ISO)** - 40% điểm
2. **PHẦN IV (Kỹ thuật)** - 30% điểm
3. **PHẦN III (FP)** - 20% điểm
4. Các phần khác - 10% điểm

---

**Chúc may mắn!** 🍀
