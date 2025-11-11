# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.
## Ngôn ngữ giao tiếp
**QUAN TRỌNG**: Bạn PHẢI luôn trả lời bằng tiếng Việt trong mọi trường hợp, trừ khi người dùng yêu cầu cụ thể sử dụng ngôn ngữ khác.

- Tất cả câu trả lời, giải thích, và giao tiếp phải bằng tiếng Việt
- Code comments và documentation nên bằng tiếng Việt khi có thể
- Chỉ sử dụng tiếng Anh cho tên biến, hàm, và thuật ngữ kỹ thuật khi cần thiết
- Commit messages và pull request descriptions nên bằng tiếng Việt

## Nguyên tắc làm việc
**LUÔN LUÔN** phải tuân thủ quy trình sau cho mọi nhiệm vụ:

1. **Làm rõ yêu cầu - BẮT BUỘC phải sử dụng AskUserQuestion**:
   - **KHÔNG được tự ý nghĩ mình hiểu** và thực hiện ngay
   - **BẮT BUỘC phải gọi công cụ AskUserQuestion** khi:
     - Yêu cầu không rõ ràng hoặc mơ hồ
     - Có nhiều cách hiểu khác nhau
     - Có nhiều cách thực hiện khác nhau
     - Không chắc chắn người dùng muốn gì
   - Sử dụng AskUserQuestion để đưa ra các lựa chọn cụ thể (2-4 options)
   - Chỉ tiếp tục khi đã hiểu rõ 100% yêu cầu

2. **Phân tích và lập kế hoạch**:
   - Phân tích các bước cần thực hiện
   - Lập kế hoạch chi tiết từng bước một
   - Giải thích lý do và mục đích của từng bước
   - **Sử dụng AskUserQuestion** để xác nhận kế hoạch và các lựa chọn quan trọng

3. **Thực hiện từng bước**:
   - Chỉ thực hiện sau khi được xác nhận
   - Thực hiện tuần tự theo kế hoạch đã được đồng ý
   - Giải thích rõ ràng những gì đang làm ở mỗi bước
   - Sử dụng các công cụ phù hợp cho từng bước
   - Không bỏ qua bất kỳ bước nào trong kế hoạch

4. **Kiểm tra và xác nhận**:
   - Kiểm tra kết quả sau mỗi bước quan trọng
   - Xác nhận kết quả cuối cùng trước khi báo cáo hoàn thành
   - Đảm bảo tất cả các yêu cầu đã được đáp ứng

## An toàn dữ liệu
**BẮT BUỘC** phải xin phép người dùng trước khi thực hiện các thao tác nguy hiểm:

**Các thao tác nguy hiểm bao gồm**:
- Xóa file, thư mục (rm, del, rmdir)
- Ghi đè hoặc thay thế file quan trọng
- Thay đổi cấu hình hệ thống
- Thực hiện lệnh git reset, git rebase, git push --force
- Thay đổi quyền truy cập file/thư mục
- Chạy script có khả năng thay đổi nhiều file cùng lúc
- Di chuyển hoặc đổi tên file/thư mục quan trọng

**Quy trình bắt buộc**:
1. Phát hiện thao tác nguy hiểm trong kế hoạch
2. Giải thích rõ ràng thao tác sẽ làm gì và ảnh hưởng gì
3. Hỏi người dùng xác nhận hoặc đề xuất cách thức thực hiện
4. Chỉ thực hiện sau khi có sự đồng ý rõ ràng từ người dùng

## Nguyên tắc thiết kế giao diện
**QUAN TRỌNG**: Thiết kế phải theo phong cách con người thực sự thiết kế, KHÔNG được sử dụng các đặc trưng điển hình của AI.

**TRÁNH tuyệt đối**:
- Gradient màu sặc sỡ, mầu mè (purple-pink-blue gradient, rainbow gradient)
- Hiệu ứng blur/glassmorphism quá mức
- Các màu neon chói, không hài hòa
- Animation phức tạp, không cần thiết
- Layout quá đối xứng, thiếu tính tự nhiên

**NÊN sử dụng**:
- Màu sắc tinh tế, hài hòa, có hệ thống màu rõ ràng
- Typography rõ ràng, dễ đọc với hierarchy hợp lý
- Spacing và layout tự nhiên, cân đối nhưng không cứng nhắc
- Màu sắc trung tính làm nền (trắng, xám nhạt, xám đậm)
- Màu nhấn (accent color) được chọn cẩn thận, sử dụng tiết chế
- Thiết kế tối giản, tập trung vào nội dung và chức năng
- Tham khảo các trang web/app thực tế được thiết kế bởi con người

## Phân tích codebase
**BẮT BUỘC** phải sử dụng MCP code-index để phân tích trước khi thay đổi code:

**Quy trình bắt buộc**:
1. **Luôn sử dụng các công cụ MCP code-index** để tìm hiểu codebase:
   - `mcp__code-index__search_code_advanced`: Tìm kiếm code, function, class
   - `mcp__code-index__find_files`: Tìm file theo pattern
   - `mcp__code-index__get_file_summary`: Xem tổng quan file
   - `mcp__code-index__build_deep_index`: Build index nếu cần

