# Contributing to CHND

Cảm ơn bạn đã quan tâm đến việc đóng góp cho dự án! 🎉

## 🚀 Cách Đóng Góp

### 1. Fork & Clone

```bash
# Fork repo trên GitHub, sau đó clone về máy
git clone https://github.com/YOUR_USERNAME/CuahangNongDuoc.git
cd CuahangNongDuoc
```

### 2. Tạo Branch

```bash
git checkout -b feature/ten-tinh-nang
# hoặc
git checkout -b fix/ten-bug
```

### 3. Commit Changes

```bash
git add .
git commit -m "feat: Thêm tính năng XYZ"
```

**Commit message conventions:**
- `feat:` - Tính năng mới
- `fix:` - Sửa bug
- `docs:` - Cập nhật documentation
- `refactor:` - Refactor code (không thêm tính năng, không sửa bug)
- `style:` - Format code, thêm comments
- `test:` - Thêm tests

### 4. Push & Create Pull Request

```bash
git push origin feature/ten-tinh-nang
```

Sau đó tạo Pull Request trên GitHub.

---

## 📁 Cấu Trúc Code

| Folder | Mô tả |
|--------|-------|
| `Forms/` | UI Layer - Windows Forms |
| `BusinessObject/` | Entity Layer - Data models |
| `Controller/` | Business Logic Layer |
| `DataLayer/` | Data Access Layer |
| `Strategy/` | Strategy Pattern implementations |
| `Decorator/` | Decorator Pattern implementations |
| `Specification/` | Specification Pattern implementations |

---

## 🧪 Testing

Trước khi submit PR, đảm bảo:

1. ✅ Build thành công (`Ctrl+Shift+B`)
2. ✅ Test các chức năng liên quan
3. ✅ Không có lỗi runtime

---

## 📝 Code Style

- Đặt tên biến, hàm theo tiếng Việt không dấu hoặc tiếng Anh
- Prefix forms với `frm` (ví dụ: `frmBanLe`)
- Comment code quan trọng
- Tách logic phức tạp thành các hàm nhỏ

---

## ❓ Có Câu Hỏi?

Tạo Issue trên GitHub hoặc liên hệ maintainer.
