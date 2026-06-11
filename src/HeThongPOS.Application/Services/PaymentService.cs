using System;
using System.Threading.Tasks;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Enums;
using HeThongPOS.Core.Interfaces;
using HeThongPOS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HeThongPOS.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly AppDbContext _context;

    public PaymentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ProcessPaymentAsync(int donHangId, PaymentMethod paymentMethod, decimal soTien, string maGiaoDich = "")
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var order = await _context.DonHangs
                .Include(o => o.ChiTietDonHangs)
                .FirstOrDefaultAsync(o => o.Id == donHangId);

            if (order == null)
                throw new Exception($"Không tìm thấy đơn hàng ID {donHangId}");

            if (order.TrangThai != OrderStatus.Pending)
                throw new Exception("Chỉ có thể thanh toán đơn hàng ở trạng thái chờ (Pending).");

            if (soTien < order.TongThanhToan)
                throw new Exception("Số tiền thanh toán không đủ.");

            // Ghi nhận giao dịch
            var giaoDich = new GiaoDich
            {
                DonHangId = donHangId,
                PhuongThucThanhToanId = (int)paymentMethod,
                SoTien = soTien, 
                NgayGiaoDich = DateTime.Now,
                TrangThai = "SUCCESS",
                MaGiaoDichDoiTac = maGiaoDich
            };

            await _context.GiaoDichs.AddAsync(giaoDich);

            // Cập nhật trạng thái đơn hàng
            order.TrangThai = OrderStatus.Completed;
            _context.DonHangs.Update(order);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception($"Lỗi thanh toán: {ex.Message}", ex);
        }
    }
}
