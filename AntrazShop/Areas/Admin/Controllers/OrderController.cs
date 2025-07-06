using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
