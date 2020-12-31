using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SportCoolNew2.Models;

namespace SportCoolNew2.Areas.Manager.Controllers
{
    [ManagerCheckController]
    public class ClassesController : Controller
        {
        private SportCoolEntities db = new SportCoolEntities();
        int pagesize = 5;

        // GET: Classes
        //public ActionResult Index()
        //{
        //    var classes = db.Class.ToList();

        //    return View(classes);
        //}

        public ActionResult Index(int page = 1)
        {
            int currentPage = pagesize < 1 ? 1 : page;
            var classes = db.Class.OrderByDescending(m => m.cId).ToList();
            var result = classes.ToPagedList(currentPage, pagesize);
            //var result = db.Class.ToList();

            return View(result);
        }

        // GET: Classes/Details/5
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

        // GET: Classes/Create
        public ActionResult Create()
        {
            ViewBag.cTypeId = new SelectList(db.ClassType, "cTypeId", "typeName");
            ViewBag.cGradeId = new SelectList(db.ClassGrade, "cGradeId", "gradeName");
            ViewBag.coachId = new SelectList(db.Coach, "coachId", "expertise");


            return View();
        }

        // POST: Classes/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "cId,className,video,cGradeId,cTypeId,coachId,audio")] Class @class)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Class.Add(@class);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.cTypeId = new SelectList(db.ClassType, "cTypeId", "typeName", @class.cTypeId);
        //    ViewBag.cGradeId = new SelectList(db.ClassGrade, "cGradeId", "gradeName", @class.cGradeId);
        //    ViewBag.coachId = new SelectList(db.Coach, "coachId", "expertise", @class.coachId);
        //    return View(@class);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Class classes, HttpPostedFileBase file)
        {

            string fileName = "";
            if (file != null) 
            {
                if (file.ContentLength > 0)
                {
                    fileName = Path.GetFileName(file.FileName); //取得檔案名稱
                    fileName = "~/Video/" + fileName;
                    file.SaveAs(Server.MapPath(fileName));
                }
                classes.audio = fileName;
                var prod = db.Class.Add(classes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewBag.cTypeId = new SelectList(db.ClassType, "cTypeId", "typeName", classes.cTypeId);
            ViewBag.cGradeId = new SelectList(db.ClassGrade, "cGradeId", "gradeName", classes.cGradeId);
            ViewBag.coachId = new SelectList(db.Coach, "coachId", "expertise",classes.coachId);
           
            return View(classes);
            
        }

        // GET: Classes/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.cTypeId = new SelectList(db.ClassType, "cTypeId", "typeName", @class.cTypeId);
            ViewBag.cGradeId = new SelectList(db.ClassGrade, "cGradeId", "gradeName", @class.cGradeId);
            ViewBag.coachId = new SelectList(db.Coach, "coachId", "expertise", @class.coachId);
            return View(@class);
        }

        // POST: Classes/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "cId,className,video,cGradeId,cTypeId,coachId,audio")] Class @class)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(@class).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.cTypeId = new SelectList(db.ClassType, "cTypeId", "typeName", @class.cTypeId);
        //    ViewBag.cGradeId = new SelectList(db.ClassGrade, "cGradeId", "gradeName", @class.cGradeId);
        //    ViewBag.coachId = new SelectList(db.Coach, "coachId", "expertise", @class.coachId);
        //    return View(@class);
        //}
        public ActionResult Edit(int cId, Class classes, HttpPostedFileBase file)
        {
            string fileName = "";
            var cla = db.Class.Where(m => m.cId == cId).FirstOrDefault();

            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    System.IO.File.Delete(Server.MapPath(cla.audio));

                    fileName = Path.GetFileName(file.FileName);
                    fileName = "~/Video/" + fileName;
                    file.SaveAs(Server.MapPath(fileName));

                    cla.audio = fileName;
                }
            }

            cla.className = classes.className;
            cla.cGradeId = classes.cGradeId;
            cla.cTypeId = classes.cTypeId;
            cla.coachId = classes.coachId;
            //cla.audio = classes.audio;

            db.SaveChanges();

            return RedirectToAction("Index");
        }


        // GET: Classes/Delete/5
        public ActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Class @class = db.Class.Find(id);
            //if (@class == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(@class);
            Class @class = db.Class.Where(c => c.cId == id).FirstOrDefault();
            //Class @class = (from a in db.Class
            //                    where a.cId == id
            //                    select a).FirstOrDefault();
            db.Class.Remove(@class);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]


        public ActionResult DeleteConfirmed(int id)
        {
            Class @class = (from a in db.Class
                            where a.cId == id
                            select a).FirstOrDefault();
            db.Class.Remove(@class);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}
