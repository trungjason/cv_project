using System;
using System.Collections.Generic;

namespace ComNha_API.Models
{
    public partial class Chitietgoimon
    {
        public string MaMonAn { get; set; } = null!;
        public string MaOrder { get; set; } = null!;
        public int SoLuongMonAn { get; set; }

        public virtual Monan MaMonAnNavigation { get; set; } = null!;
        public virtual Phieugoimon MaOrderNavigation { get; set; } = null!;
    }
}
