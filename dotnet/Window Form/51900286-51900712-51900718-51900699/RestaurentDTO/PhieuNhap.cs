using System;
using System.Data;

namespace RestaurentDTO
{
    public class PhieuNhap
    {
        private string _maPhieuNhap;
        private DateTime _ngayNhap;
        private double _tongGia;
        private string _ghiChu;
        private string _maNCC;
        private string _maNhanVien;

        public PhieuNhap(string maPhieuNhap, DateTime ngayNhap, double tongGia, string ghiChu, string maNCC, string maNhanVien)
        {
            _maPhieuNhap = maPhieuNhap;
            _ngayNhap = ngayNhap;
            _tongGia = tongGia;
            _ghiChu = ghiChu;
            _maNCC = maNCC;
            _maNhanVien = maNhanVien;
        }

        public PhieuNhap(DataRow dataRow)
        {
            _maPhieuNhap = dataRow["MaPhieuNhap"].ToString();
            _ngayNhap = (DateTime)dataRow["NgayNhapHang"];
            _tongGia = (double)dataRow["TongGiaNhap"];
            _ghiChu = dataRow["GhiChu"].ToString();
            _maNCC = dataRow["MaNhaCungCap"].ToString();
            _maNhanVien = dataRow["MaNhanVien"].ToString();
        }

        public string MaPhieuNhap { get => _maPhieuNhap; set => _maPhieuNhap = value; }
        public DateTime NgayNhapHang { get => _ngayNhap; set => _ngayNhap = value; }
        public double TongGiaNhap { get => _tongGia; set => _tongGia = value; }
        public string GhiChu { get => _ghiChu; set => _ghiChu = value; }
        public string MaNCC { get => _maNCC; set => _maNCC = value; }
        public string MaNhanVien { get => _maNhanVien; set => _maNhanVien = value; }
    }
}
