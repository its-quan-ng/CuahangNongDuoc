# 🏪 Hệ Thống Quản Lý Cửa Hàng Nông Dược

<p align="center">
  <img src="CuahangNongduoc.ico" alt="Logo" width="80" height="80">
</p>

<p align="center">
  Phần mềm quản lý cửa hàng kinh doanh nông dược (phân bón, thuốc trừ sâu, thuốc bảo vệ thực vật).
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET%20Framework-4.8-blue?style=flat-square&logo=.net" alt=".NET Framework">
  <img src="https://img.shields.io/badge/Platform-Windows-lightgrey?style=flat-square&logo=windows" alt="Platform">
  <img src="https://img.shields.io/badge/Database-SQL%20Server-red?style=flat-square&logo=microsoft-sql-server" alt="Database">
  <img src="https://img.shields.io/badge/UI-WinForms-green?style=flat-square" alt="WinForms">
</p>

---

## 📸 Screenshots



| Màn hình chính | Bán lẻ | Bán sỉ |
|:-:|:-:|:-:|
| **[Main]**<img width="1919" height="1013" alt="image" src="https://github.com/user-attachments/assets/5a5c16ca-c597-4109-b665-3bb5a60e5d39" /> | [BanLe]<img width="927" height="494" alt="image" src="https://github.com/user-attachments/assets/5e8eb069-045e-4146-9e9b-f626b3d12183" /> | [BanSi]<img width="1919" height="1014" alt="image" src="https://github.com/user-attachments/assets/ef179faf-91ac-4b79-ad49-7ae74a02a803" />|

| Nhập hàng | Quản lý sản phẩm | Báo cáo |
|:-:|:-:|:-:|
| [NhapHang]<img width="1919" height="1021" alt="image" src="https://github.com/user-attachments/assets/628046c1-08a4-4a88-b40f-9214e7a93b31" /> | [SanPham]<img width="974" height="681" alt="image" src="https://github.com/user-attachments/assets/c1f286da-518f-4d9f-b36d-2a1ff9d3098e" /> | [BaoCao]<img width="996" height="645" alt="image" src="https://github.com/user-attachments/assets/6f467161-12f9-4a62-85de-b9dc29f5d669" />|

---

## 🎯 Tính Năng Chính

### 📦 Quản Lý Danh Mục
- ✅ Quản lý sản phẩm (phân bón, thuốc BVTV)
- ✅ Quản lý đơn vị tính
- ✅ Quản lý khách hàng (lẻ & đại lý)
- ✅ Quản lý nhà cung cấp
- ✅ Quản lý người dùng & phân quyền

### 💼 Nghiệp Vụ
- ✅ Lập phiếu nhập hàng (tự động cập nhật giá nhập trung bình)
- ✅ Bán lẻ (bán cho khách hàng cá nhân)
- ✅ Bán sỉ (bán cho đại lý, có công nợ)
- ✅ Quản lý phiếu thu/chi
- ✅ Thanh toán công nợ
- ✅ Quản lý khuyến mãi

### 📊 Báo Cáo & Thống Kê
- ✅ Báo cáo tồn kho (theo serial/mã sản phẩm)
- ✅ Báo cáo doanh thu theo thời gian
- ✅ Báo cáo công nợ khách hàng
- ✅ Báo cáo sản phẩm hết hạn
- ✅ Báo cáo số lượng bán
- ✅ Thống kê chi phí phụ, chiết khấu

---

## 🛠️ Công Nghệ Sử Dụng

| Thành phần | Công nghệ |
|------------|-----------|
| **Framework** | .NET Framework 4.8 |
| **UI** | Windows Forms |
| **Database** | SQL Server (SQLEXPRESS) |
| **Data Access** | ADO.NET |
| **Reporting** | Microsoft ReportViewer (.rdlc) |
| **Architecture** | 3-Tier (Presentation → Business Logic → Data Access) |
| **Design Patterns** | Strategy, Decorator, Specification |

---

## 📁 Cấu Trúc Thư Mục

