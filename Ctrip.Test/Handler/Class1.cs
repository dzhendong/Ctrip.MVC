using System.Net.Http;

namespace Ctrip.Test
{
    /// <summary>
    /// DelegatingHandler
    /// NETWebAPI 管道中用于处理请求和响应的H即MessageHandler 并不是孤立的
    /// 而是作为一个完整的HttpMessageHandler 链中的某个环节而存在的
    /// 这种链式结构可以通过具有如下定义的System.Net. Http.DelegatingHandler 来构建。
    /// </summary>
    public class FooMessageHandler : DelegatingHandler
    {
    }

    public class BarMessageHandler : DelegatingHandler
    { }
    public class BazMessageHandler : DelegatingHandler
    { }
}