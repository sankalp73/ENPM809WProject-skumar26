using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.Models;

namespace VMS.Controllers
{
    public class SignOnController : Controller
    {
        private AdminService _adminService;
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignOn(string Email, string Pass, string confPass, string ID)
        {
            if (!Pass.Equals(confPass))
                return View();

            var cred  = Encryption.HashWithSalt(Pass);

            Admin a = new Admin(Email, cred.Item2, cred.Item1, ID, (DateTime.Now));
            a = _adminService.Create(a);

            // send an email to HR for verfication


            return View();
        }
    }
}
