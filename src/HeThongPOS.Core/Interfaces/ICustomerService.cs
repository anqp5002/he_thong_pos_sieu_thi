using System.Collections.Generic;
using System.Threading.Tasks;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.Core.Interfaces;

public interface ICustomerService
{
    Task<IEnumerable<KhachHang>> GetAllCustomersAsync();
    Task<IEnumerable<KhachHang>> SearchCustomersAsync(string keyword);
    Task<KhachHang?> GetCustomerByIdAsync(int id);
    Task<(bool Success, string ErrorMessage)> CreateCustomerAsync(KhachHang customer);
    Task<(bool Success, string ErrorMessage)> UpdateCustomerAsync(KhachHang customer);
    Task<bool> DeleteCustomerAsync(int id);
}
