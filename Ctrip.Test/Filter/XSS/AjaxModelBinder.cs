using System;
using System.ComponentModel;
using System.Web;
using System.Web.Mvc;

namespace XSS.Web.Common
{
    /// <summary>
    /// ModelBinders.Binders.DefaultBinder = new AjaxModelBinder();
    /// </summary>
    public class AjaxModelBinder : DefaultModelBinder
    {
        protected override bool OnPropertyValidating(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, object value)
        {
            var contentType = controllerContext.HttpContext.Request.ContentType;

            if (contentType.Equals("application/json", StringComparison.OrdinalIgnoreCase) &&
                value is string &&
                controllerContext.Controller.ValidateRequest &&
                bindingContext.PropertyMetadata[propertyDescriptor.Name].RequestValidationEnabled)
            {
                if (IsDangerousString(value.ToString()))
                {
                    throw new HttpRequestValidationException("在请求中检测到有潜在危险的值！");
                }
            }

            return base.OnPropertyValidating(controllerContext, bindingContext, propertyDescriptor, value);
        }

        /// <summary>
        /// Refer the method "System.Web.CrossSiteScriptingValidation.IsDangerousString".
        /// </summary>
        private static bool IsDangerousString(string str)
        {
            var startingChars = new[] { '<', '&' };
            var startIndex = 0;

            while (true)
            {
                var index = str.IndexOfAny(startingChars, startIndex);

                if (index < 0)
                {
                    return false;
                }

                if (index == (str.Length - 1))
                {
                    return false;
                }

                var ch = str[index];

                if (ch != '&')
                {
                    if ((ch == '<') && ((IsAtoZ(str[index + 1]) || (str[index + 1] == '!')) || ((str[index + 1] == '/') || (str[index + 1] == '?'))))
                    {
                        return true;
                    }
                }

                else if (str[index + 1] == '#')
                {
                    return true;
                }

                startIndex = index + 1;
            }
        }

        private static bool IsAtoZ(char c)
        {
            return (((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')));
        }
    }
}