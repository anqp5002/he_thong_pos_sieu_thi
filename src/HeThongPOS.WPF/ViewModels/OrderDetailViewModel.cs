using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Interfaces;
using HeThongPOS.WPF.Services;
using HeThongPOS.WPF.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HeThongPOS.WPF.ViewModels;

public partial class OrderDetailViewModel : ObservableObject
{
    [ObservableProperty]
    private DonHang _order;

    public ObservableCollection<ChiTietDonHang> OrderItems { get; }

    public OrderDetailViewModel(DonHang order)
    {
        _order = order;
        OrderItems = new ObservableCollection<ChiTietDonHang>(order.ChiTietDonHangs);
    }

    [RelayCommand]
    private void PreviewInvoice()
    {
        try
        {
            var previewWindow = new Window
            {
                Title = $"Xem trước Hóa đơn - {Order.MaDonHang}",
                Width = 800,
                Height = 600,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Content = new BillPreviewView()
            };

            var billPreviewVm = ((App)System.Windows.Application.Current).ServiceProvider.GetRequiredService<BillPreviewViewModel>();
            billPreviewVm.Setup(Order);
            previewWindow.DataContext = billPreviewVm;

            previewWindow.ShowDialog();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Không thể hiển thị xem trước hóa đơn: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void PrintInvoice()
    {
        try
        {
            var serviceProvider = ((App)System.Windows.Application.Current).ServiceProvider;
            var invoiceService = serviceProvider.GetRequiredService<IInvoiceService>();
            var printService = serviceProvider.GetRequiredService<PrintService>();

            var doc = (FlowDocument)invoiceService.GenerateInvoiceDocument(Order, Order.TongThanhToan, 0);
            printService.PrintDocument(doc, $"In Hoa Don - {Order.MaDonHang}");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi in ấn", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
