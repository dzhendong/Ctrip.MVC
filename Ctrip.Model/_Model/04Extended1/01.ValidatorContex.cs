using System;
using System.Collections.Generic;
using System.Globalization;

namespace Ctrip.Model
{
    /// <summary>
    /// 基于某个规则的验证
    /// 为验证创建一个上下文：ValidatorContext
    /// </summary>
    public class ValidatorContext
    {
        [ThreadStatic]
        private static ValidatorContext current;
     
        /// <summary>
        /// 表示当前的验证规则
        /// </summary>
        public string RuleName { get; private set; }

        /// <summary>
        /// 语言文化
        /// </summary>
        public CultureInfo Culture { get; private set; }

        /// <summary>
        /// 字典类型的属性Properties用户存放一些额外信息
        /// </summary>
        public IDictionary<string, object> Properties { get; private set; }
     
        public ValidatorContext(string ruleName, CultureInfo culture=null)
        {
            this.RuleName = ruleName;
            this.Properties = new Dictionary<string, object>();
            this.Culture = culture??CultureInfo.CurrentUICulture;
        }
  
        public static ValidatorContext Current
        {
            get { return current; }
            set { current = value; }
        }
    }
}
