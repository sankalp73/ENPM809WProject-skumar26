﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VMS.Controllers
{
    public class BookAppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
