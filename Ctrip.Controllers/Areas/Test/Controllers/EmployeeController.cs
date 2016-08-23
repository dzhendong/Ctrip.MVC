using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ctrip.Model;

namespace Ctrip.MVC.Areas.Test.Controllers
{
    /// <summary>
    /// 表格
    /// http://localhost:52809/Test/Employee/Paged
    /// </summary>
    public class EmployeeController : Controller
    {
        [NonAction]
        private IEnumerable<Employee> GetData1()
        {
            IEnumerable<Employee> list = new List<Employee>
            {
                new Employee() {EmployeeNO = 1001, EmployName = "李1",Sex="男",Birthday=DateTime.Now, Marital = 1},
                new Employee() {EmployeeNO = 1002, EmployName = "李2",Sex="男",Birthday=DateTime.Now, Marital = 0},
                new Employee() {EmployeeNO = 1002, EmployName = "李3",Sex="男",Birthday=DateTime.Now, Marital = 0},
                new Employee() {EmployeeNO = 1002, EmployName = "李4",Sex="男",Birthday=DateTime.Now, Marital = 0},
                new Employee() {EmployeeNO = 1002, EmployName = "李5",Sex="男",Birthday=DateTime.Now, Marital = 0},
                new Employee() {EmployeeNO = 1002, EmployName = "李6",Sex="男",Birthday=DateTime.Now, Marital = 0},
                new Employee() {EmployeeNO = 1002, EmployName = "李7",Sex="男",Birthday=DateTime.Now, Marital = 0},
                new Employee() {EmployeeNO = 1002, EmployName = "李8",Sex="男",Birthday=DateTime.Now, Marital = 0}
            };
            return list;
        }

        [NonAction]
        private IEnumerable<Employee> GetData2()
        {
            IEnumerable<Employee> list = new List<Employee>
            {
                new Employee() {EmployeeNO = 1001, EmployName = "李8",Sex="男",Birthday=DateTime.Now, Marital = 1},
                new Employee() {EmployeeNO = 1002, EmployName = "李7",Sex="男",Birthday=DateTime.Now, Marital = 0},
                new Employee() {EmployeeNO = 1002, EmployName = "李6",Sex="男",Birthday=DateTime.Now, Marital = 0},
                new Employee() {EmployeeNO = 1002, EmployName = "李5",Sex="男",Birthday=DateTime.Now, Marital = 0},
                new Employee() {EmployeeNO = 1002, EmployName = "李4",Sex="男",Birthday=DateTime.Now, Marital = 0},
                new Employee() {EmployeeNO = 1002, EmployName = "李3",Sex="男",Birthday=DateTime.Now, Marital = 0},
                new Employee() {EmployeeNO = 1002, EmployName = "李2",Sex="男",Birthday=DateTime.Now, Marital = 0},
                new Employee() {EmployeeNO = 1002, EmployName = "李1",Sex="男",Birthday=DateTime.Now, Marital = 0}
            };
            return list;
        }

        [NonAction]
        private IEnumerable<Employee> GetData3()
        {
            IEnumerable<Employee> list = new List<Employee>
            {
                new Employee() {EmployeeNO = 1001, EmployName = "李1",Sex="男",Birthday=DateTime.Now, Marital = 1},
                new Employee() {EmployeeNO = 1002, EmployName = "李2",Sex="男",Birthday=DateTime.Now, Marital = 0},
                new Employee() {EmployeeNO = 1002, EmployName = "李3",Sex="男",Birthday=DateTime.Now, Marital = 0},
                new Employee() {EmployeeNO = 1002, EmployName = "李4",Sex="男",Birthday=DateTime.Now, Marital = 0},
                new Employee() {EmployeeNO = 1002, EmployName = "李5",Sex="男",Birthday=DateTime.Now, Marital = 0}
            };
            return list;
        }

        [NonAction]
        private IEnumerable<Employee> GetData4()
        {
            IEnumerable<Employee> list = new List<Employee>
            {                
                new Employee() {EmployeeNO = 1002, EmployName = "李6",Sex="男",Birthday=DateTime.Now, Marital = 0},
                new Employee() {EmployeeNO = 1002, EmployName = "李7",Sex="男",Birthday=DateTime.Now, Marital = 0},
                new Employee() {EmployeeNO = 1002, EmployName = "李8",Sex="男",Birthday=DateTime.Now, Marital = 0}
            };
            return list;
        }       

        public ActionResult Index()
        {
            return View(GetData1());
        }

        public ActionResult Sortable(bool? ascending, string sortBy = "EmployeeNO")
        {
            var model = new Employee1()
            {
                SortBy = sortBy,
                SortAscending = ascending.GetValueOrDefault(),
                Employees = (ascending == true) ? GetData1() : GetData2()
            };

            return View(model);
        }

        public ActionResult Paged(int page = 1, int pageSize = 5)
        {          
            var model = new Employee2
            {
                CurrentPageIndex = page,
                PageSize = pageSize,
                //确定记录总数（才能计算出PageCount页数）
                TotalRecordCount = 8
            };

            // 获取当前页的信息
            model.Employees = (page == 1) ? GetData3() : GetData4();

            return View(model);
        }

        public ActionResult Filterable(int? employeeNO, int? departmentID, bool? isMarital = null)
        {
            SelectListItem item = new SelectListItem();               
            item.Text = "a";
            item.Value = "a";

            List<SelectListItem> list = new List<SelectListItem>(1);
            list.Add( item );

            var model = new Employee3()
            {                
                EmployeeNO = employeeNO.ToString(),
                IsMarital = isMarital.HasValue ? isMarital.Value : false,
                DepartmentList = list                
            };

            IEnumerable<Employee> filteredResults;
            if (employeeNO != null)
            {
                filteredResults = new List<Employee>
                {                                
                    new Employee() {EmployeeNO = 2001, EmployName = "李8",Sex="男",Birthday=DateTime.Now, Marital = 0}
                };
            }
            else
            {
                filteredResults = GetData3();
            }
          
            model.Employees = filteredResults;

            return View(model);
        }       
    }
}