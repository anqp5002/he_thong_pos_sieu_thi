using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Tự động tạo DB nếu chưa có và chạy migrations
        await context.Database.MigrateAsync();

        // 1. Seed VaiTro
        if (!await context.VaiTros.AnyAsync())
        {
            var vaiTros = new[]
            {
                new VaiTro { TenVaiTro = "Admin", MoTa = "Quản trị viên hệ thống" },
                new VaiTro { TenVaiTro = "Cashier", MoTa = "Nhân viên thu ngân" }
            };
            await context.VaiTros.AddRangeAsync(vaiTros);
            await context.SaveChangesAsync();
        }

        // 2. Seed NhanVien
        if (!await context.NhanViens.AnyAsync())
        {
            var adminRole = await context.VaiTros.FirstOrDefaultAsync(v => v.TenVaiTro == "Admin");
            var cashierRole = await context.VaiTros.FirstOrDefaultAsync(v => v.TenVaiTro == "Cashier");

            if (adminRole != null && cashierRole != null)
            {
                var nhanViens = new[]
                {
                    new NhanVien 
                    { 
                        Username = "admin", 
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), 
                        HoTen = "Quản Trị Viên",
                        VaiTroId = adminRole.Id,
                        TrangThai = true,
                        NgayTao = DateTime.Now
                    },
                    new NhanVien 
                    { 
                        Username = "cashier", 
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("cashier123"), 
                        HoTen = "Thu Ngân 1",
                        VaiTroId = cashierRole.Id,
                        TrangThai = true,
                        NgayTao = DateTime.Now
                    }
                };
                await context.NhanViens.AddRangeAsync(nhanViens);
                await context.SaveChangesAsync();
            }
        }
    }
}
