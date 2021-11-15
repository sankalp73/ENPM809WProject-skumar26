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
    public class FirstTabController : Controller
    {
        private readonly CampaignService _service;

        public FirstTabController(CampaignService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult create()
        {
            return View("../Welcome/FirstTab");
        }

        [HttpGet]
        public IActionResult List()
        {
            var model = new List<Campaign>();

            model = _service.Get();
            return View("List", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCampaign(Campaign c)
        {
            Campaign n = c;

            Campaign exists = _service.Get(c.Name);

            if (exists != null)
            {
                ModelState.AddModelError("", "uhhh! The campaign Exists, Preparing yourself for a ban!");
                return View();
            }

            _service.Create(c);
            return View("../Welcome/FirstTab",c);
        }
    }
}
