using System.Collections.Generic;
using System.Threading.Tasks;
using HeThongPOS.Core.Entities;

namespace HeThongPOS.Core.Interfaces;

public interface IOrderService
{
    Task<DonHang> CreateOrderAsync(int nhanVienId, int? khachHangId, List<ChiTietDonHang> items, string ghiChu = "");
    void CalculateOrderTotals(DonHang order);
}
