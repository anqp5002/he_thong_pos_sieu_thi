using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Interfaces;

namespace HeThongPOS.WPF.ViewModels;

public partial class ProductFormViewModel : ObservableObject
{
    private readonly IProductService _productService;
    private readonly SanPham? _editingProduct;

    [ObservableProperty]
    private SanPham _product;

    [ObservableProperty]
    private ObservableCollection<DanhMuc> _categories = new();

    public string Title => _editingProduct == null ? "Thêm Sản Phẩm" : "Sửa Sản Phẩm";

    public Action<bool>? CloseAction { get; set; }

    public ProductFormViewModel(IProductService productService, SanPham? editingProduct)
    {
        _productService = productService;
        _editingProduct = editingProduct;

        if (_editingProduct != null)
        {
            Product = new SanPham
            {
                Id = _editingProduct.Id,
                TenSanPham = _editingProduct.TenSanPham,
                MaVach = _editingProduct.MaVach,
                GiaBan = _editingProduct.GiaBan,
                TonKho = _editingProduct.TonKho,
                DonViTinh = _editingProduct.DonViTinh,
                DanhMucId = _editingProduct.DanhMucId,
                TrangThai = _editingProduct.TrangThai,
                HinhAnhUrl = _editingProduct.HinhAnhUrl
            };
        }
        else
        {
            Product = new SanPham();
        }

        LoadCategoriesCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadCategoriesAsync()
    {
        var cats = await _productService.GetCategoriesAsync();
        Categories.Clear();
        foreach (var c in cats) Categories.Add(c);
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(Product.HinhAnhUrl))
            {
                Product.HinhAnhUrl = "pack://application:,,,/Images/default-product.png";
            }

            if (_editingProduct == null)
            {
                await _productService.AddProductAsync(Product);
            }
            else
            {
                await _productService.UpdateProductAsync(Product);
            }
            
            CloseAction?.Invoke(true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        CloseAction?.Invoke(false);
    }

    [RelayCommand]
    private void SelectImage()
    {
        var dialog = new Microsoft.Win32.OpenFileDialog
        {
            Title = "Chọn ảnh sản phẩm",
            Filter = "Image Files (*.jpg;*.jpeg;*.png;*.webp)|*.jpg;*.jpeg;*.png;*.webp|All Files (*.*)|*.*"
        };

        if (dialog.ShowDialog() == true)
        {
            try
            {
                string imagesDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                if (!System.IO.Directory.Exists(imagesDir))
                {
                    System.IO.Directory.CreateDirectory(imagesDir);
                }

                string ext = System.IO.Path.GetExtension(dialog.FileName);
                string newFileName = Guid.NewGuid().ToString("N") + ext;
                string newFilePath = System.IO.Path.Combine(imagesDir, newFileName);

                System.IO.File.Copy(dialog.FileName, newFilePath, true);

                Product.HinhAnhUrl = newFilePath;
                OnPropertyChanged(nameof(Product));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi copy ảnh: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
