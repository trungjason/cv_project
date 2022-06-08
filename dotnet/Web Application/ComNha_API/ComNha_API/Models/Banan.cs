using System;
using System.Collections.Generic;

namespace ComNha_API.Models
{
    public partial class Banan
    {
        public Banan()
        {
            Lichhens = new HashSet<Lichhen>();
            Phieugoimons = new HashSet<Phieugoimon>();
        }

        public string MaBanAn { get; set; } = null!;
        public string TinhTrang { get; set; } = null!;
        public int SucChua { get; set; }
        public string GhiChu { get; set; } = null!;

        public virtual ICollection<Lichhen> Lichhens { get; set; }
        public virtual ICollection<Phieugoimon> Phieugoimons { get; set; }
    }
}
