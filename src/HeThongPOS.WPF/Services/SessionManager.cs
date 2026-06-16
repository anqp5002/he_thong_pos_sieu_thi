using System;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.WPF.Services;

/// <summary>
/// Global Auth State - Lưu giữ thông tin nhân viên đăng nhập xuyên suốt ứng dụng.
/// Tương đương cơ chế NextAuth session bên Web POS.
/// Đăng ký dạng Singleton trong DI Container.
/// </summary>
public class SessionManager
{
    private NhanVien? _currentUser;

    /// <summary>
    /// Nhân viên đang đăng nhập hiện tại
    /// </summary>
    public NhanVien? CurrentUser
    {
        get => _currentUser;
        private set
        {
            _currentUser = value;
            OnSessionChanged?.Invoke();
        }
    }

    /// <summary>
    /// Kiểm tra đã đăng nhập chưa
    /// </summary>
    public bool IsLoggedIn => CurrentUser != null;

    /// <summary>
    /// Lấy tên hiển thị (viết tắt chữ cái đầu, giống userInitials bên Web)
    /// </summary>
    public string UserInitials => IsLoggedIn 
        ? CurrentUser!.HoTen.Substring(0, 1).ToUpper() 
        : "?";

    /// <summary>
    /// Lấy tên vai trò hiện tại
    /// </summary>
    public string RoleName => CurrentUser?.VaiTro?.TenVaiTro ?? "Unknown";

    /// <summary>
    /// Kiểm tra có phải Admin không (tương đương isAdmin bên Web POS layout.tsx)
    /// </summary>
    public bool IsAdmin => RoleName == "Admin";

    /// <summary>
    /// Event khi session thay đổi (đăng nhập/đăng xuất) - để UI lắng nghe và cập nhật
    /// </summary>
    public event Action? OnSessionChanged;

    /// <summary>
    /// Đăng nhập - lưu thông tin user vào session
    /// </summary>
    public void Login(NhanVien user)
    {
        CurrentUser = user;
    }

    /// <summary>
    /// Đăng xuất - xóa session
    /// </summary>
    public void Logout()
    {
        CurrentUser = null;
    }
}
