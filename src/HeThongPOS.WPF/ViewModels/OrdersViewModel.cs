using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Enums;
using HeThongPOS.Core.Interfaces;
using HeThongPOS.WPF.Controls;

namespace HeThongPOS.WPF.ViewModels;

public partial class OrdersViewModel : ObservableObject
{
    private readonly IOrderService _orderService;

    [ObservableProperty]
    private ObservableCollection<DonHang> _orders = new();

    [ObservableProperty]
    private DateTime? _fromDate;

    [ObservableProperty]
    private DateTime? _toDate;

    [ObservableProperty]
    private OrderStatus? _selectedStatus;

    [ObservableProperty]
    private DonHang? _selectedOrder;

    public Array OrderStatuses => Enum.GetValues(typeof(OrderStatus));

    public OrdersViewModel(IOrderService orderService)
    {
        _orderService = orderService;
        LoadDataCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        await SearchAsync();
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        try
        {
            var orders = await _orderService.GetOrdersAsync(FromDate, ToDate, SelectedStatus);
            Orders.Clear();
            foreach (var order in orders)
            {
                Orders.Add(order);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tìm kiếm đơn hàng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task ViewDetailAsync()
    {
        if (SelectedOrder == null)
        {
            MessageBox.Show("Vui lòng chọn một đơn hàng để xem chi tiết.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        try
        {
            var orderWithItems = await _orderService.GetOrderDetailsAsync(SelectedOrder.Id);
            if (orderWithItems != null)
            {
                var dialog = new OrderDetailDialog();
                dialog.DataContext = new OrderDetailViewModel(orderWithItems);
                dialog.ShowDialog();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải chi tiết đơn hàng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    [RelayCommand]
    private void ClearFilter()
    {
        FromDate = null;
        ToDate = null;
        SelectedStatus = null;
        SearchCommand.Execute(null);
    }
}
