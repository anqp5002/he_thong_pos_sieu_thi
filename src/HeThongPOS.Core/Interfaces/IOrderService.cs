using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HeThongPOS.Core.Entities;
using HeThongPOS.Core.Enums;

namespace HeThongPOS.Core.Interfaces;

public interface IOrderService
{
    // Read methods for Dev C (Sprint 2)
    Task<IEnumerable<DonHang>> GetOrdersAsync(DateTime? fromDate = null, DateTime? toDate = null, OrderStatus? status = null);
    Task<DonHang?> GetOrderDetailsAsync(int id);
    
    // Dev B will add Create/Calc methods here later (Task 2.5, 2.6)
}
