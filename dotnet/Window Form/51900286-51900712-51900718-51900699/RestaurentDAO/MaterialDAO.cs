using RestaurentDTO;
using System.Collections.Generic;
using System.Data;

namespace RestaurentDAO
{
    public class MaterialDAO
    {
        private static MaterialDAO uniqueInstanceMaterialDAO;

        private MaterialDAO() { }

        public static MaterialDAO GetInstance()
        {
            if (uniqueInstanceMaterialDAO == null)
            {
                uniqueInstanceMaterialDAO = new MaterialDAO();
            };

            return uniqueInstanceMaterialDAO;
        }

        public List<NguyenVatLieu> GetAllMaterials()
        {
            List<NguyenVatLieu> nguyenVatLieus = new List<NguyenVatLieu>();
            string query = "SELECT * FROM nguyenvatlieu";

            DataTable data = DataProvider.GetInstance().ExecuteQuery(query);

            if (data.Rows.Count > 0)
            {
                foreach(DataRow dataRow in data.Rows){
                    NguyenVatLieu nguyenVatLieu = new NguyenVatLieu(dataRow);
                    nguyenVatLieus.Add(nguyenVatLieu);
                }             
            }

            return nguyenVatLieus;
        }

        public string GetMaxMaterialID()
        {
            string query = "SELECT MAX(MaNguyenVatLieu) AS 'MaxMaterialID' FROM nguyenvatlieu";

            DataTable result = DataProvider.GetInstance().ExecuteQuery(query);

            if (result.Rows.Count > 0)
            {
                return result.Rows[0]["MaxMaterialID"].ToString() == "" ? "NVL000" : result.Rows[0]["MaxMaterialID"].ToString();
            };

            return "NVL000";
        }

        public bool InsertNewMaterial(NguyenVatLieu material)
        {
            string query = "INSERT INTO nguyenvatlieu VALUES(@MaterialID, @MaterialName, @MaterialPrice, @MaterialUnit, @MaterialState, 0)";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@MaterialID", material.MaNguyenVatLieu);
            DataProvider.GetInstance().AddParams("@MaterialName", material.TenNguyenVatLieu);
            DataProvider.GetInstance().AddParams("@MaterialPrice", material.DonGia);
            DataProvider.GetInstance().AddParams("@MaterialUnit", material.DonVi);
            DataProvider.GetInstance().AddParams("@MaterialState", material.TinhTrang);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool UpdateMaterial(NguyenVatLieu material)
        {
            string query = "UPDATE nguyenvatlieu " +
                "SET TenNguyenVatLieu = @MaterialName, DonGia = @MaterialPrice, " +
                "DonVi = @MaterialUnit, TinhTrang = @MaterialState , SoLuongTonKho = @MaterialQuantityLeft" +
                " WHERE MaNguyenVatLieu = @MaterialID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@MaterialID", material.MaNguyenVatLieu);
            DataProvider.GetInstance().AddParams("@MaterialName", material.TenNguyenVatLieu);
            DataProvider.GetInstance().AddParams("@MaterialPrice", material.DonGia);
            DataProvider.GetInstance().AddParams("@MaterialUnit", material.DonVi);
            DataProvider.GetInstance().AddParams("@MaterialState", material.TinhTrang);
            DataProvider.GetInstance().AddParams("@MaterialQuantityLeft", material.SoLuongTonKho);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool DeleteMaterial(string materialID)
        {
            string query = "DELETE FROM nguyenvatlieu WHERE MaNguyenVatLieu = @MaterialID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@MaterialID", materialID);


            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }
    }
}
