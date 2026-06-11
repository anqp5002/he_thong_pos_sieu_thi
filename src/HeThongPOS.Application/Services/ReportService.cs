using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeThongPOS.Core.Enums;
using HeThongPOS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HeThongPOS.Application.Services;

public class DailyRevenueDto
{
    public DateTime Date { get; set; }
    public decimal Revenue { get; set; }
}

public class TopProductDto
{
    public string TenSanPham { get; set; } = string.Empty;
    public int SoLuongBan { get; set; }
    public decimal DoanhThu { get; set; }
}

public class ReportService
{
    private readonly AppDbContext _context;

    public ReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<decimal> GetTotalRevenueAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.DonHangs
            .Where(o => o.TrangThai == OrderStatus.Completed && o.NgayTao >= startDate && o.NgayTao <= endDate)
            .SumAsync(o => o.TongThanhToan);
    }
    
    public async Task<int> GetTotalOrdersAsync(DateTime startDate, DateTime endDate)
    {
        return await _context.DonHangs
            .Where(o => o.TrangThai == OrderStatus.Completed && o.NgayTao >= startDate && o.NgayTao <= endDate)
            .CountAsync();
    }

    public async Task<List<DailyRevenueDto>> GetRevenueByDayAsync(DateTime startDate, DateTime endDate)
    {
        var orders = await _context.DonHangs
            .Where(o => o.TrangThai == OrderStatus.Completed && o.NgayTao >= startDate && o.NgayTao <= endDate)
            .Select(o => new { o.NgayTao.Date, o.TongThanhToan })
            .ToListAsync();

        return orders
            .GroupBy(o => o.Date)
            .Select(g => new DailyRevenueDto
            {
                Date = g.Key,
                Revenue = g.Sum(o => o.TongThanhToan)
            })
            .OrderBy(r => r.Date)
            .ToList();
    }

    public async Task<List<TopProductDto>> GetTopProductsAsync(DateTime startDate, DateTime endDate, int limit = 10)
    {
        var items = await _context.ChiTietDonHangs
            .Include(c => c.DonHang)
            .Include(c => c.SanPham)
            .Where(c => c.DonHang.TrangThai == OrderStatus.Completed && c.DonHang.NgayTao >= startDate && c.DonHang.NgayTao <= endDate)
            .ToListAsync();

        return items
            .GroupBy(c => c.SanPham)
            .Select(g => new TopProductDto
            {
                TenSanPham = g.Key.TenSanPham,
                SoLuongBan = g.Sum(c => c.SoLuong),
                DoanhThu = g.Sum(c => c.ThanhTien)
            })
            .OrderByDescending(p => p.SoLuongBan)
            .Take(limit)
            .ToList();
    }
}
