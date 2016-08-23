using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Ctrip.Model
{
    /// <summary>
    /// 一种Model 类型，多种验证规则
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RangeValidatorAttribute : ValidatorAttribute
    {       
        private RangeAttribute rangeAttribute;

        public RangeValidatorAttribute(int minimum, int maximum)
        {
            rangeAttribute = new RangeAttribute(minimum, maximum);
        }

        public RangeValidatorAttribute(double minimum, double maximum)
        {
            rangeAttribute = new RangeAttribute(minimum, maximum);
        }

        public RangeValidatorAttribute(Type type, string minimum, string maximum)
        {
            rangeAttribute = new RangeAttribute(type, minimum, maximum);
        }

        public override bool IsValid(object value)
        {
            return rangeAttribute.IsValid(value);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                CultureInfo.CurrentCulture, 
                base.ErrorMessageString,
                new object[] { name , rangeAttribute.Minimum,rangeAttribute.Maximum });
        }
    }   
}
