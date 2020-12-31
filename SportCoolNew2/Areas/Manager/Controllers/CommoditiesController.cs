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

namespace SportCoolNew2.Areas.Manager.Controllers
{
    [ManagerCheckController]
    public class CommoditiesController : Controller
    {
        private SportCoolEntities db = new SportCoolEntities();

        // GET: Manager/Commodities
        public ActionResult Index()
        {
            //var commodity = db.Commodity.Include(c => c.Shop);
            return View(db.Commodity.ToList());
        }



        // GET: Manager/Commodities/Create
        public ActionResult Create()
        {
            ViewBag.shopId = new SelectList(db.Shop, "shopId", "shopAccount");
            return View();
        }

        // POST: Manager/Commodities/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Commodity commodity, HttpPostedFileBase commodityImage)
        {
            string fileName = "";
            //int UID = commodity.commodityId;
            if (commodityImage != null)
            {
                if (commodityImage.ContentLength > 0)
                {
                    fileName = Path.GetFileName(commodityImage.FileName); //取得檔案名稱
                    commodityImage.SaveAs(Server.MapPath("~/images/" + fileName));
                }
                commodity.commodityImage = fileName;
                db.Commodity.Add(commodity);
                db.SaveChanges();
                ViewBag.shopId = new SelectList(db.Shop, "shopId", "shopAccount", commodity.shopId);

            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commodity commodity = db.Commodity.Find(id);
            if (commodity == null)
            {
                return HttpNotFound();
            }
            return View(commodity);
        }
        // GET: Manager/Commodities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commodity commodity = db.Commodity.Find(id);
            if (commodity == null)
            {
                return HttpNotFound();
            }
            ViewBag.shopId = new SelectList(db.Shop, "shopId", "shopAccount", commodity.shopId);
            return View(commodity);
        }

        // POST: Manager/Commodities/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (Commodity commodity,int id, HttpPostedFileBase commodityImage)
        {
            string fileName = "";
            var commodities = db.Commodity.Where(m => m.commodityId == id).FirstOrDefault();
            if (commodityImage != null)
            {
                if (commodityImage.ContentLength > 0)
                {
                    System.IO.File.Delete(Server.MapPath("~/Images/" + commodities.commodityImage));
                    fileName = Path.GetFileName(commodityImage.FileName);
                    commodityImage.SaveAs(Server.MapPath("~/Images/" + fileName));
                    commodities.commodityImage = fileName;

                }
               
            }
           
            
                db.SaveChanges();
                return RedirectToAction("Index");
           
        }

        // GET: Manager/Commodities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commodity commodity = db.Commodity.Find(id);
            if (commodity == null)
            {
                return HttpNotFound();
            }
            return View(commodity);
        }

        // POST: Manager/Commodities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Commodity commodity = db.Commodity.Find(id);
            db.Commodity.Remove(commodity);
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
