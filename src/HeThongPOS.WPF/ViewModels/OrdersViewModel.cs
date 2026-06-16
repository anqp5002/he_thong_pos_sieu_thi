using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using HeThongPOS.Core.Entities;
using HeThongPOS.Infrastructure.Data;
using System.Windows;

namespace HeThongPOS.WPF.ViewModels;

public partial class OrdersViewModel : ObservableObject
{
    private readonly AppDbContext _context;

    [ObservableProperty]
    private ObservableCollection<DonHang> _ordersList = new();

    [ObservableProperty]
    private DonHang? _selectedOrder;

    [ObservableProperty]
    private string _searchKeyword = string.Empty;

    public OrdersViewModel(AppDbContext context)
    {
        _context = context;
        _ = LoadOrdersAsync();
    }

    [RelayCommand]
    private async Task LoadOrdersAsync()
    {
        try
        {
            var query = _context.DonHangs
                .Include(d => d.NhanVien)
                .Include(d => d.KhachHang)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchKeyword))
            {
                var keyword = SearchKeyword.ToLower();
                query = query.Where(d => d.MaDonHang.ToLower().Contains(keyword) 
                                      || (d.KhachHang != null && d.KhachHang.HoTen.ToLower().Contains(keyword)));
            }

            // Sắp xếp đơn mới nhất lên đầu
            var list = await query.OrderByDescending(d => d.NgayTao).Take(100).ToListAsync();
            OrdersList = new ObservableCollection<DonHang>(list);
        }
        catch (System.Exception ex)
        {
            MessageBox.Show($"Lỗi tải danh sách đơn hàng: {ex.Message}");
        }
    }

    partial void OnSearchKeywordChanged(string value)
    {
        _ = LoadOrdersAsync();
    }
}
