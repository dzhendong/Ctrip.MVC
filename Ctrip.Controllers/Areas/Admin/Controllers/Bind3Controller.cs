using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Ctrip.MVC.Areas.Admin.Controllers
{
    public class Bind3Controller : Controller
    {
        public ActionResult Index1()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return View();
        }

        /// <summary>
        /// 上传文件的值对象提供机制
        /// </summary>
        [HttpPost]
        public void DisplayPostedFiles()
        {
            HttpFileCollectionValueProvider valueProvider = new HttpFileCollectionValueProvider(ControllerContext);
            IEnumerable<HttpPostedFileBase> foo = (IEnumerable<HttpPostedFileBase>)valueProvider.GetValue("foo").ConvertTo(typeof(IEnumerable<HttpPostedFileBase>));
            IEnumerable<HttpPostedFileBase> bar = (IEnumerable<HttpPostedFileBase>)valueProvider.GetValue("bar").ConvertTo(typeof(IEnumerable<HttpPostedFileBase>));

            Response.Write("foo<br/>");
            foreach (var file in foo)
            {
                Response.Write(file.FileName + "<br/>");
            }

            Response.Write("<br/>bar<br/>");
            foreach (var file in bar)
            {
                Response.Write(file.FileName + "<br/>");
            }
        }
        
        /// <summary>
        /// 子Action和普通意义上的Action的不同之处在于它不能用于响应来自客户端的请求
        /// 而在某个View中被调用以生成某个部分的HTML
        /// ChildActionValueProvider专门服务于针对子Action方法参数的Model绑定
        /// </summary>
        /// <returns></returns>
        public ActionResult DisplayRouteData()
        {
            ControllerContext.RouteData.Values["Foo"] = "abc";
            ControllerContext.RouteData.Values["Bar"] = "ijk";
            ControllerContext.RouteData.Values["Baz"] = "xyz";

            StringBuilder sb = new StringBuilder();
            ChildActionValueProvider valueProvider = new ChildActionValueProvider(ControllerContext);
            FieldInfo valuesField = typeof(DictionaryValueProvider<object>).GetField("_values", BindingFlags.Instance | BindingFlags.NonPublic);
            Dictionary<string, ValueProviderResult> values = (Dictionary<string, ValueProviderResult>)valuesField.GetValue(valueProvider);
            foreach (string key in values.Keys)
            {
                sb.Append(string.Format("{0}: {1}<br/>", key, values[key].RawValue));
                DictionaryValueProvider<object> innerValueProvider = values[key].RawValue as DictionaryValueProvider<object>;
                if (innerValueProvider == null)
                {
                    continue;
                }
                sb.Append(string.Format("&nbsp;&nbsp;&nbsp;&nbsp;{0}: {1}<br/>", "Foo", innerValueProvider.GetValue("Foo").RawValue));
                sb.Append(string.Format("&nbsp;&nbsp;&nbsp;&nbsp;{0}: {1}<br/>", "Bar", innerValueProvider.GetValue("Bar").RawValue));
                sb.Append(string.Format("&nbsp;&nbsp;&nbsp;&nbsp;{0}: {1}<br/>", "Baz", innerValueProvider.GetValue("Baz").RawValue));
            }

            sb.Append("<br/>ChildActionValueProvider.GetValue()<br/>");
            sb.Append(string.Format("{0}: {1}<br/>", "Foo", valueProvider.GetValue("Foo").RawValue));
            sb.Append(string.Format("{0}: {1}<br/>", "Bar", valueProvider.GetValue("Bar").RawValue));
            sb.Append(string.Format("{0}: {1}<br/>", "Baz", valueProvider.GetValue("Baz").RawValue));

            return Content(sb.ToString());
        }
    }
}
