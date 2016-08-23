using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// 客户端和服务器端验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AgeRangeAttribute : RangeAttribute, IClientValidatable
    {
        public AgeRangeAttribute(int minnum, int maxnum)
        :base(minnum, maxnum)
        {    
        }

        public override bool IsValid(object value)
        {
            DateTime? birthDate = value as DateTime?;
            if (null == birthDate)
            {
                return true;
            }

            DateTime age = new DateTime(DateTime.Today.Ticks - birthDate.Value.Ticks);

            return (int) this . Minimum <= age. Year && age. Year <= (int) this . Maximum;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentCulture, this.ErrorMessageString,
                this.Minimum, this.Maximum);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata , ControllerContext context)
        {
            string errorMessage = FormatErrorMessage("");

            var rule = new ModelClientValidationRule
            {
                ErrorMessage = errorMessage,
                ValidationType = "agerange"
            };          

            rule.ValidationParameters.Add("minage",this.Minimum) ;
            rule.ValidationParameters.Add("maxage",this.Maximum);
            yield return rule;
        }
    }
}
