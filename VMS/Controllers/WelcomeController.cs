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

namespace VMS.Controllers
{
    public class WelcomeController : Controller
    {
        private readonly ILogger<WelcomeController> _logger;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public WelcomeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(string email, string pass)
        {
         
                if (ModelState.IsValid)
                {
                    ApplicationUser appUser = await _userManager.FindByEmailAsync(email);
                    if (appUser != null)
                    {
                        await _userManager.IsEmailConfirmedAsync(appUser);
                        await _signInManager.SignOutAsync();
                        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, pass, false, false);
                        if (result.Succeeded)
                            return View("AdminHome");
                    }
                    ModelState.AddModelError(nameof(email), "Login Failed: Invalid Email or password");
                }
           

            // If we got this far, something failed, redisplay form
            return View("Index");

        }

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
