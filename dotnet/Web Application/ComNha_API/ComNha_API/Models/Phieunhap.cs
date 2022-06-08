using System;
using System.Collections.Generic;

namespace ComNha_API.Models
{
    public partial class Phieunhap
    {
        public string MaPhieuNhap { get; set; } = null!;
        public DateTime NgayNhapHang { get; set; }
        public double TongGiaNhap { get; set; }
        public string GhiChu { get; set; } = null!;
        public string MaNhaCungCap { get; set; } = null!;
        public string MaNhanVien { get; set; } = null!;

        public virtual Nhacungcap MaNhaCungCapNavigation { get; set; } = null!;
        public virtual Nhanvien MaNhanVienNavigation { get; set; } = null!;
    }
}
