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
using ClosedXML.Excel;
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

    // --- Các thông tin quản lý yêu cầu thêm ---
    [ObservableProperty]
    private decimal _tienMatHomNay;

    [ObservableProperty]
    private decimal _chuyenKhoanHomNay;

    [ObservableProperty]
    private int _donThanhCongHomNay;

    [ObservableProperty]
    private int _donThatBaiHomNay;

    [ObservableProperty]
    private decimal _thueVATHomNay;

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

    // --- Ca làm việc ---
    [ObservableProperty]
    private ObservableCollection<CaLamViecDTO> _caLamViecs = new();

    // --- Điều hướng UI Khung Phải ---
    [ObservableProperty]
    private bool _isShowingShiftReport = false; // False = Top Sản Phẩm, True = Báo cáo Ca

    // --- Bộ lọc ngày ---
    [ObservableProperty]
    private DateTime _selectedDate = DateTime.Today;

    partial void OnSelectedDateChanged(DateTime value)
    {
        _ = LoadDataAsync();
    }

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
        await LoadCaLamViecAsync();
    }

    [RelayCommand]
    private void ToggleRightPanel()
    {
        IsShowingShiftReport = !IsShowingShiftReport;
    }

    /// <summary>
    /// Tải dữ liệu tổng quan (4 thẻ thống kê trên cùng).
    /// </summary>
    private async Task LoadOverviewAsync()
    {
        var targetDate = SelectedDate.Date;
        var firstDayOfMonth = new DateTime(targetDate.Year, targetDate.Month, 1);

        // Lấy tất cả đơn hàng theo ngày đã chọn kèm Giao dịch
        var donHangsToday = await _context.DonHangs
            .Include(d => d.GiaoDichs)
            .ThenInclude(g => g.PhuongThucThanhToan)
            .Where(d => d.NgayTao.Date == targetDate)
            .ToListAsync();

        DoanhThuHomNay = donHangsToday
            .Where(d => d.TrangThai == OrderStatus.Completed)
            .Sum(d => d.TongThanhToan);

        DonThanhCongHomNay = donHangsToday.Count(d => d.TrangThai == OrderStatus.Completed);
        DonThatBaiHomNay = donHangsToday.Count(d => d.TrangThai == OrderStatus.Cancelled);
        SoDonHomNay = donHangsToday.Count; // Tổng số đơn

        ThueVATHomNay = donHangsToday
            .Where(d => d.TrangThai == OrderStatus.Completed)
            .Sum(d => d.ThueVAT);

        // Tính toán Tiền mặt / Chuyển khoản (Gộp cả đơn cũ không có GiaoDich)
        decimal tienMat = 0;
        decimal chuyenKhoan = 0;

        foreach (var don in donHangsToday.Where(d => d.TrangThai == OrderStatus.Completed))
        {
            if (don.GiaoDichs == null || !don.GiaoDichs.Any(g => g.TrangThai == "SUCCESS"))
            {
                // Các đơn hàng CŨ (trước khi sửa lỗi) không có bản ghi GiaoDich -> Mặc định là Tiền mặt
                tienMat += don.TongThanhToan;
            }
            else
            {
                // Các đơn hàng MỚI đã có GiaoDich
                foreach (var g in don.GiaoDichs.Where(g => g.TrangThai == "SUCCESS"))
                {
                    if (g.PhuongThucThanhToan != null && g.PhuongThucThanhToan.TenPhuongThuc.Contains("mặt", StringComparison.OrdinalIgnoreCase))
                    {
                        tienMat += g.SoTien;
                    }
                    else
                    {
                        chuyenKhoan += g.SoTien;
                    }
                }
            }
        }

        TienMatHomNay = tienMat;
        ChuyenKhoanHomNay = chuyenKhoan;

        // Doanh thu tháng này
        DoanhThuThangNay = await _context.DonHangs
            .Where(d => d.NgayTao >= firstDayOfMonth && d.TrangThai == OrderStatus.Completed)
            .SumAsync(d => d.TongThanhToan);

        // Tổng sản phẩm đang kinh doanh
        TongSanPham = await _context.SanPhams
            .CountAsync(s => s.TrangThai);
    }

    /// <summary>
    /// Tải dữ liệu biểu đồ doanh thu 7 ngày gần nhất (tính từ ngày được chọn).
    /// </summary>
    private async Task LoadSalesChartAsync()
    {
        var targetDate = SelectedDate.Date;
        var last7Days = Enumerable.Range(0, 7)
            .Select(i => targetDate.AddDays(-6 + i))
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

    private async Task LoadCaLamViecAsync()
    {
        var targetDate = SelectedDate.Date;
        var shifts = await _context.CaLamViecs
            .Include(c => c.NhanVien)
            .Where(c => c.ThoiGianBatDau.Date == targetDate)
            .OrderByDescending(c => c.ThoiGianBatDau)
            .ToListAsync();

        CaLamViecs.Clear();
        foreach (var shift in shifts)
        {
            CaLamViecs.Add(new CaLamViecDTO
            {
                TenNhanVien = shift.NhanVien.HoTen,
                ThoiGianBatDau = shift.ThoiGianBatDau.ToString("HH:mm"),
                ThoiGianKetThuc = shift.ThoiGianKetThuc?.ToString("HH:mm") ?? "Chưa kết thúc",
                SoDuDauCa = shift.SoDuDauCa,
                TongDoanhThu = shift.TongDoanhThu ?? 0,
                SoDuCuoiCaThucTe = shift.SoDuCuoiCaThucTe ?? 0,
                ChenhLech = shift.ChenhLech ?? 0,
                TrangThai = shift.TrangThai == "OPEN" ? "Đang mở" : "Đã đóng"
            });
        }
    }

    [RelayCommand]
    private async Task ExportToExcelAsync()
    {
        try
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = System.IO.Path.Combine(desktopPath, $"BaoCao_CaLamViec_{SelectedDate:dd_MM_yyyy}.xlsx");

            await Task.Run(() =>
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Báo Cáo Ca");
                    
                    // Header
                    worksheet.Cell(1, 1).Value = "Nhân viên";
                    worksheet.Cell(1, 2).Value = "Giờ mở ca";
                    worksheet.Cell(1, 3).Value = "Giờ đóng ca";
                    worksheet.Cell(1, 4).Value = "Tiền mở ca";
                    worksheet.Cell(1, 5).Value = "Doanh thu ca";
                    worksheet.Cell(1, 6).Value = "Thực thu (Kết ca)";
                    worksheet.Cell(1, 7).Value = "Độ lệch (Thiếu hụt)";
                    worksheet.Cell(1, 8).Value = "Trạng thái";

                    var headerRange = worksheet.Range("A1:H1");
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.DodgerBlue;
                    headerRange.Style.Font.FontColor = XLColor.White;
                    headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    // Data
                    int row = 2;
                    foreach (var shift in CaLamViecs)
                    {
                        worksheet.Cell(row, 1).Value = shift.TenNhanVien;
                        worksheet.Cell(row, 2).Value = shift.ThoiGianBatDau;
                        worksheet.Cell(row, 3).Value = shift.ThoiGianKetThuc;
                        
                        worksheet.Cell(row, 4).Value = shift.SoDuDauCa;
                        worksheet.Cell(row, 4).Style.NumberFormat.Format = "#,##0 ₫";

                        worksheet.Cell(row, 5).Value = shift.TongDoanhThu;
                        worksheet.Cell(row, 5).Style.NumberFormat.Format = "#,##0 ₫";
                        worksheet.Cell(row, 5).Style.Font.Bold = true;

                        worksheet.Cell(row, 6).Value = shift.SoDuCuoiCaThucTe;
                        worksheet.Cell(row, 6).Style.NumberFormat.Format = "#,##0 ₫";

                        worksheet.Cell(row, 7).Value = shift.ChenhLech;
                        worksheet.Cell(row, 7).Style.NumberFormat.Format = "#,##0 ₫";
                        if (shift.ChenhLech < 0)
                        {
                            worksheet.Cell(row, 7).Style.Font.FontColor = XLColor.Red;
                            worksheet.Cell(row, 7).Style.Font.Bold = true;
                        }
                        else if (shift.ChenhLech > 0)
                        {
                            worksheet.Cell(row, 7).Style.Font.FontColor = XLColor.Green;
                        }

                        worksheet.Cell(row, 8).Value = shift.TrangThai;
                        row++;
                    }

                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(filePath);
                }
            });

            System.Windows.MessageBox.Show($"Đã xuất báo cáo thành công tại:\n{filePath}", "Thành công", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show($"Lỗi xuất Excel: {ex.Message}", "Lỗi", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
        }
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

public class CaLamViecDTO
{
    public string TenNhanVien { get; set; } = string.Empty;
    public string ThoiGianBatDau { get; set; } = string.Empty;
    public string ThoiGianKetThuc { get; set; } = string.Empty;
    public decimal SoDuDauCa { get; set; }
    public decimal TongDoanhThu { get; set; }
    public decimal SoDuCuoiCaThucTe { get; set; }
    public decimal ChenhLech { get; set; }
    public string TrangThai { get; set; } = string.Empty;
}
