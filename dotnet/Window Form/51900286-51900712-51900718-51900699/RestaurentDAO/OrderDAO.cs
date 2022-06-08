using _51900286_51900712_51900718_51900699;
using RestaurentDTO;
using System.Data;

namespace RestaurentDAO
{
    public class OrderDAO
    {
        private static OrderDAO uniqueInstanceOrderDAO;

        private OrderDAO() { }

        public static OrderDAO GetInstance()
        {
            if (uniqueInstanceOrderDAO == null)
            {
                uniqueInstanceOrderDAO = new OrderDAO();
            };

            return uniqueInstanceOrderDAO;
        }

        public PhieuGoiMon GetOrderByTableID(string tableID)
        {
            string query = "SELECT * FROM phieugoimon WHERE MaBanAn = @tableID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@tableID", tableID);

            DataTable data = DataProvider.GetInstance().ExecuteQueryWithParams();

            return new PhieuGoiMon(data.Rows[0]);
        }

        public string GetMaxOrderID()
        {
            string query = "SELECT MAX(MaOrder) as 'MAXORDER' FROM phieugoimon";

            DataTable data = DataProvider.GetInstance().ExecuteQuery(query);

            if (data.Rows.Count > 0)
            {
                return data.Rows[0]["MAXORDER"].ToString() == "" ? "PGM000" : data.Rows[0]["MAXORDER"].ToString();
            };

            return "PGM000";
        }

        public bool InsertNewOrder(string orderNote, string tableID, string employeeID)
        {
            // lấy mã order cao nhất hiện tại + thêm 1 
            string newMaxOrderID = DatabaseIDSupport.CreateNewMaxID(GetMaxOrderID());

            string query = "INSERT INTO phieugoimon(MaOrder, GhiChuMonAn, MaBanAn, MaNhanVien) " +
                "VALUES(@newMaxOrderID,@orderNote,@tableID,@employeeID)";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@newMaxOrderID", newMaxOrderID);
            DataProvider.GetInstance().AddParams("@orderNote", orderNote);
            DataProvider.GetInstance().AddParams("@tableID", tableID);
            DataProvider.GetInstance().AddParams("@employeeID", employeeID);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }
    }
}
