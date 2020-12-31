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

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AddComment(int? articlesId, string Content)

        {
            if (Session["memberId"] == null)
            {
                TempData["message"] = "請先登入會員!";
                return RedirectToAction("Login", "Home");
            }
           
            var id = Session["memberId"];
            var Name = db.Member.Find(id).name;
            var articles = db.TrendingArticles.Where(m => m.articlesId == articlesId).FirstOrDefault();
            var userId = HttpContext.User.Identity.Name;
            var currentDateTime = DateTime.Now;
            var comment = new Models.Comment()
            {
              
                TrendingId = articlesId,
                commentcontent = Content,
                CreateDate = currentDateTime,
                userName = Name
            };
            {
                db.Comment.Add(comment);
                db.SaveChanges();
            }
            ViewBag.acid = articlesId;
            ViewBag.aont = Content;
            return RedirectToAction("Details", new { id = articlesId });
        }
    }
}
