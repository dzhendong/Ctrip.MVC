using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// 以当前验证规则验证规则为核心的ValidatorContext需要在Action操作之前设置
    /// （严格地说应该在进行Model绑定之前）
    /// 而在Action操作完成后清除。
    /// 很自然地，
    /// 我们可以通过自定义ActionInvoker来完成，
    /// 为此我定义了如下一个直接继承自ControllerActionInvoker的ExtendedControllerActionInvoker类。
    /// </summary>
    public class ExtendedControllerActionInvoker : ControllerActionInvoker
    {
        public ExtendedControllerActionInvoker()
        {
            this.CurrentCultureAccessor = (context =>
            {
                string culture = context.RouteData.GetRequiredString("culture");
                if (string.IsNullOrEmpty(culture))
                {
                    return null;
                }
                else
                {
                    return new CultureInfo(culture);
                }
            });
        }

        public virtual Func<ControllerContext, CultureInfo> CurrentCultureAccessor { get; set; }

        /// <summary>
        /// 在重写的InvokeAction方法中
        /// 我们通过ControllerDescriptor/ActionDescriptor
        /// 得到应用在Controller类型/Action方法上的ValidationRuleAttribute特性
        /// 并或者到设置的验证规则名称
        /// 然后我们创建ValidatorContextScope对象
        /// 针对基类InvokeAction方法的执行就在该ValidatorContextScope中执行的
        /// 我们还对当前线程的Culture进行了相应地设置，
        /// 默认的Culture 信息来源于当前RouteData
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public override bool InvokeAction(ControllerContext controllerContext, string actionName)
        {
            CultureInfo originalCulture = CultureInfo.CurrentCulture;
            CultureInfo originalUICulture = CultureInfo.CurrentUICulture;

            try
            {
                CultureInfo culture = this.CurrentCultureAccessor(controllerContext);
                if (null != culture)
                {
                    Thread.CurrentThread.CurrentCulture = culture;
                    Thread.CurrentThread.CurrentUICulture = culture;
                }

                var controllerDescriptor = this.GetControllerDescriptor(controllerContext);
                var actionDescriptor = this.FindAction(controllerContext, controllerDescriptor, actionName);
                ValidationRule2Attribute attribute = actionDescriptor.GetCustomAttributes(true).OfType<ValidationRule2Attribute>().FirstOrDefault() as ValidationRule2Attribute;

                if (null == attribute)
                {
                    attribute = controllerDescriptor.GetCustomAttributes(true).OfType<ValidationRule2Attribute>().FirstOrDefault() as ValidationRule2Attribute;
                }

                string ruleName = (null == attribute) ? string.Empty : attribute.Name;
                using (ValidatorContextScope contextScope = new ValidatorContextScope(ruleName))
                {
                    return base.InvokeAction(controllerContext, actionName);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = originalCulture;
                Thread.CurrentThread.CurrentUICulture = originalUICulture;
            }
        }
    }
}
