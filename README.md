# 🏪 Hệ Thống Quản Lý Cửa Hàng Nông Dược

Phần mềm quản lý cửa hàng kinh doanh nông dược (phân bón, thuốc trừ sâu, thuốc bảo vệ thực vật).

## 🎯 Tính Năng Chính

### Quản Lý Danh Mục
- Quản lý sản phẩm (phân bón, thuốc BVTV)
- Quản lý đơn vị tính
- Quản lý khách hàng (lẻ & đại lý)
- Quản lý nhà cung cấp

### Nghiệp Vụ
- Lập phiếu nhập hàng (tự động cập nhật giá nhập trung bình)
- Bán lẻ (bán cho khách hàng cá nhân)
- Bán sỉ (bán cho đại lý, có công nợ)
- Quản lý phiếu thu/chi
- Thanh toán công nợ

### Báo Cáo & Thống Kê
- Báo cáo tồn kho (theo serial/mã sản phẩm)
- Báo cáo doanh thu theo thời gian
- Báo cáo công nợ khách hàng
- Báo cáo sản phẩm hết hạn
- Báo cáo số lượng bán

## 🛠️ Công Nghệ

- **Framework:** .NET Framework 4.8
- **UI:** Windows Forms
- **Database:** SQL Server (SQLEXPRESS)
- **Data Access:** ADO.NET (không dùng Entity Framework)
- **Reporting:** Microsoft ReportViewer (.rdlc)
- **Architecture:** 3-Tier Architecture (Presentation → Business Logic → Data Access)

## 📋 Yêu Cầu Hệ Thống

### Phát Triển (Development)
- Windows 10/11
- Visual Studio 2022 (recommended) hoặc Visual Studio 2019
- .NET Framework 4.8 SDK
- SQL Server 2019 Express (hoặc cao hơn)

### Triển Khai (Deployment)
- Windows 7 SP1 trở lên
- .NET Framework 4.8 Runtime
- SQL Server 2019 Express LocalDB (hoặc SQL Server instance)

## 🚀 Hướng Dẫn Cài Đặt

### Bước 1: Clone Repository

```bash
git clone https://github.com/your-team/CHND.git
cd CHND/CHND
```

### Bước 2: Restore NuGet Packages

Trong Visual Studio:
1. Mở file `CuahangNongDuoc.sln`
2. Visual Studio sẽ tự động restore packages
3. Hoặc click chuột phải vào Solution → `Restore NuGet Packages`

Hoặc dùng command line:
```bash
nuget restore CuahangNongDuoc.sln
```

### Bước 3: Tạo Database

1. Mở **SQL Server Management Studio (SSMS)**
2. Connect đến server: `.\SQLEXPRESS` (hoặc tên server của bạn)
3. Mở file `data/CHNongDuoc.sql`
4. Execute script để tạo database và tables

### Bước 4: Cấu Hình Connection String

**Option A: Sửa trong `DataService.cs`** (line ~20)

```csharp
public static String m_ConnectString = 
    "Server=.\\SQLEXPRESS;Initial Catalog=QLCHNongDuoc;Integrated Security=SSPI;TrustServerCertificate=True;";
```

Thay `.\SQLEXPRESS` bằng SQL Server instance của bạn.

**Option B: Dùng `app.config`** (recommended cho production)

