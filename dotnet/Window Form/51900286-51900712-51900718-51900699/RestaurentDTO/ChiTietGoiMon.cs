using System.Data;

namespace RestaurentDTO
{
    public class ChiTietGoiMon
    {
        private string _maMonAn;
        private string _maOrder;
        private int _soLuongMonAn;

        public ChiTietGoiMon(string maMonAn, string maOrder, int soLuongMonAn)
        {
            _maMonAn = maMonAn;
            _maOrder = maOrder;
            _soLuongMonAn = soLuongMonAn;
        }

        public ChiTietGoiMon(DataRow dataRow)
        {
            _maMonAn = dataRow["MaMonAn"].ToString();
            _maOrder = dataRow["MaOrder"].ToString();
            _soLuongMonAn = (int)dataRow["SoLuongMonAn"];
        }

        public string MaMonAn { get => _maMonAn; set => _maMonAn = value; }
        public string MaOrder { get => _maOrder; set => _maOrder = value; }
        public int SoLuongMonAn { get => _soLuongMonAn; set => _soLuongMonAn = value; }
    }
}
