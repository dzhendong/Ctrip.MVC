using System;
using System.Web.Mvc;
using Ctrip.Test;
using System.Web.Routing;

namespace Ctrip.MVC.Areas.Admin.Controllers
{
    /// <summary>
    /// 自定义
    /// http://localhost:52809/Admin/Customer/Exception1/50   
    /// </summary>
    public class FilterController : Controller
    {
        [Authorize]
        [CustomAuthorize1(true)]          
        public string AuthFilter1()
        {
            return "<br/> This is the Index action on the Home controller";
        }

        [AllowAnonymous]
        public string AuthFilter2()
        {
            return "<br/> This is the Index action on the Home controller";
        }

        //[CustomAuthorize1(true)]   
        //[ProfileResult] 
        //[ProfileAction]
        //[ProfileActionFilter]           
        public string ActionFilter1()
        {
            Test1(50);
            return "<br/> method";
        }

        [RoleActionFilter2]
        public ActionResult ActionFilter2()
        {
            return Content("有权访问");
        }

        [RoleActionFilter1(checkRole = "2")]
        public ActionResult ActionFilter3()
        {
            return Content("有权访问");
        }

        /// <summary>
        /// 使用内置的 Exceptin Filter
        /// <customErrors mode="On" />
        /// [HandleError]
        /// [HandleError(View = "Error1")]             
        /// Shared下面有一个Error.cshtml,如果页面名字不一致,
        /// 可以使用 View 指定
        /// </summary> 
        //[HandleError]
        //[HandleError(View = "Error",Order=-1)]
        [AllowAnonymous]
        public string Exception1(int id)
        {
           return Test1(id);
           //return View("Exception2");
           //return RedirectToAction("Exception2");
           //RedirectToRoute
           //return Redirect(@"http://localhost:1124/admin/Filter/Exception1/500");
        }        

        /// <summary>
        /// 自定义异常过滤       
        /// </summary> 
        //[RangeException]
        public string Exception2(int id)
        {            
            return Test1(id);
            //return View();
        }

        private string Test1(int id)
        {
            if (id > 100)
            {
                return String.Format("The id value is: {0}", id);
            }
            else
            {
                throw new ArgumentOutOfRangeException("id", id, "");
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {            
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //ViewData["B"] = "id";
            //TempData.Add("A", "A");
            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        /// 如果以action标记了HandleError属性
        /// 同时期所在的controller又重写了OnException方法
        /// 最终会怎样处理呢？
        /// 按照mvc中filter的执行顺序
        /// controller重写的方法会被优先执行，不考虑action中的order顺序
        /// 执行完毕之后再执行action标记的filter的方法。
        /// ok，有了这个理论之后，
        /// 再看看之前提到的情况的执行顺序。
        /// 首先执行OnException中的处理方式，这时候filterContext.ExceptionHandled已经被标记为true了，
        /// 再执行HandleError属性的方法时，就不会在被执行了，
        /// 也就是说自定义的错误页白费了，不起作用。
        /// 这是因为内置的HandleError在执行的时候会先判断filterContext.ExceptionHandled是否为true，为true就不执行了
        /// 因此会出现一些很奇怪的bug，明白这个道理就知道如何处理了。
        /// </summary>
        /// <param name="filterContext"></param>
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    RouteValueDictionary a = new RouteValueDictionary(new
        //    {
        //        Controller = "Error",
        //        Action = "Error"
        //    });
                        
        //    filterContext.ExceptionHandled = true;
        //    filterContext.Result = new RedirectToRouteResult("Default", a);            
        //}

        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    // 此处进行异常记录，可以记录到数据库或文本，也可以应用其另日记记录组件。
        //    // 经由过程filterContext.Exception来获取这个异常。
        //    string filePath = ＠"D:\Temp\Exceptions.txt";
        //    StreamWriter sw = System.IO.File.AppendText（filePath）;
        //    sw.Write（filterContext.Exception.Message）;
        //    sw.Close（）;
            
        //    // 履行基类中的OnException
        //    base.OnException（filterContext）;
        //}

        //public ViewResult Index()
        //{
        //    return View("Result", new Result
        //    {
        //        ControllerName = "Customer",
        //        ActionName = "Index"
        //    });
        //}

        //[Local]
        //[ActionName("Index")]
        //public ViewResult LocalIndex()
        //{
        //    return View("Result", new Result
        //    {
        //        ControllerName = "Customer",
        //        ActionName = "LocalIndex"
        //    });
        //}
    }
}
