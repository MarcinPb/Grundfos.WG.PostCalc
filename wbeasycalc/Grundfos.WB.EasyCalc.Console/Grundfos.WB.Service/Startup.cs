using System;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Console;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Grundfos.WB.Service.Startup))]

namespace Grundfos.WB.Service
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            GlobalConfiguration.Configuration.UseSqlServerStorage("wbjobs");
            GlobalConfiguration.Configuration.UseConsole();

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
