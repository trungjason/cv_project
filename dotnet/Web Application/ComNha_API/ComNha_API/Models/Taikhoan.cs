using System;
using System.Collections.Generic;

namespace ComNha_API.Models
{
    public partial class Taikhoan
    {
        public string TenTaiKhoan { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public string MaNhanVien { get; set; } = null!;

        public virtual Nhanvien MaNhanVienNavigation { get; set; } = null!;
    }
}
