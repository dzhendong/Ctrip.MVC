using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// ChildActionValueProvider 的值提供机制
    /// 子Action 和普通Action 的不同之处在于它不能用于响应来自客户端的请求
	/// 只是在某个View 中被调用以生成某个部分的HTML
    /// </summary>
    public static class DictionaryValueProviderExtensions
    {
        public static Dictionary<string, ValueProviderResult> GetDataSource<TValue>(
            this System.Web.Mvc.DictionaryValueProvider<TValue> valueProvider)
        { 
            FieldInfo valuesField = typeof(DictionaryValueProvider<TValue>)
                .GetField ("_ values", BindingFlags.Instance | BindingFlags.NonPublic) ;

            return (Dictionary<string,ValueProviderResult>)valuesField.GetValue(valueProvider);
        }
    }
}
