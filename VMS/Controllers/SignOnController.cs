using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.Models;
using IdentityResult = Microsoft.AspNetCore.Identity.IdentityResult;

namespace VMS.Controllers
{
    public class SignOnController : Controller
    {
        private Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _adminManager;

        public SignOnController(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> adminManager)
        {
            this._adminManager = adminManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOn(string email, string pass, string confpass, string id)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(confpass) || string.IsNullOrEmpty(pass)) 
                    return Content("All fields are mandatory!");

                if(!pass.Equals(confpass))
                    return Content("password do not match!");

                ApplicationUser appUser = new ApplicationUser
                {
                    Email = email,
                    UserName = id
                };

                IdentityResult result = await _adminManager.CreateAsync(appUser, pass);
                if (result.Succeeded)
                    return Content("User Created Successfully! Please verify email!");
                else
                {
                    foreach (IdentityError error in result.Errors)
                         ModelState.AddModelError(string.Empty, error.Description.ToString());
                }
            }

            return View();
        }
    }
}
