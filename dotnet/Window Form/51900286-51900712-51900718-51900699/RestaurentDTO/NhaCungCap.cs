using System;
using System.Data;

namespace RestaurentDTO
{
    public class NhaCungCap
    {
        private string _maNCC;
        private string _tenNCC;
        private string _moTaNCC;
        private string _diaChi;
        private string _soDienThoai;

        public NhaCungCap(DataRow dataRow)
        {
            _maNCC = dataRow["MaNhaCungCap"].ToString();
            _tenNCC = dataRow["TenNhaCungCap"].ToString();
            _moTaNCC = dataRow["MoTaNhaCungCap"].ToString();
            _diaChi = dataRow["DiaChi"].ToString();
            _soDienThoai = dataRow["SoDienThoai"].ToString();
        }

        public string MaNCC { get => _maNCC; set => _maNCC = value; }
        public string TenNCC { get => _tenNCC; set => _tenNCC = value; }
        public string MoTaNCC { get => _moTaNCC; set => _moTaNCC = value; }
        public string DiaChi { get => _diaChi; set => _diaChi = value; }
        public string SoDienThoai { get => _soDienThoai; set => _soDienThoai = value; }
    }
}
