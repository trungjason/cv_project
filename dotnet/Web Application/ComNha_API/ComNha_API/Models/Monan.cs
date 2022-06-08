using System;
using System.Collections.Generic;

namespace ComNha_API.Models
{
    public partial class Monan
    {
        public Monan()
        {
            Chitietgoimons = new HashSet<Chitietgoimon>();
        }

        public string MaMonAn { get; set; } = null!;
        public string TenMonAn { get; set; } = null!;
        public double DonGia { get; set; }
        public string DonVi { get; set; } = null!;
        public string MoTa { get; set; } = null!;
        public string MoTaNgan { get; set; } = null!;
        public string HinhAnh { get; set; } = null!;
        public string MaNhom { get; set; } = null!;

        public virtual Nhommonan MaNhomNavigation { get; set; } = null!;
        public virtual ICollection<Chitietgoimon> Chitietgoimons { get; set; }
    }
}
