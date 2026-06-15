using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Enums;
using HeThongPOS.Core.Interfaces;
using HeThongPOS.Infrastructure.Data;

namespace HeThongPOS.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DonHang>> GetAllAsync(DateTime? fromDate = null, DateTime? toDate = null, OrderStatus? status = null)
    {
        var query = _context.DonHangs
            .Include(o => o.KhachHang)
            .Include(o => o.NhanVien)
            .AsQueryable();

        if (fromDate.HasValue)
        {
            query = query.Where(o => o.NgayTao >= fromDate.Value.Date);
        }

        if (toDate.HasValue)
        {
            // Bao gồm đến hết ngày của toDate
            var endOfDay = toDate.Value.Date.AddDays(1).AddTicks(-1);
            query = query.Where(o => o.NgayTao <= endOfDay);
        }

        if (status.HasValue)
        {
            query = query.Where(o => o.TrangThai == status.Value);
        }

        return await query.OrderByDescending(o => o.NgayTao).ToListAsync();
    }

    public async Task<DonHang?> GetByIdWithItemsAsync(int id)
    {
        return await _context.DonHangs
            .Include(o => o.KhachHang)
            .Include(o => o.NhanVien)
            .Include(o => o.ChiTietDonHangs)
                .ThenInclude(c => c.SanPham)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}
