using _51900286_51900712_51900718_51900699;
using RestaurentDTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace RestaurentDAO
{
    public class ImportDAO
    {
        private static ImportDAO uniqueInstanceImportDAO;

        private ImportDAO() { }

        public static ImportDAO GetInstance()
        {
            if (uniqueInstanceImportDAO == null)
            {
                uniqueInstanceImportDAO = new ImportDAO();
            };

            return uniqueInstanceImportDAO;
        }

        public List<PhieuNhap> GetAllImports()
        {
            var phieuNhaps = new List<PhieuNhap>();

            string query = "SELECT * FROM PhieuNhap";
            DataTable data = DataProvider.GetInstance().ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                var phieuNhap = new PhieuNhap(row);
                phieuNhaps.Add(phieuNhap);
            };

            return phieuNhaps;
        }

        public bool AddImport(DateTime date, double price, string note, string supplierID, string employeeID)
        {
            string id = DatabaseIDSupport.CreateNewMaxID(GetMaxImportID());

            string query = "insert into PhieuNhap values(@id, @date, @price, @note, @supplierID, @employeeID)";
            DataProvider.GetInstance().CreateCommand(query);

            // Bind params
            DataProvider.GetInstance().AddParams("@id", id);
            DataProvider.GetInstance().AddParams("@date", date);
            DataProvider.GetInstance().AddParams("@price", price);
            DataProvider.GetInstance().AddParams("@note", note);
            DataProvider.GetInstance().AddParams("@supplierID", supplierID);
            DataProvider.GetInstance().AddParams("@employeeID", employeeID);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool DeleteImport(string id)
        {
            string query = "delete from PhieuNhap where MaPhieuNhap = @id";
            DataProvider.GetInstance().CreateCommand(query);

            // bind params
            DataProvider.GetInstance().AddParams("@id", id);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool UpdateImport(string id, DateTime date, double price, string note, string supplierID, string employeeID)
        {
            string query = "update PhieuNhap set NgayNhapHang = @date, TongGiaNhap = @price, GhiChu = @note, MaNhaCungCap = @supplierID, MaNhanVien = @employeeID where MaPhieuNhap = @id";

            DataProvider.GetInstance().CreateCommand(query);

            // Bind params
            DataProvider.GetInstance().AddParams("@id", id);
            DataProvider.GetInstance().AddParams("@date", date);
            DataProvider.GetInstance().AddParams("@price", price);
            DataProvider.GetInstance().AddParams("@note", note);
            DataProvider.GetInstance().AddParams("@supplierID", supplierID);
            DataProvider.GetInstance().AddParams("@employeeID", employeeID);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public string GetMaxImportID()
        {
            string query = "SELECT MAX(MaPhieuNhap) as 'max' FROM PhieuNhap";

            DataTable data = DataProvider.GetInstance().ExecuteQuery(query);

            if (data.Rows.Count > 0)
            {
                return data.Rows[0]["max"].ToString() == "" ? "PN000" : data.Rows[0]["max"].ToString();
            };

            return "PN000";
        }
    }
}
