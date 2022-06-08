using Microsoft.AspNetCore.Mvc;

namespace ComNha.Controllers
{
    public class CheckoutsController : Controller
    {
        [HttpGet]
        [Route("ComNha/checkouts")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
