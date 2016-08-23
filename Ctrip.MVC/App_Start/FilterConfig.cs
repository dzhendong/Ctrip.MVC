using Ctrip.Test.Session;
using System.Web;
using System.Web.Mvc;

namespace Ctrip.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SessionFilterAttribute());
        }
    }
}