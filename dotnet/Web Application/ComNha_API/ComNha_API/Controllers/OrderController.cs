using ComNha_API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComNha_API.Controllers
{
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }


        [EnableCors("AllowOrigin")]
        [HttpGet("ComNha/MemberInfo/{memberID}")]
        public Hoivien GetMemberInfo(string memberID)
        {
            using (var context = new quanlynhahangContext())
            {
                return context.Hoiviens.FirstOrDefault(hoivien => hoivien.SoDienThoai.Equals(memberID));
            }         
        }

        [EnableCors("AllowOrigin")]
        [HttpPost("ComNha/DatMonTrucTuyen")]
        public IActionResult CreateOrder(YeuCauDatMon orderDetail)
        {
            using (var context = new quanlynhahangContext())
            {
                orderDetail.MemberID = String.IsNullOrEmpty(orderDetail.MemberID) ? null : orderDetail.MemberID;

                // Kiểm tra món ăn có hợp lệ không, đồng thời tổng tiền lại có bằng tiền tính tạm không
                if (!checkFoodsAndPrice(orderDetail.OrderFood, orderDetail.TempPrice))
                {
                    return new JsonResult(new { code = 4, status = false, message = "Vui lòng đặt món lại ! Hệ thống không hỗ trợ giảm tiền ăn chùa !" });
                }

                // Kiểm tra phí ship
                if (!checkShippingFee(orderDetail.ShippingFee, orderDetail.TempPrice))
                {
                    return new JsonResult(new { code = 4, status = false, message = "Phí ship không hợp lệ" });
                }

                // Kiểm tra xem không có mã khách hàng mà có số tiền giảm
                if (String.IsNullOrEmpty(orderDetail.MemberID) && orderDetail.DiscountFee != 0)
                {
                    return new JsonResult(new { code = 4, status = false, message = "Vui lòng đặt món lại ! Hệ thống không hỗ trợ giảm tiền ăn chùa !" });
                }

                // Xem khách hàng có sử dụng mã hội viên không
                if (String.IsNullOrEmpty(orderDetail.MemberID))
                {
                    // Kiểm tra mã hội viên và số tiền giảm có hợp lệ không
                    if (checkMemberAndDiscount(orderDetail.MemberID, orderDetail.TempPrice, orderDetail.ShippingFee, orderDetail.DiscountFee))
                    {
                        double discountPercent = (orderDetail.DiscountFee) / (orderDetail.TempPrice);
                        // Kiểm tra tổng tiền lúc sau có hợp lệ không
                        if (checkTotalPrice(orderDetail.TempPrice, orderDetail.ShippingFee, orderDetail.DiscountFee, orderDetail.TotalPrice))
                        {
                            Hoivien hoiVien = (from x in context.Hoiviens
                                          where x.SoDienThoai == orderDetail.MemberID
                                          select x).First();
                            hoiVien.TongSoTienThanhToan += orderDetail.TotalPrice;
                            context.SaveChanges();
                        }
                        else
                        {
                            return new JsonResult(new { code = 4, status = false, message = "Mã hội viên không hợp lệ" });
                        }
                    }                    
                }

                // Kiểm tra tổng tiền lúc sau có hợp lệ không
                if (!checkTotalPrice(orderDetail.TempPrice, orderDetail.ShippingFee, orderDetail.DiscountFee, orderDetail.TotalPrice))
                {
                    return new JsonResult(new { code = 4, status = false, message = "" });
                }

                context.Khachhangs.Add(new Khachhang { SoDienThoai = orderDetail.Phone, TenKhachHang = orderDetail.Name, DiaChi = orderDetail.Address, Email = orderDetail.Email });
                context.SaveChanges();

                var customerID = context.Khachhangs.Max(Khachhang => Khachhang.MaKhachHang);

                // Tạo phiếu gọi món mới
                string orderID = createNewOrderID();
                context.Phieugoimons.Add(new Phieugoimon { MaOrder = orderID, GhiChuMonAn = orderDetail.Message });
                context.SaveChanges();

                // Nhập thông tin vào chi tiết gọi món
                foreach (Food food in orderDetail.OrderFood)
                {
                    context.Chitietgoimons.Add(new Chitietgoimon { MaOrder = orderID, MaMonAn = food.FoodID, SoLuongMonAn = food.Quantity });
                    context.SaveChanges();
                }

                // Tạo hóa đơn mới
                string billID = createNewBillID();
                double total = orderDetail.TempPrice + orderDetail.ShippingFee;
                double discountPercent1 = (orderDetail.DiscountFee) / (orderDetail.TempPrice);
                context.Hoadons.Add(new Hoadon { MaHoaDon = billID, TongTien = total, ChietKhau = discountPercent1, TinhTrang = 1, MaKhachHang = customerID, MaHoiVien = orderDetail.MemberID, MaOrder = orderID });
                context.SaveChanges();

                return new JsonResult(new { code = 1, status = true, message = "Đặt hàng thành công", billID = billID });
            }
        }

        private double getDiscount(double totalMoney)
        {
            double discountPercent = 0.02;
            if (totalMoney <= 3000000) {
                discountPercent = 0.02;
            } else if (totalMoney <= 5000000) {
                discountPercent = 0.05;
            } else if (totalMoney <= 10000000) {
                discountPercent = 0.08;
            } else if (totalMoney <= 15000000) {
                discountPercent = 0.1;
            } else if (totalMoney <= 20000000) {
                discountPercent = 0.12;
            } else
            {
                discountPercent = 0.15;
            }

            return discountPercent;
        }

        // Kiểm tra Id hội viên được gửi lên và số tiền giảm giá có khớp nhau không
        private bool checkMemberAndDiscount(string memberID, double tempPrice, double shippingFee, double discountFee)
        {
            using ( var context = new quanlynhahangContext())
            {
                var hoiVien = context.Hoiviens.Where(hoivien => hoivien.SoDienThoai.Equals(memberID)).FirstOrDefault();

                if ( hoiVien == null)
                {
                    return false;
                }

                double discountPercent = getDiscount(hoiVien.TongSoTienThanhToan);
                if ( discountFee == (tempPrice + shippingFee) * discountPercent)
                {
                    return true;
                }
            }
            return false;
        }

        // Kiểm tra món ăn và tổng tiền có hợp lệ không
        private bool checkFoodsAndPrice(List<Food> orderFood, double tempPrice)
        {
            double price = 0;

            using (var context = new quanlynhahangContext())
            {
                foreach (Food food in orderFood)
                {
                    var foodDetail = context.Monans.FirstOrDefault(foodDetail => foodDetail.MaMonAn.Equals(food.FoodID));

                    if ( foodDetail == null)
                    {
                        return false;
                    }

                    if (food.Quantity * food.Price != food.Quantity * foodDetail.DonGia)
                    {
                        return false;
                    };

                    price += foodDetail.DonGia * food.Quantity;
                }

                return price == tempPrice;
            }
        }

        // Kiểm tra phí ship
        private bool checkShippingFee(double shippingFee, double tempPrice)
        {
            if (tempPrice <= 1000000 && shippingFee != 50000) {
                return false;
            } else if (tempPrice > 1000000 && shippingFee != 0) {
                return false;
            }

            return true;
        }

        // Kiểm tra số tiền cuối cùng
        private bool checkTotalPrice(double tempPrice, double shippingFee, double discountFee, double totalPrice)
        {
            if (totalPrice != (tempPrice + shippingFee - discountFee)) {
                return false;
            }
            return true;
        }

        // Lấy mã phiếu gọi món mới
        private string createNewOrderID()
        {
            using (var context = new quanlynhahangContext())
            {
                var maxPhieuGoiMon = context.Phieugoimons.Max(x => x.MaOrder);

                if (maxPhieuGoiMon != null)
                {
                    int currentMax = int.Parse(maxPhieuGoiMon.ToString().Substring(maxPhieuGoiMon.Length - 3));
                    currentMax += 1;

                    maxPhieuGoiMon = maxPhieuGoiMon.ToString().Substring(0, maxPhieuGoiMon.Length - 3) + currentMax.ToString().PadLeft(3, '0');
                }
                else
                {
                    maxPhieuGoiMon = "PGM001";
                }
                return maxPhieuGoiMon;
            }       
        }

        // Lấy mã hóa đơn mới
        private string createNewBillID()
        {
            using (var context = new quanlynhahangContext())
            {
                var maxMaHoaDon = context.Hoadons.Max(x => x.MaHoaDon);

                if (maxMaHoaDon != null)
                {
                    int currentMax = int.Parse(maxMaHoaDon.ToString().Substring(maxMaHoaDon.Length - 3));
                    currentMax += 1;

                    maxMaHoaDon = maxMaHoaDon.ToString().Substring(0, maxMaHoaDon.Length - 3) + currentMax.ToString().PadLeft(3, '0');
                }
                else
                {
                    maxMaHoaDon = "HD001";
                }
                return maxMaHoaDon;
            }
        }
    }

    public class YeuCauDatMon
    {
        public string Name { get; set; }

        public string MemberID { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public List<Food> OrderFood { get; set; }

        public string Message { get; set; }

        public bool PaymentComNhaValid { get; set; }

        public double TempPrice { get; set; }

        public double DiscountFee { get; set; }

        public double ShippingFee { get; set; }

        public double TotalPrice { get; set; }
    }

    public class Food
    {
        public string FoodID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

    }
}
