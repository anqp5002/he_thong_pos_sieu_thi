using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Enums;
using HeThongPOS.Core.Interfaces;
using HeThongPOS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HeThongPOS.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly AppDbContext _context;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, AppDbContext context)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _context = context;
    }

    public void CalculateOrderTotals(DonHang order)
    {
        order.TongTienHang = order.ChiTietDonHangs.Sum(x => x.DonGia * x.SoLuong);
        // Giả sử VAT 10% nếu hệ thống có yêu cầu (tuỳ FR), ở đây tính đơn giản: 0
        // order.ThueVAT = order.TongTienHang * 0.1m; 
        
        order.TongThanhToan = order.TongTienHang + order.ThueVAT - order.ChietKhau;
        
        foreach(var item in order.ChiTietDonHangs)
        {
            item.ThanhTien = item.DonGia * item.SoLuong;
        }
    }

    public async Task<DonHang> CreateOrderAsync(int nhanVienId, int? khachHangId, List<ChiTietDonHang> items, string ghiChu = "")
    {
        if (items == null || !items.Any())
        {
            throw new Exception("Đơn hàng phải có ít nhất 1 sản phẩm.");
        }

        // Validate products and stock
        foreach (var item in items)
        {
            if (item.SoLuong <= 0)
                throw new Exception("Số lượng sản phẩm phải lớn hơn 0.");

            var product = await _productRepository.GetByIdAsync(item.SanPhamId);
            if (product == null)
            {
                throw new Exception($"Không tìm thấy sản phẩm có ID {item.SanPhamId}");
            }

            if (!product.TrangThai)
            {
                throw new Exception($"Sản phẩm '{product.TenSanPham}' đã ngừng kinh doanh.");
            }

            if (product.TonKho < item.SoLuong)
            {
                throw new Exception($"Sản phẩm '{product.TenSanPham}' không đủ tồn kho (Còn lại: {product.TonKho}).");
            }

            // Deduct stock
            product.TonKho -= item.SoLuong;
            await _productRepository.UpdateAsync(product);

            // Assign unit price from current product price
            item.DonGia = product.GiaBan;
        }

        var order = new DonHang
        {
            MaDonHang = $"HD{DateTime.Now:yyyyMMddHHmmss}",
            NhanVienId = nhanVienId,
            KhachHangId = khachHangId,
            NgayTao = DateTime.Now,
            TrangThai = OrderStatus.Pending,
            GhiChu = ghiChu,
            ChiTietDonHangs = items
        };

        CalculateOrderTotals(order);

        await _orderRepository.AddAsync(order);
        return order;
    }

    public async Task<bool> CancelOrderAsync(int donHangId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var order = await _context.DonHangs
                .Include(o => o.ChiTietDonHangs)
                .FirstOrDefaultAsync(o => o.Id == donHangId);

            if (order == null)
                throw new Exception($"Không tìm thấy đơn hàng ID {donHangId}");

            if (order.TrangThai == OrderStatus.Cancelled)
                throw new Exception("Đơn hàng đã bị hủy trước đó.");

            // Khôi phục tồn kho
            foreach (var item in order.ChiTietDonHangs)
            {
                var product = await _productRepository.GetByIdAsync(item.SanPhamId);
                if (product != null)
                {
                    product.TonKho += item.SoLuong;
                    await _productRepository.UpdateAsync(product);
                }
            }

            // Đổi trạng thái
            order.TrangThai = OrderStatus.Cancelled;
            _context.DonHangs.Update(order);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new Exception($"Lỗi khi hủy đơn hàng: {ex.Message}", ex);
        }
    }
}
