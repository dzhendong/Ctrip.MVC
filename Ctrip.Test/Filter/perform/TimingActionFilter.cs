using System;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;

namespace Ctrip.Test
{
    public class TimingActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            GetTimer(filterContext, "action").Start();
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            GetTimer(filterContext, "action").Stop();
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var renderTimer = GetTimer(filterContext, "render");
            renderTimer.Stop();
            var actionTimer = GetTimer(filterContext, "action");
            if (actionTimer.ElapsedMilliseconds >= 100 || renderTimer.ElapsedMilliseconds >= 100)
            {
                //LogHelper.WriteLog("运营监控(" + filterContext.RouteData.Values["controller"] + ")", String.Format(
                //        "【{0}】-【{1}】,执行:{2}ms,渲染:{3}ms",
                //        filterContext.RouteData.Values["controller"],
                //        filterContext.RouteData.Values["action"],
                //        actionTimer.ElapsedMilliseconds,
                //        renderTimer.ElapsedMilliseconds
                //    ));

                object a = filterContext.RouteData.Values["controller"];
                object b = filterContext.RouteData.Values["action"];
                string url1 = HttpContext.Current.Request.RawUrl;
                string url2 = HttpContext.Current.Request.Url.ToString();
                string url3 = HttpContext.Current.Request.Url.AbsolutePath;
                string url4 = HttpContext.Current.Request.Url.Host;
                string url5 = HttpContext.Current.Request.Url.Query;
                Uri url6 = HttpContext.Current.Request.UrlReferrer;
            }

            base.OnResultExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            GetTimer(filterContext, "render").Start();
            base.OnResultExecuting(filterContext);
        }

        private Stopwatch GetTimer(ControllerContext context, string name)
        {
            string key = "__timer__" + name;
            if (context.HttpContext.Items.Contains(key))
            {
                return (Stopwatch)context.HttpContext.Items[key];
            }

            var result = new Stopwatch();
            context.HttpContext.Items[key] = result;
            return result;
        }
    }
}