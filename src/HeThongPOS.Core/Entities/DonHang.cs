using System;
using System.Collections.Generic;
using HeThongPOS.Core.Enums;

namespace HeThongPOS.Core.Entities;

public class DonHang
{
    public int Id { get; set; }
    public string MaDonHang { get; set; } = string.Empty;
    public int NhanVienId { get; set; }
    public int? KhachHangId { get; set; }
    public DateTime NgayTao { get; set; } = DateTime.Now;
    public decimal TongTienHang { get; set; }
    public decimal ChietKhau { get; set; }
    public decimal ThueVAT { get; set; }
    public decimal TongThanhToan { get; set; }
    public OrderStatus TrangThai { get; set; } = OrderStatus.Pending;
    public string GhiChu { get; set; } = string.Empty;

    public NhanVien NhanVien { get; set; } = null!;
    public KhachHang? KhachHang { get; set; }
    public ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
    public ICollection<GiaoDich> GiaoDichs { get; set; } = new List<GiaoDich>();
}
