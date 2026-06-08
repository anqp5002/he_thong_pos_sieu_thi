using System.Threading.Tasks;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.Application.Interfaces;

public interface IAuthService
{
    Task<(bool IsSuccess, string ErrorMessage, NhanVien? User)> LoginAsync(string username, string password);
}
