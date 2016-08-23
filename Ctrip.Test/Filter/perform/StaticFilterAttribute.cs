using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ctrip.Test
{
    /// <summary>
    /// [StaticFilter]
    /// public ActionResult Index() { return View(); }
    /// </summary>
    public class StaticFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //filterContext.HttpContext.Response.Write("OnActionExecuted</br>");  
            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // filterContext.HttpContext.Response.Write("OnActionExecuting</br>");  
            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //filterContext.HttpContext.Response.Write("OnResultExecuted</br>");  

            if (filterContext.HttpContext.Response.StatusCode == 200)
            {
                filterContext.HttpContext.Response.Filter = new StaticFileWriteResponseFilterWrapper(filterContext.HttpContext.Response.Filter, filterContext);
            }
            // filterContext.HttpContext.Response.Charset = "utf8";  
            base.OnResultExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            // filterContext.HttpContext.Response.Write("OnResultExecuting</br>");  
            base.OnResultExecuting(filterContext);
        }
    }

    class StaticFileWriteResponseFilterWrapper : System.IO.Stream
    {
        private Stream inner;
        private FileStream writer;
        private ControllerContext context;
        private int expireSconds;
        private bool filter;
        private string tempPath, path;

        public StaticFileWriteResponseFilterWrapper(System.IO.Stream s, ControllerContext context, int expireSeconds = 600)
        {
            this.filter = false;
            this.inner = s;
            this.context = context;
            this.expireSconds = expireSeconds;
            this.EnsureStaticFile();
        }

        void EnsureStaticFile()
        {
            this.path = this.context.HttpContext.Server.MapPath(HttpContext.Current.Request.Path);

            if (!Path.HasExtension(path))
            {
                return;
            }
            if (!".html".Equals(Path.GetExtension(HttpContext.Current.Request.Path)))
            {
                return;
            }

            if (File.Exists(path))
            {
                var delay = DateTime.UtcNow - File.GetCreationTimeUtc(path);
                if (delay.TotalSeconds <= this.expireSconds)
                {
                    return;
                }
                File.Delete(path);
            }
            else
            {
                var dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {

                    try
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                    }
                    catch
                    { 
                    }
                }
            }
            this.filter = true;

            this.tempPath = this.path + "_" + DateTime.Now.Ticks;

            try
            {
                writer = new FileStream(tempPath, FileMode.Create, FileAccess.Write);
            }
            catch
            {
                this.filter = false;
            }
        }

        public override bool CanRead
        {
            get { return inner.CanRead; }
        }

        public override bool CanSeek
        {
            get { return inner.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return inner.CanWrite; }
        }

        public override void Flush()
        {
            inner.Flush();
        }

        public override long Length
        {
            get { return inner.Length; }
        }

        public override long Position
        {
            get
            {
                return inner.Position;
            }
            set
            {
                inner.Position = value;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return inner.Read(buffer, offset, count);
        }

        public override long Seek(long offset, System.IO.SeekOrigin origin)
        {
            return inner.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            inner.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            try
            {
                inner.Write(buffer, offset, count);
            }
            catch
            {
            }

            try
            {
                this.writer.Write(buffer, offset, count);
            }
            catch 
            {

            }
        }

        protected override void Dispose(bool disposing)
        {
            if (this.filter)
            {
                try
                {
                    if (this.writer != null)
                    {
                        this.writer.Dispose();
                        this.writer = null;
                    }

                    File.Delete(this.path);
                    File.Move(this.tempPath, this.path);

                    #region 生成文件日志

                    #endregion
                }
                catch
                { 
                }
            }
            base.Dispose(disposing);
        }
    }

    public class SSOFilterAttribute : ActionFilterAttribute
    {
        public string Message { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if (!filterContext.HttpContext.Request.Url.ToString().ToLower().StartsWith("http://www."))  
            //{  
            //    filterContext.HttpContext.Response.Redirect(filterContext.HttpContext.Request.Url.ToString().Replace("http://", "http://www."));  
            //    return;  
            //}  

            //var sso_cookies = filterContext.HttpContext.Request.Cookies["sso_token"];
            //if (sso_cookies == null || sso_cookies.Value == null || sso_cookies.Value.Equals(""))
            //{
            //    //清空所有cookies  
            //    filterContext.HttpContext.Request.Cookies.Clear();
            //    //如果不存在token，跳转到验证站点进行验证；  
            //    filterContext.HttpContext.Response.Redirect("http://jump.yuan.cn/Home/index/?type=mvc&backurl=" + filterContext.HttpContext.Request.Url.ToString());
            //    return;
            //}
            //else
            //{
            //    //如过存在token，检测登录状态  
            //    var userid = ServiceLocator.Create<ISSOService>().CheckUser(sso_cookies.Value);
            //    if (userid != null)
            //    {
            //        if (filterContext.HttpContext.Session["userid"] != userid || filterContext.HttpContext.Session["userid"] == null || filterContext.HttpContext.Session["isVip"] == null || filterContext.HttpContext.Session["username"] == null)
            //        {
            //            //用户id  
            //            filterContext.HttpContext.Session["userid"] = userid;
            //            //取得用户对象  
            //            UserDto userInfo = ServiceLocator.Create<IUserService>().GetUser(userid);
            //            filterContext.HttpContext.Session["isVip"] = DataAccess.isVip(userid);
            //            filterContext.HttpContext.Session["username"] = !String.IsNullOrEmpty(userInfo.NickName) ? userInfo.NickName : userInfo.Name;
            //        }
            //    }
            //    else
            //    {
            //        filterContext.HttpContext.Session.Clear();
            //    }
            //}
        }

        public override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
    }
}