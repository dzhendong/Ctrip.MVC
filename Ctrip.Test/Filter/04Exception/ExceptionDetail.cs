using System;

namespace Ctrip.Test
{
    /// <summary>
    /// 用于实施认证的Action 方法In dex 也可以通过Aj 缸请求的方式来调用。
    /// 对于Ajax 请求来说，
    /// 我们会将通过EntLib 处理后的异常封装成如下一个类型为ExceptionD etail 的对象，
    /// 它具有与Exception 对应的属性设置，最终根据这个ExceptionDetail 对象创建一个JsonResult来响应当前的请求。
    /// </summary>
    public class ExceptionDetail
    {
        public string HelpLink { get; set; }
        public ExceptionDetail InnerException { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string ExceptionType { get; set; }

        public ExceptionDetail(Exception exception, string errorMessage = null)
        {
            this.HelpLink = exception.HelpLink;
            this.Message = string.IsNullOrEmpty(errorMessage)? exception.Message : errorMessage;
            this.StackTrace = exception.StackTrace;
            this.ExceptionType = exception.GetType() .ToString();
            if (exception.InnerException != null)
            {
                this.InnerException = new ExceptionDetail(exception.InnerException);
            }
        }
    }
}