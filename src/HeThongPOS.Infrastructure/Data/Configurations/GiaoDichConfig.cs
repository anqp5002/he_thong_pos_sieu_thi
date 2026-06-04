using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.Infrastructure.Data.Configurations;

public class GiaoDichConfig : IEntityTypeConfiguration<GiaoDich>
{
    public void Configure(EntityTypeBuilder<GiaoDich> builder)
    {
        builder.ToTable("GIAO_DICH");

        builder.Property(g => g.SoTien).HasColumnType("decimal(18,2)");

        builder.HasOne(g => g.DonHang)
               .WithMany(d => d.GiaoDichs)
               .HasForeignKey(g => g.DonHangId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(g => g.PhuongThucThanhToan)
               .WithMany(p => p.GiaoDichs)
               .HasForeignKey(g => g.PhuongThucThanhToanId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
