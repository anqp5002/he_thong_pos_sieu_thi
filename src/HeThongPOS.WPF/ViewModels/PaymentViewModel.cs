using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Enums;
using HeThongPOS.Core.Models;
using HeThongPOS.Infrastructure.Data;

namespace HeThongPOS.WPF.ViewModels;

public partial class PaymentViewModel : ObservableObject
{
    private readonly AppDbContext _context;

    [ObservableProperty]
    private decimal _tongTienHang;

    [ObservableProperty]
    private decimal _tongThanhToan;

    [ObservableProperty]
    private decimal _khachDua;

    [ObservableProperty]
    private string _phuongThucThanhToan = "CASH"; // CASH, CARD

    [ObservableProperty]
    private string _searchCustomerKeyword = string.Empty;

    [ObservableProperty]
    private KhachHang? _selectedCustomer;

    [ObservableProperty]
    private ObservableCollection<KhachHang> _suggestedCustomers = new();

    public decimal ThueVAT => TongTienHang * 0.08m;
    public decimal TienThoi => KhachDua >= TongThanhToan ? KhachDua - TongThanhToan : 0;
    
    public bool CanCheckout => KhachDua >= TongThanhToan || PhuongThucThanhToan != "CASH";

    // Data truyền từ POS
    public List<CartItem> CartItems { get; set; } = new();
    public int NhanVienId { get; set; }
    public string TenNhanVien { get; set; } = "Thu ngân";

    // Kết quả trả về sau thanh toán
    public ReceiptViewModel? ReceiptData { get; private set; }

    public Action? OnPaymentSuccess;
    public Action? OnRequestClose;

    public PaymentViewModel(AppDbContext context)
    {
        _context = context;
    }

    public void Initialize(decimal tongTienHang, decimal tongThanhToan, List<CartItem> cartItems, int nhanVienId, string tenNhanVien)
    {
        TongTienHang = tongTienHang;
        TongThanhToan = tongThanhToan;
        CartItems = cartItems;
        NhanVienId = nhanVienId;
        TenNhanVien = tenNhanVien;
        KhachDua = 0;
        PhuongThucThanhToan = "CASH";
        SelectedCustomer = null;
        SearchCustomerKeyword = string.Empty;
        SuggestedCustomers.Clear();
        ReceiptData = null;
        UpdateTienThoi();
    }

    partial void OnSearchCustomerKeywordChanged(string value)
    {
        // Chỉ search khi chưa chọn customer (tránh loop khi set text sau khi chọn)
        if (SelectedCustomer == null)
            _ = SearchCustomerAsync();
    }

    partial void OnKhachDuaChanged(decimal value)
    {
        UpdateTienThoi();
    }

    partial void OnPhuongThucThanhToanChanged(string value)
    {
        UpdateTienThoi();
    }

    private void UpdateTienThoi()
    {
        OnPropertyChanged(nameof(TienThoi));
        OnPropertyChanged(nameof(CanCheckout));
        CheckoutCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand]
    private async Task SearchCustomerAsync()
    {
        if (string.IsNullOrWhiteSpace(SearchCustomerKeyword))
        {
            SuggestedCustomers.Clear();
            return;
        }

        var keyword = SearchCustomerKeyword.ToLower().Trim();
        var customers = await _context.KhachHangs
            .Where(c => c.SoDienThoai.Contains(keyword) || c.HoTen.ToLower().Contains(keyword))
            .Take(5)
            .ToListAsync();

        SuggestedCustomers = new ObservableCollection<KhachHang>(customers);
    }

    [RelayCommand]
    private void SelectCustomer(KhachHang? customer)
    {
        SelectedCustomer = customer;
        if (customer != null)
        {
            SearchCustomerKeyword = $"{customer.HoTen} ({customer.SoDienThoai})";
            SuggestedCustomers.Clear();
        }
    }

    [RelayCommand]
    private void ClearCustomer()
    {
        SelectedCustomer = null;
        SearchCustomerKeyword = string.Empty;
        SuggestedCustomers.Clear();
    }

    [RelayCommand]
    private void SetQuickAmount(string amountStr)
    {
        if (decimal.TryParse(amountStr, out decimal amount))
        {
            KhachDua = amount;
        }
        else if (amountStr == "EXACT")
        {
            KhachDua = TongThanhToan;
        }
    }

    [RelayCommand(CanExecute = nameof(CanCheckout))]
    private async Task CheckoutAsync()
    {
        if (!CanCheckout) return;

        try
        {
            // 1. Tạo mã đơn hàng
            string maDonHang = $"DH{DateTime.Now:yyyyMMddHHmmss}";

            // 2. Tạo đơn hàng
            var donHang = new DonHang
            {
                MaDonHang = maDonHang,
                NhanVienId = NhanVienId,
                KhachHangId = SelectedCustomer?.Id,
                NgayTao = DateTime.Now,
                TongTienHang = TongTienHang,
                ChietKhau = 0,
                ThueVAT = ThueVAT,
                TongThanhToan = TongThanhToan,
                TrangThai = OrderStatus.Completed,
                GhiChu = PhuongThucThanhToan == "CASH" ? "Thanh toán tiền mặt" : "Thanh toán chuyển khoản/thẻ"
            };

            _context.DonHangs.Add(donHang);
            await _context.SaveChangesAsync();

            // 3. Tạo chi tiết đơn hàng
            foreach (var item in CartItems)
            {
                var chiTiet = new ChiTietDonHang
                {
                    DonHangId = donHang.Id,
                    SanPhamId = item.SanPhamId,
                    SoLuong = item.SoLuong,
                    DonGia = item.DonGia,
                    ThanhTien = item.ThanhTien
                };
                _context.ChiTietDonHangs.Add(chiTiet);

                // 4. Giảm tồn kho
                var sanPham = await _context.SanPhams.FindAsync(item.SanPhamId);
                if (sanPham != null)
                {
                    sanPham.TonKho -= item.SoLuong;
                }
            }

            await _context.SaveChangesAsync();

            // 5. Tạo dữ liệu hóa đơn
            ReceiptData = new ReceiptViewModel
            {
                MaDonHang = maDonHang,
                NgayTao = DateTime.Now.ToString("HH:mm dd/MM/yyyy"),
                TenNhanVien = TenNhanVien,
                TenKhachHang = SelectedCustomer?.HoTen ?? "Khách lẻ",
                TongTienHang = TongTienHang,
                ThueVAT = ThueVAT,
                TongThanhToan = TongThanhToan,
                PhuongThuc = PhuongThucThanhToan == "CASH" ? "Tiền mặt" : "Chuyển khoản / Thẻ",
                KhachDua = KhachDua,
                TienThoi = TienThoi,
                Items = CartItems.Select(c => new ReceiptItem
                {
                    TenSanPham = c.TenSanPham,
                    SoLuong = c.SoLuong,
                    DonGia = c.DonGia,
                    ThanhTien = c.ThanhTien
                }).ToList()
            };

            // 6. Báo thanh toán thành công
            OnPaymentSuccess?.Invoke();
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show($"Lỗi khi lưu đơn hàng: {ex.Message}\n{ex.InnerException?.Message}", 
                "Lỗi Thanh Toán", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        OnRequestClose?.Invoke();
    }
}
