using System;
using System.Collections.Generic;

namespace ComNha_API.Models
{
    public partial class Nhommonan
    {
        public Nhommonan()
        {
            Monans = new HashSet<Monan>();
        }

        public string MaNhom { get; set; } = null!;
        public string TenNhom { get; set; } = null!;

        public virtual ICollection<Monan> Monans { get; set; }
    }
}
