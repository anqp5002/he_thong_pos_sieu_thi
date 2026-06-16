using System;

namespace HeThongPOS.Core.Entities;

public class SanPham
{
    public int Id { get; set; }
    public string MaVach { get; set; } = string.Empty;
    public string TenSanPham { get; set; } = string.Empty;
    public decimal GiaBan { get; set; }
    public int DanhMucId { get; set; }
    public int TonKho { get; set; }
    public string DonViTinh { get; set; } = string.Empty;
    public bool TrangThai { get; set; } = true;
    public DateTime NgayTao { get; set; } = DateTime.Now;

    public DanhMuc? DanhMuc { get; set; }
    public string HinhAnhUrl { get; set; } = string.Empty;
}
