using System.Diagnostics;
using System.Web.Mvc;

namespace Ctrip.Test
{
    /// <summary>
    /// 结果筛选器
    /// </summary>
    public class ProfileResultAttribute : System.Web.Mvc.FilterAttribute, System.Web.Mvc.IResultFilter
    {
        private Stopwatch timer;
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            timer = Stopwatch.StartNew();

            filterContext.HttpContext.Response.Write("<br/> IResultFilter start.....");
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            timer.Stop();
            filterContext.HttpContext.Response.Write(
                string.Format("<div>IResultFilter end: {0}</div>", timer.Elapsed.TotalSeconds));
        }
    }
}