using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Enums;
using HeThongPOS.Core.Interfaces;

namespace HeThongPOS.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<DonHang>> GetOrdersAsync(DateTime? fromDate = null, DateTime? toDate = null, OrderStatus? status = null)
    {
        return await _orderRepository.GetAllAsync(fromDate, toDate, status);
    }

    public async Task<DonHang?> GetOrderDetailsAsync(int id)
    {
        return await _orderRepository.GetByIdWithItemsAsync(id);
    }

    // Dev B will add CreateOrder() and calculate logic here in their tasks (2.5, 2.6).
}
