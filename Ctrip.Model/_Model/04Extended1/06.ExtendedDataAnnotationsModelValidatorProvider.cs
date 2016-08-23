using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// 自定义ModelValidatorProvider在验证之前将不匹配Validator移除
    /// </summary>
    public class ExtendedDataAnnotationsModelValidatorProvider : DataAnnotationsModelValidatorProvider
    {
        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            var validators = attributes.OfType<ValidatorBase2Attribute>();
            var allAttributes = attributes.Except(validators).ToList();
            foreach (ValidatorBase2Attribute validator in validators)
            {
                if (validator.Match(ValidatorContext.Current, validators))
                {
                    allAttributes.Add(validator);
                }
            }
            return base.GetValidators(metadata, context, allAttributes);
        }
    }
}
