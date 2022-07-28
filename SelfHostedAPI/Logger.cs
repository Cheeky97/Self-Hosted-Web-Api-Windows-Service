using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostedAPI
{
    public static class Logger
    {
        static readonly string logPath = ConfigurationManager.AppSettings["logFile"];

        public static void WriteLog(string message)
        {
            using(StreamWriter sw = new StreamWriter(@logPath,true))
            {
                sw.WriteLine($"{DateTime.Now} : {message}");
            }
        }
    }
}
