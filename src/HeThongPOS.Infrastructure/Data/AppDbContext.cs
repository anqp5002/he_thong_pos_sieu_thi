using Microsoft.EntityFrameworkCore;
using HeThongPOS.Core.Entities;
using System.Reflection;

namespace HeThongPOS.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<VaiTro> VaiTros { get; set; } = null!;
    public DbSet<NhanVien> NhanViens { get; set; } = null!;
    public DbSet<CaLamViec> CaLamViecs { get; set; } = null!;
    public DbSet<DanhMuc> DanhMucs { get; set; } = null!;
    public DbSet<SanPham> SanPhams { get; set; } = null!;
    public DbSet<KhachHang> KhachHangs { get; set; } = null!;
    public DbSet<DonHang> DonHangs { get; set; } = null!;
    public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; } = null!;
    public DbSet<PhuongThucThanhToan> PhuongThucThanhToans { get; set; } = null!;
    public DbSet<GiaoDich> GiaoDichs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Apply all configurations from the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Dummy Data for testing
        modelBuilder.Entity<DanhMuc>().HasData(
            new DanhMuc { Id = 1, TenDanhMuc = "Đồ ăn nhanh" },
            new DanhMuc { Id = 2, TenDanhMuc = "Đồ uống" },
            new DanhMuc { Id = 3, TenDanhMuc = "Gia vị" },
            new DanhMuc { Id = 4, TenDanhMuc = "Hóa mỹ phẩm" }
        );

        modelBuilder.Entity<PhuongThucThanhToan>().HasData(
            new PhuongThucThanhToan { Id = 1, TenPhuongThuc = "Tiền mặt" },
            new PhuongThucThanhToan { Id = 2, TenPhuongThuc = "Thẻ ngân hàng" },
            new PhuongThucThanhToan { Id = 3, TenPhuongThuc = "Chuyển khoản" }
        );

        modelBuilder.Entity<VaiTro>().HasData(
            new VaiTro { Id = 1, TenVaiTro = "Admin" },
            new VaiTro { Id = 2, TenVaiTro = "Cashier" }
        );

        modelBuilder.Entity<NhanVien>().HasData(
            new NhanVien 
            { 
                Id = 1, 
                HoTen = "Admin Tester", 
                Username = "admin", 
                PasswordHash = "123", // Fake hash for now
                VaiTroId = 1,
                NgayTao = new System.DateTime(2023, 1, 1)
            }
        );

        // Seed KhachHangs
        var khachHangs = new System.Collections.Generic.List<KhachHang>();
        for (int i = 1; i <= 20; i++)
        {
            khachHangs.Add(new KhachHang
            {
                Id = i + 1000,
                HoTen = $"Khách hàng {i}",
                SoDienThoai = $"090{i:D7}",
                NgayTao = new System.DateTime(2023, 1, 1)
            });
        }
        modelBuilder.Entity<KhachHang>().HasData(khachHangs);

        // Seed SanPhams
        var sanPhams = new System.Collections.Generic.List<SanPham>();
        for (int i = 1; i <= 50; i++)
        {
            sanPhams.Add(new SanPham
            {
                Id = i + 1000,
                MaVach = $"893{i:D10}",
                TenSanPham = $"Sản phẩm mẫu {i}",
                GiaBan = 10000m + (i * 1000m),
                TonKho = 100,
                DanhMucId = (i % 4) + 1,
                TrangThai = true,
                NgayTao = new System.DateTime(2023, 1, 1)
            });
        }
        modelBuilder.Entity<SanPham>().HasData(sanPhams);
    }
}
