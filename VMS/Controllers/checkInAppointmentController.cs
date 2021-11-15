using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMS.Controllers
{
    public class checkInAppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /*
         *  The user needs to tell the admin within 5 mins 
         *  of OTP generation to check in for appointment and
         *  generating vaccine certificate
         */
        public IActionResult checkIn()
        {
            return View();
        }
    }
}
