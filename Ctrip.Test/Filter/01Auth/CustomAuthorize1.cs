using System.Web;
using System.Web.Mvc;

namespace Ctrip.Test
{
    /// <summary>
    /// 自定义Authorization Filter
    /// 此类型（或过滤器）用于限制进入控制器或控制器的某个行为方法
    /// </summary>
    public class CustomAuthorize1 : AuthorizeAttribute
    {
        /// <summary>
        /// 允许我们阻止本地的请求
        /// </summary>
        private bool localAllowed;

        public CustomAuthorize1(bool allowedParam)
        {
            localAllowed = allowedParam;
        }

        /// <summary>
        /// 在应用该Filter时，可以通过构造函数来指定是否允许本地请求
        /// </summary>                
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            httpContext.Response.Write(string.Format("<br/> {0} AuthorizeCore start.....", "CustomAuthorize1"));

            if (httpContext.Request.IsLocal)
            {
                return localAllowed;
            }
            else
            {
                return true;
            }
        }
    }
}