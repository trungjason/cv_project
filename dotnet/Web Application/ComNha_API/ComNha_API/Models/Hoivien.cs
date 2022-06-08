using System;
using System.Collections.Generic;

namespace ComNha_API.Models
{
    public partial class Hoivien
    {
        public Hoivien()
        {
            Hoadons = new HashSet<Hoadon>();
        }

        public string TenHoiVien { get; set; } = null!;
        public string SoDienThoai { get; set; } = null!;
        public double TongSoTienThanhToan { get; set; }
        public string Email { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public string DiaChi { get; set; } = null!;
        public string QuyenLoi { get; set; } = null!;

        public virtual ICollection<Hoadon> Hoadons { get; set; }
    }
}
