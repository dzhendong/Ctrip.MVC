using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Ctrip.Model
{
    /// <summary>
    /// 为了演示针对简单数据类型的Model 绑定
    /// 我们自定义了如下一个DefaultModeffiinder ，
    /// 它是对ASP.NETMVC 默认使用的DefaultModeffiinder 的模拟
    /// </summary>
    public class DefaultModelBinder1 : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return this.GetModel(controllerContext, bindingContext.ModelType, bindingContext.ValueProvider, bindingContext.ModelName);
        }

        #region 简单类型
       
        //public object GetModel(ControllerContext controllerContext, Type modelType,IValueProvider valueProvider, string key)
        //{
        //    if (!valueProvider.ContainsPrefix(key))
        //    {
        //        return null;
        //    }

        //    return valueProvider.GetValue(key).ConvertTo(modelType);
        //}
        #endregion

        #region 复杂类型
        private object CreateModel(Type modelType)
        {
            Type type = modelType;
                
            if (modelType.IsGenericType)
            { 
                Type genericTypeDefintion = modelType.GetGenericTypeDefinition () ;

                if (genericTypeDefintion == typeof(IDictionary< ,>))
                {
                    type = typeof(Dictionary<,>).MakeGenericType(modelType.GetGenericArguments());
                }
                else if (((genericTypeDefintion == typeof(IEnumerable<>))
                    || (genericTypeDefintion == typeof(ICollection<>)))
                    || (genericTypeDefintion == typeof(IList<>)))
                {
                    type = typeof(List<>) .MakeGenericType(modelType.GetGenericArguments());
                }                
            }

            return Activator.CreateInstance(type);
        }

        protected virtual object GetComplexModel(ControllerContext controllerContext,Type modelType, IValueProvider valueProvider, string prefix)
        {
            object model = CreateModel(modelType);
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(modelType))
            {
                if (property.IsReadOnly)
                {
                    continue;
                }
                string key = string.IsNullOrEmpty(prefix) ? property.Name : prefix + "." + property.Name;

                property.SetValue(model, GetModel(controllerContext,property.PropertyType, valueProvider, key));
            }

            return model;
        }

        public virtual object GetModel(ControllerContext controllerContext, Type modelType, IValueProvider valueProvider, string key)
        {
            if (!valueProvider.ContainsPrefix(key))
            {
                return null;
            }

            ModelMetadata modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, modelType);

            if (!modelMetadata.IsComplexType)
            {
                return valueProvider.GetValue(key).ConvertTo(modelType);
            }

            if (modelMetadata.IsComplexType)
            {
                return GetComplexModel(controllerContext, modelType, valueProvider,key);
            }

            return null;
        }
        #endregion
    }
}
