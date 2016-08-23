using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ctrip.MVC.Areas.Test.Controllers
{
    public class Asyn1Controller : AsyncController
    {
        public void ArticleAsync(string narne)
        {
            AsyncManager.OutstandingOperations.Increment();

            Task.Factory.StartNew(() =>
            {
                string path = ControllerContext.HttpContext.Server.MapPath(string.Format(@"\articles\{O}.htrnl" , narne));

                using (StreamReader reader = new StreamReader(path))
                {
                    AsyncManager.Parameters["content"] = reader.ReadToEnd();
                }

                AsyncManager.OutstandingOperations.Decrement();
            }) ;
        }

        public ActionResult ArticleCornpleted(string content)
        {
            return Content(content);
        }
    }
}
