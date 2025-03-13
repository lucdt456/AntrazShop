using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
