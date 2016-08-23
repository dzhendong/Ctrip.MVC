using System;
using System.Globalization;
using System.Threading;

namespace Ctrip.Resources
{
    #if DEBUG
    class Program
    {
        public static void Main()
        {
            String cultureInfoString = "zh-cn";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfoString);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureInfoString);

            //RM rm = new RM();
            //string str = rm["B.121"];     
       
            //string str = Msg.B_107;
            string str = UI.Name;
            Console.WriteLine(str);          
        }
    }
    #endif
}
