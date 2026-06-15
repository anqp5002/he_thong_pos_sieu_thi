using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Interfaces;
using HeThongPOS.WPF.Controls;

namespace HeThongPOS.WPF.ViewModels;

public partial class CustomersViewModel : ObservableObject
{
    private readonly ICustomerService _customerService;

    [ObservableProperty]
    private ObservableCollection<KhachHang> _customers = new();

    [ObservableProperty]
    private string _searchKeyword = string.Empty;

    [ObservableProperty]
    private KhachHang? _selectedCustomer;

    public CustomersViewModel(ICustomerService customerService)
    {
        _customerService = customerService;
        LoadDataCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        try
        {
            await SearchAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        try
        {
            var customers = await _customerService.SearchCustomersAsync(SearchKeyword);
            Customers.Clear();
            foreach (var customer in customers)
            {
                Customers.Add(customer);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task AddCustomerAsync()
    {
        var dialog = new CustomerFormDialog();
        var vm = new CustomerFormViewModel(_customerService, null);
        vm.CloseAction = (result) => dialog.DialogResult = result;
        dialog.DataContext = vm;

        if (dialog.ShowDialog() == true)
        {
            await SearchAsync();
        }
    }

    [RelayCommand]
    private async Task EditCustomerAsync()
    {
        if (SelectedCustomer == null)
        {
            MessageBox.Show("Vui lòng chọn khách hàng để sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var dialog = new CustomerFormDialog();
        var vm = new CustomerFormViewModel(_customerService, SelectedCustomer);
        vm.CloseAction = (result) => dialog.DialogResult = result;
        dialog.DataContext = vm;

        if (dialog.ShowDialog() == true)
        {
            await SearchAsync();
        }
    }

    [RelayCommand]
    private async Task DeleteCustomerAsync()
    {
        if (SelectedCustomer == null)
        {
            MessageBox.Show("Vui lòng chọn khách hàng để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var result = MessageBox.Show($"Bạn có chắc muốn xóa khách hàng '{SelectedCustomer.HoTen}'?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            try
            {
                var success = await _customerService.DeleteCustomerAsync(SelectedCustomer.Id);
                if (success)
                {
                    await SearchAsync();
                }
                else
                {
                    MessageBox.Show("Không thể xóa khách hàng này.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa khách hàng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
