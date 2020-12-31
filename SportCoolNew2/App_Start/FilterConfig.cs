using System.Web;
using System.Web.Mvc;
using static SportCoolNew2.Controllers.CeckLogStusController;

namespace SportCoolNew2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new CeckLogStusController());
            filters.Add(new NoStoreAttribute());
        }
    }
}
