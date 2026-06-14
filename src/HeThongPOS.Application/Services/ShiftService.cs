using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HeThongPOS.Application.Interfaces;
using HeThongPOS.Infrastructure.Data;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Enums;

namespace HeThongPOS.Application.Services;

public class ShiftService : IShiftService
{
    private readonly AppDbContext _context;

    public ShiftService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(bool IsSuccess, string Message, CaLamViec? Shift)> OpenShiftAsync(int nhanVienId, decimal soDuDauCa)
    {
        // Kiểm tra xem nhân viên có đang mở ca nào chưa
        var activeShift = await GetActiveShiftAsync(nhanVienId);
        if (activeShift != null)
        {
            return (false, "Nhân viên đang có ca làm việc chưa đóng. Vui lòng đóng ca trước.", null);
        }

        // Kiểm tra số dư đầu ca hợp lệ
        if (soDuDauCa < 0)
        {
            return (false, "Số dư đầu ca không được âm.", null);
        }

        var shift = new CaLamViec
        {
            NhanVienId = nhanVienId,
            SoDuDauCa = soDuDauCa,
            ThoiGianBatDau = DateTime.Now,
            TrangThai = "OPEN"
        };

        _context.CaLamViecs.Add(shift);
        await _context.SaveChangesAsync();

        return (true, "Mở ca thành công.", shift);
    }

    public async Task<(bool IsSuccess, string Message, CaLamViec? Shift)> CloseShiftAsync(int shiftId, decimal soDuCuoiCaThucTe, string ghiChu)
    {
        var shift = await _context.CaLamViecs
            .Include(c => c.NhanVien)
            .FirstOrDefaultAsync(c => c.Id == shiftId);

        if (shift == null)
        {
            return (false, "Không tìm thấy ca làm việc.", null);
        }

        if (shift.TrangThai == "CLOSED")
        {
            return (false, "Ca làm việc đã được đóng trước đó.", null);
        }

        // Tính tổng doanh thu trong ca
        decimal tongDoanhThu = await CalculateShiftRevenueAsync(shiftId);

        // Tính số dư cuối ca theo hệ thống = Số dư đầu ca + Tổng doanh thu
        decimal soDuCuoiCaHeThong = shift.SoDuDauCa + tongDoanhThu;

        // Chênh lệch = Thực tế - Hệ thống
        decimal chenhLech = soDuCuoiCaThucTe - soDuCuoiCaHeThong;

        shift.ThoiGianKetThuc = DateTime.Now;
        shift.SoDuCuoiCaThucTe = soDuCuoiCaThucTe;
        shift.TongDoanhThu = tongDoanhThu;
        shift.ChenhLech = chenhLech;
        shift.GhiChu = ghiChu;
        shift.TrangThai = "CLOSED";

        await _context.SaveChangesAsync();

        // Tạo cảnh báo nếu chênh lệch lớn
        string message = "Đóng ca thành công.";
        if (Math.Abs(chenhLech) > 0)
        {
            string loai = chenhLech > 0 ? "THỪA" : "THIẾU";
            message += $" Cảnh báo: Tiền {loai} {Math.Abs(chenhLech):#,##0} đ so với hệ thống.";
        }

        return (true, message, shift);
    }

    public async Task<CaLamViec?> GetActiveShiftAsync(int nhanVienId)
    {
        return await _context.CaLamViecs
            .Include(c => c.NhanVien)
            .FirstOrDefaultAsync(c => c.NhanVienId == nhanVienId && c.TrangThai == "OPEN");
    }

    public async Task<decimal> CalculateShiftRevenueAsync(int shiftId)
    {
        var shift = await _context.CaLamViecs.FindAsync(shiftId);
        if (shift == null) return 0;

        // Tính tổng doanh thu = Tổng tiền các đơn hàng COMPLETED trong khoảng thời gian ca
        var endTime = shift.ThoiGianKetThuc ?? DateTime.Now;

        return await _context.DonHangs
            .Where(d => d.NhanVienId == shift.NhanVienId
                     && d.NgayTao >= shift.ThoiGianBatDau
                     && d.NgayTao <= endTime
                     && d.TrangThai == OrderStatus.Completed)
            .SumAsync(d => d.TongThanhToan);
    }
}
