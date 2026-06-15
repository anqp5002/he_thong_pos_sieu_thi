using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Interfaces;
using HeThongPOS.Infrastructure.Data;

namespace HeThongPOS.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<KhachHang?> GetByIdAsync(int id)
    {
        return await _context.KhachHangs.FindAsync(id);
    }

    public async Task<IEnumerable<KhachHang>> GetAllAsync()
    {
        return await _context.KhachHangs.ToListAsync();
    }

    public async Task<IEnumerable<KhachHang>> SearchAsync(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return await GetAllAsync();

        var lowerKeyword = keyword.ToLower();
        return await _context.KhachHangs
            .Where(k => k.HoTen.ToLower().Contains(lowerKeyword) || 
                        k.SoDienThoai.Contains(keyword) ||
                        k.Email.ToLower().Contains(lowerKeyword))
            .ToListAsync();
    }

    public async Task<KhachHang?> GetByEmailAsync(string email)
    {
        return await _context.KhachHangs.FirstOrDefaultAsync(k => k.Email.ToLower() == email.ToLower());
    }

    public async Task<KhachHang?> GetByPhoneAsync(string phone)
    {
        return await _context.KhachHangs.FirstOrDefaultAsync(k => k.SoDienThoai == phone);
    }

    public async Task AddAsync(KhachHang customer)
    {
        await _context.KhachHangs.AddAsync(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(KhachHang customer)
    {
        _context.KhachHangs.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var customer = await GetByIdAsync(id);
        if (customer != null)
        {
            _context.KhachHangs.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}
