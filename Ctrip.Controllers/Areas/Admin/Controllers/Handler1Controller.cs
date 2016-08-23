using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using Ctrip.Test;

namespace Ctrip.MVC.Areas.Admin.Controllers
{
    public class Handler1Controller : Controller
    {
        /// <summary>
        /// 扩展消息处理通道
        /// </summary>
        public void Index()
        { 
            HttpConfiguration configuration = new HttpConfiguration();
            configuration.MessageHandlers.Add(new FooMessageHandler());
            configuration.MessageHandlers.Add(new BarMessageHandler());
            configuration.MessageHandlers.Add(new BazMessageHandler());

            HttpServer httpServer = new HttpServer(configuration);
            Response.Write(" 初始化前: " + GetPipeline(httpServer) + "<br/>");

            //以反射的方式执行Sen dA sync 方法促使HttpMessageHandler 管道的创建
            MethodInfo method = typeof (HttpMessageHandler) . GetMethod ("SendAsync" ,BindingFlags.Instance | BindingFlags.NonPublic);

            method.Invoke(httpServer, new object[] {new HttpRequestMessage() ,new CancellationToken()});

            Response.Write(" 初始化后: " + GetPipeline(httpServer) + "<br/>");
        }

        /// <summary>
        /// 利用Global Configuration 获取当前注册的AssembliesResolver 和HttpControllerTypeResolver 对象，
        /// 然后调用HttpControllerTypeResolver 的GetControllerTypes 方法得到所有的HttpController 类型并作为Model 呈现在默认的View 中
        /// </summary>
        /// <returns></returns>
        public ActionResult Index1()
        { 
            IHttpControllerTypeResolver controllerTypeResolver
                = GlobalConfiguration.Configuration.Services.GetHttpControllerTypeResolver( );

            IAssembliesResolver assernbliesResolver = GlobalConfiguration
                .Configuration.Services.GetAssembliesResolver();

            ViewBag.ControllerTypeResolver = controllerTypeResolver;
            ViewBag.AssernbliesResolver = assernbliesResolver;

            return View(controllerTypeResolver.GetControllerTypes(assernbliesResolver));
        }

        /// <summary>
        /// 类型的选择
        /// </summary>
        public ActionResult Index2()
        {
            IHttpControllerSelector controllerSelector = GlobalConfiguration
                .Configuration.Services.GetHttpControllerSelector();

            ViewBag.ControllerSelector = controllerSelector;
            return View(controllerSelector.GetControllerMapping());
        }

        private string GetPipeline(HttpMessageHandler httpServer)
        {
            string pipeline = httpServer.GetType() .Name;
            DelegatingHandler delegatingHandler = httpServer as DelegatingHandler;

            if (null != delegatingHandler && delegatingHandler.InnerHandler != null)
            {
                return pipeline + " => " + GetPipeline(delegatingHandler.InnerHandler);
            }
            else
            {
                return pipeline;
            }
        }
    }
}
