using Microsoft.AspNetCore.Mvc;

namespace AntrazShop.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
