using System;
using System.Globalization;
using System.Resources;
using System.Threading;    

namespace Ctrip.Resources 
{            
    public class Msg 
    {        
        private static ResourceManager resourceMan;        
        private static CultureInfo resourceCulture;
     
        public Msg()
        {                        
        }
              
        public static ResourceManager ResourceManager
        {
            get 
            {
                if (object.ReferenceEquals(resourceMan, null))
                {                    
                    ResourceManager temp = new ResourceManager("Ctrip.Resources.Msg", typeof(Msg).Assembly);
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
               
        public static string B_107
        {
            get {
                return ResourceManager.GetString("B.107", resourceCulture);               
            }
        }
               
        public static string B_121
        {
            get {
                return ResourceManager.GetString("B.121", resourceCulture);
            }
        }
              
        public static string B_122
        {
            get {
                return ResourceManager.GetString("B.122", resourceCulture);
            }
        }
              
        public static string B_123
        {
            get {
                return ResourceManager.GetString("B.123", resourceCulture);
            }
        }
              
        public static string B_124
        {
            get {
                return ResourceManager.GetString("B.124", resourceCulture);
            }
        }
             
        public static string B_125
        {
            get {
                return ResourceManager.GetString("B.125", resourceCulture);
            }
        }

        public static string B_200
        {
            get
            {
                return ResourceManager.GetString("B.200", resourceCulture);
            }
        }

        public static string B_201
        {
            get
            {
                return ResourceManager.GetString("B.201", resourceCulture);
            }
        }
    }
}
