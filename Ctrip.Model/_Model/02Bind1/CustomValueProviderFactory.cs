using System.Globalization;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// 为了让 Model Binder 调用这个 Value Provider
    /// 我们需要创建一个能实现化它的类
    /// 这个类需要继承  ValueProviderFactory 抽象类
    /// Application_Start
    /// ValueProviderFactories.Factories.Insert(0, new CustomValueProviderFactory());
    /// </summary>
    public class CustomValueProviderFactory : ValueProviderFactory
    {
        /// <summary>
        /// 当 model binder 在绑定的过程中需要获取值时会调用这里的 GetValueProvider 方法
        /// </summary>
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {            
            return new CountryValueProvider();
        }
    }
}
