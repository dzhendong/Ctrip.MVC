using Ctrip.Controllers.Controllers;
using Ctrip.Model;
using System.Web.Mvc;
using System.Web.Routing;

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
            Session["UserId"] = "0";
            Session.Clear();
            return View();
        }

        public void Notify()
        {
            RechargeDomian.PaySuccess(1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                        //return RedirectToAction("Index", "Home");

                        //设置登录会话
                        Session["UserId"] = "1";

                        return RedirectToAction("Index", "User");
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

        public ActionResult New()
        {
            //if (Session["UserId"] == null ||
            //    Session["UserId"].ToString() == "0")
            //{
            //    ViewData["Message"] = "UserId!";
            //}
            return View();
        }

        [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult New1()
         {
             return View("New");
         }

        public ActionResult Calendar()
        {
            return View("Index");
        }
    }
}
