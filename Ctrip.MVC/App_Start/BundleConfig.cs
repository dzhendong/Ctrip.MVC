using System.Web;
using System.Web.Optimization;

namespace Ctrip.MVC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Content/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Content/Scripts/jquery.unobtrusive*",
                        "~/Content/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/Content/Webjs/index").Include(
                      "~/Content/Webjs/headsearch_a-1.0.1.js",
                      "~/Content/Webjs/index-1.0.1.js"));           
        }
    }
}