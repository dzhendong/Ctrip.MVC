using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// [Range(int.MinValue, 160, "LessThan", "Weight", 160)]
    /// [Range(18,int.MaxValue,"GreaterThan","Age",18)]
    /// public int Age { get; set; }
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class Range2Attribute : ValidatorBase2Attribute
    {
        private System.ComponentModel.DataAnnotations.RangeAttribute innerRangeAttribute;

        public Range2Attribute(string messageCategory,double minimum, double maximum, string messageId, params object[] args) :
            base(messageCategory, messageId, args)
        {
            innerRangeAttribute = new System.ComponentModel.DataAnnotations.RangeAttribute(minimum, maximum);
        }

        public Range2Attribute(string messageCategory,int minimum, int maximum, string messageId, params object[] args) :
            base(messageCategory, messageId, args)
        {
            innerRangeAttribute = new System.ComponentModel.DataAnnotations.RangeAttribute(minimum, maximum);
        }

        public Range2Attribute(string messageCategory, Type type, string minimum, string maximum, string messageId, params object[] args) :
            base(messageCategory, messageId, args)
        {
            innerRangeAttribute = new System.ComponentModel.DataAnnotations.RangeAttribute(type, minimum, maximum);
        }

        public override bool IsValid(object value)
        {
            return innerRangeAttribute.IsValid(value);
        }

        public override IEnumerable<System.Web.Mvc.ModelClientValidationRule> GetClientValidationRules(System.Web.Mvc.ModelMetadata metadata, System.Web.Mvc.ControllerContext context)
        {
            return null;
            //return new ModelClientValidationRangeRule[] { new ModelClientValidationRangeRule(this.ErrorMessageString) };
        }
    }
}
