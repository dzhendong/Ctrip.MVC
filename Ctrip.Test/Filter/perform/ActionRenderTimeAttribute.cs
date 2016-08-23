using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ctrip.Test
{
    /// <summary>
    /// Action渲染页面所需要的时间
    /// </summary>
    public class ActionRenderTimeAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        /// <summary>
        /// 锁对象
        /// </summary>
        static object lockObj = new object();

        /// <summary>
        /// 记录进行Action的时间
        /// </summary>
        DateTime joinTime;

        /// <summary>
        /// 进行action之前
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            joinTime = DateTime.Now;
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 渲染页面HTML之后
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(System.Web.Mvc.ResultExecutedContext filterContext)
        {
            int outSeconds;//! 超时的秒数,默认为60S
            int.TryParse((System.Configuration.ConfigurationManager.AppSettings["ActionRenderTime"] ?? "60").ToString(), out outSeconds);
            var timeSpan = (DateTime.Now - joinTime).Seconds;
            if (timeSpan > outSeconds)
            {
                lock (lockObj)
                {
                    var temp = (System.Web.HttpRuntime.Cache["RunTime"] as Queue<Tuple<int, string>>) ?? new Queue<Tuple<int, string>>();
                    temp.Enqueue(new Tuple<int, string>(timeSpan, filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri));
                    System.Web.HttpRuntime.Cache.Insert("RunTime", temp);
                }
            }

            base.OnResultExecuted(filterContext);
        }
    }
}