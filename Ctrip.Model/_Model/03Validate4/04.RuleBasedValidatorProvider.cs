using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ctrip.Model._Model._03Validate4
{
    /// <summary>
    /// 对于应用在同一个属性或者类型上的多个基于不同验证规则的ValidatorAttribute
    /// 对应的验证规则名称并没有应用到具体的验证逻辑中
    /// 所有的基于不同规则的RangeValidatorAttribute都还参与到最终的Model验证过程中
    /// 是在根据验证特性创建ModelValidator的时候只选择那些与当前验证规则一直的ValidatorAttribute
    /// Application_Start
    /// DataAnnotationsModelValidatorProvider validator = ModelValidatorProviders.Providers.OfType<DataAnnotationsModelValidatorProvider>().FirstOrDefault();
    /// if(null != validator){ModelValidatorProviders.Providers.Remove(validator);}
    /// ModelValidatorProviders.Providers.Add(new RuleBasedValidatorProvider());
    /// </summary>
    public class RuleBasedValidatorProvider : DataAnnotationsModelValidatorProvider
    {
        protected override IEnumerable<ModelValidator> GetValidators(
            ModelMetadata metadata, ControllerContext context,
            IEnumerable<Attribute> attributes)
        {
            object validationRuleName = string.Empty;
            context.RouteData.DataTokens.TryGetValue("ValidationRuleName",out validationRuleName);

            string ruleName = validationRuleName.ToString();
            attributes = this.FilterAttributes(attributes ,ruleName);
            return base.GetValidators(metadata, context , attributes);
        }

        private IEnumerable<Attribute> FilterAttributes(
            IEnumerable<Attribute> attributes, string validationRule)
        {
            var validatorAttributes = attributes.OfType<ValidatorAttribute>();
            var nonValidatorAttributes = attributes.Except(validatorAttributes);

            List<ValidatorAttribute> validValidatorAttributes =new List<ValidatorAttribute>();

            if (string.IsNullOrEmpty(validationRule))
            {
                validValidatorAttributes.AddRange(validatorAttributes.Where(v => string.IsNullOrEmpty(v.RuleName)));
            }
            else
            {
                var groups = from validator in validatorAttributes
                             group validator by validator.GetType();

                foreach (var group in groups)
                { 
                    ValidatorAttribute validatorAttribute = group.Where(v => string.Compare(v.RuleName , validationRule , true) == 0) .FirstOrDefault();

                    if (null != validatorAttribute)
                    {
                        validValidatorAttributes.Add(validatorAttribute);
                    }
                    else
                    {
                        validatorAttribute = group.Where(
                            v => string.IsNullOrEmpty(v.RuleName)) .FirstOrDefault();

                        if (null != validatorAttribute)
                        {
                            validValidatorAttributes.Add(validatorAttribute);
                        }
                    }
                }
            }

            return nonValidatorAttributes.Union(validValidatorAttributes);
        }
    }
}
