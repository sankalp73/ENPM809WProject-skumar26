using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor.Navigations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Authentication;
using VMS.Models;

namespace VMS.Controllers
{
    [Authorize]
    public class LogOutController : Controller
    {
        private  SignInManager<ApplicationUser> _signInManager;
        public LogOutController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync();
            await _signInManager.SignOutAsync();
            return View("../Welcome/Index");
        }
    }
}
