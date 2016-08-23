using System.Web.Mvc;

namespace Ctrip.Test.Session
{
    public class SessionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 每次请求都续期
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //new RedisSession(filterContext.HttpContext).Postpone();
            //new LocalSession(filterContext.HttpContext).Postpone();
        }
    }
}

