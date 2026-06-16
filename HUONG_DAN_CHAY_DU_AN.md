# Hướng dẫn chạy dự án Hệ Thống POS Siêu Thị (Desktop WPF)

Dự án này sử dụng kiến trúc WPF với .NET 10, Entity Framework Core (Code-First) và cơ sở dữ liệu SQL Server.

## 1. Yêu cầu hệ thống
- **.NET 10 SDK** trở lên.
- **SQL Server** (hoặc SQL Server Express/Developer Edition).
- **Visual Studio 2022** (khuyến nghị) hoặc **Visual Studio Code** với extension C#.

## 2. Cấu hình Cơ sở dữ liệu
1. Mở thư mục: `src/HeThongPOS.WPF/`
2. Đổi tên file `appsettings.example.json` thành `appsettings.json`.
3. Mở file `appsettings.json` và cập nhật chuỗi kết nối (`DefaultConnection`) cho phù hợp với SQL Server của bạn. 
   - Ví dụ: Thay đổi `Server=YOUR_SERVER_NAME` thành `Server=localhost` hoặc `Server=.\SQLEXPRESS`.
   - Cập nhật `User Id` và `Password` nếu có dùng SQL Authentication, hoặc chuyển sang dùng `Integrated Security=True`.

*Ví dụ cấu hình appsettings.json:*
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HeThongPOS;Integrated Security=True;TrustServerCertificate=true;"
  },
  "StockAlert": {
    "MinimumStock": 10
  }
}
```

## 3. Khởi tạo Cơ sở dữ liệu & Chạy ứng dụng

Bạn có thể chạy dự án thông qua Terminal/Command Prompt hoặc bằng Visual Studio.

### Cách 1: Dùng Terminal (.NET CLI)
Mở Terminal ở thư mục gốc của dự án (`he_thong_pos_sieu_thi`) và chạy lệnh sau:
```bash
# Di chuyển vào thư mục dự án WPF
cd src/HeThongPOS.WPF

# Build và chạy dự án
dotnet run
```
*Lưu ý: Hệ thống sử dụng EF Core Code-First. Khi ứng dụng chạy lần đầu tiên, file `DataSeeder.cs` sẽ tự động tạo Database, áp dụng Migrations và thêm các tài khoản mồi (Admin/Cashier).*

### Cách 2: Dùng Visual Studio
1. Mở file solution `HeThongPOS.slnx` (hoặc `.sln`) bằng Visual Studio.
2. Đặt `HeThongPOS.WPF` làm **Startup Project** (Chuột phải vào project -> Set as Startup Project).
3. Bấm **F5** hoặc **Ctrl+F5** để build và chạy ứng dụng.

## 4. Tài khoản Đăng nhập (Mặc định)

Sau khi giao diện khởi động, bạn có thể sử dụng các tài khoản có sẵn dưới đây để đăng nhập:

| Chức vụ | Username | Password | Quyền hạn |
| :--- | :--- | :--- | :--- |
| **Quản trị viên** | `admin` | `admin123` | Toàn quyền (Thấy tất cả Menu) |
| **Thu ngân** | `cashier` | `cashier123` | Bán hàng (Chỉ thấy POS & Ca làm việc) |

---
**Chúc bạn chạy dự án thành công! 🎉**
