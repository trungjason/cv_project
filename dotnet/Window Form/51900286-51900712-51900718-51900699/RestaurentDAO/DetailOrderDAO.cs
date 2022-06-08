using System;
using System.Data;

namespace RestaurentDAO
{
    public class DetailOrderDAO
    {
        private static DetailOrderDAO uniqueInstanceDetailOrderDAO;

        private DetailOrderDAO(){}

        public static DetailOrderDAO GetInstance()
        {
            if (uniqueInstanceDetailOrderDAO == null)
            {
                uniqueInstanceDetailOrderDAO = new DetailOrderDAO();
            };

            return uniqueInstanceDetailOrderDAO;
        }
    
        public bool InsertNewDetailOrder(string foodID, int foodQuantity)
        {
            // FUNCTION DETAIL: Tạo hóa chi tiết hóa đơn mới dựa trên: Mã món ăn và Số lượng món ăn, ID chi tiết hóa đơn được tạo tự động
            // Lấy mã order mới nhất từ phiếu gọi món
            string orderID = OrderDAO.GetInstance().GetMaxOrderID(); // ?
            string query = "INSERT INTO chitietgoimon VALUES(@foodID, @orderID, @foodQuantity)";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@foodID", foodID);
            DataProvider.GetInstance().AddParams("@orderID", orderID);
            DataProvider.GetInstance().AddParams("@foodQuantity", foodQuantity);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool CheckExistFoodInDetailOrderByOrderID(string foodID, string detailOrderID)
        {
            // FUNCTION DETAIL: Kiểm tra món ăn có tồn tại trong chi tiết gọi món hay không dựa trên mã món ăn và mã phiếu gọi món

            string query = "SELECT * FROM chitietgoimon WHERE MaMonAn = @foodID AND MaOrder = @detailOrderID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@foodID", foodID);
            DataProvider.GetInstance().AddParams("@detailOrderID", detailOrderID);

            return DataProvider.GetInstance().ExecuteQueryWithParams().Rows.Count > 0;
        }

        public bool CheckExistDetailOrderByOrderID(string detailOrderID)
        {
            // FUNCTION DETAIL: Kiểm tra chi tiết gọi món còn món ăn nào không
         
            string query = "SELECT * FROM chitietgoimon WHERE MaOrder = @detailOrderID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@detailOrderID", detailOrderID);

            return DataProvider.GetInstance().ExecuteQueryWithParams().Rows.Count > 0;
        }

        public bool UpdateFoodQuantityByFoodIDAndDetailOrderID(string foodID, string existDetailOrderID, int foodQuantity)
        {
            // FUNCTION DETAIL: Cập nhật số lượng món trong trong chi tiết gọi món 
            // Nếu số lượng là âm
            if (foodQuantity < 0)
            {
                // Kiểm tra nếu số lượng trong chi tiết gọi món - cho số lượng user chọn
                string getFoodQuantity = "SELECT SoLuongMonAn FROM chitietgoimon " +
                    "WHERE MaMonAn = @foodID AND MaOrder = @existDetailOrderID";

                DataProvider.GetInstance().CreateCommand(getFoodQuantity);
                DataProvider.GetInstance().AddParams("@foodID", foodID);
                DataProvider.GetInstance().AddParams("@existDetailOrderID", existDetailOrderID);

                DataTable data = DataProvider.GetInstance().ExecuteQueryWithParams();

                if ( data.Rows.Count > 0)
                {
                    int currentFoodQuantity = (int)data.Rows[0]["SoLuongMonAn"];
                    currentFoodQuantity = currentFoodQuantity + foodQuantity;

                    // Nếu là dương thì update trừ số lượng
                    if ( currentFoodQuantity > 0)
                    {
                        string query = "UPDATE chitietgoimon SET SoLuongMonAn = @currentFoodQuantity " +
                        "WHERE MaMonAn = @foodID AND MaOrder = @existDetailOrderID";

                        DataProvider.GetInstance().CreateCommand(query);
                        DataProvider.GetInstance().AddParams("@currentFoodQuantity", currentFoodQuantity);
                        DataProvider.GetInstance().AddParams("@foodID", foodID);
                        DataProvider.GetInstance().AddParams("@existDetailOrderID", existDetailOrderID);

                        return DataProvider.GetInstance().ExecuteNonQueryWithParams();
                    } else
                    {
                        DeleteFoodInDetailOrder(foodID, existDetailOrderID);
                    }
                };

                return false;
            }
            else
            {
                // Nếu số lượng món ăn là dương chỉ cần update lại + thêm
                string query = "UPDATE chitietgoimon SET SoLuongMonAn = SoLuongMonAn + @foodQuantity " +
                    "WHERE MaMonAn = @foodID AND MaOrder = @existDetailOrderID";
          
                DataProvider.GetInstance().CreateCommand(query);
                DataProvider.GetInstance().AddParams("@foodQuantity", foodQuantity);
                DataProvider.GetInstance().AddParams("@foodID", foodID);
                DataProvider.GetInstance().AddParams("@existDetailOrderID", existDetailOrderID);

                return DataProvider.GetInstance().ExecuteNonQueryWithParams();
            }
        }

        public bool InsertNewFoodByFoodIDAndDetailOrderID(string foodID, string existDetailOrderID, int foodQuantity)
        {
            string query = "INSERT INTO chitietgoimon VALUES(@foodID, @existDetailOrderID, @foodQuantity)";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@foodID", foodID);
            DataProvider.GetInstance().AddParams("@existDetailOrderID", existDetailOrderID);
            DataProvider.GetInstance().AddParams("@foodQuantity", foodQuantity);
            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }

        public bool DeleteFoodInDetailOrder(string foodID, string existDetailOrderID)
        {
            string query = "DELETE FROM chitietgoimon " +
                "WHERE MaMonAn = @foodID AND MaOrder = @existDetailOrderID";

            DataProvider.GetInstance().CreateCommand(query);
            DataProvider.GetInstance().AddParams("@foodID", foodID);
            DataProvider.GetInstance().AddParams("@existDetailOrderID", existDetailOrderID);

            return DataProvider.GetInstance().ExecuteNonQueryWithParams();
        }
    }
}
