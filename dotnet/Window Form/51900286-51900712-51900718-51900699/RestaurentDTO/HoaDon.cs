using System;
using System.Data;

namespace RestaurentDTO
{
    public class HoaDon
    {
        private string _maHoaDon;
        private string _maOrder;
        private int _tinhTrang;
        private DateTime _thoiGianThanhToan;
        private double _tongTien;
        private double _chietKhau;
        private string _maHoiVien;

        public HoaDon(string maHoaDon, string maOrder, DateTime thoiGianThanhToan, double tongTien, double chietKhau, string maHoiVien)
        {
            _maHoaDon = maHoaDon;
            _maOrder = maOrder;
            _thoiGianThanhToan = thoiGianThanhToan;
            _tongTien = tongTien;
            _chietKhau = chietKhau;
            _maHoiVien = maHoiVien;
        }

        public HoaDon(DataRow dataRow)
        {
            _maHoaDon = dataRow["MaHoaDon"].ToString();
            _maOrder = dataRow["MaOrder"].ToString();
            _tinhTrang = int.Parse(dataRow["TinhTrang"].ToString());
            _thoiGianThanhToan = (DateTime)dataRow["ThoiGianThanhToan"];
            _tongTien = (double) dataRow["TongTien"];
            _chietKhau = (double)dataRow["ChietKhau"];
            _maHoiVien = dataRow["MaHoiVien"].ToString();
        }

        public string MaHoaDon { get => _maHoaDon; set => _maHoaDon = value; }
        public string MaOrder { get => _maOrder; set => _maOrder = value; }
        public int TinhTrang { get => _tinhTrang; set => _tinhTrang = value; }
        public DateTime ThoiGianThanhToan { get => _thoiGianThanhToan; set => _thoiGianThanhToan = value; }
        public double TongTien { get => _tongTien; set => _tongTien = value; }
        public double ChietKhau { get => _chietKhau; set => _chietKhau = value; }
        public string MaHoiVien { get => _maHoiVien; set => _maHoiVien = value; }
    }
}
