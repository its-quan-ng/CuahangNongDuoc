# 📊 TIẾN ĐỘ DỰ ÁN - ĐỒ ÁN BẢO TRÌ PHẦN MỀM

**Cập nhật lần cuối:** 08/11/2025
**Deadline:** 24/11/2025 (17 ngày)
**Team:** 7 thành viên
**Loại đồ án:** Bảo trì phần mềm (Software Maintenance)

---

## 🎯 MỤC TIÊU TỔNG

### Yêu cầu chức năng (8 yêu cầu chính):
1. ✅ Fix lỗi nhỏ và hoàn chỉnh phần mềm
2. ⏳ Xuất kho theo lô (FIFO/Manual) + Tính giá 2 phương pháp
3. ⏳ Thêm chi phí vận chuyển & dịch vụ phụ vào hóa đơn
4. ⏳ Thêm giảm giá/chiết khấu trên hóa đơn
5. ⏳ Thống kê theo khoảng ngày
6. ⏳ Thống kê theo nhân viên
7. ⏳ Đăng nhập & phân quyền
8. ⏳ Tư vấn phát triển tương lai

### Yêu cầu tài liệu (200-300 trang):
- Phân tích & Thiết kế (Interface, Database, Process)
- Qui trình bảo trì ISO/IEC/IEEE 14764
- Ước lượng chi phí (Function Point, COCOMO II, Thông tư 2589)
- Kỹ thuật bảo trì (Reverse Engineering, Reengineering, Improvement)
- Công cụ bảo trì (AlVota UML, Doxygen, GitHub, Datatect, QuickTest Pro)

---

## ✅ ĐÃ HOÀN THÀNH (08/11/2025)

### 1. Phân tích yêu cầu đồ án ✅
- Đọc và phân tích file PDF yêu cầu
- Đối chiếu với codebase hiện tại
- Xác định gap analysis

### 2. Audit bảo mật (SQL Injection) ✅
- **Kết quả:** Toàn bộ codebase ĐÃ AN TOÀN!
- Kiểm tra `ThamSo.cs`: ✅ All methods dùng parameterized queries
- Scan toàn bộ Factory classes: ✅ All safe
- **Kết luận:** Không cần fix gì về SQL injection

### 3. Lập kế hoạch chi tiết ✅
- Timeline 17 ngày được chia thành 3 giai đoạn
- Phân công công việc cho 7 thành viên
- Xác định milestones và deliverables

---

## 🔄 ĐANG LÀM (Tuần 1: 08-14/11)

### Giai đoạn 1: Hạ tầng & Bảo mật
- [ ] **Ngày 1 (08/11)**: Thiết kế database cho tính năng mới
- [ ] **Ngày 2 (09/11)**: Implement hệ thống đăng nhập
- [ ] **Ngày 3 (10/11)**: Implement phân quyền

---

## 📋 KẾ HOẠCH CHI TIẾT 17 NGÀY

### **GIAI ĐOẠN 1: HẠ TẦNG & BẢO MẬT** (08-10/11 - 3 ngày)

#### Ngày 1 (08/11): Thiết kế Database ⏳
**Mục tiêu:** Chuẩn bị database cho tất cả tính năng mới

**Tasks:**
- [ ] Tạo bảng `NHAN_VIEN` (ID, HO_TEN, USERNAME, PASSWORD_HASH, ID_QUYEN, TRANG_THAI)
- [ ] Tạo bảng `QUYEN` (ID, TEN_QUYEN, MO_TA)
- [ ] Tạo bảng `CAU_HINH` (ID, TEN_CAU_HINH, GIA_TRI, MO_TA)
  - Lưu cấu hình: PHUONG_PHAP_XUAT_KHO (FIFO/MANUAL)
  - Lưu cấu hình: PHUONG_PHAP_TINH_GIA (BINH_QUAN/FIFO)
- [ ] Thêm columns vào `PHIEU_BAN`:
  - `CHI_PHI_VAN_CHUYEN` (int, default 0)
  - `CHI_PHI_DICH_VU` (int, default 0)
  - `GIAM_GIA` (int, default 0)
  - `TY_LE_GIAM_GIA` (decimal(5,2), default 0)
  - `ID_NHAN_VIEN` (int, nullable, FK to NHAN_VIEN)
