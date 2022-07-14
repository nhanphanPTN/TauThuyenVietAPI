using Microsoft.AspNetCore.Mvc;

namespace TauThuyenViet.ViewComponents
{
	public class vcScript : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
