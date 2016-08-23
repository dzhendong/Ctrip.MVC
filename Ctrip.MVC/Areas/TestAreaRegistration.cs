using System.Web.Mvc;
using System.Web.Routing;

namespace Ctrip.MVC.Areas
{
    public class TestAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Test";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.Routes.RouteExistingFiles = true;
            //context.Routes.IgnoreRoute("{filename}.aspx/{*pathInfo}");
                    
            //var defaults = new RouteValueDictionary {
            //    { "areacode" , "010" }, { "days" , 2 }};
            //var constaints = new RouteValueDictionary {
            //    { "areacode",@" 0\d{ 2,3 }" },
            //    { "days ", @" [1-3 ]{ 1 }"},
            //    { "httpMethod" , new HttpMethodConstraint("POST")}};
            //var dataTokens = new RouteValueDictionary {
            //    { "defaultCity" , "BeiJing" }, 
            //    { "defaul tDays" , 2 } };

            //RouteTable.Routes.MapPageRoute("default" , "{areacode}/{days}" ,
            //"-/weather.aspx" , false , defaults , constaints , dataTokens);

            context.MapRoute(
                "Test_default1",
                "Test/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Ctrip.MVC.Areas.Test.Controllers" }
                );

            //context.MapRoute(
            //   "Test_default2",
            //   "Test/{controller}/{action}/{p1}/{p2}",
            //   new { action = "Edit", p1 = UrlParameter.Optional, p2 = UrlParameter.Optional },
            //   namespaces: new[] { "Ctrip.MVC.Areas.Test.Controllers" },
            //   new { year = @"\d{4}", month = @"\d{2}" }
            //   );

            //http://localhost:1124/Test/Person/Edit/1001/1
            //context.MapRoute(null, "Test/{controller}/{action}/{p1}/{p2}");
            //context.MapRoute(null, "Test/{controller}/{action}/{p1}/{p2}/{p3}");
           
            //var defaults = new RouteValueDictionary{{"name" ,"女"}, {"id" , "女" } } ;
            //context.Routes.MapPageRoute("", "Test/Employee/{name}/{id}",
            //"~/Default.aspx", true, defaults);
        }
    }
}