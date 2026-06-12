using HeThongPOS.Core.Entities;

namespace HeThongPOS.WPF.Services;

public static class SessionContext
{
    /// <summary>
    /// Holds the currently logged-in user context.
    /// </summary>
    public static NhanVien? CurrentUser { get; set; }
}
