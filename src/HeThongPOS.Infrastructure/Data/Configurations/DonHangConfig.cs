using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.Infrastructure.Data.Configurations;

public class DonHangConfig : IEntityTypeConfiguration<DonHang>
{
    public void Configure(EntityTypeBuilder<DonHang> builder)
    {
        builder.ToTable("DON_HANG");

        builder.Property(d => d.TongTienHang).HasColumnType("decimal(18,2)");
        builder.Property(d => d.ChietKhau).HasColumnType("decimal(18,2)");
        builder.Property(d => d.ThueVAT).HasColumnType("decimal(18,2)");
        builder.Property(d => d.TongThanhToan).HasColumnType("decimal(18,2)");
        
        builder.HasOne(d => d.NhanVien)
               .WithMany(n => n.DonHangs)
               .HasForeignKey(d => d.NhanVienId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.KhachHang)
               .WithMany(k => k.DonHangs)
               .HasForeignKey(d => d.KhachHangId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
