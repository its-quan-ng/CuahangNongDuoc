# TÀI LIỆU RESEARCH LÝ THUYẾT CHO BÁO CÁO

**Mục tiêu:** Tìm hiểu sâu về các phương pháp ước lượng và qui trình bảo trì để viết báo cáo chi tiết

---

## 📚 1. ISO/IEC/IEEE 14764 - QUI TRÌNH BẢO TRÌ PHẦN MỀM

### Tổng quan
- Tiêu chuẩn quốc tế về qui trình bảo trì phần mềm
- Thay thế cho ISO/IEC 12207:1995
- Được áp dụng rộng rãi trong công nghiệp phần mềm

### Các giai đoạn bảo trì (Maintenance Process):

#### 1. **Process Implementation** (Triển khai qui trình)
- [ ] Lập kế hoạch bảo trì
- [ ] Xác định resources
- [ ] Thiết lập môi trường bảo trì
- [ ] Đào tạo team

#### 2. **Problem and Modification Analysis** (Phân tích vấn đề)
- [ ] Thu thập Problem Reports
- [ ] Phân tích tác động (Impact Analysis)
- [ ] Phân loại: Corrective, Adaptive, Perfective, Preventive
- [ ] Ước lượng effort

#### 3. **Modification Implementation** (Triển khai thay đổi)
- [ ] Design changes
- [ ] Code changes  
- [ ] Review changes
- [ ] Documentation updates

#### 4. **Maintenance Review/Acceptance** (Đánh giá)
- [ ] Testing
- [ ] Quality assurance
- [ ] User acceptance
- [ ] Sign-off

#### 5. **Migration** (Chuyển đổi - nếu cần)
- [ ] Data migration
- [ ] System migration
- [ ] Parallel running

#### 6. **Software Retirement** (Ngừng sử dụng - nếu cần)
- [ ] Notification
- [ ] Archive
- [ ] Transition plan

### Biểu mẫu cần có:
- ✅ **Problem Report Form** (Mẫu báo cáo lỗi)
- ✅ **Change Request Form** (Mẫu yêu cầu thay đổi)
- ✅ **Impact Analysis Document** (Phân tích tác động)
- ✅ **Modification Request** (Yêu cầu sửa đổi)
- ✅ **Test Plan & Test Cases** (Kế hoạch kiểm thử)
- ✅ **Acceptance Checklist** (Danh sách chấp nhận)
- ✅ **Configuration Management Log** (Nhật ký quản lý cấu hình)
- ✅ **Maintenance Report** (Báo cáo bảo trì)

### Áp dụng cho dự án:
**Loại bảo trì:** 
- **Corrective** (Sửa lỗi): SQL injection, bugs nhỏ
- **Perfective** (Cải tiến): FIFO/LIFO, chiết khấu, thống kê
- **Adaptive** (Thích nghi): Đăng nhập, phân quyền

---

## 📊 2. FUNCTION POINT ANALYSIS (FPA)

### Công thức cơ bản:
```
FP = UFP × VAF
```

Trong đó:
- **UFP** (Unadjusted Function Points) = Function Points chưa điều chỉnh
- **VAF** (Value Adjustment Factor) = Hệ số điều chỉnh giá trị

### Tính UFP:

#### 2.1. Đếm các thành phần chức năng:

**A. Internal Logical Files (ILF)** - File/Table nội bộ
- Mỗi table trong database đếm là 1 ILF
- **Độ phức tạp:**
  - Simple: 7 FP
  - Average: 10 FP  
  - Complex: 15 FP

**Ví dụ cho dự án:**
| Table | RETs | DETs | Complexity | FP |
|-------|------|------|------------|-----|
| SAN_PHAM | 1 | 10 | Average | 10 |
| KHACH_HANG | 1 | 8 | Average | 10 |
| PHIEU_BAN | 2 | 12 | Complex | 15 |
| CHI_TIET_PHIEU_BAN | 2 | 8 | Average | 10 |

**B. External Interface Files (EIF)** - File/API bên ngoài
- Không có trong dự án này (không kết nối hệ thống ngoài)

