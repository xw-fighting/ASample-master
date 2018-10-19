using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASample.WebSite.Startup))]
namespace ASample.WebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //UnityConfig(app);
        }
    }
}
