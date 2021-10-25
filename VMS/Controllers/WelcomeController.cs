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

        public ActionResult Login(string Email, string Pass)
        {
            Admin a = new Admin(Email, Pass); 
            a = _adminService.Create(a);
            return Content("Good Morning " + Email);
        
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
