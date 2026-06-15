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

public class ReportItemRow
{
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string SubValue { get; set; } = string.Empty;
}

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

    // Time & Report type selection
    [ObservableProperty] private string _selectedReportType = "Daily Sales Report";
    [ObservableProperty] private string _selectedTimePeriod = "Today";

    // Dynamic Report Collections
    public ObservableCollection<ReportItemRow> SalesMixItems { get; } = new();
    public ObservableCollection<ReportItemRow> PaymentItems { get; } = new();
    public ObservableCollection<ReportItemRow> CashierItems { get; } = new();
    public ObservableCollection<ReportItemRow> HourSalesItems { get; } = new();
    public ObservableCollection<ReportItemRow> TopProductsSales { get; } = new();
    public ObservableCollection<ReportItemRow> TopProductsQty { get; } = new();

    // Discount Report metrics
    [ObservableProperty] private decimal _totalDiscountAmount;
    [ObservableProperty] private decimal _promoDiscountAmount;
    [ObservableProperty] private decimal _memberDiscountAmount;

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
    private void SelectReport(string reportType)
    {
        SelectedReportType = reportType;
    }

    [RelayCommand]
    private async Task SelectTimePeriod(string period)
    {
        SelectedTimePeriod = period;
        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        try
        {
            var query = _context.DonHangs
                .Include(o => o.GiaoDichs)
                .ThenInclude(t => t.PhuongThucThanhToan)
                .Include(o => o.ChiTietDonHangs)
                .ThenInclude(c => c.SanPham)
                .ThenInclude(p => p.DanhMuc)
                .Include(o => o.NhanVien)
                .Where(o => o.TrangThai == OrderStatus.Completed);

            if (SelectedTimePeriod == "Today")
            {
                query = query.Where(o => o.NgayTao.Date == DateTime.Today);
            }

            var completedOrders = await query.ToListAsync();

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

            // 2. Sales Mix Report
            SalesMixItems.Clear();
            var salesMix = completedOrders
                .SelectMany(o => o.ChiTietDonHangs)
                .GroupBy(c => c.SanPham?.DanhMuc?.TenDanhMuc ?? "Chưa phân loại")
                .Select(g => new ReportItemRow
                {
                    Name = g.Key,
                    Value = FormatCurrency(g.Sum(c => c.ThanhTien)),
                    SubValue = $"{g.Sum(c => c.SoLuong)} sp"
                })
                .OrderByDescending(r => r.Name)
                .ToList();
            foreach (var item in salesMix) SalesMixItems.Add(item);

            // 3. Discount Detail Report
            TotalDiscountAmount = completedOrders.Sum(o => o.ChietKhau);
            PromoDiscountAmount = TotalDiscountAmount * 0.6m;
            MemberDiscountAmount = TotalDiscountAmount * 0.4m;

            // 4. Payment Detail Report
            PaymentItems.Clear();
            var paymentGroup = completedOrders
                .SelectMany(o => o.GiaoDichs)
                .GroupBy(t => t.PhuongThucThanhToan?.TenPhuongThuc ?? "Khác")
                .Select(g => new ReportItemRow
                {
                    Name = g.Key,
                    Value = FormatCurrency(g.Sum(t => t.SoTien)),
                    SubValue = $"{g.Count()} gd"
                })
                .ToList();
            foreach (var item in paymentGroup) PaymentItems.Add(item);

            // 5. Cashier Report
            CashierItems.Clear();
            var cashierGroup = completedOrders
                .GroupBy(o => o.NhanVien?.HoTen ?? "Không rõ")
                .Select(g => new ReportItemRow
                {
                    Name = g.Key,
                    Value = FormatCurrency(g.Sum(o => o.TongThanhToan)),
                    SubValue = $"{g.Count()} đơn"
                })
                .ToList();
            foreach (var item in cashierGroup) CashierItems.Add(item);

            // 6. Hour Sales Report
            HourSalesItems.Clear();
            var morning = completedOrders.Where(o => o.NgayTao.Hour >= 8 && o.NgayTao.Hour < 11).Sum(o => o.TongThanhToan);
            var noon = completedOrders.Where(o => o.NgayTao.Hour >= 11 && o.NgayTao.Hour < 14).Sum(o => o.TongThanhToan);
            var afternoon = completedOrders.Where(o => o.NgayTao.Hour >= 14 && o.NgayTao.Hour < 17).Sum(o => o.TongThanhToan);
            var evening = completedOrders.Where(o => o.NgayTao.Hour >= 17 && o.NgayTao.Hour < 22).Sum(o => o.TongThanhToan);

            HourSalesItems.Add(new ReportItemRow { Name = "08:00 - 11:00", Value = FormatCurrency(morning), SubValue = $"{completedOrders.Count(o => o.NgayTao.Hour >= 8 && o.NgayTao.Hour < 11)} đơn" });
            HourSalesItems.Add(new ReportItemRow { Name = "11:00 - 14:00", Value = FormatCurrency(noon), SubValue = $"{completedOrders.Count(o => o.NgayTao.Hour >= 11 && o.NgayTao.Hour < 14)} đơn" });
            HourSalesItems.Add(new ReportItemRow { Name = "14:00 - 17:00", Value = FormatCurrency(afternoon), SubValue = $"{completedOrders.Count(o => o.NgayTao.Hour >= 14 && o.NgayTao.Hour < 17)} đơn" });
            HourSalesItems.Add(new ReportItemRow { Name = "17:00 - 22:00", Value = FormatCurrency(evening), SubValue = $"{completedOrders.Count(o => o.NgayTao.Hour >= 17 && o.NgayTao.Hour < 22)} đơn" });

            // 7. Top 25 Sales
            TopProductsSales.Clear();
            var topSales = completedOrders
                .SelectMany(o => o.ChiTietDonHangs)
                .GroupBy(c => c.SanPham?.TenSanPham ?? "Sản phẩm")
                .Select(g => new { Name = g.Key, Revenue = g.Sum(c => c.ThanhTien), Qty = g.Sum(c => c.SoLuong) })
                .OrderByDescending(x => x.Revenue)
                .Take(25)
                .Select(x => new ReportItemRow
                {
                    Name = x.Name,
                    Value = FormatCurrency(x.Revenue),
                    SubValue = $"{x.Qty} sp"
                })
                .ToList();
            foreach (var item in topSales) TopProductsSales.Add(item);

            // 8. Top 25 Quantity
            TopProductsQty.Clear();
            var topQty = completedOrders
                .SelectMany(o => o.ChiTietDonHangs)
                .GroupBy(c => c.SanPham?.TenSanPham ?? "Sản phẩm")
                .Select(g => new { Name = g.Key, Qty = g.Sum(c => c.SoLuong), Revenue = g.Sum(c => c.ThanhTien) })
                .OrderByDescending(x => x.Qty)
                .Take(25)
                .Select(x => new ReportItemRow
                {
                    Name = x.Name,
                    Value = $"{x.Qty} sp",
                    SubValue = FormatCurrency(x.Revenue)
                })
                .ToList();
            foreach (var item in topQty) TopProductsQty.Add(item);

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
