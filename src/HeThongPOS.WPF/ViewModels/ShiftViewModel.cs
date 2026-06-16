using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeThongPOS.Application.Interfaces;
using HeThongPOS.Core.Entities;
using HeThongPOS.WPF.Services;

namespace HeThongPOS.WPF.ViewModels;

public partial class ShiftViewModel : ObservableObject
{
    private readonly IShiftService _shiftService;
    private readonly SessionManager _sessionManager;

    // --- Trạng thái ca ---
    [ObservableProperty]
    private bool _hasActiveShift;

    [ObservableProperty]
    private CaLamViec? _activeShift;

    // --- Mở ca ---
    [ObservableProperty]
    private decimal _soDuDauCa;

    // --- Đóng ca ---
    [ObservableProperty]
    private decimal _soDuCuoiCaThucTe;

    [ObservableProperty]
    private string _ghiChu = string.Empty;

    [ObservableProperty]
    private decimal _tongDoanhThuCa;

    [ObservableProperty]
    private decimal _soDuCuoiCaHeThong;

    [ObservableProperty]
    private decimal _chenhLech;

    // --- Thông báo ---
    [ObservableProperty]
    private string _message = string.Empty;

    [ObservableProperty]
    private bool _isSuccess;

    [ObservableProperty]
    private bool _isBusy;

    // --- Thời gian hiển thị ---
    [ObservableProperty]
    private string _thoiGianBatDau = string.Empty;

    [ObservableProperty]
    private string _thoiGianLamViec = string.Empty;

    public ShiftViewModel(IShiftService shiftService, SessionManager sessionManager)
    {
        _shiftService = shiftService;
        _sessionManager = sessionManager;

        // Tự động kiểm tra ca khi ViewModel được tạo (lấy ID từ SessionManager)
        _ = CheckActiveShiftAsync();
    }

    /// <summary>
    /// Lấy ID nhân viên hiện tại từ SessionManager
    /// </summary>
    private int CurrentNhanVienId => _sessionManager.CurrentUser?.Id ?? 0;

    /// <summary>
    /// Kiểm tra xem nhân viên có đang mở ca nào không.
    /// </summary>
    [RelayCommand]
    private async Task CheckActiveShiftAsync()
    {
        try
        {
            if (CurrentNhanVienId == 0) return;

            ActiveShift = await _shiftService.GetActiveShiftAsync(CurrentNhanVienId);
            HasActiveShift = ActiveShift != null;

            if (HasActiveShift && ActiveShift != null)
            {
                ThoiGianBatDau = ActiveShift.ThoiGianBatDau.ToString("HH:mm dd/MM/yyyy");

                var duration = DateTime.Now - ActiveShift.ThoiGianBatDau;
                ThoiGianLamViec = $"{(int)duration.TotalHours} giờ {duration.Minutes} phút";

                // Tải doanh thu hiện tại của ca
                TongDoanhThuCa = await _shiftService.CalculateShiftRevenueAsync(ActiveShift.Id);
                SoDuCuoiCaHeThong = ActiveShift.SoDuDauCa + TongDoanhThuCa;
            }
        }
        catch (Exception ex)
        {
            Message = $"Lỗi kiểm tra ca: {ex.Message}";
            IsSuccess = false;
        }
    }

    /// <summary>
    /// Mở ca mới.
    /// </summary>
    [RelayCommand]
    private async Task OpenShiftAsync()
    {
        if (IsBusy) return;

        if (CurrentNhanVienId == 0)
        {
            Message = "Lỗi: Không xác định được nhân viên đăng nhập.";
            IsSuccess = false;
            return;
        }

        if (SoDuDauCa < 0)
        {
            Message = "Số dư đầu ca không được âm.";
            IsSuccess = false;
            return;
        }

        IsBusy = true;
        Message = string.Empty;

        try
        {
            var (isSuccess, message, shift) = await _shiftService.OpenShiftAsync(CurrentNhanVienId, SoDuDauCa);

            Message = message;
            IsSuccess = isSuccess;

            if (isSuccess)
            {
                ActiveShift = shift;
                HasActiveShift = true;
                ThoiGianBatDau = shift!.ThoiGianBatDau.ToString("HH:mm dd/MM/yyyy");
                ThoiGianLamViec = "0 giờ 0 phút";
                TongDoanhThuCa = 0;
                SoDuCuoiCaHeThong = SoDuDauCa;
            }
        }
        catch (Exception ex)
        {
            Message = $"Lỗi mở ca: {ex.Message}";
            IsSuccess = false;
        }

        IsBusy = false;
    }

    /// <summary>
    /// Đóng ca hiện tại.
    /// </summary>
    [RelayCommand]
    private async Task CloseShiftAsync()
    {
        if (IsBusy || ActiveShift == null) return;

        IsBusy = true;
        Message = string.Empty;

        try
        {
            var (isSuccess, message, shift) = await _shiftService.CloseShiftAsync(
                ActiveShift.Id, SoDuCuoiCaThucTe, GhiChu);

            Message = message;
            IsSuccess = isSuccess;

            if (isSuccess && shift != null)
            {
                ChenhLech = shift.ChenhLech ?? 0;
                HasActiveShift = false;

                // Reset form
                SoDuDauCa = 0;
                SoDuCuoiCaThucTe = 0;
                GhiChu = string.Empty;
            }
        }
        catch (Exception ex)
        {
            Message = $"Lỗi đóng ca: {ex.Message}";
            IsSuccess = false;
        }

        IsBusy = false;
    }
}
