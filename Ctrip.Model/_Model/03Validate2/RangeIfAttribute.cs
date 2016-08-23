using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Ctrip.Model
{
    /// <summary>
    /// public string Grade { geti seti }
    /// [Rangelf("Grade" , "G7" , 2000 , 3000)]
    /// [Rangelf("Grade" , "G8" , 3000 , 4000)]
    /// [Rangelf("Grade" , "G9" , 4000 , 5000)]
    /// public decimal Salary { geti seti }
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RangeIfAttribute : RangeAttribute
    {
        public string Property { get; set; }
        public string Value { get; set; }

        public RangeIfAttribute(string property, string value, double minimum, double maximum)
            : base(minimum, maximum)
        {
            this.Property = property;
            this.Value = value ?? "";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo property = validationContext.ObjectType.GetProperty(this.Property);
            object propertyValue = property.GetValue(validationContext.ObjectInstance, null);

            propertyValue = propertyValue ?? "";

            if (propertyValue.ToString() != this.Value)
            {
                return ValidationResult.Success;
            }

            return base.IsValid(value, validationContext);
        }

        private object typeid;

        /// <summary>
        /// MVC 在根据ValidationAttribute 特性创建相应的DataAnn otationsModel
        /// Validator 对象的时候会根据该TypeId 属性值进行分组
        /// 同一组的ValidationAttribute 只会选择第一个
        /// 这就意味着对于多个应用到相同目标元素的同类ValidationAttribute上
        /// 有且只有一个是有效的。
        /// 只需要通过重写TypeId 属性使每个ValidationAttribute 具有不同的属性值就可以了
        /// </summary>
        public override object TypeId
        { 
            get
            { 
                return typeid?? (typeid= new object());
            }
        }
    }
}
