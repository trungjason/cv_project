using _51900286_51900712_51900718_51900699;
using RestaurentDTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace RestaurentDAO
{
    public class BillDAO
    {
        private static BillDAO uniqueInstanceBillDAO;

        private BillDAO() { }

        public static BillDAO GetInstance()
        {
            if (uniqueInstanceBillDAO == null)
            {
                uniqueInstanceBillDAO = new BillDAO();
            };

            return uniqueInstanceBillDAO;
        }

        public List<HoaDon> GetBillInPeriod(DateTime fromDate, DateTime toDate)
        {
            string query = "SELECT * FROM Hoadon WHERE ThoiGianThanhToan >= @fromDate and ThoiGianThanhToan <= @toDate";

            List<HoaDon> bills = new List<HoaDon>();

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@fromDate", fromDate);
            DataProvider.GetInstance().AddParams("@toDate", toDate);

            DataTable data = DataProvider.GetInstance().ExecuteQueryWithParams();

            foreach (DataRow row in data.Rows)
            {
                HoaDon bill = new HoaDon(row);
                bills.Add(bill);
            };

            return bills;
        }

        public List<HoaDon> GetBillByMonth(int month, int year)
        {
            string query = "SELECT * FROM Hoadon WHERE month(ThoiGianThanhToan) = @month and year(ThoiGianThanhToan) = @year";

            List<HoaDon> bills = new List<HoaDon>();

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@month", month);
            DataProvider.GetInstance().AddParams("@year", year);

            DataTable data = DataProvider.GetInstance().ExecuteQueryWithParams();

            foreach (DataRow row in data.Rows)
            {
                HoaDon bill = new HoaDon(row);
                bills.Add(bill);
            };

            return bills;
        }

        public List<HoaDon> GetBillByYear(int year)
        {
            string query = "SELECT * FROM Hoadon WHERE year(ThoiGianThanhToan) = @year";

            List<HoaDon> bills = new List<HoaDon>();

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@year", year);

            DataTable data = DataProvider.GetInstance().ExecuteQueryWithParams();

            foreach (DataRow row in data.Rows)
            {
                HoaDon bill = new HoaDon(row);
                bills.Add(bill);
            };

            return bills;
        }

        public string GetBillIDByTableID(string tableID)
        {
            string query = "SELECT MaHoaDon FROM hoadon, phieugoimon " +
                "WHERE MaBanAn = @tableID AND hoadon.MaOrder = phieugoimon.MaOrder " +
                "AND hoadon.TinhTrang = '0' ";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@tableID", tableID);

            DataTable data = DataProvider.GetInstance().ExecuteQueryWithParams();

            if ( data.Rows.Count > 0)
            {
                return data.Rows[0]["MaHoaDon"].ToString();
            };

            return "-1";
        }

        public string GetOrderIDByBillID(string billID)
        {
            string query = "SELECT MaOrder FROM hoadon WHERE hoadon.MaHoaDon = @billID AND hoadon.TinhTrang = '0'";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@billID", billID);

            DataTable result = DataProvider.GetInstance().ExecuteQueryWithParams();

            if (result.Rows.Count > 0)
            {
                return result.Rows[0]["MaOrder"].ToString();
            }

            return "-1";
        }

        public string GetMaxBillID()
        {
            string query = "SELECT MAX(MaHoaDon) AS 'MaxHoaDonID' FROM hoadon";

            DataTable data = DataProvider.GetInstance().ExecuteQuery(query);

            if (data.Rows.Count > 0)
            {
                return data.Rows[0]["MaxHoaDonID"].ToString() == "" ? "HD000" : data.Rows[0]["MaxHoaDonID"].ToString();
            };

            return "HD000";
        }

        public bool InsertNewBill( string orderID)
        {
            string newMaxBillID = DatabaseIDSupport.CreateNewMaxID(GetMaxBillID());
            string query = "INSERT INTO hoadon (MaHoaDon, TinhTrang, MaOrder) VALUES" +
                "(@newMaxBillID, 0, @orderID)";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@newMaxBillID", newMaxBillID);
            DataProvider.GetInstance().AddParams("@orderID", orderID);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool Payment(string billID, double tongTien, double chietKhau, string memberPhoneNumber, string employeeID)
        {
            DateTime dateTime = DateTime.Now;
            string sqlFormattedDate = dateTime.ToString("yyyy/MM/dd HH:mm:ss");

            memberPhoneNumber = memberPhoneNumber == "" ? null : memberPhoneNumber;

            // Cập nhật tổng tiền
            if (memberPhoneNumber != null)
            {
                MemberDAO.GetInstance().UpdateMemberTotalPay(memberPhoneNumber,tongTien);
            }

            string query = "UPDATE hoadon SET TinhTrang = '1',ThoiGianThanhToan = @sqlFormattedDate, TongTien = @tongTien, ChietKhau = @chietKhau" +
                ", MaHoiVien = @memberPhoneNumber, MaNhanVien = @employeeID " +
                "WHERE MaHoaDon = @billID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@sqlFormattedDate", sqlFormattedDate);
            DataProvider.GetInstance().AddParams("@tongTien", tongTien);
            DataProvider.GetInstance().AddParams("@billID", billID);
            DataProvider.GetInstance().AddParams("@chietKhau", chietKhau);

            if (String.IsNullOrEmpty(memberPhoneNumber))
            {
                DataProvider.GetInstance().AddParams("@memberPhoneNumber", DBNull.Value);
            } else
            {
                DataProvider.GetInstance().AddParams("@memberPhoneNumber", memberPhoneNumber);
            }
            
            DataProvider.GetInstance().AddParams("@employeeID", employeeID);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public double GetTotalBillPrice(string billID)
        {
            string query = "SELECT SUM(( monan.DonGia* chitietgoimon.SoLuongMonAn )) AS 'Total'" +
                " FROM hoadon, chitietgoimon, phieugoimon, monan " +
                "WHERE hoadon.MaOrder = phieugoimon.MaOrder AND " +
                " chitietgoimon.MaMonAn = monan.MaMonAn AND chitietgoimon.MaOrder = phieugoimon.MaOrder" +
                " AND hoadon.MaHoaDon = @billID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@billID", billID);

            DataTable result = DataProvider.GetInstance().ExecuteQueryWithParams();

            if ( result.Rows.Count > 0)
            {
                return (double)result.Rows[0]["Total"];
            };

            return 0;
        }

        public bool DeleteBillByOrderID(string orderID)
        {
            string query = "DELETE FROM hoadon WHERE MaOrder = @orderID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@orderID", orderID);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }
    }
}
