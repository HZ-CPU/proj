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
    public class FriendsController : Controller
    {
        private SportCoolEntities db = new SportCoolEntities();

        // GET: Friends
        //好友清單
        public ActionResult Index(int? mId)
        {
            if (Session["memberId"] == null)
            {
                TempData["message"] = "請先登入會員!";
                return RedirectToAction("Login", "Home");
            }
            var Uid = Session["memberId"];
            var friend = db.Friend.Include(f => f.Member);
            return View(friend.ToList());
            
        }


        //加好友
        public ActionResult Create(int? mId)
        {
            if (Session["memberId"] == null)
            {
                TempData["message"] = "請先登入會員!";
                return RedirectToAction("Login", "Home");
            }
            ViewBag.fmId = new SelectList(db.Member, "mId", "name");
            return View();
        }

        // POST: Friends/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Friend friend, int fmId)
        {

            var Uid = Session["memberId"];
            friend.mId = (int)Uid;
            int a = fmId;
            int b = friend.mId;

            var p = db.Member.Where(m => m.mId == fmId).FirstOrDefault();
            var result = db.Friend.Where(m => m.fmId == friend.mId && m.mId == friend.fmId).FirstOrDefault();
            string ViewBagerror = "";
            //Session["friend"]=result.status;
            //friend.mId = (int)Uid;
         
            //不邀請能發送給自己
            if (p != null)
            {
                
               
               if(result != null)
                {
                    if(friend.status == true)
                    {
                        ViewBagerror = "已經為好友";
                    }
                    ViewBagerror = "已發出邀請";
                }

                if ((ModelState.IsValid) && (fmId != friend.mId) && (result == null))
                {
                   
                    friend.status = false;
                    friend.mId = a;
                    friend.fmId = (int)Uid;
                    db.Friend.Add(friend);
                    db.SaveChanges();
                    ViewBagerror = "";
                    return RedirectToAction("Index");
                }
            }
            ViewBagerror = "找不到此人";
            ViewBag.nul = ViewBagerror;
            ViewBag.fmId = new SelectList(db.Member, "mId", "name", friend.fmId);

            return View(friend);
        }


        //同意好友邀請

       
        public ActionResult Agree(int? id)
        {
          
            var Uid = (int)Session["memberId"];

            Friend friend = (from a in db.Friend
                             where a.fId == id
                             select a).FirstOrDefault();
            Friend newFriend = new Friend();
            {
                newFriend.mId = friend.fmId;
                newFriend.fmId = Uid;
                newFriend.status = true;
            }
            db.Friend.Remove(friend);
            db.Friend.Add(newFriend);
            db.SaveChanges();

            friend.status = true;
            db.Friend.Add(friend);
            db.SaveChanges();

            return RedirectToAction("Index","Friends");
    
    }


        //拒絕好友邀請
        public ActionResult Refuse(int? id)
        {
            var Uid = (int)Session["memberId"];

            Friend friend = (from a in db.Friend
                             where a.fId == id
                             select a).FirstOrDefault();
           
            db.Friend.Remove(friend);
            db.SaveChanges();
            return RedirectToAction("Index", "Friends");
        }



    //刪除好友
       
        public ActionResult Delete(int? id)
        {
            Friend friend = db.Friend.Find(id);
            Friend oldFriend = db.Friend.Where(f => f.mId == friend.fmId && f.fmId == friend.mId).FirstOrDefault();
            db.Friend.Remove(friend);
            db.Friend.Remove(oldFriend);
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
