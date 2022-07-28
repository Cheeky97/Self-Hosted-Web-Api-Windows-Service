using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostedAPI
{
    public class SelfHostedAgent
    {
        static readonly string baseAddress = ConfigurationManager.AppSettings["baseAddress"];
        public void StartAgent()
        {
            WebApp.Start<Startup>(url: baseAddress);
            Logger.WriteLog($"The Windows Service has been Started and the url : {baseAddress}");
        }
        public void StopAgent()
        {
            Logger.WriteLog("The Windows Service has been Stopped");
        }
    }
}
