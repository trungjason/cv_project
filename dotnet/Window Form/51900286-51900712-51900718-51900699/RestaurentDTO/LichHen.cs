using System;
using System.Data;

namespace RestaurentDTO
{
    public class LichHen
    {
        private string _maLichHen;
        private string _nhuCau;
        private int _soLuongKhach;
        private string _thoiGian;
        private string _ngayHen;
        private string _tinhTrang;
        private string _maKhachHang;
        private string _maNhanVien;
        private string _maBanAn;

        public LichHen(string maLichHen, string nhuCau, int soLuongKhach, string thoiGian, string ngayHen, string tinhTrang, string maKhachHang, string maNhanVien, string maBanAn)
        {
            _maLichHen = maLichHen;
            _nhuCau = nhuCau;
            _soLuongKhach = soLuongKhach;
            _thoiGian = thoiGian;
            _ngayHen = ngayHen;
            _tinhTrang = tinhTrang;
            _maKhachHang = maKhachHang;
            _maNhanVien = maNhanVien;
            _maBanAn = maBanAn;
        }

        public LichHen()
        {
            _maLichHen = "";
            _nhuCau = "";
            _soLuongKhach = 0;
            _thoiGian = "";
            _ngayHen = "";
            _tinhTrang = "0";
            _maKhachHang = "";
            _maNhanVien = "";
            _maBanAn = "";
        }

        public LichHen(DataRow dataRow)
        {
            _maLichHen = dataRow["MaLichHen"].ToString();
            _nhuCau = dataRow["NhuCau"].ToString();
            _soLuongKhach = (int)dataRow["SoLuongKhach"];
            _thoiGian = dataRow["ThoiGian"].ToString();
            _ngayHen = dataRow["NgayHen"].ToString();
            _tinhTrang = dataRow["TinhTrang"].ToString();
            _maKhachHang = dataRow["MaKhachHang"].ToString();
            _maNhanVien = dataRow["MaNhanVien"].ToString();
            _maBanAn = dataRow["MaBanAn"].ToString();
        }



        public string MaLichHen { get => _maLichHen; set => _maLichHen = value; }
        public string NhuCau { get => _nhuCau; set => _nhuCau = value; }
        public int SoLuongKhach { get => _soLuongKhach; set => _soLuongKhach = value; }
        public string ThoiGian { get => _thoiGian; set => _thoiGian = value; }
        public string NgayHen { get => _ngayHen; set => _ngayHen = value; }
        public string TinhTrang { get => _tinhTrang; set => _tinhTrang = value; }
        public string MaKhachHang { get => _maKhachHang; set => _maKhachHang = value; }
        public string MaNhanVien { get => _maNhanVien; set => _maNhanVien = value; }
        public string MaBanAn { get => _maBanAn; set => _maBanAn = value; }
    }
}
