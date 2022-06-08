using RestaurentDTO;
using System.Data;
using CryptSharp;

namespace RestaurentDAO
{
    public class AccountDAO
    {
        private static AccountDAO uniqueInstanceAccountDAO;

        private AccountDAO() { }

        public static AccountDAO GetInstance()
        {
            if (uniqueInstanceAccountDAO == null)
            {
                uniqueInstanceAccountDAO = new AccountDAO();
            };

            return uniqueInstanceAccountDAO;
        }

        public bool ValidateLogin(string username, string password)
        {
            // Hash Password and Select - We Can use SQL Parameter to prevent SQL injection
            string query = "SELECT MatKhau FROM taikhoan WHERE TenTaiKhoan = @username";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@username", username);

            // Execute Query Use for SELECT STATEMENT AND CHECK IF ROWS COUNT > 0
            var data = DataProvider.GetInstance().ExecuteQueryWithParams();
            if( data.Rows.Count < 1 )
            {
                return false;
            }
            return Crypter.CheckPassword(password, data.Rows[0]["MatKhau"].ToString());
        }

        public TaiKhoan GetAccountByUsername(string username)
        {
            string query = "SELECT * FROM taikhoan WHERE TenTaiKhoan = @username";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@username", username);

            DataTable result = DataProvider.GetInstance().ExecuteQueryWithParams();

            if (result.Rows.Count > 0)
            {
                return new TaiKhoan(result.Rows[0]);
            }

            return new TaiKhoan();
        }

        public TaiKhoan GetAccountByEmployeeID(string employeeID)
        {
            string query = "SELECT * FROM taikhoan WHERE MaNhanVien = @employeeID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@employeeID", employeeID);

            DataTable result = DataProvider.GetInstance().ExecuteQueryWithParams();

            if (result.Rows.Count > 0)
            {
                return new TaiKhoan(result.Rows[0]);
            }

            return new TaiKhoan();
        }

        public bool UpdateNewPassword(string username, string newPassword)
        {
            string query = "UPDATE taikhoan SET MatKhau = @MatKhau WHERE TenTaiKhoan = @TenTaiKhoan";

            string hash = Crypter.Blowfish.Crypt(newPassword);

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@TenTaiKhoan", username);
            DataProvider.GetInstance().AddParams("@MatKhau", hash);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool CreateEmployeeAccount(NhanVien nhanVien)
        {
            string query = "INSERT INTO taikhoan VALUES(@TenTaiKhoan, @MatKhau, @MaNhanVien)";

            string hash = Crypter.Blowfish.Crypt(nhanVien.SoDienThoai);

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@TenTaiKhoan", nhanVien.Email);
            DataProvider.GetInstance().AddParams("@MatKhau", hash);
            DataProvider.GetInstance().AddParams("@MaNhanVien", nhanVien.MaNhanVien);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }
    }
}
