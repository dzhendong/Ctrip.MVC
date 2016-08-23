using System;
using System.Web.Mvc;

namespace Ctrip.Test
{
    /// <summary>
    /// 执行顺序
    /// OnActionExecuting
    /// OnActionExecuted
    /// OnResultExecuting
    /// OnResultExecuted
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ProfileActionFilter : ActionFilterAttribute
    {                
        public string Message { get; set; }
       
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CheckMessage(filterContext);
            filterContext.HttpContext.Response.Write(string.Format("<br/> {0} Action start.....", Message));
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            filterContext.HttpContext.Response.Write(string.Format("<br/> {0} Action finish.....", Message));
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Write(string.Format("<br/> {0} Result start.....", Message));
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Write(string.Format("<br/> {0} Result finish.....", Message));
            base.OnResultExecuted(filterContext);
        }
        
        private void CheckMessage(ActionExecutingContext filterContext)
        {
            Message = "ActionFilterAttribute";
            //if (string.IsNullOrEmpty(Message) || string.IsNullOrWhiteSpace(Message))
            //    Message = filterContext.Controller.GetType().Name + "'s " + filterContext.ActionDescriptor.ActionName;
        }
    }
}