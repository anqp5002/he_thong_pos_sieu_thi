using System;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Interfaces;

namespace HeThongPOS.WPF.ViewModels;

public partial class CustomerFormViewModel : ObservableObject
{
    private readonly ICustomerService _customerService;
    private readonly KhachHang? _editingCustomer;

    public Action<bool>? CloseAction { get; set; }

    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string _hoTen = string.Empty;

    [ObservableProperty]
    private string _soDienThoai = string.Empty;

    [ObservableProperty]
    private string _email = string.Empty;

    public CustomerFormViewModel(ICustomerService customerService, KhachHang? editingCustomer)
    {
        _customerService = customerService;
        _editingCustomer = editingCustomer;

        if (_editingCustomer != null)
        {
            Title = "Cập nhật Khách hàng";
            HoTen = _editingCustomer.HoTen;
            SoDienThoai = _editingCustomer.SoDienThoai;
            Email = _editingCustomer.Email;
        }
        else
        {
            Title = "Thêm mới Khách hàng";
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        try
        {
            var customer = new KhachHang
            {
                Id = _editingCustomer?.Id ?? 0,
                HoTen = HoTen,
                SoDienThoai = SoDienThoai,
                Email = Email,
                DiemTichLuy = _editingCustomer?.DiemTichLuy ?? 0,
                NgayTao = _editingCustomer?.NgayTao ?? DateTime.Now
            };

            (bool Success, string ErrorMessage) result;

            if (_editingCustomer == null)
            {
                result = await _customerService.CreateCustomerAsync(customer);
            }
            else
            {
                result = await _customerService.UpdateCustomerAsync(customer);
            }

            if (result.Success)
            {
                CloseAction?.Invoke(true);
            }
            else
            {
                MessageBox.Show(result.ErrorMessage, "Lỗi xác thực", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi lưu dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        CloseAction?.Invoke(false);
    }
}
