using _51900286_51900712_51900718_51900699;
using RestaurentDTO;
using System.Collections.Generic;
using System.Data;

namespace RestaurentDAO
{
    public class FoodDAO
    {
        private static FoodDAO uniqueInstanceFoodDAO;

        private FoodDAO() { }

        public static FoodDAO GetInstance()
        {
            if (uniqueInstanceFoodDAO == null)
            {
                uniqueInstanceFoodDAO = new FoodDAO();
            };

            return uniqueInstanceFoodDAO;
        }

        public List<MonAn> GetAllFood()
        {
            List<MonAn> MonAns = new List<MonAn>();

            string query = "SELECT * FROM MonAn";
            DataTable data = DataProvider.GetInstance().ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                MonAn MonAn = new MonAn(row);
                MonAns.Add(MonAn);
            };

            return MonAns;
        }

        public List<MonAn> GetFoodListByFoodGroupID(string foodGroupID)
        {
            List<MonAn> MonAns = new List<MonAn>();

            string query = "SELECT * FROM MonAn WHERE MaNhom = @foodGroupID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@foodGroupID", foodGroupID);

            DataTable data = DataProvider.GetInstance().ExecuteQueryWithParams();

            foreach (DataRow row in data.Rows)
            {
                MonAn MonAn = new MonAn(row);
                MonAns.Add(MonAn);
            };

            return MonAns;
        }

        public bool AddFood(string name, double price, string unit, string description, string desc, string image, string groupID)
        {
            string id = DatabaseIDSupport.CreateNewMaxID(GetMaxFoodID());

            string query = "insert into MonAn values(@id, @name, @price, @unit, @description, @desc, @image, @groupID)";
            DataProvider.GetInstance().CreateCommand(query);

            // Bind params
            DataProvider.GetInstance().AddParams("@id", id);
            DataProvider.GetInstance().AddParams("@name", name);
            DataProvider.GetInstance().AddParams("@price", price);
            DataProvider.GetInstance().AddParams("@unit", unit);
            DataProvider.GetInstance().AddParams("@description", description);
            DataProvider.GetInstance().AddParams("@desc", desc);
            DataProvider.GetInstance().AddParams("@image", image);
            DataProvider.GetInstance().AddParams("@groupID", groupID);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool DeleteFood(string id)
        {
            string query = "delete from MonAn where MaMonAn = @id";
            DataProvider.GetInstance().CreateCommand(query);

            // bind params
            DataProvider.GetInstance().AddParams("@id", id);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool UpdateFood(string id, string name, double price, string unit, string description, string desc, string image, string groupID)
        {
            string query = "update MonAn set TenMonAn = @name, DonGia = @price, DonVi = @unit, MoTa = @description, MoTaNgan = @desc, HinhAnh = @image, MaNhom = @groupID where MaMonAn = @id";

            DataProvider.GetInstance().CreateCommand(query);

            // Bind params
            DataProvider.GetInstance().AddParams("@id", id);
            DataProvider.GetInstance().AddParams("@name", name);
            DataProvider.GetInstance().AddParams("@price", price);
            DataProvider.GetInstance().AddParams("@unit", unit);
            DataProvider.GetInstance().AddParams("@description", description);
            DataProvider.GetInstance().AddParams("@desc", desc);
            DataProvider.GetInstance().AddParams("@image", image);
            DataProvider.GetInstance().AddParams("@groupID", groupID);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public string GetMaxFoodID()
        {
            string query = "SELECT MAX(MaMonAn) as 'max' FROM monan";

            DataTable data = DataProvider.GetInstance().ExecuteQuery(query);

            if (data.Rows.Count > 0)
            {
                return data.Rows[0]["max"].ToString() == "" ? "MA000" : data.Rows[0]["max"].ToString();
            };

            return "MA000";
        }
    }
}
