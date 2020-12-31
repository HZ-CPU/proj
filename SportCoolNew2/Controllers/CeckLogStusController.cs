using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportCoolNew2.Controllers
{
    public class CeckLogStusController : ActionFilterAttribute
    {
        // GET: CeckLogStus
        void LoginStatus(HttpContext context)
        {

            if (context.Session["manarger"] == null)
                context.Response.Redirect("/Login/MrLogin");

        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

            HttpContext context = HttpContext.Current;
            LoginStatus(context);

        }

        public class NoStoreAttribute : ActionFilterAttribute
        {
            public override void OnResultExecuted(ResultExecutedContext filterContext)
            {
                filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                filterContext.HttpContext.Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            }
        }
    }
}