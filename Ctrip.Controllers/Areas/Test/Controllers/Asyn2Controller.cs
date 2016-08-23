using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ctrip.MVC.Areas.Test.Controllers
{
    /// <summary>
    /// 方法Article 返回一个Task<ActionR esult> 对象
    /// 异步文件内容的读取体现在返回的Task 对象中
    /// 对文件内容呈现的回调操作则通过调用该Task 对象的Continue With<ActionResult>方法进行注
    /// 该操作会在异步操作完成之后被自动调用。
    /// </summary>
    public class Asyn2Controller : Controller
    {
        public Task<ActionResult> Article(string name)
        {
           return Task.Factory.StartNew(() =>
           {
                string path = ControllerContext.HttpContext.Server.MapPath(string.Format(@"\articles\{O}.htrnl", name));

                using (StreamReader reader = new StreamReader(path))
                {
                    AsyncManager.Parameters["content"] = reader.ReadToEnd();
                }

                AsyncManager.OutstandingOperations.Decrement();
            }).ContinueWith<ActionResult>(task =>
                {
                    string content = (string)AsyncManager.Parameters["content"];
                    return Content(content);
                });
        }

    }
}