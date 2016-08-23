using Ctrip.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Ctrip.MVC.Areas.Admin.Controllers
{
    public class AjaxController : Controller
    {
        private IEnumerable<Person1> GetData1()
        {
            IEnumerable<Person1> list = new List<Person1>
            {
                new Person1 {ID = "ZhangSan", Name = "张三", Role = Role.Admin}, 
                new Person1 {ID = "LiSi", Name = "李四", Role = Role.User}, 
                new Person1 {ID = "WangWu", Name = "王五", Role = Role.User}, 
                new Person1 {ID = "MaLiu", Name = "马六", Role = Role.Guest}
            };
            
            return list;
        }

        public ActionResult Pop1()
        {
            return View();
        }

        public ActionResult Pop2()
        {
            return View();
        }

        public ActionResult Pop3()
        {
            return View();
        }

        public ActionResult Pop4()
        {
            return View();
        }

        public ActionResult Pop5()
        {
            return View();
        }

        private IEnumerable<Person1> GetData2()
        {
            IEnumerable<Person1> list = new List<Person1>
            {
                new Person1 {ID = "ZhangSan", Name = "张三", Role = Role.Admin}                
            };
            return list;
        }

        private IEnumerable<Person1> GetData3()
        {
            IEnumerable<Person1> list = new List<Person1>
            {
                new Person1 {ID = "MaLiu", Name = "马六", Role = Role.Guest}                
            };
            return list;
        }

        public PartialViewResult AjaxData(string selectedRole = "All")
        {
            if (selectedRole == "All")
            {
                return PartialView(GetData1());
            }
            if (selectedRole == "Admin")
            {
                return PartialView(GetData2());
            }

            return PartialView(GetData3());
        }

        public ActionResult Ajax1(string selectedRole = "All")
        {
            return View((object)selectedRole);
        }

        public ActionResult Ajax2()
        {
            return View();
        }

        public ActionResult Ajax21(Person1 person)
        {
            string a = person.Name;
            return Json(
               new { msg = "Datetime from server：" + DateTime.Now.ToString("HH:mm:ss") }
           );
        }  
    }
}
