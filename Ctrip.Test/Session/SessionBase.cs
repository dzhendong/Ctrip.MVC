using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ctrip.Test.Session
{
    public abstract class SessionBase
    {
        #region Field
        private HttpContextBase context;

        public const string SessionName = "__sessionName__";

        /// <summary>
        /// 生成一个新ID
        /// </summary>
        /// <returns></returns>
        private string NewGuid()
        {
            return SessionName + Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 当前SessionId
        /// </summary>
        public string SessionId { get; set; }

        public HttpContextBase CurrentContext
        {
            get
            {
                return context;
            }
        }
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_context"></param>
        public SessionBase(HttpContextBase _context)
        {
            var context = _context;
            var cookie = context.Request.Cookies.Get(SessionName);
            if (cookie == null || string.IsNullOrEmpty(cookie.Value))
            {
                SessionId = NewGuid();
                context.Response.Cookies.Add(new HttpCookie(SessionName, SessionId));
                context.Request.Cookies.Add(new HttpCookie(SessionName, SessionId));
            }
            else
            {
                SessionId = cookie.Value;
            }
        }
        #endregion
    }
}