**C. External Inputs (EI)** - Nhập dữ liệu từ user
- Mỗi form thêm/sửa/xóa
- **Độ phức tạp:**
  - Simple: 3 FP
  - Average: 4 FP
  - Complex: 6 FP

**Ví dụ:**
| Function | FTRs | DETs | Complexity | FP |
|----------|------|------|------------|-----|
| Thêm sản phẩm | 1 | 10 | Average | 4 |
| Thêm khách hàng | 1 | 8 | Average | 4 |
| Nhập hàng (FIFO/LIFO) | 3 | 15 | Complex | 6 |
| Bán hàng + chiết khấu | 3 | 18 | Complex | 6 |

**D. External Outputs (EO)** - Báo cáo, xuất dữ liệu
- Mỗi report
- **Độ phức tạp:**
  - Simple: 4 FP
  - Average: 5 FP
  - Complex: 7 FP

**Ví dụ:**
| Report | FTRs | DETs | Complexity | FP |
|--------|------|------|------------|-----|
| Báo cáo tồn kho | 2 | 12 | Average | 5 |
| Hóa đơn bán hàng | 3 | 18 | Complex | 7 |
| Thống kê theo nhân viên | 3 | 15 | Complex | 7 |

**E. External Queries (EQ)** - Tra cứu
- Mỗi chức năng tìm kiếm, filter
- **Độ phức tạp:**
  - Simple: 3 FP
  - Average: 4 FP
  - Complex: 6 FP

### 2.2. Tính VAF (Value Adjustment Factor):

```
VAF = 0.65 + (0.01 × Σ GSC)
```

**14 General System Characteristics (GSC):**
1. Data communications
2. Distributed data processing
3. Performance
4. Heavily used configuration
5. Transaction rate
6. Online data entry
7. End-user efficiency
8. Online update
9. Complex processing
10. Reusability
11. Installation ease
12. Operational ease
13. Multiple sites
14. Facilitate change

Mỗi GSC đánh giá từ 0-5:
- 0 = Không ảnh hưởng
- 1 = Ảnh hưởng nhỏ
- 2 = Ảnh hưởng trung bình thấp
- 3 = Ảnh hưởng trung bình
- 4 = Ảnh hưởng đáng kể
- 5 = Ảnh hưởng lớn

**Ví dụ cho dự án:**
- Online data entry: 5 (toàn bộ nhập liệu online)
- Complex processing: 4 (FIFO/LIFO, tính toán giá)
- Online update: 5 (cập nhật realtime)
- End-user efficiency: 4 (AutoComplete, shortcuts)
- Performance: 3 (yêu cầu phản hồi nhanh)
- Reusability: 3 (Controller, Factory pattern)
- ... (tính còn lại)

Giả sử: Σ GSC = 35
=> VAF = 0.65 + (0.01 × 35) = 1.00

### 2.3. Tính Effort & Cost:

```
Effort (hours) = FP × Productivity Rate (hours/FP)
Cost (VND) = Effort × Labor Rate (VND/hour)
```

**Productivity Rate:** Thường 6-8 hours/FP cho maintenance
**Labor Rate:** 50,000 - 150,000 VND/hour (tùy level)

---

## 📈 3. COCOMO II (COnstructive COst MOdel II)

### Model: Post-Architecture

```
Effort = A × Size^E × Π(EM_i)
E = B + 0.01 × Σ(SF_j)
```

Trong đó:
- **A** = 2.94 (nominal constant)
- **Size** = KSLOC (thousands source lines of code)
- **E** = Exponent (scaling factor)
- **B** = 0.91 (base exponent)
- **SF** = Scale Factors (5 factors)
- **EM** = Effort Multipliers (17 factors)

### 3.1. Đếm SLOC (Source Lines of Code):

**Cách đếm:**
- Physical lines (bao gồm comments, blank lines)
- Logical lines (chỉ code thực thi)

**Công cụ đếm:**
```bash
# Đếm .cs files
find . -name "*.cs" | xargs wc -l
```

