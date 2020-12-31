using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportCoolNew2.Models;

namespace SportCoolNew2.Controllers
{
    public class MembersController : Controller
    {
        private SportCoolEntities db = new SportCoolEntities();


        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            id = (int?)Session["memberId"];
            if (id == null)
            {
                TempData["message"] = "請先登入會員!";
                return RedirectToAction("Login", "Home");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            ViewBag.aLId = new SelectList(db.Activity_Level, "aLId", "activitydesc");
           
            ViewBag.mTypeId = new SelectList(db.MemberType, "mTypeId", "mtname");
            ViewBag.wayofdietId = new SelectList(db.WayOfDiet, "wayofdietId", "wdcont");
            return View();
        }

        // POST: Members/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member,HttpPostedFileBase image)
        { 
            var result = db.Member.Where(m => m.email == member.email).FirstOrDefault();
            var idID = db.Member.Where(m => m.identityId == member.identityId).FirstOrDefault();
            var imagename = "";
            int UID = member.mId;
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    imagename = UID + Path.GetFileName(image.FileName);//取得檔名
                    image.SaveAs(Server.MapPath("~/images/" + imagename));
                }
                member.photoSticker= imagename;
            }

            if (result != null)
            {
                ViewBag.Message = "此信箱已有人使用";

            }
            if (idID != null)
            {
                ViewBag.Message1 = "此身分證字號已有人使用";
            }
            if ((ModelState.IsValid) && (result == null) && ((idID == null)))
            {
                
                db.Member.Add(member);
                db.SaveChanges();
                return RedirectToAction("Login", "Home");
            }
            ViewBag.aLId = new SelectList(db.Activity_Level, "aLId", "activitydesc", member.aLId);
         
            ViewBag.mTypeId = new SelectList(db.MemberType, "mTypeId", "mtname", member.mTypeId);
            ViewBag.wayofdietId = new SelectList(db.WayOfDiet, "wayofdietId", "wdcont", member.wayofdietId);
            return View();
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? mId)
        {
            mId = (int?)Session["memberId"];
            if (mId == null)
            {
                TempData["message"] = "請先登入會員!";
                return RedirectToAction("Login","Home");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(mId);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.aLId = new SelectList(db.Activity_Level, "aLId", "activitydesc", member.aLId);
          
            ViewBag.mTypeId = new SelectList(db.MemberType, "mTypeId", "mtname", member.mTypeId);
            ViewBag.wayofdietId = new SelectList(db.WayOfDiet, "wayofdietId", "wdcont", member.wayofdietId);
            return View(member);
        }

        // POST: Members/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Member member)
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
