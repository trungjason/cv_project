using System.Data;

namespace RestaurentDTO
{
    public class MonAn
    {
        private string _maMonAn;
        private string _tenMonAn;
        private double _donGia;
        private string _donVi;
        private string _moTa;
        private string _moTaNgan;
        private string _hinhAnh;
        private string _maNhom;

        public MonAn(string maMonAn, string tenMonAn, double donGia, string donVi, string moTa, string moTaNgan, string hinhAnh, string maNhom)
        {
            _maMonAn = maMonAn;
            _tenMonAn = tenMonAn;
            _donGia = donGia;
            _donVi = donVi;
            _moTa = moTa;
            _moTaNgan = moTaNgan;
            _hinhAnh = hinhAnh;
            _maNhom = maNhom;
        }

        public MonAn(DataRow dataRow)
        {
            _maMonAn = dataRow["MaMonAn"].ToString();
            _tenMonAn = dataRow["TenMonAn"].ToString();
            _donGia = (double)dataRow["DonGia"];
            _donVi = dataRow["DonVi"].ToString();
            _moTa = dataRow["MoTa"].ToString();
            _moTaNgan = dataRow["MoTaNgan"].ToString();
            _hinhAnh = dataRow["HinhAnh"].ToString();
            _maNhom = dataRow["MaNhom"].ToString();
        }

        public string MaMonAn { get => _maMonAn; set => _maMonAn = value; }
        public string TenMonAn { get => _tenMonAn; set => _tenMonAn = value; }
        public double DonGia { get => _donGia; set => _donGia = value; }
        public string DonVi { get => _donVi; set => _donVi = value; }
        public string MoTa { get => _moTa; set => _moTa = value; }
        public string MoTaNgan { get => _moTaNgan; set => _moTaNgan = value; }
        public string HinhAnh { get => _hinhAnh; set => _hinhAnh = value; }
        public string MaNhom { get => _maNhom; set => _maNhom = value; }
    }
}
