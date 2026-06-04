using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.Infrastructure.Data.Configurations;

public class NhanVienConfig : IEntityTypeConfiguration<NhanVien>
{
    public void Configure(EntityTypeBuilder<NhanVien> builder)
    {
        builder.ToTable("NHAN_VIEN");

        builder.HasIndex(n => n.Username).IsUnique();

        builder.HasOne(n => n.VaiTro)
               .WithMany(v => v.NhanViens)
               .HasForeignKey(n => n.VaiTroId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