**Ước lượng cho dự án:**
- BusinessObject: ~1,500 lines
- Controller: ~2,000 lines
- DataLayer: ~2,000 lines
- Forms: ~15,000 lines
- DataService: ~200 lines
**Total:** ~20,700 lines ≈ **21 KSLOC**

### 3.2. Scale Factors (SF):

**5 Scale Factors** (mỗi factor 0-5):

| SF | Description | Rating | Value |
|----|-------------|--------|-------|
| PREC | Precedentedness (Độ quen thuộc) | High | 1.24 |
| FLEX | Development Flexibility | High | 1.01 |
| RESL | Architecture/Risk Resolution | Nominal | 3.04 |
| TEAM | Team Cohesion | High | 1.10 |
| PMAT | Process Maturity (CMMI) | Low | 4.68 |

**Tính E:**
```
Σ SF = 1.24 + 1.01 + 3.04 + 1.10 + 4.68 = 11.07
E = 0.91 + 0.01 × 11.07 = 1.0207
```

### 3.3. Effort Multipliers (EM):

**17 Effort Multipliers:**

#### Product Factors:
- RELY (Required Reliability): High = 1.26
- DATA (Database Size): High = 1.14  
- CPLX (Product Complexity): High = 1.34
- RUSE (Reusability): Nominal = 1.00
- DOCU (Documentation): High = 1.23

#### Platform Factors:
- TIME (Execution Time Constraint): Nominal = 1.00
- STOR (Main Storage Constraint): Nominal = 1.00
- PVOL (Platform Volatility): Low = 0.87

#### Personnel Factors:
- ACAP (Analyst Capability): High = 0.85
- PCAP (Programmer Capability): Nominal = 1.00
- PCON (Personnel Continuity): High = 0.90
- APEX (Applications Experience): Low = 1.22
- PLEX (Platform Experience): Nominal = 1.00
- LTEX (Language & Tool Experience): Nominal = 1.00

#### Project Factors:
- TOOL (Use of Software Tools): High = 0.90
- SITE (Multisite Development): Nominal = 1.00
- SCED (Schedule Constraint): Nominal = 1.00

**Tính Π(EM):**
```
Π(EM) = 1.26 × 1.14 × 1.34 × 1.00 × 1.23 × 1.00 × 1.00 × 0.87 
      × 0.85 × 1.00 × 0.90 × 1.22 × 1.00 × 1.00 × 0.90 × 1.00 × 1.00
      ≈ 1.35
```

### 3.4. Tính Effort:

```
Effort = 2.94 × 21^1.0207 × 1.35
       = 2.94 × 21.44 × 1.35
       ≈ 85 Person-Months
```

**Chuyển sang Person-Hours:**
```
Effort = 85 PM × 152 hours/PM = 12,920 hours
```

**Tính Duration (thời gian):**
```
Duration = 3.67 × Effort^0.28
         = 3.67 × 85^0.28
         ≈ 11.5 months
```

---

## 📜 4. THÔNG TƯ 2589/2010/TT-BTTTT

### Về Thông tư:
- Ban hành: 30/12/2010
- Cơ quan: Bộ Thông tin và Truyền thông Việt Nam
- Nội dung: Định mức kinh tế kỹ thuật phần mềm

### Phương pháp tính:

#### 4.1. Điểm chức năng (tương tự FPA):

**Công thức:**
```
Điểm chức năng = Σ (Số lượng × Hệ số phức tạp × Hệ số điều chỉnh)
```

**Các thành phần:**
- Input (Nhập): Đơn giản (3), TB (4), Phức tạp (6)
- Output (Xuất): Đơn giản (4), TB (5), Phức tạp (7)
- Query (Truy vấn): Đơn giản (3), TB (4), Phức tạp (6)
- File (File nội): Đơn giản (7), TB (10), Phức tạp (15)
- Interface (File ngoại): Đơn giản (5), TB (7), Phức tạp (10)

#### 4.2. Hệ số điều chỉnh (14 yếu tố):

Giống FPA GSC (0-5 cho mỗi yếu tố)

#### 4.3. Công suất lao động:

