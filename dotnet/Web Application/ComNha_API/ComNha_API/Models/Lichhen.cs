using System;
using System.Collections.Generic;

namespace ComNha_API.Models
{
    public partial class Lichhen
    {
        public string MaLichHen { get; set; } = null!;
        public string? NhuCau { get; set; }
        public int SoLuongKhach { get; set; }
        public TimeSpan ThoiGian { get; set; }
        public DateTime NgayHen { get; set; }
        public int? TinhTrang { get; set; }
        public int MaKhachHang { get; set; }
        public string? MaNhanVien { get; set; }
        public string? MaBanAn { get; set; }

        public virtual Banan? MaBanAnNavigation { get; set; }
        public virtual Khachhang MaKhachHangNavigation { get; set; } = null!;
        public virtual Nhanvien? MaNhanVienNavigation { get; set; }
    }
}
