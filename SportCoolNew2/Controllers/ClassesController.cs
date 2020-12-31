using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SportCoolNew2.Models;

namespace SportCoolNew2.Controllers
{
    public class ClassesController :Controller
    {
        private SportCoolEntities db = new SportCoolEntities();

            int pagesize = 6;

            public ActionResult Index(int page = 1)
            {
                int currentPage = pagesize < 1 ? 1 : page;
                var classes = db.Class.OrderBy(m => m.cId).ToList();
                var result = classes.ToPagedList(currentPage, pagesize);
                //var result = db.Class.ToList();

                return View(result);
            }

       


        public ActionResult Details(int? id)
        {
           


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Class.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }



    }
 }
