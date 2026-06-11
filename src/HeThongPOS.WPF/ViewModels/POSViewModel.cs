using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Interfaces;

namespace HeThongPOS.WPF.ViewModels;

public partial class POSViewModel : ObservableObject
{
    private readonly IProductService _productService;
    private readonly IOrderService _orderService;

    [ObservableProperty]
    private ObservableCollection<SanPham> _products = new();

    [ObservableProperty]
    private ObservableCollection<ChiTietDonHang> _cartItems = new();

    [ObservableProperty]
    private string _searchKeyword = string.Empty;
    
    [ObservableProperty]
    private string _barcodeInput = string.Empty;

    [ObservableProperty]
    private decimal _tongTienHang;

    [ObservableProperty]
    private decimal _thueVAT;

    [ObservableProperty]
    private decimal _tongThanhToan;

    public POSViewModel(IProductService productService, IOrderService orderService)
    {
        _productService = productService;
        _orderService = orderService;
        LoadProductsCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadProductsAsync()
    {
        try
        {
            var products = await _productService.SearchProductsAsync(SearchKeyword, null);
            Products.Clear();
            foreach (var p in products)
            {
                if (p.TrangThai && p.TonKho > 0)
                {
                    Products.Add(p);
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task ProcessBarcodeAsync()
    {
        if (string.IsNullOrWhiteSpace(BarcodeInput)) return;

        try
        {
            var product = await _productService.GetProductByBarcodeAsync(BarcodeInput);
            if (product != null && product.TrangThai && product.TonKho > 0)
            {
                AddToCart(product);
            }
            else
            {
                MessageBox.Show("Sản phẩm không tồn tại hoặc hết hàng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            BarcodeInput = string.Empty;
        }
    }

    [RelayCommand]
    private void AddToCart(SanPham product)
    {
        if (product == null) return;
        
        var existingItem = CartItems.FirstOrDefault(x => x.SanPhamId == product.Id);
        if (existingItem != null)
        {
            if (existingItem.SoLuong < product.TonKho)
            {
                existingItem.SoLuong++;
                existingItem.ThanhTien = existingItem.DonGia * existingItem.SoLuong;
            }
            else
            {
                MessageBox.Show("Không đủ tồn kho.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        else
        {
            CartItems.Add(new ChiTietDonHang
            {
                SanPhamId = product.Id,
                SanPham = product,
                SoLuong = 1,
                DonGia = product.GiaBan,
                ThanhTien = product.GiaBan
            });
        }
        
        var items = CartItems.ToList();
        CartItems.Clear();
        foreach (var item in items) CartItems.Add(item);

        CalculateTotals();
    }

    [RelayCommand]
    private void RemoveFromCart(ChiTietDonHang item)
    {
        if (item != null)
        {
            CartItems.Remove(item);
            CalculateTotals();
        }
    }

    [RelayCommand]
    private void ClearCart()
    {
        CartItems.Clear();
        CalculateTotals();
    }

    [RelayCommand]
    private async Task CheckoutAsync()
    {
        if (!CartItems.Any())
        {
            MessageBox.Show("Giỏ hàng trống.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        try
        {
            // NhanVienId = 1 is hardcoded for now until Auth module is implemented
            var order = await _orderService.CreateOrderAsync(1, null, CartItems.ToList());
            
            // Resolve PaymentViewModel and PaymentDialog
            var app = (App)System.Windows.Application.Current;
            var paymentViewModel = (PaymentViewModel)app.ServiceProvider.GetService(typeof(PaymentViewModel))!;
            paymentViewModel.Initialize(order);

            var paymentDialog = new HeThongPOS.WPF.Controls.PaymentDialog
            {
                DataContext = paymentViewModel,
                Owner = System.Windows.Application.Current.MainWindow
            };

            var result = paymentDialog.ShowDialog();
            
            if (result == true)
            {
                CartItems.Clear();
                CalculateTotals();
                await LoadProductsAsync(); // Reload to get updated stock
            }
            else
            {
                // Nếu khách không thanh toán -> Hủy đơn vừa tạo
                await _orderService.CancelOrderAsync(order.Id);
                MessageBox.Show("Đã hủy giao dịch thanh toán.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                await LoadProductsAsync(); // Reload stock
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi thanh toán", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void CalculateTotals()
    {
        var dummyOrder = new DonHang { ChiTietDonHangs = CartItems.ToList() };
        _orderService.CalculateOrderTotals(dummyOrder);
        
        TongTienHang = dummyOrder.TongTienHang;
        ThueVAT = dummyOrder.ThueVAT;
        TongThanhToan = dummyOrder.TongThanhToan;
    }
}
