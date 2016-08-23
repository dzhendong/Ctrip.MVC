using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// Application_Start
    /// var provider = ModelValidatorProviders.Providers.OfType<DataAnnotationsModelValidatorProvider>().FirstOrDefault();
    /// if (null != provider){ModelValidatorProviders.Providers.Remove(provider);}
    /// ModelValidatorProviders.Providers.Add(new ExtendedDataAnnotationsModelValidatorProvider());
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class RequiredValidator2Attribute : ValidatorBase2Attribute
   {
        public RequiredValidator2Attribute(string messageCategory, string messageId, params object[] args)
           : base(messageCategory, messageId, args)
       { }   
    
       public override bool IsValid(object value)
       {
           if (value == null)
           {
               return false;
           }
           string str = value as string;
           if (str != null)
           {
               return (str.Trim().Length != 0);
           }
           return true;
       }
    
       public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
       {
           return new ModelClientValidationRequiredRule[] { new ModelClientValidationRequiredRule(this.ErrorMessageString) };
       }
   }
}