```
CHND/
├── 📂 Forms/                    # Windows Forms (UI Layer)
│   ├── Main/                    # Form chính & đăng nhập
│   ├── DanhMuc/                 # Quản lý danh mục
│   ├── NghiepVu/                # Các nghiệp vụ (nhập, bán, chi)
│   ├── DanhSach/                # Danh sách phiếu
│   ├── TimKiem/                 # Forms tìm kiếm
│   ├── BaoCao/                  # Forms báo cáo
│   ├── In/                      # Forms in ấn
│   └── CauHinh/                 # Cấu hình hệ thống
│
├── 📂 BusinessObject/           # Business Objects (Entity Layer)
├── 📂 Controller/               # Controllers (Business Logic Layer)
├── 📂 DataLayer/                # Data Access Layer (Factories)
├── 📂 Report/                   # Report templates (.rdlc)
├── 📂 Resources/                # Images & Icons
├── 📂 database/                 # SQL Scripts
├── 📂 docs/                     # Documentation
├── 📂 lib/                      # External DLLs
│
├── 📂 Strategy/                 # Strategy Pattern (Xuất kho, Tính giá)
├── 📂 Decorator/                # Decorator Pattern (Tổng tiền)
├── 📂 Specification/            # Specification Pattern (Khuyến mãi)
│
├── 📄 Program.cs                # Entry point
├── 📄 DataService.cs            # Database connection
├── 📄 ThamSo.cs                 # Application parameters
└── 📄 CuahangNongduoc.csproj    # Project file
```

---

## 🚀 Hướng Dẫn Cài Đặt

### 📋 Yêu Cầu Hệ Thống

#### Phát Triển (Development)
- Windows 10/11
- Visual Studio 2022 (hoặc 2019)
- .NET Framework 4.8 SDK
- SQL Server 2019 Express trở lên

#### Triển Khai (Deployment)
- Windows 7 SP1 trở lên
- .NET Framework 4.8 Runtime
- SQL Server 2019 Express

---

### Bước 1: Clone Repository

```bash
git clone https://github.com/its-quan-ng/CuahangNongDuoc.git
cd CuahangNongDuoc
```

### Bước 2: Tạo Database

1. Mở **SQL Server Management Studio (SSMS)**
2. Connect đến server: `.\SQLEXPRESS`
3. Mở file `database/CHNongDuoc.sql`
4. Execute script để tạo database và tables

```sql
-- Hoặc dùng command line:
sqlcmd -S .\SQLEXPRESS -i database/CHNongDuoc.sql
```

### Bước 3: Cấu Hình Connection String

Mở file `DataService.cs` và sửa connection string (nếu cần):

```csharp
public static String m_ConnectString = 
    "Server=.\\SQLEXPRESS;Initial Catalog=QLCHNongDuoc;Integrated Security=SSPI;TrustServerCertificate=True;";
```

### Bước 4: Restore NuGet Packages & Build

1. Mở file `CuahangNongDuoc.sln` trong Visual Studio
2. Visual Studio sẽ tự động restore packages
3. Build: `Ctrl+Shift+B`
4. Run: `F5`

---

## 🔑 Tài Khoản Mặc Định

| Username | Password | Quyền |
|----------|----------|-------|
| `admin`  | `123`    | Admin (full quyền) |

> ⚠️ **Lưu ý:** Đổi mật khẩu mặc định sau khi cài đặt!

---

## 📖 Tài Liệu Thêm

- 📄 [Database Schema](database/CHNongDuoc.sql) - Cấu trúc database
- 📄 [Hướng dẫn SQL](database/HD-run-SQL-va-taikhoan.txt) - Hướng dẫn chạy script SQL
- 📄 [Thư viện ngoài](lib/README.md) - Danh sách DLL dependencies

---

## 🤝 Contributing

Xem [CONTRIBUTING.md](CONTRIBUTING.md) để biết cách đóng góp cho dự án.

---

## 📝 Changelog

Xem [CHANGELOG.md](CHANGELOG.md) để theo dõi lịch sử thay đổi.

---

## 📜 License

Dự án này được phát triển cho mục đích học tập.

---

<p align="center">
  Made with ❤️ by CHND Team
</p>
