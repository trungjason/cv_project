using System.Data;

namespace RestaurentDTO
{
    public class NhomMonAn
    {
        private string _maNhom;
        private string _tenNhom;

        public NhomMonAn(string maNhom, string tenNhom)
        {
            _maNhom = maNhom;
            _tenNhom = tenNhom;
        }

        public NhomMonAn(DataRow dataRow)
        {
            _maNhom = dataRow["MaNhom"].ToString();
            _tenNhom = dataRow["TenNhom"].ToString();
        }

        public string MaNhom { get => _maNhom; set => _maNhom = value; }
        public string TenNhom { get => _tenNhom; set => _tenNhom = value; }
    }
}
