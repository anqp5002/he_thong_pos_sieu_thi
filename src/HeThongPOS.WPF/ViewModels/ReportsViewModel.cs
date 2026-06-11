using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeThongPOS.Application.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.Win32;
using SkiaSharp;

namespace HeThongPOS.WPF.ViewModels;

public partial class ReportsViewModel : ObservableObject
{
    private readonly ReportService _reportService;
    private readonly ExportService _exportService;

    [ObservableProperty]
    private DateTime _startDate = DateTime.Today.AddDays(-7);

    [ObservableProperty]
    private DateTime _endDate = DateTime.Today;

    [ObservableProperty]
    private decimal _totalRevenue;

    [ObservableProperty]
    private int _totalOrders;

    [ObservableProperty]
    private ObservableCollection<TopProductDto> _topProducts = new();

    public ISeries[] RevenueSeries { get; set; } = new ISeries[1];
    public Axis[] XAxes { get; set; } = new Axis[1];
    public Axis[] YAxes { get; set; } = new Axis[1];

    public ReportsViewModel(ReportService reportService, ExportService exportService)
    {
        _reportService = reportService;
        _exportService = exportService;
        
        // Initial setup for empty charts
        RevenueSeries[0] = new LineSeries<decimal>
        {
            Values = new decimal[] { },
            Name = "Doanh Thu",
            GeometrySize = 10,
            LineSmoothness = 0.5,
            Stroke = new SolidColorPaint(SKColors.DodgerBlue) { StrokeThickness = 3 },
            Fill = new SolidColorPaint(SKColors.DodgerBlue.WithAlpha(50))
        };

        XAxes[0] = new Axis
        {
            Labels = new string[] { },
            LabelsRotation = 15
        };

        YAxes[0] = new Axis
        {
            Labeler = value => value.ToString("N0")
        };

        LoadDataCommand.Execute(null);
    }

    public decimal AOV => TotalOrders > 0 ? TotalRevenue / TotalOrders : 0;

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        try
        {
            // Update summary
            TotalRevenue = await _reportService.GetTotalRevenueAsync(StartDate, EndDate.AddDays(1).AddSeconds(-1));
            TotalOrders = await _reportService.GetTotalOrdersAsync(StartDate, EndDate.AddDays(1).AddSeconds(-1));
            OnPropertyChanged(nameof(AOV));

            // Load top products
            var products = await _reportService.GetTopProductsAsync(StartDate, EndDate.AddDays(1).AddSeconds(-1), 10);
            TopProducts.Clear();
            foreach (var p in products)
            {
                TopProducts.Add(p);
            }

            // Load charts
            var daily = await _reportService.GetRevenueByDayAsync(StartDate, EndDate.AddDays(1).AddSeconds(-1));
            
            var lineSeries = (LineSeries<decimal>)RevenueSeries[0];
            lineSeries.Values = daily.Select(x => x.Revenue).ToArray();
            
            XAxes[0].Labels = daily.Select(x => x.Date.ToString("dd/MM")).ToArray();
            
            OnPropertyChanged(nameof(RevenueSeries));
            OnPropertyChanged(nameof(XAxes));
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task ExportDataAsync()
    {
        var saveFileDialog = new SaveFileDialog
        {
            Filter = "Excel Files|*.xlsx",
            Title = "Lưu báo cáo",
            FileName = $"BaoCao_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            try
            {
                var daily = await _reportService.GetRevenueByDayAsync(StartDate, EndDate.AddDays(1).AddSeconds(-1));
                _exportService.ExportDailyRevenueToExcel(daily, saveFileDialog.FileName);
                MessageBox.Show("Xuất báo cáo thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất dữ liệu: {ex.Message}");
            }
        }
    }
}
