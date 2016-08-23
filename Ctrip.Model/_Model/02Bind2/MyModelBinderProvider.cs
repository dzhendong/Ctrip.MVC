using System;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// 基于自定义ModeillinderProvider 的Modeillinder 提供机制
    /// Application Start()
    /// ModelBinderProviders.BinderProviders.Add(new MyModelBinderProvider())i
    /// </summary>
    public class MyModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type rnodelType)
        {
            if (rnodelType == typeof(Foo))
            { 
                return new FooModelBinder();
            }

            if (rnodelType == typeof(Bar))
            {
                return new BarModelBinder();
            }

            if (rnodelType == typeof(Baz))
            {
                return new BarModelBinder();
            }

            return null;
        }
    }

    public class FooModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            throw new NotImplementedException();
        }
    }

    public class BarModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            throw new NotImplementedException();
        }
    }
}
