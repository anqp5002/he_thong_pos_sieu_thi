using System;

namespace HeThongPOS.Core.Entities;

public class GiaoDich
{
    public int Id { get; set; }
    public int DonHangId { get; set; }
    public int PhuongThucThanhToanId { get; set; }
    public decimal SoTien { get; set; }
    public DateTime NgayGiaoDich { get; set; } = DateTime.Now;
    public string TrangThai { get; set; } = "SUCCESS"; // SUCCESS, FAILED
    public string MaGiaoDichDoiTac { get; set; } = string.Empty; // Mã tham chiếu VNPay/Momo nếu có

    public DonHang DonHang { get; set; } = null!;
    public PhuongThucThanhToan PhuongThucThanhToan { get; set; } = null!;
}
