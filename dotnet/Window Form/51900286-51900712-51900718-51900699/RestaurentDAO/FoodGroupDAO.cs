using RestaurentDTO;
using System.Collections.Generic;
using System.Data;

namespace RestaurentDAO
{
    public class FoodGroupDAO
    {
        private static FoodGroupDAO uniqueInstanceFoodGroupDAO;

        private FoodGroupDAO() { }

        public static FoodGroupDAO GetInstance()
        {
            if (uniqueInstanceFoodGroupDAO == null)
            {
                uniqueInstanceFoodGroupDAO = new FoodGroupDAO();
            };

            return uniqueInstanceFoodGroupDAO;
        }

        public List<NhomMonAn> GetFoodGroupList()
        {
            List<NhomMonAn> nhomMonAns = new List<NhomMonAn>();

            string query = "SELECT * FROM nhommonan";

            DataTable data = DataProvider.GetInstance().ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                NhomMonAn nhomMonAn = new NhomMonAn(row);
                nhomMonAns.Add(nhomMonAn);
            };

            return nhomMonAns;
        }

        public string GetMaxFoodGroupID()
        {
            string query = "SELECT MAX(MaNhom) AS 'MaxFoodGroupID' FROM nhommonan";

            DataTable result = DataProvider.GetInstance().ExecuteQuery(query);

            if (result.Rows.Count > 0)
            {
                return result.Rows[0]["MaxFoodGroupID"].ToString() == "" ? "NMA000" : result.Rows[0]["MaxFoodGroupID"].ToString();
            };

            return "NMA000";
        }

        public bool AddFoodGroup(NhomMonAn nhomMonAn)
        {
            string query = "INSERT INTO nhommonan VALUES(@foodGroupID, @foodGroupName)";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@foodGroupID", nhomMonAn.MaNhom);
            DataProvider.GetInstance().AddParams("@foodGroupName", nhomMonAn.TenNhom);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool DeleteFoodGroup(string foodGroupID)
        {
            string query = "DELETE FROM nhommonan WHERE MaNhom = @foodGroupID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@foodGroupID", foodGroupID);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool UpdateFoodGroup(NhomMonAn nhomMonAn)
        {
            string query = "UPDATE nhommonan SET TenNhom = @TenNhom" +
                " WHERE MaNhom = @foodGroupID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@TenNhom", nhomMonAn.TenNhom);
            DataProvider.GetInstance().AddParams("@foodGroupID", nhomMonAn.MaNhom);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }
    }
}
