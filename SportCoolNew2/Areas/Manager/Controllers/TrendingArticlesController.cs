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
    public class TrendingArticlesController : Controller
    {
        private SportCoolEntities db = new SportCoolEntities();

        // GET: TrendingArticles
       
        public ActionResult Index()
        {
            return View(db.TrendingArticles.ToList());
        }

        // GET: TrendingArticles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrendingArticles trendingArticles = db.TrendingArticles.Find(id);
            if (trendingArticles == null)
            {
                return HttpNotFound();
            }
            return View(trendingArticles);
        }

        // GET: TrendingArticles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrendingArticles/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrendingArticles trendingArticles, HttpPostedFileBase image)
        {
            string imagename = "";
            int UID = trendingArticles.articlesId;
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    imagename = UID + Path.GetFileName(image.FileName);//取得檔名
                    image.SaveAs(Server.MapPath("~/images/" + imagename));
                }
                trendingArticles.image = imagename;

                db.TrendingArticles.Add(trendingArticles);
                db.SaveChanges();

            }
            return RedirectToAction("Index");

        }

        // GET: TrendingArticles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrendingArticles trendingArticles = db.TrendingArticles.Find(id);
            if (trendingArticles == null)
            {
                return HttpNotFound();
            }
            return View(trendingArticles);
        }

        // POST: TrendingArticles/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "articlesId,aContent,article,image")] TrendingArticles trendingArticles, HttpPostedFileBase image)
        {
            string fileName = "";
            var articles = db.TrendingArticles.Where(m => m.articlesId == trendingArticles.articlesId).FirstOrDefault();
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    System.IO.File.Delete(Server.MapPath("~/Images/" + articles.image));
                    fileName = Path.GetFileName(image.FileName);
                    image.SaveAs(Server.MapPath("~/Images/" + fileName));
                    articles.image = fileName;

                }

            }
            articles.aContent = trendingArticles.aContent;
            articles.article = trendingArticles.article;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: TrendingArticles/Delete/5
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrendingArticles trendingArticles = db.TrendingArticles.Find(id);
            if (trendingArticles == null)
            {
                return HttpNotFound();
            }
            return View(trendingArticles);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "articlesId,aContent,article,image")] int id)
        {
            if (ModelState.IsValid)
            {
                TrendingArticles trendingArticles = db.TrendingArticles.Find(id);

                db.TrendingArticles.Remove(trendingArticles);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
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
