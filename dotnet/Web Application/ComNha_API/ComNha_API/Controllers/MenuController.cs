using ComNha_API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ComNha_API.Controllers
{
    [EnableCors("AllowOrigin")]
    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly ILogger<MenuController> _logger;

        public MenuController(ILogger<MenuController> logger)
        {
            _logger = logger;
        }
       
        [HttpGet]
        public IEnumerable<Nhommonan> Get()
        {
            using ( var context = new quanlynhahangContext())
            {
                return context.Nhommonans.ToList();
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("{MaNhom}")]
        public IEnumerable<Monan> GetListFood(string MaNhom)
        {
            using (var context = new quanlynhahangContext())
            {
                var monans = from monan in context.Monans
                             join nhommonan in context.Nhommonans on monan.MaNhom equals nhommonan.MaNhom
                             where nhommonan.MaNhom == MaNhom.ToString()
                             select monan; 

                return monans.ToList();
            }
        }

        [EnableCors("AllowOrigin")]
        [HttpGet("chitiet/{MaMonAn}")]
        public Monan GetFoodDetail(string MaMonAn)
        {
            using (var context = new quanlynhahangContext())
            {                             
                return context.Monans.FirstOrDefault(monan => monan.MaMonAn == MaMonAn);
            }
        }
    }
}