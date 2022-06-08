using _51900286_51900712_51900718_51900699;
using RestaurentDTO;
using System.Collections.Generic;
using System.Data;

namespace RestaurentDAO
{
    public class TableDAO
    {
        private static TableDAO uniqueInstanceTableDAO;
        public static readonly int TABLE_WIDTH = 100;
        public static readonly int TABLE_HEIGHT = 100;
        private TableDAO() { }

        public static TableDAO GetInstance()
        {
            if (uniqueInstanceTableDAO == null)
            {
                uniqueInstanceTableDAO = new TableDAO();
            };

            return uniqueInstanceTableDAO;
        }

        public List<BanAn> LoadTableList() 
        {
            List<BanAn> listTable = new List<BanAn>();
            string query = "SELECT * FROM banan";

            DataTable result = DataProvider.GetInstance().ExecuteQuery(query);

            foreach( DataRow dataRow in result.Rows ){
                BanAn banAn = new BanAn(dataRow);
                listTable.Add(banAn);
            };

            return listTable;
        }

        public bool SetTableState(string tableID, string tableState)
        {
            string query = "UPDATE banan SET TinhTrang = @tableState WHERE MaBanAn = @tableID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@tableState", tableState);
            DataProvider.GetInstance().AddParams("@tableID", tableID);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public BanAn GetTableByID(string tableID)
        {
            string query = "SELECT * FROM banan WHERE MaBanAn = @tableID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@tableID", tableID);

            DataTable result = DataProvider.GetInstance().ExecuteQueryWithParams();

            if ( result.Rows.Count > 0)
            {
                return new BanAn(result.Rows[0]);
            }

            return new BanAn();
        }

        public bool AddTable(int seat, string status, string desc)
        {
            string id = DatabaseIDSupport.CreateNewMaxID(GetMaxTableID());

            string query = "insert into BanAn values(@id, @status, @seat, @desc)";
            DataProvider.GetInstance().CreateCommand(query);

            // Bind params
            DataProvider.GetInstance().AddParams("@id", id);
            DataProvider.GetInstance().AddParams("@status", status);
            DataProvider.GetInstance().AddParams("@seat", seat);
            DataProvider.GetInstance().AddParams("@desc", desc);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool DeleteTable(string id)
        {
            string query = "delete from BanAn where MaBanAn = @id";
            DataProvider.GetInstance().CreateCommand(query);

            // bind params
            DataProvider.GetInstance().AddParams("@id", id);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool UpdateTable(string id, int seat, string status, string desc)
        {
            string query = "update BanAn set TinhTrang = @status, SucChua = @seat, GhiChu = @desc  where MaBanAn = @id";

            DataProvider.GetInstance().CreateCommand(query);

            // Bind params
            DataProvider.GetInstance().AddParams("@id", id);
            DataProvider.GetInstance().AddParams("@status", status);
            DataProvider.GetInstance().AddParams("@seat", seat);
            DataProvider.GetInstance().AddParams("@desc", desc);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public string GetMaxTableID()
        {
            string query = "SELECT MAX(MaBanAn) as 'max' FROM BanAn";

            DataTable data = DataProvider.GetInstance().ExecuteQuery(query);

            if (data.Rows.Count > 0)
            {
                return data.Rows[0]["max"].ToString() == "" ? "BA000" : data.Rows[0]["max"].ToString();
            };

            return "BA000";
        }
    }
}
