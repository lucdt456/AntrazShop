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
		public IActionResult Role()
		{
			return View();
		}
		public IActionResult CreateRole()
		{
			return View();
		}
		public IActionResult Customer()
		{
			return View();
		}
		public IActionResult RolePermissions()
		{
			return View();
		}

	}
}
