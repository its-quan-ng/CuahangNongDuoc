# YÊU CẦU ĐỒ ÁN - CỬA HÀNG NÔNG DƯỢC AN GIANG

**Môn học:** Thiết kế phát triển và Bảo trì Phần mềm  
**Nhóm:** 7 thành viên (5 active)  
**Deadline:** 17/11/2025 (Hard) | 24/11/2025 (Extended - Low possibility)  
**Báo cáo:** 200-300 trang

---

## 📋 ĐỀ BÀI TỔNG QUAN

Công ty nông dược An Giang đã thuê lập trình viên viết chương trình quản lý bán hàng (C# + Access). Phần mềm có nhiều lỗi và chức năng chưa thích hợp. Sinh viên cần:
1. **Ước lượng chi phí** bảo trì
2. **Báo giá** bảo trì phần mềm
3. **Soạn hợp đồng** bảo trì
4. **Thực hiện** bảo trì phần mềm

---

## 🔍 HIỆN TRẠNG HỆ THỐNG

### Vấn đề hiện tại:
1. ❌ Một số lỗi nhỏ, không rõ nguyên nhân
2. ❌ Nhập hàng theo lô và xuất theo lô (chưa linh hoạt)
3. ❌ Hóa đơn bán hàng không tính chi phí vận chuyển và dịch vụ phụ (giảm giá cho khuyến mãi)
4. ❌ Thống kê hàng hóa chưa hoàn chỉnh, không chọn được từ ngày đến ngày
5. ❌ Chưa có chức năng đăng nhập và thống kê cho nhân viên bán hàng

---

## ✅ YÊU CẦU CHÍNH CẦN CHỈNH SỬA (8 yêu cầu)

### 1. Chỉnh sửa lỗi nhỏ và hoàn chỉnh phần mềm
- Fix các bugs hiện có
- Hoàn thiện tài liệu thiết kế và phát triển

### 2. Chỉnh chức năng bán hàng sỉ và lẻ
**2.1. Cấu hình phương pháp nhập xuất kho:**
- ✅ **FIFO** (First In First Out - Nhập trước xuất trước)
- ✅ **LIFO** (Last In First Out - Xuất hàng chỉ định)
- ✅ Hệ thống tự động tính và phân lô theo ngày hết hạn

**2.2. Nhập hàng theo lô:**
- Khi nhập hàng thì hệ thống tự động phân lô theo cấu hình
- Hệ thống tự động tính và có hiển thị lô và ngày hết hạn

**2.3. Giá xuất sản phẩm:**
- ✅ **Bình quân gia quyền** (Weighted Average)
- ✅ **Nhập trước xuất trước** (FIFO Pricing)

### 3. Lập hóa đơn bán hàng với dịch vụ phát sinh
- ✅ Nhập chi phí vận chuyển
- ✅ Nhập dịch vụ phụ

### 4. Lập hóa đơn với chiết khấu và giảm giá
- ✅ Chiết khấu trên từng sản phẩm
- ✅ Giảm giá trên toàn hóa đơn

### 5. Thống kê tồn kho hàng hóa
- ✅ Tồn kho theo ngày đến ngày
- ✅ Chi phí vận chuyển
- ✅ Dịch vụ phụ
- ✅ Giảm giá
- ✅ Khuyến mãi
- ✅ Thống kê báo cáo theo filter

### 6. Thống kê hóa đơn bán giảm giá và khuyến mãi theo nhân viên
- ✅ Theo từng nhân viên đăng nhập
- ✅ Từ ngày đến ngày

### 7. Hoàn thiện chức năng đăng nhập và phân quyền
- ✅ User authentication
- ✅ Role-based authorization
- ✅ Phân quyền theo nhân viên

### 8. Tư vấn các chức năng có thể phát triển trong tương lai
- ✅ Đề xuất roadmap phát triển
- ✅ Các module mở rộng
- ✅ Tích hợp công nghệ mới

---

## 📊 YÊU CẦU NỘI DUNG CHUYÊN MÔN CỦA BÁO CÁO

## I. PHÂN TÍCH VÀ THIẾT KẾ

### 1. Thiết kế giao diện (UI/UX Design)
- Wireframes
- Mockups
- User flows

### 2. Thiết kế dữ liệu (Database Design)
- ERD (Entity Relationship Diagram)
- Database schema
- Normalization
- Indexing strategy

### 3. Thiết kế xử lý (Process Design)
- Activity diagrams
- Sequence diagrams
- State diagrams
- Business logic flows

### 4. Tái cấu trúc (Refactoring)
- Code smells identification
- Refactoring patterns
- Before/After comparison
- Performance improvements

### 5. Mẫu thiết kế áp dụng (Design Patterns)
- Factory Pattern (đã có)
- Repository Pattern
- Strategy Pattern (cho FIFO/LIFO)
- Observer Pattern
- Singleton Pattern
- Các patterns khác phù hợp

---

## II. QUI TRÌNH BẢO TRÌ PHẦN MỀM (CRITICAL!)

### ⚠️ BẮT BUỘC: ISO/IEC/IEEE 14764

**Phải áp dụng đầy đủ:**
1. ✅ **Ứng dụng đầy đủ qui trình** bảo trì phần mềm theo mô hình ISO/IEC/IEEE 14764
2. ✅ **Ứng dụng cải tiến** qui trình bảo trì phần mềm theo mô hình ISO/IEC/IEEE 14764

**Các giai đoạn bảo trì theo ISO 14764:**
1. Problem/Modification Identification
2. Analysis
3. Design
4. Implementation
5. Testing
6. Acceptance
7. Migration (if needed)
8. Retirement (if needed)

**Biểu mẫu cần có:**
- Problem Report Form
- Change Request Form
- Impact Analysis Document
- Test Plan & Test Cases
- Acceptance Checklist
- Configuration Management Log

---

## III. ƯỚC LƯỢNG CHI PHÍ BẢO TRÌ (3 phương pháp)

### 1. Mô hình Điểm Chức Năng (Function Point Analysis)
**Công thức:**
```
FP = UFP × VAF
UFP = Σ(ILF, EIF, EI, EO, EQ) × Complexity Weight
VAF = 0.65 + (0.01 × Σ GSC)
```

**Cần tính:**
- ILF (Internal Logical Files)
- EIF (External Interface Files)
- EI (External Inputs)
- EO (External Outputs)
- EQ (External Queries)
- GSC (General System Characteristics) - 14 factors

**Sau đó:**
- Effort = FP × Productivity Rate (hours/FP)
- Cost = Effort × Labor Rate (VND/hour)

### 2. Mô hình COCOMO II
**COCOMO II Post-Architecture Model:**
```
Effort = A × Size^E × Π(EM_i)
E = B + 0.01 × Σ(SF_j)
```

**Tham số:**
- A = 2.94 (nominal)
- Size = KSLOC (thousand source lines of code)
- SF = Scale Factors (5 factors)
- EM = Effort Multipliers (17 factors)

**Scale Factors:**
1. PREC (Precedentedness)
2. FLEX (Development Flexibility)
3. RESL (Architecture/Risk Resolution)
4. TEAM (Team Cohesion)
5. PMAT (Process Maturity)

### 3. Mô hình cải tiến (Thông tư 2589)
**Thông tư 2589/2010/TT-BTTTT** - Định mức kinh tế - kỹ thuật phần mềm

**Cần tính:**
- Điểm chức năng theo chuẩn Việt Nam
- Hệ số điều chỉnh
- Đơn giá nhân công
- Chi phí tổng thể

---

## IV. KỸ THUẬT BẢO TRÌ PHẦN MỀM

### 1. Kỹ thuật hiểu biết chương trình và Đảo ngược (Reverse Engineering)
- Static analysis
- Dynamic analysis
- Code reading techniques
- Documentation extraction

### 2. Kỹ thuật hiểu biết chương trình và Tái kiến tạo (Reengineering)
- Restructuring
- Forward engineering
- Migration strategies

### 3. Kỹ thuật cải tiến hoặc kết hợp
- Hybrid approaches
- Best practices

---

## V. CÔNG CỤ BẢO TRÌ PHẦN MỀM

### BẮT BUỘC sử dụng:

1. ✅ **Alvota UML 2013** - Công cụ đảo ngược và hiểu chương trình
   - Tạo UML diagrams từ code
   - Class diagrams
   - Sequence diagrams

2. ✅ **Doxygen** - Công cụ hiểu chương trình
   - Generate documentation từ code
   - Call graphs
   - Dependency graphs
   - HTML/PDF output

3. ✅ **GitHub** - Quản lý tài nguyên
   - Version control
   - Issue tracking
   - Pull requests
   - Project management

4. ✅ **Datatect** - Công cụ kiểm thử (nếu có)

5. ✅ **QuickTest Pro** - Công cụ kiểm thử (nếu có)

6. ✅ **Các công cụ hiện đại khác:**
   - Visual Studio Code Analysis
   - ReSharper (Code Quality)
   - SonarQube (Code Quality & Security)
   - NUnit/xUnit (Unit Testing)
   - Selenium (UI Testing)

---

## 📝 CẤU TRÚC BÁO CÁO (200-300 trang)

### Phần 1: Mở đầu (20-30 trang)
- Lời mở đầu
- Giới thiệu đề tài
- Mục tiêu và phạm vi
- Phương pháp nghiên cứu
- Cấu trúc báo cáo

### Phần 2: Cơ sở lý thuyết (40-50 trang)
- Bảo trì phần mềm
- ISO/IEC/IEEE 14764
- Function Points
- COCOMO II
- Thông tư 2589
- Design Patterns
- Reverse Engineering

### Phần 3: Phân tích hệ thống hiện tại (30-40 trang)
- Mô tả hệ thống hiện tại
- Kiến trúc hệ thống
- Database schema
- Code analysis
- Vấn đề và hạn chế
- Yêu cầu bảo trì

### Phần 4: Ước lượng chi phí (30-40 trang)
- Function Point Analysis (chi tiết)
- COCOMO II Estimation (chi tiết)
- Thông tư 2589 (chi tiết)
- So sánh các phương pháp
- Báo giá bảo trì

### Phần 5: Thiết kế giải pháp (40-50 trang)
- Thiết kế giao diện
- Thiết kế database (ERD, schema)
- Thiết kế xử lý (diagrams)
- Design patterns áp dụng
- Tái cấu trúc code

### Phần 6: Triển khai (30-40 trang)
- Qui trình bảo trì theo ISO 14764
- Implementation details
- Code samples
- Testing strategy
- Deployment plan

### Phần 7: Công cụ và kỹ thuật (20-30 trang)
- Doxygen documentation
- UML diagrams (Alvota)
- GitHub workflow
- Testing tools
- Quality assurance

### Phần 8: Tư vấn phát triển tương lai (10-15 trang)
- Roadmap
- New features proposal
- Technology stack recommendations
- Maintenance strategy

### Phần 9: Kết luận (10-15 trang)
- Tổng kết công việc
- Kết quả đạt được
- Bài học kinh nghiệm
- Hướng phát triển

### Phụ lục (20-30 trang)
- Biểu mẫu ISO 14764
- Source code quan trọng
- Database scripts
- Test cases
- User manual
- Hợp đồng bảo trì mẫu

---

## 🎯 PRIORITY & TIMELINE (16 ngày)

### Week 1 (01-07/11) - DEVELOPMENT
**Days 1-2: Critical Fixes**
- [ ] Fix SQL Injection trong ThamSo.cs
- [ ] Refactor Connection String
- [ ] Fix các bugs nhỏ hiện có

**Days 3-5: Core Features**
- [ ] Đăng nhập và phân quyền
- [ ] Cấu hình FIFO/LIFO
- [ ] Chi phí vận chuyển, dịch vụ phụ
- [ ] Chiết khấu, giảm giá

**Days 6-7: Advanced Features**
- [ ] Thống kê nâng cao
- [ ] Báo cáo theo nhân viên
- [ ] Testing

### Week 2 (08-14/11) - DOCUMENTATION & ESTIMATION
**Days 8-9: Estimation**
- [ ] Function Points calculation
- [ ] COCOMO II estimation
- [ ] Thông tư 2589 calculation
- [ ] Báo giá

**Days 10-12: Technical Documentation**
- [ ] Doxygen documentation
- [ ] UML diagrams (Alvota)
- [ ] ERD & Database design
- [ ] Code analysis

**Days 13-14: Report Writing**
- [ ] Phần 1-3: Lý thuyết & Phân tích
- [ ] Phần 4: Ước lượng
- [ ] Phần 5-6: Thiết kế & Triển khai

### Week 3 (15-17/11) - FINALIZATION
**Days 15-16: Complete Report**
- [ ] Phần 7-9: Công cụ, Tư vấn, Kết luận
- [ ] Phụ lục
- [ ] Review & Polish

**Day 17: SUBMISSION**
- [ ] Final testing
- [ ] Final review
- [ ] Submit

---

## ⚠️ CRITICAL SUCCESS FACTORS

1. ✅ Áp dụng ĐÚNG và ĐẦY ĐỦ ISO/IEC/IEEE 14764
2. ✅ Sử dụng 3 phương pháp ước lượng với số liệu chi tiết
3. ✅ Dùng Doxygen và Alvota UML (có screenshots)
4. ✅ Báo cáo 200-300 trang với đầy đủ biểu mẫu
5. ✅ Code phải chạy được và fix hết bugs
6. ✅ Design patterns áp dụng đúng
7. ✅ Testing kỹ lưỡng

---

## 📚 TÀI LIỆU THAM KHẢO CẦN HỌC

1. **ISO/IEC/IEEE 14764:2006** - Software Engineering - Software Life Cycle Processes - Maintenance
2. **Function Point Analysis** - IFPUG Counting Practices Manual
3. **COCOMO II** - Software Cost Estimation with COCOMO II (Barry Boehm)
4. **Thông tư 2589/2010/TT-BTTTT** - Định mức kinh tế kỹ thuật phần mềm
5. **Design Patterns** - Gang of Four
6. **Reverse Engineering** - IEEE Standards

---

*Document này sẽ được update liên tục trong quá trình làm đồ án.*

