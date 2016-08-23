using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Ctrip.Model;
using Ctrip.Test;
using Newtonsoft.Json;

namespace Ctrip.MVC.Areas.Admin.Controllers
{
    /// <summary>
    /// Action Result返回类型
    /// http://localhost:52809/Admin/ActionResult/Index
    /// </summary>
    public class ActionResultController : Controller
    {        
        public ActionResult Index()
        {
            string a = Session["A"].ToString();
            return View();
        }

        public ActionResult ViewResult()
        {
            return View();
        }
             
        public ActionResult Partial1()
        {
            return Partial2("Test");
        }

        public ActionResult Partial2(string name)
        {
            PartialM2 model = new PartialM2();
            model.dt = DateTime.Now;
            model.Name = name;

            return PartialView("Partial2", model);
        }

        public ActionResult Content1()
        {
            return Content("Hi, 我是ContentResult结果");
        }

        public JsonResult Json1(Person1 users)
        {
            return Json(
                new { msg = "Datetime from server：" + DateTime.Now.ToString("HH:mm:ss") }
            );
        }

        public ActionResult Json2(List<Person1> users)
        {
            ViewBag.DayOfWeek = DateTime.Now.DayOfWeek;
          
            var jsonObj = new
            {
                Id = 1,
                Name = "小铭",                
                Sex = "男",
                Like = "足球"
            };
            return Json(jsonObj, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Json31(List<Person1> users)
        {
            return Json(
                  new { msg = "Datetime from server：" + DateTime.Now.ToString("HH:mm:ss") }
              );
        }

        public ActionResult Json32(string json)
        {
            List<Person1> users = JsonConvert.DeserializeObject<List<Person1>>(json);

            return Json(
                     new { msg = "Datetime from server：" + DateTime.Now.ToString("HH:mm:ss") }
                 );
        }

        public ActionResult Json41()
        {
            StreamReader reader = new StreamReader(Request.InputStream);
            string bodyText = reader.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Person1> users = js.Deserialize<List<Person1>>(bodyText);

            return Json(
                  new { msg = "Datetime from server：" + DateTime.Now.ToString("HH:mm:ss") }
              );
        }

        public ActionResult Json42([ModelBinder(typeof(JsonBinder<List<Person1>>))] List<Person1> users)
        {
            return Json(
                     new { msg = "Datetime from server：" + DateTime.Now.ToString("HH:mm:ss") }
                 );
        }
       
        public ActionResult JavaScript1()
        {
            string js = "alert(\"Hi, I'm JavaScript.\");";
            return JavaScript(js);
        }

        public ActionResult JavaScript2()
        {
            string js = "alert(\"Hi, I'm JavaScript.\");";
            return JavaScript(js);
        }

        public ActionResult FileResult()
        {
            var imgPath = Server.MapPath("~/demo.jpg");
            return File(imgPath, "application/x-jpg", "demo.jpg");
        }

        /// <summary>
        /// 空结果当然是空白了!
        /// 至于你信不信, 我反正信了
        /// </summary>        
        public ActionResult EmptyResult()
        {          
            return new EmptyResult();
        }        

        /// <summary>
        /// 302重定向
        /// 暂时重定向
        /// </summary>        
        public ActionResult RedirectResult()
        {             
            return Redirect("http://www.asp.net");
        }

        /// <summary>
        /// 301重定向
        /// 永久重定向
        /// </summary>
        public ActionResult RedirectPermanent()
        {
            return RedirectPermanent("http://www.asp.net");
        }

        /// <summary>
        /// 根据Route规则重定向页面
        /// 这告诉MVC查找指定路由的路由表中定义是全球性的。
        /// asax然后重定向到该控制器/动作中定义。
        /// 这也使一个新请求像RedirectToAction()。
        /// </summary>        
        public ActionResult RedirectToRouteResult()
        {
            return RedirectToRoute(new
            {
                controller = "Person",
                action = "Index"
            });
        }

        /// <summary>
        /// RedirectToAction构造一个url重定向到一个特定的行动/控制器在应用程序中
        /// 使用路由表来生成正确的url。
        /// RedirectToAction导致浏览器应用程序中获得302重定向和给你一个更简单的方法来处理你的路由表。
        /// </summary>
        /// <returns></returns>
        public ActionResult RedirectToActionResult()
        {
            return RedirectToAction("Index", "Person");
        }
        
        /// <summary>
        /// 404
        /// </summary>        
        public ActionResult HttpNotFoundResult()
        {
            return HttpNotFound("Page Not Found");
        }

        /// <summary>
        /// HTTP 错误 401.1
        /// - 未经授权：访问由于凭据无效被拒绝。
        /// --未验证时,跳转到Logon
        /// </summary>        
        public ActionResult HttpUnauthorizedResult()
        {            
            return new HttpUnauthorizedResult();
        }

        /// <summary>
        /// 禁止直接访问的ChildAction
        /// </summary>        
        [ChildActionOnly]        
        public ActionResult ChildAction()
        {
            return Partial2(DateTime.Now.ToString());
        }

        /// <summary>
        /// 正确使用ChildAction
        /// </summary>        
        public ActionResult UsingChildAction()
        {
            return View();
        }
    }
}
