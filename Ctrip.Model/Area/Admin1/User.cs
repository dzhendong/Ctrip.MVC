using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ctrip.Model
{
    public class User
    {
        public int ID { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "姓名不能为空")]
        public string UserName { get; set; }

        [DisplayName("密码")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }

        public RolesM Roles { get; set; }

        [UIHint("Enum")]
        public RolesM Role { get; set; }
    }

    public class OperationData
    {
        [DisplayName("操作数1")]
        public double Operandl { get; set; }

        [DisplayName("操作数2")]
        public double Operand2 { get; set; }

        [DisplayName("操作符")]
        public string Operator { get; set; }

        [DisplayName("运算结果")]
        public double Result { get; set; }
    }
}