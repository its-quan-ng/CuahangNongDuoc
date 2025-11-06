# External Libraries / Third-party DLLs

Thư mục này chứa các **external DLL** không có trên NuGet hoặc cần reference trực tiếp.

## 📦 Danh sách DLL

### XPExplorerBar.dll
- **Version:** 3.3.0.0
- **PublicKeyToken:** 26272737b5f33015
- **Mục đích:** UI component cho TaskPane trong form chính (`frmMain.cs`)
- **License:** Freeware (kiểm tra license trước khi phân phối thương mại)
- **Nguồn:** [WinForms XPExplorerBar Control](http://www.dotnetbar.com/) (hoặc tương tự)

### Các DLL khác
- `Microsoft.ReportViewer.*.dll` - Được quản lý qua NuGet package, copy vào đây để backup
- `Microsoft.SqlServer.Types.dll` - Được quản lý qua NuGet package

## ⚠️ Lưu Ý

- **KHÔNG XÓA** các file trong thư mục này
- Các DLL này được **commit vào Git** để đảm bảo mọi người clone về đều build được
- Nếu thiếu DLL, project sẽ **không compile**

## 🔧 Troubleshooting

### Lỗi: "Could not load file or assembly 'XPExplorerBar'"

**Nguyên nhân:** File DLL bị thiếu hoặc không được reference đúng

**Giải pháp:**
1. Kiểm tra file `XPExplorerBar.dll` có tồn tại trong thư mục `lib/`
2. Mở file `.csproj`, tìm đến reference của XPExplorerBar
3. Đảm bảo có dòng: `<HintPath>lib\XPExplorerBar.dll</HintPath>`
4. Clean solution và Rebuild

### Lỗi: "The referenced component 'XPExplorerBar' could not be found"

**Giải pháp:**
1. Trong Visual Studio: `Solution Explorer` → `References` → Right-click `XPExplorerBar` → `Remove`
2. Right-click `References` → `Add Reference...` → `Browse` → Chọn `lib\XPExplorerBar.dll`
3. Rebuild project

---

*Nếu có vấn đề, liên hệ lead developer hoặc check Git history để biết DLL được thêm khi nào.*

