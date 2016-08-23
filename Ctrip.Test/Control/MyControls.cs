using System;
using System.Web.Mvc;

namespace Ctrip.Test
{
    public class MyControls
    {
        /// <summary>
        /// Lable文本
        /// </summary>
        /// <param name="fortarget">for属性</param>
        /// <param name="text">显示文本</param>
        /// <returns></returns>
        public static MvcHtmlString Label(string fortarget, string text)
        {
            string str = String.Format("<label for='{0}'>{1}</label>", fortarget, text);
            return new MvcHtmlString(str);

        }

        public static MvcHtmlString Label(string text)
        {
            return Label("", text);
        }


        /// <summary>
        /// RadioButton
        /// </summary>
        /// <param name="nametarget">name属性</param>
        /// <param name="idtarget">id属性</param>
        /// <returns></returns>
        public static MvcHtmlString RadioButton(string nametarget, string idtarget)
        {
            string str = String.Format("<input type='radio' name='{0}' id='{1}' />", nametarget, idtarget);
            return new MvcHtmlString(str);
        }
    }
}