using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Ctrip.Resources
{
    /// <summary>
    /// 对资源文件的访问
    /// resgen D:\Msg.en.resx /output D:\ /str:C#,Ctrip.Selected.Web.Resources,Msg,Msg.cs /publicclass
    /// </summary>
    public sealed class RM
    {
        private const string c_English= "en";
        private const string c_Chinese = "zh-cn";
        private const string c_Assembly = "Ctrip.Selected.Web.Resources";
        private const string c_Enamespace = "Ctrip.Selected.Web.Resources";
        private static Dictionary<String, ResourceManager> c_Resource = new Dictionary<String, ResourceManager>();

        private ResourceManager rm = null;
        
        public RM()
        {
            rm = GetResourceManager("Msg");
            SetCultureInfo();
        }
      
        private static ResourceManager GetResourceManager(String resourceFileName)
        {
            if (!c_Resource.ContainsKey(resourceFileName))
            {
                c_Resource[resourceFileName] = CreateResourceManager(resourceFileName);
            }

            return (ResourceManager)c_Resource[resourceFileName];
        }

        private static ResourceManager CreateResourceManager(String resourceFileName)
        {
            ResourceManager r = null;
            System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(c_Assembly);
            r = new ResourceManager(c_Enamespace + "." + resourceFileName, assembly);
            r.IgnoreCase = true;
            return r;
        }

        private void SetCultureInfo()
        {
            String cultureInfoString = c_Chinese;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfoString);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureInfoString);
        }

        public String this[string resourceName]
        {
            get
            {
                return GetString(resourceName);
            }
        }

        public String GetString(String resourceName)
        {
            String rst = "";

            if (resourceName == "")
            {
                return rst;
            }
            
            resourceName = resourceName.ToUpper();

            try
            {
                rst = rm.GetString(resourceName);
            }
            catch
            {                
                rst = resourceName.ToUpper();
            }

            return rst;
        }
    }
}
