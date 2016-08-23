using System;
using System.Web;
using System.Web.Mvc;

namespace Ctrip.MVC.Areas.Test.Controllers
{
    /// <summary>
    /// 文件上传和表单
    /// </summary>
    public class File2Controller : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveImage1(HttpPostedFileWrapper imageFile, string tbAddTitle, string tbAddNote, string chkAddSort)
        {
            return Json(
              new { msg = "Datetime from server：" + DateTime.Now.ToString("HH:mm:ss") }
          );
        }

        public JsonResult SaveImage2(string tbAddTitle, string tbAddNote, string chkAddSort)
        {
            HttpPostedFileBase file = Request.Files[0];

            return Json(
              new { msg = "Datetime from server：" + DateTime.Now.ToString("HH:mm:ss") }
            );
        }

        /// <summary>
        /// 绑定有个顺序
        /// </summary>      
        public JsonResult SaveImage(FormCollection form)
        {
            return Json(
              new { msg = "Datetime from server：" + DateTime.Now.ToString("HH:mm:ss") }
            );
        }

        public ActionResult Form1()
        {
            return View();
        }

        public ActionResult Form2()
        {
            return View();
        }
    }
}
