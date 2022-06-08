using RestaurentDTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace RestaurentDAO
{
    public class EmployeeDAO
    {
        private static EmployeeDAO uniqueInstanceEmployeeDAO;

        private EmployeeDAO() { }

        public static EmployeeDAO GetInstance()
        {
            if (uniqueInstanceEmployeeDAO == null)
            {
                uniqueInstanceEmployeeDAO = new EmployeeDAO();
            };

            return uniqueInstanceEmployeeDAO;
        }

        public NhanVien GetEmployeeByID(string employeeID)
        {
            string query = "SELECT * FROM nhanvien WHERE MaNhanVien = @employeeID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@employeeID", employeeID);

            DataTable result = DataProvider.GetInstance().ExecuteQueryWithParams();

            if (result.Rows.Count > 0)
            {
                return new NhanVien(result.Rows[0]);
            }

            return new NhanVien();
        }

        public List<NhanVien> GetEmployeeList()
        {
            List<NhanVien> nhanViens = new List<NhanVien>();
            string query = "SELECT * FROM nhanvien";
    
            DataTable result = DataProvider.GetInstance().ExecuteQuery(query);

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    NhanVien nhanvien = new NhanVien(row);
                    nhanViens.Add(nhanvien);
                };

                return nhanViens;
            }

            return nhanViens;
        }

        public bool DeleteEmployeeByID(string employeeID)
        {
            string query = "DELETE FROM nhanvien WHERE MaNhanVien = @employeeID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@employeeID", employeeID);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool UpdateEmployeeByID(NhanVien nhanVien)
        {
            string query = "UPDATE nhanvien SET TenNhanVien = @TenNhanVien, SoDienThoai = @SoDienThoai, Luong = @Luong, " +
                "ChucVu= @ChucVu, DiaChi = @DiaChi" +
                " WHERE MaNhanVien = @employeeID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@TenNhanVien", nhanVien.TenNhanVien);
            DataProvider.GetInstance().AddParams("@SoDienThoai", nhanVien.SoDienThoai);
            DataProvider.GetInstance().AddParams("@Luong", nhanVien.Luong);
            DataProvider.GetInstance().AddParams("@ChucVu", nhanVien.ChucVu);
            DataProvider.GetInstance().AddParams("@DiaChi", nhanVien.DiaChi);
            DataProvider.GetInstance().AddParams("@employeeID", nhanVien.MaNhanVien);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public string GetMaxEmployeeID()
        {
            string query = "SELECT MAX(MaNhanVien) AS 'MaxEmployeeID' FROM nhanvien";

            DataTable result = DataProvider.GetInstance().ExecuteQuery(query);

            if ( result.Rows.Count > 0)
            {
                return result.Rows[0]["MaxEmployeeID"].ToString() == "" ? "NV000" : result.Rows[0]["MaxEmployeeID"].ToString();
            };

            return "NV000";
        }

        public bool AddEmployee(NhanVien nhanVien)
        {
            string query = "INSERT INTO nhanvien VALUES(@employeeID, @TenNhanVien, @SoDienThoai, @Luong, @ChucVu, @DiaChi, @Email)";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@employeeID", nhanVien.MaNhanVien);
            DataProvider.GetInstance().AddParams("@TenNhanVien", nhanVien.TenNhanVien);
            DataProvider.GetInstance().AddParams("@SoDienThoai", nhanVien.SoDienThoai);
            DataProvider.GetInstance().AddParams("@Luong", nhanVien.Luong);
            DataProvider.GetInstance().AddParams("@ChucVu", nhanVien.ChucVu);
            DataProvider.GetInstance().AddParams("@DiaChi", nhanVien.DiaChi);
            DataProvider.GetInstance().AddParams("@Email", nhanVien.Email);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool CheckExistEmployeeEmail(string employeeEmail)
        {
            string query = "SELECT * FROM nhanvien WHERE Email = @employeeEmail";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@employeeEmail", employeeEmail);

            return DataProvider.GetInstance().ExecuteQueryWithParams().Rows.Count > 0;
        }
    }
}
