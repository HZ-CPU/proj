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
    public class HoldListsController : Controller
    {
        private SportCoolEntities db = new SportCoolEntities();

        // GET: HoldLists
        public ActionResult Index(int? id)
        {
            if (Session["memberId"] == null)
            {
                TempData["message"] = "請先登入會員!";
                return RedirectToAction("Login", "Home");
            }
            var Uid = Session["memberId"];
            var holdList = db.HoldList.Include(h => h.Activity).Include(h => h.Member);
            return View(holdList.ToList());
        }

        // GET: HoldLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoldList holdList = db.HoldList.Find(id);
            if (holdList == null)
            {
                return HttpNotFound();
            }
            return View(holdList);
        }

        // GET: HoldLists/Create
        public ActionResult Create()
        {
            if (Session["memberId"] == null)
            {
                TempData["message"] = "請先登入會員!";
                return RedirectToAction("Login", "Home");
            }
            ViewBag.activityId = new SelectList(db.Activity, "activityId", "name");
            ViewBag.mId = new SelectList(db.Member, "mId", "identityId");
            return View();
        }

        // POST: HoldLists/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HoldList holdList)
        {
            var acid = (int)Session["memberId"];
           
            if (ModelState.IsValid)
            {
                holdList.mId = acid;
                db.HoldList.Add(holdList);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.activityId = new SelectList(db.Activity, "activityId", "name", holdList.activityId);
            ViewBag.mId = new SelectList(db.Member, "mId", "identityId", holdList.mId);
            return View(holdList);
        }

        // GET: HoldLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoldList holdList = db.HoldList.Find(id);
            if (holdList == null)
            {
                return HttpNotFound();
            }
            ViewBag.activityId = new SelectList(db.Activity, "activityId", "name", holdList.activityId);
            ViewBag.mId = new SelectList(db.Member, "mId", "identityId", holdList.mId);
            return View(holdList);
        }

        // POST: HoldLists/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "organizerName,participantName,participantNumber,mId,activityId")] HoldList holdList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(holdList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.activityId = new SelectList(db.Activity, "activityId", "name", holdList.activityId);
            ViewBag.mId = new SelectList(db.Member, "mId", "identityId", holdList.mId);
            return View(holdList);
        }

        // GET: HoldLists/Delete/5
        //public ActionResult Delete(int? id,int? acid)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    HoldList holdList = db.HoldList.Find(id);
        //    if (holdList == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(holdList);
        //}

        //// POST: HoldLists/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]

        //取消加入
        public ActionResult Delete (int id,int aid)
        {
            
            id = (int)Session["memberId"];
            HoldList holdList = db.HoldList.Where(h => h.mId == id && h.activityId== aid).FirstOrDefault(); 
            //var daid = db.HoldList.Where(h => h.mId == aid);

            db.HoldList.Remove(holdList);
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
