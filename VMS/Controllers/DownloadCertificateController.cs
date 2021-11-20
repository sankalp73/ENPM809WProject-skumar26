using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Drawing;
using Syncfusion.Pdf.Grid;
using System.Data;
using System.IO;
using VMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace VMS.Controllers
{
    [Authorize(Roles = "User")]
    public class DownloadCertificateController : Controller
    {
        private readonly CertificateService _service;
        private readonly AppointmentService _appservice;
        private Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;

        public DownloadCertificateController(CertificateService service, AppointmentService appservice, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
            _appservice = appservice;
    }

        public async Task<IActionResult> Index()
        {
            List<Appointment> list = new List<Appointment>();
            List<string> listName = new List<string>();

            string email = HttpContext.Session.GetString("Username");
            ApplicationUser appUser = await _userManager.FindByEmailAsync(email);

            list = _appservice.Get(appUser);
            return View(list);
        }

        [NonAction]
        public async Task<FileResult> buildPdfAsync(Appointment a)
        {
            // Get Appointment Details

            PdfDocument doc = new PdfDocument();

            PdfPage page = doc.Pages.Add();
            
            PdfGrid pdfGrid = new PdfGrid();
            
            DataTable dataTable = new DataTable();
            
            dataTable.Columns.Add("ID"); 
            dataTable.Columns.Add("Email");
            dataTable.Columns.Add("Campaign Name");
            dataTable.Columns.Add("Vaccine Name");
            dataTable.Columns.Add("Time");
            //dataTable.Columns.Add("Expiry");

            dataTable.Rows.Add(new object[] { a.appUser.UserName, a.appUser.Email, a.center.campaign.Name, a.center.vname, a.appointmentTime});
           
            
            pdfGrid.DataSource = dataTable;
            
            pdfGrid.Draw(page.Graphics, new Syncfusion.Drawing.RectangleF(10, 10, 100, 100));

            MemoryStream stream = new System.IO.MemoryStream();
            // Open the document in browser after saving it
            doc.Save(stream);
            //close the document
            doc.Close(true);

            // create a certificate
            var email = HttpContext.Session.GetString("Username");
            var user = await _userManager.FindByEmailAsync(email);
            Tuple <string, string> certCred = Encryption.HashWithSalt(stream.ToString());
            Certificate cert = new Certificate();
            cert.digest = certCred.Item2;
            cert.salt = certCred.Item1;
            cert.center = a.center;
            cert.applicationUser = user;
            _service.Create(cert);

            return File(stream.ToArray(), "application/pdf", "Cert.pdf");
        }

        /*
         *  This function would check if a certificate already exists:
         *      Then do not create a certificate.   
         *  Otherwise create the hash to check for certificate validity later on.
         *  Also check if the appointment was attended.
         */
        [HttpPost]
        public async Task<IActionResult> Download(string centerName, string centerVname, string campaignName)
        {
            if(ModelState.IsValid)
            {
                var email = HttpContext.Session.GetString("Username");
                // Get appointment from the value submitted
                Appointment a = _appservice.Get(email, centerName, centerVname, campaignName);
                if (a.cert != null && a.cert.digest != null)
                {
                    ModelState.AddModelError(string.Empty, "Wow! You downloaded the certificate and don't remember?");
                    return View();
                }
                else if (a.attended == false)
                {
                    ModelState.AddModelError(string.Empty, "You understand? You need to attend the appointment first!");
                    return View();
                }
                else
                    return await buildPdfAsync(a);
                //TempData["Success"] = "Your Certificate should have been downloaded! Remember to keep this safe!";
            }
            return View("Index");
        }
    }
}
