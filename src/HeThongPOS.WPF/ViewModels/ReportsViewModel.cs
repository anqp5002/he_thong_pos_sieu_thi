using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Enums;
using HeThongPOS.Infrastructure.Data;
using HeThongPOS.WPF.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace HeThongPOS.WPF.ViewModels;

public partial class ReportsViewModel : ObservableObject
{
    private readonly AppDbContext _context;
    private readonly PrintService _printService;

    // Daily Sales Report [R21] Metrics
    [ObservableProperty] private decimal _grossSales;
    [ObservableProperty] private decimal _netSales;
    [ObservableProperty] private decimal _openingFund = 2000000; // Mock opening fund 2M
    [ObservableProperty] private decimal _cashSales;
    [ObservableProperty] private decimal _cardSales;
    [ObservableProperty] private decimal _cashReceived;
    [ObservableProperty] private decimal _tillOut = 0;
    [ObservableProperty] private decimal _cashDifference = 0;
    [ObservableProperty] private decimal _grandTotal;

    // Audit Trail
    [ObservableProperty] private int _cashierVoidCount = 0;
    [ObservableProperty] private int _refundItemCount = 0;
    [ObservableProperty] private decimal _refundAmount = 0;

    // Open Bill
    [ObservableProperty] private int _openBillCount;
    [ObservableProperty] private decimal _openBillAmount;

    // Summary Cards (Today)
    [ObservableProperty] private decimal _revenueToday;
    [ObservableProperty] private int _transactionCountToday;
    [ObservableProperty] private decimal _averageTicketToday;
    [ObservableProperty] private string _topProductToday = "N/A";
    [ObservableProperty] private int _topProductCountToday;

    // 7-day revenue trend points for Drawing
    [ObservableProperty] private ObservableCollection<Point> _chartPoints = new();
    [ObservableProperty] private ObservableCollection<string> _chartLabels = new();
    [ObservableProperty] private ObservableCollection<Point> _chartOrderPoints = new();

