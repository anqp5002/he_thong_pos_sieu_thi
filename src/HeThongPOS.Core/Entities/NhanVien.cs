using System;
using System.Collections.Generic;

namespace HeThongPOS.Core.Entities;

public class NhanVien
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string HoTen { get; set; } = string.Empty;
    public int VaiTroId { get; set; }
    public bool TrangThai { get; set; } = true;
    public DateTime NgayTao { get; set; } = DateTime.Now;

    public VaiTro VaiTro { get; set; } = null!;
    public ICollection<CaLamViec> CaLamViecs { get; set; } = new List<CaLamViec>();
    public ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
