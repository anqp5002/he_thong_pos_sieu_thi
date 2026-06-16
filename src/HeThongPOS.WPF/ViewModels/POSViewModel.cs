using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Models;
using HeThongPOS.Infrastructure.Data;
using HeThongPOS.WPF.Controls;

namespace HeThongPOS.WPF.ViewModels;

public partial class POSViewModel : ObservableObject
{
    private readonly AppDbContext _context;

    [ObservableProperty]
    private string _barcodeInput = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private ObservableCollection<SanPham> _danhSachSanPham = new();

    [ObservableProperty]
    private ObservableCollection<CartItem> _gioHang = new();

    public decimal TongTienHang => GioHang.Sum(x => x.ThanhTien);
    public decimal ThueVAT => TongTienHang * 0.08m;
    public decimal TongThanhToan => TongTienHang + ThueVAT;
    public int TongSoLuong => GioHang.Sum(x => x.SoLuong);

    private readonly IServiceProvider _serviceProvider;

    private List<SanPham> _allSanPhams = new();

    public POSViewModel(AppDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
        _ = LoadSanPhamAsync();
    }

    partial void OnBarcodeInputChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            DanhSachSanPham = new ObservableCollection<SanPham>(_allSanPhams);
            return;
        }

        var keyword = value.ToLower();
        var filtered = _allSanPhams.Where(s => 
            s.TenSanPham.ToLower().Contains(keyword) || 
            s.MaVach.ToLower().Contains(keyword));
        
        DanhSachSanPham = new ObservableCollection<SanPham>(filtered);
    }

    /// <summary>
    /// Tải danh sách sản phẩm đang hoạt động từ database.
    /// </summary>
    [RelayCommand]
    private async Task LoadSanPhamAsync()
    {
        _allSanPhams = await _context.SanPhams
            .Where(s => s.TrangThai && s.TonKho > 0)
            .OrderBy(s => s.TenSanPham)
            .ToListAsync();

        DanhSachSanPham = new ObservableCollection<SanPham>(_allSanPhams);
    }

    /// <summary>
    /// Tìm sản phẩm theo mã vạch (quét scanner USB hoặc nhập tay) rồi thêm vào giỏ.
    /// </summary>
    [RelayCommand]
    private async Task SearchBarcodeAsync()
    {
        if (string.IsNullOrWhiteSpace(BarcodeInput))
            return;

        var sanPham = await _context.SanPhams
            .FirstOrDefaultAsync(s => s.MaVach == BarcodeInput.Trim() && s.TrangThai);

        if (sanPham == null)
        {
            ErrorMessage = $"Không tìm thấy sản phẩm với mã vạch: {BarcodeInput}";
            BarcodeInput = string.Empty;
            return;
        }

        ErrorMessage = string.Empty;
        ThemVaoGio(sanPham);
        BarcodeInput = string.Empty;
    }

    /// <summary>
    /// Thêm sản phẩm vào giỏ hàng khi bấm vào ProductCard.
    /// </summary>
    [RelayCommand]
    private void ThemSanPham(SanPham? sanPham)
    {
        if (sanPham == null) return;
        ThemVaoGio(sanPham);
    }

    /// <summary>
    /// Logic thêm: nếu SP đã có trong giỏ thì tăng SL, chưa có thì thêm mới.
    /// </summary>
    private void ThemVaoGio(SanPham sanPham)
    {
        var existing = GioHang.FirstOrDefault(c => c.SanPhamId == sanPham.Id);

        if (existing != null)
        {
            if (existing.SoLuong < sanPham.TonKho)
            {
                existing.SoLuong++;
            }
            else
            {
                ErrorMessage = $"Không đủ tồn kho cho \"{sanPham.TenSanPham}\" (còn {sanPham.TonKho}).";
                return;
            }
        }
        else
        {
            GioHang.Add(new CartItem
            {
                SanPhamId = sanPham.Id,
                MaVach = sanPham.MaVach,
                TenSanPham = sanPham.TenSanPham,
                DonGia = sanPham.GiaBan,
                SoLuong = 1
            });
        }

        ErrorMessage = string.Empty;
        CapNhatTongTien();
    }

    /// <summary>
    /// Tăng số lượng 1 item trong giỏ.
    /// </summary>
    [RelayCommand]
    private void TangSoLuong(CartItem? item)
    {
        if (item == null) return;

        var sp = DanhSachSanPham.FirstOrDefault(s => s.Id == item.SanPhamId);
        if (sp != null && item.SoLuong < sp.TonKho)
        {
            item.SoLuong++;
            CapNhatTongTien();
        }
    }

    /// <summary>
    /// Giảm số lượng 1 item trong giỏ. Nếu về 0 thì xóa khỏi giỏ.
    /// </summary>
    [RelayCommand]
    private void GiamSoLuong(CartItem? item)
    {
        if (item == null) return;

        if (item.SoLuong > 1)
        {
            item.SoLuong--;
        }
        else
        {
            GioHang.Remove(item);
        }

        CapNhatTongTien();
    }

    /// <summary>
    /// Xóa 1 dòng sản phẩm khỏi giỏ.
    /// </summary>
    [RelayCommand]
    private void XoaItem(CartItem? item)
    {
        if (item == null) return;
        GioHang.Remove(item);
        CapNhatTongTien();
    }

    /// <summary>
    /// Xóa toàn bộ giỏ hàng.
    /// </summary>
    [RelayCommand]
    private void XoaHet()
    {
        GioHang.Clear();
        CapNhatTongTien();
    }

    /// <summary>
    /// Thông báo cho UI cập nhật các dòng tổng tiền.
    /// </summary>
    private void CapNhatTongTien()
    {
        OnPropertyChanged(nameof(TongTienHang));
        OnPropertyChanged(nameof(ThueVAT));
        OnPropertyChanged(nameof(TongThanhToan));
        OnPropertyChanged(nameof(TongSoLuong));
    }

    [RelayCommand]
    private void OpenPayment()
    {
        if (GioHang.Count == 0)
        {
            ErrorMessage = "Giỏ hàng đang trống, không thể thanh toán.";
            return;
        }

        var paymentViewModel = _serviceProvider.GetRequiredService<PaymentViewModel>();
        paymentViewModel.Initialize(TongThanhToan);

        var paymentDialog = _serviceProvider.GetRequiredService<PaymentDialog>();
        paymentDialog.DataContext = paymentViewModel;
        
        bool? result = paymentDialog.ShowDialog();
        
        if (result == true)
        {
            // Thanh toán thành công, xóa giỏ hàng
            GioHang.Clear();
            CapNhatTongTien();
            ErrorMessage = "Thanh toán thành công!";
        }
    }
}
