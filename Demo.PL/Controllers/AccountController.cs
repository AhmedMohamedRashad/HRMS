using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Registration()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            return View();
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
        public IActionResult ConfirmResetPassword()
        {
            return View();
        }
    }
}
