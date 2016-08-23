using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// 通过自定义ModelMetadataProvider 定制Model 元数据
    /// Application_Start
    /// DisplayTextAttribute.SetResourceType(typeof(Resources}};
    /// ModelMetadataProviders.Current = new ExtendedDataAnnotationsProvider(};
    /// </summary>
    public class ExtendedDataAnnotationsProvider : CachedDataAnnotationsModelMetadataProvider
    {
        protected override CachedDataAnnotationsModelMetadata
            CreateMetadataPrototype(IEnumerable<Attribute> attributes,
            Type containerType, Type rnodelType, string propertyNarne)
        {
            CachedDataAnnotationsModelMetadata rnodelMetadata =
                base.CreateMetadataPrototype(attributes, containerType, rnodelType,
                propertyNarne);

            if (string.IsNullOrEmpty(rnodelMetadata.DisplayName))
            {
                DisplayTextAttribute displayTextAttribute = attributes
                    .OfType<DisplayTextAttribute>().FirstOrDefault();

                if (null != displayTextAttribute)
                {
                    displayTextAttribute.SetDisplayName(rnodelMetadata);
                }
            }

            return rnodelMetadata;
        }

        protected override CachedDataAnnotationsModelMetadata CreateMetadataFromPrototype(
            CachedDataAnnotationsModelMetadata prototype,
            Func<object> modelAccessor)
        {
                CachedDataAnnotationsModelMetadata modelMetadata =
                base.CreateMetadataFromPrototype(prototype , modelAccessor);

                modelMetadata.DisplayName = prototype.DisplayName;
                return modelMetadata;
        }
    }
}
