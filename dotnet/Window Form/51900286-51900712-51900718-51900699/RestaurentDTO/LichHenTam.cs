using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurentDTO
{
    public class LichHenTam
    {
        private string _maLichHen;
        private string _nhuCau;
        private int _soLuongKhach;
        private string _thoiGian;
        private string _ngayHen;
        private int _tinhTrang;
        private string _maNhanVien;
        private string _maBanAn;
        private string _soDienThoai;
        private string _tenKhachHang;
        private string _diaChi;
        private string _email;

        public string MaLichHen { get => _maLichHen; set => _maLichHen = value; }
        public string NhuCau { get => _nhuCau; set => _nhuCau = value; }
        public int SoLuongKhach { get => _soLuongKhach; set => _soLuongKhach = value; }
        public string ThoiGian { get => _thoiGian; set => _thoiGian = value; }
        public string NgayHen { get => _ngayHen; set => _ngayHen = value; }
        public int TinhTrang { get => _tinhTrang; set => _tinhTrang = value; }
        public string MaNhanVien { get => _maNhanVien; set => _maNhanVien = value; }
        public string MaBanAn { get => _maBanAn; set => _maBanAn = value; }
        public string SoDienThoai { get => _soDienThoai; set => _soDienThoai = value; }
        public string TenKhachHang { get => _tenKhachHang; set => _tenKhachHang = value; }
        public string DiaChi { get => _diaChi; set => _diaChi = value; }
        public string Email { get => _email; set => _email = value; }

        public LichHenTam(string maLichHen, string nhuCau, int soLuongKhach, string thoiGian, string ngayHen, int tinhTrang, string maNhanVien, string maBanAn, string soDienThoai, string tenKhachHang, string diaChi, string email)
        {
            MaLichHen = maLichHen;
            NhuCau = nhuCau;
            SoLuongKhach = soLuongKhach;
            ThoiGian = thoiGian;
            NgayHen = ngayHen;
            TinhTrang = tinhTrang;
            MaNhanVien = maNhanVien;
            MaBanAn = maBanAn;
            SoDienThoai = soDienThoai;
            TenKhachHang = tenKhachHang;
            DiaChi = diaChi;
            Email = email;
        }

        public LichHenTam(DataRow dataRow)
        {
            MaLichHen = dataRow["MaLichHen"].ToString();
            NhuCau = dataRow["NhuCau"].ToString();
            SoLuongKhach = (int)dataRow["SoLuongKhach"];
            ThoiGian = dataRow["ThoiGian"].ToString();
            NgayHen = dataRow["NgayHen"].ToString();
   
            MaNhanVien = dataRow["MaNhanVien"].ToString();
            MaBanAn = dataRow["MaBanAn"].ToString();
         
            SoDienThoai = dataRow["SoDienThoai"].ToString();
            TenKhachHang = dataRow["TenKhachHang"].ToString();
            DiaChi = dataRow["DiaChi"].ToString();
            Email = dataRow["Email"].ToString();
        }
    }
}
