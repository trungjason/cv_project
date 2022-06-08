using System;
using System.Collections.Generic;

namespace ComNha_API.Models
{
    public partial class Nhanvien
    {
        public Nhanvien()
        {
            Hoadons = new HashSet<Hoadon>();
            Lichhens = new HashSet<Lichhen>();
            Phieugoimons = new HashSet<Phieugoimon>();
            Phieunhaps = new HashSet<Phieunhap>();
            Taikhoans = new HashSet<Taikhoan>();
        }

        public string MaNhanVien { get; set; } = null!;
        public string TenNhanVien { get; set; } = null!;
        public string SoDienThoai { get; set; } = null!;
        public double Luong { get; set; }
        public string ChucVu { get; set; } = null!;
        public string DiaChi { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Hoadon> Hoadons { get; set; }
        public virtual ICollection<Lichhen> Lichhens { get; set; }
        public virtual ICollection<Phieugoimon> Phieugoimons { get; set; }
        public virtual ICollection<Phieunhap> Phieunhaps { get; set; }
        public virtual ICollection<Taikhoan> Taikhoans { get; set; }
    }
}
