using ComNha_API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ComNha_API.Controllers
{
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }
       
        [EnableCors("AllowOrigin")]
        [HttpPost("DatBan")]
        public IActionResult CreateBook(Booking bookingInfo)
        {
            if ( bookingInfo == null ){
                return new JsonResult(new { code = 4, status = false, message = "Vui lòng nhập đủ thông tin trước khi nhấn nút 'Đặt bàn'." });    
            }

            if ( String.IsNullOrEmpty(bookingInfo.Name) ||
            String.IsNullOrEmpty(bookingInfo.Phone) ||
            String.IsNullOrEmpty(bookingInfo.Quantity.ToString()) ||
            String.IsNullOrEmpty(bookingInfo.Date.ToString()) ||
            String.IsNullOrEmpty(bookingInfo.Time.ToString()) ||
            String.IsNullOrEmpty(bookingInfo.Note) 
            ){
                return new JsonResult(new { code = 4, status = false, message = "Vui lòng nhập đủ thông tin trước khi nhấn nút 'Đặt bàn'." });
            }

            using ( var context = new quanlynhahangContext()){
                context.Khachhangs.Add(new Khachhang { SoDienThoai = bookingInfo.Phone, TenKhachHang = bookingInfo.Name});
                context.SaveChanges();

				var maxMaLicHhen = context.Lichhens.Max(x => x.MaLichHen);
				
				if (maxMaLicHhen != null ){
                    int currentMax = int.Parse(maxMaLicHhen.ToString().Substring(maxMaLicHhen.Length - 3));
                    currentMax += 1;

					maxMaLicHhen = maxMaLicHhen.ToString().Substring(0, maxMaLicHhen.Length - 3) + currentMax.ToString().PadLeft(3, '0');
                } else {
					maxMaLicHhen = "LH001";
				}

                var maKhachHang = context.Khachhangs.Max(Khachhang => Khachhang.MaKhachHang);

                context.Lichhens.Add(new Lichhen { MaLichHen = maxMaLicHhen, NhuCau = bookingInfo.Note, SoLuongKhach = int.Parse(bookingInfo.Quantity), ThoiGian = TimeSpan.Parse(bookingInfo.Time), NgayHen = DateTime.Parse(bookingInfo.Date), MaKhachHang = maKhachHang });
                context.SaveChanges();
            }
			
            return new JsonResult(new { code = 1, status = true, message = "Gửi yêu cầu đặt bàn thành công" });
        }
    }

    public class Booking
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Quantity { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Note { get; set; }
    }
}