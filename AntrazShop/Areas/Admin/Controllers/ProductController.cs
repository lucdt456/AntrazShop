using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