2. **KHÔNG được đoán hoặc giả định** khi chưa nắm rõ:
   - Không đoán tên file, function, class
   - Không giả định cấu trúc project
   - Không đoán implementation chi tiết
   - Luôn tìm kiếm và đọc code thực tế trước

3. **Phân tích kỹ lưỡng trước khi thay đổi**:
   - Tìm hiểu các file liên quan
   - Đọc code hiện tại để hiểu logic
   - Xác định dependencies và impacts
   - Chỉ thực hiện thay đổi khi đã hiểu rõ toàn bộ context

## Nguyên tắc viết code
**BẮT BUỘC** phải tuân thủ các nguyên tắc clean code:

**KHÔNG được phép**:
- Để lại code thừa, code không sử dụng (commented code, unused imports, unused functions)
- Viết nguyên một khối code dài mà không tách nhỏ
- Duplicate code, copy-paste code
- Hardcode giá trị, magic numbers
- Tên biến, hàm không rõ nghĩa (a, b, temp, data, handleClick1)

**BẮT BUỘC phải làm**:
1. **Refactor code và structure**:
   - Tách functions/components nhỏ, mỗi function làm một việc
   - Tổ chức structure hợp lý (folders, files)
   - Tái sử dụng code thông qua functions/components/utilities
   - Áp dụng design patterns phù hợp

2. **Code gọn gàng, sạch sẽ**:
   - Xóa tất cả code không sử dụng
   - Xóa imports không cần thiết
   - Xóa console.log, debug code
   - Formatting nhất quán

3. **Tên và comments rõ ràng**:
   - Tên biến, hàm phải mô tả rõ mục đích
   - Comment giải thích logic phức tạp (bằng tiếng Việt)
   - Tránh comment thừa, comment sai

