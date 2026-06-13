using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using HeThongPOS.Infrastructure.Data;
using HeThongPOS.Core.Enums;

namespace HeThongPOS.WPF.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    private readonly AppDbContext _context;

    // --- Thẻ thống kê tổng quan ---
    [ObservableProperty]
    private decimal _doanhThuHomNay;

    [ObservableProperty]
    private int _soDonHomNay;

    [ObservableProperty]
    private decimal _doanhThuThangNay;

    [ObservableProperty]
    private int _tongSanPham;

    // --- Biểu đồ doanh thu 7 ngày ---
    [ObservableProperty]
    private ISeries[] _salesSeries = Array.Empty<ISeries>();

    [ObservableProperty]
    private Axis[] _xAxes = Array.Empty<Axis>();

    [ObservableProperty]
    private Axis[] _yAxes = Array.Empty<Axis>();

    // --- Top sản phẩm bán chạy ---
    [ObservableProperty]
    private ObservableCollection<TopProductItem> _topProducts = new();

    public DashboardViewModel(AppDbContext context)
    {
        _context = context;
        _ = LoadDataAsync();
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        await LoadOverviewAsync();
        await LoadSalesChartAsync();
        await LoadTopProductsAsync();
    }

    /// <summary>
    /// Tải dữ liệu tổng quan (4 thẻ thống kê trên cùng).
    /// </summary>
    private async Task LoadOverviewAsync()
    {
        var today = DateTime.Today;
        var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);

        // Doanh thu hôm nay
        DoanhThuHomNay = await _context.DonHangs
            .Where(d => d.NgayTao.Date == today && d.TrangThai == OrderStatus.Completed)
            .SumAsync(d => d.TongThanhToan);

        // Số đơn hôm nay
        SoDonHomNay = await _context.DonHangs
            .CountAsync(d => d.NgayTao.Date == today && d.TrangThai == OrderStatus.Completed);

        // Doanh thu tháng này
        DoanhThuThangNay = await _context.DonHangs
            .Where(d => d.NgayTao >= firstDayOfMonth && d.TrangThai == OrderStatus.Completed)
            .SumAsync(d => d.TongThanhToan);

        // Tổng sản phẩm đang kinh doanh
        TongSanPham = await _context.SanPhams
            .CountAsync(s => s.TrangThai);
    }

    /// <summary>
    /// Tải dữ liệu biểu đồ doanh thu 7 ngày gần nhất.
    /// </summary>
    private async Task LoadSalesChartAsync()
    {
        var today = DateTime.Today;
        var last7Days = Enumerable.Range(0, 7)
            .Select(i => today.AddDays(-6 + i))
            .ToList();

        // Truy vấn doanh thu theo từng ngày
        var salesData = await _context.DonHangs
            .Where(d => d.NgayTao.Date >= last7Days.First() && d.TrangThai == OrderStatus.Completed)
            .GroupBy(d => d.NgayTao.Date)
            .Select(g => new { Date = g.Key, Total = g.Sum(d => d.TongThanhToan) })
            .ToListAsync();

        // Map vào mảng 7 phần tử (ngày nào không có đơn thì = 0)
        var values = last7Days
            .Select(date => (double)(salesData.FirstOrDefault(s => s.Date == date)?.Total ?? 0))
            .ToArray();

        var labels = last7Days
            .Select(d => d.ToString("dd/MM"))
            .ToArray();

        SalesSeries = new ISeries[]
        {
            new ColumnSeries<double>
            {
                Values = values,
                Name = "Doanh thu",
                Fill = new SolidColorPaint(SKColors.DodgerBlue),
                MaxBarWidth = 30,
            }
        };

        XAxes = new Axis[]
        {
            new Axis
            {
                Labels = labels,
                LabelsRotation = 0,
                TextSize = 12,
            }
        };

        YAxes = new Axis[]
        {
            new Axis
            {
                Labeler = value => ((decimal)value).ToString("#,##0"),
                TextSize = 12,
            }
        };
    }

    /// <summary>
    /// Tải danh sách top 10 sản phẩm bán chạy nhất.
    /// </summary>
    private async Task LoadTopProductsAsync()
    {
        var topItems = await _context.ChiTietDonHangs
            .Include(ct => ct.SanPham)
            .Include(ct => ct.DonHang)
            .Where(ct => ct.DonHang.TrangThai == OrderStatus.Completed)
            .GroupBy(ct => new { ct.SanPhamId, ct.SanPham.TenSanPham })
            .Select(g => new TopProductItem
            {
                TenSanPham = g.Key.TenSanPham,
                TongSoLuong = g.Sum(x => x.SoLuong),
                TongDoanhThu = g.Sum(x => x.ThanhTien),
            })
            .OrderByDescending(x => x.TongSoLuong)
            .Take(10)
            .ToListAsync();

        TopProducts = new ObservableCollection<TopProductItem>(topItems);
    }
}

/// <summary>
/// DTO hiển thị top sản phẩm bán chạy trên DataGrid.
/// </summary>
public class TopProductItem
{
    public string TenSanPham { get; set; } = string.Empty;
    public int TongSoLuong { get; set; }
    public decimal TongDoanhThu { get; set; }
}
