using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurentDTO
{
    public class KhachHang
    {
        private int _maKhachHang;
        private string _soDienThoai;
        private string _tenKhachHang;
        private string _diaChi;
        private string _email;

        public KhachHang(int maKhachHang, string soDienThoai, string tenKhachHang, string diaChi, string email)
        {
            MaKhachHang = maKhachHang;
            SoDienThoai = soDienThoai;
            TenKhachHang = tenKhachHang;
            DiaChi = diaChi;
            Email = email;
        }

        public KhachHang(DataRow dataRow)
        {
            MaKhachHang = (int)dataRow["MaKhachHang"];
            SoDienThoai = dataRow["SoDienThoai"].ToString();
            TenKhachHang = dataRow["TenKhachHang"].ToString();
            DiaChi = dataRow["DiaChi"].ToString();
            Email = dataRow["Email"].ToString();
        }

        public int MaKhachHang { get => _maKhachHang; set => _maKhachHang = value; }
        public string SoDienThoai { get => _soDienThoai; set => _soDienThoai = value; }
        public string TenKhachHang { get => _tenKhachHang; set => _tenKhachHang = value; }
        public string DiaChi { get => _diaChi; set => _diaChi = value; }
        public string Email { get => _email; set => _email = value; }
    }
}
