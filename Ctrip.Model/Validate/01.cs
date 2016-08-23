using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// 1-手工验证绑定的参数
    /// 2-ValidationAttribute
    /// </summary>
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult Index(Person person)
        {
            Validate(person);

            if (!ModelState.IsValid)
            {
                return View(person);
            }
            else
            {
                return Content("输入数据通过验证");
            }
        }

        private void Validate(Person person)
        {
            if (string.IsNullOrEmpty(person.Name))
            {
                ModelState.AddModelError("Name", "'Name'是必需字段");
            }

            if (null == person.Age)
            {
                ModelState.AddModelError("Age", "'Age'是必需字段");
            }
            else if (person.Age > 25 || person.Age < 18)
            {
                ModelState.AddModelError("Age", "有效'Age'必须在18到25周岁之间");
            }
        }
    }
}