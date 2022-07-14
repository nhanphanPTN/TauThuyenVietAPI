using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TauThuyenViet.Models;

namespace TauThuyenViet.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient client;
        public ProductController(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("default");
        }

        [Route("/san-pham", Name = "ProductList")]
        [Route("/san-pham/{mid}/{cid}/{title}", Name = "ProductListByID")]
        public async Task<IActionResult> Index(int? mid, int? cid, string title)
        {

            ViewBag.Title = "Sản Phẩm";

            var response = await client.GetAsync($"api/Products/getlist/{mid}/{cid}");
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();
            string json = response.Content.ReadAsStringAsync().Result;
            List<Product> data = JsonConvert.DeserializeObject<List<Product>>(json);

            return View(data);
        }

        [Route("/chi-tiet-san-pham/{ID}/{title}", Name = "ProductDetail")]
        [Route("/product-detail")]
        public async Task<IActionResult> Detail(int? id, string title)
        {
            var response = await client.GetAsync($"api/Products/{id}");
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();
            string json = response.Content.ReadAsStringAsync().Result;
            Product data = JsonConvert.DeserializeObject<Product>(json);


            if (data != null)
            {
                ViewBag.title = data.Title;
            }

            return View(data);
        }
    }
}
