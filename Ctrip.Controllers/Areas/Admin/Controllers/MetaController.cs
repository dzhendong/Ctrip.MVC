using System.Web.Mvc;
using Ctrip.Model;

namespace Ctrip.MVC.Areas.Admin.Controllers
{
    public class MetaController : Controller
    {
        /// <summary>
        /// 元数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Index1()
        {
            ModelMetadataInfo a = new ModelMetadataInfo(typeof(DemoModel),
                //metadata => metadata.TemplateHint,
                //metadata => metadata.HideSurroundingHtml
                metadata => metadata.DisplayName,
                metadata => metadata.Description,
                metadata => metadata.ShortDisplayName,
                metadata => metadata.Watermark,
                metadata => metadata.Order
                );
            return View(a);
        }

        /// <summary>
        /// 自定义模板
        /// </summary>
        /// <returns></returns>
        public ActionResult Index2()
        {
            Employee4 employee = new Employee4
            {
                Name = "张三",
                Gender = "M",
                Education = "M",
                Departments = new string[] { "HR", "AD" },
                Skills = new string[] { "CSharp", "AdoNet" }
            };
            return View(employee);
        }

        [HttpPost]
        public ActionResult Index2(Employee4 employee)
        {
            return View(employee);
        }
    }
}
