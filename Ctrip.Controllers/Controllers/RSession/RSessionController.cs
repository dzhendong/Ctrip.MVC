using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ctrip.Test.Session;

namespace Ctrip.MVC.Controllers
{
    public class RSessionController : Controller
    {
        private class test
        {
            public string name { get; set; }
        }

        public ActionResult Index()
        {
            var t = this.SessionExt().KGet<test>();

            this.SessionExt()["A"] = 123;

            return View(t);
        }

        public ActionResult Login(string name)
        {
            test t = new test() { name = name };

            //登录
            this.SessionExt().KSet<test>(t);

            return this.RedirectToAction("Index");
        }
    }
}
