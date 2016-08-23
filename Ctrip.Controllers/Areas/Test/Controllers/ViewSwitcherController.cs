using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Ctrip.MVC.Areas.Test.Controllers
{
    /// <summary>
    /// 演示如何手动重写 UserAgent
    /// 从而强制使用对应的视图
    /// </summary>
    public class ViewSwitcherController : Controller
    {
        public ActionResult SwitchView(bool? mobile)
        {
            mobile = mobile ?? false;

            //重写 UserAgent
            HttpContext.SetOverriddenBrowser(mobile.Value ? BrowserOverride.Mobile : BrowserOverride.Desktop);
            
            //HttpContext.SetOverriddenBrowser(string userAgent);

            return View();
        }
    }
}
