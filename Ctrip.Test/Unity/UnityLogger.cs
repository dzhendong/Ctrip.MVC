using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ctrip.Test
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    public interface ILogger
    {
        void Write(string message);
        string Read();
    }

    public class FlatFileLogger : ILogger
    {
        public void Write(string message)
        {
            Console.WriteLine(String.Format("Message:{0}", message));
            Console.WriteLine("Target:FlatFile");
        }

        public string Read()
        {
            return "FlatFileLogger";
        }
    }

    public class DatabaseLogger : ILogger
    {
        public void Write(string message)
        {
            Console.WriteLine(String.Format("Message:{0}", message));
            Console.WriteLine("Target:Database");
        }

        public string Read()
        {
            return "DatabaseLogger";
        }
    }

}