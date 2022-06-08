using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurentDTO
{
    public class HoiVien
    {
        private string _tenHoiVien;
        private string _soDienThoai;
        private double _tongSoTienThanhToan;
        private string _email;
        private string _diaChi;
        private string _quyenLoi;

        public HoiVien(string tenHoiVien, string soDienThoai, double tongSoTienThanhToan, string email, string diaChi, string quyenLoi)
        {
            _tenHoiVien = tenHoiVien;
            _soDienThoai = soDienThoai;
            _tongSoTienThanhToan = tongSoTienThanhToan;
            _email = email;
            _diaChi = diaChi;
            _quyenLoi = quyenLoi;
        }

        public HoiVien(string tenHoiVien, string soDienThoai,string diaChi, string email, string quyenLoi)
        {
            _tenHoiVien = tenHoiVien;
            _soDienThoai = soDienThoai;
            _tongSoTienThanhToan = 0;
            _email = email;
            _diaChi = diaChi;
            _quyenLoi = quyenLoi;
        }

        public HoiVien(DataRow dataRow)
        {
            _tenHoiVien = dataRow["TenHoiVien"].ToString();
            _soDienThoai = dataRow["SoDienThoai"].ToString();
            _tongSoTienThanhToan = (double)dataRow["TongSoTienThanhToan"];
            _email = dataRow["Email"].ToString();
            _diaChi = dataRow["DiaChi"].ToString();
            _quyenLoi = dataRow["QuyenLoi"].ToString();
        }

        public string TenHoiVien { get => _tenHoiVien; set => _tenHoiVien = value; }
        public string SoDienThoai { get => _soDienThoai; set => _soDienThoai = value; }
        public double TongSoTienThanhToan { get => _tongSoTienThanhToan; set => _tongSoTienThanhToan = value; }
        public string Email { get => _email; set => _email = value; }
        public string DiaChi { get => _diaChi; set => _diaChi = value; }
        public string QuyenLoi { get => _quyenLoi; set => _quyenLoi = value; }
    }
}
