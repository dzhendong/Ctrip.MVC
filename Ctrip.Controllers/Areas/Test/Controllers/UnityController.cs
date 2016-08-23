using Ctrip.Test;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Ctrip.MVC.Areas.Test.Controllers
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    public class UnityController : Controller
    {
        [Dependency]
        public ILogger Logger { get; set; }

        public ActionResult Index()
        {
            ViewBag.Message = Logger.Read();

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
