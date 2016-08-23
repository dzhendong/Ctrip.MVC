using System;
using System.Collections.Specialized;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace XSS.Web.Common.Filters
{
    /// <summary>
    /// public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    /// filters.Add(new HandleAjaxErrorAttribute());
    /// </summary>
    public class HandleAjaxErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            if (new HttpException(null, filterContext.Exception).GetHttpCode() != 500)
            {
                return;
            }

            if (!ExceptionType.IsInstanceOfType(filterContext.Exception))
            {
                return;
            }

            if (IsAjaxRequest(filterContext.HttpContext.Request.Headers))
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        filterContext.Exception.Message
                    }
                };

                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            }
            else
            {
                base.OnException(filterContext);
            }
        }

        private static bool IsAjaxRequest(NameValueCollection headers)
        {
            return headers["X-Requested-With"] != null && headers["X-Requested-With"].Equals("XMLHttpRequest", StringComparison.OrdinalIgnoreCase);
        }
    }
}