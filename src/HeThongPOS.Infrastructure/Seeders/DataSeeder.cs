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
}
