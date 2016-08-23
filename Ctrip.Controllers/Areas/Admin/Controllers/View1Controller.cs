using Ctrip.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Ctrip.MVC.Areas.Admin.Controllers
{
    /// <summary>
    /// ViewResult 为我们提供了一种与View 引擎交互的捷径
    /// 其实我们在进行View 的获取和呈现的时候完全可以抛开ViewResult
    /// 介绍了ViewResult 利用View 引擎进行View 的获取和呈现
    /// 其实当我们调用HtmlHelper 的扩展方法Partial 进行Partial View 呈现时
    /// 内部调用View 引擎的方式与之类似。
    /// </summary>
    public class View1Controller : Controller
    {
        public void Index()
        {
            string viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewEngineResult result = ViewEngines.Engines.FindView(ControllerContext,viewName, null);

            if (null == result.View)
            {
                throw new InvalidOperationException(FormatErrorMessage(viewName, result.SearchedLocations));
            }

            try
            { 
                ViewContext viewContext =new ViewContext(
                    ControllerContext,
                    result.View, 
                    this.ViewData ,
                    this.TempData, 
                    Response.Output);

                result.View.Render(viewContext, viewContext.Writer);
            }
            finally
            {
                result.ViewEngine.ReleaseView(ControllerContext, result.View);
            }
        }

        private string FormatErrorMessage(string viewName,IEnumerable<string> searchedLocations)
        { 
            string format = "The view '{ O}' or i ts master was not found or no view engine supports the searched loc~t~ons. The following locations weresearched:{l}";

            StringBuilder builder = new StringBuilder();
            foreach (string str in searchedLocations)
            {
                builder.AppendLine();
                builder.Append(str);
            }

            return string.Format(CultureInfo.CurrentCulture, format, viewName,builder);
        }

        public ActionResult Index1()
        {
            Response.ClearHeaders();
            Response.ClearContent();
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", "A"));
            
            ViewData.Model = new Contact
            {
                Name = "张三",
                PhoneNo = "1111111111",
                EmailAddress = "111@11.com"
            };

            string filePath = "~/Areas/Admin/Views/View1/View1.cshtml";
            
            
            Type viewType = BuildManager.GetCompiledType(filePath);
            object instance = Activator.CreateInstance(viewType);

            //一:使用了_ViewStart
            WebViewPage page = (WebViewPage)instance as WebViewPage;
            WebPageRenderingBase startPage = StartPage.GetStartPage(page, "_ViewStart", new string[] { "cshtml", "vbhtml" });
            
            //二:单独的页面
            RazorView view = new RazorView(this.ControllerContext, filePath, null, false, null);

            //三:使用自定义的View
            //SimpleRazorView view = new SimpleRazorView(filePath);

            ViewContext viewContext = new ViewContext(ControllerContext, view, ViewData, TempData, Response.Output);
            view.Render(viewContext, viewContext.Writer);

            Response.Flush();
            Response.End();
            Response.Close();
            return View();        
        }
    }
}
