using System;
using System.Collections.Generic;

namespace ComNha_API.Models
{
    public partial class Hoadon
    {
        public string MaHoaDon { get; set; } = null!;
        public DateTime ThoiGianThanhToan { get; set; }
        public double? TongTien { get; set; }
        public double? ChietKhau { get; set; }
        public int TinhTrang { get; set; }
        public int? MaKhachHang { get; set; }
        public string? MaHoiVien { get; set; }
        public string? MaNhanVien { get; set; }
        public string MaOrder { get; set; } = null!;

        public virtual Hoivien? MaHoiVienNavigation { get; set; }
        public virtual Khachhang? MaKhachHangNavigation { get; set; }
        public virtual Nhanvien? MaNhanVienNavigation { get; set; }
        public virtual Phieugoimon MaOrderNavigation { get; set; } = null!;
    }
}
