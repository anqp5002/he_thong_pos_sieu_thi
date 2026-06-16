using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HeThongPOS.Core.Entities;
using HeThongPOS.Infrastructure.Data;

namespace HeThongPOS.WPF.Services;

/// <summary>
/// Service kiểm tra và cảnh báo tồn kho thấp.
/// Mô phỏng component StockAlert của Web POS.
/// </summary>
public class StockAlertService
{
    private readonly AppDbContext _context;
    private const int DefaultMinStock = 10;

    public StockAlertService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Lấy danh sách sản phẩm có tồn kho dưới mức tối thiểu
    /// </summary>
    public async Task<List<SanPham>> GetLowStockProductsAsync(int minStock = DefaultMinStock)
    {
        return await _context.SanPhams
            .Where(sp => sp.TrangThai && sp.TonKho <= minStock)
            .OrderBy(sp => sp.TonKho)
            .Take(20)
            .ToListAsync();
    }

    /// <summary>
    /// Đếm số sản phẩm có tồn kho thấp
    /// </summary>
    public async Task<int> CountLowStockAsync(int minStock = DefaultMinStock)
    {
        return await _context.SanPhams
            .CountAsync(sp => sp.TrangThai && sp.TonKho <= minStock);
    }
}
