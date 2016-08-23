using System;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace Ctrip.Test
{
    /// <summary>
    /// 自定义处理工厂
    /// </summary>
    public class CustomControllerFactory : IControllerFactory
    {
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            Type targetType = null;
            //switch (controllerName)
            //{
            //    case "Product":
            //        targetType = typeof(ProductController);
            //        break;
            //    case "Customer":
            //        targetType = typeof(CustomerController);
            //        break;
            //    default:
            //        requestContext.RouteData.Values["controller"] = "Product";
            //        targetType = typeof(ProductController);
            //        break;
            //}
            return targetType == null ? null : (IController)DependencyResolver.Current.GetService(targetType);

            //string controllerType = string.Empty;
            //IController controller = null;
            //controllerType = ConfigurationManager.AppSettings[controllerName];

            //if (controllerType == null)
            //{
            //    throw new ConfigurationErrorsException("Assembly not configured for controller " + controllerName);
            //}

            //controller = Activator.CreateInstance(Type.GetType(controllerType)) as IController;
            //return controller;
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }    
}