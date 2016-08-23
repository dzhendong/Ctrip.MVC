using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ctrip.Test
{
    /// <summary>
    ///　权限拦截
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class RoleActionFilter2 : ActionFilterAttribute
    {
        filterContextInfo fcinfo;
     
        /// <summary>
        /// 在执行操作方法之前由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            fcinfo = new filterContextInfo(filterContext);
          
            bool isstate = true;
          
            if (isstate)
            {
                RouteValueDictionary a = new RouteValueDictionary(new { Controller = "Error", Action = "DefaultError" });

                filterContext.Result = new HttpUnauthorizedResult();
                filterContext.Result = new RedirectResult("http://www.baidu.com");                
                filterContext.Result = new RedirectToRouteResult("Default",a);
            }
            else
            {
                filterContext.Result = new ContentResult { Content = @"抱歉,你不具有当前操作的权限！" };
            }

        }

        /// <summary>
        /// 在执行操作方法后由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
      
        /// <summary>
        /// OnResultExecuting 
        /// 在执行操作结果之前由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        /// <summary>
        ///  OnResultExecuted 
        ///  在执行操作结果后由 ASP.NET MVC 框架调用。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
    }

    public class filterContextInfo
    {
        public filterContextInfo(ActionExecutingContext filterContext)
        {           
            // 获取域名
            domainName = filterContext.HttpContext.Request.Url.Authority;

            //获取模块名称
            module = filterContext.HttpContext.Request.Url.Segments[1].Replace('/', ' ').Trim();

            //获取 controllerName 名称
            controllerName = filterContext.RouteData.Values["controller"].ToString();

            //获取ACTION 名称
            actionName = filterContext.RouteData.Values["action"].ToString();
        }

        /// <summary>
        /// 获取域名
        /// </summary>
        public string domainName { get; set; }

        /// <summary>
        /// 获取模块名称
        /// </summary>
        public string module { get; set; }

        /// <summary>
        /// 获取 controllerName 名称
        /// </summary>
        public string controllerName { get; set; }

        /// <summary>
        /// 获取ACTION 名称
        /// </summary>
        public string actionName { get; set; }
    }
}