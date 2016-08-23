using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI;
using Ctrip.Model;

namespace Ctrip.MVC.Areas.Test.Controllers
{
    /// <summary>
    /// CRUD
    /// http://localhost:52809/test/person/index
    /// </summary>
    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    public class PersonController : Controller
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>PersonList</returns>
        [NonAction]
        private IEnumerable<Person> GetData()
        {
            IEnumerable<Person> list = new List<Person>
                {
                    new Person() {ID = 1001, Name = "张三", Age = 20},
                    new Person() {ID = 1002, Name = "李四", Age = 22}
                };
            return list;
        }

        /// <summary>
        /// 用于显示添加人员成功
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>        
        public string Success(Person person)
        {
            return "删除用户成功！姓名：" + person.Name;
        }

        public ActionResult Index()
        {
            return View(GetData());
        }

        public ActionResult Details(int id)
        {
            foreach (var person in GetData())
            {
                if (person.ID.Equals(id))
                {
                    return View(person);
                }
            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            { 
            
            }

            try
            {
                //操作数据的代码
                return RedirectToAction("Success", person);
            }
            catch
            {
                return View();
            }
        }        

        public ActionResult Edit(int p1,string p2)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, Person person)
        {
            try
            {
                // 数据库操作代码
                return RedirectToAction("Success", person);
            }
            catch
            {
                return View();
            }
        }        

        public ActionResult Delete(int id)
        {
            foreach (var person in GetData())
            {
                if (person.ID.Equals(id))
                {
                    return View(person);
                }
            }
            return View();
        }        

        [HttpPost]
        public string Delete(int id, Person person)
        {
            return "已删除编号为" + id + "的人员";
        }
        
        [HttpGet]
        public JsonResult GetUser(string name)
        {
            JsonResult a = Json(name != "aa", JsonRequestBehavior.AllowGet);
            return a;
        }
    }
}
