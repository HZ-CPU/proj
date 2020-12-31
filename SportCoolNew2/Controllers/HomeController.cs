using SportCoolNew2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;

namespace SportCoolNew2.Controllers
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

        SportCoolEntities db = new SportCoolEntities();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string id, string pswd)
        {
            var member = db.Member.Where(m => m.email == id && m.password == pswd).FirstOrDefault();
        
            if (member == null)
            {
                ViewBag.Erroer = "帳號或密碼有誤!";
                //TempData["message"] = ViewBag.Erroer;
                return View();
            }
            
            Session["member"] = member;
            Session["memberId"] = member.mId;
            //Session["membern"] = member.name;
            Session["membername"] = member.name.Substring(1);
            return RedirectToAction("Login", "Home");
            //return RedirectToAction("Login", "Home");
            //return View();



        }
        public ActionResult Logout()
        {
            

            Session["member"] = null;
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return RedirectToAction("Login", "Home");

        }

        public ActionResult ForgetPwd()
        {
            return View();
        }

        



        public ActionResult Personal( )
        {
            if (Session["memberId"] == null)
            {
                TempData["message"] = "請先登入會員!";
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

    }
}