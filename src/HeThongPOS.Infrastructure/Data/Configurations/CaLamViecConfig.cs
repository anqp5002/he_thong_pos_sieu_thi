using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.Infrastructure.Data.Configurations;

public class CaLamViecConfig : IEntityTypeConfiguration<CaLamViec>
{
    public void Configure(EntityTypeBuilder<CaLamViec> builder)
    {
        builder.ToTable("CA_LAM_VIEC");

        builder.Property(c => c.SoDuDauCa).HasColumnType("decimal(18,2)");
        builder.Property(c => c.SoDuCuoiCaThucTe).HasColumnType("decimal(18,2)");
        builder.Property(c => c.TongDoanhThu).HasColumnType("decimal(18,2)");
        builder.Property(c => c.ChenhLech).HasColumnType("decimal(18,2)");

        builder.HasOne(c => c.NhanVien)
               .WithMany(n => n.CaLamViecs)
               .HasForeignKey(c => c.NhanVienId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
