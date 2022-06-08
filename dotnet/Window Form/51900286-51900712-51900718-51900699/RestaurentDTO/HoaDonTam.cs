using System.Data;


namespace RestaurentDTO
{
    public class HoaDonTam
    {
        private string _maMonAn;
        private string _tenMonAn;
        private double _donGia;
        private string _donVi;
        private int _soLuongMonAn;
        private string _maBanAn;
        private double totalPrice;

        public HoaDonTam(string maMonAn, string tenMonAn, double donGia, string donVi, int soLuongMonAn, string maBanAn, double totalPrice)
        {
            _maMonAn = maMonAn;
            _tenMonAn = tenMonAn;
            _donGia = donGia;
            _donVi = donVi;
            _soLuongMonAn = soLuongMonAn;
            _maBanAn = maBanAn;
            this.totalPrice = totalPrice;
        }

        public HoaDonTam(DataRow dataRow)
        {
            _maMonAn = dataRow["MaMonAn"].ToString();
            _tenMonAn = dataRow["TenMonAn"].ToString();
            _donGia = (double)dataRow["DonGia"];
            _donVi = dataRow["DonVi"].ToString();
            _soLuongMonAn = (int)dataRow["SoLuongMonAn"];
            _maBanAn = dataRow["MaBanAn"].ToString();
            this.totalPrice = (double)dataRow["Thành tiền"];
        }

        public string MaMonAn { get => _maMonAn; set => _maMonAn = value; }
        public string TenMonAn { get => _tenMonAn; set => _tenMonAn = value; }
        public double DonGia { get => _donGia; set => _donGia = value; }
        public string DonVi { get => _donVi; set => _donVi = value; }
        public int SoLuongMonAn { get => _soLuongMonAn; set => _soLuongMonAn = value; }
        public string MaBanAn { get => _maBanAn; set => _maBanAn = value; }
        public double TotalPrice { get => totalPrice; set => totalPrice = value; }
    }
}
