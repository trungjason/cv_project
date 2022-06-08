using _51900286_51900712_51900718_51900699;
using RestaurentDTO;
using System.Collections.Generic;
using System.Data;

namespace RestaurentDAO
{
    public class SupplierDAO
    {
        private static SupplierDAO uniqueInstanceSupplierDAO;

        private SupplierDAO() { }

        public static SupplierDAO GetInstance()
        {
            if (uniqueInstanceSupplierDAO == null)
            {
                uniqueInstanceSupplierDAO = new SupplierDAO();
            };

            return uniqueInstanceSupplierDAO;
        }

        public List<NhaCungCap> GetAllSuppliers()
        {
            var suppliers = new List<NhaCungCap>();

            string query = "SELECT * FROM NhaCungCap";
            DataTable data = DataProvider.GetInstance().ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                var MonAn = new NhaCungCap(row);
                suppliers.Add(MonAn);
            };

            return suppliers;
        }

        public bool AddSupplier(string name, string desc, string address, string phone)
        {
            string id = DatabaseIDSupport.CreateNewMaxID(GetMaxSupplierID());

            string query = "insert into NhaCungCap values(@id, @desc, @name, @address, @phone)";
            DataProvider.GetInstance().CreateCommand(query);

            // Bind params
            DataProvider.GetInstance().AddParams("@id", id);
            DataProvider.GetInstance().AddParams("@name", name);
            DataProvider.GetInstance().AddParams("@desc", desc);
            DataProvider.GetInstance().AddParams("@address", address);
            DataProvider.GetInstance().AddParams("@phone", phone);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool DeleteSupplier(string id)
        {
            string query = "delete from NhaCungCap where MaNhaCungCap = @id";
            DataProvider.GetInstance().CreateCommand(query);

            // bind params
            DataProvider.GetInstance().AddParams("@id", id);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool UpdateSupplier(string id, string name, string desc, string address, string phone)
        {
            string query = "update NhaCungCap set TenNhaCungCap = @name, MoTaNhaCungCap = @desc, DiaChi = @address, SoDienThoai = @phone where MaNhaCungCap = @id";

            DataProvider.GetInstance().CreateCommand(query);

            // Bind params
            DataProvider.GetInstance().AddParams("@id", id);
            DataProvider.GetInstance().AddParams("@name", name);
            DataProvider.GetInstance().AddParams("@desc", desc);
            DataProvider.GetInstance().AddParams("@address", address);
            DataProvider.GetInstance().AddParams("@phone", phone);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public string GetMaxSupplierID()
        {
            string query = "SELECT MAX(MaNhaCungCap) as 'max' FROM NhaCungCap";

            DataTable data = DataProvider.GetInstance().ExecuteQuery(query);

            if (data.Rows.Count > 0)
            {
                return data.Rows[0]["max"].ToString() == "" ? "NCC000" : data.Rows[0]["max"].ToString();
            };

            return "NCC000";
        }
    }
}
