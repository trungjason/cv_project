using System.Data;

namespace RestaurentDTO
{
    public class NguyenVatLieu
    {
        private string _maNguyenVatLieu;
        private string _tenNguyenVatLieu;
        private double _donGia;
        private string _donVi;
        private int _soLuongTonKho;
        private string _tinhTrang;

        public NguyenVatLieu(string maNguyenVatLieu, string tenNguyenVatLieu, double donGia, string donVi, int soLuongTonKho, string tinhTrang)
        {
            MaNguyenVatLieu = maNguyenVatLieu;
            TenNguyenVatLieu = tenNguyenVatLieu;
            DonGia = donGia;
            DonVi = donVi;
            SoLuongTonKho = soLuongTonKho;
            TinhTrang = tinhTrang;
        }

        public NguyenVatLieu(DataRow dataRow)
        {
            MaNguyenVatLieu = dataRow["MaNguyenVatLieu"].ToString();
            TenNguyenVatLieu = dataRow["TenNguyenVatLieu"].ToString();
            DonGia = (double)dataRow["DonGia"];
            DonVi = dataRow["DonVi"].ToString();
            SoLuongTonKho = (int)dataRow["SoLuongTonKho"];
            TinhTrang = dataRow["TinhTrang"].ToString();
        }

        public string MaNguyenVatLieu { get => _maNguyenVatLieu; set => _maNguyenVatLieu = value; }
        public string TenNguyenVatLieu { get => _tenNguyenVatLieu; set => _tenNguyenVatLieu = value; }
        public double DonGia { get => _donGia; set => _donGia = value; }
        public string DonVi { get => _donVi; set => _donVi = value; }
        public string TinhTrang { get => _tinhTrang; set => _tinhTrang = value; }
        public int SoLuongTonKho { get => _soLuongTonKho; set => _soLuongTonKho = value; }
    }
}