Thêm vào `app.config`:
```xml
<connectionStrings>
  <add name="QLCHNongDuoc" 
       connectionString="Server=.\SQLEXPRESS;Database=QLCHNongDuoc;Integrated Security=True;TrustServerCertificate=True;" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### Bước 5: Build & Run

1. Build Solution: `Ctrl+Shift+B` hoặc `Build → Build Solution`
2. Run: `F5` hoặc `Debug → Start Debugging`

## 🏗️ Cấu Trúc Project

```
CHND/
├── BusinessObject/          # Entity models (POCO)
│   ├── SanPham.cs
│   ├── KhachHang.cs
│   └── ...
├── Controller/              # Business logic layer
│   ├── SanPhamController.cs
│   └── ...
├── DataLayer/               # Data access layer (Factory pattern)
│   ├── SanPhamFactory.cs
│   └── ...
├── Report/                  # RDLC report files
├── Resources/               # Images, icons
├── lib/                     # External DLLs (XPExplorerBar, etc.)
├── data/                    # SQL scripts
│   └── CHNongDuoc.sql
├── frm*.cs                  # WinForms (UI layer)
├── DataService.cs           # Core data service (ADO.NET)
├── ThamSo.cs               # App parameters
└── Settings.cs             # App settings
```

## 🐛 Troubleshooting

### Lỗi: "Could not load file or assembly 'XPExplorerBar'"

**Nguyên nhân:** DLL bị thiếu

**Giải pháp:**
1. Kiểm tra thư mục `lib/` có file `XPExplorerBar.dll`
2. Nếu không có, check Git LFS hoặc liên hệ team
3. Xem thêm: `lib/README.md`

### Lỗi: "Cannot open database 'QLCHNongDuoc'"

**Nguyên nhân:** Database chưa được tạo

**Giải pháp:**
1. Chạy script `data/CHNongDuoc.sql` trong SSMS
2. Kiểm tra connection string trong `DataService.cs`

### Lỗi: "Login failed for user"

**Nguyên nhân:** SQL Server authentication

**Giải pháp:**
1. Sử dụng `Integrated Security=True` (Windows Authentication)
2. Hoặc đổi sang SQL Authentication và thêm `User ID=sa;Password=yourpassword`

### Build Warning: "Could not resolve this reference"

**Giải pháp:**
1. Clean Solution: `Build → Clean Solution`
2. Restore NuGet Packages
3. Rebuild: `Build → Rebuild Solution`

## 📚 Tài Liệu

- **Quy tắc Code:** Xem file `.cursorrules` (chỉ dành cho dev)
- **Database Schema:** Xem file `data/CHNongDuoc.sql`
- **External DLLs:** Xem `lib/README.md`

## 👥 Team

- **Size:** 7 thành viên
- **Lead Developer:** [Tên của bạn]
- **Timeline:** Deadline 17/11/2025

## 📝 Git Workflow

### Branch Strategy
- `master` - Production-ready code
- `feature/*` - New features
- `fix/*` - Bug fixes

### Commit Convention
```
feat: Thêm chức năng quản lý phiếu chi
fix: Sửa lỗi tính giá nhập trung bình
refactor: Tối ưu SanPhamController
docs: Cập nhật README
```

## ⚙️ Development Guidelines

### Naming Conventions

**Controls:**
- ComboBox: `cmb` prefix (e.g., `cmbDonViTinh`)
- DataGridView: `dg` prefix (e.g., `dgSanPham`)
- TextBox: `txt` prefix (e.g., `txtTenSP`)
- Button: `btn` prefix (e.g., `btnLuu`)

**Classes:**
- Forms: `frm` prefix (e.g., `frmSanPham`)
- Controllers: `*Controller` suffix (e.g., `SanPhamController`)
- Factories: `*Factory` suffix (e.g., `SanPhamFactory`)

**Database:**
- Tables: `UPPER_CASE` (e.g., `SAN_PHAM`, `PHIEU_NHAP`)
- Columns: `UPPER_CASE` (e.g., `TEN_SAN_PHAM`, `DON_GIA`)

### Security
⚠️ **BẮT BUỘC:** Luôn dùng SQL Parameters, KHÔNG string concatenation

```csharp
// ❌ WRONG - SQL Injection risk
SqlCommand cmd = new SqlCommand("SELECT * FROM SAN_PHAM WHERE ID = " + id);

// ✅ CORRECT - Safe
SqlCommand cmd = new SqlCommand("SELECT * FROM SAN_PHAM WHERE ID = @id");
cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
```

## 📄 License

[Thêm license của dự án nếu có]

## 📞 Liên Hệ

Nếu có vấn đề, liên hệ:
- Lead Developer: [email/phone]
- GitHub Issues: [repo-url]/issues

---

*Dự án Đồ Án Chuyên Ngành - Quản Lý Cửa Hàng Nông Dược*

