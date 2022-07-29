using Microsoft.Owin.Hosting;
using SelfHostedAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement_Service
{
    [RunInstaller(true)]
    public partial class HostingService : ServiceBase
    {

        public HostingService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            SelfHostedAgent selfHostedAPI = new SelfHostedAgent();
            selfHostedAPI.StartAgent();
        }

        protected override void OnStop()
        {
            
        }
    }
}
