using Microsoft.AspNetCore.Mvc;

namespace ComNha.Controllers
{
    public class CartController : Controller
    {
        [HttpGet]
        [Route("ComNha/cart")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
