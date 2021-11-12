using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor.Navigations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace VMS.Controllers
{
    public class LogOutController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public LogOutController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View("Welcome");
        }
    }
}
