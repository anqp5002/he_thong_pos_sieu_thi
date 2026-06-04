using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.Infrastructure.Data.Configurations;

public class ChiTietDonHangConfig : IEntityTypeConfiguration<ChiTietDonHang>
{
    public void Configure(EntityTypeBuilder<ChiTietDonHang> builder)
    {
        builder.ToTable("CHI_TIET_DON_HANG");

        builder.HasKey(c => new { c.DonHangId, c.SanPhamId });

        builder.Property(c => c.DonGia)
               .HasColumnType("decimal(18,2)");

        builder.Property(c => c.ThanhTien)
               .HasColumnType("decimal(18,2)");
    }
}
