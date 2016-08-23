using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Ctrip.Test.Unity
{
    public class LogMessage
    {
        public static void WriteLog(string spcId, string msg)
        {
            try
            {
                lock (msg)
                {
                    Process currentProcess = Process.GetCurrentProcess();
                    string text = string.Format("{0}{1}", LogMessage.GetCurFolder(), "entsyslog");
                    if (!Directory.Exists(text))
                    {
                        Directory.CreateDirectory(text);
                    }
                    text = string.Format("{0}/{1}", text, DateTime.Now.ToString("yyyyMMdd"));
                    if (!Directory.Exists(text))
                    {
                        Directory.CreateDirectory(text);
                    }
                    string path = string.Format("{0}/{1}{2}", text, DateTime.Now.ToString("yyyyMMddHH"), ".log");
                    StreamWriter streamWriter = new StreamWriter(path, true);
                    streamWriter.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t\n", new object[]
					{
						DateTime.Now,
						currentProcess.Id,
						currentProcess.MachineName,
						currentProcess.ProcessName,
						spcId,
						msg
					});
                    streamWriter.Close();
                }
            }
            catch
            {
            }
        }
       
        public static void WriteLog_ForLinux(string spcId, string msg)
        {
            try
            {
                lock (msg)
                {
                    Process currentProcess = Process.GetCurrentProcess();
                    string text = string.Format("{0}{1}", LogMessage.GetCurFolderForLinux(), "entsyslog");
                    if (!Directory.Exists(text))
                    {
                        Directory.CreateDirectory(text);
                    }
                    text = string.Format("{0}/{1}", text, DateTime.Now.ToString("yyyyMMdd"));
                    if (!Directory.Exists(text))
                    {
                        Directory.CreateDirectory(text);
                    }
                    string path = string.Format("{0}/{1}{2}", text, DateTime.Now.ToString("yyyyMMddHH"), ".log");
                    StreamWriter streamWriter = new StreamWriter(path, true);
                    streamWriter.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t\n", new object[]
					{
						DateTime.Now,
						currentProcess.Id,
						currentProcess.MachineName,
						currentProcess.ProcessName,
						spcId,
						msg
					});
                    streamWriter.Close();
                }
            }
            catch
            {
            }
        }
        
        private static string GetCurFolder()
        {
            string text = Assembly.GetExecutingAssembly().CodeBase;
            text = text.Substring(8, text.Length - 8);
            string[] array = text.Split(new char[]
			{
				'/'
			});
            string text2 = "";
            for (int i = 0; i < array.Length - 1; i++)
            {
                text2 = text2 + array[i] + "/";
            }
            return text2;
        }
        
        private static string GetCurFolderForLinux()
        {
            return HttpContext.Current.Server.MapPath("~/bin/");
        }
    }
}
