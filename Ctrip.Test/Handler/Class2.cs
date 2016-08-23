using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Ctrip.Test
{
    /// <summary>
    /// 一些客户端只支持GET 和POST 这两种基本的HTTP 方法
    /// 意味着它不能正常发送PUT 和DELETE 请求对资源作添加和删除操作
    /// HTTP 方法重写
    /// 针对资源添加和删除操作的HTTP 请求依然可以采用POST 方法
    /// X-HTTP-Me由od-Override
    /// </summary>
    public class HttpMethodOverrideHandler : DelegatingHandler
    {
        /// <summary>
        /// Application Start()
        /// GlobalConfiguration.Configuration.MessageHandlers.Add(new HttpMethodOverrideHandler())i
        /// </summary>
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request , CancellationToken cancellationToken)
        {
            IEnumerable<string> rnethodOverrideHeader;

            if (request. Method == HttpMethod.Post && request.Headers.TryGetValues ("X-HTTP-Method-Override" , out rnethodOverrideHeader))
            {
                request.Method = new HttpMethod(rnethodOverrideHeader.First());
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}