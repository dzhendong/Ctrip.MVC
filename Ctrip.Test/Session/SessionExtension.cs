using System.Web;
using System.Web.Mvc;

namespace Ctrip.Test.Session
{
    public static class SessionExtension
    {
        #region Extension
        public static RedisSession SessionExt(this HttpContext context)
        {
            HttpContextWrapper _context = new HttpContextWrapper(context);
            return new RedisSession(_context);
        }

        public static RedisSession SessionExt(this HttpContextBase context)
        {
            return new RedisSession(context);
        }

        public static RedisSession SessionExt(this Controller controller)
        {
            return new RedisSession(controller.HttpContext);
        }

        #endregion
    }
}