**Theo Thông tư 2589:**
- Phân tích: 15-20 ĐCN/tháng
- Thiết kế: 20-25 ĐCN/tháng
- Lập trình: 25-30 ĐCN/tháng
- Kiểm thử: 30-35 ĐCN/tháng
- **Bảo trì:** 35-40 ĐCN/tháng

**ĐCN** = Điểm chức năng

#### 4.4. Đơn giá nhân công (2010):

**Theo Thông tư (cần cập nhật 2025):**
- Cấp 1 (Junior): 1,5 - 2 triệu VND/tháng
- Cấp 2 (Middle): 2,5 - 3,5 triệu VND/tháng
- Cấp 3 (Senior): 4 - 6 triệu VND/tháng
- Cấp 4 (Expert): 7 - 10 triệu VND/tháng

**Cập nhật 2025 (tăng ~5-7 lần):**
- Junior: 8 - 12 triệu VND/tháng
- Middle: 15 - 25 triệu VND/tháng
- Senior: 25 - 40 triệu VND/tháng
- Expert: 40 - 70 triệu VND/tháng

#### 4.5. Tính chi phí:

```
Chi phí = (Điểm CN / Năng suất) × Đơn giá × Hệ số chi phí phụ
```

Hệ số chi phí phụ: 1.2 - 1.5 (bao gồm overhead, equipment, etc.)

---

## 🛠️ 5. REVERSE ENGINEERING & REENGINEERING

### 5.1. Reverse Engineering (Đảo ngược):

**Mục đích:**
- Hiểu code hiện tại
- Tạo documentation
- Phân tích dependency
- Tạo diagrams từ code

**Công cụ:**

#### **Doxygen** - Documentation Generator
- **Cài đặt:** https://www.doxygen.nl/download.html
- **Config file:** Doxyfile
- **Chạy:** `doxygen Doxyfile`

**Output:**
- HTML documentation
- Class diagrams
- Call graphs
- File dependency graphs
- Cross-references

**Cấu hình cho C#:**
```
PROJECT_NAME = "Cửa Hàng Nông Dược"
OUTPUT_DIRECTORY = docs/doxygen
INPUT = . BusinessObject Controller DataLayer
RECURSIVE = YES
EXTRACT_ALL = YES
EXTRACT_PRIVATE = YES
EXTRACT_STATIC = YES
GENERATE_HTML = YES
GENERATE_LATEX = YES
HAVE_DOT = YES
CALL_GRAPH = YES
CALLER_GRAPH = YES
```

#### **Alvota UML 2013** - UML from Code
- Tạo Class Diagrams
- Tạo Sequence Diagrams  
- Reverse engineer từ .NET assemblies

**Cách dùng:**
1. Build project → tạo .dll
2. Import .dll vào Alvota
3. Generate diagrams
4. Export to image/XMI

### 5.2. Reengineering (Tái kiến tạo):

**Process:**
1. **Analysis** - Phân tích code hiện tại
2. **Restructuring** - Sắp xếp lại cấu trúc
3. **Forward Engineering** - Thiết kế lại
4. **Implementation** - Code mới

**Techniques:**
- Code refactoring
- Design pattern application
- Architecture improvement
- Performance optimization

---

## 📋 6. BIỂU MẪU THEO ISO 14764

### 6.1. Problem Report Form (Mẫu Báo Cáo Lỗi)

```
====================================
PROBLEM REPORT
====================================
Report ID: PR-2024-001
Date: 01/11/2024
Reported by: [Tên người báo]
Priority: [High/Medium/Low]

--- Problem Description ---
[Mô tả chi tiết vấn đề]

--- Steps to Reproduce ---
1. [Bước 1]
2. [Bước 2]
...

--- Expected Behavior ---
[Hành vi mong đợi]

--- Actual Behavior ---
[Hành vi thực tế]

--- System Information ---
- OS: Windows 10/11
- .NET Framework: 4.8
- SQL Server: Express 2019

--- Screenshots/Logs ---
[Đính kèm]

--- Impact Analysis ---
Severity: [Critical/Major/Minor]
Affected Modules: [List modules]
Users Affected: [Number/All]

====================================
```

