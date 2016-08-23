using Ctrip.Test;
using Microsoft.Practices.Unity;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ctrip.MVC
{    
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            /*
             * 显示模式可以方便地为不同客户端提供不同视图
             * 默认 DisplayModeProvider.Instance.Modes 有两种显示模式，分别是 Mobile 和 ""
             * 
             * 以 Home/Index.cshtml 为例
             * 1、windows phone 客户端访问会使用 Index.wp.cshtml 视图
             * 2、其他移动客户端访问会使用 Index.Mobile.cshtml 视图
             * 3、不符合以上两个条件的客户端访问会使用 Index.cshtml 视图
             * 注：找不到对应的视图时，会默认使用 Index.cshtml 视图
             */

            ////为 windows phone 客户端新增加一个名为 wp 的显示模式
            ////设置判断 windows phone 客户端的条件
            //DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("wp")
            //{
            //    ContextCondition = (context => context.GetOverriddenUserAgent().IndexOf
            //        ("Windows Phone", StringComparison.InvariantCultureIgnoreCase) >= 0)
            //});

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = false;

            //根据资源设置元数据
            //DisplayTextAttribute.SetResourceType(typeof(UI));
            //ModelMetadataProviders.Current = new ExtendedDataAnnotationsProvider();

            //自定义
            //ValueProviderFactories.Factories.Insert(0, new CustomValueProviderFactory());    

            IUnityContainer container = GetUnityContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        /// <summary>
        ///  设置资源文件区域
        /// </summary>        
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            string cultureInfoString = "en";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfoString);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureInfoString);
        }

        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <returns></returns>
        private IUnityContainer GetUnityContainer()
        {        
            IUnityContainer container = new UnityContainer()
            .RegisterType<IControllerActivator, UnityControllerActivator>()
            .RegisterType<ILogger, DatabaseLogger>();
            return container;
        }

        //protected void Application_Error(object s, EventArgs e)
        //{
        //    Exception ex = Server.GetLastError();

        //    if (ex.GetType().Name == "HttpException")
        //    {
        //        HttpException exception = (HttpException)ex;

        //        if (exception.GetHttpCode() == 404)
        //        {
        //            Response.StatusCode = 404;
        //        }
        //    }

        //    Server.ClearError();
        //}
    }
}