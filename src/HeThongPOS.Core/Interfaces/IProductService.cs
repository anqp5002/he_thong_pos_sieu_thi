using System.Collections.Generic;
using System.Threading.Tasks;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.Core.Interfaces;

public interface IProductService
{
    Task<IEnumerable<SanPham>> GetAllProductsAsync();
    Task<IEnumerable<SanPham>> SearchProductsAsync(string keyword, int? categoryId);
    Task<SanPham?> GetProductByIdAsync(int id);
    Task<SanPham?> GetProductByBarcodeAsync(string barcode);
    Task AddProductAsync(SanPham product);
    Task UpdateProductAsync(SanPham product);
    Task DeleteProductAsync(int id);
    Task<IEnumerable<DanhMuc>> GetCategoriesAsync();
}
