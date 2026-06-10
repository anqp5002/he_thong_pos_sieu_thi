using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Interfaces;
using HeThongPOS.WPF.Controls;

namespace HeThongPOS.WPF.ViewModels;

public partial class ProductsViewModel : ObservableObject
{
    private readonly IProductService _productService;

    [ObservableProperty]
    private ObservableCollection<SanPham> _products = new();

    [ObservableProperty]
    private ObservableCollection<DanhMuc> _categories = new();

    [ObservableProperty]
    private string _searchKeyword = string.Empty;

    [ObservableProperty]
    private DanhMuc? _selectedCategory;

    [ObservableProperty]
    private SanPham? _selectedProduct;

    public ProductsViewModel(IProductService productService)
    {
        _productService = productService;
        LoadDataCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        try
        {
            var categories = await _productService.GetCategoriesAsync();
            Categories.Clear();
            Categories.Add(new DanhMuc { Id = 0, TenDanhMuc = "Tất cả danh mục" });
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
            SelectedCategory = Categories.First();

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
            int? categoryId = SelectedCategory?.Id > 0 ? SelectedCategory.Id : null;
            var products = await _productService.SearchProductsAsync(SearchKeyword, categoryId);
            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private async Task AddProductAsync()
    {
        var dialog = new ProductFormDialog();
        var vm = new ProductFormViewModel(_productService, null);
        vm.CloseAction = (result) => dialog.DialogResult = result;
        dialog.DataContext = vm;

        if (dialog.ShowDialog() == true)
        {
            await SearchAsync();
        }
    }

    [RelayCommand]
    private async Task EditProductAsync()
    {
        if (SelectedProduct == null)
        {
            MessageBox.Show("Vui lòng chọn sản phẩm để sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var dialog = new ProductFormDialog();
        var vm = new ProductFormViewModel(_productService, SelectedProduct);
        vm.CloseAction = (result) => dialog.DialogResult = result;
        dialog.DataContext = vm;

        if (dialog.ShowDialog() == true)
        {
            await SearchAsync();
        }
    }

    [RelayCommand]
    private async Task DeleteProductAsync()
    {
        if (SelectedProduct == null)
        {
            MessageBox.Show("Vui lòng chọn sản phẩm để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var result = MessageBox.Show($"Bạn có chắc muốn xóa sản phẩm '{SelectedProduct.TenSanPham}'?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            try
            {
                await _productService.DeleteProductAsync(SelectedProduct.Id);
                await SearchAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa sản phẩm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
