using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.Models;

namespace VMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class checkInAppointmentController : Controller
    {
        private readonly ILogger<checkInAppointmentController> _logger;
        private readonly AppointmentService _appservice;
        public checkInAppointmentController(ILogger<checkInAppointmentController> logger, AppointmentService appservice)
        {
            _logger = logger;
            _appservice = appservice;
        }


        public IActionResult Index()
        {
            return View();
        }

        /*
         *  The user needs to tell the admin within 5 mins 
         *  of OTP generation to check in for appointment and
         *  generating vaccine certificate
         */
        [ValidateAntiForgeryToken]
        public IActionResult CheckIn(string email, string otp)
        {
            var found = 0;
            OTP onetime = new OTP();

            foreach (KeyValuePair<string, OTP> o in TokenHashMap.HashMap)
            {
                if (o.Value.token.Equals(otp) && o.Key.Equals(email))
                {
                    found = 1;
                    onetime = o.Value;
                    email = o.Key;
                    /* mark appointment attended */
                    Appointment a = _appservice.Get(onetime.token);
                    a.attended = true;
                    _appservice.Update(a);
                }
            }
            if (found == 0)
            {
                _logger.LogError("Did not find OTP! for user"+ email);
                ViewBag.Mesage = "OTP does not exist!";
                return View("Index");
            }

            var remainingSeconds = onetime.totp.RemainingSeconds(DateTime.UtcNow);
            /* OTP exhasuted */
            if (remainingSeconds <= 0)
            {
                foreach (KeyValuePair<string, OTP> pair in TokenHashMap.HashMap)
                    if (email == pair.Key)
                        TokenHashMap.HashMap.Remove(pair.Key);
                return View("Index");
            }

            TempData["Sucess"] = "Client has been checkedIn for appoitment!";
            return View("Index");
        }
    }
}
