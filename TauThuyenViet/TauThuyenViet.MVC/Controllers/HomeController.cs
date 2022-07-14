using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TauThuyenViet.Models;

namespace TauThuyenViet.Controllers
{
    public class HomeController : Controller
    {

        private readonly HttpClient client;
        public HomeController(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("default");
        }


        [Route("/", Name = "Home")]
        [Route("/home", Name = "HomeEngLish")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Tin Tức";

            var response = await client.GetAsync("api/ProductMainCategories/NestedProduct");
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();
            string json = response.Content.ReadAsStringAsync().Result;
            List<ProductMainCategory> data = JsonConvert.DeserializeObject<List<ProductMainCategory>>(json);

            return View(data);

        }
    }
}
