using Microsoft.AspNetCore.Mvc;

namespace TauThuyenViet.ViewComponents
{
	public class vcCss : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