### 6.2. Change Request Form

```
====================================
CHANGE REQUEST
====================================
CR ID: CR-2024-001
Date: 01/11/2024
Requested by: [Tên]
Type: [Corrective/Perfective/Adaptive/Preventive]

--- Change Description ---
[Mô tả yêu cầu thay đổi]

--- Business Justification ---
[Lý do kinh doanh]

--- Estimated Effort ---
Hours: [số giờ]
Cost: [số tiền]

--- Risk Assessment ---
Risk Level: [Low/Medium/High]
Risks: [Danh sách rủi ro]
Mitigation: [Cách giảm thiểu]

--- Approval ---
Approved by: [Tên]
Date: [Ngày]
Signature: ______________

====================================
```

### 6.3. Test Plan Template

```
====================================
TEST PLAN
====================================
Test Plan ID: TP-2024-001
Feature: [Tên tính năng]
Version: 1.0

--- Test Objectives ---
[Mục tiêu kiểm thử]

--- Test Scope ---
In Scope: [Phạm vi trong]
Out of Scope: [Phạm vi ngoài]

--- Test Cases ---

TC-001: [Tên test case]
- Preconditions: [Điều kiện]
- Steps: [Các bước]
- Expected Result: [Kết quả mong đợi]
- Status: [Pass/Fail]

--- Test Environment ---
- Hardware: [Cấu hình]
- Software: [Phần mềm]
- Test Data: [Dữ liệu test]

--- Test Schedule ---
Start Date: [Ngày bắt đầu]
End Date: [Ngày kết thúc]

====================================
```

---

## 📊 7. SO SÁNH 3 PHƯƠNG PHÁP ƯỚC LƯỢNG

| Tiêu chí | Function Points | COCOMO II | Thông tư 2589 |
|----------|-----------------|-----------|---------------|
| **Cơ sở** | Chức năng | SLOC | Chức năng |
| **Độ chính xác** | Cao | Cao | Trung bình |
| **Thời điểm áp dụng** | Đầu dự án | Giữa dự án | Đầu dự án |
| **Phù hợp** | Tất cả loại | Dự án lớn | Dự án VN |
| **Độ phức tạp** | Trung bình | Cao | Trung bình |
| **Ưu điểm** | Độc lập ngôn ngữ | Chi tiết, chính xác | Chuẩn VN |
| **Nhược điểm** | Chủ quan | Cần có code | Lỗi thời |

---

## ✅ CHECKLIST RESEARCH

### Lý thuyết cần nắm:
- [ ] ISO 14764 - 8 giai đoạn
- [ ] Function Points - Công thức, ví dụ
- [ ] COCOMO II - Scale factors, Effort multipliers
- [ ] Thông tư 2589 - Định mức, đơn giá
- [ ] Reverse Engineering concepts
- [ ] Reengineering process

### Công cụ cần biết:
- [ ] Doxygen - Cài đặt, config, chạy
- [ ] Alvota UML - Generate diagrams
- [ ] Visual Studio Analysis Tools
- [ ] GitHub workflow

### Biểu mẫu cần tạo:
- [ ] Problem Report Form (2-3 mẫu)
- [ ] Change Request Form (2-3 mẫu)
- [ ] Impact Analysis (1-2 mẫu)
- [ ] Test Plan (1-2 mẫu)
- [ ] Test Cases (5-10 cases)
- [ ] Acceptance Checklist
- [ ] Maintenance Log

### Tính toán cần làm:
- [ ] Đếm ILF, EIF, EI, EO, EQ
- [ ] Tính UFP và VAF
- [ ] Tính Function Points total
- [ ] Đếm SLOC
- [ ] Đánh giá Scale Factors
- [ ] Đánh giá Effort Multipliers
- [ ] Tính COCOMO II effort
- [ ] Áp dụng Thông tư 2589
- [ ] So sánh 3 phương pháp
- [ ] Tính báo giá cuối cùng

---

*Document này sẽ được update khi research thêm thông tin.*

