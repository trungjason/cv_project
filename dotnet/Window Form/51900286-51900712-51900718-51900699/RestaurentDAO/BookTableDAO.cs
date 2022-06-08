using RestaurentDTO;
using System.Data;

namespace RestaurentDAO
{
    public class BookTableDAO
    {
        private static BookTableDAO uniqueInstanceBookTableDAO;

        private BookTableDAO() { }

        public static BookTableDAO GetInstance()
        {
            if (uniqueInstanceBookTableDAO == null)
            {
                uniqueInstanceBookTableDAO = new BookTableDAO();
            };

            return uniqueInstanceBookTableDAO;
        }

        public bool DeleteBookTable(string bookID)
        {
            string query = "DELETE FROM lichhen WHERE MaLichHen = @bookID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@bookID", bookID);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool UpdateBookTable( string bookID, string tableID, string employeeID )
        {
            string query = "UPDATE lichhen SET TinhTrang = 1, MaNhanVien = @employeeID, MaBanAn = @tableID WHERE MaLichHen = @bookID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@employeeID", employeeID);
            DataProvider.GetInstance().AddParams("@tableID", tableID);
            DataProvider.GetInstance().AddParams("@bookID", bookID);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public LichHen GetBookTableTime(string tableID)
        {
            string query = "SELECT * FROM lichhen WHERE TinhTrang = 1 AND MaBanAn = @tableID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@tableID", tableID);

            DataTable result = DataProvider.GetInstance().ExecuteQueryWithParams();

            if (result.Rows.Count > 0)
            {
                return new LichHen(result.Rows[0]);
            }

            return new LichHen();
        }
    }
}
