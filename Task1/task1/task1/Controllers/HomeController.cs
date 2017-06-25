using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Image(HttpPostedFileBase file)
        {
            if (file == null)
            {
                ViewBag.error = "File didn't chosen !";
                return View("Index");
            }

            byte[] binaryData;
            binaryData = new Byte[file.InputStream.Length];
            long bytesRead = file.InputStream.Read(binaryData, 0, (int)file.InputStream.Length);
            file.InputStream.Close();
            string base64String = System.Convert.ToBase64String(binaryData, 0, binaryData.Length);

            ViewBag.url = base64String;
            return View();
        }
    }
}