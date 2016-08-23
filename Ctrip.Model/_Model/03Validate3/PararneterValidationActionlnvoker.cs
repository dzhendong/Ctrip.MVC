using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace Ctrip.Model
{
    public class ParameterValidationActionlnvoker : ControllerActionInvoker
    {
        protected override object GetParameterValue(ControllerContext controllerContext, ParameterDescriptor parameterDescriptor)
        {
            try
            {
                controllerContext.RouteData.DataTokens.Add("ParameterDescriptor", parameterDescriptor);
                return base.GetParameterValue(controllerContext, parameterDescriptor);
            }
            finally
            {
                controllerContext.RouteData.DataTokens.Remove("ParameterDescriptor");
            }
        }
    }

    public class ParameterValidationAsyncActionlnvoker : AsyncControllerActionInvoker
    {
        protected override object GetParameterValue(ControllerContext controllerContext, ParameterDescriptor parameterDescriptor)
        {
            try
            {
                controllerContext.RouteData.DataTokens.Add("ParameterDescriptor", parameterDescriptor);
                return base.GetParameterValue(controllerContext, parameterDescriptor);
            }
            finally
            {
                controllerContext.RouteData.DataTokens.Remove("ParameterDescriptor");
            }
        }
    }

    public class ParameterValidationModelValidatorProvider : DataAnnotationsModelValidatorProvider
    {
        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
           return null;          
        }
    }
}
