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
using Microsoft.AspNetCore.Authentication;

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

        private bool CheckAdminApprovedByHR(string email, string id, string path)
        {

            string[] lines = System.IO.File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] values = line.Split(',');
                if (email.Equals(values[0]) && id.Equals(values[1]))
                    return true;
            }
            return false;
        }

        [NonAction]
        public async Task<IActionResult> SetRole(ApplicationUser appUser)
        {
            ApplicationRoles identityRole;
            var action = "";
            var controller = "";

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
            return RedirectToAction(action, controller);
        }


        public async Task<IActionResult> Login2FA(string code)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string email = HttpContext.Session.GetString("Username");
            ApplicationUser appUser = await _userManager.FindByEmailAsync(email);
            //ApplicationUser appUser =  await _signInManager.GetTwoFactorAuthenticationUserAsync();
            //var result = await _signInManager.TwoFactorSignInAsync("Email", code, false, false);
            var result = await _userManager.VerifyTwoFactorTokenAsync(appUser, "Email", code);
            if (result)
            {
                /* HACK: Need to find a better way to do this and make signInmanager work */
                appUser.TwoFactorEnabled = false;
                await _userManager.UpdateAsync(appUser);
                await _signInManager.SignInAsync(appUser, false, "");
                return await SetRole(appUser);
            }
           
            ModelState.AddModelError("", "Invalid Login Attempt");
            return View("Index1");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(string email, string pass)
        {
         
                if (ModelState.IsValid)
                {
                    
                    if (email == null || pass == null)
                    {
                        ModelState.AddModelError(nameof(email), "Login Failed: Invalid Email or password");
                        return View("Index");
                    }
                    ApplicationUser appUser = await _userManager.FindByEmailAsync(email);
                    if (appUser != null)
                    {
                        /* Enable 2FA */
                        if (appUser.TwoFactorEnabled == false)
                        {
                            appUser.TwoFactorEnabled = true;
                            await _userManager.UpdateAsync(appUser);
                        }
                        
                        await _userManager.IsEmailConfirmedAsync(appUser);
                        await _signInManager.SignOutAsync();
                        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, pass, false, true);
                        HttpContext.Session.SetString("Username", appUser.Email);
                        string guid = Guid.NewGuid().ToString();
                        HttpContext.Session.SetString("AuthToken", guid);
                        Response.Cookies.Append("AuthToken", guid);
                        Response.Cookies.Append("Username", appUser.Email);
                        if (result.RequiresTwoFactor)
                        {                          
                            var token = await _userManager.GenerateTwoFactorTokenAsync(appUser, "Email");
                            EmailOtp.sendOTP(email, 2, token);
                            return View("Index1");
                        }
                        if (result.IsLockedOut)
                        {
                            ModelState.AddModelError(nameof(email), "Account has been locked due to malicious activity");
                            return View("Index");
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
