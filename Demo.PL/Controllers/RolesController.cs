using Demo.BL.Model;
using Demo.DAL.Extend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{

    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {


        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }


        public IActionResult Index()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleVM model)
        {

            try
            {

                var role = new IdentityRole()
                {
                    Name = model.Name
                };
                var result = await roleManager.CreateAsync(role);

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
            catch (Exception )
            {
                return View(model);
            }



        }


        public async Task<IActionResult> Edit(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if(role != null)
                return View(role);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IdentityRole model)
        {

            try
            {

                var role = await roleManager.FindByIdAsync(model.Id);

                role.Name = model.Name;
                role.NormalizedName = model.Name.ToUpper();

                var result = await roleManager.UpdateAsync(role);

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
            catch (Exception )
            {
                return View(model);
            }



        }

        public async Task<IActionResult> Delete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role != null)
                return View(role);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(IdentityRole model)
        {

            try
            {

                var role = await roleManager.FindByIdAsync(model.Id);

                if (role != null)
                {
                    var result = await roleManager.DeleteAsync(role);


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

                return View(model);

            }
            catch (Exception ex)
            {
                return View(model);
            }



        }

        public async Task<IActionResult> AddOrRemoveUsers(string RoleId)
        {
            try
            {
                ViewBag.RoleId = RoleId;

                var role = await roleManager.FindByIdAsync(RoleId);
                if (role != null)
                {

                    var model = new List<UserInRoleVM>();

                    foreach (var user in userManager.Users)
                    {
                        var userInRole = new UserInRoleVM()
                        {
                            UserId = user.Id,
                            UserName = user.UserName
                        };
                        userInRole.IsSelected = await userManager.IsInRoleAsync(user, role.Name);

                        model.Add(userInRole);
                    }
                    return View(model);

                }
                return RedirectToAction("Index");
            
            }
            catch (Exception )
            {
                return RedirectToAction("Index");
            }
}


        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleVM> model, string RoleId)
        {
            try
            { 
            var role = await roleManager.FindByIdAsync(RoleId);
                if (role != null)
                {
                    for (int i = 0; i < model.Count; i++)
                    {
                        var user = await userManager.FindByIdAsync(model[i].UserId);
                        if (user != null)
                        {
                            IdentityResult result = null;

                            if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                            {
                                result = await userManager.AddToRoleAsync(user, role.Name);
                            }
                            else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                            {
                                result = await userManager.RemoveFromRoleAsync(user, role.Name);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    return RedirectToAction("Edit", new { id = RoleId });
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return RedirectToAction("Edit", new { id = RoleId });
            }
        }


    }
}
