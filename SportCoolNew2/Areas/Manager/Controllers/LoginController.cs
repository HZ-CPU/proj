using SportCoolNew2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SportCoolNew2.Areas.Manager.Controllers
{
    public class LoginController : Controller
    {
        // GET: Manager/Login
        SportCoolEntities db = new SportCoolEntities();
        public ActionResult MrLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MrLogin(string mid, string mpswd)
        {
            var manarger = db.Manager.Where(m => m.mAccount == mid && m.mPassword == mpswd).FirstOrDefault();
            if (manarger == null)
            {
                ViewBag.Erroer = "帳號或密碼有誤!";
                return View();
            }

            Session["manarger"] = manarger;
            return RedirectToAction("Index", "Login");
        }


        public ActionResult MrLogout()
        {

            Session["manarger"] = null;
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("MrLogin", "Login");

        }

        [ManagerCheckController]
        public ActionResult Index()
        {
            return View();
        }
    }
}