using Ctrip.Test.Unity;
using System;
using System.Threading;

namespace Ctrip.Controllers.Controllers
{
    public partial class RechargeDomian
    {
        private static int _isSucced;

        public static string PaySuccess(int demoz)
        {
            int issleep = 0;
            LogMessage.WriteLog("ThreadId=", Thread.CurrentThread.ManagedThreadId.ToString() + "|=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            LogMessage.WriteLog("IsSucced-1", _isSucced.ToString() + "|=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            int d = Interlocked.Exchange(ref _isSucced, 1);  //_isSucced=1;
            LogMessage.WriteLog("Exchange-1", d.ToString() + "|_isSucced=" + _isSucced.ToString() + "|=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));

            if (d != 0)
            {
                LogMessage.WriteLog("休眠", "休眠一秒钟,issleep=" + issleep + "|=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
                issleep = 1;
                Thread.Sleep(1000);
            }
            //执行

            LogMessage.WriteLog("zhixing-1", "执行issleep" + issleep.ToString() + "|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));

            Interlocked.Exchange(ref _isSucced, 0);  //返回1

            LogMessage.WriteLog("IsSucced-2", _isSucced.ToString() + "|=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            return "";
        }
    }
}
