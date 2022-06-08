using RestaurentDTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace RestaurentDAO
{
    public class MemberDAO
    {
        private static MemberDAO uniqueInstanceMemberDAO;

        private MemberDAO() { }

        public static MemberDAO GetInstance()
        {
            if (uniqueInstanceMemberDAO == null)
            {
                uniqueInstanceMemberDAO = new MemberDAO();
            };

            return uniqueInstanceMemberDAO;
        }

        public List<HoiVien> GetAllMember()
        {
            List<HoiVien> hoiViens = new List<HoiVien>();

            string query = "SELECT * FROM hoivien";
            DataTable data = DataProvider.GetInstance().ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                HoiVien hoivien = new HoiVien(row);
                hoiViens.Add(hoivien);
            };

            return hoiViens;
        }

        public bool InsertNewMember(HoiVien hoiVien)
        {
            string query = "INSERT INTO hoivien VALUES(@TenHoiVien, @SoDienThoai, @TongSoTienThanhToan, @Email, @DiaChi, @QuyenLoi)";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@TenHoiVien", hoiVien.TenHoiVien);
            DataProvider.GetInstance().AddParams("@SoDienThoai", hoiVien.SoDienThoai);
            DataProvider.GetInstance().AddParams("@TongSoTienThanhToan", hoiVien.TongSoTienThanhToan);
            DataProvider.GetInstance().AddParams("@Email", hoiVien.Email);
            DataProvider.GetInstance().AddParams("@DiaChi", hoiVien.DiaChi);
            DataProvider.GetInstance().AddParams("@QuyenLoi", hoiVien.QuyenLoi);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool UpdateMember(HoiVien hoiVien)
        {
            string query = "UPDATE hoivien " +
                "SET TenHoiVien = @TenHoiVien, Email = @Email, DiaChi = @DiaChi, QuyenLoi = @QuyenLoi " +
                "WHERE SoDienThoai = @SoDienThoai";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@TenHoiVien", hoiVien.TenHoiVien);
            DataProvider.GetInstance().AddParams("@SoDienThoai", hoiVien.SoDienThoai);
            DataProvider.GetInstance().AddParams("@Email", hoiVien.Email);
            DataProvider.GetInstance().AddParams("@DiaChi", hoiVien.DiaChi);
            DataProvider.GetInstance().AddParams("@QuyenLoi", hoiVien.QuyenLoi);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool UpdateMemberTotalPay(string memberPhoneNumber, double totalPrice)
        {
            string query = "UPDATE hoivien " +
                "SET TongSoTienThanhToan = TongSoTienThanhToan + @totalPrice " +
                "WHERE SoDienThoai = @memberPhoneNumber";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@totalPrice", totalPrice);
            DataProvider.GetInstance().AddParams("@memberPhoneNumber", memberPhoneNumber);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool CheckExistsMemberByPhoneNumber(string memberPhoneNumber)
        {
            string query = "SELECT * FROM hoivien WHERE SoDienThoai = @memberPhoneNumber";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@memberPhoneNumber", memberPhoneNumber);

            return DataProvider.GetInstance().ExecuteQueryWithParams().Rows.Count > 0;
        }

        public double GetTotalPaidByPhoneNumber(string memberPhoneNumber)
        {
            string query = "SELECT * FROM hoivien WHERE SoDienThoai = @memberPhoneNumber";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@memberPhoneNumber", memberPhoneNumber);

            DataTable data = DataProvider.GetInstance().ExecuteQueryWithParams();

            if (data.Rows.Count > 0)
            {
                return (double)data.Rows[0]["TongSoTienThanhToan"];
            }

            return 0;
        }
    }
}
