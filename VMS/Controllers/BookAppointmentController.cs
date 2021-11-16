using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMS.Models;

namespace VMS.Controllers
{
    [Authorize(Roles ="User")]
    public class BookAppointmentController : Controller
    {
        private readonly CenterService _service;
        private readonly CampaignService _cservice;
        private readonly AppointmentService _aservice;
        private Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public BookAppointmentController(CenterService service, CampaignService cservice, AppointmentService aservice, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _cservice = cservice;
            _userManager = userManager;
            _aservice = aservice;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<string> listName = new List<string>();
           
            var list = _cservice.Get();
            foreach (var c in list)
                if (c.EndDate > DateTime.Now) 
                    listName.Add(c.Name);
            
            ViewBag.message = listName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LookUp(int zip, string camName)
        {
            List<Center> center = new List<Center>();
            center = _service.Get(camName, zip, DateTime.Now);

            if(center.Count == 0)
                ModelState.AddModelError(string.Empty, "No Vaccine center available in your area! Please be patient, vaccines will be available soon");

            return View(center);
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeAppointment(Center model, string camname, string timings)
        {
           if(timings ==  null)
            {
                ModelState.AddModelError(string.Empty,"Please select time!");
                return RedirectToAction("Index","BookAppointment"); 
            }
            string email = HttpContext.Session.GetString("Username");
            Appointment a = new Appointment();
            Campaign cam = _cservice.Get(camname);
            ApplicationUser appUser = await _userManager.FindByEmailAsync(email);
            DateTime time = DateTime.ParseExact(timings, "t", null);

            if (!(model.startTime <= time && model.EndTime >= time))
            {
                TempData["Success"] = "Select a time between :" + model.startTime.ToString() + " and " + model.EndTime.ToString();
                return RedirectToAction("Index", "BookAppointment"); 
            }

            model = _service.Get(model.Name, model.zip);
            a.center = model;
            a.appUser = appUser;
            a.appointmentTime = time;

            _aservice.Create(a);

            TempData["Success"] = "Your Appointment has been booked!";

            Center newc = model;
            newc.quantity -= 1;
            _service.Update(model.Name,model.vname, model.zip, newc);

            return RedirectToAction("Index", "BookAppointment"); 
        }
    }
}
