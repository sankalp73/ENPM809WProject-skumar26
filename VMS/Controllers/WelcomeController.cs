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
        private dbService _db;
        private AdminService _adminService;
        public WelcomeController(ILogger<WelcomeController> logger, dbService db, AdminService adminService)
        {
            _logger = logger;
            _db = db;
            _adminService = adminService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string Email, string Pass)
        {
            // verify if the user exists or not
            var client = _adminService.Get(Email);

            if (client.Verified == 0)
                return Content("Please verify your email");

            return View();
        
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
