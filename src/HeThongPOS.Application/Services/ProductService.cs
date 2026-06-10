using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Interfaces;

namespace HeThongPOS.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<SanPham>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<IEnumerable<SanPham>> SearchProductsAsync(string keyword, int? categoryId)
    {
        return await _productRepository.SearchAsync(keyword, categoryId);
    }

    public async Task<SanPham?> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task<SanPham?> GetProductByBarcodeAsync(string barcode)
    {
        return await _productRepository.GetByBarcodeAsync(barcode);
    }

    public async Task AddProductAsync(SanPham product)
    {
        ValidateProduct(product);
        
        var existing = await _productRepository.GetByBarcodeAsync(product.MaVach);
        if (existing != null)
        {
            throw new Exception("Mã vạch đã tồn tại trong hệ thống.");
        }

        await _productRepository.AddAsync(product);
    }

    public async Task UpdateProductAsync(SanPham product)
    {
        ValidateProduct(product);

        var existing = await _productRepository.GetByBarcodeAsync(product.MaVach);
        if (existing != null && existing.Id != product.Id)
        {
            throw new Exception("Mã vạch đã tồn tại cho một sản phẩm khác.");
        }

        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<DanhMuc>> GetCategoriesAsync()
    {
        return await _productRepository.GetCategoriesAsync();
    }

    private void ValidateProduct(SanPham product)
    {
        if (string.IsNullOrWhiteSpace(product.TenSanPham))
        {
            throw new Exception("Tên sản phẩm không được để trống.");
        }

        if (product.GiaBan < 0)
        {
            throw new Exception("Giá bán phải lớn hơn hoặc bằng 0.");
        }

        if (product.TonKho < 0)
        {
            throw new Exception("Tồn kho phải lớn hơn hoặc bằng 0.");
        }
        
        if (string.IsNullOrWhiteSpace(product.MaVach))
        {
            throw new Exception("Mã vạch không được để trống.");
        }
    }
}
