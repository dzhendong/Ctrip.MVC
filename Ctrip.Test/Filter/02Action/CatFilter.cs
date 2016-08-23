using System.Web.Mvc;

namespace Ctrip.Test.Filter._02Action
{
    /// <summary>
    /// Cat拦截器，主要拦截Http请求
    /// </summary>
    public class CatFilter : System.Web.Mvc.ActionFilterAttribute
    {
        /// <summary>
        /// 请求来到时
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 请求结束时
        /// 调用次序：A->B->C->c->b->a,从c开始执行，把context结果在响应头里依据向回传
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //var context = PureCat.CatClient.GetCatContextFromServer();
            //if (context != null)
            //{

            //    context = PureCat.CatClient.DoTransaction("youDomain", filterContext.HttpContext.Request.Url.AbsoluteUri, () =>
            //    {
            //        PureCat.CatClient.LogRemoteCallServer(context);

            //        PureCat.CatClient.LogEvent(filterContext.HttpContext.Request.Url.AbsoluteUri, "Action  Finish...");

            //        if (filterContext.Exception != null)
            //        {
            //            PureCat.CatClient.LogError(filterContext.Exception);
            //        }
            //    });

            //    #region 响应头写数据
            //    if (filterContext.HttpContext.Response.Headers.GetValues("catContext") != null
            //        && filterContext.HttpContext.Response.Headers.GetValues("catContext").Length > 0)
            //    {
            //        filterContext.HttpContext.Response.Headers.Remove("catContext");
            //    }
            //    filterContext.HttpContext.Response.Headers.Add("catContext", Lind.DDD.Utils.SerializeMemoryHelper.SerializeToJson(context));

            //    #endregion
            //}

            base.OnActionExecuted(filterContext);
        }
    }
}