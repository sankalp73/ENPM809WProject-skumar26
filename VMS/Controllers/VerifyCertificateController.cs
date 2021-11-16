using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Myrmec;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadFileAsync(IFormFile file, string campname)
        {
            try
            {
                long mb = 5368709120; // 5 MB;
                if (file != null && file.Length > 0 && file.Length > (mb))
                {
                    var supported = "pdf";
                    var extension = System.IO.Path.GetExtension(file.FileName).Substring(1);

                    var sniffer = new Sniffer();
                    var supportedFiles = new List<Record>
                    {                       
                        new Record("pdf", "25 50 44 46"),
                    };
                    sniffer.Populate(supportedFiles);

                    byte[] header = new byte[0];
                    foreach(KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues> v in file.Headers)
                    {                       
                        header.Concat(Encoding.UTF8.GetBytes(v.Value));
                    }
                    var results = sniffer.Match(header);
                    if(results.Count == 0)
                    {
                        ViewBag.Message = "Not a PDF!!";
                        return View();
                    }

                    if (supported.Equals(extension))
                    {
                        ViewBag.Message = "Not a PDF!!";
                        return View();

                    }
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
