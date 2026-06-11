using System;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Enums;
using HeThongPOS.Core.Interfaces;

namespace HeThongPOS.WPF.ViewModels;

public partial class PaymentViewModel : ObservableObject
{
    private readonly IPaymentService _paymentService;
    private readonly HeThongPOS.WPF.Services.PrintService _printService;
    private readonly HeThongPOS.WPF.Services.InvoiceGenerator _invoiceGenerator;

    [ObservableProperty]
    private DonHang _currentOrder = null!;

    [ObservableProperty]
    private PaymentMethod _selectedPaymentMethod = PaymentMethod.Cash;

    [ObservableProperty]
    private decimal _amountProvided;

    [ObservableProperty]
    private decimal _changeAmount;

    public Action? OnPaymentSuccess;
    public Action? OnCancel;

    public PaymentViewModel(IPaymentService paymentService, HeThongPOS.WPF.Services.PrintService printService, HeThongPOS.WPF.Services.InvoiceGenerator invoiceGenerator)
    {
        _paymentService = paymentService;
        _printService = printService;
        _invoiceGenerator = invoiceGenerator;
    }

    public void Initialize(DonHang order)
    {
        CurrentOrder = order;
        AmountProvided = order.TongThanhToan;
        CalculateChange();
    }

    partial void OnAmountProvidedChanged(decimal value)
    {
        CalculateChange();
    }

    private void CalculateChange()
    {
        if (CurrentOrder != null)
        {
            ChangeAmount = AmountProvided - CurrentOrder.TongThanhToan;
        }
    }

    [RelayCommand]
    private async Task ProcessPaymentAsync()
    {
        if (AmountProvided < CurrentOrder.TongThanhToan)
        {
            MessageBox.Show("Số tiền khách đưa không đủ.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            var success = await _paymentService.ProcessPaymentAsync(
                CurrentOrder.Id, 
                SelectedPaymentMethod, 
                AmountProvided, 
                ""); 

            if (success)
            {
                var printResult = MessageBox.Show("Thanh toán thành công! Bạn có muốn in hóa đơn không?", "Thành công", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (printResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        var doc = _invoiceGenerator.GenerateInvoice(CurrentOrder);
                        _printService.PrintDocument(doc, $"HoaDon_{CurrentOrder.MaDonHang}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Lỗi in", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                
                OnPaymentSuccess?.Invoke();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi thanh toán", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void CancelPayment()
    {
        OnCancel?.Invoke();
    }
}
