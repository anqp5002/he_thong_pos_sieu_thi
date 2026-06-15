using CommunityToolkit.Mvvm.ComponentModel;

namespace HeThongPOS.Core.Models;

/// <summary>
/// Model đại diện cho 1 dòng sản phẩm trong giỏ hàng (không phải Entity DB).
/// </summary>
public partial class CartItem : ObservableObject
{
    public int SanPhamId { get; set; }
    public string MaVach { get; set; } = string.Empty;
    public string TenSanPham { get; set; } = string.Empty;
    public decimal DonGia { get; set; }

    [ObservableProperty]
    private int _soLuong = 1;

    public decimal ThanhTien => DonGia * SoLuong;

    partial void OnSoLuongChanged(int value)
    {
        OnPropertyChanged(nameof(ThanhTien));
    }
}
