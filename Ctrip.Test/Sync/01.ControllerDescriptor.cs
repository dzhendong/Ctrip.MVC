using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace Ctrip.Test
{
    /// <summary>
    /// ControllerDescriptor的同步与异步
    /// public class FooController : Controller
    /// protected override IActionInvoker CreateActionInvoker()
    /// return new FooActionInvoker();
    /// </summary>
    public class FooActionInvoker : ControllerActionInvoker
    {
        public new ControllerDescriptor GetControllerDescriptor(ControllerContext controllerContext)
        {
            return base.GetControllerDescriptor(controllerContext);
        }
    }

    /// <summary>
    /// 如果采用ControllerActionInvoker，Action总是以同步的方式来直接
    /// 但是当AsyncControllerActionInvoker作为Controller的ActionInvoker时
    /// 并不意味着总是以异步的方式来执行所有的Action
    /// public class BarController : Controller
    /// protected override IActionInvoker CreateActionInvoker()
    /// return new BarActionInvoker();
    /// </summary>
    public class BarActionInvoker : AsyncControllerActionInvoker
    {
        public new ControllerDescriptor GetControllerDescriptor(ControllerContext controllerContext)
        {
            return base.GetControllerDescriptor(controllerContext);
        }
    }
}