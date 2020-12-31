using SportCoolNew2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportCoolNew2.Controllers
{
    public class PartialView_Controller : Controller
    {
        private SportCoolEntities db = new SportCoolEntities();
        // GET: PartialView_
        public ActionResult _classPartialView(int num=0)
        {
            List<Class> classpart;
            if (num == 0)
            {
                classpart = db.Class.OrderByDescending(c => c.cId).ToList();

            }
            else
            {
                classpart = db.Class.OrderByDescending(c=> c.cId).Take(num).ToList();
            }
            
            return PartialView(classpart);
        }

        public ActionResult _TrendingArticlesPV(int num=0)
        {
            List <TrendingArticles> ta;
            if(num==0)
            { 
                ta = db.TrendingArticles.OrderByDescending(a => a.articlesId).ToList();
           
            }
            else
            {
                 ta = db.TrendingArticles.OrderByDescending(a => a.articlesId).Take(num).ToList();
            }
             return PartialView(ta);
        }
    }
}