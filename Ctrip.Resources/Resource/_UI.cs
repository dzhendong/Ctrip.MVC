using System;
using System.Globalization;
using System.Resources;
using System.Threading;    

namespace Ctrip.Resources 
{            
    public class UI 
    {        
        private static ResourceManager resourceMan;        
        private static CultureInfo resourceCulture;

        public UI()
        {                        
        }
    
        public static ResourceManager ResourceManager
        {
            get 
            {
                if (object.ReferenceEquals(resourceMan, null))
                {                    
                    ResourceManager temp = new ResourceManager("Ctrip.Resources.UI", typeof(UI).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
            
        public static CultureInfo Culture
        {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
           
        public static string Name
        {
            get {
                return ResourceManager.GetString("Name", resourceCulture);               
            }
        }
    }
}
