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
}
