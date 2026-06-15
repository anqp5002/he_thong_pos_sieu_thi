using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Interfaces;
using HeThongPOS.WPF.Services;

namespace HeThongPOS.WPF.ViewModels;

public partial class BillPreviewViewModel : ObservableObject
{
    private readonly IInvoiceService _invoiceService;
    private readonly PrintService _printService;

    [ObservableProperty]
    private DonHang? _order;

    [ObservableProperty]
    private decimal _cashReceived;

    [ObservableProperty]
    private decimal _changeAmount;

    [ObservableProperty]
    private FlowDocument? _invoiceDocument;

    [ObservableProperty]
    private string _paymentMethodName = "Tiền mặt";

    public BillPreviewViewModel(IInvoiceService invoiceService, PrintService printService)
    {
        _invoiceService = invoiceService;
        _printService = printService;
    }

    public void Setup(DonHang order)
    {
        // Try to get payment details from transaction
        var transaction = order.GiaoDichs?.FirstOrDefault(t => t.TrangThai == "SUCCESS");
        decimal cash = order.TongThanhToan;
        decimal change = 0;

        if (transaction != null)
        {
            // For card/bank transfer, cash received is exactly the total paid, change is 0
            // For cash, if we have cash received, we can seed or calculate it.
            // Let's calculate: if it's cash, we assume they gave a rounded amount or exact.
            // Let's default cash received to order total if not specified
            cash = transaction.SoTien; 
            if (transaction.PhuongThucThanhToan?.TenPhuongThuc == "Tiền mặt")
            {
                // Set fake cash received based on amount to show change
                // e.g. round up to nearest 50k or 100k
                decimal total = order.TongThanhToan;
                if (total <= 50000) cash = 50000;
                else if (total <= 100000) cash = 100000;
                else if (total <= 200000) cash = 200000;
                else if (total <= 500000) cash = 500000;
                else cash = Math.Ceiling(total / 100000) * 100000;

                change = cash - total;
            }
        }

        Setup(order, cash, change);
    }

    public void Setup(DonHang order, decimal cashReceived, decimal changeAmount)
    {
        Order = order;
        CashReceived = cashReceived;
        ChangeAmount = changeAmount;

        var transaction = order.GiaoDichs?.FirstOrDefault(t => t.TrangThai == "SUCCESS");
        PaymentMethodName = transaction?.PhuongThucThanhToan?.TenPhuongThuc ?? "Tiền mặt";

        // Generate the FlowDocument receipt layout
        InvoiceDocument = (FlowDocument)_invoiceService.GenerateInvoiceDocument(order, cashReceived, changeAmount);
    }

    [RelayCommand]
    private void Print()
    {
        if (InvoiceDocument == null) return;

        try
        {
            _printService.PrintDocument(InvoiceDocument, $"In Hoa Don - {Order?.MaDonHang}");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi in ấn", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    [RelayCommand]
    private void NewOrder()
    {
        // Close the host Window if it's shown as a dialog
        foreach (Window window in System.Windows.Application.Current.Windows)
        {
            if (window.DataContext == this)
            {
                window.Close();
                return;
            }
        }

        // Navigate back to the Orders list
        if (System.Windows.Application.Current.MainWindow is MainWindow mainWindow)
        {
            mainWindow.NavigateToOrders();
        }
    }
}
