using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VMS.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System.Web;
using Microsoft.AspNet.Identity;
using IdentityResult = Microsoft.AspNet.Identity.IdentityResult;

namespace VMS.Controllers
{
    public class WelcomeController : Controller
    {
        private Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private Microsoft.AspNetCore.Identity.RoleManager<ApplicationRoles> _roleManager;

        public WelcomeController(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, Microsoft.AspNetCore.Identity.RoleManager<ApplicationRoles> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        private bool CheckAdminApprovedByHR(string email, string id,string path)
        {

            string[] lines = System.IO.File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                if (email.Equals(values[0]) &&  id.Equals(values[1]))
                    return true;
            }
            return false;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(string email, string pass)
        {
         
                if (ModelState.IsValid)
                {
                    var action = "";
                    var controller = "";
                    if (email == null || pass == null)
                    {
                        ModelState.AddModelError(nameof(email), "Login Failed: Invalid Email or password");
                        return View("Index");
                    }
                    ApplicationUser appUser = await _userManager.FindByEmailAsync(email);
                    if (appUser != null)
                    {
                        await _userManager.IsEmailConfirmedAsync(appUser);
                        await _signInManager.SignOutAsync();
                        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, pass, false, true);
                        if (result.Succeeded)
                        {
                            ApplicationRoles identityRole;

                        // check if this is an Admin based on the Id provided:
                        if (CheckAdminApprovedByHR(appUser.Email, appUser.UserName, "C:\\Users\\student\\Workspace\\AdminId.txt"))
                        {
                            identityRole = new ApplicationRoles { Name = "Admin" };
                            action = "List";
                            controller = "FirstTab"; 
                        }
                        else
                        {
                            action = "Index";
                            controller = "BookAppointment";
                            identityRole = new ApplicationRoles { Name = "User" };
                        }

                            Microsoft.AspNetCore.Identity.IdentityResult r = await _roleManager.CreateAsync(identityRole);
                            var result1 = await _userManager.AddToRoleAsync(appUser, identityRole.Name);

                            HttpContext.Session.SetString("Username", email);
                            string guid = Guid.NewGuid().ToString();
                            HttpContext.Session.SetString("AuthToken", guid);
                            Response.Cookies.Append("AuthToken", guid);
                            Response.Cookies.Append("Username", email);
                            return RedirectToAction(action,controller);
                        }
                    } 
                    ModelState.AddModelError(nameof(email), "Login Failed: Invalid Email or password");
                }

            // If we got this far, something failed, redisplay form
            return View("Index");
        }

        [HttpGet]
        public ActionResult SignOn()
        {
            return View("SignOn");

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
