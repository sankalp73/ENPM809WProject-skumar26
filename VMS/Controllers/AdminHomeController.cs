using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor.Navigations;
using Microsoft.AspNetCore.Authorization;

namespace VMS.Controllers
{
    public class AdminHomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult FirstTab()
        {
            return PartialView("_FirstTab");
        }

        [Authorize]
        public ActionResult SecondTab()
        {
            return PartialView("_SecondTab");
        }
    }
}
