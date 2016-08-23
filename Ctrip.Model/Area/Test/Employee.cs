using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ctrip.Model
{
    public class Employee
    {
        public int EmployeeNO;

        public string EmployName;

        public string Sex;

        public DateTime Birthday;

        public int Marital;
    }

    /// <summary>
    /// ViewMode排序
    /// </summary>
    public class Employee1
    {
        public IEnumerable<Employee> Employees { get; set; }

        /// <summary>
        /// 数据库列的名称，通过它对数据进行排序
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// 一个布尔值，指示是否升序排序数据
        /// </summary>
        public bool SortAscending { get; set; }

        /// <summary>
        /// 一个只读属性，返回一个排序的值的字符串SortBy SortAscending属性
        /// </summary>
        public string SortExpression
        {
            get
            {
                return this.SortAscending ? this.SortBy + " desc" : this.SortBy + " asc";
            }
        }
    }

    /// <summary>
    /// ViewMode分页
    /// </summary>
    public class Employee2
    {
        public Employee2()
        {
            //默认值 每页显示5条记录 从第1页开始
            this.PageSize = 5;
            this.CurrentPageIndex = 1;
        }

        public IEnumerable<Employee> Employees { get; set; }

        /// <summary>
        /// 当前页的索引
        /// </summary>
        public int CurrentPageIndex { get; set; }

        /// <summary>
        /// 每页显示的记录条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总记录条数
        /// </summary>
        public int TotalRecordCount { get; set; }

        /// <summary>
        /// 分页总数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (TotalRecordCount % PageSize == 0)
                {
                    return TotalRecordCount / PageSize;
                }
                else
                {
                    return TotalRecordCount / PageSize + 1;
                }
            }
        }
    }

    /// <summary>
    /// ViewMode过滤
    /// </summary>
    public class Employee3
    {
        /// <summary>
        /// 查询条件：查询编号
        /// </summary>
        public string EmployeeNO { get; set; }

        /// <summary>
        /// 查询条件：是否婚配
        /// </summary>
        public bool IsMarital { get; set; }

        /// <summary>
        /// 查询条件：种族编号
        /// 用于绑定DepartmentList
        /// </summary>
        public int? DepartmentID { get; set; }

        /// <summary>
        /// 绑定数据库中的种族集合
        /// </summary>
        public IEnumerable<System.Web.Mvc.SelectListItem> DepartmentList { get; set; }

        /// <summary>
        /// 绑定数据库中的种族集合
        /// </summary>
        public IEnumerable<Employee> Employees { get; set; }
    }

     /// <summary>
    /// 通过定制Model 元数据和自定义模板
    /// 实现预定义列表的呈现
    /// </summary>
    public class Employee4
    {
        [DisplayName(" 姓名")]
        public string Name { get; set; }

        [RadioButtonList("Gender")]
        [DisplayName(" 性别")]
        public string Gender { get; set; }

        [DropdownList("Education")]
        [DisplayName(" 学历")]
        public string Education { get; set; }

        [ListBox("Department")]
        [DisplayName("所在部门")]
        public IEnumerable<string> Departments { get; set; }

        [CheckBoxList("Skill")]
        [DisplayName("擅长技能")]
        public IEnumerable<string> Skills { get; set; }
    }

    /// <summary>
    /// 应用多个规则
    /// </summary>
    public class Employee5
    { 
        public string Name { get; set; }
        public string Grade { get; set; }
        
        [RangeIf("Grade", "G7", 2000, 3000)]
        [RangeIf("Grade", "G8", 3000, 4000)]
        [RangeIf("Grade", "G9", 4000, 5000)]
        public decimal Salary { get; set; }     
    }

    public class Employee6
    {
        public string Name { get; set; }

        //一种Model 类型，多种验证规则
        //三个自定义的RangeValidatorAttribute
        [RangeValidator(10, 20, RuleName = "Rulel", ErrorMessage = "{O} 必须在{1} 和{2} 之间! ")]
        [RangeValidator(20, 30, RuleName = "Rule2", ErrorMessage = "{O} 必须在{1} 和{2} 之间! ")]
        [RangeValidator(30, 40, RuleName = "Rule3", ErrorMessage = "{O} 必须在{1} 和{2} 之间! ")]
        public int Age { get; set; }
    }


    /// <summary>
    /// 错误验证
    /// </summary>   
    [AlwaysFails(ErrorMessage = "Contact")]
    public class Contact : IDataErrorInfo
    {
        [AlwaysFails(ErrorMessage = "Contact.Name")]
        public string Name { get; set; }

        [AlwaysFails(ErrorMessage = "Contact.PhoneNo")]
        public string PhoneNo { get; set; }

        [AlwaysFails(ErrorMessage = "Contact.EmailAddress")]
        public string EmailAddress { get; set; }

        [AlwaysFails(ErrorMessage = "Contact.Address")]
        public Address Address { get; set; }

        public string Error
        {
            get { return "无效联系人! "; }
        }

        public string this[string columnName]
        {
            get 
            {
                switch (columnName)
                {
                    case "Name":
                         return "姓名是必需的! ";
                    case "PhoneNo":
                         return "电话号码格式牵制吴! ";
                    case "EmailAddress":
                        return "无效的电子邮箱地址! ";
                    default:
                        return null;
                }
            }
        }
    }

    [DisplayColumn("DisplayText")]
    [AlwaysFails(ErrorMessage = "Address")]
    public class Address
    {
        [AlwaysFails(ErrorMessage = "Address.Province")]
        public string Province { get; set; }

        [AlwaysFails(ErrorMessage = "Address.District")]
        public string District { get; set; }

        [AlwaysFails(ErrorMessage = "Address.City")]
        public string City { get; set; }

        [AlwaysFails(ErrorMessage = "Address.Street")]
        public string Street { get; set; }

        [AlwaysFails(ErrorMessage = "Address.DisplayText")]
        public string DisplayText { get; set; }
    }
}