using _51900286_51900712_51900718_51900699;
using RestaurentDTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace RestaurentDAO
{
    public class DetailImportDAO
    {
        private static DetailImportDAO uniqueInstanceDetailImportDAO;

        private DetailImportDAO() { }

        public static DetailImportDAO GetInstance()
        {
            if (uniqueInstanceDetailImportDAO == null)
            {
                uniqueInstanceDetailImportDAO = new DetailImportDAO();
            };

            return uniqueInstanceDetailImportDAO;
        }

        public List<ChiTietPhieuNhap> GetAllDetailImportByID(string id)
        {
            var phieuNhaps = new List<ChiTietPhieuNhap>();

            string query = "SELECT * FROM ChiTietPhieuNhap where MaPhieuNhap = @id";
            DataProvider.GetInstance().CreateCommand(query);

            DataProvider.GetInstance().AddParams("@id", id);

            DataTable data = DataProvider.GetInstance().ExecuteQueryWithParams();

            foreach (DataRow row in data.Rows)
            {
                var phieuNhap = new ChiTietPhieuNhap(row);
                phieuNhaps.Add(phieuNhap);
            };

            return phieuNhaps;
        }

        public bool AddDetailImport(string idNVL, string id, int quantity)
        {
            string query = "insert into ChiTietPhieuNhap values(@idNVL, @id, @quantity)";
            DataProvider.GetInstance().CreateCommand(query);

            // Bind params
            
            DataProvider.GetInstance().AddParams("@idNVL", idNVL);
            DataProvider.GetInstance().AddParams("@id", id);
            DataProvider.GetInstance().AddParams("@quantity", quantity);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool DeleteDetailImport(string idNVL, string id)
        {
            string query = "delete from ChiTietPhieuNhap where MaPhieuNhap = @id and MaNguyenVatLieu = @idNVL";
            DataProvider.GetInstance().CreateCommand(query);

            // bind params
            DataProvider.GetInstance().AddParams("@idNVL", idNVL);
            DataProvider.GetInstance().AddParams("@id", id);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool UpdateDetailImport(string idNVL, string id, int quantity)
        {
            string query = "update ChiTietPhieuNhap set SoLuongNhap = @quantity where MaPhieuNhap = @id and MaNguyenVatLieu = @idNVL";

            DataProvider.GetInstance().CreateCommand(query);

            // Bind params
            DataProvider.GetInstance().AddParams("@idNVL", idNVL);
            DataProvider.GetInstance().AddParams("@id", id);
            DataProvider.GetInstance().AddParams("@quantity", quantity);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }
    }
}
