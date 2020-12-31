using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportCoolNew2.Areas.Manager.Controllers
{
    public class ManagerCheckController : ActionFilterAttribute
    {
        void LoginStatus(HttpContext context)
        {

            if (context.Session["manarger"] == null)
                context.Response.Redirect("/Manager/Login/MrLogin");

        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

            HttpContext context = HttpContext.Current;
            LoginStatus(context);

        }
    }
}