using System.Web.Mvc;

namespace Ctrip.Test
{
    /// <summary>
    /// 自定义 Action Invoker
    /// 创建一个自定义的Action调用者
    /// </summary>
    public class CustomActionInvoker : IActionInvoker
    {
        public bool InvokeAction(ControllerContext controllerContext, string actionName)
        {
            if (actionName == "Index")
            {
                controllerContext.HttpContext.Response.Write("This is output from the Index action");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}