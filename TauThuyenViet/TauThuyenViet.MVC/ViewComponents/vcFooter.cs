using Microsoft.AspNetCore.Mvc;

namespace TauThuyenViet.ViewComponents
{
	public class vcFooter : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
