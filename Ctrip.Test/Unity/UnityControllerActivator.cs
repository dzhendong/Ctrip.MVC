using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ctrip.Test
{
    /// <summary>
    /// 激活与自定义的行为控制器
    /// 并且可以使用依赖注入
    /// 让我们创建一个派生自IControllerActivator 接口的一个自定义的控制器
    /// </summary>
    public class UnityControllerActivator : IControllerActivator
    {
        IController IControllerActivator.Create(RequestContext requestContext,Type controllerType)
        {
            return DependencyResolver.Current
                .GetService(controllerType) as IController;
        }
    }
}