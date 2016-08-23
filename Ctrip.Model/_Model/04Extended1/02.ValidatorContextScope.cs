using System;
using System.Globalization;

namespace Ctrip.Model
{
    /// <summary>
    /// ValidatorContextScope对象用于设置ValidatorContext的作用范围
    /// </summary>
    public class ValidatorContextScope : IDisposable
    {
        private ValidatorContext current = ValidatorContext.Current;

        public ValidatorContextScope(string ruleName, CultureInfo culture = null)
        {
            ValidatorContext.Current = new ValidatorContext(ruleName, culture);
        }

        public void Dispose()
        {
           if (null == current)
           {
               foreach (object property in ValidatorContext.Current.Properties.Values)
               {
                   IDisposable disposable = property as IDisposable;
                   if (null != disposable)
                   {
                       disposable.Dispose();
                   }
               }
           }
           ValidatorContext.Current = current;
       }
   }
}
