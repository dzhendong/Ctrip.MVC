using System;

namespace CSRF.Web.Common
{
    /// <summary>
    /// public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    /// filters.Add(new HandleErrorAttribute());
    /// filters.Add(new ExtendedAuthorizeAttribute());
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public class BypassCsrfValidationAttribute : Attribute
    {

    }
}