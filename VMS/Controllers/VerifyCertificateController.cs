using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VMS.Models;

namespace VMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VerifyCertificateController : Controller
    {
        private readonly CertificateService _service;
        private readonly CampaignService _cservice;
        private Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        public VerifyCertificateController(CertificateService service, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, CampaignService cservice)
        {
            _service = service;
            _userManager = userManager;
            _cservice = cservice;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadFileAsync(IFormFile file, string campname)
        {
            try
            {
                if (file.Length > 0)
                {
                    Campaign camp = _cservice.Get(campname);
                    var email = HttpContext.Session.GetString("Username");
                    var user = await _userManager.FindByEmailAsync(email);

                    List<Certificate> c = _service.Get(user, camp);
                    if (c.Count == 0)
                    {
                        ViewBag.Message = "No Cert found !!";
                        return View();
                    }
                    string _FileName = Path.GetFileName(file.FileName);
                    string salt = c[0].salt;
                    string digest = c[0].digest;
                    if (Encryption.VerifyHashWithSalt(file.OpenReadStream().ToString(), salt, digest))
                    {
                        TempData["Success"] = "This is a legit Certificate!";
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = "Fake Certificate!!";
                        return View();
                    }
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
    }
}
