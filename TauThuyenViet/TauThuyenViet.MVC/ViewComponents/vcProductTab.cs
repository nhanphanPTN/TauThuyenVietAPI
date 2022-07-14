using Microsoft.AspNetCore.Mvc;


namespace TauThuyenViet.ViewComponents
{
	public class vcProductTab : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			//var data = db.ProductMainCategories
			//			 .OrderBy(x => x.Position)
			//			 .ToList();
			return View();
		}
	}
}
