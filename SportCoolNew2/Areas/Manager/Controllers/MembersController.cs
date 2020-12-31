using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportCoolNew2.Controllers;
using SportCoolNew2.Models;

namespace SportCoolNew2.Areas.Manager.Controllers
{ 
    [ManagerCheckController]
    public class MembersController : Controller
    {
        private SportCoolEntities db = new SportCoolEntities();

        // GET: Manager/Members
       
        public ActionResult Index()
        {
            var member = db.Member.Include(m => m.Activity_Level).Include(m => m.MemberType).Include(m => m.WayOfDiet);
            return View(member.ToList());
        }

        // GET: Manager/Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Manager/Members/Create
        public ActionResult Create()
        {
            ViewBag.aLId = new SelectList(db.Activity_Level, "aLId", "activitydesc");
           
            ViewBag.mTypeId = new SelectList(db.MemberType, "mTypeId", "mtname");
            ViewBag.wayofdietId = new SelectList(db.WayOfDiet, "wayofdietId", "wdcont");
            return View();
        }

        // POST: Manager/Members/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "mId,identityId,email,password,name,birthday,age,gender,phone,postalCode,city,region,other,mTypeId,wayofdietId,startDate,credits,emailstatus,transacId,bvId,photoSticker,aLId,height,weight,BMI,BMR,TDEE")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Member.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.aLId = new SelectList(db.Activity_Level, "aLId", "activitydesc", member.aLId);
           
            ViewBag.mTypeId = new SelectList(db.MemberType, "mTypeId", "mtname", member.mTypeId);
            ViewBag.wayofdietId = new SelectList(db.WayOfDiet, "wayofdietId", "wdcont", member.wayofdietId);
            return View(member);
        }

        // GET: Manager/Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.aLId = new SelectList(db.Activity_Level, "aLId", "activitydesc", member.aLId);
          
            ViewBag.mTypeId = new SelectList(db.MemberType, "mTypeId", "mtname", member.mTypeId);
            ViewBag.wayofdietId = new SelectList(db.WayOfDiet, "wayofdietId", "wdcont", member.wayofdietId);
            return View(member);
        }

        // POST: Manager/Members/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mId,identityId,email,password,name,birthday,age,gender,phone,postalCode,city,region,other,mTypeId,wayofdietId,startDate,credits,emailstatus,transacId,bvId,photoSticker,aLId,height,weight,BMI,BMR,TDEE")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.aLId = new SelectList(db.Activity_Level, "aLId", "activitydesc", member.aLId);
          
            ViewBag.mTypeId = new SelectList(db.MemberType, "mTypeId", "mtname", member.mTypeId);
            ViewBag.wayofdietId = new SelectList(db.WayOfDiet, "wayofdietId", "wdcont", member.wayofdietId);
            return View(member);
        }

        // GET: Manager/Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Manager/Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Member.Find(id);
            db.Member.Remove(member);
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
