namespace HeThongPOS.Core.Entities;

public class ChiTietDonHang
{
    public int DonHangId { get; set; }
    public int SanPhamId { get; set; }
    public int SoLuong { get; set; }
    public decimal DonGia { get; set; }
    public decimal ThanhTien { get; set; }

    public DonHang DonHang { get; set; } = null!;
    public SanPham SanPham { get; set; } = null!;
}
