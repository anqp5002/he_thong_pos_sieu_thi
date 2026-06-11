using System.Threading.Tasks;
using HeThongPOS.Core.Enums;

namespace HeThongPOS.Core.Interfaces;

public interface IPaymentService
{
    Task<bool> ProcessPaymentAsync(int donHangId, PaymentMethod paymentMethod, decimal soTien, string maGiaoDich = "");
}
