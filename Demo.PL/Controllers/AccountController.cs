using AutoMapper;
using Demo.BL.Helper;
using Demo.BL.Model;
using Demo.DAL.Entity;
using Demo.DAL.Extend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
       
        
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = mapper.Map<ApplicationUser>(model);
                    user.UserName = model.Email;
                    //var user = new ApplicationUser()
                    //{
                    //    UserName = model.Email,
                    //    Email = model.Email,
                    //    IsAgree = model.IsAgree
                    //};
                    var result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }
                }
                return View(model);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid Password");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Your Email Not Exist");
                    }

                }
                return View(model);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        var token = await userManager.GeneratePasswordResetTokenAsync(user);
                        var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token });
                        MailSender.SendMail(new MailVM { To = user.Email, Title = "Reset Password", Message = passwordResetLink });
                        return RedirectToAction("ConfirmForgetPassword");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Your Email Not Exist");

                    }

                }
                return View(model);
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string Email,string Token)
        {
            if(Email != null && Token != null)
            {
                return View();

            }
            return RedirectToAction("Login");
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Login");
                        }
                        else
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Your Email Not Exist");

                    }

                }
                return View(model);
            }
            catch (Exception)
            {
                return View(model);
            }
        }
        public IActionResult ConfirmResetPassword()
        {
            return View();
        }
    }
}
