# Changelog

Tất cả các thay đổi đáng chú ý của dự án sẽ được ghi lại ở đây.

Định dạng dựa trên [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

---

## [Unreleased]

### Changed
- 🏗️ **Tổ chức lại cấu trúc thư mục Forms**
  - Di chuyển 34 forms vào 8 sub-folders theo chức năng: Main, DanhMuc, NghiepVu, DanhSach, TimKiem, BaoCao, In, CauHinh
  - Cập nhật tất cả đường dẫn trong `.csproj`
- 📝 Viết lại README.md với cấu trúc chuyên nghiệp
- 🧹 Dọn dẹp files dư thừa (UpgradeLog*.htm, .suo)

### Added
- 📄 Thêm CHANGELOG.md
- 📄 Thêm CONTRIBUTING.md
- 📁 Tạo folder `screenshots/` cho hình ảnh demo

---

## [1.0.0] - Initial Release

### Features
- Quản lý danh mục sản phẩm, khách hàng, nhà cung cấp
- Lập phiếu nhập hàng với tính giá nhập trung bình
- Bán lẻ cho khách hàng cá nhân
- Bán sỉ cho đại lý (có công nợ)
- Quản lý phiếu thu/chi
- Thanh toán công nợ
- Báo cáo tồn kho, doanh thu, công nợ
- Quản lý khuyến mãi
- Phân quyền người dùng
