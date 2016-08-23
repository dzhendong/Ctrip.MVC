using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ctrip.Test
{
    /// <summary>
    /// 对action进行了监控
    /// 这个监控功能我选择了在global里注入全局的filter来实现这个功能
    /// 为了避免并发
    /// 所选择了将记录存储到cache的队列里
    /// 再通过quartZ的任务调度功能
    /// 来实现数据的ＩＯ写入或者数据库与入
    /// filters.Add(new HandleErrorAttribute());
    /// filters.Add(new MVVM.ActionRenderTimeAttribute());
    /// </summary>
    public class ActionTimeAttribute:ActionFilterAttribute
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
                    var temp = (HttpRuntime.Cache["RunTime"] as Queue<Tuple<int, string>>) ?? new Queue<Tuple<int, string>>();
                    temp.Enqueue(new Tuple<int, string>(timeSpan, filterContext.RequestContext.HttpContext.Request.Url.AbsoluteUri));
                    System.Web.HttpRuntime.Cache.Insert("RunTime", temp);
                }
            }

            base.OnResultExecuted(filterContext);
        }
    }
}