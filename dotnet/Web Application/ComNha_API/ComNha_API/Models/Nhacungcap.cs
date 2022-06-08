using System;
using System.Collections.Generic;

namespace ComNha_API.Models
{
    public partial class Nhacungcap
    {
        public Nhacungcap()
        {
            Phieunhaps = new HashSet<Phieunhap>();
        }

        public string MaNhaCungCap { get; set; } = null!;
        public string MoTaNhaCungCap { get; set; } = null!;
        public string TenNhaCungCap { get; set; } = null!;
        public string DiaChi { get; set; } = null!;
        public string SoDienThoai { get; set; } = null!;

        public virtual ICollection<Phieunhap> Phieunhaps { get; set; }
    }
}
