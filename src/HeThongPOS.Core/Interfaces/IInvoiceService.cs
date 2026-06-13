using HeThongPOS.Core.Entities;

namespace HeThongPOS.Core.Interfaces;

public interface IInvoiceService
{
    /// <summary>
    /// Generates a visual layout for the invoice. Returns a FlowDocument (as object to keep Core decoupled from WPF).
    /// </summary>
    object GenerateInvoiceDocument(DonHang order, decimal cashReceived, decimal changeAmount);
}
