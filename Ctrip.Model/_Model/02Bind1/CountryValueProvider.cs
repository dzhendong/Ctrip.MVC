using System.Globalization;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// 自定义 Value Provider
    /// </summary>
    public class CountryValueProvider : IValueProvider
    {
        /// <summary>
        /// ContainsPrefix 方法是 Model Binder 根据给定的前缀用来判断是否要解析所给数据
        /// </summary>
        public bool ContainsPrefix(string prefix)
        {
            return prefix.ToLower().IndexOf("country") > -1;
        }

        /// <summary>
        /// 自定义好了一个 Value Provider
        /// 当需要一个 Country 的值时，它始终返回"China"，其它返回 null
        /// ValueProviderResult 类的构造器有三个参数，
        /// 第一个参数是原始值对象
        /// 第二个参数是原始对象的字符串表示
        /// 最后一个是转换这个值所关联的 culture 信息
        /// </summary>
        public ValueProviderResult GetValue(string key)
        {
            if (ContainsPrefix(key))
                return new ValueProviderResult("China", "China", CultureInfo.InvariantCulture);
            else
                return null;
        }
    }
}