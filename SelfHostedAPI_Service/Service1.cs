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

namespace SelfHostedAPI_Service
{
    [RunInstaller(true)]
    public partial class Service1 : ServiceBase
    {
        private IDisposable app;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string baseAddress = "http://localhost:9000/";
            app = WebApp.Start<Startup>(url: baseAddress);
        }

        protected override void OnStop()
        {
            app.Dispose();  
        }
    }
}
