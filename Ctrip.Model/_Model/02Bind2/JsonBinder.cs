using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Ctrip.Model
{
    /// <summary> 
    /// Json数据绑定类 
    /// </summary> 
    /// <typeparam name="T"></typeparam>     
    public class JsonBinder<T> : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            StreamReader reader = new StreamReader(controllerContext.HttpContext.Request.InputStream);
            string json = reader.ReadToEnd();

            if (string.IsNullOrEmpty(json))
                return json;

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            object jsonData = serializer.DeserializeObject(json);
            return serializer.Deserialize<T>(json);
        }
    }
}