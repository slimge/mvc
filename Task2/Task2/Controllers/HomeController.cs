using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task2.Models;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Task2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.result = "Please Log In";
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.result = "Hello " + User.Identity.Name + " masw ;)";
            }
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.UserName.Equals(model.UserName));

                }
                SHA1 sha1 = SHA1.Create();
                var pass = BitConverter.ToString(sha1.ComputeHash(Encoding.ASCII.GetBytes(model.Password))).Replace("-", String.Empty).ToLower();
                if (user != null)
                {
                    if (user.Password.Equals(pass))
                    {

                        FormsAuthentication.SetAuthCookie(model.UserName, true);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Password Is Incorrect !");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Not Found !");
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}