- [ ] Tạo script SQL migration: `data/migration_20251108.sql`
- [ ] Test script trên database hiện có

**Deliverable:** Script SQL hoàn chỉnh, database updated

---

#### Ngày 2 (09/11): Hệ thống đăng nhập 🔐
**Mục tiêu:** Implement login system hoàn chỉnh

**Sáng:**
- [ ] Tạo `BusinessObject/NhanVien.cs` (POCO)
- [ ] Tạo `DataLayer/NhanVienFactory.cs` (CRUD methods)
- [ ] Tạo `Controller/NhanVienController.cs`
- [ ] Implement password hashing (SHA256 hoặc bcrypt)
- [ ] Method: `DangNhap(string username, string password)` → return NhanVien object

**Chiều:**
- [ ] Tạo `frmDangNhap.cs` (Login form)
  - TextBox: Username, Password (PasswordChar = '*')
  - Button: Đăng nhập, Thoát
  - Label: Thông báo lỗi
- [ ] Tạo class `Session.cs` (static) để lưu user hiện tại
- [ ] Update `Program.cs`:
  ```csharp
  Application.Run(new frmDangNhap());
  // Nếu đăng nhập thành công → Show frmMain
  ```
- [ ] Test: Đăng nhập thành công/thất bại

**Deliverable:** Login system hoàn chỉnh, user phải đăng nhập mới vào được

---

#### Ngày 3 (10/11): Phân quyền 👥
**Mục tiêu:** Ẩn/hiện chức năng theo quyền

**Sáng:**
- [ ] Tạo enum `Permission`:
  ```csharp
  public enum Permission
  {
      ADMIN = 1,      // Toàn quyền
      QUAN_LY = 2,    // Xem báo cáo, quản lý hàng hóa
      NV_BAN_HANG = 3 // Chỉ bán hàng
  }
  ```
- [ ] Implement method `Session.HasPermission(Permission requiredPermission)`
- [ ] Ẩn/hiện menu items trong `frmMain` theo quyền
- [ ] Disable buttons không có quyền

**Chiều:**
- [ ] Test với 3 loại user khác nhau
- [ ] Chụp screenshots cho báo cáo
- [ ] Viết tài liệu: "Chức năng đăng nhập & phân quyền" (10-15 trang)

**Deliverable:** Hệ thống phân quyền hoàn chỉnh

---

### **GIAI ĐOẠN 2: CHỨC NĂNG CORE** (11-16/11 - 6 ngày)

#### Ngày 4-5 (11-12/11): Xuất kho theo lô 📦

**Ngày 4 Sáng:**
- [ ] Tạo `frmCauHinh.cs` (Cấu hình hệ thống)
- [ ] Radio buttons: FIFO / Manual (Chọn thủ công)
- [ ] Lưu vào bảng `CAU_HINH`

**Ngày 4 Chiều:**
- [ ] Update `MaSanPhamController.cs`
- [ ] Method: `LayLoXuatTheoFIFO(int idSanPham, int soLuong)`
  - Query: `SELECT * FROM MA_SAN_PHAM WHERE ID_SAN_PHAM = @id AND SO_LUONG > 0 ORDER BY NGAY_HET_HAN ASC`
  - Logic: Lấy lô hết hạn sớm nhất trước
  - Nếu không đủ → Lấy thêm lô tiếp theo

**Ngày 5 Sáng:**
- [ ] Update `frmBanLe.cs` và `frmBanSi.cs`
- [ ] Khi chọn mode Manual:
  - Show danh sách lô available (serial, ngày hết hạn, số lượng tồn)
  - Cho phép người dùng chọn lô cụ thể

**Ngày 5 Chiều:**
- [ ] Update `CHI_TIET_PHIEU_BAN` để lưu ID_MA_SAN_PHAM (serial/lô)
- [ ] Update report `.rdlc` để hiển thị lô trong phiếu xuất
- [ ] Test cả 2 mode: FIFO và Manual

**Deliverable:** Xuất kho theo lô hoàn chỉnh

---

#### Ngày 6 (13/11): Tính giá xuất 💰

