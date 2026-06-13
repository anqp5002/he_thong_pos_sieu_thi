using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using HeThongPOS.Core.Entities;

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
}
