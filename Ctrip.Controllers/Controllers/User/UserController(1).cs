using System.Web.Mvc;
using System.Web.Routing;
using Ctrip.Model;

namespace Ctrip.MVC.Controllers
{
    /// <summary>
    /// 用户登录
    /// </summary>
    public class UserController : Controller
    {
        public IUserBusiness UserBusiness { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (UserBusiness == null) { UserBusiness = new UserBusiness(); }

            base.Initialize(requestContext);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOn()
        {
            Session["A"] = 1;
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(User user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (UserBusiness.GetByNameAndPassword(user.UserName, user.Password) != null)
                {
                    UserBusiness.SignIn(user.UserName, user.RememberMe);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "提供的用户名或密码不正确。");
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(user);
        }

        public ActionResult LogOff()
        {
            UserBusiness.SignOut();

            return RedirectToAction("Index", "User");
        }

        public ActionResult Calendar()
        {
            return View("Index");
        }

    }
}
