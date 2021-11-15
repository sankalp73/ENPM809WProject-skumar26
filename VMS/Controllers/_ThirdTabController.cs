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
    public class _ThirdTabController : Controller
    {
        private readonly CampaignService _cservice;
        private readonly CenterService _service;

        public _ThirdTabController(CampaignService cservice, CenterService service)
        {
            _cservice = cservice;
            _service = service;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(_service.Get());
        }     
    }
}
