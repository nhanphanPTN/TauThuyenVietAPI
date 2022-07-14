using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TauThuyenViet.Models;

namespace TauThuyenViet.ViewComponents
{
	public class vcNavigation : ViewComponent
	{
        private readonly HttpClient client;
        public vcNavigation(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("default");
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await client.GetAsync("api/ProductMainCategories/NestedMenu");
            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
                return View();
            string json = response.Content.ReadAsStringAsync().Result;
            List<ProductMainCategory> data = JsonConvert.DeserializeObject<List<ProductMainCategory>>(json);

            return View(data);
        }
    }
}
