using System.Threading.Tasks;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.Core.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(DonHang order);
}
