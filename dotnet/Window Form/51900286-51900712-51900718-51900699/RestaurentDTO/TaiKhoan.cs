using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurentDTO
{
    public class TaiKhoan
    {
        private string _tenTaiKhoan;
        private string _matKhau;
        private string _maNhanVien;

        public TaiKhoan(string tenTaiKhoan, string matKhau, string maNhanVien)
        {
            TenTaiKhoan = tenTaiKhoan;
            MatKhau = matKhau;
            MaNhanVien = maNhanVien;
        }

        public TaiKhoan( DataRow dataRow)
        {
            TenTaiKhoan = dataRow["TenTaiKhoan"].ToString();
            MatKhau = dataRow["MatKhau"].ToString();
            MaNhanVien = dataRow["MaNhanVien"].ToString();
        }

        public TaiKhoan()
        {
            TenTaiKhoan = "";
            MatKhau = "";
            MaNhanVien = "";
        }

        public string TenTaiKhoan { get => _tenTaiKhoan; set => _tenTaiKhoan = value; }
        public string MatKhau { get => _matKhau; set => _matKhau = value; }
        public string MaNhanVien { get => _maNhanVien; set => _maNhanVien = value; }
    }
}
