using System;
using System.Web.Mvc;

namespace Ctrip.Test
{
    public static class RadioButtonExtensions
    {
        public static MvcHtmlString LKRadioButton(this HtmlHelper helper, string nametarget, string idtarget)
        {
            string str = String.Format("<input type='radio' name='{0}' id='{1}' />", nametarget, idtarget);
            return new MvcHtmlString(str);
        }
    }
}