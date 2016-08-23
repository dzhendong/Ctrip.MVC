using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Ctrip.Test
{
    /// <summary>
    /// 内置的 Action 和 Result Filter
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class RoleActionFilter1: ActionFilterAttribute
    {
        /// <summary>
        /// 应传入的功能权限值
        /// </summary>
        public string checkRole { get; set; }     

        public override void OnActionExecuting(ActionExecutingContext filterContext) 
        {            
            if(!string.IsNullOrEmpty(checkRole)) 
            {
                if(!filterContext.HttpContext.User.Identity.IsAuthenticated) 
                {    
                    //判断用户是否已经登录，没登录跳转到登录页面，
                    //string loginUrl = FormsAuthentication.LoginUrl + redirectUrl;

                    string okurl = filterContext.HttpContext.Request.RawUrl;
                    string redirectUrl =string.Format("?ReturnUrl={0}", okurl);
                    string loginUrl = "~/Account/LogOn" + redirectUrl;                   
                    filterContext.Result = new RedirectResult(loginUrl);
                } 
                else 
                {   
                    //已登录用户
                    bool isAuthorize = filterContext.HttpContext.User.IsInRole(checkRole);  

                    //判断用户是否拥有checkRole权限，没有的话跳转到权限错误页。
                    if(!isAuthorize)  
                    {
                        RouteValueDictionary a = new RouteValueDictionary(new { 
                            Controller = "Error",
                            Action = "DefaultError"
                        });

                        filterContext.Result = new RedirectResult("~/Error/NotFound");
                        filterContext.Result = new RedirectToRouteResult("Default", a);
                    }
                }
            } 
            else
            {
                throw new InvalidOperationException("该用户没有指定角色，请联系管理员给予角色。");
            }
        }
    }
}