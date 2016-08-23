using System;

namespace Ctrip.Model
{
    /// <summary>
    /// 通过自定义ActionInvoker在进行操作执行之前初始化上下文
    /// </summary>
    [AttributeUsage( AttributeTargets.Class| AttributeTargets.Method)]
    public class ValidationRule2Attribute:Attribute
    {
        public string Name { get; private set; }

        public ValidationRule2Attribute(string name)
        {
            this.Name = name;
        }
    }
}
