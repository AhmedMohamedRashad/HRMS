using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.data = "Dashboard";
            return View();
        }
    }
}
