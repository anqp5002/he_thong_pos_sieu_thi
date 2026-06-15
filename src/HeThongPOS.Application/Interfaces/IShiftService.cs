using System.Threading.Tasks;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.Application.Interfaces;

public interface IShiftService
{
    /// <summary>
    /// Mở ca làm việc mới cho nhân viên.
    /// </summary>
    Task<(bool IsSuccess, string Message, CaLamViec? Shift)> OpenShiftAsync(int nhanVienId, decimal soDuDauCa);

    /// <summary>
    /// Đóng ca làm việc hiện tại, tính toán chênh lệch.
    /// </summary>
    Task<(bool IsSuccess, string Message, CaLamViec? Shift)> CloseShiftAsync(int shiftId, decimal soDuCuoiCaThucTe, string ghiChu);

    /// <summary>
    /// Lấy ca làm việc đang mở của nhân viên (nếu có).
    /// </summary>
    Task<CaLamViec?> GetActiveShiftAsync(int nhanVienId);

    /// <summary>
    /// Tính tổng doanh thu trong ca (tổng đơn hàng COMPLETED trong khoảng thời gian ca).
    /// </summary>
    Task<decimal> CalculateShiftRevenueAsync(int shiftId);
}
