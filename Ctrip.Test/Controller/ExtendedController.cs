using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Ctrip.Test
{
    /// <summary>
    /// 配合EHAB
    /// </summary>
    public class ExtendedController : Controller
    {
        private static object syncHelper = new object();

        private static Dictionary<Type, ControllerDescriptor> controllerDescriptors= new Dictionary<Type, ControllerDescriptor>();

        protected override void OnException(ExceptionContext filterContext)
        {

        }

        public ControllerDescriptor Descriptor
        {
            get
            {
            
                ControllerDescriptor descriptor;
                if (controllerDescriptors.TryGetValue(this.GetType(), out descriptor))
                {
                    return descriptor;
                }
                lock (syncHelper)
                {
                    if (controllerDescriptors.TryGetValue(this.GetType(), out descriptor))
                    {
                        return descriptor;
                    }
                    else
                    {
                        descriptor = new ReflectedControllerDescriptor(this.GetType());controllerDescriptors.Add(this.GetType(), descriptor);
                        return descriptor;
                    }
                }
            }
        }

        //取异常处理策略名称
        public string GetExceptionPolicyName()
        {
            return "";
        }
    }
}