using System;
using System.Reflection;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// 资源
    /// 属性的名称就是资源的名称
    /// Application Start
    /// DisplayTextAttribute.SetResourceType(typeof(Resources)};
    /// </summary>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Property)]
    public class DisplayTextAttribute : Attribute, IMetadataAware
    {
        private static Type staticResourceType;
        public string DisplayName { get; set; }
        public Type ResourceType { get; set; }

        public DisplayTextAttribute()
        {
            this.ResourceType = staticResourceType;
        }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            this.DisplayName = this.DisplayName ?? (metadata.PropertyName ?? metadata.ModelType.Name);
            if (null == this.ResourceType)
            { 
                metadata.DisplayName = this.DisplayName;
                return;
            }

            PropertyInfo property = this.ResourceType.GetProperty(this.DisplayName,
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            metadata.DisplayName = property.GetValue(null , null) .ToString();
        }

        public static void SetResourceType(Type resourceType)
        {
            staticResourceType = resourceType;
        }

        public void SetDisplayName(ModelMetadata rnetadata)
        {
            this.DisplayName = this.DisplayName ?? (rnetadata.PropertyName ?? rnetadata.ModelType.Name);
            if (null == this.ResourceType)
            {
                rnetadata.DisplayName = this.DisplayName;
                return;
            }

            PropertyInfo property = this.ResourceType.GetProperty(this.DisplayName,
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);

            rnetadata.DisplayName = property.GetValue(null, null).ToString();
        }
    }
}
