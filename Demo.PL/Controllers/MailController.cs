﻿using Demo.BL.Helper;
using Demo.BL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize(Roles ="Admin,User")]
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult Send()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Send(MailVM obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   
                    TempData["Msg"] = MailSender.SendMail(obj);
                    return RedirectToAction("Send");
                }
                return View();
            }
            catch (Exception)
            {
                TempData["Msg"] = "Failed";
                return View();

            }
        }
    }
}
