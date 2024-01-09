using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.data = "Dashboard";
            return View();
        }
    }
}
