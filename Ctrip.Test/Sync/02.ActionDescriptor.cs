
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Async;
namespace Ctrip.Test
{
    /// <summary>
    /// ActionDescriptor的同步与异步 
    /// </summary>
    public class HomeController : AsyncController
    {
        public void Index()
        {
            MethodInfo method = typeof(AsyncControllerActionInvoker).GetMethod("GetControllerDescriptor", BindingFlags.Instance | BindingFlags.NonPublic);
            ControllerDescriptor controllerDescriptor = (ControllerDescriptor)method.Invoke(this.ActionInvoker, new object[] { this.ControllerContext });
            Response.Write(controllerDescriptor.GetType().FullName + "<br/>");
            CheckAction(controllerDescriptor, "Foo");
            CheckAction(controllerDescriptor, "Bar");
            CheckAction(controllerDescriptor, "Baz");
        }

        private void CheckAction(ControllerDescriptor controllerDescriptor, string actionName)
        {
            ActionDescriptor actionDescriptor = controllerDescriptor.FindAction(this.ControllerContext, actionName);
            Response.Write(string.Format("{0}: {1}<br/>", actionName, actionDescriptor.GetType().FullName));
        }

        public void Foo() { }

        public void BarAsync() { }

        public void BarCompleted() { }

        public Task<ActionResult> Baz()
        {
            throw new NotImplementedException();
        }
    }
}