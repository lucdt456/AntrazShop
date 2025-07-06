using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
		public IActionResult EditRole()
		{
			return View();
		}
		public IActionResult CreateRole()
		{
			return View();
		}
	}
}
