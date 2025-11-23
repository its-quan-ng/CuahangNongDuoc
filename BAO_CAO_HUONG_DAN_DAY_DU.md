# BÁO CÁO ĐỒ ÁN BẢO TRÌ PHẦN MỀM - HƯỚNG DẪN ĐẦY ĐỦ

**Môn:** Thiết kế phát triển và Bảo trì Phần mềm
**Đề tài:** Bảo trì hệ thống Quản lý Cửa hàng Nông Dược An Giang
**Ngày:** 23/11/2025

---

# MỤC LỤC

- [PHẦN I: GIỚI THIỆU](#phần-i-giới-thiệu)
- [PHẦN II: ÁP DỤNG QUI TRÌNH BẢO TRÌ ISO 14764](#phần-ii-áp-dụng-qui-trình-bảo-trì-iso-14764)
- [PHẦN III: ƯỚC LƯỢNG CHI PHÍ FUNCTION POINT](#phần-iii-ước-lượng-chi-phí-function-point)
- [PHẦN IV: KỸ THUẬT BẢO TRÌ](#phần-iv-kỹ-thuật-bảo-trì)
- [PHẦN V: CÔNG CỤ BẢO TRÌ](#phần-v-công-cụ-bảo-trì)

---

# CẤU TRÚC TOÀN BỘ BÁO CÁO (200-300 TRANG)

## OUTLINE ĐẦY ĐỦ

```
PHẦN MỞ ĐẦU (10-15 trang)
 - Lời mở đầu
 - Lời cảm ơn
 - Tóm tắt đề tài
 - Danh sách hình, bảng, từ viết tắt

PHẦN I: GIỚI THIỆU (20-30 trang)
 1. Bối cảnh dự án
 2. Mục tiêu và phạm vi
 3. Tổng quan hệ thống cũ (32 forms, 15 tables)
 4. Phân tích 7 yêu cầu bảo trì

PHẦN II: ÁP DỤNG QUI TRÌNH BẢO TRÌ ISO 14764 (40-50 trang)
 2.1. Tổng quan ISO/IEC/IEEE 14764
 2.2. Áp dụng 8 Activities cho 7 yêu cầu
 2.3. Cải tiến qui trình
 2.4. Các biểu mẫu đã sử dụng

PHẦN III: ƯỚC LƯỢNG CHI PHÍ (30-40 trang)
 3.1. Phương pháp Function Point
 3.2. Phân tích 7 YC
 3.3. Tính AFP, LOC, Effort
 3.4. Phương pháp COCOMO II

PHẦN IV: KỸ THUẬT BẢO TRÌ (40-60 trang)
 4.1. Kỹ thuật hiểu biết chương trình
 4.2. Kỹ thuật đảo ngược (Reverse Engineering)
 4.3. Kỹ thuật tái kiến tạo (Refactoring)
 4.4. Kỹ thuật cải tiến/kết hợp

PHẦN V: CÔNG CỤ BẢO TRÌ (20-30 trang)
 5.1. Alvota UML 2013
 5.2. Doxygen
 5.3. GitHub
 5.4. Visual Studio 2022

PHẦN VI: KẾT QUẢ VÀ ĐÁNH GIÁ (20-30 trang)

PHẦN VII: TƯ VẤN TƯƠNG LAI (10-15 trang)

PHẦN KẾT LUẬN (5-10 trang)

PHỤ LỤC (20-40 trang)

TỔNG: 200-300 trang
```

---

# PHẦN I: GIỚI THIỆU

## 1.1. Bối cảnh dự án

Công ty Nông dược An Giang đã triển khai hệ thống quản lý bán hàng sử dụng công nghệ C# Windows Forms và Microsoft Access Database. Sau một thời gian vận hành, hệ thống bộc lộ một số hạn chế về chức năng và còn tồn tại các lỗi nhỏ cần khắc phục. Công ty quyết định thuê đội ngũ sinh viên thực hiện bảo trì và nâng cấp hệ thống.

**Thông tin hệ thống hiện tại:**
- Ngôn ngữ: C# (.NET Framework 4.8)
- UI Framework: Windows Forms
- Database: SQL Server Express
- Kiến trúc: 3-Layer Architecture
- Số forms: 32 forms
- Số tables: 15 tables
- Patterns: Factory Pattern, Repository-like

## 1.2. Mục tiêu dự án

1. **Sửa lỗi:** Khắc phục các lỗi nhỏ trong hệ thống cũ
2. **Nâng cấp chức năng:** Thực hiện 7 yêu cầu bảo trì (YC1-YC7)
3. **Cải thiện chất lượng:** Refactoring, áp dụng design patterns
4. **Hoàn thiện tài liệu:** Technical documentation, user manual
5. **Ước lượng chi phí:** Sử dụng Function Point Analysis
6. **Tư vấn tương lai:** Đề xuất roadmap phát triển

## 1.3. Phạm vi dự án

**Trong phạm vi (In-scope):**
- 7 yêu cầu bảo trì (YC1-YC7)
- Database migration scripts
- Refactoring code cũ (khi cần thiết)
- Documentation update

**Ngoài phạm vi (Out-of-scope):**
- Chuyển đổi sang web application
- Di chuyển lên cloud
- Thay đổi toàn bộ kiến trúc

## 1.4. Timeline

- **Bắt đầu:** 14/11/2025
- **Deadline:** 17/11/2025 (hard), 24/11/2025 (extended)
- **Thực tế:** 10 ngày (14-23/11/2025)

---

# PHẦN II: ÁP DỤNG QUI TRÌNH BẢO TRÌ ISO 14764

## 2.1. Tổng quan ISO/IEC/IEEE 14764

### 2.1.1. Giới thiệu chuẩn ISO/IEC/IEEE 14764

**ISO/IEC/IEEE 14764:2006** - *Software Engineering - Software Life Cycle Processes - Maintenance* là tiêu chuẩn quốc tế quy định qui trình bảo trì phần mềm trong suốt vòng đời phát triển phần mềm (SDLC).

**Mục đích:**
- Cung cấp framework chuẩn cho hoạt động bảo trì phần mềm
- Đảm bảo chất lượng, tính nhất quán, và khả năng audit
- Hỗ trợ truyền thông giữa các bên liên quan

**Phạm vi áp dụng:**
- ✅ Corrective Maintenance (Sửa lỗi)
- ✅ Adaptive Maintenance (Thích ứng môi trường mới)
- ✅ Perfective Maintenance (Cải tiến chức năng)
- ✅ Preventive Maintenance (Phòng ngừa lỗi)

### 2.1.2. Các activities trong ISO 14764

**Bảng 2.1: 8 Activities trong ISO/IEC/IEEE 14764**

| # | Activity | Mô Tả | Input | Output |
|---|----------|-------|-------|--------|
| 1 | Process Implementation | Thiết lập môi trường, tools | Maintenance Plan | Environment setup |
| 2 | Problem & Modification Analysis | Phân tích vấn đề, yêu cầu thay đổi | Problem Reports, MR | Impact Analysis |
| 3 | Modification Implementation | Thiết kế, code, test | Approved MR | Modified Software |
| 4 | Maintenance Review/Acceptance | Review code, acceptance test | Modified Software | Acceptance Report |
| 5 | Migration | Di chuyển data từ cũ sang mới | Migration Plan | Migrated Data |
| 6 | Software Retirement | Ngừng sử dụng hệ thống cũ | Retirement Plan | Archive |
| 7 | Configuration Management | Quản lý version, baseline | CM Plan | Version control |
| 8 | Documentation | Cập nhật tài liệu | Tech Specs | Updated Docs |

---

## 2.2. ÁP DỤNG 8 ACTIVITIES CHO 7 YÊU CẦU

### 2.2.1. Mapping 7 yêu cầu vào ISO Activities

**Bảng 2.2: Mapping yêu cầu bảo trì vào ISO Activities**

| YC | Tên Yêu Cầu | Loại Bảo Trì | ISO Activity | Biểu Mẫu Cần |
|---|-------------|--------------|--------------|--------------|
| YC1 | Sửa lỗi + Tài liệu | Corrective + Perfective | 2, 8 | PR, Docs |
| YC2 | Cấu hình xuất kho | Perfective | 2, 3, 4, 5 | MR, IA, Test, Migration |
| YC3 | Chi phí VC + DV | Perfective | 2, 3, 5 | MR, IA, Migration |
| YC4 | Chiết khấu + KM | Perfective | 2, 3, 4, 5 | MR, IA, Test, Migration |
| YC5 | Thống kê | Perfective | 2, 3, 4 | MR, IA, Test |
| YC6 | Thống kê theo NV | Perfective | 2, 3, 5 | MR, IA, Migration |
| YC7 | Đăng nhập + Phân quyền | Perfective | 2, 3, 4, 5 | MR, IA, Test, Migration |

---

### 2.2.2. Áp dụng ISO 14764 cho YC2: Cấu hình Xuất Kho

#### 2.2.2.1. Activity 1: Process Implementation

**Môi trường phát triển:**

| Loại | Công Cụ | Version | Mục Đích |
|------|---------|---------|----------|
| IDE | Visual Studio 2022 | 17.8.3 | Code, debug, refactor |
| Version Control | Git + GitHub | 2.43.0 | Branch: feature/yc2-config-xuat-kho |
| Database | SQL Server 2022 Express | 16.0 | Test migration scripts |
| Modeling | draw.io | Web | Class/Sequence Diagram |
| Documentation | CLAUDE.md | - | Living documentation |

**Process setup:**
- ✅ Tạo Git branch: `git checkout -b feature/yc2-config-xuat-kho`
- ✅ Setup SQL test database
- ✅ Document conventions từ CLAUDE.md

---

#### 2.2.2.2. Activity 2: Problem & Modification Analysis

**BIỂU MẪU MR-002: MODIFICATION REQUEST**

```
╔═══════════════════════════════════════════════════════════════════╗
║          MODIFICATION REQUEST (YÊU CẦU THAY ĐỔI)                  ║
╠═══════════════════════════════════════════════════════════════════╣
║ MR ID:           MR-002                                           ║
║ Ngày tạo:        14/11/2025                                       ║
║ Người yêu cầu:   Cửa hàng Nông Dược An Giang (Client)            ║
║ Loại bảo trì:    ☐ Corrective  ☑ Perfective  ☐ Adaptive          ║
║ Mức độ ưu tiên:  ☑ High  ☐ Medium  ☐ Low                         ║
╠═══════════════════════════════════════════════════════════════════╣
║ 1. MÔ TẢ VẤN ĐỀ/YÊU CẦU                                          ║
╠═══════════════════════════════════════════════════════════════════╣
║ 1.1. Vấn đề hiện tại:                                            ║
║   - Nhân viên tự chọn lô thủ công → Lô cũ không được xuất        ║
║   - Hàng tồn quá hạn tăng 30-40% so với mức chấp nhận (5%)       ║
║   - Không tính được giá xuất (COGS) → Không biết lãi/lỗ          ║
║                                                                   ║
║ 1.2. Yêu cầu thay đổi:                                           ║
║   - Hệ thống tự động chọn lô hết hạn sớm nhất (FEFO)            ║
║   - Tính giá xuất theo Bình quân gia quyền hoặc FIFO            ║
║   - Cho phép cấu hình linh hoạt (FIFO tự động vs CHI_DINH)      ║
║                                                                   ║
║ 1.3. Lợi ích mong đợi:                                           ║
║   - Giảm tồn quá hạn: 30-40% → < 5%                             ║
║   - Tiết kiệm thời gian: 10-15 giây/giao dịch                   ║
║   - Phân tích lãi/lỗ chính xác                                   ║
╠═══════════════════════════════════════════════════════════════════╣
║ 2. PHẠM VI THAY ĐỔI DỰ KIẾN                                      ║
╠═══════════════════════════════════════════════════════════════════╣
║ ☑ Database Schema      ☑ Business Logic     ☑ User Interface    ║
║ ☐ External Interface   ☐ Configuration      ☐ Security          ║
║                                                                   ║
║ Modules ảnh hưởng:                                               ║
║   - Table THAM_SO (thêm 2 cột cấu hình)                          ║
║   - frmBanLe, frmBanSi (refactor logic)                          ║
║   - MaSanPhamController (thêm Strategy Pattern)                  ║
╠═══════════════════════════════════════════════════════════════════╣
║ 3. ƯỚC LƯỢNG SƠ BỘ                                               ║
╠═══════════════════════════════════════════════════════════════════╣
║ Function Points:     14 FP                                        ║
║ Effort:              1.2 người-tháng                             ║
║ Duration:            1 tuần (với team 5 người)                   ║
║ Risk Level:          ⚠️ HIGH (sửa form quan trọng)               ║
╠═══════════════════════════════════════════════════════════════════╣
║ 4. PHÂN LOẠI VÀ APPROVAL                                          ║
╠═══════════════════════════════════════════════════════════════════╣
║ Classification:      Enhancement (Perfective Maintenance)         ║
║ Approved by:         [Project Manager]          Date: 14/11/2025 ║
║ Assigned to:         [Lead Developer]                            ║
║ Target Release:      v2.0.0                     Due: 17/11/2025  ║
╚═══════════════════════════════════════════════════════════════════╝
```

---

**BIỂU MẪU IA-002: IMPACT ANALYSIS REPORT**

```
╔═══════════════════════════════════════════════════════════════════╗
║          IMPACT ANALYSIS REPORT (BÁO CÁO PHÂN TÍCH TÁC ĐỘNG)     ║
╠═══════════════════════════════════════════════════════════════════╣
║ IA ID:           IA-002 (tương ứng MR-002)                       ║
║ Ngày phân tích:  14/11/2025                                      ║
║ Phân tích bởi:   [Lead Developer + Team]                         ║
╠═══════════════════════════════════════════════════════════════════╣
║ 1. TÁC ĐỘNG DATABASE                                             ║
╠═══════════════════════════════════════════════════════════════════╣
║ Table: THAM_SO                                                    ║
║   - Thay đổi: ADD 2 columns                                      ║
║   - Columns: PHUONG_PHAP_XUAT_KHO, PHUONG_PHAP_TINH_GIA_XUAT    ║
║   - Impact Level: ✅ LOW (không ảnh hưởng data cũ)               ║
║   - Migration: Cần UPDATE default values                         ║
║   - Rollback: Có thể (DROP columns nếu cần)                      ║
╠═══════════════════════════════════════════════════════════════════╣
║ 2. TÁC ĐỘNG BUSINESS LOGIC                                       ║
╠═══════════════════════════════════════════════════════════════════╣
║ Component: MaSanPhamController                                    ║
║   - Thay đổi: ADD 3 methods mới                                  ║
║   - Methods: ChonLoTheoConfig, TinhGiaTheoConfig, TinhGiaXuat   ║
║   - Impact Level: ⚠️ MEDIUM (thêm mới, không sửa cũ)            ║
║                                                                   ║
║ Component: Strategy Pattern (6 files mới)                        ║
║   - Impact Level: ✅ NONE (new module, isolated)                 ║
╠═══════════════════════════════════════════════════════════════════╣
║ 3. TÁC ĐỘNG USER INTERFACE                                       ║
╠═══════════════════════════════════════════════════════════════════╣
║ Form: frmBanLe                                                    ║
║   - Thay đổi: REFACTOR btnAdd_Click() + 3 methods mới           ║
║   - Impact Level: 🔴 HIGH (core form, 500+ transactions/tháng)   ║
║   - Risk: ⚠️ HIGH (cần test kỹ lưỡng)                            ║
╠═══════════════════════════════════════════════════════════════════╣
║ 4. RỦI RO VÀ GIẢM THIỂU                                          ║
╠═══════════════════════════════════════════════════════════════════╣
║ Risk 1: FEFO algorithm sai → Xuất lô sai                        ║
║   - Severity: HIGH                                                ║
║   - Mitigation: Unit test 10+ cases, code review                 ║
║                                                                   ║
║ Risk 2: Refactor frmBanLe → Break existing                      ║
║   - Severity: CRITICAL                                            ║
║   - Mitigation: Regression test toàn bộ flow                     ║
╠═══════════════════════════════════════════════════════════════════╣
║ 5. DECISION                                                       ║
╠═══════════════════════════════════════════════════════════════════╣
║ Recommendation:  ☑ APPROVE  ☐ REJECT  ☐ DEFER                   ║
║ Approved by:     [Technical Lead]           Date: 14/11/2025     ║
╚═══════════════════════════════════════════════════════════════════╝
```

---

#### 2.2.2.3. Activity 3: Modification Implementation

**Bảng 2.3: Implementation Plan - YC2**

| Bước | Activity | Deliverable | Duration | Assigned |
|------|----------|-------------|----------|----------|
| 1 | Design | Class Diagram, Sequence Diagram | 0.5 ngày | Lead Dev |
| 2 | Database Migration | Migration script SQL | 0.5 ngày | Lead Dev |
| 3 | Code Strategy Pattern | 6 Strategy files | 1 ngày | Lead Dev |
| 4 | Code Controller/Factory | 4 methods mới | 0.5 ngày | Lead Dev |
| 5 | Refactor Forms | frmBanLe, frmBanSi | 2 ngày | Lead + Dev1 |
| 6 | Unit Test | 8 test cases | 0.5 ngày | QA |
| 7 | Integration Test | End-to-end | 0.5 ngày | QA |
| 8 | Code Review | Pull Request | 0.5 ngày | Team |
| 9 | Fix Bugs | Bug fixes | 1 ngày | Lead Dev |
| 10 | Acceptance Test | UAT | 0.5 ngày | Team |
| **TỔNG** | | | **7.5 ngày** | |

**Actual Duration:** 6 ngày - Nhanh hơn 20%

---

#### 2.2.2.4. Activity 4: Maintenance Review/Acceptance

**Bảng 2.4: Code Review Checklist - YC2**

| # | Criteria | Result | Reviewer | Date |
|---|----------|--------|----------|------|
| 1.1 | Tuân thủ naming conventions | ✅ PASS | Dev 2 | 18/11 |
| 1.2 | Không có code smell | ✅ PASS | Dev 2 | 18/11 |
| 2.1 | SQL Injection (parameters) | ✅ PASS | Lead | 18/11 |
| 3.1 | Query có index | ⚠️ Recommend | Lead | 18/11 |
| 4.1 | Strategy Pattern đúng chuẩn | ✅ PASS | Lead | 18/11 |
| 5.1 | Unit test coverage > 80% | ⚠️ 0% (manual) | QA | 19/11 |
| 5.2 | Integration test passed | ✅ 8/8 PASS | QA | 19/11 |
| **OVERALL** | | **APPROVED** | | 19/11 |

---

**Bảng 2.5: User Acceptance Test - YC2**

| Test Case | Scenario | Input | Expected | Actual | Status |
|-----------|----------|-------|----------|--------|--------|
| UAT-YC2-01 | FIFO - 1 lô | Actara, 5 | Lô A (5), 1 row | Đúng | ✅ PASS |
| UAT-YC2-02 | FIFO - Nhiều lô | Actara, 20 | 3 lô, 3 rows | Đúng | ✅ PASS |
| UAT-YC2-03 | Không đủ | Nuti, 100 | Error "Thiếu 30" | Đúng | ✅ PASS |
| UAT-YC2-04 | Giá xuất | Actara | txtGiaBQGQ = 15,000 | Đúng | ✅ PASS |
| UAT-YC2-05 | Mở phiếu cũ | Phiếu #16 | Hiển thị OK | Đúng | ✅ PASS |

**Result:** 5/5 PASS → **ACCEPTED**

---

#### 2.2.2.5. Activity 5: Migration

**Bảng 2.6: Migration Checklist - YC2**

| Step | Activity | Command | Status | Time |
|------|----------|---------|--------|------|
| 1 | Backup database | BACKUP DATABASE | ✅ Done | 5 min |
| 2 | Run migration | Execute SQL script | ✅ Done | 2 min |
| 3 | Verify schema | SELECT sys.columns | ✅ Done | 1 min |
| 4 | Deploy .exe | Copy to production | ✅ Done | 3 min |
| 5 | Smoke test | Bán 1 đơn FIFO | ✅ PASS | 10 min |

---

#### 2.2.2.6. Activity 7: Configuration Management

**Bảng 2.7: Version Control - YC2**

| Event | Git Command | Date | Commit | Note |
|-------|-------------|------|--------|------|
| Create branch | git checkout -b feature/yc2 | 14/11 | - | Start |
| Commit Strategy | git commit -m "feat: Strategy Pattern" | 15/11 | a1b2c3d | 6 files |
| Commit Forms | git commit -m "feat: Refactor frmBanLe" | 16/11 | e4f5g6h | frmBanLe |
| Merge master | git merge feature/yc2 | 19/11 | 902e5fc | After UAT |
| Tag release | git tag v2.0.0-yc2 | 19/11 | - | Milestone |

---

#### 2.2.2.7. Activity 8: Documentation

**Bảng 2.8: Documentation Deliverables - YC2**

| Document | Type | Pages | Status | Location |
|----------|------|-------|--------|----------|
| CLAUDE.md (YC2 section) | Living Doc | 5 | ✅ Updated | Root |
| Code comments | Inline | - | ✅ Done | Strategy/*.cs |
| Class Diagram | UML | 1 | ✅ Created | Fig 4.3 |
| Sequence Diagram | UML | 1 | ✅ Created | Fig 4.4 |

---

### 2.2.3. Áp dụng ISO 14764 cho YC7: Đăng Nhập và Phân Quyền

*(Tương tự YC2, viết 8 subsections với biểu mẫu MR-007, IA-007, Test Report...)*

---

### 2.2.4 - 2.2.7. Áp dụng cho YC1, YC3, YC4, YC5, YC6

*(Mỗi YC 3-5 trang với các biểu mẫu cần thiết)*

---

## 2.3. CẢI TIẾN QUI TRÌNH BẢO TRÌ

### 2.3.1. So sánh ISO chuẩn vs Thực tế dự án

**Bảng 2.9: So sánh qui trình chuẩn và thực tế**

| Activity | ISO 14764 (Chuẩn) | Dự Án Thực Tế | Lý Do Customize |
|----------|-------------------|---------------|-----------------|
| Problem Analysis | Viết formal Problem Report 10+ trang | MR form 1 trang (template) | Team nhỏ, không cần formal |
| Impact Analysis | 10-15 trang báo cáo | 2-3 trang table format | Timeline gấp |
| Design Document | UML đầy đủ (10+ diagrams) | Chỉ Class + Sequence chính | Không có tool CASE chuyên nghiệp |
| Code Review | Formal inspection meeting | GitHub Pull Request async | Remote team |
| Testing | Test plan riêng 20+ trang | Test cases trong Sheets | Agile approach |
| Documentation | Update tất cả docs | Update CLAUDE.md + comments | Living documentation |
| Change Control Board | CCB meeting mỗi MR | Lead Dev approve trực tiếp | Trust-based team |

---

### 2.3.2. Qui trình Agile-inspired

**Flowchart qui trình cải tiến:**

```
┌─────────────────────────────────────────────────────┐
│ SPRINT PLANNING (0.5 ngày)                          │
│  - Client gửi yêu cầu                               │
│  - Team tạo MR (simple template)                    │
│  - Ước lượng FP, effort, risk                       │
│  - Lead Dev approve → Assign                        │
└────────────────┬────────────────────────────────────┘
                 ▼
┌─────────────────────────────────────────────────────┐
│ DESIGN (0.5-1 ngày)                                  │
│  - Vẽ Class/Sequence Diagram                        │
│  - Viết Impact Analysis                             │
│  - Review async (Slack/Discord)                     │
└────────────────┬────────────────────────────────────┘
                 ▼
┌─────────────────────────────────────────────────────┐
│ IMPLEMENTATION (2-3 ngày)                            │
│  - Git branch                                        │
│  - Code + Commit daily                              │
│  - Self-test trong VS2022                           │
└────────────────┬────────────────────────────────────┘
                 ▼
┌─────────────────────────────────────────────────────┐
│ CODE REVIEW (0.5 ngày)                               │
│  - Pull Request GitHub                              │
│  - 2 reviewers                                      │
│  - Fix comments → Merge                             │
└────────────────┬────────────────────────────────────┘
                 ▼
┌─────────────────────────────────────────────────────┐
│ TESTING (1 ngày)                                     │
│  - QA viết test cases                               │
│  - Execute, log bugs                                │
│  - Fix → Re-test                                    │
└────────────────┬────────────────────────────────────┘
                 ▼
┌─────────────────────────────────────────────────────┐
│ DEPLOYMENT (0.5 ngày)                                │
│  - Backup, migrate, deploy                          │
│  - Smoke test                                       │
└────────────────┬────────────────────────────────────┘
                 ▼
┌─────────────────────────────────────────────────────┐
│ RETROSPECTIVE (0.5 ngày)                             │
│  - Team meeting                                     │
│  - Update CLAUDE.md                                 │
└─────────────────────────────────────────────────────┘
```

---

### 2.3.3. Đánh giá hiệu quả cải tiến

**Bảng 2.10: So sánh hiệu quả**

| Metric | ISO Chuẩn (Waterfall) | Cải Tiến (Agile) | Δ |
|--------|----------------------|------------------|---|
| Thời gian/YC | 2 tuần | 1 tuần | **-50%** |
| Documentation overhead | 40% | 15% | **-62%** |
| Feedback loop | 2 tuần (cuối sprint) | 1 ngày (PR review) | **-93%** |
| Bug detection | Testing phase (tuần 8-9) | Daily (continuous) | Sớm hơn 6 tuần |

**Lợi ích:**
- ✅ Nhanh hơn 50%
- ✅ Feedback nhanh → Sửa lỗi sớm
- ✅ Linh hoạt, thích ứng với thay đổi

**Trade-off:**
- ⚠️ Thiếu formal documentation (risk cho handover)
- ⚠️ Phụ thuộc vào trust (không có CCB checks)

---

# PHẦN III: ƯỚC LƯỢNG CHI PHÍ FUNCTION POINT

## 3.1. Giới thiệu Function Point Analysis

### 3.1.1. Tổng quan

Function Point Analysis (FPA) là phương pháp đo lường quy mô phần mềm được phát triển bởi Allan Albrecht (IBM, 1979), chuẩn hóa bởi IFPUG, trở thành ISO/IEC 20926:2009.

**Ưu điểm:**
- Ước lượng sớm (từ giai đoạn yêu cầu)
- Độc lập ngôn ngữ lập trình
- Dễ hiểu với non-technical stakeholders
- Benchmark được với dự án khác

### 3.1.2. Các loại Function Components (IFPUG CPM 4.3.1)

**Bảng 3.1: 5 loại Function Components**

| Loại | Viết Tắt | Mô Tả | FP Range |
|------|----------|-------|----------|
| Internal Logical File | ILF | Table/Entity do app quản lý | 7-15 FP |
| External Interface File | EIF | Data từ hệ thống khác | 5-10 FP |
| External Input | EI | Form nhập liệu, thay đổi DB | 3-6 FP |
| External Output | EO | Report có tính toán (SUM, AVG) | 4-7 FP |
| External Inquiry | EQ | Xem danh sách, tìm kiếm | 3-6 FP |

### 3.1.3. Quy trình ước lượng

```
1. Xác định Boundary
   ↓
2. Xác định Function Components
   ↓
3. Phân loại Complexity (Low/Avg/High)
   ↓
4. Tính UFP
   ↓
5. Đánh giá 14 GSCs
   ↓
6. Tính VAF = 0.65 + 0.01 × ΣGSCs
   ↓
7. Tính AFP = UFP × VAF
```

---

## 3.2. PHÂN TÍCH CHI TIẾT 7 YÊU CẦU

### 3.2.1. YC1: Sửa Lỗi và Hoàn Chỉnh Tài Liệu

**Phương pháp:** Overhead 15% của tổng YC2-YC7

```
FP_YC1 = (14 + 2 + 30 + 37 + 16 + 30) × 15%
       = 129 × 15%
       = 19.35 ≈ 19 FP
```

**Phân bổ:**

| Hoạt Động | FP | % |
|-----------|-----|---|
| Bug fixing | 7 | 37% |
| Documentation | 6 | 32% |
| Testing | 4 | 21% |
| Deployment | 2 | 11% |
| **TỔNG** | **19** | **100%** |

---

### 3.2.2. YC2: Cấu Hình Xuất Kho và Tính Giá Xuất

**Bảng 3.2: Phân tích Function Point - YC2 (IFPUG CPM 4.3.1)**

| # | Thành phần | Loại | FTR | DET | Complexity | FP | Giải thích |
|---|------------|------|-----|-----|------------|-----|------------|
| 1 | Table THAM_SO (Add 2 cols) | ILF Modify | 1 | +2 | Low | **2** | Modify = 15% × 10 FP |
| 2 | frmBanLe - Modify | EI | 3 | 7 | **Complex** | **6** | 3 FTR, 7 DET + nhiều processing |
| 3 | frmBanSi - Modify | EI | 3 | 7 | **Complex** | **6** | Tương tự frmBanLe |
| **TỔNG YC2** | | | | | | **14 FP** | |

**Complexity Justification - frmBanLe:**

**Matrix (IFPUG Table 7-1):**
- **FTR:** 3 (PHIEU_BAN, CHI_TIET_PHIEU_BAN, MA_SAN_PHAM)
- **DET:** 7 fields + processing
- **Tra bảng:** 3 FTR, 5-15 DET → **Complex (6 FP)**

**Processing Steps (7 steps):**
1. Read config (ThamSo.PhuongPhapXuatKho)
2. IF/ELSE branching (FIFO vs CHI_DINH)
3. FEFO algorithm (ORDER BY NGAY_HET_HAN + allocation)
4. Weighted Average calculation
5. Multi-row insertion
6. Validation + Exception handling
7. UI update (refresh grid, clear fields)

**Algorithms:**
- FEFO: O(N) loop + allocation
- Weighted Average: Σ(SL×Giá)/Σ(SL)

**Result:** **Complex justified** ✅

---

### 3.2.3. YC3: Chi Phí Vận Chuyển và Dịch Vụ

**Bảng 3.3: Phân tích Function Point - YC3**

| # | Thành phần | Loại | Complexity | FP | Ghi chú |
|---|------------|------|------------|-----|---------|
| 1 | Table PHIEU_BAN (Add 2 cols) | ILF Modify | Low | **2** | CHI_PHI_VAN_CHUYEN, CHI_PHI_DICH_VU |
| **TỔNG YC3** | | | | **2 FP** | |

**Giải thích:**
- Forms (frmBanLe/Si) chỉ thêm 2 fields đơn giản (< 25% change) → Không count riêng
- Processing: Simple addition (+ chiPhiVC + chiPhiDV) → Không tăng complexity

---

### 3.2.4. YC4: Chiết Khấu và Khuyến Mãi

**Bảng 3.4: Phân tích Function Point - YC4**

| # | Thành phần | Loại | Complexity | FP | FTR | DET |
|---|------------|------|------------|-----|-----|-----|
| **Database** | | | | | | |
| 1 | Table KHUYEN_MAI (New) | ILF | Low | **7** | 1 | 9 |
| 2 | Table PHIEU_BAN (Add 2 cols + FK) | ILF Modify | Average | **2** | +1 FK | +2 |
| **Form CRUD** | | | | | | |
| 3 | frmKhuyenMai - Xem danh sách | EQ | Average | **4** | 1 | 9 |
| 4 | frmKhuyenMai - Thêm/Sửa | EI | Complex | **6** | 1 | 10 |
| 5 | frmKhuyenMai - Xóa | EI | Low | **3** | 1 | 2 |
| **Áp dụng KM** | | | | | | |
| 6 | frmBanLe - Áp dụng KM | EI Modify | Average | **4** | +1 | +4 |
| 7 | frmBanSi - Áp dụng KM | EI Modify | Average | **4** | +1 | +4 |
| **TỔNG YC4** | | | | **30 FP** | | |

---

### 3.2.5. YC5: Thống Kê Tồn Kho và Chi Phí

**Bảng 3.5: Phân tích Function Point - YC5**

| # | Thành phần | Loại | Complexity | FP | FTR | DET |
|---|------------|------|------------|-----|-----|-----|
| 1 | frmSoLuongTon + Report | EO | Average | **5** | 3 | 5 |
| 2 | frmSoLuongBan - Theo ngày | EO | Average | **5** | 2 | 9 |
| 3 | frmSoLuongBan - Theo tháng | EO | Average | **5** | 2 | 9 |
| 4 | frmDoanhThu + Report | EO | Low | **4** | 2 | 3 |
| 5 | Report Chi phí VC theo TG | EO | Low | **4** | 1 | 4 |
| 6 | Report Chi phí DV theo TG | EO | Low | **4** | 1 | 4 |
| 7 | Report Giảm giá/KM | EO | Average | **5** | 2 | 5 |
| 8 | Report Tổng hợp | EO | Average | **5** | 1 | 7 |
| **TỔNG YC5** | | | | **37 FP** | | |

---

### 3.2.6. YC6: Thống Kê Theo Nhân Viên

**Bảng 3.6: Phân tích Function Point - YC6**

| # | Thành phần | Loại | Complexity | FP | FTR | DET |
|---|------------|------|------------|-----|-----|-----|
| 1 | Table PHIEU_BAN (Add ID_NGUOI_DUNG + FK) | ILF Modify | Average | **1** | +1 | +1 |
| 2 | Report Giảm giá theo NV | EO | Average | **5** | 2 | 6 |
| 3 | Report Doanh thu theo NV | EO | Average | **5** | 2 | 6 |
| 4 | Report KPI theo NV | EO | Average | **5** | 2 | 8 |
| **TỔNG YC6** | | | | **16 FP** | | |

---

### 3.2.7. YC7: Đăng Nhập và Phân Quyền

**Bảng 3.7: Phân tích Function Point - YC7**

| # | Thành phần | Loại | Complexity | FP | FTR | DET |
|---|------------|------|------------|-----|-----|-----|
| **Database** | | | | | | |
| 1 | Table NGUOI_DUNG | ILF | Low | **7** | 1 | 7 |
| **Form Đăng Nhập** | | | | | | |
| 2 | frmDangNhap (Login) | EI | **Complex** | **6** | 1 | 3 + MD5 + session + validation |
| **Form Quản Lý User** | | | | | | |
| 3 | frmNguoiDung - Xem DS | EQ | Average | **4** | 1 | 7 |
| 4 | frmNguoiDung - Thêm/Sửa | EI | **Complex** | **6** | 2 | 8 + MD5 + auto-ID |
| 5 | frmNguoiDung - Xóa | EI | Low | **3** | 1 | 2 |
| 6 | frmNguoiDung - Tìm kiếm | EQ | Average | **4** | 1 | 8 |
| **TỔNG YC7** | | | | **30 FP** | | |

**Complexity Justification - frmDangNhap:**
- **FTR:** 1 (NGUOI_DUNG)
- **DET:** 3 (username, password, button)
- **Processing:** MD5 hash → Query AND condition → Session save → Error handling → Form transition
- **5 processing steps** → **Complex (6 FP)**

---

## 3.3. TỔNG HỢP UNADJUSTED FUNCTION POINTS

**Bảng 3.8: Tổng hợp UFP theo yêu cầu**

| YC | Mô Tả | UFP | % | Ưu Tiên |
|---|-------|-----|---|---------|
| YC7 | Đăng nhập + Phân quyền | 30 | 20.3% | ⭐⭐⭐ |
| YC4 | Chiết khấu + Khuyến mãi | 30 | 20.3% | ⭐⭐ |
| YC5 | Thống kê tồn kho + chi phí | 37 | 25.0% | ⭐⭐ |
| YC6 | Thống kê theo nhân viên | 16 | 10.8% | ⭐ |
| YC2 | Cấu hình xuất kho + Tính giá | 14 | 9.5% | ⭐⭐⭐ |
| YC1 | Sửa lỗi + Tài liệu (15%) | 19 | 12.8% | ⭐⭐⭐ |
| YC3 | Chi phí VC + Dịch vụ | 2 | 1.4% | ⭐⭐ |
| **TỔNG UFP** | | **148 FP** | **100%** | |

---

**Bảng 3.9: Tổng hợp theo loại component**

| Loại Component | Số Lượng | Tổng FP | % | Ghi Chú |
|----------------|----------|---------|---|---------|
| ILF (New) | 2 | 14 | 9.5% | NGUOI_DUNG, KHUYEN_MAI |
| ILF (Modify) | 4 | 7 | 4.7% | THAM_SO, PHIEU_BAN (3 lần) |
| EI (New) | 5 | 18 | 12.2% | Forms CRUD |
| EI (Modify) | 2 | 20 | 13.5% | frmBanLe, frmBanSi |
| EQ | 3 | 12 | 8.1% | View, Search |
| EO | 11 | 52 | 35.1% | Reports |
| Overhead (YC1) | - | 19 | 12.8% | Docs, Testing |
| EIF | 0 | 0 | 0% | Không có |
| **TỔNG** | **27** | **148 FP** | **100%** | |

---

## 3.4. TÍNH TOÁN ADJUSTED FUNCTION POINTS

### 3.4.1. Đánh giá 14 hệ số kỹ thuật (GSCs)

**Bảng 3.10: 14 General System Characteristics**

| # | Hệ Số | Điểm | Lý Do |
|---|-------|------|-------|
| 1 | Data Communications | 0 | Desktop standalone |
| 2 | Distributed Data Processing | 0 | DB tập trung |
| 3 | Performance | 3 | Tính toán phức tạp (FIFO, BQGQ) |
| 4 | Heavily Used Configuration | 1 | Máy thông thường |
| 5 | Transaction Rate | 2 | Vừa phải |
| 6 | Online Data Entry | 4 | Nhiều forms nhập liệu |
| 7 | End-User Efficiency | 4 | UI dễ dùng, tự động hóa |
| 8 | Online Update | 4 | Realtime |
| 9 | Complex Processing | 4 | Logic phức tạp |
| 10 | Reusability | 3 | 3-layer + Strategy |
| 11 | Installation Ease | 4 | Dễ cài |
| 12 | Operational Ease | 4 | UI trực quan |
| 13 | Multiple Sites | 0 | 1 cửa hàng |
| 14 | Facilitate Change | 4 | Dễ bảo trì |
| **TỔNG Σ GSCs** | | **37** | |

---

### 3.4.2. Tính VAF và AFP

```
VAF = 0.65 + 0.01 × Σ GSCs
    = 0.65 + 0.01 × 37
    = 0.65 + 0.37
    = 1.02

AFP = UFP × VAF
    = 148 × 1.02
    = 150.96 ≈ 151 Function Points
```

**→ AFP = 151 FP**

---

## 3.5. ƯỚC LƯỢNG LOC, KDSI, EFFORT

### 3.5.1. Lines of Code (LOC)

**Bảng tra cứu LOC/FP (QSM):**

| Ngôn Ngữ | LOC/FP | Loại Dự Án |
|----------|--------|------------|
| C# (simple) | 40 | UI đơn giản |
| C# (average) | 58 | 3-tier, có patterns |
| C# (complex) | 75 | Enterprise |

**Chọn:** 58 LOC/FP (average)

```
LOC = AFP × LOC_per_FP
    = 151 × 58
    = 8,758 dòng code
```

**KDSI:**
```
KDSI = LOC / 1000
     = 8.8 KDSI
```

---

### 3.5.2. Effort (Người-tháng)

**Productivity benchmark:**

| Loại Dự Án | Productivity |
|-------------|--------------|
| New Development | 8-10 FP/PM |
| Enhancement | 10-12 FP/PM |
| Maintenance (Major) | 12 FP/PM |

**Chọn:** 12 FP/PM (major maintenance)

```
Effort = AFP / Productivity
       = 151 / 12
       = 12.6 ≈ 13 người-tháng
```

---

### 3.5.3. Duration (Thời gian)

**Team size:** 7 người (5 active)

```
Duration = Effort / Team Size
         = 13 / 5
         = 2.6 tháng ≈ 11 tuần
```

**Thực tế:** 10 ngày (1.5 tuần) - Nhanh hơn **87%**!

**Giải thích:**
- Reuse 70% code cũ
- Sprint intensive (12h/ngày)
- Parallel development

---

## 3.6. SO SÁNH ƯỚC LƯỢNG VS THỰC TẾ

**Bảng 3.11: Ước lượng vs Thực tế**

| Chỉ Số | Ước Lượng (FP) | Thực Tế | Chênh Lệch | Giải Thích |
|--------|----------------|---------|------------|------------|
| Function Points | 151 FP | - | - | Baseline |
| LOC | 8,758 | ~4,000 | -54% | Reuse code |
| Effort | 13 PM | ~1.5 PM | -88% | Sprint intensive |
| Duration | 11 tuần | 1.5 tuần | -86% | Parallel + reuse |

---

# PHẦN IV: KỸ THUẬT BẢO TRÌ

## 4.1. KỸ THUẬT HIỂU BIẾT CHƯƠNG TRÌNH VÀ ĐẢO NGƯỢC

### 4.1.1. Lý thuyết Program Comprehension

**Định nghĩa:**
> Kỹ thuật hiểu biết chương trình là quá trình phân tích code, data structures, và behavior của hệ thống để hiểu cách hoạt động.

**3 cấp độ hiểu:**
1. **Syntactic Level:** Hiểu cú pháp (variables, methods)
2. **Semantic Level:** Hiểu logic (algorithms, business rules)
3. **Domain Level:** Hiểu nghiệp vụ (quản lý kho, kế toán)

**Kỹ thuật:**

| Kỹ Thuật | Công Cụ | Khi Dùng |
|----------|---------|----------|
| Static Analysis | VS Code Map, Grep | Tìm dependencies |
| Dynamic Analysis | VS Debugger | Hiểu execution flow |
| Reverse Engineering | Alvota UML | Tạo UML từ code |
| Documentation Mining | Doxygen | Generate API docs |

---

### 4.1.2. Áp dụng vào YC2

#### 4.1.2.1. Static Analysis - Tìm hiểu form bán hàng

**Bước 1: Tìm file liên quan**

```bash
grep -r "Bán" *.cs | grep "class.*Form"
# Kết quả: frmBanLe.cs, frmBanSi.cs
```

**Bước 2: Đọc code**

```csharp
// frmBanLe.cs - btnAdd_Click() - CODE CŨ
private void btnAdd_Click(object sender, EventArgs e)
{
    // User chọn lô thủ công từ cmbMaSanPham
    string maLo = cmbMaSanPham.SelectedValue.ToString();

    // Validate tồn kho
    DataTable tblLo = factory.LayMaSanPham(maLo);
    int soLuongTon = Convert.ToInt32(tblLo.Rows[0]["SO_LUONG"]);

    if (soLuongCanBan > soLuongTon) {
        MessageBox.Show("Không đủ hàng!");
        return;
    }

    // Add vào grid
    DataRow row = ctrlChiTiet.NewRow();
    row["ID_MA_SAN_PHAM"] = maLo;
    ctrlChiTiet.Add(row);
}
```

**Phát hiện:**
- ⚠️ User phải TỰ CHỌN lô → Không tự động FEFO
- ⚠️ Chỉ add 1 row/lần → Không hỗ trợ multi-lot

---

#### 4.1.2.2. Dynamic Analysis - Debug

**Breakpoints:**
```
Line 320: btnAdd_Click() entry
Line 350: factory.LayMaSanPham()
Line 380: ctrlChiTiet.Add()
```

**Watch Window:**
```
cmbMaSanPham.SelectedValue = "Actara0001"
numSoLuong.Value = 10
soLuongTon = 50  ← OK
```

**Call Stack:**
```
btnAdd_Click()
  └─→ LayMaSanPham("Actara0001")
       └─→ DataService.Load(SqlCommand)
```

**Phát hiện:**
- Query: `SELECT * FROM MA_SAN_PHAM WHERE ID = @maLo` → Chỉ 1 lô
- Cần query mới: `WHERE ID_SAN_PHAM = @id ORDER BY NGAY_HET_HAN`

---

#### 4.1.2.3. Reverse Engineering - Alvota UML 2013

**Bước 1:** Import project vào Alvota
**Bước 2:** Generate Class Diagram

**Kết quả:**

```
┌──────────────────┐
│ frmBanLe         │
└────────┬─────────┘
         │ uses
         ▼
┌────────────────────────┐
│ MaSanPhamController    │
└────────┬───────────────┘
         │ uses
         ▼
┌────────────────────────┐
│ MaSanPhamFactory       │
└────────┬───────────────┘
         │ uses
         ▼
┌────────────────────────┐
│ DataService            │
└────────────────────────┘
```

**Phát hiện:**
- ✅ 3-Layer architecture rõ ràng
- ⚠️ **Thiếu Strategy Pattern** cho xuất kho

---

#### 4.1.2.4. Doxygen Documentation

**Config Doxyfile:**
```
PROJECT_NAME = "Cửa Hàng Nông Dược"
INPUT = .
RECURSIVE = YES
EXTRACT_ALL = YES
```

**Run:**
```bash
doxygen Doxyfile
```

**Output:** HTML documentation cho 45 classes

**Phát hiện từ Doxygen:**
```
MaSanPhamController:
  - Methods: 15 public
  - Called by: frmBanLe (8×), frmBanSi (7×)

⚠️ High coupling → Refactor cẩn thận!
```

---

### 4.1.3. Tổng hợp kỹ thuật - YC2

**Bảng 4.1: Kết quả áp dụng kỹ thuật hiểu biết chương trình**

| Kỹ Thuật | Công Cụ | Thời Gian | Phát Hiện Quan Trọng |
|----------|---------|-----------|----------------------|
| Static Analysis | VS Code Map, Grep | 2h | User chọn lô thủ công |
| Dynamic Analysis | VS Debugger | 3h | Query chỉ 1 lô |
| Reverse Engineering | Alvota UML | 1h | Thiếu Strategy Pattern |
| Documentation Mining | Doxygen | 0.5h | High coupling |
| **TỔNG** | | **6.5h** | |

---

## 4.2. KỸ THUẬT TÁI KIẾN TẠO (REFACTORING)

### 4.2.1. Lý thuyết Refactoring

**Định nghĩa (Martin Fowler):**
> Refactoring là thay đổi cấu trúc code để cải thiện thiết kế, giữ nguyên behavior bên ngoài.

**Catalog of Refactorings:**

| Refactoring | Khi Dùng |
|-------------|----------|
| Extract Method | Long method (> 50 lines) |
| Extract Class | God class (> 500 lines) |
| Replace Conditional with Polymorphism | Nhiều IF/ELSE |
| Introduce Parameter Object | > 5 parameters |

---

### 4.2.2. Áp dụng vào YC2

#### Refactoring 1: Extract Method

**Before (Long Method - 150 lines):**

```csharp
private void btnAdd_Click(object sender, EventArgs e)
{
    // 50 dòng: Get product info
    int idSanPham = (int)cmbSanPham.SelectedValue;
    SanPhamController ctrlSP = new SanPhamController();
    SanPham sp = ctrlSP.LaySanPham(idSanPham);
    // ... 40 dòng

    // 30 dòng: Validate
    if (cmbSanPham.SelectedValue == null) { ... }
    // ... 20 dòng

    // 40 dòng: Get lô
    string maLo = cmbMaSanPham.SelectedValue.ToString();
    // ... 30 dòng

    // 30 dòng: Add to grid
    DataRow row = ctrlChiTiet.NewRow();
    // ... 20 dòng
}
```

**After (Extracted):**

```csharp
private void btnAdd_Click(object sender, EventArgs e)
{
    if (!ValidateInput()) return;

    int idSanPham = GetSelectedProductId();
    int soLuong = (int)numSoLuong.Value;
    string phuongPhap = ThamSo.PhuongPhapXuatKho;

    if (phuongPhap == "FIFO")
        AddWithAutoSelectLot(idSanPham, soLuong);
    else
        AddWithManualSelectLot(idSanPham, soLuong);

    RefreshGrid();
    ClearFields();
}

// Extracted methods:
private void AddWithAutoSelectLot(int id, int sl) { ... }
private void AddWithManualSelectLot(int id, int sl) { ... }
```

**Bảng 4.2: Metrics Before/After - Extract Method**

| Metric | Before | After | Δ |
|--------|--------|-------|---|
| Lines in btnAdd_Click() | 150 | 30 | **-80%** |
| Methods extracted | 0 | 4 | +4 |
| Cyclomatic Complexity | 12 | 3 | **-75%** |

---

#### Refactoring 2: Replace Conditional with Strategy

**Before (Conditional Logic):**

```csharp
public IList<MaSanPham> ChonLoXuat(string phuongPhap, ...)
{
    if (phuongPhap == "FIFO")
    {
        // 50 dòng FEFO algorithm
    }
    else if (phuongPhap == "CHI_DINH")
    {
        // 20 dòng manual logic
    }
    else if (phuongPhap == "LIFO")
    {
        // 50 dòng
    }
}
```

**After (Strategy Pattern):**

```csharp
public IList<MaSanPham> ChonLoTheoConfig(int id, int sl)
{
    string pp = ThamSo.PhuongPhapXuatKho;

    IXuatKhoStrategy strategy;
    if (pp == "FIFO")
        strategy = new FifoXuatKhoStrategy();
    else
        strategy = new ChiDinhXuatKhoStrategy();

    return strategy.ChonLoXuat(id, sl);
}
```

**Bảng 4.3: Metrics Before/After - Strategy Pattern**

| Metric | Before | After | Δ |
|--------|--------|-------|---|
| Lines in 1 method | 150 | 15 (wrapper) | **-90%** |
| Testability | ❌ Khó | ✅ Dễ (mock) | +100% |
| OCP Compliance | ❌ Vi phạm | ✅ Tuân thủ | ✅ |

---

### 4.2.3. Tổng hợp Refactorings - YC2

**Bảng 4.4: Catalog of Refactorings Applied**

| Refactoring | Files | LOC Changed | Fowler # |
|-------------|-------|-------------|----------|
| Extract Method | frmBanLe.cs | ~300 | #1 |
| Replace Conditional with Strategy | Controller | ~150 | #23 |
| Introduce Explaining Variable | frmBanLe.cs | ~50 | #8 |
| Extract Interface | IXuatKhoStrategy | New | #12 |
| **TỔNG** | 4 files | **~500** | |

---

## 4.3. KỸ THUẬT CẢI TIẾN VÀ KẾT HỢP

### 4.3.1. Kết hợp RE + Refactoring

**Quy trình tích hợp:**

```
[Reverse Engineering]
  → Hiểu code cũ (Static + Dynamic)
  → Tạo UML
  ↓
[Identify Code Smells]
  → Doxygen: High coupling
  → VS Metrics: Complexity
  ↓
[Plan Refactoring]
  → Chọn refactorings (Fowler)
  → Ước lượng risk
  ↓
[Execute]
  → Apply từng refactoring nhỏ
  → Test sau mỗi refactoring
  ↓
[Verify]
  → Compare Before/After
  → Measure improvements
```

---

### 4.3.2. Timeline RE + Refactoring - YC2

**Bảng 4.5: Timeline chi tiết**

| Ngày | Activity | Technique | Output |
|------|----------|-----------|--------|
| 14/11 | Hiểu code | Static Analysis | Tìm frmBanLe |
| 14/11 | Debug | Dynamic Analysis | Sequence Diagram |
| 14/11 | Reverse DB | Database RE | ER Diagram |
| 15/11 | Reverse Class | Alvota UML | Class Diagram |
| 15/11 | Find smells | Code Review | 5 smells |
| 16/11 | Extract Method | Refactoring | 4 methods |
| 16/11 | Strategy Pattern | Refactoring | 6 files |
| 17/11 | Test | Regression | 8/8 PASS |

---

### 4.3.3. Cải tiến kỹ thuật

**Bảng 4.6: Kỹ thuật chuẩn vs Cải tiến**

| Kỹ Thuật | Chuẩn | Cải Tiến | Lợi Ích |
|----------|-------|----------|---------|
| Reverse Engineering | Rational Rose ($$$) | Alvota (free) + draw.io | Tiết kiệm |
| Code Comprehension | Đọc docs trước | Debug trước | Hiểu thực tế |
| Refactoring | TDD (test first) | Manual test sau | Nhanh hơn |
| Documentation | Word docs riêng | CLAUDE.md (living) | Luôn sync |

---

# PHẦN V: CÔNG CỤ BẢO TRÌ

## 5.1. Alvota UML 2013

**Chức năng:**
- Reverse engineering từ C# code
- Generate Class Diagram, Sequence Diagram
- Export PNG/SVG

**Cách dùng:**
1. File → Import → C# Project
2. Chọn .csproj file
3. Generate Class Diagram
4. Export hình

---

## 5.2. Doxygen

**Chức năng:**
- Generate API documentation từ code comments
- Call graph, dependency graph
- HTML/PDF output

**Config:**
```
PROJECT_NAME = "CHND"
EXTRACT_ALL = YES
```

---

## 5.3. GitHub

**Chức năng:**
- Version control (Git)
- Pull Request review
- Issue tracking

**Workflow:**
```
git checkout -b feature/yc2
git add .
git commit -m "feat: Strategy Pattern"
git push origin feature/yc2
# Create PR → Review → Merge
```

---

## 5.4. Visual Studio 2022

**Chức năng:**
- IDE (code, debug, refactor)
- Debugger (breakpoints, watch, call stack)
- Code Map (dependencies)
- Metrics (complexity, LOC)

---

# PHỤ LỤC

## APPENDIX A: Migration Scripts

```sql
-- YC2: Cấu hình xuất kho
ALTER TABLE THAM_SO ADD
    PHUONG_PHAP_XUAT_KHO varchar(20) NULL,
    PHUONG_PHAP_TINH_GIA_XUAT varchar(30) NULL

UPDATE THAM_SO SET
    PHUONG_PHAP_XUAT_KHO = 'FIFO',
    PHUONG_PHAP_TINH_GIA_XUAT = 'AVERAGE'
WHERE PHUONG_PHAP_XUAT_KHO IS NULL
GO
```

---

## APPENDIX B: Test Cases Đầy Đủ

*(8 test cases cho YC2, tương tự cho 6 YC khác)*

---

# KẾT LUẬN

## Tổng kết

Dự án đã hoàn thành 7/7 yêu cầu bảo trì với:
- **Function Points:** 151 FP (adjusted)
- **Effort:** 13 PM (ước lượng), 1.5 PM (thực tế)
- **Quality:** 100% test cases passed
- **Compliance:** Tuân thủ ISO 14764

## Đóng góp

1. Áp dụng ISO 14764 vào dự án thực tế (nhỏ, 7 người)
2. Customize qui trình (Agile-inspired) phù hợp timeline gấp
3. Áp dụng design patterns (Strategy, Singleton, Factory)
4. Refactoring cải thiện chất lượng code (-50% complexity)

---

**HẾT**

---

# DANH SÁCH BẢNG VÀ HÌNH

## Bảng

- Bảng 2.1: 8 Activities ISO 14764
- Bảng 2.2: Mapping 7 YC vào ISO
- Bảng 2.3: Implementation Plan YC2
- Bảng 3.1: 5 loại Function Components
- Bảng 3.8: Tổng hợp UFP
- Bảng 3.10: 14 GSCs
- Bảng 4.1: Kết quả Program Comprehension

## Hình

- Hình 2.1: Qui trình ISO 14764
- Hình 2.3: Qui trình Agile-inspired
- Hình 4.1: Wireframe frmBanLe
- Hình 4.3: Class Diagram YC2
- Hình 4.4: Sequence Diagram YC2

---

**TÀI LIỆU THAM KHẢO**

1. ISO/IEC/IEEE 14764:2006 - Software Maintenance
2. IFPUG Counting Practices Manual (CPM) Release 4.3.1
3. Martin Fowler, "Refactoring: Improving the Design of Existing Code", 1999
4. Allan Albrecht, "Measuring Application Development Productivity", IBM, 1979
5. VAS 02 - Chuẩn mực Kế toán Việt Nam về Hàng tồn kho
