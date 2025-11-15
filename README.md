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
