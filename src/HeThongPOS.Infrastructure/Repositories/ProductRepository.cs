using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Interfaces;
using HeThongPOS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HeThongPOS.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SanPham>> GetAllAsync()
    {
        return await _context.SanPhams
            .Include(sp => sp.DanhMuc)
            .ToListAsync();
    }

    public async Task<IEnumerable<SanPham>> SearchAsync(string keyword, int? categoryId)
    {
        var query = _context.SanPhams.Include(sp => sp.DanhMuc).AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(sp => sp.TenSanPham.Contains(keyword) || sp.MaVach.Contains(keyword));
        }

        if (categoryId.HasValue && categoryId.Value > 0)
        {
            query = query.Where(sp => sp.DanhMucId == categoryId.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<SanPham?> GetByIdAsync(int id)
    {
        return await _context.SanPhams
            .Include(sp => sp.DanhMuc)
            .FirstOrDefaultAsync(sp => sp.Id == id);
    }

    public async Task<SanPham?> GetByBarcodeAsync(string barcode)
    {
        return await _context.SanPhams
            .Include(sp => sp.DanhMuc)
            .FirstOrDefaultAsync(sp => sp.MaVach == barcode);
    }

    public async Task AddAsync(SanPham product)
    {
        await _context.SanPhams.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SanPham product)
    {
        _context.SanPhams.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.SanPhams.FindAsync(id);
        if (product != null)
        {
            _context.SanPhams.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<DanhMuc>> GetCategoriesAsync()
    {
        return await _context.DanhMucs.ToListAsync();
    }
}
