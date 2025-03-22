using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Data
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
