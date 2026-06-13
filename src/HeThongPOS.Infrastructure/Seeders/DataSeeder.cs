using System;
using System.Collections.Generic;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.Infrastructure.Seeders;

public static class DataSeeder
{
    public static List<KhachHang> GetSampleCustomers()
    {
        var customers = new List<KhachHang>();
        for (int i = 1; i <= 20; i++)
        {
            customers.Add(new KhachHang
            {
                Id = i,
                HoTen = $"Khách hàng {i}",
                SoDienThoai = $"090{i.ToString("D7")}",
                Email = $"khachhang{i}@example.com",
                DiemTichLuy = new Random().Next(0, 500),
                NgayTao = DateTime.Now.AddDays(-i)
            });
        }
        return customers;
    }

    public static List<SanPham> GetSampleProducts()
    {
        var products = new List<SanPham>();
        var random = new Random();
        for (int i = 1; i <= 50; i++)
        {
            products.Add(new SanPham
            {
                Id = i,
                MaVach = $"893{i.ToString("D10")}",
                TenSanPham = $"Sản phẩm {i}",
                GiaBan = random.Next(10, 500) * 1000,
                TonKho = random.Next(10, 100),
                DonViTinh = i % 2 == 0 ? "Cái" : "Hộp",
                TrangThai = true,
                DanhMucId = random.Next(1, 5) // Assuming 4 categories exist as per AppDbContext
            });
        }
        return products;
    }

    public static List<DonHang> GetSampleOrders()
    {
        var orders = new List<DonHang>();
        var random = new Random();
        for (int i = 1; i <= 10; i++)
        {
            var date = DateTime.Now.AddDays(-random.Next(0, 30));
            orders.Add(new DonHang
            {
                Id = i,
                MaDonHang = $"DH{date:yyyyMMdd}{i.ToString("D4")}",
                NhanVienId = 1, // Assume employee 1 exists
                KhachHangId = random.Next(1, 20),
                NgayTao = date,
                TongTienHang = 0, // Will be set in DB manually or calculated
                ChietKhau = 0,
                ThueVAT = 0,
                TongThanhToan = random.Next(50, 500) * 1000,
                TrangThai = (HeThongPOS.Core.Enums.OrderStatus)random.Next(1, 4),
                GhiChu = $"Đơn hàng mẫu {i}"
            });
        }
        return orders;
    }

    public static List<ChiTietDonHang> GetSampleOrderDetails()
    {
        var details = new List<ChiTietDonHang>();
        var random = new Random();
        int detailId = 1;
        for (int orderId = 1; orderId <= 10; orderId++)
        {
            int numItems = random.Next(1, 5);
            var usedProductIds = new HashSet<int>();
            for (int j = 0; j < numItems; j++)
            {
                int productId;
                do { productId = random.Next(1, 50); } while (usedProductIds.Contains(productId));
                usedProductIds.Add(productId);

                details.Add(new ChiTietDonHang
                {
                    DonHangId = orderId,
                    SanPhamId = productId,
                    SoLuong = random.Next(1, 5),
                    DonGia = random.Next(10, 200) * 1000,
                    ThanhTien = 0 // Calculated field
                });
            }
        }
        return details;
    }

    public static List<PhuongThucThanhToan> GetSamplePaymentMethods()
    {
        return new List<PhuongThucThanhToan>
        {
            new PhuongThucThanhToan { Id = 1, TenPhuongThuc = "Tiền mặt", TrangThai = true },
            new PhuongThucThanhToan { Id = 2, TenPhuongThuc = "Thẻ ngân hàng", TrangThai = true },
            new PhuongThucThanhToan { Id = 3, TenPhuongThuc = "Chuyển khoản", TrangThai = true }
        };
    }

    public static List<GiaoDich> GetSampleTransactions()
    {
        return new List<GiaoDich>
        {
            new GiaoDich { Id = 1, DonHangId = 1, PhuongThucThanhToanId = 1, SoTien = 126000m, TrangThai = "SUCCESS", NgayGiaoDich = DateTime.Now.AddDays(-29) },
            new GiaoDich { Id = 2, DonHangId = 2, PhuongThucThanhToanId = 1, SoTien = 482000m, TrangThai = "SUCCESS", NgayGiaoDich = DateTime.Now.AddDays(-15) },
            new GiaoDich { Id = 3, DonHangId = 3, PhuongThucThanhToanId = 2, SoTien = 214000m, TrangThai = "SUCCESS", NgayGiaoDich = DateTime.Now.AddDays(-1) },
            new GiaoDich { Id = 4, DonHangId = 4, PhuongThucThanhToanId = 1, SoTien = 488000m, TrangThai = "SUCCESS", NgayGiaoDich = DateTime.Now.AddDays(-6) },
            new GiaoDich { Id = 5, DonHangId = 5, PhuongThucThanhToanId = 2, SoTien = 463000m, TrangThai = "SUCCESS", NgayGiaoDich = DateTime.Now.AddDays(-22) },
            new GiaoDich { Id = 6, DonHangId = 6, PhuongThucThanhToanId = 1, SoTien = 204000m, TrangThai = "SUCCESS", NgayGiaoDich = DateTime.Now.AddDays(-27) },
            new GiaoDich { Id = 7, DonHangId = 7, PhuongThucThanhToanId = 3, SoTien = 417000m, TrangThai = "SUCCESS", NgayGiaoDich = DateTime.Now.AddDays(-15) },
            new GiaoDich { Id = 8, DonHangId = 8, PhuongThucThanhToanId = 2, SoTien = 237000m, TrangThai = "SUCCESS", NgayGiaoDich = DateTime.Now.AddDays(-28) },
            new GiaoDich { Id = 9, DonHangId = 9, PhuongThucThanhToanId = 1, SoTien = 482000m, TrangThai = "SUCCESS", NgayGiaoDich = DateTime.Now.AddDays(-24) },
            new GiaoDich { Id = 10, DonHangId = 10, PhuongThucThanhToanId = 1, SoTien = 67000m, TrangThai = "SUCCESS", NgayGiaoDich = DateTime.Now.AddDays(-7) }
        };
    }
}
