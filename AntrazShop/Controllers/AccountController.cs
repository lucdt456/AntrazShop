using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
