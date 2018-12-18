using System;
using System.Threading.Tasks;
using ASample.SignalRNet.WebSite.Core;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ASample.SignalRNet.WebSite.Startup))]

namespace ASample.SignalRNet.WebSite
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR<MyConnection>("/MyConnection");
            //app.MapSignalR();
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
