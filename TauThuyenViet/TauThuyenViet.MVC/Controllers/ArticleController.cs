using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TauThuyenViet.Models;

namespace TauThuyenViet.Controllers
{
    public class ArticleController : Controller
    {

        private readonly HttpClient client;
        public ArticleController(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("default");
        }


        [Route("/tin-tuc", Name = "ArticleList")]
        [Route("/article")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Tin Tức";

            var response = await client.GetAsync("api/Articles");
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();
            string json = response.Content.ReadAsStringAsync().Result;
            List<Article> data = JsonConvert.DeserializeObject<List<Article>>(json);

            return View(data);

        }

        [Route("/chi-tiet-tin-tuc/{id}/{title}", Name = "ArticleDetail")]
        [Route("/article-detail")]
        public async Task<IActionResult> Detail(int id, string title)
        {
            var response = await client.GetAsync($"api/Articles/{id}");
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();
            string json = response.Content.ReadAsStringAsync().Result;
            Article data = JsonConvert.DeserializeObject<Article>(json);


            if (data != null)
            {
                ViewBag.title = data.Title;
            }

            return View(data);
        }
    }
}
