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

namespace VMS.Controllers
{
    public class DownloadCertificate : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [NonAction]
        public FileResult buildPdf()
        {
            // Get Appointment Details

            PdfDocument doc = new PdfDocument();

            PdfPage page = doc.Pages.Add();
            
            PdfGrid pdfGrid = new PdfGrid();
            
            DataTable dataTable = new DataTable();
            
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Vaccine Name");
            dataTable.Columns.Add("Expiry");

            dataTable.Rows.Add(new object[] { "", "", "", ""});
           
            
            pdfGrid.DataSource = dataTable;
            
            pdfGrid.Draw(page.Graphics, new Syncfusion.Drawing.RectangleF(10,10, 100, 100));

            MemoryStream stream = new System.IO.MemoryStream();
            // Open the document in browser after saving it
            doc.Save(stream);
            //close the document
            doc.Close(true);

            return File(stream.ToArray(), "application/pdf", "Cert.pdf");
        }
    }
}
