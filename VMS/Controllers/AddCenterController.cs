using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor.Navigations;
using Microsoft.AspNetCore.Authorization;
using VMS.Models;

namespace VMS.Controllers
{
    [Authorize]
    public class AddCenterController : Controller
    {
        private readonly CampaignService _cservice;
        private readonly CenterService _service;
        private readonly Campaign cam;

        public AddCenterController(CampaignService cservice, CenterService service)
        {
            _cservice = cservice;
            _service = service;
        }

       
        [HttpGet]
        public IActionResult Index()
        {
            List<Campaign> list = new List<Campaign>();
            List<string> listName = new List<string>();
            list = _cservice.Get();
            foreach(var c in list){
                listName.Add(c.Name);
            }            
            ViewBag.message =  listName;
            return View();
        }

        [HttpGet]
        public IActionResult List()
        {
            var model = new List<Center>();

            model = _service.Get();
            return View("ListCenter", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCenter(Center c, string campaign)
        {
            // check if campaign added exists
            Campaign cam = _cservice.Get(campaign);

            if (cam == null)
            {
                ModelState.AddModelError(string.Empty, "Campaign does not exist!");
                return RedirectToAction("Index","AddCenter");
            }

            c.campaign = cam;
            Center n = c;
            _service.Create(c);

            return RedirectToAction("Index", "AddCenter"); ;
        }
    }
}
