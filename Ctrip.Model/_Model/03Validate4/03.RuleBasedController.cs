using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ctrip.Model
{
    public class RuleBasedController : Controller
    {
        private static Dictionary<Type , ControllerDescriptor>controllerDescriptors = new Dictionary<Type , ControllerDescriptor>();

        public ControllerDescriptor ControllerDescriptor
        {
            get 
            {
                ControllerDescriptor controllerDescriptor;

                if (controllerDescriptors.TryGetValue(this.GetType(),out controllerDescriptor))
                { 
                    return controllerDescriptor;
                }

                lock (controllerDescriptors)
                {
                    if (!controllerDescriptors.TryGetValue(this.GetType(), out controllerDescriptor))
                    {
                        controllerDescriptor =new ReflectedControllerDescriptor(this.GetType());controllerDescriptors.Add(this.GetType(),controllerDescriptor);
                    }

                    return controllerDescriptor;
                }
            }
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            SetValidationRule();
            return base.BeginExecuteCore(callback,state);
        }

        protected override void ExecuteCore()
        {
            SetValidationRule();
            base.ExecuteCore();
        }

        /// <summary>
        /// 方法SetValidationRule方法被调用
        /// 将应用在当前Action方法或者Controller类型上的ValidationRuleAttribute特性
        /// 指定的验证规则名称保存到当前Controller上下文中
        /// </summary>
        private void SetValidationRule()
        {
            string actionName = this.ControllerContext.RouteData.GetRequiredString("action");

            ActionDescriptor actionDescriptor = this.ControllerDescriptor.FindAction (this.ControllerContext , actionName);

            if (null != actionDescriptor)
            { 
                ValidationRuleAttribute rule =
                    actionDescriptor.GetCustomAttributes(true).OfType<ValidationRuleAttribute>() .FirstOrDefault() ??
                    this.ControllerDescriptor.GetCustomAttributes(true).OfType<ValidationRuleAttribute>() .FirstOrDefault() ??
                new ValidationRuleAttribute(string.Empty);

                this.ControllerContext.RouteData.DataTokens.Add("ValidationRuleName", rule.RuleName);
            }

        }
    }
}
