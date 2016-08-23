using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// 元数据信息
    /// </summary>
    public class ModelMetadataInfo  
    {
        public ModelMetadata ModelMetadata { get; private set; }
        public Expression<Func<ModelMetadata, object>>[] PropertyAccessors { get; private set; }

        public ModelMetadataInfo(Type modelType, params Expression<Func<ModelMetadata, object>>[] propertyAccessors)
        {
            this.ModelMetadata = new ModelMetadata(ModelMetadataProviders.Current, null, null, modelType, null);
            this.PropertyAccessors = propertyAccessors;
        }
    }
}
