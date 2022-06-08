using ComNha.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ComNha.Controllers
{
    public class MenuController : Controller
    {
        //Hosted web API REST Service base url
        private const string API_URL = "https://localhost:5556/";

        [HttpGet]
        [Route("ComNha/menu")]
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(API_URL);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("Menu");

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the FoodGroup List
                    ViewData["FoodGroupList"] = JsonConvert.DeserializeObject<List<Nhommonan>>(response);
                }
                else
                {
                    // Error message here
                    return NotFound();
                }
            }

            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(API_URL);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                // Default FoodGroup Is The First FoodGroup With Key 'NMA001'
                HttpResponseMessage Res = await client.GetAsync("Menu/NMA001");

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Food List
                    ViewData["FoodList"] = JsonConvert.DeserializeObject<List<Monan>>(response);
                    
                }
                else
                {
                    // Error message here
                    return NotFound();
                }
            }

            return View();
        }

        [HttpGet]
        [Route("Menu/chitiet/{foodID}")]
        public async Task<ActionResult> Detail(string foodID)
        { 
            if (foodID == null)
            {
                return NotFound();
            }

            Monan chitietmonan;
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(API_URL);
                client.DefaultRequestHeaders.Clear();

                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource using HttpClient
                HttpResponseMessage Res = await client.GetAsync("Menu/chitiet/" + foodID);

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Food Model
                    chitietmonan = JsonConvert.DeserializeObject<Monan>(response);
                    return View(chitietmonan);
                }
                else
                {
                    // Error message here
                    return NotFound();
                }
            }        
        }
    }
}
