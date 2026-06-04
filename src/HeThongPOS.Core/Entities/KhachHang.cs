using System;
using System.Collections.Generic;

namespace HeThongPOS.Core.Entities;

public class KhachHang
{
    public int Id { get; set; }
    public string HoTen { get; set; } = string.Empty;
    public string SoDienThoai { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int DiemTichLuy { get; set; }
    public DateTime NgayTao { get; set; } = DateTime.Now;

    public ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
