using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Enums;

namespace HeThongPOS.Core.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<DonHang>> GetAllAsync(DateTime? fromDate = null, DateTime? toDate = null, OrderStatus? status = null);
    Task<DonHang?> GetByIdWithItemsAsync(int id);
}
