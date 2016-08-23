using System.Web.Mvc;
using Ctrip.Component;
using System.IO;

namespace Ctrip.Controllers
{
    public class SharedController : Controller
    {
        /// <summary>
        /// 生成验证码图像对象
        /// </summary>
        /// <returns></returns>
        public ActionResult Checkcode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(4);

            Session["ValidateCode"] = code;

            byte[] bytes = vCode.CreateValidateGraphic(code);

            return File(bytes, @"image/jpeg");
        }

        /// <summary>
        /// 得到当前验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCurrentValidateCode()
        {
            return Content(Session["ValidateCode"].ToString());
        }
    }
}
