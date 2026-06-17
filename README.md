# Hệ Thống POS Siêu Thị — Desktop App

> **Nhóm 6** — PTIT HCM — Thực tập Cơ sở  
> Hệ thống Quản lý Bán hàng tại Quầy (Point of Sale) — Phiên bản Desktop

## Tổng Quan

Ứng dụng Desktop POS dành cho siêu thị mini/cửa hàng tiện lợi.  
Hỗ trợ: quét mã vạch, quản lý giỏ hàng, thanh toán, in hóa đơn, báo cáo doanh thu.

## Công Nghệ

| Layer | Công nghệ |
|-------|-----------|
| Platform | .NET 8 (LTS) |
| Ngôn ngữ | C# 12 |
| UI Framework | WPF (XAML + MVVM) |
| MVVM Toolkit | CommunityToolkit.Mvvm |
| ORM | Entity Framework Core 8 |
| Database | SQL Server 2022 (Docker) |
| Charts | LiveCharts2 |
| DI | Microsoft.Extensions.DependencyInjection |
| Testing | xUnit + Moq |

## Cách Chạy

### Yêu cầu
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### Bước 1: Clone & khởi động DB
```bash
git clone https://github.com/anqp5002/he_thong_pos_sieu_thi.git
cd he_thong_pos_sieu_thi
cp .env.example .env

# Khởi động SQL Server
docker compose up -d
```

### Bước 2: Chạy Migration
```bash
cd src/HeThongPOS.Infrastructure
dotnet ef database update --startup-project ../HeThongPOS.WPF
```

### Bước 3: Chạy ứng dụng
```bash
cd src/HeThongPOS.WPF
dotnet run
```

## Cấu Trúc Dự Án

```
HeThongPOS.sln
├── src/
│   ├── HeThongPOS.Core/            # Entities, Interfaces, Enums
│   ├── HeThongPOS.Infrastructure/  # EF Core, Repositories, Seeders
│   ├── HeThongPOS.Application/     # Business Logic (Services)
│   └── HeThongPOS.WPF/            # Desktop UI (Views, ViewModels)
├── tests/
│   └── HeThongPOS.UnitTests/       # xUnit + Moq
├── docker-compose.yml              # SQL Server 2022
├── .env.example                    # Biến môi trường (DB, PayOS)
└── README.md
```

## 💳 Cấu hình Thanh toán Tự động (PayOS)

Dự án đã được tích hợp sẵn mã nguồn kết nối **PayOS** (`src/HeThongPOS.Application/Services/PayOSService.cs`) trường hợp bạn muốn sử dụng tính năng **tự động chốt đơn** khi khách chuyển khoản thành công.

**Cách kích hoạt:**
1. Đăng ký tài khoản trên [payos.vn](https://payos.vn/) và lấy 3 mã: `Client ID`, `API Key`, `Checksum Key`.
2. Mở file `.env` (copy từ `.env.example`) và điền 3 mã này vào phần `PAYOS_*`.
3. Trong `PaymentViewModel.cs`, khi khách chọn "Chuyển khoản / Thẻ", hãy gọi `PayOSService.CreatePaymentLink()` để tạo mã.
4. Sử dụng `DispatcherTimer` của WPF lặp lại 3 giây/lần để gọi hàm `PayOSService.CheckPaymentStatus()`. Nếu trả về `true` (PAID), tự động gọi hàm `CheckoutAsync()` để hoàn tất đơn hàng mà thu ngân không cần bấm chuột.

## Nhóm 6

| Thành viên | Vai trò |
|-----------|---------|
| Pham Quốc An | Auth, POS UI, Payment UI, Dashboard, Shift |
| Nguyễn Trường Hiếu | Product CRUD, Order Service, Payment Service, Reports, Testing |
| Hồ Văn Đức | Customer CRUD, Order List, Invoice/Print, Deploy, Polish |

## License

Dự án học thuật — PTIT HCM © 2026
