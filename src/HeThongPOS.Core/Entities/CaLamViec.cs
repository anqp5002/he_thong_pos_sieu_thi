using System;

namespace HeThongPOS.Core.Entities;

public class CaLamViec
{
    public int Id { get; set; }
    public int NhanVienId { get; set; }
    public DateTime ThoiGianBatDau { get; set; } = DateTime.Now;
    public DateTime? ThoiGianKetThuc { get; set; }
    public decimal SoDuDauCa { get; set; }
    public decimal? SoDuCuoiCaThucTe { get; set; }
    public decimal? TongDoanhThu { get; set; }
    public decimal? ChenhLech { get; set; }
    public string GhiChu { get; set; } = string.Empty;
    public string TrangThai { get; set; } = "OPEN"; // OPEN, CLOSED

    public NhanVien NhanVien { get; set; } = null!;
}
