using Ctrip.Test.Route;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ctrip.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "LogOn", id = UrlParameter.Optional },                
                namespaces: new[] { "Ctrip.MVC.Controllers" }
            );

            routes.Add("DomainRoute", new DomainRoute(
                "www.{companyUrl}.wenthink.com",     
                "{controller}/{action}/{id}",    
                new { companyUrl = "", controller = "Home", action = "Login", id = "" } 
            ));

            //routes.MapRoute("500", "500", new { controller = "Error", action = "NotFound" });
            //routes.MapRoute("404", "404", new { controller = "Error", action = "NotFound" });
        }
    }
}