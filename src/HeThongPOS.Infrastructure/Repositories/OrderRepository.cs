using System.Threading.Tasks;
using HeThongPOS.Core.Entities;
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

    public async Task AddAsync(DonHang order)
    {
        await _context.DonHangs.AddAsync(order);
        await _context.SaveChangesAsync();
    }
}
