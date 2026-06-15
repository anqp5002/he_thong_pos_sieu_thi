using HeThongPOS.Core.Entities;

namespace HeThongPOS.WPF.Services;

public static class SessionContext
{
    /// <summary>
    /// Holds the currently logged-in user context.
    /// </summary>
    public static NhanVien? CurrentUser { get; set; }

    /// <summary>
    /// Holds the currently active shift context.
    /// </summary>
    public static CaLamViec? CurrentShift { get; set; }
}
