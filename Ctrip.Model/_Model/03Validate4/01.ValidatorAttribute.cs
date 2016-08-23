using System;
using System.ComponentModel.DataAnnotations;

namespace Ctrip.Model
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public abstract class ValidatorAttribute : ValidationAttribute
    {
        private object typeid;
        public string RuleName { get; set; }

        public override object TypeId
        {
            get
            {
                return typeid ?? (typeid = new object());
            }
        }
    }
}
