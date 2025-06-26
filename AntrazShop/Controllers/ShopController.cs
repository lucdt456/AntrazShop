using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		public IActionResult ProductDetail()
		{
			return View();
		}
		public IActionResult WishList()
		{
			return View();
		}
	}
}
