using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.Models;
using IdentityResult = Microsoft.AspNetCore.Identity.IdentityResult;

namespace VMS.Controllers
{

    public class EmailConfirmationTokenProvider<ApplicationUser> : DataProtectorTokenProvider<ApplicationUser> where ApplicationUser : class
    {
        public EmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider,
            IOptions<EmailConfirmationTokenProviderOptions> options,
            ILogger<DataProtectorTokenProvider<ApplicationUser>> logger)
            : base(dataProtectionProvider, options, logger)
        {
        }
    }
    public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
    {
    }

    public class VerifyAccountController : Controller
    {

        private Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public VerifyAccountController(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) {
                ModelState.AddModelError("", "Something went wrong!");
                return View();
            }
          
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                ModelState.AddModelError("", "Successfully verified!");
                return View();
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong!");
                return View();
            }
        }
    }
}
