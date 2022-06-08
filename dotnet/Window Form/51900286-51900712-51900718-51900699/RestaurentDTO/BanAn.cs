using System.Data;

namespace RestaurentDTO
{
    public class BanAn
    {
        private string _maBanAn;
        private string _tinhTrang;
        private int _sucChua;
        private string _ghiChu;

        public BanAn()
        {
            MaBanAn = "";
            TinhTrang = "";
            SucChua = 100;
            GhiChu = "";
        }

        public BanAn(DataRow dataRow)
        {
            this._maBanAn = dataRow["MaBanAn"].ToString();
            this._tinhTrang = dataRow["TinhTrang"].ToString();
            this._sucChua = (int) dataRow["SucChua"];
            this._ghiChu = dataRow["GhiChu"].ToString();
        }

        public BanAn(string maBanAn, string tinhTrang, int sucChua, string ghiChu)
        {
            this._maBanAn = maBanAn;
            this._tinhTrang = tinhTrang;
            this._sucChua = sucChua;
            this._ghiChu = ghiChu;
        }


        public string MaBanAn { get => _maBanAn; set => _maBanAn = value; }
        public string TinhTrang { get => _tinhTrang; set => _tinhTrang = value; }
        public int SucChua { get => _sucChua; set => _sucChua = value; }
        public string GhiChu { get => _ghiChu; set => _ghiChu = value; }
    }
}
