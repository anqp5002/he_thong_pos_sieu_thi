using System.Collections.Generic;
using System.Threading.Tasks;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.Core.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<SanPham>> GetAllAsync();
    Task<IEnumerable<SanPham>> SearchAsync(string keyword, int? categoryId);
    Task<SanPham?> GetByIdAsync(int id);
    Task<SanPham?> GetByBarcodeAsync(string barcode);
    Task AddAsync(SanPham product);
    Task UpdateAsync(SanPham product);
    Task DeleteAsync(int id);
    Task<IEnumerable<DanhMuc>> GetCategoriesAsync();
}
