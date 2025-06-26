using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
		public IActionResult CheckOut()
		{
			return View();
		}

	}
}
