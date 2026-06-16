using System.Collections.Generic;

namespace HeThongPOS.WPF.ViewModels;

/// <summary>
/// ViewModel cho cửa sổ in hóa đơn (ReceiptDialog).
/// </summary>
public class ReceiptViewModel
{
    public string MaDonHang { get; set; } = string.Empty;
    public string NgayTao { get; set; } = string.Empty;
    public string TenNhanVien { get; set; } = "Thu ngân";
    public string TenKhachHang { get; set; } = "Khách lẻ";
    public List<ReceiptItem> Items { get; set; } = new();
    public decimal TongTienHang { get; set; }
    public decimal ThueVAT { get; set; }
    public decimal TongThanhToan { get; set; }
    public string PhuongThuc { get; set; } = "Tiền mặt";
    public decimal KhachDua { get; set; }
    public decimal TienThoi { get; set; }
}

public class ReceiptItem
{
    public string TenSanPham { get; set; } = string.Empty;
    public int SoLuong { get; set; }
    public decimal DonGia { get; set; }
    public decimal ThanhTien { get; set; }
}