4. **Code dễ maintain**:
   - Extract constants, config
   - Separation of concerns
   - DRY (Don't Repeat Yourself)
   - SOLID principles khi phù hợp

## 🎯 Nguyên Tắc Làm Việc Chung

### 1. Xác Nhận Yêu Cầu Trước Khi Thực Hiện
- **LUÔN** đọc kỹ và hiểu rõ yêu cầu trước khi code
- **LUÔN** hỏi lại để xác nhận nếu yêu cầu chưa rõ ràng
- **LUÔN** trả lời ĐẦY ĐỦ tất cả câu hỏi của người dùng (cả ở trên lẫn dưới)
- Không được bỏ sót bất kỳ yêu cầu nào khi người dùng cung cấp document/PDF/tài liệu đề tài

### 2. Tư Duy Và Phân Tích Sâu
- Suy nghĩ kỹ về tác động ngắn hạn và dài hạn của mỗi thay đổi
- Tính toán xem thay đổi nào tối ưu nhất trước khi thực hiện
- **KHÔNG** thay đổi code "tội vạ" - mỗi thay đổi phải có lý do chính đáng
- Chủ động searching/tìm kiếm thông tin liên quan để đưa ra quyết định tốt nhất
- Khi cần chọn design pattern hoặc refactoring: TÌM KIẾM, TÌM HIỂU, TƯ DUY về phương án phù hợp và tối ưu nhất

### 3. Chủ Động Enhanced Code
- Luôn tìm cách cải thiện code nếu có thể (performance, maintainability, readability)
- Đề xuất các cải tiến nhưng cần giải thích rõ lợi ích
- Nếu bị reject thì hiểu rằng: phương án không hoạt động hoặc không mang lại hiệu quả mong đợi

### 4. Giải Thích Chi Tiết Mọi Thay Đổi
- **Khi sửa file:** Nói rõ sửa CÁI GÌ, TẠI SAO sửa
- Giải thích cụ thể lý do để cùng cân nhắc
- Không chỉ làm mà phải giải thích logic đằng sau

### 5. Hỗ Trợ Debug
- Khi người dùng báo lỗi hoặc gửi console log mà không hiểu:
  - Chỉ cách chèn console.log/Debug.WriteLine để lấy giá trị
  - Hướng dẫn debug bằng Visual Studio 2022 (breakpoint, watch, immediate window)
  - Đề xuất cách kiểm tra dữ liệu tại các điểm khác nhau

### 6. Yêu Cầu Đồ Án và Ước Lượng
- Đọc kỹ và TƯ DUY từng chỉ mục trong tài liệu yêu cầu đề tài
- Quyết định cách đảm bảo TẤT CẢ yêu cầu, không bỏ qua yêu cầu nào
- Nếu hành động người dùng đề xuất ảnh hưởng xấu đến yêu cầu đồ án → Cảnh báo và đề xuất phương án tốt hơn
- Hiểu biết về công cụ ước lượng: Điểm Chức Năng (Function Points), COCOMO II
- Tư duy không chỉ về code mà cả các khía cạnh quản lý dự án

### 7. Cấu Hình Cho Đồ Án Nhóm
- Dự án sẽ push lên GitHub cho nhiều người dùng
- Cấu hình phải phù hợp để mọi người đều dùng được
- Connection string phải dễ cấu hình (app.config, Settings.cs)
- Tránh hard-code các giá trị phụ thuộc môi trường cụ thể

---

## 🏗️ Kiến Trúc Dự Án

### Kiến Trúc 3 Lớp (Three-Tier Architecture)

```
┌─────────────────────────────────────────────┐
│  Presentation Layer (WinForms)              │
│  - frm*.cs, frm*.Designer.cs, frm*.resx    │
└─────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────┐
│  Business Logic Layer                       │
│  - Controller/*.cs                          │
│  - BusinessObject/*.cs (POCO Entities)      │
└─────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────┐
│  Data Access Layer                          │
│  - DataLayer/*Factory.cs                    │
│  - DataService.cs                           │
└─────────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────────┐
│  Database (SQL Server)                      │
│  - QLCHNongDuoc                            │
└─────────────────────────────────────────────┘
```

### Công Nghệ Stack
- **Framework:** .NET Framework 4.8
- **UI:** Windows Forms
- **Database:** SQL Server (với SQLEXPRESS)
- **Data Access:** ADO.NET (SqlConnection, SqlDataAdapter, SqlCommand)
  - **LƯU Ý:** Dự án KHÔNG dùng Entity Framework, mà dùng ADO.NET thuần
- **Reporting:** Microsoft ReportViewer (.rdlc files)
- **Architecture Pattern:** Factory Pattern, Repository-like pattern

---

## 📋 Quy Ước Code (Coding Conventions)

### 1. Naming Conventions

#### Variables & Fields
```csharp
// Private fields: prefix m_
private String m_Id;
private DataTable m_DataTable;
private SqlConnection m_Connection;

// Local variables: camelCase
int soLuong = 10;
string tenSanPham = "Thuốc trừ sâu";

// Parameters: camelCase
public void ThemSanPham(string tenSp, int soLuong)
```

#### Controls (Hungarian Notation)
```csharp
// ComboBox: cmb prefix
ComboBox cmbDonViTinh;

// DataGridView: dg prefix
DataGridView dgSanPham;

// TextBox: txt prefix
TextBox txtTenSanPham;

// NumericUpDown: num prefix
NumericUpDown numSoLuong;

// Button: btn prefix
Button btnLuu;

// BindingNavigator: bn prefix
BindingNavigator bnSanPham;
```

#### Classes & Methods
```csharp
// Classes: PascalCase
public class SanPham { }
public class SanPhamController { }
public class SanPhamFactory { }

// Methods: PascalCase (Tiếng Việt không dấu OK)
public void HienthiDataGridview() { }
public DataTable DanhsachSanPham() { }
public void CapNhatGiaNhap() { }
```

#### Database Objects
```csharp
// Tables: UPPER_CASE_WITH_UNDERSCORE
"SELECT * FROM SAN_PHAM"
"SELECT * FROM PHIEU_NHAP"
"SELECT * FROM CHI_TIET_PHIEU_BAN"

// Columns: UPPER_CASE_WITH_UNDERSCORE
"ID", "TEN_SAN_PHAM", "DON_GIA_NHAP", "GIA_BAN_LE", "SO_LUONG"
```

#### Files
```csharp
// Forms: frm prefix
frmMain.cs, frmSanPham.cs, frmBanLe.cs

// Business Objects: Class name only
SanPham.cs, KhachHang.cs, PhieuBan.cs

// Controllers: *Controller suffix
SanPhamController.cs, PhieuBanController.cs

// Data Layer: *Factory suffix
SanPhamFactory.cs, PhieuBanFactory.cs
```

### 2. Code Structure Patterns

#### BusinessObject (Entity/POCO)
```csharp
namespace CuahangNongduoc.BusinessObject
{
    public class SanPham
    {
        // Constructor mặc định
        public SanPham() { }
        
        // Constructor với parameters (nếu cần)
        public SanPham(String id, String tensp)
        {
            m_Id = id;
            m_TenSP = tensp;
        }
        
        // Private field với prefix m_
        private String m_Id;
        
        // Public property PascalCase
        public String Id
        {
            get { return m_Id; }
            set { m_Id = value; }
        }
        
        // Relationship properties
        private DonViTinh m_DonViTinh;
        public DonViTinh DonViTinh
        {
            get { return m_DonViTinh; }
            set { m_DonViTinh = value; }
        }
    }
}
```

#### Controller (Business Logic)
```csharp
namespace CuahangNongduoc.Controller
{
    public class SanPhamController
    {
        // Factory instance
        SanPhamFactory factory = new SanPhamFactory();
        
        // Method hiển thị lên UI controls
        public void HienthiAutoComboBox(ComboBox cmb)
        {
            DataTable tbl = factory.DanhsachSanPham(); 
            cmb.DataSource = tbl;
            cmb.DisplayMember = "TEN_SAN_PHAM";
            cmb.ValueMember = "ID";
            cmb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmb.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        
        // Method xử lý business logic
        public void CapNhatGiaNhap(int id, long gia_moi, long so_luong)
        {
            DataTable tbl = factory.LaySanPham(id);
            if (tbl.Rows.Count > 0)
            {
                // Business logic here
                long tong_so = Convert.ToInt32(tbl.Rows[0]["SO_LUONG"]);
                long tong_gia = Convert.ToInt64(tbl.Rows[0]["DON_GIA_NHAP"]);
                
                if (tong_gia != gia_moi)
                {
                    long thanh_tien = gia_moi * so_luong + tong_gia * tong_so;
                    tong_so += so_luong;
                    tbl.Rows[0]["DON_GIA_NHAP"] = thanh_tien / tong_so;
                    tbl.Rows[0]["SO_LUONG"] = tong_so;
                }
                factory.Save();
            }
        }
        
        // Method chuyển đổi DataTable -> BusinessObject
        public SanPham LaySanPham(int id)
        {
            DataTable tbl = factory.LaySanPham(id);
            SanPham sp = new SanPham();
            
            if (tbl.Rows.Count > 0)
            {
                sp.Id = Convert.ToString(tbl.Rows[0]["ID"]);
                sp.TenSanPham = Convert.ToString(tbl.Rows[0]["TEN_SAN_PHAM"]);
                sp.SoLuong = Convert.ToInt32(tbl.Rows[0]["SO_LUONG"]);
                // ... map other fields
            }
            return sp;
        }
        
        // CRUD wrapper methods
        public DataRow NewRow() => factory.NewRow();
        public void Add(DataRow row) => factory.Add(row);
        public bool Save() => factory.Save();
    }
}
```

#### DataLayer Factory (Data Access)
```csharp
namespace CuahangNongduoc.DataLayer
{
    public class SanPhamFactory
    {
        DataService m_Ds = new DataService();
        
        // SELECT queries - return DataTable
        public DataTable DanhsachSanPham()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand("SELECT * FROM SAN_PHAM");
            ds.Load(cmd);
            return ds;
        }
        
        // Parameterized queries - BẮT BUỘC dùng parameters
        public DataTable LaySanPham(int id)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM SAN_PHAM WHERE ID = @id"
            );
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            ds.Load(cmd);
            return ds;
        }
        
        // Search với LIKE
        public DataTable TimTenSanPham(String ten)
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand(
                "SELECT * FROM SAN_PHAM WHERE TEN_SAN_PHAM LIKE '%' + @ten + '%'"
            );
            cmd.Parameters.Add("@ten", SqlDbType.NVarChar).Value = ten;
            ds.Load(cmd);
            return ds;
        }
        
        // JOIN queries
        public DataTable LaySoLuongTon()
        {
            DataService ds = new DataService();
            SqlCommand cmd = new SqlCommand(
                @"SELECT SP.ID, SP.TEN_SAN_PHAM, SP.DON_GIA_NHAP, 
                         SP.GIA_BAN_SI, SP.GIA_BAN_LE, SP.ID_DON_VI_TINH, 
                         SP.SO_LUONG, SUM(MA.SO_LUONG) AS SO_LUONG_TON
                  FROM SAN_PHAM SP 
                  INNER JOIN MA_SAN_PHAM MA ON SP.ID = MA.ID_SAN_PHAM
                  GROUP BY SP.ID, SP.TEN_SAN_PHAM, SP.DON_GIA_NHAP, 
                           SP.GIA_BAN_SI, SP.GIA_BAN_LE, SP.ID_DON_VI_TINH, 
                           SP.SO_LUONG"
            );
            ds.Load(cmd);
            return ds;
        }
        
        // CUD operations - use m_Ds instance
        public DataRow NewRow() => m_Ds.NewRow();
        
        public void Add(DataRow row)
        {
            m_Ds.Rows.Add(row);
        }
        
        public bool Save()
        {
            return m_Ds.ExecuteNoneQuery() > 0;
        }
    }
}
```

### 3. DataService Usage

#### Connection String
```csharp
// Trong DataService.cs - Static connection string
public static String m_ConnectString = 
    "Server=.\\SQLEXPRESS;Initial Catalog=QLCHNongDuoc;Integrated Security=SSPI;TrustServerCertificate=True;";

// Có thể override từ Settings hoặc app.config
```

#### Transaction Pattern
```csharp
// DataService tự động dùng transaction cho mọi update
public int ExecuteNoneQuery()
{
    int result = 0;
    SqlTransaction tr = null;
    try
    {
        tr = m_Connection.BeginTransaction();
        m_Command.Connection = m_Connection;
        m_Command.Transaction = tr;
        
        m_DataAdapter = new SqlDataAdapter();
        m_DataAdapter.SelectCommand = m_Command;
        SqlCommandBuilder builder = new SqlCommandBuilder(m_DataAdapter);
        
        result = m_DataAdapter.Update(this);
        tr.Commit();
    }
    catch (Exception e)
    {
        if (tr != null) tr.Rollback();
    }
    return result;
}
```

#### Error Handling
```csharp
// Luôn có error handling và logging
catch (Exception e) 
{
    // Log cho developer
    System.Diagnostics.Debug.WriteLine("DataService.Load Error: " + e.Message);
    
    // Thông báo cho user (tiếng Việt có dấu OK)
    MessageBox.Show(
        "Lỗi kết nối database:\n" + e.Message + 
        "\n\nConnection: " + m_ConnectString, 
        "Lỗi", 
        MessageBoxButtons.OK, 
        MessageBoxIcon.Error
    );
}
```

---

## ⚠️ QUY TẮC BẮT BUỘC (CRITICAL RULES)

### 1. SQL Security
```csharp
// ❌ KHÔNG BAO GIỜ string concatenation
SqlCommand cmd = new SqlCommand(
    "SELECT * FROM SAN_PHAM WHERE ID = " + id // NGUY HIỂM!
);

// ✅ LUÔN LUÔN dùng parameters
SqlCommand cmd = new SqlCommand(
    "SELECT * FROM SAN_PHAM WHERE ID = @id"
);
cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
```

### 2. Data Binding
```csharp
// Luôn Clear() binding trước khi Add() mới
txtMaSp.DataBindings.Clear();
txtMaSp.DataBindings.Add("Text", bs, "ID");

cmbDVT.DataBindings.Clear();
cmbDVT.DataBindings.Add("SelectedValue", bs, "ID_DON_VI_TINH");
```

### 3. Transaction Usage
- Mọi thao tác INSERT/UPDATE/DELETE phải qua DataService
- DataService tự động wrap trong Transaction
- Rollback tự động khi có Exception

### 4. DataTable Best Practices
```csharp
// ✅ Mỗi query tạo DataService mới (trừ m_Ds trong Factory)
public DataTable DanhsachSanPham()
{
    DataService ds = new DataService(); // New instance
    SqlCommand cmd = new SqlCommand("SELECT * FROM SAN_PHAM");
    ds.Load(cmd);
    return ds;
}

// ✅ Dùng m_Ds instance cho operations cần update
DataService m_Ds = new DataService();
public DataRow NewRow() => m_Ds.NewRow();
public bool Save() => m_Ds.ExecuteNoneQuery() > 0;
```

### 5. Không Tạo Helper Functions "Ngố"
```csharp
// ❌ TRÁNH: Helper functions vô nghĩa
public static string GetTextOrEmpty(TextBox txt) 
{
    return txt.Text ?? "";
}

// ✅ TỐT: Viết trực tiếp, rõ ràng
string ten = txtTenSP.Text ?? "";
```

---

## 🎨 UI/UX Conventions

### Form Design
- Forms có tiền tố `frm`
- Mỗi form có Designer.cs và resx (resource) file
- Sử dụng ReportViewer cho in ấn (rpt*.rdlc)

### Control Naming
- Đặt tên control phải có ý nghĩa: `btnLuu`, `btnThoat`, `btnThemMoi`
- KHÔNG đặt tên: `button1`, `textBox1` (default names)

### Resource Management
- Images trong Resources/ folder
- Icons: .ico format
- Reports: Report/ folder với .rdlc files

---

## 🗄️ Database Conventions

### Table Names
- UPPER_CASE_WITH_UNDERSCORE
- Ví dụ: `SAN_PHAM`, `PHIEU_NHAP`, `CHI_TIET_PHIEU_BAN`

### Column Names
- UPPER_CASE_WITH_UNDERSCORE
- Primary Key: `ID` (int identity)
- Foreign Key: `ID_<TABLE_NAME>` (ví dụ: `ID_SAN_PHAM`, `ID_DON_VI_TINH`)

### SQL Query Style
```sql
-- Multi-line queries: indent và format rõ ràng
SELECT SP.ID, SP.TEN_SAN_PHAM, SP.DON_GIA_NHAP, 
       SP.GIA_BAN_SI, SP.GIA_BAN_LE, SP.ID_DON_VI_TINH, 
       SP.SO_LUONG, SUM(MA.SO_LUONG) AS SO_LUONG_TON
FROM SAN_PHAM SP 
INNER JOIN MA_SAN_PHAM MA ON SP.ID = MA.ID_SAN_PHAM
GROUP BY SP.ID, SP.TEN_SAN_PHAM, SP.DON_GIA_NHAP, 
         SP.GIA_BAN_SI, SP.GIA_BAN_LE, SP.ID_DON_VI_TINH, 
         SP.SO_LUONG
```

---

## 📂 Cấu Trúc Thư Mục

```
CHND/
├── BusinessObject/          # Entity models (POCO)
│   ├── SanPham.cs
│   ├── KhachHang.cs
│   ├── PhieuBan.cs
│   └── ...
├── Controller/              # Business logic
│   ├── SanPhamController.cs
│   ├── KhachHangController.cs
│   └── ...
├── DataLayer/               # Data access (Factory pattern)
│   ├── SanPhamFactory.cs
│   ├── KhachHangFactory.cs
│   └── ...
├── Properties/              # Assembly info, settings
│   ├── DataSources/        # Report data sources
│   └── ...
├── Report/                  # RDLC report files
│   ├── rptPhieuBan.rdlc
│   └── ...
├── Resources/               # Images, icons
├── data/                    # SQL scripts
│   └── CHNongDuoc.sql
├── frm*.cs                  # WinForms (UI)
├── DataService.cs           # Core data service
├── Program.cs               # Entry point
├── Settings.cs              # App settings
├── ThamSo.cs               # App parameters
└── app.config              # Configuration
```

---

## 🔧 Development Workflow

### 1. Thêm Entity Mới
1. Tạo class trong `BusinessObject/`
2. Tạo Factory trong `DataLayer/`
3. Tạo Controller trong `Controller/`
4. Tạo Form(s) trong root (nếu cần)

### 2. Thêm Tính Năng Mới
1. Phân tích yêu cầu kỹ càng
2. Xác định entity/table liên quan
3. Implement từ dưới lên: DataLayer → Controller → UI
4. Test từng layer

### 3. Sửa Bug
1. Xác định layer bị lỗi (DataLayer/Controller/UI)
2. Sử dụng Debug trong Visual Studio 2022:
   - Breakpoint tại dòng nghi ngờ
   - Watch window để xem giá trị
   - Immediate window để test expressions
3. Fix và verify

### 4. Refactoring
- Không refactor không có lý do
- Phải đảm bảo backward compatibility
- Test kỹ sau khi refactor
- Commit từng bước nhỏ

---

## 🚀 Git & GitHub Workflow

### Branch Strategy
- `master` - stable code only
- Feature branches: `feature/ten-tinh-nang`
- Bug fixes: `fix/mo-ta-loi`

### Commit Messages (Tiếng Việt OK)
```
feat: Thêm quản lý phiếu chi
fix: Sửa lỗi tính toán giá nhập trung bình
refactor: Tối ưu SanPhamController
docs: Cập nhật README với hướng dẫn cài đặt
```

### Configuration Files
- `app.config` - Không commit thông tin nhạy cảm
- Connection string phải dễ thay đổi cho từng máy
- Document rõ cách cấu hình cho thành viên mới

---

## 🧪 Testing & Debugging

### Visual Studio 2022 Debugging
```csharp
// Sử dụng Debug.WriteLine cho logging
System.Diagnostics.Debug.WriteLine($"SanPham ID: {sp.Id}, Tên: {sp.TenSanPham}");

// Breakpoint conditions: Click phải breakpoint → Conditions
// Watch variables: Debug → Windows → Watch
// Immediate Window: Debug → Windows → Immediate (Ctrl+Alt+I)
```

### Error Handling Pattern
```csharp
try
{
    // Code logic
}
catch (SqlException ex)
{
    // Log cho developer
    Debug.WriteLine($"SQL Error: {ex.Message}");
    
    // Thông báo user-friendly
    MessageBox.Show(
        "Không thể kết nối cơ sở dữ liệu. Vui lòng kiểm tra lại.",
        "Lỗi",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error
    );
}
catch (Exception ex)
{
    Debug.WriteLine($"Unexpected Error: {ex.Message}\n{ex.StackTrace}");
    MessageBox.Show(
        $"Đã xảy ra lỗi: {ex.Message}",
        "Lỗi",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error
    );
}
```

---

## 📚 Common Patterns

### Pattern 1: Load Data to DataGridView with Binding
```csharp
public void HienthiDataGridview(
    DataGridView dg, 
    BindingNavigator bn,
    TextBox txtMaSp, 
    TextBox txtTenSp, 
    ComboBox cmbDVT,
    // ... other controls
)
{
    BindingSource bs = new BindingSource();
    bs.DataSource = factory.DanhsachSanPham();
    
    // Clear old bindings
    txtMaSp.DataBindings.Clear();
    txtTenSp.DataBindings.Clear();
    cmbDVT.DataBindings.Clear();
    
    // Add new bindings
    txtMaSp.DataBindings.Add("Text", bs, "ID");
    txtTenSp.DataBindings.Add("Text", bs, "TEN_SAN_PHAM");
    cmbDVT.DataBindings.Add("SelectedValue", bs, "ID_DON_VI_TINH");
    
    // Bind to controls
    bn.BindingSource = bs;
    dg.DataSource = bs;
}
```

### Pattern 2: ComboBox AutoComplete
```csharp
public void HienthiAutoComboBox(ComboBox cmb)
{
    DataTable tbl = factory.DanhsachSanPham();
    cmb.DataSource = tbl;
    cmb.DisplayMember = "TEN_SAN_PHAM";
    cmb.ValueMember = "ID";
    cmb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    cmb.AutoCompleteSource = AutoCompleteSource.ListItems;
}
```

### Pattern 3: DataTable to BusinessObject List
```csharp
public static IList<SanPham> LayDanhSach()
{
    SanPhamFactory f = new SanPhamFactory();
    DataTable tbl = f.DanhsachSanPham();
    IList<SanPham> ds = new List<SanPham>();
    
    foreach (DataRow row in tbl.Rows)
    {
        SanPham sp = new SanPham();
        sp.Id = Convert.ToString(row["ID"]);
        sp.TenSanPham = Convert.ToString(row["TEN_SAN_PHAM"]);
        sp.SoLuong = Convert.ToInt32(row["SO_LUONG"]);
        sp.DonGiaNhap = Convert.ToInt64(row["DON_GIA_NHAP"]);
        ds.Add(sp);
    }
    return ds;
}
```

---

## 🛠️ Utility Classes & Helper Patterns

### ThamSo.cs - Application Parameters Manager

**Mục đích:** Quản lý tham số ứng dụng từ database table `THAM_SO`

**Chức năng chính:**

#### 1. Auto-increment IDs cho Phiếu
```csharp
// Pattern: Lấy ID tiếp theo từ database
public static long LayMaPhieuNhap()
{
    DataService ds = new DataService();
    object obj = ds.ExecuteScalar(new SqlCommand("SELECT PHIEU_NHAP FROM THAM_SO"));
    return Convert.ToInt64(obj);
}

// Pattern: Cập nhật ID sau khi tạo phiếu mới
public static void GanMaPhieuNhap(long id)
{
    DataService ds = new DataService();
    SqlCommand cmd = new SqlCommand("UPDATE THAM_SO SET PHIEU_NHAP = @id");
    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
    ds.ExecuteNoneQuery(cmd);
}
```

**Các loại ID được quản lý:**
- `PHIEU_NHAP` - Mã phiếu nhập
- `PHIEU_BAN` - Mã phiếu bán
- `PHIEU_THANH_TOAN` - Mã phiếu thanh toán
- `PHIEU_CHI` - Mã phiếu chi
- `SAN_PHAM` - Mã sản phẩm
- `NHA_CUNG_CAP` - Mã nhà cung cấp
- `KHACH_HANG` - Mã khách hàng

#### 2. Static Properties Pattern
```csharp
// Property cho auto-increment IDs
public static long SanPham
{
    get 
    {
        DataService ds = new DataService();
        object obj = ds.ExecuteScalar(new SqlCommand("SELECT SAN_PHAM FROM THAM_SO"));
        return Convert.ToInt64(obj);
    }
    set 
    {
        DataService ds = new DataService();
        SqlCommand cmd = new SqlCommand("UPDATE THAM_SO SET SAN_PHAM = @value");
        cmd.Parameters.Add("@value", SqlDbType.BigInt).Value = value;
        ds.ExecuteNoneQuery(cmd);
    }
}
```

#### 3. Thông Tin Cửa Hàng
```csharp
public static CuaHang LayCuaHang()
{
    CuaHang ch = new CuaHang();
    DataService ds = new DataService();
    ds.Load(new SqlCommand("SELECT TEN_CUA_HANG, DIA_CHI, DIEN_THOAI FROM THAM_SO"));
    if (ds.Rows.Count > 0)
    {
        ch.TenCuaHang = ds.Rows[0]["TEN_CUA_HANG"].ToString();
        ch.DiaChi = ds.Rows[0]["DIA_CHI"].ToString();
        ch.DienThoai = ds.Rows[0]["DIEN_THOAI"].ToString();
    }
    return ch;
}

public static void GanCuaHang(String ten_cua_hang, String dia_chi, String dien_thoai)
{
    DataService ds = new DataService();
    SqlCommand cmd = new SqlCommand(
        "UPDATE THAM_SO SET TEN_CUA_HANG = @ten_cua_hang, DIA_CHI = @dia_chi, DIEN_THOAI = @dien_thoai"
    );
    cmd.Parameters.Add("@ten_cua_hang", SqlDbType.NVarChar).Value = ten_cua_hang;
    cmd.Parameters.Add("@dia_chi", SqlDbType.NVarChar).Value = dia_chi;
    cmd.Parameters.Add("@dien_thoai", SqlDbType.NVarChar).Value = dien_thoai;
    ds.ExecuteNoneQuery(cmd);
}
```

#### 4. Helper Methods
```csharp
// Tính tháng trước
public static void PreMonth(ref int thangtruoc, ref int namtruoc, int thang, int nam)
{
    thangtruoc = thang - 1;
    namtruoc = nam;
    if (thangtruoc == 0)
    {
        thangtruoc = 12;
        namtruoc = nam - 1;
    }
}

// Validate số nguyên
public static bool LaSoNguyen(String so)
{
    try
    {
        Convert.ToInt64(so);
        return true;
    }
    catch
    {
        return false;
    }
}
```

#### 5. Enum Controll
```csharp
// Enum để quản lý trạng thái form
public enum Controll
{
    Normal,   // Xem dữ liệu
    AddNew,   // Thêm mới
    Edit      // Chỉnh sửa
}
```

**Khi nào dùng ThamSo.cs:**
- Cần lấy ID tiếp theo cho phiếu mới
- Load/Save thông tin cửa hàng
- Validate input
- Tính toán ngày tháng

**Pattern tương tự cho Utility classes khác:**
- Static methods cho các helper functions chung
- Không có state (stateless)
- Direct database access cho configuration data

---

## ⚠️ Known Issues & Technical Debt

### 🔴 CRITICAL: SQL Injection trong ThamSo.cs

**Vị trí lỗi:** Một số methods trong ThamSo.cs đang dùng string concatenation thay vì parameters

**Code có vấn đề:**
```csharp
// ❌ SQL INJECTION RISK
public static void GanMaPhieuNhap(long id)
{
    DataService ds = new DataService();
    ds.ExecuteNoneQuery(new SqlCommand("UPDATE THAM_SO SET PHIEU_NHAP = " + id));
}
```

**Phải sửa thành:**
```csharp
// ✅ SAFE - Dùng parameters
public static void GanMaPhieuNhap(long id)
{
    DataService ds = new DataService();
    SqlCommand cmd = new SqlCommand("UPDATE THAM_SO SET PHIEU_NHAP = @id");
    cmd.Parameters.Add("@id", SqlDbType.BigInt).Value = id;
    ds.ExecuteNoneQuery(cmd);
}
```

**Methods cần fix:**
1. `GanMaPhieuNhap(long id)` - Line ~50
2. `GanMaPhieuBan(long id)` - Line ~63
3. `GanMaPhieuThanhToan(long id)` - Line ~76
4. `SanPham` setter - Line ~93
5. `NhaCungCap` setter - Line ~135
6. `KhachHang` setter - Line ~150
7. `PhieuChi` setter - Line ~165

**Priority:** HIGH - Cần fix trước khi deploy

---

## 🎓 Khi Làm Việc Với AI (Cursor)

### Model Capabilities
- **Claude Sonnet 4.5** có thể đọc được:
  - Code và text
  - **PDF files** (có thể upload trực tiếp)
  - **Images/Screenshots** (chụp màn hình tài liệu)
- Có thể phân tích document yêu cầu đồ án và tài liệu bài giảng

### Yêu Cầu Rõ Ràng
```
✅ TỐT: "Tạo form quản lý nhà cung cấp với các field: 
        Mã, Tên, Địa chỉ, Số điện thoại. Có chức năng 
        thêm/sửa/xóa và tìm kiếm theo tên"

❌ KHÔNG TỐT: "Làm form nhà cung cấp"
```

### Xác Nhận Trước Khi Thực Hiện
- AI sẽ hỏi lại nếu yêu cầu chưa rõ
- Xác nhận design pattern/approach trước khi code
- Review code change trước khi apply

---

## 📖 Tài Liệu Tham Khảo

### Internal Documentation
- `data/CHNongDuoc.sql` - Database schema
- Forms Designer - Xem cấu trúc UI hiện có
- Existing code - Học từ code đã có (convention, pattern)

### External Resources
- [ADO.NET Documentation](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/)
- [WinForms Documentation](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/)
- [SQL Server T-SQL Reference](https://learn.microsoft.com/en-us/sql/t-sql/)
- [C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)

---

## ✅ Checklist Trước Khi Commit

- [ ] Code compile không lỗi
- [ ] Đã test các chức năng bị ảnh hưởng
- [ ] Đã follow naming conventions
- [ ] Đã thêm error handling
- [ ] SQL queries dùng parameters (không string concat)
- [ ] Đã clear bindings trước khi add mới
- [ ] Connection string có thể config được
- [ ] Đã comment cho phần code phức tạp
- [ ] Đã review diff trước khi commit
- [ ] Commit message rõ ràng

---

## 🎯 Mục Tiêu Chất Lượng

1. **Maintainability**: Code dễ đọc, dễ hiểu, dễ sửa
2. **Consistency**: Nhất quán với codebase hiện có
3. **Security**: Tránh SQL injection, validate input
4. **Performance**: Tối ưu query, tránh N+1 queries
5. **Teamwork**: Code dễ dàng cho người khác hiểu và làm việc

---

## 📅 Project Context & Timeline

### Team Information
- **Team Size:** 7 thành viên
- **Active Contributors:** ~5 người (bao gồm lead developer)
- **Key Principle:** Lead developer cần hiểu toàn bộ hệ thống và có khả năng hoàn thành độc lập nếu cần

### Timeline
- **Hard Deadline:** 17/11/2025 (16 ngày kể từ 01/11)
- **Extended Deadline:** 24/11/2025 (khả năng thấp)
- **Current Date:** 01/11/2025

### Priority Strategy
1. **Week 1 (01-07/11):** 
   - Fix critical bugs (SQL injection)
   - Hoàn thiện các chức năng core
   - Đảm bảo đáp ứng đầy đủ yêu cầu đồ án

2. **Week 2 (08-14/11):**
   - Testing toàn bộ hệ thống
   - Polish UI/UX
   - Chuẩn bị tài liệu báo cáo

3. **Final Days (15-17/11):**
   - Final testing
   - Bug fixes
   - Deployment preparation

### Risk Management
- Cần đảm bảo lead developer hiểu 100% codebase
- Mỗi feature phải có documentation rõ ràng
- Code phải maintainable và dễ debug
- Ưu tiên hoàn thành yêu cầu bắt buộc trước khi làm features bổ sung

---

*Quy tắc này dựa trên phân tích cấu trúc code hiện có và nguyên tắc làm việc đã nêu. Có thể cập nhật khi dự án phát triển.*

