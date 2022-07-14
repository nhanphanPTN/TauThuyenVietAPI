using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TauThuyenViet.Models;


namespace TauThuyenViet.Controllers
{
    public class ContactController : Controller
    {
        private readonly HttpClient client;
        public ContactController(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("default");
        }

        [HttpGet]
        [Route("/lien-he", Name = "Contact")]
        [Route("/contact")]
        public IActionResult Index()
        {
            ViewBag.Title = "Liên Hệ";
            ViewBag.MessageType = "alert alert-info";
            ViewBag.MessageText = "Mời Nhập Thông Tin";

            return View();
        }


        [HttpPost]
        [Route("/lien-he", Name = "Contact")]
        [Route("/contact")]
        public async Task<IActionResult> Index(Contact item)
        {


            //Kiếm tra lỗi
            if (item == null) //Nếu tất cả đều null k nhập gì hết thì báo
            {
                ViewBag.MessageType = "alert alert-danger";
                ViewBag.MessageText = "Mời Nhập Thông Tin";
                ViewBag.Title = "Liên Hệ";
                return View();
            }

            if (string.IsNullOrEmpty(item.FullName))
            {
                ViewBag.MessageType = "alert alert-danger";
                ViewBag.MessageText = "Mời Nhập Họ Tên";
                ViewBag.Title = "Liên Hệ";
                return View();
            }

            if (string.IsNullOrEmpty(item.Mobi))
            {
                ViewBag.MessageType = "alert alert-danger";
                ViewBag.MessageText = "Mời Nhập Điện Thoại";
                ViewBag.Title = "Liên Hệ";
                return View();
            }

            if (string.IsNullOrEmpty(item.Content))
            {
                ViewBag.MessageType = "alert alert-danger";
                ViewBag.MessageText = "Mời Nhập Nội Dung";
                ViewBag.Title = "Liên Hệ";
                return View();
            }
            //Save db
            item.CreateTime = DateTime.Now;
            item.Status = false;

            //Gọi API
            //Convert object gửi từ form lên thành json theo format contact
            string json = JsonConvert.SerializeObject(item);
            // convert thành dạng đặc biệt binary(lai lai giữa string, object) mới dùng dc post này
            StringContent stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            //Gọi lại api
            var response = await client.PostAsync("api/Contacts", stringContent);

            if (response == null || response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                ViewBag.MessageType = "alert alert-danger";
                ViewBag.MessageText = "Chưa Gửi Dữ liệu thành công. Vui Lòng Thử Lại";
                return View();
            }

            ViewBag.MessageType = "alert alert-success";
            ViewBag.MessageText = "Cảm Ơn Đã Liên Hệ. Chúng Tôi Sẽ Phản Hồi Sớm";
            ViewBag.Title = "Liên Hệ";

            //Reset form khi gửi 
            ModelState.Clear();
            return View();
        }
    }
}
