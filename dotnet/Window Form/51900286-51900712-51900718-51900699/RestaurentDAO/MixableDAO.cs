using RestaurentDTO;
using System.Collections.Generic;
using System.Data;

namespace RestaurentDAO
{
    public class MixableDAO
    {
        private static MixableDAO uniqueInstanceMixableDAO;

        private MixableDAO() { }

        public static MixableDAO GetInstance()
        {
            if (uniqueInstanceMixableDAO == null)
            {
                uniqueInstanceMixableDAO = new MixableDAO();
            };

            return uniqueInstanceMixableDAO;
        }

        public List<HoaDonTam> GetHoaDonTamByTableID(string tableID)
        {
            List<HoaDonTam> hoaDonTams = new List<HoaDonTam>();
            string query = "SELECT monan.MaMonAn, monan.TenMonAn, monan.DonGia, monan.DonVi, chitietgoimon.SoLuongMonAn, " +
                "banan.MaBanAn, (monan.DonGia*chitietgoimon.SoLuongMonAn) as 'Thành tiền' " +
                "FROM chitietgoimon, phieugoimon, banan, monan, hoadon " +
                "WHERE banan.MaBanAn = @tableID " +
                "AND chitietgoimon.MaOrder = phieugoimon.MaOrder " +
                "AND phieugoimon.MaBanAn = banan.MaBanAn " +
                "AND monan.MaMonAn = chitietgoimon.MaMonAn " +
                "AND hoadon.MaOrder = phieugoimon.MaOrder " +
                "AND hoadon.TinhTrang = '0'";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@tableID", tableID);

            DataTable data = DataProvider.GetInstance().ExecuteQueryWithParams();

            if ( data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    HoaDonTam chiTietGoiMon = new HoaDonTam(row);
                    hoaDonTams.Add(chiTietGoiMon);
                };               
            }

            return hoaDonTams;
        }

        public List<LichHenTam> GetLichHenTam()
        {
            List<LichHenTam> lichHenTams = new List<LichHenTam>();
            string query = "SELECT * FROM lichhen, khachhang WHERE lichhen.MaKhachHang = khachhang.MaKhachHang";

            DataTable data = DataProvider.GetInstance().ExecuteQuery(query);

            if (data.Rows.Count > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    LichHenTam lichHenTam = new LichHenTam(row);
                    lichHenTams.Add(lichHenTam);
                };
            }

            return lichHenTams;
        }
    }
}
