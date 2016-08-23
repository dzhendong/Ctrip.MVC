using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Ctrip.Model
{
    /// <summary>
    /// 3-让数据类型实现IValidatableObject接口
    /// </summary>
    public class Person11 : IValidatableObject
    {
        [DisplayName("姓名")]
        public string Name { get; set; }

        [DisplayName("性别")]
        public string Gender { get; set; }

        [DisplayName("年龄")]
        public int? Age { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            Person person = validationContext.ObjectInstance as Person;
            if (null == person)
            {
                yield break;
            }
            if (string.IsNullOrEmpty(person.Name))
            {
                yield return new ValidationResult("'Name'是必需字段", new string[] { "Name" });
            }
            
            if (null == person.Age)
            {
                yield return new ValidationResult("'Age'是必需字段", new string[] { "Age" });
            }
            else if (person.Age > 25 || person.Age < 18)
            {
                yield return new ValidationResult("'Age'必须在18到25周岁之间", new string[] { "Age" });
            }
        }
    }
}