using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportCoolNew2.Models;

namespace SportCoolNew2.Controllers
{
    public class ActivitiesController : Controller
    {
        private SportCoolEntities db = new SportCoolEntities();

        // GET: Activities
        public ActionResult Index()
        {
            return View(db.Activity.ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activity.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        ////參加活動
        //public ActionResult Adalist(HoldList holdList, int? id)
        //{
            
        //    var hid = (int)Session["memberId"];
        //    Activity activity = db.Activity.Find(id);
           
        //    holdList.mId = hid;
        //    holdList.activityId = activity.activityId;
        //    db.HoldList.Add(holdList);
        //    db.SaveChanges();

        //    return RedirectToAction("Index", "Activities");

        //}



        // 舉辦活動
        public ActionResult Create()
        {
            if (Session["memberId"] == null)
            {
                TempData["message"] = "請先登入會員!";
                return RedirectToAction("Login", "Home");
            }
            
            return View();
        }

        // POST: Activities/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (Activity activity)
        {
           
           
            var acid = (int)Session["memberId"];
          
            HoldList holdList = (from a in db.HoldList
                             where a.mId == acid
                                 select a).FirstOrDefault();
            HoldList newholdList = new HoldList();

            var member = db.Member.Find(acid).name;
           

            if (ModelState.IsValid)
            {
                activity.holder = member;
                db.Activity.Add(activity);
                db.SaveChanges();

                newholdList.mId = acid;
                newholdList.activityId = activity.activityId;
                db.HoldList.Add(newholdList);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activity.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "activityId,name,place,dateTime,item")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activity.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activity.Find(id);
            db.Activity.Remove(activity);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //參加活動
        public ActionResult Adalist(HoldList holdList, int? id)
        {
            if (Session["memberId"] == null)
            {
                TempData["message"] = "請先登入會員!";
                return RedirectToAction("Login", "Home");
            }

           

            var hid = (int)Session["memberId"];
            Activity activity = db.Activity.Find(id);
            var result = db.HoldList.Where(h => h.mId == hid && h.activityId==activity.activityId).FirstOrDefault();

            if (result!=null)
            {
                TempData["act"] = "已經參加!";
                return RedirectToAction("Index", "Activities");
            }
            else
            {
                holdList.mId = hid;
                holdList.activityId = activity.activityId;
                db.HoldList.Add(holdList);
                db.SaveChanges();
                return RedirectToAction("Index", "Activities"); 
            }

            
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
