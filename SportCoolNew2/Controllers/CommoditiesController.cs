using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PagedList;
using SportCoolNew2.Models;


namespace SportCoolNew2.Controllers
{
    public class CommoditiesController : Controller
    {
        private SportCoolEntities db = new SportCoolEntities();

        // GET: Commodities
        public class Orderc
        {
            public string name;
            public int price;
            public int amount;
            public int fPId;
        }
        public ActionResult Index()
        {
            var commodity = db.Commodity.Include(c => c.Shop);
            return View(commodity.ToList());
        }

        // GET: Commodities/Details/5
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

        // GET: Commodities/Create
        public ActionResult Create()
        {
            ViewBag.shopId = new SelectList(db.Shop, "shopId", "shopAccount");
            return View();
        }

        // POST: Commodities/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "commodityId,commodityName,commodityPrice,commodityDiscount,commodityImage,commodityDescription,commodityStock,shopId,soldTF")] Commodity commodity)
        {
            if (ModelState.IsValid)
            {
                db.Commodity.Add(commodity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.shopId = new SelectList(db.Shop, "shopId", "shopAccount", commodity.shopId);
            return View(commodity);
        }

        // GET: Commodities/Edit/5
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

        // POST: Commodities/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "commodityId,commodityName,commodityPrice,commodityDiscount,commodityImage,commodityDescription,commodityStock,shopId,soldTF")] Commodity commodity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commodity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.shopId = new SelectList(db.Shop, "shopId", "shopAccount", commodity.shopId);
            return View(commodity);
        }


        // GET: Commodities/Delete/5
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

        // POST: Commodities/Delete/5
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
        public ActionResult MyCar()
        {
            return View("MyCar", "_Layout");
        }
        public ActionResult Order()
        {
            if (Session["memberId"] == null)
            {
                TempData["message"] = "請先登入會員!";
                return RedirectToAction("Login", "Home");
            }
            //    if (Session["Member"] == null)
            //        return RedirectToAction("Login");

            return View("Order");
        }

        [HttpPost]
        public ActionResult Order(string fAddress, string data, int ftelephone, string fName)
        {


            var aOrder = JsonConvert.DeserializeObject<List<Orderc>>(data);
            //OrderDetails orderDetails = new OrderDetails()

            var orderDetails2 = new Models.OrderDetails();

            foreach (var n in aOrder)
            {

                orderDetails2.receiver = fName;

                orderDetails2.tel = ftelephone;
                orderDetails2.mId = (int)Session["memberId"];
                orderDetails2.receivedAddress = fAddress;
                orderDetails2.Qty = n.amount;
                orderDetails2.PId = n.fPId;
                orderDetails2.Name = n.name;
                orderDetails2.price = n.price;
                db.OrderDetails.Add(orderDetails2);
                db.SaveChanges();
            }
            ;

            TempData["Order"] = "Y";
            return RedirectToAction("MyCar");
            //var member =  (int)Session["memberId"];
            //db.Add_Order  ( Convert.ToString(member) , fAddress, data);
            //db.SaveChanges();
            //
            //
        }
    }
}
