using System;
using System.Web.Mvc;

namespace Ctrip.Test
{
    public static class LabelExtensions
    {
        public static MvcHtmlString LKLabel(this HtmlHelper helper, string fortarget, string text)
        {
            string str = String.Format("<label for='{0}'>{1}</label>", fortarget, text);
            return new MvcHtmlString(str);
        }
    }
}