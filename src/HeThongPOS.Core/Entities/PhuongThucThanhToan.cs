using System.Collections.Generic;

namespace HeThongPOS.Core.Entities;

public class PhuongThucThanhToan
{
    public int Id { get; set; }
    public string TenPhuongThuc { get; set; } = string.Empty; // Tiền mặt, Thẻ, Chuyển khoản
    public bool TrangThai { get; set; } = true;

    public ICollection<GiaoDich> GiaoDichs { get; set; } = new List<GiaoDich>();
}