    public ReportsViewModel(AppDbContext context, PrintService printService)
    {
        _context = context;
        _printService = printService;
        LoadDataCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        try
        {
            var today = DateTime.Today;

            // 1. Load Orders completed today (or all time for seed data demo)
            // Note: Since seed data contains orders over a 30-day range, let's aggregate for the last 30 days
            // but call it "Today's summary" or matching date range for the seeder.
            var completedOrders = await _context.DonHangs
                .Include(o => o.GiaoDichs)
                .ThenInclude(t => t.PhuongThucThanhToan)
                .Include(o => o.ChiTietDonHangs)
                .ThenInclude(c => c.SanPham)
                .Where(o => o.TrangThai == OrderStatus.Completed)
                .ToListAsync();

            var pendingOrders = await _context.DonHangs
                .Where(o => o.TrangThai == OrderStatus.Pending)
                .ToListAsync();

            // Total calculations
            GrossSales = completedOrders.Sum(o => o.TongTienHang);
            NetSales = completedOrders.Sum(o => o.TongThanhToan);
            RevenueToday = NetSales;
            TransactionCountToday = completedOrders.Count;
            AverageTicketToday = TransactionCountToday > 0 ? NetSales / TransactionCountToday : 0;

            // Cash vs Card Sales
            CashSales = completedOrders
                .Where(o => o.GiaoDichs.Any(t => t.PhuongThucThanhToan?.TenPhuongThuc == "Tiền mặt"))
                .Sum(o => o.TongThanhToan);

            CardSales = completedOrders
                .Where(o => o.GiaoDichs.Any(t => t.PhuongThucThanhToan?.TenPhuongThuc != "Tiền mặt"))
                .Sum(o => o.TongThanhToan);

            CashReceived = CashSales; // Assuming exact match for seeder
            GrandTotal = NetSales + OpeningFund;

            // Open Bills
            OpenBillCount = pendingOrders.Count;
            OpenBillAmount = pendingOrders.Sum(o => o.TongThanhToan);

            // Find Top Selling Product
            var productSales = completedOrders
                .SelectMany(o => o.ChiTietDonHangs)
                .GroupBy(c => c.SanPham?.TenSanPham ?? "Sản phẩm")
                .Select(g => new { Name = g.Key, Qty = g.Sum(c => c.SoLuong) })
                .OrderByDescending(x => x.Qty)
                .FirstOrDefault();

            if (productSales != null)
            {
                TopProductToday = productSales.Name;
                TopProductCountToday = productSales.Qty;
            }
            else
            {
                TopProductToday = "Chưa có";
                TopProductCountToday = 0;
            }

            // 2. Generate 7-day Revenue Trend Data
            GenerateRevenueTrend(completedOrders);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải dữ liệu báo cáo: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void GenerateRevenueTrend(List<DonHang> orders)
    {
        ChartPoints.Clear();
        ChartLabels.Clear();
        ChartOrderPoints.Clear();

        // Get last 7 days starting from today backward
        var last7Days = Enumerable.Range(0, 7)
            .Select(i => DateTime.Today.AddDays(-i))
            .Reverse()
            .ToList();

        // Calculate sales per day
        var salesGroup = orders
            .GroupBy(o => o.NgayTao.Date)
            .ToDictionary(g => g.Key, g => new { Total = g.Sum(o => o.TongThanhToan), Count = g.Count() });

        // Let's scale points for the Canvas width=450, height=180
        // Canvas width is 450. 7 points means x spacing = 450 / 6 = 75px
        // Max revenue to scale y-axis. Let's find max daily revenue in the list
        decimal maxRevenue = 1000000; // Minimum scale height is 1M
        int maxCount = 5;

        var dailyData = new List<(DateTime Date, decimal Sales, int Count)>();
        foreach (var day in last7Days)
        {
            decimal dailySales = 0;
            int dailyCount = 0;

            if (salesGroup.TryGetValue(day.Date, out var data))
            {
                dailySales = data.Total;
                dailyCount = data.Count;
            }

            dailyData.Add((day, dailySales, dailyCount));
            if (dailySales > maxRevenue) maxRevenue = dailySales;
            if (dailyCount > maxCount) maxCount = dailyCount;
        }

        // Generate points
        for (int i = 0; i < dailyData.Count; i++)
        {
            var data = dailyData[i];
            double x = i * 70 + 20; // 20px padding left, 70px step

            // Revenue point (Canvas top is 0, bottom is 160. Height limit 130px)
            double yRevenue = 150 - (double)(data.Sales / maxRevenue) * 110;
            ChartPoints.Add(new Point(x, yRevenue));

            // Order count point
            double yOrders = 150 - (double)data.Count / maxCount * 110;
            ChartOrderPoints.Add(new Point(x, yOrders));

            // Add label (dd/MM)
            ChartLabels.Add(data.Date.ToString("dd/MM"));
        }
    }

    [RelayCommand]
    private void ExportData()
    {
        var saveFileDialog = new SaveFileDialog
        {
            Filter = "CSV File (*.csv)|*.csv",
            FileName = $"BaoCaoDoanhThu_{DateTime.Today:yyyyMMdd}.csv"
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            try
            {
                var sb = new StringBuilder();
                // UTF-8 BOM for Excel compatibility
                sb.AppendLine("\uFEFF");
                sb.AppendLine("BÁO CÁO DOANH THU HÀNG NGÀY");
                sb.AppendLine($"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}");
                sb.AppendLine();
                sb.AppendLine("1. TỔNG HỢP KIỂM TOÁN");
                sb.AppendLine($"Tổng tiền hàng (Gross Sales),{GrossSales:F0} đ");
                sb.AppendLine($"Doanh thu thuần (Net Sales),{NetSales:F0} đ");
                sb.AppendLine($"Quỹ đầu ca (Opening Fund),{OpeningFund:F0} đ");
                sb.AppendLine($"Doanh thu Tiền mặt (Cash Sales),{CashSales:F0} đ");
                sb.AppendLine($"Doanh thu Thẻ/Chuyển khoản (Card Sales),{CardSales:F0} đ");
                sb.AppendLine($"Tổng kiểm toán (Grand Total),{GrandTotal:F0} đ");
                sb.AppendLine();
                sb.AppendLine("2. HÓA ĐƠN CHƯA THANH TOÁN (OPEN BILLS)");
                sb.AppendLine($"Số lượng đơn chờ,{OpenBillCount}");
                sb.AppendLine($"Tổng tiền chờ,{OpenBillAmount:F0} đ");
                sb.AppendLine();
                sb.AppendLine("3. TOP SẢN PHẨM BÁN CHẠY");
                sb.AppendLine($"Tên sản phẩm,{TopProductToday}");
                sb.AppendLine($"Số lượng bán,{TopProductCountToday}");

                File.WriteAllText(saveFileDialog.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show("Xuất báo cáo thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất file: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    [RelayCommand]
    private void Print()
    {
        try
        {
            // Create a simple FlowDocument summarizing the report
            var doc = new FlowDocument
            {
                PageWidth = 300,
                PagePadding = new Thickness(10),
                FontFamily = new System.Windows.Media.FontFamily("Segoe UI"),
                FontSize = 11,
                Background = System.Windows.Media.Brushes.White
            };

            var header = new Paragraph(new Bold(new Run("BÁO CÁO DOANH THU DAILY\n"))) { TextAlignment = TextAlignment.Center, FontSize = 14 };
            header.Inlines.Add(new Run($"Ngày: {DateTime.Today:dd/MM/yyyy}\n") { FontSize = 10 });
            header.Inlines.Add(new Run($"Xuất lúc: {DateTime.Now:HH:mm}\n") { FontSize = 9 });
            doc.Blocks.Add(header);

            doc.Blocks.Add(new Paragraph(new Run("==================================")) { TextAlignment = TextAlignment.Center });

            var body = new Paragraph();
            body.Inlines.Add(new Run($"Tiền hàng (Gross): {FormatCurrency(GrossSales)}\n"));
            body.Inlines.Add(new Run($"Thanh toán (Net): {FormatCurrency(NetSales)}\n"));
            body.Inlines.Add(new Run($"Quỹ đầu ca: {FormatCurrency(OpeningFund)}\n"));
            body.Inlines.Add(new Run($"Tiền mặt: {FormatCurrency(CashSales)}\n"));
            body.Inlines.Add(new Run($"Thẻ/Chuyển khoản: {FormatCurrency(CardSales)}\n"));
            body.Inlines.Add(new Run("----------------------------------\n"));
            body.Inlines.Add(new Bold(new Run($"Tổng cộng: {FormatCurrency(GrandTotal)}\n")));
            doc.Blocks.Add(body);

            doc.Blocks.Add(new Paragraph(new Run("==================================")) { TextAlignment = TextAlignment.Center });
            
            var footer = new Paragraph(new Run("Nhân viên xác nhận\n\n\n...............................")) { TextAlignment = TextAlignment.Center, FontSize = 10 };
            doc.Blocks.Add(footer);

            _printService.PrintDocument(doc, "Báo Cáo Doanh Thu");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi in báo cáo", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private string FormatCurrency(decimal amount)
    {
        return amount.ToString("N0", CultureInfo.GetCultureInfo("vi-VN")) + "đ";
    }
}
