using System.Data;

namespace RestaurentDTO
{
    public class NhanVien
    {
        private string _maNhanVien;
        private string _tenNhanVien;
        private string _soDienThoai;
        private double _luong;
        private string _chucVu;
        private string _diaChi;
        private string _email;

        public NhanVien(string maNhanVien, string tenNhanVien, string soDienThoai, double luong, string chucVu, string diaChi, string email)
        {
            _maNhanVien = maNhanVien;
            _tenNhanVien = tenNhanVien;
            _soDienThoai = soDienThoai;
            _luong = luong;
            _chucVu = chucVu;
            _diaChi = diaChi;
            _email = email;
        }

        public NhanVien()
        {
            _maNhanVien = "";
            _tenNhanVien = "";
            _soDienThoai = "";
            _luong = 0;
            _chucVu = "";
            _diaChi = "";
            _email = "";
        }

        public NhanVien(DataRow dataRow)
        {
            _maNhanVien = dataRow["MaNhanVien"].ToString();
            _tenNhanVien = dataRow["TenNhanVien"].ToString();
            _soDienThoai = dataRow["SoDienThoai"].ToString();
            _luong = (double)dataRow["Luong"];
            _chucVu = dataRow["ChucVu"].ToString();
            _diaChi = dataRow["DiaChi"].ToString();
            _email = dataRow["Email"].ToString();
        }

        public string MaNhanVien { get => _maNhanVien; set => _maNhanVien = value; }
        public string TenNhanVien { get => _tenNhanVien; set => _tenNhanVien = value; }
        public string SoDienThoai { get => _soDienThoai; set => _soDienThoai = value; }
        public double Luong { get => _luong; set => _luong = value; }
        public string ChucVu { get => _chucVu; set => _chucVu = value; }
        public string DiaChi { get => _diaChi; set => _diaChi = value; }
        public string Email { get => _email; set => _email = value; }
    }
}
