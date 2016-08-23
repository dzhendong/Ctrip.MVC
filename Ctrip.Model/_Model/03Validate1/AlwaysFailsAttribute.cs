using System;
using System.ComponentModel.DataAnnotations;

namespace Ctrip.Model
{
    /// <summary>
    /// CompositeModelValidator 采用的验证行为
    /// </summary>
    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Property)]
    public class AlwaysFailsAttribute : ValidationAttribute
    {
        private object typeId;
        public override bool IsValid(object value)
        {
            return false;
        }

        public override object TypeId
        {
            get { return typeId ?? (typeId = new object()); }
        }
    }
}
