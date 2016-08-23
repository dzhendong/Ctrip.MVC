using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ctrip.Test
{
    /// <summary>
    /// 异常筛选器
    /// ExceptionContext
    /// --> ControllerContext 
    /// </summary>
    public class RangeExceptionAttribute : System.Web.Mvc.FilterAttribute, System.Web.Mvc.IExceptionFilter
    {
        /// <summary>
        /// 需要ErrorController
        /// 不能传递参数
        /// </summary>      
        //public void OnException(ExceptionContext filterContext)
        //{
        //    UrlHelper url = new UrlHelper(filterContext.RequestContext);
        //    string a = url.Action("NotFound", "Error");

        //    if (!filterContext.ExceptionHandled && filterContext.Exception is ArgumentOutOfRangeException)
        //    {
        //        filterContext.Result = new RedirectResult("~/Error/NotFound");
        //        filterContext.ExceptionHandled = true;
        //    }
        //}

        //public void OnException(ExceptionContext filterContext)
        //{
        //    UrlHelper url = new UrlHelper(filterContext.RequestContext);
        //    filterContext.ExceptionHandled = true;
        //    filterContext.Result = new RedirectResult(url.Action("NotFound", "Error"));
        //}

        /// <summary>
        /// 此情况没有验证成功
        /// 不需要ErrorController
        /// 能传递参数
        /// 一旦错误页面也发生了错误，设置
        /// <customErrors mode="On" defaultRedirect="/Error/RangeError2"/>
        /// </summary>      
        public void OnException(ExceptionContext filterContext)
        {            
            if (!filterContext.ExceptionHandled && filterContext.Exception is ArgumentOutOfRangeException)
            {                
                int val = (int)(((ArgumentOutOfRangeException)filterContext.Exception).ActualValue);

                //如何传递数据,暂时没有成功
                RouteValueDictionary a = new RouteValueDictionary(new { 
                    Controller = "Error",
                    Action = "RangeError",
                    Exception = filterContext.Exception
                });

                //DefaultError要注意页面的存放位置
                ViewResult b = new ViewResult {
                    //MasterName = this.Master,                            
                    ViewName = "DefaultError", 
                    ViewData = new ViewDataDictionary<int>(val),
                    TempData = filterContext.Controller.TempData
                };
                
                filterContext.Result = b;                
                //filterContext.Result = new RedirectToRouteResult("Default", a);
                filterContext.ExceptionHandled = true;
            }
        }
    }
}