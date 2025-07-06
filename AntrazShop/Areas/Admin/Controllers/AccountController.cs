using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AccountController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Login()
		{
			return View();
		}
		public IActionResult SignUp()
		{
			return View();
		}
		public IActionResult CreateAccount()
		{
			return View();
		}
		public IActionResult Customer()
		{
			return View();
		}
		public IActionResult FogotPassword()
		{
			return View();
		}

	}
}