**Sáng: Bình quân gia quyền**
- [ ] Method: `TinhGiaXuatBinhQuan(int idSanPham)`
  ```csharp
  // Formula: SUM(gia_nhap * so_luong) / SUM(so_luong)
  SELECT SUM(DON_GIA_NHAP * SO_LUONG) / SUM(SO_LUONG)
  FROM MA_SAN_PHAM
  WHERE ID_SAN_PHAM = @id AND SO_LUONG > 0
  ```
- [ ] Lưu vào `CAU_HINH` hoặc `Settings`

**Chiều: FIFO Costing**
- [ ] Method: `TinhGiaXuatFIFO(int idSanPham, int soLuong)`
- [ ] Lấy giá từ lô nhập đầu tiên (ORDER BY NGAY_NHAP ASC)
- [ ] Nếu xuất nhiều lô → Tính giá trung bình theo số lượng mỗi lô
- [ ] Test và so sánh kết quả 2 phương pháp

**Deliverable:** 2 phương pháp tính giá hoàn chỉnh

---

#### Ngày 7 (14/11): Giảm giá & Phí phụ 💸

**Sáng:**
- [ ] Update `PhieuBanFactory.cs` để lưu các field mới
- [ ] Update `PhieuBanController.cs`:
  ```csharp
  TONG_TIEN = THANH_TIEN - GIAM_GIA + CHI_PHI_VAN_CHUYEN + CHI_PHI_DICH_VU
  ```
- [ ] Method: `TinhTongTien(PhieuBan phieu)` → tính tổng tiền mới

**Chiều:**
- [ ] Update `frmBanLe.cs` và `frmBanSi.cs`:
  - TextBox: `txtChiPhiVanChuyen`, `txtDichVuPhu`, `txtGiamGia`, `txtTyLeGiamGia`
  - Auto-calculate khi thay đổi
  - Label: `lblTongTien` (update real-time)
- [ ] Update report để hiển thị đầy đủ chi tiết
- [ ] Test: Nhập giảm giá → Tổng tiền tự động giảm

**Deliverable:** Hóa đơn có đầy đủ giảm giá & phí phụ

---

#### Ngày 8-9 (15-16/11): Thống kê nâng cao 📊

**Ngày 8: Thống kê theo khoảng ngày**
- [ ] Update `frmDoanhThu.cs`, `frmSoLuongTon.cs`, `frmDunoKhachhang.cs`
- [ ] Thêm DateTimePicker: `dtpTuNgay`, `dtpDenNgay`
- [ ] Update queries trong Factory:
  ```sql
  WHERE NGAY_BAN >= @tungay AND NGAY_BAN <= @denngay
  ```
- [ ] Button: "Xem báo cáo"
- [ ] Test với nhiều khoảng thời gian

**Ngày 9: Thống kê theo nhân viên**
- [ ] Tạo `frmBaoCaoNhanVien.cs`
- [ ] Báo cáo: Hóa đơn giảm giá theo nhân viên
  - Columns: Tên NV, Số phiếu, Tổng giảm giá, Từ ngày, Đến ngày
- [ ] ComboBox: Chọn nhân viên (hoặc "Tất cả")
- [ ] DateTimePicker: Từ ngày - Đến ngày
- [ ] DataGridView: Hiển thị kết quả
- [ ] (Optional) Button: Export Excel

**Deliverable:** Thống kê theo ngày & nhân viên hoàn chỉnh

---

### **GIAI ĐOẠN 3: HOÀN THIỆN & TÀI LIỆU** (17-24/11 - 8 ngày)

#### Ngày 10-11 (17-18/11): Testing & Bug fixes 🐛
- [ ] Test toàn bộ workflow end-to-end
- [ ] Test với nhiều user, nhiều quyền
- [ ] Test edge cases (hết hàng, hết hạn, số âm, giá 0...)
- [ ] Fix tất cả bugs phát hiện được
- [ ] Polish UI (alignment, colors, fonts, messages)

**Deliverable:** Hệ thống stable, no critical bugs

---

#### Ngày 12-13 (19-20/11): Phần I - Phân tích & Thiết kế (50-60 trang)
- [ ] 1. Thiết kế giao diện
  - Screenshots tất cả forms (Before/After nếu có)
  - Mô tả chức năng từng form
