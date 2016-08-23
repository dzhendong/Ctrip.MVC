using System;

namespace Ctrip.Model
{
    /// <summary>
    /// 验证规则
    /// </summary>
    [AttributeUsage( AttributeTargets.Class | AttributeTargets.Method)]
    public class ValidationRuleAttribute:Attribute
    {
        public string RuleName { get; private set; }

        public ValidationRuleAttribute(string ruleName)
        {
            this.RuleName = ruleName;
        }
    }
}
