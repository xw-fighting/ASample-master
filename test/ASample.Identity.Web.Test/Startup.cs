using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASample.Identity.Web.Test.Startup))]
namespace ASample.Identity.Web.Test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
