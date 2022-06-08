using System.Data;

namespace RestaurentDTO
{
    public class ChiTietPhieuNhap
    {
        private string _maNVL;
        private string _maPhieuNhap;
        private int _soLuong;

        public ChiTietPhieuNhap(string maNVL, string maPhieuNhap, int soLuong)
        {
            _maNVL = maNVL;
            _maPhieuNhap = maPhieuNhap;
            _soLuong = soLuong;
        }

        public ChiTietPhieuNhap(DataRow dataRow)
        {
            _maNVL = dataRow["MaNguyenVatLieu"].ToString();
            _maPhieuNhap = dataRow["MaPhieuNhap"].ToString();
            _soLuong = (int)dataRow["SoLuongNhap"];
        }

        public string MaNVL { get => _maNVL; set => _maNVL = value; }
        public string MaPhieuNhap { get => _maPhieuNhap; set => _maPhieuNhap = value; }
        public int SoLuong { get => _soLuong; set => _soLuong = value; }
    }
}
