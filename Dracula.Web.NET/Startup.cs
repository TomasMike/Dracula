using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Dracula.Web.NET.Startup))]
namespace Dracula.Web.NET
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR(new HubConfiguration() {EnableDetailedErrors = true });
        }
    }
}
