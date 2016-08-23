using System.Collections.Specialized;
using System.Globalization;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// 创建一个自定义ValueProviderFactory
    /// MVC 提供的6 种ValueProviderFactory 基本上可以满足绝大部分Model 绑定需求
    /// 将创建一个以HTTP 请求报头集合作为数据来源的ValueProviderFactory
    /// 剔除了HTTP 报头名称中包含的" "字符。
    /// Application_Start
    /// ValueProviderFactories.Factories.Add (HttpHeaderValueProviderFactory (New));
    /// </summary>
    public class HttpHeaderValueProviderFactory : ValueProviderFactory
    {
        /// <summary>
        /// 我们将针对指定的Controller上下文得到HTTP报头集合
        /// 并借此创建NameValueCollection对象。
        /// 由于作为报头名称具有“-”字符，为了与参数命名规则相匹配，我们将该字符剔除
        /// </summary>
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            NameValueCollection requestData = new NameValueCollection();
            var headers = controllerContext.RequestContext.HttpContext.Request.Headers;

            foreach (string key in headers.Keys)
            {
                requestData.Add(key.Replace("-", ""), headers[key]);
            }

            return new NameValueCollectionValueProvider(requestData,CultureInfo.InvariantCulture);
        }
    }
}
