using System.Collections.Generic;

namespace HeThongPOS.Core.Entities;

public class DanhMuc
{
    public int Id { get; set; }
    public string TenDanhMuc { get; set; } = string.Empty;
    public string MoTa { get; set; } = string.Empty;

    public ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}
