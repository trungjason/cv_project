using System.Data;

namespace RestaurentDTO
{
    public class PhieuGoiMon
    {
        private string _maOrder;
        private string _ghiChuMonAn;
        private string _maBanAn;
        private string _maNhanVien;

        public PhieuGoiMon(string maOrder, string ghiChuMonAn, string maBanAn, string maNhanVien)
        {
            _maOrder = maOrder;
            _ghiChuMonAn = ghiChuMonAn;
            _maBanAn = maBanAn;
            _maNhanVien = maNhanVien;
        }

        public PhieuGoiMon(DataRow dataRow)
        {
            _maOrder = dataRow["MaOrder"].ToString();
            _ghiChuMonAn = dataRow["GhiChuMonAn"].ToString();
            _maBanAn = dataRow["MaBanAn"].ToString();
            _maNhanVien = dataRow["MaNhanVien"].ToString();
        }

        public string MaOrder { get => _maOrder; set => _maOrder = value; }
        public string GhiChuMonAn { get => _ghiChuMonAn; set => _ghiChuMonAn = value; }
        public string MaBanAn { get => _maBanAn; set => _maBanAn = value; }
        public string MaNhanVien { get => _maNhanVien; set => _maNhanVien = value; }
    }
}
