using System.Collections.Generic;
using System.Web.Mvc;
using Ctrip.Model;

namespace Ctrip.MVC.Areas.Admin.Controllers
{
    /// <summary>
    /// 演示MVC 在执行目标Action 的时候
    /// 是如何基于参数创建Model 绑定上下文，
    /// 并最终利用Modeffiinder 来提供具体参数值的
    /// </summary>
    public abstract class BaseController : Controller
    {
        public IModelBinder ModelBinder { get; private set; }

        public BaseController()
        {            
            this.ModelBinder = new DefaultModelBinder1();
            this.ValueProvider = this.CreateValueProvider();
        }

        protected abstract IValueProvider CreateValueProvider();

        protected ActionResult InvokeAction(string actionName)
        {
            ControllerDescriptor controllerDescriptor =new ReflectedControllerDescriptor(this.GetType());
            ActionDescriptor actionDescriptor = controllerDescriptor.FindAction(ControllerContext, actionName);

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            foreach (ParameterDescriptor parameterDescriptor in actionDescriptor.GetParameters())
            {
                string modelName = parameterDescriptor.BindingInfo.Prefix ?? parameterDescriptor.ParameterName;

                ModelBindingContext bindingContext = new ModelBindingContext
                {
                    FallbackToEmptyPrefix = parameterDescriptor.BindingInfo.Prefix == null,
                    ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, parameterDescriptor.ParameterType),
                    ModelName = modelName,
                    ModelState = ModelState,
                    ValueProvider = this.ValueProvider
                };

                parameters.Add(parameterDescriptor.ParameterName, this.ModelBinder.BindModel(ControllerContext, bindingContext));
            }

            return (ActionResult)actionDescriptor.Execute(ControllerContext, parameters);
        }     
    }
}
