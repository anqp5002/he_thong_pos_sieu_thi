using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using HeThongPOS.Core.Entities;
using HeThongPOS.Infrastructure.Data;

namespace HeThongPOS.WPF.ViewModels;

public partial class PaymentViewModel : ObservableObject
{
    private readonly AppDbContext _context;

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

    public decimal TienThoi => KhachDua >= TongThanhToan ? KhachDua - TongThanhToan : 0;
    
    public bool CanCheckout => KhachDua >= TongThanhToan || PhuongThucThanhToan != "CASH";

    public Action? OnPaymentSuccess;
    public Action? OnRequestClose;

    public PaymentViewModel(AppDbContext context)
    {
        _context = context;
    }

    public void Initialize(decimal tongThanhToan)
    {
        TongThanhToan = tongThanhToan;
        KhachDua = 0;
        PhuongThucThanhToan = "CASH";
        SelectedCustomer = null;
        SearchCustomerKeyword = string.Empty;
        SuggestedCustomers.Clear();
        UpdateTienThoi();
    }

    partial void OnSearchCustomerKeywordChanged(string value)
    {
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

        // Giả lập delay xử lý thanh toán (thực tế sẽ gọi PaymentService lưu DB)
        await Task.Delay(500); 

        // Gửi event báo thanh toán xong
        OnPaymentSuccess?.Invoke();
    }

    [RelayCommand]
    private void Cancel()
    {
        OnRequestClose?.Invoke();
    }
}
