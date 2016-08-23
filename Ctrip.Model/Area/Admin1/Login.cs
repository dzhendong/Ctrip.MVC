using Ctrip.Resources;
using System.ComponentModel.DataAnnotations;

namespace Ctrip.Model
{
    public class LoginModel
    {
        [Display(ResourceType = typeof(UI), Name = "Name")]
        [RequiredValidator2("Validation", "MandatoryField", "User Name")]
        [RequiredValidator2("Validation", "MandatoryField", "用户名", Culture = "zh-CN")]
        public string UserName { get; set; }

        [RequiredValidator2("Validation", "MandatoryField", "Password", RuleName = "Production")]
        [RequiredValidator2("Validation", "MandatoryField", "密码", Culture = "zh-CN", RuleName = "Production")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(UI), Name = "Name")]
        public string Password { get; set; }
    }
}