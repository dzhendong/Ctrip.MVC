using System.ComponentModel.DataAnnotations;

namespace Ctrip.Model
{
    /// <summary>
    /// 自定义验证特性类
    /// </summary>
    public class MustBeTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is bool && (bool)value;
        }
    }
}