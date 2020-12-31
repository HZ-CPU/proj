using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using SportCoolNew2.Models;
using SportCoolNew2.Controllers;
using System.Web;

namespace SportCoolNew2
{
    /// <summary>
    /// 用户實體類
    /// </summary>
    ///
    public class User
    {
        /// <summary>
        /// 連接ID
        /// </summary>

        [Key]
        public string ConnectionID { get; set; }

        /// <summary>
        /// 用户名稱
        /// </summary>
        public string Name { get; set; }

        public User(string name, string connectionId)
        {


            this.Name = name;
            this.ConnectionID = connectionId;
        }
    }

    /// <summary>
    /// 一對一聊天
    /// </summary>
    [HubName("chat")]
    public class OneToOneHub : Hub
    {
        public static List<User> users = new List<User>();
        public SportCoolEntities db = new SportCoolEntities();
        //發送消息
        public void SendMessage(string connectionId, string message)
        {
            Clients.All.hello();
            var user = users.Where(s => s.ConnectionID == connectionId).FirstOrDefault();
            if (user != null)
            {
                Clients.Client(connectionId).addMessage(message + "" + DateTime.Now, Context.ConnectionId);
                //给自己發送，把用户的ID傳给自己
                Clients.Client(Context.ConnectionId).addMessage(message + "" + DateTime.Now, connectionId);
            }
            else
            {
                Clients.Client(Context.ConnectionId).showMessage("該用戶不在線上");
            }
        }

        [HubMethodName("getName")]
        public void GetName(int id)

        {
            SportCoolEntities db = new SportCoolEntities();
            var member = db.Member.Find(id);
            var user = users.SingleOrDefault(u => u.ConnectionID == Context.ConnectionId);
            if (user != null)
            {
                user.Name = member.name.ToString();

                Clients.Client(Context.ConnectionId).showId(user.Name);
            }
            GetUsers();
        }

        //public void GetName(int id)

        //{
        //    SportCoolEntities db = new SportCoolEntities();
        //    var member = db.Friend.Find(id);
        //    var user = users.SingleOrDefault(u => u.ConnectionID == Context.ConnectionId);
        //    var fname = db.Member.Where(m => m.mId == member.fmId).FirstOrDefault();
        //    if (user != null)
        //    {
        //        user.Name = fname.name;

        //        Clients.Client(Context.ConnectionId).showId(user.Name);
        //    }
        //    GetUsers();
        //}




        /// <summary>
        /// 重寫連接事件
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            //查询用户
            var user = users.Where(u => u.ConnectionID == Context.ConnectionId).SingleOrDefault();
            //判斷用户是否存在，否則添加集合
            if (user == null)
            {
                user = new User("", Context.ConnectionId);
                users.Add(user);
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var user = users.Where(p => p.ConnectionID == Context.ConnectionId).FirstOrDefault();
            //判斷用户是否存在，存在則删除
            if (user != null)
            {
                //删除用户
                users.Remove(user);
            }
            GetUsers();//獲取所有用戶列表
            return base.OnDisconnected(stopCalled);
        }

        private void GetUsers()
        {

            var list = users.Select(s => new { s.Name, s.ConnectionID }).ToList();
            string jsonList = JsonConvert.SerializeObject(list);
            Clients.All.getUsers(jsonList);
        }
        //獲取所有在線用表
        private void GetUser()
        {
            var memberId = HttpContext.Current.Session["memberId"];
            var friend = db.Friend.Where(m => m.mId == (int)memberId);

            var list = users.Select(s => new { s.Name, s.ConnectionID }).ToList();
            string jsonList = JsonConvert.SerializeObject(list);
            Clients.All.getUsers(jsonList);
        }
    }
}