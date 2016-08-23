using System;
using System.Web.Mvc;

namespace Ctrip.Test
{
    /// <summary>
    /// @Html.DisplayMessage("Test")
    /// MVC 会把它编译成 HtmlHelper 类的扩展方法
    /// </summary>
    public static class CustomHelpers
    {
        public static MvcHtmlString StripHtml(this HtmlHelper html, string input)
        {
            return new MvcHtmlString(System.Text.RegularExpressions.Regex.Replace(input, "<.*?>", string.Empty));
        }

        public static MvcHtmlString DisplayMessage(this HtmlHelper html, string msg)
        {
            //string result = String.Format("This is the message: <p>{0}</p>", msg);
            string result = String.Format("This is the message: <p>{0}</p>", html.Encode(msg));            
            return new MvcHtmlString(result);
        }
    }
}