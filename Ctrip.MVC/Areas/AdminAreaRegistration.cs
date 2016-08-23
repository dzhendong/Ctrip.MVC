
using System.Web.Mvc;

namespace Ctrip.MVC.Areas
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional } ,
                namespaces: new[] { "Ctrip.MVC.Areas.Admin.Controllers" }
            );
        }
    }
}