using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.Infrastructure.Data.Configurations;

public class SanPhamConfig : IEntityTypeConfiguration<SanPham>
{
    public void Configure(EntityTypeBuilder<SanPham> builder)
    {
        builder.ToTable("SAN_PHAM");

        builder.HasIndex(s => s.MaVach).IsUnique();

        builder.Property(s => s.GiaBan).HasColumnType("decimal(18,2)");

        builder.HasOne(s => s.DanhMuc)
               .WithMany(d => d.SanPhams)
               .HasForeignKey(s => s.DanhMucId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
