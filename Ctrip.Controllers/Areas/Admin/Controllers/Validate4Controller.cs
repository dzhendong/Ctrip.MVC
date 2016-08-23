using System.Web.Mvc;
using Ctrip.Model;

namespace Ctrip.MVC.Areas.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ValidationRule2("Production")]
    public class Validate4Controller : Base2Controller
    {
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidationRule("Production")]
        public ActionResult SignIn(LoginModel logInfo)
        {
            if (ModelState.IsValid)
            {
                return this.View();
            }
            else
            {
                return this.View();
            }
        }

    }
}
