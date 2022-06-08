using System;
using System.Collections.Generic;

namespace ComNha_API.Models
{
    public partial class Khachhang
    {
        public Khachhang()
        {
            Hoadons = new HashSet<Hoadon>();
            Lichhens = new HashSet<Lichhen>();
        }

        public int MaKhachHang { get; set; }
        public string SoDienThoai { get; set; } = null!;
        public string TenKhachHang { get; set; } = null!;
        public string? DiaChi { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Hoadon> Hoadons { get; set; }
        public virtual ICollection<Lichhen> Lichhens { get; set; }
    }
}
