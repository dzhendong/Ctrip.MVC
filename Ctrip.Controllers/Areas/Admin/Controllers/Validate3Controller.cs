using System.Web.Mvc;
using Ctrip.Model;

namespace Ctrip.MVC.Areas.Admin.Controllers
{
    /// <summary>
    /// 一种Model 类型，多种验证规则
    /// </summary>
    public class Validate3Controller : RuleBasedController
    {
        [ValidationRule("Rulel")]
        public ActionResult Rulel()
        {
            return View("person", new Employee6());
        }

        [HttpPost]
        [ValidationRule("Rulel")]
        public ActionResult Rulel(Employee6 person)
        {
            return View("person", person);
        }

        [ValidationRule("Rule2")]
        public ActionResult Rule2()
        {
            return View("person", new Employee6());
        }

        [ValidationRule("Rule2")]
        public ActionResult Rule2(Employee6 person)
        {
            return View("person", person);
        }
    }
}
