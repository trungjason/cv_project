using ComNha_API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ComNha_API.Controllers
{
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly ILogger<MemberController> _logger;

        public MemberController(ILogger<MemberController> logger)
        {
            _logger = logger;
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("{sdt}/discount")]
        public IActionResult GetMemberDiscount(string sdt)
        {
            using (var context = new quanlynhahangContext())
            {
                var hoivien = context.Hoiviens.FirstOrDefault(m => m.SoDienThoai == sdt);
                if (hoivien == null) return new JsonResult(new { status = false, message = "Mã hội viên không tồn tại." });

                if (hoivien.TongSoTienThanhToan > 20000000)
                {
                    return new JsonResult(new { status = true, discount = 0.15 });
                }
                if (hoivien.TongSoTienThanhToan > 15000000)
                {
                    return new JsonResult(new { status = true, discount = 0.12 });
                }
                if (hoivien.TongSoTienThanhToan > 10000000)
                {
                    return new JsonResult(new { status = true, discount = 0.1 });
                }
                if (hoivien.TongSoTienThanhToan > 5000000)
                {
                    return new JsonResult(new { status = true, discount = 0.08 });
                }
                if (hoivien.TongSoTienThanhToan > 3000000)
                {
                    return new JsonResult(new { status = true, discount = 0.05 });
                }
                return new JsonResult(new { status = true, discount = 0.02 });
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpPost("{sdt}/change-password")]
        public IActionResult ChangeMemberPassword(string sdt, Password password)
        {
            using (var context = new quanlynhahangContext())
            {
                var hoivien = context.Hoiviens.FirstOrDefault(m => m.SoDienThoai == sdt);
                if(hoivien==null) return new JsonResult(new { status = false, message = "Mã hội viên không tồn tại." });

                if (!BCrypt.Net.BCrypt.Verify(password.oldPassword, hoivien.MatKhau))
                {
                    return new JsonResult(new { status = false, message = "Mật khẩu cũ không chính xác." });
                }
                hoivien.MatKhau = BCrypt.Net.BCrypt.HashPassword(password.newPassword);
                context.SaveChanges();
                return new JsonResult(new { status = true, message = "Đổi mật khẩu thành công." });
            }
        }
    }

    public class Password
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}