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
    }
}
