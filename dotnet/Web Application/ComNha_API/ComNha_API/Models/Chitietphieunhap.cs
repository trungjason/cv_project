using System;
using System.Collections.Generic;

namespace ComNha_API.Models
{
    public partial class Chitietphieunhap
    {
        public string MaNguyenVatLieu { get; set; } = null!;
        public string MaPhieuNhap { get; set; } = null!;
        public int SoLuongNhap { get; set; }

        public virtual Nguyenvatlieu MaNguyenVatLieuNavigation { get; set; } = null!;
        public virtual Phieunhap MaPhieuNhapNavigation { get; set; } = null!;
    }
}