- [ ] 2. Thiết kế dữ liệu
  - ER Diagram (dùng AlVota UML 2013 hoặc Visual Studio)
  - Mô tả từng bảng, relationships
  - Data dictionary
- [ ] 3. Thiết kế xử lý
  - Sequence diagrams (đăng nhập, bán hàng, xuất kho FIFO...)
  - Activity diagrams (workflow)
  - Class diagrams
- [ ] 4. Tái cấu trúc (Refactoring)
  - Liệt kê những gì đã refactor
  - Before/After code examples
  - Explain benefits
- [ ] 5. Mẫu thiết kế (Design Patterns)
  - Factory Pattern (DataLayer/*Factory)
  - MVC Pattern (Controller-View separation)
  - Singleton Pattern (DataService connection)
  - Repository Pattern (Factory classes)

**Deliverable:** 50-60 trang với diagrams đầy đủ

---

#### Ngày 14-15 (21-22/11): Phần II-IV (80-100 trang)

**Ngày 14: Phần II - Qui trình bảo trì (30-40 trang)**
- [ ] Áp dụng ISO/IEC/IEEE 14764:
  - Problem identification (Xác định vấn đề cần bảo trì)
  - Analysis (Phân tích yêu cầu)
  - Design (Thiết kế giải pháp)
  - Implementation (Triển khai)
  - System test (Kiểm thử hệ thống)
  - Acceptance test (Kiểm thử chấp nhận)
  - Delivery (Bàn giao)
  - Maintenance (Bảo trì)
- [ ] Mô tả chi tiết từng bước đã làm
- [ ] Checklist và deliverables

**Ngày 15 Sáng: Phần III - Ước lượng chi phí (30-40 trang)**
- [ ] A. Function Point Analysis:
  - Đếm ILF, EIF, EI, EO, EQ
  - Calculate UFP (Unadjusted Function Points)
  - Apply complexity factors
  - Calculate final FP
- [ ] B. COCOMO II:
  - Ước lượng Lines of Code (LOC)
  - Scale Factors
  - Effort Multipliers
  - Calculate effort (person-months)
- [ ] C. Cải tiến theo Thông tư 2589 (nếu có)
- [ ] So sánh 3 phương pháp

**Ngày 15 Chiều: Phần IV - Kỹ thuật bảo trì (20-30 trang)**
- [ ] 1. Đảo ngược (Reverse Engineering):
  - Dùng Doxygen generate documentation từ code
  - Extract class diagrams từ code
- [ ] 2. Tái kiến tạo (Reengineering):
  - Mô tả quá trình migrate Access → SQL Server (nếu có)
  - Refactoring code
- [ ] 3. Cải tiến (Improvement):
  - Liệt kê improvements: FIFO, login, discount...
  - Benefit analysis

**Deliverable:** 80-100 trang hoàn chỉnh

---

#### Ngày 16-17 (23-24/11): Phần V & Finalize (40-60 trang)

**Ngày 16: Phần V - Công cụ bảo trì (20-30 trang)**
- [ ] 1. Doxygen:
  - Generate HTML documentation
  - Screenshots
  - Hướng dẫn sử dụng
- [ ] 2. AlVota UML 2013 / Visual Studio:
  - Generate class diagrams
  - ER diagrams
  - Screenshots
- [ ] 3. GitHub:
  - Commit history
  - Branch strategy
  - Pull requests (nếu có)
- [ ] 4. Testing tools (optional):
  - Datatect
  - QuickTest Pro

**Ngày 17: Hoàn thiện tài liệu (20-30 trang + review)**
- [ ] Mở đầu:
  - Giới thiệu đề tài
  - Mục tiêu
  - Phạm vi
  - Phương pháp thực hiện
- [ ] Kết luận:
  - Tóm tắt những gì đã làm
  - Kết quả đạt được
  - Hạn chế
  - Hướng phát triển
- [ ] Tài liệu tham khảo
- [ ] Phụ lục:
  - Database schema script
  - Key source code snippets
  - User manual
- [ ] **Kiểm tra format:**
  - Mục lục, danh mục hình, danh mục bảng
  - Page numbers
  - Font, margin, spacing
  - 200-300 trang ✅

**Deliverable:** Báo cáo hoàn chỉnh, ready to submit

---

## 👥 PHÂN CÔNG CÔNG VIỆC CHO 7 THÀNH VIÊN

### **Nhóm 1: Backend Development (2 người)**
**Trách nhiệm:** Database, Business Logic, Data Access Layer

**Người 1: Database Architect**
- Thiết kế & implement database changes (NHAN_VIEN, QUYEN, CAU_HINH, columns mới)
- Viết migration scripts
- Tạo stored procedures (nếu cần)
- Optimize queries

**Người 2: Business Logic Developer**
- Implement Controllers (NhanVienController, updated PhieuBanController...)
- Business logic: FIFO, tính giá, giảm giá
- Password hashing
- Validation rules

**Deliverables:**
- Migration scripts
- Factory classes updated
- Controller classes
- Unit test cases (optional)

---

### **Nhóm 2: Frontend Development (2 người)**
**Trách nhiệm:** User Interface, Forms, Reports

**Người 3: Forms Developer**
- Implement forms: frmDangNhap, frmCauHinh, frmBaoCaoNhanVien
- Update existing forms: frmBanLe, frmBanSi (thêm giảm giá, phí phụ)
- Data binding
- Input validation

**Người 4: Reports Developer**
- Update .rdlc reports (hiển thị lô, giảm giá, phí phụ)
- Design report layouts
- Test reports với nhiều data
- Export functionality

**Deliverables:**
- New forms
- Updated existing forms
- Report files (.rdlc)
- Screenshots for documentation

---

### **Nhóm 3: Testing & Quality Assurance (1-2 người)**
**Trách nhiệm:** Testing, Bug tracking, Quality control

**Người 5: Tester**
- Tạo test cases
- Test toàn bộ chức năng (manual testing)
- Track bugs (dùng Excel hoặc GitHub Issues)
- Verify bug fixes
- Test edge cases
- UAT (User Acceptance Testing)

**Người 6 (Optional): QA**
- Code review
- Security testing
- Performance testing
- Integration testing

**Deliverables:**
- Test cases document
- Bug reports
- Test results
- Sign-off checklist

---

### **Nhóm 4: Documentation (1-2 người)**
**Trách nhiệm:** Viết báo cáo 200-300 trang

**Người 7: Technical Writer**
- Viết Phần I-III (Thiết kế, Qui trình, Ước lượng)
- Tạo diagrams (ER, Sequence, Activity...)
- Chụp screenshots
- Format document theo chuẩn

**Người 8 (Optional nếu có 8 người)**
- Viết Phần IV-V (Kỹ thuật, Công cụ)
- Mở đầu, Kết luận, Tài liệu tham khảo
- Review & edit
- Generate table of contents, figures, tables

**Deliverables:**
- Báo cáo 200-300 trang hoàn chỉnh
- Diagrams (PNG/SVG)
- PowerPoint slides for presentation

---

### **Lead Developer (1 người - quan trọng nhất!)**
**Trách nhiệm:** Hiểu toàn bộ hệ thống, integration, coordination

**Nhiệm vụ:**
- Hiểu 100% codebase và requirements
- Review code của tất cả thành viên
- Integration testing (ghép các phần lại)
- Giải quyết conflicts
- Điều phối công việc
- Backup cho bất kỳ ai không làm được
- Final review trước khi submit

**Critical Skills:**
- Hiểu rõ 3-tier architecture
- Biết ADO.NET, SQL Server
- Có thể code mọi layer
- Communication & project management

---

## 📅 TIMELINE VÀ MILESTONES

### Week 1 (08-14/11): Core Development
- **Milestone 1 (10/11):** Đăng nhập & phân quyền hoàn chỉnh ✅
- **Milestone 2 (12/11):** Xuất kho FIFO hoàn chỉnh ✅
- **Milestone 3 (14/11):** Giảm giá & phí phụ hoàn chỉnh ✅

### Week 2 (15-21/11): Testing & Documentation
- **Milestone 4 (16/11):** Thống kê hoàn chỉnh ✅
- **Milestone 5 (18/11):** Testing xong, no critical bugs ✅
- **Milestone 6 (20/11):** Phần I-III document xong (130-140 trang) ✅

### Week 3 (22-24/11): Finalization
- **Milestone 7 (22/11):** Phần IV-V xong (180-200 trang) ✅
- **Milestone 8 (23/11):** Hoàn thiện document (200-300 trang) ✅
- **Milestone 9 (24/11):** SUBMIT ĐỒ ÁN ✅

---

## 🚨 RỦI RO & GIẢI PHÁP

### Rủi ro cao:
1. **Không đủ thời gian**
   - Giải pháp: Ưu tiên chức năng quan trọng, làm song song, có thể skip testing tools

2. **Thành viên bỏ làm/không làm đúng hạn**
   - Giải pháp: Lead phải backup, có thể redistribute tasks

3. **Bugs phát sinh muộn**
   - Giải pháp: Test sớm, test thường xuyên, fix ngay khi phát hiện

### Rủi ro trung bình:
4. **Tài liệu không đủ 200 trang**
   - Giải pháp: Thêm screenshots, diagrams, explain chi tiết, appendix

5. **Conflict trong code**
   - Giải pháp: Dùng Git properly, merge thường xuyên, code review

---

## 📝 GHI CHÚ QUAN TRỌNG

### Quyết định kỹ thuật:
- ✅ Database: SQL Server (đã migrate từ Access)
- ✅ Security: Codebase đã an toàn về SQL injection
- ✅ Password: Sẽ dùng SHA256 hoặc bcrypt
- ✅ Xuất kho default: FIFO (có thể chuyển sang Manual)
- ✅ Tính giá default: Bình quân gia quyền (có thể chuyển sang FIFO)

### Lưu ý khi code:
1. Luôn dùng parameterized queries (đã làm rất tốt!)
2. Naming conventions: `m_` prefix cho private fields
3. Hungarian notation cho controls: `cmb`, `dg`, `txt`, `btn`
4. Database: UPPER_CASE_WITH_UNDERSCORE
5. Comments bằng tiếng Việt OK
6. Commit message: Tiếng Việt OK

### Các file quan trọng:
- `CLAUDE.md`: Thông tin tổng quan về project, architecture
- `PROGRESS.md`: File này - Tiến độ và kế hoạch
- `README.md`: Hướng dẫn setup và sử dụng
- `.cursorrules`: Quy tắc code chi tiết

### Contacts & Resources:
- Lead Developer: [Tên của bạn]
- GitHub Repo: [URL]
- Database Server: `.\SQLEXPRESS`
- Database Name: `QLCHNongDuoc`

---

## 🎯 CHECKLIST TRƯỚC KHI NỘP (24/11)

### Code:
- [ ] Build thành công, no errors
- [ ] Tất cả 8 yêu cầu chức năng đã implement
- [ ] Test với nhiều scenarios, no critical bugs
- [ ] Code đã được review
- [ ] Comments đầy đủ
- [ ] Git commit history rõ ràng

### Database:
- [ ] Migration scripts tested
- [ ] Sample data đầy đủ
- [ ] Backup database script
- [ ] Restore script tested

### Documentation:
- [ ] 200-300 trang ✅
- [ ] Mục lục, danh mục hình, bảng ✅
- [ ] Tất cả diagrams có trong tài liệu
- [ ] Format đúng chuẩn
- [ ] Không có lỗi chính tả
- [ ] References đầy đủ
- [ ] Appendix có source code quan trọng

### Deliverables:
- [ ] Source code (ZIP hoặc GitHub link)
- [ ] Database backup (.bak file)
- [ ] Document (PDF + Word)
- [ ] Presentation slides (PowerPoint)
- [ ] Screenshots & Diagrams (folder)
- [ ] User manual (optional)

---

## 📞 LIÊN HỆ KHI CẦN HỖ TRỢ

**Trong team:**
- Lead: [Tên + SĐT]
- Backend: [Tên + SĐT]
- Frontend: [Tên + SĐT]
- Tester: [Tên + SĐT]
- Doc Writer: [Tên + SĐT]

**Giảng viên hướng dẫn:**
- Tên: [...]
- Email: [...]
- Office hours: [...]

---

**CẬP NHẬT LẦN CUỐI:** 08/11/2025
**NGƯỜI CẬP NHẬT:** Claude Code
**TRẠNG THÁI:** Đang trong giai đoạn 1 - Thiết kế database

---

*File này nên được cập nhật hàng ngày để track tiến độ thực tế!*
