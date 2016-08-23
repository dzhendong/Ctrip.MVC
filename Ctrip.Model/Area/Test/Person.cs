using Ctrip.Resources;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Ctrip.Model
{
    public class Person
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        //[DisplayName("姓名")]        
        //[Required(ErrorMessage = "姓名不能为空")]
        //[StringLength(6, ErrorMessage = "姓名不能超过6个字符")]        
        //[Remote("GetUser", "Person", ErrorMessage = "该姓名已存在")]
        //[Editable(true)]
        //public string Name { get; set; }

         [Display(ResourceType = typeof(UI),Name="Name")]
         [DisplayText(DisplayName = "Name", ResourceType = typeof(UI))]         
         [Required(ErrorMessageResourceType = typeof(Msg), ErrorMessageResourceName = "B_200")]
         [StringLength(6, ErrorMessageResourceType = typeof(Msg), ErrorMessageResourceName = "B_201")]
        public string Name { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>                
        [DisplayName("年龄")]        
        [Required(ErrorMessage = "年龄不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "请输入大于等于1的数")]
        public int Age { get; set; }

        [DisplayName("电子邮件")]
        [RegularExpression(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$",
            ErrorMessage = "请输入正确的Email格式\n示例：abc@123.com")]       
        public string Email { get; set; }

        [Required(ErrorMessage = "邮箱不能为空")]          
        [CompareValues("Email", CompareValues.NotEqualTo, ErrorMessage = "邮箱要相同")]
         public string TEmail { get; set; }  

        [DisplayName("网址")]
        [RegularExpression(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?",
            ErrorMessage = "请输入合法的网址!\n示例：https://abc.a;http://www.abc.dd")]
        public string Httpaddress { get; set; }

        [Required]
        [ValidatePasswordLength]
        [Integer(ErrorMessage = "密码必须是整型")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]       
        public string Password { get; set; }
    }
}