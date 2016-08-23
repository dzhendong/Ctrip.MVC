using System;
using System.IO;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Ctrip.Model
{
    public class SimpleRazorView : IView
    {
        public string ViewPath { get; private set; }

        public SimpleRazorView(string viewPath)
        {
            this.ViewPath = viewPath;
        }

        public void Render(ViewContext viewContext, TextWriter writer)
        {
            Type viewType = BuildManager.GetCompiledType(this.ViewPath);
            object instance = Activator.CreateInstance(viewType);
            WebViewPage page = (WebViewPage)instance as WebViewPage;

            page.VirtualPath = this.ViewPath;
            page.ViewContext = viewContext;
            page.ViewData = viewContext.ViewData;
            page.InitHelpers();

            WebPageContext pageContext = new WebPageContext(viewContext.HttpContext, null, null);
            WebPageRenderingBase startPage = StartPage.GetStartPage(page, "_ViewStart", new string[] { "cshtml", "vbhtml" });
            page.ExecutePageHierarchy(pageContext, writer, startPage);
        }
    }
}
