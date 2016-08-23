using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace Ctrip.Test
{
    /// <summary>
    /// 此类型（或过滤器）用于限制进入控制器或控制器的某个行为方法
    /// </summary>
    public class CustomAuthorize2 : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // 不对ChildAction进行权限检查
            if (filterContext.IsChildAction)
            {
                return;
            }

            base.OnAuthorization(filterContext);
        }

        //public void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    var acl = filterContext.HttpContext.Session[SessionName.PermissionSessionName] as IEnumerable<IAcl>;

        //    var attNames = filterContext.ActionDescriptor.GetCustomAttributes(typeof(ResourceCustomerNameAttribute), true) as IEnumerable<ResourceCustomerNameAttribute>;

        //    var anonymous = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true) as IEnumerable<AllowAnonymousAttribute>;

        //    if (anonymous != null && anonymous.Any())
        //    {
        //        return;
        //    }
        //    if (acl == null || !acl.Any())
        //    {
        //        filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = _controller, action = _action }));
        //    }
        //    else
        //    {
        //        var joinResult = (from aclEntity in acl
        //                          join attName in attNames on aclEntity.ResourceName equals attName.ResourceName
        //                          select attName
        //        ).Any();

        //        if (joinResult)
        //        {
        //            return;
        //        }
        //        else
        //        {
        //            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = _controller, action = _action }));
        //        }
        //    }
        //}

        /// <summary>
        /// 授权验证的逻辑处理，返回true的则是通过授权，返回了false则不是
        /// </summary>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            string controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
            string actionName = httpContext.Request.RequestContext.RouteData.Values["action"].ToString();
            string action = string.Format("/{0}/{1}", controllerName, actionName);

            //// 允许的Role：action --> Activity --> Roles
            //base.Roles = SysContext.GetRolesForActivity(action);

            //// 用户的Role：从Session或Cookie中取得（在哪存的到哪取）
            //string userRole = "Guest";
            //httpContext.User = new GenericPrincipal(httpContext.User.Identity, new string[] { userRole });

            return base.AuthorizeCore(httpContext);
        }

        /// <summary>
        /// 处理授权失败
        /// </summary>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                // Ajax须返回JsonResult
                filterContext.Result = new JsonResult
                {
                    //Data = new { Error = "未授权", LogOnUrl = FormsAuthentication.LoginUrl },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                // 默认显示登录页面，也可以自定义
                //filterContext.Result = new ViewResult { ViewName = MVC.Shared.Views.Error };
                // base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class ResourceCustomerNameAttribute : Attribute
    {
        private readonly string _resourceName;

        public ResourceCustomerNameAttribute(string resourceName)
        {
            _resourceName = resourceName;
        }

        public string ResourceName
        {
            get { return _resourceName; }
        }
    }
}