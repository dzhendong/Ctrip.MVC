using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace Ctrip.Test
{
    /// <summary>
    /// 操作筛选器
    /// Action Filter是对action方法的执行进行“筛选”的
    /// 包括执行前和执行后。它需要实现 IActionFilter 接口
    /// OnActionExecuting
    /// OnActionExecuted
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ProfileActionAttribute : System.Web.Mvc.FilterAttribute, System.Web.Mvc.IActionFilter
    {
        private Stopwatch timer;
     
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            timer = Stopwatch.StartNew();

            filterContext.HttpContext.Response.Write("<br/> IActionFilter start.....");
        }

        /// <summary>
        /// ProfileAction的 OnActionExecuted 方法是在 FilterTest 方法返回结果之前执行的。
        /// 确切的说
        /// OnActionExecuted 方法是在action方法执行结束之后和处理action返回结果之前执行的。
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            timer.Stop();

            filterContext.Controller.TempData.Add("A", "A");
            filterContext.Controller.ViewBag.CurrentUser = "1";

            if (filterContext.Exception == null)
            {               
                filterContext.HttpContext.Response.Write(
                    string.Format("<div>IActionFilter end: {0}</div>", timer.Elapsed.TotalSeconds));
            }
        }
    } 
}