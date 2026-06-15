using System.Collections.Generic;
using System.Threading.Tasks;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.Core.Interfaces;

public interface ICustomerRepository
{
    Task<KhachHang?> GetByIdAsync(int id);
    Task<IEnumerable<KhachHang>> GetAllAsync();
    Task<IEnumerable<KhachHang>> SearchAsync(string keyword);
    Task<KhachHang?> GetByEmailAsync(string email);
    Task<KhachHang?> GetByPhoneAsync(string phone);
    Task AddAsync(KhachHang customer);
    Task UpdateAsync(KhachHang customer);
    Task DeleteAsync(int id);
}
