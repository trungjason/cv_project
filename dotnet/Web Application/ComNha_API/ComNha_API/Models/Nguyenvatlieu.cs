using System;
using System.Collections.Generic;

namespace ComNha_API.Models
{
    public partial class Nguyenvatlieu
    {
        public string MaNguyenVatLieu { get; set; } = null!;
        public string TenNguyenVatLieu { get; set; } = null!;
        public double DonGia { get; set; }
        public string DonVi { get; set; } = null!;
        public string TinhTrang { get; set; } = null!;
        public int SoLuongTonKho { get; set; }
    }
}
