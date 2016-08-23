using System.Diagnostics;
using System.Web.Http.Filters;

namespace Ctrip.Test.Filter._04Exception
{
    /// <summary>
    /// public static void RegisterApis(HttpConfiguration config) 
    /// config.Filters.Add(new LogExceptionFilter());
    /// </summary>
    public class LogExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //增加二行 Trace 代码

            Trace.TraceError("异常: {0}", actionExecutedContext.Exception.Message);
            Trace.TraceError("请求 URI: {0}", actionExecutedContext.Request.RequestUri);

            base.OnException(actionExecutedContext);
        }
    } 
}