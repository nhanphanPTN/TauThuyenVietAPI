using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TauThuyenViet.Models;

namespace TauThuyenViet.ViewComponents
{
	public class vcProductRelated : ViewComponent
	{
        private readonly HttpClient client;
        public vcProductRelated(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("default");
        }

        public async Task<IViewComponentResult> InvokeAsync(int? categoryID, int ID)
        {
            // api/Products/related/{catid}/{Bid}
            var response = await client.GetAsync($"api/Products/related/{categoryID}/{ID}");
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();
            string json = response.Content.ReadAsStringAsync().Result;
            List<Product> data = JsonConvert.DeserializeObject<List<Product>>(json);

            return View(data);
        }
    }
}
