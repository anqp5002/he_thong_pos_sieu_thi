using System.Collections.Generic;

namespace HeThongPOS.Core.Entities;

public class VaiTro
{
    public int Id { get; set; }
    public string TenVaiTro { get; set; } = string.Empty;
    public string MoTa { get; set; } = string.Empty;

    public ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
}
