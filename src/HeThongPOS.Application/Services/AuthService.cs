using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HeThongPOS.Application.Interfaces;
using HeThongPOS.Infrastructure.Data;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.Application.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private const int MaxFailedAttempts = 5;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool IsSuccess, string ErrorMessage, NhanVien? User)> LoginAsync(string username, string password)
    {
        var user = await _context.NhanViens
            .Include(n => n.VaiTro)
            .FirstOrDefaultAsync(n => n.Username == username);

        if (user == null)
        {
            return (false, "Tài khoản không tồn tại.", null);
        }

        if (!user.TrangThai)
        {
            return (false, "Tài khoản đã bị vô hiệu hóa.", null);
        }

        if (user.IsLocked)
        {
            return (false, "Tài khoản đã bị khóa do đăng nhập sai quá nhiều lần. Vui lòng liên hệ Admin.", null);
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

        if (!isPasswordValid)
        {
            user.FailedLoginAttempts++;
            if (user.FailedLoginAttempts >= MaxFailedAttempts)
            {
                user.IsLocked = true;
                await _context.SaveChangesAsync();
                return (false, $"Tài khoản đã bị khóa do đăng nhập sai {MaxFailedAttempts} lần.", null);
            }
            
            await _context.SaveChangesAsync();
            return (false, "Mật khẩu không chính xác.", null);
        }

        // Đăng nhập thành công, reset số lần sai
        user.FailedLoginAttempts = 0;
        await _context.SaveChangesAsync();

        return (true, string.Empty, user);
    }
}
