using System;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// 自定义 Model Binder
    /// 当 MVC 框架需要一个 model 类型的实现时，则调用 BindModel 方法。
    /// 它的 ControllerContext 类型参数提供请求相关的上下文信息，
    /// ModelBindingContext 类型参数提供 model 对象相关的上下文信息。
    /// ModelBindingContext 常用的属性有Model、ModelName、ModelType 和 ValueProvider。
    /// 这里的 GetValue 方法用到的 context.ModelName 属性可以告诉我们
    /// 如果有前缀（一般指复合类型名）
    /// 则需要把它加在属性名的前面
    /// 这样 MVC 才能获取到以 [0].City、[0].Country 名称传递的值。
    /// </summary>
    public class AddressBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Address2 model = (Address2)bindingContext.Model ?? new Address2();
            model.City = GetValue(bindingContext, "City");
            model.Country = GetValue(bindingContext, "Country");
            return model;
        }

        private string GetValue(ModelBindingContext context, string name)
        {
            name = (context.ModelName == "" ? "" : context.ModelName + ".") + name;
            ValueProviderResult result = context.ValueProvider.GetValue(name);
            if (result == null || result.AttemptedValue == "")
                return "<Not Specified>";
            else
                return (string)result.AttemptedValue;
        }
    }
}