using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Ctrip.Model
{
    public class FieldTemplateMetadata : DataAnnotationsModelMetadata
    {
        public FieldTemplateMetadata(DisplayAttribute aa, DataAnnotationsModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName, DisplayColumnAttribute displayColumnAttribute, IEnumerable<Attribute> attributes)
            : base(provider, containerType, modelAccessor, modelType, propertyName, displayColumnAttribute)
        {
            Attributes = new List<Attribute>(attributes);
            Display = aa;
        }

        public IList<Attribute> Attributes
        {
            get;
            private set;
        }
        public DisplayAttribute Display { get; set; }
    }
}
