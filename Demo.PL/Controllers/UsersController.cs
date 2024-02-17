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
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public  IActionResult Index()
        {
            var data = userManager.Users;

            return View(data);
        }
        public async Task<IActionResult> Update(string id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);
                if(user != null)
                {
                    return View(user);
                }
                return RedirectToAction("Index");
            }
            catch (Exception )
            {
                return RedirectToAction("Index");
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Update(ApplicationUser model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await userManager.FindByIdAsync(model.Id);
                    if (user != null)
                    { 
                        user.UserName = model.UserName;
                        user.NormalizedUserName = model.UserName.ToUpper();
                        user.Email = model.Email;
                        user.NormalizedEmail = model.Email.ToUpper();

                        var result = await userManager.UpdateAsync(user);   
                        if (result.Succeeded) 
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                            return View(model); 
                        }
                    }
                    else
                        ModelState.AddModelError("", "User Not Found ");
                        return View(model);

                }
                ModelState.AddModelError("", "Validation Error ");
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    return View(user);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Index");
            }

        }
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);
                if (user != null)
                {
                    return View(user);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ApplicationUser model)
        {
            try
            {
                    var user = await userManager.FindByIdAsync(model.Id);
                    if (user != null)
                    {
                        var result = await userManager.DeleteAsync(user);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                            return View(model);
                        }
                    }
                    ModelState.AddModelError("", "User Not Found ");
                    return View(model);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }

        }

    }
}
