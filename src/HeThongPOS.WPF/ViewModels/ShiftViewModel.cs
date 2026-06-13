using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Enums;
using HeThongPOS.Infrastructure.Data;
using HeThongPOS.WPF.Services;
using Microsoft.EntityFrameworkCore;

namespace HeThongPOS.WPF.ViewModels;

public partial class ShiftViewModel : ObservableObject
{
    private readonly AppDbContext _context;

    [ObservableProperty] private bool _isShiftOpen;
    [ObservableProperty] private string _cashierName = string.Empty;
    [ObservableProperty] private string _startTimeDisplay = string.Empty;
    
    // Open Shift Inputs
    [ObservableProperty] private decimal _openingFundInput = 2000000; // Default 2M

    // Close Shift Stats & Inputs
    [ObservableProperty] private decimal _openingFund;
    [ObservableProperty] private decimal _systemSales;
    [ObservableProperty] private decimal _expectedCash;
    [ObservableProperty] private decimal _actualCashInput;
    [ObservableProperty] private string _noteInput = string.Empty;

    public ShiftViewModel(AppDbContext context)
    {
        _context = context;
        LoadShiftDataCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadShiftDataAsync()
    {
        try
        {
            var currentUser = SessionContext.CurrentUser;
            if (currentUser == null)
            {
                CashierName = "Chưa đăng nhập";
                IsShiftOpen = false;
                return;
            }

            CashierName = currentUser.HoTen;

            // Check if there is an open shift in DB for this cashier
            var openShift = await _context.CaLamViecs
                .FirstOrDefaultAsync(c => c.NhanVienId == currentUser.Id && c.TrangThai == "OPEN");

            if (openShift != null)
            {
                SessionContext.CurrentShift = openShift;
                IsShiftOpen = true;
                OpeningFund = openShift.SoDuDauCa;
                StartTimeDisplay = openShift.ThoiGianBatDau.ToString("dd/MM/yyyy HH:mm:ss");

                // Calculate completed orders' revenue during this shift
                var completedOrders = await _context.DonHangs
                    .Include(o => o.GiaoDichs)
                    .ThenInclude(t => t.PhuongThucThanhToan)
                    .Where(o => o.NhanVienId == currentUser.Id && 
                               o.NgayTao >= openShift.ThoiGianBatDau && 
                               o.TrangThai == OrderStatus.Completed)
                    .ToListAsync();

                SystemSales = completedOrders.Sum(o => o.TongThanhToan);

                // Calculate cash specifically received
                decimal cashReceived = 0;
                foreach (var order in completedOrders)
                {
                    var cashTransactions = order.GiaoDichs
                        .Where(g => g.PhuongThucThanhToan?.TenPhuongThuc == "Tiền mặt");
                    cashReceived += cashTransactions.Sum(g => g.SoTien);
                }

                ExpectedCash = openShift.SoDuDauCa + cashReceived;
                ActualCashInput = ExpectedCash; // pre-fill with expected cash for convenience
            }
            else
            {
                SessionContext.CurrentShift = null;
                IsShiftOpen = false;
                OpeningFundInput = 2000000; // reset to default
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải dữ liệu ca làm việc: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task OpenShiftAsync()
    {
        try
        {
            var currentUser = SessionContext.CurrentUser;
            if (currentUser == null)
            {
                MessageBox.Show("Không tìm thấy thông tin nhân viên đăng nhập.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (OpeningFundInput < 0)
            {
                MessageBox.Show("Số dư đầu ca không thể là số âm.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newShift = new CaLamViec
            {
                NhanVienId = currentUser.Id,
                ThoiGianBatDau = DateTime.Now,
                SoDuDauCa = OpeningFundInput,
                TrangThai = "OPEN",
                GhiChu = "Mở ca đầu ngày"
            };

            _context.CaLamViecs.Add(newShift);
            await _context.SaveChangesAsync();

            SessionContext.CurrentShift = newShift;
            MessageBox.Show("Mở ca làm việc thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            
            await LoadShiftDataAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi mở ca làm việc: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task CloseShiftAsync()
    {
        try
        {
            var currentShift = SessionContext.CurrentShift;
            if (currentShift == null)
            {
                MessageBox.Show("Không tìm thấy ca làm việc đang mở.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ActualCashInput < 0)
            {
                MessageBox.Show("Số dư cuối ca thực tế không thể là số âm.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dbShift = await _context.CaLamViecs.FirstOrDefaultAsync(c => c.Id == currentShift.Id);
            if (dbShift == null)
            {
                MessageBox.Show("Không tìm thấy ca làm việc này trong cơ sở dữ liệu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            decimal chenhLech = ActualCashInput - ExpectedCash;

            dbShift.ThoiGianKetThuc = DateTime.Now;
            dbShift.SoDuCuoiCaThucTe = ActualCashInput;
            dbShift.TongDoanhThu = SystemSales;
            dbShift.ChenhLech = chenhLech;
            dbShift.GhiChu = NoteInput.Trim();
            dbShift.TrangThai = "CLOSED";

            _context.CaLamViecs.Update(dbShift);
            await _context.SaveChangesAsync();

            SessionContext.CurrentShift = null;

            // Trigger warning dialog if there is a difference [FR-06]
            if (chenhLech != 0)
            {
                string message = chenhLech > 0
                    ? $"Cảnh báo: Phát hiện chênh lệch thừa quỹ cuối ca!\nSố tiền thừa: +{chenhLech:N0}đ.\nVui lòng kiểm tra lại tiền mặt."
                    : $"Cảnh báo: Phát hiện chênh lệch thiếu quỹ cuối ca!\nSố tiền thiếu hụt: {chenhLech:N0}đ.\nVui lòng đối chiếu hóa đơn bán hàng.";

                MessageBox.Show(message, "Cảnh báo chênh lệch quỹ cuối ca", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("Đóng ca làm việc thành công! Quỹ tiền mặt hoàn toàn khớp.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            // Reset inputs
            NoteInput = string.Empty;
            await LoadShiftDataAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi đóng ca làm việc: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
